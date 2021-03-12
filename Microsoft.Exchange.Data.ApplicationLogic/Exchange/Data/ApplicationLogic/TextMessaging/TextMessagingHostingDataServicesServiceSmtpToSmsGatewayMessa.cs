using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000087 RID: 135
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[XmlType(AnonymousType = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRendering
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00015CF9 File Offset: 0x00013EF9
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00015D01 File Offset: 0x00013F01
		[XmlElement("Capacity", Form = XmlSchemaForm.Unqualified, IsNullable = true)]
		public TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity[] Capacity
		{
			get
			{
				return this.capacityField;
			}
			set
			{
				this.capacityField = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00015D0A File Offset: 0x00013F0A
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00015D12 File Offset: 0x00013F12
		[XmlAttribute]
		public TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer Container
		{
			get
			{
				return this.containerField;
			}
			set
			{
				this.containerField = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00015D1B File Offset: 0x00013F1B
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00015D23 File Offset: 0x00013F23
		[XmlIgnore]
		public bool ContainerSpecified
		{
			get
			{
				return this.containerFieldSpecified;
			}
			set
			{
				this.containerFieldSpecified = value;
			}
		}

		// Token: 0x04000282 RID: 642
		private TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingCapacity[] capacityField;

		// Token: 0x04000283 RID: 643
		private TextMessagingHostingDataServicesServiceSmtpToSmsGatewayMessageRenderingContainer containerField;

		// Token: 0x04000284 RID: 644
		private bool containerFieldSpecified;
	}
}
