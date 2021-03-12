using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C5 RID: 965
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Page", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum Page
	{
		// Token: 0x04001074 RID: 4212
		[EnumMember]
		First,
		// Token: 0x04001075 RID: 4213
		[EnumMember]
		Next,
		// Token: 0x04001076 RID: 4214
		[EnumMember]
		Previous,
		// Token: 0x04001077 RID: 4215
		[EnumMember]
		Last
	}
}
