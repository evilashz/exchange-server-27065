using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000012 RID: 18
	internal abstract class ProviderPropertyMapping<TProviderPropertyDefinition, TProviderItem, TContext> : PropertyMapping
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002F17 File Offset: 0x00001117
		protected ProviderPropertyMapping(PropertyDefinition propertyDefinition, TProviderPropertyDefinition[] providerPropertyDefinitions) : base(propertyDefinition)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			Util.ThrowOnNullOrEmptyArgument<TProviderPropertyDefinition>(providerPropertyDefinitions, "providerPropertyDefinitions");
			this.ProviderSpecificPropertyDefinitions = providerPropertyDefinitions;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002F3D File Offset: 0x0000113D
		protected TProviderPropertyDefinition[] ProviderPropertyDefinitions
		{
			get
			{
				return this.ProviderSpecificPropertyDefinitions;
			}
		}

		// Token: 0x0600007A RID: 122
		public abstract object GetPropertyValue(TProviderItem item, TContext context);

		// Token: 0x0600007B RID: 123
		public abstract void SetPropertyValue(TProviderItem item, object value, TContext context);

		// Token: 0x0600007C RID: 124
		public abstract object GetPropertyValue(IDictionary<TProviderPropertyDefinition, object> dictionary);

		// Token: 0x0600007D RID: 125
		public abstract void SetPropertyValue(IDictionary<TProviderPropertyDefinition, object> dictionary, object value);

		// Token: 0x04000033 RID: 51
		protected readonly TProviderPropertyDefinition[] ProviderSpecificPropertyDefinitions;
	}
}
