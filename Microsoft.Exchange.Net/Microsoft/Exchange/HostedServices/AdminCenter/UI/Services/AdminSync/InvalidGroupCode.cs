using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200087F RID: 2175
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "InvalidGroupCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	internal enum InvalidGroupCode
	{
		// Token: 0x04002855 RID: 10325
		[EnumMember]
		GroupDoesNotExist,
		// Token: 0x04002856 RID: 10326
		[EnumMember]
		GroupBelongsToDifferentCompany
	}
}
