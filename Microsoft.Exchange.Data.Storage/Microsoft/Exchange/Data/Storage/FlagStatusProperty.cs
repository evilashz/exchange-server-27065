using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C5F RID: 3167
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class FlagStatusProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F8B RID: 28555 RVA: 0x001E065C File Offset: 0x001DE85C
		internal FlagStatusProperty() : base("FlagStatusProperty", typeof(int), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiFlagStatus, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.ItemColor, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TaskStatus, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006F8C RID: 28556 RVA: 0x001E06C4 File Offset: 0x001DE8C4
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (!ObjectClass.IsMeetingRequest(valueOrDefault))
			{
				return propertyBag.GetValueOrDefault<FlagStatus>(InternalSchema.MapiFlagStatus);
			}
			int valueOrDefault2 = propertyBag.GetValueOrDefault<int>(InternalSchema.ItemColor);
			if (valueOrDefault2 > 0)
			{
				return FlagStatus.Flagged;
			}
			int valueOrDefault3 = propertyBag.GetValueOrDefault<int>(InternalSchema.TaskStatus);
			if (valueOrDefault3 == 2)
			{
				return FlagStatus.Complete;
			}
			return FlagStatus.NotFlagged;
		}

		// Token: 0x06006F8D RID: 28557 RVA: 0x001E0734 File Offset: 0x001DE934
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("FlagStatusProperty");
			}
			EnumValidator.ThrowIfInvalid<FlagStatus>((FlagStatus)value);
			switch ((FlagStatus)value)
			{
			case FlagStatus.NotFlagged:
				FlagStatusProperty.ClearFlagStatus(propertyBag);
				return;
			case FlagStatus.Complete:
				FlagStatusProperty.CompleteFlagStatus(propertyBag);
				return;
			case FlagStatus.Flagged:
				FlagStatusProperty.SetFlagStatus(propertyBag);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006F8E RID: 28558 RVA: 0x001E078A File Offset: 0x001DE98A
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.MapiFlagStatus);
		}

		// Token: 0x06006F8F RID: 28559 RVA: 0x001E0798 File Offset: 0x001DE998
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, InternalSchema.MapiFlagStatus, (int)comparisonFilter.PropertyValue);
			}
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(InternalSchema.MapiFlagStatus);
			}
			return base.SmartFilterToNativeFilter(filter);
		}

		// Token: 0x06006F90 RID: 28560 RVA: 0x001E07EC File Offset: 0x001DE9EC
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(InternalSchema.MapiFlagStatus))
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter != null)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, this, (int)comparisonFilter.PropertyValue);
				}
				if (filter is ExistsFilter)
				{
					return new ExistsFilter(this);
				}
			}
			return null;
		}

		// Token: 0x06006F91 RID: 28561 RVA: 0x001E084C File Offset: 0x001DEA4C
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06006F92 RID: 28562 RVA: 0x001E086E File Offset: 0x001DEA6E
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006F93 RID: 28563 RVA: 0x001E0871 File Offset: 0x001DEA71
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiFlagStatus;
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x001E0878 File Offset: 0x001DEA78
		private static void CompleteFlagStatus(PropertyBag.BasicPropertyStore propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (ObjectClass.IsMeetingMessage(valueOrDefault))
			{
				propertyBag.Delete(InternalSchema.MapiFlagStatus);
				propertyBag.Delete(InternalSchema.ItemColor);
				propertyBag.SetValueWithFixup(InternalSchema.TaskStatus, TaskStatus.Completed);
				return;
			}
			propertyBag.SetValueWithFixup(InternalSchema.MapiFlagStatus, FlagStatus.Complete);
			propertyBag.SetValueWithFixup(InternalSchema.TaskStatus, TaskStatus.Completed);
			propertyBag.Delete(InternalSchema.ItemColor);
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x001E08FA File Offset: 0x001DEAFA
		private static void SetFlagStatus(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MapiFlagStatus, FlagStatus.Flagged);
			propertyBag.SetValueWithFixup(InternalSchema.TaskStatus, TaskStatus.InProgress);
			propertyBag.SetValueWithFixup(InternalSchema.ItemColor, ItemColor.Red);
		}

		// Token: 0x06006F96 RID: 28566 RVA: 0x001E0932 File Offset: 0x001DEB32
		private static void ClearFlagStatus(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.MapiFlagStatus);
			propertyBag.Delete(InternalSchema.TaskStatus);
			propertyBag.Delete(InternalSchema.ItemColor);
		}
	}
}
