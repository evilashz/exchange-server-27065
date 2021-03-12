using System;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000009 RID: 9
	internal class MessagesTaskCommandAction : QueueViewerTaskCommandAction
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00004110 File Offset: 0x00002310
		protected override void OnInitialize()
		{
			base.OnInitialize();
			QueueViewerMessagesResultPane queueViewerMessagesResultPane = base.DataListViewResultPane as QueueViewerMessagesResultPane;
			base.RefreshOnFinish = queueViewerMessagesResultPane.CreateRefreshableObject(new object[]
			{
				RefreshCategories.Message
			});
		}
	}
}
