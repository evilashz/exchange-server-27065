using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiUnbindAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000B516 File Offset: 0x00009716
		public NspiUnbindAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence | AsyncOperationCookieFlags.DestroySession)
		{
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000B521 File Offset: 0x00009721
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "Unbind";
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B52E File Offset: 0x0000972E
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiUnbindRequest(reader);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B778 File Offset: 0x00009978
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiUnbindRequest request = (NspiUnbindRequest)mapiHttpRequest;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginUnbind(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndUnbind((ICancelableAsyncResult)asyncResult, out nspiContextHandle);
				this.NspiContextHandle = nspiContextHandle;
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiUnbindResponse(returnCode, Array<byte>.EmptySegment));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B7C6 File Offset: 0x000099C6
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiUnbindAsyncOperation>(this);
		}
	}
}
