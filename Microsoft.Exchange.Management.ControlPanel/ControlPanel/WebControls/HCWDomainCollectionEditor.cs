using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005EB RID: 1515
	[ClientScriptResource("HCWDomainCollectionEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:HCWDomainCollectionEditor runat=server></{0}:HCWDomainCollectionEditor>")]
	public class HCWDomainCollectionEditor : EcpCollectionEditor
	{
		// Token: 0x0600440B RID: 17419 RVA: 0x000CDE4C File Offset: 0x000CC04C
		protected override void InitListviewCommandCollection()
		{
			base.InitListviewCommandCollection();
			this.setAutodiscoverDomainCommand = new Command(string.Empty, CommandSprite.SpriteId.AutodiscoverDomain);
			this.setAutodiscoverDomainCommand.Name = "SetAutodiscoverDomain";
			this.setAutodiscoverDomainCommand.ImageAltText = Strings.SetAutodiscoverDomain;
			this.setAutodiscoverDomainCommand.OnClientClick = "HCWDomainCollectionEditor.SetAutodiscoverDomain";
			this.setAutodiscoverDomainCommand.Condition = "!HCWDomainCollectionEditor.IsAutodiscoverDomain($_)";
			this.setAutodiscoverDomainCommand.SelectionMode = SelectionMode.RequiresSingleSelection;
			base.Listview.Commands.Add(this.setAutodiscoverDomainCommand);
		}

		// Token: 0x04002DD5 RID: 11733
		private Command setAutodiscoverDomainCommand;
	}
}
