using System;
using System.Xml;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008F RID: 143
	internal class PerfCounter : DiscoveryContext
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		public PerfCounter(XmlNode node, TracingContext traceContext) : this(node, traceContext, null)
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001E4C3 File Offset: 0x0001C6C3
		public PerfCounter(XmlNode node, TracingContext traceContext, MaintenanceResult result) : base(node, traceContext, result)
		{
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001E4CE File Offset: 0x0001C6CE
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0001E4D6 File Offset: 0x0001C6D6
		internal string Object { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001E4DF File Offset: 0x0001C6DF
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0001E4E7 File Offset: 0x0001C6E7
		internal string Counter { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		internal string Instance { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001E501 File Offset: 0x0001C701
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001E509 File Offset: 0x0001C709
		internal string PerfCounterName { get; set; }

		// Token: 0x06000518 RID: 1304 RVA: 0x0001E514 File Offset: 0x0001C714
		internal override void ProcessDefinitions(IMaintenanceWorkBroker broker)
		{
			if (!base.CheckEnvironment())
			{
				return;
			}
			this.Object = DefinitionHelperBase.GetMandatoryXmlAttribute<string>(base.ContextNode, "Object", base.TraceContext);
			this.Counter = DefinitionHelperBase.GetMandatoryXmlAttribute<string>(base.ContextNode, "Counter", base.TraceContext);
			this.Instance = DefinitionHelperBase.GetMandatoryXmlAttribute<string>(base.ContextNode, "Instance", base.TraceContext);
			if (string.IsNullOrEmpty(this.Counter))
			{
				throw new XmlException("The Counter attribute of the Counter node cannot be null or an empty string.");
			}
			if (string.IsNullOrEmpty(this.Object))
			{
				throw new XmlException("The Object attribute of the Counter node cannot be null or an empty string.");
			}
			if (string.IsNullOrEmpty(this.Instance) || this.Instance.Equals("*"))
			{
				this.PerfCounterName = string.Format("{0}\\{1}", this.Object, this.Counter);
			}
			else
			{
				this.PerfCounterName = string.Format("{0}\\{1}\\{2}", this.Object, this.Counter, this.Instance);
			}
			base.ProcessMonitorDefinitions();
			base.ProcessResponderDefinitions();
			base.AddDefinitions(broker);
		}
	}
}
