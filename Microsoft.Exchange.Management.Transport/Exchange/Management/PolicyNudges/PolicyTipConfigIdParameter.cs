using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public class PolicyTipConfigIdParameter : ADIdParameter
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000D9F3 File Offset: 0x0000BBF3
		public PolicyTipConfigIdParameter()
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000D9FB File Offset: 0x0000BBFB
		public PolicyTipConfigIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public PolicyTipConfigIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000DA0D File Offset: 0x0000BC0D
		public PolicyTipConfigIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000DA16 File Offset: 0x0000BC16
		public static PolicyTipConfigIdParameter Parse(string identity)
		{
			return new PolicyTipConfigIdParameter(identity);
		}
	}
}
