using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001CF RID: 463
	internal class TraceLog : ObjectLog<TraceLogData>
	{
		// Token: 0x060012E0 RID: 4832 RVA: 0x0002B44F File Offset: 0x0002964F
		private TraceLog() : base(new TraceLog.TraceLogSchema(), new SimpleObjectLogConfiguration("Trace", null, "TraceLogMaxDirSize", "TraceLogMaxFileSize"))
		{
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0002B474 File Offset: 0x00029674
		public static void Write(string tracerName, TraceType traceType, string traceMessage)
		{
			TraceLogData objectToLog = default(TraceLogData);
			objectToLog.TracerName = tracerName;
			objectToLog.TraceType = traceType;
			objectToLog.TraceMessage = traceMessage;
			TraceLog.instance.LogObject(objectToLog);
		}

		// Token: 0x040009CB RID: 2507
		private const int MaxDataContextLength = 1000;

		// Token: 0x040009CC RID: 2508
		private static TraceLog instance = new TraceLog();

		// Token: 0x020001D0 RID: 464
		private class TraceLogSchema : ObjectLogSchema
		{
			// Token: 0x1700064B RID: 1611
			// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0002B4B8 File Offset: 0x000296B8
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Mailbox Replication Service";
				}
			}

			// Token: 0x1700064C RID: 1612
			// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0002B4BF File Offset: 0x000296BF
			public override string LogType
			{
				get
				{
					return "Trace Log";
				}
			}

			// Token: 0x040009CD RID: 2509
			public static readonly ObjectLogSimplePropertyDefinition<TraceLogData> TracerName = new ObjectLogSimplePropertyDefinition<TraceLogData>("Tracer", (TraceLogData d) => d.TracerName);

			// Token: 0x040009CE RID: 2510
			public static readonly ObjectLogSimplePropertyDefinition<TraceLogData> TraceType = new ObjectLogSimplePropertyDefinition<TraceLogData>("Type", (TraceLogData d) => d.TraceType.ToString());

			// Token: 0x040009CF RID: 2511
			public static readonly ObjectLogSimplePropertyDefinition<TraceLogData> TraceMessage = new ObjectLogSimplePropertyDefinition<TraceLogData>("Message", (TraceLogData d) => d.TraceMessage);
		}
	}
}
