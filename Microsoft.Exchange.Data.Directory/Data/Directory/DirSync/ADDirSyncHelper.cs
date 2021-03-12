using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Data.Directory.DirSync
{
	// Token: 0x020001B0 RID: 432
	internal static class ADDirSyncHelper
	{
		// Token: 0x06001200 RID: 4608 RVA: 0x000577C4 File Offset: 0x000559C4
		internal static MultiValuedPropertyBase GetAddedRemovedLinks(ADPropertyDefinition propertyDefinition, SearchResultAttributeCollection attributeCollection, List<ADPropertyDefinition> rangedProperties, List<ValidationError> errors)
		{
			MultiValuedProperty<ADObjectId> links = ADDirSyncHelper.GetLinks(propertyDefinition, false, attributeCollection, rangedProperties, errors);
			MultiValuedProperty<ADObjectId> links2 = ADDirSyncHelper.GetLinks(propertyDefinition, true, attributeCollection, rangedProperties, errors);
			if (links != null || links2 != null)
			{
				List<ADDirSyncLink> list = new List<ADDirSyncLink>();
				ADDirSyncHelper.AddLinks(list, links, LinkState.Added, propertyDefinition);
				ADDirSyncHelper.AddLinks(list, links2, LinkState.Removed, propertyDefinition);
				return ADValueConvertor.CreateGenericMultiValuedProperty(propertyDefinition, true, list, ADDirSyncHelper.EmptyList, null);
			}
			return null;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00057820 File Offset: 0x00055A20
		internal static void AddLinks(List<ADDirSyncLink> result, MultiValuedProperty<ADObjectId> links, LinkState state, ADPropertyDefinition propertyDefinition)
		{
			if (links != null)
			{
				foreach (ADObjectId link in links)
				{
					result.Add(ADDirSyncHelper.CreateSyncLinks(link, state, propertyDefinition));
				}
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00057878 File Offset: 0x00055A78
		internal static MultiValuedProperty<ADObjectId> GetLinks(ADPropertyDefinition propertyDefinition, bool deleted, SearchResultAttributeCollection attributeCollection, List<ADPropertyDefinition> rangedProperties, List<ValidationError> errors)
		{
			int num = deleted ? 0 : 1;
			IntRange range = new IntRange(num, num);
			ADPropertyDefinition adpropertyDefinition = new ADPropertyDefinition(propertyDefinition.Name, propertyDefinition.VersionAdded, typeof(ADObjectId), propertyDefinition.LdapDisplayName, propertyDefinition.Flags | ADPropertyDefinitionFlags.MultiValued, propertyDefinition.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
			adpropertyDefinition = RangedPropertyHelper.CreateRangedProperty(adpropertyDefinition, range);
			DirectoryAttribute directoryAttribute = attributeCollection[adpropertyDefinition.LdapDisplayName];
			if (directoryAttribute != null)
			{
				return ADValueConvertor.GetValueFromDirectoryAttribute(null, directoryAttribute, adpropertyDefinition, true, rangedProperties, null, errors, null, false) as MultiValuedProperty<ADObjectId>;
			}
			return null;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00057900 File Offset: 0x00055B00
		internal static string GetDirSyncLinkAttribute(string ldapAttribute, bool deleted)
		{
			string text = deleted ? "0" : "1";
			return ADSession.GetAttributeNameWithRange(ldapAttribute, text, text);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00057925 File Offset: 0x00055B25
		internal static bool IsDirSyncLinkProperty(ADPropertyDefinition propertyDefinition)
		{
			return typeof(ADDirSyncLink).IsAssignableFrom(propertyDefinition.Type);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0005793C File Offset: 0x00055B3C
		internal static ADDirSyncResult CreateAndInitializeDirSyncResult(PropertyBag bag, ADDirSyncResult dummyInstance)
		{
			bag.SetField(ADObjectSchema.ObjectState, ObjectState.Unchanged);
			ADDirSyncResult addirSyncResult = dummyInstance.CreateInstance(bag);
			addirSyncResult.SetIsReadOnly(true);
			addirSyncResult.ResetChangeTracking(true);
			return addirSyncResult;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00057974 File Offset: 0x00055B74
		internal static bool ContainsProperty(PropertyBag propertyBag, ProviderPropertyDefinition property)
		{
			if (property.IsCalculated)
			{
				foreach (ProviderPropertyDefinition key in property.SupportingProperties)
				{
					if (!propertyBag.Contains(key))
					{
						return false;
					}
				}
				return true;
			}
			return propertyBag.Contains(property);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000579E0 File Offset: 0x00055BE0
		internal static IDictionary<ADPropertyDefinition, object> GetChangedProperties(ICollection properties, PropertyBag propertyBag)
		{
			Dictionary<ADPropertyDefinition, object> dictionary = new Dictionary<ADPropertyDefinition, object>(properties.Count);
			foreach (object obj in properties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)obj;
				if (ADDirSyncHelper.ContainsProperty(propertyBag, adpropertyDefinition))
				{
					dictionary[adpropertyDefinition] = propertyBag[adpropertyDefinition];
				}
			}
			return dictionary;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00057A54 File Offset: 0x00055C54
		private static ADDirSyncLink CreateSyncLinks(ADObjectId link, LinkState state, PropertyDefinition propertyDefinition)
		{
			Type type = propertyDefinition.Type;
			if (typeof(SyncLink) == type)
			{
				return new SyncLink(link, state);
			}
			return new ADDirSyncLink(link, state);
		}

		// Token: 0x04000A79 RID: 2681
		private const int DeletedLinkRangeBound = 0;

		// Token: 0x04000A7A RID: 2682
		private const int AddedLinkRangeBound = 1;

		// Token: 0x04000A7B RID: 2683
		internal static readonly IList EmptyList = new object[0];
	}
}
