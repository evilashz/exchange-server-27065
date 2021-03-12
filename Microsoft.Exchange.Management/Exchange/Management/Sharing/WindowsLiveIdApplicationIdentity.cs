using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D8A RID: 3466
	[Serializable]
	public sealed class WindowsLiveIdApplicationIdentity : ConfigurableObject
	{
		// Token: 0x06008517 RID: 34071 RVA: 0x00220690 File Offset: 0x0021E890
		public WindowsLiveIdApplicationIdentity() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x1700295D RID: 10589
		// (get) Token: 0x06008518 RID: 34072 RVA: 0x0022069D File Offset: 0x0021E89D
		// (set) Token: 0x06008519 RID: 34073 RVA: 0x002206B4 File Offset: 0x0021E8B4
		public LiveIdInstanceType InstanceType
		{
			get
			{
				return (LiveIdInstanceType)this.propertyBag[WindowsLiveIdApplicationIdentitySchema.InstanceType];
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.InstanceType] = value;
			}
		}

		// Token: 0x1700295E RID: 10590
		// (get) Token: 0x0600851A RID: 34074 RVA: 0x002206CC File Offset: 0x0021E8CC
		// (set) Token: 0x0600851B RID: 34075 RVA: 0x002206E3 File Offset: 0x0021E8E3
		public string Name
		{
			get
			{
				return (string)this.propertyBag[WindowsLiveIdApplicationIdentitySchema.Name];
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.Name] = value;
			}
		}

		// Token: 0x1700295F RID: 10591
		// (get) Token: 0x0600851C RID: 34076 RVA: 0x002206F6 File Offset: 0x0021E8F6
		// (set) Token: 0x0600851D RID: 34077 RVA: 0x0022070D File Offset: 0x0021E90D
		public string Id
		{
			get
			{
				return this.propertyBag[WindowsLiveIdApplicationIdentitySchema.Id] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.Id] = value;
			}
		}

		// Token: 0x17002960 RID: 10592
		// (get) Token: 0x0600851E RID: 34078 RVA: 0x00220720 File Offset: 0x0021E920
		public override ObjectId Identity
		{
			get
			{
				return this.propertyBag[SimpleProviderObjectSchema.Identity] as ObjectId;
			}
		}

		// Token: 0x17002961 RID: 10593
		// (get) Token: 0x0600851F RID: 34079 RVA: 0x00220737 File Offset: 0x0021E937
		// (set) Token: 0x06008520 RID: 34080 RVA: 0x0022074E File Offset: 0x0021E94E
		public string Status
		{
			get
			{
				return this.propertyBag[WindowsLiveIdApplicationIdentitySchema.Status] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.Status] = value;
			}
		}

		// Token: 0x17002962 RID: 10594
		// (get) Token: 0x06008521 RID: 34081 RVA: 0x00220761 File Offset: 0x0021E961
		// (set) Token: 0x06008522 RID: 34082 RVA: 0x00220778 File Offset: 0x0021E978
		public MultiValuedProperty<Uri> UriCollection
		{
			get
			{
				return this.propertyBag[WindowsLiveIdApplicationIdentitySchema.UriCollection] as MultiValuedProperty<Uri>;
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.UriCollection] = value;
			}
		}

		// Token: 0x17002963 RID: 10595
		// (get) Token: 0x06008523 RID: 34083 RVA: 0x0022078B File Offset: 0x0021E98B
		// (set) Token: 0x06008524 RID: 34084 RVA: 0x002207A2 File Offset: 0x0021E9A2
		public MultiValuedProperty<WindowsLiveIdApplicationCertificate> CertificateCollection
		{
			get
			{
				return this.propertyBag[WindowsLiveIdApplicationIdentitySchema.CertificateCollection] as MultiValuedProperty<WindowsLiveIdApplicationCertificate>;
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.CertificateCollection] = value;
			}
		}

		// Token: 0x17002964 RID: 10596
		// (get) Token: 0x06008525 RID: 34085 RVA: 0x002207B5 File Offset: 0x0021E9B5
		// (set) Token: 0x06008526 RID: 34086 RVA: 0x002207CC File Offset: 0x0021E9CC
		public string RawXml
		{
			get
			{
				return this.propertyBag[WindowsLiveIdApplicationIdentitySchema.RawXml] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdApplicationIdentitySchema.RawXml] = value;
			}
		}

		// Token: 0x17002965 RID: 10597
		// (get) Token: 0x06008527 RID: 34087 RVA: 0x002207DF File Offset: 0x0021E9DF
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return WindowsLiveIdApplicationIdentity.schema;
			}
		}

		// Token: 0x17002966 RID: 10598
		// (get) Token: 0x06008528 RID: 34088 RVA: 0x002207E6 File Offset: 0x0021E9E6
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06008529 RID: 34089 RVA: 0x002207F0 File Offset: 0x0021E9F0
		internal static WindowsLiveIdApplicationIdentity ParseXml(LiveIdInstanceType liveIdInstanceType, XmlDocument xml)
		{
			WindowsLiveIdApplicationIdentity windowsLiveIdApplicationIdentity = new WindowsLiveIdApplicationIdentity();
			windowsLiveIdApplicationIdentity.InstanceType = liveIdInstanceType;
			XmlNode xmlNode = xml.SelectSingleNode("AppidData/Application/Name");
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdApplicationIdentity.Name = xmlNode.InnerText;
			}
			xmlNode = xml.SelectSingleNode("AppidData/Application/ID");
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdApplicationIdentity.propertyBag[SimpleProviderObjectSchema.Identity] = new WindowsLiveIdIdentity(xmlNode.InnerText);
				windowsLiveIdApplicationIdentity.Id = xmlNode.InnerText;
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdApplicationIdentity.GetPropertyXPath("Status"));
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdApplicationIdentity.Status = xmlNode.InnerText;
			}
			using (XmlNodeList xmlNodeList = xml.SelectNodes("AppidData/Application/URIs/URI/URIAddress"))
			{
				if (xmlNodeList != null)
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						if (!string.IsNullOrEmpty(xmlNode2.InnerText))
						{
							Uri item = null;
							if (Uri.TryCreate(xmlNode2.InnerText, UriKind.RelativeOrAbsolute, out item))
							{
								windowsLiveIdApplicationIdentity.UriCollection.Add(item);
							}
						}
					}
				}
			}
			using (XmlNodeList xmlNodeList2 = xml.SelectNodes("AppidData/Application/Certificates/Certificate"))
			{
				if (xmlNodeList2 != null)
				{
					foreach (object obj2 in xmlNodeList2)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						string name = string.Empty;
						X509Certificate2 x509Certificate = null;
						bool isCurrent = false;
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("CertificateName");
						if (xmlNode4 != null)
						{
							name = xmlNode4.InnerText;
						}
						xmlNode4 = xmlNode3.SelectSingleNode("CertificateData");
						if (xmlNode4 != null && !string.IsNullOrEmpty(xmlNode4.InnerText))
						{
							x509Certificate = WindowsLiveIdApplicationCertificate.CertificateFromBase64(xmlNode4.InnerText);
						}
						xmlNode4 = xmlNode3.SelectSingleNode("CertificateIsCurrent");
						if (xmlNode4 != null && !string.IsNullOrEmpty(xmlNode4.InnerText) && !bool.TryParse(xmlNode4.InnerText, out isCurrent))
						{
							isCurrent = false;
						}
						if (x509Certificate != null)
						{
							windowsLiveIdApplicationIdentity.CertificateCollection.Add(new WindowsLiveIdApplicationCertificate(name, isCurrent, x509Certificate));
						}
					}
				}
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdApplicationIdentity.GetPropertyXPath("Key"));
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				xmlNode.InnerText = "*****";
			}
			windowsLiveIdApplicationIdentity.RawXml = xml.DocumentElement.InnerXml;
			return windowsLiveIdApplicationIdentity;
		}

		// Token: 0x0600852A RID: 34090 RVA: 0x00220A98 File Offset: 0x0021EC98
		private static string GetPropertyXPath(string propertyName)
		{
			return string.Format("AppidData/Application/Properties/{0}", propertyName);
		}

		// Token: 0x0400404D RID: 16461
		private static ObjectSchema schema = ObjectSchema.GetInstance<WindowsLiveIdApplicationIdentitySchema>();
	}
}
