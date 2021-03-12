using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003AE RID: 942
	[XmlRoot("MailboxCulture", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MailboxCultureType : SoapHeader
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0002A704 File Offset: 0x00028904
		// (set) Token: 0x06001D50 RID: 7504 RVA: 0x0002A70C File Offset: 0x0002890C
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

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0002A715 File Offset: 0x00028915
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0002A71D File Offset: 0x0002891D
		[XmlText(DataType = "language")]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04001363 RID: 4963
		private XmlAttribute[] anyAttrField;

		// Token: 0x04001364 RID: 4964
		private string valueField;
	}
}
