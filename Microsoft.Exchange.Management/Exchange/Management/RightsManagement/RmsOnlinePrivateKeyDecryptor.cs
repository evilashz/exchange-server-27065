using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000743 RID: 1859
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RmsOnlinePrivateKeyDecryptor : IPrivateKeyDecryptor
	{
		// Token: 0x060041D7 RID: 16855 RVA: 0x0010CB20 File Offset: 0x0010AD20
		public byte[] Decrypt(string encryptedData)
		{
			RmsUtil.ThrowIfParameterNull(encryptedData, "encryptedData");
			RmsUtil.ThrowIfStringParameterNullOrEmpty(encryptedData, "encryptedData");
			byte[] result;
			try
			{
				result = this.DecryptTenantsPrivateKey(encryptedData);
			}
			catch (CryptographicException ex)
			{
				string ski;
				Exception ex2;
				if (RmsUtil.TryExtractDecryptionCertificateSKIFromEncryptedXml(encryptedData, out ski, out ex2))
				{
					throw new PrivateKeyDecryptionFailedException(ex.Message + " " + Strings.RequiredDecryptionCertificate(ski), ex);
				}
				throw new PrivateKeyDecryptionFailedException(ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x0010CB9C File Offset: 0x0010AD9C
		protected virtual XmlDocument DecryptKeyBlobXml(string encryptedXmlString)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(encryptedXmlString);
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.DecryptDocument();
			this.ThrowIfKeyBlobXmlFailsSchemaValidation(xmlDocument);
			return xmlDocument;
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x0010CBCC File Offset: 0x0010ADCC
		private byte[] DecryptTenantsPrivateKey(string encryptedData)
		{
			XmlDocument xmlDocument = this.DecryptKeyBlobXml(encryptedData);
			XmlElement xmlElement = xmlDocument.GetElementsByTagName("KeyBlob")[0] as XmlElement;
			byte[] result;
			try
			{
				result = Convert.FromBase64String(xmlElement.InnerText);
			}
			catch (FormatException innerException)
			{
				throw new PrivateKeyDecryptionFailedException("Decrypted private key XML KeyBlob element contains bad Base64 characters", innerException);
			}
			return result;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x0010CC38 File Offset: 0x0010AE38
		private void ThrowIfKeyBlobXmlFailsSchemaValidation(XmlDocument xmlDocument)
		{
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("KeyBlobSchema.xsd"))
			{
				if (manifestResourceStream == null)
				{
					throw new PrivateKeyDecryptionFailedException("Unable to load XML schema KeyBlobSchema.xsd", null);
				}
				xmlDocument.Schemas.Add(null, XmlReader.Create(manifestResourceStream));
				xmlDocument.Validate(delegate(object _, ValidationEventArgs eventArgs)
				{
					throw new PrivateKeyDecryptionFailedException("Decrypted private key XML failed schema validation", eventArgs.Exception);
				});
			}
		}
	}
}
