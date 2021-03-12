using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200001C RID: 28
	public class ProbeIdentity : WorkItemIdentity.Typed<ProbeDefinition>
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00009855 File Offset: 0x00007A55
		private ProbeIdentity(Component component, string baseName, string targetResource) : base(component, WorkItemIdentity.ToLocalName(baseName, "Probe"), targetResource)
		{
			this.baseName = baseName;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009874 File Offset: 0x00007A74
		public static implicit operator ProbeIdentity(ProbeDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			Component component = ExchangeComponent.WellKnownComponents[definition.ServiceName];
			return new ProbeIdentity(component, WorkItemIdentity.GetLocalName(component, definition.Name, "Probe"), definition.TargetResource);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000098B4 File Offset: 0x00007AB4
		public static ProbeIdentity Create(Component healthSet, ProbeType probeType, string scenario = null, string targetResource = null)
		{
			string subsetName = healthSet.SubsetName;
			if (((probeType == ProbeType.DeepTest || probeType == ProbeType.SelfTest) && subsetName != "Protocol") || (probeType == ProbeType.ProxyTest && subsetName != "Proxy") || (probeType == ProbeType.Ctp && healthSet.HealthGroup != HealthGroup.CustomerTouchPoints))
			{
				throw new ArgumentException(string.Format("Probes of type {0} cannot be declared on \"{1}\" health subsets {2}", probeType, healthSet.HealthGroup, subsetName), "probeType");
			}
			return new ProbeIdentity(healthSet, string.Format("{0}{1}", scenario, probeType), targetResource);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009939 File Offset: 0x00007B39
		public MonitorIdentity CreateMonitorIdentity()
		{
			return new MonitorIdentity(base.Component, this.baseName, base.TargetResource);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009952 File Offset: 0x00007B52
		public ProbeIdentity ForTargetResource(string targetResource)
		{
			return new ProbeIdentity(base.Component, this.baseName, targetResource);
		}

		// Token: 0x040001F1 RID: 497
		private const string StandardSuffix = "Probe";

		// Token: 0x040001F2 RID: 498
		private readonly string baseName;
	}
}
