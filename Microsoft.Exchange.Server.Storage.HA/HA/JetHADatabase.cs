using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.EseRepl;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.BlockMode;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PhysicalAccessJet;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200001A RID: 26
	[CLSCompliant(false)]
	public class JetHADatabase : JetDatabase
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000060B7 File Offset: 0x000042B7
		internal JetHADatabase(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions) : base(dbGuid, displayName, logPath, filePath, fileName, databaseFlags, databaseOptions)
		{
			if (JetHADatabase.IsBlockModeEnabled())
			{
				this.collector = new BlockModeCollector(dbGuid, displayName);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000060DE File Offset: 0x000042DE
		[CLSCompliant(false)]
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.JetHADatabaseTracer;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000060E8 File Offset: 0x000042E8
		public ThrottlingData ThrottlingData
		{
			get
			{
				ThrottlingData throttlingData = null;
				if (this.collector != null)
				{
					throttlingData = this.collector.ThrottlingData;
				}
				if (throttlingData == null)
				{
					throttlingData = this.unhealthyThrottlingData;
					if (throttlingData == null)
					{
						throttlingData = new ThrottlingData();
						throttlingData.MarkFailed();
						this.unhealthyThrottlingData = throttlingData;
					}
				}
				return throttlingData;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000612C File Offset: 0x0000432C
		internal LastLogWriter LastLogWriter
		{
			get
			{
				return this.lastLogWriter;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006134 File Offset: 0x00004334
		[CLSCompliant(false)]
		public static Database JetHADatabaseCreator(Guid dbGuid, string displayName, string logPath, string filePath, string fileName, DatabaseFlags databaseFlags, DatabaseOptions databaseOptions)
		{
			return new JetHADatabase(dbGuid, displayName, logPath, filePath, fileName, databaseFlags, databaseOptions);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006145 File Offset: 0x00004345
		[CLSCompliant(false)]
		public static Factory.JetHADatabaseCreator GetCreator()
		{
			return new Factory.JetHADatabaseCreator(JetHADatabase.JetHADatabaseCreator);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006154 File Offset: 0x00004354
		public static bool IsBlockModeEnabled()
		{
			IRegistryReader instance = RegistryReader.Instance;
			return instance.GetValue<int>(Registry.LocalMachine, "Software\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "DisableGranularReplication", 0) == 0;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006184 File Offset: 0x00004384
		[CLSCompliant(false)]
		public ErrorCode StartBlockModeReplicationToPassive(IExecutionDiagnostics context, string passiveName, uint firstGenToSend)
		{
			Exception ex = null;
			try
			{
				if (this.collector == null)
				{
					throw new GranularReplicationInitFailedException("BlockMode is disabled");
				}
				this.collector.StartReplicationToPassive(passiveName, firstGenToSend);
			}
			catch (NetworkTransportException ex2)
			{
				ex = ex2;
				context.OnExceptionCatch(ex);
			}
			catch (NetworkRemoteException ex3)
			{
				ex = ex3;
				context.OnExceptionCatch(ex);
			}
			catch (GranularReplicationInitFailedException ex4)
			{
				ex = ex4;
				context.OnExceptionCatch(ex);
			}
			if (ex != null)
			{
				context.OnExceptionCatch(ex);
				JetHADatabase.Tracer.TraceError<string, string, Exception>((long)this.GetHashCode(), "StartBlockModeReplicationToPassive({0}\\{1}) caught {2}", base.DisplayName, passiveName, ex);
				ReplayCrimsonEvents.ActiveFailedToEnterBlockMode.Log<string, string, string, string>(base.DisplayName, passiveName, ex.Message, ex.ToString());
				return ErrorCode.CreateBlockModeInitFailed((LID)58312U);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000625C File Offset: 0x0000445C
		internal override void PublishHaFailure(FailureTag failureTag)
		{
			FailureItem.PublishHaFailure(this.DbGuid, base.DisplayName, failureTag);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006270 File Offset: 0x00004470
		protected override void PrepareToMountAsActive()
		{
			if (this.collector != null)
			{
				this.collector.PrepareToMountAsActive(base.JetInstance);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000628B File Offset: 0x0000448B
		protected override void PrepareToMountAsPassive()
		{
			if (this.collector != null)
			{
				this.collector.PrepareToMountAsPassive(base.JetInstance);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000062A6 File Offset: 0x000044A6
		protected override void PrepareToTransitionToActive()
		{
			base.PrepareToTransitionToActive();
			if (this.collector != null)
			{
				this.collector.PrepareToTransitionToActive();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000062C1 File Offset: 0x000044C1
		protected override void JetInitComplete()
		{
			this.StartLastLogWriter();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000062C9 File Offset: 0x000044C9
		protected override void DismountBegins()
		{
			this.StopLastLogWriter();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000062D1 File Offset: 0x000044D1
		protected override void DismountComplete()
		{
			if (this.collector != null)
			{
				this.collector.DismountComplete();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000062E8 File Offset: 0x000044E8
		private void StartLastLogWriter()
		{
			int num = LastLogWriter.ReadUpdateInterval();
			if (num > 0)
			{
				this.lastLogWriter = new LastLogWriter(this.DbGuid, base.DisplayName, base.JetInstance, base.DatabaseFile);
				this.lastLogWriter.Start(num);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000632E File Offset: 0x0000452E
		private void StopLastLogWriter()
		{
			if (this.lastLogWriter != null)
			{
				this.lastLogWriter.Stop();
				this.lastLogWriter = null;
			}
		}

		// Token: 0x04000076 RID: 118
		private ThrottlingData unhealthyThrottlingData;

		// Token: 0x04000077 RID: 119
		private BlockModeCollector collector;

		// Token: 0x04000078 RID: 120
		private LastLogWriter lastLogWriter;
	}
}
