using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.Rpc.RfriClient
{
	// Token: 0x020003A4 RID: 932
	internal class RfriAsyncRpcClient : RpcClientBase, IRfriAsyncDispatch
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x0004C5F0 File Offset: 0x0004B9F0
		public RfriAsyncRpcClient(RpcBindingInfo bindingInfo) : base(bindingInfo.UseKerberosSpn("exchangeRFR", null))
		{
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0004C988 File Offset: 0x0004BD88
		public virtual ICancelableAsyncResult BeginGetNewDSA(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetNewDSAFlags flags, string userDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_GetNewDSA clientAsyncCallState_GetNewDSA = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				IntPtr bindingHandle = (IntPtr)base.BindingHandle;
				clientAsyncCallState_GetNewDSA = new ClientAsyncCallState_GetNewDSA(asyncCallback, asyncState, bindingHandle, flags, userDn);
				clientAsyncCallState_GetNewDSA.Begin();
				flag = true;
				result = clientAsyncCallState_GetNewDSA;
			}
			finally
			{
				if (!flag && clientAsyncCallState_GetNewDSA != null)
				{
					((IDisposable)clientAsyncCallState_GetNewDSA).Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004C8F8 File Offset: 0x0004BCF8
		public virtual RfriStatus EndGetNewDSA(ICancelableAsyncResult asyncResult, out string server)
		{
			RfriStatus result;
			using (ClientAsyncCallState_GetNewDSA clientAsyncCallState_GetNewDSA = (ClientAsyncCallState_GetNewDSA)asyncResult)
			{
				result = clientAsyncCallState_GetNewDSA.End(out server);
			}
			return result;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004C9EC File Offset: 0x0004BDEC
		public virtual ICancelableAsyncResult BeginGetFQDNFromLegacyDN(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetFQDNFromLegacyDNFlags flags, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			ClientAsyncCallState_GetFQDNFromLegacyDN clientAsyncCallState_GetFQDNFromLegacyDN = null;
			bool flag = false;
			ICancelableAsyncResult result;
			try
			{
				IntPtr bindingHandle = (IntPtr)base.BindingHandle;
				clientAsyncCallState_GetFQDNFromLegacyDN = new ClientAsyncCallState_GetFQDNFromLegacyDN(asyncCallback, asyncState, bindingHandle, flags, serverDn);
				clientAsyncCallState_GetFQDNFromLegacyDN.Begin();
				flag = true;
				result = clientAsyncCallState_GetFQDNFromLegacyDN;
			}
			finally
			{
				if (!flag && clientAsyncCallState_GetFQDNFromLegacyDN != null)
				{
					((IDisposable)clientAsyncCallState_GetFQDNFromLegacyDN).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004C940 File Offset: 0x0004BD40
		public virtual RfriStatus EndGetFQDNFromLegacyDN(ICancelableAsyncResult asyncResult, out string serverFqdn)
		{
			RfriStatus result;
			using (ClientAsyncCallState_GetFQDNFromLegacyDN clientAsyncCallState_GetFQDNFromLegacyDN = (ClientAsyncCallState_GetFQDNFromLegacyDN)asyncResult)
			{
				result = clientAsyncCallState_GetFQDNFromLegacyDN.End(out serverFqdn);
			}
			return result;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004C5A0 File Offset: 0x0004B9A0
		public virtual ICancelableAsyncResult BeginGetAddressBookUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetAddressBookUrlFlags flags, string hostname, string userDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004C5B4 File Offset: 0x0004B9B4
		public virtual RfriStatus EndGetAddressBookUrl(ICancelableAsyncResult asyncResult, out string url)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004C5C8 File Offset: 0x0004B9C8
		public virtual ICancelableAsyncResult BeginGetMailboxUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetMailboxUrlFlags flags, string hostname, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004C5DC File Offset: 0x0004B9DC
		public virtual RfriStatus EndGetMailboxUrl(ICancelableAsyncResult asyncResult, out string url)
		{
			throw new NotSupportedException();
		}
	}
}
