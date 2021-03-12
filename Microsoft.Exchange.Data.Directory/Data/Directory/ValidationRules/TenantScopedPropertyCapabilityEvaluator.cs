using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.ValidationRules
{
	// Token: 0x02000A22 RID: 2594
	internal abstract class TenantScopedPropertyCapabilityEvaluator : CapabilityIdentifierEvaluator
	{
		// Token: 0x060077BA RID: 30650 RVA: 0x00189CEC File Offset: 0x00187EEC
		public TenantScopedPropertyCapabilityEvaluator(Capability capability) : base(capability)
		{
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x00189CF8 File Offset: 0x00187EF8
		protected IConfigurationSession GetTenantScopedSystemConfigurationSession(OrganizationId userOrgId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), userOrgId, null, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.FullyConsistent, sessionSettings, 104, "GetTenantScopedSystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ValidationRules\\CapabilityIdentifier.cs");
		}
	}
}
