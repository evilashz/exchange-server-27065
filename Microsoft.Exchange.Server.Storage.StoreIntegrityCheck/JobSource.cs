using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000019 RID: 25
	public enum JobSource : short
	{
		// Token: 0x04000041 RID: 65
		[MapToManagement(null, false)]
		OnDemand,
		// Token: 0x04000042 RID: 66
		[MapToManagement(null, false)]
		Maintenance
	}
}
