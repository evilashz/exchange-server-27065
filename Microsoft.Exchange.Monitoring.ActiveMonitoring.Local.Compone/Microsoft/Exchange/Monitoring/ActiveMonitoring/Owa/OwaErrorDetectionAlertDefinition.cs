using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa
{
	// Token: 0x0200026E RID: 622
	internal sealed class OwaErrorDetectionAlertDefinition : OwaEdsAlertDefinitions
	{
		// Token: 0x0600119E RID: 4510 RVA: 0x00076278 File Offset: 0x00074478
		public OwaErrorDetectionAlertDefinition(string redEvent, Component component, NotificationServiceClass responderType, bool monitorEnabled, bool responderEnabled, string whiteListedExceptions, string messageSubject)
		{
			base.RedEvent = redEvent;
			base.MessageSubject = ((!string.IsNullOrEmpty(messageSubject)) ? messageSubject : ((this.Api == "PerfTraceCTQ") ? string.Format("At least one exception reached the threshold for PerfTraceCTQ {0}", this.ClientActionName) : ((this.ClientActionName == string.Empty) ? string.Format("At least one exception reached the threshold for Api {0}", this.Api) : string.Format("At least one exception reached the threshold for Api {0} And Client Action {1}", this.Api, this.ClientActionName))));
			base.MessageBody = string.Empty;
			base.NotificationClass = responderType;
			this.Component = component;
			this.MonitorEnabled = monitorEnabled;
			this.ResponderEnabled = responderEnabled;
			this.WhiteListedExceptions = whiteListedExceptions;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00076334 File Offset: 0x00074534
		public string Api
		{
			get
			{
				if (this.api == null)
				{
					this.api = base.RedEvent.Split(new char[]
					{
						'_'
					})[0];
				}
				return this.api;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00076370 File Offset: 0x00074570
		public string ClientActionName
		{
			get
			{
				if (this.can == null)
				{
					string[] array = base.RedEvent.Split(new char[]
					{
						'_'
					});
					this.can = ((array.Length == 3) ? array[1] : string.Empty);
				}
				return this.can;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x000763BA File Offset: 0x000745BA
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x000763C2 File Offset: 0x000745C2
		public Component Component { get; private set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000763CB File Offset: 0x000745CB
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x000763D3 File Offset: 0x000745D3
		public bool MonitorEnabled { get; private set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x000763DC File Offset: 0x000745DC
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x000763E4 File Offset: 0x000745E4
		public bool ResponderEnabled { get; private set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x000763ED File Offset: 0x000745ED
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x000763F5 File Offset: 0x000745F5
		public string WhiteListedExceptions { get; private set; }

		// Token: 0x04000D45 RID: 3397
		private const string ClientEventPrefix = "PerfTraceCTQ";

		// Token: 0x04000D46 RID: 3398
		private string api;

		// Token: 0x04000D47 RID: 3399
		private string can;
	}
}
