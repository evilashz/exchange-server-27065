using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Dkm.Proxy;

namespace Microsoft.Exchange.Security.Dkm
{
	// Token: 0x02000006 RID: 6
	internal sealed class ExchangeGroupKey : IExchangeGroupKey
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002654 File Offset: 0x00000854
		public ExchangeGroupKey(string dkmPath = null, string containerName = "Microsoft Exchange DKM")
		{
			this.groupKeyObject = null;
			this.dkmPath = dkmPath;
			this.containerName = containerName;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002679 File Offset: 0x00000879
		public string ParentContainerDN
		{
			get
			{
				return this.parentContainerDN;
			}
			set
			{
				this.parentContainerDN = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002684 File Offset: 0x00000884
		private DkmProxy GroupKey
		{
			get
			{
				DkmProxy result;
				lock (ExchangeGroupKey.lockObject)
				{
					if (this.groupKeyObject == null)
					{
						this.groupKeyObject = new DkmProxy(this.containerName, this.dkmPath, this.parentContainerDN);
					}
					result = this.groupKeyObject;
				}
				return result;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026EC File Offset: 0x000008EC
		public string ClearStringToEncryptedString(string clearString)
		{
			if (string.IsNullOrEmpty(clearString))
			{
				return null;
			}
			string result;
			using (MemoryStream memoryStream = this.ClearStringToMemoryStream(clearString))
			{
				using (MemoryStream memoryStream2 = this.GroupKey.Protect(memoryStream))
				{
					string text = this.MemoryStreamToBase64String(memoryStream2);
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002758 File Offset: 0x00000958
		public string ByteArrayToEncryptedString(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
			{
				return null;
			}
			string result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream2 = this.GroupKey.Protect(memoryStream))
				{
					string text = this.MemoryStreamToBase64String(memoryStream2);
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027C0 File Offset: 0x000009C0
		public string SecureStringToEncryptedString(SecureString secureString)
		{
			if (secureString == null || secureString.Length == 0)
			{
				return null;
			}
			string result;
			using (MemoryStream memoryStream = this.SecureStringToMemoryStream(secureString))
			{
				using (MemoryStream memoryStream2 = this.GroupKey.Protect(memoryStream))
				{
					string text = this.MemoryStreamToBase64String(memoryStream2);
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000282C File Offset: 0x00000A2C
		public SecureString EncryptedStringToSecureString(string encryptedString)
		{
			if (string.IsNullOrEmpty(encryptedString))
			{
				return null;
			}
			SecureString result;
			using (MemoryStream memoryStream = this.Base64StringToMemoryStream(encryptedString))
			{
				using (MemoryStream memoryStream2 = this.GroupKey.Unprotect(memoryStream))
				{
					SecureString secureString = this.MemoryStreamToSecureString(memoryStream2);
					result = secureString;
				}
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002898 File Offset: 0x00000A98
		public bool TrySecureStringToEncryptedString(SecureString secureString, out string encryptedString, out Exception exception)
		{
			exception = null;
			encryptedString = null;
			try
			{
				encryptedString = this.SecureStringToEncryptedString(secureString);
				return true;
			}
			catch (CryptographicException ex)
			{
				exception = ex;
			}
			catch (InvalidDataException ex2)
			{
				exception = ex2;
			}
			catch (Exception ex3)
			{
				if (!this.GroupKey.IsDkmException(ex3))
				{
					throw;
				}
				exception = ex3;
			}
			return false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002908 File Offset: 0x00000B08
		public bool TryEncryptedStringToBuffer(string encryptedString, out byte[] buffer, out Exception exception)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("encryptedString", encryptedString);
			exception = null;
			buffer = null;
			try
			{
				using (MemoryStream memoryStream = this.Base64StringToMemoryStream(encryptedString))
				{
					using (MemoryStream memoryStream2 = this.GroupKey.Unprotect(memoryStream))
					{
						buffer = new byte[memoryStream2.Length];
						memoryStream2.Position = 0L;
						memoryStream2.Read(buffer, 0, buffer.Length);
						return true;
					}
				}
			}
			catch (CryptographicException ex)
			{
				exception = ex;
			}
			catch (InvalidDataException ex2)
			{
				exception = ex2;
			}
			catch (FormatException ex3)
			{
				exception = ex3;
			}
			catch (Exception ex4)
			{
				if (!this.GroupKey.IsDkmException(ex4))
				{
					throw;
				}
				exception = ex4;
			}
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029F8 File Offset: 0x00000BF8
		public bool TryEncryptedStringToSecureString(string encryptedString, out SecureString secureString, out Exception exception)
		{
			exception = null;
			secureString = null;
			try
			{
				secureString = this.EncryptedStringToSecureString(encryptedString);
				return true;
			}
			catch (CryptographicException ex)
			{
				exception = ex;
			}
			catch (InvalidDataException ex2)
			{
				exception = ex2;
			}
			catch (FormatException ex3)
			{
				exception = ex3;
			}
			catch (Exception ex4)
			{
				if (!this.GroupKey.IsDkmException(ex4))
				{
					throw;
				}
				exception = ex4;
			}
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A7C File Offset: 0x00000C7C
		public bool TryByteArrayToEncryptedString(byte[] bytes, out string encryptedString, out Exception exception)
		{
			exception = null;
			encryptedString = null;
			try
			{
				encryptedString = this.ByteArrayToEncryptedString(bytes);
				return true;
			}
			catch (CryptographicException ex)
			{
				exception = ex;
			}
			catch (InvalidDataException ex2)
			{
				exception = ex2;
			}
			catch (Exception ex3)
			{
				if (!this.GroupKey.IsDkmException(ex3))
				{
					throw;
				}
				exception = ex3;
			}
			return false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AEC File Offset: 0x00000CEC
		public bool IsDkmException(Exception e)
		{
			return this.GroupKey.IsDkmException(e);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002AFC File Offset: 0x00000CFC
		private MemoryStream Base64StringToMemoryStream(string clearString)
		{
			if (string.IsNullOrEmpty(clearString))
			{
				return null;
			}
			byte[] buffer = Convert.FromBase64String(clearString);
			return new MemoryStream(buffer);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B24 File Offset: 0x00000D24
		private MemoryStream ClearStringToMemoryStream(string clearString)
		{
			if (string.IsNullOrEmpty(clearString))
			{
				return null;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(clearString);
			return new MemoryStream(bytes);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B50 File Offset: 0x00000D50
		private string MemoryStreamToBase64String(MemoryStream memoryStream)
		{
			if (memoryStream == null)
			{
				return null;
			}
			if (memoryStream.Length < 1L)
			{
				return null;
			}
			if (memoryStream.Position != 0L)
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
			}
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, (int)memoryStream.Length);
			string result = Convert.ToBase64String(array);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private MemoryStream SecureStringToMemoryStream(SecureString secureString)
		{
			if (secureString == null)
			{
				return null;
			}
			IntPtr intPtr = IntPtr.Zero;
			MemoryStream result = null;
			try
			{
				intPtr = Marshal.SecureStringToBSTR(secureString);
				int num = Marshal.ReadInt32(intPtr, -4);
				byte[] array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
				result = new MemoryStream(array);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C1C File Offset: 0x00000E1C
		private SecureString MemoryStreamToSecureString(MemoryStream memoryStream)
		{
			if (memoryStream == null)
			{
				return null;
			}
			if (memoryStream.Position != 0L)
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
			}
			SecureString secureString = new SecureString();
			byte[] buffer = memoryStream.GetBuffer();
			int num = 0;
			while ((long)num < memoryStream.Length)
			{
				byte b = buffer[num];
				byte b2 = buffer[num + 1];
				char c = (char)((int)b | (int)b2 << 8);
				secureString.AppendChar(c);
				buffer[num] = 0;
				buffer[num + 1] = 0;
				num += 2;
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			secureString.MakeReadOnly();
			return secureString;
		}

		// Token: 0x04000012 RID: 18
		private static readonly object lockObject = new object();

		// Token: 0x04000013 RID: 19
		private readonly string dkmPath;

		// Token: 0x04000014 RID: 20
		private readonly string containerName;

		// Token: 0x04000015 RID: 21
		private string parentContainerDN;

		// Token: 0x04000016 RID: 22
		private DkmProxy groupKeyObject;
	}
}
