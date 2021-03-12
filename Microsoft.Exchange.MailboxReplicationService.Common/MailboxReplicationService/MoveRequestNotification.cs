using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000018 RID: 24
	[DataContract]
	internal enum MoveRequestNotification
	{
		// Token: 0x040000D3 RID: 211
		[EnumMember]
		Created = 1,
		// Token: 0x040000D4 RID: 212
		[EnumMember]
		Updated,
		// Token: 0x040000D5 RID: 213
		[EnumMember]
		Canceled,
		// Token: 0x040000D6 RID: 214
		[EnumMember]
		SuspendResume
	}
}
