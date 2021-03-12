using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200018A RID: 394
	[XmlType(TypeName = "stringWithEncodingType", Namespace = "HMMAIL:")]
	[XmlRoot("ConversationTopic", Namespace = "HMMAIL:", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class stringWithEncodingType1
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x0001DC0A File Offset: 0x0001BE0A
		public stringWithEncodingType1()
		{
			this.encodingField = "0";
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0001DC1D File Offset: 0x0001BE1D
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0001DC25 File Offset: 0x0001BE25
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001DC2E File Offset: 0x0001BE2E
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0001DC36 File Offset: 0x0001BE36
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

		// Token: 0x04000650 RID: 1616
		private string encodingField;

		// Token: 0x04000651 RID: 1617
		private string valueField;
	}
}
