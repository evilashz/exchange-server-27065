using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000191 RID: 401
	[Serializable]
	public class TransportServerIdParameter : RoleServerIdParameter
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x0002B0CC File Offset: 0x000292CC
		public TransportServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002B0D5 File Offset: 0x000292D5
		public TransportServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0002B0DE File Offset: 0x000292DE
		public TransportServerIdParameter()
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002B0E6 File Offset: 0x000292E6
		protected TransportServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0002B0EF File Offset: 0x000292EF
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.HubTransport | ServerRole.Edge;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0002B0F3 File Offset: 0x000292F3
		public new static TransportServerIdParameter Parse(string identity)
		{
			return new TransportServerIdParameter(identity);
		}
	}
}
