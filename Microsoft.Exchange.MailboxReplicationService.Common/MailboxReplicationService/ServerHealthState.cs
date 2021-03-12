using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000045 RID: 69
	[DataContract]
	internal enum ServerHealthState
	{
		// Token: 0x0400027F RID: 639
		[EnumMember]
		Unknown,
		// Token: 0x04000280 RID: 640
		[EnumMember]
		Healthy,
		// Token: 0x04000281 RID: 641
		[EnumMember]
		NotHealthy
	}
}
