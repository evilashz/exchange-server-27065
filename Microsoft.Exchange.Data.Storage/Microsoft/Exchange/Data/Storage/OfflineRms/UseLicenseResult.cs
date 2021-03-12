using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AC3 RID: 2755
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UseLicenseResult
	{
		// Token: 0x06006465 RID: 25701 RVA: 0x001A9B3E File Offset: 0x001A7D3E
		internal UseLicenseResult(string endUseLicense, RightsManagementServerException e)
		{
			this.EndUseLicense = endUseLicense;
			this.Error = e;
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x06006466 RID: 25702 RVA: 0x001A9B54 File Offset: 0x001A7D54
		// (set) Token: 0x06006467 RID: 25703 RVA: 0x001A9B5C File Offset: 0x001A7D5C
		public string EndUseLicense { get; private set; }

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x06006468 RID: 25704 RVA: 0x001A9B65 File Offset: 0x001A7D65
		// (set) Token: 0x06006469 RID: 25705 RVA: 0x001A9B6D File Offset: 0x001A7D6D
		public RightsManagementServerException Error { get; private set; }
	}
}
