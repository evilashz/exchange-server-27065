using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.TimeZoneOffsets
{
	// Token: 0x020001BE RID: 446
	public class TimeZoneRange
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0004733D File Offset: 0x0004553D
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00047345 File Offset: 0x00045545
		public ExDateTime UtcTime { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0004734E File Offset: 0x0004554E
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00047356 File Offset: 0x00045556
		public int Offset { get; set; }
	}
}
