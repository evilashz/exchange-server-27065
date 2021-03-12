using System;
using System.Configuration;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000023 RID: 35
	public class JobSection : ConfigurationSection
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000060A8 File Offset: 0x000042A8
		static JobSection()
		{
			JobSection.properties.Add(JobSection.jobs);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000060DE File Offset: 0x000042DE
		public JobConfigurationCollection Jobs
		{
			get
			{
				return (JobConfigurationCollection)base[JobSection.jobs];
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000060F0 File Offset: 0x000042F0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return JobSection.properties;
			}
		}

		// Token: 0x04000306 RID: 774
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000307 RID: 775
		private static ConfigurationProperty jobs = new ConfigurationProperty("Jobs", typeof(JobConfigurationCollection), null, ConfigurationPropertyOptions.IsRequired);
	}
}
