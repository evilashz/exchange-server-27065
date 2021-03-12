using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C60 RID: 3168
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class FlagsProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F97 RID: 28567 RVA: 0x001E0958 File Offset: 0x001DEB58
		private FlagsProperty(string displayName, NativeStorePropertyDefinition nativeProperty, int flag, PropertyDefinitionConstraint[] constraints, params PropertyDependency[] dependencies) : base(displayName, typeof(bool), PropertyFlags.None, constraints, dependencies)
		{
			this.flag = flag;
			this.nativeProperty = nativeProperty;
		}

		// Token: 0x06006F98 RID: 28568 RVA: 0x001E0980 File Offset: 0x001DEB80
		internal FlagsProperty(string displayName, NativeStorePropertyDefinition nativeProperty, int flag, PropertyDefinitionConstraint[] constraints) : this(displayName, nativeProperty, flag, constraints, new PropertyDependency[]
		{
			new PropertyDependency(nativeProperty, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006F99 RID: 28569 RVA: 0x001E09AC File Offset: 0x001DEBAC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(this.nativeProperty);
			PropertyError propertyError = value as PropertyError;
			if (propertyError != null)
			{
				return propertyError;
			}
			return BoxedConstants.GetBool(((int)value & this.flag) == this.flag);
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x001E09F0 File Offset: 0x001DEBF0
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (!(value is bool))
			{
				string message = ServerStrings.ExInvalidValueForFlagsCalculatedProperty(this.flag);
				ExTraceGlobals.StorageTracer.TraceError((long)this.GetHashCode(), message);
				throw new ArgumentException(message);
			}
			object value2 = propertyBag.GetValue(this.nativeProperty);
			PropertyError propertyError = value2 as PropertyError;
			int num;
			if (propertyError == null)
			{
				num = (int)value2;
			}
			else
			{
				if (propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
				{
					throw PropertyError.ToException(new PropertyError[]
					{
						propertyError
					});
				}
				num = 0;
			}
			if ((bool)value)
			{
				propertyBag.SetValueWithFixup(this.nativeProperty, num | this.flag);
				return;
			}
			propertyBag.SetValueWithFixup(this.nativeProperty, num & ~this.flag);
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x001E0AB2 File Offset: 0x001DECB2
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			this.InternalSetValue(propertyBag, false);
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x001E0AC4 File Offset: 0x001DECC4
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			bool isNonZero = true;
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				if ((comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && !(bool)comparisonFilter.PropertyValue) || (comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual && (bool)comparisonFilter.PropertyValue))
				{
					isNonZero = false;
				}
				else if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && comparisonFilter.ComparisonOperator != ComparisonOperator.NotEqual)
				{
					throw base.CreateInvalidFilterConversionException(filter);
				}
				return new BitMaskFilter(this.nativeProperty, (ulong)this.flag, isNonZero);
			}
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(this.nativeProperty);
			}
			return base.SmartFilterToNativeFilter(filter);
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x001E0B54 File Offset: 0x001DED54
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(this.nativeProperty))
			{
				BitMaskFilter bitMaskFilter = filter as BitMaskFilter;
				if (bitMaskFilter != null && bitMaskFilter.Mask == (ulong)this.flag)
				{
					return new ComparisonFilter(ComparisonOperator.Equal, this, bitMaskFilter.IsNonZero);
				}
			}
			return null;
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06006F9E RID: 28574 RVA: 0x001E0BAB File Offset: 0x001DEDAB
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.CanQuery;
			}
		}

		// Token: 0x06006F9F RID: 28575 RVA: 0x001E0BAE File Offset: 0x001DEDAE
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(BitMaskFilter));
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x001E0BC0 File Offset: 0x001DEDC0
		internal int Flag
		{
			get
			{
				return this.flag;
			}
		}

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06006FA1 RID: 28577 RVA: 0x001E0BC8 File Offset: 0x001DEDC8
		internal NativeStorePropertyDefinition NativeProperty
		{
			get
			{
				return this.nativeProperty;
			}
		}

		// Token: 0x040043A9 RID: 17321
		private readonly NativeStorePropertyDefinition nativeProperty;

		// Token: 0x040043AA RID: 17322
		private readonly int flag;
	}
}
