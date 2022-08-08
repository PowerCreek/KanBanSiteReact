using KanBanAuthpassthru.TypedClients;
using Microsoft.AspNetCore.Mvc;

namespace KanBanAuthpassthru.Implementation
{
    public readonly record struct AuthorizationSlug(string AuthorizationPolicyName);

    public static class MethodIntakeExt
    {
        public static string GetRoute(string inputRoutePrefix = null!)
            => $"/{string.Join('/', new string[] { inputRoutePrefix, "{*rest}" }.Where(e => !string.IsNullOrEmpty(e)))}";

        private static string GetAdjustedPath(string prefix, string? value)
            //value is null -> /
            //prefix is null -> value
            //create adjusted path if prefix and value exist
         => string.IsNullOrEmpty(value) ?
                "/" :
            string.IsNullOrEmpty(prefix) ?
                value :
                $"/{new string(value.Where((c, i) => !char.Equals(char.ToLowerInvariant(prefix.ElementAtOrDefault(i - 1)), char.ToLowerInvariant(c))).SkipWhile(c => c == '/').ToArray())}";


        private delegate RouteHandlerBuilder MapFunc(string pattern, Delegate handler);

        private delegate Func<HttpRequest, CancellationToken, Task<HttpResponseMessage>> ApiCall(ApiClient client);

        private static RouteHandlerBuilder HandleRouteBuilderMapping(MapFunc func, ApiCall call, string prefix = null!)
        {
            return func(GetRoute(inputRoutePrefix: prefix),
                async
                (
                    [FromRoute] string rest,
                    [FromServices] ApiClient client,
                    HttpContext ctx,
                    CancellationToken token
                ) =>
                {
                    try
                    {
                        ctx.Request.Path = GetAdjustedPath(prefix, ctx.Request.Path.Value);
                        var resultTask = call(client)(ctx.Request, token);
                        var result = await resultTask;

                        if (result.IsSuccessStatusCode)
                            await result.Content.CopyToAsync(ctx.Response.BodyWriter.AsStream());
                        else
                            ctx.Response.StatusCode = (int)result.StatusCode;
                    }
                    catch (Exception e)
                    {
                        //can catch cancallations with an explicit exception target.
                    }
                });
        }

        public static WebApplication UseGetPassThru(this WebApplication app, string inputRoutePrefix = null!, AuthorizationSlug authorization = default)
        {
            var routeBuilder = HandleRouteBuilderMapping(app.MapGet, (client) => client.MakeGetCallAsync, prefix: inputRoutePrefix);

            if (authorization is not { AuthorizationPolicyName: null })
                routeBuilder.RequireAuthorization(authorization.AuthorizationPolicyName);

            return app;
        }
        
        public static WebApplication UsePostPassThru(this WebApplication app, string inputRoutePrefix = null!, AuthorizationSlug authorization = default)
        {
            var routeBuilder = HandleRouteBuilderMapping(app.MapPost, (client) => client.MakePostCallAsync, prefix: inputRoutePrefix);

            if (authorization is not { AuthorizationPolicyName: null })
                routeBuilder.RequireAuthorization(authorization.AuthorizationPolicyName);

            return app;
        }

    }
}
