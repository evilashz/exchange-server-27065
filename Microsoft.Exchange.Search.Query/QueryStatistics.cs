using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;

namespace Microsoft.Exchange.Search.Query
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class QueryStatistics
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00006C6C File Offset: 0x00004E6C
		internal QueryStatistics(int traceContext)
		{
			this.traceContext = traceContext;
			this.executionStep = this.StartNewStep(QueryExecutionStepType.InstantSearchRequest);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006C9E File Offset: 0x00004E9E
		public int Version
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006CA1 File Offset: 0x00004EA1
		public DateTime StartTime
		{
			get
			{
				return this.executionStep.StartTime;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006CAE File Offset: 0x00004EAE
		public DateTime EndTime
		{
			get
			{
				return this.executionStep.EndTime;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006CBB File Offset: 0x00004EBB
		public long ElapsedMilliseconds
		{
			get
			{
				return this.executionStep.ElapsedMilliseconds;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006CC8 File Offset: 0x00004EC8
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public bool StoreBypassed { get; internal set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006CD9 File Offset: 0x00004ED9
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006CE1 File Offset: 0x00004EE1
		public bool LightningEnabled { get; internal set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006CEA File Offset: 0x00004EEA
		public IReadOnlyCollection<QueryExecutionStep> Steps
		{
			get
			{
				return this.steps;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006CF2 File Offset: 0x00004EF2
		public void Complete()
		{
			this.executionStep.Complete(this.stopwatch);
			ExTraceGlobals.InstantSearchTracer.TraceDebug<QueryExecutionStepType, long>((long)this.traceContext, "Query step completed, {0} {1}ms.", this.executionStep.StepType, this.executionStep.ElapsedMilliseconds);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006D32 File Offset: 0x00004F32
		public QueryExecutionStep StartNewStep(QueryExecutionStepType stepType)
		{
			ExTraceGlobals.InstantSearchTracer.TraceDebug<QueryExecutionStepType>((long)this.traceContext, "Query step started, {0}.", stepType);
			return new QueryExecutionStep(stepType, this.stopwatch);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006D57 File Offset: 0x00004F57
		public void CompleteStep(QueryExecutionStep step)
		{
			this.CompleteStep(step, null);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006D64 File Offset: 0x00004F64
		public void CompleteStep(QueryExecutionStep step, IReadOnlyCollection<KeyValuePair<string, object>> additionalStatistics)
		{
			step.Complete(this.stopwatch, additionalStatistics);
			ExTraceGlobals.InstantSearchTracer.TraceDebug<QueryExecutionStepType, long>((long)this.traceContext, "Query step completed, {0} {1}ms.", step.StepType, step.ElapsedMilliseconds);
			lock (this.steps)
			{
				this.steps.Add(step);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006DDC File Offset: 0x00004FDC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("QueryStatistics -> Version: {0}, StartTime: {1}, EndTime {2}, ElapsedMilliseconds: {3}, LightningEnabled: {4}, StoreBypassed: {5}", new object[]
			{
				this.Version,
				this.StartTime,
				this.EndTime,
				this.ElapsedMilliseconds,
				this.LightningEnabled,
				this.StoreBypassed
			});
			stringBuilder.AppendLine();
			lock (this.steps)
			{
				foreach (QueryExecutionStep queryExecutionStep in this.steps)
				{
					stringBuilder.Append("  ");
					stringBuilder.AppendLine(queryExecutionStep.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000A4 RID: 164
		private const int QueryStatisticsVersion = 1;

		// Token: 0x040000A5 RID: 165
		private readonly Stopwatch stopwatch = Stopwatch.StartNew();

		// Token: 0x040000A6 RID: 166
		private readonly int traceContext;

		// Token: 0x040000A7 RID: 167
		private readonly QueryExecutionStep executionStep;

		// Token: 0x040000A8 RID: 168
		private List<QueryExecutionStep> steps = new List<QueryExecutionStep>();
	}
}
