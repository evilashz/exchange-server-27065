using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetTemplateInfoAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x06000184 RID: 388 RVA: 0x000093B2 File Offset: 0x000075B2
		public NspiGetTemplateInfoAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000093BD File Offset: 0x000075BD
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetTemplateInfo";
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000093CA File Offset: 0x000075CA
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiGetTemplateInfoRequest(reader);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000964C File Offset: 0x0000784C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiGetTemplateInfoRequest request = (NspiGetTemplateInfoRequest)mapiHttpRequest;
			int localCodePage = 0;
			PropertyValue[] localRow = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginGetTemplateInfo(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, (int)request.DisplayType, request.TemplateDn, (int)request.CodePage, (int)request.LocaleId, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndGetTemplateInfo((ICancelableAsyncResult)asyncResult, out localCodePage, out localRow);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiGetTemplateInfoResponse(returnCode, (uint)localCodePage, localRow, Array<byte>.EmptySegment));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000969A File Offset: 0x0000789A
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetTemplateInfoAsyncOperation>(this);
		}
	}
}
