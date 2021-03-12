using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000721 RID: 1825
	public interface IMailboxUrls
	{
		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x06005676 RID: 22134
		string InboxUrl { get; }

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x06005677 RID: 22135
		string CalendarUrl { get; }

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x06005678 RID: 22136
		string PeopleUrl { get; }

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x06005679 RID: 22137
		string PhotoUrl { get; }

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x0600567A RID: 22138
		string OwaUrl { get; }
	}
}
