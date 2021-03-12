using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007F8 RID: 2040
	internal static class MailboxSearchHelper
	{
		// Token: 0x06003BA5 RID: 15269 RVA: 0x000D0390 File Offset: 0x000CE590
		internal static void PerformCommonAuthorization(bool isExternalUser, out ExchangeRunspaceConfiguration runspaceConfig, out IRecipientSession recipientSession)
		{
			runspaceConfig = null;
			recipientSession = null;
			try
			{
				if (isExternalUser)
				{
					ExTraceGlobals.SearchTracer.TraceError(0L, "External user is not supported at this moment.");
					throw new ServiceAccessDeniedException();
				}
				bool flag = false;
				if ((CallContext.Current.LogonType == LogonType.Admin || CallContext.Current.LogonType == LogonType.SystemService) && CallContext.Current.LogonTypeSource == LogonTypeSource.OpenAsAdminOrSystemServiceHeader)
				{
					flag = true;
				}
				OAuthIdentity oauthIdentity = CallContext.Current.HttpContext.User.Identity as OAuthIdentity;
				OrganizationId organizationId;
				if (oauthIdentity != null && oauthIdentity.IsAppOnly)
				{
					organizationId = oauthIdentity.OrganizationId;
				}
				else if (flag)
				{
					organizationId = CallContext.Current.EffectiveCaller.UserIdentity.ADUser.OrganizationId;
				}
				else
				{
					runspaceConfig = ExchangeRunspaceConfigurationCache.Singleton.Get(CallContext.Current.EffectiveCaller, null, false);
					organizationId = runspaceConfig.OrganizationId;
				}
				ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
				if (organizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					adsessionSettings = ADSessionSettings.RescopeToSubtree(adsessionSettings);
				}
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
				{
					adsessionSettings.IncludeInactiveMailbox = true;
				}
				recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, adsessionSettings, 123, "PerformCommonAuthorization", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\MailboxSearch.cs");
			}
			catch (AuthzException innerException)
			{
				throw new ServiceAccessDeniedException(innerException);
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000D04D8 File Offset: 0x000CE6D8
		internal static bool IsOpenAsAdmin(CallContext callContext)
		{
			return callContext.LogonTypeSource == LogonTypeSource.OpenAsAdminOrSystemServiceHeader;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000D04E8 File Offset: 0x000CE6E8
		internal static ADPagedReader<MiniRecipient> QueryADObject(IRecipientSession recipientSession, List<QueryFilter> additionalFilters, SortBy sortBy)
		{
			QueryFilter queryFilter = QueryFilter.OrTogether(new List<QueryFilter>
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.UserMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.LinkedMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.SharedMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.TeamMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.LegacyMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RoomMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.EquipmentMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.LinkedRoomMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, (RecipientTypeDetails)((ulong)int.MinValue)),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RemoteRoomMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RemoteEquipmentMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RemoteSharedMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.RemoteTeamMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MailUniversalDistributionGroup),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MailUniversalSecurityGroup),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.MailNonUniversalGroup),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.DynamicDistributionGroup)
			}.ToArray());
			if (additionalFilters != null && additionalFilters.Count > 0)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					QueryFilter.OrTogether(additionalFilters.ToArray())
				});
			}
			return recipientSession.FindPagedMiniRecipient<MiniRecipient>(null, QueryScope.SubTree, queryFilter, sortBy, 1500, MailboxSearchHelper.AdditionalProperties);
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000D0720 File Offset: 0x000CE920
		internal static bool IsMembershipGroup(ADRawEntry recipient)
		{
			RecipientType recipientType = (RecipientType)recipient[ADRecipientSchema.RecipientType];
			return recipientType == RecipientType.MailUniversalDistributionGroup || recipientType == RecipientType.MailUniversalSecurityGroup || recipientType == RecipientType.MailNonUniversalGroup || recipientType == RecipientType.DynamicDistributionGroup;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000D0754 File Offset: 0x000CE954
		internal static bool IsValidRecipientType(ADRawEntry recipient)
		{
			return (RecipientType)recipient[ADRecipientSchema.RecipientType] == RecipientType.UserMailbox || ((RecipientType)recipient[ADRecipientSchema.RecipientType] == RecipientType.MailUser && VariantConfiguration.InvariantNoFlightingSnapshot.Ews.OnlineArchive.Enabled) || (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails] == (RecipientTypeDetails)((ulong)int.MinValue) || (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails] == RecipientTypeDetails.RemoteRoomMailbox || (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails] == RecipientTypeDetails.RemoteEquipmentMailbox || (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails] == RecipientTypeDetails.RemoteTeamMailbox || (RecipientTypeDetails)recipient[ADRecipientSchema.RecipientTypeDetails] == RecipientTypeDetails.RemoteSharedMailbox;
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x000D082C File Offset: 0x000CEA2C
		internal static bool HasValidVersion(ADRawEntry recipient)
		{
			return !recipient.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2012) || MailboxSearchHelper.IsMembershipGroup(recipient) || (RecipientType)recipient[ADRecipientSchema.RecipientType] == RecipientType.MailUser;
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x000D0860 File Offset: 0x000CEA60
		internal static bool HasPermissionToSearchMailbox(ExchangeRunspaceConfiguration runspaceConfig, ADRawEntry recipient)
		{
			return runspaceConfig == null || runspaceConfig.IsCmdletAllowedInScope("New-MailboxSearch", new string[]
			{
				"EstimateOnly"
			}, recipient, ScopeLocation.RecipientWrite);
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000D0890 File Offset: 0x000CEA90
		internal static bool CanEnableHoldOnMailbox(ExchangeRunspaceConfiguration runspaceConfig, ADRawEntry recipient)
		{
			return runspaceConfig == null || runspaceConfig.IsCmdletAllowedInScope("New-MailboxSearch", new string[]
			{
				"InPlaceHoldEnabled"
			}, recipient, ScopeLocation.RecipientWrite);
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000D08BF File Offset: 0x000CEABF
		internal static Dictionary<ADObjectId, ADRawEntry> ProcessGroupExpansion(IRecipientSession recipientSession, ADRawEntry adEntry, OrganizationId organizationId)
		{
			return MailboxSearchHelper.ProcessGroupExpansion(recipientSession, adEntry, organizationId, MailboxSearchHelper.AdditionalProperties);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000D0928 File Offset: 0x000CEB28
		internal static Dictionary<ADObjectId, ADRawEntry> ProcessGroupExpansion(IRecipientSession recipientSession, ADRawEntry adEntry, OrganizationId organizationId, PropertyDefinition[] properties)
		{
			Dictionary<ADObjectId, ADRawEntry> expandedMemberIds = new Dictionary<ADObjectId, ADRawEntry>();
			ADRawEntry recipientToExpand = recipientSession.Read(adEntry.Id);
			ADRecipientExpansion adrecipientExpansion = new ADRecipientExpansion(properties, organizationId);
			adrecipientExpansion.Expand(recipientToExpand, delegate(ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentType)
			{
				if (recipientExpansionType == ExpansionType.GroupMembership)
				{
					return ExpansionControl.Continue;
				}
				if (MailboxSearchHelper.IsValidRecipientType(recipient) && !expandedMemberIds.ContainsKey(recipient.Id))
				{
					expandedMemberIds.Add(recipient.Id, recipient);
				}
				return ExpansionControl.Skip;
			}, delegate(ExpansionFailure expansionFailure, ADRawEntry recipient, ExpansionType recipientExpansionType, ADRawEntry parent, ExpansionType parentExpansionType)
			{
				ExTraceGlobals.SearchTracer.TraceDebug<ADObjectId, ExpansionType>(0L, "Expand group member failed for {0}, reason {1}", recipient.Id, recipientExpansionType);
				return ExpansionControl.Skip;
			});
			return expandedMemberIds;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000D0994 File Offset: 0x000CEB94
		internal static Dictionary<string, ADRawEntry> FindADEntriesByLegacyExchangeDNs(IRecipientSession recipientSession, string[] legacyExchangeDNs, PropertyDefinition[] properties)
		{
			Result<ADRawEntry>[] array = null;
			try
			{
				array = recipientSession.FindByLegacyExchangeDNs(legacyExchangeDNs, properties);
			}
			catch (NonUniqueRecipientException ex)
			{
				ExTraceGlobals.SearchTracer.TraceError(0L, "Duplicate legacy exchange DN found for: {0}", new object[]
				{
					ex.AmbiguousData
				});
				throw new DuplicateLegacyDistinguishedNameException(ex.AmbiguousData.ToString(), ex);
			}
			if (array != null && array.Length > 0)
			{
				Dictionary<string, ADRawEntry> dictionary = new Dictionary<string, ADRawEntry>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					ADRawEntry data = array[i].Data;
					if (data != null)
					{
						string key = (string)data[ADRecipientSchema.LegacyExchangeDN];
						if (!dictionary.ContainsKey(key))
						{
							dictionary.Add(key, data);
						}
						foreach (string text in legacyExchangeDNs)
						{
							if (!dictionary.ContainsKey(text) && Util.CheckLegacyDnExistInProxyAddresses(text, data))
							{
								dictionary.Add(text, data);
								break;
							}
						}
					}
				}
				return dictionary;
			}
			ExTraceGlobals.SearchTracer.TraceError<string>(0L, "The legacy DNs are not found in AD: {0}", string.Join(", ", legacyExchangeDNs));
			throw new ServiceArgumentException((CoreResources.IDs)4252616528U);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000D0AC4 File Offset: 0x000CECC4
		internal static Dictionary<Guid, string> CreateObjectGuidLegacyDNMapping(Dictionary<string, ADRawEntry> dictAdRawEntries)
		{
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>(dictAdRawEntries.Count);
			foreach (KeyValuePair<string, ADRawEntry> keyValuePair in dictAdRawEntries)
			{
				if (!dictionary.ContainsKey(keyValuePair.Value.Id.ObjectGuid))
				{
					dictionary.Add(keyValuePair.Value.Id.ObjectGuid, keyValuePair.Key);
				}
			}
			return dictionary;
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000D0B50 File Offset: 0x000CED50
		internal static string GetExchangeLegacyDNFromObjectGuid(Dictionary<Guid, string> dictObjectGuidLegacyDNMapping, Guid objectGuid)
		{
			string result;
			if (dictObjectGuidLegacyDNMapping.TryGetValue(objectGuid, out result))
			{
				return result;
			}
			ExTraceGlobals.SearchTracer.TraceError<string>(0L, "The user with this object guid is not found in AD: {0}", objectGuid.ToString());
			throw new ServiceArgumentException((CoreResources.IDs)4252616528U);
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000D0B98 File Offset: 0x000CED98
		internal static string GetExchangeLegacyDNFromObjectGuid(IRecipientSession recipientSession, Guid objectGuid)
		{
			ADRecipient adrecipient = recipientSession.FindByObjectGuid(objectGuid);
			if (adrecipient != null)
			{
				return adrecipient.LegacyExchangeDN;
			}
			ExTraceGlobals.SearchTracer.TraceError<string>(0L, "The user with this object guid is not found in AD: {0}", objectGuid.ToString());
			throw new ServiceArgumentException((CoreResources.IDs)4252616528U);
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000D0BE4 File Offset: 0x000CEDE4
		internal static void GetMailboxHoldStatuses(MailboxDiscoverySearch discoverySearch, IRecipientSession recipientSession, List<MailboxHoldStatus> statuses)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.InPlaceHolds, discoverySearch.InPlaceHoldIdentity);
			ADPagedReader<ADRawEntry> adpagedReader = recipientSession.FindPagedADRawEntry(null, QueryScope.SubTree, filter, null, 0, new PropertyDefinition[]
			{
				ADRecipientSchema.LegacyExchangeDN
			});
			ADRawEntry[] array = adpagedReader.ReadAllPages();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (ADRawEntry adrawEntry in array)
			{
				string text = (string)adrawEntry[ADRecipientSchema.LegacyExchangeDN];
				if (!string.IsNullOrEmpty(text))
				{
					hashSet.Add(text);
				}
			}
			foreach (string text2 in discoverySearch.Sources)
			{
				HoldStatus status = HoldStatus.Pending;
				string additionalInfo = string.Empty;
				if (hashSet.Contains(text2))
				{
					status = HoldStatus.OnHold;
				}
				else
				{
					int num = discoverySearch.FailedToHoldMailboxes.IndexOf(text2);
					if (num >= 0)
					{
						status = HoldStatus.Failed;
						additionalInfo = discoverySearch.InPlaceHoldErrors[num];
					}
				}
				statuses.Add(new MailboxHoldStatus(text2, status, additionalInfo));
			}
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000D0D04 File Offset: 0x000CEF04
		internal static List<SearchableMailbox> GetSearchableMailboxes(MailboxDiscoverySearch searchObject, bool expandGroupMembership, IRecipientSession recipientSession, ExchangeRunspaceConfiguration runspaceConfig, out List<FailedSearchMailbox> nonSearchableMailboxes)
		{
			nonSearchableMailboxes = new List<FailedSearchMailbox>(1);
			List<PropertyDefinition> list = new List<PropertyDefinition>(MailboxInfo.PropertyDefinitionCollection);
			list.AddRange(MailboxSearchHelper.AdditionalProperties);
			PropertyDefinition[] properties = list.ToArray();
			List<SearchableMailbox> list2 = new List<SearchableMailbox>();
			IEnumerable<ADRawEntry> enumerable;
			if (searchObject.Sources != null && searchObject.Sources.Count != 0)
			{
				Dictionary<string, ADRawEntry> dictionary = MailboxSearchHelper.FindADEntriesByLegacyExchangeDNs(recipientSession, searchObject.Sources.ToArray(), properties);
				List<ADRawEntry> list3 = new List<ADRawEntry>(dictionary.Count);
				foreach (KeyValuePair<string, ADRawEntry> keyValuePair in dictionary)
				{
					list3.Add(keyValuePair.Value);
				}
				enumerable = list3;
			}
			else
			{
				SortBy sortBy = new SortBy(MiniRecipientSchema.DisplayName, SortOrder.Ascending);
				ADPagedReader<MiniRecipient> adpagedReader = MailboxSearchHelper.QueryADObject(recipientSession, null, sortBy);
				enumerable = adpagedReader;
			}
			foreach (ADRawEntry adrawEntry in enumerable)
			{
				bool flag = MailboxSearchHelper.IsMembershipGroup(adrawEntry);
				if (flag && expandGroupMembership)
				{
					Dictionary<ADObjectId, ADRawEntry> dictionary2 = MailboxSearchHelper.ProcessGroupExpansion(recipientSession, adrawEntry, recipientSession.SessionSettings.CurrentOrganizationId);
					using (Dictionary<ADObjectId, ADRawEntry>.Enumerator enumerator3 = dictionary2.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<ADObjectId, ADRawEntry> keyValuePair2 = enumerator3.Current;
							if (keyValuePair2.Value != null)
							{
								MailboxSearchHelper.VerifyAndAddSearchableMailboxToCollection(list2, keyValuePair2.Value, false, runspaceConfig, nonSearchableMailboxes);
							}
						}
						continue;
					}
				}
				MailboxSearchHelper.VerifyAndAddSearchableMailboxToCollection(list2, adrawEntry, false, runspaceConfig, nonSearchableMailboxes);
			}
			list2.Sort();
			return list2;
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000D0EA8 File Offset: 0x000CF0A8
		internal static void VerifyAndAddSearchableMailboxToCollection(List<SearchableMailbox> searchableMailboxes, ADRawEntry recipient, bool isMembershipGroup, ExchangeRunspaceConfiguration runspaceConfig, List<FailedSearchMailbox> nonSearchableMailboxes)
		{
			MailboxSearchHelper.VerifyAndAddSearchableMailboxToCollection(searchableMailboxes, recipient, isMembershipGroup, runspaceConfig, nonSearchableMailboxes, false);
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000D0EB8 File Offset: 0x000CF0B8
		internal static void VerifyAndAddSearchableMailboxToCollection(List<SearchableMailbox> searchableMailboxes, ADRawEntry recipient, bool isMembershipGroup, ExchangeRunspaceConfiguration runspaceConfig, List<FailedSearchMailbox> nonSearchableMailboxes, bool skipPermissionCheck)
		{
			string externalEmailAddress = (recipient[ADRecipientSchema.ExternalEmailAddress] == null) ? string.Empty : ((ProxyAddress)recipient[ADRecipientSchema.ExternalEmailAddress]).AddressString;
			SearchableMailbox searchableMailbox = new SearchableMailbox(recipient.Id.ObjectGuid, ((SmtpAddress)recipient[ADRecipientSchema.PrimarySmtpAddress]).ToString(), (RecipientType)recipient[ADRecipientSchema.RecipientType] == RecipientType.MailUser, externalEmailAddress, (string)recipient[ADRecipientSchema.DisplayName], isMembershipGroup, (string)recipient[ADRecipientSchema.LegacyExchangeDN]);
			if (!searchableMailboxes.Contains(searchableMailbox))
			{
				bool flag = MailboxSearchHelper.HasValidVersion(recipient);
				bool flag2 = true;
				if (!skipPermissionCheck)
				{
					flag2 = MailboxSearchHelper.HasPermissionToSearchMailbox(runspaceConfig, recipient);
				}
				if (flag && flag2)
				{
					searchableMailboxes.Add(searchableMailbox);
					return;
				}
				ExTraceGlobals.SearchTracer.TraceDebug<string, string, string>(0L, "The recipient '{0}' not being added because either valid version = {1} or has permission to search = {2}", searchableMailbox.PrimarySmtpAddress, flag.ToString(), flag2.ToString());
				string errorMessage = string.Empty;
				if (!flag)
				{
					errorMessage = CoreResources.GetLocalizedString(CoreResources.IDs.ErrorMailboxVersionNotSupported);
				}
				else if (!flag2)
				{
					errorMessage = CoreResources.GetLocalizedString((CoreResources.IDs)2354781453U);
				}
				nonSearchableMailboxes.Add(new FailedSearchMailbox
				{
					Mailbox = searchableMailbox.ReferenceId,
					ErrorMessage = errorMessage
				});
			}
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000D1000 File Offset: 0x000CF200
		internal static ExTimeZone GetTimeZone()
		{
			ExTimeZone result = ExTimeZone.UtcTimeZone;
			if (EWSSettings.RequestTimeZone != null && EWSSettings.RequestTimeZone != ExTimeZone.UtcTimeZone)
			{
				result = EWSSettings.RequestTimeZone;
			}
			return result;
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000D1030 File Offset: 0x000CF230
		internal static Guid GetQueryCorrelationId()
		{
			Guid result;
			if (!Guid.TryParse(ActivityContext.GetCurrentActivityScope().GetProperty(ActivityStandardMetadata.ClientRequestId), out result))
			{
				result = Guid.NewGuid();
				if (string.IsNullOrEmpty(ActivityContext.GetCurrentActivityScope().GetProperty(ActivityStandardMetadata.ClientRequestId)))
				{
					ActivityContext.GetCurrentActivityScope().SetProperty(ActivityStandardMetadata.ClientRequestId, result.ToString("N"));
				}
			}
			return result;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000D10A0 File Offset: 0x000CF2A0
		internal static string[] GetUserRolesFromAuthZClientInfo(AuthZClientInfo authz)
		{
			if (authz == null || authz.UserRoleTypes == null)
			{
				return null;
			}
			return (from role in authz.UserRoleTypes
			select role.ToString()).ToArray<string>();
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x000D10EC File Offset: 0x000CF2EC
		internal static string[] GetApplicationRolesFromAuthZClientInfo(AuthZClientInfo authz)
		{
			AuthZClientInfo.ApplicationAttachedAuthZClientInfo applicationAttachedAuthZClientInfo = authz as AuthZClientInfo.ApplicationAttachedAuthZClientInfo;
			if (applicationAttachedAuthZClientInfo == null || applicationAttachedAuthZClientInfo.ApplicationRoleTypes == null)
			{
				return null;
			}
			return (from role in applicationAttachedAuthZClientInfo.ApplicationRoleTypes
			select role.ToString()).ToArray<string>();
		}

		// Token: 0x040020E0 RID: 8416
		internal const int MaxResultSize = 1500;

		// Token: 0x040020E1 RID: 8417
		internal static readonly ADPropertyDefinition[] AdditionalProperties = new ADPropertyDefinition[]
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.RecipientTypeDetailsValue,
			IADSecurityPrincipalSchema.SamAccountName,
			ADObjectSchema.OrganizationId,
			ADRecipientSchema.RawCapabilities
		};
	}
}
