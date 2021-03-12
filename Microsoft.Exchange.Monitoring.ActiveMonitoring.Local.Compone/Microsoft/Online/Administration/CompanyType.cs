using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D2 RID: 978
	[DataContract(Name = "CompanyType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum CompanyType
	{
		// Token: 0x040010CF RID: 4303
		[EnumMember]
		CompanyTenant,
		// Token: 0x040010D0 RID: 4304
		[EnumMember]
		MicrosoftSupportTenant,
		// Token: 0x040010D1 RID: 4305
		[EnumMember]
		SyndicatePartnerTenant,
		// Token: 0x040010D2 RID: 4306
		[EnumMember]
		SupportPartnerTenant
	}
}
