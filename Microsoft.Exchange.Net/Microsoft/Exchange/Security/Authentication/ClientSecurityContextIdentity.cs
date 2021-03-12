using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200063A RID: 1594
	[Serializable]
	public abstract class ClientSecurityContextIdentity : GenericIdentity
	{
		// Token: 0x06001CE2 RID: 7394 RVA: 0x00034095 File Offset: 0x00032295
		protected ClientSecurityContextIdentity(string name, string type) : base(name, type)
		{
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001CE3 RID: 7395
		public abstract SecurityIdentifier Sid { get; }

		// Token: 0x06001CE4 RID: 7396
		internal abstract ClientSecurityContext CreateClientSecurityContext();
	}
}
