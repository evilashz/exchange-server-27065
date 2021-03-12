using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000075 RID: 117
	internal class PropertyTranslationRule<TLeft, TRight, TLeftProperty, TLeftValue, TRightValue> : IStorageTranslationRule<!0, !1>, IPropertyValueCollectionTranslationRule<TLeft, TLeftProperty, TRight>, ITranslationRule<TLeft, TRight>
	{
		// Token: 0x0600027B RID: 635 RVA: 0x000087D8 File Offset: 0x000069D8
		public PropertyTranslationRule(IPropertyAccessor<TLeft, TLeftValue> leftAccessor, IPropertyAccessor<TRight, TRightValue> rightAccessor, IConverter<TLeftValue, TRightValue> leftToRightConverter = null, IConverter<TRightValue, TLeftValue> rightToLeftConverter = null)
		{
			this.leftToRightConverter = leftToRightConverter;
			this.rightToLeftConverter = rightToLeftConverter;
			this.LeftAccessor = leftAccessor;
			this.RightAccessor = rightAccessor;
			IStoragePropertyAccessor<TLeft, TLeftValue> storagePropertyAccessor = leftAccessor as IStoragePropertyAccessor<TLeft, TLeftValue>;
			if (storagePropertyAccessor == null)
			{
				IStoragePropertyAccessor<TRight, TRightValue> storagePropertyAccessor2 = rightAccessor as IStoragePropertyAccessor<TRight, TRightValue>;
				if (storagePropertyAccessor2 != null)
				{
					EntityPropertyAccessorBase<TLeft, TLeftValue> entityPropertyAccessorBase = leftAccessor as EntityPropertyAccessorBase<TLeft, TLeftValue>;
					if (entityPropertyAccessorBase != null)
					{
						this.storageDependencies = storagePropertyAccessor2.Dependencies;
						this.storagePropertyGroup = storagePropertyAccessor2.PropertyChangeMetadataGroup;
						this.entityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
						{
							entityPropertyAccessorBase.PropertyDefinition
						};
						return;
					}
				}
			}
			else
			{
				EntityPropertyAccessorBase<TRight, TRightValue> entityPropertyAccessorBase2 = rightAccessor as EntityPropertyAccessorBase<TRight, TRightValue>;
				if (entityPropertyAccessorBase2 != null)
				{
					this.storageDependencies = storagePropertyAccessor.Dependencies;
					this.storagePropertyGroup = storagePropertyAccessor.PropertyChangeMetadataGroup;
					this.entityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
					{
						entityPropertyAccessorBase2.PropertyDefinition
					};
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008895 File Offset: 0x00006A95
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000889D File Offset: 0x00006A9D
		public IPropertyAccessor<TLeft, TLeftValue> LeftAccessor { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000088A6 File Offset: 0x00006AA6
		// (set) Token: 0x0600027F RID: 639 RVA: 0x000088AE File Offset: 0x00006AAE
		public IPropertyAccessor<TRight, TRightValue> RightAccessor { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000088B7 File Offset: 0x00006AB7
		IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> IStorageTranslationRule<!0, !1>.StorageDependencies
		{
			get
			{
				return this.storageDependencies;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000088BF File Offset: 0x00006ABF
		PropertyChangeMetadata.PropertyGroup IStorageTranslationRule<!0, !1>.StoragePropertyGroup
		{
			get
			{
				return this.storagePropertyGroup;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000088C7 File Offset: 0x00006AC7
		IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> IStorageTranslationRule<!0, !1>.EntityProperties
		{
			get
			{
				return this.entityProperties;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000088CF File Offset: 0x00006ACF
		public virtual IConverter<TLeftValue, TRightValue> GetLeftToRightConverter(TLeft left, TRight right)
		{
			return this.leftToRightConverter;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000088D7 File Offset: 0x00006AD7
		public virtual IConverter<TRightValue, TLeftValue> GetRightToLeftConverter(TLeft left, TRight right)
		{
			return this.rightToLeftConverter;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000088DF File Offset: 0x00006ADF
		public void FromLeftToRightType(TLeft left, TRight right)
		{
			PropertyTranslationRule<TLeft, TRight, TLeftProperty, TLeftValue, TRightValue>.PerformTranslation<TLeft, TRight, TLeftValue, TRightValue>(left, this.LeftAccessor, right, this.RightAccessor, this.GetLeftToRightConverter(left, right));
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000088FC File Offset: 0x00006AFC
		public void FromRightToLeftType(TLeft left, TRight right)
		{
			PropertyTranslationRule<TLeft, TRight, TLeftProperty, TLeftValue, TRightValue>.PerformTranslation<TRight, TLeft, TRightValue, TLeftValue>(right, this.RightAccessor, left, this.LeftAccessor, this.GetRightToLeftConverter(left, right));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000893C File Offset: 0x00006B3C
		public void FromPropertyValues(IDictionary<TLeftProperty, int> propertyIndices, IList values, TRight right)
		{
			IPropertyValueCollectionAccessor<TLeft, TLeftProperty, TLeftValue> accessor;
			IConverter<TLeftValue, TRightValue> converter;
			if (this.CanTranslateFromPropertyValues<TLeftProperty>(out accessor, out converter))
			{
				PropertyTranslationRule<TLeft, TRight, TLeftProperty, TLeftValue, TRightValue>.PerformTranslation<TRight, TLeftValue, TRightValue>(delegate(out TLeftValue leftValue)
				{
					return accessor.TryGetValue(propertyIndices, values, out leftValue);
				}, right, this.RightAccessor, converter);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000089AC File Offset: 0x00006BAC
		protected static void PerformTranslation<TSource, TDestination, TSourceValue, TDestinationValue>(TSource source, IPropertyAccessor<TSource, TSourceValue> sourceAccessor, TDestination destination, IPropertyAccessor<TDestination, TDestinationValue> destinationAccessor, IConverter<TSourceValue, TDestinationValue> converter)
		{
			PropertyTranslationRule<TLeft, TRight, TLeftProperty, TLeftValue, TRightValue>.PerformTranslation<TDestination, TSourceValue, TDestinationValue>(delegate(out TSourceValue sourceValue)
			{
				return sourceAccessor.TryGetValue(source, out sourceValue);
			}, destination, destinationAccessor, converter);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000089E4 File Offset: 0x00006BE4
		protected static void PerformTranslation<TDestination, TSourceValue, TDestinationValue>(PropertyTranslationRule<TLeft, TRight, TLeftProperty, TLeftValue, TRightValue>.TryGetValueFunc<TSourceValue> tryGetSourceValue, TDestination destination, IPropertyAccessor<TDestination, TDestinationValue> destinationAccessor, IConverter<TSourceValue, TDestinationValue> converter)
		{
			if (tryGetSourceValue == null)
			{
				throw new ArgumentNullException("tryGetSourceValue");
			}
			TSourceValue value;
			if (converter != null && !destinationAccessor.Readonly && tryGetSourceValue(out value))
			{
				TDestinationValue value2 = converter.Convert(value);
				destinationAccessor.Set(destination, value2);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00008A24 File Offset: 0x00006C24
		private bool CanTranslateFromPropertyValues<TProperty>(out IPropertyValueCollectionAccessor<TLeft, TProperty, TLeftValue> accessor, out IConverter<TLeftValue, TRightValue> converter)
		{
			accessor = (this.LeftAccessor as IPropertyValueCollectionAccessor<TLeft, TProperty, TLeftValue>);
			converter = this.leftToRightConverter;
			return accessor != null && converter != null;
		}

		// Token: 0x040000ED RID: 237
		private readonly IConverter<TLeftValue, TRightValue> leftToRightConverter;

		// Token: 0x040000EE RID: 238
		private readonly IConverter<TRightValue, TLeftValue> rightToLeftConverter;

		// Token: 0x040000EF RID: 239
		private readonly IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> storageDependencies;

		// Token: 0x040000F0 RID: 240
		private readonly PropertyChangeMetadata.PropertyGroup storagePropertyGroup;

		// Token: 0x040000F1 RID: 241
		private readonly Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[] entityProperties;

		// Token: 0x02000076 RID: 118
		// (Invoke) Token: 0x0600028C RID: 652
		public delegate bool TryGetValueFunc<TValue>(out TValue value);
	}
}
