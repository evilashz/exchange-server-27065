using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000066 RID: 102
	internal class MailboxDataForTags : MailboxData, IDisposable
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00019700 File Offset: 0x00017900
		internal MailboxDataForTags()
		{
			this.corruptItemList = new List<StoreObjectId>();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019714 File Offset: 0x00017914
		public void Init(MailboxSession mailboxSession)
		{
			bool flag = false;
			ElcUserTagInformation elcUserTagInformation = null;
			try
			{
				elcUserTagInformation = new ElcUserTagInformation(mailboxSession);
				base.Init(elcUserTagInformation);
				flag = true;
			}
			finally
			{
				if (!flag && elcUserTagInformation != null)
				{
					elcUserTagInformation.Dispose();
					elcUserTagInformation = null;
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00019758 File Offset: 0x00017958
		internal List<StoreObjectId> CorruptItemList
		{
			get
			{
				return this.corruptItemList;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00019760 File Offset: 0x00017960
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00019768 File Offset: 0x00017968
		internal bool PersonalTagDeleted
		{
			get
			{
				return this.personalTagDeleted;
			}
			set
			{
				this.personalTagDeleted = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00019771 File Offset: 0x00017971
		internal ElcUserTagInformation ElcUserTagInformation
		{
			get
			{
				return base.ElcUserInformation as ElcUserTagInformation;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001977E File Offset: 0x0001797E
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.ElcUserTagInformation != null)
				{
					this.ElcUserTagInformation.Dispose();
				}
				base.Dispose(disposing);
				this.disposed = true;
			}
		}

		// Token: 0x040002EF RID: 751
		private List<StoreObjectId> corruptItemList;

		// Token: 0x040002F0 RID: 752
		private bool personalTagDeleted;

		// Token: 0x040002F1 RID: 753
		private bool disposed;
	}
}
