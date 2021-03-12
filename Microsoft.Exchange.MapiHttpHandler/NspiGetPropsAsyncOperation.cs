using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetPropsAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00008DDE File Offset: 0x00006FDE
		public NspiGetPropsAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00008DE9 File Offset: 0x00006FE9
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetProps";
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008DF6 File Offset: 0x00006FF6
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiGetPropsRequest(reader);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009060 File Offset: 0x00007260
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiGetPropsRequest request = (NspiGetPropsRequest)mapiHttpRequest;
			int codePage = 0;
			PropertyValue[] propertyValues = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.NspiAsyncDispatch.BeginGetProps(this.ProtocolRequestInfo, this.NspiContextHandle, request.Flags, request.State, request.PropertyTags, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndGetProps((ICancelableAsyncResult)asyncResult, out codePage, out propertyValues);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiGetPropsResponse(returnCode, (uint)codePage, propertyValues, Array<byte>.EmptySegment));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000090AE File Offset: 0x000072AE
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetPropsAsyncOperation>(this);
		}
	}
}
