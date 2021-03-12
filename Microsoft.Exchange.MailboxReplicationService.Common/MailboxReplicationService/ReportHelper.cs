using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E4 RID: 228
	internal abstract class ReportHelper<T> : IReportHelper where T : class
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00010A20 File Offset: 0x0000EC20
		public ReportHelper(string reportFolder, string messageClass)
		{
			if (string.IsNullOrEmpty(reportFolder))
			{
				throw new ArgumentNullException("A Report folder name should be provided!");
			}
			if (string.IsNullOrEmpty(messageClass))
			{
				throw new ArgumentNullException("A Report message class must be provided!");
			}
			this.reportFolderName = reportFolder;
			this.reportMessageClass = messageClass;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00010A5C File Offset: 0x0000EC5C
		public static List<ReportEntry> MergeEntries(List<ReportEntry> entries1, List<ReportEntry> entries2)
		{
			if (entries1 == null || entries1.Count == 0)
			{
				return entries2;
			}
			if (entries2 == null || entries2.Count == 0)
			{
				return entries1;
			}
			List<ReportEntry> list = new List<ReportEntry>(entries1.Count + entries2.Count);
			int num = 0;
			int num2 = 0;
			while (num < entries1.Count || num2 < entries2.Count)
			{
				ReportEntry reportEntry = (num < entries1.Count) ? entries1[num] : ReportEntry.MaxEntry;
				ReportEntry reportEntry2 = (num2 < entries2.Count) ? entries2[num2] : ReportEntry.MaxEntry;
				if (reportEntry2.CreationTime < reportEntry.CreationTime)
				{
					list.Add(reportEntry2);
					num2++;
				}
				else
				{
					list.Add(reportEntry);
					num++;
				}
			}
			return list;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00010B10 File Offset: 0x0000ED10
		void IReportHelper.Load(ReportData report, MapiStore storeToUse)
		{
			using (MoveObjectInfo<T> moveObjectInfo = new MoveObjectInfo<T>(Guid.Empty, storeToUse, report.MessageId, this.reportFolderName, this.reportMessageClass, this.CreateMessageSubject(report), this.CreateMessageSearchKey(report)))
			{
				if (!moveObjectInfo.OpenMessage())
				{
					report.Entries = null;
				}
				else
				{
					report.MessageId = moveObjectInfo.MessageId;
					report.Entries = this.DeserializeEntries(moveObjectInfo, false);
				}
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		void IReportHelper.Flush(ReportData report, MapiStore storeToUse)
		{
			MapiUtils.RetryOnObjectChanged(delegate
			{
				using (MoveObjectInfo<T> moveObjectInfo = new MoveObjectInfo<T>(Guid.Empty, storeToUse, report.MessageId, this.reportFolderName, this.reportMessageClass, this.CreateMessageSubject(report), this.CreateMessageSearchKey(report)))
				{
					if (moveObjectInfo.OpenMessage())
					{
						report.MessageId = moveObjectInfo.MessageId;
						List<ReportEntry> entries = this.DeserializeEntries(moveObjectInfo, true);
						List<ReportEntry> entries2 = ReportHelper<T>.MergeEntries(entries, report.NewEntries) ?? new List<ReportEntry>();
						this.SaveEntries(entries2, moveObjectInfo);
					}
					else
					{
						List<ReportEntry> entries2 = report.Entries ?? new List<ReportEntry>();
						this.SaveEntries(entries2, moveObjectInfo);
						report.MessageId = moveObjectInfo.MessageId;
					}
					report.Entries = null;
				}
			});
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00010D80 File Offset: 0x0000EF80
		void IReportHelper.Delete(ReportData report, MapiStore storeToUse)
		{
			MapiUtils.RetryOnObjectChanged(delegate
			{
				using (MoveObjectInfo<T> moveObjectInfo = new MoveObjectInfo<T>(Guid.Empty, storeToUse, report.MessageId, this.reportFolderName, this.reportMessageClass, this.CreateMessageSubject(report), this.CreateMessageSearchKey(report)))
				{
					moveObjectInfo.OpenMessage();
					if (moveObjectInfo.MessageFound)
					{
						moveObjectInfo.DeleteMessage();
					}
				}
			});
			report.MessageId = null;
		}

		// Token: 0x060008C5 RID: 2245
		protected abstract void SaveEntries(List<ReportEntry> entries, MoveObjectInfo<T> moveObjectInfo);

		// Token: 0x060008C6 RID: 2246
		protected abstract List<ReportEntry> DeserializeEntries(MoveObjectInfo<T> moveObjectInfo, bool loadLastChunkOnly);

		// Token: 0x060008C7 RID: 2247 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		private string CreateMessageSubject(ReportData report)
		{
			return string.Format(CultureInfo.InvariantCulture, "Subject = Mailbox Move Report : {0}", new object[]
			{
				report.IdentifyingGuid.ToString()
			});
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00010E04 File Offset: 0x0000F004
		private byte[] CreateMessageSearchKey(ReportData report)
		{
			byte[] array = new byte[16];
			report.IdentifyingGuid.ToByteArray().CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000511 RID: 1297
		private readonly string reportFolderName;

		// Token: 0x04000512 RID: 1298
		private readonly string reportMessageClass;
	}
}
