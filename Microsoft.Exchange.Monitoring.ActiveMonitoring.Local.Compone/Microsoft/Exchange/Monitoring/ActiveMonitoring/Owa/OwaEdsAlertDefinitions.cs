using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa
{
	// Token: 0x0200026C RID: 620
	internal class OwaEdsAlertDefinitions
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x00075DC8 File Offset: 0x00073FC8
		public OwaEdsAlertDefinitions()
		{
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00075DD0 File Offset: 0x00073FD0
		public OwaEdsAlertDefinitions(string redEvent, string subject, string body, NotificationServiceClass responderType, bool recycleAppPool)
		{
			this.RedEvent = redEvent;
			this.MessageSubject = subject;
			this.MessageBody = body;
			this.NotificationClass = responderType;
			this.RecycleAppPool = recycleAppPool;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00075DFD File Offset: 0x00073FFD
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00075E05 File Offset: 0x00074005
		public string RedEvent { get; protected set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00075E0E File Offset: 0x0007400E
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x00075E16 File Offset: 0x00074016
		public string MessageSubject { get; protected set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00075E1F File Offset: 0x0007401F
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00075E27 File Offset: 0x00074027
		public string MessageBody { get; protected set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00075E30 File Offset: 0x00074030
		public string MonitorName
		{
			get
			{
				return string.Format("{0}{1}", this.RedEvent, "Monitor");
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00075E47 File Offset: 0x00074047
		public string EscalateResponderName
		{
			get
			{
				return string.Format("{0}{1}", this.RedEvent, "Escalate");
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00075E5E File Offset: 0x0007405E
		public string RecycleResponderName
		{
			get
			{
				return string.Format("{0}{1}", this.RedEvent, "Recycle");
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00075E75 File Offset: 0x00074075
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00075E7D File Offset: 0x0007407D
		public NotificationServiceClass NotificationClass { get; protected set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00075E86 File Offset: 0x00074086
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00075E8E File Offset: 0x0007408E
		public bool RecycleAppPool { get; protected set; }

		// Token: 0x04000D3C RID: 3388
		internal const string MonitorString = "Monitor";

		// Token: 0x04000D3D RID: 3389
		internal const string EscalateString = "Escalate";

		// Token: 0x04000D3E RID: 3390
		internal const string RecycleString = "Recycle";
	}
}
