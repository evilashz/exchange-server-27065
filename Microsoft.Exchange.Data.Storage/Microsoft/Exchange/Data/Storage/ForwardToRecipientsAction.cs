using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B79 RID: 2937
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ForwardToRecipientsAction : RecipientAction
	{
		// Token: 0x06006A1D RID: 27165 RVA: 0x001C5C4F File Offset: 0x001C3E4F
		private ForwardToRecipientsAction(IList<Participant> participants, Rule rule) : base(ActionType.ForwardToRecipientsAction, participants, rule)
		{
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x001C5C5C File Offset: 0x001C3E5C
		public static ForwardToRecipientsAction Create(IList<Participant> participants, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				participants
			});
			return new ForwardToRecipientsAction(participants, rule);
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x001C5C88 File Offset: 0x001C3E88
		internal override RuleAction BuildRuleAction()
		{
			List<AdrEntry> list = new List<AdrEntry>();
			for (int i = 0; i < base.Participants.Count; i++)
			{
				list.Add(Rule.AdrEntryFromParticipant(base.Participants[i]));
			}
			return new RuleAction.Forward(list.ToArray(), RuleAction.Forward.ActionFlags.None);
		}
	}
}
