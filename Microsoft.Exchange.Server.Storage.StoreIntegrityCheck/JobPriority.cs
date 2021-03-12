using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200001A RID: 26
	public enum JobPriority : short
	{
		// Token: 0x04000044 RID: 68
		[MapToManagement(null, false)]
		High,
		// Token: 0x04000045 RID: 69
		[MapToManagement(null, false)]
		Normal,
		// Token: 0x04000046 RID: 70
		[MapToManagement(null, false)]
		Low
	}
}
