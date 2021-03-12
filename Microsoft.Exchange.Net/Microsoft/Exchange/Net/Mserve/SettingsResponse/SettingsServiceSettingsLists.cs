using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008EE RID: 2286
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class SettingsServiceSettingsLists
	{
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x0007348A File Offset: 0x0007168A
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x00073492 File Offset: 0x00071692
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

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x0007349B File Offset: 0x0007169B
		// (set) Token: 0x06003145 RID: 12613 RVA: 0x000734A3 File Offset: 0x000716A3
		public SettingsServiceSettingsListsGet Get
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

		// Token: 0x04002A72 RID: 10866
		private int statusField;

		// Token: 0x04002A73 RID: 10867
		private SettingsServiceSettingsListsGet getField;
	}
}
