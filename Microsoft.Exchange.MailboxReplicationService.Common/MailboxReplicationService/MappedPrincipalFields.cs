using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003E RID: 62
	[DataContract]
	[Flags]
	internal enum MappedPrincipalFields
	{
		// Token: 0x04000255 RID: 597
		None = 0,
		// Token: 0x04000256 RID: 598
		MailboxGuid = 1,
		// Token: 0x04000257 RID: 599
		ObjectSid = 2,
		// Token: 0x04000258 RID: 600
		ObjectGuid = 4,
		// Token: 0x04000259 RID: 601
		ObjectDN = 8,
		// Token: 0x0400025A RID: 602
		LegacyDN = 16,
		// Token: 0x0400025B RID: 603
		ProxyAddresses = 32,
		// Token: 0x0400025C RID: 604
		Alias = 64,
		// Token: 0x0400025D RID: 605
		DisplayName = 128
	}
}
