using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiDNToEphAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x0600016B RID: 363 RVA: 0x0000853E File Offset: 0x0000673E
		public NspiDNToEphAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00008549 File Offset: 0x00006749
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "DNToMId";
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008556 File Offset: 0x00006756
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiDnToEphRequest(reader);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000879C File Offset: 0x0000699C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiDnToEphRequest request = (NspiDnToEphRequest)mapiHttpRequest;
			int[] localMids = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginDNToEph(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.DistinguishedNames, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndDNToEph((ICancelableAsyncResult)asyncResult, out localMids);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiDnToEphResponse(returnCode, localMids, Array<byte>.EmptySegment));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000087EA File Offset: 0x000069EA
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiDNToEphAsyncOperation>(this);
		}
	}
}
