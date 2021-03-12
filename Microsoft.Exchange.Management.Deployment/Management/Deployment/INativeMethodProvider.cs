using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200001B RID: 27
	internal interface INativeMethodProvider
	{
		// Token: 0x0600003D RID: 61
		string GetSiteName(string server);

		// Token: 0x0600003E RID: 62
		uint GetAccessCheck(byte[] ntsd, string listString);

		// Token: 0x0600003F RID: 63
		uint GetAccessCheck(string ntsdString, string listString);

		// Token: 0x06000040 RID: 64
		bool TokenMembershipCheck(string sid);

		// Token: 0x06000041 RID: 65
		bool IsCoreServer();
	}
}
