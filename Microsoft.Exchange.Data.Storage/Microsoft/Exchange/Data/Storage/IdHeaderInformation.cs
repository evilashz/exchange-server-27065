using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D85 RID: 3461
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IdHeaderInformation
	{
		// Token: 0x06007739 RID: 30521 RVA: 0x0020E521 File Offset: 0x0020C721
		public IdHeaderInformation()
		{
			this.mailboxId = new MailboxId(null);
		}

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x0600773A RID: 30522 RVA: 0x0020E535 File Offset: 0x0020C735
		// (set) Token: 0x0600773B RID: 30523 RVA: 0x0020E53D File Offset: 0x0020C73D
		public byte[] StoreIdBytes
		{
			get
			{
				return this.storeIdBytes;
			}
			set
			{
				this.storeIdBytes = value;
			}
		}

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x0600773C RID: 30524 RVA: 0x0020E546 File Offset: 0x0020C746
		// (set) Token: 0x0600773D RID: 30525 RVA: 0x0020E54E File Offset: 0x0020C74E
		public byte[] FolderIdBytes
		{
			get
			{
				return this.folderIdBytes;
			}
			set
			{
				this.folderIdBytes = value;
			}
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x0600773E RID: 30526 RVA: 0x0020E557 File Offset: 0x0020C757
		// (set) Token: 0x0600773F RID: 30527 RVA: 0x0020E55F File Offset: 0x0020C75F
		public MailboxId MailboxId
		{
			get
			{
				return this.mailboxId;
			}
			set
			{
				this.mailboxId = value;
			}
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x06007740 RID: 30528 RVA: 0x0020E568 File Offset: 0x0020C768
		// (set) Token: 0x06007741 RID: 30529 RVA: 0x0020E570 File Offset: 0x0020C770
		public IdProcessingInstruction IdProcessingInstruction
		{
			get
			{
				return this.idProcessingInstruction;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<IdProcessingInstruction>(value, "value");
				this.idProcessingInstruction = value;
			}
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x06007742 RID: 30530 RVA: 0x0020E584 File Offset: 0x0020C784
		// (set) Token: 0x06007743 RID: 30531 RVA: 0x0020E58C File Offset: 0x0020C78C
		public int OccurrenceInstanceIndex
		{
			get
			{
				return this.occurrenceInstanceIndex;
			}
			set
			{
				this.occurrenceInstanceIndex = value;
			}
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x06007744 RID: 30532 RVA: 0x0020E595 File Offset: 0x0020C795
		// (set) Token: 0x06007745 RID: 30533 RVA: 0x0020E59D File Offset: 0x0020C79D
		public IdStorageType IdStorageType
		{
			get
			{
				return this.idStorageType;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<IdStorageType>(value, "value");
				this.idStorageType = value;
			}
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x0020E5B4 File Offset: 0x0020C7B4
		public StoreObjectId ToStoreObjectId()
		{
			switch (this.IdProcessingInstruction)
			{
			case IdProcessingInstruction.Normal:
				return StoreObjectId.FromProviderSpecificId(this.StoreIdBytes);
			case IdProcessingInstruction.Series:
				return StoreObjectId.FromProviderSpecificId(this.StoreIdBytes, StoreObjectType.CalendarItemSeries);
			}
			return StoreObjectId.Deserialize(this.StoreIdBytes);
		}

		// Token: 0x0400528B RID: 21131
		private IdStorageType idStorageType;

		// Token: 0x0400528C RID: 21132
		private IdProcessingInstruction idProcessingInstruction;

		// Token: 0x0400528D RID: 21133
		private byte[] storeIdBytes;

		// Token: 0x0400528E RID: 21134
		private byte[] folderIdBytes;

		// Token: 0x0400528F RID: 21135
		private MailboxId mailboxId;

		// Token: 0x04005290 RID: 21136
		private int occurrenceInstanceIndex;
	}
}
