using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000033 RID: 51
	[DataContract]
	internal enum CreateMailboxResult
	{
		// Token: 0x040001AA RID: 426
		[EnumMember]
		Success,
		// Token: 0x040001AB RID: 427
		[EnumMember]
		CleanupNotComplete,
		// Token: 0x040001AC RID: 428
		[EnumMember]
		ObjectNotFound
	}
}
