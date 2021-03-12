using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADSessionProxy : IADSession
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public ADSessionProxy(IAnchorADProvider anchorAdProvider)
		{
			this.anchorAdProvider = anchorAdProvider;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000238C File Offset: 0x0000058C
		public IEnumerable<RecipientWrapper> FindPagedMiniRecipient(UpgradeBatchCreatorScheduler.MailboxType mailboxType, ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			ADPagedReader<MiniRecipient> pagedReader = this.anchorAdProvider.FindPagedMiniRecipient<MiniRecipient>(rootId, scope, filter, sortBy, pageSize, properties);
			foreach (MiniRecipient recipient in pagedReader)
			{
				RequestStatus moveStatus = (RequestStatus)recipient[SharedPropertyDefinitions.MailboxMoveStatus];
				string moveBatchName = (string)recipient[SharedPropertyDefinitions.MailboxMoveBatchName];
				RequestFlags requestFlags = (RequestFlags)recipient[SharedPropertyDefinitions.MailboxMoveFlags];
				RecipientWrapper wrappedRecipient = new RecipientWrapper(recipient.Id, moveStatus, moveBatchName, requestFlags, recipient.RecipientType, recipient.RecipientTypeDetails);
				yield return wrappedRecipient;
			}
			yield break;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000025FC File Offset: 0x000007FC
		public IEnumerable<RecipientWrapper> FindPilotUsersADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			ADPagedReader<ADRawEntry> pagedReader = this.anchorAdProvider.FindPagedADRawEntry(rootId, scope, filter, sortBy, pageSize, properties);
			foreach (ADRawEntry adRawEntry in pagedReader)
			{
				RecipientWrapper wrappedRecipient = new RecipientWrapper(adRawEntry.Id, RequestStatus.None, null, RequestFlags.None, RecipientType.UserMailbox, RecipientTypeDetails.None);
				yield return wrappedRecipient;
			}
			yield break;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002646 File Offset: 0x00000846
		public QueryFilter BuildE14MailboxQueryFilter()
		{
			return CommonUtils.BuildMbxFilter(((AnchorADProvider)this.anchorAdProvider).ConfigurationSession, Server.E14MinVersion, Server.E15MinVersion);
		}

		// Token: 0x04000001 RID: 1
		private readonly IAnchorADProvider anchorAdProvider;
	}
}
