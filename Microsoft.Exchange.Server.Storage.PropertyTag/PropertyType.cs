using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000003 RID: 3
	public enum PropertyType : ushort
	{
		// Token: 0x0400001A RID: 26
		Unspecified,
		// Token: 0x0400001B RID: 27
		Null,
		// Token: 0x0400001C RID: 28
		Int16,
		// Token: 0x0400001D RID: 29
		Int32,
		// Token: 0x0400001E RID: 30
		Real32,
		// Token: 0x0400001F RID: 31
		Real64,
		// Token: 0x04000020 RID: 32
		Currency,
		// Token: 0x04000021 RID: 33
		AppTime,
		// Token: 0x04000022 RID: 34
		Error = 10,
		// Token: 0x04000023 RID: 35
		Boolean,
		// Token: 0x04000024 RID: 36
		Object = 13,
		// Token: 0x04000025 RID: 37
		Int64 = 20,
		// Token: 0x04000026 RID: 38
		String8 = 30,
		// Token: 0x04000027 RID: 39
		Unicode,
		// Token: 0x04000028 RID: 40
		SysTime = 64,
		// Token: 0x04000029 RID: 41
		Guid = 72,
		// Token: 0x0400002A RID: 42
		SvrEid = 251,
		// Token: 0x0400002B RID: 43
		SRestriction = 253,
		// Token: 0x0400002C RID: 44
		Actions,
		// Token: 0x0400002D RID: 45
		Binary = 258,
		// Token: 0x0400002E RID: 46
		Invalid = 4095,
		// Token: 0x0400002F RID: 47
		MVFlag,
		// Token: 0x04000030 RID: 48
		MVNull,
		// Token: 0x04000031 RID: 49
		MVInt16,
		// Token: 0x04000032 RID: 50
		MVInt32,
		// Token: 0x04000033 RID: 51
		MVReal32,
		// Token: 0x04000034 RID: 52
		MVInt64 = 4116,
		// Token: 0x04000035 RID: 53
		MVCurrency = 4102,
		// Token: 0x04000036 RID: 54
		MVReal64 = 4101,
		// Token: 0x04000037 RID: 55
		MVGuid = 4168,
		// Token: 0x04000038 RID: 56
		MVString8 = 4126,
		// Token: 0x04000039 RID: 57
		MVUnicode,
		// Token: 0x0400003A RID: 58
		MVBinary = 4354,
		// Token: 0x0400003B RID: 59
		MVAppTime = 4103,
		// Token: 0x0400003C RID: 60
		MVSysTime = 4160,
		// Token: 0x0400003D RID: 61
		MVInvalid = 8191,
		// Token: 0x0400003E RID: 62
		MVInstance,
		// Token: 0x0400003F RID: 63
		MVINull = 12289,
		// Token: 0x04000040 RID: 64
		MVIInt16,
		// Token: 0x04000041 RID: 65
		MVIInt32,
		// Token: 0x04000042 RID: 66
		MVIReal32,
		// Token: 0x04000043 RID: 67
		MVIInt64 = 12308,
		// Token: 0x04000044 RID: 68
		MVICurrency = 12294,
		// Token: 0x04000045 RID: 69
		MVIReal64 = 12293,
		// Token: 0x04000046 RID: 70
		MVIGuid = 12360,
		// Token: 0x04000047 RID: 71
		MVIString8 = 12318,
		// Token: 0x04000048 RID: 72
		MVIUnicode,
		// Token: 0x04000049 RID: 73
		MVIBinary = 12546,
		// Token: 0x0400004A RID: 74
		MVIAppTime = 12295,
		// Token: 0x0400004B RID: 75
		MVISysTime = 12352,
		// Token: 0x0400004C RID: 76
		MVIInvalid = 16383
	}
}
