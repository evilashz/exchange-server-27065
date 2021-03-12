using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C0 RID: 960
	[DataContract(Name = "SortDirection", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum SortDirection
	{
		// Token: 0x04001062 RID: 4194
		[EnumMember]
		Ascending,
		// Token: 0x04001063 RID: 4195
		[EnumMember]
		Descending
	}
}
