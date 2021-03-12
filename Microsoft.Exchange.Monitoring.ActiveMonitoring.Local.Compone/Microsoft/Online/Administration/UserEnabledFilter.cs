using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C3 RID: 963
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UserEnabledFilter", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum UserEnabledFilter
	{
		// Token: 0x0400106C RID: 4204
		[EnumMember]
		All,
		// Token: 0x0400106D RID: 4205
		[EnumMember]
		EnabledOnly,
		// Token: 0x0400106E RID: 4206
		[EnumMember]
		DisabledOnly
	}
}
