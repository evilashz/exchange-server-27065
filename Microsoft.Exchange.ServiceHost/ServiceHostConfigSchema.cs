using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x0200000E RID: 14
	internal class ServiceHostConfigSchema : ConfigSchemaBase
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000039E4 File Offset: 0x00001BE4
		public override string Name
		{
			get
			{
				return "ServiceHost";
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000039EB File Offset: 0x00001BEB
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000039F8 File Offset: 0x00001BF8
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003A06 File Offset: 0x00001C06
		protected override ExchangeConfigurationSection ScopeSchema
		{
			get
			{
				return ServiceHostConfigSchema.scopeSchema;
			}
		}

		// Token: 0x0400003A RID: 58
		private static readonly ServiceHostConfigSchema.ServiceHostScopeSchema scopeSchema = new ServiceHostConfigSchema.ServiceHostScopeSchema();

		// Token: 0x0200000F RID: 15
		public static class Scope
		{
			// Token: 0x0400003B RID: 59
			public const string ModuleName = "ModuleName";
		}

		// Token: 0x02000010 RID: 16
		public static class Setting
		{
			// Token: 0x0400003C RID: 60
			public const string IsEnabled = "IsEnabled";
		}

		// Token: 0x02000011 RID: 17
		[Serializable]
		private class ServiceHostScopeSchema : ExchangeConfigurationSection
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600006A RID: 106 RVA: 0x00003A21 File Offset: 0x00001C21
			// (set) Token: 0x0600006B RID: 107 RVA: 0x00003A2E File Offset: 0x00001C2E
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
