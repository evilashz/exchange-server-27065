using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A8 RID: 680
	[Flags]
	internal enum StreamPropertyType : short
	{
		// Token: 0x04000E6A RID: 3690
		Null = 1,
		// Token: 0x04000E6B RID: 3691
		Bool = 2,
		// Token: 0x04000E6C RID: 3692
		Byte = 3,
		// Token: 0x04000E6D RID: 3693
		SByte = 4,
		// Token: 0x04000E6E RID: 3694
		Int16 = 5,
		// Token: 0x04000E6F RID: 3695
		UInt16 = 6,
		// Token: 0x04000E70 RID: 3696
		Int32 = 7,
		// Token: 0x04000E71 RID: 3697
		UInt32 = 8,
		// Token: 0x04000E72 RID: 3698
		Int64 = 9,
		// Token: 0x04000E73 RID: 3699
		UInt64 = 10,
		// Token: 0x04000E74 RID: 3700
		Single = 11,
		// Token: 0x04000E75 RID: 3701
		Double = 12,
		// Token: 0x04000E76 RID: 3702
		Decimal = 13,
		// Token: 0x04000E77 RID: 3703
		Char = 14,
		// Token: 0x04000E78 RID: 3704
		String = 15,
		// Token: 0x04000E79 RID: 3705
		DateTime = 16,
		// Token: 0x04000E7A RID: 3706
		Guid = 17,
		// Token: 0x04000E7B RID: 3707
		IPAddress = 18,
		// Token: 0x04000E7C RID: 3708
		IPEndPoint = 19,
		// Token: 0x04000E7D RID: 3709
		RoutingAddress = 20,
		// Token: 0x04000E7E RID: 3710
		ADObjectId = 21,
		// Token: 0x04000E7F RID: 3711
		RecipientType = 22,
		// Token: 0x04000E80 RID: 3712
		ADObjectIdUTF8 = 23,
		// Token: 0x04000E81 RID: 3713
		ADObjectIdWithString = 24,
		// Token: 0x04000E82 RID: 3714
		ProxyAddress = 25,
		// Token: 0x04000E83 RID: 3715
		Array = 4096,
		// Token: 0x04000E84 RID: 3716
		List = 8192,
		// Token: 0x04000E85 RID: 3717
		MultiValuedProperty = 16384
	}
}
