using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200087D RID: 2173
	[DataContract(Name = "InternalFaultCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum InternalFaultCode
	{
		// Token: 0x04002850 RID: 10320
		[EnumMember]
		DatabaseError,
		// Token: 0x04002851 RID: 10321
		[EnumMember]
		ConfigurationError
	}
}
