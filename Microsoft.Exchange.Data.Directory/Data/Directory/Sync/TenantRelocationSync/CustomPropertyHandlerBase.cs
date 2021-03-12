using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F6 RID: 2038
	internal abstract class CustomPropertyHandlerBase
	{
		// Token: 0x060064BE RID: 25790
		public abstract List<ADObjectId> EnumerateObjectDependenciesInSource(TenantRelocationSyncTranslator translator, DirectoryAttribute DirectoryAttributeModification);

		// Token: 0x060064BF RID: 25791
		public abstract void UpdateModifyRequestForTarget(TenantRelocationSyncTranslator translator, DirectoryAttribute sourceValue, ref DirectoryAttributeModification mod);
	}
}
