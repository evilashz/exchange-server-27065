using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F3 RID: 1011
	[DataContract(Name = "SkuTargetClass", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum SkuTargetClass
	{
		// Token: 0x04001190 RID: 4496
		[EnumMember]
		NotAvailable,
		// Token: 0x04001191 RID: 4497
		[EnumMember]
		User,
		// Token: 0x04001192 RID: 4498
		[EnumMember]
		Tenant
	}
}
