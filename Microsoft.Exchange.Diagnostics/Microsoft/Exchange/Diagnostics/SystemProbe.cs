using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Microsoft.Exchange.Diagnostics.Components.SystemProbe;
using Microsoft.Exchange.Diagnostics.Internal;
using Microsoft.Exchange.SystemProbe;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001DE RID: 478
	internal static class SystemProbe
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00038490 File Offset: 0x00036690
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x000384B7 File Offset: 0x000366B7
		public static Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("SystemProbe.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("SystemProbe.ActivityID", value);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000384C9 File Offset: 0x000366C9
		public static bool Enabled
		{
			get
			{
				return SystemProbe.enabled;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x000384D0 File Offset: 0x000366D0
		internal static Log Log
		{
			get
			{
				return SystemProbe.log;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x000384D7 File Offset: 0x000366D7
		internal static ExEventLog EventLogger
		{
			get
			{
				if (SystemProbe.eventLogger == null)
				{
					SystemProbe.eventLogger = new ExEventLog(ExTraceGlobals.ProbeTracer.Category, "FfoSystemProbe");
				}
				return SystemProbe.eventLogger;
			}
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000384FE File Offset: 0x000366FE
		internal static bool IsTraceEnabled()
		{
			return SystemProbe.IsTraceEnabled(SystemProbe.ActivityId);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0003850A File Offset: 0x0003670A
		internal static bool IsTraceEnabled(Guid activityId)
		{
			return SystemProbe.enabled && SystemProbe.log != null && activityId != Guid.Empty && activityId.ToString().IndexOf("-0000-0000-0000-") == -1;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00038543 File Offset: 0x00036743
		internal static bool IsTraceEnabled(ISystemProbeTraceable activityIdHolder)
		{
			return activityIdHolder != null && SystemProbe.IsTraceEnabled(activityIdHolder.SystemProbeId);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00038555 File Offset: 0x00036755
		public static void Start()
		{
			SystemProbe.Start("SYSPRB", null);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00038564 File Offset: 0x00036764
		public static void Start(string logFilePrefix, string logFileSubfolder = null)
		{
			SystemProbe.sysProbeSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "System Probe Log", SystemProbe.Fields);
			SystemProbe.log = new Log(logFilePrefix, new LogHeaderFormatter(SystemProbe.sysProbeSchema), "SystemProbeLogs");
			SystemProbe.startTime = DateTime.UtcNow;
			SystemProbe.stopwatch.Start();
			try
			{
				SystemProbe.hostName = Dns.GetHostName();
			}
			catch (SocketException)
			{
				SystemProbe.hostName = Environment.MachineName;
			}
			string text = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("SystemProbeLogPath");
					if (value != null)
					{
						text = value.ToString();
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeLogPathNotConfigured, null, new object[0]);
				return;
			}
			if (!string.IsNullOrEmpty(logFileSubfolder))
			{
				text = Path.Combine(text, logFileSubfolder);
			}
			SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeStarted, null, new object[0]);
			long num = 10485760L;
			long maxDirectorySize = 50L * num;
			int bufferSize = 4096;
			TimeSpan maxAge = TimeSpan.FromDays(5.0);
			TimeSpan streamFlushInterval = TimeSpan.FromSeconds(30.0);
			SystemProbe.log.Configure(text, maxAge, maxDirectorySize, num, bufferSize, streamFlushInterval);
			SystemProbe.enabled = true;
			SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeConfigured, null, new object[]
			{
				text
			});
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000386F4 File Offset: 0x000368F4
		public static void Stop()
		{
			if (SystemProbe.log != null)
			{
				SystemProbe.log.Close();
				SystemProbe.log = null;
			}
			SystemProbe.enabled = false;
			SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeStopped, null, new object[0]);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0003872C File Offset: 0x0003692C
		public static void Configure(string path, TimeSpan maxAge, long maxDirectorySize, long maxLogFileSize, int bufferSize, TimeSpan streamFlushInterval)
		{
			SystemProbe.log.Configure(path, maxAge, maxDirectorySize, maxLogFileSize, false, bufferSize, streamFlushInterval);
			SystemProbe.enabled = true;
			SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeConfigured, null, new object[]
			{
				path
			});
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003876F File Offset: 0x0003696F
		public static void FlushBuffer()
		{
			if (SystemProbe.log != null)
			{
				SystemProbe.log.Flush();
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00038782 File Offset: 0x00036982
		public static void TracePass(string component, string formatString, params object[] args)
		{
			SystemProbe.Trace(component, SystemProbe.Status.Pass, formatString, args);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0003878D File Offset: 0x0003698D
		public static void TraceFail(string component, string formatString, params object[] args)
		{
			SystemProbe.Trace(component, SystemProbe.Status.Fail, formatString, args);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00038798 File Offset: 0x00036998
		public static void Trace(string component, SystemProbe.Status status, string formatString, params object[] args)
		{
			if (!SystemProbe.IsTraceEnabled())
			{
				return;
			}
			SystemProbe.AddToLog(SystemProbe.ActivityId, component, status, formatString, args);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000387B0 File Offset: 0x000369B0
		public static void TracePass(Guid activityId, string component, string message)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Pass, message, new object[0]);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000387CC File Offset: 0x000369CC
		public static void TracePass<T0>(Guid activityId, string component, string formatString, T0 arg0)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Pass, formatString, new object[]
			{
				arg0
			});
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000387FC File Offset: 0x000369FC
		public static void TracePass<T0, T1>(Guid activityId, string component, string formatString, T0 arg0, T1 arg1)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Pass, formatString, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00038838 File Offset: 0x00036A38
		public static void TracePass<T0, T1, T2>(Guid activityId, string component, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Pass, formatString, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0003887C File Offset: 0x00036A7C
		public static void TracePass(Guid activityId, string component, string formatString, params object[] args)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Pass, formatString, args);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00038891 File Offset: 0x00036A91
		public static void TraceFail(Guid activityId, string component, string message)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Fail, message, new object[0]);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000388AC File Offset: 0x00036AAC
		public static void TraceFail<T0>(Guid activityId, string component, string formatString, T0 arg0)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Fail, formatString, new object[]
			{
				arg0
			});
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000388DC File Offset: 0x00036ADC
		public static void TraceFail<T0, T1>(Guid activityId, string component, string formatString, T0 arg0, T1 arg1)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Fail, formatString, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00038918 File Offset: 0x00036B18
		public static void TraceFail<T0, T1, T2>(Guid activityId, string component, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Fail, formatString, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003895C File Offset: 0x00036B5C
		public static void TraceFail(Guid activityId, string component, string formatString, params object[] args)
		{
			if (!SystemProbe.IsTraceEnabled(activityId))
			{
				return;
			}
			SystemProbe.AddToLog(activityId, component, SystemProbe.Status.Fail, formatString, args);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00038971 File Offset: 0x00036B71
		public static void TracePass(ISystemProbeTraceable activityIdHolder, string component, string message)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Pass, message, new object[0]);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00038990 File Offset: 0x00036B90
		public static void TracePass<T0>(ISystemProbeTraceable activityIdHolder, string component, string formatString, T0 arg0)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Pass, formatString, new object[]
			{
				arg0
			});
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000389C8 File Offset: 0x00036BC8
		public static void TracePass<T0, T1>(ISystemProbeTraceable activityIdHolder, string component, string formatString, T0 arg0, T1 arg1)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Pass, formatString, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00038A08 File Offset: 0x00036C08
		public static void TracePass<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, string component, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Pass, formatString, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00038A51 File Offset: 0x00036C51
		public static void TracePass(ISystemProbeTraceable activityIdHolder, string component, string formatString, params object[] args)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Pass, formatString, args);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00038A6B File Offset: 0x00036C6B
		public static void TraceFail(ISystemProbeTraceable activityIdHolder, string component, string message)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Fail, message, new object[0]);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00038A8C File Offset: 0x00036C8C
		public static void TraceFail<T0>(ISystemProbeTraceable activityIdHolder, string component, string formatString, T0 arg0)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Fail, formatString, new object[]
			{
				arg0
			});
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00038AC4 File Offset: 0x00036CC4
		public static void TraceFail<T0, T1>(ISystemProbeTraceable activityIdHolder, string component, string formatString, T0 arg0, T1 arg1)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Fail, formatString, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00038B04 File Offset: 0x00036D04
		public static void TraceFail<T0, T1, T2>(ISystemProbeTraceable activityIdHolder, string component, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Fail, formatString, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00038B4D File Offset: 0x00036D4D
		public static void TraceFail(ISystemProbeTraceable activityIdHolder, string component, string formatString, params object[] args)
		{
			if (!SystemProbe.IsTraceEnabled(activityIdHolder))
			{
				return;
			}
			SystemProbe.AddToLog(activityIdHolder.SystemProbeId, component, SystemProbe.Status.Fail, formatString, args);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00038B68 File Offset: 0x00036D68
		private static void AddToLog(Guid activityId, string component, SystemProbe.Status status, string formatString, params object[] args)
		{
			string value = string.Empty;
			try
			{
				value = string.Format(CultureInfo.InvariantCulture, formatString, args);
			}
			catch (ArgumentNullException)
			{
				SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeFormatArgumentNullException, component, new object[0]);
				return;
			}
			catch (FormatException ex)
			{
				SystemProbe.EventLogger.LogEvent(FfoSystemProbeEventLogConstants.Tuple_SystemProbeFormatException, component, new object[]
				{
					ex.Message
				});
				return;
			}
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("SysProbe", string.Empty));
			list.Add(new KeyValuePair<string, string>("Server", SystemProbe.hostName));
			list.Add(new KeyValuePair<string, string>("Component", component));
			list.Add(new KeyValuePair<string, string>("Status", status.ToString()));
			list.Add(new KeyValuePair<string, string>("Message", value));
			List<List<KeyValuePair<string, string>>> list2 = new List<List<KeyValuePair<string, string>>>();
			list2.Add(list);
			LogRowFormatter logRowFormatter = new LogRowFormatter(SystemProbe.sysProbeSchema);
			logRowFormatter[0] = (SystemProbe.startTime + SystemProbe.stopwatch.Elapsed).ToString("yyyy-MM-ddTHH\\:mm\\:ss.ffffffZ", DateTimeFormatInfo.InvariantInfo);
			logRowFormatter[10] = activityId;
			logRowFormatter[11] = activityId;
			logRowFormatter[8] = MessageTrackingEvent.SYSPROBEINFO;
			logRowFormatter[7] = MessageTrackingSource.AGENT;
			logRowFormatter[23] = SystemProbeConstants.TenantID;
			logRowFormatter[26] = LoggingFormatter.GetAgentInfoString(list2);
			logRowFormatter[12] = new string[]
			{
				"sysprb@contoso.com"
			};
			if (SystemProbe.log != null)
			{
				SystemProbe.log.Append(logRowFormatter, -1);
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00038D34 File Offset: 0x00036F34
		private static string[] InitializeFields()
		{
			string[] array = new string[Enum.GetValues(typeof(SystemProbe.SysProbeField)).Length];
			array[0] = "date-time";
			array[1] = "client-ip";
			array[2] = "client-hostname";
			array[3] = "server-ip";
			array[4] = "server-hostname";
			array[5] = "source-context";
			array[6] = "connector-id";
			array[7] = "source";
			array[8] = "event-id";
			array[9] = "internal-message-id";
			array[10] = "message-id";
			array[11] = "network-message-id";
			array[12] = "recipient-address";
			array[13] = "recipient-status";
			array[14] = "total-bytes";
			array[15] = "recipient-count";
			array[16] = "related-recipient-address";
			array[17] = "reference";
			array[18] = "message-subject";
			array[19] = "sender-address";
			array[20] = "return-path";
			array[21] = "message-info";
			array[22] = "directionality";
			array[23] = "tenant-id";
			array[24] = "original-client-ip";
			array[25] = "original-server-ip";
			array[26] = "custom-data";
			return array;
		}

		// Token: 0x040009CF RID: 2511
		private const string LogComponentName = "SystemProbeLogs";

		// Token: 0x040009D0 RID: 2512
		private const string LogPathRegistryKey = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x040009D1 RID: 2513
		private const string LogPathRegistryValue = "SystemProbeLogPath";

		// Token: 0x040009D2 RID: 2514
		private const string SysProbeRecipient = "sysprb@contoso.com";

		// Token: 0x040009D3 RID: 2515
		private static readonly string[] Fields = SystemProbe.InitializeFields();

		// Token: 0x040009D4 RID: 2516
		private static LogSchema sysProbeSchema;

		// Token: 0x040009D5 RID: 2517
		private static Log log;

		// Token: 0x040009D6 RID: 2518
		private static bool enabled;

		// Token: 0x040009D7 RID: 2519
		private static string hostName;

		// Token: 0x040009D8 RID: 2520
		private static ExEventLog eventLogger = null;

		// Token: 0x040009D9 RID: 2521
		private static DateTime startTime;

		// Token: 0x040009DA RID: 2522
		private static Stopwatch stopwatch = new Stopwatch();

		// Token: 0x020001DF RID: 479
		private enum SysProbeField
		{
			// Token: 0x040009DC RID: 2524
			Time,
			// Token: 0x040009DD RID: 2525
			ClientIP,
			// Token: 0x040009DE RID: 2526
			ClientHostName,
			// Token: 0x040009DF RID: 2527
			ServerIP,
			// Token: 0x040009E0 RID: 2528
			ServerHostName,
			// Token: 0x040009E1 RID: 2529
			SourceContext,
			// Token: 0x040009E2 RID: 2530
			ConnectorId,
			// Token: 0x040009E3 RID: 2531
			Source,
			// Token: 0x040009E4 RID: 2532
			EventID,
			// Token: 0x040009E5 RID: 2533
			InternalMsgID,
			// Token: 0x040009E6 RID: 2534
			MessageID,
			// Token: 0x040009E7 RID: 2535
			NetworkMsgID,
			// Token: 0x040009E8 RID: 2536
			RecipientAddress,
			// Token: 0x040009E9 RID: 2537
			RecipStatus,
			// Token: 0x040009EA RID: 2538
			TotalBytes,
			// Token: 0x040009EB RID: 2539
			RecipientCount,
			// Token: 0x040009EC RID: 2540
			RelatedRecipientAddress,
			// Token: 0x040009ED RID: 2541
			Reference,
			// Token: 0x040009EE RID: 2542
			Subject,
			// Token: 0x040009EF RID: 2543
			Sender,
			// Token: 0x040009F0 RID: 2544
			ReturnPath,
			// Token: 0x040009F1 RID: 2545
			MessageInfo,
			// Token: 0x040009F2 RID: 2546
			Directionality,
			// Token: 0x040009F3 RID: 2547
			TenantID,
			// Token: 0x040009F4 RID: 2548
			OriginalClientIP,
			// Token: 0x040009F5 RID: 2549
			OriginalServerIP,
			// Token: 0x040009F6 RID: 2550
			CustomData
		}

		// Token: 0x020001E0 RID: 480
		public enum Status
		{
			// Token: 0x040009F8 RID: 2552
			Pass,
			// Token: 0x040009F9 RID: 2553
			Fail
		}
	}
}
