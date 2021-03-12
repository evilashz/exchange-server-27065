using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Provisioning.LoadBalancing
{
	// Token: 0x02000207 RID: 519
	internal class MailboxDatabaseWithLocationInfo
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00039076 File Offset: 0x00037276
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0003907E File Offset: 0x0003727E
		public MailboxDatabase MailboxDatabase { get; private set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00039087 File Offset: 0x00037287
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0003908F File Offset: 0x0003728F
		public DatabaseLocationInfo DatabaseLocationInfo { get; private set; }

		// Token: 0x06001220 RID: 4640 RVA: 0x00039098 File Offset: 0x00037298
		public MailboxDatabaseWithLocationInfo(MailboxDatabase mailboxDatabase, DatabaseLocationInfo databaseLocationInfo)
		{
			if (mailboxDatabase == null)
			{
				throw new ArgumentNullException("mailboxDatabase");
			}
			if (databaseLocationInfo == null)
			{
				throw new ArgumentNullException("databaseLocationInfo");
			}
			this.MailboxDatabase = mailboxDatabase;
			this.DatabaseLocationInfo = databaseLocationInfo;
		}
	}
}
