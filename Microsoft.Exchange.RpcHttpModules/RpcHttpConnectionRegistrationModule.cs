using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcHttpModules
{
	// Token: 0x0200000F RID: 15
	public class RpcHttpConnectionRegistrationModule : RpcHttpModule
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000027A4 File Offset: 0x000009A4
		public RpcHttpConnectionRegistrationModule() : this(CachedDirectory.DefaultCachedDirectory, new RpcHttpConnectionRegistrationClient())
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027B6 File Offset: 0x000009B6
		internal RpcHttpConnectionRegistrationModule(IDirectory directoryService, IRpcHttpConnectionRegistration rpcHttpConnectionRegistration)
		{
			this.directoryService = directoryService;
			this.rpcHttpConnectionRegistration = rpcHttpConnectionRegistration;
			this.EnsureConnectionRegistrationCacheIsInitialized();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027D4 File Offset: 0x000009D4
		private bool CanTrustEntireForwardedForHeader
		{
			get
			{
				bool flag;
				return bool.TryParse(WebConfigurationManager.AppSettings["TrustEntireForwardedFor"], out flag) && flag;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027FC File Offset: 0x000009FC
		internal static string ExtractAssociationGuid(HttpRequestBase httpRequest)
		{
			string text = httpRequest.Headers[WellKnownHeader.Pragma];
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf("SessionId=", StringComparison.Ordinal);
				if (num >= 0 && num + "SessionId=".Length + 36 <= text.Length)
				{
					return text.Substring(num + "SessionId=".Length, 36);
				}
			}
			return null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002890 File Offset: 0x00000A90
		internal override void InitializeModule(HttpApplication application)
		{
			application.AuthorizeRequest += delegate(object sender, EventArgs args)
			{
				this.OnAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
			application.EndRequest += delegate(object sender, EventArgs args)
			{
				this.OnEndRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
			};
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028B8 File Offset: 0x00000AB8
		internal override void OnAuthorizeRequest(HttpContextBase context)
		{
			HttpRequestBase request = context.Request;
			if (request.HttpMethod != "RPC_OUT_DATA" && request.HttpMethod != "RPC_IN_DATA")
			{
				return;
			}
			string serverTarget = this.GetServerTarget(request);
			if (request.IsSecureConnection)
			{
				CommonAccessToken commonAccessToken = context.Items["Item-CommonAccessToken"] as CommonAccessToken;
				string text;
				bool flag;
				if (commonAccessToken != null)
				{
					text = commonAccessToken.Serialize();
					flag = true;
				}
				else
				{
					text = request.Headers["X-CommonAccessToken"];
					if (string.IsNullOrEmpty(text))
					{
						WindowsIdentity windowsIdentity = context.User.Identity as WindowsIdentity;
						if (windowsIdentity == null)
						{
							return;
						}
						text = new CommonAccessToken(windowsIdentity).Serialize();
						flag = true;
					}
					else
					{
						flag = this.IsTokenSerializationAllowed(context);
					}
				}
				Guid guid;
				Guid.TryParse(RpcHttpConnectionRegistrationModule.ExtractAssociationGuid(request), out guid);
				context.Items["associationGuid"] = guid;
				string text2 = base.GetOutlookSessionId(context) ?? string.Empty;
				string text3 = string.Empty;
				IPAddress ipaddress;
				IPAddress ipaddress2;
				if (GccUtils.GetClientIPEndpointsFromHttpRequest(context, out ipaddress, out ipaddress2, false, this.CanTrustEntireForwardedForHeader) && ipaddress != null && ipaddress != IPAddress.IPv6None)
				{
					text3 = ipaddress.ToString();
				}
				Guid requestId = base.GetRequestId(context);
				if (!flag)
				{
					base.SendErrorResponse(context, 403, "Insufficient permission");
					return;
				}
				if (request.HttpMethod == "RPC_IN_DATA")
				{
					context.SetServerVariable("ShimServerTarget", serverTarget);
					context.SetServerVariable("ShimAccessToken", text);
					context.SetServerVariable("ShimSessionCookie", text2);
					context.SetServerVariable("ShimForwardedFor", text3);
					context.SetServerVariable("ShimRequestId", requestId.ToString("D"));
					return;
				}
				if (request.HttpMethod == "RPC_OUT_DATA" && guid != Guid.Empty && !string.IsNullOrEmpty(serverTarget))
				{
					this.RegisterRequest(context, guid, text, serverTarget, text2, text3, requestId);
					return;
				}
			}
			else if (!string.IsNullOrEmpty(request.Headers["X-CommonAccessToken"]) || !this.IsRequestFromExchangeServer(context))
			{
				base.SendErrorResponse(context, 403, "Insufficient permission");
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AC4 File Offset: 0x00000CC4
		internal override void OnEndRequest(HttpContextBase context)
		{
			if (context.Items.Contains("connectionIsRegistered") && context.Items.Contains("associationGuid"))
			{
				Guid associationGroupId = (Guid)context.Items["associationGuid"];
				Guid requestId = base.GetRequestId(context);
				this.UnregisterRequest(associationGroupId, requestId);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B1C File Offset: 0x00000D1C
		private string GetServerTarget(HttpRequestBase httpRequest)
		{
			string text = httpRequest.Headers[WellKnownHeader.RpcHttpProxyServerTarget];
			if (string.IsNullOrEmpty(text))
			{
				text = new RpcHttpQueryString(httpRequest.Url.Query).RcaServer;
			}
			return text;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B5C File Offset: 0x00000D5C
		private void EnsureConnectionRegistrationCacheIsInitialized()
		{
			if (!RpcHttpConnectionRegistrationModule.haveClearedRegistrationCache)
			{
				lock (RpcHttpConnectionRegistrationModule.ClearCacheLock)
				{
					if (!RpcHttpConnectionRegistrationModule.haveClearedRegistrationCache)
					{
						this.ClearRegistrations();
						RpcHttpConnectionRegistrationModule.haveClearedRegistrationCache = true;
					}
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BB0 File Offset: 0x00000DB0
		private bool IsTokenSerializationAllowed(HttpContextBase httpContext)
		{
			WindowsIdentity windowsIdentity = httpContext.User.Identity as WindowsIdentity;
			return windowsIdentity != null && !(windowsIdentity.User == null) && this.directoryService.AllowsTokenSerializationBy(windowsIdentity);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C0C File Offset: 0x00000E0C
		private bool IsRequestFromExchangeServer(HttpContextBase httpContext)
		{
			bool result = false;
			WindowsIdentity windowsIdentity = (WindowsIdentity)httpContext.User.Identity;
			if (windowsIdentity.User != null && windowsIdentity.User.IsWellKnown(WellKnownSidType.LocalSystemSid))
			{
				result = true;
			}
			else if (windowsIdentity.Groups != null)
			{
				SecurityIdentifier exchangeServersSid = this.directoryService.GetExchangeServersUsgSid();
				if (exchangeServersSid != null)
				{
					if ((from identityReference in windowsIdentity.Groups
					select identityReference as SecurityIdentifier).Any((SecurityIdentifier sid) => sid == exchangeServersSid))
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CB8 File Offset: 0x00000EB8
		private void RegisterRequest(HttpContextBase context, Guid associationGroupId, string token, string serverTarget, string sessionCookie, string forwardedFor, Guid requestId)
		{
			try
			{
				string text = null;
				string value = null;
				int num = this.rpcHttpConnectionRegistration.Register(associationGroupId, token, serverTarget, sessionCookie, forwardedFor, requestId, out text, out value);
				if (num != 0)
				{
					string httpStatusText = string.Format("Connection Registration Failed:{0} ({1})", num, text ?? string.Empty);
					if (!string.IsNullOrEmpty(value))
					{
						context.Items["ExtendedStatus"] = value;
					}
					base.SendErrorResponse(context, 500, num, httpStatusText);
				}
				else
				{
					context.Items["connectionIsRegistered"] = true;
				}
			}
			catch (RpcHttpConnectionRegistrationException ex)
			{
				context.Items["ExtendedStatus"] = ex.ToString();
				base.SendErrorResponse(context, 500, ex.ErrorCode, "Connection Registration Threw Exception: " + ex.Message);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D94 File Offset: 0x00000F94
		private void UnregisterRequest(Guid associationGroupId, Guid requestId)
		{
			try
			{
				this.rpcHttpConnectionRegistration.Unregister(associationGroupId, requestId);
			}
			catch (RpcHttpConnectionRegistrationInternalException)
			{
			}
			catch (RpcHttpConnectionRegistrationException)
			{
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DD4 File Offset: 0x00000FD4
		private void ClearRegistrations()
		{
			try
			{
				this.rpcHttpConnectionRegistration.Clear();
			}
			catch (RpcHttpConnectionRegistrationInternalException)
			{
			}
			catch (RpcHttpConnectionRegistrationException)
			{
			}
		}

		// Token: 0x04000013 RID: 19
		public const string RpcInMethod = "RPC_IN_DATA";

		// Token: 0x04000014 RID: 20
		public const string RpcOutMethod = "RPC_OUT_DATA";

		// Token: 0x04000015 RID: 21
		public const string TrustEntireForwardedForConfigurationKey = "TrustEntireForwardedFor";

		// Token: 0x04000016 RID: 22
		internal const string MissingPuidError = "Password sync failure because of missing PUID";

		// Token: 0x04000017 RID: 23
		internal const string MissingMemberNameError = "Password sync failure because of missing MemberName";

		// Token: 0x04000018 RID: 24
		internal const string PasswordSyncResultPrefix = "Password sync result";

		// Token: 0x04000019 RID: 25
		private const int AssociationGuidStringLength = 36;

		// Token: 0x0400001A RID: 26
		private const string AssociationGuidContextKey = "associationGuid";

		// Token: 0x0400001B RID: 27
		private const string ConnectionIsRegisteredContextKey = "connectionIsRegistered";

		// Token: 0x0400001C RID: 28
		private static readonly object ClearCacheLock = new object();

		// Token: 0x0400001D RID: 29
		private static bool haveClearedRegistrationCache = false;

		// Token: 0x0400001E RID: 30
		private readonly IDirectory directoryService;

		// Token: 0x0400001F RID: 31
		private readonly IRpcHttpConnectionRegistration rpcHttpConnectionRegistration;
	}
}
