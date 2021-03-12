using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200029D RID: 669
	public class AdfsAuthenticationConfig
	{
		// Token: 0x06001F37 RID: 7991 RVA: 0x0008B093 File Offset: 0x00089293
		public AdfsAuthenticationConfig(string encodedRawConfig)
		{
			this.encodedRawConfig = encodedRawConfig;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x0008B0A4 File Offset: 0x000892A4
		internal static bool Validate(string encodedRawConfig)
		{
			AdfsAuthenticationConfig adfsAuthenticationConfig = new AdfsAuthenticationConfig(encodedRawConfig);
			List<ValidationError> list = new List<ValidationError>();
			adfsAuthenticationConfig.Validate(list);
			return list.Count == 0;
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0008B0D0 File Offset: 0x000892D0
		internal static string Encode(string inputString)
		{
			string result = null;
			if (inputString != null)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(inputString);
				result = Convert.ToBase64String(bytes);
			}
			return result;
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x0008B0F8 File Offset: 0x000892F8
		internal static bool TryDecode(string inputString, out string outputString)
		{
			outputString = null;
			bool result = false;
			if (inputString != null)
			{
				try
				{
					byte[] bytes = Convert.FromBase64String(inputString);
					outputString = Encoding.UTF8.GetString(bytes);
					result = true;
				}
				catch (FormatException)
				{
				}
			}
			return result;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0008B13C File Offset: 0x0008933C
		private static bool StringParser(string inputValue, out string outputValue)
		{
			outputValue = inputValue;
			return true;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0008B142 File Offset: 0x00089342
		private static bool UriParser(string inputValue, out Uri uri)
		{
			return Uri.TryCreate(inputValue, UriKind.Absolute, out uri);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0008B14C File Offset: 0x0008934C
		private static XmlNode GetOrAppendNode(XmlNode parentNode, bool isAttribute, string name, string value = null)
		{
			IEnumerable enumerable;
			if (!isAttribute)
			{
				IEnumerable childNodes = parentNode.ChildNodes;
				enumerable = childNodes;
			}
			else
			{
				enumerable = parentNode.Attributes;
			}
			IEnumerable enumerable2 = enumerable;
			XmlNode xmlNode = null;
			using (enumerable2 as IDisposable)
			{
				xmlNode = AdfsAuthenticationConfig.SearchNodeByName(enumerable2, name);
			}
			if (xmlNode == null)
			{
				if (isAttribute)
				{
					xmlNode = parentNode.OwnerDocument.CreateAttribute(name);
					parentNode.Attributes.Append((XmlAttribute)xmlNode);
				}
				else
				{
					xmlNode = parentNode.OwnerDocument.CreateElement(name);
					parentNode.AppendChild((XmlElement)xmlNode);
				}
				if (isAttribute && value != null)
				{
					xmlNode.Value = value;
				}
			}
			return xmlNode;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0008B1EC File Offset: 0x000893EC
		private static XmlNode SearchNodeByName(IEnumerable list, string name)
		{
			XmlNode result = null;
			foreach (object obj in list)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode != null && xmlNode.Name == name)
				{
					result = xmlNode;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0008B27C File Offset: 0x0008947C
		internal void Validate(List<ValidationError> errors)
		{
			if (string.IsNullOrEmpty(this.encodedRawConfig))
			{
				return;
			}
			if (this.EnsureXmlObject(false))
			{
				this.ValidateNodeList<Uri>(errors, "/service/audienceUris/add/@value", new AdfsAuthenticationConfig.TryParseValueDelegate<Uri>(AdfsAuthenticationConfig.UriParser), OrganizationSchema.AdfsAudienceUris, DirectoryStrings.ErrorAdfsAudienceUris, (string x) => DirectoryStrings.ErrorAdfsAudienceUriFormat(x), (string x) => DirectoryStrings.ErrorAdfsAudienceUriDup(x));
				this.ValidateNodeList<string>(errors, "/service/issuerNameRegistry/trustedIssuers/add/@thumbprint", delegate(string input, out string output)
				{
					output = input;
					return !string.IsNullOrEmpty(input);
				}, OrganizationSchema.AdfsSignCertificateThumbprints, DirectoryStrings.ErrorAdfsTrustedIssuers, (string x) => DirectoryStrings.ErrorAdfsTrustedIssuerFormat(x), null);
				XmlNode xmlNode = this.configXmlDoc.SelectSingleNode("/service/federatedAuthentication/wsFederation/@issuer");
				Uri uri;
				if (xmlNode == null || !Uri.TryCreate(xmlNode.Value, UriKind.Absolute, out uri))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorAdfsIssuer, OrganizationSchema.AdfsIssuer, null));
					return;
				}
			}
			else
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorAdfsConfigFormat, OrganizationSchema.AdfsAuthenticationRawConfiguration, null));
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0008B3A4 File Offset: 0x000895A4
		public string ConfigXml
		{
			get
			{
				string outerXml;
				if (this.configXmlDoc == null)
				{
					AdfsAuthenticationConfig.TryDecode(this.encodedRawConfig, out outerXml);
				}
				else
				{
					outerXml = this.configXmlDoc.OuterXml;
				}
				return outerXml;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x0008B3D6 File Offset: 0x000895D6
		public string EncodedConfig
		{
			get
			{
				return AdfsAuthenticationConfig.Encode(this.ConfigXml);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x0008B3E3 File Offset: 0x000895E3
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x0008B3FC File Offset: 0x000895FC
		public Uri Issuer
		{
			get
			{
				return this.GetValue<Uri>("/service/federatedAuthentication/wsFederation/@issuer", new AdfsAuthenticationConfig.TryParseValueDelegate<Uri>(AdfsAuthenticationConfig.UriParser));
			}
			set
			{
				this.SetValue("/service/federatedAuthentication/wsFederation/@issuer", (value == null) ? string.Empty : value.ToString());
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0008B41F File Offset: 0x0008961F
		// (set) Token: 0x06001F45 RID: 8005 RVA: 0x0008B450 File Offset: 0x00089650
		public MultiValuedProperty<Uri> AudienceUris
		{
			get
			{
				return this.GetValues<Uri>("/service/audienceUris/add/@value", new AdfsAuthenticationConfig.TryParseValueDelegate<Uri>(AdfsAuthenticationConfig.UriParser));
			}
			set
			{
				if (value != null && this.EnsureXmlObject(true))
				{
					XmlElement nodePrototype = this.configXmlDoc.CreateElement("add");
					this.SetValues<Uri>("/service/audienceUris", value, nodePrototype, delegate(XmlNode newNode, Uri uri)
					{
						AdfsAuthenticationConfig.GetOrAppendNode(newNode, true, "value", uri.ToString());
					});
				}
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0008B4A4 File Offset: 0x000896A4
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x0008B4E0 File Offset: 0x000896E0
		public MultiValuedProperty<string> SignCertificateThumbprints
		{
			get
			{
				return this.GetValues<string>("/service/issuerNameRegistry/trustedIssuers/add/@thumbprint", new AdfsAuthenticationConfig.TryParseValueDelegate<string>(AdfsAuthenticationConfig.StringParser));
			}
			set
			{
				if (value != null && this.EnsureXmlObject(true))
				{
					XmlElement nodePrototype = this.configXmlDoc.CreateElement("add");
					this.SetValues<string>("/service/issuerNameRegistry/trustedIssuers", value, nodePrototype, delegate(XmlNode newNode, string thumbprint)
					{
						AdfsAuthenticationConfig.GetOrAppendNode(newNode, true, "thumbprint", thumbprint);
						AdfsAuthenticationConfig.GetOrAppendNode(newNode, true, "name", "Adfs");
					});
				}
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x0008B534 File Offset: 0x00089734
		// (set) Token: 0x06001F49 RID: 8009 RVA: 0x0008B550 File Offset: 0x00089750
		public string EncryptCertificateThumbprint
		{
			get
			{
				return this.GetValue<string>("/service/serviceCertificate/certificateReference/@findValue", new AdfsAuthenticationConfig.TryParseValueDelegate<string>(AdfsAuthenticationConfig.StringParser));
			}
			set
			{
				if (this.EnsureXmlObject(true))
				{
					if (string.IsNullOrEmpty(value))
					{
						XmlNode xmlNode = this.configXmlDoc.SelectSingleNode("/service/serviceCertificate");
						if (xmlNode != null)
						{
							xmlNode.ParentNode.RemoveChild(xmlNode);
							return;
						}
					}
					else
					{
						XmlNode xmlNode2 = this.configXmlDoc.SelectSingleNode("service");
						if (xmlNode2 != null)
						{
							XmlNode orAppendNode = AdfsAuthenticationConfig.GetOrAppendNode(xmlNode2, false, "serviceCertificate", null);
							XmlNode orAppendNode2 = AdfsAuthenticationConfig.GetOrAppendNode(orAppendNode, false, "certificateReference", null);
							AdfsAuthenticationConfig.GetOrAppendNode(orAppendNode2, true, "x509FindType", "FindByThumbprint");
							AdfsAuthenticationConfig.GetOrAppendNode(orAppendNode2, true, "findValue", null);
							AdfsAuthenticationConfig.GetOrAppendNode(orAppendNode2, true, "storeLocation", "LocalMachine");
							AdfsAuthenticationConfig.GetOrAppendNode(orAppendNode2, true, "storeName", "My");
							this.SetValue("/service/serviceCertificate/certificateReference/@findValue", value ?? string.Empty);
						}
					}
				}
			}
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0008B620 File Offset: 0x00089820
		private void ValidateNodeList<T>(List<ValidationError> errors, string xPath, AdfsAuthenticationConfig.TryParseValueDelegate<T> parser, ADPropertyDefinition propertyDefinition, LocalizedString nodeCountErrorString, AdfsAuthenticationConfig.NodeValueErrorDelegate nodeValueError, AdfsAuthenticationConfig.NodeValueErrorDelegate nodeValueDupError)
		{
			using (XmlNodeList xmlNodeList = this.configXmlDoc.SelectNodes(xPath))
			{
				if (xmlNodeList.Count == 0)
				{
					errors.Add(new PropertyValidationError(nodeCountErrorString, propertyDefinition, null));
				}
				else
				{
					MultiValuedProperty<T> multiValuedProperty = new MultiValuedProperty<T>();
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj;
						T item;
						if (!parser(xmlNode.Value, out item))
						{
							errors.Add(new PropertyValidationError(nodeValueError(xmlNode.Value), propertyDefinition, null));
						}
						else if (nodeValueDupError != null)
						{
							try
							{
								multiValuedProperty.Add(item);
							}
							catch (InvalidOperationException)
							{
								errors.Add(new PropertyValidationError(nodeValueDupError(xmlNode.Value), propertyDefinition, null));
							}
						}
					}
				}
			}
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x0008B720 File Offset: 0x00089920
		private MultiValuedProperty<T> GetValues<T>(string xPath, AdfsAuthenticationConfig.TryParseValueDelegate<T> parser)
		{
			MultiValuedProperty<T> multiValuedProperty = new MultiValuedProperty<T>();
			if (this.EnsureXmlObject(false))
			{
				using (XmlNodeList xmlNodeList = this.configXmlDoc.SelectNodes(xPath))
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode = (XmlNode)obj;
						string value = xmlNode.Value;
						T item;
						if (parser(value, out item))
						{
							try
							{
								multiValuedProperty.Add(item);
							}
							catch (InvalidOperationException)
							{
							}
						}
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0008B7D4 File Offset: 0x000899D4
		private T GetValue<T>(string xPath, AdfsAuthenticationConfig.TryParseValueDelegate<T> parser)
		{
			MultiValuedProperty<T> values = this.GetValues<T>(xPath, parser);
			if (values.Count <= 0)
			{
				return default(T);
			}
			return values[0];
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0008B804 File Offset: 0x00089A04
		private void SetValue(string xPath, string value)
		{
			if (this.EnsureXmlObject(true))
			{
				XmlNode xmlNode = this.configXmlDoc.SelectSingleNode(xPath);
				if (xmlNode != null)
				{
					xmlNode.Value = value;
				}
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0008B834 File Offset: 0x00089A34
		private void SetValues<T>(string xPath, MultiValuedProperty<T> values, XmlNode nodePrototype, AdfsAuthenticationConfig.AddXmlNodeDelegate<T> processor)
		{
			if (this.EnsureXmlObject(true))
			{
				XmlNode xmlNode = this.configXmlDoc.SelectSingleNode(xPath);
				if (xmlNode != null)
				{
					xmlNode.RemoveAll();
					foreach (T value in values)
					{
						XmlNode xmlNode2 = nodePrototype.Clone();
						processor(xmlNode2, value);
						xmlNode.AppendChild(xmlNode2);
					}
				}
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0008B8B4 File Offset: 0x00089AB4
		private bool EnsureXmlObject(bool forceCreateDefaultObject)
		{
			bool result = false;
			string text = null;
			if (this.configXmlDoc != null)
			{
				result = true;
			}
			else
			{
				if (string.IsNullOrEmpty(this.encodedRawConfig) || !AdfsAuthenticationConfig.TryDecode(this.encodedRawConfig, out text))
				{
					if (!forceCreateDefaultObject)
					{
						return result;
					}
				}
				try
				{
					this.configXmlDoc = new SafeXmlDocument();
					this.configXmlDoc.LoadXml(string.IsNullOrEmpty(text) ? "<service>  <federatedAuthentication>      <wsFederation passiveRedirectEnabled=\"true\" issuer=\"\" realm=\"https://fakerealm/\" requireHttps=\"true\" />      <cookieHandler requireSsl=\"true\" path=\"/\" />  </federatedAuthentication>  <certificateValidation certificateValidationMode=\"PeerOrChainTrust\" />  <audienceUris></audienceUris>  <issuerNameRegistry type=\"Microsoft.IdentityModel.Tokens.ConfigurationBasedIssuerNameRegistry, Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\">      <trustedIssuers></trustedIssuers>  </issuerNameRegistry></service>" : text);
					result = true;
				}
				catch (XmlException)
				{
					this.configXmlDoc = null;
				}
			}
			return result;
		}

		// Token: 0x0400129A RID: 4762
		private const string IssuerXPath = "/service/federatedAuthentication/wsFederation/@issuer";

		// Token: 0x0400129B RID: 4763
		private const string AudienceUrisXPath = "/service/audienceUris/add/@value";

		// Token: 0x0400129C RID: 4764
		private const string AudienceUrisParentXPath = "/service/audienceUris";

		// Token: 0x0400129D RID: 4765
		private const string EncryptCertXPath = "/service/serviceCertificate/certificateReference/@findValue";

		// Token: 0x0400129E RID: 4766
		private const string ServiceCertificateXPath = "/service/serviceCertificate";

		// Token: 0x0400129F RID: 4767
		private const string TrustedIssuersXPath = "/service/issuerNameRegistry/trustedIssuers/add/@thumbprint";

		// Token: 0x040012A0 RID: 4768
		private const string TrustedIssuersParentXPath = "/service/issuerNameRegistry/trustedIssuers";

		// Token: 0x040012A1 RID: 4769
		private const string WsFederationIssuerXPath = "/service/federatedAuthentication/wsFederation/@issuer";

		// Token: 0x040012A2 RID: 4770
		private const string DefaultTemplate = "<service>  <federatedAuthentication>      <wsFederation passiveRedirectEnabled=\"true\" issuer=\"\" realm=\"https://fakerealm/\" requireHttps=\"true\" />      <cookieHandler requireSsl=\"true\" path=\"/\" />  </federatedAuthentication>  <certificateValidation certificateValidationMode=\"PeerOrChainTrust\" />  <audienceUris></audienceUris>  <issuerNameRegistry type=\"Microsoft.IdentityModel.Tokens.ConfigurationBasedIssuerNameRegistry, Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\">      <trustedIssuers></trustedIssuers>  </issuerNameRegistry></service>";

		// Token: 0x040012A3 RID: 4771
		private readonly string encodedRawConfig;

		// Token: 0x040012A4 RID: 4772
		private XmlDocument configXmlDoc;

		// Token: 0x0200029E RID: 670
		// (Invoke) Token: 0x06001F57 RID: 8023
		private delegate bool TryParseValueDelegate<T>(string inputValue, out T outputValue);

		// Token: 0x0200029F RID: 671
		// (Invoke) Token: 0x06001F5B RID: 8027
		private delegate void AddXmlNodeDelegate<T>(XmlNode newNode, T value);

		// Token: 0x020002A0 RID: 672
		// (Invoke) Token: 0x06001F5F RID: 8031
		private delegate LocalizedString NodeValueErrorDelegate(string value);
	}
}
