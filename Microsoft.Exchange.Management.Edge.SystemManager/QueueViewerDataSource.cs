using System;
using System.Data;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.Tasks;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000004 RID: 4
	public class QueueViewerDataSource : TaskDataSource
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public QueueViewerDataSource(string noun) : base(noun)
		{
			base.RefreshCommandText = Strings.RefreshLabelText;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002BBC File Offset: 0x00000DBC
		protected override void OnDoRefreshWork(RefreshRequestEventArgs e)
		{
			MonadCommand monadCommand = e.Argument as MonadCommand;
			if (monadCommand == null || monadCommand.Parameters.Contains("Identity") || (monadCommand.Parameters.Contains("server") && !string.IsNullOrEmpty((string)monadCommand.Parameters["server"].Value)))
			{
				base.OnDoRefreshWork(e);
				return;
			}
			DataTable result = base.Table.Clone();
			e.Result = result;
			e.ReportProgress(100, 100, "", null);
		}

		// Token: 0x04000009 RID: 9
		internal const string ServerParameter = "server";
	}
}
