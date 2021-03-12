using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F2 RID: 498
	internal class MailboxInfoList : List<MailboxInfo>, IMailboxInfoList, IList<MailboxInfo>, ICollection<MailboxInfo>, IEnumerable<MailboxInfo>, IEnumerable
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x00037397 File Offset: 0x00035597
		public MailboxInfoList(MailboxInfo[] mailboxes) : base(mailboxes)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000373A0 File Offset: 0x000355A0
		public MailboxInfoList()
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000373A8 File Offset: 0x000355A8
		public MailboxInfoList(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000373B4 File Offset: 0x000355B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = base.Count - 1;
			for (int i = 0; i < base.Count; i++)
			{
				stringBuilder.Append(base[i].DisplayName);
				stringBuilder.Append(base[i].IsPrimary ? "(Primary)" : "(Archive)");
				if (i != num)
				{
					stringBuilder.Append(',');
				}
			}
			return stringBuilder.ToString();
		}
	}
}
