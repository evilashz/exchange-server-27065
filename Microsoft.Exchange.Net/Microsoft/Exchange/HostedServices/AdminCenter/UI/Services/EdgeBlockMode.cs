using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200084E RID: 2126
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "EdgeBlockMode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	internal enum EdgeBlockMode
	{
		// Token: 0x04002791 RID: 10129
		[EnumMember]
		Reject = 1,
		// Token: 0x04002792 RID: 10130
		[EnumMember]
		Test = 3,
		// Token: 0x04002793 RID: 10131
		[EnumMember]
		Disabled = 5
	}
}
