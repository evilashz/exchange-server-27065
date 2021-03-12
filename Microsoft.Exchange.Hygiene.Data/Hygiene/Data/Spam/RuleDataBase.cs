using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F3 RID: 499
	[Serializable]
	public class RuleDataBase : ISerializable
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00041E54 File Offset: 0x00040054
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x00041E5C File Offset: 0x0004005C
		public long RuleID { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00041E65 File Offset: 0x00040065
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x00041E6D File Offset: 0x0004006D
		public long GroupID { get; set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00041E76 File Offset: 0x00040076
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x00041E7E File Offset: 0x0004007E
		public decimal Sequence { get; set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00041E87 File Offset: 0x00040087
		// (set) Token: 0x060014EA RID: 5354 RVA: 0x00041E8F File Offset: 0x0004008F
		public RuleScopeType ScopeID { get; set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00041E98 File Offset: 0x00040098
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00041EA0 File Offset: 0x000400A0
		public bool IsActive { get; set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x00041EA9 File Offset: 0x000400A9
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x00041EB1 File Offset: 0x000400B1
		public bool IsPersistent { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x00041EBA File Offset: 0x000400BA
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00041EC2 File Offset: 0x000400C2
		public RuleStatusType? RuleStatus { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00041ECB File Offset: 0x000400CB
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x00041ED3 File Offset: 0x000400D3
		public string Comment { get; set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00041EDC File Offset: 0x000400DC
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x00041EE4 File Offset: 0x000400E4
		public int? ApprovalStatusID { get; set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00041EED File Offset: 0x000400ED
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00041EF5 File Offset: 0x000400F5
		public string ApprovedBy { get; set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00041EFE File Offset: 0x000400FE
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00041F06 File Offset: 0x00040106
		public string ModifiedBy { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00041F0F File Offset: 0x0004010F
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x00041F17 File Offset: 0x00040117
		public string DeletedBy { get; set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00041F20 File Offset: 0x00040120
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00041F28 File Offset: 0x00040128
		public DateTime? ApprovedDate { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00041F31 File Offset: 0x00040131
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x00041F39 File Offset: 0x00040139
		public long? AddedVersion { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00041F42 File Offset: 0x00040142
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x00041F4A File Offset: 0x0004014A
		public long? RemovedVersion { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00041F53 File Offset: 0x00040153
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x00041F5B File Offset: 0x0004015B
		public DateTime? CreatedDatetime { get; internal set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00041F64 File Offset: 0x00040164
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x00041F6C File Offset: 0x0004016C
		public DateTime? ChangeDatetime { get; internal set; }

		// Token: 0x06001505 RID: 5381 RVA: 0x00041F75 File Offset: 0x00040175
		public RuleDataBase()
		{
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00041F7D File Offset: 0x0004017D
		public RuleDataBase(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException("RuleDataBaseSerializer");
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00041F8F File Offset: 0x0004018F
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException("RuleDataBaseGetObjectData");
		}
	}
}
