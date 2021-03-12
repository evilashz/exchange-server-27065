using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C8 RID: 968
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RoleMemberType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum RoleMemberType
	{
		// Token: 0x04001088 RID: 4232
		[EnumMember]
		User,
		// Token: 0x04001089 RID: 4233
		[EnumMember]
		Group
	}
}
