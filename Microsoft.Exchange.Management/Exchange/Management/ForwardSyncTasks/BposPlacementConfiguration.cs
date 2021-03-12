using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000342 RID: 834
	[Serializable]
	public class BposPlacementConfiguration : ConfigurableObject
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x0007F750 File Offset: 0x0007D950
		internal BposPlacementConfiguration(string configuration) : this()
		{
			this.Configuration = configuration;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0007F75F File Offset: 0x0007D95F
		internal BposPlacementConfiguration() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0007F788 File Offset: 0x0007D988
		// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0007F79A File Offset: 0x0007D99A
		public string Configuration
		{
			get
			{
				return (string)base[BposPlacementConfigurationSchema.Configuration];
			}
			private set
			{
				base[BposPlacementConfigurationSchema.Configuration] = value;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0007F7A8 File Offset: 0x0007D9A8
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return BposPlacementConfiguration.schema;
			}
		}

		// Token: 0x04001860 RID: 6240
		private static BposPlacementConfigurationSchema schema = ObjectSchema.GetInstance<BposPlacementConfigurationSchema>();
	}
}
