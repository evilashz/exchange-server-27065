using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B7A RID: 2938
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ForwardAsAttachmentToRecipientsAction : RecipientAction
	{
		// Token: 0x06006A20 RID: 27168 RVA: 0x001C5CD6 File Offset: 0x001C3ED6
		private ForwardAsAttachmentToRecipientsAction(IList<Participant> participants, Rule rule) : base(ActionType.ForwardAsAttachmentToRecipientsAction, participants, rule)
		{
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x001C5CE4 File Offset: 0x001C3EE4
		public static ForwardAsAttachmentToRecipientsAction Create(IList<Participant> participants, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				participants
			});
			return new ForwardAsAttachmentToRecipientsAction(participants, rule);
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x001C5D10 File Offset: 0x001C3F10
		internal override RuleAction BuildRuleAction()
		{
			List<AdrEntry> list = new List<AdrEntry>();
			for (int i = 0; i < base.Participants.Count; i++)
			{
				list.Add(Rule.AdrEntryFromParticipant(base.Participants[i]));
			}
			return new RuleAction.Forward(list.ToArray(), RuleAction.Forward.ActionFlags.ForwardAsAttachment);
		}
	}
}
