using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000109 RID: 265
	internal class DownlevelReportHelper : ReportHelper<List<ReportEntry>>
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00012A70 File Offset: 0x00010C70
		public DownlevelReportHelper(string reportFolder, string messageClass) : base(reportFolder, messageClass)
		{
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00012A7A File Offset: 0x00010C7A
		protected override void SaveEntries(List<ReportEntry> entries, MoveObjectInfo<List<ReportEntry>> moveObjectInfo)
		{
			moveObjectInfo.SaveObject(entries);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00012A83 File Offset: 0x00010C83
		protected override List<ReportEntry> DeserializeEntries(MoveObjectInfo<List<ReportEntry>> moveObjectInfo, bool loadLastChunkOnly)
		{
			return moveObjectInfo.ReadObject(ReadObjectFlags.DontThrowOnCorruptData);
		}
	}
}
