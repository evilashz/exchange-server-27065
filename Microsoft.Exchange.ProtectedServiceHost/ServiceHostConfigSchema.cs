using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000008 RID: 8
	internal class ServiceHostConfigSchema : ConfigSchemaBase
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public override string Name
		{
			get
			{
				return "ServiceHost";
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002DEB File Offset: 0x00000FEB
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[ConfigurationProperty("IsEnabled", DefaultValue = true)]
		public bool IsEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("IsEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "IsEnabled");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002E06 File Offset: 0x00001006
		protected override ExchangeConfigurationSection ScopeSchema
		{
			get
			{
				return ServiceHostConfigSchema.scopeSchema;
			}
		}

		// Token: 0x0400001D RID: 29
		private static readonly ServiceHostConfigSchema.ServiceHostScopeSchema scopeSchema = new ServiceHostConfigSchema.ServiceHostScopeSchema();

		// Token: 0x02000009 RID: 9
		public static class Scope
		{
			// Token: 0x0400001E RID: 30
			public const string ModuleName = "ModuleName";
		}

		// Token: 0x0200000A RID: 10
		public static class Setting
		{
			// Token: 0x0400001F RID: 31
			public const string IsEnabled = "IsEnabled";
		}

		// Token: 0x0200000B RID: 11
		[Serializable]
		private class ServiceHostScopeSchema : ExchangeConfigurationSection
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000039 RID: 57 RVA: 0x00002E21 File Offset: 0x00001021
			// (set) Token: 0x0600003A RID: 58 RVA: 0x00002E2E File Offset: 0x0000102E
			[ConfigurationProperty("ModuleName", DefaultValue = "")]
			public string ModuleName
			{
				get
				{
					return this.InternalGetConfig<string>("ModuleName");
				}
				set
				{
					this.InternalSetConfig<string>(value, "ModuleName");
				}
			}
		}
	}
}
