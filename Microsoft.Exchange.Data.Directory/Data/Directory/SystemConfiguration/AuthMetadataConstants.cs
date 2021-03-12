using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000699 RID: 1689
	internal static class AuthMetadataConstants
	{
		// Token: 0x0400356A RID: 13674
		public static readonly string Protocol = "OAuth2";

		// Token: 0x0400356B RID: 13675
		public static readonly string MetadataEndpointUsage = "metadata";

		// Token: 0x0400356C RID: 13676
		public static readonly string IssuingEndpointUsage = "issuance";

		// Token: 0x0400356D RID: 13677
		public static readonly string KeyUsage = "signing";

		// Token: 0x0400356E RID: 13678
		public static readonly string OpenIdConnectSigningKeyUsage = "sig";

		// Token: 0x0400356F RID: 13679
		public static readonly string SigningKeyType = "x509Certificate";

		// Token: 0x04003570 RID: 13680
		public static readonly string AzureADTenantIdTemplate = "{tenantid}";

		// Token: 0x04003571 RID: 13681
		public static readonly string SelfIssuingAuthorityMetadataVersion = "Exchange/1.0";
	}
}
