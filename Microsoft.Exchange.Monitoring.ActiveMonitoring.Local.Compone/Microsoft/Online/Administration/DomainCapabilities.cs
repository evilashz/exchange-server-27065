using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D6 RID: 982
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainCapabilities", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[Flags]
	public enum DomainCapabilities
	{
		// Token: 0x040010E1 RID: 4321
		[EnumMember]
		None = 0,
		// Token: 0x040010E2 RID: 4322
		[EnumMember]
		Email = 1,
		// Token: 0x040010E3 RID: 4323
		[EnumMember]
		Sharepoint = 2,
		// Token: 0x040010E4 RID: 4324
		[EnumMember]
		OfficeCommunicationsOnline = 4,
		// Token: 0x040010E5 RID: 4325
		[EnumMember]
		SharepointDefault = 8,
		// Token: 0x040010E6 RID: 4326
		[EnumMember]
		FullRedelegation = 16,
		// Token: 0x040010E7 RID: 4327
		[EnumMember]
		All = 31
	}
}
