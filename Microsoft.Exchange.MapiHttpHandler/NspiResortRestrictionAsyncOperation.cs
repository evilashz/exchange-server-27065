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
	// Token: 0x0200002F RID: 47
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiResortRestrictionAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000AF46 File Offset: 0x00009146
		public NspiResortRestrictionAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000AF51 File Offset: 0x00009151
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "ResortRestriction";
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000AF5E File Offset: 0x0000915E
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiResortRestrictionRequest(reader);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B1C8 File Offset: 0x000093C8
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiResortRestrictionRequest request = (NspiResortRestrictionRequest)mapiHttpRequest;
			NspiState localState = null;
			int[] localMids = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginResortRestriction(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, request.EphemeralIds, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndResortRestriction((ICancelableAsyncResult)asyncResult, out localState, out localMids);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiResortRestrictionResponse(returnCode, localState, localMids, Array<byte>.EmptySegment));
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B216 File Offset: 0x00009416
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiResortRestrictionAsyncOperation>(this);
		}
	}
}
