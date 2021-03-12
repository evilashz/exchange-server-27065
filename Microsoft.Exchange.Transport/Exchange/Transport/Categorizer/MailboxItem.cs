using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001D5 RID: 469
	internal class MailboxItem : DeliverableItem
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x000559BA File Offset: 0x00053BBA
		public MailboxItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000559C3 File Offset: 0x00053BC3
		public override ADObjectId Database
		{
			get
			{
				return base.GetProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.Database");
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x000559D0 File Offset: 0x00053BD0
		public bool InStorageGroup
		{
			get
			{
				return base.GetProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.Database").Depth >= 14;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x000559E9 File Offset: 0x00053BE9
		public Guid MailboxGuid
		{
			get
			{
				return base.GetProperty<Guid>("Microsoft.Exchange.Transport.DirectoryData.ExchangeGuid");
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000559F6 File Offset: 0x00053BF6
		public string ServerName
		{
			get
			{
				return base.GetProperty<string>("Microsoft.Exchange.Transport.DirectoryData.ServerName");
			}
		}
	}
}
