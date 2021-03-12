using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x02000102 RID: 258
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMoveHistory
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x00015E58 File Offset: 0x00014058
		public SoftDeletedMoveHistory(int badItemsEncountered, int largeItemsEncountered, int missingItemsEncountered)
		{
			this.BadItemsEncountered = badItemsEncountered;
			this.LargeItemsEncountered = largeItemsEncountered;
			this.MissingItemsEncountered = missingItemsEncountered;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00015E75 File Offset: 0x00014075
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00015E7D File Offset: 0x0001407D
		[DataMember]
		public int BadItemsEncountered { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00015E86 File Offset: 0x00014086
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00015E8E File Offset: 0x0001408E
		[DataMember]
		public int LargeItemsEncountered { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00015E97 File Offset: 0x00014097
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x00015E9F File Offset: 0x0001409F
		[DataMember]
		public int MissingItemsEncountered { get; private set; }

		// Token: 0x060007A8 RID: 1960 RVA: 0x00015EC8 File Offset: 0x000140C8
		public static SoftDeletedMoveHistory GetHistoryForSourceDatabase(Guid mailboxGuid, Guid currentDatabase, Guid sourceDatabaseGuid)
		{
			List<MoveHistoryEntryInternal> source = MoveHistoryEntryInternal.LoadMoveHistory(mailboxGuid, currentDatabase, UserMailboxFlags.None);
			MoveHistoryEntryInternal moveHistoryEntryInternal = source.FirstOrDefault((MoveHistoryEntryInternal m) => m.SourceDatabase.ObjectGuid == sourceDatabaseGuid);
			if (moveHistoryEntryInternal == null)
			{
				return null;
			}
			return new SoftDeletedMoveHistory(moveHistoryEntryInternal.BadItemsEncountered, moveHistoryEntryInternal.LargeItemsEncountered, moveHistoryEntryInternal.MissingItemsEncountered);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00015F1A File Offset: 0x0001411A
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((SoftDeletedMoveHistory)obj)));
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00015F54 File Offset: 0x00014154
		public override int GetHashCode()
		{
			int num = this.BadItemsEncountered;
			num = (num * 397 ^ this.LargeItemsEncountered);
			return num * 397 ^ this.MissingItemsEncountered;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00015F87 File Offset: 0x00014187
		protected bool Equals(SoftDeletedMoveHistory other)
		{
			return this.BadItemsEncountered == other.BadItemsEncountered && this.LargeItemsEncountered == other.LargeItemsEncountered && this.MissingItemsEncountered == other.MissingItemsEncountered;
		}
	}
}
