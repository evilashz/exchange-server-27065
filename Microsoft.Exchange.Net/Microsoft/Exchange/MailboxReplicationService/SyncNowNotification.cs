using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000772 RID: 1906
	[DataContract]
	internal sealed class SyncNowNotification
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x0004F5C8 File Offset: 0x0004D7C8
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x0004F5D0 File Offset: 0x0004D7D0
		[DataMember(IsRequired = true)]
		public Guid MailboxGuid { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0004F5D9 File Offset: 0x0004D7D9
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x0004F5E1 File Offset: 0x0004D7E1
		[DataMember(IsRequired = true)]
		public Guid MdbGuid { get; set; }

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x0004F5EA File Offset: 0x0004D7EA
		// (set) Token: 0x060025B8 RID: 9656 RVA: 0x0004F5F2 File Offset: 0x0004D7F2
		[DataMember(IsRequired = true)]
		public int Flags { get; set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x0004F5FB File Offset: 0x0004D7FB
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x0004F603 File Offset: 0x0004D803
		public SyncNowNotificationFlags SyncNowNotificationFlags
		{
			get
			{
				return (SyncNowNotificationFlags)this.Flags;
			}
			set
			{
				this.Flags = (int)value;
			}
		}
	}
}
