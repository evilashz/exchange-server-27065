using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000009 RID: 9
	public class MonitorIdentity : WorkItemIdentity.Typed<MonitorDefinition>
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00004E42 File Offset: 0x00003042
		public MonitorIdentity(Component component, string baseName, string targetResource) : base(component, WorkItemIdentity.ToLocalName(baseName, "Monitor"), targetResource)
		{
			this.baseName = baseName;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004E60 File Offset: 0x00003060
		public static implicit operator MonitorIdentity(MonitorDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			Component component = ExchangeComponent.WellKnownComponents[definition.ServiceName];
			return new MonitorIdentity(component, WorkItemIdentity.GetLocalName(component, definition.Name, "Monitor"), definition.TargetResource);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00004EA0 File Offset: 0x000030A0
		public ResponderIdentity CreateResponderIdentity(string responderDisplayType, string targetResource = null)
		{
			return new ResponderIdentity(base.Component, this.baseName, responderDisplayType, targetResource ?? base.TargetResource);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00004EBF File Offset: 0x000030BF
		public override void ApplyTo(MonitorDefinition definition)
		{
			base.ApplyTo(definition);
			definition.Component = base.Component;
		}

		// Token: 0x04000158 RID: 344
		private const string StandardSuffix = "Monitor";

		// Token: 0x04000159 RID: 345
		private readonly string baseName;
	}
}
