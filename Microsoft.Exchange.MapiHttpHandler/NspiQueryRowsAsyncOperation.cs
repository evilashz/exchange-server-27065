using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiQueryRowsAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000A946 File Offset: 0x00008B46
		public NspiQueryRowsAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A951 File Offset: 0x00008B51
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "QueryRows";
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A95E File Offset: 0x00008B5E
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiQueryRowsRequest(reader);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiQueryRowsRequest request = (NspiQueryRowsRequest)mapiHttpRequest;
			NspiState localState = null;
			PropertyValue[][] localRowset = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginQueryRows(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, request.ExplicitTable, (int)request.RowCount, request.Columns, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndQueryRows((ICancelableAsyncResult)asyncResult, out localState, out localRowset);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiQueryRowsResponse(returnCode, localState, localRowset.GetColumns(), localRowset, Array<byte>.EmptySegment));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AC36 File Offset: 0x00008E36
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiQueryRowsAsyncOperation>(this);
		}
	}
}
