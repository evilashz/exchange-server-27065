using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000108 RID: 264
	public sealed class VariantConfigurationHighAvailabilityComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
		internal VariantConfigurationHighAvailabilityComponent() : base("HighAvailability")
		{
			base.Add(new VariantConfigurationSection("HighAvailability.settings.ini", "ActiveManager", typeof(IActiveManagerSettings), false));
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001CC05 File Offset: 0x0001AE05
		public VariantConfigurationSection ActiveManager
		{
			get
			{
				return base["ActiveManager"];
			}
		}
	}
}
