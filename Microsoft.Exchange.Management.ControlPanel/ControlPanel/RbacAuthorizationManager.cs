using System;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C2 RID: 1730
	public class RbacAuthorizationManager : ServiceAuthorizationManager
	{
		// Token: 0x060049C5 RID: 18885 RVA: 0x000E135D File Offset: 0x000DF55D
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			operationContext.SetRbacPrincipal((RbacPrincipal)HttpContext.Current.User);
			return base.CheckAccessCore(operationContext);
		}
	}
}
