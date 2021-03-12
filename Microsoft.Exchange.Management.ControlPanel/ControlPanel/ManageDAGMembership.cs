using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000107 RID: 263
	public class ManageDAGMembership : BaseForm
	{
		// Token: 0x06001F8B RID: 8075 RVA: 0x0005F0B8 File Offset: 0x0005D2B8
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			string text = base.Request.QueryString["dagManageType"];
			if (text != null && text.Equals("AddOnly", StringComparison.OrdinalIgnoreCase))
			{
				this.ceServers.DisableRemove = true;
				base.Title = Strings.AddDAGMembershipTitle;
				this.ceServers_label.Text = Strings.DAGMembershipAddServers;
				base.Caption = Strings.AddDAGMembershipCaption;
			}
		}

		// Token: 0x04001C78 RID: 7288
		private const string DagManageTypeKey = "dagManageType";

		// Token: 0x04001C79 RID: 7289
		private const string AddOnly = "AddOnly";

		// Token: 0x04001C7A RID: 7290
		protected EcpCollectionEditor ceServers;

		// Token: 0x04001C7B RID: 7291
		protected Label ceServers_label;
	}
}
