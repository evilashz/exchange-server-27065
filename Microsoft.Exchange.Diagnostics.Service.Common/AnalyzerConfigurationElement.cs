using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200001F RID: 31
	public class AnalyzerConfigurationElement : ConfigurationElement
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00005B8C File Offset: 0x00003D8C
		static AnalyzerConfigurationElement()
		{
			AnalyzerConfigurationElement.properties.Add(AnalyzerConfigurationElement.name);
			AnalyzerConfigurationElement.properties.Add(AnalyzerConfigurationElement.role);
			AnalyzerConfigurationElement.properties.Add(AnalyzerConfigurationElement.datacenter);
			AnalyzerConfigurationElement.properties.Add(AnalyzerConfigurationElement.enabled);
			AnalyzerConfigurationElement.properties.Add(AnalyzerConfigurationElement.assembly);
			AnalyzerConfigurationElement.properties.Add(AnalyzerConfigurationElement.outputFormat);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00005CA9 File Offset: 0x00003EA9
		public string Name
		{
			get
			{
				return (string)base[AnalyzerConfigurationElement.name];
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00005CBB File Offset: 0x00003EBB
		public string Role
		{
			get
			{
				return (string)base[AnalyzerConfigurationElement.role];
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00005CCD File Offset: 0x00003ECD
		public bool Datacenter
		{
			get
			{
				return (bool)base[AnalyzerConfigurationElement.datacenter];
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00005CDF File Offset: 0x00003EDF
		public bool Enabled
		{
			get
			{
				return (bool)base[AnalyzerConfigurationElement.enabled];
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00005CF1 File Offset: 0x00003EF1
		public string Assembly
		{
			get
			{
				return (string)base[AnalyzerConfigurationElement.assembly];
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00005D03 File Offset: 0x00003F03
		public string OutputFormat
		{
			get
			{
				return (string)base[AnalyzerConfigurationElement.outputFormat];
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005D15 File Offset: 0x00003F15
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AnalyzerConfigurationElement.properties;
			}
		}

		// Token: 0x040002F1 RID: 753
		private static ConfigurationProperty name = new ConfigurationProperty("Name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040002F2 RID: 754
		private static ConfigurationProperty role = new ConfigurationProperty("Role", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040002F3 RID: 755
		private static ConfigurationProperty datacenter = new ConfigurationProperty("Datacenter", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x040002F4 RID: 756
		private static ConfigurationProperty enabled = new ConfigurationProperty("Enabled", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040002F5 RID: 757
		private static ConfigurationProperty assembly = new ConfigurationProperty("Assembly", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040002F6 RID: 758
		private static ConfigurationProperty outputFormat = new ConfigurationProperty("OutputFormat", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x040002F7 RID: 759
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
