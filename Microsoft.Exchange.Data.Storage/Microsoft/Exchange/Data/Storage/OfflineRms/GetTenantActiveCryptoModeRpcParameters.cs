using System;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AB5 RID: 2741
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetTenantActiveCryptoModeRpcParameters : LicensingRpcParameters
	{
		// Token: 0x060063FC RID: 25596 RVA: 0x001A7B08 File Offset: 0x001A5D08
		public GetTenantActiveCryptoModeRpcParameters(byte[] data) : base(data)
		{
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x001A7B11 File Offset: 0x001A5D11
		public GetTenantActiveCryptoModeRpcParameters(RmsClientManagerContext rmsClientManagerContext) : base(rmsClientManagerContext)
		{
			if (rmsClientManagerContext == null)
			{
				throw new ArgumentNullException("rmsClientManagerContext");
			}
		}
	}
}
