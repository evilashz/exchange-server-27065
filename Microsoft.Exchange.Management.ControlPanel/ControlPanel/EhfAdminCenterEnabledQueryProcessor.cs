using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200037D RID: 893
	internal class EhfAdminCenterEnabledQueryProcessor : SafelistingUIModeQueryProcessor
	{
		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x000934CB File Offset: 0x000916CB
		protected override SafelistingUIMode SafelistingUIMode
		{
			get
			{
				return SafelistingUIMode.EhfAC;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x000934CE File Offset: 0x000916CE
		protected override string RbacRoleName
		{
			get
			{
				return "EhfAdminCenterEnabledRole";
			}
		}

		// Token: 0x0400235B RID: 9051
		public const string RoleName = "EhfAdminCenterEnabledRole";
	}
}
