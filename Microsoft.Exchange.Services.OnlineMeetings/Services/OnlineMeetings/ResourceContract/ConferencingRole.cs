using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200005B RID: 91
	[DataContract(Name = "conferencingRole")]
	internal enum ConferencingRole
	{
		// Token: 0x040001C0 RID: 448
		[EnumMember]
		None,
		// Token: 0x040001C1 RID: 449
		[EnumMember]
		Attendee,
		// Token: 0x040001C2 RID: 450
		[EnumMember]
		Leader
	}
}
