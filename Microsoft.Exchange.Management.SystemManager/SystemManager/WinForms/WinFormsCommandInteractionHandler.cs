using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000186 RID: 390
	internal sealed class WinFormsCommandInteractionHandler : CommandInteractionHandler
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0003B22A File Offset: 0x0003942A
		internal IUIService UIService
		{
			get
			{
				return this.uiService;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003B232 File Offset: 0x00039432
		public WinFormsCommandInteractionHandler(IUIService uiService)
		{
			if (uiService == null)
			{
				throw new ArgumentNullException("uiService");
			}
			this.uiService = uiService;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003B250 File Offset: 0x00039450
		public override ConfirmationChoice ShowConfirmationDialog(string message, ConfirmationChoice defaultChoice)
		{
			Control control = this.uiService.GetDialogOwnerWindow() as Control;
			if (control != null && control.InvokeRequired)
			{
				return (ConfirmationChoice)control.Invoke(new WinFormsCommandInteractionHandler.ShowConfirmationDialogDelegate(this.ShowConfirmationDialog), new object[]
				{
					message,
					defaultChoice
				});
			}
			ConfirmationChoice userChoice;
			using (PromptForChoicesDialog promptForChoicesDialog = new PromptForChoicesDialog(message, defaultChoice))
			{
				this.uiService.ShowDialog(promptForChoicesDialog);
				userChoice = promptForChoicesDialog.UserChoice;
			}
			return userChoice;
		}

		// Token: 0x04000609 RID: 1545
		private IUIService uiService;

		// Token: 0x02000187 RID: 391
		// (Invoke) Token: 0x06000F4D RID: 3917
		private delegate ConfirmationChoice ShowConfirmationDialogDelegate(string message, ConfirmationChoice defaultChoice);
	}
}
