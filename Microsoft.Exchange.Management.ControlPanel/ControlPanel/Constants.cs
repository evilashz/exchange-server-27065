using System;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000549 RID: 1353
	public static class Constants
	{
		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x000BFEA0 File Offset: 0x000BE0A0
		public static bool IsGallatin
		{
			get
			{
				return RbacPrincipal.Current.IsInRole("IsGallatin");
			}
		}
	}
}
