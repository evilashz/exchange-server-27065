using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000022 RID: 34
	public class JobConfigurationElement : ConfigurationElement
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00005E5C File Offset: 0x0000405C
		static JobConfigurationElement()
		{
			JobConfigurationElement.enabled = new ConfigurationProperty("Enabled", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);
			JobConfigurationElement.className = new ConfigurationProperty("Class", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
			JobConfigurationElement.role = new ConfigurationProperty("Role", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
			JobConfigurationElement.datacenter = new ConfigurationProperty("Datacenter", typeof(bool), false, ConfigurationPropertyOptions.None);
			JobConfigurationElement.outputStream = new ConfigurationProperty("OutputStream", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
			JobConfigurationElement.analyzers = new ConfigurationProperty("Analyzers", typeof(AnalyzerConfigurationCollection), null, ConfigurationPropertyOptions.None);
			JobConfigurationElement.properties = new ConfigurationPropertyCollection();
			JobConfigurationElement.properties.Add(JobConfigurationElement.name);
			JobConfigurationElement.properties.Add(JobConfigurationElement.assembly);
			JobConfigurationElement.properties.Add(JobConfigurationElement.method);
			JobConfigurationElement.properties.Add(JobConfigurationElement.enabled);
			JobConfigurationElement.properties.Add(JobConfigurationElement.className);
			JobConfigurationElement.properties.Add(JobConfigurationElement.role);
			JobConfigurationElement.properties.Add(JobConfigurationElement.datacenter);
			JobConfigurationElement.properties.Add(JobConfigurationElement.outputStream);
			JobConfigurationElement.properties.Add(JobConfigurationElement.analyzers);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005FF7 File Offset: 0x000041F7
		public string Name
		{
			get
			{
				return (string)base[JobConfigurationElement.name];
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00006009 File Offset: 0x00004209
		public string Assembly
		{
			get
			{
				return (string)base[JobConfigurationElement.assembly];
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000601B File Offset: 0x0000421B
		public string Method
		{
			get
			{
				return (string)base[JobConfigurationElement.method];
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000602D File Offset: 0x0000422D
		public bool Enabled
		{
			get
			{
				return (bool)base[JobConfigurationElement.enabled];
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000603F File Offset: 0x0000423F
		public string Class
		{
			get
			{
				return (string)base[JobConfigurationElement.className];
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00006051 File Offset: 0x00004251
		public string Role
		{
			get
			{
				return (string)base[JobConfigurationElement.role];
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00006063 File Offset: 0x00004263
		public bool Datacenter
		{
			get
			{
				return (bool)base[JobConfigurationElement.datacenter];
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00006075 File Offset: 0x00004275
		public string OutputStream
		{
			get
			{
				return (string)base[JobConfigurationElement.outputStream];
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006087 File Offset: 0x00004287
		public AnalyzerConfigurationCollection Analyzers
		{
			get
			{
				return (AnalyzerConfigurationCollection)base[JobConfigurationElement.analyzers];
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006099 File Offset: 0x00004299
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return JobConfigurationElement.properties;
			}
		}

		// Token: 0x040002FC RID: 764
		private static ConfigurationProperty name = new ConfigurationProperty("Name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040002FD RID: 765
		private static ConfigurationProperty assembly = new ConfigurationProperty("Assembly", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040002FE RID: 766
		private static ConfigurationProperty enabled;

		// Token: 0x040002FF RID: 767
		private static ConfigurationProperty className;

		// Token: 0x04000300 RID: 768
		private static ConfigurationProperty method = new ConfigurationProperty("Method", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000301 RID: 769
		private static ConfigurationProperty role;

		// Token: 0x04000302 RID: 770
		private static ConfigurationProperty datacenter;

		// Token: 0x04000303 RID: 771
		private static ConfigurationProperty outputStream;

		// Token: 0x04000304 RID: 772
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000305 RID: 773
		private static ConfigurationProperty analyzers;
	}
}
