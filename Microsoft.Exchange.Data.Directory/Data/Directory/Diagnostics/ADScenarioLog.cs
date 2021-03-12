using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Compliance;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000B5 RID: 181
	internal sealed class ADScenarioLog : BaseDirectoryProtocolLog
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002B891 File Offset: 0x00029A91
		protected override LogSchema Schema
		{
			get
			{
				return ADScenarioLog.schema;
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002B898 File Offset: 0x00029A98
		internal static T InvokeGetObjectAPIAndLog<T>(DateTime whenUTC, string name, Guid activityId, string implementation, string caller, Func<T> action, Func<string> getDcFunc) where T : ADRawEntry
		{
			T t = default(T);
			Exception ex = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				t = action();
			}
			catch (Exception ex2)
			{
				ex = ex2;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				try
				{
					string server;
					if (t != null && t.IsCached)
					{
						server = string.Empty;
					}
					else
					{
						server = getDcFunc();
					}
					ADScenarioLog.BeginAppend(whenUTC, name, implementation, stopwatch.ElapsedMilliseconds, activityId, caller, (ex == null) ? "" : ex.ToString(), server);
				}
				catch (Exception arg)
				{
					ExTraceGlobals.GeneralTracer.TraceError<Exception>((long)default(Guid).GetHashCode(), "Failed to create API logging with exception {0}", arg);
				}
			}
			return t;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002B978 File Offset: 0x00029B78
		internal static T InvokeWithAPILog<T>(DateTime whenUTC, string name, Guid activityId, string implementation, string caller, Func<T> action, Func<string> getDcFunc)
		{
			T result = default(T);
			Exception ex = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				result = action();
			}
			catch (Exception ex2)
			{
				ex = ex2;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				try
				{
					ADScenarioLog.BeginAppend(whenUTC, name, implementation, stopwatch.ElapsedMilliseconds, activityId, caller, (ex == null) ? "" : ex.ToString(), getDcFunc());
				}
				catch (Exception arg)
				{
					ExTraceGlobals.GeneralTracer.TraceError<Exception>((long)default(Guid).GetHashCode(), "Failed to create API logging with exception {0}", arg);
				}
			}
			return result;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002BA30 File Offset: 0x00029C30
		private void AppendInstance(DateTime whenUTC, string name, string implementor, long processingTime, Guid activityId, string callerInfo, string error, string server)
		{
			if (!base.Initialized)
			{
				this.Initialize();
			}
			if (BaseDirectoryProtocolLog.LoggingEnabled && !this.protocolLogDisabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(ADScenarioLog.schema);
				logRowFormatter[1] = ADScenarioLog.instance.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessName;
				logRowFormatter[4] = Globals.ProcessAppName;
				logRowFormatter[3] = Globals.ProcessId;
				logRowFormatter[5] = name;
				logRowFormatter[6] = implementor;
				logRowFormatter[7] = processingTime;
				logRowFormatter[8] = callerInfo;
				logRowFormatter[9] = error;
				logRowFormatter[10] = server;
				base.Logger.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002BAF4 File Offset: 0x00029CF4
		internal static void BeginAppend(DateTime whenUTC, string name, string implementor, long processingTime, Guid activityId, string callerInfo, string error = null, string server = null)
		{
			if (ADScenarioLog.instance == null)
			{
				ADScenarioLog value = new ADScenarioLog();
				Interlocked.CompareExchange<ADScenarioLog>(ref ADScenarioLog.instance, value, null);
			}
			ADScenarioLog.AppendDelegate appendDelegate = new ADScenarioLog.AppendDelegate(ADScenarioLog.instance.AppendInstance);
			appendDelegate.BeginInvoke(whenUTC, name, implementor, processingTime, activityId, callerInfo, error, server, null, null);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002BB44 File Offset: 0x00029D44
		private void Initialize()
		{
			lock (this.logLock)
			{
				if (!base.Initialized)
				{
					base.Initialize(ExDateTime.UtcNow, Path.Combine(BaseDirectoryProtocolLog.GetExchangeInstallPath(), "Logging\\ADScenario\\"), BaseDirectoryProtocolLog.DefaultMaxRetentionPeriod, BaseDirectoryProtocolLog.DefaultDirectorySizeQuota, BaseDirectoryProtocolLog.DefaultPerFileSizeQuota, true, "ADScenarioLogs");
				}
			}
			this.protocolLogDisabled = true;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002BBBC File Offset: 0x00029DBC
		protected override void UpdateConfigIfChanged(object state)
		{
			base.UpdateConfigIfChanged(state);
			this.ReadConfigData();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002BBCC File Offset: 0x00029DCC
		private void ReadConfigData()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				this.protocolLogDisabled = BaseDirectoryProtocolLog.GetRegistryBool(registryKey, "ADScenarioLogDisabled", true);
			}
		}

		// Token: 0x04000353 RID: 851
		private const string ADScenarioLogDisabled = "ADScenarioLogDisabled";

		// Token: 0x04000354 RID: 852
		private const string LogTypeName = "ADScenario Logs";

		// Token: 0x04000355 RID: 853
		private const string LogComponent = "ADScenarioLogs";

		// Token: 0x04000356 RID: 854
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\ADScenario\\";

		// Token: 0x04000357 RID: 855
		private static ADScenarioLog instance = null;

		// Token: 0x04000358 RID: 856
		internal static readonly BaseDirectoryProtocolLog.FieldInfo[] Fields = new BaseDirectoryProtocolLog.FieldInfo[]
		{
			new BaseDirectoryProtocolLog.FieldInfo(0, "date-time"),
			new BaseDirectoryProtocolLog.FieldInfo(1, "seq-number"),
			new BaseDirectoryProtocolLog.FieldInfo(2, "process-name"),
			new BaseDirectoryProtocolLog.FieldInfo(3, "process-id"),
			new BaseDirectoryProtocolLog.FieldInfo(4, "application-name"),
			new BaseDirectoryProtocolLog.FieldInfo(5, "api-name"),
			new BaseDirectoryProtocolLog.FieldInfo(6, "implementor"),
			new BaseDirectoryProtocolLog.FieldInfo(7, "processing-time"),
			new BaseDirectoryProtocolLog.FieldInfo(8, "caller-info"),
			new BaseDirectoryProtocolLog.FieldInfo(9, "error"),
			new BaseDirectoryProtocolLog.FieldInfo(10, "server")
		};

		// Token: 0x04000359 RID: 857
		private static readonly LogSchema schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "ADScenario Logs", BaseDirectoryProtocolLog.GetColumnArray(ADScenarioLog.Fields));

		// Token: 0x0400035A RID: 858
		private object logLock = new object();

		// Token: 0x0400035B RID: 859
		private bool protocolLogDisabled;

		// Token: 0x020000B6 RID: 182
		private enum Field : byte
		{
			// Token: 0x0400035D RID: 861
			DateTime,
			// Token: 0x0400035E RID: 862
			SequenceNumber,
			// Token: 0x0400035F RID: 863
			ClientName,
			// Token: 0x04000360 RID: 864
			Pid,
			// Token: 0x04000361 RID: 865
			AppName,
			// Token: 0x04000362 RID: 866
			ApiName,
			// Token: 0x04000363 RID: 867
			Implementor,
			// Token: 0x04000364 RID: 868
			ProcessingTime,
			// Token: 0x04000365 RID: 869
			CallerInfo,
			// Token: 0x04000366 RID: 870
			Error,
			// Token: 0x04000367 RID: 871
			Server
		}

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x060009BD RID: 2493
		internal delegate void AppendDelegate(DateTime whenUTC, string name, string implementor, long processingTime, Guid activityId, string callerInfo, string error, string server);
	}
}
