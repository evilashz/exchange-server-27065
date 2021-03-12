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
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiUpdateStatAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000B7CE File Offset: 0x000099CE
		public NspiUpdateStatAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000B7D9 File Offset: 0x000099D9
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "UpdateStat";
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B7E6 File Offset: 0x000099E6
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiUpdateStatRequest(reader);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000BA54 File Offset: 0x00009C54
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiUpdateStatRequest request = (NspiUpdateStatRequest)mapiHttpRequest;
			NspiState localState = null;
			int? localDelta = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginUpdateStat(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, request.DeltaRequested, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndUpdateStat((ICancelableAsyncResult)asyncResult, out localState, out localDelta);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiUpdateStatResponse(returnCode, localState, localDelta, Array<byte>.EmptySegment));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BAA2 File Offset: 0x00009CA2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiUpdateStatAsyncOperation>(this);
		}
	}
}
