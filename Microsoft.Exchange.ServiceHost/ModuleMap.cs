using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x0200000D RID: 13
	internal sealed class ModuleMap
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000038FC File Offset: 0x00001AFC
		public ModuleMap(string moduleName, string typeName, ServerRole role, ServerRole excludeIfRole, RunInExchangeMode runInExchangeMode)
		{
			this.ModuleName = moduleName;
			this.TypeName = typeName;
			this.Role = role;
			this.ExcludeIfRole = excludeIfRole;
			this.RunInExchangeMode = runInExchangeMode;
			this.configContext = new GenericSettingsContext("ModuleName", this.ModuleName, null);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000394B File Offset: 0x00001B4B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003953 File Offset: 0x00001B53
		public string ModuleName { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000395C File Offset: 0x00001B5C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003964 File Offset: 0x00001B64
		public string TypeName { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000396D File Offset: 0x00001B6D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003975 File Offset: 0x00001B75
		public ServerRole Role { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000397E File Offset: 0x00001B7E
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003986 File Offset: 0x00001B86
		public ServerRole ExcludeIfRole { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000398F File Offset: 0x00001B8F
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003997 File Offset: 0x00001B97
		public RunInExchangeMode RunInExchangeMode { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000039A0 File Offset: 0x00001BA0
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

		// Token: 0x04000034 RID: 52
		private GenericSettingsContext configContext;
	}
}
