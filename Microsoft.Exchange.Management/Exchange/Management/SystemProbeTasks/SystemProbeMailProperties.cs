using System;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DAA RID: 3498
	[Serializable]
	public class SystemProbeMailProperties
	{
		// Token: 0x170029BB RID: 10683
		// (get) Token: 0x06008612 RID: 34322 RVA: 0x00224565 File Offset: 0x00222765
		// (set) Token: 0x06008613 RID: 34323 RVA: 0x0022456D File Offset: 0x0022276D
		public Guid Guid { get; set; }

		// Token: 0x170029BC RID: 10684
		// (get) Token: 0x06008614 RID: 34324 RVA: 0x00224576 File Offset: 0x00222776
		// (set) Token: 0x06008615 RID: 34325 RVA: 0x0022457E File Offset: 0x0022277E
		public string MailMessage { get; set; }
	}
}
