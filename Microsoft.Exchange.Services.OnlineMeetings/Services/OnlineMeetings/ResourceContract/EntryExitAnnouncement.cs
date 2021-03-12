using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000058 RID: 88
	[DataContract(Name = "entryExitAnnouncement")]
	internal enum EntryExitAnnouncement
	{
		// Token: 0x040001B2 RID: 434
		[EnumMember]
		Unsupported,
		// Token: 0x040001B3 RID: 435
		[EnumMember]
		Disabled,
		// Token: 0x040001B4 RID: 436
		[EnumMember]
		Enabled
	}
}
