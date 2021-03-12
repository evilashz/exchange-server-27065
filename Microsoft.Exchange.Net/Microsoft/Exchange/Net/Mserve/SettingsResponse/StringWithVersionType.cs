using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E6 RID: 2278
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public class StringWithVersionType
	{
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000731D5 File Offset: 0x000713D5
		// (set) Token: 0x060030F1 RID: 12529 RVA: 0x000731DD File Offset: 0x000713DD
		[XmlAttribute]
		public int version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000731E6 File Offset: 0x000713E6
		// (set) Token: 0x060030F3 RID: 12531 RVA: 0x000731EE File Offset: 0x000713EE
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

		// Token: 0x04002A4D RID: 10829
		private int versionField;

		// Token: 0x04002A4E RID: 10830
		private string valueField;
	}
}
