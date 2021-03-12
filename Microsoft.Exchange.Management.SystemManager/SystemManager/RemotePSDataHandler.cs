using System;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000051 RID: 81
	internal class RemotePSDataHandler : ExchangeDataHandler
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000B90C File Offset: 0x00009B0C
		public RemotePSDataHandler(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B91C File Offset: 0x00009B1C
		internal override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo();
			this.mockSettings = new PSRemoteServer();
			this.mockSettings.DisplayName = this.displayName;
			this.mockSettings.UserAccount = PSConnectionInfoSingleton.GetInstance().UserAccount;
			this.mockSettings.RemotePSServer = PSConnectionInfoSingleton.GetInstance().RemotePSServer;
			this.mockSettings.AutomaticallySelect = (this.mockSettings.RemotePSServer == null);
			base.DataSource = this.mockSettings;
			base.OnReadData(interactionHandler, pageName);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		internal override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			if (this.mockSettings.ObjectState == ObjectState.Changed)
			{
				PSConnectionInfoSingleton.GetInstance().UpdateRemotePSServer(this.mockSettings.AutomaticallySelect ? null : this.mockSettings.RemotePSServer);
			}
			base.OnSaveData(interactionHandler);
		}

		// Token: 0x040000DB RID: 219
		private PSRemoteServer mockSettings;

		// Token: 0x040000DC RID: 220
		private string displayName;
	}
}
