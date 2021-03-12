using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200006E RID: 110
	internal class EncryptedBuffer
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		internal EncryptedBuffer(byte[] input) : this(input, string.Empty)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		internal EncryptedBuffer(byte[] input, string stopTones)
		{
			if (input == null || input.Length == 0)
			{
				this.sbuf = new byte[0];
				return;
			}
			int num = input.Length;
			byte[] bytes = Encoding.ASCII.GetBytes(stopTones);
			if (-1 != Array.IndexOf<byte>(bytes, input[num - 1]))
			{
				num--;
				if (num == 0)
				{
					this.sbuf = new byte[0];
					return;
				}
			}
			this.entropy = new byte[8];
			EncryptedBuffer.rng.GetBytes(this.entropy);
			if (num != input.Length)
			{
				using (SafeBuffer safeBuffer = new SafeBuffer(num))
				{
					Array.Copy(input, safeBuffer.Buffer, num);
					this.sbuf = ProtectedData.Protect(safeBuffer.Buffer, this.entropy, DataProtectionScope.CurrentUser);
					return;
				}
			}
			this.sbuf = ProtectedData.Protect(input, this.entropy, DataProtectionScope.CurrentUser);
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000E494 File Offset: 0x0000C694
		internal SafeBuffer Decrypted
		{
			get
			{
				SafeBuffer result;
				if (this.sbuf != null && this.sbuf.Length > 0)
				{
					result = new SafeBuffer(ProtectedData.Unprotect(this.sbuf, this.entropy, DataProtectionScope.CurrentUser));
				}
				else
				{
					result = new SafeBuffer(new byte[0]);
				}
				return result;
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000E4DD File Offset: 0x0000C6DD
		public static bool operator ==(EncryptedBuffer lhs, EncryptedBuffer rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000E4E6 File Offset: 0x0000C6E6
		public static bool operator !=(EncryptedBuffer lhs, EncryptedBuffer rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			EncryptedBuffer encryptedBuffer = o as EncryptedBuffer;
			if (encryptedBuffer == null)
			{
				return false;
			}
			using (SafeBuffer decrypted = this.Decrypted)
			{
				using (SafeBuffer decrypted2 = encryptedBuffer.Decrypted)
				{
					if (decrypted == null || decrypted2 == null)
					{
						return decrypted == null && null == decrypted2;
					}
					if (decrypted.Buffer == null || decrypted2.Buffer == null)
					{
						return decrypted.Buffer == null && null == decrypted2.Buffer;
					}
					if (decrypted.Buffer.Length != decrypted2.Buffer.Length)
					{
						return false;
					}
					for (int i = 0; i < decrypted.Buffer.Length; i++)
					{
						if (decrypted.Buffer[i] != decrypted2.Buffer[i])
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public override int GetHashCode()
		{
			if (this.sbuf == null || this.sbuf.Length < 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(this.sbuf, 0);
		}

		// Token: 0x040002C1 RID: 705
		private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

		// Token: 0x040002C2 RID: 706
		private byte[] sbuf;

		// Token: 0x040002C3 RID: 707
		private byte[] entropy;
	}
}
