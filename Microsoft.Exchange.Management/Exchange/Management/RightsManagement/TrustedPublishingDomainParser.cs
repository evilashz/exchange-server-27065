using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x0200072F RID: 1839
	internal class TrustedPublishingDomainParser
	{
		// Token: 0x06004160 RID: 16736 RVA: 0x0010C418 File Offset: 0x0010A618
		public static TrustedDocDomain Parse(SecureString password, byte[] rawData)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentNullException("rawData");
			}
			EncryptedTrustedDocDomain encryptedTrustedDocDomain = null;
			using (MemoryStream memoryStream = new MemoryStream(rawData))
			{
				try
				{
					SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(ArrayList), new Type[]
					{
						typeof(EncryptedTrustedDocDomain[])
					});
					ArrayList arrayList = safeXmlSerializer.Deserialize(memoryStream) as ArrayList;
					if (arrayList == null || arrayList.Count != 1)
					{
						throw new TrustedPublishingDomainParser.ParseFailedException("EncryptedTPD_NoOrMoreThanOneElementsIn");
					}
					encryptedTrustedDocDomain = (arrayList[0] as EncryptedTrustedDocDomain);
					if (encryptedTrustedDocDomain == null)
					{
						throw new TrustedPublishingDomainParser.ParseFailedException("EncryptedTPD_NotOfType_EncryptedTrustedDocDomain");
					}
				}
				catch (InvalidOperationException innerException)
				{
					throw new TrustedPublishingDomainParser.ParseFailedException("EncryptedTPD_XmlDeserializationFailed", innerException);
				}
				catch (XmlException innerException2)
				{
					throw new TrustedPublishingDomainParser.ParseFailedException("EncryptedTPD_XmlDeserializationFailed", innerException2);
				}
			}
			if (string.IsNullOrEmpty(encryptedTrustedDocDomain.m_strTrustedDocDomainInfo))
			{
				throw new TrustedPublishingDomainParser.ParseFailedException("EncryptedTPD_NoTrustedDocDomainInfo");
			}
			return TrustedPublishingDomainParser.DecryptTrustedDocDomainData(encryptedTrustedDocDomain, password);
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x0010C530 File Offset: 0x0010A730
		private static TrustedDocDomain DecryptTrustedDocDomainData(EncryptedTrustedDocDomain encryptedData, SecureString password)
		{
			IPrivateKeyDecryptor privateKeyDecryptor = new OnPremisePrivateKeyDecryptor(password);
			byte[] buffer = null;
			try
			{
				buffer = privateKeyDecryptor.Decrypt(encryptedData.m_strTrustedDocDomainInfo);
			}
			catch (PrivateKeyDecryptionFailedException innerException)
			{
				throw new TrustedPublishingDomainParser.ParseFailedException("OnPremisePrivateKeyDecryptor.Decrypt() failed", innerException);
			}
			TrustedDocDomain result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				try
				{
					SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(ArrayList), new Type[]
					{
						typeof(TrustedDocDomain[])
					});
					ArrayList arrayList = safeXmlSerializer.Deserialize(memoryStream) as ArrayList;
					if (arrayList == null || arrayList.Count != 1)
					{
						throw new TrustedPublishingDomainParser.ParseFailedException("DecryptedTPD_NoORMoreThanOneElements");
					}
					TrustedDocDomain trustedDocDomain = arrayList[0] as TrustedDocDomain;
					if (trustedDocDomain == null)
					{
						throw new TrustedPublishingDomainParser.ParseFailedException("DecryptedTPD_NotOfType_TrustedDocDomain");
					}
					trustedDocDomain.m_ttdki.strEncryptedPrivateKey = encryptedData.m_strKeyData;
					result = trustedDocDomain;
				}
				catch (InvalidOperationException innerException2)
				{
					throw new TrustedPublishingDomainParser.ParseFailedException("DecryptedTPD_XmlDeserializationFailed", innerException2);
				}
				catch (XmlException innerException3)
				{
					throw new TrustedPublishingDomainParser.ParseFailedException("DecryptedTPD_XmlDeserializationFailed", innerException3);
				}
			}
			return result;
		}

		// Token: 0x02000730 RID: 1840
		[Serializable]
		public class ParseFailedException : Exception
		{
			// Token: 0x06004163 RID: 16739 RVA: 0x0010C658 File Offset: 0x0010A858
			public ParseFailedException(string message) : base(message)
			{
			}

			// Token: 0x06004164 RID: 16740 RVA: 0x0010C661 File Offset: 0x0010A861
			public ParseFailedException(string message, Exception innerException) : base(message, innerException)
			{
			}

			// Token: 0x06004165 RID: 16741 RVA: 0x0010C66B File Offset: 0x0010A86B
			protected ParseFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}
	}
}
