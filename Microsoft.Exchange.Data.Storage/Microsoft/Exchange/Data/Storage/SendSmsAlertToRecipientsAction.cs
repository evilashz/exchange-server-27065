using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B7B RID: 2939
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SendSmsAlertToRecipientsAction : RecipientAction
	{
		// Token: 0x06006A23 RID: 27171 RVA: 0x001C5D5E File Offset: 0x001C3F5E
		private SendSmsAlertToRecipientsAction(IList<Participant> participants, Rule rule) : base(ActionType.SendSmsAlertToRecipientsAction, participants, rule)
		{
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x001C5D6C File Offset: 0x001C3F6C
		public static SendSmsAlertToRecipientsAction Create(IList<Participant> participants, Rule rule)
		{
			ActionBase.CheckParams(new object[]
			{
				rule,
				participants
			});
			return new SendSmsAlertToRecipientsAction(participants, rule);
		}

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x06006A25 RID: 27173 RVA: 0x001C5D95 File Offset: 0x001C3F95
		public override Rule.ProviderIdEnum ProviderId
		{
			get
			{
				return Rule.ProviderIdEnum.Exchange14;
			}
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x001C5D98 File Offset: 0x001C3F98
		internal override RuleAction BuildRuleAction()
		{
			List<AdrEntry> list = new List<AdrEntry>();
			for (int i = 0; i < base.Participants.Count; i++)
			{
				list.Add(Rule.AdrEntryFromParticipant(base.Participants[i]));
			}
			return new RuleAction.Forward(list.ToArray(), RuleAction.Forward.ActionFlags.SendSmsAlert);
		}
	}
}
