using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RfriAsyncOperation : AsyncOperation
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000BAAA File Offset: 0x00009CAA
		protected RfriAsyncOperation(HttpContextBase context, AsyncOperationCookieFlags cookieFlags) : base(context, MapiHttpEndpoints.VdirPathNspi, cookieFlags)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000BABC File Offset: 0x00009CBC
		public override string GetTraceEndParameters(MapiHttpRequest mapiHttpRequest, MapiHttpResponse mapiHttpResponse)
		{
			base.CheckDisposed();
			if (mapiHttpResponse.StatusCode != 0U)
			{
				return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Failed; statusCode={0}", mapiHttpResponse.StatusCode));
			}
			if (mapiHttpResponse is MapiHttpOperationResponse && ((MapiHttpOperationResponse)mapiHttpResponse).ReturnCode != 0U)
			{
				return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Failed; statusCode={0}. errorCode={1}", mapiHttpResponse.StatusCode, ((MapiHttpOperationResponse)mapiHttpResponse).ReturnCode));
			}
			return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), "Success");
		}
	}
}
