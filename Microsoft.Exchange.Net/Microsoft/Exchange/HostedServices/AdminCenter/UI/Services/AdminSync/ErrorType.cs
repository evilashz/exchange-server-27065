using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000879 RID: 2169
	[DataContract(Name = "ErrorType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum ErrorType
	{
		// Token: 0x04002844 RID: 10308
		[EnumMember]
		Permanent,
		// Token: 0x04002845 RID: 10309
		[EnumMember]
		Transient
	}
}
