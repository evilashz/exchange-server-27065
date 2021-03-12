using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class ResultsPerAttendee
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003500 File Offset: 0x00001700
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003508 File Offset: 0x00001708
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				if (value != null)
				{
					this.identity = value;
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003514 File Offset: 0x00001714
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000351C File Offset: 0x0000171C
		public SmtpAddress PrimarySMTPAddress { get; set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00003528 File Offset: 0x00001728
		public bool HasErrors()
		{
			bool flag = false;
			flag |= !string.IsNullOrEmpty(this.ErrorDescription);
			flag |= !string.IsNullOrEmpty(this.WrongStartTime);
			flag |= !string.IsNullOrEmpty(this.WrongEndTime);
			flag |= !string.IsNullOrEmpty(this.WrongLocation);
			flag |= !string.IsNullOrEmpty(this.IntentionalWrongTrackingInfo);
			flag |= !string.IsNullOrEmpty(this.WrongTrackingInfo);
			flag |= !string.IsNullOrEmpty(this.WrongTimeZone);
			flag |= !string.IsNullOrEmpty(this.CantOpen);
			flag |= !string.IsNullOrEmpty(this.Duplicates);
			flag |= !string.IsNullOrEmpty(this.IntentionalMissingMeetings);
			flag |= !string.IsNullOrEmpty(this.MissingMeetings);
			flag |= !string.IsNullOrEmpty(this.RecurrenceProblems);
			flag |= !string.IsNullOrEmpty(this.DelayedUpdatesWrongVersion);
			flag |= !string.IsNullOrEmpty(this.WrongOverlap);
			return flag | !string.IsNullOrEmpty(this.MailboxUnavailable);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003637 File Offset: 0x00001837
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000363F File Offset: 0x0000183F
		public string ErrorDescription { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003648 File Offset: 0x00001848
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003650 File Offset: 0x00001850
		public string WrongStartTime { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003659 File Offset: 0x00001859
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003661 File Offset: 0x00001861
		public string WrongEndTime { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000366A File Offset: 0x0000186A
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003672 File Offset: 0x00001872
		public string WrongLocation { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000367B File Offset: 0x0000187B
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003683 File Offset: 0x00001883
		public string WrongTrackingInfo { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000368C File Offset: 0x0000188C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003694 File Offset: 0x00001894
		public string IntentionalWrongTrackingInfo { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000369D File Offset: 0x0000189D
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000036A5 File Offset: 0x000018A5
		public string WrongTimeZone { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000036AE File Offset: 0x000018AE
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000036B6 File Offset: 0x000018B6
		public string CantOpen { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000036BF File Offset: 0x000018BF
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000036C7 File Offset: 0x000018C7
		public string Duplicates { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000036D0 File Offset: 0x000018D0
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000036D8 File Offset: 0x000018D8
		public string MissingMeetings { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000036E1 File Offset: 0x000018E1
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000036E9 File Offset: 0x000018E9
		public string IntentionalMissingMeetings { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036F2 File Offset: 0x000018F2
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000036FA File Offset: 0x000018FA
		public string RecurrenceProblems { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003703 File Offset: 0x00001903
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000370B File Offset: 0x0000190B
		public string DelayedUpdatesWrongVersion { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000371C File Offset: 0x0000191C
		public string WrongOverlap { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003725 File Offset: 0x00001925
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000372D File Offset: 0x0000192D
		public string MailboxUnavailable { get; set; }

		// Token: 0x0600007E RID: 126 RVA: 0x00003738 File Offset: 0x00001938
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} ", this.PrimarySMTPAddress);
			if (!string.IsNullOrEmpty(this.ErrorDescription))
			{
				stringBuilder.AppendFormat("Error:{0} ", this.ErrorDescription);
			}
			if (!string.IsNullOrEmpty(this.WrongStartTime))
			{
				stringBuilder.AppendFormat("WrongStart:{0} ", this.WrongStartTime);
			}
			if (!string.IsNullOrEmpty(this.WrongEndTime))
			{
				stringBuilder.AppendFormat("WrongEnd:{0} ", this.WrongEndTime);
			}
			if (!string.IsNullOrEmpty(this.WrongLocation))
			{
				stringBuilder.AppendFormat("WrongLocation:{0} ", this.WrongLocation);
			}
			if (!string.IsNullOrEmpty(this.WrongOverlap))
			{
				stringBuilder.AppendFormat("WrongOverlap:{0} ", this.WrongOverlap);
			}
			if (!string.IsNullOrEmpty(this.WrongTimeZone))
			{
				stringBuilder.AppendFormat("WrongTimeZone:{0} ", this.WrongTimeZone);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000011 RID: 17
		private ObjectId identity;
	}
}
