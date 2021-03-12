using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000546 RID: 1350
	internal static class MonitoringServerManager
	{
		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x000CABDD File Offset: 0x000C8DDD
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x000CABE4 File Offset: 0x000C8DE4
		public static IObserverFactory ObserverFactory { get; internal set; } = new ObserverFactory();

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x000CABEC File Offset: 0x000C8DEC
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x000CABF3 File Offset: 0x000C8DF3
		public static ISubjectFactory SubjectFactory { get; internal set; } = new SubjectFactory();

		// Token: 0x06002133 RID: 8499 RVA: 0x000CABFC File Offset: 0x000C8DFC
		public static bool TryAddDatabase(Guid mdbGuid)
		{
			string text = mdbGuid.ToString().ToLower();
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Try add mailbox database {0} into monitoring list", text, null, "TryAddDatabase", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 200);
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.MdbListRegistry))
			{
				registryKey.SetValue(text, MonitoringServerManager.ConstructStatusCode(DatabaseMonitoringStatus.Received));
				result = true;
			}
			return result;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000CAC84 File Offset: 0x000C8E84
		public static void RemoveDatabase(Guid mdbGuid)
		{
			string text = mdbGuid.ToString().ToLower();
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Removing mailbox database {0} from monitoring list", text, null, "RemoveDatabase", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 224);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.MdbListRegistry))
			{
				registryKey.DeleteValue(text, false);
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x000CAD00 File Offset: 0x000C8F00
		public static string[] GetAllDatabaseGuids()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Read all monitored mailbox database lists", null, "GetAllDatabaseGuids", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 242);
			string[] valueNames;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.MdbListRegistry))
			{
				valueNames = registryKey.GetValueNames();
			}
			return valueNames;
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000CAD68 File Offset: 0x000C8F68
		public static void SetDatabaseStatus(string name, DatabaseMonitoringStatus databaseMonitoringStatus)
		{
			WTFDiagnostics.TraceFunction<string, DatabaseMonitoringStatus>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Update mailbox database {0} with monitoring status {1}", name, databaseMonitoringStatus, null, "SetDatabaseStatus", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 260);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.MdbListRegistry))
			{
				registryKey.SetValue(name, MonitoringServerManager.ConstructStatusCode(databaseMonitoringStatus));
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x000CADDC File Offset: 0x000C8FDC
		public static Dictionary<string, int> GetDatabaseStatusList()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Read all monitored mailbox databases with status", null, "GetDatabaseStatusList", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 274);
			return MonitoringServerManager.GetDatabaseListHelper(MonitoringServerManager.MdbListRegistry);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x000CAE0C File Offset: 0x000C900C
		private static Dictionary<string, int> GetDatabaseListHelper(string subkey)
		{
			Dictionary<string, int> result;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subkey))
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (string text in registryKey.GetValueNames())
				{
					object value = registryKey.GetValue(text);
					if (value is int)
					{
						dictionary[text] = (int)value;
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000CAE8C File Offset: 0x000C908C
		public static bool TryAddObserver(string server)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Trying to add server {0} into observers list", server, null, "TryAddObserver", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 311);
			return MonitoringServerManager.TryAddServer(server, MonitoringServerManager.ObserverList, MonitoringServerManager.MaxObservers);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000CAEC3 File Offset: 0x000C90C3
		public static void RemoveObserver(string server)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Removing server {0} from observers list", server, null, "RemoveObserver", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 322);
			MonitoringServerManager.RemoveServer(server, MonitoringServerManager.ObserverList);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x000CAEF5 File Offset: 0x000C90F5
		public static string[] GetAllObservers()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Read observers list", null, "GetAllObservers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 333);
			return MonitoringServerManager.GetAllServers(MonitoringServerManager.ObserverList);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000CAF28 File Offset: 0x000C9128
		public static bool TryAddSubject(string server)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Trying to add server {0} into subjects list", server, null, "TryAddSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 345);
			ISubject subject = MonitoringServerManager.SubjectFactory.CreateSubject(NativeHelpers.GetLocalComputerFqdn(true));
			if (!subject.IsInMaintenance)
			{
				return MonitoringServerManager.TryAddServer(server, MonitoringServerManager.SubjectList, MonitoringServerManager.MaxSubjects);
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Cannot add server {0} into list because this machine is in maintenance.", server, null, "TryAddSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 356);
			return false;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000CAFAA File Offset: 0x000C91AA
		public static void RemoveSubject(string server)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Removing server {0} from subjects list", server, null, "RemoveSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 368);
			MonitoringServerManager.RemoveServer(server, MonitoringServerManager.SubjectList);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000CAFDC File Offset: 0x000C91DC
		public static string[] GetAllSubjects()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Read subjects list", null, "GetAllSubjects", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 379);
			return MonitoringServerManager.GetAllServers(MonitoringServerManager.SubjectList);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000CB00C File Offset: 0x000C920C
		public static ObserverHeartbeatResponse UpdateObserverHeartbeat(string observer)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Update heartbeat from observer {0}", observer, null, "UpdateObserverHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 391);
			bool flag = false;
			ObserverHeartbeatResponse result;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.ObserverList))
				{
					if (registryKey.GetValue(observer) == null)
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Invalid request from unknown observer {0}", observer, null, "UpdateObserverHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 411);
						result = ObserverHeartbeatResponse.UnknownObserver;
					}
					else
					{
						registryKey.SetValue(observer, DateTime.UtcNow.ToString());
						result = ObserverHeartbeatResponse.Success;
					}
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
			return result;
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000CB0F8 File Offset: 0x000C92F8
		public static DateTime? GetObserverHeartbeat(string observer)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Looking for heartbeat timestamp from observer {0}", observer, null, "GetObserverHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 438);
			bool flag = false;
			DateTime? result;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.ObserverList))
				{
					object value = registryKey.GetValue(observer);
					DateTime value2;
					if (value == null)
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "No heartbeat timestamp found for observer {0}", observer, null, "GetObserverHeartbeat", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 458);
					}
					else if (DateTime.TryParse(value as string, out value2))
					{
						return new DateTime?(value2);
					}
					result = null;
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
			return result;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000CB1EC File Offset: 0x000C93EC
		public static DateTime? GetLastObserverSelectionTimestamp()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Looking for timestamp of last maintenance run", null, "GetLastObserverSelectionTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 488);
			bool flag = false;
			DateTime? result;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.RegistryPathBase))
				{
					object value = registryKey.GetValue("ObserverSelection");
					DateTime value2;
					if (value == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Timestamp of last maintenance run not found", null, "GetLastObserverSelectionTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 508);
					}
					else if (DateTime.TryParse(value as string, out value2))
					{
						return new DateTime?(value2);
					}
					result = null;
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
			return result;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000CB2E0 File Offset: 0x000C94E0
		public static void UpdateLastObserverSelectionTimestamp()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Update last observer selection timestamp", null, "UpdateLastObserverSelectionTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 537);
			bool flag = false;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MonitoringServerManager.RegistryPathBase))
				{
					registryKey.SetValue("ObserverSelection", DateTime.UtcNow.ToString());
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000CB398 File Offset: 0x000C9598
		internal static DatabaseMonitoringStatus DecodeStatus(int statusCode, out DateTime time)
		{
			DatabaseMonitoringStatus result = (DatabaseMonitoringStatus)(statusCode & 15);
			int num = statusCode >> 4;
			time = MonitoringServerManager.StartTime.AddMinutes((double)num);
			return result;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000CB3DC File Offset: 0x000C95DC
		internal static bool TryAddServer(string server, string serverList, int maxServers)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Trying to add server {0} into list", server, null, "TryAddServer", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 591);
			bool flag = false;
			bool result;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(serverList))
				{
					if (registryKey.ValueCount >= maxServers)
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Cannot add server {0} into list because list is full", server, null, "TryAddServer", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 610);
						result = false;
					}
					else if (Array.Exists<string>(MonitoringServerManager.GetAllServers(serverList), (string newServer) => server == newServer))
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Server {0} is already in the list; reporting success even though we no-op'ed", server, null, "TryAddServer", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 620);
						result = true;
					}
					else
					{
						registryKey.SetValue(server, DateTime.UtcNow.ToString());
						result = true;
					}
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
			return result;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000CB534 File Offset: 0x000C9734
		internal static void RemoveServer(string server, string serverList)
		{
			WTFDiagnostics.TraceFunction<string>(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Removing server {0} from list", server, null, "RemoveServer", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 645);
			bool flag = false;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(serverList))
				{
					registryKey.DeleteValue(server, false);
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000CB5D4 File Offset: 0x000C97D4
		internal static string[] GetAllServers(string serverList)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, MonitoringServerManager.traceContext, "Read servers list", null, "GetAllServers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\MonitoringServerManager.cs", 681);
			bool flag = false;
			string[] valueNames;
			try
			{
				try
				{
					flag = MonitoringServerManager.registryMutex.WaitOne();
				}
				catch (AbandonedMutexException)
				{
					flag = true;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(serverList))
				{
					valueNames = registryKey.GetValueNames();
				}
			}
			finally
			{
				if (flag)
				{
					MonitoringServerManager.registryMutex.ReleaseMutex();
				}
			}
			return valueNames;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000CB670 File Offset: 0x000C9870
		internal static bool IsSameServer(string owner, string p)
		{
			if (owner == null && p == null)
			{
				return true;
			}
			if (owner != null ^ p != null)
			{
				return false;
			}
			string[] array = owner.Split(new char[]
			{
				'.'
			});
			string[] array2 = p.Split(new char[]
			{
				'.'
			});
			return string.Compare(array[0], array2[0], true) == 0;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000CB6D0 File Offset: 0x000C98D0
		private static int ConstructStatusCode(DatabaseMonitoringStatus status)
		{
			int num = (int)(DateTime.UtcNow - MonitoringServerManager.StartTime).TotalMinutes;
			return num << 4 | (int)status;
		}

		// Token: 0x04001840 RID: 6208
		private const string ObserverSelectionName = "ObserverSelection";

		// Token: 0x04001841 RID: 6209
		private const string HeartbeatName = "Heartbeat";

		// Token: 0x04001842 RID: 6210
		private const string FirstHeartbeatName = "FirstHeartbeat";

		// Token: 0x04001843 RID: 6211
		private const string OwnerName = "Owner";

		// Token: 0x04001844 RID: 6212
		private const string MutexName = "MSExchangeHMMonitoringServerManager";

		// Token: 0x04001845 RID: 6213
		internal static readonly DateTime StartTime = new DateTime(2012, 7, 12, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04001846 RID: 6214
		internal static readonly int MaxObservers = Settings.MaxObservers;

		// Token: 0x04001847 RID: 6215
		internal static readonly int MaxSubjects = Settings.MaxSubjects;

		// Token: 0x04001848 RID: 6216
		internal static readonly int MaxRequestObservers = Settings.MaxRequestObservers;

		// Token: 0x04001849 RID: 6217
		private static readonly string RegistryPathBase = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";

		// Token: 0x0400184A RID: 6218
		private static readonly string MdbListRegistry = MonitoringServerManager.RegistryPathBase + "MdbList";

		// Token: 0x0400184B RID: 6219
		private static readonly string ObserverList = MonitoringServerManager.RegistryPathBase + "Observers\\";

		// Token: 0x0400184C RID: 6220
		private static readonly string SubjectList = MonitoringServerManager.RegistryPathBase + "Subjects\\";

		// Token: 0x0400184D RID: 6221
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x0400184E RID: 6222
		private static Mutex registryMutex = new Mutex(false, "MSExchangeHMMonitoringServerManager");
	}
}
