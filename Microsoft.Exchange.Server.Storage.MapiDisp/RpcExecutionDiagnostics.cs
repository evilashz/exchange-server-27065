using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.HA;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000004 RID: 4
	public class RpcExecutionDiagnostics : ExecutionDiagnostics
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0002948A File Offset: 0x0002768A
		public static int AverageRpcLatencyInMilliseconds
		{
			get
			{
				return RpcExecutionDiagnostics.rpcLatencyAverage.Average;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00029496 File Offset: 0x00027696
		public TimeSpan RpcLatency
		{
			get
			{
				return this.rpcLatency;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0002949E File Offset: 0x0002769E
		protected int RpcExecutionCookie
		{
			get
			{
				return this.rpcExecutionCookie;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000294A6 File Offset: 0x000276A6
		public override uint TypeIdentifier
		{
			get
			{
				return 2U;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000294A9 File Offset: 0x000276A9
		public ThrottlingData ReplicationThrottlingData
		{
			get
			{
				return this.replicationThrottlingData;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000294B1 File Offset: 0x000276B1
		public static void ResetLatencyStatistics()
		{
			RpcExecutionDiagnostics.rpcLatencyAverage.Reset();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000294C0 File Offset: 0x000276C0
		internal PerRpcStatisticsAuxiliaryBlock CreateRpcStatisticsAuxiliaryBlock(StorePerDatabasePerformanceCountersInstance perfInstancePerDB)
		{
			DatabaseConnectionStatistics databaseCollector = base.RpcStatistics.DatabaseCollector;
			return new PerRpcStatisticsAuxiliaryBlock((uint)base.RpcStatistics.ElapsedTime.TotalMilliseconds, (uint)(base.RpcStatistics.CpuKernelTime.TotalMilliseconds + base.RpcStatistics.CpuUserTime.TotalMilliseconds), (uint)databaseCollector.ThreadStats.cPageRead, (uint)databaseCollector.ThreadStats.cPagePreread, (uint)databaseCollector.ThreadStats.cLogRecord, (uint)databaseCollector.ThreadStats.cbLogRecord, 0UL, 0UL, (uint)RpcExecutionDiagnostics.AverageRpcLatencyInMilliseconds, (uint)RpcExecutionDiagnostics.AverageRpcLatencyInMilliseconds, 0U, (perfInstancePerDB != null) ? ((uint)perfInstancePerDB.RateOfROPs.RawValue) : 1U, 0U, 0U, 0U, (uint)RpcExecutionDiagnostics.currentProcessId, this.GetDataProtectionHealth(), this.GetDataAvailabilityHealth(), Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.GetCurrentServerCPUUsagePercentage());
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00029580 File Offset: 0x00027780
		internal override void EnablePerClientTypePerfCounterUpdate()
		{
			base.EnablePerClientTypePerfCounterUpdate();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00029588 File Offset: 0x00027788
		internal override void DisablePerClientTypePerfCounterUpdate()
		{
			if (base.PerClientPerfInstance != null)
			{
				base.PerClientPerfInstance.DirectoryAccessSearchRate.IncrementBy((long)base.RpcStatistics.DirectoryCount);
			}
			base.DisablePerClientTypePerfCounterUpdate();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000295B5 File Offset: 0x000277B5
		protected void OnRpcBegin()
		{
			this.rpcStartTimeStamp = StopwatchStamp.GetStamp();
			this.rpcExecutionCookie = Interlocked.Increment(ref RpcExecutionDiagnostics.lastRpcExecutionCookieCookie);
			base.OnBeginRpc();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000295D8 File Offset: 0x000277D8
		protected void OnRpcEnd()
		{
			ResourceDigestStats activity = new ResourceDigestStats
			{
				TimeInCPU = base.RpcStatistics.CpuKernelTime + base.RpcStatistics.CpuUserTime
			};
			base.ActivityCollector.LogActivity(activity);
			this.rpcLatency = this.rpcStartTimeStamp.ElapsedTime;
			RpcExecutionDiagnostics.rpcLatencyAverage.AddValue((int)base.RpcStatistics.ElapsedTime.TotalMilliseconds);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0002964C File Offset: 0x0002784C
		private uint GetDataProtectionHealth()
		{
			return (uint)this.ReplicationThrottlingData.DataProtectionHealth;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0002965A File Offset: 0x0002785A
		private uint GetDataAvailabilityHealth()
		{
			return (uint)this.ReplicationThrottlingData.DataAvailabilityHealth;
		}

		// Token: 0x040000FC RID: 252
		private static int currentProcessId = Process.GetCurrentProcess().Id;

		// Token: 0x040000FD RID: 253
		private static int lastRpcExecutionCookieCookie = 0;

		// Token: 0x040000FE RID: 254
		private static MovingAverage rpcLatencyAverage = new MovingAverage();

		// Token: 0x040000FF RID: 255
		private StopwatchStamp rpcStartTimeStamp;

		// Token: 0x04000100 RID: 256
		private TimeSpan rpcLatency;

		// Token: 0x04000101 RID: 257
		private int rpcExecutionCookie;

		// Token: 0x04000102 RID: 258
		private ThrottlingData replicationThrottlingData = new ThrottlingData();
	}
}
