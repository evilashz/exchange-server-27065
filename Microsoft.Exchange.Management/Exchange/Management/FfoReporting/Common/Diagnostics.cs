using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003CF RID: 975
	internal sealed class Diagnostics
	{
		// Token: 0x060022F1 RID: 8945 RVA: 0x0008DF58 File Offset: 0x0008C158
		static Diagnostics()
		{
			if (!Diagnostics.staticsInitialized)
			{
				lock (Diagnostics.syncobject)
				{
					if (!Diagnostics.staticsInitialized)
					{
						SystemProbe.Start();
						Diagnostics.staticsInitialized = true;
					}
				}
			}
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0008DFBC File Offset: 0x0008C1BC
		public Diagnostics(string componentName)
		{
			this.ComponentName = componentName;
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x0008DFF7 File Offset: 0x0008C1F7
		// (set) Token: 0x060022F4 RID: 8948 RVA: 0x0008DFFF File Offset: 0x0008C1FF
		internal string ComponentName { get; private set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x0008E008 File Offset: 0x0008C208
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x0008E010 File Offset: 0x0008C210
		internal string ActivityId { get; set; }

		// Token: 0x060022F7 RID: 8951 RVA: 0x0008E019 File Offset: 0x0008C219
		internal void Checkpoint(string msg)
		{
			this.lastKnownCheckpoint = msg;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0008E022 File Offset: 0x0008C222
		internal void TraceWarning(string msg)
		{
			this.warnings.Add(msg);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0008E030 File Offset: 0x0008C230
		internal void TraceError(string msg)
		{
			this.Trace(SystemProbe.Status.Fail, msg);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x0008E03C File Offset: 0x0008C23C
		internal void TraceException(string msg, Exception exception)
		{
			string text = string.Format(string.Format("{0}\n{1}", msg, Schema.Utilities.GenerateDetailedError(exception)), new object[0]);
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FfoReportingTaskFailure, new string[]
			{
				text
			});
			string msg2 = string.Format("{0}:{1}", msg, exception.Message);
			this.Trace(SystemProbe.Status.Fail, msg2);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x0008E096 File Offset: 0x0008C296
		internal void SetHealthGreen(string monitor, string msg)
		{
			FaultInjection.FaultInjectionTracer.TraceTest(39836U);
			this.Checkpoint(msg);
			this.Trace(SystemProbe.Status.Pass, string.Empty);
			EventNotificationItem.Publish(this.ComponentName, monitor, null, msg, ResultSeverityLevel.Informational, false);
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0008E0CA File Offset: 0x0008C2CA
		internal void SetHealthRed(string monitor, string msg, Exception exception)
		{
			FaultInjection.FaultInjectionTracer.TraceTest(64160U);
			this.TraceException(msg, exception);
			EventNotificationItem.Publish(this.ComponentName, monitor, null, msg, ResultSeverityLevel.Error, false);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x0008E0F3 File Offset: 0x0008C2F3
		internal void StartTimer(string id)
		{
			this.timings[id] = new Diagnostics.Timing(id, this.timer.ElapsedMilliseconds);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x0008E112 File Offset: 0x0008C312
		internal void StopTimer(string id)
		{
			this.timings[id].Stop(this.timer.ElapsedMilliseconds);
			this.Checkpoint(id);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x0008E138 File Offset: 0x0008C338
		private void Trace(SystemProbe.Status status, string msg)
		{
			this.timer.Stop();
			if (!string.IsNullOrEmpty(this.ActivityId))
			{
				SystemProbe.ActivityId = new Guid(this.ActivityId);
			}
			StringBuilder stringBuilder = new StringBuilder(this.lastKnownCheckpoint);
			if (!string.IsNullOrEmpty(msg))
			{
				stringBuilder.AppendFormat(" Msg:{0}", msg);
			}
			if (this.warnings.Count > 0)
			{
				stringBuilder.AppendFormat(" Warning:{0}", string.Join(",", this.warnings));
			}
			stringBuilder.AppendFormat(" Total:{0}", this.timer.ElapsedMilliseconds);
			foreach (Diagnostics.Timing timing in this.timings.Values)
			{
				timing.Stop(this.timer.ElapsedMilliseconds);
				stringBuilder.AppendFormat(" {0}", timing);
			}
			SystemProbe.Trace("FFO-RWS-FfoReportingTask", status, stringBuilder.ToString(), new object[0]);
		}

		// Token: 0x04001B85 RID: 7045
		internal const string FfoDALRetrievalEvent = "FFO DAL Retrieval Status Monitor";

		// Token: 0x04001B86 RID: 7046
		internal const string FfoReportingEvent = "FFO Reporting Task Status Monitor";

		// Token: 0x04001B87 RID: 7047
		internal const string MailFilterListEvent = "FFO GetFilterValueList Status Monitor";

		// Token: 0x04001B88 RID: 7048
		internal const string FfoSmtpCheckerEvent = "FfoReporting.SmtpChecker";

		// Token: 0x04001B89 RID: 7049
		private const string ComponentTraceName = "FFO-RWS-FfoReportingTask";

		// Token: 0x04001B8A RID: 7050
		private static readonly object syncobject = new object();

		// Token: 0x04001B8B RID: 7051
		private static volatile bool staticsInitialized;

		// Token: 0x04001B8C RID: 7052
		private string lastKnownCheckpoint = string.Empty;

		// Token: 0x04001B8D RID: 7053
		private Dictionary<string, Diagnostics.Timing> timings = new Dictionary<string, Diagnostics.Timing>();

		// Token: 0x04001B8E RID: 7054
		private Stopwatch timer = Stopwatch.StartNew();

		// Token: 0x04001B8F RID: 7055
		private List<string> warnings = new List<string>();

		// Token: 0x020003D0 RID: 976
		internal static class Checkpoints
		{
			// Token: 0x04001B92 RID: 7058
			internal const string Authentication = "Authentication";

			// Token: 0x04001B93 RID: 7059
			internal const string Conversion = "Conversion";

			// Token: 0x04001B94 RID: 7060
			internal const string DalAccess = "DalAccess";

			// Token: 0x04001B95 RID: 7061
			internal const string Validation = "Validation";

			// Token: 0x04001B96 RID: 7062
			internal const string WriteObject = "WriteObject";
		}

		// Token: 0x020003D1 RID: 977
		private sealed class Timing
		{
			// Token: 0x06002300 RID: 8960 RVA: 0x0008E250 File Offset: 0x0008C450
			internal Timing(string id, long startTime)
			{
				this.Id = id;
				this.startTime = startTime;
			}

			// Token: 0x17000A59 RID: 2649
			// (get) Token: 0x06002301 RID: 8961 RVA: 0x0008E272 File Offset: 0x0008C472
			// (set) Token: 0x06002302 RID: 8962 RVA: 0x0008E27A File Offset: 0x0008C47A
			public string Id { get; private set; }

			// Token: 0x06002303 RID: 8963 RVA: 0x0008E283 File Offset: 0x0008C483
			public void Stop(long endTime)
			{
				if (this.endTime == null)
				{
					this.endTime = new long?(endTime);
				}
			}

			// Token: 0x06002304 RID: 8964 RVA: 0x0008E2A0 File Offset: 0x0008C4A0
			public override string ToString()
			{
				string arg = (this.endTime != null) ? (this.endTime.Value - this.startTime).ToString() : "?";
				return string.Format("{0}:{1}", this.Id, arg);
			}

			// Token: 0x04001B97 RID: 7063
			private readonly long startTime;

			// Token: 0x04001B98 RID: 7064
			private long? endTime = null;
		}
	}
}
