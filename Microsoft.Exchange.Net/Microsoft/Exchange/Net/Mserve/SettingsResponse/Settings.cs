using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E9 RID: 2281
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "HMSETTINGS:", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Settings
	{
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x00073341 File Offset: 0x00071541
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x00073349 File Offset: 0x00071549
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

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x00073352 File Offset: 0x00071552
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x0007335A File Offset: 0x0007155A
		public SettingsFault Fault
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

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x00073363 File Offset: 0x00071563
		// (set) Token: 0x06003120 RID: 12576 RVA: 0x0007336B File Offset: 0x0007156B
		public SettingsAuthPolicy AuthPolicy
		{
			get
			{
				return this.authPolicyField;
			}
			set
			{
				this.authPolicyField = value;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x00073374 File Offset: 0x00071574
		// (set) Token: 0x06003122 RID: 12578 RVA: 0x0007337C File Offset: 0x0007157C
		public SettingsServiceSettings ServiceSettings
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

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x00073385 File Offset: 0x00071585
		// (set) Token: 0x06003124 RID: 12580 RVA: 0x0007338D File Offset: 0x0007158D
		public SettingsAccountSettings AccountSettings
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

		// Token: 0x04002A61 RID: 10849
		private int statusField;

		// Token: 0x04002A62 RID: 10850
		private SettingsFault faultField;

		// Token: 0x04002A63 RID: 10851
		private SettingsAuthPolicy authPolicyField;

		// Token: 0x04002A64 RID: 10852
		private SettingsServiceSettings serviceSettingsField;

		// Token: 0x04002A65 RID: 10853
		private SettingsAccountSettings accountSettingsField;
	}
}
