using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200020D RID: 525
	internal enum PropType
	{
		// Token: 0x04000F44 RID: 3908
		Unspecified,
		// Token: 0x04000F45 RID: 3909
		Null,
		// Token: 0x04000F46 RID: 3910
		Short,
		// Token: 0x04000F47 RID: 3911
		Int,
		// Token: 0x04000F48 RID: 3912
		Float,
		// Token: 0x04000F49 RID: 3913
		Double,
		// Token: 0x04000F4A RID: 3914
		Currency,
		// Token: 0x04000F4B RID: 3915
		AppTime,
		// Token: 0x04000F4C RID: 3916
		Error = 10,
		// Token: 0x04000F4D RID: 3917
		Boolean,
		// Token: 0x04000F4E RID: 3918
		Object = 13,
		// Token: 0x04000F4F RID: 3919
		Long = 20,
		// Token: 0x04000F50 RID: 3920
		AnsiString = 30,
		// Token: 0x04000F51 RID: 3921
		String,
		// Token: 0x04000F52 RID: 3922
		SysTime = 64,
		// Token: 0x04000F53 RID: 3923
		Guid = 72,
		// Token: 0x04000F54 RID: 3924
		Restriction = 253,
		// Token: 0x04000F55 RID: 3925
		Actions,
		// Token: 0x04000F56 RID: 3926
		Binary = 258,
		// Token: 0x04000F57 RID: 3927
		MultiValueFlag = 4096,
		// Token: 0x04000F58 RID: 3928
		ShortArray = 4098,
		// Token: 0x04000F59 RID: 3929
		IntArray,
		// Token: 0x04000F5A RID: 3930
		FloatArray,
		// Token: 0x04000F5B RID: 3931
		DoubleArray,
		// Token: 0x04000F5C RID: 3932
		CurrencyArray,
		// Token: 0x04000F5D RID: 3933
		AppTimeArray,
		// Token: 0x04000F5E RID: 3934
		ObjectArray = 4109,
		// Token: 0x04000F5F RID: 3935
		LongArray = 4116,
		// Token: 0x04000F60 RID: 3936
		AnsiStringArray = 4126,
		// Token: 0x04000F61 RID: 3937
		StringArray,
		// Token: 0x04000F62 RID: 3938
		SysTimeArray = 4160,
		// Token: 0x04000F63 RID: 3939
		GuidArray = 4168,
		// Token: 0x04000F64 RID: 3940
		BinaryArray = 4354,
		// Token: 0x04000F65 RID: 3941
		MultiInstanceFlag = 8192
	}
}
