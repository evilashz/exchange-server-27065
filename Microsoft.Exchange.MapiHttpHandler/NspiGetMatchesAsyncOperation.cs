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
	// Token: 0x02000023 RID: 35
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetMatchesAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x06000170 RID: 368 RVA: 0x000087F2 File Offset: 0x000069F2
		public NspiGetMatchesAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000087FD File Offset: 0x000069FD
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetMatches";
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000880A File Offset: 0x00006A0A
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiGetMatchesRequest(reader);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008AC8 File Offset: 0x00006CC8
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiGetMatchesRequest request = (NspiGetMatchesRequest)mapiHttpRequest;
			NspiState state = null;
			int[] mids = null;
			PropertyValue[][] rowSet = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.NspiAsyncDispatch.BeginGetMatches(this.ProtocolRequestInfo, this.NspiContextHandle, request.Flags, request.State, request.InputEphemeralIds, (int)request.InterfaceOptions, request.Restriction, IntPtr.Zero, (int)request.MaxCount, request.Columns, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndGetMatches((ICancelableAsyncResult)asyncResult, out state, out mids, out rowSet);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiGetMatchesResponse(returnCode, state, mids, rowSet.GetColumns(), rowSet, Array<byte>.EmptySegment));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008B16 File Offset: 0x00006D16
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetMatchesAsyncOperation>(this);
		}
	}
}
