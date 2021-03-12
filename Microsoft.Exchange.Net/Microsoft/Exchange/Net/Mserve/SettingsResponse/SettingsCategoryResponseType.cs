using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D8 RID: 2264
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[Serializable]
	public class SettingsCategoryResponseType
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x00072F38 File Offset: 0x00071138
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x00072F40 File Offset: 0x00071140
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

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x00072F49 File Offset: 0x00071149
		// (set) Token: 0x060030A4 RID: 12452 RVA: 0x00072F51 File Offset: 0x00071151
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

		// Token: 0x04002A03 RID: 10755
		private int statusField;

		// Token: 0x04002A04 RID: 10756
		private Fault faultField;
	}
}
