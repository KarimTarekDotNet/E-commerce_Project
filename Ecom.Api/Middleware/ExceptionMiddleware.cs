using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _envitoment;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment envitoment, IMemoryCache cache)
        {
            _next = next;
            _envitoment = envitoment;
            _cache = cache;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurityHeaders(context);
                if (IsRequestAllow(context) == false)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var response
                        = new ExceptionAPI((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");

                    var jsonResponse = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsJsonAsync(jsonResponse);
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _envitoment.IsDevelopment() ?
                    new ExceptionAPI((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!)
                    : new ExceptionAPI((int)HttpStatusCode.InternalServerError, ex.Message);

                var jsonResponse = JsonSerializer.Serialize(response);

                await context.Response.WriteAsJsonAsync(jsonResponse);
            }
        }

        private bool IsRequestAllow(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var cachKey = $"BlockedIP-{ip}";
            var dateNow = DateTime.UtcNow;

            var (timesTamp, count) = _cache.GetOrCreate(cachKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (time: dateNow, count: 0);
            });
            if(dateNow - timesTamp < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;

                }
                else
                {
                    _cache.Set(cachKey, (timesTamp, count += 1), _rateLimitWindow);
                    return true;
                }
            }
            else
            {
                _cache.Set(cachKey, (timesTamp, count), _rateLimitWindow);
            }
            return true;
        }

        private void ApplySecurityHeaders(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}