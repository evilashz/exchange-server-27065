using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000093 RID: 147
	public enum OperationCategory
	{
		// Token: 0x040002E0 RID: 736
		Unknown,
		// Token: 0x040002E1 RID: 737
		GetBaseKey,
		// Token: 0x040002E2 RID: 738
		OpenKey,
		// Token: 0x040002E3 RID: 739
		OpenOrCreateKey,
		// Token: 0x040002E4 RID: 740
		DeleteKey,
		// Token: 0x040002E5 RID: 741
		GetSubKeyNames,
		// Token: 0x040002E6 RID: 742
		CloseKey,
		// Token: 0x040002E7 RID: 743
		SetValue,
		// Token: 0x040002E8 RID: 744
		GetValue,
		// Token: 0x040002E9 RID: 745
		DeleteValue,
		// Token: 0x040002EA RID: 746
		GetValueNames,
		// Token: 0x040002EB RID: 747
		GetValueInfos,
		// Token: 0x040002EC RID: 748
		ExecuteBatch,
		// Token: 0x040002ED RID: 749
		CreateChangeNotify,
		// Token: 0x040002EE RID: 750
		GetAllValues
	}
}
