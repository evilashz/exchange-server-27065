using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000035 RID: 53
	internal class AutoAttendantContext
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000AF5A File Offset: 0x0000915A
		public AutoAttendantContext(UMAutoAttendant aa, bool isBusinessHours)
		{
			this.AutoAttendant = aa;
			this.IsBusinessHours = isBusinessHours;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000AF70 File Offset: 0x00009170
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000AF78 File Offset: 0x00009178
		public UMAutoAttendant AutoAttendant { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000AF81 File Offset: 0x00009181
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000AF89 File Offset: 0x00009189
		public bool IsBusinessHours { get; set; }
	}
}
