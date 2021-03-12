using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A4B RID: 2635
	internal class HostedContentFilterRuleSchema : HygieneFilterRuleSchema
	{
		// Token: 0x040034D3 RID: 13523
		public static readonly ADPropertyDefinition HostedContentFilterPolicy = new ADPropertyDefinition("HostedContentFilterPolicy", ExchangeObjectVersion.Exchange2012, typeof(HostedContentFilterPolicyIdParameter), "hostedContentFilterPolicy", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
