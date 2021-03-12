using System;
using System.Data;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000E5 RID: 229
	internal class RemoteMonadCommandExecutionContext : MonadCommandExecutionContext
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D1B5 File Offset: 0x0001B3B5
		public override void Open(IUIService service)
		{
			this.uiService = service;
			this.commandInteractionHandler = ((service != null) ? new WinFormsCommandInteractionHandler(service) : new CommandInteractionHandler());
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		private void ConnectTo(string targetForest)
		{
			this.connection = new MonadConnection("timeout=30", this.commandInteractionHandler, null, PSConnectionInfoSingleton.GetInstance().GetMonadConnectionInfo(this.uiService, ConnectedForestsInfoSingleton.GetInstance().ForestInfoOf(targetForest)));
			this.connection.Open();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001D213 File Offset: 0x0001B413
		public override void Execute(TaskProfileBase profile, DataRow row, DataObjectStore store)
		{
			this.ConnectTo((string)row["TargetForest"]);
			base.Execute(profile, row, store);
		}

		// Token: 0x040003EE RID: 1006
		private IUIService uiService;
	}
}
