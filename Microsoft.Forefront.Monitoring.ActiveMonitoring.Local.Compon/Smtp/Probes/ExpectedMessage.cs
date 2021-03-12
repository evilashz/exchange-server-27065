using System;
using System.Collections.Generic;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200021D RID: 541
	public class ExpectedMessage
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00033B74 File Offset: 0x00031D74
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x00033B7C File Offset: 0x00031D7C
		internal Notification Subject { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00033B85 File Offset: 0x00031D85
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00033B8D File Offset: 0x00031D8D
		internal Notification Body { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00033B96 File Offset: 0x00031D96
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00033B9E File Offset: 0x00031D9E
		internal List<Notification> Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00033BA7 File Offset: 0x00031DA7
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00033BAF File Offset: 0x00031DAF
		internal Notification Attachment { get; set; }

		// Token: 0x04000831 RID: 2097
		private List<Notification> headers = new List<Notification>();
	}
}
