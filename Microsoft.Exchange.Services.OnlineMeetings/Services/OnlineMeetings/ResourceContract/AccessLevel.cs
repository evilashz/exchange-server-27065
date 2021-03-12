using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200005A RID: 90
	[DataContract(Name = "accessLevel")]
	internal enum AccessLevel
	{
		// Token: 0x040001BA RID: 442
		[EnumMember]
		SameEnterprise = 1,
		// Token: 0x040001BB RID: 443
		[EnumMember]
		None = 0,
		// Token: 0x040001BC RID: 444
		[EnumMember]
		Locked = 2,
		// Token: 0x040001BD RID: 445
		[EnumMember]
		Invited,
		// Token: 0x040001BE RID: 446
		[EnumMember]
		Everyone
	}
}
