using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C9 RID: 713
	[DataContract]
	public class RecipientCounts
	{
		// Token: 0x06002C4C RID: 11340 RVA: 0x000890A9 File Offset: 0x000872A9
		internal RecipientCounts(int delivered, int pending, int transferred, int unsuccessful)
		{
			this.deliveredRecipients = delivered;
			this.pendingRecipients = pending;
			this.transferredRecipients = transferred;
			this.unsuccessfulRecipients = unsuccessful;
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x000890CE File Offset: 0x000872CE
		// (set) Token: 0x06002C4E RID: 11342 RVA: 0x000890D6 File Offset: 0x000872D6
		[DataMember]
		public int Delivered
		{
			get
			{
				return this.deliveredRecipients;
			}
			private set
			{
				this.deliveredRecipients = value;
			}
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x000890DF File Offset: 0x000872DF
		// (set) Token: 0x06002C50 RID: 11344 RVA: 0x000890E7 File Offset: 0x000872E7
		[DataMember]
		public int Pending
		{
			get
			{
				return this.pendingRecipients;
			}
			private set
			{
				this.pendingRecipients = value;
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000890F0 File Offset: 0x000872F0
		// (set) Token: 0x06002C52 RID: 11346 RVA: 0x000890F8 File Offset: 0x000872F8
		[DataMember]
		public int Transferred
		{
			get
			{
				return this.transferredRecipients;
			}
			private set
			{
				this.transferredRecipients = value;
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x00089101 File Offset: 0x00087301
		// (set) Token: 0x06002C54 RID: 11348 RVA: 0x00089109 File Offset: 0x00087309
		[DataMember]
		public int Unsuccessful
		{
			get
			{
				return this.unsuccessfulRecipients;
			}
			private set
			{
				this.unsuccessfulRecipients = value;
			}
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x00089112 File Offset: 0x00087312
		// (set) Token: 0x06002C56 RID: 11350 RVA: 0x0008912F File Offset: 0x0008732F
		[DataMember]
		public int Total
		{
			get
			{
				return this.deliveredRecipients + this.pendingRecipients + this.transferredRecipients + this.unsuccessfulRecipients;
			}
			private set
			{
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x00089131 File Offset: 0x00087331
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x00089135 File Offset: 0x00087335
		[DataMember]
		public int MaxRecipientsInList
		{
			get
			{
				return 30;
			}
			private set
			{
			}
		}

		// Token: 0x040021EF RID: 8687
		private int deliveredRecipients;

		// Token: 0x040021F0 RID: 8688
		private int pendingRecipients;

		// Token: 0x040021F1 RID: 8689
		private int unsuccessfulRecipients;

		// Token: 0x040021F2 RID: 8690
		private int transferredRecipients;
	}
}
