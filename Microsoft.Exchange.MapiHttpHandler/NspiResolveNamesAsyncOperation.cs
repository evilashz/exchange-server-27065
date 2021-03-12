using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002E RID: 46
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiResolveNamesAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000AC3E File Offset: 0x00008E3E
		public NspiResolveNamesAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000AC49 File Offset: 0x00008E49
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "ResolveNames";
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000AC56 File Offset: 0x00008E56
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiResolveNamesRequest(reader);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AEF0 File Offset: 0x000090F0
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiResolveNamesRequest request = (NspiResolveNamesRequest)mapiHttpRequest;
			int localCodePage = 0;
			int[] localMids = null;
			PropertyValue[][] localRowset = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginResolveNamesW(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, request.Columns, request.Names, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndResolveNamesW((ICancelableAsyncResult)asyncResult, out localCodePage, out localMids, out localRowset);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiResolveNamesResponse(returnCode, (uint)localCodePage, localMids, localRowset.GetColumns(), localRowset, Array<byte>.EmptySegment));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000AF3E File Offset: 0x0000913E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiResolveNamesAsyncOperation>(this);
		}
	}
}
