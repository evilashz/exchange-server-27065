using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200016B RID: 363
	[XmlRoot("From", Namespace = "EMAIL:", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "EMAIL:")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[Serializable]
	public class stringWithEncodingType
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001D744 File Offset: 0x0001B944
		public stringWithEncodingType()
		{
			this.encodingField = "0";
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0001D757 File Offset: 0x0001B957
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0001D75F File Offset: 0x0001B95F
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

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001D768 File Offset: 0x0001B968
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0001D770 File Offset: 0x0001B970
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

		// Token: 0x040005E2 RID: 1506
		private string encodingField;

		// Token: 0x040005E3 RID: 1507
		private string valueField;
	}
}
