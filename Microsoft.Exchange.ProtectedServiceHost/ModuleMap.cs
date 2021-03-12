using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000007 RID: 7
	internal sealed class ModuleMap
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002CFC File Offset: 0x00000EFC
		public ModuleMap(string moduleName, string typeName, ServerRole role, ServerRole excludeIfRole, RunInExchangeMode runInExchangeMode)
		{
			this.ModuleName = moduleName;
			this.TypeName = typeName;
			this.Role = role;
			this.ExcludeIfRole = excludeIfRole;
			this.RunInExchangeMode = runInExchangeMode;
			this.configContext = new GenericSettingsContext("ModuleName", this.ModuleName, null);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002D4B File Offset: 0x00000F4B
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002D53 File Offset: 0x00000F53
		public string ModuleName { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002D5C File Offset: 0x00000F5C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002D64 File Offset: 0x00000F64
		public string TypeName { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002D75 File Offset: 0x00000F75
		public ServerRole Role { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002D7E File Offset: 0x00000F7E
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002D86 File Offset: 0x00000F86
		public ServerRole ExcludeIfRole { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002D97 File Offset: 0x00000F97
		public RunInExchangeMode RunInExchangeMode { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public bool IsEnabled
		{
			get
			{
				bool config;
				using (this.configContext.Activate())
				{
					config = ConfigBase<ServiceHostConfigSchema>.GetConfig<bool>("IsEnabled");
				}
				return config;
			}
		}

		// Token: 0x04000017 RID: 23
		private GenericSettingsContext configContext;
	}
}
