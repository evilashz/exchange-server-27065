using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200015D RID: 349
	[Serializable]
	public class UMDialPlanIdParameter : ADIdParameter
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x000279D6 File Offset: 0x00025BD6
		public UMDialPlanIdParameter()
		{
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000279DE File Offset: 0x00025BDE
		public UMDialPlanIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000279E7 File Offset: 0x00025BE7
		public UMDialPlanIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000279F0 File Offset: 0x00025BF0
		public UMDialPlanIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000279F9 File Offset: 0x00025BF9
		public static UMDialPlanIdParameter Parse(string identity)
		{
			return new UMDialPlanIdParameter(identity);
		}
	}
}
