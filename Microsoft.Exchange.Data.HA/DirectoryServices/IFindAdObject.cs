using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFindAdObject<TADWrapperObject> where TADWrapperObject : class, IADObjectCommon
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001B9 RID: 441
		IADToplogyConfigurationSession AdSession { get; }

		// Token: 0x060001BA RID: 442
		void Clear();

		// Token: 0x060001BB RID: 443
		TADWrapperObject ReadAdObjectByObjectId(ADObjectId objectId);

		// Token: 0x060001BC RID: 444
		TADWrapperObject ReadAdObjectByObjectIdEx(ADObjectId objectId, out Exception ex);

		// Token: 0x060001BD RID: 445
		TADWrapperObject FindAdObjectByGuid(Guid objectGuid);

		// Token: 0x060001BE RID: 446
		TADWrapperObject FindAdObjectByGuidEx(Guid objectGuid, AdObjectLookupFlags flags);

		// Token: 0x060001BF RID: 447
		TADWrapperObject FindAdObjectByGuidEx(Guid objectGuid, AdObjectLookupFlags flags, IPerformanceDataLogger perfLogger);

		// Token: 0x060001C0 RID: 448
		TADWrapperObject FindAdObjectByQuery(QueryFilter queryFilter);

		// Token: 0x060001C1 RID: 449
		TADWrapperObject FindAdObjectByQueryEx(QueryFilter queryFilter, AdObjectLookupFlags flags);

		// Token: 0x060001C2 RID: 450
		TADWrapperObject FindServerByFqdn(string fqdn);

		// Token: 0x060001C3 RID: 451
		TADWrapperObject FindServerByFqdnWithException(string fqdn, out Exception ex);
	}
}
