using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003B0 RID: 944
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RequestServerVersion : SoapHeader
	{
		// Token: 0x06001D67 RID: 7527 RVA: 0x0002A7CF File Offset: 0x000289CF
		public RequestServerVersion()
		{
			this.versionField = ExchangeVersionType.Exchange2013_SP1;
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0002A7DE File Offset: 0x000289DE
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0002A7E6 File Offset: 0x000289E6
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

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0002A7EF File Offset: 0x000289EF
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x0002A7F7 File Offset: 0x000289F7
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

		// Token: 0x0400136E RID: 4974
		private ExchangeVersionType versionField;

		// Token: 0x0400136F RID: 4975
		private XmlAttribute[] anyAttrField;
	}
}
