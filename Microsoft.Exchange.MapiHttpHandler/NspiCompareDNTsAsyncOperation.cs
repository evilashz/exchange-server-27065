using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiCompareDNTsAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00008272 File Offset: 0x00006472
		public NspiCompareDNTsAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000827D File Offset: 0x0000647D
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "CompareDNTs";
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000828A File Offset: 0x0000648A
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiCompareDntsRequest(reader);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000084E8 File Offset: 0x000066E8
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiCompareDntsRequest request = (NspiCompareDntsRequest)mapiHttpRequest;
			int localResult = 0;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginCompareDNTs(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, request.EphemeralId1, request.EphemeralId2, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndCompareDNTs((ICancelableAsyncResult)asyncResult, out localResult);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiCompareDntsResponse(returnCode, localResult, Array<byte>.EmptySegment));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008536 File Offset: 0x00006736
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiCompareDNTsAsyncOperation>(this);
		}
	}
}
