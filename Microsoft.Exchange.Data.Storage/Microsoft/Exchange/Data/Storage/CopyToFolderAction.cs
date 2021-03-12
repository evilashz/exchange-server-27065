using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B76 RID: 2934
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CopyToFolderAction : IdAction
	{
		// Token: 0x06006A14 RID: 27156 RVA: 0x001C5B2A File Offset: 0x001C3D2A
		private CopyToFolderAction(StoreObjectId id, Rule rule) : base(ActionType.CopyToFolderAction, id, rule)
		{
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x001C5B38 File Offset: 0x001C3D38
		public static CopyToFolderAction Create(StoreObjectId folderId, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				folderId
			});
			if (!IdConverter.IsFolderId(folderId))
			{
				throw new ArgumentException("folderId");
			}
			return new CopyToFolderAction(folderId, rule);
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x001C5B74 File Offset: 0x001C3D74
		internal override RuleAction BuildRuleAction()
		{
			MailboxSession mailboxSession = base.Rule.Folder.Session as MailboxSession;
			if (mailboxSession != null)
			{
				byte[] providerLevelItemId = base.Id.ProviderLevelItemId;
				return new RuleAction.InMailboxCopy(providerLevelItemId);
			}
			throw new NotSupportedException();
		}
	}
}
