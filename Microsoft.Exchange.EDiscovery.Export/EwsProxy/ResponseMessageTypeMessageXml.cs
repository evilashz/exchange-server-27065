using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000AC RID: 172
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ResponseMessageTypeMessageXml
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001FC0E File Offset: 0x0001DE0E
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001FC16 File Offset: 0x0001DE16
		[XmlAnyElement]
		public XmlElement[] Any
		{
			get
			{
				return this.anyField;
			}
			set
			{
				this.anyField = value;
			}
		}

		// Token: 0x0400053A RID: 1338
		private XmlElement[] anyField;
	}
}
