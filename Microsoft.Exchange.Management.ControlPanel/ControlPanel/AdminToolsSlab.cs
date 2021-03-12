using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000296 RID: 662
	public class AdminToolsSlab : SlabControl
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x000869A4 File Offset: 0x00084BA4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.pnlRemotePowerShell.Visible = RbacPrincipal.Current.RbacConfiguration.ExecutingUserIsAllowedRemotePowerShell;
		}

		// Token: 0x0400217C RID: 8572
		protected Panel pnlRemotePowerShell;
	}
}
