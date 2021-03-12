using System;

namespace System.Reflection
{
	// Token: 0x020005AB RID: 1451
	[Serializable]
	internal enum CustomAttributeEncoding
	{
		// Token: 0x04001BAC RID: 7084
		Undefined,
		// Token: 0x04001BAD RID: 7085
		Boolean = 2,
		// Token: 0x04001BAE RID: 7086
		Char,
		// Token: 0x04001BAF RID: 7087
		SByte,
		// Token: 0x04001BB0 RID: 7088
		Byte,
		// Token: 0x04001BB1 RID: 7089
		Int16,
		// Token: 0x04001BB2 RID: 7090
		UInt16,
		// Token: 0x04001BB3 RID: 7091
		Int32,
		// Token: 0x04001BB4 RID: 7092
		UInt32,
		// Token: 0x04001BB5 RID: 7093
		Int64,
		// Token: 0x04001BB6 RID: 7094
		UInt64,
		// Token: 0x04001BB7 RID: 7095
		Float,
		// Token: 0x04001BB8 RID: 7096
		Double,
		// Token: 0x04001BB9 RID: 7097
		String,
		// Token: 0x04001BBA RID: 7098
		Array = 29,
		// Token: 0x04001BBB RID: 7099
		Type = 80,
		// Token: 0x04001BBC RID: 7100
		Object,
		// Token: 0x04001BBD RID: 7101
		Field = 83,
		// Token: 0x04001BBE RID: 7102
		Property,
		// Token: 0x04001BBF RID: 7103
		Enum
	}
}
