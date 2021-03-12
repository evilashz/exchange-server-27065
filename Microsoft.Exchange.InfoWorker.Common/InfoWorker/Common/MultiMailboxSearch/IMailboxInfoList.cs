using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001F1 RID: 497
	internal interface IMailboxInfoList : IList<MailboxInfo>, ICollection<MailboxInfo>, IEnumerable<MailboxInfo>, IEnumerable
	{
	}
}
