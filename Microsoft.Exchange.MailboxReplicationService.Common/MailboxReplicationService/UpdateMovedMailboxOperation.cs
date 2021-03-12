using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000035 RID: 53
	[DataContract]
	internal enum UpdateMovedMailboxOperation
	{
		// Token: 0x040001B2 RID: 434
		[EnumMember]
		UpdateMailbox = 1,
		// Token: 0x040001B3 RID: 435
		[EnumMember]
		MorphToMailbox,
		// Token: 0x040001B4 RID: 436
		[EnumMember]
		MorphToMailUser,
		// Token: 0x040001B5 RID: 437
		[EnumMember]
		UpdateArchiveOnly
	}
}
