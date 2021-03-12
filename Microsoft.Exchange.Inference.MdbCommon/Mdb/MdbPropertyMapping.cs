using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000013 RID: 19
	internal abstract class MdbPropertyMapping : ProviderPropertyMapping<StorePropertyDefinition, IItem, IMdbPropertyMappingContext>
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003F8D File Offset: 0x0000218D
		protected MdbPropertyMapping(PropertyDefinition propertyDefinition, StorePropertyDefinition[] providerPropertyDefinitions) : base(propertyDefinition, providerPropertyDefinitions)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003F97 File Offset: 0x00002197
		public StorePropertyDefinition[] StorePropertyDefinitions
		{
			get
			{
				return this.ProviderSpecificPropertyDefinitions;
			}
		}
	}
}
