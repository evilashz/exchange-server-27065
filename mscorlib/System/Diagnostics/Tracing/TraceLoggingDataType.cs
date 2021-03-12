using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000455 RID: 1109
	internal enum TraceLoggingDataType
	{
		// Token: 0x040017B4 RID: 6068
		Nil,
		// Token: 0x040017B5 RID: 6069
		Utf16String,
		// Token: 0x040017B6 RID: 6070
		MbcsString,
		// Token: 0x040017B7 RID: 6071
		Int8,
		// Token: 0x040017B8 RID: 6072
		UInt8,
		// Token: 0x040017B9 RID: 6073
		Int16,
		// Token: 0x040017BA RID: 6074
		UInt16,
		// Token: 0x040017BB RID: 6075
		Int32,
		// Token: 0x040017BC RID: 6076
		UInt32,
		// Token: 0x040017BD RID: 6077
		Int64,
		// Token: 0x040017BE RID: 6078
		UInt64,
		// Token: 0x040017BF RID: 6079
		Float,
		// Token: 0x040017C0 RID: 6080
		Double,
		// Token: 0x040017C1 RID: 6081
		Boolean32,
		// Token: 0x040017C2 RID: 6082
		Binary,
		// Token: 0x040017C3 RID: 6083
		Guid,
		// Token: 0x040017C4 RID: 6084
		FileTime = 17,
		// Token: 0x040017C5 RID: 6085
		SystemTime,
		// Token: 0x040017C6 RID: 6086
		HexInt32 = 20,
		// Token: 0x040017C7 RID: 6087
		HexInt64,
		// Token: 0x040017C8 RID: 6088
		CountedUtf16String,
		// Token: 0x040017C9 RID: 6089
		CountedMbcsString,
		// Token: 0x040017CA RID: 6090
		Struct,
		// Token: 0x040017CB RID: 6091
		Char16 = 518,
		// Token: 0x040017CC RID: 6092
		Char8 = 516,
		// Token: 0x040017CD RID: 6093
		Boolean8 = 772,
		// Token: 0x040017CE RID: 6094
		HexInt8 = 1028,
		// Token: 0x040017CF RID: 6095
		HexInt16 = 1030,
		// Token: 0x040017D0 RID: 6096
		Utf16Xml = 2817,
		// Token: 0x040017D1 RID: 6097
		MbcsXml,
		// Token: 0x040017D2 RID: 6098
		CountedUtf16Xml = 2838,
		// Token: 0x040017D3 RID: 6099
		CountedMbcsXml,
		// Token: 0x040017D4 RID: 6100
		Utf16Json = 3073,
		// Token: 0x040017D5 RID: 6101
		MbcsJson,
		// Token: 0x040017D6 RID: 6102
		CountedUtf16Json = 3094,
		// Token: 0x040017D7 RID: 6103
		CountedMbcsJson,
		// Token: 0x040017D8 RID: 6104
		HResult = 3847
	}
}
