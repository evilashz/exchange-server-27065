using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008BB RID: 2235
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public class StringWithCharSetType
	{
		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x0006C9AE File Offset: 0x0006ABAE
		// (set) Token: 0x06002FED RID: 12269 RVA: 0x0006C9B6 File Offset: 0x0006ABB6
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

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x0006C9BF File Offset: 0x0006ABBF
		// (set) Token: 0x06002FEF RID: 12271 RVA: 0x0006C9C7 File Offset: 0x0006ABC7
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

		// Token: 0x0400296C RID: 10604
		private string charsetField;

		// Token: 0x0400296D RID: 10605
		private string valueField;
	}
}
