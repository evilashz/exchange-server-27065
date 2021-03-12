using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000044 RID: 68
	internal class SearchRecipient
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00014E48 File Offset: 0x00013048
		public SearchRecipient(ADRawEntry entry, ADRawEntry parent = null)
		{
			this.ADEntry = entry;
			this.Parent = parent;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00014E5E File Offset: 0x0001305E
		public static PropertyDefinition[] SearchProperties
		{
			get
			{
				return SearchRecipient.searchProperties;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00014E65 File Offset: 0x00013065
		public static PropertyDefinition[] DisplayProperties
		{
			get
			{
				return SearchRecipient.displayProperties;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00014E6C File Offset: 0x0001306C
		public static RecipientType[] RecipientTypes
		{
			get
			{
				return SearchRecipient.recipientTypes;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00014E73 File Offset: 0x00013073
		public static RecipientTypeDetails[] RecipientTypeDetail
		{
			get
			{
				return SearchRecipient.recipientTypesDetails;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00014E7A File Offset: 0x0001307A
		public static RecipientTypeDetails[] GroupRecipientTypeDetail
		{
			get
			{
				return SearchRecipient.groupRecipientTypesDetails;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00014E81 File Offset: 0x00013081
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00014E89 File Offset: 0x00013089
		public ADRawEntry Parent { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00014E92 File Offset: 0x00013092
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00014E9A File Offset: 0x0001309A
		public ADRawEntry ADEntry { get; set; }

		// Token: 0x06000331 RID: 817 RVA: 0x00014EA4 File Offset: 0x000130A4
		public static bool IsMembershipGroup(ADRawEntry recipient)
		{
			RecipientTypeDetails typeDetails = (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails];
			return SearchRecipient.IsMembershipGroupTypeDetail(typeDetails);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00014EDC File Offset: 0x000130DC
		public static bool IsMembershipGroupTypeDetail(RecipientTypeDetails typeDetails)
		{
			return SearchRecipient.GroupRecipientTypeDetail.Any((RecipientTypeDetails t) => t == typeDetails);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00014F0C File Offset: 0x0001310C
		public static bool IsPublicFolder(ADRawEntry recipient)
		{
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails];
			return recipientTypeDetails == RecipientTypeDetails.PublicFolder;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00014F34 File Offset: 0x00013134
		public static IEnumerable<QueryFilter> GetRecipientTypeFilter(bool includeGroups = false)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (RecipientTypeDetails recipientTypeDetails in SearchRecipient.RecipientTypeDetail)
			{
				if (recipientTypeDetails != RecipientTypeDetails.PublicFolderMailbox)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, recipientTypeDetails));
				}
			}
			if (includeGroups)
			{
				foreach (RecipientTypeDetails recipientTypeDetails2 in SearchRecipient.GroupRecipientTypeDetail)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, recipientTypeDetails2));
				}
			}
			return list;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00014FC4 File Offset: 0x000131C4
		public static QueryFilter GetRecipientTypeSearchFilter(string searchFilter, bool includeGroups = false)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if (!string.IsNullOrEmpty(searchFilter) && (!searchFilter.StartsWith("*") || !searchFilter.EndsWith("*") || searchFilter.Length > 2))
			{
				Guid empty = Guid.Empty;
				if (Guid.TryParse(searchFilter, out empty))
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, empty));
				}
				else
				{
					SmtpAddress smtpAddress = new SmtpAddress(searchFilter);
					if (smtpAddress.IsValidAddress)
					{
						list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "SMTP:" + smtpAddress.ToString()));
						list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ExternalEmailAddress, "SMTP:" + smtpAddress.ToString()));
					}
					else
					{
						list.Add(SearchRecipient.CreateComparisonFilter(ADUserSchema.UserPrincipalName, searchFilter));
						list.Add(SearchRecipient.CreateComparisonFilter(ADRecipientSchema.Alias, searchFilter));
						list.Add(SearchRecipient.CreateComparisonFilter(ADUserSchema.FirstName, searchFilter));
						list.Add(SearchRecipient.CreateComparisonFilter(ADUserSchema.LastName, searchFilter));
						list.Add(SearchRecipient.CreateComparisonFilter(ADRecipientSchema.DisplayName, searchFilter));
					}
				}
			}
			QueryFilter queryFilter = SearchRecipient.CombineFilters(SearchRecipient.GetRecipientTypeFilter(includeGroups));
			if (list.Count > 0)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					SearchRecipient.CombineFilters(list)
				});
			}
			return queryFilter;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00015124 File Offset: 0x00013324
		public static QueryFilter CombineFilters(IEnumerable<QueryFilter> orFilters)
		{
			return QueryFilter.AndTogether(new QueryFilter[]
			{
				new OrFilter(orFilters.ToArray<QueryFilter>())
			});
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001514C File Offset: 0x0001334C
		public static QueryFilter GetSourceFilter(SearchSource source)
		{
			switch (source.SourceType)
			{
			case SourceType.LegacyExchangeDN:
				if (source.CanBeCrossPremise)
				{
					return new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, source.ReferenceId),
						new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, "x500:" + source.ReferenceId)
					});
				}
				return new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, source.ReferenceId);
			case SourceType.Recipient:
				return SearchRecipient.SearchRecipientIdParameter.GetFilter(source.ReferenceId);
			case SourceType.MailboxGuid:
			{
				Guid guid;
				if (Guid.TryParse(source.ReferenceId, out guid))
				{
					return new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ExchangeGuid, guid);
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00015203 File Offset: 0x00013403
		public static bool IsWildcard(string s)
		{
			return !string.IsNullOrEmpty(s) && (s.StartsWith("*") || s.EndsWith("*"));
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001522A File Offset: 0x0001342A
		public static bool IsSuffixSearchWildcard(string s)
		{
			return !string.IsNullOrEmpty(s) && s.StartsWith("*") && s.Length > 1;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00015250 File Offset: 0x00013450
		private static QueryFilter CreateComparisonFilter(ADPropertyDefinition schemaProperty, string searchFilter)
		{
			if (SearchRecipient.IsWildcard(searchFilter))
			{
				string text = searchFilter.Substring(0, searchFilter.Length - 1);
				return new TextFilter(schemaProperty, text, MatchOptions.Prefix, MatchFlags.IgnoreCase);
			}
			return new ComparisonFilter(ComparisonOperator.Equal, schemaProperty, searchFilter);
		}

		// Token: 0x04000165 RID: 357
		private static PropertyDefinition[] searchProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion,
			ADObjectSchema.Id,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.ExternalEmailAddress,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientTypeDetails,
			ADUserSchema.ArchiveGuid,
			ADUserSchema.ArchiveDomain,
			ADUserSchema.ArchiveDatabaseRaw,
			ADUserSchema.ArchiveStatus,
			ADMailboxRecipientSchema.Database,
			ADMailboxRecipientSchema.ExchangeGuid,
			ADRecipientSchema.MasterAccountSid,
			ADRecipientSchema.RecipientTypeDetailsValue,
			IADSecurityPrincipalSchema.SamAccountName,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.RawCapabilities,
			ADPublicFolderSchema.EntryId,
			ADRecipientSchema.DefaultPublicFolderMailbox
		};

		// Token: 0x04000166 RID: 358
		private static PropertyDefinition[] displayProperties = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.ExternalEmailAddress,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientTypeDetails
		};

		// Token: 0x04000167 RID: 359
		private static RecipientType[] recipientTypes = new RecipientType[]
		{
			RecipientType.UserMailbox,
			RecipientType.MailUser,
			RecipientType.PublicDatabase
		};

		// Token: 0x04000168 RID: 360
		private static RecipientTypeDetails[] recipientTypesDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailUser,
			RecipientTypeDetails.UserMailbox,
			RecipientTypeDetails.SharedMailbox,
			RecipientTypeDetails.TeamMailbox,
			RecipientTypeDetails.RoomMailbox,
			RecipientTypeDetails.EquipmentMailbox,
			RecipientTypeDetails.PublicFolderMailbox,
			(RecipientTypeDetails)((ulong)int.MinValue),
			RecipientTypeDetails.RemoteRoomMailbox,
			RecipientTypeDetails.RemoteTeamMailbox,
			RecipientTypeDetails.RemoteSharedMailbox,
			RecipientTypeDetails.RemoteEquipmentMailbox,
			RecipientTypeDetails.LinkedMailbox,
			RecipientTypeDetails.LinkedRoomMailbox,
			RecipientTypeDetails.LegacyMailbox
		};

		// Token: 0x04000169 RID: 361
		private static RecipientTypeDetails[] groupRecipientTypesDetails = new RecipientTypeDetails[]
		{
			RecipientTypeDetails.MailUniversalDistributionGroup,
			RecipientTypeDetails.MailUniversalSecurityGroup,
			RecipientTypeDetails.MailNonUniversalGroup,
			RecipientTypeDetails.DynamicDistributionGroup,
			RecipientTypeDetails.UniversalDistributionGroup,
			RecipientTypeDetails.UniversalSecurityGroup,
			RecipientTypeDetails.NonUniversalGroup
		};

		// Token: 0x02000045 RID: 69
		[Serializable]
		private class SearchRecipientIdParameter : RecipientIdParameter
		{
			// Token: 0x0600033C RID: 828 RVA: 0x000154BC File Offset: 0x000136BC
			public static QueryFilter GetFilter(string identity)
			{
				ADObjectId propertyValue;
				if (ADIdParameter.TryResolveCanonicalName(identity, out propertyValue))
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, propertyValue);
				}
				return null;
			}
		}
	}
}
