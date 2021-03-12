using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000712 RID: 1810
	[Flags]
	internal enum FrontendTransportFlags
	{
		// Token: 0x04003917 RID: 14615
		None = 0,
		// Token: 0x04003918 RID: 14616
		AntispamAgentsEnabled = 1,
		// Token: 0x04003919 RID: 14617
		ConnectivityLogEnabled = 2,
		// Token: 0x0400391A RID: 14618
		ExternalDNSAdapterDisabled = 4,
		// Token: 0x0400391B RID: 14619
		InternalDNSAdapterDisabled = 8
	}
}
