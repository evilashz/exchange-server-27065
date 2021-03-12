using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000034 RID: 52
	[DataContract]
	internal enum MailboxType
	{
		// Token: 0x040001AE RID: 430
		[EnumMember]
		SourceMailbox,
		// Token: 0x040001AF RID: 431
		[EnumMember]
		DestMailboxIntraOrg,
		// Token: 0x040001B0 RID: 432
		[EnumMember]
		DestMailboxCrossOrg
	}
}
