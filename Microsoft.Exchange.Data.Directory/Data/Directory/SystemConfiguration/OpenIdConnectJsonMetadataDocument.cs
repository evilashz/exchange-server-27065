using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006A2 RID: 1698
	internal class OpenIdConnectJsonMetadataDocument
	{
		// Token: 0x04003597 RID: 13719
		public string token_endpoint;

		// Token: 0x04003598 RID: 13720
		public string check_session_iframe;

		// Token: 0x04003599 RID: 13721
		public string[] scopes_supported;

		// Token: 0x0400359A RID: 13722
		public string[] response_modes_supported;

		// Token: 0x0400359B RID: 13723
		public bool microsoft_multi_refresh_token;

		// Token: 0x0400359C RID: 13724
		public string authorization_endpoint;

		// Token: 0x0400359D RID: 13725
		public string userinfo_endpoint;

		// Token: 0x0400359E RID: 13726
		public string[] token_endpoint_auth_methods_supported;

		// Token: 0x0400359F RID: 13727
		public string jwks_uri;

		// Token: 0x040035A0 RID: 13728
		public string[] id_token_signing_alg_values_supported;

		// Token: 0x040035A1 RID: 13729
		public string end_session_endpoint;

		// Token: 0x040035A2 RID: 13730
		public string issuer;

		// Token: 0x040035A3 RID: 13731
		public string[] subject_types_supported;

		// Token: 0x040035A4 RID: 13732
		public string[] response_types_supported;
	}
}
