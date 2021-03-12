using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000169 RID: 361
	[XmlRoot("ConversationTopic", Namespace = "HMMAIL:", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(TypeName = "stringWithEncodingType", Namespace = "HMMAIL:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[Serializable]
	public class stringWithEncodingType1
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x0001D6C9 File Offset: 0x0001B8C9
		public stringWithEncodingType1()
		{
			this.encodingField = "0";
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x0001D6E4 File Offset: 0x0001B8E4
		[XmlAttribute]
		[DefaultValue("0")]
		public string encoding
		{
			get
			{
				return this.encodingField;
			}
			set
			{
				this.encodingField = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001D6ED File Offset: 0x0001B8ED
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0001D6F5 File Offset: 0x0001B8F5
		[XmlText]
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

		// Token: 0x040005DD RID: 1501
		private string encodingField;

		// Token: 0x040005DE RID: 1502
		private string valueField;
	}
}
