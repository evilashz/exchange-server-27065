using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B96 RID: 2966
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FromRecipientsCondition : RecipientCondition
	{
		// Token: 0x06006A8B RID: 27275 RVA: 0x001C72A8 File Offset: 0x001C54A8
		private FromRecipientsCondition(Rule rule, IList<Participant> participants) : base(ConditionType.FromRecipientsCondition, rule, participants)
		{
		}

		// Token: 0x06006A8C RID: 27276 RVA: 0x001C72B4 File Offset: 0x001C54B4
		public static FromRecipientsCondition Create(Rule rule, IList<Participant> participants)
		{
			Condition.CheckParams(new object[]
			{
				rule,
				participants
			});
			RecipientCondition.CheckParticipants(rule, participants);
			return new FromRecipientsCondition(rule, participants);
		}

		// Token: 0x06006A8D RID: 27277 RVA: 0x001C72E4 File Offset: 0x001C54E4
		internal override Restriction BuildRestriction()
		{
			return Condition.CreateFromRestriction(base.Participants);
		}
	}
}
