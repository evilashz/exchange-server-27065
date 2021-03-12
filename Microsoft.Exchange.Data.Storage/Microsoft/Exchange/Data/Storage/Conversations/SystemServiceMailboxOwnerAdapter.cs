using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F74 RID: 3956
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SystemServiceMailboxOwnerAdapter : IMailboxOwner
	{
		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x0600872A RID: 34602 RVA: 0x00250700 File Offset: 0x0024E900
		public bool SideConversationProcessingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x0600872B RID: 34603 RVA: 0x00250703 File Offset: 0x0024E903
		public bool ThreadedConversationProcessingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x0600872C RID: 34604 RVA: 0x00250706 File Offset: 0x0024E906
		public bool ModernConversationPreparationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x0600872D RID: 34605 RVA: 0x00250709 File Offset: 0x0024E909
		public bool SearchDuplicatedMessagesEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x0600872E RID: 34606 RVA: 0x0025070C File Offset: 0x0024E90C
		public bool IsGroupMailbox
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600872F RID: 34607 RVA: 0x0025070F File Offset: 0x0024E90F
		public bool SentToMySelf(ICorePropertyBag item)
		{
			return false;
		}

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x06008730 RID: 34608 RVA: 0x00250712 File Offset: 0x0024E912
		public bool RequestExtraPropertiesWhenSearching
		{
			get
			{
				return false;
			}
		}
	}
}
