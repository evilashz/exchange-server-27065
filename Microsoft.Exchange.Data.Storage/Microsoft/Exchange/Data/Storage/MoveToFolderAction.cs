using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B75 RID: 2933
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MoveToFolderAction : IdAction
	{
		// Token: 0x06006A10 RID: 27152 RVA: 0x001C5A84 File Offset: 0x001C3C84
		private MoveToFolderAction(StoreObjectId id, Rule rule) : base(ActionType.MoveToFolderAction, id, rule)
		{
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x001C5A9C File Offset: 0x001C3C9C
		public static MoveToFolderAction Create(StoreObjectId folderId, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				folderId
			});
			if (!IdConverter.IsFolderId(folderId))
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentNullException("folderId");
				}, "folderId");
			}
			return new MoveToFolderAction(folderId, rule);
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x001C5AF8 File Offset: 0x001C3CF8
		internal override RuleAction BuildRuleAction()
		{
			StoreSession session = base.Rule.Folder.Session;
			byte[] providerLevelItemId = base.Id.ProviderLevelItemId;
			return new RuleAction.InMailboxMove(providerLevelItemId);
		}
	}
}
