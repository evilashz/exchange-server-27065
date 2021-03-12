using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class ClientAccessRuleIdParameter : ADIdParameter
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0001ECD4 File Offset: 0x0001CED4
		public ClientAccessRuleIdParameter()
		{
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001ECDC File Offset: 0x0001CEDC
		public ClientAccessRuleIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001ECE5 File Offset: 0x0001CEE5
		public ClientAccessRuleIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001ECEE File Offset: 0x0001CEEE
		public ClientAccessRuleIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001ECF7 File Offset: 0x0001CEF7
		public static ClientAccessRuleIdParameter Parse(string identity)
		{
			return new ClientAccessRuleIdParameter(identity);
		}
	}
}
