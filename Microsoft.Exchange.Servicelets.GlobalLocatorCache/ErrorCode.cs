using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000015 RID: 21
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public enum ErrorCode
	{
		// Token: 0x04000046 RID: 70
		[EnumMember]
		UnknownError,
		// Token: 0x04000047 RID: 71
		[EnumMember]
		Authentication,
		// Token: 0x04000048 RID: 72
		[EnumMember]
		Authorization,
		// Token: 0x04000049 RID: 73
		[EnumMember]
		Arguments,
		// Token: 0x0400004A RID: 74
		[EnumMember]
		Server
	}
}
