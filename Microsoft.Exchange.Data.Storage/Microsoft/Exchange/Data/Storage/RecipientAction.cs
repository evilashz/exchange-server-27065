using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B78 RID: 2936
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RecipientAction : ActionBase
	{
		// Token: 0x06006A1B RID: 27163 RVA: 0x001C5C36 File Offset: 0x001C3E36
		protected RecipientAction(ActionType actionType, IList<Participant> participants, Rule rule) : base(actionType, rule)
		{
			this.participants = participants;
		}

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06006A1C RID: 27164 RVA: 0x001C5C47 File Offset: 0x001C3E47
		public IList<Participant> Participants
		{
			get
			{
				return this.participants;
			}
		}

		// Token: 0x04003C68 RID: 15464
		private readonly IList<Participant> participants;
	}
}
