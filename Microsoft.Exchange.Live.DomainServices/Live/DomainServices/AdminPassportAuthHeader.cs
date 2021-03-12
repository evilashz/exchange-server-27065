using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200000F RID: 15
	[XmlRoot(Namespace = "http://domains.live.com/Service/DomainServices/V1.0", IsNullable = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[Serializable]
	public class AdminPassportAuthHeader : SoapHeader
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006A2E File Offset: 0x00004C2E
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00006A36 File Offset: 0x00004C36
		public string NetId
		{
			get
			{
				return this.netIdField;
			}
			set
			{
				this.netIdField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006A3F File Offset: 0x00004C3F
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006A47 File Offset: 0x00004C47
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

		// Token: 0x04000094 RID: 148
		private string netIdField;

		// Token: 0x04000095 RID: 149
		private XmlAttribute[] anyAttrField;
	}
}
