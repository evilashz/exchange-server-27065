using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiGetSpecialTableAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000090B6 File Offset: 0x000072B6
		public NspiGetSpecialTableAsyncOperation(HttpContextBase context) : base(context, AsyncOperationCookieFlags.RequireSession | AsyncOperationCookieFlags.AllowSession | AsyncOperationCookieFlags.RequireSequence)
		{
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000090C1 File Offset: 0x000072C1
		public override string RequestType
		{
			get
			{
				base.CheckDisposed();
				return "GetSpecialTable";
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000090CE File Offset: 0x000072CE
		protected override MapiHttpRequest InternalParseRequest(Reader reader)
		{
			return new NspiGetSpecialTableRequest(reader);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000935C File Offset: 0x0000755C
		protected override async Task<MapiHttpResponse> InternalExecuteAsync(MapiHttpRequest mapiHttpRequest)
		{
			NspiGetSpecialTableRequest request = (NspiGetSpecialTableRequest)mapiHttpRequest;
			int localCodePage = 0;
			int localVersion = 0;
			PropertyValue[][] localRowset = null;
			uint returnCode = 0U;
			MapiHttpDispatchedCallResult callResult = await MapiHttpHandler.DispatchCallAsync(delegate(AsyncCallback asyncCallback, object asyncState)
			{
				IntPtr nspiContextHandle = this.NspiContextHandle;
				return NspiHttpHandler.NspiAsyncDispatch.BeginGetHierarchyInfo(this.ProtocolRequestInfo, nspiContextHandle, request.Flags, request.State, (int)request.Version.Value, delegate(ICancelableAsyncResult cancelableAsyncResult)
				{
					asyncCallback(cancelableAsyncResult);
				}, asyncState);
			}, delegate(IAsyncResult asyncResult)
			{
				returnCode = (uint)NspiHttpHandler.NspiAsyncDispatch.EndGetHierarchyInfo((ICancelableAsyncResult)asyncResult, out localCodePage, out localVersion, out localRowset);
			});
			return callResult.CreateResponse(new Func<ArraySegment<byte>>(base.AllocateErrorBuffer), () => new NspiGetSpecialTableResponse(returnCode, (uint)localCodePage, new uint?((uint)localVersion), localRowset, Array<byte>.EmptySegment));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000093AA File Offset: 0x000075AA
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetSpecialTableAsyncOperation>(this);
		}
	}
}
