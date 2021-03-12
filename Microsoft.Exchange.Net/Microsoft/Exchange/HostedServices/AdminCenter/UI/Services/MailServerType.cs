using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000862 RID: 2146
	[DataContract(Name = "MailServerType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum MailServerType
	{
		// Token: 0x04002821 RID: 10273
		[EnumMember]
		None,
		// Token: 0x04002822 RID: 10274
		[EnumMember]
		Exchange2007,
		// Token: 0x04002823 RID: 10275
		[EnumMember]
		Exchange2003SP2,
		// Token: 0x04002824 RID: 10276
		[EnumMember]
		Exchange2003SP1,
		// Token: 0x04002825 RID: 10277
		[EnumMember]
		Exchange2000SP3,
		// Token: 0x04002826 RID: 10278
		[EnumMember]
		Exchange2000SP2,
		// Token: 0x04002827 RID: 10279
		[EnumMember]
		Exchange2000SP1,
		// Token: 0x04002828 RID: 10280
		[EnumMember]
		Exchange55,
		// Token: 0x04002829 RID: 10281
		[EnumMember]
		Other
	}
}
