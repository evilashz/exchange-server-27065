using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020008F1 RID: 2289
	internal static class SendMeetingInvitationsOrCancellations
	{
		// Token: 0x06003FB3 RID: 16307 RVA: 0x000DC358 File Offset: 0x000DA558
		public static CalendarItemOperationType.Update ConvertToEnum(string sendMeetingInvitationsOrCancellationsValue)
		{
			CalendarItemOperationType.Update result = CalendarItemOperationType.Update.SendToNone;
			if (sendMeetingInvitationsOrCancellationsValue != null)
			{
				if (!(sendMeetingInvitationsOrCancellationsValue == "SendToNone"))
				{
					if (!(sendMeetingInvitationsOrCancellationsValue == "SendOnlyToAll"))
					{
						if (!(sendMeetingInvitationsOrCancellationsValue == "SendOnlyToChanged"))
						{
							if (!(sendMeetingInvitationsOrCancellationsValue == "SendToAllAndSaveCopy"))
							{
								if (sendMeetingInvitationsOrCancellationsValue == "SendToChangedAndSaveCopy")
								{
									result = CalendarItemOperationType.Update.SendToChangedAndSaveCopy;
								}
							}
							else
							{
								result = CalendarItemOperationType.Update.SendToAllAndSaveCopy;
							}
						}
						else
						{
							result = CalendarItemOperationType.Update.SendOnlyToChanged;
						}
					}
					else
					{
						result = CalendarItemOperationType.Update.SendOnlyToAll;
					}
				}
				else
				{
					result = CalendarItemOperationType.Update.SendToNone;
				}
			}
			return result;
		}

		// Token: 0x0400265E RID: 9822
		public const string SendToNone = "SendToNone";

		// Token: 0x0400265F RID: 9823
		public const string SendOnlyToAll = "SendOnlyToAll";

		// Token: 0x04002660 RID: 9824
		public const string SendOnlyToChanged = "SendOnlyToChanged";

		// Token: 0x04002661 RID: 9825
		public const string SendToAllAndSaveCopy = "SendToAllAndSaveCopy";

		// Token: 0x04002662 RID: 9826
		public const string SendToChangedAndSaveCopy = "SendToChangedAndSaveCopy";
	}
}
