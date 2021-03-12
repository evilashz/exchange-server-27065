using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbDisconnectAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000045B2 File Offset: 0x000027B2
		public EmsmdbDisconnectAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence | AsyncOperationCookieFlags.DestroySession)
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000045BD File Offset: 0x000027BD
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "Disconnect";
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000045CA File Offset: 0x000027CA
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new EmsmdbDisconnectRequest(reader);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000047D4 File Offset: 0x000029D4
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr emsmdbContextHandle = base.EmsmdbContextHandle;
				return EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginDisconnect(base.ProtocolRequestInfo, emsmdbContextHandle, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				IntPtr emsmdbContextHandle = this.EmsmdbContextHandle;
				returnCode = (uint)EmsmdbHttpHandler.ExchangeAsyncDispatch.EndDisconnect((ICancelableAsyncResult)asyncResult, out emsmdbContextHandle);
				this.EmsmdbContextHandle = emsmdbContextHandle;
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new EmsmdbDisconnectResponse(returnCode, Array<byte>.EmptySegment));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000481A File Offset: 0x00002A1A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbDisconnectAsyncOperation>(this);
		}
	}
}
