using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200019F RID: 415
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlRoot(Namespace = "HMFOLDER:", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMFOLDER:")]
	[Serializable]
	public class DisplayName
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x0001DF9B File Offset: 0x0001C19B
		public DisplayName()
		{
			this.encodingField = "0";
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0001DFAE File Offset: 0x0001C1AE
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0001DFB6 File Offset: 0x0001C1B6
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0001DFBF File Offset: 0x0001C1BF
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0001DFC7 File Offset: 0x0001C1C7
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
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

		// Token: 0x0400069F RID: 1695
		private string charsetField;

		// Token: 0x040006A0 RID: 1696
		private string encodingField;

		// Token: 0x040006A1 RID: 1697
		private string valueField;
	}
}
