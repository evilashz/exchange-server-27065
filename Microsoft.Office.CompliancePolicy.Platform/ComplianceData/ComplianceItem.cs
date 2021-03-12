using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.CompliancePolicy.PolicyEvaluation;

namespace Microsoft.Office.CompliancePolicy.ComplianceData
{
	// Token: 0x02000054 RID: 84
	public abstract class ComplianceItem
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000644C File Offset: 0x0000464C
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00006454 File Offset: 0x00004654
		public virtual string Id { get; protected set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000645D File Offset: 0x0000465D
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00006465 File Offset: 0x00004665
		public virtual string Extension { get; protected set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000646E File Offset: 0x0000466E
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00006476 File Offset: 0x00004676
		public virtual string DisplayName { get; protected set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000647F File Offset: 0x0000467F
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00006487 File Offset: 0x00004687
		public virtual string WorkloadIdentifier { get; protected set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006490 File Offset: 0x00004690
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00006498 File Offset: 0x00004698
		public virtual DateTime WhenCreated { get; protected set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000064A1 File Offset: 0x000046A1
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000064A9 File Offset: 0x000046A9
		public virtual DateTime WhenLastModified { get; protected set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000064B2 File Offset: 0x000046B2
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000064BA File Offset: 0x000046BA
		public virtual string Creator { get; protected set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000064C3 File Offset: 0x000046C3
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000064CB File Offset: 0x000046CB
		public virtual string LastModifier { get; protected set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000064D4 File Offset: 0x000046D4
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x000064DC File Offset: 0x000046DC
		public virtual bool IsDirty { get; protected set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000064E5 File Offset: 0x000046E5
		// (set) Token: 0x060001EA RID: 490 RVA: 0x000064ED File Offset: 0x000046ED
		public virtual DateTime ExpiryTime { get; protected set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000064F6 File Offset: 0x000046F6
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000064FE File Offset: 0x000046FE
		public virtual IList<Guid> ClassificationScanned { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006507 File Offset: 0x00004707
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000650F File Offset: 0x0000470F
		public virtual IDictionary<Guid, ClassificationResult> ClassificationDiscovered { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006518 File Offset: 0x00004718
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00006520 File Offset: 0x00004720
		public virtual ComplianceItemStatusFlag Status { get; protected set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00006529 File Offset: 0x00004729
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00006531 File Offset: 0x00004731
		public virtual AuditState AuditState { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000653A File Offset: 0x0000473A
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00006542 File Offset: 0x00004742
		public virtual AccessScope AccessScope { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000654B File Offset: 0x0000474B
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00006553 File Offset: 0x00004753
		public virtual IDictionary<string, List<string>> Metadata { get; set; }

		// Token: 0x060001F7 RID: 503 RVA: 0x0000655C File Offset: 0x0000475C
		public virtual Stream OpenBodyReadStream()
		{
			return null;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000655F File Offset: 0x0000475F
		public virtual Stream OpenExtractedBodyTextReadStream()
		{
			return null;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006562 File Offset: 0x00004762
		public virtual Stream OpenExtractedAttachmentsTextReadStream()
		{
			return null;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006565 File Offset: 0x00004765
		public void Save()
		{
			if (!this.IsDirty)
			{
				throw new InvalidOperationException("Cannot call Save if the item is not dirty.");
			}
			this.InternalSave();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006580 File Offset: 0x00004780
		public virtual void InternalSave()
		{
		}

		// Token: 0x040000F1 RID: 241
		public const string ExMessageExtensionName = "ExMessage";
	}
}
