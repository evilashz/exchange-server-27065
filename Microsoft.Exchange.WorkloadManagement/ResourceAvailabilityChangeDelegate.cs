using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200002C RID: 44
	// (Invoke) Token: 0x0600017C RID: 380
	internal delegate void ResourceAvailabilityChangeDelegate(ResourceKey resourceKey, WorkloadClassification classification, bool available);
}
