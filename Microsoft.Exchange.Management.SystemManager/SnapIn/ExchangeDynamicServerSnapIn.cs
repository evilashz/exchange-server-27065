using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200028D RID: 653
	public abstract class ExchangeDynamicServerSnapIn : ExchangeSnapIn
	{
		// Token: 0x06001BAB RID: 7083 RVA: 0x000792FC File Offset: 0x000774FC
		public ExchangeDynamicServerSnapIn()
		{
			PSConnectionInfoSingleton.GetInstance().RemotePSServerChanged += delegate(object param0, EventArgs param1)
			{
				this.UpdateRemotePSServerSettings();
			};
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0007932C File Offset: 0x0007752C
		private void UpdateRemotePSServerSettings()
		{
			bool cancelAutoRefresh = true;
			try
			{
				this.Settings.DoBeginInit();
				this.Settings.RemotePSServer = PSConnectionInfoSingleton.GetInstance().RemotePSServer;
				cancelAutoRefresh = false;
			}
			finally
			{
				this.Settings.DoEndInit(cancelAutoRefresh);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0007937C File Offset: 0x0007757C
		public new ExchangeDynamicServerSettings Settings
		{
			get
			{
				return (ExchangeDynamicServerSettings)base.Settings;
			}
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00079389 File Offset: 0x00077589
		public override ExchangeSettings CreateSettings(IComponent owner)
		{
			return SettingsBase.Synchronized(new ExchangeDynamicServerSettings(owner)) as ExchangeSettings;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0007939C File Offset: 0x0007759C
		public override void Initialize(IProgressProvider progressProvider)
		{
			PSConnectionInfoSingleton.GetInstance().DisplayName = this.RootNodeDisplayName;
			PSConnectionInfoSingleton.GetInstance().Type = OrganizationType.ToolOrEdge;
			PSConnectionInfoSingleton.GetInstance().Uri = PSConnectionInfoSingleton.GetRemotePowerShellUri(this.Settings.RemotePSServer);
			PSConnectionInfoSingleton.GetInstance().LogonWithDefaultCredential = true;
			PSConnectionInfoSingleton.GetInstance().Enabled = true;
			base.Initialize(progressProvider);
		}
	}
}
