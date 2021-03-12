using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200003C RID: 60
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingComparisonResult : IEnumerable<ConsistencyCheckResult>, IEnumerable
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000C82C File Offset: 0x0000AA2C
		private MeetingComparisonResult()
		{
			this.checkStatusTable = new Dictionary<ConsistencyCheckType, CheckStatusType>(Enum.GetValues(typeof(ConsistencyCheckType)).Length);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C853 File Offset: 0x0000AA53
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000C85B File Offset: 0x0000AA5B
		public string AttendeePrimarySmtpAddress { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000C864 File Offset: 0x0000AA64
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public MeetingData Meeting { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000C875 File Offset: 0x0000AA75
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000C87D File Offset: 0x0000AA7D
		public RepairSteps RepairInfo { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000C886 File Offset: 0x0000AA86
		public bool InquiredMeeting
		{
			get
			{
				return this.RepairInfo.InquiredMeeting;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C893 File Offset: 0x0000AA93
		public int CheckResultCount
		{
			get
			{
				return this.CheckResultList.Count;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		public List<ConsistencyCheckResult> CheckResultList { get; private set; }

		// Token: 0x17000077 RID: 119
		internal CheckStatusType? this[ConsistencyCheckType check]
		{
			get
			{
				if (!this.checkStatusTable.ContainsKey(check))
				{
					return null;
				}
				return new CheckStatusType?(this.checkStatusTable[check]);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C8EA File Offset: 0x0000AAEA
		public void ForEachCheckResult(Action<ConsistencyCheckResult> action)
		{
			this.CheckResultList.ForEach(action);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		internal static MeetingComparisonResult CreateInstance(UserObject attendee, MeetingData meeting)
		{
			return new MeetingComparisonResult
			{
				CheckResultList = new List<ConsistencyCheckResult>(),
				RepairInfo = RepairSteps.CreateInstance(),
				AttendeePrimarySmtpAddress = ((attendee.ExchangePrincipal == null) ? attendee.EmailAddress : attendee.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()),
				Meeting = meeting
			};
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C95D File Offset: 0x0000AB5D
		internal void AddCheckResult(ConsistencyCheckResult result)
		{
			this.checkStatusTable.Add(result.CheckType, result.Status);
			this.CheckResultList.Add(result);
			this.RepairInfo.Merge(result.RepairInfo);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C993 File Offset: 0x0000AB93
		public IEnumerator<ConsistencyCheckResult> GetEnumerator()
		{
			return this.CheckResultList.GetEnumerator();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C9A5 File Offset: 0x0000ABA5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000148 RID: 328
		private Dictionary<ConsistencyCheckType, CheckStatusType> checkStatusTable;
	}
}
