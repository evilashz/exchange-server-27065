using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000414 RID: 1044
	[Flags]
	internal enum ElcContentSettingFlags
	{
		// Token: 0x04001FB6 RID: 8118
		None = 0,
		// Token: 0x04001FB7 RID: 8119
		RetentionEnabled = 1,
		// Token: 0x04001FB8 RID: 8120
		MoveDateBasedRetention = 2,
		// Token: 0x04001FB9 RID: 8121
		JournalingEnabled = 4,
		// Token: 0x04001FBA RID: 8122
		JournalAsMSG = 8,
		// Token: 0x04001FBB RID: 8123
		Tag = 16
	}
}
