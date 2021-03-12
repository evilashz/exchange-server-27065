using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008C9 RID: 2249
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public class OptionsTypeMailForwarding
	{
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0006CB7E File Offset: 0x0006AD7E
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x0006CB86 File Offset: 0x0006AD86
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

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003025 RID: 12325 RVA: 0x0006CB8F File Offset: 0x0006AD8F
		// (set) Token: 0x06003026 RID: 12326 RVA: 0x0006CB97 File Offset: 0x0006AD97
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

		// Token: 0x040029A0 RID: 10656
		private ForwardingMode modeField;

		// Token: 0x040029A1 RID: 10657
		private string[] addressesField;
	}
}
