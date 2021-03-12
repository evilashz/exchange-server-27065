using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Directory.UnifiedPolicy
{
	// Token: 0x02000A11 RID: 2577
	internal class UnifiedPolicyStorageBaseSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04004C64 RID: 19556
		public static ADPropertyDefinition WorkloadProp = new ADPropertyDefinition("Workload", ExchangeObjectVersion.Exchange2012, typeof(Workload), "msExchOWASettings", ADPropertyDefinitionFlags.None, Workload.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C65 RID: 19557
		public static ADPropertyDefinition PolicyVersion = new ADPropertyDefinition("PolicyVersion", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchCanaryData0", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C66 RID: 19558
		public static ADPropertyDefinition MasterIdentity = new ADPropertyDefinition("MasterIdentity", ExchangeObjectVersion.Exchange2012, typeof(Guid), "msExchEdgeSyncSourceGuid", ADPropertyDefinitionFlags.WriteOnce, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04004C67 RID: 19559
		public static ADPropertyDefinition ContainerProp = new ADPropertyDefinition("Container", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchEwsExceptions", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
