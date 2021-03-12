using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbExecuteAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00004A6A File Offset: 0x00002C6A
		public EmsmdbExecuteAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004A75 File Offset: 0x00002C75
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "Execute";
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004A82 File Offset: 0x00002C82
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new EmsmdbExecuteRequest(reader);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004D4C File Offset: 0x00002F4C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			EmsmdbExecuteRequest request = (EmsmdbExecuteRequest)mapiHttpRequest;
			ArraySegment<byte> outputRopBuffer = base.AllocateBuffer((int)request.MaxOutputBufferSize, EmsmdbConstants.MaxChainedExtendedRopBufferSize);
			ArraySegment<byte> outputAuxiliaryBuffer = base.AllocateBuffer(4104, 4104);
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr emsmdbContextHandle = this.EmsmdbContextHandle;
				return EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginExecute(this.ProtocolRequestInfo, emsmdbContextHandle, (int)request.Flags, request.RopBuffer, outputRopBuffer, request.AuxiliaryBuffer, outputAuxiliaryBuffer, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				IntPtr emsmdbContextHandle = this.EmsmdbContextHandle;
				returnCode = (uint)EmsmdbHttpHandler.ExchangeAsyncDispatch.EndExecute((ICancelableAsyncResult)asyncResult, out emsmdbContextHandle, out outputRopBuffer, out outputAuxiliaryBuffer);
				this.EmsmdbContextHandle = emsmdbContextHandle;
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new EmsmdbExecuteResponse(returnCode, 0U, outputRopBuffer, outputAuxiliaryBuffer));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004D9A File Offset: 0x00002F9A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbExecuteAsyncOperation>(this);
		}
	}
}
