using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200016F RID: 367
	[XmlType(AnonymousType = true, Namespace = "HMFOLDER:")]
	[XmlRoot(Namespace = "HMFOLDER:", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DisplayName
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001D7CD File Offset: 0x0001B9CD
		public DisplayName()
		{
			this.encodingField = "0";
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001D7E0 File Offset: 0x0001B9E0
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001D7F1 File Offset: 0x0001B9F1
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0001D7F9 File Offset: 0x0001B9F9
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

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001D802 File Offset: 0x0001BA02
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0001D80A File Offset: 0x0001BA0A
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

		// Token: 0x040005ED RID: 1517
		private string charsetField;

		// Token: 0x040005EE RID: 1518
		private string encodingField;

		// Token: 0x040005EF RID: 1519
		private string valueField;
	}
}
