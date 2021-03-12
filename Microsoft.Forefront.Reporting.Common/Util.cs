using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000019 RID: 25
	public static class Util
	{
		// Token: 0x04000082 RID: 130
		public static readonly string RegionTag = DatacenterRegistry.GetForefrontRegion();
	}
}
