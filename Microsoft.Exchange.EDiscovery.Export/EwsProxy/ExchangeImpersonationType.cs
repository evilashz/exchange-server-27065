using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003AD RID: 941
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlRoot("ExchangeImpersonation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExchangeImpersonationType : SoapHeader
	{
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0002A6DA File Offset: 0x000288DA
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x0002A6E2 File Offset: 0x000288E2
		public ConnectingSIDType ConnectingSID
		{
			get
			{
				return this.connectingSIDField;
			}
			set
			{
				this.connectingSIDField = value;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0002A6EB File Offset: 0x000288EB
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0002A6F3 File Offset: 0x000288F3
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

		// Token: 0x04001361 RID: 4961
		private ConnectingSIDType connectingSIDField;

		// Token: 0x04001362 RID: 4962
		private XmlAttribute[] anyAttrField;
	}
}
