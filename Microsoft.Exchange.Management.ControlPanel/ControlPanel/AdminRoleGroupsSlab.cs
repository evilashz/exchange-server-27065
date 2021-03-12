using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E0 RID: 1248
	public class AdminRoleGroupsSlab : SlabControl
	{
		// Token: 0x06003CF0 RID: 15600 RVA: 0x000B6F5B File Offset: 0x000B515B
		protected override void OnLoad(EventArgs e)
		{
			if (RbacPrincipal.Current.IsInRole("FFO"))
			{
				this.roleGroupsResultPane.ShowSearchBar = false;
			}
			base.OnLoad(e);
		}

		// Token: 0x040027D1 RID: 10193
		protected ListView roleGroupsResultPane;
	}
}
