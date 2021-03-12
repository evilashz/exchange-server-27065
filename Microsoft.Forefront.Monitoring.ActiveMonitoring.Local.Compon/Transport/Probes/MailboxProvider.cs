using System;
using System.Collections.Generic;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport.Probes
{
	// Token: 0x02000270 RID: 624
	internal class MailboxProvider : IMailboxProvider
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x0003D000 File Offset: 0x0003B200
		private MailboxProvider()
		{
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0003D008 File Offset: 0x0003B208
		public static MailboxProvider GetInstance()
		{
			return MailboxProvider.instance;
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0003D010 File Offset: 0x0003B210
		public MailboxSelectionResult TryGetMailboxToUse(out Guid mbxGuid, out Guid mdbGuid, out string emailAddress)
		{
			emailAddress = null;
			mbxGuid = Guid.Empty;
			mdbGuid = Guid.Empty;
			MailboxSelectionResult result;
			MailboxDatabaseInfo mailboxDatabaseInfo = this.PickAnActiveMailbox(out result);
			if (mailboxDatabaseInfo != null)
			{
				mbxGuid = mailboxDatabaseInfo.MonitoringAccountMailboxGuid;
				mdbGuid = mailboxDatabaseInfo.MailboxDatabaseGuid;
				emailAddress = string.Format("{0}@{1}", mailboxDatabaseInfo.MonitoringAccount, mailboxDatabaseInfo.MonitoringAccountDomain);
			}
			return result;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0003D074 File Offset: 0x0003B274
		public MailboxDatabaseSelectionResult GetAllMailboxDatabaseInfo(out ICollection<MailboxDatabaseInfo> mailboxDatabases)
		{
			MailboxDatabaseSelectionResult result = MailboxDatabaseSelectionResult.Success;
			mailboxDatabases = null;
			LocalEndpointManager localEndpointManager = LocalEndpointManager.Instance;
			if (localEndpointManager == null || localEndpointManager.MailboxDatabaseEndpoint == null)
			{
				return MailboxDatabaseSelectionResult.NoLocalEndpointManager;
			}
			mailboxDatabases = localEndpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			if (mailboxDatabases == null || mailboxDatabases.Count == 0)
			{
				return MailboxDatabaseSelectionResult.NoMonitoringMDBs;
			}
			return result;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0003D0BC File Offset: 0x0003B2BC
		private MailboxDatabaseInfo PickAnActiveMailbox(out MailboxSelectionResult mailboxSelectionResult)
		{
			mailboxSelectionResult = MailboxSelectionResult.Success;
			LocalEndpointManager localEndpointManager = LocalEndpointManager.Instance;
			ICollection<MailboxDatabaseInfo> mailboxDatabaseInfoCollectionForBackend = localEndpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			if (mailboxDatabaseInfoCollectionForBackend == null || mailboxDatabaseInfoCollectionForBackend.Count == 0)
			{
				mailboxSelectionResult = MailboxSelectionResult.NoMonitoringMDBs;
				return null;
			}
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in mailboxDatabaseInfoCollectionForBackend)
			{
				if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseInfo.MailboxDatabaseGuid))
				{
					return mailboxDatabaseInfo;
				}
			}
			mailboxSelectionResult = MailboxSelectionResult.NoMonitoringMDBsAreActive;
			return null;
		}

		// Token: 0x040009D4 RID: 2516
		private static readonly MailboxProvider instance = new MailboxProvider();
	}
}
