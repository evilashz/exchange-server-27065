using System;
using System.Data.Services.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x0200038B RID: 907
	[DataServiceKey("Index")]
	[Serializable]
	public class TrafficObject : FfoReportObject, IPageableObject
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x00087BBF File Offset: 0x00085DBF
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x00087BC7 File Offset: 0x00085DC7
		[ODataInput("StartDate")]
		public DateTime StartDate { get; private set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00087BD0 File Offset: 0x00085DD0
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x00087BD8 File Offset: 0x00085DD8
		[ODataInput("EndDate")]
		public DateTime EndDate { get; private set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x00087BE1 File Offset: 0x00085DE1
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x00087BE9 File Offset: 0x00085DE9
		[ODataInput("AggregateBy")]
		public string AggregateBy { get; private set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00087BF2 File Offset: 0x00085DF2
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x00087BFA File Offset: 0x00085DFA
		public int Index { get; set; }
	}
}
