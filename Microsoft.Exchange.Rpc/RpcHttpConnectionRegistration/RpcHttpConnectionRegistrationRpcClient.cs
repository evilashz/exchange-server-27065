using System;
using System.Net;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003E5 RID: 997
	internal class RpcHttpConnectionRegistrationRpcClient : RpcClientBase, IRpcHttpConnectionRegistrationDispatch
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x0005594C File Offset: 0x00054D4C
		public RpcHttpConnectionRegistrationRpcClient(string machineName, string proxyServer, string protocolSequence, NetworkCredential networkCredential) : base(machineName, proxyServer, protocolSequence, networkCredential, HttpAuthenticationScheme.Basic, AuthenticationService.Negotiate, true)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00055924 File Offset: 0x00054D24
		public RpcHttpConnectionRegistrationRpcClient() : base("localhost", null, null, null, null, null, true, null, HttpAuthenticationScheme.Basic, AuthenticationService.Negotiate, false, false, true)
		{
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000559BC File Offset: 0x00054DBC
		public virtual int Register(Guid associationGroupId, string token, string serverTarget, string sessionCookie, string clientIp, Guid requestId, out string failureMessage, out string failureDetails)
		{
			ClientCallWrapper_Register clientCallWrapper_Register = null;
			int result;
			try
			{
				clientCallWrapper_Register = new ClientCallWrapper_Register(base.BindingHandle, associationGroupId, token, serverTarget, sessionCookie, clientIp, requestId);
				int num = clientCallWrapper_Register.Execute();
				failureMessage = clientCallWrapper_Register.FailureMessage;
				failureDetails = clientCallWrapper_Register.FailureDetails;
				result = num;
			}
			finally
			{
				if (clientCallWrapper_Register != null)
				{
					((IDisposable)clientCallWrapper_Register).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00055A24 File Offset: 0x00054E24
		public virtual int Unregister(Guid associationGroupId, Guid requestId)
		{
			ClientCallWrapper_Unregister clientCallWrapper_Unregister = null;
			int result;
			try
			{
				clientCallWrapper_Unregister = new ClientCallWrapper_Unregister(base.BindingHandle, associationGroupId, requestId);
				result = clientCallWrapper_Unregister.Execute();
			}
			finally
			{
				if (clientCallWrapper_Unregister != null)
				{
					((IDisposable)clientCallWrapper_Unregister).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00055A74 File Offset: 0x00054E74
		public virtual int Clear()
		{
			ClientCallWrapper_Clear clientCallWrapper_Clear = null;
			int result;
			try
			{
				clientCallWrapper_Clear = new ClientCallWrapper_Clear(base.BindingHandle);
				result = clientCallWrapper_Clear.Execute();
			}
			finally
			{
				if (clientCallWrapper_Clear != null)
				{
					((IDisposable)clientCallWrapper_Clear).Dispose();
				}
			}
			return result;
		}
	}
}
