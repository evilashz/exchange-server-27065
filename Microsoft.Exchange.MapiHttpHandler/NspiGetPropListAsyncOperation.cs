using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetPropListAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00008B1E File Offset: 0x00006D1E
		public NspiGetPropListAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008B29 File Offset: 0x00006D29
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetPropList";
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008B36 File Offset: 0x00006D36
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiGetPropListRequest(reader);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008D88 File Offset: 0x00006F88
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiGetPropListRequest request = (NspiGetPropListRequest)mapiHttpRequest;
			PropertyTag[] propertyTags = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.NspiAsyncDispatch.BeginGetPropList(this.ProtocolRequestInfo, this.NspiContextHandle, request.Flags, request.EphemeralId, (int)request.CodePage, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndGetPropList((ICancelableAsyncResult)asyncResult, out propertyTags);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiGetPropListResponse(returnCode, propertyTags, Array<byte>.EmptySegment));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008DD6 File Offset: 0x00006FD6
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetPropListAsyncOperation>(this);
		}
	}
}
