using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B71 RID: 2929
	internal sealed class TokenDecryption
	{
		// Token: 0x06003ECF RID: 16079 RVA: 0x000A4CF2 File Offset: 0x000A2EF2
		public TokenDecryption(IEnumerable<X509Certificate2> trustedTokenIssuerCertificates, IEnumerable<X509Certificate2> tokenDecryptionCertificates)
		{
			this.trustedTokenIssuerCertificates = trustedTokenIssuerCertificates;
			this.tokenDecryptionCertificates = tokenDecryptionCertificates;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000A4D08 File Offset: 0x000A2F08
		public SamlSecurityToken DecryptToken(XmlElement encryptedToken)
		{
			byte[] array = this.DecryptTokenInternal(encryptedToken);
			if (array == null)
			{
				TokenDecryption.Tracer.TraceError<string>((long)this.GetHashCode(), "Unable to decrypt encrypted XML token: {0}", encryptedToken.OuterXml);
				throw new TokenDecryptionException();
			}
			TokenDecryption.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Decrypted token content : {0}", Encoding.ASCII.GetString(array));
			return this.GetSecurityToken(array);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000A4D6C File Offset: 0x000A2F6C
		private SamlSecurityToken GetSecurityToken(byte[] decryptedTokenBytes)
		{
			List<SecurityToken> list = new List<SecurityToken>();
			TokenDecryption.AddToSecurityTokenList(list, this.tokenDecryptionCertificates);
			TokenDecryption.AddToSecurityTokenList(list, this.trustedTokenIssuerCertificates);
			SecurityTokenResolver tokenResolver = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(list.AsReadOnly(), true);
			SamlSecurityToken result;
			using (MemoryStream memoryStream = new MemoryStream(decryptedTokenBytes))
			{
				using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(memoryStream))
				{
					xmlTextReader.MoveToContent();
					try
					{
						result = (SamlSecurityToken)WSSecurityTokenSerializer.DefaultInstance.ReadToken(xmlTextReader, tokenResolver);
					}
					catch (SecurityTokenException arg)
					{
						TokenDecryption.Tracer.TraceError<SecurityTokenException>((long)this.GetHashCode(), "Unable to read token due exception {0}", arg);
						throw new TokenDecryptionException();
					}
				}
			}
			return result;
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000A4E2C File Offset: 0x000A302C
		private static void AddToSecurityTokenList(List<SecurityToken> securityTokenList, IEnumerable<X509Certificate2> certificates)
		{
			foreach (X509Certificate2 certificate in certificates)
			{
				securityTokenList.Add(new X509SecurityToken(certificate));
			}
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000A4E7C File Offset: 0x000A307C
		private byte[] DecryptTokenInternal(XmlElement encryptedToken)
		{
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(encryptedToken);
			if (subjectKeyIdentifier == null)
			{
				TokenDecryption.Tracer.TraceError<string>((long)this.GetHashCode(), "Unable to find key identifier in encrypted XML token: {0}", encryptedToken.OuterXml);
				throw new TokenDecryptionException();
			}
			X509Certificate2 x509Certificate = this.FindBySubjectKeyIdentifier(subjectKeyIdentifier);
			if (x509Certificate == null)
			{
				TokenDecryption.Tracer.TraceError<string>((long)this.GetHashCode(), "Unable to find certificate based on Subject Key Identifier: {0}", subjectKeyIdentifier);
				throw new TokenDecryptionException();
			}
			EncryptedKey encryptedKey = null;
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.LoadXml(encryptedToken);
			foreach (object obj in encryptedData.KeyInfo)
			{
				KeyInfoClause keyInfoClause = (KeyInfoClause)obj;
				KeyInfoEncryptedKey keyInfoEncryptedKey = keyInfoClause as KeyInfoEncryptedKey;
				if (keyInfoEncryptedKey != null)
				{
					keyInfoEncryptedKey.EncryptedKey.KeyInfo.AddClause(new KeyInfoName(subjectKeyIdentifier));
					encryptedKey = keyInfoEncryptedKey.EncryptedKey;
					break;
				}
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlNode newChild = xmlDocument.ImportNode(encryptedData.GetXml(), true);
			xmlDocument.AppendChild(newChild);
			EncryptedXml encryptedXml = new EncryptedXml(xmlDocument);
			encryptedXml.AddKeyNameMapping(subjectKeyIdentifier, x509Certificate.PrivateKey);
			encryptedXml.DecryptEncryptedKey(encryptedKey);
			encryptedXml.GetDecryptionIV(encryptedData, encryptedData.EncryptionMethod.KeyAlgorithm);
			SymmetricAlgorithm decryptionKey = encryptedXml.GetDecryptionKey(encryptedData, encryptedData.EncryptionMethod.KeyAlgorithm);
			return encryptedXml.DecryptData(encryptedData, decryptionKey);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000A4FDC File Offset: 0x000A31DC
		private XmlElement GetRequiredChildElement(XmlElementDefinition xmlElementDefinition, XmlElement xmlElement)
		{
			XmlElement singleElementByName = xmlElementDefinition.GetSingleElementByName(xmlElement);
			if (singleElementByName == null)
			{
				throw new TokenDecryptionException();
			}
			return singleElementByName;
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000A4FFC File Offset: 0x000A31FC
		private string GetSubjectKeyIdentifier(XmlElement encryptedToken)
		{
			XmlElement requiredChildElement = this.GetRequiredChildElement(XmlDigitalSignature.KeyInfo, encryptedToken);
			XmlElement requiredChildElement2 = this.GetRequiredChildElement(XmlEncryption.EncryptedKey, requiredChildElement);
			XmlElement requiredChildElement3 = this.GetRequiredChildElement(XmlDigitalSignature.KeyInfo, requiredChildElement2);
			XmlElement requiredChildElement4 = this.GetRequiredChildElement(WSSecurityExtensions.SecurityTokenReference, requiredChildElement3);
			XmlElement requiredChildElement5 = this.GetRequiredChildElement(WSSecurityExtensions.KeyIdentifier, requiredChildElement4);
			return TokenDecryption.GetHex(Convert.FromBase64String(requiredChildElement5.InnerText));
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000A505C File Offset: 0x000A325C
		private static string GetHex(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
			for (int i = 0; i < bytes.Length; i++)
			{
				stringBuilder.Append(bytes[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000A50A0 File Offset: 0x000A32A0
		private X509Certificate2 FindBySubjectKeyIdentifier(string subjectKeyIdentifier)
		{
			foreach (X509Certificate2 x509Certificate in this.tokenDecryptionCertificates)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(subjectKeyIdentifier, TokenDecryption.GetSubjectKeyIdentifier(x509Certificate)))
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000A5100 File Offset: 0x000A3300
		private static string GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			foreach (X509Extension x509Extension in certificate.Extensions)
			{
				X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = x509Extension as X509SubjectKeyIdentifierExtension;
				if (x509SubjectKeyIdentifierExtension != null)
				{
					return x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier;
				}
			}
			return null;
		}

		// Token: 0x04003698 RID: 13976
		private IEnumerable<X509Certificate2> trustedTokenIssuerCertificates;

		// Token: 0x04003699 RID: 13977
		private IEnumerable<X509Certificate2> tokenDecryptionCertificates;

		// Token: 0x0400369A RID: 13978
		private static readonly Trace Tracer = ExTraceGlobals.WSTrustTracer;
	}
}
