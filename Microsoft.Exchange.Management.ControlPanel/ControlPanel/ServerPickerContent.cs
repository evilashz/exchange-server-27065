using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200035A RID: 858
	public class ServerPickerContent : PickerContent
	{
		// Token: 0x06002FC2 RID: 12226 RVA: 0x000916F4 File Offset: 0x0008F8F4
		protected override void CreateChildControls()
		{
			string text = this.Page.Request.QueryString["workflow"];
			if (!string.IsNullOrEmpty(text))
			{
				base.ServiceUrl = new WebServiceReference(string.Format("{0}&{1}={2}", base.ServiceUrl.ServiceUrl, "workflow", text));
			}
			base.CreateChildControls();
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00091750 File Offset: 0x0008F950
		protected override void OnPreRender(EventArgs e)
		{
			string text = this.Page.Request.QueryString["workflow"];
			if (!string.IsNullOrEmpty(text))
			{
				string a;
				if ((a = text) != null)
				{
					if (a == "GetNameAndRole")
					{
						base.ListView.Columns.Clear();
						base.ListView.Columns.Add(this.CreateNameColumn(30));
						base.ListView.Columns.Add(this.CreateServerRoleColumn(70));
						goto IL_DC;
					}
					if (a == "GetEdgeServer")
					{
						base.ListView.Columns.Clear();
						base.ListView.Columns.Add(this.CreateNameColumn(40));
						base.ListView.Columns.Add(this.CreateAdminDisplayVersionColumn(60));
						goto IL_DC;
					}
				}
				throw new BadQueryParameterException("workflow");
			}
			IL_DC:
			base.OnPreRender(e);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x00091840 File Offset: 0x0008FA40
		private ColumnHeader CreateNameColumn(int percentage)
		{
			return new ColumnHeader
			{
				Name = "Name",
				Text = Strings.Name,
				Width = Unit.Percentage((double)percentage)
			};
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0009187C File Offset: 0x0008FA7C
		private ColumnHeader CreateServerRoleColumn(int percentage)
		{
			return new ColumnHeader
			{
				Name = "ServerRole",
				Text = Strings.ServerPickerRole,
				Width = Unit.Percentage((double)percentage)
			};
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000918B8 File Offset: 0x0008FAB8
		private ColumnHeader CreateAdminDisplayVersionColumn(int percentage)
		{
			return new ColumnHeader
			{
				Name = "AdminDisplayVersion",
				Text = Strings.ServerPickerVersion,
				Width = Unit.Percentage((double)percentage)
			};
		}

		// Token: 0x0400231B RID: 8987
		private const string WorkflowQueryString = "workflow";
	}
}
