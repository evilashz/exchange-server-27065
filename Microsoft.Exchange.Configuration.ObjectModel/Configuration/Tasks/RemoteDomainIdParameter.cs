using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public class RemoteDomainIdParameter : ADIdParameter
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0002448F File Offset: 0x0002268F
		public RemoteDomainIdParameter()
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00024497 File Offset: 0x00022697
		public RemoteDomainIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000244A0 File Offset: 0x000226A0
		public RemoteDomainIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000244A9 File Offset: 0x000226A9
		protected RemoteDomainIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000244B2 File Offset: 0x000226B2
		public static RemoteDomainIdParameter Parse(string identity)
		{
			return new RemoteDomainIdParameter(identity);
		}
	}
}
