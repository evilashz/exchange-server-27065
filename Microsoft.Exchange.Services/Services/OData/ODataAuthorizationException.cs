using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E0D RID: 3597
	internal class ODataAuthorizationException : ODataResponseException
	{
		// Token: 0x06005D3E RID: 23870 RVA: 0x00122E1A File Offset: 0x0012101A
		public ODataAuthorizationException(Exception innerException) : base(HttpStatusCode.Forbidden, ResponseCodeType.ErrorAccessDenied, CoreResources.ErrorAccessDenied, innerException)
		{
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00122E2E File Offset: 0x0012102E
		public ODataAuthorizationException(LocalizedString errorMessage) : base(HttpStatusCode.Forbidden, ResponseCodeType.ErrorAccessDenied, errorMessage, null)
		{
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x00122E40 File Offset: 0x00121040
		public override void AppendResponseHeader(HttpContext httpContext)
		{
			if (base.InnerException != null)
			{
				InvalidOAuthTokenException ex = base.InnerException as InvalidOAuthTokenException;
				if (ex != null)
				{
					MSDiagnosticsHeader.AppendInvalidOAuthTokenExceptionToBackendResponse(httpContext, ex);
				}
			}
		}
	}
}
