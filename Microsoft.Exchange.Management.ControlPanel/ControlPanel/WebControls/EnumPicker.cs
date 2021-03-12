using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005D7 RID: 1495
	[ClientScriptResource("EnumPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ControlValueProperty("Value")]
	[ToolboxData("<{0}:EnumPicker runat=server></{0}:EnumPicker>")]
	public class EnumPicker : ScriptControlBase, INamingContainer
	{
		// Token: 0x0600435D RID: 17245 RVA: 0x000CC044 File Offset: 0x000CA244
		public EnumPicker() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x000CC04E File Offset: 0x000CA24E
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("DropDownList", this.DropDownListID, this);
			descriptor.AddElementProperty("DescriptionLabel", this.DescriptionLabelID, this);
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x000CC07C File Offset: 0x000CA27C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CssClass = "EnumPicker";
			DropDownList dropDownList = new DropDownList();
			dropDownList.ID = "bugfix_dropdownhiddenlist";
			dropDownList.Attributes["aria-label"] = "none";
			dropDownList.Style.Add(HtmlTextWriterStyle.Display, "none");
			dropDownList.Items.Add("hidden item");
			this.Controls.Add(dropDownList);
			this.dropDownList = new DropDownList();
			this.dropDownList.ID = "dropDownList";
			this.dropDownList.CssClass = "EnumPickerSelect";
			this.descriptionLabel = new Label();
			this.descriptionLabel.ID = "dropDownList_Description_label";
			this.descriptionLabel.CssClass = "EnumPickerDescription";
			this.Controls.Add(this.descriptionLabel);
			this.Controls.Add(this.dropDownList);
		}

		// Token: 0x17002623 RID: 9763
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x000CC166 File Offset: 0x000CA366
		public string DropDownListID
		{
			get
			{
				this.EnsureChildControls();
				return this.dropDownList.ClientID;
			}
		}

		// Token: 0x17002624 RID: 9764
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x000CC179 File Offset: 0x000CA379
		public string DescriptionLabelID
		{
			get
			{
				this.EnsureChildControls();
				return this.descriptionLabel.ClientID;
			}
		}

		// Token: 0x04002D9B RID: 11675
		private DropDownList dropDownList;

		// Token: 0x04002D9C RID: 11676
		private Label descriptionLabel;
	}
}
