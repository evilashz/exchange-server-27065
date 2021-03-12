using System;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000159 RID: 345
	internal class RbacCheckerWrapper
	{
		// Token: 0x060021A8 RID: 8616 RVA: 0x000655F1 File Offset: 0x000637F1
		private RbacCheckerWrapper()
		{
		}

		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x000655F9 File Offset: 0x000637F9
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x00065609 File Offset: 0x00063809
		internal static IIsInRole RbacChecker
		{
			get
			{
				return RbacCheckerWrapper.rbacChecker ?? RbacPrincipal.Current;
			}
			set
			{
				RbacCheckerWrapper.rbacChecker = value;
			}
		}

		// Token: 0x04001D3A RID: 7482
		private static IIsInRole rbacChecker;
	}
}
