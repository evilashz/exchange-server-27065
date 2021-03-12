using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000853 RID: 2131
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "DomainMailFlowType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[Flags]
	internal enum DomainMailFlowType
	{
		// Token: 0x040027A7 RID: 10151
		[EnumMember]
		Inbound = 1,
		// Token: 0x040027A8 RID: 10152
		[EnumMember]
		Outbound = 2,
		// Token: 0x040027A9 RID: 10153
		[EnumMember]
		InboundOutbound = 5
	}
}
