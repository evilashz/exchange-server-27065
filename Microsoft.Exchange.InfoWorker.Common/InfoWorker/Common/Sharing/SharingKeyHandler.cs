using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Sharing;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000274 RID: 628
	internal static class SharingKeyHandler
	{
		// Token: 0x0600120B RID: 4619 RVA: 0x00054484 File Offset: 0x00052684
		public static SmtpAddress Decrypt(XmlElement encryptedSharingKey, SymmetricSecurityKey symmetricSecurityKey)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			try
			{
				xmlDocument.AppendChild(xmlDocument.ImportNode(encryptedSharingKey, true));
			}
			catch (XmlException)
			{
				SharingKeyHandler.Tracer.TraceError<string>(0L, "Unable to import XML element of sharing key: {0}", encryptedSharingKey.OuterXml);
				return SmtpAddress.Empty;
			}
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping("key", symmetricSecurityKey.GetSymmetricAlgorithm("http://www.w3.org/2001/04/xmlenc#tripledes-cbc"));
			try
			{
				encryptedXml.DecryptDocument();
			}
			catch (CryptographicException)
			{
				SharingKeyHandler.Tracer.TraceError<string>(0L, "Unable to decrypt XML element sharing key: {0}", encryptedSharingKey.OuterXml);
				return SmtpAddress.Empty;
			}
			return new SmtpAddress(xmlDocument.DocumentElement.InnerText);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0005453C File Offset: 0x0005273C
		public static XmlElement Encrypt(SmtpAddress externalId, SymmetricSecurityKey symmetricSecurityKey)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("SharingKey");
			xmlElement.InnerText = externalId.ToString();
			EncryptedXml encryptedXml = new EncryptedXml();
			encryptedXml.AddKeyNameMapping("key", symmetricSecurityKey.GetSymmetricAlgorithm("http://www.w3.org/2001/04/xmlenc#tripledes-cbc"));
			EncryptedData encryptedData = encryptedXml.Encrypt(xmlElement, "key");
			return encryptedData.GetXml();
		}

		// Token: 0x04000BD6 RID: 3030
		private const string KeyName = "key";

		// Token: 0x04000BD7 RID: 3031
		private static readonly Trace Tracer = ExTraceGlobals.SharingKeyHandlerTracer;
	}
}
