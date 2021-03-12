using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200016A RID: 362
	[DebuggerStepThrough]
	[XmlType(Namespace = "HMMAIL:")]
	[XmlRoot("Categories", Namespace = "HMMAIL:", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[Serializable]
	public class stringWithCharSetType
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0001D6FE File Offset: 0x0001B8FE
		public stringWithCharSetType()
		{
			this.encodingField = "0";
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0001D711 File Offset: 0x0001B911
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0001D719 File Offset: 0x0001B919
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

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0001D722 File Offset: 0x0001B922
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0001D72A File Offset: 0x0001B92A
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0001D733 File Offset: 0x0001B933
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0001D73B File Offset: 0x0001B93B
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

		// Token: 0x040005DF RID: 1503
		private string charsetField;

		// Token: 0x040005E0 RID: 1504
		private string encodingField;

		// Token: 0x040005E1 RID: 1505
		private string valueField;
	}
}
