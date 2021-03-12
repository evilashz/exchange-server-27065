using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000189 RID: 393
	[Serializable]
	public class ClientAccessServerIdParameter : RoleServerIdParameter
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x0002AB51 File Offset: 0x00028D51
		public ClientAccessServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0002AB5A File Offset: 0x00028D5A
		public ClientAccessServerIdParameter(ClientAccessServer caServer) : base(caServer.Id)
		{
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002AB68 File Offset: 0x00028D68
		public ClientAccessServerIdParameter(ExchangeRpcClientAccess rpcClientAccess) : base(rpcClientAccess.Server)
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0002AB76 File Offset: 0x00028D76
		public ClientAccessServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002AB7F File Offset: 0x00028D7F
		public ClientAccessServerIdParameter()
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002AB87 File Offset: 0x00028D87
		protected ClientAccessServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0002AB90 File Offset: 0x00028D90
		protected override ServerRole RoleRestriction
		{
			get
			{
				throw new NotImplementedException("DEV BUG, this method should not be invoked.");
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002AB9C File Offset: 0x00028D9C
		internal override IEnumerableFilter<T> GetEnumerableFilter<T>()
		{
			return new ClientAccessServerIdParameter.E15CafeOrE14CASFilter<T>();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002ABA3 File Offset: 0x00028DA3
		public new static ClientAccessServerIdParameter Parse(string identity)
		{
			return new ClientAccessServerIdParameter(identity);
		}

		// Token: 0x0200018A RID: 394
		private class E15CafeOrE14CASFilter<T> : IEnumerableFilter<T>
		{
			// Token: 0x06000E68 RID: 3688 RVA: 0x0002ABAC File Offset: 0x00028DAC
			public bool AcceptElement(T element)
			{
				if (element == null)
				{
					return false;
				}
				Server server = element as Server;
				return server != null && (server.IsCafeServer || (server.IsClientAccessServer && server.VersionNumber < Server.E15MinVersion));
			}
		}
	}
}
