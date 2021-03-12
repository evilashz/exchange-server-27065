using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000031 RID: 49
	[Flags]
	[DataContract]
	internal enum EnumerateMessagesFlags
	{
		// Token: 0x0400019F RID: 415
		[EnumMember]
		RegularMessages = 1,
		// Token: 0x040001A0 RID: 416
		[EnumMember]
		DeletedMessages = 2,
		// Token: 0x040001A1 RID: 417
		[EnumMember]
		IncludeExtendedData = 4,
		// Token: 0x040001A2 RID: 418
		[EnumMember]
		ReturnLongTermIDs = 8,
		// Token: 0x040001A3 RID: 419
		[EnumMember]
		SkipICSMidSetMissing = 16,
		// Token: 0x040001A4 RID: 420
		[EnumMember]
		AllMessages = 3
	}
}
