using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020008F0 RID: 2288
	internal static class SendMeetingInvitations
	{
		// Token: 0x06003FB2 RID: 16306 RVA: 0x000DC310 File Offset: 0x000DA510
		public static CalendarItemOperationType.CreateOrDelete ConvertToEnum(string sendMeetingInvitationsValue)
		{
			CalendarItemOperationType.CreateOrDelete result = CalendarItemOperationType.CreateOrDelete.SendToNone;
			if (sendMeetingInvitationsValue != null)
			{
				if (!(sendMeetingInvitationsValue == "SendToNone"))
				{
					if (!(sendMeetingInvitationsValue == "SendOnlyToAll"))
					{
						if (sendMeetingInvitationsValue == "SendToAllAndSaveCopy")
						{
							result = CalendarItemOperationType.CreateOrDelete.SendToAllAndSaveCopy;
						}
					}
					else
					{
						result = CalendarItemOperationType.CreateOrDelete.SendOnlyToAll;
					}
				}
				else
				{
					result = CalendarItemOperationType.CreateOrDelete.SendToNone;
				}
			}
			return result;
		}

		// Token: 0x0400265B RID: 9819
		public const string SendToNone = "SendToNone";

		// Token: 0x0400265C RID: 9820
		public const string SendOnlyToAll = "SendOnlyToAll";

		// Token: 0x0400265D RID: 9821
		public const string SendToAllAndSaveCopy = "SendToAllAndSaveCopy";
	}
}
