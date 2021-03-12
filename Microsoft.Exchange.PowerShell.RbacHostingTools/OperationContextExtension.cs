using System;
using System.ServiceModel;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools
{
	// Token: 0x02000004 RID: 4
	internal static class OperationContextExtension
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002DF7 File Offset: 0x00000FF7
		public static RbacPrincipal GetRbacPrincipal(this OperationContext operationContext)
		{
			return operationContext.Extensions.Find<RbacPrincipal>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002E04 File Offset: 0x00001004
		public static void SetRbacPrincipal(this OperationContext operationContext, RbacPrincipal principal)
		{
			RbacPrincipal rbacPrincipal = operationContext.GetRbacPrincipal();
			if (rbacPrincipal != null)
			{
				operationContext.Extensions.Remove(rbacPrincipal);
			}
			if (principal != null)
			{
				operationContext.Extensions.Add(principal);
			}
		}
	}
}
