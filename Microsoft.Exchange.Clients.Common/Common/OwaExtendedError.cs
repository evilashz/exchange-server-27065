using System;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200002A RID: 42
	public static class OwaExtendedError
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000083A0 File Offset: 0x000065A0
		public static void SendError(NameValueCollection headerCollection, Action<HttpStatusCode> responseCodeSetter, OwaExtendedErrorCode code, string message = "", string user = "", string extra = "")
		{
			string name = "X-OWA-ExtendedErrorCode";
			int num = (int)code;
			headerCollection.Set(name, num.ToString());
			headerCollection.Set("X-OWA-ExtendedErrorMessage", message ?? string.Empty);
			headerCollection.Set("X-OWA-ExtendedErrorUser", user ?? string.Empty);
			headerCollection.Set("X-OWA-ExtendedErrorData", extra ?? string.Empty);
			responseCodeSetter(HttpStatusCode.PreconditionFailed);
		}

		// Token: 0x0400027D RID: 637
		public const HttpStatusCode ExtendedErrorStatusCode = HttpStatusCode.PreconditionFailed;

		// Token: 0x0400027E RID: 638
		public const string ExtendedErrorCodeHeader = "X-OWA-ExtendedErrorCode";

		// Token: 0x0400027F RID: 639
		public const string ExtendedErrorMessageHeader = "X-OWA-ExtendedErrorMessage";

		// Token: 0x04000280 RID: 640
		public const string ExtendedErrorUserHeader = "X-OWA-ExtendedErrorUser";

		// Token: 0x04000281 RID: 641
		public const string ExtendedErrorDataHeader = "X-OWA-ExtendedErrorData";
	}
}
