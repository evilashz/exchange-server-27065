using System;
using Microsoft.Exchange.Protocols.MAPI;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000049 RID: 73
	public class QueryableSessionsPerService
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000E520 File Offset: 0x0000C720
		public QueryableSessionsPerService(MapiServiceType mapiServiceType, long sessionCount)
		{
			this.MapiServiceType = mapiServiceType.ToString();
			this.SessionCount = sessionCount;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E540 File Offset: 0x0000C740
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000E548 File Offset: 0x0000C748
		public string MapiServiceType { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000E551 File Offset: 0x0000C751
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000E559 File Offset: 0x0000C759
		public long SessionCount { get; private set; }
	}
}
