using System;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000265 RID: 613
	public class NewRetentionPolicyTag : BaseForm
	{
		// Token: 0x06002940 RID: 10560 RVA: 0x00081A94 File Offset: 0x0007FC94
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			bool includeArchive = true;
			this.InitializeControls();
			NameValueCollection queryString = this.Context.Request.QueryString;
			string text = queryString["typeGroup"];
			if (!string.IsNullOrEmpty(text))
			{
				this.divType.Visible = false;
				string text2;
				if (string.Compare(text, "SystemFolder", true) == 0)
				{
					text2 = Strings.NewRetentionTagForSystemFolder;
					this.tbxTypeHidden.Text = string.Empty;
					this.divType.Visible = true;
					this.tbxTypeHidden.Visible = false;
					RetentionUtils.PopulateRetentionTypes(this.ddlType);
					includeArchive = false;
				}
				else if (string.Compare(text, "All", true) == 0)
				{
					text2 = Strings.NewRetentionTagForAll;
					this.tbxTypeHidden.Text = "All";
				}
				else
				{
					if (string.Compare(text, "Personal", true) != 0)
					{
						throw new BadQueryParameterException("typeGroup", new ArgumentException(string.Format("Retention tag type group [{0}] is not supported.", text)));
					}
					text2 = Strings.NewRetentionTagForPersonal;
					this.tbxTypeHidden.Text = "Personal";
				}
				base.Title = text2;
				base.Caption = text2;
				RetentionUtils.PopulateRetentionActions(this.rblRetentionAction, includeArchive);
				return;
			}
			throw new BadQueryParameterException("typeGroup", new ArgumentException("Retention tag type group is required."));
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x00081BD8 File Offset: 0x0007FDD8
		private void InitializeControls()
		{
			PropertyPageSheet propertyPageSheet = (PropertyPageSheet)base.ContentPanel.FindControl("RetentionPolicyTagProperties");
			Section section = (Section)propertyPageSheet.FindControl("GroupInformationSection");
			this.divType = (HtmlControl)section.FindControl("divType");
			this.ddlType = (DropDownList)section.FindControl("ddlType");
			this.tbxTypeHidden = (TextBox)section.FindControl("tbxTypeHidden");
			this.rblRetentionAction = (RadioButtonList)section.FindControl("rblRetentionAction");
		}

		// Token: 0x040020C3 RID: 8387
		private const string TypeGroupAll = "All";

		// Token: 0x040020C4 RID: 8388
		private const string TypeGroupSystemFolder = "SystemFolder";

		// Token: 0x040020C5 RID: 8389
		private const string TypeGroupPersonal = "Personal";

		// Token: 0x040020C6 RID: 8390
		private const string PropertyID = "RetentionPolicyTagProperties";

		// Token: 0x040020C7 RID: 8391
		private const string SectionID = "GroupInformationSection";

		// Token: 0x040020C8 RID: 8392
		private const string TypePanelID = "divType";

		// Token: 0x040020C9 RID: 8393
		private const string TypeID = "ddlType";

		// Token: 0x040020CA RID: 8394
		private const string TypeGroupID = "tbxTypeHidden";

		// Token: 0x040020CB RID: 8395
		private const string RetentionActionID = "rblRetentionAction";

		// Token: 0x040020CC RID: 8396
		private HtmlControl divType;

		// Token: 0x040020CD RID: 8397
		private DropDownList ddlType;

		// Token: 0x040020CE RID: 8398
		private TextBox tbxTypeHidden;

		// Token: 0x040020CF RID: 8399
		private RadioButtonList rblRetentionAction;
	}
}
