using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000192 RID: 402
	[Serializable]
	public class UMServerIdParameter : RoleServerIdParameter
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0002B0FB File Offset: 0x000292FB
		public UMServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002B104 File Offset: 0x00029304
		public UMServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002B10D File Offset: 0x0002930D
		public UMServerIdParameter()
		{
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002B115 File Offset: 0x00029315
		protected UMServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0002B11E File Offset: 0x0002931E
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.UnifiedMessaging;
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002B122 File Offset: 0x00029322
		public new static UMServerIdParameter Parse(string identity)
		{
			return new UMServerIdParameter(identity);
		}
	}
}
