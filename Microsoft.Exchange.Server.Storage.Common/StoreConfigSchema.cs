using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000087 RID: 135
	internal class StoreConfigSchema : ConfigSchemaBase
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00014374 File Offset: 0x00012574
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.propertyCollection == null)
				{
					this.propertyCollection = new ConfigurationPropertyCollection();
					for (int i = 0; i < ConfigurationSchema.RegisteredConfigurations.Count; i++)
					{
						ConfigurationSchema configurationSchema = ConfigurationSchema.RegisteredConfigurations[i];
						ConfigurationProperty configurationProperty = configurationSchema.ConfigurationProperty;
						this.propertyCollection.Add(configurationProperty);
					}
				}
				return this.propertyCollection;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000143CE File Offset: 0x000125CE
		public override string Name
		{
			get
			{
				return "Store";
			}
		}

		// Token: 0x0400067D RID: 1661
		private ConfigurationPropertyCollection propertyCollection;
	}
}
