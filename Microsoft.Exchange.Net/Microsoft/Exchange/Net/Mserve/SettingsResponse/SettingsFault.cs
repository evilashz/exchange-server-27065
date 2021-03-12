using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008EA RID: 2282
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[Serializable]
	public class SettingsFault
	{
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x0007339E File Offset: 0x0007159E
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x000733A6 File Offset: 0x000715A6
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

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000733AF File Offset: 0x000715AF
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x000733B7 File Offset: 0x000715B7
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

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000733C0 File Offset: 0x000715C0
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x000733C8 File Offset: 0x000715C8
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

		// Token: 0x04002A66 RID: 10854
		private string faultcodeField;

		// Token: 0x04002A67 RID: 10855
		private string faultstringField;

		// Token: 0x04002A68 RID: 10856
		private string detailField;
	}
}
