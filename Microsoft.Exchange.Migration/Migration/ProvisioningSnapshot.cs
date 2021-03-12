using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ProvisioningSnapshot : IStepSnapshot
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00009414 File Offset: 0x00007614
		public ProvisioningSnapshot(ProvisionedObject provisionedObject)
		{
			this.Id = (ISnapshotId)provisionedObject.ItemId;
			if (provisionedObject.Succeeded)
			{
				this.Status = SnapshotStatus.Finalized;
			}
			else
			{
				this.Status = SnapshotStatus.Failed;
				this.ErrorMessage = new LocalizedString?(new LocalizedString(provisionedObject.Error ?? string.Empty));
			}
			this.MailboxData = provisionedObject.MailboxData;
			this.InjectionCompletedTime = provisionedObject.TimeFinished;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000948C File Offset: 0x0000768C
		protected ProvisioningSnapshot()
		{
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009494 File Offset: 0x00007694
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000949C File Offset: 0x0000769C
		public ISnapshotId Id { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000094A5 File Offset: 0x000076A5
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000094AD File Offset: 0x000076AD
		public SnapshotStatus Status { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000094B6 File Offset: 0x000076B6
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000094BE File Offset: 0x000076BE
		public LocalizedString? ErrorMessage { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000094C7 File Offset: 0x000076C7
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000094CF File Offset: 0x000076CF
		public IMailboxData MailboxData { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000094D8 File Offset: 0x000076D8
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000094E0 File Offset: 0x000076E0
		public ExDateTime? InjectionCompletedTime { get; private set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x000094EC File Offset: 0x000076EC
		public static ProvisioningSnapshot CreateRemoved()
		{
			return new ProvisioningSnapshot
			{
				Status = SnapshotStatus.Removed
			};
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009508 File Offset: 0x00007708
		public static ProvisioningSnapshot CreateInProgress(ISnapshotId id)
		{
			return new ProvisioningSnapshot
			{
				Id = id,
				Status = SnapshotStatus.InProgress
			};
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000952C File Offset: 0x0000772C
		public static ProvisioningSnapshot CreateCompleted(ISnapshotId id)
		{
			return new ProvisioningSnapshot
			{
				Id = id,
				Status = SnapshotStatus.Finalized
			};
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009550 File Offset: 0x00007750
		internal static ProvisioningSnapshot CreateFromMessage(IMigrationStoreObject message, MigrationUserRecipientType recipientType)
		{
			if (recipientType != MigrationUserRecipientType.Group)
			{
				return null;
			}
			GroupProvisioningSnapshot groupProvisioningSnapshot = new GroupProvisioningSnapshot();
			groupProvisioningSnapshot.ReadFromMessageItem(message);
			return groupProvisioningSnapshot;
		}
	}
}
