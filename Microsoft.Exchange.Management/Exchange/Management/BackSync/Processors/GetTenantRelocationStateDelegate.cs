using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000BF RID: 191
	// (Invoke) Token: 0x0600060A RID: 1546
	internal delegate TenantRelocationState GetTenantRelocationStateDelegate(ADObjectId tenantOUId, out bool isSourceTenant, bool readThrough = false);
}
