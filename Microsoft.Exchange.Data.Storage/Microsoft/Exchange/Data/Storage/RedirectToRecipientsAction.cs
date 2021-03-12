using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B7C RID: 2940
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RedirectToRecipientsAction : RecipientAction
	{
		// Token: 0x06006A27 RID: 27175 RVA: 0x001C5DE6 File Offset: 0x001C3FE6
		private RedirectToRecipientsAction(IList<Participant> participants, Rule rule) : base(ActionType.RedirectToRecipientsAction, participants, rule)
		{
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x001C5DF4 File Offset: 0x001C3FF4
		public static RedirectToRecipientsAction Create(IList<Participant> participants, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				participants
			});
			return new RedirectToRecipientsAction(participants, rule);
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x001C5E20 File Offset: 0x001C4020
		internal override RuleAction BuildRuleAction()
		{
			List<AdrEntry> list = new List<AdrEntry>();
			for (int i = 0; i < base.Participants.Count; i++)
			{
				list.Add(Rule.AdrEntryFromParticipant(base.Participants[i]));
			}
			return new RuleAction.Forward(list.ToArray(), (RuleAction.Forward.ActionFlags)3U);
		}
	}
}
