using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000030 RID: 48
	public enum DisplayType
	{
		// Token: 0x040002DA RID: 730
		DT_MAILUSER,
		// Token: 0x040002DB RID: 731
		DT_DISTLIST,
		// Token: 0x040002DC RID: 732
		DT_FORUM,
		// Token: 0x040002DD RID: 733
		DT_AGENT,
		// Token: 0x040002DE RID: 734
		DT_ORGANIZATION,
		// Token: 0x040002DF RID: 735
		DT_PRIVATE_DISTLIST,
		// Token: 0x040002E0 RID: 736
		DT_REMOTE_MAILUSER,
		// Token: 0x040002E1 RID: 737
		DT_MODIFIABLE = 65536,
		// Token: 0x040002E2 RID: 738
		DT_GLOBAL = 131072,
		// Token: 0x040002E3 RID: 739
		DT_LOCAL = 196608,
		// Token: 0x040002E4 RID: 740
		DT_WAN = 262144,
		// Token: 0x040002E5 RID: 741
		DT_NOT_SPECIFIC = 327680,
		// Token: 0x040002E6 RID: 742
		DT_FOLDER = 16777216,
		// Token: 0x040002E7 RID: 743
		DT_FOLDER_LINK = 33554432,
		// Token: 0x040002E8 RID: 744
		DT_FOLDER_SPECIAL = 67108864
	}
}
