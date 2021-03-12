using System;
using Microsoft.Exchange.VariantConfiguration.Reflection;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000144 RID: 324
	public sealed class VariantConfigurationSettingOverride : VariantConfigurationOverride
	{
		// Token: 0x06000EFD RID: 3837 RVA: 0x0002533F File Offset: 0x0002353F
		public VariantConfigurationSettingOverride(string componentName, string sectionName, params string[] parameters) : base(componentName, sectionName, parameters)
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002534A File Offset: 0x0002354A
		public VariantConfigurationSettingOverride(VariantConfigurationSection section, params string[] parameters) : this(section.Name, section.SectionName, parameters)
		{
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00025361 File Offset: 0x00023561
		public override string FileName
		{
			get
			{
				return base.ComponentName + ".settings.ini";
			}
		}
	}
}
