using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000847 RID: 2119
	internal class RulePackageDecrypter
	{
		// Token: 0x0600498F RID: 18831 RVA: 0x0012E4F9 File Offset: 0x0012C6F9
		public static bool DecryptRulePackage(byte[] inputRulePackageData, out byte[] outputDecryptedRulePackageData, out byte[] outputEncryptedRulePackageDataWithSymmetricKey)
		{
			return RulePackageDecrypter.DecryptRulePackage(new RulePackageDecrypter.InternalRulePackageDecrypter(inputRulePackageData), out outputDecryptedRulePackageData, out outputEncryptedRulePackageDataWithSymmetricKey);
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x0012E508 File Offset: 0x0012C708
		internal static bool DecryptRulePackage(RulePackageDecrypter.InternalRulePackageDecrypter internalRulePackageDecrypter, out byte[] outputDecryptedRulePackageData, out byte[] outputEncryptedRulePackageDataWithSymmetricKey)
		{
			return internalRulePackageDecrypter.DecryptRulePackage(out outputDecryptedRulePackageData, out outputEncryptedRulePackageDataWithSymmetricKey);
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x0012E514 File Offset: 0x0012C714
		internal static bool IsRulePackageEncrypted(XDocument rulePackXDoc)
		{
			if (rulePackXDoc == null)
			{
				throw new ArgumentNullException("rulePackXDoc");
			}
			bool result;
			try
			{
				XElement xelement = rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Encryption")).SingleOrDefault<XElement>();
				result = (xelement != null);
			}
			catch (InvalidOperationException ex)
			{
				throw new XmlException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x02000848 RID: 2120
		internal class InternalRulePackageDecrypter
		{
			// Token: 0x06004993 RID: 18835 RVA: 0x0012E578 File Offset: 0x0012C778
			public InternalRulePackageDecrypter(byte[] inputRulePackageData)
			{
				this.inputRulePackageData = inputRulePackageData;
				XmlReader reader = XmlReader.Create(new MemoryStream(this.inputRulePackageData), ClassificationDefinitionUtils.CreateSafeXmlReaderSettings());
				this.document.Load(reader);
				this.nsmgr = new XmlNamespaceManager(this.document.NameTable);
				this.nsmgr.AddNamespace("ns", this.document.DocumentElement.NamespaceURI);
			}

			// Token: 0x06004994 RID: 18836 RVA: 0x0012E5F8 File Offset: 0x0012C7F8
			public bool DecryptRulePackage(out byte[] outputDecryptedRulePackageData, out byte[] outputEncryptedRulePackageDataWithSymmetricKey)
			{
				XmlNode encryptionNode = this.GetEncryptionNode();
				if (encryptionNode == null)
				{
					outputDecryptedRulePackageData = null;
					outputEncryptedRulePackageDataWithSymmetricKey = null;
					return false;
				}
				byte[] array = this.AsymmetricDecrypt(this.GetEncryptionParameter(encryptionNode, "ns:Key"));
				byte[] array2 = this.AsymmetricDecrypt(this.GetEncryptionParameter(encryptionNode, "ns:IV"));
				using (this.algorithm = new AesCryptoServiceProvider())
				{
					this.algorithm.Key = array;
					this.algorithm.IV = array2;
					this.DecryptEntities();
					this.DecryptAffinities();
					this.DecryptRegexes();
					this.DecryptKeywordLists();
				}
				this.RemoveEncryptionNode();
				outputDecryptedRulePackageData = XmlProcessingUtils.XmlDocumentToUtf16EncodedBytes(this.document);
				outputEncryptedRulePackageDataWithSymmetricKey = this.ReplaceSymmetricAlgorithmParameters(array, array2);
				return true;
			}

			// Token: 0x06004995 RID: 18837 RVA: 0x0012E6BC File Offset: 0x0012C8BC
			private static XmlNode SelectAndEnsureSingleNode(XmlNode node, XmlNamespaceManager nsmgr, string strXPath)
			{
				XmlNode result;
				using (XmlNodeList xmlNodeList = node.SelectNodes(strXPath, nsmgr))
				{
					if (xmlNodeList.Count > 1)
					{
						throw new XmlException(string.Format("Too many '{0}' found in XML", strXPath));
					}
					result = ((1 == xmlNodeList.Count) ? xmlNodeList.Item(0) : null);
				}
				return result;
			}

			// Token: 0x06004996 RID: 18838 RVA: 0x0012E8F4 File Offset: 0x0012CAF4
			protected virtual IEnumerable<RSACryptoServiceProvider> GetCryptoServiceProviders()
			{
				string certSubjectName = this.GetCertificateSubjectName();
				List<X509Certificate2> candidateCerts = this.GetCertificates(certSubjectName);
				foreach (X509Certificate2 certificate in candidateCerts)
				{
					RSACryptoServiceProvider asymmetricAlgorithm = certificate.PrivateKey as RSACryptoServiceProvider;
					if (asymmetricAlgorithm == null)
					{
						throw new InvalidOperationException("Failed to obtain private key from certificate");
					}
					yield return asymmetricAlgorithm;
				}
				yield break;
			}

			// Token: 0x06004997 RID: 18839 RVA: 0x0012E914 File Offset: 0x0012CB14
			private string SymmetricDecrypt(byte[] cipherBytes)
			{
				byte[] bytes = null;
				using (ICryptoTransform cryptoTransform = this.algorithm.CreateDecryptor())
				{
					MemoryStream memoryStream = new MemoryStream();
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
						cryptoStream.FlushFinalBlock();
					}
					bytes = memoryStream.ToArray();
				}
				Encoding encoding = new UnicodeEncoding();
				return encoding.GetString(bytes);
			}

			// Token: 0x06004998 RID: 18840 RVA: 0x0012E99C File Offset: 0x0012CB9C
			private void DecryptAndReplaceNodeTextFragment(XmlNode node)
			{
				if (node.ChildNodes.Count != 1)
				{
					throw new XmlException(string.Format("Expected one child of '{0}', but found {1:d} instead.", node.Name, node.ChildNodes.Count));
				}
				string value = this.SymmetricDecrypt(Convert.FromBase64String(node.InnerText));
				node.RemoveChild(node.FirstChild);
				StringBuilder stringBuilder = new StringBuilder("<root ");
				stringBuilder.Append("xmlns=\"http://schemas.microsoft.com/office/2011/mce\">");
				stringBuilder.Append(value);
				stringBuilder.Append("</root>");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(stringBuilder.ToString());
				XmlNode firstChild = xmlDocument.FirstChild;
				foreach (object obj in firstChild.ChildNodes)
				{
					XmlNode node2 = (XmlNode)obj;
					XmlNode newChild = this.document.ImportNode(node2, true);
					node.AppendChild(newChild);
				}
			}

			// Token: 0x06004999 RID: 18841 RVA: 0x0012EAA8 File Offset: 0x0012CCA8
			private void DecryptAndReplaceNodeText(XmlNode node)
			{
				string innerText = this.SymmetricDecrypt(Convert.FromBase64String(node.InnerText));
				node.InnerText = innerText;
			}

			// Token: 0x0600499A RID: 18842 RVA: 0x0012EAD0 File Offset: 0x0012CCD0
			private void DecryptAndReplaceAttribute(XmlNode node, string attributeName)
			{
				XmlAttribute xmlAttribute = node.Attributes[attributeName];
				if (xmlAttribute == null)
				{
					throw new XmlException(string.Format("Attribute '{0}' was not found in node '{1}'", attributeName, node.Name));
				}
				string value = this.SymmetricDecrypt(Convert.FromBase64String(xmlAttribute.Value));
				xmlAttribute.Value = value;
			}

			// Token: 0x0600499B RID: 18843 RVA: 0x0012EB20 File Offset: 0x0012CD20
			private void DecryptEntities()
			{
				using (XmlNodeList xmlNodeList = this.document.SelectNodes("/ns:RulePackage/ns:Rules/ns:Entity", this.nsmgr))
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode node = (XmlNode)obj;
						this.DecryptAndReplaceAttribute(node, "patternsProximity");
						this.DecryptAndReplaceNodeTextFragment(node);
					}
				}
			}

			// Token: 0x0600499C RID: 18844 RVA: 0x0012EBB0 File Offset: 0x0012CDB0
			private void DecryptAffinities()
			{
				using (XmlNodeList xmlNodeList = this.document.SelectNodes("/ns:RulePackage/ns:Rules/ns:Affinity", this.nsmgr))
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode node = (XmlNode)obj;
						this.DecryptAndReplaceAttribute(node, "evidencesProximity");
						this.DecryptAndReplaceNodeTextFragment(node);
					}
				}
			}

			// Token: 0x0600499D RID: 18845 RVA: 0x0012EC40 File Offset: 0x0012CE40
			private void DecryptRegexes()
			{
				using (XmlNodeList xmlNodeList = this.document.SelectNodes("/ns:RulePackage/ns:Rules/ns:Regex", this.nsmgr))
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode node = (XmlNode)obj;
						this.DecryptAndReplaceNodeText(node);
					}
				}
			}

			// Token: 0x0600499E RID: 18846 RVA: 0x0012ECC4 File Offset: 0x0012CEC4
			private void DecryptKeywordLists()
			{
				using (XmlNodeList xmlNodeList = this.document.SelectNodes("/ns:RulePackage/ns:Rules/ns:Keyword", this.nsmgr))
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode node = (XmlNode)obj;
						this.DecryptAndReplaceNodeTextFragment(node);
					}
				}
			}

			// Token: 0x0600499F RID: 18847 RVA: 0x0012ED48 File Offset: 0x0012CF48
			private void RemoveEncryptionNode()
			{
				XmlNode encryptionNode = this.GetEncryptionNode();
				XmlNode parentNode = encryptionNode.ParentNode;
				parentNode.RemoveChild(encryptionNode);
			}

			// Token: 0x060049A0 RID: 18848 RVA: 0x0012ED6B File Offset: 0x0012CF6B
			private XmlNode GetEncryptionNode()
			{
				return RulePackageDecrypter.InternalRulePackageDecrypter.SelectAndEnsureSingleNode(this.document, this.nsmgr, "/ns:RulePackage/ns:RulePack/ns:Encryption");
			}

			// Token: 0x060049A1 RID: 18849 RVA: 0x0012ED84 File Offset: 0x0012CF84
			private byte[] GetEncryptionParameter(XmlNode encryptionNode, string xpath)
			{
				XmlNode xmlNode = RulePackageDecrypter.InternalRulePackageDecrypter.SelectAndEnsureSingleNode(encryptionNode, this.nsmgr, xpath);
				if (xmlNode == null)
				{
					throw new XmlException(string.Format("Invalid <Encryption> node: '{0}' was not found", xpath));
				}
				return Convert.FromBase64String(xmlNode.InnerText);
			}

			// Token: 0x060049A2 RID: 18850 RVA: 0x0012EDC0 File Offset: 0x0012CFC0
			private byte[] ReplaceSymmetricAlgorithmParameters(byte[] symmetricKey, byte[] initializationVector)
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlReader reader = XmlReader.Create(new MemoryStream(this.inputRulePackageData), ClassificationDefinitionUtils.CreateSafeXmlReaderSettings());
				xmlDocument.Load(reader);
				XmlNode xmlNode = RulePackageDecrypter.InternalRulePackageDecrypter.SelectAndEnsureSingleNode(xmlDocument, this.nsmgr, "/ns:RulePackage/ns:RulePack/ns:Encryption/ns:Key");
				if (xmlNode == null)
				{
					throw new XmlException("Encryption key node not found");
				}
				xmlNode.InnerText = Convert.ToBase64String(symmetricKey);
				XmlNode xmlNode2 = RulePackageDecrypter.InternalRulePackageDecrypter.SelectAndEnsureSingleNode(xmlDocument, this.nsmgr, "/ns:RulePackage/ns:RulePack/ns:Encryption/ns:IV");
				if (xmlNode2 == null)
				{
					throw new XmlException("Encryption IV node not found");
				}
				xmlNode2.InnerText = Convert.ToBase64String(initializationVector);
				return XmlProcessingUtils.XmlDocumentToUtf16EncodedBytes(xmlDocument);
			}

			// Token: 0x060049A3 RID: 18851 RVA: 0x0012EE50 File Offset: 0x0012D050
			private byte[] AsymmetricDecrypt(byte[] encryptedKey)
			{
				byte[] array = null;
				CryptographicException ex = null;
				foreach (RSACryptoServiceProvider rsacryptoServiceProvider in this.GetCryptoServiceProviders())
				{
					try
					{
						array = rsacryptoServiceProvider.Decrypt(encryptedKey, false);
						break;
					}
					catch (CryptographicException ex2)
					{
						if (ex == null)
						{
							ex = ex2;
						}
						array = null;
					}
				}
				if (array == null)
				{
					throw ex;
				}
				return array;
			}

			// Token: 0x060049A4 RID: 18852 RVA: 0x0012EEC8 File Offset: 0x0012D0C8
			private string GetCertificateSubjectName()
			{
				string text = null;
				using (RegistryKey registryKey = this.OpenExchangeDlpRulePackageEncryptionRegKey())
				{
					text = (string)registryKey.GetValue("CertSubject");
				}
				if (text == null)
				{
					throw new InvalidOperationException(string.Format("Error reading registry value '{0}/{1}'", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Dlp\\ClassificationRulePackageEncryption", "CertSubject"));
				}
				return text;
			}

			// Token: 0x060049A5 RID: 18853 RVA: 0x0012EF2C File Offset: 0x0012D12C
			private RegistryKey OpenExchangeDlpRulePackageEncryptionRegKey()
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Dlp\\ClassificationRulePackageEncryption");
				if (registryKey == null)
				{
					throw new InvalidOperationException(string.Format("Error reading registry key '{0}'", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Dlp\\ClassificationRulePackageEncryption"));
				}
				return registryKey;
			}

			// Token: 0x060049A6 RID: 18854 RVA: 0x0012EF64 File Offset: 0x0012D164
			private bool IsCertValid(X509Certificate2 cert)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				return utcNow >= (ExDateTime)cert.NotBefore && utcNow <= (ExDateTime)cert.NotAfter;
			}

			// Token: 0x060049A7 RID: 18855 RVA: 0x0012EFA0 File Offset: 0x0012D1A0
			private List<X509Certificate2> GetCertificates(string distinguishedSubjectName)
			{
				X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				List<X509Certificate2> list = new List<X509Certificate2>();
				try
				{
					X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, distinguishedSubjectName, false);
					if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
					{
						throw new InvalidOperationException(string.Format("Unable to find any certificates with subject name '{0}' in local machine store.", distinguishedSubjectName));
					}
					foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
					{
						if (this.IsCertValid(x509Certificate) && x509Certificate.HasPrivateKey && x509Certificate.Verify())
						{
							list.Add(x509Certificate);
						}
					}
					if (list.Count == 0)
					{
						throw new InvalidOperationException(string.Format("Unable to find a valid certificate with subject name '{0}' in local machine store.", distinguishedSubjectName));
					}
				}
				finally
				{
					x509Store.Close();
				}
				list.Sort(new Comparison<X509Certificate2>(CertSearcher.CompareByNotBefore));
				return list;
			}

			// Token: 0x04002C53 RID: 11347
			private const string ExchangeDlpRulePackageEncryptionRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Dlp\\ClassificationRulePackageEncryption";

			// Token: 0x04002C54 RID: 11348
			private const string CertThumbprint = "CertThumbprint";

			// Token: 0x04002C55 RID: 11349
			private const string CertSubject = "CertSubject";

			// Token: 0x04002C56 RID: 11350
			private byte[] inputRulePackageData;

			// Token: 0x04002C57 RID: 11351
			private XmlDocument document = new XmlDocument();

			// Token: 0x04002C58 RID: 11352
			private XmlNamespaceManager nsmgr;

			// Token: 0x04002C59 RID: 11353
			private SymmetricAlgorithm algorithm;
		}
	}
}
