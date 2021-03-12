using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.LiveServices;

namespace Microsoft.Exchange.Management.Sharing
{
	// Token: 0x02000D8D RID: 3469
	[Serializable]
	public sealed class WindowsLiveIdNamespace : ConfigurableObject
	{
		// Token: 0x06008531 RID: 34097 RVA: 0x00220C30 File Offset: 0x0021EE30
		public WindowsLiveIdNamespace() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17002967 RID: 10599
		// (get) Token: 0x06008532 RID: 34098 RVA: 0x00220C3D File Offset: 0x0021EE3D
		// (set) Token: 0x06008533 RID: 34099 RVA: 0x00220C54 File Offset: 0x0021EE54
		public LiveIdInstanceType InstanceType
		{
			get
			{
				return (LiveIdInstanceType)this.propertyBag[WindowsLiveIdNamespaceSchema.InstanceType];
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.InstanceType] = value;
			}
		}

		// Token: 0x17002968 RID: 10600
		// (get) Token: 0x06008534 RID: 34100 RVA: 0x00220C6C File Offset: 0x0021EE6C
		// (set) Token: 0x06008535 RID: 34101 RVA: 0x00220C83 File Offset: 0x0021EE83
		public string Name
		{
			get
			{
				return (string)this.propertyBag[WindowsLiveIdNamespaceSchema.Name];
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.Name] = value;
			}
		}

		// Token: 0x17002969 RID: 10601
		// (get) Token: 0x06008536 RID: 34102 RVA: 0x00220C96 File Offset: 0x0021EE96
		// (set) Token: 0x06008537 RID: 34103 RVA: 0x00220CAD File Offset: 0x0021EEAD
		public string ID
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.ID] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.ID] = value;
			}
		}

		// Token: 0x1700296A RID: 10602
		// (get) Token: 0x06008538 RID: 34104 RVA: 0x00220CC0 File Offset: 0x0021EEC0
		public override ObjectId Identity
		{
			get
			{
				return this.propertyBag[SimpleProviderObjectSchema.Identity] as ObjectId;
			}
		}

		// Token: 0x1700296B RID: 10603
		// (get) Token: 0x06008539 RID: 34105 RVA: 0x00220CD7 File Offset: 0x0021EED7
		// (set) Token: 0x0600853A RID: 34106 RVA: 0x00220CEE File Offset: 0x0021EEEE
		public string SiteID
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.SiteID] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.SiteID] = value;
			}
		}

		// Token: 0x1700296C RID: 10604
		// (get) Token: 0x0600853B RID: 34107 RVA: 0x00220D01 File Offset: 0x0021EF01
		// (set) Token: 0x0600853C RID: 34108 RVA: 0x00220D18 File Offset: 0x0021EF18
		public string AppID
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.AppID] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.AppID] = value;
			}
		}

		// Token: 0x1700296D RID: 10605
		// (get) Token: 0x0600853D RID: 34109 RVA: 0x00220D2B File Offset: 0x0021EF2B
		// (set) Token: 0x0600853E RID: 34110 RVA: 0x00220D42 File Offset: 0x0021EF42
		public Uri Uri
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.URI] as Uri;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.URI] = value;
			}
		}

		// Token: 0x1700296E RID: 10606
		// (get) Token: 0x0600853F RID: 34111 RVA: 0x00220D55 File Offset: 0x0021EF55
		// (set) Token: 0x06008540 RID: 34112 RVA: 0x00220D6C File Offset: 0x0021EF6C
		public X509Certificate2 Certificate
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.Certificate] as X509Certificate2;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.Certificate] = value;
			}
		}

		// Token: 0x1700296F RID: 10607
		// (get) Token: 0x06008541 RID: 34113 RVA: 0x00220D7F File Offset: 0x0021EF7F
		// (set) Token: 0x06008542 RID: 34114 RVA: 0x00220D96 File Offset: 0x0021EF96
		public X509Certificate2 NextCertificate
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.NextCertificate] as X509Certificate2;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.NextCertificate] = value;
			}
		}

		// Token: 0x17002970 RID: 10608
		// (get) Token: 0x06008543 RID: 34115 RVA: 0x00220DA9 File Offset: 0x0021EFA9
		// (set) Token: 0x06008544 RID: 34116 RVA: 0x00220DC0 File Offset: 0x0021EFC0
		public string RawXml
		{
			get
			{
				return this.propertyBag[WindowsLiveIdNamespaceSchema.RawXml] as string;
			}
			set
			{
				this.propertyBag[WindowsLiveIdNamespaceSchema.RawXml] = value;
			}
		}

		// Token: 0x17002971 RID: 10609
		// (get) Token: 0x06008545 RID: 34117 RVA: 0x00220DD3 File Offset: 0x0021EFD3
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return WindowsLiveIdNamespace.schema;
			}
		}

		// Token: 0x17002972 RID: 10610
		// (get) Token: 0x06008546 RID: 34118 RVA: 0x00220DDA File Offset: 0x0021EFDA
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06008547 RID: 34119 RVA: 0x00220DE4 File Offset: 0x0021EFE4
		internal static WindowsLiveIdNamespace ParseXml(LiveIdInstanceType liveIdInstanceType, XmlDocument xml)
		{
			WindowsLiveIdNamespace windowsLiveIdNamespace = new WindowsLiveIdNamespace();
			windowsLiveIdNamespace.InstanceType = liveIdInstanceType;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xml.NameTable);
			LiveServicesHelper.AddNamespaces(xml, xmlNamespaceManager);
			XmlNode xmlNode = xml.SelectSingleNode("p:Namespace/p:name", xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdNamespace.Name = xmlNode.InnerText;
			}
			xmlNode = xml.SelectSingleNode("p:Namespace/p:ID", xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdNamespace.propertyBag[SimpleProviderObjectSchema.Identity] = new WindowsLiveIdIdentity(xmlNode.InnerText);
				windowsLiveIdNamespace.ID = xmlNode.InnerText;
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdNamespace.GetPropertyXPath("SiteID"), xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdNamespace.SiteID = xmlNode.InnerText;
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdNamespace.GetPropertyXPath("AppID"), xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdNamespace.AppID = xmlNode.InnerText;
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdNamespace.GetPropertyXPath("URI"), xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				Uri uri = null;
				if (Uri.TryCreate(xmlNode.InnerText, UriKind.RelativeOrAbsolute, out uri))
				{
					windowsLiveIdNamespace.Uri = uri;
				}
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdNamespace.GetPropertyXPath("Certificate"), xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdNamespace.Certificate = WindowsLiveIdApplicationCertificate.CertificateFromBase64(xmlNode.InnerText);
			}
			xmlNode = xml.SelectSingleNode(WindowsLiveIdNamespace.GetPropertyXPath("NextCertificate"), xmlNamespaceManager);
			if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText))
			{
				windowsLiveIdNamespace.NextCertificate = WindowsLiveIdApplicationCertificate.CertificateFromBase64(xmlNode.InnerText);
			}
			windowsLiveIdNamespace.RawXml = xml.DocumentElement.InnerXml;
			return windowsLiveIdNamespace;
		}

		// Token: 0x06008548 RID: 34120 RVA: 0x00220F8D File Offset: 0x0021F18D
		private static string GetPropertyXPath(string propertyName)
		{
			return string.Format("p:Namespace/p:property[@name=\"{0}\"]", propertyName);
		}

		// Token: 0x04004056 RID: 16470
		private static ObjectSchema schema = ObjectSchema.GetInstance<WindowsLiveIdNamespaceSchema>();
	}
}
