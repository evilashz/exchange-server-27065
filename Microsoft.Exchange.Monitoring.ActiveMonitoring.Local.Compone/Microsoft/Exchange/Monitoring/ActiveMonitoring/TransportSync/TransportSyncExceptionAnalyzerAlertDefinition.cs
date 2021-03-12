using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004FB RID: 1275
	internal sealed class TransportSyncExceptionAnalyzerAlertDefinition
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000C0326 File Offset: 0x000BE526
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x000C032E File Offset: 0x000BE52E
		public bool IsEnabled { get; internal set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x000C0337 File Offset: 0x000BE537
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x000C033F File Offset: 0x000BE53F
		public TimeSpan RecurrenceInterval { get; internal set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x000C0348 File Offset: 0x000BE548
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x000C0350 File Offset: 0x000BE550
		public TimeSpan MonitoringInterval { get; internal set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x000C0359 File Offset: 0x000BE559
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x000C0361 File Offset: 0x000BE561
		public string RedEvent { get; internal set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x000C036A File Offset: 0x000BE56A
		// (set) Token: 0x06001F8B RID: 8075 RVA: 0x000C0372 File Offset: 0x000BE572
		public string MessageSubject { get; internal set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x000C037B File Offset: 0x000BE57B
		// (set) Token: 0x06001F8D RID: 8077 RVA: 0x000C0383 File Offset: 0x000BE583
		public string MessageBody { get; internal set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x000C038C File Offset: 0x000BE58C
		public string MonitorName
		{
			get
			{
				return this.RedEvent.ToString();
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x000C0399 File Offset: 0x000BE599
		// (set) Token: 0x06001F90 RID: 8080 RVA: 0x000C03A1 File Offset: 0x000BE5A1
		public Component Component { get; internal set; }
	}
}
