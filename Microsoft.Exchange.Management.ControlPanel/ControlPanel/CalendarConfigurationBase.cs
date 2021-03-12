using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200005E RID: 94
	[DataContract]
	public class CalendarConfigurationBase : BaseRow
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x00053FE9 File Offset: 0x000521E9
		public CalendarConfigurationBase(MailboxCalendarConfiguration mailboxCalendarConfiguration) : base(mailboxCalendarConfiguration)
		{
			this.MailboxCalendarConfiguration = mailboxCalendarConfiguration;
		}

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00053FF9 File Offset: 0x000521F9
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x00054001 File Offset: 0x00052201
		public MailboxCalendarConfiguration MailboxCalendarConfiguration { get; private set; }
	}
}
