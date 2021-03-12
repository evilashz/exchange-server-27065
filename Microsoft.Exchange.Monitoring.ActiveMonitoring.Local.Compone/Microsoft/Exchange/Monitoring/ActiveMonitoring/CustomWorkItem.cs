using System;
using System.Xml;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000080 RID: 128
	internal class CustomWorkItem : DiscoveryContext
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0001D4AA File Offset: 0x0001B6AA
		public CustomWorkItem(XmlNode node, TracingContext traceContext) : this(node, traceContext, null)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001D4B5 File Offset: 0x0001B6B5
		public CustomWorkItem(XmlNode node, TracingContext traceContext, MaintenanceResult result) : base(node, traceContext, result)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		internal override void ProcessDefinitions(IMaintenanceWorkBroker broker)
		{
			if (!base.CheckEnvironment())
			{
				return;
			}
			base.ProcessProbeDefinition();
			base.ProcessMonitorDefinitions();
			base.ProcessResponderDefinitions();
			base.AddDefinitions(broker);
		}
	}
}
