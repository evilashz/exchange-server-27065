using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000017 RID: 23
	internal class ShowDetailsTemplatesProperitiesAction : ResultsCommandAction
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000053FC File Offset: 0x000035FC
		protected override void OnExecute()
		{
			base.OnExecute();
			DetailsTemplatesResultPane detailsTemplatesResultPane = base.ResultPane as DetailsTemplatesResultPane;
			string templateIdentity = detailsTemplatesResultPane.SelectedIdentity.ToString();
			string text = WinformsHelper.GenerateFormName<DetailsTemplatesEditor>((ADObjectId)detailsTemplatesResultPane.SelectedIdentity);
			if (!ExchangeForm.ActivateSingleInstanceForm(text))
			{
				DetailsTemplatesEditor detailsTemplatesEditor = new DetailsTemplatesEditor(templateIdentity);
				detailsTemplatesEditor.Icon = Icons.DetailsTemplate;
				detailsTemplatesEditor.Name = text;
				detailsTemplatesEditor.RefreshOnFinish = detailsTemplatesResultPane.GetSelectionRefreshObjects();
				detailsTemplatesEditor.PrivateSettings = (detailsTemplatesResultPane.PrivateSettings as DetailsTemplatesEditorSettings);
				detailsTemplatesEditor.ShowModeless(detailsTemplatesResultPane);
				detailsTemplatesEditor.HelpTopic = detailsTemplatesResultPane.SelectionHelpTopic;
			}
		}
	}
}
