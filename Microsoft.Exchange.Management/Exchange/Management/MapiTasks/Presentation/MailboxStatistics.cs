using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.Management.MapiTasks.Presentation
{
	// Token: 0x02000488 RID: 1160
	[Serializable]
	public sealed class MailboxStatistics : MailboxStatistics
	{
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000A2151 File Offset: 0x000A0351
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x000A2159 File Offset: 0x000A0359
		public List<MoveHistoryEntry> MoveHistory { get; internal set; }
	}
}
