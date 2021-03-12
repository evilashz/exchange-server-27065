using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000201 RID: 513
	internal enum RestrictionType : uint
	{
		// Token: 0x04000657 RID: 1623
		And,
		// Token: 0x04000658 RID: 1624
		Or,
		// Token: 0x04000659 RID: 1625
		Not,
		// Token: 0x0400065A RID: 1626
		Content,
		// Token: 0x0400065B RID: 1627
		Property,
		// Token: 0x0400065C RID: 1628
		CompareProps,
		// Token: 0x0400065D RID: 1629
		BitMask,
		// Token: 0x0400065E RID: 1630
		Size,
		// Token: 0x0400065F RID: 1631
		Exists,
		// Token: 0x04000660 RID: 1632
		SubRestriction,
		// Token: 0x04000661 RID: 1633
		Comment,
		// Token: 0x04000662 RID: 1634
		Count,
		// Token: 0x04000663 RID: 1635
		Near = 13U,
		// Token: 0x04000664 RID: 1636
		True = 131U,
		// Token: 0x04000665 RID: 1637
		False,
		// Token: 0x04000666 RID: 1638
		Null = 255U
	}
}
