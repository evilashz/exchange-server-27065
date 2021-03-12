using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008EB RID: 2283
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SettingsAuthPolicy
	{
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000733D9 File Offset: 0x000715D9
		// (set) Token: 0x0600312E RID: 12590 RVA: 0x000733E1 File Offset: 0x000715E1
		public string SAP
		{
			get
			{
				return this.sAPField;
			}
			set
			{
				this.sAPField = value;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000733EA File Offset: 0x000715EA
		// (set) Token: 0x06003130 RID: 12592 RVA: 0x000733F2 File Offset: 0x000715F2
		public string Version
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

		// Token: 0x04002A69 RID: 10857
		private string sAPField;

		// Token: 0x04002A6A RID: 10858
		private string versionField;
	}
}
