using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200018B RID: 395
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "HMMAIL:")]
	[XmlRoot("Categories", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class stringWithCharSetType
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x0001DC3F File Offset: 0x0001BE3F
		public stringWithCharSetType()
		{
			this.encodingField = "0";
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001DC52 File Offset: 0x0001BE52
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0001DC5A File Offset: 0x0001BE5A
		[XmlAttribute]
		public string charset
		{
			get
			{
				return this.charsetField;
			}
			set
			{
				this.charsetField = value;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001DC63 File Offset: 0x0001BE63
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0001DC6B File Offset: 0x0001BE6B
		[DefaultValue("0")]
		[XmlAttribute]
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001DC74 File Offset: 0x0001BE74
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0001DC7C File Offset: 0x0001BE7C
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

		// Token: 0x04000652 RID: 1618
		private string charsetField;

		// Token: 0x04000653 RID: 1619
		private string encodingField;

		// Token: 0x04000654 RID: 1620
		private string valueField;
	}
}
