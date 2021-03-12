using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200001B RID: 27
	internal class LastLogWriter
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x0000634A File Offset: 0x0000454A
		public LastLogWriter(Guid dbGuid, string dbName, JET_INSTANCE jetInstance, string dbFileName)
		{
			this.dbGuid = dbGuid;
			this.dbName = dbName;
			this.jetInstance = jetInstance;
			this.dbFileName = dbFileName;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000637A File Offset: 0x0000457A
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LastLogWriterTracer;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006384 File Offset: 0x00004584
		public static int ReadUpdateInterval()
		{
			IRegistryReader instance = RegistryReader.Instance;
			return instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Last Log Committed Update Interval in Seconds", 300);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000063B4 File Offset: 0x000045B4
		public void Start(int updateIntervalInSec)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)updateIntervalInSec);
			this.timer = new Timer(new TimerCallback(this.Callback), null, timeSpan, timeSpan);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000063E4 File Offset: 0x000045E4
		public void Stop()
		{
			using (LockManager.Lock(this.workerLock))
			{
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006438 File Offset: 0x00004638
		internal long GetLastLog()
		{
			long result;
			using (Session session = new Session(this.jetInstance))
			{
				JET_DBID nil = JET_DBID.Nil;
				try
				{
					Api.JetOpenDatabase(session, this.dbFileName, null, out nil, OpenDatabaseGrbit.None);
					JET_DBINFOMISC jet_DBINFOMISC;
					Api.JetGetDatabaseInfo(session, nil, out jet_DBINFOMISC, JET_DbInfo.Misc);
					long num = (long)(jet_DBINFOMISC.genCommitted - 1);
					LastLogWriter.Tracer.TraceDebug<string, long>((long)this.GetHashCode(), "Last Log for db '{0}' is {1}", this.dbName, num);
					result = num;
				}
				finally
				{
					if (nil != JET_DBID.Nil)
					{
						Api.JetCloseDatabase(session, nil, CloseDatabaseGrbit.None);
					}
				}
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000064EC File Offset: 0x000046EC
		private void Callback(object ignored)
		{
			bool flag = false;
			try
			{
				IClusterWriter instance = ClusterWriter.Instance;
				long lastLogGen = 0L;
				using (LockManager.Lock(this.workerLock))
				{
					if (this.timer == null)
					{
						LastLogWriter.Tracer.TraceError((long)this.GetHashCode(), "Callback scheduled after stop.");
						return;
					}
					if (this.workerIsBusy)
					{
						LastLogWriter.Tracer.TraceError<string>((long)this.GetHashCode(), "Callback detected hung writer for db '{0}'", this.dbName);
						Globals.LogPeriodicEvent(this.dbName, MSExchangeISEventLogConstants.Tuple_LastLogWriterHung, new object[]
						{
							this.dbName
						});
						return;
					}
					if (!instance.IsClusterRunning())
					{
						return;
					}
					lastLogGen = this.GetLastLog();
					flag = true;
					this.workerIsBusy = true;
				}
				this.UpdateLastLog(instance, lastLogGen);
			}
			finally
			{
				if (flag)
				{
					using (LockManager.Lock(this.workerLock))
					{
						this.workerIsBusy = false;
					}
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006604 File Offset: 0x00004804
		private void UpdateLastLog(IClusterWriter writer, long lastLogGen)
		{
			Exception ex = writer.TryWriteLastLog(this.dbGuid, lastLogGen);
			if (ex != null)
			{
				LastLogWriter.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "TryWriteLastLog on db '{0}' failed: {1}", this.dbName, ex);
				Globals.LogPeriodicEvent(this.dbName, MSExchangeISEventLogConstants.Tuple_LastLogUpdateFailed, new object[]
				{
					this.dbName,
					ex.ToString()
				});
			}
		}

		// Token: 0x04000079 RID: 121
		private readonly Guid dbGuid;

		// Token: 0x0400007A RID: 122
		private readonly string dbName;

		// Token: 0x0400007B RID: 123
		private readonly string dbFileName;

		// Token: 0x0400007C RID: 124
		private JET_INSTANCE jetInstance;

		// Token: 0x0400007D RID: 125
		private Timer timer;

		// Token: 0x0400007E RID: 126
		private object workerLock = new object();

		// Token: 0x0400007F RID: 127
		private bool workerIsBusy;
	}
}
