using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x0200094B RID: 2379
	[Cmdlet("New", "SettingOverride", SupportsShouldProcess = true)]
	public sealed class NewSettingOverride : NewOverrideBase
	{
		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x060054EC RID: 21740 RVA: 0x0015E909 File Offset: 0x0015CB09
		protected override bool IsFlight
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x0015E90C File Offset: 0x0015CB0C
		// (set) Token: 0x060054EE RID: 21742 RVA: 0x0015E923 File Offset: 0x0015CB23
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Component
		{
			get
			{
				return base.Fields["Component"] as string;
			}
			set
			{
				base.Fields["Component"] = value;
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x0015E936 File Offset: 0x0015CB36
		// (set) Token: 0x060054F0 RID: 21744 RVA: 0x0015E94D File Offset: 0x0015CB4D
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Section
		{
			get
			{
				return base.Fields["Section"] as string;
			}
			set
			{
				base.Fields["Section"] = value;
			}
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0015E960 File Offset: 0x0015CB60
		protected override SettingOverrideXml GetXml()
		{
			SettingOverrideXml xml = base.GetXml();
			xml.ComponentName = this.Component;
			xml.SectionName = this.Section;
			return xml;
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x0015E98D File Offset: 0x0015CB8D
		protected override VariantConfigurationOverride GetOverride()
		{
			return new VariantConfigurationSettingOverride(this.Component, this.Section, base.Parameters.ToArray());
		}
	}
}
