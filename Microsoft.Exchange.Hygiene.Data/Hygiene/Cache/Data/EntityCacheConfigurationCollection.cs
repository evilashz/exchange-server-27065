using System;
using System.Configuration;
using System.Linq;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200005D RID: 93
	[ConfigurationCollection(typeof(EntityCacheConfiguration), AddItemName = "entityCache")]
	internal class EntityCacheConfigurationCollection : ConfigurationElementCollection
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000A670 File Offset: 0x00008870
		public EntityCacheConfiguration FindByName(string name)
		{
			return this.Cast<EntityCacheConfiguration>().FirstOrDefault((EntityCacheConfiguration c) => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A6A1 File Offset: 0x000088A1
		protected override ConfigurationElement CreateNewElement()
		{
			return new EntityCacheConfiguration();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A6A8 File Offset: 0x000088A8
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((EntityCacheConfiguration)element).Name;
		}
	}
}
