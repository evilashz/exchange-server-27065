using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008AF RID: 2223
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlRoot(Namespace = "HMSETTINGS:", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Settings
	{
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x0006C78A File Offset: 0x0006A98A
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x0006C792 File Offset: 0x0006A992
		public ServiceSettingsType ServiceSettings
		{
			get
			{
				return this.serviceSettingsField;
			}
			set
			{
				this.serviceSettingsField = value;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x0006C79B File Offset: 0x0006A99B
		// (set) Token: 0x06002FAE RID: 12206 RVA: 0x0006C7A3 File Offset: 0x0006A9A3
		public AccountSettingsType AccountSettings
		{
			get
			{
				return this.accountSettingsField;
			}
			set
			{
				this.accountSettingsField = value;
			}
		}

		// Token: 0x04002941 RID: 10561
		private ServiceSettingsType serviceSettingsField;

		// Token: 0x04002942 RID: 10562
		private AccountSettingsType accountSettingsField;
	}
}
