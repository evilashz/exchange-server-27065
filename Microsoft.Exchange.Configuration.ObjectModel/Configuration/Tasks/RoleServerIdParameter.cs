using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	public abstract class RoleServerIdParameter : ServerIdParameter
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x0002AB21 File Offset: 0x00028D21
		public RoleServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0002AB2A File Offset: 0x00028D2A
		public RoleServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0002AB33 File Offset: 0x00028D33
		public RoleServerIdParameter()
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0002AB3B File Offset: 0x00028D3B
		protected RoleServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E5D RID: 3677
		protected abstract ServerRole RoleRestriction { get; }

		// Token: 0x06000E5E RID: 3678 RVA: 0x0002AB44 File Offset: 0x00028D44
		internal override IEnumerableFilter<T> GetEnumerableFilter<T>()
		{
			return ServerRoleFilter<T>.GetServerRoleFilter(this.RoleRestriction);
		}
	}
}
