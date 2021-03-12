using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum IntegrityCheckRequestFlags : uint
	{
		// Token: 0x04000021 RID: 33
		None = 0U,
		// Token: 0x04000022 RID: 34
		DetectOnly = 1U,
		// Token: 0x04000023 RID: 35
		Force = 2U,
		// Token: 0x04000024 RID: 36
		Maintenance = 4U,
		// Token: 0x04000025 RID: 37
		Verbose = 2147483648U
	}
}
