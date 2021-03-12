using System;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000085 RID: 133
	internal class StoreConfigContext : SettingsContextBase
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x000142AC File Offset: 0x000124AC
		public static void Initialize()
		{
			StoreConfigContext.Default = new StoreConfigContext();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000142B8 File Offset: 0x000124B8
		private StoreConfigContext() : base(null)
		{
			StoreConfigContext.ConfigProvider = new StoreConfigProvider(new StoreConfigSchema());
			StoreConfigContext.ConfigProvider.Initialize();
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000142DA File Offset: 0x000124DA
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x000142E1 File Offset: 0x000124E1
		public static IConfigProvider ConfigProvider { get; private set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000142E9 File Offset: 0x000124E9
		public override Guid? DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000142F1 File Offset: 0x000124F1
		public override Guid? DagOrServerGuid
		{
			get
			{
				return this.dagOrServerGuid;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000142F9 File Offset: 0x000124F9
		public void SetDatabaseContext(Guid? databaseGuid, Guid? dagOrServerGuid)
		{
			this.databaseGuid = databaseGuid;
			this.dagOrServerGuid = dagOrServerGuid;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00014309 File Offset: 0x00012509
		public T GetConfig<T>(string settingName, T defaultValue)
		{
			return StoreConfigContext.ConfigProvider.GetConfig<T>(this, settingName, defaultValue);
		}

		// Token: 0x04000677 RID: 1655
		public static StoreConfigContext Default;

		// Token: 0x04000678 RID: 1656
		private Guid? databaseGuid;

		// Token: 0x04000679 RID: 1657
		private Guid? dagOrServerGuid;
	}
}
