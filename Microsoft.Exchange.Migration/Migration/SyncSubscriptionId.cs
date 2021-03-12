using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200008A RID: 138
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncSubscriptionId : ISubscriptionId, ISnapshotId, IMigrationSerializable
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x000232EA File Offset: 0x000214EA
		internal SyncSubscriptionId(IMailboxData mailboxData)
		{
			this.MailboxData = mailboxData;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x000232F9 File Offset: 0x000214F9
		internal SyncSubscriptionId(Guid? subscriptionId, StoreObjectId subscriptionMessageId, IMailboxData mailboxData) : this(mailboxData)
		{
			this.Id = subscriptionId;
			this.MessageId = subscriptionMessageId;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00023310 File Offset: 0x00021510
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00023318 File Offset: 0x00021518
		public StoreObjectId MessageId
		{
			get
			{
				return this.messageId;
			}
			private set
			{
				this.messageId = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00023321 File Offset: 0x00021521
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00023329 File Offset: 0x00021529
		public Guid? Id
		{
			get
			{
				return this.id;
			}
			private set
			{
				this.id = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00023332 File Offset: 0x00021532
		public bool HasValue
		{
			get
			{
				return this.MessageId != null;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00023340 File Offset: 0x00021540
		public ADObjectId MailboxOwner
		{
			get
			{
				return ((MailboxData)this.MailboxData).UserMailboxADObjectId;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00023352 File Offset: 0x00021552
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return SyncSubscriptionId.SyncSubscriptionIdPropertyDefinitions;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00023359 File Offset: 0x00021559
		public MigrationType MigrationType
		{
			get
			{
				return MigrationType.IMAP;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002335C File Offset: 0x0002155C
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00023364 File Offset: 0x00021564
		public IMailboxData MailboxData { get; private set; }

		// Token: 0x060007C0 RID: 1984 RVA: 0x00023370 File Offset: 0x00021570
		public override string ToString()
		{
			if (this.Id != null)
			{
				return this.Id.Value.ToString();
			}
			if (this.MessageId != null)
			{
				return this.MessageId.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000233C4 File Offset: 0x000215C4
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.MessageId = MigrationHelper.GetObjectIdProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionMessageId, false);
			Guid guidProperty = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionId, false);
			this.Id = null;
			if (guidProperty != Guid.Empty)
			{
				this.Id = new Guid?(guidProperty);
			}
			return this.HasValue;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00023420 File Offset: 0x00021620
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			if (this.MessageId == null)
			{
				message.Delete(MigrationBatchMessageSchema.MigrationJobItemSubscriptionMessageId);
			}
			else
			{
				message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionMessageId] = this.MessageId.GetBytes();
			}
			if (this.Id == null)
			{
				message.Delete(MigrationBatchMessageSchema.MigrationJobItemSubscriptionId);
				return;
			}
			message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionId] = this.Id;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002348C File Offset: 0x0002168C
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return new XElement(MigrationBatchMessageSchema.MigrationJobItemSubscriptionId.Name, new object[]
			{
				new XElement("Id", this.Id),
				new XElement("MessageId", this.MessageId)
			});
		}

		// Token: 0x04000343 RID: 835
		public static readonly PropertyDefinition[] SyncSubscriptionIdPropertyDefinitions = new StorePropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemSubscriptionId,
			MigrationBatchMessageSchema.MigrationJobItemSubscriptionMessageId
		};

		// Token: 0x04000344 RID: 836
		private StoreObjectId messageId;

		// Token: 0x04000345 RID: 837
		private Guid? id;
	}
}
