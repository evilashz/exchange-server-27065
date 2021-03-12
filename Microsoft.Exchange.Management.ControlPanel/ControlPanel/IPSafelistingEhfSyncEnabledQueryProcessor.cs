using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200037C RID: 892
	internal class IPSafelistingEhfSyncEnabledQueryProcessor : SafelistingUIModeQueryProcessor
	{
		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000934B9 File Offset: 0x000916B9
		protected override SafelistingUIMode SafelistingUIMode
		{
			get
			{
				return SafelistingUIMode.Ecp;
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000934BC File Offset: 0x000916BC
		protected override string RbacRoleName
		{
			get
			{
				return "IPSafelistingEhfSyncEnabledRole";
			}
		}

		// Token: 0x0400235A RID: 9050
		public const string RoleName = "IPSafelistingEhfSyncEnabledRole";
	}
}
