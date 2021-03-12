using System;
using System.Web;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000256 RID: 598
	public sealed class OwaServiceHttpHandlerFactory : HttpHandlerFactoryBase<OWAService>
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x00053348 File Offset: 0x00051548
		internal override string SelectOperation(string url, HttpRequest httpRequest, string requestType)
		{
			string text = httpRequest.Headers["Action"];
			if (string.IsNullOrEmpty(text))
			{
				string path = httpRequest.Path;
				if (!string.IsNullOrEmpty(path))
				{
					string[] array = path.Split(new char[]
					{
						'/'
					});
					if (array != null && array.Length > 0)
					{
						text = array[array.Length - 1];
					}
				}
			}
			return text;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000533A4 File Offset: 0x000515A4
		internal override bool UseHttpHandlerFactory(HttpContext httpContext)
		{
			if (OwaServiceHttpHandlerFactory.FlightEnableOverride.Member)
			{
				return true;
			}
			if (Globals.IsAnonymousCalendarApp)
			{
				return false;
			}
			if (EsoRequest.IsEsoRequest(httpContext.Request))
			{
				return false;
			}
			UserContext userContext = UserContextManager.GetMailboxContext(httpContext, null, true) as UserContext;
			return userContext != null && userContext.FeaturesManager.ServerSettings.OwaHttpHandler.Enabled;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00053402 File Offset: 0x00051602
		internal override IHttpAsyncHandler CreateAsyncHttpHandler(HttpContext httpContext, OWAService service, ServiceMethodInfo methodInfo)
		{
			return new OwaServiceHttpAsyncHandler(httpContext, service, methodInfo);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0005340C File Offset: 0x0005160C
		internal override IHttpHandler CreateHttpHandler(HttpContext httpContext, OWAService service, ServiceMethodInfo methodInfo)
		{
			return new OwaServiceHttpHandler(httpContext, service, methodInfo);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00053416 File Offset: 0x00051616
		internal override OWAService CreateServiceInstance()
		{
			return new OWAService();
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0005341D File Offset: 0x0005161D
		internal override bool TryGetServiceMethod(string actionName, out ServiceMethodInfo methodInfo)
		{
			return OwaServiceHttpHandlerFactory.methodMap.Member.TryGetMethodInfo(actionName, out methodInfo);
		}

		// Token: 0x04000C69 RID: 3177
		private static LazyMember<OwaServiceMethodMap> methodMap = new LazyMember<OwaServiceMethodMap>(() => new OwaServiceMethodMap(typeof(OWAService)));

		// Token: 0x04000C6A RID: 3178
		internal static LazyMember<bool> FlightEnableOverride = new LazyMember<bool>(() => BaseApplication.GetAppSetting<bool>("EnableOwaHttpHandler", false));
	}
}
