using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200017A RID: 378
	internal static class CannedAddressListsFilterHelper
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x000404FC File Offset: 0x0003E6FC
		static CannedAddressListsFilterHelper()
		{
			CannedAddressListsFilterHelper.DefaultAllModernGroupsFilter = CannedAddressListsFilterHelper.CompleteRecipientFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.GroupMailbox));
			CannedAddressListsFilterHelper.InitializeOlderLdapFilter();
			CannedAddressListsFilterHelper.InitializeRecipientFilter();
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00040769 File Offset: 0x0003E969
		internal static Dictionary<string, QueryFilter> RecipientFiltersOfAddressList
		{
			get
			{
				return CannedAddressListsFilterHelper.recipientFiltersOfAddressList;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00040770 File Offset: 0x0003E970
		private static Dictionary<string, string[]> LdapFiltersOfOlderAddressList
		{
			get
			{
				return CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList;
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00040778 File Offset: 0x0003E978
		private static QueryFilter CompleteRecipientFilter(QueryFilter restrictions)
		{
			return new AndFilter(new QueryFilter[]
			{
				new ExistsFilter(ADRecipientSchema.Alias),
				restrictions
			});
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000407A4 File Offset: 0x0003E9A4
		private static void InitializeRecipientFilter()
		{
			CannedAddressListsFilterHelper.recipientFiltersOfAddressList = new Dictionary<string, QueryFilter>();
			CannedAddressListsFilterHelper.recipientFiltersOfAddressList.Add(CannedAddressListsFilterHelper.DefaultAllContacts, CannedAddressListsFilterHelper.DefaultAllContactsFilter);
			CannedAddressListsFilterHelper.recipientFiltersOfAddressList.Add(CannedAddressListsFilterHelper.DefaultAllDistributionLists, CannedAddressListsFilterHelper.DefaultAllDistributionListsFilter);
			CannedAddressListsFilterHelper.recipientFiltersOfAddressList.Add(CannedAddressListsFilterHelper.DefaultAllRooms, CannedAddressListsFilterHelper.DefaultAllRoomsFilter);
			CannedAddressListsFilterHelper.recipientFiltersOfAddressList.Add(CannedAddressListsFilterHelper.DefaultAllUsers, CannedAddressListsFilterHelper.DefaultAllUsersFilter);
			CannedAddressListsFilterHelper.recipientFiltersOfAddressList.Add(CannedAddressListsFilterHelper.DefaultAllModernGroups, CannedAddressListsFilterHelper.DefaultAllModernGroupsFilter);
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				CannedAddressListsFilterHelper.recipientFiltersOfAddressList.Add(CannedAddressListsFilterHelper.DefaultPublicFolders, CannedAddressListsFilterHelper.DefaultPublicFoldersFilter);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0004083C File Offset: 0x0003EA3C
		private static void InitializeOlderLdapFilter()
		{
			CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList = new Dictionary<string, string[]>();
			CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList.Add(CannedAddressListsFilterHelper.DefaultAllContacts, new string[]
			{
				"(& (mailnickname=*) (| (&(objectCategory=person)(objectClass=contact)) ))"
			});
			CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList.Add(CannedAddressListsFilterHelper.DefaultAllDistributionLists, new string[]
			{
				"(& (mailnickname=*) (| (objectCategory=group) ))"
			});
			CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList.Add(CannedAddressListsFilterHelper.DefaultAllRooms, new string[]
			{
				"(& (mailnickname=*) (| (msExchResourceMetaData=ResourceType:Room) ))",
				"(&(mailnickname=*)(msExchResourceMetaData=ResourceType:Room))"
			});
			CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList.Add(CannedAddressListsFilterHelper.DefaultAllUsers, new string[]
			{
				"(&(mailnickname=*)(|(&(objectCategory=person)(objectClass=user)(!(homeMDB=*))(!(msExchHomeServerName=*)))(&(objectCategory=person)(objectClass=user)(|(homeMDB=*)(msExchHomeServerName=*)))))"
			});
			CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList.Add(CannedAddressListsFilterHelper.DefaultAllModernGroups, new string[]
			{
				"(&(!(!objectClass=user)))(objectCategory=person)(mailNickname=*)(msExchHomeServerName=*)(msExchRecipientTypeDetails=1099511627776))"
			});
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				CannedAddressListsFilterHelper.ldapFiltersOfOlderAddressList.Add(CannedAddressListsFilterHelper.DefaultPublicFolders, new string[]
				{
					"(& (mailnickname=*) (| (objectCategory=publicFolder) ))"
				});
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00040924 File Offset: 0x0003EB24
		internal static Dictionary<QueryFilter, QueryFilter> GetFindFiltersForCannedAddressLists()
		{
			Dictionary<QueryFilter, QueryFilter> dictionary = new Dictionary<QueryFilter, QueryFilter>();
			foreach (KeyValuePair<string, QueryFilter> keyValuePair in CannedAddressListsFilterHelper.RecipientFiltersOfAddressList)
			{
				string key = keyValuePair.Key;
				QueryFilter value = keyValuePair.Value;
				dictionary.Add(CannedAddressListsFilterHelper.GetFindFilterForCannedAddressLists(key, value), value);
			}
			return dictionary;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00040998 File Offset: 0x0003EB98
		internal static QueryFilter GetFindFilterForCannedAddressLists(string name, QueryFilter recipientFilter)
		{
			string propertyValue = LdapFilterBuilder.LdapFilterFromQueryFilter(recipientFilter);
			return new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.LdapRecipientFilter, propertyValue),
				CannedAddressListsFilterHelper.GetFindFilterForOlderAddressList(name)
			});
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000409D4 File Offset: 0x0003EBD4
		private static QueryFilter GetFindFilterForOlderAddressList(string name)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (string propertyValue in CannedAddressListsFilterHelper.LdapFiltersOfOlderAddressList[name])
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.LdapRecipientFilter, propertyValue));
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00040A22 File Offset: 0x0003EC22
		internal static bool IsKnownException(Exception exception)
		{
			return exception is CannotDetermineExchangeModeException;
		}

		// Token: 0x040006A7 RID: 1703
		internal static string DefaultAllContacts = Strings.DefaultAllContacts;

		// Token: 0x040006A8 RID: 1704
		internal static string DefaultAllGroups = Strings.DefaultAllGroups;

		// Token: 0x040006A9 RID: 1705
		internal static string DefaultAllRooms = Strings.DefaultAllRooms;

		// Token: 0x040006AA RID: 1706
		internal static string DefaultAllUsers = Strings.DefaultAllUsers;

		// Token: 0x040006AB RID: 1707
		internal static string DefaultAllModernGroups = Strings.DefaultAllModernGroups;

		// Token: 0x040006AC RID: 1708
		internal static string DefaultPublicFolders = Strings.DefaultPublicFolders;

		// Token: 0x040006AD RID: 1709
		internal static string DefaultAllDistributionLists = Strings.DefaultAllDistributionLists;

		// Token: 0x040006AE RID: 1710
		internal static QueryFilter DefaultAllContactsFilter = CannedAddressListsFilterHelper.CompleteRecipientFilter(new AndFilter(new QueryFilter[]
		{
			new TextFilter(ADObjectSchema.ObjectCategory, "person", MatchOptions.FullString, MatchFlags.Default),
			new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "contact")
		}));

		// Token: 0x040006AF RID: 1711
		internal static QueryFilter DefaultAllDistributionListsFilter = CannedAddressListsFilterHelper.CompleteRecipientFilter(new TextFilter(ADObjectSchema.ObjectCategory, "group", MatchOptions.FullString, MatchFlags.Default));

		// Token: 0x040006B0 RID: 1712
		internal static QueryFilter DefaultAllRoomsFilter = CannedAddressListsFilterHelper.CompleteRecipientFilter(new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientDisplayType, RecipientDisplayType.ConferenceRoomMailbox),
			new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientDisplayType, RecipientDisplayType.SyncedConferenceRoomMailbox)
		}));

		// Token: 0x040006B1 RID: 1713
		internal static QueryFilter DefaultAllUsersFilter = CannedAddressListsFilterHelper.CompleteRecipientFilter(new AndFilter(new QueryFilter[]
		{
			new OrFilter(new QueryFilter[]
			{
				new AndFilter(new QueryFilter[]
				{
					new TextFilter(ADObjectSchema.ObjectCategory, "person", MatchOptions.FullString, MatchFlags.Default),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "user"),
					new NotFilter(new ExistsFilter(IADMailStorageSchema.Database)),
					new NotFilter(new ExistsFilter(IADMailStorageSchema.ServerLegacyDN))
				}),
				new AndFilter(new QueryFilter[]
				{
					new TextFilter(ADObjectSchema.ObjectCategory, "person", MatchOptions.FullString, MatchFlags.Default),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "user"),
					new OrFilter(new QueryFilter[]
					{
						new ExistsFilter(IADMailStorageSchema.Database),
						new ExistsFilter(IADMailStorageSchema.ServerLegacyDN)
					})
				})
			}),
			new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.GroupMailbox))
		}));

		// Token: 0x040006B2 RID: 1714
		internal static QueryFilter DefaultAllModernGroupsFilter;

		// Token: 0x040006B3 RID: 1715
		internal static QueryFilter DefaultPublicFoldersFilter = CannedAddressListsFilterHelper.CompleteRecipientFilter(new TextFilter(ADObjectSchema.ObjectCategory, "publicFolder", MatchOptions.FullString, MatchFlags.Default));

		// Token: 0x040006B4 RID: 1716
		private static Dictionary<string, QueryFilter> recipientFiltersOfAddressList;

		// Token: 0x040006B5 RID: 1717
		private static Dictionary<string, string[]> ldapFiltersOfOlderAddressList;
	}
}
