using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x0200002A RID: 42
	public static class SyncHealthLogSchema
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00007839 File Offset: 0x00005A39
		public static CsvTable SyncHealthLogEvent
		{
			get
			{
				return SyncHealthLogSchema.syncHealthLogEvent;
			}
		}

		// Token: 0x04000089 RID: 137
		private static readonly Version E14Version = new Version("14.00.0565.00");

		// Token: 0x0400008A RID: 138
		private static readonly CsvTable syncHealthLogEvent = new CsvTable(new CsvField[]
		{
			new CsvField("TimeStamp", typeof(DateTime), SyncHealthLogSchema.E14Version),
			new CsvField("EventId", typeof(string), SyncHealthLogSchema.E14Version),
			new CsvField("EventData", typeof(KeyValuePair<string, object>[]), SyncHealthLogSchema.E14Version)
		});
	}
}
