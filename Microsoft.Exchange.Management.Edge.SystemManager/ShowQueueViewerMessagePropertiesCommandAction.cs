using System;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000007 RID: 7
	internal class ShowQueueViewerMessagePropertiesCommandAction : ShowSelectionPropertiesCommandAction
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003F0C File Offset: 0x0000210C
		protected override ExchangePropertyPageControl[] OnGetSingleSelectionPropertyPageControls()
		{
			QueueViewerMessagesResultPane queueViewerMessagesResultPane = base.ResultPane as QueueViewerMessagesResultPane;
			MonadDataHandler monadDataHandler = new MonadDataHandler(queueViewerMessagesResultPane.SelectedIdentity.ToString(), "get-message", "");
			monadDataHandler.SelectCommand.Parameters.AddWithValue("IncludeRecipientInfo", true);
			DataContext context = new DataContext(monadDataHandler);
			return new ExchangePropertyPageControl[]
			{
				new MessagePropertyPage
				{
					Context = context
				},
				new RecipientsInfoPropertyPage
				{
					Context = context
				}
			};
		}
	}
}
