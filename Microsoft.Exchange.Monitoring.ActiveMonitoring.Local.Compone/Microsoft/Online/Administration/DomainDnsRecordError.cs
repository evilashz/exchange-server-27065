using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003FA RID: 1018
	[DataContract(Name = "DomainDnsRecordError", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum DomainDnsRecordError
	{
		// Token: 0x040011A5 RID: 4517
		[EnumMember]
		Unknown,
		// Token: 0x040011A6 RID: 4518
		[EnumMember]
		ServiceUnlicensedOrPendingProvision
	}
}
