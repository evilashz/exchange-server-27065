using System;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000D7 RID: 215
	internal class WorkingSetAgentPerfLogging : IPerformanceDataLogger
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0002428A File Offset: 0x0002248A
		public TimeSpan StopwatchTime
		{
			get
			{
				return this.stopwatchTime;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00024292 File Offset: 0x00022492
		public TimeSpan CpuTime
		{
			get
			{
				return this.cpuTime;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0002429A File Offset: 0x0002249A
		public uint StoreRPCs
		{
			get
			{
				return this.storeRPCs;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000242A2 File Offset: 0x000224A2
		public void Log(string marker, string counter, TimeSpan dataPoint)
		{
			if (counter.Equals("ElapsedTime"))
			{
				this.stopwatchTime = dataPoint;
				return;
			}
			if (counter.Equals("CpuTime"))
			{
				this.cpuTime = dataPoint;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000242CD File Offset: 0x000224CD
		public void Log(string marker, string counter, uint dataPoint)
		{
			if (counter.Equals("StoreRpcCount"))
			{
				this.storeRPCs = dataPoint;
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000242E3 File Offset: 0x000224E3
		public void Log(string marker, string counter, string dataPoint)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000386 RID: 902
		private TimeSpan stopwatchTime;

		// Token: 0x04000387 RID: 903
		private TimeSpan cpuTime;

		// Token: 0x04000388 RID: 904
		private uint storeRPCs;
	}
}
