using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C6 RID: 198
	public class PolicyHistoryResult
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x0000F45C File Offset: 0x0000D65C
		public PolicyHistoryResult()
		{
			this.Type = typeof(object);
			this.SupplementalInfo = 0;
			this.Results = new List<string>();
			this.TimeSpent = default(TimeSpan);
			this.Error = string.Empty;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000F4AB File Offset: 0x0000D6AB
		public PolicyHistoryResult(Type type, IEnumerable<string> results, int supplementalInfo, TimeSpan timeSpent)
		{
			this.Type = type;
			this.Results = new List<string>(results);
			this.SupplementalInfo = supplementalInfo;
			this.TimeSpent = timeSpent;
			this.Error = string.Empty;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		public string Name { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000F4F1 File Offset: 0x0000D6F1
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000F4F9 File Offset: 0x0000D6F9
		public Type Type { get; private set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000F502 File Offset: 0x0000D702
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000F50A File Offset: 0x0000D70A
		public int SupplementalInfo { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000F513 File Offset: 0x0000D713
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000F51B File Offset: 0x0000D71B
		public List<string> Results { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000F524 File Offset: 0x0000D724
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0000F52C File Offset: 0x0000D72C
		public TimeSpan TimeSpent { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000F535 File Offset: 0x0000D735
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x0000F53D File Offset: 0x0000D73D
		public string Error { get; private set; }
	}
}
