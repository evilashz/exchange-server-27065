using System;
using System.Web;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000112 RID: 274
	public static class OwaAuthenticationHelper
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0003AB14 File Offset: 0x00038D14
		internal static bool IsOwaUserActivityRequest(HttpRequest httpRequest)
		{
			return httpRequest.Headers["X-UserActivity"] != "0" && httpRequest.QueryString["UA"] != "0" && !Utility.IsOWAPingRequest(httpRequest) && !Utility.IsResourceRequest(httpRequest.Path);
		}

		// Token: 0x04000827 RID: 2087
		public const string UserActivity = "X-UserActivity";
	}
}
