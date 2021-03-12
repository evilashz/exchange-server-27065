using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E4 RID: 2276
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[Serializable]
	public class OptionsTypeMailForwarding
	{
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000731AB File Offset: 0x000713AB
		// (set) Token: 0x060030EC RID: 12524 RVA: 0x000731B3 File Offset: 0x000713B3
		public ForwardingMode Mode
		{
			get
			{
				return this.modeField;
			}
			set
			{
				this.modeField = value;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000731BC File Offset: 0x000713BC
		// (set) Token: 0x060030EE RID: 12526 RVA: 0x000731C4 File Offset: 0x000713C4
		[XmlArrayItem("Address", IsNullable = false)]
		public string[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		// Token: 0x04002A47 RID: 10823
		private ForwardingMode modeField;

		// Token: 0x04002A48 RID: 10824
		private string[] addressesField;
	}
}
