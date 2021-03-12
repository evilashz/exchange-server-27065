using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000059 RID: 89
	[DataContract(Name = "automaticLeaderAssignment")]
	internal enum AutomaticLeaderAssignment : long
	{
		// Token: 0x040001B6 RID: 438
		[EnumMember]
		Disabled,
		// Token: 0x040001B7 RID: 439
		[EnumMember]
		SameEnterprise = 32768L,
		// Token: 0x040001B8 RID: 440
		[EnumMember]
		Everyone = 2147483648L
	}
}
