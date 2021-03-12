using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001BB RID: 443
	public class FailureEvent : IFailureObjectLoggable
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x000272A6 File Offset: 0x000254A6
		public FailureEvent(Guid objectGuid, string objectType, int flags, string failureContext)
		{
			this.ObjectGuid = objectGuid;
			this.ObjectType = objectType;
			this.Flags = flags;
			this.FailureContext = failureContext;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000272CB File Offset: 0x000254CB
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x000272D3 File Offset: 0x000254D3
		public Guid ObjectGuid { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x000272DC File Offset: 0x000254DC
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x000272E4 File Offset: 0x000254E4
		public string ObjectType { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x000272ED File Offset: 0x000254ED
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x000272F5 File Offset: 0x000254F5
		public int Flags { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x000272FE File Offset: 0x000254FE
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00027306 File Offset: 0x00025506
		public string FailureContext { get; set; }
	}
}
