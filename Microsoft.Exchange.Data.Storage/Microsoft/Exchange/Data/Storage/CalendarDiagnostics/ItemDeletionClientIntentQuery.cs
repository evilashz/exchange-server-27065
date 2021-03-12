using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000368 RID: 872
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ItemDeletionClientIntentQuery : ClientIntentQuery
	{
		// Token: 0x060026A7 RID: 9895 RVA: 0x0009AE49 File Offset: 0x00099049
		public ItemDeletionClientIntentQuery(GlobalObjectId globalObjectId, ICalendarItemStateDefinition targetState) : base(globalObjectId, targetState)
		{
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0009AEC0 File Offset: 0x000990C0
		public override ClientIntentQuery.QueryResult Execute(MailboxSession session, CalendarVersionStoreGateway cvsGateway)
		{
			ClientIntentFlags targetVersionClientIntent = ClientIntentFlags.None;
			bool foundVersionInTargetState = false;
			VersionedId sourceVersionId = null;
			base.RunQuery(session, cvsGateway, delegate(PropertyBag propertyBag)
			{
				CalendarItemState state = new CalendarItemState();
				if (this.TargetState.Evaluate(state, propertyBag, session))
				{
					targetVersionClientIntent = this.GetClientIntentFromPropertyBag(propertyBag);
					sourceVersionId = this.GetIdFromPropertyBag(propertyBag);
					foundVersionInTargetState = true;
				}
				return !foundVersionInTargetState;
			});
			return new ClientIntentQuery.QueryResult(foundVersionInTargetState ? new ClientIntentFlags?(targetVersionClientIntent) : null, sourceVersionId);
		}
	}
}
