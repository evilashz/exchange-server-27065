using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000AF RID: 175
	[Flags]
	public enum TenantConnectorSource
	{
		// Token: 0x040002AB RID: 683
		[LocDescription(DataStrings.IDs.ConnectorSourceDefault)]
		Default = 0,
		// Token: 0x040002AC RID: 684
		[LocDescription(DataStrings.IDs.ConnectorSourceMigrated)]
		Migrated = 1,
		// Token: 0x040002AD RID: 685
		[LocDescription(DataStrings.IDs.ConnectorSourceHybridWizard)]
		HybridWizard = 2
	}
}
