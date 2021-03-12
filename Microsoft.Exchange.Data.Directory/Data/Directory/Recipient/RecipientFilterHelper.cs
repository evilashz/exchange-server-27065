using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200025F RID: 607
	internal static class RecipientFilterHelper
	{
		// Token: 0x06001D3B RID: 7483 RVA: 0x00079174 File Offset: 0x00077374
		internal static QueryFilter DiscoveryMailboxFilterForAuditLog(string serverLegDn)
		{
			if (RecipientFilterHelper.discoveryMailboxFilterForAuditLog == null)
			{
				RecipientFilterHelper.discoveryMailboxFilterForAuditLog = QueryFilter.AndTogether(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}"),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.ArbitrationMailbox),
					new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ServerLegacyDN, serverLegDn)
				});
			}
			return RecipientFilterHelper.discoveryMailboxFilterForAuditLog;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000791F0 File Offset: 0x000773F0
		internal static QueryFilter DiscoveryMailboxFilterUnifiedPolicy(ADObjectId databaseId)
		{
			return QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}"),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.ArbitrationMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Database, databaseId)
			});
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0007925C File Offset: 0x0007745C
		static RecipientFilterHelper()
		{
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap = new Dictionary<ADPropertyDefinition, string>();
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute1, ADRecipientSchema.CustomAttribute1.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute2, ADRecipientSchema.CustomAttribute2.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute3, ADRecipientSchema.CustomAttribute3.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute4, ADRecipientSchema.CustomAttribute4.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute5, ADRecipientSchema.CustomAttribute5.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute6, ADRecipientSchema.CustomAttribute6.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute7, ADRecipientSchema.CustomAttribute7.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute8, ADRecipientSchema.CustomAttribute8.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute9, ADRecipientSchema.CustomAttribute9.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute10, ADRecipientSchema.CustomAttribute10.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute11, ADRecipientSchema.CustomAttribute11.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute12, ADRecipientSchema.CustomAttribute12.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute13, ADRecipientSchema.CustomAttribute13.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute14, ADRecipientSchema.CustomAttribute14.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(ADDynamicGroupSchema.ConditionalCustomAttribute15, ADRecipientSchema.CustomAttribute15.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute1, ADRecipientSchema.CustomAttribute1.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute2, ADRecipientSchema.CustomAttribute2.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute3, ADRecipientSchema.CustomAttribute3.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute4, ADRecipientSchema.CustomAttribute4.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute5, ADRecipientSchema.CustomAttribute5.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute6, ADRecipientSchema.CustomAttribute6.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute7, ADRecipientSchema.CustomAttribute7.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute8, ADRecipientSchema.CustomAttribute8.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute9, ADRecipientSchema.CustomAttribute9.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute10, ADRecipientSchema.CustomAttribute10.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute11, ADRecipientSchema.CustomAttribute11.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute12, ADRecipientSchema.CustomAttribute12.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute13, ADRecipientSchema.CustomAttribute13.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute14, ADRecipientSchema.CustomAttribute14.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(AddressBookBaseSchema.ConditionalCustomAttribute15, ADRecipientSchema.CustomAttribute15.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute1, ADRecipientSchema.CustomAttribute1.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute2, ADRecipientSchema.CustomAttribute2.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute3, ADRecipientSchema.CustomAttribute3.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute4, ADRecipientSchema.CustomAttribute4.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute5, ADRecipientSchema.CustomAttribute5.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute6, ADRecipientSchema.CustomAttribute6.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute7, ADRecipientSchema.CustomAttribute7.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute8, ADRecipientSchema.CustomAttribute8.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute9, ADRecipientSchema.CustomAttribute9.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute10, ADRecipientSchema.CustomAttribute10.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute11, ADRecipientSchema.CustomAttribute11.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute12, ADRecipientSchema.CustomAttribute12.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute13, ADRecipientSchema.CustomAttribute13.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute14, ADRecipientSchema.CustomAttribute14.Name);
			RecipientFilterHelper.ConditionalToCustomAttributeNameMap.Add(EmailAddressPolicySchema.ConditionalCustomAttribute15, ADRecipientSchema.CustomAttribute15.Name);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00079874 File Offset: 0x00077A74
		private static QueryFilter ConstructSimpleComparisionFilters(ICollection<string> values, ADPropertyDefinition property)
		{
			if (values == null || values.Count == 0)
			{
				return null;
			}
			List<QueryFilter> list = new List<QueryFilter>(values.Count);
			foreach (string propertyValue in values)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, property, propertyValue));
			}
			if (1 != values.Count)
			{
				return new OrFilter(list.ToArray());
			}
			return list[0];
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000798F8 File Offset: 0x00077AF8
		private static List<QueryFilter> GetRecipientTypeFilter(WellKnownRecipientType? includeRecipient)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (WellKnownRecipientType.AllRecipients == includeRecipient.Value)
			{
				list.Add(new ExistsFilter(ADRecipientSchema.Alias));
			}
			else
			{
				if (WellKnownRecipientType.MailboxUsers == (WellKnownRecipientType.MailboxUsers & includeRecipient.Value))
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox));
				}
				if (WellKnownRecipientType.MailContacts == (WellKnownRecipientType.MailContacts & includeRecipient.Value))
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailContact));
				}
				if (WellKnownRecipientType.MailUsers == (WellKnownRecipientType.MailUsers & includeRecipient.Value))
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUser));
				}
				if (WellKnownRecipientType.MailGroups == (WellKnownRecipientType.MailGroups & includeRecipient.Value))
				{
					list.Add(new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUniversalDistributionGroup),
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUniversalSecurityGroup),
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailNonUniversalGroup),
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.DynamicDistributionGroup)
					}));
				}
				if (WellKnownRecipientType.Resources == (WellKnownRecipientType.Resources & includeRecipient.Value))
				{
					list.Add(new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
						new TextFilter(ADRecipientSchema.ResourceMetaData, "ResourceType" + ':', MatchOptions.Prefix, MatchFlags.IgnoreCase),
						new ExistsFilter(ADRecipientSchema.ResourceSearchProperties)
					}));
				}
			}
			return list;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00079A68 File Offset: 0x00077C68
		private static void PersistPrecannedRecipientFilter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition ldapFilter, bool isDynamicGroup)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			switch (RecipientFilterHelper.GetPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, ldapFilter, false, list))
			{
			case -1:
				break;
			case 0:
				propertyBag[filter] = string.Empty;
				propertyBag[ldapFilter] = string.Empty;
				return;
			default:
				if (list.Count > 0)
				{
					QueryFilter queryFilter = (list.Count > 1) ? new AndFilter(list.ToArray()) : list[0];
					if (isDynamicGroup)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							RecipientFilterHelper.ExcludingSystemMailboxFilter,
							RecipientFilterHelper.ExcludingCasMailboxFilter,
							RecipientFilterHelper.ExcludingMailboxPlanFilter,
							RecipientFilterHelper.ExcludingDiscoveryMailboxFilter,
							RecipientFilterHelper.ExcludingPublicFolderMailboxFilter,
							RecipientFilterHelper.ExcludingArbitrationMailboxFilter,
							RecipientFilterHelper.ExcludingAuditLogMailboxFilter
						});
					}
					propertyBag[filter] = queryFilter.GenerateInfixString(FilterLanguage.Monad);
					propertyBag[ldapFilter] = LdapFilterBuilder.LdapFilterFromQueryFilter(queryFilter);
				}
				break;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00079B54 File Offset: 0x00077D54
		private static int GetPrecannedRecipientFilter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition ldapFilter, bool validationOnly, List<QueryFilter> conditions)
		{
			WellKnownRecipientType? wellKnownRecipientType = (WellKnownRecipientType?)RecipientFilterHelper.IncludeRecipientGetter(propertyBag, filterMeatadata, filter);
			bool flag = wellKnownRecipientType == null || wellKnownRecipientType == WellKnownRecipientType.None;
			if (!flag && validationOnly)
			{
				return 1;
			}
			MultiValuedProperty<string> conditionVal = (MultiValuedProperty<string>)RecipientFilterHelper.DepartmentGetter(propertyBag, filterMeatadata, filter, null);
			if (!RecipientFilterHelper.MergeConditionFilter(conditionVal, ADOrgPersonSchema.Department, conditions, flag, validationOnly))
			{
				return -1;
			}
			MultiValuedProperty<string> conditionVal2 = (MultiValuedProperty<string>)RecipientFilterHelper.CompanyGetter(propertyBag, filterMeatadata, filter, null);
			if (!RecipientFilterHelper.MergeConditionFilter(conditionVal2, ADOrgPersonSchema.Company, conditions, flag, validationOnly))
			{
				return -1;
			}
			MultiValuedProperty<string> conditionVal3 = (MultiValuedProperty<string>)RecipientFilterHelper.StateOrProvinceGetter(propertyBag, filterMeatadata, filter, null);
			if (!RecipientFilterHelper.MergeConditionFilter(conditionVal3, ADOrgPersonSchema.StateOrProvince, conditions, flag, validationOnly))
			{
				return -1;
			}
			foreach (ADPropertyDefinition adpropertyDefinition in RecipientFilterHelper.allCustomAttributePropertyDefinition)
			{
				MultiValuedProperty<string> conditionVal4 = (MultiValuedProperty<string>)RecipientFilterHelper.InternalStringValuesGetter(propertyBag, filterMeatadata, filter, null, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:" + adpropertyDefinition.Name + "=");
				if (!RecipientFilterHelper.MergeConditionFilter(conditionVal4, adpropertyDefinition, conditions, flag, validationOnly))
				{
					return -1;
				}
			}
			if (wellKnownRecipientType == WellKnownRecipientType.None)
			{
				return 0;
			}
			if (validationOnly)
			{
				return 1;
			}
			if (wellKnownRecipientType != null)
			{
				List<QueryFilter> recipientTypeFilter = RecipientFilterHelper.GetRecipientTypeFilter(wellKnownRecipientType);
				if (recipientTypeFilter.Count > 1)
				{
					conditions.Add(new OrFilter(recipientTypeFilter.ToArray()));
				}
				else
				{
					if (recipientTypeFilter.Count != 1)
					{
						return -1;
					}
					conditions.Add(recipientTypeFilter[0]);
				}
			}
			return 1;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00079CD4 File Offset: 0x00077ED4
		private static bool MergeConditionFilter(MultiValuedProperty<string> conditionVal, ADPropertyDefinition conditionDef, List<QueryFilter> conditions, bool noRecipients, bool validationOnly)
		{
			if (conditionVal != null && conditionVal.Count > 0)
			{
				if (noRecipients)
				{
					return false;
				}
				if (!validationOnly)
				{
					QueryFilter queryFilter = RecipientFilterHelper.ConstructSimpleComparisionFilters(conditionVal, conditionDef);
					if (queryFilter != null)
					{
						conditions.Add(queryFilter);
					}
				}
			}
			return true;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00079D09 File Offset: 0x00077F09
		private static bool IsValidRecipientFilterMetadata(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition includedRecipients)
		{
			return 0 <= RecipientFilterHelper.GetPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, null, true, null);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00079D1C File Offset: 0x00077F1C
		internal static ValidationError ValidatePrecannedRecipientFilter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition includedRecipients, ObjectId id)
		{
			if (!RecipientFilterHelper.IsValidRecipientFilterMetadata(propertyBag, filterMeatadata, filter, includedRecipients))
			{
				return new ObjectValidationError(DirectoryStrings.ErrorNullRecipientTypeInPrecannedFilter(includedRecipients.Name), id, string.Empty);
			}
			return null;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00079D44 File Offset: 0x00077F44
		internal static object IncludeRecipientGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter)
		{
			WellKnownRecipientType? wellKnownRecipientType = null;
			if (WellKnownRecipientFilterType.Precanned == (WellKnownRecipientFilterType)RecipientFilterHelper.RecipientFilterTypeGetter(propertyBag, filterMeatadata, filter))
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[filterMeatadata];
				wellKnownRecipientType = new WellKnownRecipientType?(WellKnownRecipientType.None);
				foreach (string text in multiValuedProperty)
				{
					if (text.StartsWith("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:IncludedRecipients=", StringComparison.OrdinalIgnoreCase))
					{
						int value;
						if (int.TryParse(text.Substring("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:IncludedRecipients=".Length), out value))
						{
							wellKnownRecipientType = new WellKnownRecipientType?((WellKnownRecipientType)value);
							break;
						}
						wellKnownRecipientType = null;
						break;
					}
				}
			}
			return wellKnownRecipientType;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00079DFC File Offset: 0x00077FFC
		internal static void IncludeRecipientSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition ldapFilter, bool isDynamicGroup)
		{
			RecipientFilterHelper.InternalStringValuesSetter(new MultiValuedProperty<string>(((int)((WellKnownRecipientType?)(value ?? WellKnownRecipientType.None)).Value).ToString()), propertyBag, filterMeatadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:IncludedRecipients=");
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Precanned, propertyBag, filterMeatadata);
			RecipientFilterHelper.PersistPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, ldapFilter, isDynamicGroup);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00079E50 File Offset: 0x00078050
		private static object InternalStringValuesGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition filterPropertyDefinition, string filterPrefix)
		{
			MultiValuedProperty<string> result = null;
			if (WellKnownRecipientFilterType.Precanned == (WellKnownRecipientFilterType)RecipientFilterHelper.RecipientFilterTypeGetter(propertyBag, filterMeatadata, filter))
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[filterMeatadata];
				Collection<string> collection = new Collection<string>();
				foreach (string text in multiValuedProperty)
				{
					if (text.StartsWith(filterPrefix, StringComparison.OrdinalIgnoreCase) && !text.Equals(filterPrefix, StringComparison.OrdinalIgnoreCase))
					{
						collection.Add(text.Substring(filterPrefix.Length));
					}
				}
				result = new MultiValuedProperty<string>(multiValuedProperty.IsReadOnly, filterPropertyDefinition, collection);
			}
			return result;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00079EF8 File Offset: 0x000780F8
		private static void InternalStringValuesSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, string filterPrefix)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[filterMeatadata];
			RecipientFilterHelper.ClearNonExchange12RecipientFilterMetadata(multiValuedProperty);
			int num = multiValuedProperty.Count - 1;
			while (0 <= num)
			{
				if (multiValuedProperty[num].StartsWith(filterPrefix, StringComparison.OrdinalIgnoreCase))
				{
					multiValuedProperty.RemoveAt(num);
				}
				num--;
			}
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)value;
			if (multiValuedProperty2 != null && multiValuedProperty2.Count != 0)
			{
				foreach (string text in multiValuedProperty2)
				{
					if (!string.IsNullOrEmpty(text))
					{
						multiValuedProperty.Add(filterPrefix + text);
					}
				}
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00079FA4 File Offset: 0x000781A4
		internal static object DepartmentGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition filterPropertyDefinition)
		{
			return RecipientFilterHelper.InternalStringValuesGetter(propertyBag, filterMeatadata, filter, filterPropertyDefinition, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:Department=");
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00079FB4 File Offset: 0x000781B4
		internal static void DepartmentSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition ldapFilter, bool isDynamicGroup)
		{
			RecipientFilterHelper.InternalStringValuesSetter(value, propertyBag, filterMeatadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:Department=");
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Precanned, propertyBag, filterMeatadata);
			RecipientFilterHelper.PersistPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, ldapFilter, isDynamicGroup);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00079FD7 File Offset: 0x000781D7
		internal static object CustomAttributeGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition filterPropertyDefinition)
		{
			return RecipientFilterHelper.InternalStringValuesGetter(propertyBag, filterMeatadata, filter, filterPropertyDefinition, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:" + RecipientFilterHelper.ConditionalToCustomAttributeNameMap[filterPropertyDefinition] + "=");
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00079FFC File Offset: 0x000781FC
		internal static void CustomAttributeSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition filterPropertyDefinition, ADPropertyDefinition ldapFilter, bool isDynamicGroup)
		{
			RecipientFilterHelper.InternalStringValuesSetter(value, propertyBag, filterMeatadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:" + RecipientFilterHelper.ConditionalToCustomAttributeNameMap[filterPropertyDefinition] + "=");
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Precanned, propertyBag, filterMeatadata);
			RecipientFilterHelper.PersistPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, ldapFilter, isDynamicGroup);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x0007A035 File Offset: 0x00078235
		internal static object CompanyGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition filterPropertyDefinition)
		{
			return RecipientFilterHelper.InternalStringValuesGetter(propertyBag, filterMeatadata, filter, filterPropertyDefinition, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:Company=");
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x0007A045 File Offset: 0x00078245
		internal static void CompanySetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition ldapFilter, bool isDynamicGroup)
		{
			RecipientFilterHelper.InternalStringValuesSetter(value, propertyBag, filterMeatadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:Company=");
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Precanned, propertyBag, filterMeatadata);
			RecipientFilterHelper.PersistPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, ldapFilter, isDynamicGroup);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x0007A068 File Offset: 0x00078268
		internal static object StateOrProvinceGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition filterPropertyDefinition)
		{
			return RecipientFilterHelper.InternalStringValuesGetter(propertyBag, filterMeatadata, filter, filterPropertyDefinition, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:StateOrProvincePrefix=");
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0007A078 File Offset: 0x00078278
		internal static void StateOrProvinceSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter, ADPropertyDefinition ldapFilter, bool isDynamicGroup)
		{
			RecipientFilterHelper.InternalStringValuesSetter(value, propertyBag, filterMeatadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:StateOrProvincePrefix=");
			RecipientFilterHelper.SetRecipientFilterType(WellKnownRecipientFilterType.Precanned, propertyBag, filterMeatadata);
			RecipientFilterHelper.PersistPrecannedRecipientFilter(propertyBag, filterMeatadata, filter, ldapFilter, isDynamicGroup);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0007A09C File Offset: 0x0007829C
		internal static object RecipientFilterTypeGetter(IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata, ADPropertyDefinition filter)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[filterMeatadata];
			string text = (string)propertyBag[filter];
			WellKnownRecipientFilterType wellKnownRecipientFilterType;
			if (((ExchangeObjectVersion)propertyBag[ADObjectSchema.ExchangeVersion]).IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				wellKnownRecipientFilterType = WellKnownRecipientFilterType.Legacy;
			}
			else
			{
				wellKnownRecipientFilterType = WellKnownRecipientFilterType.Unknown;
				int num = multiValuedProperty.Count - 1;
				while (0 <= num)
				{
					if (multiValuedProperty[num].StartsWith("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:RecipientFilterType=", StringComparison.OrdinalIgnoreCase))
					{
						int num2;
						if (!int.TryParse(multiValuedProperty[num].Substring("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:RecipientFilterType=".Length), out num2))
						{
							wellKnownRecipientFilterType = WellKnownRecipientFilterType.Unknown;
							break;
						}
						wellKnownRecipientFilterType = (WellKnownRecipientFilterType)num2;
					}
					else if (0 < multiValuedProperty[num].Length && !multiValuedProperty[num].StartsWith("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:"))
					{
						wellKnownRecipientFilterType = WellKnownRecipientFilterType.Unknown;
						break;
					}
					num--;
				}
			}
			return wellKnownRecipientFilterType;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0007A160 File Offset: 0x00078360
		internal static void SetRecipientFilterType(WellKnownRecipientFilterType type, IPropertyBag propertyBag, ADPropertyDefinition filterMeatadata)
		{
			if (WellKnownRecipientFilterType.Custom == type)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[filterMeatadata];
				if (0 < multiValuedProperty.Count)
				{
					multiValuedProperty.Clear();
				}
			}
			int num = (int)type;
			RecipientFilterHelper.InternalStringValuesSetter(new MultiValuedProperty<string>(num.ToString()), propertyBag, filterMeatadata, "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:RecipientFilterType=");
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0007A1A8 File Offset: 0x000783A8
		private static void ClearNonExchange12RecipientFilterMetadata(MultiValuedProperty<string> purportedSearchUI)
		{
			int num = purportedSearchUI.Count - 1;
			while (0 <= num)
			{
				if (!purportedSearchUI[num].StartsWith("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:", StringComparison.OrdinalIgnoreCase))
				{
					purportedSearchUI.RemoveAt(num);
				}
				num--;
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0007A1E3 File Offset: 0x000783E3
		internal static object RecipientFilterAppliedGetter(IPropertyBag propertyBag, ADPropertyDefinition filterFlags)
		{
			return RecipientFilterableObjectFlags.FilterApplied == (RecipientFilterableObjectFlags.FilterApplied & (RecipientFilterableObjectFlags)propertyBag[filterFlags]);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0007A1FB File Offset: 0x000783FB
		internal static void RecipientFilterAppliedSetter(object value, IPropertyBag propertyBag, ADPropertyDefinition filterFlags)
		{
			if ((bool)value)
			{
				propertyBag[filterFlags] = (RecipientFilterableObjectFlags.FilterApplied | (RecipientFilterableObjectFlags)propertyBag[filterFlags]);
				return;
			}
			propertyBag[filterFlags] = (~RecipientFilterableObjectFlags.FilterApplied & (RecipientFilterableObjectFlags)propertyBag[filterFlags]);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0007A23C File Offset: 0x0007843C
		internal static RecipientTypeDetails RecipientTypeDetailsValueFromTextFilter(TextFilter filter)
		{
			if (filter.MatchOptions != MatchOptions.SubString)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(filter.Property.Name, filter.MatchOptions.ToString()));
			}
			RecipientTypeDetails result;
			try
			{
				result = (RecipientTypeDetails)Enum.Parse(typeof(RecipientTypeDetails), filter.Text);
			}
			catch (ArgumentException)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValue(ADRecipientSchema.RecipientTypeDetails.Name, filter.Text));
			}
			return result;
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0007A2C4 File Offset: 0x000784C4
		internal static bool FixExchange12RecipientFilterMetadata(IPropertyBag propertyBag, ADPropertyDefinition objectVersionProperty, ADPropertyDefinition e2003MetadataProperty, ADPropertyDefinition e12MetadataProperty, string ldapRecipientFilter)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[e2003MetadataProperty];
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[e12MetadataProperty];
			bool flag = false;
			if (ExchangeObjectVersion.Exchange2007.IsSameVersion((ExchangeObjectVersion)propertyBag[objectVersionProperty]))
			{
				if (multiValuedProperty2.Count == 0)
				{
					foreach (string text in multiValuedProperty)
					{
						if (text.StartsWith("Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:", StringComparison.OrdinalIgnoreCase))
						{
							multiValuedProperty2.Add(text);
							flag = true;
						}
					}
				}
				flag |= RecipientFilterHelper.StampE2003FilterMetadata(propertyBag, ldapRecipientFilter, e2003MetadataProperty);
			}
			return flag;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0007A36C File Offset: 0x0007856C
		internal static bool StampE2003FilterMetadata(IPropertyBag propertyBag, string ldapRecipientFilter, ADPropertyDefinition purportedSearchUIDefinition)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[purportedSearchUIDefinition];
			bool flag;
			if (string.IsNullOrEmpty(ldapRecipientFilter))
			{
				flag = (0 != multiValuedProperty.Count);
				if (flag)
				{
					multiValuedProperty.Clear();
				}
			}
			else
			{
				string[] array = new string[]
				{
					"CommonQuery_Handler=5EE6238AC231D011891C00A024AB2DBB",
					"CommonQuery_Form=E33FEE83D957D011B93200A024AB2DBB",
					"DsQuery_ViewMode=4868",
					"DsQuery_EnableFilter=0",
					"Microsoft.PropertyWell_Items=0",
					"Microsoft.PropertyWell_QueryString=" + ldapRecipientFilter
				};
				flag = (multiValuedProperty.Count != array.Length);
				if (!flag)
				{
					foreach (string item in array)
					{
						if (!multiValuedProperty.Contains(item))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					multiValuedProperty.Clear();
					foreach (string item2 in array)
					{
						multiValuedProperty.Add(item2);
					}
				}
			}
			return flag;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0007A460 File Offset: 0x00078660
		internal static bool IsRecipientFilterPropertiesModified(ADObject adObject, bool isChanged)
		{
			ISupportRecipientFilter supportRecipientFilter = (ISupportRecipientFilter)adObject;
			ADPropertyDefinition[] array = new ADPropertyDefinition[]
			{
				supportRecipientFilter.RecipientFilterSchema,
				supportRecipientFilter.LdapRecipientFilterSchema,
				supportRecipientFilter.IncludedRecipientsSchema,
				supportRecipientFilter.ConditionalDepartmentSchema,
				supportRecipientFilter.ConditionalCompanySchema,
				supportRecipientFilter.ConditionalStateOrProvinceSchema,
				supportRecipientFilter.ConditionalCustomAttribute1Schema,
				supportRecipientFilter.ConditionalCustomAttribute2Schema,
				supportRecipientFilter.ConditionalCustomAttribute3Schema,
				supportRecipientFilter.ConditionalCustomAttribute4Schema,
				supportRecipientFilter.ConditionalCustomAttribute5Schema,
				supportRecipientFilter.ConditionalCustomAttribute6Schema,
				supportRecipientFilter.ConditionalCustomAttribute7Schema,
				supportRecipientFilter.ConditionalCustomAttribute8Schema,
				supportRecipientFilter.ConditionalCustomAttribute9Schema,
				supportRecipientFilter.ConditionalCustomAttribute10Schema,
				supportRecipientFilter.ConditionalCustomAttribute11Schema,
				supportRecipientFilter.ConditionalCustomAttribute12Schema,
				supportRecipientFilter.ConditionalCustomAttribute13Schema,
				supportRecipientFilter.ConditionalCustomAttribute14Schema,
				supportRecipientFilter.ConditionalCustomAttribute15Schema
			};
			foreach (ADPropertyDefinition providerPropertyDefinition in array)
			{
				if (isChanged)
				{
					if (adObject.IsChanged(providerPropertyDefinition))
					{
						return true;
					}
				}
				else if (adObject.IsModified(providerPropertyDefinition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000DE7 RID: 3559
		internal const string RecipientFilterMetadataPrefix = "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:";

		// Token: 0x04000DE8 RID: 3560
		internal const string RecipientFilterTypePrefix = "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:RecipientFilterType=";

		// Token: 0x04000DE9 RID: 3561
		internal const string IncludeRecipientPrefix = "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:IncludedRecipients=";

		// Token: 0x04000DEA RID: 3562
		internal const string DepartmentPrefix = "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:Department=";

		// Token: 0x04000DEB RID: 3563
		internal const string CompanyPrefix = "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:Company=";

		// Token: 0x04000DEC RID: 3564
		internal const string StateOrProvincePrefix = "Microsoft.Exchange12.8f91d340bc0c47e4b4058a479602f94c:StateOrProvincePrefix=";

		// Token: 0x04000DED RID: 3565
		internal static readonly QueryFilter ExcludingSystemMailboxFilter = new NotFilter(new TextFilter(ADObjectSchema.Name, "SystemMailbox{", MatchOptions.Prefix, MatchFlags.IgnoreCase));

		// Token: 0x04000DEE RID: 3566
		internal static readonly QueryFilter ExcludingCasMailboxFilter = new NotFilter(new TextFilter(ADObjectSchema.Name, "CAS_{", MatchOptions.Prefix, MatchFlags.IgnoreCase));

		// Token: 0x04000DEF RID: 3567
		internal static readonly QueryFilter ExcludingArbitrationMailboxFilter = new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.ArbitrationMailbox));

		// Token: 0x04000DF0 RID: 3568
		internal static readonly QueryFilter ExcludingPublicFolderMailboxFilter = new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.PublicFolderMailbox));

		// Token: 0x04000DF1 RID: 3569
		internal static readonly QueryFilter MailboxPlanFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MailboxPlan);

		// Token: 0x04000DF2 RID: 3570
		internal static readonly QueryFilter ExcludingMailboxPlanFilter = new NotFilter(RecipientFilterHelper.MailboxPlanFilter);

		// Token: 0x04000DF3 RID: 3571
		internal static readonly QueryFilter ExcludingLinkedUserFilter = new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.LinkedUser));

		// Token: 0x04000DF4 RID: 3572
		internal static readonly QueryFilter DiscoveryMailboxFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.DiscoveryMailbox);

		// Token: 0x04000DF5 RID: 3573
		internal static readonly QueryFilter ExcludingAuditLogMailboxFilter = new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.AuditLogMailbox));

		// Token: 0x04000DF6 RID: 3574
		private static QueryFilter discoveryMailboxFilterForAuditLog;

		// Token: 0x04000DF7 RID: 3575
		internal static readonly QueryFilter ExcludingDiscoveryMailboxFilter = new NotFilter(RecipientFilterHelper.DiscoveryMailboxFilter);

		// Token: 0x04000DF8 RID: 3576
		private static readonly Dictionary<ADPropertyDefinition, string> ConditionalToCustomAttributeNameMap;

		// Token: 0x04000DF9 RID: 3577
		private static ADPropertyDefinition[] allCustomAttributePropertyDefinition = new ADPropertyDefinition[]
		{
			ADRecipientSchema.CustomAttribute1,
			ADRecipientSchema.CustomAttribute2,
			ADRecipientSchema.CustomAttribute3,
			ADRecipientSchema.CustomAttribute4,
			ADRecipientSchema.CustomAttribute5,
			ADRecipientSchema.CustomAttribute6,
			ADRecipientSchema.CustomAttribute7,
			ADRecipientSchema.CustomAttribute8,
			ADRecipientSchema.CustomAttribute9,
			ADRecipientSchema.CustomAttribute10,
			ADRecipientSchema.CustomAttribute11,
			ADRecipientSchema.CustomAttribute12,
			ADRecipientSchema.CustomAttribute13,
			ADRecipientSchema.CustomAttribute14,
			ADRecipientSchema.CustomAttribute15
		};
	}
}
