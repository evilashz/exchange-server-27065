using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000528 RID: 1320
	[DataContract(Namespace = "")]
	internal class MailboxDatabase
	{
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000C67DB File Offset: 0x000C49DB
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x000C67E3 File Offset: 0x000C49E3
		[DataMember(Order = 0)]
		public Guid Guid { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000C67EC File Offset: 0x000C49EC
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x000C67F4 File Offset: 0x000C49F4
		[DataMember(Order = 1)]
		public bool IsAssistantEnabled { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000C67FD File Offset: 0x000C49FD
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x000C6805 File Offset: 0x000C4A05
		[DataMember(Order = 2)]
		public DateTime StartTime { get; set; }
	}
}
