using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Provisioning.CompanyManagement
{
	// Token: 0x0200029F RID: 671
	[DataContract(Name = "SubscriptionState", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum SubscriptionState
	{
		// Token: 0x04000E7E RID: 3710
		[EnumMember]
		Active,
		// Token: 0x04000E7F RID: 3711
		[EnumMember]
		Warning,
		// Token: 0x04000E80 RID: 3712
		[EnumMember]
		Suspend,
		// Token: 0x04000E81 RID: 3713
		[EnumMember]
		Delete,
		// Token: 0x04000E82 RID: 3714
		[EnumMember]
		LockOut
	}
}
