using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	internal class SimpleIdentityWithComparer<TIdentity, TComparer> : IIdentity, IEquatable<IIdentity> where TComparer : IComparer<TIdentity>
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00003999 File Offset: 0x00001B99
		public SimpleIdentityWithComparer(TIdentity identity, TComparer comparer)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity is Null");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("Comparer is Null");
			}
			this.identity = identity;
			this.comparer = comparer;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000039D8 File Offset: 0x00001BD8
		public override string ToString()
		{
			TIdentity tidentity = this.identity;
			return tidentity.ToString();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000039F9 File Offset: 0x00001BF9
		public override bool Equals(object other)
		{
			return this.Equals(other as SimpleIdentityWithComparer<TIdentity, TComparer>);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003A07 File Offset: 0x00001C07
		public bool Equals(IIdentity other)
		{
			return this.Equals(other as SimpleIdentityWithComparer<TIdentity, TComparer>);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003A18 File Offset: 0x00001C18
		public override int GetHashCode()
		{
			if (this.identity is string && this.comparer is StringComparer)
			{
				int hashCode = (this.identity as string).ToLower().GetHashCode();
				TComparer tcomparer = this.comparer;
				return hashCode ^ tcomparer.GetHashCode();
			}
			TIdentity tidentity = this.identity;
			int hashCode2 = tidentity.GetHashCode();
			TComparer tcomparer2 = this.comparer;
			return hashCode2 ^ tcomparer2.GetHashCode();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003AA4 File Offset: 0x00001CA4
		private bool Equals(SimpleIdentityWithComparer<TIdentity, TComparer> other)
		{
			if (other == null)
			{
				return false;
			}
			TComparer tcomparer = this.comparer;
			if (tcomparer.Compare(this.identity, other.identity) == 0)
			{
				TComparer tcomparer2 = other.comparer;
				return tcomparer2.Compare(other.identity, this.identity) == 0;
			}
			return false;
		}

		// Token: 0x040000E7 RID: 231
		private readonly TIdentity identity;

		// Token: 0x040000E8 RID: 232
		private readonly TComparer comparer;
	}
}
