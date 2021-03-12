using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200070D RID: 1805
	internal static class ExtendedSecurityPrincipalSearchHelper
	{
		// Token: 0x060054F7 RID: 21751 RVA: 0x0013319C File Offset: 0x0013139C
		internal static IEnumerable<ExtendedSecurityPrincipal> PerformSearch(ExtendedSecurityPrincipalSearcher searcher, IConfigDataProvider session, ADObjectId rootId, ADObjectId includeDomailLocalFrom, MultiValuedProperty<SecurityPrincipalType> types)
		{
			if (types.Contains(SecurityPrincipalType.WellknownSecurityPrincipal))
			{
				IRecipientSession dataSession = (IRecipientSession)session;
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				IConfigurationSession configSession;
				if (dataSession.ConfigScope == ConfigScopes.TenantSubTree)
				{
					configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(dataSession.DomainController, dataSession.ReadOnly, dataSession.ConsistencyMode, dataSession.NetworkCredential, sessionSettings, dataSession.ConfigScope, 60, "PerformSearch", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Management\\ExtendedSecurityPrincipalSearchHelper.cs");
				}
				else
				{
					configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(dataSession.DomainController, dataSession.ReadOnly, dataSession.ConsistencyMode, dataSession.NetworkCredential, sessionSettings, 70, "PerformSearch", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Management\\ExtendedSecurityPrincipalSearchHelper.cs");
				}
				foreach (ExtendedSecurityPrincipal wellknown in searcher(configSession, null, ExtendedSecurityPrincipalSearchHelper.GenerateTargetFilterForWellknownSecurityPrincipal()))
				{
					yield return wellknown;
				}
			}
			IRecipientSession recipientSession = (IRecipientSession)session;
			if (types.Contains(SecurityPrincipalType.GlobalSecurityGroup) || types.Contains(SecurityPrincipalType.UniversalSecurityGroup) || types.Contains(SecurityPrincipalType.User) || types.Contains(SecurityPrincipalType.Group))
			{
				recipientSession.UseGlobalCatalog = (rootId == null);
				foreach (ExtendedSecurityPrincipal recipient in searcher(recipientSession, rootId, ExtendedSecurityPrincipalSearchHelper.GenerateTargetFilterForUserAndNonDomainLocalGroup(types)))
				{
					yield return recipient;
				}
			}
			if (includeDomailLocalFrom != null)
			{
				recipientSession.UseGlobalCatalog = false;
				ADObjectId searchRoot = null;
				if (rootId == null || includeDomailLocalFrom.IsDescendantOf(rootId))
				{
					searchRoot = includeDomailLocalFrom;
				}
				else if (rootId.DomainId.Equals(includeDomailLocalFrom))
				{
					searchRoot = rootId;
				}
				if (searchRoot != null)
				{
					foreach (ExtendedSecurityPrincipal group in searcher(recipientSession, searchRoot, ExtendedSecurityPrincipalSearchHelper.GenerateTargetFilterForSecurityGroup(GroupTypeFlags.DomainLocal)))
					{
						yield return group;
					}
				}
			}
			yield break;
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x001331D6 File Offset: 0x001313D6
		private static QueryFilter GenerateTargetFilterForWellknownSecurityPrincipal()
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ExtendedSecurityPrincipal.WellknownSecurityPrincipalClassName);
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x001331E8 File Offset: 0x001313E8
		private static QueryFilter GenerateTargetFilterForUserAndNonDomainLocalGroup(MultiValuedProperty<SecurityPrincipalType> types)
		{
			List<CompositeFilter> list = new List<CompositeFilter>();
			foreach (SecurityPrincipalType securityPrincipalType in types)
			{
				CompositeFilter compositeFilter = null;
				switch (securityPrincipalType)
				{
				case SecurityPrincipalType.User:
					compositeFilter = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADUser.ObjectCategoryNameInternal),
						ADObject.ObjectClassFilter(ADUser.MostDerivedClass, true)
					});
					break;
				case SecurityPrincipalType.Group:
					compositeFilter = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADGroup.MostDerivedClass),
						new BitMaskOrFilter(ADGroupSchema.GroupType, (ulong)int.MinValue),
						new NotFilter(new BitMaskAndFilter(ADGroupSchema.GroupType, 4UL))
					});
					break;
				case SecurityPrincipalType.UniversalSecurityGroup:
					if (!types.Contains(SecurityPrincipalType.Group))
					{
						compositeFilter = ExtendedSecurityPrincipalSearchHelper.GenerateTargetFilterForSecurityGroup(GroupTypeFlags.Universal);
					}
					break;
				case SecurityPrincipalType.GlobalSecurityGroup:
					if (!types.Contains(SecurityPrincipalType.Group))
					{
						compositeFilter = ExtendedSecurityPrincipalSearchHelper.GenerateTargetFilterForSecurityGroup(GroupTypeFlags.Global);
					}
					break;
				}
				if (compositeFilter != null)
				{
					if (Datacenter.IsMicrosoftHostedOnly(true) && (securityPrincipalType == SecurityPrincipalType.Group || securityPrincipalType == SecurityPrincipalType.UniversalSecurityGroup))
					{
						compositeFilter = new AndFilter(new QueryFilter[]
						{
							compositeFilter,
							new OrFilter(new QueryFilter[]
							{
								new NotFilter(new BitMaskAndFilter(ADGroupSchema.GroupType, 8UL)),
								new AndFilter(new QueryFilter[]
								{
									new BitMaskAndFilter(ADGroupSchema.GroupType, 8UL),
									new OrFilter(new QueryFilter[]
									{
										new ExistsFilter(ADRecipientSchema.Alias),
										Filters.GetRecipientTypeDetailsFilterOptimization(RecipientTypeDetails.RoleGroup)
									})
								})
							})
						});
					}
					list.Add(compositeFilter);
				}
			}
			return new OrFilter(list.ToArray());
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x001333D8 File Offset: 0x001315D8
		private static CompositeFilter GenerateTargetFilterForSecurityGroup(GroupTypeFlags flag)
		{
			return new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADGroup.MostDerivedClass),
				new BitMaskOrFilter(ADGroupSchema.GroupType, (ulong)int.MinValue),
				new BitMaskAndFilter(ADGroupSchema.GroupType, (ulong)flag)
			});
		}
	}
}
