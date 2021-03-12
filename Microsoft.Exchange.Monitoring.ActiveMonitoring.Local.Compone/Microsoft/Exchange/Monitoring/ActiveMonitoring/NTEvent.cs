using System;
using System.Xml;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000089 RID: 137
	internal class NTEvent : DiscoveryContext
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001E27B File Offset: 0x0001C47B
		public NTEvent(XmlNode node, TracingContext traceContext) : this(node, traceContext, null)
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001E286 File Offset: 0x0001C486
		public NTEvent(XmlNode node, TracingContext traceContext, MaintenanceResult result) : base(node, traceContext, result)
		{
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001E291 File Offset: 0x0001C491
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0001E299 File Offset: 0x0001C499
		internal bool IsInstrumented { get; set; }

		// Token: 0x06000503 RID: 1283 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		internal override void ProcessDefinitions(IMaintenanceWorkBroker broker)
		{
			if (!base.CheckEnvironment())
			{
				return;
			}
			if (base.GetDescendantCount("Probe") == 0)
			{
				this.IsInstrumented = true;
			}
			else
			{
				this.IsInstrumented = false;
				base.ProcessProbeDefinition();
			}
			base.ProcessMonitorDefinitions();
			base.ProcessResponderDefinitions();
			base.AddDefinitions(broker);
		}
	}
}
