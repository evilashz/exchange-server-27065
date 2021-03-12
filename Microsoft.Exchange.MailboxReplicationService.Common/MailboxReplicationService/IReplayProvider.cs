using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200025F RID: 607
	internal interface IReplayProvider
	{
		// Token: 0x06001EDF RID: 7903
		void MarkAsRead(IReadOnlyCollection<MarkAsReadAction> actions);

		// Token: 0x06001EE0 RID: 7904
		void MarkAsUnRead(IReadOnlyCollection<MarkAsUnReadAction> actions);

		// Token: 0x06001EE1 RID: 7905
		IReadOnlyCollection<MoveActionResult> Move(IReadOnlyCollection<MoveAction> actions);

		// Token: 0x06001EE2 RID: 7906
		void Send(SendAction action);

		// Token: 0x06001EE3 RID: 7907
		void Delete(IReadOnlyCollection<DeleteAction> actions);

		// Token: 0x06001EE4 RID: 7908
		void Flag(IReadOnlyCollection<FlagAction> actions);

		// Token: 0x06001EE5 RID: 7909
		void FlagClear(IReadOnlyCollection<FlagClearAction> actions);

		// Token: 0x06001EE6 RID: 7910
		void FlagComplete(IReadOnlyCollection<FlagCompleteAction> actions);

		// Token: 0x06001EE7 RID: 7911
		IReadOnlyCollection<CreateCalendarEventActionResult> CreateCalendarEvent(IReadOnlyCollection<CreateCalendarEventAction> actions);

		// Token: 0x06001EE8 RID: 7912
		void UpdateCalendarEvent(IReadOnlyCollection<UpdateCalendarEventAction> actions);
	}
}
