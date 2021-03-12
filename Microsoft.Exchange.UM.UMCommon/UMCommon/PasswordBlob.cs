using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000DD RID: 221
	internal sealed class PasswordBlob
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x0001C65C File Offset: 0x0001A85C
		internal PasswordBlob(byte[] blobdata)
		{
			this.Initialize(blobdata, 0);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001C66C File Offset: 0x0001A86C
		internal PasswordBlob(byte[] blobdata, int startIdx)
		{
			this.Initialize(blobdata, startIdx);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001C67C File Offset: 0x0001A87C
		internal PasswordBlob(EncryptedBuffer pwd, string algorithm, int iterations)
		{
			this.salt = new byte[8];
			PasswordBlob.rng.GetBytes(this.salt);
			this.Initialize(pwd, algorithm, iterations);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		private PasswordBlob(EncryptedBuffer pwd, byte[] salt, string algorithm, int iterations)
		{
			if (salt.Length != 8)
			{
				throw new UserConfigurationException(Strings.TamperedPin);
			}
			this.salt = new byte[8];
			salt.CopyTo(this.salt, 0);
			this.Initialize(pwd, algorithm, iterations);
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		internal byte[] Blob
		{
			get
			{
				return this.blob;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C700 File Offset: 0x0001A900
		internal string Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001C708 File Offset: 0x0001A908
		internal int Iterations
		{
			get
			{
				return this.iterations;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001C710 File Offset: 0x0001A910
		public static bool operator ==(PasswordBlob lhs, PasswordBlob rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001C719 File Offset: 0x0001A919
		public static bool operator !=(PasswordBlob lhs, PasswordBlob rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001C728 File Offset: 0x0001A928
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			PasswordBlob passwordBlob;
			if (o is PasswordBlob)
			{
				passwordBlob = (PasswordBlob)o;
			}
			else
			{
				if (!(o is EncryptedBuffer))
				{
					return false;
				}
				passwordBlob = new PasswordBlob((EncryptedBuffer)o, this.salt, this.algorithm, this.iterations);
			}
			if (this.blob.Length != passwordBlob.blob.Length)
			{
				return false;
			}
			for (int i = 0; i < this.blob.Length; i++)
			{
				if (passwordBlob.blob[i] != this.blob[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001C7B3 File Offset: 0x0001A9B3
		public override int GetHashCode()
		{
			if (this.hash != null)
			{
				return BitConverter.ToInt32(this.hash, 0);
			}
			return 0;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		private void Initialize(EncryptedBuffer pwd, string algorithm, int iterations)
		{
			this.algorithm = algorithm;
			this.iterations = iterations;
			byte[] bytes = Encoding.UTF8.GetBytes(algorithm);
			CallIdTracer.TraceDebug(ExTraceGlobals.AuthenticationTracer, this, "initializing password blob with algorithm={0} and iterations={1}.", new object[]
			{
				algorithm,
				iterations
			});
			using (SafeBuffer decrypted = pwd.Decrypted)
			{
				using (PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(decrypted.Buffer, this.salt, algorithm, iterations))
				{
					this.hash = passwordDeriveBytes.GetBytes(16);
				}
				int num = 0;
				this.blob = new byte[32 + bytes.Length];
				this.hash.CopyTo(this.blob, num);
				num += this.hash.Length;
				this.salt.CopyTo(this.blob, num);
				num += this.salt.Length;
				byte[] bytes2 = BitConverter.GetBytes(iterations);
				bytes2.CopyTo(this.blob, num);
				num += bytes2.Length;
				byte[] bytes3 = BitConverter.GetBytes(bytes.Length);
				bytes3.CopyTo(this.blob, num);
				num += bytes3.Length;
				bytes.CopyTo(this.blob, num);
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001C910 File Offset: 0x0001AB10
		private void Initialize(byte[] rawdata, int startIdx)
		{
			if (checked(startIdx + 32) > rawdata.Length)
			{
				CallIdTracer.TraceError(ExTraceGlobals.AuthenticationTracer, this, "failed to intialize password blob from rawdata because rawdata is the wrong size.", new object[0]);
				throw new UserConfigurationException(Strings.TamperedPin);
			}
			int num = 0;
			this.hash = new byte[16];
			Array.Copy(rawdata, startIdx + num, this.hash, 0, 16);
			num += 16;
			this.salt = new byte[8];
			Array.Copy(rawdata, startIdx + num, this.salt, 0, 8);
			num += 8;
			this.iterations = BitConverter.ToInt32(rawdata, startIdx + num);
			num += 4;
			int num2 = BitConverter.ToInt32(rawdata, startIdx + num);
			num += 4;
			if (checked(startIdx + 32 + num2) > rawdata.Length)
			{
				CallIdTracer.TraceError(ExTraceGlobals.AuthenticationTracer, this, "failed to intialize password blob from rawdata because rawdata is the wrong size.", new object[0]);
				throw new UserConfigurationException(Strings.TamperedPin);
			}
			this.algorithm = Encoding.UTF8.GetString(rawdata, startIdx + num, num2);
			num += num2;
			this.blob = new byte[num];
			Array.Copy(rawdata, startIdx, this.blob, 0, num);
		}

		// Token: 0x04000420 RID: 1056
		internal const int CbFixedBlobSize = 32;

		// Token: 0x04000421 RID: 1057
		private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

		// Token: 0x04000422 RID: 1058
		private byte[] salt;

		// Token: 0x04000423 RID: 1059
		private byte[] hash;

		// Token: 0x04000424 RID: 1060
		private byte[] blob;

		// Token: 0x04000425 RID: 1061
		private string algorithm;

		// Token: 0x04000426 RID: 1062
		private int iterations;
	}
}
