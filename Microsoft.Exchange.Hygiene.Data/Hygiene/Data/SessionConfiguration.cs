using System;
using System.Configuration;
using System.Reflection;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A4 RID: 164
	public class SessionConfiguration : ConfigurationSection
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000125D1 File Offset: 0x000107D1
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000125E3 File Offset: 0x000107E3
		[ConfigurationProperty("PerMessageRecipientSaveThreshold", IsRequired = false, DefaultValue = "100")]
		public int PerMessageRecipientSaveThreshold
		{
			get
			{
				return (int)base["PerMessageRecipientSaveThreshold"];
			}
			internal set
			{
				base["PerMessageRecipientSaveThreshold"] = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000125F6 File Offset: 0x000107F6
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00012608 File Offset: 0x00010808
		[ConfigurationProperty("PerMessageRecipientSplitSaveThreshold", IsRequired = false, DefaultValue = "1000")]
		public int PerMessageRecipientSplitSaveThreshold
		{
			get
			{
				return (int)base["PerMessageRecipientSplitSaveThreshold"];
			}
			internal set
			{
				base["PerMessageRecipientSplitSaveThreshold"] = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001261B File Offset: 0x0001081B
		public static SessionConfiguration Instance
		{
			get
			{
				return SessionConfiguration.instance;
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00012624 File Offset: 0x00010824
		private static SessionConfiguration GetInstance()
		{
			SessionConfiguration sessionConfiguration = (SessionConfiguration)ConfigurationManager.GetSection("DALSessionConfiguration");
			if (sessionConfiguration == null)
			{
				string exeConfigFilename = Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path) + ".config";
				ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
				{
					ExeConfigFilename = exeConfigFilename
				};
				Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
				sessionConfiguration = (SessionConfiguration)configuration.GetSection("DALSessionConfiguration");
			}
			if (sessionConfiguration == null)
			{
				sessionConfiguration = new SessionConfiguration();
			}
			return sessionConfiguration;
		}

		// Token: 0x04000367 RID: 871
		private const string PerMessageRecipientSaveThresholdKey = "PerMessageRecipientSaveThreshold";

		// Token: 0x04000368 RID: 872
		private const string PerMessageRecipientSplitSaveThresholdKey = "PerMessageRecipientSplitSaveThreshold";

		// Token: 0x04000369 RID: 873
		private const string SectionName = "DALSessionConfiguration";

		// Token: 0x0400036A RID: 874
		private static SessionConfiguration instance = SessionConfiguration.GetInstance();
	}
}
