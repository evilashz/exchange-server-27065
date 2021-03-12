using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000029 RID: 41
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiModLinkAttAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000A091 File Offset: 0x00008291
		public NspiModLinkAttAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000A09C File Offset: 0x0000829C
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "ModLinkAtt";
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A0A9 File Offset: 0x000082A9
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiModLinkAttRequest(reader);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A2F0 File Offset: 0x000084F0
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiModLinkAttRequest request = (NspiModLinkAttRequest)mapiHttpRequest;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginModLinkAtt(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.PropertyTag, request.EphemeralId, request.EntryIds, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndModLinkAtt((ICancelableAsyncResult)asyncResult);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiModLinkAttResponse(returnCode, Array<byte>.EmptySegment));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A33E File Offset: 0x0000853E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiModLinkAttAsyncOperation>(this);
		}
	}
}
