using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000307 RID: 775
	[ComVisible(false)]
	public abstract class IdentityReference
	{
		// Token: 0x060027D5 RID: 10197 RVA: 0x000925FB File Offset: 0x000907FB
		internal IdentityReference()
		{
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060027D6 RID: 10198
		public abstract string Value { get; }

		// Token: 0x060027D7 RID: 10199
		public abstract bool IsValidTargetType(Type targetType);

		// Token: 0x060027D8 RID: 10200
		public abstract IdentityReference Translate(Type targetType);

		// Token: 0x060027D9 RID: 10201
		public abstract override bool Equals(object o);

		// Token: 0x060027DA RID: 10202
		public abstract override int GetHashCode();

		// Token: 0x060027DB RID: 10203
		public abstract override string ToString();

		// Token: 0x060027DC RID: 10204 RVA: 0x00092604 File Offset: 0x00090804
		public static bool operator ==(IdentityReference left, IdentityReference right)
		{
			return (left == null && right == null) || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x0009262C File Offset: 0x0009082C
		public static bool operator !=(IdentityReference left, IdentityReference right)
		{
			return !(left == right);
		}
	}
}
