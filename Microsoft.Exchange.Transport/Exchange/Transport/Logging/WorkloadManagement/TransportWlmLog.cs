using System;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Logging.WorkloadManagement
{
	// Token: 0x02000097 RID: 151
	internal class TransportWlmLog
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00016668 File Offset: 0x00014868
		public static LogSchema Schema
		{
			get
			{
				if (TransportWlmLog.schema == null)
				{
					LogSchema value = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Transport Workload Management Log", TransportWlmLog.Row.Headers);
					Interlocked.CompareExchange<LogSchema>(ref TransportWlmLog.schema, value, null);
				}
				return TransportWlmLog.schema;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000166B7 File Offset: 0x000148B7
		public static void Start()
		{
			TransportWlmLog.CreateLog();
			TransportWlmLog.Configure(Components.Configuration.LocalServer);
			TransportWlmLog.RegisterConfigurationChangeHandlers();
			TransportWlmLog.processRole = Components.Configuration.ProcessTransportRole;
			TransportWlmLog.hostName = Dns.GetHostName();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000166EB File Offset: 0x000148EB
		public static void Stop()
		{
			TransportWlmLog.UnregisterConfigurationChangeHandlers();
			if (TransportWlmLog.log != null)
			{
				TransportWlmLog.log.Close();
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016703 File Offset: 0x00014903
		public static void FlushBuffer()
		{
			if (TransportWlmLog.log != null)
			{
				TransportWlmLog.log.Flush();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016716 File Offset: 0x00014916
		public static void StartTest(string directory)
		{
			TransportWlmLog.CreateLog();
			TransportWlmLog.log.Configure(directory, TimeSpan.FromDays(1.0), 0L, 0L, 0, TimeSpan.MaxValue);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016740 File Offset: 0x00014940
		public static void LogActivity(string messageId, Guid tenantId, string source, IActivityScope scope)
		{
			if (scope == null)
			{
				throw new ArgumentNullException("scope");
			}
			if (!TransportWlmLog.enabled)
			{
				return;
			}
			TransportWlmLog.Row row = new TransportWlmLog.Row
			{
				MessageId = messageId,
				TenantId = tenantId,
				Source = source,
				Process = TransportWlmLog.processRole,
				HostName = TransportWlmLog.hostName,
				Description = string.Format("[{0}]", LogRowFormatter.FormatCollection(WorkloadManagementLogger.FormatWlmActivity(scope, true)))
			};
			row.Log(source);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000167B9 File Offset: 0x000149B9
		private static void CreateLog()
		{
			TransportWlmLog.log = new Log("TRANSPORTWLMLOG", new LogHeaderFormatter(TransportWlmLog.Schema), "TransportWorkloadManagementLog");
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000167D9 File Offset: 0x000149D9
		private static void RegisterConfigurationChangeHandlers()
		{
			Components.Configuration.LocalServerChanged += TransportWlmLog.Configure;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000167F1 File Offset: 0x000149F1
		private static void UnregisterConfigurationChangeHandlers()
		{
			Components.Configuration.LocalServerChanged -= TransportWlmLog.Configure;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001680C File Offset: 0x00014A0C
		private static void Configure(TransportServerConfiguration server)
		{
			Server transportServer = server.TransportServer;
			if (transportServer.WlmLogPath == null || string.IsNullOrEmpty(transportServer.WlmLogPath.PathName))
			{
				TransportWlmLog.enabled = false;
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Transport Wlm Log path was set to null, Transport Wlm Log is disabled");
				Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_TransportWlmLogPathIsNull, null, new object[0]);
				return;
			}
			TransportWlmLog.enabled = true;
			TransportWlmLog.log.Configure(transportServer.WlmLogPath.PathName, transportServer.WlmLogMaxAge, (long)(transportServer.WlmLogMaxDirectorySize.IsUnlimited ? 0UL : transportServer.WlmLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.WlmLogMaxFileSize.IsUnlimited ? 0UL : transportServer.WlmLogMaxFileSize.Value.ToBytes()), Components.TransportAppConfig.Logging.TransportWlmLogBufferSize, Components.TransportAppConfig.Logging.TransportWlmLogFlushInterval);
		}

		// Token: 0x040002B6 RID: 694
		private const string LogComponentName = "TransportWorkloadManagementLog";

		// Token: 0x040002B7 RID: 695
		private const string LogFileName = "TRANSPORTWLMLOG";

		// Token: 0x040002B8 RID: 696
		private static LogSchema schema;

		// Token: 0x040002B9 RID: 697
		private static Log log;

		// Token: 0x040002BA RID: 698
		private static bool enabled;

		// Token: 0x040002BB RID: 699
		private static ProcessTransportRole processRole;

		// Token: 0x040002BC RID: 700
		private static string hostName;

		// Token: 0x02000098 RID: 152
		public struct Source
		{
			// Token: 0x040002BD RID: 701
			public const string Categorizer = "CAT";
		}

		// Token: 0x02000099 RID: 153
		private sealed class Row : LogRowFormatter
		{
			// Token: 0x0600055C RID: 1372 RVA: 0x00016912 File Offset: 0x00014B12
			public Row() : base(TransportWlmLog.Schema)
			{
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x0600055D RID: 1373 RVA: 0x00016920 File Offset: 0x00014B20
			public static string[] Headers
			{
				get
				{
					if (TransportWlmLog.Row.headers == null)
					{
						string[] array = new string[Enum.GetValues(typeof(TransportWlmLog.Row.Field)).Length];
						array[0] = "date-time";
						array[1] = "message-id";
						array[2] = "tenant-id";
						array[3] = "hostname";
						array[4] = "process";
						array[5] = "source";
						array[6] = "description";
						Interlocked.CompareExchange<string[]>(ref TransportWlmLog.Row.headers, array, null);
					}
					return TransportWlmLog.Row.headers;
				}
			}

			// Token: 0x17000136 RID: 310
			// (set) Token: 0x0600055E RID: 1374 RVA: 0x00016998 File Offset: 0x00014B98
			public string MessageId
			{
				set
				{
					base[1] = value;
				}
			}

			// Token: 0x17000137 RID: 311
			// (set) Token: 0x0600055F RID: 1375 RVA: 0x000169A2 File Offset: 0x00014BA2
			public Guid TenantId
			{
				set
				{
					base[2] = value;
				}
			}

			// Token: 0x17000138 RID: 312
			// (set) Token: 0x06000560 RID: 1376 RVA: 0x000169B1 File Offset: 0x00014BB1
			public string HostName
			{
				set
				{
					base[3] = value;
				}
			}

			// Token: 0x17000139 RID: 313
			// (set) Token: 0x06000561 RID: 1377 RVA: 0x000169BB File Offset: 0x00014BBB
			public ProcessTransportRole Process
			{
				set
				{
					base[4] = value;
				}
			}

			// Token: 0x1700013A RID: 314
			// (set) Token: 0x06000562 RID: 1378 RVA: 0x000169CA File Offset: 0x00014BCA
			public string Source
			{
				set
				{
					base[5] = value;
				}
			}

			// Token: 0x1700013B RID: 315
			// (set) Token: 0x06000563 RID: 1379 RVA: 0x000169D4 File Offset: 0x00014BD4
			public string Description
			{
				set
				{
					base[6] = value;
				}
			}

			// Token: 0x06000564 RID: 1380 RVA: 0x000169E0 File Offset: 0x00014BE0
			public void Log(string component)
			{
				try
				{
					TransportWlmLog.log.Append(this, 0);
				}
				catch (ObjectDisposedException)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Appending to transport WLM log for component {0} failed with ObjectDisposedException", component);
				}
			}

			// Token: 0x040002BE RID: 702
			private static string[] headers;

			// Token: 0x0200009A RID: 154
			public enum Field
			{
				// Token: 0x040002C0 RID: 704
				Time,
				// Token: 0x040002C1 RID: 705
				MessageId,
				// Token: 0x040002C2 RID: 706
				TenantId,
				// Token: 0x040002C3 RID: 707
				HostName,
				// Token: 0x040002C4 RID: 708
				Process,
				// Token: 0x040002C5 RID: 709
				Source,
				// Token: 0x040002C6 RID: 710
				Description
			}
		}
	}
}
