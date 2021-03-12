using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000127 RID: 295
	public sealed class VariantConfigurationWorkingSetComponent : VariantConfigurationComponent
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x000215C7 File Offset: 0x0001F7C7
		internal VariantConfigurationWorkingSetComponent() : base("WorkingSet")
		{
			base.Add(new VariantConfigurationSection("WorkingSet.settings.ini", "WorkingSetAgent", typeof(IFeature), false));
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000215F4 File Offset: 0x0001F7F4
		public VariantConfigurationSection WorkingSetAgent
		{
			get
			{
				return base["WorkingSetAgent"];
			}
		}
	}
}
