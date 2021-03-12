using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DA5 RID: 3493
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class SharingAnonymousIdentityCacheKey
	{
		// Token: 0x0600780B RID: 30731 RVA: 0x00211DCD File Offset: 0x0020FFCD
		protected SharingAnonymousIdentityCacheKey(int cachedHashCode)
		{
			this.cachedHashCode = cachedHashCode;
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x00211DDC File Offset: 0x0020FFDC
		public static bool operator ==(SharingAnonymousIdentityCacheKey key1, SharingAnonymousIdentityCacheKey key2)
		{
			return object.Equals(key1, key2);
		}

		// Token: 0x0600780D RID: 30733 RVA: 0x00211DE5 File Offset: 0x0020FFE5
		public static bool operator !=(SharingAnonymousIdentityCacheKey key1, SharingAnonymousIdentityCacheKey key2)
		{
			return !object.Equals(key1, key2);
		}

		// Token: 0x0600780E RID: 30734
		public abstract string Lookup(out SecurityIdentifier sid);

		// Token: 0x0600780F RID: 30735 RVA: 0x00211DF1 File Offset: 0x0020FFF1
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x06007810 RID: 30736 RVA: 0x00211DF9 File Offset: 0x0020FFF9
		public override bool Equals(object obj)
		{
			return this.InternalEquals(obj);
		}

		// Token: 0x06007811 RID: 30737
		protected abstract bool InternalEquals(object obj);

		// Token: 0x04005316 RID: 21270
		private readonly int cachedHashCode;
	}
}
