using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003DB RID: 987
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainVerificationMode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum DomainVerificationMode
	{
		// Token: 0x040010FE RID: 4350
		[EnumMember]
		DnsTxtRecord,
		// Token: 0x040010FF RID: 4351
		[EnumMember]
		DnsMXRecord,
		// Token: 0x04001100 RID: 4352
		[EnumMember]
		Other
	}
}
