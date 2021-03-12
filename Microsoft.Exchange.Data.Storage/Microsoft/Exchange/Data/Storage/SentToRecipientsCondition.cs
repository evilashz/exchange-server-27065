using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B97 RID: 2967
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SentToRecipientsCondition : RecipientCondition
	{
		// Token: 0x06006A8E RID: 27278 RVA: 0x001C72F1 File Offset: 0x001C54F1
		private SentToRecipientsCondition(Rule rule, IList<Participant> participants) : base(ConditionType.SentToRecipientsCondition, rule, participants)
		{
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x001C7300 File Offset: 0x001C5500
		public static SentToRecipientsCondition Create(Rule rule, IList<Participant> participants)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				participants
			});
			RecipientCondition.CheckParticipants(rule, participants);
			return new SentToRecipientsCondition(rule, participants);
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x001C7330 File Offset: 0x001C5530
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateRecipientRestriction(base.Participants);
		}
	}
}
