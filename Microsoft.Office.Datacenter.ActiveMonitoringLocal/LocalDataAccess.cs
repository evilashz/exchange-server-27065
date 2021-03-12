using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework;
using Microsoft.Office.Datacenter.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200009A RID: 154
	public class LocalDataAccess : DataAccess
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x0001DB80 File Offset: 0x0001BD80
		static LocalDataAccess()
		{
			IndexCapabilities.SupportsCaseInsensitiveStringComparison = true;
			LocalDataAccess.tables = new Dictionary<Type, ITable>();
			LocalDataAccess.notificationQueues = new Dictionary<Type, ConcurrentQueue<WorkDefinition>>();
			LocalDataAccess.maintenanceResultTable = new ResultsTable<MaintenanceResult>(Settings.MaintenanceResultHistoryWindowSize, false);
			LocalDataAccess.maintenanceResultTable.OnInsertNotificationDelegate = delegate(MaintenanceResult r)
			{
				LocalDataAccess.NotifyMaintenanceFailure(r);
			};
			LocalDataAccess.tables.Add(typeof(MaintenanceResult), LocalDataAccess.maintenanceResultTable);
			LocalDataAccess.maintenanceDefinitionTable = new DefinitionTable<MaintenanceDefinition>();
			LocalDataAccess.tables.Add(typeof(MaintenanceDefinition), LocalDataAccess.maintenanceDefinitionTable);
			LocalDataAccess.notificationQueues.Add(typeof(MaintenanceDefinition), new ConcurrentQueue<WorkDefinition>());
			LocalDataAccess.responderResultTable = new ResultsTable<ResponderResult>(Settings.ResponderResultHistoryWindowSize, false);
			LocalDataAccess.tables.Add(typeof(ResponderResult), LocalDataAccess.responderResultTable);
			LocalDataAccess.responderDefinitionTable = new DefinitionTable<ResponderDefinition>();
			LocalDataAccess.responderDefinitionTable.AddIndex<string>(ResponderDefinitionIndex.AlertMask(null));
			LocalDataAccess.tables.Add(typeof(ResponderDefinition), LocalDataAccess.responderDefinitionTable);
			LocalDataAccess.notificationQueues.Add(typeof(ResponderDefinition), new ConcurrentQueue<WorkDefinition>());
			LocalDataAccess.monitorResultTable = new ResultsTable<MonitorResult>(Settings.MonitorResultHistoryWindowSize, true);
			LocalDataAccess.monitorResultTable.AddIndex<string>(MonitorResultIndex.ComponentNameAndExecutionEndTime(null, DateTime.MinValue));
			LocalDataAccess.monitorResultTable.OnInsertNotificationDelegate = delegate(MonitorResult r)
			{
				LocalDataAccess.ProcessNotification<ResponderDefinition, MonitorResult>(LocalDataAccess.responderDefinitionTable, r, new Func<string, IIndexDescriptor<ResponderDefinition, string>>(ResponderDefinitionIndex.AlertMask));
			};
			LocalDataAccess.tables.Add(typeof(MonitorResult), LocalDataAccess.monitorResultTable);
			LocalDataAccess.monitorDefinitionTable = new DefinitionTable<MonitorDefinition>();
			LocalDataAccess.monitorDefinitionTable.AddIndex<string>(MonitorDefinitionIndex.SampleMask(null));
			LocalDataAccess.tables.Add(typeof(MonitorDefinition), LocalDataAccess.monitorDefinitionTable);
			LocalDataAccess.notificationQueues.Add(typeof(MonitorDefinition), new ConcurrentQueue<WorkDefinition>());
			LocalDataAccess.probeResultTable = new ResultsTable<ProbeResult>(Settings.ProbeResultHistoryWindowSize, true);
			LocalDataAccess.probeResultTable.AddIndex<string>(ProbeResultIndex.ScopeNameAndExecutionEndTime(null, DateTime.MinValue));
			LocalDataAccess.probeResultTable.OnInsertNotificationDelegate = delegate(ProbeResult r)
			{
				LocalDataAccess.ProcessNotification<MonitorDefinition, ProbeResult>(LocalDataAccess.monitorDefinitionTable, r, new Func<string, IIndexDescriptor<MonitorDefinition, string>>(MonitorDefinitionIndex.SampleMask));
			};
			LocalDataAccess.tables.Add(typeof(ProbeResult), LocalDataAccess.probeResultTable);
			LocalDataAccess.probeDefinitionTable = new DefinitionTable<ProbeDefinition>();
			LocalDataAccess.tables.Add(typeof(ProbeDefinition), LocalDataAccess.probeDefinitionTable);
			LocalDataAccess.notificationQueues.Add(typeof(ProbeDefinition), new ConcurrentQueue<WorkDefinition>());
			LocalDataAccess.monitorResultLogger = new MonitorResultLogger(new MonitorResultLogConfiguration("MonitorResults", "Monitor results"));
			LocalDataAccess.probeResultLogger = new MonitorResultLogger(new MonitorResultLogConfiguration("ProbeResults", "Probe results"));
			LocalDataAccess.responderResultLogger = new MonitorResultLogger(new MonitorResultLogConfiguration("ResponderResults", "Responder results"));
			LocalDataAccess.maintenanceResultLogger = new MonitorResultLogger(new MonitorResultLogConfiguration("MaintenanceResults", "Maintenance results"));
			LocalDataAccess.probePersistentStateLogger = new PersistentStateLogger(new PersistentStateLogConfiguration("ProbeCache", (long)Settings.MaxPersistentStateDirectorySizeInBytes, (long)Settings.MaxPersistentStateFileSizeInBytes));
			LocalDataAccess.monitorPersistentStateLogger = new PersistentStateLogger(new PersistentStateLogConfiguration("MonitorCache", (long)Settings.MaxPersistentStateDirectorySizeInBytes, (long)Settings.MaxPersistentStateFileSizeInBytes));
			LocalDataAccess.responderPersistentStateLogger = new PersistentStateLogger(new PersistentStateLogConfiguration("ResponderCache", (long)Settings.MaxPersistentStateDirectorySizeInBytes, (long)Settings.MaxPersistentStateFileSizeInBytes));
			LocalDataAccess.maintenancePersistentStateLogger = new PersistentStateLogger(new PersistentStateLogConfiguration("MaintenanceCache", (long)Settings.MaxPersistentStateDirectorySizeInBytes, (long)Settings.MaxPersistentStateFileSizeInBytes));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001DF07 File Offset: 0x0001C107
		public LocalDataAccess()
		{
			this.traceContext = TracingContext.Default;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001DF1A File Offset: 0x0001C11A
		public static string EndpointManagerNotificationId
		{
			get
			{
				return LocalDataAccess.endpointManagerNotificationId;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001DF21 File Offset: 0x0001C121
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001DF28 File Offset: 0x0001C128
		public static DateTime StartTime { get; internal set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001DF30 File Offset: 0x0001C130
		internal static MonitorResultLogger MonitorResultLogger
		{
			get
			{
				return LocalDataAccess.monitorResultLogger;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001DF37 File Offset: 0x0001C137
		internal static MonitorResultLogger ProbeResultLogger
		{
			get
			{
				return LocalDataAccess.probeResultLogger;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001DF3E File Offset: 0x0001C13E
		internal static MonitorResultLogger ResponderResultLogger
		{
			get
			{
				return LocalDataAccess.responderResultLogger;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001DF45 File Offset: 0x0001C145
		internal static MonitorResultLogger MaintenanceResultLogger
		{
			get
			{
				return LocalDataAccess.maintenanceResultLogger;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001DF4C File Offset: 0x0001C14C
		internal static string PersistentStateIdentity
		{
			get
			{
				return "WritePersistentState|Succeed";
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001DF90 File Offset: 0x0001C190
		public static void Initialize(IEnumerable<MaintenanceDefinition> discoveryWorkItems = null)
		{
			LocalDataAccess.StartTime = DateTime.UtcNow;
			LocalDataAccess.ClearDefinitionChannelAndInitializeIdGenerator<MaintenanceDefinition>();
			LocalDataAccess.ClearDefinitionChannelAndInitializeIdGenerator<ProbeDefinition>();
			LocalDataAccess.ClearDefinitionChannelAndInitializeIdGenerator<MonitorDefinition>();
			LocalDataAccess.ClearDefinitionChannelAndInitializeIdGenerator<ResponderDefinition>();
			DateTime now = DateTime.UtcNow;
			Task task = Task.Factory.StartNew(delegate()
			{
				LocalDataAccess.ReadResults<ProbeResult>(now);
			});
			Task task2 = Task.Factory.StartNew(delegate()
			{
				LocalDataAccess.ReadResults<MonitorResult>(now);
			});
			Task task3 = Task.Factory.StartNew(delegate()
			{
				LocalDataAccess.ReadResults<ResponderResult>(now);
			});
			Task task4 = Task.Factory.StartNew(delegate()
			{
				LocalDataAccess.ReadResults<MaintenanceResult>(now);
			});
			try
			{
				Task.WaitAll(new Task[]
				{
					task,
					task2,
					task3,
					task4
				});
			}
			catch (Exception arg)
			{
				string arg2 = Settings.IsPersistentStateEnabled ? "PersistentState" : "Crimson";
				WTFDiagnostics.TraceError<string, Exception>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.Initialize]: Failed read back results from the {0} system.\n{1}", arg2, arg, null, "Initialize", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 387);
			}
			LocalDataAccess.InitializeProbeResultNotification(now);
			LocalDataAccess.InitializeStartupNotification(now);
			LocalDataAccess.StartDiscovery(discoveryWorkItems);
			LocalDataAccess.StartXmlReader();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		internal static void InitializeProbeResultNotification(DateTime startTime)
		{
			WTFDiagnostics.TraceDebug(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "InitializeProbeResultNotification Start", null, "InitializeProbeResultNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 416);
			LocalDataAccess.probeResultWatcher = new ResultWatcher<ProbeResult>();
			LocalDataAccess.probeResultWatcher.QueryStartTime = new DateTime?(startTime);
			LocalDataAccess.probeResultWatcher.ResultArrivedCallback = new ResultWatcher<ProbeResult>.ResultArrivedDelegate(LocalDataAccess.ResultArrivedCallback);
			LocalDataAccess.probeResultWatcher.Start(true);
			WTFDiagnostics.TraceDebug(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "InitializeProbeResultNotification End", null, "InitializeProbeResultNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 428);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001E158 File Offset: 0x0001C358
		internal static void InitializeStartupNotification(DateTime startTime)
		{
			LocalDataAccess.startupNotificationIds = new HashSet<string>();
			WTFDiagnostics.TraceFunction(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.InitializeStartupNotification]: Start reading StartupNotification.", null, "InitializeStartupNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 439);
			using (CrimsonReader<StartupNotification> crimsonReader = new CrimsonReader<StartupNotification>(null, null, "Microsoft-Exchange-ManagedAvailability/StartupNotification"))
			{
				crimsonReader.QueryEndTime = new DateTime?(startTime);
				crimsonReader.QueryStartTime = new DateTime?(LocalDataAccess.systemBootTime);
				while (!crimsonReader.EndOfEventsReached)
				{
					StartupNotification startupNotification = crimsonReader.ReadNext();
					if (startupNotification != null && string.Compare(startupNotification.NotificationId, LocalDataAccess.EndpointManagerNotificationId, StringComparison.Ordinal) != 0 && LocalDataAccess.startupNotificationIds.Add(startupNotification.NotificationId))
					{
						WTFDiagnostics.TraceDebug<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.InitializeStartupNotification]: Added StartupNotification; NotificationId: {0}", startupNotification.NotificationId, null, "InitializeStartupNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 457);
					}
				}
			}
			LocalDataAccess.startupNotificationWatcher = new StartupNotificationWatcher();
			LocalDataAccess.startupNotificationWatcher.QueryStartTime = new DateTime?(startTime);
			LocalDataAccess.startupNotificationWatcher.StartupNotificationArrivedCallback = new StartupNotificationWatcher.StartupNotificationArrivedDelegate(LocalDataAccess.StartupNotificationArrivedCallback);
			LocalDataAccess.startupNotificationWatcher.Start();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001E278 File Offset: 0x0001C478
		internal static void ResultArrivedCallback(ProbeResult result)
		{
			if (result != null)
			{
				WTFDiagnostics.TraceDebug(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, string.Format("[LocalDataAccess.ResultArrivedCallback]: Adding notification result; name: {0}; start time {1}; end time {2}; workitem id: {3}", new object[]
				{
					result.ResultName,
					result.ExecutionStartTime,
					result.ExecutionEndTime,
					result.WorkItemId
				}), null, "ResultArrivedCallback", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 478);
				LocalDataAccess.probeResultTable.Insert(result, LocalDataAccess.staticTraceContext);
				LocalDataAccess.probeResultLogger.LogEvent(DateTime.UtcNow, result.ToDictionary());
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001E314 File Offset: 0x0001C514
		internal static void StartupNotificationArrivedCallback(StartupNotification startupNotification)
		{
			if (LocalDataAccess.startupNotificationIds.Add(startupNotification.NotificationId))
			{
				WTFDiagnostics.TraceDebug<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.StartupNotificationArrivedCallback]: Added StartupNotification; NotificationId: {0}", startupNotification.NotificationId, null, "StartupNotificationArrivedCallback", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 492);
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001E354 File Offset: 0x0001C554
		internal static Exception WritePersistentState<TResult>(CancellationToken cancellationToken) where TResult : WorkItemResult, IWorkItemResultSerialization, new()
		{
			WTFDiagnostics.TraceDebug<Type>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.WritePersistentState]: Start writing to PersistentState; Type: {0}", typeof(TResult), null, "WritePersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 505);
			bool flag = true;
			int num = 0;
			int num2 = 0;
			PersistentStateLogger persistentStateLogger = LocalDataAccess.GetPersistentStateLogger<TResult>();
			DateTime utcNow = DateTime.UtcNow;
			Exception result;
			try
			{
				IEnumerable<int> allWorkitemIds = LocalDataAccess.GetAllWorkitemIds<TResult>();
				foreach (int workItemId in allWorkitemIds)
				{
					IDataAccessQuery<TResult> resultsQuery = LocalDataAccess.GetResultsQuery<TResult>(workItemId, LocalDataAccess.GetNumberofLastResult<TResult>());
					foreach (TResult tresult in resultsQuery)
					{
						if (flag)
						{
							persistentStateLogger.SetForeLogFileRollOver(true);
							persistentStateLogger.LogEvent(tresult.Serialize());
							persistentStateLogger.SetForeLogFileRollOver(false);
							flag = false;
							num++;
						}
						else
						{
							persistentStateLogger.LogEvent(tresult.Serialize());
							if (cancellationToken.IsCancellationRequested)
							{
								WTFDiagnostics.TraceError<int, int>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.WritePersistentState]: cancelled by manager, NumOfDefinitions: {0}, NumofResults: {1}.\n", num2, num, null, "WritePersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 542);
								throw new OperationCanceledException(cancellationToken);
							}
							num++;
						}
					}
					num2++;
				}
				DateTime utcNow2 = DateTime.UtcNow;
				TimeSpan timeSpan = utcNow2 - utcNow;
				if (num != 0)
				{
					LocalDataAccessPerfCounters.DurationPersistentStateWrite.RawValue = (long)timeSpan.Milliseconds;
					LocalDataAccessPerfCounters.NumberofResultsPersistentStateWrite.RawValue = (long)num;
					persistentStateLogger.LogEvent(string.Format("{0}|StartTime|{1}|EndTime|{2}|Duration|{3}|NumOfDefinitions|{4}|NumofResults|{5}", new object[]
					{
						"WritePersistentState|Succeed",
						utcNow.ToString("o"),
						utcNow2.ToString("o"),
						timeSpan.Milliseconds,
						num2,
						num
					}));
					WTFDiagnostics.TraceDebug<DateTime, DateTime, int, int, int>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.WritePersistentState]: Succeed to write PersistentState; StartTime: {0}, EndTime: {1}, Duration: {2}, NumOfDefinitions: {3}, NumofResults: {4}", utcNow, utcNow2, timeSpan.Milliseconds, num2, num, null, "WritePersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 564);
				}
				WTFDiagnostics.TraceDebug<Type>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.WritePersistentState]: Finish writing to PersistentState; Type: {0}", typeof(TResult), null, "WritePersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 567);
				result = null;
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<Type, Exception>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.WritePersistentState]: Failed to write {0} PersistentState.\n{1}", typeof(TResult), ex, null, "WritePersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 573);
				result = ex;
			}
			return result;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001E620 File Offset: 0x0001C820
		internal static void WriteAllPersistentResults(CancellationToken cancellationToken)
		{
			if (!Settings.IsPersistentStateEnabled)
			{
				return;
			}
			Exception ex = LocalDataAccess.WritePersistentState<ProbeResult>(cancellationToken);
			Exception ex2 = LocalDataAccess.WritePersistentState<MonitorResult>(cancellationToken);
			Exception ex3 = LocalDataAccess.WritePersistentState<ResponderResult>(cancellationToken);
			Exception ex4 = LocalDataAccess.WritePersistentState<MaintenanceResult>(cancellationToken);
			if (ex2 != null || ex != null || ex3 != null || ex4 != null)
			{
				throw new Exception(string.Format("[LocalDataAccess.WriteAllPersistentResults]: Failed to write PersistentState.\n{0}\n{1}\n{2}\n{3}", new object[]
				{
					ex,
					ex2,
					ex3,
					ex4
				}));
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001E68C File Offset: 0x0001C88C
		internal static void ReadPersistentState<TResult>() where TResult : WorkItemResult, IWorkItemResultSerialization, new()
		{
			WTFDiagnostics.TraceDebug(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadPersistentState]: Start reading PersistentState.", null, "ReadPersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 609);
			string text = string.Empty;
			int num = 0;
			try
			{
				text = LocalDataAccess.GetPersistentStateLogger<TResult>().GetLogFile();
				if (string.IsNullOrEmpty(text))
				{
					WTFDiagnostics.TraceDebug(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadPersistentState]: Failed to read PersistentState; empty file.", null, "ReadPersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 619);
				}
				else
				{
					WTFDiagnostics.TraceDebug<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadPersistentState]: Succeeded to get the PersistentState file {0}.", text, null, "ReadPersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 623);
					DateTime utcNow = DateTime.UtcNow;
					using (StreamReader streamReader = new StreamReader(text))
					{
						string text2 = string.Empty;
						ITable<TResult> table = LocalDataAccess.GetTable<TResult>();
						while ((text2 = streamReader.ReadLine()) != null && !text2.Contains("WritePersistentState|Succeed"))
						{
							TResult tresult = Activator.CreateInstance<TResult>();
							tresult.Deserialize(text2);
							if (tresult != null)
							{
								WTFDiagnostics.TraceDebug<string, string, DateTime, DateTime>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadPersistentState]: Reading result: type {0}; name: {1}; start time {2}; end time {3}", typeof(WorkItemResult).Name, tresult.ResultName, tresult.ExecutionStartTime, tresult.ExecutionEndTime, null, "ReadPersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 644);
								table.Insert(tresult, LocalDataAccess.staticTraceContext);
								num++;
							}
						}
					}
					DateTime utcNow2 = DateTime.UtcNow;
					TimeSpan timeSpan = utcNow2 - utcNow;
					LocalDataAccessPerfCounters.DurationPersistentStateRead.RawValue = (long)timeSpan.Milliseconds;
					LocalDataAccessPerfCounters.NumberofResultsPersistentStateRead.RawValue = (long)num;
					WTFDiagnostics.TraceDebug<int, string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadPersistentState]: Succeed to read {0} results from the PersistentState file {0}.", num, text, null, "ReadPersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 657);
				}
			}
			catch (Exception arg)
			{
				WTFDiagnostics.TraceError<string, Exception>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadPersistentState]: Failed to read results from the PersistentState file {0}.\n{1}", text, arg, null, "ReadPersistentState", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 663);
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		internal static PersistentStateLogger GetPersistentStateLogger<TResult>()
		{
			PersistentStateLogger result;
			if (typeof(TResult) == typeof(ProbeResult))
			{
				result = LocalDataAccess.probePersistentStateLogger;
			}
			else if (typeof(TResult) == typeof(MonitorResult))
			{
				result = LocalDataAccess.monitorPersistentStateLogger;
			}
			else if (typeof(TResult) == typeof(ResponderResult))
			{
				result = LocalDataAccess.responderPersistentStateLogger;
			}
			else
			{
				if (!(typeof(TResult) == typeof(MaintenanceResult)))
				{
					throw new InvalidOperationException("Work result type is not supported");
				}
				result = LocalDataAccess.maintenancePersistentStateLogger;
			}
			return result;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001E95C File Offset: 0x0001CB5C
		internal static int GetNumberofLastResult<TResult>()
		{
			int result;
			if (typeof(TResult) == typeof(ProbeResult))
			{
				result = Settings.NumberOfLastProbeResults;
			}
			else if (typeof(TResult) == typeof(MonitorResult))
			{
				result = Settings.NumberOfLastMonitorResults;
			}
			else if (typeof(TResult) == typeof(ResponderResult))
			{
				result = Settings.NumberOfLastResponderResults;
			}
			else
			{
				if (!(typeof(TResult) == typeof(MaintenanceResult)))
				{
					throw new InvalidOperationException("Work result type is not supported");
				}
				result = Settings.NumberOfLastMaintenanceResults;
			}
			return result;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001EA1C File Offset: 0x0001CC1C
		internal static IDataAccessQuery<TWorkDefinition> GetAllDefinitions<TWorkDefinition>() where TWorkDefinition : WorkDefinition
		{
			IEnumerable<TWorkDefinition> query = from workDefinition in LocalDataAccess.GetTable<TWorkDefinition>().GetItems<bool>(WorkDefinitionIndex<TWorkDefinition>.Enabled(true))
			where workDefinition.DeploymentId == Settings.DeploymentId
			select workDefinition;
			LocalDataAccess localDataAccess = new LocalDataAccess();
			return localDataAccess.AsDataAccessQuery<TWorkDefinition>(query);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001EA58 File Offset: 0x0001CC58
		internal override IDataAccessQuery<TEntity> GetTable<TEntity, TKey>(IIndexDescriptor<TEntity, TKey> indexDescriptor)
		{
			IEnumerable<TEntity> items = LocalDataAccess.GetTable<TEntity>().GetItems<TKey>(indexDescriptor);
			DataAccessQuery<TEntity> query = new DataAccessQuery<TEntity>(items, this);
			return indexDescriptor.ApplyIndexRestriction(query);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		internal override Task<int> AsyncGetExclusive<TEntity>(int maxWorkitemCount, int deploymentId, Action<TEntity> processResult, Func<object, Exception, bool> corruptRowHandler, CancellationToken cancellationToken, TracingContext traceContext)
		{
			WTFDiagnostics.TraceFunction(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: Starting task", null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 779);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(4U, maxWorkitemCount);
			DateTime dateTime = DateTime.UtcNow;
			ConcurrentQueue<WorkDefinition> notificationQueue = LocalDataAccess.GetNotificationQueue<TEntity>();
			WorkDefinition workDefinition2;
			while (notificationQueue.TryDequeue(out workDefinition2))
			{
				if (!LocalDataAccess.ShouldWorkItemBeTriggered(workDefinition2, traceContext))
				{
					workDefinition2.StartTime = workDefinition2.StartTime.AddSeconds((double)workDefinition2.RecurrenceIntervalSeconds);
				}
				else if (workDefinition2.StartTime >= dateTime)
				{
					workDefinition2.StartTime = dateTime;
				}
			}
			dateTime = DateTime.UtcNow.AddTicks(1L);
			IOrderedEnumerable<TEntity> source = from workDefinition in LocalDataAccess.GetTable<TEntity>().GetItems<DateTime>(WorkDefinitionIndex<TEntity>.StartTime(dateTime))
			where workDefinition.DeploymentId == deploymentId && workDefinition.Enabled
			orderby workDefinition.StartTime
			select workDefinition;
			IEnumerable<TEntity> enumerable = source.Take(maxWorkitemCount);
			int num = 0;
			foreach (TEntity tentity in enumerable)
			{
				tentity.IntendedStartTime = tentity.StartTime;
				tentity.StartTime = ((tentity.RecurrenceIntervalSeconds == 0) ? DateTime.MaxValue : (dateTime + TimeSpan.FromSeconds((double)tentity.RecurrenceIntervalSeconds)));
				WorkItemResult workItemResult = null;
				WorkItemResult workItemResult2 = null;
				if (tentity is ProbeDefinition)
				{
					this.GetLastResults<ProbeDefinition, ProbeResult>(tentity as ProbeDefinition, out workItemResult, out workItemResult2);
				}
				else if (tentity is MonitorDefinition)
				{
					this.GetLastResults<MonitorDefinition, MonitorResult>(tentity as MonitorDefinition, out workItemResult, out workItemResult2);
				}
				else if (tentity is ResponderDefinition)
				{
					this.GetLastResults<ResponderDefinition, ResponderResult>(tentity as ResponderDefinition, out workItemResult, out workItemResult2);
				}
				else
				{
					if (!(tentity is MaintenanceDefinition))
					{
						throw new InvalidOperationException("Work definition type is not supported");
					}
					this.GetLastResults<MaintenanceDefinition, MaintenanceResult>(tentity as MaintenanceDefinition, out workItemResult, out workItemResult2);
				}
				if (workItemResult != null && workItemResult2 != null)
				{
					WTFDiagnostics.TraceInformation<string, DateTime>(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: definition {0} update time {1}", tentity.Name, tentity.UpdateTime, null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 854);
					WTFDiagnostics.TraceInformation<byte, DateTime>(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: Found result with PoisonedCount {0}, LastExecutionStartTime {1}", workItemResult2.PoisonedCount, workItemResult.ExecutionStartTime, null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 855);
					tentity.PoisonedCount = workItemResult2.PoisonedCount;
					tentity.LastExecutionStartTime = new DateTime?(workItemResult.ExecutionStartTime);
				}
				string text;
				string text2;
				if ((workItemResult == null || workItemResult.ExecutionStartTime < LocalDataAccess.StartTime) && tentity.Attributes.TryGetValue("StartupNotificationId", out text) && tentity.Attributes.TryGetValue("StartupNotificationMaxStartWaitInSeconds", out text2))
				{
					bool flag = true;
					if (string.IsNullOrWhiteSpace(text))
					{
						flag = false;
						WTFDiagnostics.TraceError<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.AsyncGetExclusive]: StartupNotificationId '{0}' is invalid.", text, null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 875);
					}
					int num2;
					if (!int.TryParse(text2, out num2))
					{
						flag = false;
						WTFDiagnostics.TraceError<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.AsyncGetExclusive]: StartupNotificationMaxStartWaitInSeconds '{0}' is invalid.", text2, null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 882);
					}
					if (flag)
					{
						WTFDiagnostics.TraceInformation(WTFLog.DataAccess, traceContext, string.Format("[LocalDataAccess.AsyncGetExclusive]: definition {0} has {1} (boot time '{2}'; StartupNotificationId='{3}'; StartupNotificationMaxStartWaitInSeconds={4})", new object[]
						{
							tentity.Name,
							(workItemResult != null) ? string.Format("lastStarted.ExecutionStartTime {0}", workItemResult.ExecutionStartTime) : "no previous lastStarted result",
							LocalDataAccess.systemBootTime,
							text,
							num2
						}), null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 887);
						if (dateTime < tentity.CreatedTime.AddSeconds((double)num2))
						{
							if (!LocalDataAccess.startupNotificationIds.Contains(text))
							{
								tentity.StartTime = DateTime.UtcNow.AddSeconds((double)((tentity.RecurrenceIntervalSeconds == 0) ? Settings.NonRecurrentRetryIntervalSeconds : tentity.RecurrenceIntervalSeconds));
								WTFDiagnostics.TraceInformation<string>(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: Skipped definition {0} due to lack of notification and maxStartWaitInSeconds not yet exceeded.", tentity.Name, null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 912);
								continue;
							}
							WTFDiagnostics.TraceInformation(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: StartupNotificationId received.", null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 922);
						}
						else
						{
							WTFDiagnostics.TraceInformation(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: StartupNotificationMaxStartWaitInSeconds exceeded.", null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 927);
						}
					}
					else
					{
						WTFDiagnostics.TraceError(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.AsyncGetExclusive]: StartupNotification logic skipped due to invalid settings.", null, "AsyncGetExclusive", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 932);
					}
				}
				Update<TEntity>.CreateUpdateNoCheck(tentity, "UpdateTime", DateTime.UtcNow.ToString("o"));
				processResult(tentity);
				num++;
			}
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetResult(num);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001F040 File Offset: 0x0001D240
		internal override Task<int> AsyncGetWorkItemPackages<TWorkItem>(int deploymentId, Action<string> processResult, CancellationToken cancellationToken, TracingContext traceContext)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetResult(0);
			return taskCompletionSource.Task;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001F060 File Offset: 0x0001D260
		internal override Task<int> AsyncDisableWorkDefinitions(int createdById, DateTime createdBeforeTimestamp, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001F067 File Offset: 0x0001D267
		internal override Task<int> AsyncDeleteWorkItemResult<TWorkItemResult>(DateTime startTime, DateTime endTime, int timeOutInSeconds, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001F070 File Offset: 0x0001D270
		internal override Task AsyncInsert<TEntity>(TEntity entity, CancellationToken cancellationToken, TracingContext traceContext)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest<TEntity>(3U, entity);
			this.WriteToCrimson<TEntity>(entity);
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetResult(0);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001F0A3 File Offset: 0x0001D2A3
		internal override Task AsyncInsertMany<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001F0AA File Offset: 0x0001D2AA
		internal override Task AsyncInsertManyDefinitionsAndCleanup<TEntity>(IEnumerable<TEntity> entities, int id, DateTime cleanBeforeTime, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		internal override Task<int> AsyncExecuteReader<TEntity>(IDataAccessQuery<TEntity> query, Action<TEntity> processResult, CancellationToken cancellationToken, TracingContext traceContext)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest<IDataAccessQuery<TEntity>>(0U, query);
			int num = 0;
			foreach (TEntity obj in query)
			{
				processResult(obj);
				num++;
			}
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetResult(num);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001F124 File Offset: 0x0001D324
		internal override Task<TEntity> AsyncExecuteScalar<TEntity>(IDataAccessQuery<TEntity> query, CancellationToken cancellationToken, TracingContext traceContext)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest<IDataAccessQuery<TEntity>>(1U, query);
			TEntity result = query.FirstOrDefault<TEntity>();
			TaskCompletionSource<TEntity> taskCompletionSource = new TaskCompletionSource<TEntity>();
			taskCompletionSource.SetResult(result);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001F158 File Offset: 0x0001D358
		internal override TimeSpan? GetQuarantineTimeSpan<TEntity>(TEntity definition)
		{
			WTFDiagnostics.TraceFunction<string, int>(WTFLog.DataAccess, this.traceContext, "[LocalDataAccess.GetQuarantineTimeSpan]: Getting quarantine timespan for {0} with Id {1}", definition.Name, definition.Id, null, "GetQuarantineTimeSpan", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1094);
			DateTime? quarantineEndTime = Quarantine<TEntity>.GetQuarantineEndTime(definition);
			DateTime utcNow = DateTime.UtcNow;
			if (quarantineEndTime == null)
			{
				WTFDiagnostics.TraceDebug(WTFLog.DataAccess, this.traceContext, "[LocalDataAccess.GetQuarantineTimeSpan]: Quarantine endtime is null", null, "GetQuarantineTimeSpan", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1100);
				if ((int)definition.PoisonedCount >= Settings.MaxPoisonCount)
				{
					DateTime d = Quarantine<TEntity>.SetQuarantine(definition.Id);
					LocalDataAccess.NotifyQuarantine<TEntity>(definition, ResultSeverityLevel.Error);
					return new TimeSpan?(d - utcNow);
				}
				return null;
			}
			else
			{
				WTFDiagnostics.TraceDebug<DateTime?>(WTFLog.DataAccess, this.traceContext, "[LocalDataAccess.GetQuarantineTimeSpan]: Quarantine endtime is {0}", quarantineEndTime, null, "GetQuarantineTimeSpan", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1117);
				if (quarantineEndTime < utcNow)
				{
					Quarantine<TEntity>.RemoveQuarantine(definition.Id);
					LocalDataAccess.NotifyQuarantine<TEntity>(definition, ResultSeverityLevel.Informational);
					return null;
				}
				definition.PoisonedCount = 0;
				DateTime? dateTime = quarantineEndTime;
				DateTime d2 = utcNow;
				if (dateTime == null)
				{
					return null;
				}
				return new TimeSpan?(dateTime.GetValueOrDefault() - d2);
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001F2CB File Offset: 0x0001D4CB
		internal override BaseDataAccess GetTopologyDataAccessProvider()
		{
			return this;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001F2CE File Offset: 0x0001D4CE
		internal override Task<IEnumerable<TableSchema>> GetTableData<TableSchema, TKey>(IIndexDescriptor<TableSchema, TKey> indexDescriptor, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001F2D5 File Offset: 0x0001D4D5
		internal override WorkUnit RequestWorkUnit(CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001F2DC File Offset: 0x0001D4DC
		internal override int HeartbeatForWorkUnit(WorkUnit workUnit, int workUnitState, out List<WorkUnitEntry> entries, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001F2E3 File Offset: 0x0001D4E3
		internal override Task<int> AddAndRemoveWorkUnitEntries(List<WorkUnitEntry> workUnitEntriesToAdd, List<WorkUnitEntry> workUnitEntriesToRemove, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001F2EA File Offset: 0x0001D4EA
		internal override Task<int> AddWorkUnit(CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001F2F1 File Offset: 0x0001D4F1
		internal override int GetWorkState(CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		internal override Task<bool> RequestRecovery(string metricName, string recoveryType, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001F2FF File Offset: 0x0001D4FF
		internal override Task<List<StatusEntryCollection>> GetAllStatusEntries(CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001F306 File Offset: 0x0001D506
		internal override Task<StatusEntryCollection> GetStatusEntries(string key, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001F30D File Offset: 0x0001D50D
		internal override Task SaveStatusEntry(StatusEntry entry, CancellationToken cancellationToken, TracingContext traceContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001F314 File Offset: 0x0001D514
		internal override IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query)
		{
			return new DataAccessQuery<TEntity>(query, this);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001F31D File Offset: 0x0001D51D
		protected internal static void UpdateInMemoryTable<TEntity>(IPersistence entity)
		{
			LocalDataAccess.GetTable<TEntity>().Insert((TEntity)((object)entity), LocalDataAccess.staticTraceContext);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001F334 File Offset: 0x0001D534
		protected virtual void WriteToCrimson<TEntity>(TEntity entity) where TEntity : class
		{
			((IPersistence)((object)entity)).Write(new Action<IPersistence>(LocalDataAccess.UpdateInMemoryTable<TEntity>));
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001F354 File Offset: 0x0001D554
		private static bool ShouldWorkItemBeTriggered(WorkDefinition definition, TracingContext traceContext)
		{
			ResponderDefinition responderDefinition = definition as ResponderDefinition;
			if (responderDefinition != null)
			{
				MonitorResult monitorResult = definition.ObjectData as MonitorResult;
				if (monitorResult != null && responderDefinition.TargetHealthState != ServiceHealthStatus.None && (!monitorResult.IsAlert || monitorResult.HealthState != responderDefinition.TargetHealthState))
				{
					WTFDiagnostics.TraceInformation<string>(WTFLog.DataAccess, traceContext, "[LocalDataAccess.AsyncGetExclusive]: WorkItem '{0}' is not due. This workitem will be delayed.", responderDefinition.Name, null, "ShouldWorkItemBeTriggered", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1322);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001F3C4 File Offset: 0x0001D5C4
		private static void StartXmlReader()
		{
			WTFDiagnostics.TraceFunction(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.StartXmlReader]: Create XML definition reader workitem", null, "StartXmlReader", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1340);
			MaintenanceDefinition maintenanceDefinition = XmlDefinitionReaderWorkItem.CreateDefinition();
			maintenanceDefinition.Write(new Action<IPersistence>(LocalDataAccess.UpdateInMemoryTable<MaintenanceDefinition>));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001F410 File Offset: 0x0001D610
		private static void StartDiscovery(IEnumerable<MaintenanceDefinition> discoveryWorkItems)
		{
			if (discoveryWorkItems != null)
			{
				WTFDiagnostics.TraceInformation(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.StartDiscovery]: Start discovery workitems", null, "StartDiscovery", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1354);
				foreach (MaintenanceDefinition maintenanceDefinition in discoveryWorkItems)
				{
					WTFDiagnostics.TraceInformation<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.StartDiscovery]: Add discovery workitem {0}", maintenanceDefinition.Name, null, "StartDiscovery", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1358);
					maintenanceDefinition.Write(new Action<IPersistence>(LocalDataAccess.UpdateInMemoryTable<MaintenanceDefinition>));
				}
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		private static void NotifyQuarantine<TEntity>(TEntity definition, ResultSeverityLevel severity) where TEntity : WorkDefinition
		{
			WTFDiagnostics.TraceFunction<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.NotifyQuarantine]: Publishing quarantine notification event for {0}", definition.Name, null, "NotifyQuarantine", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1372);
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Monitoring.Name, MonitoringNotificationEvent.WorkitemQuarantine.ToString(), null, null, severity);
			eventNotificationItem.AddCustomProperty("Component", definition.ServiceName ?? string.Empty);
			eventNotificationItem.AddCustomProperty("Type", typeof(TEntity).Name);
			eventNotificationItem.AddCustomProperty("Name", definition.Name);
			eventNotificationItem.AddCustomProperty("Id", definition.Id);
			eventNotificationItem.Publish(false);
			WTFDiagnostics.TraceFunction<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.NotifyQuarantine]: Published quarantine notification event for {0}", definition.Name, null, "NotifyQuarantine", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1379);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001F5BC File Offset: 0x0001D7BC
		private static void SendMaintenanceFailureNotification(MaintenanceDefinition definition, MaintenanceResult r, MonitoringNotificationEvent eventType, ResultSeverityLevel severity)
		{
			WTFDiagnostics.TraceFunction<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.SendMaintenanceFailureNotification]: Publishing maintenance failure notification event for {0}", definition.Name, null, "SendMaintenanceFailureNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1391);
			string message;
			if (!string.IsNullOrWhiteSpace(r.Exception))
			{
				message = r.Exception;
			}
			else
			{
				message = r.Error;
			}
			Component component = null;
			string name;
			if (!string.IsNullOrEmpty(definition.ServiceName) && ExchangeComponent.WellKnownComponents.TryGetValue(definition.ServiceName, out component))
			{
				name = component.Name;
			}
			else
			{
				name = ExchangeComponent.Monitoring.Name;
			}
			EventNotificationItem eventNotificationItem = new EventNotificationItem(name, eventType.ToString(), null, message, severity);
			eventNotificationItem.AddCustomProperty("Component", name);
			eventNotificationItem.AddCustomProperty("Name", definition.Name);
			eventNotificationItem.AddCustomProperty("Id", definition.Id);
			eventNotificationItem.Publish(false);
			WTFDiagnostics.TraceFunction<string, string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.SendMaintenanceFailureNotification]: Published maintenance notification event '{0}' for definition '{1}'", eventType.ToString(), definition.Name, null, "SendMaintenanceFailureNotification", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1421);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
		private static ITable<TEntity> GetTable<TEntity>()
		{
			Type typeFromHandle = typeof(TEntity);
			if (!LocalDataAccess.tables.ContainsKey(typeFromHandle))
			{
				throw new ArgumentException(string.Format("Unsupported entity type {0}.", typeFromHandle.Name));
			}
			return (ITable<TEntity>)LocalDataAccess.tables[typeFromHandle];
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001F71B File Offset: 0x0001D91B
		private static ITable<TEntity> GetTable<TEntity>(TEntity entity)
		{
			return LocalDataAccess.GetTable<TEntity>();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001F724 File Offset: 0x0001D924
		private static ConcurrentQueue<WorkDefinition> GetNotificationQueue<TWorkDefinition>() where TWorkDefinition : WorkDefinition
		{
			Type typeFromHandle = typeof(TWorkDefinition);
			return LocalDataAccess.notificationQueues[typeFromHandle];
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001F75C File Offset: 0x0001D95C
		private static void NotifyMaintenanceFailure(MaintenanceResult r)
		{
			MaintenanceDefinition maintenanceDefinition = (from workDefinition in LocalDataAccess.maintenanceDefinitionTable.GetItems<int>(WorkDefinitionIndex<MaintenanceDefinition>.Id(r.WorkItemId))
			select workDefinition).FirstOrDefault<MaintenanceDefinition>();
			if (maintenanceDefinition == null)
			{
				return;
			}
			if (MonitorWorkItem.ShouldConsiderFailed(r.ResultType))
			{
				MonitoringNotificationEvent eventType;
				if (r.ResultType == ResultType.TimedOut)
				{
					eventType = MonitoringNotificationEvent.MaintenanceTimeout;
				}
				else
				{
					eventType = MonitoringNotificationEvent.MaintenanceFailure;
				}
				if (maintenanceDefinition.RecurrenceIntervalSeconds == 0)
				{
					LocalDataAccess.SendMaintenanceFailureNotification(maintenanceDefinition, r, eventType, ResultSeverityLevel.Error);
					return;
				}
				IOrderedEnumerable<MaintenanceResult> orderedEnumerable = from workResult in LocalDataAccess.maintenanceResultTable.GetItems<int>(WorkItemResultIndex<MaintenanceResult>.WorkItemIdAndExecutionEndTime(r.WorkItemId, LocalDataAccess.StartTime))
				orderby workResult.ExecutionEndTime descending
				select workResult;
				int num = 0;
				foreach (MaintenanceResult maintenanceResult in orderedEnumerable)
				{
					if (!MonitorWorkItem.ShouldConsiderFailed(maintenanceResult.ResultType) || num >= Settings.ConsecutiveMaintenanceFailureThreshold)
					{
						break;
					}
					num++;
				}
				if (num == Settings.ConsecutiveMaintenanceFailureThreshold)
				{
					LocalDataAccess.SendMaintenanceFailureNotification(maintenanceDefinition, r, eventType, ResultSeverityLevel.Error);
					return;
				}
			}
			else if (r.ResultType == ResultType.Succeeded)
			{
				IOrderedEnumerable<MaintenanceResult> orderedEnumerable2 = from workResult in LocalDataAccess.maintenanceResultTable.GetItems<int>(WorkItemResultIndex<MaintenanceResult>.WorkItemIdAndExecutionEndTime(r.WorkItemId, LocalDataAccess.StartTime))
				orderby workResult.ExecutionEndTime descending
				select workResult;
				int num2 = 0;
				MaintenanceResult maintenanceResult2 = null;
				foreach (MaintenanceResult maintenanceResult3 in orderedEnumerable2)
				{
					if (num2 == 1)
					{
						maintenanceResult2 = maintenanceResult3;
						break;
					}
					num2++;
				}
				if (maintenanceResult2 != null && MonitorWorkItem.ShouldConsiderFailed(maintenanceResult2.ResultType))
				{
					MonitoringNotificationEvent eventType2;
					if (maintenanceResult2.ResultType == ResultType.TimedOut)
					{
						eventType2 = MonitoringNotificationEvent.MaintenanceTimeout;
					}
					else
					{
						eventType2 = MonitoringNotificationEvent.MaintenanceFailure;
					}
					LocalDataAccess.SendMaintenanceFailureNotification(maintenanceDefinition, r, eventType2, ResultSeverityLevel.Informational);
				}
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001F94C File Offset: 0x0001DB4C
		private static void ProcessNotification<TWorkDefinition, TWorkItemResult>(DefinitionTable<TWorkDefinition> table, TWorkItemResult result, Func<string, IIndexDescriptor<TWorkDefinition, string>> indexDescriptorCreator) where TWorkDefinition : WorkDefinition where TWorkItemResult : WorkItemResult
		{
			IIndexDescriptor<TWorkItemResult, string> indexDescriptor = WorkItemResultIndex<TWorkItemResult>.ResultNameAndExecutionEndTime(null, DateTime.MinValue);
			Type typeFromHandle = typeof(TWorkDefinition);
			foreach (string arg in indexDescriptor.GetKeyValues(result))
			{
				IIndexDescriptor<TWorkDefinition, string> indexDescriptor2 = indexDescriptorCreator(arg);
				IEnumerable<TWorkDefinition> items = table.GetItems<string>(indexDescriptor2);
				foreach (TWorkDefinition tworkDefinition in items)
				{
					tworkDefinition.ObjectData = result;
					if (tworkDefinition is ResponderDefinition || tworkDefinition.RecurrenceIntervalSeconds == 0)
					{
						LocalDataAccess.notificationQueues[typeFromHandle].Enqueue(tworkDefinition);
					}
				}
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001FA44 File Offset: 0x0001DC44
		private static void ReadResults<TResult>(DateTime endTime) where TResult : WorkItemResult, IPersistence, IWorkItemResultSerialization, new()
		{
			if (Settings.IsPersistentStateEnabled)
			{
				LocalDataAccess.ReadPersistentState<TResult>();
				return;
			}
			WTFDiagnostics.TraceFunction<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadResults]: Start reading result {0}", typeof(TResult).Name, null, "ReadResults", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1617);
			using (CrimsonReader<TResult> crimsonReader = new CrimsonReader<TResult>())
			{
				crimsonReader.QueryEndTime = new DateTime?(endTime);
				crimsonReader.QueryStartTime = new DateTime?(endTime.AddMinutes((double)(-(double)LocalDataAccess.resultTimeWindowInMinutes)));
				ITable<TResult> table = LocalDataAccess.GetTable<TResult>();
				while (!crimsonReader.EndOfEventsReached)
				{
					TResult tresult = crimsonReader.ReadNext();
					if (tresult != null)
					{
						WTFDiagnostics.TraceDebug<string, string, DateTime, DateTime>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ReadResults]: Reading result; type {0}; name: {1}; start time {2}; end time {3}", typeof(TResult).Name, tresult.ResultName, tresult.ExecutionStartTime, tresult.ExecutionEndTime, null, "ReadResults", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1631);
						table.Insert(tresult, LocalDataAccess.staticTraceContext);
					}
				}
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001FB60 File Offset: 0x0001DD60
		private static void ClearDefinitionChannelAndInitializeIdGenerator<TDefinition>() where TDefinition : WorkDefinition, new()
		{
			WTFDiagnostics.TraceFunction<string>(WTFLog.DataAccess, LocalDataAccess.staticTraceContext, "[LocalDataAccess.ClearDefinitionChannelAndInitializeIdGenerator]: Start cleaning {0} from last run", typeof(TDefinition).Name, null, "ClearDefinitionChannelAndInitializeIdGenerator", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\LocalDataAccess.cs", 1650);
			CrimsonHelper.ClearCrimsonChannel<TDefinition>();
			DefinitionIdGenerator<TDefinition>.Initialize();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		private static IDataAccessQuery<TWorkItemResult> GetResultsQuery<TWorkItemResult>(int workItemId, int num) where TWorkItemResult : WorkItemResult
		{
			IEnumerable<TWorkItemResult> query = (from r in LocalDataAccess.GetTable<TWorkItemResult>().GetItems<int>(WorkItemResultIndex<TWorkItemResult>.WorkItemIdAndExecutionEndTime(workItemId, DateTime.MinValue))
			orderby r.ExecutionStartTime descending
			select r).Take(num);
			LocalDataAccess localDataAccess = new LocalDataAccess();
			return localDataAccess.AsDataAccessQuery<TWorkItemResult>(query);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		private static IDataAccessQuery<TWorkItemResult> GetAllResultsQuery<TWorkItemResult>() where TWorkItemResult : WorkItemResult
		{
			IEnumerable<TWorkItemResult> query = from r in LocalDataAccess.GetTable<TWorkItemResult>().GetItems<string>(WorkItemResultIndex<TWorkItemResult>.AllResultsAndExecutionEndTime(DateTime.MinValue))
			select r;
			LocalDataAccess localDataAccess = new LocalDataAccess();
			return localDataAccess.AsDataAccessQuery<TWorkItemResult>(query);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		private static IEnumerable<int> GetAllWorkitemIds<TWorkItemResult>() where TWorkItemResult : WorkItemResult
		{
			return (from r in LocalDataAccess.GetAllResultsQuery<TWorkItemResult>()
			select r.WorkItemId).Distinct<int>();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001FC94 File Offset: 0x0001DE94
		private void GetLastResults<TWorkDefinition, TWorkItemResult>(TWorkDefinition definition, out WorkItemResult lastStarted, out WorkItemResult lastEnded) where TWorkDefinition : WorkDefinition where TWorkItemResult : WorkItemResult
		{
			IIndexDescriptor<TWorkItemResult, int> indexDescriptor = WorkItemResultIndex<TWorkItemResult>.WorkItemIdAndExecutionEndTime(definition.Id, DateTime.MinValue);
			IDataAccessQuery<TWorkItemResult> table = this.GetTable<TWorkItemResult, int>(indexDescriptor);
			lastStarted = (from r in table
			orderby r.ExecutionStartTime descending
			select r).FirstOrDefault<TWorkItemResult>();
			lastEnded = (from r in table
			orderby r.ExecutionEndTime descending
			select r).FirstOrDefault<TWorkItemResult>();
		}

		// Token: 0x040005C8 RID: 1480
		private const string persistentStateIdentity = "WritePersistentState|Succeed";

		// Token: 0x040005C9 RID: 1481
		private static readonly string endpointManagerNotificationId = "LocalEndpointManager";

		// Token: 0x040005CA RID: 1482
		private static DefinitionTable<ProbeDefinition> probeDefinitionTable;

		// Token: 0x040005CB RID: 1483
		private static ResultsTable<ProbeResult> probeResultTable;

		// Token: 0x040005CC RID: 1484
		private static DefinitionTable<MonitorDefinition> monitorDefinitionTable;

		// Token: 0x040005CD RID: 1485
		private static ResultsTable<MonitorResult> monitorResultTable;

		// Token: 0x040005CE RID: 1486
		private static DefinitionTable<ResponderDefinition> responderDefinitionTable;

		// Token: 0x040005CF RID: 1487
		private static ResultsTable<ResponderResult> responderResultTable;

		// Token: 0x040005D0 RID: 1488
		private static DefinitionTable<MaintenanceDefinition> maintenanceDefinitionTable;

		// Token: 0x040005D1 RID: 1489
		private static ResultsTable<MaintenanceResult> maintenanceResultTable;

		// Token: 0x040005D2 RID: 1490
		private static Dictionary<Type, ITable> tables;

		// Token: 0x040005D3 RID: 1491
		private static Dictionary<Type, ConcurrentQueue<WorkDefinition>> notificationQueues;

		// Token: 0x040005D4 RID: 1492
		private static int resultTimeWindowInMinutes = Settings.ResultHistoryWindowInMinutes;

		// Token: 0x040005D5 RID: 1493
		private static ResultWatcher<ProbeResult> probeResultWatcher;

		// Token: 0x040005D6 RID: 1494
		private static TracingContext staticTraceContext = TracingContext.Default;

		// Token: 0x040005D7 RID: 1495
		private static DateTime systemBootTime = StartupNotification.GetSystemBootTime(true);

		// Token: 0x040005D8 RID: 1496
		private static HashSet<string> startupNotificationIds;

		// Token: 0x040005D9 RID: 1497
		private static StartupNotificationWatcher startupNotificationWatcher;

		// Token: 0x040005DA RID: 1498
		private static MonitorResultLogger monitorResultLogger;

		// Token: 0x040005DB RID: 1499
		private static MonitorResultLogger probeResultLogger;

		// Token: 0x040005DC RID: 1500
		private static MonitorResultLogger responderResultLogger;

		// Token: 0x040005DD RID: 1501
		private static MonitorResultLogger maintenanceResultLogger;

		// Token: 0x040005DE RID: 1502
		private static PersistentStateLogger probePersistentStateLogger;

		// Token: 0x040005DF RID: 1503
		private static PersistentStateLogger monitorPersistentStateLogger;

		// Token: 0x040005E0 RID: 1504
		private static PersistentStateLogger responderPersistentStateLogger;

		// Token: 0x040005E1 RID: 1505
		private static PersistentStateLogger maintenancePersistentStateLogger;

		// Token: 0x040005E2 RID: 1506
		private TracingContext traceContext;
	}
}
