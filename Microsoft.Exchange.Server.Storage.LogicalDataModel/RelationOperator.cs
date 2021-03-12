using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000B4 RID: 180
	public enum RelationOperator : byte
	{
		// Token: 0x040004C6 RID: 1222
		LessThan,
		// Token: 0x040004C7 RID: 1223
		LessThanEqual,
		// Token: 0x040004C8 RID: 1224
		GreaterThan,
		// Token: 0x040004C9 RID: 1225
		GreaterThanEqual,
		// Token: 0x040004CA RID: 1226
		Equal,
		// Token: 0x040004CB RID: 1227
		NotEqual,
		// Token: 0x040004CC RID: 1228
		Like,
		// Token: 0x040004CD RID: 1229
		MemberOfDistributionList = 100
	}
}
