using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000024 RID: 36
	public class ServiceConfiguration : ConfigurationSection
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000060FF File Offset: 0x000042FF
		static ServiceConfiguration()
		{
			ServiceConfiguration.properties.Add(ServiceConfiguration.directories);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00006135 File Offset: 0x00004335
		public ServiceConfiguration.DirectoryConfigurationCollection Directories
		{
			get
			{
				return (ServiceConfiguration.DirectoryConfigurationCollection)base[ServiceConfiguration.directories];
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00006147 File Offset: 0x00004347
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServiceConfiguration.properties;
			}
		}

		// Token: 0x04000308 RID: 776
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000309 RID: 777
		private static ConfigurationProperty directories = new ConfigurationProperty("Directories", typeof(ServiceConfiguration.DirectoryConfigurationCollection), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x02000025 RID: 37
		public class DirectoryConfigurationCollection : ConfigurationElementCollection<ServiceConfiguration.DirectoryConfigurationElement>
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00006156 File Offset: 0x00004356
			public override ConfigurationElementCollectionType CollectionType
			{
				get
				{
					return ConfigurationElementCollectionType.AddRemoveClearMap;
				}
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00006159 File Offset: 0x00004359
			protected override string ElementName
			{
				get
				{
					return "name";
				}
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00006160 File Offset: 0x00004360
			protected override object GetElementKey(ConfigurationElement element)
			{
				if (element != null)
				{
					return ((ServiceConfiguration.DirectoryConfigurationElement)element).Name;
				}
				return null;
			}
		}

		// Token: 0x02000026 RID: 38
		public class DirectoryConfigurationElement : ConfigurationElement
		{
			// Token: 0x0600009F RID: 159 RVA: 0x0000617C File Offset: 0x0000437C
			static DirectoryConfigurationElement()
			{
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.name);
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.agent);
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.logDataLoss);
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.maxSize);
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.maxSizeDatacenter);
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.maxAge);
				ServiceConfiguration.DirectoryConfigurationElement.properties.Add(ServiceConfiguration.DirectoryConfigurationElement.checkInterval);
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000A0 RID: 160 RVA: 0x000062D5 File Offset: 0x000044D5
			public string Name
			{
				get
				{
					return (string)base[ServiceConfiguration.DirectoryConfigurationElement.name];
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x000062E7 File Offset: 0x000044E7
			public string Agent
			{
				get
				{
					return (string)base[ServiceConfiguration.DirectoryConfigurationElement.agent];
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x000062F9 File Offset: 0x000044F9
			public int MaxSize
			{
				get
				{
					return (int)base[ServiceConfiguration.DirectoryConfigurationElement.maxSize];
				}
			}

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000630B File Offset: 0x0000450B
			public int MaxSizeDatacenter
			{
				get
				{
					return (int)base[ServiceConfiguration.DirectoryConfigurationElement.maxSizeDatacenter];
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000631D File Offset: 0x0000451D
			public bool LogDataLoss
			{
				get
				{
					return (bool)base[ServiceConfiguration.DirectoryConfigurationElement.logDataLoss];
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000632F File Offset: 0x0000452F
			public TimeSpan MaxAge
			{
				get
				{
					return (TimeSpan)base[ServiceConfiguration.DirectoryConfigurationElement.maxAge];
				}
			}

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x060000A6 RID: 166 RVA: 0x00006341 File Offset: 0x00004541
			public TimeSpan CheckInterval
			{
				get
				{
					return (TimeSpan)base[ServiceConfiguration.DirectoryConfigurationElement.checkInterval];
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x00006353 File Offset: 0x00004553
			protected override ConfigurationPropertyCollection Properties
			{
				get
				{
					return ServiceConfiguration.DirectoryConfigurationElement.properties;
				}
			}

			// Token: 0x0400030A RID: 778
			private static ConfigurationProperty name = new ConfigurationProperty("Name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

			// Token: 0x0400030B RID: 779
			private static ConfigurationProperty agent = new ConfigurationProperty("Agent", typeof(string), null, ConfigurationPropertyOptions.None);

			// Token: 0x0400030C RID: 780
			private static ConfigurationProperty logDataLoss = new ConfigurationProperty("LogDataLoss", typeof(bool), null, ConfigurationPropertyOptions.IsRequired);

			// Token: 0x0400030D RID: 781
			private static ConfigurationProperty maxSize = new ConfigurationProperty("MaxSize", typeof(int), 0, ConfigurationPropertyOptions.IsRequired);

			// Token: 0x0400030E RID: 782
			private static ConfigurationProperty maxSizeDatacenter = new ConfigurationProperty("MaxSizeDatacenter", typeof(int), 0, ConfigurationPropertyOptions.None);

			// Token: 0x0400030F RID: 783
			private static ConfigurationProperty maxAge = new ConfigurationProperty("MaxAge", typeof(TimeSpan), TimeSpan.Zero, ConfigurationPropertyOptions.IsRequired);

			// Token: 0x04000310 RID: 784
			private static ConfigurationProperty checkInterval = new ConfigurationProperty("CheckInterval", typeof(TimeSpan), TimeSpan.Zero, ConfigurationPropertyOptions.IsRequired);

			// Token: 0x04000311 RID: 785
			private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
		}
	}
}
