using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000826 RID: 2086
	internal class ClassificationDefinitionsDataReader : IClassificationDefinitionsDataReader
	{
		// Token: 0x06004828 RID: 18472 RVA: 0x0012890B File Offset: 0x00126B0B
		internal ClassificationDefinitionsDataReader(bool isAggregateReader = true)
		{
			this.isAggregateReader = isAggregateReader;
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x0012891A File Offset: 0x00126B1A
		internal static IClassificationDefinitionsDataReader DefaultInstance
		{
			get
			{
				return ClassificationDefinitionsDataReader.singletonDefaultInstance;
			}
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x00128924 File Offset: 0x00126B24
		public IEnumerable<TransportRule> GetAllClassificationRuleCollection(OrganizationId organizationId, IConfigurationSession currentDataSession, QueryFilter additionalFilter)
		{
			if (object.ReferenceEquals(null, organizationId))
			{
				throw new ArgumentNullException("organizationId");
			}
			if (currentDataSession != null && !organizationId.Equals(currentDataSession.SessionSettings.CurrentOrganizationId))
			{
				throw new ArgumentException(new ArgumentException().Message, "currentDataSession");
			}
			HashSet<TransportRule> hashSet = new HashSet<TransportRule>(ClassificationDefinitionsDataReader.transportRuleComparer);
			bool flag = OrganizationId.ForestWideOrgId.Equals(organizationId);
			IConfigurationSession configurationSession = null;
			if (flag && currentDataSession != null)
			{
				configurationSession = currentDataSession;
			}
			else if (flag || this.isAggregateReader)
			{
				configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1092, "GetAllClassificationRuleCollection", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\ClassificationDefinitions\\ClassificationDefinitionUtils.cs");
			}
			if (configurationSession != null)
			{
				hashSet.UnionWith(configurationSession.FindPaged<TransportRule>(additionalFilter, configurationSession.GetOrgContainerId().GetDescendantId(ClassificationDefinitionConstants.ClassificationDefinitionsRdn), false, null, 0));
			}
			if (!flag)
			{
				IConfigurationSession configurationSession2 = currentDataSession ?? DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false), 1114, "GetAllClassificationRuleCollection", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\ClassificationDefinitions\\ClassificationDefinitionUtils.cs");
				hashSet.UnionWith(configurationSession2.FindPaged<TransportRule>(additionalFilter, configurationSession2.GetOrgContainerId().GetDescendantId(ClassificationDefinitionConstants.ClassificationDefinitionsRdn), false, null, 0));
			}
			return hashSet;
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x00128A34 File Offset: 0x00126C34
		public DataClassificationConfig GetDataClassificationConfig(OrganizationId organizationId, IConfigurationSession currentDataSession)
		{
			if (object.ReferenceEquals(null, organizationId))
			{
				throw new ArgumentNullException("organizationId");
			}
			if (currentDataSession == null)
			{
				throw new ArgumentNullException("currentDataSession");
			}
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled || OrganizationId.ForestWideOrgId.Equals(organizationId))
			{
				return null;
			}
			if (!organizationId.Equals(currentDataSession.SessionSettings.CurrentOrganizationId))
			{
				throw new ArgumentException(new ArgumentException().Message, "currentDataSession");
			}
			SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(organizationId);
			IConfigurationSession configurationSession;
			if (sharedConfiguration != null)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sharedConfiguration.GetSharedConfigurationSessionSettings(), 1186, "GetDataClassificationConfig", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\ClassificationDefinitions\\ClassificationDefinitionUtils.cs");
			}
			else
			{
				configurationSession = currentDataSession;
			}
			DataClassificationConfig[] array = configurationSession.Find<DataClassificationConfig>(null, QueryScope.SubTree, null, null, 1);
			ExAssert.RetailAssert(array != null && 1 == array.Length, "There should be one and only one DataClassificationConfig applicable to a particular tenant.");
			return array[0];
		}

		// Token: 0x04002BE9 RID: 11241
		private static readonly IEqualityComparer<TransportRule> transportRuleComparer = new EqualityComparer<TransportRule>((TransportRule lhsTransportRule, TransportRule rhsTransportRule) => StringComparer.OrdinalIgnoreCase.Equals(lhsTransportRule.DistinguishedName, rhsTransportRule.DistinguishedName), (TransportRule currentTransportRule) => StringComparer.OrdinalIgnoreCase.GetHashCode(currentTransportRule.DistinguishedName));

		// Token: 0x04002BEA RID: 11242
		private static readonly ClassificationDefinitionsDataReader singletonDefaultInstance = new ClassificationDefinitionsDataReader(true);

		// Token: 0x04002BEB RID: 11243
		private readonly bool isAggregateReader;
	}
}
