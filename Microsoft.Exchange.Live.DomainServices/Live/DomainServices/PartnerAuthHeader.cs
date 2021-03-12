using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000010 RID: 16
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlRoot(Namespace = "http://domains.live.com/Service/DomainServices/V1.0", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public class PartnerAuthHeader : SoapHeader
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006A58 File Offset: 0x00004C58
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00006A60 File Offset: 0x00004C60
		public int PartnerId
		{
			get
			{
				return this.partnerIdField;
			}
			set
			{
				this.partnerIdField = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006A69 File Offset: 0x00004C69
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006A71 File Offset: 0x00004C71
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

		// Token: 0x04000096 RID: 150
		private int partnerIdField;

		// Token: 0x04000097 RID: 151
		private XmlAttribute[] anyAttrField;
	}
}
