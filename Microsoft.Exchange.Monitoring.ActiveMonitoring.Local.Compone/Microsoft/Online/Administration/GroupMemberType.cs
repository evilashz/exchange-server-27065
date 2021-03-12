using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003DE RID: 990
	[DataContract(Name = "GroupMemberType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum GroupMemberType
	{
		// Token: 0x04001118 RID: 4376
		[EnumMember]
		User,
		// Token: 0x04001119 RID: 4377
		[EnumMember]
		Group,
		// Token: 0x0400111A RID: 4378
		[EnumMember]
		Contact
	}
}
