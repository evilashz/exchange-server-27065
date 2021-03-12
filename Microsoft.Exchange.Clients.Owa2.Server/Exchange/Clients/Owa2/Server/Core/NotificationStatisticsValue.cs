using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200018F RID: 399
	public sealed class NotificationStatisticsValue
	{
		// Token: 0x06000E34 RID: 3636 RVA: 0x00035BED File Offset: 0x00033DED
		public NotificationStatisticsValue()
		{
			this.CreatedAndDispatched = new NotificationStatisticsValue.NumberAndTime();
			this.CreatedAndQueued = new NotificationStatisticsValue.NumberAndTime();
			this.CreatedAndPushed = new NotificationStatisticsValue.NumberAndTime();
			this.ReceivedAndDispatched = new NotificationStatisticsValue.NumberAndTime();
			this.QueuedAndPushed = new NotificationStatisticsValue.NumberAndTime();
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00035C2C File Offset: 0x00033E2C
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x00035C34 File Offset: 0x00033E34
		public int Created { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00035C3D File Offset: 0x00033E3D
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x00035C45 File Offset: 0x00033E45
		public int Received { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00035C4E File Offset: 0x00033E4E
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x00035C56 File Offset: 0x00033E56
		public int Queued { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00035C5F File Offset: 0x00033E5F
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x00035C67 File Offset: 0x00033E67
		public int Pushed { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00035C70 File Offset: 0x00033E70
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x00035C78 File Offset: 0x00033E78
		public int Dispatched { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00035C81 File Offset: 0x00033E81
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x00035C89 File Offset: 0x00033E89
		public int Dropped { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00035C92 File Offset: 0x00033E92
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00035C9A File Offset: 0x00033E9A
		public int CreatedAndDropped { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x00035CA3 File Offset: 0x00033EA3
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00035CAB File Offset: 0x00033EAB
		public int ReceivedAndDropped { get; set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00035CB4 File Offset: 0x00033EB4
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00035CBC File Offset: 0x00033EBC
		public int QueuedAndDropped { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00035CC5 File Offset: 0x00033EC5
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00035CCD File Offset: 0x00033ECD
		public int DispatchingAndDropped { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00035CD6 File Offset: 0x00033ED6
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00035CDE File Offset: 0x00033EDE
		public NotificationStatisticsValue.NumberAndTime CreatedAndDispatched { get; private set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00035CE7 File Offset: 0x00033EE7
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x00035CEF File Offset: 0x00033EEF
		public NotificationStatisticsValue.NumberAndTime CreatedAndPushed { get; private set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00035CF8 File Offset: 0x00033EF8
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x00035D00 File Offset: 0x00033F00
		public NotificationStatisticsValue.NumberAndTime ReceivedAndDispatched { get; private set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00035D09 File Offset: 0x00033F09
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x00035D11 File Offset: 0x00033F11
		public NotificationStatisticsValue.NumberAndTime CreatedAndQueued { get; private set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x00035D1A File Offset: 0x00033F1A
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00035D22 File Offset: 0x00033F22
		public NotificationStatisticsValue.NumberAndTime QueuedAndPushed { get; private set; }

		// Token: 0x06000E53 RID: 3667 RVA: 0x00035D2C File Offset: 0x00033F2C
		public ICollection<KeyValuePair<string, object>> GetEventData()
		{
			return new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("Created", this.Created),
				new KeyValuePair<string, object>("Received", this.Received),
				new KeyValuePair<string, object>("Queued", this.Queued),
				new KeyValuePair<string, object>("Pushed", this.Pushed),
				new KeyValuePair<string, object>("Dispatched", this.Dispatched),
				new KeyValuePair<string, object>("Dropped", this.Dropped),
				new KeyValuePair<string, object>("CreatedAndDropped", this.CreatedAndDropped),
				new KeyValuePair<string, object>("ReceivedAndDropped", this.ReceivedAndDropped),
				new KeyValuePair<string, object>("QueuedAndDropped", this.QueuedAndDropped),
				new KeyValuePair<string, object>("DispatchingAndDropped", this.DispatchingAndDropped),
				new KeyValuePair<string, object>("CreatedAndDispatched", this.CreatedAndDispatched.TotalNumber),
				new KeyValuePair<string, object>("DwellTimeCreatedAndDispatched", this.CreatedAndDispatched.TotalMilliseconds),
				new KeyValuePair<string, object>("ReceivedAndDispatched", this.ReceivedAndDispatched.TotalNumber),
				new KeyValuePair<string, object>("DwellTimeReceivedAndDispatched", this.ReceivedAndDispatched.TotalMilliseconds),
				new KeyValuePair<string, object>("CreatedAndPushed", this.CreatedAndPushed.TotalNumber),
				new KeyValuePair<string, object>("DwellTimeCreatedAndPushed", this.CreatedAndPushed.TotalMilliseconds),
				new KeyValuePair<string, object>("CreatedAndQueued", this.CreatedAndQueued.TotalNumber),
				new KeyValuePair<string, object>("DwellTimeCreatedAndQueued", this.CreatedAndQueued.TotalMilliseconds),
				new KeyValuePair<string, object>("QueuedAndPushed", this.QueuedAndPushed.TotalNumber),
				new KeyValuePair<string, object>("DwellTimeQueuedAndPushed", this.QueuedAndPushed.TotalMilliseconds)
			};
		}

		// Token: 0x02000190 RID: 400
		public class NumberAndTime
		{
			// Token: 0x170003D8 RID: 984
			// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00035F8E File Offset: 0x0003418E
			// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00035F96 File Offset: 0x00034196
			public int TotalNumber { get; private set; }

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00035F9F File Offset: 0x0003419F
			// (set) Token: 0x06000E57 RID: 3671 RVA: 0x00035FA7 File Offset: 0x000341A7
			public double TotalMilliseconds { get; private set; }

			// Token: 0x06000E58 RID: 3672 RVA: 0x00035FB0 File Offset: 0x000341B0
			public void Add(double milliseconds)
			{
				this.TotalNumber++;
				this.TotalMilliseconds += milliseconds;
			}
		}
	}
}
