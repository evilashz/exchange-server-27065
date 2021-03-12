using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009D9 RID: 2521
	internal abstract class TaskFolderActionBase<T> where T : TaskFolderActionResponse
	{
		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x000FEDA6 File Offset: 0x000FCFA6
		// (set) Token: 0x06004747 RID: 18247 RVA: 0x000FEDAE File Offset: 0x000FCFAE
		private protected MailboxSession MailboxSession { protected get; private set; }

		// Token: 0x06004748 RID: 18248 RVA: 0x000FEDB7 File Offset: 0x000FCFB7
		public TaskFolderActionBase(MailboxSession session)
		{
			this.MailboxSession = session;
		}

		// Token: 0x06004749 RID: 18249
		public abstract T Execute();
	}
}
