using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A76 RID: 2678
	[Serializable]
	public sealed class PublicFolderItemStatistics : XsoMailboxConfigurationObject
	{
		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x0019DB55 File Offset: 0x0019BD55
		internal static XsoMailboxConfigurationObjectSchema StaticSchema
		{
			get
			{
				return PublicFolderItemStatistics.schema;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x0019DB5C File Offset: 0x0019BD5C
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return PublicFolderItemStatistics.schema;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x0019DB63 File Offset: 0x0019BD63
		public string Subject
		{
			get
			{
				return (string)this[MailMessageSchema.Subject];
			}
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x060061D7 RID: 25047 RVA: 0x0019DB75 File Offset: 0x0019BD75
		// (set) Token: 0x060061D8 RID: 25048 RVA: 0x0019DB7D File Offset: 0x0019BD7D
		public string PublicFolderName
		{
			get
			{
				return this.publicFolderName;
			}
			internal set
			{
				this.publicFolderName = value;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x060061D9 RID: 25049 RVA: 0x0019DB86 File Offset: 0x0019BD86
		public DateTime? LastModificationTime
		{
			get
			{
				return (DateTime?)((ExDateTime?)this[PublicFolderItemStatisticsSchema.LastModifiedTime]);
			}
		}

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x060061DA RID: 25050 RVA: 0x0019DB9D File Offset: 0x0019BD9D
		public DateTime? CreationTime
		{
			get
			{
				return (DateTime?)((ExDateTime?)this[PublicFolderItemStatisticsSchema.CreationTime]);
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x060061DB RID: 25051 RVA: 0x0019DBB4 File Offset: 0x0019BDB4
		public bool HasAttachments
		{
			get
			{
				return (bool)this[PublicFolderItemStatisticsSchema.HasAttachment];
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x060061DC RID: 25052 RVA: 0x0019DBC6 File Offset: 0x0019BDC6
		public string ItemType
		{
			get
			{
				return (string)this[PublicFolderItemStatisticsSchema.ItemClass];
			}
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x060061DD RID: 25053 RVA: 0x0019DBD8 File Offset: 0x0019BDD8
		public Unlimited<ByteQuantifiedSize> MessageSize
		{
			get
			{
				int num = (int)this[PublicFolderItemStatisticsSchema.Size];
				return ByteQuantifiedSize.FromBytes(checked((ulong)num));
			}
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x0019DC02 File Offset: 0x0019BE02
		public override ObjectId Identity
		{
			get
			{
				return (MailboxStoreObjectId)this[MailMessageSchema.Identity];
			}
		}

		// Token: 0x04003794 RID: 14228
		private static PublicFolderItemStatisticsSchema schema = new PublicFolderItemStatisticsSchema();

		// Token: 0x04003795 RID: 14229
		private string publicFolderName;
	}
}
