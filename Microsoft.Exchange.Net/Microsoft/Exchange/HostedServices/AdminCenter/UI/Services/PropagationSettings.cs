using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000850 RID: 2128
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "PropagationSettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[Flags]
	internal enum PropagationSettings
	{
		// Token: 0x04002798 RID: 10136
		[EnumMember]
		PropagateOutboundIPConfig = 1,
		// Token: 0x04002799 RID: 10137
		[EnumMember]
		PropagateRecipientLevelRoutingConfig = 2,
		// Token: 0x0400279A RID: 10138
		[EnumMember]
		PropagateSkipListConfig = 4,
		// Token: 0x0400279B RID: 10139
		[EnumMember]
		PropagateDirectoryEdgeBlockModeConfig = 8
	}
}
