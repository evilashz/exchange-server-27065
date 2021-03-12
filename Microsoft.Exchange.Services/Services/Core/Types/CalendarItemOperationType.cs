using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000704 RID: 1796
	public static class CalendarItemOperationType
	{
		// Token: 0x04001E3F RID: 7743
		public const string SendCalendarInvitationsOrCancellations = "SendCalendarInvitationsOrCancellations";

		// Token: 0x04001E40 RID: 7744
		public const string SendCalendarInvitations = "SendCalendarInvitations";

		// Token: 0x04001E41 RID: 7745
		public const string SendCalendarCancellations = "SendCalendarCancellations";

		// Token: 0x02000705 RID: 1797
		[XmlType("CalendarItemCreateOrDeleteOperationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public enum CreateOrDelete
		{
			// Token: 0x04001E43 RID: 7747
			SendToNone,
			// Token: 0x04001E44 RID: 7748
			SendOnlyToAll,
			// Token: 0x04001E45 RID: 7749
			SendToAllAndSaveCopy
		}

		// Token: 0x02000706 RID: 1798
		[XmlType("CalendarItemUpdateOperationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public enum Update
		{
			// Token: 0x04001E47 RID: 7751
			SendToNone,
			// Token: 0x04001E48 RID: 7752
			SendOnlyToAll,
			// Token: 0x04001E49 RID: 7753
			SendOnlyToChanged,
			// Token: 0x04001E4A RID: 7754
			SendToAllAndSaveCopy,
			// Token: 0x04001E4B RID: 7755
			SendToChangedAndSaveCopy
		}
	}
}
