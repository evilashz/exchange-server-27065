using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C4 RID: 964
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GroupType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum GroupType
	{
		// Token: 0x04001070 RID: 4208
		[EnumMember]
		DistributionList,
		// Token: 0x04001071 RID: 4209
		[EnumMember]
		Security,
		// Token: 0x04001072 RID: 4210
		[EnumMember]
		MailEnabledSecurity
	}
}
