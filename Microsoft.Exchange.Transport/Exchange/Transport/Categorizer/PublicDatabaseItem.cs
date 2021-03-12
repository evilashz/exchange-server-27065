using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001E7 RID: 487
	internal class PublicDatabaseItem : DeliverableItem
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x00058980 File Offset: 0x00056B80
		public PublicDatabaseItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00058989 File Offset: 0x00056B89
		public string DistinguishedName
		{
			get
			{
				return base.GetProperty<string>("Microsoft.Exchange.Transport.DirectoryData.DistinguishedName");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00058996 File Offset: 0x00056B96
		public override ADObjectId Database
		{
			get
			{
				return base.GetProperty<ADObjectId>("Microsoft.Exchange.Transport.DirectoryData.Id");
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000589A3 File Offset: 0x00056BA3
		public override string HomeMdbDN
		{
			get
			{
				return this.DistinguishedName;
			}
		}
	}
}
