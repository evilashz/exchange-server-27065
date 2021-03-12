using System;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.PushNotifications.Server.Wcf
{
	// Token: 0x02000026 RID: 38
	internal static class Extensions
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000043DC File Offset: 0x000025DC
		public static IPrincipal GetPrincipal(this OperationContext operationContext)
		{
			Extensions.PrincipalWrapper principalWrapper = operationContext.Extensions.Find<Extensions.PrincipalWrapper>();
			if (principalWrapper != null)
			{
				return principalWrapper.Principal;
			}
			return null;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004400 File Offset: 0x00002600
		public static OAuthIdentity GetOAuthIdentity(this OperationContext operationContext)
		{
			GenericPrincipal genericPrincipal = operationContext.GetPrincipal() as GenericPrincipal;
			if (genericPrincipal != null)
			{
				return genericPrincipal.Identity as OAuthIdentity;
			}
			return null;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000442C File Offset: 0x0000262C
		public static WindowsIdentity GetWindowsIdentity(this OperationContext operationContext)
		{
			WindowsPrincipal windowsPrincipal = operationContext.GetPrincipal() as WindowsPrincipal;
			if (windowsPrincipal != null)
			{
				return windowsPrincipal.Identity as WindowsIdentity;
			}
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004455 File Offset: 0x00002655
		public static void SetPrincipal(this OperationContext operationContext, IPrincipal principal)
		{
			ArgumentValidator.ThrowIfNull("principal", principal);
			operationContext.Extensions.Add(new Extensions.PrincipalWrapper(principal));
		}

		// Token: 0x02000027 RID: 39
		private class PrincipalWrapper : IExtension<OperationContext>
		{
			// Token: 0x060000FB RID: 251 RVA: 0x00004473 File Offset: 0x00002673
			public PrincipalWrapper(IPrincipal principal)
			{
				ArgumentValidator.ThrowIfNull("principal", principal);
				this.Principal = principal;
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x060000FC RID: 252 RVA: 0x0000448D File Offset: 0x0000268D
			// (set) Token: 0x060000FD RID: 253 RVA: 0x00004495 File Offset: 0x00002695
			public IPrincipal Principal { get; private set; }

			// Token: 0x060000FE RID: 254 RVA: 0x0000449E File Offset: 0x0000269E
			public void Attach(OperationContext owner)
			{
			}

			// Token: 0x060000FF RID: 255 RVA: 0x000044A0 File Offset: 0x000026A0
			public void Detach(OperationContext owner)
			{
			}
		}
	}
}
