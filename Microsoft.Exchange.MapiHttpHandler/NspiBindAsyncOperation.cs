using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiBindAsyncOperation : NspiSecurityContextAsyncOperation
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00007F54 File Offset: 0x00006154
		public NspiBindAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.CreateSession)
		{
			context.SetTunnelExpirationTime(MapiHttpHandler.ClientTunnelExpirationTime);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007F69 File Offset: 0x00006169
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "Bind";
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007F76 File Offset: 0x00006176
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiBindRequest(reader);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000821C File Offset: 0x0000641C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiBindRequest request = (NspiBindRequest)mapiHttpRequest;
			Guid? serverGuid = new Guid?(default(Guid));
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.NspiAsyncDispatch.BeginBind(this.ProtocolRequestInfo, this.ClientBinding, request.Flags, request.State, null, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				IntPtr zero = IntPtr.Zero;
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndBind((ICancelableAsyncResult)asyncResult, out serverGuid, out zero);
				this.NspiContextHandle = zero;
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), delegate
			{
				if (serverGuid == null)
				{
					throw ProtocolException.FromResponseCode((LID)35932, "Server guid cannot be null on success", ResponseCode.InvalidResponse, null);
				}
				return new NspiBindResponse(returnCode, serverGuid.Value, Array<byte>.EmptySegment);
			});
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000826A File Offset: 0x0000646A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiBindAsyncOperation>(this);
		}
	}
}
