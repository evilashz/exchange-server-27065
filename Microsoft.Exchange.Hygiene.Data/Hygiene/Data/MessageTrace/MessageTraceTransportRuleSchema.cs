using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000183 RID: 387
	internal class MessageTraceTransportRuleSchema : ADObjectSchema
	{
		// Token: 0x04000737 RID: 1847
		public static readonly HygienePropertyDefinition DLPPolicyIdProp = new HygienePropertyDefinition("DLPPolicyId", typeof(Guid));
	}
}
