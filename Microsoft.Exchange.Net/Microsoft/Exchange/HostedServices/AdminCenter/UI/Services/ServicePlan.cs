using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000851 RID: 2129
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "ServicePlan", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	internal enum ServicePlan
	{
		// Token: 0x0400279D RID: 10141
		[EnumMember]
		None,
		// Token: 0x0400279E RID: 10142
		[EnumMember]
		Office365Enterprises,
		// Token: 0x0400279F RID: 10143
		[EnumMember]
		Office365EDU,
		// Token: 0x040027A0 RID: 10144
		[EnumMember]
		Office365SB
	}
}
