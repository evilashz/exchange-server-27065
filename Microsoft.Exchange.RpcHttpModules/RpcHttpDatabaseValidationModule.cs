using System;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x02000010 RID: 16
	public class RpcHttpDatabaseValidationModule : RpcHttpModule
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002E3A File Offset: 0x0000103A
		internal override void InitializeModule(HttpApplication application)
		{
			application.PostAuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnPostAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F0C File Offset: 0x0000110C
		internal override void OnPostAuthorizeRequest(HttpContextBase context)
		{
			HttpDatabaseValidationHelper.ValidateHttpDatabaseHeader(context, delegate
			{
				if (context.Request.HttpMethod == "HEAD")
				{
					this.Application.CompleteRequest();
				}
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
