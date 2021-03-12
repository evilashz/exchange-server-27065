using System;
using System.Data.Services.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x02000389 RID: 905
	[DataServiceKey("Index")]
	[Serializable]
	public class DetailObject : FfoReportObject, IPageableObject
	{
		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x00087A4A File Offset: 0x00085C4A
		// (set) Token: 0x06001F67 RID: 8039 RVA: 0x00087A52 File Offset: 0x00085C52
		[ODataInput("StartDate")]
		public DateTime StartDate { get; private set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x00087A5B File Offset: 0x00085C5B
		// (set) Token: 0x06001F69 RID: 8041 RVA: 0x00087A63 File Offset: 0x00085C63
		[ODataInput("EndDate")]
		public DateTime EndDate { get; private set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x00087A6C File Offset: 0x00085C6C
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x00087A74 File Offset: 0x00085C74
		public int Index { get; set; }
	}
}
