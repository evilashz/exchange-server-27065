using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConfigurationSchema<TDerivedSchema> : ConfigurationSchema where TDerivedSchema : new()
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000038E0 File Offset: 0x00001AE0
		public static TDerivedSchema Instance
		{
			get
			{
				if (ConfigurationSchema<TDerivedSchema>.instance == null)
				{
					ConfigurationSchema<TDerivedSchema>.instance = ((default(TDerivedSchema) == null) ? Activator.CreateInstance<TDerivedSchema>() : default(TDerivedSchema));
				}
				return ConfigurationSchema<TDerivedSchema>.instance;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003923 File Offset: 0x00001B23
		public override IEnumerable<ConfigurationSchema.DataSource> DataSources
		{
			get
			{
				return ConfigurationSchema<TDerivedSchema>.AllDataSources;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000392C File Offset: 0x00001B2C
		public override void LoadAll(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger)
		{
			foreach (ConfigurationSchema.DataSource dataSource in ConfigurationSchema<TDerivedSchema>.AllDataSources)
			{
				dataSource.Load(configurationUpdater, eventLogger);
			}
		}

		// Token: 0x0400003B RID: 59
		protected static readonly List<ConfigurationSchema.DataSource> AllDataSources = new List<ConfigurationSchema.DataSource>();

		// Token: 0x0400003C RID: 60
		protected new static readonly ConfigurationSchema.ConstantDataSource ConstantDataSource = new ConfigurationSchema.ConstantDataSource(ConfigurationSchema<TDerivedSchema>.AllDataSources);

		// Token: 0x0400003D RID: 61
		private static TDerivedSchema instance;
	}
}
