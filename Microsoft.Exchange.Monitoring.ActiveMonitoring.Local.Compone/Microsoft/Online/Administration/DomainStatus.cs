using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D7 RID: 983
	[DataContract(Name = "DomainStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum DomainStatus
	{
		// Token: 0x040010E9 RID: 4329
		[EnumMember]
		Unverified,
		// Token: 0x040010EA RID: 4330
		[EnumMember]
		Verified,
		// Token: 0x040010EB RID: 4331
		[EnumMember]
		PendingDeletion
	}
}
