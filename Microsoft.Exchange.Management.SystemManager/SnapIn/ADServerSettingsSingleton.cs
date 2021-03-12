using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200026F RID: 623
	internal class ADServerSettingsSingleton
	{
		// Token: 0x06001AB9 RID: 6841 RVA: 0x00075E40 File Offset: 0x00074040
		protected ADServerSettingsSingleton()
		{
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00075E48 File Offset: 0x00074048
		public static void DisposeCurrentInstance()
		{
			if (ADServerSettingsSingleton.instance != null)
			{
				ADServerSettingsSingleton.instance = null;
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00075E57 File Offset: 0x00074057
		public static ADServerSettingsSingleton GetInstance()
		{
			if (ADServerSettingsSingleton.instance == null)
			{
				ADServerSettingsSingleton.instance = new ADServerSettingsSingleton();
			}
			return ADServerSettingsSingleton.instance;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00075E6F File Offset: 0x0007406F
		public RunspaceServerSettingsPresentationObject CreateRunspaceServerSettingsObject()
		{
			if (this.ADServerSettings == null)
			{
				return null;
			}
			return this.ADServerSettings.CreateRunspaceServerSettingsObject();
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00075E86 File Offset: 0x00074086
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x00075E8E File Offset: 0x0007408E
		public ExchangeADServerSettings ADServerSettings
		{
			get
			{
				return this.adServerSettings;
			}
			internal set
			{
				this.adServerSettings = value;
			}
		}

		// Token: 0x040009EE RID: 2542
		private static ADServerSettingsSingleton instance;

		// Token: 0x040009EF RID: 2543
		private ExchangeADServerSettings adServerSettings;
	}
}
