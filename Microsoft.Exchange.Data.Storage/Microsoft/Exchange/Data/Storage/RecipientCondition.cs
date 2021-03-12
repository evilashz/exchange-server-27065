using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B95 RID: 2965
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RecipientCondition : Condition
	{
		// Token: 0x06006A87 RID: 27271 RVA: 0x001C713C File Offset: 0x001C533C
		protected RecipientCondition(ConditionType conditionType, Rule rule, IList<Participant> participants) : base(conditionType, rule)
		{
			this.participants = participants;
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x06006A88 RID: 27272 RVA: 0x001C714D File Offset: 0x001C534D
		public IList<Participant> Participants
		{
			get
			{
				return this.participants;
			}
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x001C71A4 File Offset: 0x001C53A4
		protected static void CheckParticipants(Rule rule, IList<Participant> participants)
		{
			if (participants.Count == 0)
			{
				rule.ThrowValidateException(delegate
				{
					throw new ArgumentException("No participants");
				}, "No participants");
			}
			using (IEnumerator<Participant> enumerator = participants.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Participant participant = enumerator.Current;
					if (participant.RoutingType == "MAPIPDL")
					{
						rule.ThrowValidateException(delegate
						{
							throw new ArgumentException("MAPIPDL participant:" + participant.DisplayName);
						}, "MAPIPDL participant:" + participant.DisplayName);
					}
					if (participant.ValidationStatus != ParticipantValidationStatus.NoError)
					{
						rule.ThrowValidateException(delegate
						{
							throw new ArgumentException("Invalid participant:" + participant.DisplayName);
						}, "Invalid participant:" + participant.DisplayName);
					}
				}
			}
		}

		// Token: 0x04003CE0 RID: 15584
		private readonly IList<Participant> participants;
	}
}
