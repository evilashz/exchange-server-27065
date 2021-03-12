using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000040 RID: 64
	[DataContract]
	[Flags]
	internal enum MessageRecFlags
	{
		// Token: 0x0400026A RID: 618
		[EnumMember]
		None = 0,
		// Token: 0x0400026B RID: 619
		[EnumMember]
		Deleted = 1,
		// Token: 0x0400026C RID: 620
		[EnumMember]
		Regular = 2,
		// Token: 0x0400026D RID: 621
		[EnumMember]
		Associated = 4
	}
}
