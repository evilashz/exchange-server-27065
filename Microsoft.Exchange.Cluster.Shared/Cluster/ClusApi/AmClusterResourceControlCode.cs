﻿using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000056 RID: 86
	internal enum AmClusterResourceControlCode : uint
	{
		// Token: 0x04000117 RID: 279
		CLUSCTL_RESOURCE_GET_RO_COMMON_PROPERTIES = 16777301U,
		// Token: 0x04000118 RID: 280
		CLUSCTL_RESOURCE_GET_COMMON_PROPERTIES = 16777305U,
		// Token: 0x04000119 RID: 281
		CLUSCTL_RESOURCE_GET_PRIVATE_PROPERTIES = 16777345U,
		// Token: 0x0400011A RID: 282
		CLUSCTL_RESOURCE_SET_COMMON_PROPERTIES = 20971614U,
		// Token: 0x0400011B RID: 283
		CLUSCTL_RESOURCE_SET_PRIVATE_PROPERTIES = 20971654U
	}
}