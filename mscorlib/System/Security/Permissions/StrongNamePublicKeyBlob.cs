using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DE RID: 734
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		// Token: 0x06002648 RID: 9800 RVA: 0x0008AAAA File Offset: 0x00088CAA
		internal StrongNamePublicKeyBlob()
		{
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x0008AAB2 File Offset: 0x00088CB2
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("PublicKey");
			}
			this.PublicKey = new byte[publicKey.Length];
			Array.Copy(publicKey, 0, this.PublicKey, 0, publicKey.Length);
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x0008AAE7 File Offset: 0x00088CE7
		internal StrongNamePublicKeyBlob(string publicKey)
		{
			this.PublicKey = Hex.DecodeHexString(publicKey);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x0008AAFC File Offset: 0x00088CFC
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0008AB2E File Offset: 0x00088D2E
		internal bool Equals(StrongNamePublicKeyBlob blob)
		{
			return blob != null && StrongNamePublicKeyBlob.CompareArrays(this.PublicKey, blob.PublicKey);
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0008AB46 File Offset: 0x00088D46
		public override bool Equals(object obj)
		{
			return obj != null && obj is StrongNamePublicKeyBlob && this.Equals((StrongNamePublicKeyBlob)obj);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0008AB64 File Offset: 0x00088D64
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8 ^ (int)baData[i] ^ num >> 24);
			}
			return num;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0008AB94 File Offset: 0x00088D94
		public override int GetHashCode()
		{
			return StrongNamePublicKeyBlob.GetByteArrayHashCode(this.PublicKey);
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0008ABA1 File Offset: 0x00088DA1
		public override string ToString()
		{
			return Hex.EncodeHexString(this.PublicKey);
		}

		// Token: 0x04000E8E RID: 3726
		internal byte[] PublicKey;
	}
}
