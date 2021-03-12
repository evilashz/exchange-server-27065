using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F72 RID: 3954
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullMailboxOwnerAdapter : IMailboxOwner
	{
		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x06008720 RID: 34592 RVA: 0x002506C3 File Offset: 0x0024E8C3
		public bool SideConversationProcessingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x06008721 RID: 34593 RVA: 0x002506C6 File Offset: 0x0024E8C6
		public bool ThreadedConversationProcessingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x06008722 RID: 34594 RVA: 0x002506C9 File Offset: 0x0024E8C9
		public bool ModernConversationPreparationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x06008723 RID: 34595 RVA: 0x002506CC File Offset: 0x0024E8CC
		public bool SearchDuplicatedMessagesEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x06008724 RID: 34596 RVA: 0x002506CF File Offset: 0x0024E8CF
		public bool IsGroupMailbox
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008725 RID: 34597 RVA: 0x002506D2 File Offset: 0x0024E8D2
		public bool SentToMySelf(ICorePropertyBag item)
		{
			return false;
		}

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x06008726 RID: 34598 RVA: 0x002506D5 File Offset: 0x0024E8D5
		public bool RequestExtraPropertiesWhenSearching
		{
			get
			{
				return false;
			}
		}
	}
}
