using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	internal class SimpleIdentity<TIdentity> : IIdentity, IEquatable<IIdentity>
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000038EB File Offset: 0x00001AEB
		public SimpleIdentity(TIdentity identity)
		{
			this.identity = identity;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000038FA File Offset: 0x00001AFA
		public TIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003904 File Offset: 0x00001B04
		public override int GetHashCode()
		{
			TIdentity tidentity = this.identity;
			return tidentity.GetHashCode();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003925 File Offset: 0x00001B25
		public override bool Equals(object other)
		{
			return this.Equals(other as SimpleIdentity<TIdentity>);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003933 File Offset: 0x00001B33
		public virtual bool Equals(IIdentity other)
		{
			return this.Equals(other as SimpleIdentity<TIdentity>);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003944 File Offset: 0x00001B44
		public override string ToString()
		{
			TIdentity tidentity = this.identity;
			return tidentity.ToString();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003968 File Offset: 0x00001B68
		private bool Equals(SimpleIdentity<TIdentity> other)
		{
			if (other == null)
			{
				return false;
			}
			TIdentity tidentity = this.identity;
			return tidentity.Equals(other.identity);
		}

		// Token: 0x040000E6 RID: 230
		private readonly TIdentity identity;
	}
}
