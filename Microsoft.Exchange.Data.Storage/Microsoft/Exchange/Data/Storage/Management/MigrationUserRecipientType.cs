using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A40 RID: 2624
	[Serializable]
	public enum MigrationUserRecipientType
	{
		// Token: 0x04003688 RID: 13960
		Mailbox,
		// Token: 0x04003689 RID: 13961
		Contact,
		// Token: 0x0400368A RID: 13962
		Group,
		// Token: 0x0400368B RID: 13963
		PublicFolder,
		// Token: 0x0400368C RID: 13964
		Unsupported,
		// Token: 0x0400368D RID: 13965
		Mailuser,
		// Token: 0x0400368E RID: 13966
		MailboxOrMailuser
	}
}
