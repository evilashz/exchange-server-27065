using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Exchange.Common.Net.Cryptography
{
	// Token: 0x020006B6 RID: 1718
	public static class CryptoUtils
	{
		// Token: 0x06001FFF RID: 8191 RVA: 0x0003DB3C File Offset: 0x0003BD3C
		public static byte[] Encrypt(byte[] toEncrypt, CryptoKeyPayloadType keyType)
		{
			byte[] array = null;
			byte b;
			return CryptoUtils.Encrypt(toEncrypt, keyType, out array, out b, false);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0003DB57 File Offset: 0x0003BD57
		public static byte[] Encrypt(byte[] toEncrypt, CryptoKeyPayloadType keyType, out byte keyVersion, out byte[] initializationVector)
		{
			return CryptoUtils.Encrypt(toEncrypt, keyType, out initializationVector, out keyVersion, true);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0003DB64 File Offset: 0x0003BD64
		public static byte[] Decrypt(byte[] encryptedData, CryptoKeyPayloadType keyType, byte keyVersion, byte[] initializationVector = null)
		{
			SymmetricEncryptionKey symmetricEncryptionKey = CryptoKeyStore.GetKeyByVersion(keyType, keyVersion) as SymmetricEncryptionKey;
			if (symmetricEncryptionKey == null)
			{
				throw new CryptographicException(keyType + " could not be found in CryptoKeyStore as SymmetricEncryptionKey");
			}
			byte[] result;
			using (SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(symmetricEncryptionKey.Algorithm.Name))
			{
				symmetricAlgorithm.Key = symmetricEncryptionKey.Key;
				if (initializationVector == null)
				{
					symmetricAlgorithm.IV = Encoding.UTF8.GetBytes(symmetricEncryptionKey.InitializationVector);
				}
				else
				{
					symmetricAlgorithm.IV = initializationVector;
				}
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor())
				{
					using (MemoryStream memoryStream = new MemoryStream(encryptedData))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
						{
							using (BinaryReader binaryReader = new BinaryReader(cryptoStream))
							{
								using (MemoryStream memoryStream2 = new MemoryStream())
								{
									byte[] array = new byte[2 * encryptedData.Length];
									int num;
									do
									{
										num = binaryReader.Read(array, 0, array.Length);
										memoryStream2.Write(array, 0, num);
									}
									while (num != 0);
									result = memoryStream2.ToArray();
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		private static byte[] Encrypt(byte[] toEncrypt, CryptoKeyPayloadType keyType, out byte[] initializationVector, out byte keyVersion, bool generateIV = false)
		{
			SymmetricEncryptionKey symmetricEncryptionKey = CryptoKeyStore.GetKeyByPayload(keyType) as SymmetricEncryptionKey;
			if (symmetricEncryptionKey == null)
			{
				throw new CryptographicException(keyType + " could not be found in CryptoKeyStore as SymmetricEncryptionKey");
			}
			byte[] array = null;
			keyVersion = symmetricEncryptionKey.Version;
			using (SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(symmetricEncryptionKey.Algorithm.Name))
			{
				symmetricAlgorithm.Key = symmetricEncryptionKey.Key;
				if (generateIV)
				{
					symmetricAlgorithm.GenerateIV();
				}
				else
				{
					symmetricAlgorithm.IV = Encoding.UTF8.GetBytes(symmetricEncryptionKey.InitializationVector);
				}
				initializationVector = symmetricAlgorithm.IV;
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor())
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
							cryptoStream.FlushFinalBlock();
							memoryStream.Seek(0L, SeekOrigin.Begin);
							array = new byte[memoryStream.Length];
							memoryStream.Read(array, 0, (int)memoryStream.Length);
						}
					}
				}
			}
			return array;
		}
	}
}
