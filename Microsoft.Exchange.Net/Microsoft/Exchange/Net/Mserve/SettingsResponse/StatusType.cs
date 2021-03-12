using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D4 RID: 2260
	[XmlType(Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class StatusType
	{
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x00072EA9 File Offset: 0x000710A9
		// (set) Token: 0x06003091 RID: 12433 RVA: 0x00072EB1 File Offset: 0x000710B1
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x00072EBA File Offset: 0x000710BA
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x00072EC2 File Offset: 0x000710C2
		public Fault Fault
		{
			get
			{
				return this.faultField;
			}
			set
			{
				this.faultField = value;
			}
		}

		// Token: 0x040029F8 RID: 10744
		private int statusField;

		// Token: 0x040029F9 RID: 10745
		private Fault faultField;
	}
}
