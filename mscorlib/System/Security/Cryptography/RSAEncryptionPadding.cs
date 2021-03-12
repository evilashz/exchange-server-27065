using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000280 RID: 640
	public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0007D317 File Offset: 0x0007B517
		public static RSAEncryptionPadding Pkcs1
		{
			get
			{
				return RSAEncryptionPadding.s_pkcs1;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0007D31E File Offset: 0x0007B51E
		public static RSAEncryptionPadding OaepSHA1
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA1;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0007D325 File Offset: 0x0007B525
		public static RSAEncryptionPadding OaepSHA256
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA256;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0007D32C File Offset: 0x0007B52C
		public static RSAEncryptionPadding OaepSHA384
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA384;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0007D333 File Offset: 0x0007B533
		public static RSAEncryptionPadding OaepSHA512
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA512;
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0007D33A File Offset: 0x0007B53A
		private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
		{
			this._mode = mode;
			this._oaepHashAlgorithm = oaepHashAlgorithm;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0007D350 File Offset: 0x0007B550
		public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0007D37C File Offset: 0x0007B57C
		public RSAEncryptionPaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x0007D384 File Offset: 0x0007B584
		public HashAlgorithmName OaepHashAlgorithm
		{
			get
			{
				return this._oaepHashAlgorithm;
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0007D38C File Offset: 0x0007B58C
		public override int GetHashCode()
		{
			return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0007D3B5 File Offset: 0x0007B5B5
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0007D3BE File Offset: 0x0007B5BE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSAEncryptionPadding);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0007D3CC File Offset: 0x0007B5CC
		public bool Equals(RSAEncryptionPadding other)
		{
			return other != null && this._mode == other._mode && this._oaepHashAlgorithm == other._oaepHashAlgorithm;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0007D3F8 File Offset: 0x0007B5F8
		public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0007D409 File Offset: 0x0007B609
		public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			return !(left == right);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0007D415 File Offset: 0x0007B615
		public override string ToString()
		{
			return this._mode.ToString() + this._oaepHashAlgorithm.Name;
		}

		// Token: 0x04000CA1 RID: 3233
		private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, default(HashAlgorithmName));

		// Token: 0x04000CA2 RID: 3234
		private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);

		// Token: 0x04000CA3 RID: 3235
		private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);

		// Token: 0x04000CA4 RID: 3236
		private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);

		// Token: 0x04000CA5 RID: 3237
		private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);

		// Token: 0x04000CA6 RID: 3238
		private RSAEncryptionPaddingMode _mode;

		// Token: 0x04000CA7 RID: 3239
		private HashAlgorithmName _oaepHashAlgorithm;
	}
}
