using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001CF RID: 463
	internal abstract class DeliverableItem : RestrictedItem
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00055131 File Offset: 0x00053331
		public DeliverableItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001525 RID: 5413
		public abstract ADObjectId Database { get; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0005513C File Offset: 0x0005333C
		public virtual string HomeMdbDN
		{
			get
			{
				ADObjectId database = this.Database;
				if (database == null)
				{
					return null;
				}
				return database.DistinguishedName;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0005515C File Offset: 0x0005335C
		public string HomeMdbName
		{
			get
			{
				ADObjectId database = this.Database;
				if (database != null)
				{
					return database.Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0005517F File Offset: 0x0005337F
		public string LegacyExchangeDN
		{
			get
			{
				return base.GetProperty<string>("Microsoft.Exchange.Transport.DirectoryData.LegacyExchangeDN");
			}
		}
	}
}
