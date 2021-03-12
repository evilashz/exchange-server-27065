using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000013 RID: 19
	public class QueryExecutionStep
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00006ADA File Offset: 0x00004CDA
		internal QueryExecutionStep(QueryExecutionStepType stepType, Stopwatch stopwatch)
		{
			this.StepType = stepType;
			this.StartTime = DateTime.UtcNow;
			this.startReading = stopwatch.ElapsedMilliseconds;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006B00 File Offset: 0x00004D00
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00006B08 File Offset: 0x00004D08
		public QueryExecutionStepType StepType { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006B11 File Offset: 0x00004D11
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00006B19 File Offset: 0x00004D19
		public DateTime StartTime { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006B24 File Offset: 0x00004D24
		public DateTime EndTime
		{
			get
			{
				return this.StartTime.AddMilliseconds((double)this.ElapsedMilliseconds);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006B46 File Offset: 0x00004D46
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006B4E File Offset: 0x00004D4E
		public long ElapsedMilliseconds { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006B57 File Offset: 0x00004D57
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006B5F File Offset: 0x00004D5F
		public IReadOnlyCollection<KeyValuePair<string, object>> AdditionalStatistics { get; private set; }

		// Token: 0x0600011D RID: 285 RVA: 0x00006B68 File Offset: 0x00004D68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("QueryExecutionStep -> StepType: {0}, StartTime: {1}, EndTime {2}, ElapsedMilliseconds: {3}", new object[]
			{
				this.StepType,
				this.StartTime,
				this.EndTime,
				this.ElapsedMilliseconds
			});
			if (this.AdditionalStatistics != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.AdditionalStatistics)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("    AdditionalStatistic: {0} => {1}", keyValuePair.Key, keyValuePair.Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006C30 File Offset: 0x00004E30
		internal QueryExecutionStep Complete(Stopwatch stopwatch)
		{
			return this.Complete(stopwatch, null);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006C3A File Offset: 0x00004E3A
		internal QueryExecutionStep Complete(Stopwatch stopwatch, IReadOnlyCollection<KeyValuePair<string, object>> additionalStatistics)
		{
			this.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds - this.startReading;
			this.AdditionalStatistics = (additionalStatistics ?? QueryExecutionStep.EmptyAdditionalStatistics);
			return this;
		}

		// Token: 0x0400008D RID: 141
		private static readonly IReadOnlyCollection<KeyValuePair<string, object>> EmptyAdditionalStatistics = new List<KeyValuePair<string, object>>();

		// Token: 0x0400008E RID: 142
		private readonly long startReading;
	}
}
