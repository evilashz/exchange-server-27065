using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x020000DC RID: 220
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RequestServerVersion : SoapHeader
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x000192B8 File Offset: 0x000174B8
		public RequestServerVersion()
		{
			this.versionField = ExchangeVersionType.Exchange2009;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000192C7 File Offset: 0x000174C7
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x000192CF File Offset: 0x000174CF
		[XmlAttribute]
		public ExchangeVersionType Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x000192D8 File Offset: 0x000174D8
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x000192E0 File Offset: 0x000174E0
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

		// Token: 0x04000358 RID: 856
		private ExchangeVersionType versionField;

		// Token: 0x04000359 RID: 857
		private XmlAttribute[] anyAttrField;
	}
}
