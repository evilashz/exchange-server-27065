using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F6F RID: 3951
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupMailboxOwnerAdapter : IMailboxOwner
	{
		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x06008706 RID: 34566 RVA: 0x002503E5 File Offset: 0x0024E5E5
		public bool SideConversationProcessingEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x06008707 RID: 34567 RVA: 0x002503E8 File Offset: 0x0024E5E8
		public bool ThreadedConversationProcessingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x06008708 RID: 34568 RVA: 0x002503EB File Offset: 0x0024E5EB
		public bool ModernConversationPreparationEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170023C9 RID: 9161
		// (get) Token: 0x06008709 RID: 34569 RVA: 0x002503EE File Offset: 0x0024E5EE
		public bool SearchDuplicatedMessagesEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023CA RID: 9162
		// (get) Token: 0x0600870A RID: 34570 RVA: 0x002503F1 File Offset: 0x0024E5F1
		public bool IsGroupMailbox
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600870B RID: 34571 RVA: 0x002503F4 File Offset: 0x0024E5F4
		public bool SentToMySelf(ICorePropertyBag item)
		{
			return false;
		}

		// Token: 0x170023CB RID: 9163
		// (get) Token: 0x0600870C RID: 34572 RVA: 0x002503F7 File Offset: 0x0024E5F7
		public bool RequestExtraPropertiesWhenSearching
		{
			get
			{
				return true;
			}
		}
	}
}
