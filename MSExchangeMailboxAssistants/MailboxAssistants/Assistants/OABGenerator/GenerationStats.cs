using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001DA RID: 474
	internal sealed class GenerationStats : IPerformanceDataLogger
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x000680F5 File Offset: 0x000662F5
		public GenerationStats()
		{
			this.DomainControllersUsed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.StagePerfStats = new Dictionary<string, TimeSpan>(40);
			this.CounterBasedPerfStats = new Dictionary<string, long>(20);
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00068132 File Offset: 0x00066332
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x0006813A File Offset: 0x0006633A
		public OfflineAddressBook OfflineAddressBook { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00068143 File Offset: 0x00066343
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0006814B File Offset: 0x0006634B
		public string Tenant { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00068154 File Offset: 0x00066354
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0006815C File Offset: 0x0006635C
		public DateTime StartTimestamp { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00068165 File Offset: 0x00066365
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0006816D File Offset: 0x0006636D
		public DateTime FinishTimestamp { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00068176 File Offset: 0x00066376
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0006817E File Offset: 0x0006637E
		public bool UnnecessaryGeneration { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00068187 File Offset: 0x00066387
		// (set) Token: 0x06001225 RID: 4645 RVA: 0x0006818F File Offset: 0x0006638F
		public bool HABEnabled { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00068198 File Offset: 0x00066398
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x000681A0 File Offset: 0x000663A0
		public bool GenerationSucceeded { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x000681A9 File Offset: 0x000663A9
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x000681B1 File Offset: 0x000663B1
		public TimeSpan IODuration { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x000681BA File Offset: 0x000663BA
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x000681C2 File Offset: 0x000663C2
		public int TotalNumberOfRecords { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000681CB File Offset: 0x000663CB
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x000681D3 File Offset: 0x000663D3
		public int TotalNumberOfTempFiles { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000681DC File Offset: 0x000663DC
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x000681E4 File Offset: 0x000663E4
		public HashSet<string> DomainControllersUsed { get; private set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x000681ED File Offset: 0x000663ED
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x000681F5 File Offset: 0x000663F5
		public Dictionary<string, TimeSpan> StagePerfStats { get; private set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x000681FE File Offset: 0x000663FE
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00068206 File Offset: 0x00066406
		public Dictionary<string, long> CounterBasedPerfStats { get; private set; }

		// Token: 0x06001234 RID: 4660 RVA: 0x00068210 File Offset: 0x00066410
		public string GetStringForLogging()
		{
			StringBuilder stringBuilder = new StringBuilder(40 * this.DomainControllersUsed.Count);
			foreach (string value in this.DomainControllersUsed)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			StringBuilder stringBuilder2 = new StringBuilder(60 * (this.StagePerfStats.Count + this.CounterBasedPerfStats.Count));
			foreach (string text in this.stagePerfStatsOrder)
			{
				if (this.StagePerfStats.ContainsKey(text))
				{
					stringBuilder2.AppendFormat("{0}={1};", text, this.StagePerfStats[text]);
				}
				else if (this.CounterBasedPerfStats.ContainsKey(text))
				{
					stringBuilder2.AppendFormat("{0}={1};", text, this.CounterBasedPerfStats[text]);
				}
			}
			return string.Format("S:OAB='{0}';I64:Status={1};Dt:StartTime={2:O};Dt:EndTime={3:O};S:DC={4};I32:Total.Records={5};I32:Total.TempFiles={6};Ti:TimeWritingFiles={7};S:Org={8};S:Wasted={9};S:HABEnabled={10};I32:Total.RecordsAddedChurn={11};I32:Total.RecordsDeletedChurn={12};I32:Total.RecordsModifiedChurn={13};{14};", new object[]
			{
				this.OfflineAddressBook.ToString(),
				this.GenerationSucceeded ? 0U : 2147500037U,
				this.StartTimestamp,
				this.FinishTimestamp,
				stringBuilder.ToString(),
				this.TotalNumberOfRecords,
				this.TotalNumberOfTempFiles,
				this.IODuration,
				this.Tenant,
				this.UnnecessaryGeneration,
				this.HABEnabled,
				this.totalRecordsAddedChurn,
				this.totalRecordsDeletedChurn,
				this.totalRecordsModifiedChurn,
				stringBuilder2
			});
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00068438 File Offset: 0x00066638
		public void IncrementRecordsModifiedChurn()
		{
			this.totalRecordsModifiedChurn++;
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00068448 File Offset: 0x00066648
		public void IncrementRecordsAddedChurn()
		{
			this.totalRecordsAddedChurn++;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00068458 File Offset: 0x00066658
		public void IncrementRecordsDeletedChurn()
		{
			this.totalRecordsDeletedChurn++;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00068468 File Offset: 0x00066668
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			string text = string.Format("Ti:{0}.{1}", marker, counter);
			TimeSpan timeSpan = dataPoint;
			if (this.StagePerfStats.ContainsKey(text))
			{
				timeSpan += this.StagePerfStats[text];
			}
			else
			{
				this.stagePerfStatsOrder.Add(text);
			}
			this.StagePerfStats[text] = timeSpan;
			OABLogger.LogRecord(TraceType.InfoTrace, "GenerationStats.Log: Ti:{0}.{1} += {2}", new object[]
			{
				marker,
				counter,
				dataPoint
			});
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000684E4 File Offset: 0x000666E4
		public void Log(string marker, string counter, uint dataPoint)
		{
			string text = string.Format("I32:{0}.{1}", marker, counter);
			long num = (long)((ulong)dataPoint);
			if (this.CounterBasedPerfStats.ContainsKey(text))
			{
				num += this.CounterBasedPerfStats[text];
			}
			else
			{
				this.stagePerfStatsOrder.Add(text);
			}
			this.CounterBasedPerfStats[text] = num;
			OABLogger.LogRecord(TraceType.InfoTrace, "GenerationStats.Log: I32:{0}.{1} += {2}", new object[]
			{
				marker,
				counter,
				dataPoint
			});
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0006855D File Offset: 0x0006675D
		public void Log(string marker, string counter, string dataPoint)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B16 RID: 2838
		private const string generationStatsFormat = "S:OAB='{0}';I64:Status={1};Dt:StartTime={2:O};Dt:EndTime={3:O};S:DC={4};I32:Total.Records={5};I32:Total.TempFiles={6};Ti:TimeWritingFiles={7};S:Org={8};S:Wasted={9};S:HABEnabled={10};I32:Total.RecordsAddedChurn={11};I32:Total.RecordsDeletedChurn={12};I32:Total.RecordsModifiedChurn={13};{14};";

		// Token: 0x04000B17 RID: 2839
		private List<string> stagePerfStatsOrder = new List<string>();

		// Token: 0x04000B18 RID: 2840
		private int totalRecordsModifiedChurn;

		// Token: 0x04000B19 RID: 2841
		private int totalRecordsAddedChurn;

		// Token: 0x04000B1A RID: 2842
		private int totalRecordsDeletedChurn;
	}
}
