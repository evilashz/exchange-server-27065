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
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiSeekEntriesAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000B21E File Offset: 0x0000941E
		public NspiSeekEntriesAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000B229 File Offset: 0x00009429
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "SeekEntries";
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B236 File Offset: 0x00009436
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiSeekEntriesRequest(reader);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B4C0 File Offset: 0x000096C0
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiSeekEntriesRequest request = (NspiSeekEntriesRequest)mapiHttpRequest;
			NspiState localState = null;
			PropertyValue[][] localRowset = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginSeekEntries(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, request.Target, request.ExplicitTable, request.Columns, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndSeekEntries((ICancelableAsyncResult)asyncResult, out localState, out localRowset);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiSeekEntriesResponse(returnCode, localState, localRowset.GetColumns(), localRowset, Array<byte>.EmptySegment));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B50E File Offset: 0x0000970E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiSeekEntriesAsyncOperation>(this);
		}
	}
}
