using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.Xml;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE5 RID: 3557
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SymmetricEncryptedXml
	{
		// Token: 0x06007A5F RID: 31327 RVA: 0x0021D1B0 File Offset: 0x0021B3B0
		public static XmlElement Encrypt(XmlElement xmlElement, SymmetricSecurityKey symmetricSecurityKey)
		{
			EncryptedXml encryptedXml = new EncryptedXml();
			encryptedXml.AddKeyNameMapping("key", symmetricSecurityKey.GetSymmetricAlgorithm("http://www.w3.org/2001/04/xmlenc#tripledes-cbc"));
			EncryptedData encryptedData = encryptedXml.Encrypt(xmlElement, "key");
			return encryptedData.GetXml();
		}

		// Token: 0x06007A60 RID: 31328 RVA: 0x0021D1EC File Offset: 0x0021B3EC
		public static XmlElement Decrypt(XmlElement xmlElement, SymmetricSecurityKey symmetricSecurityKey)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode newChild = xmlDocument.ImportNode(xmlElement, true);
			xmlDocument.AppendChild(newChild);
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping("key", symmetricSecurityKey.GetSymmetricAlgorithm("http://www.w3.org/2001/04/xmlenc#tripledes-cbc"));
			encryptedXml.DecryptDocument();
			return xmlDocument.DocumentElement;
		}

		// Token: 0x04005446 RID: 21574
		private const string KeyName = "key";
	}
}
