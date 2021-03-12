using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000185 RID: 389
	internal sealed class SplitQuotaVerifier : ISplitQuotaVerifier
	{
		// Token: 0x06000F80 RID: 3968 RVA: 0x0005BDBA File Offset: 0x00059FBA
		public SplitQuotaVerifier(IPublicFolderSession publicFolderSession, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory)
		{
			this.publicFolderSession = publicFolderSession;
			this.logger = logger;
			this.powershellFactory = powershellFactory;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0005BDD8 File Offset: 0x00059FD8
		public bool IsSplitNeeded()
		{
			bool result = false;
			Unlimited<ByteQuantifiedSize> mailboxQuota = PublicFolderSplitHelper.GetMailboxQuota(this.logger, this.powershellFactory, this.publicFolderSession.MailboxGuid, this.publicFolderSession.OrganizationId, null);
			Unlimited<ByteQuantifiedSize> unlimited = ByteQuantifiedSize.Zero;
			Unlimited<ByteQuantifiedSize> unlimited2 = ByteQuantifiedSize.Zero;
			if (!mailboxQuota.IsUnlimited)
			{
				Unlimited<ByteQuantifiedSize> totalItemSize;
				unlimited = (totalItemSize = PublicFolderSplitHelper.GetTotalItemSize(this.logger, this.powershellFactory, this.publicFolderSession.MailboxGuid, this.publicFolderSession.OrganizationId, null));
				if (totalItemSize.Value.ToBytes() >= mailboxQuota.Value.ToBytes() * PublicFolderSplitConfig.Instance.SplitThreshold / 100UL)
				{
					Unlimited<ByteQuantifiedSize> actualItemSize;
					unlimited2 = (actualItemSize = PublicFolderSplitHelper.GetActualItemSize(this.logger, this.powershellFactory, this.publicFolderSession.MailboxGuid, this.publicFolderSession.OrganizationId, null));
					if (actualItemSize.Value.ToBytes() >= mailboxQuota.Value.ToBytes() * PublicFolderSplitConfig.Instance.SplitThreshold / 100UL)
					{
						result = true;
					}
				}
			}
			this.logger.LogEvent(LogEventType.Statistics, string.Format("IsSplitNeeded={0},MailboxQuota={1},TotalItemSize={2},ActualItemSize={3}", new object[]
			{
				result.ToString(),
				mailboxQuota.ToString(),
				unlimited.ToString(),
				unlimited2.ToString()
			}));
			return result;
		}

		// Token: 0x040009D8 RID: 2520
		private readonly IPublicFolderMailboxLoggerBase logger;

		// Token: 0x040009D9 RID: 2521
		private readonly IPublicFolderSession publicFolderSession;

		// Token: 0x040009DA RID: 2522
		private readonly IAssistantRunspaceFactory powershellFactory;
	}
}
