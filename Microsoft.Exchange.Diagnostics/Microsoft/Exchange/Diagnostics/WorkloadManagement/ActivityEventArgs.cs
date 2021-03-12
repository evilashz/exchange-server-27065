using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001ED RID: 493
	internal class ActivityEventArgs : EventArgs
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x0003A12D File Offset: 0x0003832D
		public ActivityEventArgs(ActivityEventType activityEventType, string message = null)
		{
			this.activityEventType = activityEventType;
			this.Message = message;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0003A143 File Offset: 0x00038343
		public ActivityEventType ActivityEventType
		{
			get
			{
				return this.activityEventType;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0003A14B File Offset: 0x0003834B
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0003A153 File Offset: 0x00038353
		public string Message { get; private set; }

		// Token: 0x04000A5B RID: 2651
		private readonly ActivityEventType activityEventType;
	}
}
