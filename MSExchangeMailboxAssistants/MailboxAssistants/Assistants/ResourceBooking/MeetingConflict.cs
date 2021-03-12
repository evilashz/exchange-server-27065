using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x02000120 RID: 288
	internal class MeetingConflict : IComparable<MeetingConflict>
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0004C7C0 File Offset: 0x0004A9C0
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0004C7C8 File Offset: 0x0004A9C8
		public string OrganizerDisplay
		{
			get
			{
				return this.organizerDisplay;
			}
			set
			{
				this.organizerDisplay = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0004C7DB File Offset: 0x0004A9DB
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0004C7E3 File Offset: 0x0004A9E3
		public string OrganizerSmtp
		{
			get
			{
				return this.organizerSmtp;
			}
			set
			{
				this.organizerSmtp = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0004C7F8 File Offset: 0x0004A9F8
		int IComparable<MeetingConflict>.CompareTo(MeetingConflict other)
		{
			int num = this.MeetingStartTimeLocal.CompareTo(other.MeetingStartTimeLocal);
			if (num == 0)
			{
				num = this.OrganizerDisplay.CompareTo(other.OrganizerDisplay);
			}
			if (num == 0)
			{
				num = this.OrganizerSmtp.CompareTo(other.OrganizerSmtp);
			}
			return num;
		}

		// Token: 0x04000731 RID: 1841
		private string organizerDisplay = string.Empty;

		// Token: 0x04000732 RID: 1842
		private string organizerSmtp = string.Empty;

		// Token: 0x04000733 RID: 1843
		public DateTime MeetingStartTimeLocal;

		// Token: 0x04000734 RID: 1844
		public DateTime MeetingEndTimeLocal;
	}
}
