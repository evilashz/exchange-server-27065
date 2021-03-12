using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C65 RID: 3173
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class HasAttachmentProperty : SmartPropertyDefinition
	{
		// Token: 0x06006FB3 RID: 28595 RVA: 0x001E1084 File Offset: 0x001DF284
		internal HasAttachmentProperty() : base("HasAttachment", typeof(bool), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiHasAttachment, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.AllAttachmentsHidden, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006FB4 RID: 28596 RVA: 0x001E10D0 File Offset: 0x001DF2D0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			CalendarItem calendarItem = propertyBag.Context.StoreObject as CalendarItem;
			if (calendarItem != null && calendarItem.IsAttachmentCollectionLoaded)
			{
				return calendarItem.AttachmentCollection.Count > 0;
			}
			object value = propertyBag.GetValue(InternalSchema.MapiHasAttachment);
			if (!(value is bool))
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			object value2 = propertyBag.GetValue(InternalSchema.AllAttachmentsHidden);
			if (value2 is bool && (bool)value2)
			{
				return false;
			}
			if (!(bool)value)
			{
				return false;
			}
			Item item = propertyBag.Context.StoreObject as Item;
			if (item != null)
			{
				foreach (AttachmentHandle attachmentHandle in item.AttachmentCollection)
				{
					if (!attachmentHandle.IsInline)
					{
						return true;
					}
				}
				return false;
			}
			return (bool)value;
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x001E11E0 File Offset: 0x001DF3E0
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null && comparisonFilter.PropertyValue is bool && (comparisonFilter.ComparisonOperator == ComparisonOperator.Equal || comparisonFilter.ComparisonOperator == ComparisonOperator.NotEqual))
			{
				bool flag = (comparisonFilter.ComparisonOperator == ComparisonOperator.Equal) ? ((bool)comparisonFilter.PropertyValue) : (!(bool)comparisonFilter.PropertyValue);
				QueryFilter result;
				if (flag)
				{
					result = HasAttachmentProperty.HasAttachmentFilter;
				}
				else
				{
					result = HasAttachmentProperty.NotHasAttachmentFilter;
				}
				return result;
			}
			return base.SmartFilterToNativeFilter(filter);
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x001E1258 File Offset: 0x001DF458
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			if (filter.Equals(HasAttachmentProperty.HasAttachmentFilter) || filter.Equals(HasAttachmentProperty.NativeToSmartHasAttachmentFilter))
			{
				return new ComparisonFilter(ComparisonOperator.Equal, this, true);
			}
			if (filter.Equals(HasAttachmentProperty.NotHasAttachmentFilter))
			{
				return new ComparisonFilter(ComparisonOperator.Equal, this, false);
			}
			return null;
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x001E12A9 File Offset: 0x001DF4A9
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.CanQuery | StorePropertyCapabilities.CanSortBy;
			}
		}

		// Token: 0x06006FB8 RID: 28600 RVA: 0x001E12AC File Offset: 0x001DF4AC
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(OrFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(NotFilter));
		}

		// Token: 0x06006FB9 RID: 28601 RVA: 0x001E12D0 File Offset: 0x001DF4D0
		internal override SortBy[] GetNativeSortBy(SortOrder sortOrder)
		{
			SortOrder sortOrder2 = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
			return new SortBy[]
			{
				new SortBy(InternalSchema.MapiHasAttachment, sortOrder),
				new SortBy(InternalSchema.AllAttachmentsHidden, sortOrder2)
			};
		}

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x06006FBA RID: 28602 RVA: 0x001E1309 File Offset: 0x001DF509
		private static QueryFilter NativeToSmartHasAttachmentFilter
		{
			get
			{
				if (HasAttachmentProperty.nativeToSmartHasAttachmentFilter == null)
				{
					HasAttachmentProperty.nativeToSmartHasAttachmentFilter = new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.HasAttachment, false));
				}
				return HasAttachmentProperty.nativeToSmartHasAttachmentFilter;
			}
		}

		// Token: 0x040043BF RID: 17343
		private static readonly QueryFilter NotHasAttachmentFilter = new OrFilter(new QueryFilter[]
		{
			new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(InternalSchema.AllAttachmentsHidden),
				new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.AllAttachmentsHidden, true)
			}),
			new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.MapiHasAttachment, false)
		});

		// Token: 0x040043C0 RID: 17344
		private static QueryFilter nativeToSmartHasAttachmentFilter;

		// Token: 0x040043C1 RID: 17345
		private static readonly QueryFilter HasAttachmentFilter = new NotFilter(HasAttachmentProperty.NotHasAttachmentFilter);
	}
}
