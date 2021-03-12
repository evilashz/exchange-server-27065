using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E5 RID: 229
	internal class CompressedReportHelper : ReportHelper<CompressedReport>
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x00010E2F File Offset: 0x0000F02F
		public CompressedReportHelper(string reportFolder, string messageClass) : base(reportFolder, messageClass)
		{
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00010E3C File Offset: 0x0000F03C
		protected override void SaveEntries(List<ReportEntry> entries, MoveObjectInfo<CompressedReport> moveObjectInfo)
		{
			if (entries != null && entries.Count > 100)
			{
				List<CompressedReport> list = new List<CompressedReport>();
				int num = 0;
				while (num + 100 < entries.Count)
				{
					list.Add(new CompressedReport(entries.GetRange(num, 100)));
					num += 100;
				}
				list.Add(new CompressedReport(entries.GetRange(num, entries.Count - num)));
				int num2 = TestIntegration.Instance.MaxReportEntryCount / 100;
				if (num2 < 1)
				{
					num2 = 1;
				}
				moveObjectInfo.SaveObjectChunks(list, num2, null);
				return;
			}
			moveObjectInfo.SaveObject(new CompressedReport(entries));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00010ECC File Offset: 0x0000F0CC
		protected override List<ReportEntry> DeserializeEntries(MoveObjectInfo<CompressedReport> moveObjectInfo, bool loadLastChunkOnly)
		{
			ReadObjectFlags readObjectFlags = ReadObjectFlags.DontThrowOnCorruptData;
			if (loadLastChunkOnly)
			{
				readObjectFlags |= ReadObjectFlags.LastChunkOnly;
			}
			List<CompressedReport> list = moveObjectInfo.ReadObjectChunks(readObjectFlags);
			if (list == null)
			{
				return null;
			}
			List<ReportEntry> list2 = new List<ReportEntry>();
			foreach (CompressedReport compressedReport in list)
			{
				list2.AddRange(compressedReport.Entries);
			}
			return list2;
		}

		// Token: 0x04000513 RID: 1299
		public const int MaxReportEntryCountDefault = 10000;

		// Token: 0x04000514 RID: 1300
		public const int ChunkSize = 100;
	}
}
