using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C2C RID: 3116
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ErrorCode", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public enum ErrorCode
	{
		// Token: 0x040039EB RID: 14827
		[EnumMember]
		UnknownError,
		// Token: 0x040039EC RID: 14828
		[EnumMember]
		Authentication,
		// Token: 0x040039ED RID: 14829
		[EnumMember]
		Authorization,
		// Token: 0x040039EE RID: 14830
		[EnumMember]
		Arguments,
		// Token: 0x040039EF RID: 14831
		[EnumMember]
		Server,
		// Token: 0x040039F0 RID: 14832
		[EnumMember]
		DataNotFound
	}
}
