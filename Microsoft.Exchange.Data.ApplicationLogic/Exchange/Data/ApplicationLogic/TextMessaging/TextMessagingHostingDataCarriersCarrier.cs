using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x0200007E RID: 126
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[XmlType(AnonymousType = true)]
	[Serializable]
	public class TextMessagingHostingDataCarriersCarrier
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00015BA0 File Offset: 0x00013DA0
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00015BA8 File Offset: 0x00013DA8
		[XmlElement("LocalizedInfo", Form = XmlSchemaForm.Unqualified)]
		public TextMessagingHostingDataCarriersCarrierLocalizedInfo[] LocalizedInfo
		{
			get
			{
				return this.localizedInfoField;
			}
			set
			{
				this.localizedInfoField = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00015BB1 File Offset: 0x00013DB1
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00015BB9 File Offset: 0x00013DB9
		[XmlAttribute]
		public int Identity
		{
			get
			{
				return this.identityField;
			}
			set
			{
				this.identityField = value;
			}
		}

		// Token: 0x04000269 RID: 617
		private TextMessagingHostingDataCarriersCarrierLocalizedInfo[] localizedInfoField;

		// Token: 0x0400026A RID: 618
		private int identityField;
	}
}
