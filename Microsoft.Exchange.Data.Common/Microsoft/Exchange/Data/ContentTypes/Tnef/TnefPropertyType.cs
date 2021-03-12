using System;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000EA RID: 234
	public enum TnefPropertyType : short
	{
		// Token: 0x04000813 RID: 2067
		Unspecified,
		// Token: 0x04000814 RID: 2068
		Null,
		// Token: 0x04000815 RID: 2069
		I2,
		// Token: 0x04000816 RID: 2070
		Long,
		// Token: 0x04000817 RID: 2071
		R4,
		// Token: 0x04000818 RID: 2072
		Double,
		// Token: 0x04000819 RID: 2073
		Currency,
		// Token: 0x0400081A RID: 2074
		AppTime,
		// Token: 0x0400081B RID: 2075
		Error = 10,
		// Token: 0x0400081C RID: 2076
		Boolean,
		// Token: 0x0400081D RID: 2077
		Object = 13,
		// Token: 0x0400081E RID: 2078
		I8 = 20,
		// Token: 0x0400081F RID: 2079
		String8 = 30,
		// Token: 0x04000820 RID: 2080
		Unicode,
		// Token: 0x04000821 RID: 2081
		SysTime = 64,
		// Token: 0x04000822 RID: 2082
		ClassId = 72,
		// Token: 0x04000823 RID: 2083
		Binary = 258,
		// Token: 0x04000824 RID: 2084
		MultiValued = 4096
	}
}
