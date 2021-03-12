using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x020000EA RID: 234
	[Flags]
	internal enum RpcClientFlags : uint
	{
		// Token: 0x04000914 RID: 2324
		None = 0U,
		// Token: 0x04000915 RID: 2325
		AllowImpersonation = 1U,
		// Token: 0x04000916 RID: 2326
		UseEncryptedConnection = 2U,
		// Token: 0x04000917 RID: 2327
		IgnoreInvalidServerCertificate = 4U,
		// Token: 0x04000918 RID: 2328
		UniqueBinding = 8U,
		// Token: 0x04000919 RID: 2329
		ExplicitEndpointLookup = 16U,
		// Token: 0x0400091A RID: 2330
		UseSsl = 32U
	}
}
