using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiQueryColumnsAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000A691 File Offset: 0x00008891
		public NspiQueryColumnsAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A69C File Offset: 0x0000889C
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "QueryColumns";
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A6A9 File Offset: 0x000088A9
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiQueryColumnsRequest(reader);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiQueryColumnsRequest request = (NspiQueryColumnsRequest)mapiHttpRequest;
			PropertyTag[] localColumns = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginQueryColumns(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.MapiFlags, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndQueryColumns((ICancelableAsyncResult)asyncResult, out localColumns);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiQueryColumnsResponse(returnCode, localColumns, Array<byte>.EmptySegment));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A93E File Offset: 0x00008B3E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiQueryColumnsAsyncOperation>(this);
		}
	}
}
