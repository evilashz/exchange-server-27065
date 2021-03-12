using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000088 RID: 136
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[DebuggerStepThrough]
	[Serializable]
	public class TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00015D34 File Offset: 0x00013F34
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x00015D3C File Offset: 0x00013F3C
		[XmlAttribute]
		public TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacityCodingScheme CodingScheme
		{
			get
			{
				return this.codingSchemeField;
			}
			set
			{
				this.codingSchemeField = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00015D45 File Offset: 0x00013F45
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x00015D4D File Offset: 0x00013F4D
		[XmlText]
		public int Value
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

		// Token: 0x04000285 RID: 645
		private TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacityCodingScheme codingSchemeField;

		// Token: 0x04000286 RID: 646
		private int valueField;
	}
}
