using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RfriGetAddressBookUrlAsyncOperation : RfriSecurityContextAsyncOperation
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x0000BC7C File Offset: 0x00009E7C
		public RfriGetAddressBookUrlAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.None)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000BC86 File Offset: 0x00009E86
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetNspiUrl";
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000BC93 File Offset: 0x00009E93
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new RfriGetAddressBookUrlRequest(reader);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			RfriGetAddressBookUrlRequest request = (RfriGetAddressBookUrlRequest)mapiHttpRequest;
			string serverUrl = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.RfriAsyncDispatch.BeginGetAddressBookUrl(this.ProtocolRequestInfo, this.ClientBinding, request.Flags, this.Context.GetOriginalHost(), request.UserDn, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.RfriAsyncDispatch.EndGetAddressBookUrl((ICancelableAsyncResult)asyncResult, out serverUrl);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new RfriGetAddressBookUrlResponse(returnCode, serverUrl ?? string.Empty, Array<byte>.EmptySegment));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000BF42 File Offset: 0x0000A142
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriGetAddressBookUrlAsyncOperation>(this);
		}
	}
}
