using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.ApplicationLogic.TextMessaging
{
	// Token: 0x02000083 RID: 131
	[XmlType(AnonymousType = true)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class TextMessagingHostingDataServicesServiceVoiceCallForwarding
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00015C6A File Offset: 0x00013E6A
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00015C72 File Offset: 0x00013E72
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Enable
		{
			get
			{
				return this.enableField;
			}
			set
			{
				this.enableField = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00015C7B File Offset: 0x00013E7B
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00015C83 File Offset: 0x00013E83
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string Disable
		{
			get
			{
				return this.disableField;
			}
			set
			{
				this.disableField = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00015C8C File Offset: 0x00013E8C
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00015C94 File Offset: 0x00013E94
		[XmlAttribute]
		public TextMessagingHostingDataServicesServiceVoiceCallForwardingType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00015C9D File Offset: 0x00013E9D
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00015CA5 File Offset: 0x00013EA5
		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return this.typeFieldSpecified;
			}
			set
			{
				this.typeFieldSpecified = value;
			}
		}

		// Token: 0x04000276 RID: 630
		private string enableField;

		// Token: 0x04000277 RID: 631
		private string disableField;

		// Token: 0x04000278 RID: 632
		private TextMessagingHostingDataServicesServiceVoiceCallForwardingType typeField;

		// Token: 0x04000279 RID: 633
		private bool typeFieldSpecified;
	}
}
