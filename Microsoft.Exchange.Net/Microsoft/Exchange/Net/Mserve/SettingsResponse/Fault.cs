using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008D3 RID: 2259
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlRoot(Namespace = "HMSETTINGS:", IsNullable = false)]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Fault
	{
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x00072E6E File Offset: 0x0007106E
		// (set) Token: 0x0600308A RID: 12426 RVA: 0x00072E76 File Offset: 0x00071076
		public string Faultcode
		{
			get
			{
				return this.faultcodeField;
			}
			set
			{
				this.faultcodeField = value;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x00072E7F File Offset: 0x0007107F
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x00072E87 File Offset: 0x00071087
		public string Faultstring
		{
			get
			{
				return this.faultstringField;
			}
			set
			{
				this.faultstringField = value;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x00072E90 File Offset: 0x00071090
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x00072E98 File Offset: 0x00071098
		public string Detail
		{
			get
			{
				return this.detailField;
			}
			set
			{
				this.detailField = value;
			}
		}

		// Token: 0x040029F5 RID: 10741
		private string faultcodeField;

		// Token: 0x040029F6 RID: 10742
		private string faultstringField;

		// Token: 0x040029F7 RID: 10743
		private string detailField;
	}
}
