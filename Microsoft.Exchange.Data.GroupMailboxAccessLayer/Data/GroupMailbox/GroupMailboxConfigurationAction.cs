using System;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000035 RID: 53
	[Flags]
	public enum GroupMailboxConfigurationAction
	{
		// Token: 0x040000CA RID: 202
		None = 0,
		// Token: 0x040000CB RID: 203
		SetRegionalSettings = 1,
		// Token: 0x040000CC RID: 204
		CreateDefaultFolders = 2,
		// Token: 0x040000CD RID: 205
		SetInitialFolderPermissions = 4,
		// Token: 0x040000CE RID: 206
		SetAllFolderPermissions = 8,
		// Token: 0x040000CF RID: 207
		ConfigureCalendar = 16,
		// Token: 0x040000D0 RID: 208
		SendWelcomeMessage = 32,
		// Token: 0x040000D1 RID: 209
		GenerateGroupPhoto = 64
	}
}
