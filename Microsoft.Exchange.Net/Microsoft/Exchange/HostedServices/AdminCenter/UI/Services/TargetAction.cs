using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000852 RID: 2130
	[DataContract(Name = "TargetAction", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum TargetAction
	{
		// Token: 0x040027A2 RID: 10146
		[EnumMember]
		None,
		// Token: 0x040027A3 RID: 10147
		[EnumMember]
		Add,
		// Token: 0x040027A4 RID: 10148
		[EnumMember]
		Set,
		// Token: 0x040027A5 RID: 10149
		[EnumMember]
		Remove
	}
}
