using System;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200002A RID: 42
	public class AcceptedDomainsSlab : SlabControl
	{
		// Token: 0x060018DE RID: 6366 RVA: 0x0004E2D8 File Offset: 0x0004C4D8
		protected override void OnLoad(EventArgs e)
		{
			string[] roles = new string[]
			{
				"FFO"
			};
			ListView listView = (ListView)this.FindControl("AcceptedDomainsListView");
			if (LoginUtil.IsInRoles(HttpContext.Current.User, roles))
			{
				listView.ShowSearchBar = false;
				return;
			}
			listView.ShowSearchBar = true;
		}
	}
}
