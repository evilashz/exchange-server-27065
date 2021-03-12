using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003C9 RID: 969
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ValidationStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum ValidationStatus
	{
		// Token: 0x0400108B RID: 4235
		[EnumMember]
		NotAvailable,
		// Token: 0x0400108C RID: 4236
		[EnumMember]
		Healthy,
		// Token: 0x0400108D RID: 4237
		[EnumMember]
		Error
	}
}
