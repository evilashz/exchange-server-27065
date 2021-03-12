using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EmsmdbDummyAsyncOperation : EmsmdbSecurityContextAsyncOperation
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004822 File Offset: 0x00002A22
		public EmsmdbDummyAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.None)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000482C File Offset: 0x00002A2C
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "Dummy";
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004839 File Offset: 0x00002A39
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new EmsmdbDummyRequest(reader);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004A1C File Offset: 0x00002C1C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => EmsmdbHttpHandler.ExchangeAsyncDispatch.BeginDummy(base.ProtocolRequestInfo, base.ClientBinding, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)EmsmdbHttpHandler.ExchangeAsyncDispatch.EndDummy((ICancelableAsyncResult)asyncResult);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new EmsmdbDummyResponse(returnCode, Array<byte>.EmptySegment));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A62 File Offset: 0x00002C62
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbDummyAsyncOperation>(this);
		}
	}
}
