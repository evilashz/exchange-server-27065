using System;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B68 RID: 2920
	internal sealed class FederatedIdentity
	{
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06003E7A RID: 15994 RVA: 0x000A308E File Offset: 0x000A128E
		// (set) Token: 0x06003E7B RID: 15995 RVA: 0x000A3096 File Offset: 0x000A1296
		public string Identity { get; private set; }

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x000A309F File Offset: 0x000A129F
		// (set) Token: 0x06003E7D RID: 15997 RVA: 0x000A30A7 File Offset: 0x000A12A7
		public IdentityType Type { get; private set; }

		// Token: 0x06003E7E RID: 15998 RVA: 0x000A30B0 File Offset: 0x000A12B0
		public FederatedIdentity(string identity, IdentityType type)
		{
			this.Identity = identity;
			this.Type = type;
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x000A30C6 File Offset: 0x000A12C6
		public override string ToString()
		{
			return this.Type + ":" + this.Identity;
		}
	}
}
