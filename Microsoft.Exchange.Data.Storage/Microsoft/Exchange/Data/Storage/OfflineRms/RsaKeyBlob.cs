using System;
using System.Security.Cryptography;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD7 RID: 2775
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RsaKeyBlob : IDisposable
	{
		// Token: 0x060064C2 RID: 25794 RVA: 0x001AB7C0 File Offset: 0x001A99C0
		public RsaKeyBlob(byte[] keyBlob)
		{
			this.init(keyBlob);
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x060064C3 RID: 25795 RVA: 0x001AB7CF File Offset: 0x001A99CF
		public byte[] D
		{
			get
			{
				return this.m_D;
			}
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x001AB7D8 File Offset: 0x001A99D8
		public void Dispose()
		{
			this.m_bitLength = 0;
			RsaKeyBlob.clearArray(ref this.m_D);
			RsaKeyBlob.clearArray(ref this.m_DP);
			RsaKeyBlob.clearArray(ref this.m_DQ);
			RsaKeyBlob.clearArray(ref this.m_exponent);
			RsaKeyBlob.clearArray(ref this.m_inverseQ);
			this.m_isPrivateKey = false;
			RsaKeyBlob.clearArray(ref this.m_keyBlob);
			RsaKeyBlob.clearArray(ref this.m_modulus);
			RsaKeyBlob.clearArray(ref this.m_P);
			RsaKeyBlob.clearArray(ref this.m_Q);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x001AB85C File Offset: 0x001A9A5C
		public byte[] DP
		{
			get
			{
				return this.m_DP;
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x060064C6 RID: 25798 RVA: 0x001AB864 File Offset: 0x001A9A64
		public byte[] DQ
		{
			get
			{
				return this.m_DQ;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x060064C7 RID: 25799 RVA: 0x001AB86C File Offset: 0x001A9A6C
		public byte[] Exponent
		{
			get
			{
				return this.m_exponent;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x060064C8 RID: 25800 RVA: 0x001AB874 File Offset: 0x001A9A74
		public byte[] InverseQ
		{
			get
			{
				return this.m_inverseQ;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x060064C9 RID: 25801 RVA: 0x001AB87C File Offset: 0x001A9A7C
		public bool IsPrivateKey
		{
			get
			{
				return this.m_isPrivateKey;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x060064CA RID: 25802 RVA: 0x001AB884 File Offset: 0x001A9A84
		public int KeySize
		{
			get
			{
				return this.m_bitLength;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x001AB88C File Offset: 0x001A9A8C
		public byte[] Modulus
		{
			get
			{
				return this.m_modulus;
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x060064CC RID: 25804 RVA: 0x001AB894 File Offset: 0x001A9A94
		public byte[] P
		{
			get
			{
				return this.m_P;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x060064CD RID: 25805 RVA: 0x001AB89C File Offset: 0x001A9A9C
		public byte[] Q
		{
			get
			{
				return this.m_Q;
			}
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x001AB8A4 File Offset: 0x001A9AA4
		private static void assignPrivateKeyValue(byte[] keyBlob, ref int offset, out byte[] val, int length)
		{
			val = new byte[length];
			Array.Copy(keyBlob, offset, val, 0, length);
			offset += length;
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x001AB8C0 File Offset: 0x001A9AC0
		private static void checkKeyBlob(byte[] putativeKeyBlob)
		{
			if (!RsaKeyBlob.isValidKeyBlob(putativeKeyBlob))
			{
				throw new CryptographicException("RSA key blob data is corrupted");
			}
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x001AB8D5 File Offset: 0x001A9AD5
		private static void clearArray(ref byte[] array)
		{
			if (array != null)
			{
				Array.Clear(array, 0, array.Length);
				array = null;
			}
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x001AB8EC File Offset: 0x001A9AEC
		private static int getPrivateKeyLength(int blobLength)
		{
			int num = (blobLength - 20) * 16 / 9;
			if (num < 0 || (blobLength - 20) % 9 != 0)
			{
				throw new CryptographicException("Key size is invalid")
				{
					Data = 
					{
						{
							"Context",
							"RsaKeyBlob.getPrivateKeyLength"
						},
						{
							"bitLength",
							num
						}
					}
				};
			}
			return num;
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x001AB94C File Offset: 0x001A9B4C
		private static int getPublicKeyLength(int blobLength)
		{
			int num = (blobLength - 20) * 8;
			if (num < 0)
			{
				throw new CryptographicException("Key size is invalid")
				{
					Data = 
					{
						{
							"Context",
							"RsaKeyBlob.getPublicKeyLength"
						},
						{
							"bitLength",
							num
						}
					}
				};
			}
			return num;
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001AB9A0 File Offset: 0x001A9BA0
		private void init(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentException("InvalidKeyBlob", "keyBlob");
			}
			if (RsaKeyBlob.isValidKeyBlob(keyBlob))
			{
				this.m_keyBlob = (byte[])keyBlob.Clone();
			}
			else
			{
				if (keyBlob.Length <= 12)
				{
					throw new CryptographicException("Key blob is invalid");
				}
				this.m_keyBlob = new byte[keyBlob.Length - 12];
				Array.Copy(keyBlob, 12, this.m_keyBlob, 0, this.m_keyBlob.Length);
			}
			RsaKeyBlob.checkKeyBlob(this.m_keyBlob);
			if (keyBlob[0] == 7)
			{
				this.m_bitLength = RsaKeyBlob.getPrivateKeyLength(this.m_keyBlob.Length);
				this.m_isPrivateKey = true;
			}
			else
			{
				this.m_bitLength = RsaKeyBlob.getPublicKeyLength(this.m_keyBlob.Length);
				this.m_isPrivateKey = false;
			}
			this.m_modulus = new byte[this.m_bitLength / 8];
			Array.Copy(this.m_keyBlob, 20, this.m_modulus, 0, this.m_modulus.Length);
			this.m_exponent = new byte[4];
			Array.Copy(this.m_keyBlob, 16, this.m_exponent, 0, this.m_exponent.Length);
			if (this.m_isPrivateKey)
			{
				int num = 20 + this.m_modulus.Length;
				RsaKeyBlob.assignPrivateKeyValue(this.m_keyBlob, ref num, out this.m_P, this.m_modulus.Length / 2);
				RsaKeyBlob.assignPrivateKeyValue(this.m_keyBlob, ref num, out this.m_Q, this.m_modulus.Length / 2);
				RsaKeyBlob.assignPrivateKeyValue(this.m_keyBlob, ref num, out this.m_DP, this.m_modulus.Length / 2);
				RsaKeyBlob.assignPrivateKeyValue(this.m_keyBlob, ref num, out this.m_DQ, this.m_modulus.Length / 2);
				RsaKeyBlob.assignPrivateKeyValue(this.m_keyBlob, ref num, out this.m_inverseQ, this.m_modulus.Length / 2);
				RsaKeyBlob.assignPrivateKeyValue(this.m_keyBlob, ref num, out this.m_D, this.m_modulus.Length);
			}
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x001ABB78 File Offset: 0x001A9D78
		private static bool isValidKeyBlob(byte[] putativeKeyBlob)
		{
			bool flag = RsaKeyBlob.isValuePresent(putativeKeyBlob, 0, 1, new int[]
			{
				6
			});
			int num = (int)BitConverter.ToUInt32(putativeKeyBlob, 12);
			return RsaKeyBlob.isValuePresent(putativeKeyBlob, 0, 1, new int[]
			{
				flag ? 6 : 7
			}) && RsaKeyBlob.isValuePresent(putativeKeyBlob, 4, 2, new int[]
			{
				41984,
				9216
			}) && RsaKeyBlob.isValuePresent(putativeKeyBlob, 8, 4, new int[]
			{
				flag ? 826364754 : 843141970
			}) && num == (flag ? RsaKeyBlob.getPublicKeyLength(putativeKeyBlob.Length) : RsaKeyBlob.getPrivateKeyLength(putativeKeyBlob.Length));
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x001ABC28 File Offset: 0x001A9E28
		private static bool isValuePresent(byte[] array, int offset, int length, params int[] values)
		{
			int num = (int)ByteArrayUtilities.ToUInt32(array, offset, length);
			foreach (int num2 in values)
			{
				if (num2 == num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003959 RID: 14681
		private const int EXPONENT_OFFSET = 16;

		// Token: 0x0400395A RID: 14682
		private const int MODULUS_LENGTH_OFFSET = 12;

		// Token: 0x0400395B RID: 14683
		private const int MODULUS_OFFSET = 20;

		// Token: 0x0400395C RID: 14684
		private const int MIN_KEY_LENGTH = 384;

		// Token: 0x0400395D RID: 14685
		private const int TYPE_OFFSET = 0;

		// Token: 0x0400395E RID: 14686
		private const int TYPE_LENGTH = 1;

		// Token: 0x0400395F RID: 14687
		private const byte PUBLIC_KEYBLOB_TYPE = 6;

		// Token: 0x04003960 RID: 14688
		private const byte PRIVATE_KEYBLOB_TYPE = 7;

		// Token: 0x04003961 RID: 14689
		private const int ALGORITHM_OFFSET = 4;

		// Token: 0x04003962 RID: 14690
		private const int ALGORITHM_LENGTH = 2;

		// Token: 0x04003963 RID: 14691
		private const int SIGNATURE_ALGORITHM = 9216;

		// Token: 0x04003964 RID: 14692
		private const int KEY_EXCHANGE_ALGORITHM = 41984;

		// Token: 0x04003965 RID: 14693
		private const int MAGIC_OFFSET = 8;

		// Token: 0x04003966 RID: 14694
		private const int MAGIC_LENGTH = 4;

		// Token: 0x04003967 RID: 14695
		private const int MAGIC_PUBLIC = 826364754;

		// Token: 0x04003968 RID: 14696
		private const int MAGIC_PRIVATE = 843141970;

		// Token: 0x04003969 RID: 14697
		private const int SN_HEADER_LENGTH = 12;

		// Token: 0x0400396A RID: 14698
		private int m_bitLength;

		// Token: 0x0400396B RID: 14699
		private byte[] m_D;

		// Token: 0x0400396C RID: 14700
		private byte[] m_DP;

		// Token: 0x0400396D RID: 14701
		private byte[] m_DQ;

		// Token: 0x0400396E RID: 14702
		private byte[] m_exponent;

		// Token: 0x0400396F RID: 14703
		private byte[] m_inverseQ;

		// Token: 0x04003970 RID: 14704
		private bool m_isPrivateKey;

		// Token: 0x04003971 RID: 14705
		private byte[] m_keyBlob;

		// Token: 0x04003972 RID: 14706
		private byte[] m_modulus;

		// Token: 0x04003973 RID: 14707
		private byte[] m_P;

		// Token: 0x04003974 RID: 14708
		private byte[] m_Q;
	}
}
