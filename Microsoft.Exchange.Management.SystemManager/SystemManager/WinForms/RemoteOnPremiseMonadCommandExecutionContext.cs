using System;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E6 RID: 230
	internal class RemoteOnPremiseMonadCommandExecutionContext : MonadCommandExecutionContext
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0001D23C File Offset: 0x0001B43C
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0001D244 File Offset: 0x0001B444
		public string ServerName { get; set; }

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001D250 File Offset: 0x0001B450
		protected override MonadConnection CreateMonadConnection(IUIService uiService, CommandInteractionHandler commandInteractionHandler)
		{
			MonadConnectionInfo monadConnectionInfo = PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo();
			return new MonadConnection("timeout=30", commandInteractionHandler, null, new MonadConnectionInfo(PSConnectionInfoSingleton.GetRemotePowerShellUri(new Fqdn(this.ServerName)), monadConnectionInfo.Credentials, monadConnectionInfo.ShellUri, monadConnectionInfo.FileTypesXml, monadConnectionInfo.AuthenticationMechanism, monadConnectionInfo.SerializationLevel, monadConnectionInfo.ClientApplication, string.Empty, monadConnectionInfo.MaximumConnectionRedirectionCount, monadConnectionInfo.SkipServerCertificateCheck));
		}
	}
}
