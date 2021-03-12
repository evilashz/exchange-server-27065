using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200085E RID: 2142
	[DataContract(Name = "TargetObject", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum TargetObject
	{
		// Token: 0x040027C8 RID: 10184
		[EnumMember]
		None,
		// Token: 0x040027C9 RID: 10185
		[EnumMember]
		Company,
		// Token: 0x040027CA RID: 10186
		[EnumMember]
		Domain,
		// Token: 0x040027CB RID: 10187
		[EnumMember]
		SmtpProfile,
		// Token: 0x040027CC RID: 10188
		[EnumMember]
		OutboundIP,
		// Token: 0x040027CD RID: 10189
		[EnumMember]
		OnPremiseGatewayIP,
		// Token: 0x040027CE RID: 10190
		[EnumMember]
		InternalServerIP,
		// Token: 0x040027CF RID: 10191
		[EnumMember]
		RecipientLevelRoutingConfig,
		// Token: 0x040027D0 RID: 10192
		[EnumMember]
		SkipListConfig,
		// Token: 0x040027D1 RID: 10193
		[EnumMember]
		DirectoryEdgeBlockConfig,
		// Token: 0x040027D2 RID: 10194
		[EnumMember]
		CompanyConfigurationSettings,
		// Token: 0x040027D3 RID: 10195
		[EnumMember]
		DomainConfigurationSettings,
		// Token: 0x040027D4 RID: 10196
		[EnumMember]
		Subscription
	}
}
