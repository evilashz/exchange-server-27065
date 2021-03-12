using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200068D RID: 1677
	[ToolboxData("<{0}:KeyMappingsPicker runat=server></{0}:KeyMappingsPicker>")]
	[ClientScriptResource("KeyMappingsPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class KeyMappingsPicker : EcpCollectionEditor
	{
		// Token: 0x170027B6 RID: 10166
		// (get) Token: 0x06004856 RID: 18518 RVA: 0x000DC63B File Offset: 0x000DA83B
		public DropDownCommand AddDropDownCommand
		{
			get
			{
				return this.addDropDownCommand;
			}
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x000DC644 File Offset: 0x000DA844
		public KeyMappingsPicker()
		{
			this.ID = "ecpCollectionEditor";
			this.Height = Unit.Pixel(200);
			this.Width = Unit.Pixel(300);
			base.ValueProperty = null;
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Name = "KeyForDisplay";
			columnHeader.Text = Strings.KeyMappingKeyColumnText;
			columnHeader.Width = Unit.Pixel(75);
			ColumnHeader columnHeader2 = new ColumnHeader();
			columnHeader2.Name = "KeyMappingTypeDisplay";
			columnHeader2.Text = Strings.KeyMappingToDoColumnText;
			columnHeader2.Width = Unit.Pixel(200);
			base.Columns.Add(columnHeader);
			base.Columns.Add(columnHeader2);
			base.DialogHeight = 270;
			base.DialogWidth = 670;
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x000DC721 File Offset: 0x000DA921
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x000DC72C File Offset: 0x000DA92C
		protected override void InitListviewCommandCollection()
		{
			if (!base.ReadOnly)
			{
				string[] roles = (!string.IsNullOrEmpty(base.Attributes["SetRoles"])) ? base.Attributes["SetRoles"].ToArrayOfStrings() : null;
				base.Listview.Commands.Add(new Command
				{
					Name = "AddFindMe",
					Text = Strings.KeyMappingFindMeCommandText,
					OnClientClick = "$find('" + this.ClientID + "').addFindMeOptionCommand();"
				});
				base.Listview.Commands.Add(new Command
				{
					Name = "CallTransfer",
					Text = Strings.KeyMappingCallTransferCommand,
					OnClientClick = "$find('" + this.ClientID + "').addCallTransferOptionCommand();"
				});
				base.Listview.Commands.Add(this.addDropDownCommand);
				base.RemoveCommand.Roles = roles;
				base.Listview.Commands.Add(base.RemoveCommand);
			}
		}

		// Token: 0x04003090 RID: 12432
		private DropDownCommand addDropDownCommand = new DropDownCommand();
	}
}
