using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200023E RID: 574
	internal static class RBACHelper
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0004B551 File Offset: 0x00049751
		internal static QueryFilter ScriptEnabledRoleEntryTypeFilter
		{
			get
			{
				return RBACHelper.scriptEnabledRoleEntryTypeFilter;
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004B558 File Offset: 0x00049758
		internal static QueryFilter ConstructRoleEntryFilter(string name, ManagementRoleEntryType type)
		{
			return RBACHelper.ConstructRoleEntryFilter(name, type, null);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0004B564 File Offset: 0x00049764
		internal static QueryFilter ConstructRoleEntryFilter(string name, ManagementRoleEntryType type, string snapinName)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			char value;
			switch (type)
			{
			case ManagementRoleEntryType.Cmdlet:
				value = 'c';
				goto IL_52;
			case ManagementRoleEntryType.Script:
				value = 's';
				goto IL_52;
			case ManagementRoleEntryType.Cmdlet | ManagementRoleEntryType.Script:
				break;
			case ManagementRoleEntryType.ApplicationPermission:
				value = 'a';
				goto IL_52;
			default:
				if (type == ManagementRoleEntryType.WebService)
				{
					value = 'w';
					goto IL_52;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_52:
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Append(',');
			stringBuilder.Append(name);
			if (type == ManagementRoleEntryType.Cmdlet && !string.IsNullOrEmpty(snapinName))
			{
				stringBuilder.Append(',');
				stringBuilder.Append(snapinName);
			}
			stringBuilder.Append(',');
			stringBuilder.Append('*');
			stringBuilder.Replace('?', '*');
			return RBACHelper.ConstructRoleEntryFilter(stringBuilder.ToString());
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004B628 File Offset: 0x00049828
		internal static QueryFilter ConstructRoleEntryParameterFilter(string[] parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			List<QueryFilter> list = new List<QueryFilter>(parameters.Length);
			foreach (string value in parameters)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('*');
				stringBuilder.Append(',');
				stringBuilder.Append(value);
				stringBuilder.Append('*');
				stringBuilder.Replace('?', '*');
				list.Add(RBACHelper.ConstructRoleEntryFilter(stringBuilder.ToString()));
			}
			return new AndFilter(list.ToArray());
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004B6B8 File Offset: 0x000498B8
		internal static bool DoesRoleEntryMatchNameAndParameters(RoleEntry roleEntry, ManagementRoleEntryType type, string name, string[] parameters, string snapinName)
		{
			if (null == roleEntry)
			{
				throw new ArgumentNullException("roleEntry");
			}
			ManagementRoleEntryType managementRoleEntryType = (ManagementRoleEntryType)0;
			if (roleEntry is CmdletRoleEntry)
			{
				managementRoleEntryType = ManagementRoleEntryType.Cmdlet;
			}
			else if (roleEntry is ScriptRoleEntry)
			{
				managementRoleEntryType = ManagementRoleEntryType.Script;
			}
			else if (roleEntry is ApplicationPermissionRoleEntry)
			{
				managementRoleEntryType = ManagementRoleEntryType.ApplicationPermission;
			}
			else if (roleEntry is WebServiceRoleEntry)
			{
				managementRoleEntryType = ManagementRoleEntryType.WebService;
			}
			if ((type & managementRoleEntryType) == (ManagementRoleEntryType)0 && type != ManagementRoleEntryType.All)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(snapinName))
			{
				if (managementRoleEntryType != ManagementRoleEntryType.Cmdlet)
				{
					return false;
				}
				Regex regex = new Regex(Wildcard.ConvertToRegexPattern(snapinName), RegexOptions.IgnoreCase);
				if (!regex.IsMatch(((CmdletRoleEntry)roleEntry).PSSnapinName))
				{
					return false;
				}
			}
			if (!string.IsNullOrEmpty(name))
			{
				Regex regex2 = new Regex(Wildcard.ConvertToRegexPattern(name), RegexOptions.IgnoreCase);
				if (!regex2.IsMatch(roleEntry.Name))
				{
					if (type != ManagementRoleEntryType.Cmdlet)
					{
						return false;
					}
					if (!regex2.IsMatch(roleEntry.Name + "," + ((CmdletRoleEntry)roleEntry).PSSnapinName))
					{
						return false;
					}
				}
			}
			if (parameters != null && parameters.Length != 0)
			{
				foreach (string wildcardString in parameters)
				{
					Regex regex3 = new Regex(Wildcard.ConvertToRegexPattern(wildcardString), RegexOptions.IgnoreCase);
					bool flag = false;
					foreach (string input in roleEntry.Parameters)
					{
						if (regex3.IsMatch(input))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0004B82C File Offset: 0x00049A2C
		internal static bool TryConvertPowershellFilterIntoQueryFilter(string filter, ScopeRestrictionType scopeRestrictionType, Task task, out QueryFilter queryFilter, out string errorString)
		{
			queryFilter = null;
			errorString = null;
			ObjectSchema schema;
			switch (scopeRestrictionType)
			{
			case ScopeRestrictionType.RecipientScope:
				schema = RBACHelper.aDRecipientObjectsSchema;
				break;
			case ScopeRestrictionType.ServerScope:
				schema = RBACHelper.serverSchema;
				break;
			case ScopeRestrictionType.PartnerDelegatedTenantScope:
				schema = RBACHelper.tenantOrganizationPresentationObjectSchema;
				break;
			case ScopeRestrictionType.DatabaseScope:
				schema = RBACHelper.databaseSchema;
				break;
			default:
				throw new ArgumentException("scopeRestrictionType");
			}
			Exception ex = null;
			try
			{
				MonadFilter monadFilter = new MonadFilter(filter, task, schema);
				queryFilter = monadFilter.InnerFilter;
				return true;
			}
			catch (InvalidCastException ex2)
			{
				ex = ex2;
			}
			catch (ParsingException ex3)
			{
				ex = ex3;
			}
			errorString = ex.Message;
			return false;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004B8D8 File Offset: 0x00049AD8
		internal static ADObjectId[] AddElementToStaticArray(ADObjectId[] assigneeIds, ADObjectId newElement)
		{
			ADObjectId[] array = new ADObjectId[assigneeIds.Length + 1];
			array[0] = newElement;
			assigneeIds.CopyTo(array, 1);
			return array;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0004B900 File Offset: 0x00049B00
		private static QueryFilter ConstructRoleEntryFilter(string patternToSearch)
		{
			ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeRoleSchema.Exchange2009_R3);
			TextFilter textFilter = new TextFilter(ExchangeRoleSchema.InternalDownlevelRoleEntries, patternToSearch, MatchOptions.WildcardString, MatchFlags.IgnoreCase);
			AndFilter andFilter = new AndFilter(new QueryFilter[]
			{
				comparisonFilter,
				textFilter
			});
			ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, ExchangeRoleSchema.Exchange2009_R4DF5);
			TextFilter textFilter2 = new TextFilter(ExchangeRoleSchema.RoleEntries, patternToSearch, MatchOptions.WildcardString, MatchFlags.IgnoreCase);
			AndFilter andFilter2 = new AndFilter(new QueryFilter[]
			{
				comparisonFilter2,
				textFilter2
			});
			return new OrFilter(new QueryFilter[]
			{
				andFilter,
				andFilter2
			});
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0004B99C File Offset: 0x00049B9C
		internal static void AddValueToDictionaryList<T>(Dictionary<string, List<T>> dictionary, string key, T valueToAdd)
		{
			List<T> list;
			if (!dictionary.TryGetValue(key, out list))
			{
				list = new List<T>();
				dictionary[key] = list;
			}
			list.Add(valueToAdd);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0004BA18 File Offset: 0x00049C18
		public static ADObjectId GetDefaultRoleAssignmentPolicy(OrganizationId orgId)
		{
			if (orgId.Equals(OrganizationId.ForestWideOrgId))
			{
				return null;
			}
			IConfigurationSession scopedSession = SharedConfiguration.CreateScopedToSharedConfigADSession(orgId);
			return ProvisioningCache.Instance.TryAddAndGetOrganizationData<ADObjectId>(CannedProvisioningCacheKeys.MailboxRoleAssignmentPolicyCacheKey, orgId, delegate()
			{
				RoleAssignmentPolicy[] array = scopedSession.Find<RoleAssignmentPolicy>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, RoleAssignmentPolicySchema.IsDefault, true), null, 1);
				if (array != null && array.Length > 0)
				{
					return array[0].Id;
				}
				return null;
			});
		}

		// Token: 0x040005D6 RID: 1494
		private static readonly OrFilter scriptEnabledRoleEntryTypeFilter = new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleSchema.RoleType, RoleType.Custom),
			new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleSchema.RoleType, RoleType.UnScoped)
		});

		// Token: 0x040005D7 RID: 1495
		private static ADRecipientProperties aDRecipientObjectsSchema = ObjectSchema.GetInstance<ADRecipientProperties>();

		// Token: 0x040005D8 RID: 1496
		private static ServerSchema serverSchema = ObjectSchema.GetInstance<ServerSchema>();

		// Token: 0x040005D9 RID: 1497
		private static TenantOrganizationPresentationObjectSchema tenantOrganizationPresentationObjectSchema = ObjectSchema.GetInstance<TenantOrganizationPresentationObjectSchema>();

		// Token: 0x040005DA RID: 1498
		private static DatabaseSchema databaseSchema = ObjectSchema.GetInstance<DatabaseSchema>();
	}
}
