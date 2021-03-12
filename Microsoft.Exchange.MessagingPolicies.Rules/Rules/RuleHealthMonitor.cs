using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000027 RID: 39
	internal class RuleHealthMonitor
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00007D09 File Offset: 0x00005F09
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00007D11 File Offset: 0x00005F11
		internal string RuleId { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007D1A File Offset: 0x00005F1A
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00007D22 File Offset: 0x00005F22
		internal string TenantId { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007D2B File Offset: 0x00005F2B
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00007D33 File Offset: 0x00005F33
		internal RuleHealthMonitor.MtlLogWriterDelegate MtlLogWriter { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007D3C File Offset: 0x00005F3C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00007D44 File Offset: 0x00005F44
		internal RuleHealthMonitor.EventLogWriterDelegate EventLogWriter { get; set; }

		// Token: 0x0600017B RID: 379 RVA: 0x00007D4D File Offset: 0x00005F4D
		internal RuleHealthMonitor(RuleHealthMonitor.ActivityType activityType, long mtlLoggingThresholdMs, long eventLoggingThresholdMs, RuleHealthMonitor.EventLogWriterDelegate eventLogWriter)
		{
			this.activityType = activityType;
			this.mtlLoggingThresholdMs = mtlLoggingThresholdMs;
			this.eventLoggingThresholdMs = eventLoggingThresholdMs;
			this.EventLogWriter = eventLogWriter;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007D88 File Offset: 0x00005F88
		internal void LogMtl()
		{
			if (this.MtlLogWriter == null)
			{
				return;
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("ruleId", this.RuleId));
			list.Add(new KeyValuePair<string, string>((this.activityType == RuleHealthMonitor.ActivityType.Load) ? "LoadW" : "ExecW", this.wallClockStopwatch.ElapsedMilliseconds.ToString()));
			list.Add(new KeyValuePair<string, string>((this.activityType == RuleHealthMonitor.ActivityType.Load) ? "Loadc" : "ExecC", this.cpuStopwatch.ElapsedMilliseconds.ToString()));
			try
			{
				this.MtlLogWriter(TrackAgentInfoAgentName.TRA.ToString("G"), TrackAgentInfoGroupName.ETRP.ToString("G"), list);
			}
			catch (InvalidOperationException)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceWarning(0L, "InvalidOperationException thrown while attempting to log rule execution performance information. Expected when data size to Audit is high.");
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007E70 File Offset: 0x00006070
		internal void LogEvent()
		{
			if (this.EventLogWriter == null)
			{
				return;
			}
			this.EventLogWriter(string.Format("Rule: {0}, Tenant id: {1}, Time: {2}, Threshold: {3}", new object[]
			{
				string.IsNullOrEmpty(this.RuleId) ? string.Empty : this.RuleId,
				string.IsNullOrEmpty(this.TenantId) ? string.Empty : this.TenantId,
				this.wallClockStopwatch.ElapsedMilliseconds.ToString(),
				this.eventLoggingThresholdMs
			}));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007F04 File Offset: 0x00006104
		internal void LogIfThresholdExceeded()
		{
			if (this.mtlLoggingThresholdMs >= 0L && this.wallClockStopwatch.ElapsedMilliseconds >= this.mtlLoggingThresholdMs)
			{
				this.LogMtl();
			}
			if (this.eventLoggingThresholdMs >= 0L && this.wallClockStopwatch.ElapsedMilliseconds >= this.eventLoggingThresholdMs)
			{
				this.LogEvent();
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007F57 File Offset: 0x00006157
		internal void Reset()
		{
			this.wallClockStopwatch.Reset();
			this.cpuStopwatch.Reset();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007F6F File Offset: 0x0000616F
		internal void Restart()
		{
			this.wallClockStopwatch.Restart();
			this.cpuStopwatch.Restart();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007F87 File Offset: 0x00006187
		internal void Start()
		{
			this.wallClockStopwatch.Start();
			this.cpuStopwatch.Start();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007F9F File Offset: 0x0000619F
		internal void Stop(bool logIfThresholdExceeded = false)
		{
			this.wallClockStopwatch.Stop();
			this.cpuStopwatch.Stop();
			if (logIfThresholdExceeded)
			{
				this.LogIfThresholdExceeded();
			}
		}

		// Token: 0x0400012B RID: 299
		private readonly long mtlLoggingThresholdMs;

		// Token: 0x0400012C RID: 300
		private readonly long eventLoggingThresholdMs;

		// Token: 0x0400012D RID: 301
		private Stopwatch wallClockStopwatch = new Stopwatch();

		// Token: 0x0400012E RID: 302
		private CpuStopwatch cpuStopwatch = new CpuStopwatch();

		// Token: 0x0400012F RID: 303
		private RuleHealthMonitor.ActivityType activityType;

		// Token: 0x02000028 RID: 40
		internal enum ActivityType
		{
			// Token: 0x04000135 RID: 309
			Load,
			// Token: 0x04000136 RID: 310
			Execute
		}

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x06000184 RID: 388
		internal delegate void MtlLogWriterDelegate(string agentName, string eventTopic, List<KeyValuePair<string, string>> data);

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x06000188 RID: 392
		internal delegate void EventLogWriterDelegate(string eventMessageDetails);
	}
}
