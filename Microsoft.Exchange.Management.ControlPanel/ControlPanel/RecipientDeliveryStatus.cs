using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C5 RID: 709
	[DataContract]
	public enum RecipientDeliveryStatus
	{
		// Token: 0x040021E5 RID: 8677
		[EnumMember]
		Unsuccessful,
		// Token: 0x040021E6 RID: 8678
		[EnumMember]
		Pending,
		// Token: 0x040021E7 RID: 8679
		[EnumMember]
		Delivered,
		// Token: 0x040021E8 RID: 8680
		[EnumMember]
		Transferred,
		// Token: 0x040021E9 RID: 8681
		[EnumMember]
		Read,
		// Token: 0x040021EA RID: 8682
		[EnumMember]
		All = 99
	}
}
