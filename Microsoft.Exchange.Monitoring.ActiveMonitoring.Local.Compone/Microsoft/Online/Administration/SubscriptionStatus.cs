using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003F1 RID: 1009
	[DataContract(Name = "SubscriptionStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum SubscriptionStatus
	{
		// Token: 0x0400117D RID: 4477
		[EnumMember]
		Enabled,
		// Token: 0x0400117E RID: 4478
		[EnumMember]
		Warning,
		// Token: 0x0400117F RID: 4479
		[EnumMember]
		Suspended,
		// Token: 0x04001180 RID: 4480
		[EnumMember]
		Deleted,
		// Token: 0x04001181 RID: 4481
		[EnumMember]
		Other
	}
}
