using System;
using System.Data.Services.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003BD RID: 957
	[DataServiceKey("Index")]
	[Serializable]
	public class MtrtObject : FfoReportObject, IPageableObject
	{
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0008CCEE File Offset: 0x0008AEEE
		// (set) Token: 0x06002253 RID: 8787 RVA: 0x0008CCF6 File Offset: 0x0008AEF6
		[ODataInput("StartDate")]
		[DalConversion("ValueFromTask", "StartDate", new string[]
		{

		})]
		public DateTime StartDate { get; internal set; }

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x0008CCFF File Offset: 0x0008AEFF
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x0008CD07 File Offset: 0x0008AF07
		[ODataInput("EndDate")]
		[DalConversion("ValueFromTask", "EndDate", new string[]
		{

		})]
		public DateTime EndDate { get; internal set; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0008CD10 File Offset: 0x0008AF10
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x0008CD18 File Offset: 0x0008AF18
		public int Index { get; set; }
	}
}
