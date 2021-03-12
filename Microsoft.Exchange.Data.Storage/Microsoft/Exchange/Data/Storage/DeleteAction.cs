using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B72 RID: 2930
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeleteAction : Action
	{
		// Token: 0x06006A08 RID: 27144 RVA: 0x001C59C7 File Offset: 0x001C3BC7
		private DeleteAction(Rule rule) : base(ActionType.DeleteAction, rule)
		{
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x001C59D4 File Offset: 0x001C3BD4
		public static DeleteAction Create(Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule
			});
			return new DeleteAction(rule);
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x001C59F8 File Offset: 0x001C3BF8
		internal override RuleAction BuildRuleAction()
		{
			MailboxSession mailboxSession = base.Rule.Folder.Session as MailboxSession;
			if (mailboxSession != null)
			{
				byte[] providerLevelItemId = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems).ProviderLevelItemId;
				return new RuleAction.InMailboxMove(providerLevelItemId);
			}
			throw new NotSupportedException();
		}
	}
}
