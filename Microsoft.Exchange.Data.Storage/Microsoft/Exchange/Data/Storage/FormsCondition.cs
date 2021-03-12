﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B8F RID: 2959
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FormsCondition : StringCondition
	{
		// Token: 0x06006A77 RID: 27255 RVA: 0x001C6CA4 File Offset: 0x001C4EA4
		private static HashSet<string> BuildFormTypeSet()
		{
			return new HashSet<string>
			{
				"IPM.Schedule.Meeting.Resp.Pos",
				"IPM.Appointment",
				"IPM.Note.Microsoft.Approval.Request",
				"IPM.Conflict",
				"IPM.Conflict.Resolution.Message",
				"IPM.Contact",
				"IPM.Schedule.Meeting.Resp.Neg",
				"IPM.Note.Secure.Sign",
				"IPM.Note" + "." + "SMIME.SignedEncrypted",
				"IPM.Note" + "." + "SMIME.Encrypted",
				"IPM.DistList",
				"IPM.Document",
				"IPM.Note.Secure",
				"IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}",
				"IPM.Note.Microsoft.Voicemail.UM.CA",
				"IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA",
				"IPM.Note.rpmsg.Microsoft.Voicemail.UM",
				"IPM.Note.Microsoft.Voicemail.UM",
				"IPM.Note.Microsoft.Missed.Voice",
				"IPM.InfoPathForm",
				"IPM.Activity",
				"IPM.Appointment.Live Meeting Request",
				"IPM.Schedule.Meeting.Canceled",
				"IPM.Schedule.Meeting.Request",
				"IPM.Schedule.Meeting.Resp.Pos",
				"IPM.Schedule.Meeting.Resp.Neg",
				"IPM.Schedule.Meeting.Resp.Tent",
				"IPM.Note",
				"IPM.Recall.Report",
				"IPM.Note.Mobile.MMS",
				"IPM.StickyNote",
				"IPM.Note.Rules.OofTemplate.Microsoft",
				"IPM.Post",
				"IPM.Outlook.Recall",
				"IPM.Remote",
				"REPORT",
				"IPM.Resend",
				"IPM.Post.RSS",
				"IPM.Note.Rules.ReplyTemplate.Microsoft",
				"IPM.Sharing",
				"IPM.Note.SMIME.MultipartSigned",
				"IPM.Note.SMIME",
				"IPM.Note.RECEIPT.SMIME",
				"IPM",
				"IPM.Task",
				"IPM.TaskRequest.Accept",
				"IPM.TaskRequest.Decline",
				"IPM.TaskRequest",
				"IPM.TaskRequest.Update",
				"IPM.Schedule.Meeting.Resp.Tent",
				"IPM.Note.Mobile.SMS",
				"IPM.Note.Reminder.Event",
				"IPM.Note.QuickCapture",
				"IPM.Note.Reminder.Modern"
			};
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x001C6F5E File Offset: 0x001C515E
		protected FormsCondition(ConditionType conditionType, Rule rule, string[] text) : base(conditionType, rule, text)
		{
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x001C6F6C File Offset: 0x001C516C
		public static FormsCondition Create(ConditionType conditionType, Rule rule, string[] text)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				text
			});
			return new FormsCondition(conditionType, rule, text);
		}

		// Token: 0x06006A7A RID: 27258 RVA: 0x001C6F98 File Offset: 0x001C5198
		internal override Restriction BuildRestriction()
		{
			Restriction restriction = Condition.CreateORStringContentRestriction(base.Text, PropTag.MessageClass, ContentFlags.Prefix | ContentFlags.IgnoreCase | ContentFlags.Loose);
			if (Restriction.ResType.Content == restriction.Type)
			{
				return Restriction.Or(new Restriction[]
				{
					restriction
				});
			}
			return restriction;
		}

		// Token: 0x04003CDF RID: 15583
		internal static HashSet<string> FormTypeSet = FormsCondition.BuildFormTypeSet();
	}
}
