using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200037B RID: 891
	internal class IPSafelistingSmpEnabledQueryProcessor : SafelistingUIModeQueryProcessor
	{
		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000934A7 File Offset: 0x000916A7
		protected override SafelistingUIMode SafelistingUIMode
		{
			get
			{
				return SafelistingUIMode.Smp;
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x0600304D RID: 12365 RVA: 0x000934AA File Offset: 0x000916AA
		protected override string RbacRoleName
		{
			get
			{
				return "IPSafelistingSmpEnabledRole";
			}
		}

		// Token: 0x04002359 RID: 9049
		public const string RoleName = "IPSafelistingSmpEnabledRole";
	}
}
