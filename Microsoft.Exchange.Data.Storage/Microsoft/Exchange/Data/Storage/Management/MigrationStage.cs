using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A37 RID: 2615
	[Serializable]
	public enum MigrationStage
	{
		// Token: 0x04003657 RID: 13911
		[LocDescription(ServerStrings.IDs.MigrationStageDiscovery)]
		Discovery = 16,
		// Token: 0x04003658 RID: 13912
		[LocDescription(ServerStrings.IDs.MigrationStageValidation)]
		Validation = 21,
		// Token: 0x04003659 RID: 13913
		[LocDescription(ServerStrings.IDs.MigrationStageInjection)]
		Injection = 26,
		// Token: 0x0400365A RID: 13914
		[LocDescription(ServerStrings.IDs.MigrationStageProcessing)]
		Processing = 31
	}
}
