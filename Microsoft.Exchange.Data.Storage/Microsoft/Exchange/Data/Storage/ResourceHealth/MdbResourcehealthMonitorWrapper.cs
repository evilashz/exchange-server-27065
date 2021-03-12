using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.ResourceHealth
{
	// Token: 0x02000B2F RID: 2863
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MdbResourcehealthMonitorWrapper : ResourceHealthMonitorWrapper, IRPCLatencyProvider, IResourceLoadMonitor
	{
		// Token: 0x0600679E RID: 26526 RVA: 0x001B5C5B File Offset: 0x001B3E5B
		public MdbResourcehealthMonitorWrapper(MdbResourceHealthMonitor monitor) : base(monitor)
		{
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x001B5C64 File Offset: 0x001B3E64
		public void Update(int averageRpcLatency, uint totalDbOperations)
		{
			base.GetWrappedMonitor<MdbResourceHealthMonitor>().Update(averageRpcLatency, totalDbOperations);
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x060067A0 RID: 26528 RVA: 0x001B5C73 File Offset: 0x001B3E73
		public int LastRPCLatencyValue
		{
			get
			{
				return base.GetWrappedMonitor<MdbResourceHealthMonitor>().LastRPCLatencyValue;
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x060067A1 RID: 26529 RVA: 0x001B5C80 File Offset: 0x001B3E80
		public int AverageRPCLatencyValue
		{
			get
			{
				return base.GetWrappedMonitor<MdbResourceHealthMonitor>().AverageRPCLatencyValue;
			}
		}
	}
}
