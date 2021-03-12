using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000041 RID: 65
	internal sealed class RpcHttpConnectionRegistrationDispatch
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000CCE1 File Offset: 0x0000AEE1
		public RpcHttpConnectionRegistrationDispatch(RpcHttpConnectionRegistration connectionRegistrationCache)
		{
			this.connectionRegistrationCache = connectionRegistrationCache;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public int EcRegister(Guid associationGroupId, string serializedToken, string serverTarget, string sessionCookie, string clientIp, Guid requestId, out string failureMessage, out string failureDetails)
		{
			failureMessage = null;
			failureDetails = null;
			ClientSecurityContext clientSecurityContext = null;
			int num = 5;
			int result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Exception ex = null;
				string text = null;
				try
				{
					BackendAuthenticator backendAuthenticator = null;
					CommonAccessToken token = CommonAccessToken.Deserialize(serializedToken);
					IPrincipal principal = null;
					BackendAuthenticator.GetAuthIdentifier(token, ref backendAuthenticator, out text);
					bool flag = text != null;
					if (flag && this.connectionRegistrationCache.TryRegisterAdditionalConnection(associationGroupId, text, requestId))
					{
						return 0;
					}
					string text2 = null;
					BackendAuthenticator.Rehydrate(token, ref backendAuthenticator, !flag, out text2, out principal);
					text = (text ?? text2);
					IIdentity identity = principal.Identity;
					IDisposable disposable = identity as IDisposable;
					try
					{
						clientSecurityContext = identity.CreateClientSecurityContext(false);
					}
					finally
					{
						Util.DisposeIfPresent(disposable);
					}
					disposeGuard.Add<ClientSecurityContext>(clientSecurityContext);
				}
				catch (CommonAccessTokenException ex2)
				{
					ex = ex2;
					num = 28;
				}
				catch (BackendRehydrationException ex3)
				{
					ex = ex3;
					num = 61;
				}
				catch (AuthzException ex4)
				{
					ex = ex4;
					num = 63;
				}
				if (ex != null)
				{
					ExTraceGlobals.AccessControlTracer.TraceDebug<string, Guid, Exception>(Activity.TraceId, "Unable to parse token '{0}' for Association Group '{1}'. Exception: {2}", serializedToken, associationGroupId, ex);
					failureMessage = ex.Message;
					failureDetails = ex.ToString();
					if (ex is AuthzException && ex.InnerException != null && ex.InnerException is ExternalException)
					{
						ExternalException ex5 = (ExternalException)ex.InnerException;
						num = ((ex5.ErrorCode != 0) ? ex5.ErrorCode : 63);
						failureMessage = ex5.Message;
					}
					result = num;
				}
				else
				{
					try
					{
						this.connectionRegistrationCache.Register(associationGroupId, clientSecurityContext, text, serverTarget, sessionCookie, clientIp, requestId);
					}
					catch (ConnectionRegistrationException ex6)
					{
						failureMessage = ex6.Message;
						failureDetails = ex6.ToString();
						return 64;
					}
					disposeGuard.Success();
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000CF24 File Offset: 0x0000B124
		public int EcUnregister(Guid associationGroupId, Guid requestId)
		{
			this.connectionRegistrationCache.Unregister(associationGroupId, requestId);
			return 0;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000CF34 File Offset: 0x0000B134
		public int EcClear()
		{
			this.connectionRegistrationCache.Clear();
			return 0;
		}

		// Token: 0x04000128 RID: 296
		internal const int Ec_Success = 0;

		// Token: 0x04000129 RID: 297
		internal const int Ec_AccessDenied = 5;

		// Token: 0x0400012A RID: 298
		internal const int Ec_BadFormat = 11;

		// Token: 0x0400012B RID: 299
		internal const int Ec_CommonAccessTokenException = 28;

		// Token: 0x0400012C RID: 300
		internal const int Ec_BackendRehydrationException = 61;

		// Token: 0x0400012D RID: 301
		internal const int Ec_AuthzException = 63;

		// Token: 0x0400012E RID: 302
		internal const int Ec_RegisterException = 64;

		// Token: 0x0400012F RID: 303
		private readonly RpcHttpConnectionRegistration connectionRegistrationCache;
	}
}
