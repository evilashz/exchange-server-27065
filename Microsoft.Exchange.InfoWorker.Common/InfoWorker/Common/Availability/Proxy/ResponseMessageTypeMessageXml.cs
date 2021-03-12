using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x0200012C RID: 300
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class ResponseMessageTypeMessageXml
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00024E69 File Offset: 0x00023069
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00024E71 File Offset: 0x00023071
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

		// Token: 0x04000655 RID: 1621
		private XmlElement[] anyField;
	}
}
