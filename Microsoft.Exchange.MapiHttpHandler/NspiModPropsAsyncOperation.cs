using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiModPropsAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000A346 File Offset: 0x00008546
		public NspiModPropsAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000A351 File Offset: 0x00008551
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "ModProps";
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A35E File Offset: 0x0000855E
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiModPropsRequest(reader);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A5A0 File Offset: 0x000087A0
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiModPropsRequest request = (NspiModPropsRequest)mapiHttpRequest;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync((AsyncCallback asyncCallback, object asyncState) => NspiHttpHandler.NspiAsyncDispatch.BeginModProps(this.ProtocolRequestInfo, this.NspiContextHandle, request.Flags, request.State, request.PropertiesToRemove, request.ModificationValues, delegate(ICancelableAsyncResult cancelableAsyncResult)
			{
				asyncCallback(cancelableAsyncResult);
			}, asyncState), delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndModProps((ICancelableAsyncResult)asyncResult);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiModPropsResponse(returnCode, Array<byte>.EmptySegment));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A5EE File Offset: 0x000087EE
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiModPropsAsyncOperation>(this);
		}
	}
}
