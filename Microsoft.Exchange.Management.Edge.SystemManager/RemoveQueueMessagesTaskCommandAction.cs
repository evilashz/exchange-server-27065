using System;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x0200000D RID: 13
	internal class RemoveQueueMessagesTaskCommandAction : ResultsTaskCommandAction
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000056E0 File Offset: 0x000038E0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			QueueViewerQueuesResultPane queueViewerQueuesResultPane = base.DataListViewResultPane as QueueViewerQueuesResultPane;
			base.RefreshOnFinish = queueViewerQueuesResultPane.CreateRefreshableObject(new object[]
			{
				RefreshCategories.Message
			});
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000571C File Offset: 0x0000391C
		protected override bool ConfirmOperation(WorkUnitCollectionEventArgs inputArgs)
		{
			QueueViewerQueuesResultPane queueViewerQueuesResultPane = base.DataListViewResultPane as QueueViewerQueuesResultPane;
			QueueIdentity queueIdentity = QueueIdentity.Parse(queueViewerQueuesResultPane.SelectedIdentity.ToString());
			string message = base.SingleSelectionConfirmation(queueIdentity.ToString());
			bool flag = DialogResult.Yes == queueViewerQueuesResultPane.ShowMessage(message, MessageBoxButtons.YesNo);
			if (flag)
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ExtensibleMessageInfoSchema.Queue, queueIdentity);
				base.Parameters.Remove("Filter");
				base.Parameters.AddWithValue("Filter", queryFilter.GenerateInfixString(FilterLanguage.Monad));
				base.Parameters.Remove("Server");
				base.Parameters.AddWithValue("Server", queueViewerQueuesResultPane.ServerName);
			}
			return flag;
		}
	}
}
