using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200082A RID: 2090
	[Serializable]
	public class ClassificationRuleCollectionIdParameter : ADIdParameter
	{
		// Token: 0x06004841 RID: 18497 RVA: 0x00129044 File Offset: 0x00127244
		public ClassificationRuleCollectionIdParameter()
		{
			this.Initialize();
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00129052 File Offset: 0x00127252
		protected ClassificationRuleCollectionIdParameter(string identity) : base(identity)
		{
			ExAssert.RetailAssert(!string.IsNullOrEmpty(base.RawIdentity), "The identity argument should have been validated by the base class");
			this.Initialize();
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00129079 File Offset: 0x00127279
		public static ClassificationRuleCollectionIdParameter Parse(string identity)
		{
			return new ClassificationRuleCollectionIdParameter(identity);
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00129081 File Offset: 0x00127281
		public static implicit operator ClassificationRuleCollectionIdParameter(ADObjectId identity)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			return new ClassificationRuleCollectionIdParameter(identity.ToString());
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x0012909C File Offset: 0x0012729C
		internal override void Initialize(ObjectId objectId)
		{
			ADObjectId adobjectId = objectId as ADObjectId;
			if (adobjectId != null && base.InternalADObjectId == null && string.IsNullOrEmpty(adobjectId.DistinguishedName))
			{
				return;
			}
			base.Initialize(objectId);
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x001290D0 File Offset: 0x001272D0
		private void Initialize()
		{
			this.ShouldIncludeOutOfBoxCollections = false;
			this.IsHierarchyValid = false;
			this.OrganizationName = null;
			string rawIdentity = base.RawIdentity;
			this.FriendlyName = rawIdentity;
			if (string.IsNullOrEmpty(rawIdentity))
			{
				this.IsHierarchical = false;
				return;
			}
			if (base.InternalADObjectId != null)
			{
				this.IsHierarchical = false;
				this.FriendlyName = null;
				return;
			}
			int num = rawIdentity.IndexOf('\\');
			this.IsHierarchical = (num >= 1 && num + 1 < rawIdentity.Length && base.IsMultitenancyEnabled());
			if (this.IsHierarchical)
			{
				this.OrganizationName = rawIdentity.Substring(0, num);
				if (this.IsWildcardDefined(this.OrganizationName))
				{
					this.IsHierarchyValid = false;
				}
				else
				{
					ExchangeConfigurationUnit configurationUnit = base.GetConfigurationUnit(this.OrganizationName);
					this.IsHierarchyValid = (configurationUnit != null);
				}
				this.FriendlyName = rawIdentity.Substring(num + 1);
			}
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x001291C8 File Offset: 0x001273C8
		private static QueryFilter CreateExcludeFilter<T>(IList<T> dataObjectToBeExcludedFromQuery) where T : IConfigurable, new()
		{
			if (dataObjectToBeExcludedFromQuery.Count == 0)
			{
				return null;
			}
			IEnumerable<QueryFilter> source = from oobRuleCollection in dataObjectToBeExcludedFromQuery
			select new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.DistinguishedName, ((ADObjectId)oobRuleCollection.Identity).DistinguishedName);
			return QueryFilter.AndTogether(source.ToArray<QueryFilter>());
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00129200 File Offset: 0x00127400
		private ClassificationIdentityMatcher<Tuple<TransportRule, XDocument>> CreateNameMatchFilter(string identityToMatch)
		{
			TextFilter queryFilter = this.IsWildcardDefined(this.FriendlyName) ? ((TextFilter)base.CreateWildcardFilter(ADObjectSchema.Name, identityToMatch)) : new TextFilter(ADObjectSchema.Name, identityToMatch, MatchOptions.FullString, MatchFlags.Default);
			return ClassificationRuleCollectionNameMatcher.CreateFrom(queryFilter, identityToMatch);
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x00129248 File Offset: 0x00127448
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					ADConfigurationObjectSchema.AdminDisplayName
				};
			}
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x0600484A RID: 18506 RVA: 0x00129265 File Offset: 0x00127465
		// (set) Token: 0x0600484B RID: 18507 RVA: 0x0012926D File Offset: 0x0012746D
		internal bool IsHierarchical { get; private set; }

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x00129276 File Offset: 0x00127476
		// (set) Token: 0x0600484D RID: 18509 RVA: 0x0012927E File Offset: 0x0012747E
		internal bool IsHierarchyValid { get; private set; }

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x0600484E RID: 18510 RVA: 0x00129287 File Offset: 0x00127487
		// (set) Token: 0x0600484F RID: 18511 RVA: 0x0012928F File Offset: 0x0012748F
		internal string FriendlyName { get; private set; }

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x00129298 File Offset: 0x00127498
		// (set) Token: 0x06004851 RID: 18513 RVA: 0x001292A0 File Offset: 0x001274A0
		internal bool ShouldIncludeOutOfBoxCollections { get; set; }

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x001292A9 File Offset: 0x001274A9
		// (set) Token: 0x06004853 RID: 18515 RVA: 0x001292B1 File Offset: 0x001274B1
		private protected string OrganizationName { protected get; private set; }

		// Token: 0x06004854 RID: 18516 RVA: 0x001292E0 File Offset: 0x001274E0
		internal override IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData)
		{
			IEnumerable<T> objectsInOrganization = base.GetObjectsInOrganization<T>(identityString, rootId, session, optionalData);
			if (typeof(T) != typeof(TransportRule))
			{
				return objectsInOrganization;
			}
			ClassificationIdentityMatcher<Tuple<TransportRule, XDocument>> nameMatcher = this.CreateNameMatchFilter(identityString);
			if (nameMatcher == null)
			{
				return objectsInOrganization;
			}
			List<T> list = objectsInOrganization.ToList<T>();
			QueryFilter queryFilter = ClassificationRuleCollectionIdParameter.CreateExcludeFilter<T>(list);
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				queryFilter = ((queryFilter == null) ? optionalData.AdditionalFilter : QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					optionalData.AdditionalFilter
				}));
			}
			IEnumerable<Tuple<TransportRule, XDocument>> source = DlpUtils.AggregateOobAndCustomClassificationDefinitions(session.SessionSettings.CurrentOrganizationId, session as IConfigurationSession, null, queryFilter, new ClassificationDefinitionsDataReader(false), null);
			return list.Concat(from rulePackDataObject in source
			where nameMatcher.Match(rulePackDataObject)
			select (T)((object)rulePackDataObject.Item1));
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x001293C8 File Offset: 0x001275C8
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			IEnumerable<T> enumerable = Enumerable.Empty<T>();
			if (this.ShouldIncludeOutOfBoxCollections && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && base.InternalADObjectId == null)
			{
				bool flag = false;
				bool flag2 = OrganizationId.ForestWideOrgId.Equals(session.SessionSettings.CurrentOrganizationId);
				if (flag2 && this.IsHierarchical && this.IsHierarchyValid)
				{
					ClassificationRuleCollectionIdParameter classificationRuleCollectionIdParameter = new ClassificationRuleCollectionIdParameter(this.FriendlyName);
					enumerable = classificationRuleCollectionIdParameter.GetObjects<T>(rootId, (IConfigDataProvider)session, optionalData, out notFoundReason);
					flag = true;
				}
				else if (!flag2 && !this.IsHierarchical)
				{
					ITopologyConfigurationSession session2 = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 431, "GetObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\ClassificationDefinitions\\ClassificationRuleCollectionIdParameter.cs");
					enumerable = base.GetObjects<T>(ClassificationDefinitionUtils.GetClassificationRuleCollectionContainerId(session2), session2, null, out notFoundReason);
					flag = true;
				}
				if (flag)
				{
					if (optionalData == null)
					{
						optionalData = new OptionalIdentityData();
					}
					List<T> list = enumerable.ToList<T>();
					QueryFilter queryFilter = ClassificationRuleCollectionIdParameter.CreateExcludeFilter<T>(list);
					if (queryFilter != null)
					{
						optionalData.AdditionalFilter = ((optionalData.AdditionalFilter == null) ? queryFilter : QueryFilter.AndTogether(new QueryFilter[]
						{
							optionalData.AdditionalFilter,
							queryFilter
						}));
					}
					enumerable = list;
				}
			}
			return enumerable.Concat(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
		}
	}
}
