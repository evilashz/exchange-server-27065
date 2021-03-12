using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C74 RID: 3188
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal abstract class IdProperty : SmartPropertyDefinition
	{
		// Token: 0x06007004 RID: 28676 RVA: 0x001F02BE File Offset: 0x001EE4BE
		protected internal IdProperty(string displayName, Type valueType, PropertyFlags flags, PropertyDefinitionConstraint[] constraints, params PropertyDependency[] dependencies) : base(displayName, valueType, flags, constraints, dependencies)
		{
		}

		// Token: 0x06007005 RID: 28677 RVA: 0x001F02D0 File Offset: 0x001EE4D0
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyStore)
		{
			StoreObjectId storeObjectId = null;
			ICoreObject coreObject = propertyStore.Context.CoreObject;
			if (coreObject != null)
			{
				storeObjectId = coreObject.InternalStoreObjectId;
			}
			if (storeObjectId == null)
			{
				byte[] array = propertyStore.GetValue(InternalSchema.EntryId) as byte[];
				if (array != null)
				{
					storeObjectId = StoreObjectId.FromProviderSpecificId(array, this.GetStoreObjectType(propertyStore));
				}
				else
				{
					QueryResultPropertyBag queryResultPropertyBag = ((PropertyBag)propertyStore) as QueryResultPropertyBag;
					if (queryResultPropertyBag != null && (!((IDirectPropertyBag)queryResultPropertyBag).IsLoaded(InternalSchema.RowType) || object.Equals(queryResultPropertyBag.TryGetProperty(InternalSchema.RowType), 1)))
					{
						ExDiagnostics.FailFast(string.Format("EntryId: \"{0}\" in view", queryResultPropertyBag.TryGetProperty(InternalSchema.EntryId)), false);
					}
				}
			}
			if (storeObjectId == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			VersionedId versionedId = new VersionedId(storeObjectId, this.GetChangeKey(propertyStore));
			if (!this.IsCompatibleId(versionedId, coreObject))
			{
				return new PropertyError(this, PropertyErrorCode.NotSupported);
			}
			return versionedId;
		}

		// Token: 0x06007006 RID: 28678 RVA: 0x001F039C File Offset: 0x001EE59C
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return IdProperty.SmartIdFilterToNativeIdFilter(filter, this, InternalSchema.EntryId);
		}

		// Token: 0x06007007 RID: 28679 RVA: 0x001F03AC File Offset: 0x001EE5AC
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			ComparisonFilter comparisonFilter = (ComparisonFilter)IdProperty.NativeIdFilterToSmartIdFilter(filter, this, InternalSchema.EntryId);
			if (comparisonFilter != null && !this.IsCompatibleId((StoreObjectId)comparisonFilter.PropertyValue, null))
			{
				return null;
			}
			return comparisonFilter;
		}

		// Token: 0x06007008 RID: 28680 RVA: 0x001F03E5 File Offset: 0x001EE5E5
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06007009 RID: 28681 RVA: 0x001F03F7 File Offset: 0x001EE5F7
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x0600700A RID: 28682 RVA: 0x001F03FA File Offset: 0x001EE5FA
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.EntryId;
		}

		// Token: 0x0600700B RID: 28683 RVA: 0x001F0404 File Offset: 0x001EE604
		internal static QueryFilter SmartIdFilterToNativeIdFilter(SinglePropertyFilter filter, SmartPropertyDefinition smartProperty, PropertyDefinition nativeProperty)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null || !comparisonFilter.Property.Equals(smartProperty))
			{
				throw smartProperty.CreateInvalidFilterConversionException(filter);
			}
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && comparisonFilter.ComparisonOperator != ComparisonOperator.NotEqual)
			{
				throw smartProperty.CreateInvalidFilterConversionException(filter);
			}
			StoreId id = (StoreId)comparisonFilter.PropertyValue;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, nativeProperty, StoreId.GetStoreObjectId(id).ProviderLevelItemId);
		}

		// Token: 0x0600700C RID: 28684 RVA: 0x001F0470 File Offset: 0x001EE670
		internal static QueryFilter NativeIdFilterToSmartIdFilter(QueryFilter filter, SmartPropertyDefinition smartProperty, PropertyDefinition nativeProperty)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null || !comparisonFilter.Property.Equals(nativeProperty))
			{
				return null;
			}
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && comparisonFilter.ComparisonOperator != ComparisonOperator.NotEqual)
			{
				throw new CorruptDataException(ServerStrings.ExComparisonOperatorNotSupportedForProperty(comparisonFilter.ComparisonOperator.ToString(), smartProperty.Name));
			}
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, smartProperty, StoreObjectId.FromProviderSpecificId((byte[])comparisonFilter.PropertyValue, StoreObjectType.Unknown));
		}

		// Token: 0x0600700D RID: 28685 RVA: 0x001F04E6 File Offset: 0x001EE6E6
		internal StoreObjectType GetStoreObjectType(PropertyBag propertyBag)
		{
			return this.GetStoreObjectType((PropertyBag.BasicPropertyStore)propertyBag);
		}

		// Token: 0x0600700E RID: 28686
		protected abstract StoreObjectType GetStoreObjectType(PropertyBag.BasicPropertyStore propertyBag);

		// Token: 0x0600700F RID: 28687 RVA: 0x001F04F4 File Offset: 0x001EE6F4
		protected virtual byte[] GetChangeKey(PropertyBag.BasicPropertyStore propertyBag)
		{
			return (propertyBag.GetValue(InternalSchema.ChangeKey) as byte[]) ?? Array<byte>.Empty;
		}

		// Token: 0x06007010 RID: 28688
		protected abstract bool IsCompatibleId(StoreId id, ICoreObject coreObject);
	}
}
