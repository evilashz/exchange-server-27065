using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000039 RID: 57
	internal class AutoAttendantLocationContext
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000B686 File Offset: 0x00009886
		public AutoAttendantLocationContext(UMAutoAttendant aa, string locationMenuDescription)
		{
			this.AutoAttendant = aa;
			this.LocationMenuDescription = locationMenuDescription;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000B69C File Offset: 0x0000989C
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000B6A4 File Offset: 0x000098A4
		public UMAutoAttendant AutoAttendant { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000B6AD File Offset: 0x000098AD
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000B6B5 File Offset: 0x000098B5
		public string LocationMenuDescription { get; private set; }
	}
}
