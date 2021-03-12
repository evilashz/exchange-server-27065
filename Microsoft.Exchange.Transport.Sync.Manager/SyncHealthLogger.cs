using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncHealthLogger : ISyncHealthLog
	{
		// Token: 0x06000347 RID: 839 RVA: 0x000165F4 File Offset: 0x000147F4
		private SyncHealthLogger()
		{
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000165FC File Offset: 0x000147FC
		public static SyncHealthLogger Instance
		{
			get
			{
				return SyncHealthLogger.instance;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00016603 File Offset: 0x00014803
		public void LogWorkTypeBudgets(KeyValuePair<string, object>[] eventData)
		{
			SyncHealthLogManager.LogWorkTypeBudgets(eventData);
		}

		// Token: 0x040001DA RID: 474
		private static SyncHealthLogger instance = new SyncHealthLogger();
	}
}
