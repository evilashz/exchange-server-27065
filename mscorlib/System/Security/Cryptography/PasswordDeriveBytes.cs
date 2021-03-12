using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
	// Token: 0x02000273 RID: 627
	[ComVisible(true)]
	public class PasswordDeriveBytes : DeriveBytes
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000789D0 File Offset: 0x00076BD0
		private SafeProvHandle ProvHandle
		{
			[SecurityCritical]
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							SafeProvHandle safeProvHandle = Utils.AcquireProvHandle(this._cspParams);
							Thread.MemoryBarrier();
							this._safeProvHandle = safeProvHandle;
						}
					}
				}
				return this._safeProvHandle;
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00078A34 File Offset: 0x00076C34
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt) : this(strPassword, rgbSalt, new CspParameters())
		{
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00078A43 File Offset: 0x00076C43
		public PasswordDeriveBytes(byte[] password, byte[] salt) : this(password, salt, new CspParameters())
		{
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00078A52 File Offset: 0x00076C52
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations) : this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
		{
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00078A64 File Offset: 0x00076C64
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations) : this(password, salt, hashName, iterations, new CspParameters())
		{
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00078A76 File Offset: 0x00076C76
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams) : this(strPassword, rgbSalt, "SHA1", 100, cspParams)
		{
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00078A88 File Offset: 0x00076C88
		public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams) : this(password, salt, "SHA1", 100, cspParams)
		{
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00078A9A File Offset: 0x00076C9A
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams) : this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
		{
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00078AB4 File Offset: 0x00076CB4
		[SecuritySafeCritical]
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
		{
			this.IterationCount = iterations;
			this.Salt = salt;
			this.HashName = hashName;
			this._password = password;
			this._cspParams = cspParams;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00078AE1 File Offset: 0x00076CE1
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x00078AEC File Offset: 0x00076CEC
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", new object[]
					{
						"HashName"
					}));
				}
				this._hashName = value;
				this._hash = (HashAlgorithm)CryptoConfig.CreateFromName(this._hashName);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00078B3C File Offset: 0x00076D3C
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x00078B44 File Offset: 0x00076D44
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
				}
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", new object[]
					{
						"IterationCount"
					}));
				}
				this._iterations = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00078B97 File Offset: 0x00076D97
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x00078BB4 File Offset: 0x00076DB4
		public byte[] Salt
		{
			get
			{
				if (this._salt == null)
				{
					return null;
				}
				return (byte[])this._salt.Clone();
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_ValuesFixed", new object[]
					{
						"Salt"
					}));
				}
				if (value == null)
				{
					this._salt = null;
					return;
				}
				this._salt = (byte[])value.Clone();
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x00078C04 File Offset: 0x00076E04
		[SecuritySafeCritical]
		[Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
		public override byte[] GetBytes(int cb)
		{
			int num = 0;
			byte[] array = new byte[cb];
			if (this._baseValue == null)
			{
				this.ComputeBaseValue();
			}
			else if (this._extra != null)
			{
				num = this._extra.Length - this._extraCount;
				if (num >= cb)
				{
					Buffer.InternalBlockCopy(this._extra, this._extraCount, array, 0, cb);
					if (num > cb)
					{
						this._extraCount += cb;
					}
					else
					{
						this._extra = null;
					}
					return array;
				}
				Buffer.InternalBlockCopy(this._extra, num, array, 0, num);
				this._extra = null;
			}
			byte[] array2 = this.ComputeBytes(cb - num);
			Buffer.InternalBlockCopy(array2, 0, array, num, cb - num);
			if (array2.Length + num > cb)
			{
				this._extra = array2;
				this._extraCount = cb - num;
			}
			return array;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x00078CBD File Offset: 0x00076EBD
		public override void Reset()
		{
			this._prefix = 0;
			this._extra = null;
			this._baseValue = null;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00078CD4 File Offset: 0x00076ED4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._hash != null)
				{
					this._hash.Dispose();
				}
				if (this._baseValue != null)
				{
					Array.Clear(this._baseValue, 0, this._baseValue.Length);
				}
				if (this._extra != null)
				{
					Array.Clear(this._extra, 0, this._extra.Length);
				}
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				if (this._salt != null)
				{
					Array.Clear(this._salt, 0, this._salt.Length);
				}
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x00078D74 File Offset: 0x00076F74
		[SecuritySafeCritical]
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			int num = X509Utils.NameOrOidToAlgId(alghashname, OidGroup.HashAlgorithm);
			if (num == 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
			}
			int num2 = X509Utils.NameOrOidToAlgId(algname, OidGroup.AllGroups);
			if (num2 == 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidAlgorithm"));
			}
			if (rgbIV == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_InvalidIV"));
			}
			byte[] result = null;
			PasswordDeriveBytes.DeriveKey(this.ProvHandle, num2, num, this._password, this._password.Length, keySize << 16, rgbIV, rgbIV.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref result));
			return result;
		}

		// Token: 0x0600223D RID: 8765
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DeriveKey(SafeProvHandle hProv, int algid, int algidHash, byte[] password, int cbPassword, int dwFlags, byte[] IV, int cbIV, ObjectHandleOnStack retKey);

		// Token: 0x0600223E RID: 8766 RVA: 0x00078E10 File Offset: 0x00077010
		private byte[] ComputeBaseValue()
		{
			this._hash.Initialize();
			this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
			if (this._salt != null)
			{
				this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
			}
			this._hash.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			this._baseValue = this._hash.Hash;
			this._hash.Initialize();
			for (int i = 1; i < this._iterations - 1; i++)
			{
				this._hash.ComputeHash(this._baseValue);
				this._baseValue = this._hash.Hash;
			}
			return this._baseValue;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00078EE0 File Offset: 0x000770E0
		[SecurityCritical]
		private byte[] ComputeBytes(int cb)
		{
			int num = 0;
			this._hash.Initialize();
			int num2 = this._hash.HashSize / 8;
			byte[] array = new byte[(cb + num2 - 1) / num2 * num2];
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
			{
				this.HashPrefix(cryptoStream);
				cryptoStream.Write(this._baseValue, 0, this._baseValue.Length);
				cryptoStream.Close();
			}
			Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
			num += num2;
			while (cb > num)
			{
				this._hash.Initialize();
				using (CryptoStream cryptoStream2 = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
				{
					this.HashPrefix(cryptoStream2);
					cryptoStream2.Write(this._baseValue, 0, this._baseValue.Length);
					cryptoStream2.Close();
				}
				Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00078FFC File Offset: 0x000771FC
		private void HashPrefix(CryptoStream cs)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				48,
				48,
				48
			};
			if (this._prefix > 999)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_PasswordDerivedBytes_TooManyBytes"));
			}
			if (this._prefix >= 100)
			{
				byte[] array2 = array;
				int num2 = 0;
				array2[num2] += (byte)(this._prefix / 100);
				num++;
			}
			if (this._prefix >= 10)
			{
				byte[] array3 = array;
				int num3 = num;
				array3[num3] += (byte)(this._prefix % 100 / 10);
				num++;
			}
			if (this._prefix > 0)
			{
				byte[] array4 = array;
				int num4 = num;
				array4[num4] += (byte)(this._prefix % 10);
				num++;
				cs.Write(array, 0, num);
			}
			this._prefix++;
		}

		// Token: 0x04000C65 RID: 3173
		private int _extraCount;

		// Token: 0x04000C66 RID: 3174
		private int _prefix;

		// Token: 0x04000C67 RID: 3175
		private int _iterations;

		// Token: 0x04000C68 RID: 3176
		private byte[] _baseValue;

		// Token: 0x04000C69 RID: 3177
		private byte[] _extra;

		// Token: 0x04000C6A RID: 3178
		private byte[] _salt;

		// Token: 0x04000C6B RID: 3179
		private string _hashName;

		// Token: 0x04000C6C RID: 3180
		private byte[] _password;

		// Token: 0x04000C6D RID: 3181
		private HashAlgorithm _hash;

		// Token: 0x04000C6E RID: 3182
		private CspParameters _cspParams;

		// Token: 0x04000C6F RID: 3183
		[SecurityCritical]
		private SafeProvHandle _safeProvHandle;
	}
}
