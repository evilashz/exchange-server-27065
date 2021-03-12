using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200010D RID: 269
	internal class OAuthExtensionsManager
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x00039272 File Offset: 0x00037472
		public OAuthExtensionsManager()
		{
			this.oAuthExtensionHandlers = new List<IOAuthExtensionAuthenticationHandler>();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00039285 File Offset: 0x00037485
		public void AppendHandlerToChain(IOAuthExtensionAuthenticationHandler handler)
		{
			if (!this.oAuthExtensionHandlers.Contains(handler))
			{
				this.oAuthExtensionHandlers.Add(handler);
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000392A1 File Offset: 0x000374A1
		public void RemoveHandlerFromChain(IOAuthExtensionAuthenticationHandler handler)
		{
			if (this.oAuthExtensionHandlers.Contains(handler))
			{
				this.oAuthExtensionHandlers.Remove(handler);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000392C0 File Offset: 0x000374C0
		public bool TryHandleRequestPreAuthentication(OAuthExtensionContext context, out bool isAuthenticationNeeded)
		{
			bool flag = false;
			isAuthenticationNeeded = true;
			foreach (IOAuthExtensionAuthenticationHandler ioauthExtensionAuthenticationHandler in this.oAuthExtensionHandlers)
			{
				flag = ioauthExtensionAuthenticationHandler.TryHandleRequestPreAuthentication(context, out isAuthenticationNeeded);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00039320 File Offset: 0x00037520
		public bool TryGetBearerToken(OAuthExtensionContext context, out string token)
		{
			bool flag = false;
			token = null;
			foreach (IOAuthExtensionAuthenticationHandler ioauthExtensionAuthenticationHandler in this.oAuthExtensionHandlers)
			{
				flag = ioauthExtensionAuthenticationHandler.TryGetBearerToken(context, out token);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00039380 File Offset: 0x00037580
		public bool TryHandleRequestPostAuthentication(OAuthExtensionContext context)
		{
			bool flag = false;
			foreach (IOAuthExtensionAuthenticationHandler ioauthExtensionAuthenticationHandler in this.oAuthExtensionHandlers)
			{
				flag = ioauthExtensionAuthenticationHandler.TryHandleRequestPostAuthentication(context);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x040007F5 RID: 2037
		private readonly List<IOAuthExtensionAuthenticationHandler> oAuthExtensionHandlers;
	}
}
