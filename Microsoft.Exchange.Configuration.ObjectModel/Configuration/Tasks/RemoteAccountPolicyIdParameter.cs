using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000195 RID: 405
	[Serializable]
	public class RemoteAccountPolicyIdParameter : ADIdParameter
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002B183 File Offset: 0x00029383
		public RemoteAccountPolicyIdParameter()
		{
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002B18B File Offset: 0x0002938B
		public RemoteAccountPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity.Identity)
		{
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002B199 File Offset: 0x00029399
		public RemoteAccountPolicyIdParameter(RemoteAccountPolicy remoteAccountPolicy) : base(remoteAccountPolicy.Id)
		{
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002B1A7 File Offset: 0x000293A7
		public RemoteAccountPolicyIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0002B1B0 File Offset: 0x000293B0
		protected RemoteAccountPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0002B1B9 File Offset: 0x000293B9
		public static RemoteAccountPolicyIdParameter Parse(string identity)
		{
			return new RemoteAccountPolicyIdParameter(identity);
		}
	}
}
