using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000863 RID: 2147
	[DataContract(Name = "IPAddressType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum IPAddressType
	{
		// Token: 0x0400282B RID: 10283
		[EnumMember]
		Outbound,
		// Token: 0x0400282C RID: 10284
		[EnumMember]
		InternalServer,
		// Token: 0x0400282D RID: 10285
		[EnumMember]
		OnPremiseGateway
	}
}
