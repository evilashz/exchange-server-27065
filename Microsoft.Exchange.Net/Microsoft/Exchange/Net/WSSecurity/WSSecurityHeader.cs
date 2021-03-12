using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Net.WSSecurity
{
	// Token: 0x02000B4F RID: 2895
	[XmlRoot(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", ElementName = "Security", IsNullable = false)]
	public class WSSecurityHeader : SoapHeader
	{
		// Token: 0x06003E54 RID: 15956 RVA: 0x000A2A90 File Offset: 0x000A0C90
		internal static WSSecurityHeader Create(XmlElement securityHeader)
		{
			return new WSSecurityHeader
			{
				MustUnderstand = true,
				EncryptedToken = securityHeader["EncryptedData", "http://www.w3.org/2001/04/xmlenc#"]
			};
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000A2AC4 File Offset: 0x000A0CC4
		internal static WSSecurityHeader Create(RequestedToken token)
		{
			return new WSSecurityHeader
			{
				MustUnderstand = true,
				EncryptedToken = token.SecurityToken
			};
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000A2AEC File Offset: 0x000A0CEC
		internal static WSSecurityHeader Create(X509Certificate2 certificate)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.PreserveWhitespace = true;
			DateTime utcNow = DateTime.UtcNow;
			Timestamp timestamp = new Timestamp("_0", utcNow, utcNow + WSSecurityHeader.TimestampDuration);
			XmlElement xml = timestamp.GetXml(xmlDocument);
			XmlElement signature = WSSecurityHeader.CreateSignature(xmlDocument, new XmlElement[]
			{
				xml
			}, certificate);
			return new WSSecurityHeader
			{
				MustUnderstand = true,
				Timestamp = xml,
				Signature = signature
			};
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000A2B68 File Offset: 0x000A0D68
		private static XmlElement CreateSignature(XmlDocument xmlDocument, XmlElement[] elementsToSign, X509Certificate2 certificate)
		{
			SecuritySignedXml securitySignedXml = new SecuritySignedXml(xmlDocument, elementsToSign);
			XmlDsigExcC14NTransform transform = new XmlDsigExcC14NTransform();
			foreach (XmlElement xmlElement in elementsToSign)
			{
				string attributeValue = WSSecurityUtility.Id.GetAttributeValue(xmlElement);
				Reference reference = new Reference("#" + attributeValue);
				reference.AddTransform(transform);
				securitySignedXml.Signature.SignedInfo.AddReference(reference);
			}
			securitySignedXml.SigningKey = certificate.PrivateKey;
			securitySignedXml.Signature.KeyInfo = new KeyInfo();
			securitySignedXml.Signature.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
			securitySignedXml.ComputeSignature();
			return securitySignedXml.GetXml();
		}

		// Token: 0x04003633 RID: 13875
		private const string EncryptedDataElementName = "EncryptedData";

		// Token: 0x04003634 RID: 13876
		private const string SignatureElementName = "Signature";

		// Token: 0x04003635 RID: 13877
		private const string TimestampElementName = "Timestamp";

		// Token: 0x04003636 RID: 13878
		private static readonly TimeSpan TimestampDuration = TimeSpan.FromMinutes(5.0);

		// Token: 0x04003637 RID: 13879
		[XmlAnyElement(Name = "Timestamp", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")]
		public XmlElement Timestamp;

		// Token: 0x04003638 RID: 13880
		[XmlAnyElement(Name = "EncryptedData", Namespace = "http://www.w3.org/2001/04/xmlenc#")]
		public XmlElement EncryptedToken;

		// Token: 0x04003639 RID: 13881
		[XmlAnyElement(Name = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public XmlElement Signature;
	}
}
