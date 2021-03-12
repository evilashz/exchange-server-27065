using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C1 RID: 961
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SortField", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum SortField
	{
		// Token: 0x04001065 RID: 4197
		[EnumMember]
		DisplayName,
		// Token: 0x04001066 RID: 4198
		[EnumMember]
		UserPrincipalName,
		// Token: 0x04001067 RID: 4199
		[EnumMember]
		None
	}
}
