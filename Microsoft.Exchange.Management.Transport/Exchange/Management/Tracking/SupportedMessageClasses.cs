using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x0200009D RID: 157
	internal static class SupportedMessageClasses
	{
		// Token: 0x040001F7 RID: 503
		public static HashSet<string> Classes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"IPM.Note",
			"IPM.Schedule.Meeting.Canceled",
			"IPM.Schedule.Meeting.Request",
			"IPM.Schedule.Meeting.Resp.Pos",
			"IPM.Schedule.Meeting.Resp.Tent",
			"IPM.Schedule.Meeting.Resp.Neg",
			"IPM.Note.Microsoft.Approval.Reply.Approve",
			"IPM.Note.Microsoft.Approval.Reply.Reject",
			"IPM.Note.SMIME",
			"IPM.Note.MultipartSigned",
			"IPM.Note.SMIME.MultipartSigned",
			"IPM.Note.Secure",
			"IPM.Note.Sign",
			"IPM.TaskRequest",
			"IPM.TaskRequest.Accept",
			"IPM.TaskRequest.Decline",
			"IPM.TaskRequest.Update"
		};
	}
}
