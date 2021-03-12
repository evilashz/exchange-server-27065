using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200001D RID: 29
	[Flags]
	public enum DatabaseFlags : uint
	{
		// Token: 0x0400009B RID: 155
		None = 0U,
		// Token: 0x0400009C RID: 156
		CircularLoggingEnabled = 1U,
		// Token: 0x0400009D RID: 157
		IsMultiRole = 2U,
		// Token: 0x0400009E RID: 158
		BackgroundMaintenanceEnabled = 4U
	}
}
