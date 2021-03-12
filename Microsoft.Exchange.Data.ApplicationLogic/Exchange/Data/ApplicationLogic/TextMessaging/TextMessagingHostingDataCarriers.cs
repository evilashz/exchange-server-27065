using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x0200007D RID: 125
	[XmlType(AnonymousType = true)]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class TextMessagingHostingDataCarriers
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00015B87 File Offset: 0x00013D87
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00015B8F File Offset: 0x00013D8F
		[XmlElement("Carrier", Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataCarriersCarrier[] Carrier
		{
			get
			{
				return this.carrierField;
			}
			set
			{
				this.carrierField = value;
			}
		}

		// Token: 0x04000268 RID: 616
		private TextMessagingHostingDataCarriersCarrier[] carrierField;
	}
}
