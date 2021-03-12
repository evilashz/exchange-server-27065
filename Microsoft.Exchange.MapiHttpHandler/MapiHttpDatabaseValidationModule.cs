using System;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001C RID: 28
	public class MapiHttpDatabaseValidationModule : MapiHttpModule
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00007667 File Offset: 0x00005867
		internal override void InitializeModule(HttpApplication application)
		{
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(MapiHttpContextWrapper.GetWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007710 File Offset: 0x00005910
		internal override void OnPostAuthorizeRequest(HttpContextBase context)
		{
			if (string.Equals(context.Request.RequestType, "POST", StringComparison.OrdinalIgnoreCase))
			{
				string text = context.Request.Headers["X-RequestType"];
				if (!string.IsNullOrEmpty(text) && !string.Equals(text, "Connect", StringComparison.OrdinalIgnoreCase) && !string.Equals(text, "EcDoConnectEx", StringComparison.OrdinalIgnoreCase) && !string.Equals(text, "Bind", StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}
			HttpDatabaseValidationHelper.ValidateHttpDatabaseHeader(context, delegate
			{
			}, delegate(string routingError)
			{
				this.SendErrorResponse(context, 555, 0, routingError, delegate(HttpResponseBase response)
				{
					response.Headers[WellKnownHeader.BEServerRoutingError] = routingError;
				});
			}, delegate
			{
				this.SendErrorResponse(context, 400, 0, "Invalid database guid");
			});
		}
	}
}
