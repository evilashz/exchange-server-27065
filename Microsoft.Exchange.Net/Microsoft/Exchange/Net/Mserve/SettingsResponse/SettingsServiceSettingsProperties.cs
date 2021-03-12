using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008ED RID: 2285
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SettingsServiceSettingsProperties
	{
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x00073460 File Offset: 0x00071660
		// (set) Token: 0x0600313E RID: 12606 RVA: 0x00073468 File Offset: 0x00071668
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

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x00073471 File Offset: 0x00071671
		// (set) Token: 0x06003140 RID: 12608 RVA: 0x00073479 File Offset: 0x00071679
		public ServiceSettingsPropertiesType Get
		{
			get
			{
				return this.getField;
			}
			set
			{
				this.getField = value;
			}
		}

		// Token: 0x04002A70 RID: 10864
		private int statusField;

		// Token: 0x04002A71 RID: 10865
		private ServiceSettingsPropertiesType getField;
	}
}
