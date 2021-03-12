using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.BDM.Pets.SharedLibrary.Enums
{
	// Token: 0x02000BC6 RID: 3014
	[DataContract(Name = "ResourceRecordType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary.Enums")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public enum ResourceRecordType
	{
		// Token: 0x0400383A RID: 14394
		[EnumMember]
		DNS_TYPE_ZERO,
		// Token: 0x0400383B RID: 14395
		[EnumMember]
		DNS_TYPE_A,
		// Token: 0x0400383C RID: 14396
		[EnumMember]
		DNS_TYPE_NS,
		// Token: 0x0400383D RID: 14397
		[EnumMember]
		DNS_TYPE_CNAME = 5,
		// Token: 0x0400383E RID: 14398
		[EnumMember]
		DNS_TYPE_SOA,
		// Token: 0x0400383F RID: 14399
		[EnumMember]
		DNS_TYPE_MX = 15,
		// Token: 0x04003840 RID: 14400
		[EnumMember]
		DNS_TYPE_TEXT,
		// Token: 0x04003841 RID: 14401
		[EnumMember]
		DNS_TYPE_SRV = 33
	}
}
