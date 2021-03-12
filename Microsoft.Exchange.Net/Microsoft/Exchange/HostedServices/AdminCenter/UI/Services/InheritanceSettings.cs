using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200084A RID: 2122
	[DataContract(Name = "InheritanceSettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Flags]
	internal enum InheritanceSettings
	{
		// Token: 0x04002764 RID: 10084
		[EnumMember]
		InheritInboundIPConfig = 1,
		// Token: 0x04002765 RID: 10085
		[EnumMember]
		InheritOutboundIPConfig = 2,
		// Token: 0x04002766 RID: 10086
		[EnumMember]
		InheritOnPremiseGatewayIPConfig = 4,
		// Token: 0x04002767 RID: 10087
		[EnumMember]
		InheritInternalServerIPConfig = 8,
		// Token: 0x04002768 RID: 10088
		[EnumMember]
		InheritRecipientLevelRoutingConfig = 16,
		// Token: 0x04002769 RID: 10089
		[EnumMember]
		InheritSkipListConfig = 32,
		// Token: 0x0400276A RID: 10090
		[EnumMember]
		InheritDirectoryEdgeBlockModeConfig = 64,
		// Token: 0x0400276B RID: 10091
		[EnumMember]
		InheritSubscriptions = 128
	}
}
