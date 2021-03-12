using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A95 RID: 2709
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderMailboxMonitoringInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003808 RID: 14344
		internal const string NumberOfBatchesExecuted = "NumberOfBatchesExecuted";

		// Token: 0x04003809 RID: 14345
		internal const string NumberOfFoldersToBeSynced = "NumberOfFoldersToBeSynced";

		// Token: 0x0400380A RID: 14346
		internal const string NumberOfFoldersSynced = "NumberOfFoldersSynced";

		// Token: 0x0400380B RID: 14347
		internal const string BatchSize = "BatchSize";

		// Token: 0x0400380C RID: 14348
		internal const string LastAttemptedSyncTimeFieldName = "LastAttemptedSyncTime";

		// Token: 0x0400380D RID: 14349
		internal const string LastSuccessfulSyncTimeFieldName = "LastSuccessfulSyncTime";

		// Token: 0x0400380E RID: 14350
		internal const string LastFailedSyncTimeFieldName = "LastFailedSyncTime";

		// Token: 0x0400380F RID: 14351
		internal const string LastSyncFailureFieldName = "LastSyncFailure";

		// Token: 0x04003810 RID: 14352
		internal const string NumberofAttemptsAfterLastSuccessFieldName = "NumberofAttemptsAfterLastSuccess";

		// Token: 0x04003811 RID: 14353
		internal const string FirstFailedSyncTimeAfterLastSuccessFieldName = "FirstFailedSyncTimeAfterLastSuccess";

		// Token: 0x04003812 RID: 14354
		internal const string DiagnosticsInfoFieldName = "DiagnosticsInfo";

		// Token: 0x04003813 RID: 14355
		public static readonly ProviderPropertyDefinition DisplayName = new SimpleProviderPropertyDefinition("DisplayName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003814 RID: 14356
		public static readonly ProviderPropertyDefinition LastAttemptedSyncTime = new SimpleProviderPropertyDefinition("LastAttemptedSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003815 RID: 14357
		public static readonly ProviderPropertyDefinition LastSuccessfulSyncTime = new SimpleProviderPropertyDefinition("LastSuccessfulSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003816 RID: 14358
		public static readonly ProviderPropertyDefinition LastFailedSyncTime = new SimpleProviderPropertyDefinition("LastFailedSyncTime", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003817 RID: 14359
		public static readonly ProviderPropertyDefinition LastSyncFailure = new SimpleProviderPropertyDefinition("LastSyncFailure", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003818 RID: 14360
		public static readonly ProviderPropertyDefinition NumberofAttemptsAfterLastSuccess = new SimpleProviderPropertyDefinition("NumberofAttemptsAfterLastSuccess", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003819 RID: 14361
		public static readonly ProviderPropertyDefinition FirstFailedSyncTimeAfterLastSuccess = new SimpleProviderPropertyDefinition("FirstFailedSyncTimeAfterLastSuccess", ExchangeObjectVersion.Exchange2010, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400381A RID: 14362
		public static readonly ProviderPropertyDefinition DiagnosticsInfo = new SimpleProviderPropertyDefinition("DiagnosticsInfo", ExchangeObjectVersion.Exchange2010, typeof(PublicFolderMailboxDiagnosticsInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400381B RID: 14363
		public static readonly ProviderPropertyDefinition LastSyncCycleLog = new SimpleProviderPropertyDefinition("LastSyncCycleLog", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
