using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A4D RID: 2637
	[Flags]
	[Serializable]
	public enum SkippableMigrationSteps
	{
		// Token: 0x040036E6 RID: 14054
		[LocDescription(ServerStrings.IDs.MigrationSkippableStepNone)]
		None = 0,
		// Token: 0x040036E7 RID: 14055
		[LocDescription(ServerStrings.IDs.MigrationSkippableStepSettingTargetAddress)]
		SettingTargetAddress = 1
	}
}
