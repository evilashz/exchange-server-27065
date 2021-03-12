using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps
{
	// Token: 0x0200042B RID: 1067
	public class FailureInstance
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0009AF34 File Offset: 0x00099134
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0009AF3C File Offset: 0x0009913C
		public string Cause { get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x0009AF45 File Offset: 0x00099145
		// (set) Token: 0x06001B80 RID: 7040 RVA: 0x0009AF4D File Offset: 0x0009914D
		public string Resolution { get; set; }
	}
}
