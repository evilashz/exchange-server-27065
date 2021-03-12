using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000702 RID: 1794
	[Flags]
	public enum ManagementRoleEntryType
	{
		// Token: 0x040038B6 RID: 14518
		Cmdlet = 1,
		// Token: 0x040038B7 RID: 14519
		Script = 2,
		// Token: 0x040038B8 RID: 14520
		ApplicationPermission = 4,
		// Token: 0x040038B9 RID: 14521
		WebService = 8,
		// Token: 0x040038BA RID: 14522
		All = 255
	}
}
