using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200027C RID: 636
	public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
	{
		// Token: 0x06002297 RID: 8855 RVA: 0x0007C68F File Offset: 0x0007A88F
		private RSASignaturePadding(RSASignaturePaddingMode mode)
		{
			this._mode = mode;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0007C69E File Offset: 0x0007A89E
		public static RSASignaturePadding Pkcs1
		{
			get
			{
				return RSASignaturePadding.s_pkcs1;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0007C6A5 File Offset: 0x0007A8A5
		public static RSASignaturePadding Pss
		{
			get
			{
				return RSASignaturePadding.s_pss;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x0007C6AC File Offset: 0x0007A8AC
		public RSASignaturePaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0007C6B4 File Offset: 0x0007A8B4
		public override int GetHashCode()
		{
			return this._mode.GetHashCode();
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007C6D5 File Offset: 0x0007A8D5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSASignaturePadding);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0007C6E3 File Offset: 0x0007A8E3
		public bool Equals(RSASignaturePadding other)
		{
			return other != null && this._mode == other._mode;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0007C6FE File Offset: 0x0007A8FE
		public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0007C70F File Offset: 0x0007A90F
		public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
		{
			return !(left == right);
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007C71C File Offset: 0x0007A91C
		public override string ToString()
		{
			return this._mode.ToString();
		}

		// Token: 0x04000C8D RID: 3213
		private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);

		// Token: 0x04000C8E RID: 3214
		private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);

		// Token: 0x04000C8F RID: 3215
		private readonly RSASignaturePaddingMode _mode;
	}
}
