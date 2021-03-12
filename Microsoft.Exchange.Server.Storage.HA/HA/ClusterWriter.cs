using System;
using System.ComponentModel;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000014 RID: 20
	internal class ClusterWriter : IClusterWriter
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000054A9 File Offset: 0x000036A9
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LastLogWriterTracer;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000054B0 File Offset: 0x000036B0
		public static IClusterWriter Instance
		{
			get
			{
				return ClusterWriter.hookableInstance.Value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000054BC File Offset: 0x000036BC
		public static TimeSpan RefreshCheckDuration
		{
			get
			{
				int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "LastLogRefreshCheckDurationInSec", DefaultSettings.Get.LastLogRefreshCheckDurationInSeconds);
				return TimeSpan.FromSeconds((double)value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000054F4 File Offset: 0x000036F4
		public static long LastLogUpdateAdvancingLimit
		{
			get
			{
				return RegistryReader.Instance.GetValue<long>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "LastLogUpdateAdvancingLimit", DefaultSettings.Get.LastLogUpdateAdvancingLimit);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005528 File Offset: 0x00003728
		public static long LastLogUpdateLaggingLimit
		{
			get
			{
				return RegistryReader.Instance.GetValue<long>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "LastLogUpdateLaggingLimit", DefaultSettings.Get.LastLogUpdateLaggingLimit);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000555C File Offset: 0x0000375C
		public bool IsClusterRunning()
		{
			bool result = false;
			using (Context context = Context.CreateForSystem())
			{
				try
				{
					result = AmCluster.IsRunning();
				}
				catch (ClusterException ex)
				{
					ClusterWriter.Tracer.TraceError<ClusterException>(0L, "IsClusterRunning failed: {0}", ex);
					context.OnExceptionCatch(ex);
				}
			}
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000055C0 File Offset: 0x000037C0
		public Exception TryWriteLastLog(Guid dbGuid, long lastLogGen)
		{
			Exception result = null;
			using (Context context = Context.CreateForSystem())
			{
				try
				{
					using (IClusterDB clusterDB = ClusterDB.Open())
					{
						if (clusterDB != null && clusterDB.IsInitialized)
						{
							string text = dbGuid.ToString();
							string text2 = text + "_Time";
							ExDateTime lastUpdateTimeFromClusdbUtc;
							if (this.IsClusterDatabaseUpdateRequired(clusterDB, text, text2, out lastUpdateTimeFromClusdbUtc))
							{
								this.CheckLogGenerationSanity(clusterDB, text, lastLogGen, lastUpdateTimeFromClusdbUtc);
								using (IClusterDBWriteBatch clusterDBWriteBatch = clusterDB.CreateWriteBatch(ClusterWriter.lastLogUpdateKeyName))
								{
									clusterDBWriteBatch.SetValue(text, lastLogGen.ToString());
									clusterDBWriteBatch.SetValue(text2, ExDateTime.UtcNow.ToString("s"));
									clusterDBWriteBatch.Execute();
								}
							}
						}
					}
				}
				catch (Win32Exception ex)
				{
					context.OnExceptionCatch(ex);
					ClusterWriter.Tracer.TraceError<Win32Exception>(0L, "TryWriteLastLog failed: {0}", ex);
					result = ex;
				}
				catch (ClusterException ex2)
				{
					context.OnExceptionCatch(ex2);
					ClusterWriter.Tracer.TraceError<ClusterException>(0L, "TryWriteLastLog failed: {0}", ex2);
					result = ex2;
				}
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000570C File Offset: 0x0000390C
		internal static IDisposable SetTestHook(IClusterWriter testHook)
		{
			return ClusterWriter.hookableInstance.SetTestHook(testHook);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000571C File Offset: 0x0000391C
		private bool IsClusterDatabaseUpdateRequired(IClusterDB iClusterDb, string dbGuidString, string timeStampProperty, out ExDateTime lastUpdateTimeFromClusdbUtc)
		{
			bool result = true;
			lastUpdateTimeFromClusdbUtc = ExDateTime.MinValue.ToUtc();
			string value = iClusterDb.GetValue<string>(ClusterWriter.lastLogUpdateKeyName, timeStampProperty, string.Empty);
			ExDateTime dt;
			if (!string.IsNullOrEmpty(value) && ExDateTime.TryParse(value, out dt) && ClusterWriter.RefreshCheckDuration != TimeSpan.Zero && ExDateTime.UtcNow - dt < ClusterWriter.RefreshCheckDuration)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005790 File Offset: 0x00003990
		private void CheckLogGenerationSanity(IClusterDB iClusterDb, string dbGuidString, long lastLogGenFromEse, ExDateTime lastUpdateTimeFromClusdbUtc)
		{
			string value = iClusterDb.GetValue<string>(ClusterWriter.lastLogUpdateKeyName, dbGuidString, string.Empty);
			long num;
			if (long.TryParse(value, out num))
			{
				if (num > lastLogGenFromEse)
				{
					long num2 = num - lastLogGenFromEse;
					if (num2 > ClusterWriter.LastLogUpdateAdvancingLimit)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.LogPeriodicEvent(dbGuidString, MSExchangeISEventLogConstants.Tuple_LastLogUpdateTooAdvanced, new object[]
						{
							dbGuidString,
							num,
							lastLogGenFromEse,
							lastUpdateTimeFromClusdbUtc
						});
						return;
					}
				}
				else
				{
					long num3 = lastLogGenFromEse - num;
					if (num3 > ClusterWriter.LastLogUpdateLaggingLimit)
					{
						Microsoft.Exchange.Server.Storage.Common.Globals.LogPeriodicEvent(dbGuidString, MSExchangeISEventLogConstants.Tuple_LastLogUpdateLaggingBehind, new object[]
						{
							dbGuidString,
							num,
							lastLogGenFromEse,
							lastUpdateTimeFromClusdbUtc
						});
					}
				}
			}
		}

		// Token: 0x04000069 RID: 105
		private static Hookable<IClusterWriter> hookableInstance = Hookable<IClusterWriter>.Create(false, new ClusterWriter());

		// Token: 0x0400006A RID: 106
		private static string lastLogUpdateKeyName = "ExchangeActiveManager\\LastLog";
	}
}
