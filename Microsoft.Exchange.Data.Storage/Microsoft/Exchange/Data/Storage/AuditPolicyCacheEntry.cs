using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F24 RID: 3876
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditPolicyCacheEntry : IUpdatableItem
	{
		// Token: 0x17002348 RID: 9032
		// (get) Token: 0x06008547 RID: 34119 RVA: 0x00247490 File Offset: 0x00245690
		// (set) Token: 0x06008548 RID: 34120 RVA: 0x00247498 File Offset: 0x00245698
		public PolicyLoadStatus LoadStatus { get; private set; }

		// Token: 0x17002349 RID: 9033
		// (get) Token: 0x06008549 RID: 34121 RVA: 0x002474A1 File Offset: 0x002456A1
		// (set) Token: 0x0600854A RID: 34122 RVA: 0x002474A9 File Offset: 0x002456A9
		public MailboxAuditOperations AuditOperationsDelegate { get; private set; }

		// Token: 0x0600854B RID: 34123 RVA: 0x002474B2 File Offset: 0x002456B2
		public AuditPolicyCacheEntry(MailboxAuditOperations auditDelegateOperations = MailboxAuditOperations.None, PolicyLoadStatus loadStatus = PolicyLoadStatus.Unknown)
		{
			this.AuditOperationsDelegate = auditDelegateOperations;
			this.LoadStatus = loadStatus;
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x002474C8 File Offset: 0x002456C8
		public bool IsExist()
		{
			return this.LoadStatus == PolicyLoadStatus.FailedToLoad || this.LoadStatus == PolicyLoadStatus.Loaded;
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x002474E0 File Offset: 0x002456E0
		public bool UpdateWith(IUpdatableItem newItem)
		{
			AuditPolicyCacheEntry auditPolicyCacheEntry = newItem as AuditPolicyCacheEntry;
			if (auditPolicyCacheEntry == null)
			{
				return false;
			}
			if (AuditPolicyCacheEntry.CanUpdate(auditPolicyCacheEntry.LoadStatus, this.LoadStatus))
			{
				this.LoadStatus = auditPolicyCacheEntry.LoadStatus;
				this.AuditOperationsDelegate = auditPolicyCacheEntry.AuditOperationsDelegate;
				return true;
			}
			return false;
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x00247527 File Offset: 0x00245727
		public static bool CanUpdate(PolicyLoadStatus loadStatus, PolicyLoadStatus cachedStatus)
		{
			return AuditPolicyCacheEntry.CanUpdateCachedEntry[(int)loadStatus][(int)cachedStatus];
		}

		// Token: 0x0600854F RID: 34127 RVA: 0x00247544 File Offset: 0x00245744
		// Note: this type is marked as 'beforefieldinit'.
		static AuditPolicyCacheEntry()
		{
			bool[][] array = new bool[4][];
			bool[][] array2 = array;
			int num = 0;
			bool[] array3 = new bool[4];
			array2[num] = array3;
			array[1] = new bool[]
			{
				true,
				false,
				true,
				true
			};
			bool[][] array4 = array;
			int num2 = 2;
			bool[] array5 = new bool[4];
			array5[0] = true;
			array5[1] = true;
			array4[num2] = array5;
			array[3] = new bool[]
			{
				true,
				true,
				true,
				true
			};
			AuditPolicyCacheEntry.CanUpdateCachedEntry = array;
		}

		// Token: 0x0400594F RID: 22863
		private static readonly bool[][] CanUpdateCachedEntry;
	}
}
