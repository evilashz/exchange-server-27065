using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RfriGetMailboxUrlAsyncOperation : RfriSecurityContextAsyncOperation
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000BF4A File Offset: 0x0000A14A
		public RfriGetMailboxUrlAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.None)
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000BF54 File Offset: 0x0000A154
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetMailboxUrl";
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000BF61 File Offset: 0x0000A161
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new RfriGetMailboxUrlRequest(reader);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			RfriGetMailboxUrlRequest request = (RfriGetMailboxUrlRequest)mapiHttpRequest;
			string serverUrl = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.RfriAsyncDispatch.BeginGetMailboxUrl(this.ProtocolRequestInfo, this.ClientBinding, request.Flags, this.Context.GetOriginalHost(), request.ServerDn, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.RfriAsyncDispatch.EndGetMailboxUrl((ICancelableAsyncResult)asyncResult, out serverUrl);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new RfriGetMailboxUrlResponse(returnCode, serverUrl ?? string.Empty, Array<byte>.EmptySegment));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000C20E File Offset: 0x0000A40E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriGetMailboxUrlAsyncOperation>(this);
		}
	}
}
