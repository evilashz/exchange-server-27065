using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A87 RID: 2695
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TeamMailboxDiagnosticsInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040037CE RID: 14286
		public static readonly ProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037CF RID: 14287
		public static readonly SimpleProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2010, typeof(TeamMailboxSyncStatus), PropertyDefinitionFlags.None, TeamMailboxSyncStatus.NotAvailable, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D0 RID: 14288
		public static readonly ProviderPropertyDefinition LastDocumentSyncCycleLog = new SimpleProviderPropertyDefinition("LastDocumentSyncCycleLog", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D1 RID: 14289
		public static readonly ProviderPropertyDefinition LastMembershipSyncCycleLog = new SimpleProviderPropertyDefinition("LastMembershipSyncCycleLog", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D2 RID: 14290
		public static readonly ProviderPropertyDefinition LastMaintenanceSyncCycleLog = new SimpleProviderPropertyDefinition("LastMaintenanceSyncCycleLog", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D3 RID: 14291
		public static readonly ProviderPropertyDefinition DocLibSyncInfo = new SimpleProviderPropertyDefinition("DocLibSyncInfo", ExchangeObjectVersion.Exchange2010, typeof(SyncInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D4 RID: 14292
		public static readonly ProviderPropertyDefinition MembershipSyncInfo = new SimpleProviderPropertyDefinition("MembershipSyncInfo", ExchangeObjectVersion.Exchange2010, typeof(SyncInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040037D5 RID: 14293
		public static readonly ProviderPropertyDefinition MaintenanceSyncInfo = new SimpleProviderPropertyDefinition("MaintenanceSyncInfo", ExchangeObjectVersion.Exchange2010, typeof(SyncInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
