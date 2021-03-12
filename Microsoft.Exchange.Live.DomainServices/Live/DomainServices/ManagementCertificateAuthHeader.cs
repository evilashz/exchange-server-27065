using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000003 RID: 3
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[XmlRoot(Namespace = "http://domains.live.com/Service/DomainServices/V1.0", IsNullable = false)]
	[Serializable]
	public class ManagementCertificateAuthHeader : SoapHeader
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000683C File Offset: 0x00004A3C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00006844 File Offset: 0x00004A44
		public CertData ManagementCertificate
		{
			get
			{
				return this.managementCertificateField;
			}
			set
			{
				this.managementCertificateField = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000684D File Offset: 0x00004A4D
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006855 File Offset: 0x00004A55
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x04000060 RID: 96
		private CertData managementCertificateField;

		// Token: 0x04000061 RID: 97
		private XmlAttribute[] anyAttrField;
	}
}
