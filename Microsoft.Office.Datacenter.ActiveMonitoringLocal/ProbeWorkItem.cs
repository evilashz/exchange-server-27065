using System;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000025 RID: 37
	public abstract class ProbeWorkItem : WorkItem
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000BF10 File Offset: 0x0000A110
		public new ProbeDefinition Definition
		{
			get
			{
				return (ProbeDefinition)base.Definition;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public new ProbeResult Result
		{
			get
			{
				return (ProbeResult)base.Result;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000BF2A File Offset: 0x0000A12A
		protected new IProbeWorkBroker Broker
		{
			get
			{
				return (IProbeWorkBroker)base.Broker;
			}
		}
	}
}
