using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000DE RID: 222
	internal class PasswordDeriveBytes : DeriveBytes
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x0001CA25 File Offset: 0x0001AC25
		internal PasswordDeriveBytes(string strPassword, byte[] rgbSalt) : this(strPassword, rgbSalt, "SHA1", 100)
		{
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001CA36 File Offset: 0x0001AC36
		internal PasswordDeriveBytes(byte[] password, byte[] salt) : this(password, salt, "SHA1", 100)
		{
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001CA47 File Offset: 0x0001AC47
		internal PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations) : this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations)
		{
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001CA5F File Offset: 0x0001AC5F
		internal PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations)
		{
			this.IterationCount = iterations;
			this.Salt = salt;
			this.HashName = hashName;
			this.password = password;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001CA84 File Offset: 0x0001AC84
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001CA8C File Offset: 0x0001AC8C
		internal string HashName
		{
			get
			{
				return this.hashName;
			}
			set
			{
				if (this.baseValue != null)
				{
					throw new CryptographicException(Strings.PasswordDerivedBytesValuesFixed("HashName"));
				}
				this.hashName = value;
				this.hash = PasswordDeriveBytes.CreateFromName(this.hashName);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001CAC3 File Offset: 0x0001ACC3
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001CACB File Offset: 0x0001ACCB
		internal int IterationCount
		{
			get
			{
				return this.iterations;
			}
			set
			{
				if (this.baseValue != null)
				{
					throw new CryptographicException(Strings.PasswordDerivedBytesValuesFixed("IterationCount"));
				}
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Strings.PasswordDerivedBytesNeedNonNegNum);
				}
				this.iterations = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001CB0A File Offset: 0x0001AD0A
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001CB26 File Offset: 0x0001AD26
		internal byte[] Salt
		{
			get
			{
				if (this.salt == null)
				{
					return null;
				}
				return (byte[])this.salt.Clone();
			}
			set
			{
				if (this.baseValue != null)
				{
					throw new CryptographicException(Strings.PasswordDerivedBytesValuesFixed("Salt"));
				}
				if (value == null)
				{
					this.salt = null;
					return;
				}
				this.salt = (byte[])value.Clone();
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001CB64 File Offset: 0x0001AD64
		public override byte[] GetBytes(int cb)
		{
			int num = 0;
			byte[] array = new byte[cb];
			if (this.baseValue == null)
			{
				this.ComputeBaseValue();
			}
			else if (this.extra != null)
			{
				num = this.extra.Length - this.extraCount;
				if (num >= cb)
				{
					Buffer.BlockCopy(this.extra, this.extraCount, array, 0, cb);
					if (num > cb)
					{
						this.extraCount += cb;
					}
					else
					{
						this.extra = null;
					}
					return array;
				}
				Buffer.BlockCopy(this.extra, num, array, 0, num);
				this.extra = null;
			}
			byte[] array2 = this.ComputeBytes(cb - num);
			Buffer.BlockCopy(array2, 0, array, num, cb - num);
			if (array2.Length + num > cb)
			{
				this.extra = array2;
				this.extraCount = cb - num;
			}
			return array;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001CC1D File Offset: 0x0001AE1D
		public override void Reset()
		{
			this.prefix = 0;
			this.extra = null;
			this.baseValue = null;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001CC34 File Offset: 0x0001AE34
		private static HashAlgorithm CreateFromName(string hashName)
		{
			if (string.Equals(hashName, "SHA256CryptoServiceProvider", StringComparison.InvariantCultureIgnoreCase))
			{
				return new SHA256CryptoServiceProvider();
			}
			if (string.Equals(hashName, "SHA256", StringComparison.InvariantCultureIgnoreCase))
			{
				return new SHA256CryptoServiceProvider();
			}
			if (string.Equals(hashName, "SHA512", StringComparison.InvariantCultureIgnoreCase))
			{
				return new SHA512CryptoServiceProvider();
			}
			return (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001CC88 File Offset: 0x0001AE88
		private byte[] ComputeBaseValue()
		{
			this.hash.Initialize();
			this.hash.TransformBlock(this.password, 0, this.password.Length, this.password, 0);
			if (this.salt != null)
			{
				this.hash.TransformBlock(this.salt, 0, this.salt.Length, this.salt, 0);
			}
			this.hash.TransformFinalBlock(new byte[0], 0, 0);
			this.baseValue = this.hash.Hash;
			this.hash.Initialize();
			for (int i = 1; i < this.iterations - 1; i++)
			{
				this.hash.ComputeHash(this.baseValue);
				this.baseValue = this.hash.Hash;
			}
			return this.baseValue;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001CD58 File Offset: 0x0001AF58
		private byte[] ComputeBytes(int cb)
		{
			int num = 0;
			this.hash.Initialize();
			int num2 = this.hash.HashSize / 8;
			byte[] array = new byte[(cb + num2 - 1) / num2 * num2];
			CryptoStream cryptoStream = new CryptoStream(Stream.Null, this.hash, CryptoStreamMode.Write);
			this.HashPrefix(cryptoStream);
			cryptoStream.Write(this.baseValue, 0, this.baseValue.Length);
			cryptoStream.Close();
			Buffer.BlockCopy(this.hash.Hash, 0, array, num, num2);
			num += num2;
			while (cb > num)
			{
				this.hash.Initialize();
				cryptoStream = new CryptoStream(Stream.Null, this.hash, CryptoStreamMode.Write);
				this.HashPrefix(cryptoStream);
				cryptoStream.Write(this.baseValue, 0, this.baseValue.Length);
				cryptoStream.Close();
				Buffer.BlockCopy(this.hash.Hash, 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001CE44 File Offset: 0x0001B044
		private void HashPrefix(CryptoStream cs)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				48,
				48,
				48
			};
			if (this.prefix > 999)
			{
				throw new CryptographicException(Strings.PasswordDerivedBytesTooManyBytes);
			}
			if (this.prefix >= 100)
			{
				byte[] array2 = array;
				int num2 = 0;
				array2[num2] += (byte)(this.prefix / 100);
				num++;
			}
			if (this.prefix >= 10)
			{
				byte[] array3 = array;
				int num3 = num;
				array3[num3] += (byte)(this.prefix % 100 / 10);
				num++;
			}
			if (this.prefix > 0)
			{
				byte[] array4 = array;
				int num4 = num;
				array4[num4] += (byte)(this.prefix % 10);
				num++;
				cs.Write(array, 0, num);
			}
			this.prefix++;
		}

		// Token: 0x04000427 RID: 1063
		private int extraCount;

		// Token: 0x04000428 RID: 1064
		private int prefix;

		// Token: 0x04000429 RID: 1065
		private int iterations;

		// Token: 0x0400042A RID: 1066
		private byte[] baseValue;

		// Token: 0x0400042B RID: 1067
		private byte[] extra;

		// Token: 0x0400042C RID: 1068
		private byte[] salt;

		// Token: 0x0400042D RID: 1069
		private string hashName;

		// Token: 0x0400042E RID: 1070
		private byte[] password;

		// Token: 0x0400042F RID: 1071
		private HashAlgorithm hash;
	}
}
