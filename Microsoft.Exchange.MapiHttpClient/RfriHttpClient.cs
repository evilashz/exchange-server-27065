using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RfriHttpClient : MapiHttpClient, IRfriAsyncDispatch
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00008232 File Offset: 0x00006432
		public RfriHttpClient(MapiHttpBindingInfo bindingInfo) : base(bindingInfo)
		{
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000823B File Offset: 0x0000643B
		internal override string VdirPath
		{
			get
			{
				return MapiHttpEndpoints.VdirPathNspi;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000828C File Offset: 0x0000648C
		public ICancelableAsyncResult BeginGetAddressBookUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetAddressBookUrlFlags flags, string hostname, string userDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			IntPtr contextHandle = base.CreateNewContextHandle(null);
			return base.BeginWrapper<RfriGetAddressBookUrlClientAsyncOperation>(contextHandle, true, delegate(ClientSessionContext context)
			{
				RfriGetAddressBookUrlClientAsyncOperation rfriGetAddressBookUrlClientAsyncOperation = new RfriGetAddressBookUrlClientAsyncOperation(context, asyncCallback, asyncState);
				rfriGetAddressBookUrlClientAsyncOperation.Begin(new RfriGetAddressBookUrlRequest(flags, userDn, Array<byte>.EmptySegment));
				return rfriGetAddressBookUrlClientAsyncOperation;
			});
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008300 File Offset: 0x00006500
		public RfriStatus EndGetAddressBookUrl(ICancelableAsyncResult asyncResult, out string serverUrl)
		{
			RfriGetAddressBookUrlResponse getAddressBookUrlResponse = null;
			IntPtr intPtr;
			ErrorCode result = base.EndWrapper<RfriGetAddressBookUrlClientAsyncOperation>(asyncResult, true, true, out intPtr, (RfriGetAddressBookUrlClientAsyncOperation operation) => operation.End(out getAddressBookUrlResponse));
			serverUrl = getAddressBookUrlResponse.ServerUrl;
			return (RfriStatus)result;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008388 File Offset: 0x00006588
		public ICancelableAsyncResult BeginGetMailboxUrl(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetMailboxUrlFlags flags, string hostname, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			IntPtr contextHandle = base.CreateNewContextHandle(null);
			return base.BeginWrapper<RfriGetMailboxUrlClientAsyncOperation>(contextHandle, true, delegate(ClientSessionContext context)
			{
				RfriGetMailboxUrlClientAsyncOperation rfriGetMailboxUrlClientAsyncOperation = new RfriGetMailboxUrlClientAsyncOperation(context, asyncCallback, asyncState);
				rfriGetMailboxUrlClientAsyncOperation.Begin(new RfriGetMailboxUrlRequest(flags, serverDn, Array<byte>.EmptySegment));
				return rfriGetMailboxUrlClientAsyncOperation;
			});
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000083FC File Offset: 0x000065FC
		public RfriStatus EndGetMailboxUrl(ICancelableAsyncResult asyncResult, out string serverUrl)
		{
			RfriGetMailboxUrlResponse getMailboxUrlResponse = null;
			IntPtr intPtr;
			ErrorCode result = base.EndWrapper<RfriGetMailboxUrlClientAsyncOperation>(asyncResult, true, true, out intPtr, (RfriGetMailboxUrlClientAsyncOperation operation) => operation.End(out getMailboxUrlResponse));
			serverUrl = getMailboxUrlResponse.ServerUrl;
			return (RfriStatus)result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000843C File Offset: 0x0000663C
		public ICancelableAsyncResult BeginGetNewDSA(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetNewDSAFlags flags, string userDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008443 File Offset: 0x00006643
		public RfriStatus EndGetNewDSA(ICancelableAsyncResult asyncResult, out string serverUrl)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000844A File Offset: 0x0000664A
		public ICancelableAsyncResult BeginGetFQDNFromLegacyDN(ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriGetFQDNFromLegacyDNFlags flags, string serverDn, CancelableAsyncCallback asyncCallback, object asyncState)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008451 File Offset: 0x00006651
		public RfriStatus EndGetFQDNFromLegacyDN(ICancelableAsyncResult asyncResult, out string serverUrl)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008458 File Offset: 0x00006658
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriHttpClient>(this);
		}
	}
}
