using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004CD RID: 1229
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SelectionStrategy
	{
		// Token: 0x060035E4 RID: 13796 RVA: 0x000D9810 File Offset: 0x000D7A10
		protected SelectionStrategy(params StorePropertyDefinition[] dependencies)
		{
			this.dependencies = PropertyDefinitionCollection.Merge<StorePropertyDefinition>(dependencies, this.RequiredProperties());
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x000D982A File Offset: 0x000D7A2A
		public StorePropertyDefinition[] Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x060035E6 RID: 13798
		public abstract object GetValue(IStorePropertyBag item);

		// Token: 0x060035E7 RID: 13799
		public abstract bool IsSelectable(IStorePropertyBag source);

		// Token: 0x060035E8 RID: 13800
		public abstract bool HasPriority(IStorePropertyBag item1, IStorePropertyBag item2);

		// Token: 0x060035E9 RID: 13801
		public abstract StorePropertyDefinition[] RequiredProperties();

		// Token: 0x04001CF0 RID: 7408
		private readonly StorePropertyDefinition[] dependencies;

		// Token: 0x020004CE RID: 1230
		internal abstract class SingleSourcePropertySelection : SelectionStrategy
		{
			// Token: 0x060035EA RID: 13802 RVA: 0x000D9834 File Offset: 0x000D7A34
			public SingleSourcePropertySelection(StorePropertyDefinition sourceProperty) : base(new StorePropertyDefinition[]
			{
				sourceProperty
			})
			{
				this.sourceProperty = sourceProperty;
			}

			// Token: 0x060035EB RID: 13803 RVA: 0x000D985C File Offset: 0x000D7A5C
			protected SingleSourcePropertySelection(StorePropertyDefinition sourceProperty, params StorePropertyDefinition[] additionalDependencies) : base(PropertyDefinitionCollection.Merge<StorePropertyDefinition>(additionalDependencies, new StorePropertyDefinition[]
			{
				sourceProperty
			}))
			{
				this.sourceProperty = sourceProperty;
			}

			// Token: 0x060035EC RID: 13804 RVA: 0x000D9888 File Offset: 0x000D7A88
			public override bool IsSelectable(IStorePropertyBag source)
			{
				Util.ThrowOnNullArgument(source, "source");
				object value = this.GetValue(source);
				return value != null && !(value is PropertyError);
			}

			// Token: 0x060035ED RID: 13805 RVA: 0x000D98B9 File Offset: 0x000D7AB9
			public override object GetValue(IStorePropertyBag item)
			{
				Util.ThrowOnNullArgument(item, "item");
				return item.TryGetProperty(this.SourceProperty);
			}

			// Token: 0x170010CD RID: 4301
			// (get) Token: 0x060035EE RID: 13806 RVA: 0x000D98D2 File Offset: 0x000D7AD2
			protected StorePropertyDefinition SourceProperty
			{
				get
				{
					return this.sourceProperty;
				}
			}

			// Token: 0x04001CF1 RID: 7409
			private readonly StorePropertyDefinition sourceProperty;
		}
	}
}
