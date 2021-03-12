using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002B6 RID: 694
	internal class TrackingAuthorityKindInformation : Attribute
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0005A94C File Offset: 0x00058B4C
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x0005A954 File Offset: 0x00058B54
		public int ExpectedConnectionLatencyMSec { get; set; }
	}
}
