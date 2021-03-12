using System;
using System.Linq;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200054B RID: 1355
	internal class OverrideEndpoint : IEndpoint
	{
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x000CCC25 File Offset: 0x000CAE25
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x000CCC28 File Offset: 0x000CAE28
		// (set) Token: 0x060021A0 RID: 8608 RVA: 0x000CCC30 File Offset: 0x000CAE30
		public Exception Exception { get; set; }

		// Token: 0x060021A1 RID: 8609 RVA: 0x000CCC39 File Offset: 0x000CAE39
		public void Initialize()
		{
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000CCCB8 File Offset: 0x000CAEB8
		public bool DetectChange()
		{
			if (this.changed)
			{
				return this.changed;
			}
			if (LocalOverrideManager.IsLocalOverridesChanged())
			{
				this.changed = true;
			}
			if (DirectoryAccessor.Instance.IsGlobalOverridesChanged())
			{
				this.changed = true;
			}
			DateTime now = DateTime.UtcNow;
			if (ProbeDefinition.LocalOverrides != null && ProbeDefinition.LocalOverrides.Any((WorkDefinitionOverride o) => o.ExpirationDate < now))
			{
				this.changed = true;
			}
			if (ProbeDefinition.GlobalOverrides != null && ProbeDefinition.GlobalOverrides.Any((WorkDefinitionOverride o) => o.ExpirationDate < now))
			{
				this.changed = true;
			}
			if (MonitorDefinition.LocalOverrides != null && MonitorDefinition.LocalOverrides.Any((WorkDefinitionOverride o) => o.ExpirationDate < now))
			{
				this.changed = true;
			}
			if (MonitorDefinition.GlobalOverrides != null && MonitorDefinition.GlobalOverrides.Any((WorkDefinitionOverride o) => o.ExpirationDate < now))
			{
				this.changed = true;
			}
			if (ResponderDefinition.LocalOverrides != null && ResponderDefinition.LocalOverrides.Any((WorkDefinitionOverride o) => o.ExpirationDate < now))
			{
				this.changed = true;
			}
			if (ResponderDefinition.GlobalOverrides != null && ResponderDefinition.GlobalOverrides.Any((WorkDefinitionOverride o) => o.ExpirationDate < now))
			{
				this.changed = true;
			}
			return this.changed;
		}

		// Token: 0x04001888 RID: 6280
		private bool changed;
	}
}
