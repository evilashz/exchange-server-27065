using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000552 RID: 1362
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class PropertyBagAdaptor
	{
		// Token: 0x0600399F RID: 14751 RVA: 0x000EC252 File Offset: 0x000EA452
		public static PropertyBagAdaptor Create(ICoreObject coreObject)
		{
			return new PropertyBagAdaptor.CoreObjectPropertyBagAdaptor(coreObject);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000EC25A File Offset: 0x000EA45A
		public static PropertyBagAdaptor Create(IStorePropertyBag storePropertyBag)
		{
			return new PropertyBagAdaptor.StorePropertyBagAdaptor(storePropertyBag);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000EC264 File Offset: 0x000EA464
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			T result;
			try
			{
				result = this.GetValueOrDefaultInternal<T>(propertyDefinition, defaultValue);
			}
			catch (PropertyErrorException arg)
			{
				PropertyBagAdaptor.Tracer.TraceError<string, PropertyErrorException>((long)this.GetHashCode(), "Property {0} ignore due error {1}", propertyDefinition.Name, arg);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x060039A2 RID: 14754
		public abstract void SetValue(StorePropertyDefinition propertyDefinition, object value);

		// Token: 0x060039A3 RID: 14755
		public abstract void DeleteValue(StorePropertyDefinition propertyDefinition);

		// Token: 0x060039A4 RID: 14756
		protected abstract T GetValueOrDefaultInternal<T>(StorePropertyDefinition propertyDefinition, T defaultValue);

		// Token: 0x04001EC2 RID: 7874
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x02000553 RID: 1363
		private sealed class CoreObjectPropertyBagAdaptor : PropertyBagAdaptor
		{
			// Token: 0x060039A7 RID: 14759 RVA: 0x000EC2C4 File Offset: 0x000EA4C4
			public CoreObjectPropertyBagAdaptor(ICoreObject coreObject)
			{
				this.coreObject = coreObject;
			}

			// Token: 0x060039A8 RID: 14760 RVA: 0x000EC2D3 File Offset: 0x000EA4D3
			public override void SetValue(StorePropertyDefinition propertyDefinition, object value)
			{
				this.coreObject.PropertyBag[propertyDefinition] = value;
			}

			// Token: 0x060039A9 RID: 14761 RVA: 0x000EC2E7 File Offset: 0x000EA4E7
			public override void DeleteValue(StorePropertyDefinition propertyDefinition)
			{
				this.coreObject.PropertyBag.Delete(propertyDefinition);
			}

			// Token: 0x060039AA RID: 14762 RVA: 0x000EC2FA File Offset: 0x000EA4FA
			protected override T GetValueOrDefaultInternal<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
			{
				return this.coreObject.PropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}

			// Token: 0x04001EC3 RID: 7875
			private ICoreObject coreObject;
		}

		// Token: 0x02000554 RID: 1364
		private sealed class StorePropertyBagAdaptor : PropertyBagAdaptor
		{
			// Token: 0x060039AB RID: 14763 RVA: 0x000EC30E File Offset: 0x000EA50E
			public StorePropertyBagAdaptor(IStorePropertyBag storePropertyBag)
			{
				this.storePropertyBag = storePropertyBag;
			}

			// Token: 0x060039AC RID: 14764 RVA: 0x000EC31D File Offset: 0x000EA51D
			public override void SetValue(StorePropertyDefinition propertyDefinition, object value)
			{
				this.storePropertyBag[propertyDefinition] = value;
			}

			// Token: 0x060039AD RID: 14765 RVA: 0x000EC32C File Offset: 0x000EA52C
			public override void DeleteValue(StorePropertyDefinition propertyDefinition)
			{
				this.storePropertyBag.Delete(propertyDefinition);
			}

			// Token: 0x060039AE RID: 14766 RVA: 0x000EC33A File Offset: 0x000EA53A
			protected override T GetValueOrDefaultInternal<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
			{
				return this.storePropertyBag.GetValueOrDefault<T>(propertyDefinition, defaultValue);
			}

			// Token: 0x04001EC4 RID: 7876
			private IStorePropertyBag storePropertyBag;
		}
	}
}
