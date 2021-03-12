using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005FD RID: 1533
	internal enum MimeItemType
	{
		// Token: 0x040022A4 RID: 8868
		MimeMessageGeneric,
		// Token: 0x040022A5 RID: 8869
		MimeMessageSmime,
		// Token: 0x040022A6 RID: 8870
		MimeMessageSmimeMultipartSigned,
		// Token: 0x040022A7 RID: 8871
		MimeMessageDsn,
		// Token: 0x040022A8 RID: 8872
		MimeMessageMdn,
		// Token: 0x040022A9 RID: 8873
		MimeMessageJournalTnef,
		// Token: 0x040022AA RID: 8874
		MimeMessageJournalMsg,
		// Token: 0x040022AB RID: 8875
		MimeMessageSecondaryJournal,
		// Token: 0x040022AC RID: 8876
		MimeMessageCalendar,
		// Token: 0x040022AD RID: 8877
		MimeMessageReplication
	}
}
