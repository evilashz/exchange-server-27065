using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200018C RID: 396
	[XmlRoot("From", Namespace = "EMAIL:", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "EMAIL:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class stringWithEncodingType
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x0001DC85 File Offset: 0x0001BE85
		public stringWithEncodingType()
		{
			this.encodingField = "0";
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001DC98 File Offset: 0x0001BE98
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001DCA9 File Offset: 0x0001BEA9
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001DCB1 File Offset: 0x0001BEB1
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

		// Token: 0x04000655 RID: 1621
		private string encodingField;

		// Token: 0x04000656 RID: 1622
		private string valueField;
	}
}
