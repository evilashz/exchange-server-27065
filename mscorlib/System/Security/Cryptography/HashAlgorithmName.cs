using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000269 RID: 617
	public struct HashAlgorithmName : IEquatable<HashAlgorithmName>
	{
		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00077E98 File Offset: 0x00076098
		public static HashAlgorithmName MD5
		{
			get
			{
				return new HashAlgorithmName("MD5");
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x00077EA4 File Offset: 0x000760A4
		public static HashAlgorithmName SHA1
		{
			get
			{
				return new HashAlgorithmName("SHA1");
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x00077EB0 File Offset: 0x000760B0
		public static HashAlgorithmName SHA256
		{
			get
			{
				return new HashAlgorithmName("SHA256");
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00077EBC File Offset: 0x000760BC
		public static HashAlgorithmName SHA384
		{
			get
			{
				return new HashAlgorithmName("SHA384");
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x00077EC8 File Offset: 0x000760C8
		public static HashAlgorithmName SHA512
		{
			get
			{
				return new HashAlgorithmName("SHA512");
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00077ED4 File Offset: 0x000760D4
		public HashAlgorithmName(string name)
		{
			this._name = name;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x00077EDD File Offset: 0x000760DD
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x00077EE5 File Offset: 0x000760E5
		public override string ToString()
		{
			return this._name ?? string.Empty;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00077EF6 File Offset: 0x000760F6
		public override bool Equals(object obj)
		{
			return obj is HashAlgorithmName && this.Equals((HashAlgorithmName)obj);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00077F0E File Offset: 0x0007610E
		public bool Equals(HashAlgorithmName other)
		{
			return this._name == other._name;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00077F21 File Offset: 0x00076121
		public override int GetHashCode()
		{
			if (this._name != null)
			{
				return this._name.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00077F38 File Offset: 0x00076138
		public static bool operator ==(HashAlgorithmName left, HashAlgorithmName right)
		{
			return left.Equals(right);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x00077F42 File Offset: 0x00076142
		public static bool operator !=(HashAlgorithmName left, HashAlgorithmName right)
		{
			return !(left == right);
		}

		// Token: 0x04000C53 RID: 3155
		private readonly string _name;
	}
}
