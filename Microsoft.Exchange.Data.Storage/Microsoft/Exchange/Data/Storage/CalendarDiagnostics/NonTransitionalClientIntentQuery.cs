using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000366 RID: 870
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NonTransitionalClientIntentQuery : ClientIntentQuery
	{
		// Token: 0x060026A1 RID: 9889 RVA: 0x0009AAFA File Offset: 0x00098CFA
		public NonTransitionalClientIntentQuery(GlobalObjectId globalObjectId, ICalendarItemStateDefinition targetState) : base(globalObjectId, targetState)
		{
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0009AB80 File Offset: 0x00098D80
		public override ClientIntentQuery.QueryResult Execute(MailboxSession session, CalendarVersionStoreGateway cvsGateway)
		{
			ClientIntentFlags targetVersionClientIntent = ClientIntentFlags.None;
			bool foundShiftToTargetState = false;
			bool foundVersionInTargetState = false;
			VersionedId sourceVersionId = null;
			base.RunQuery(session, cvsGateway, delegate(PropertyBag propertyBag)
			{
				CalendarItemState state = new CalendarItemState();
				if (this.TargetState.Evaluate(state, propertyBag, session))
				{
					foundVersionInTargetState = true;
					targetVersionClientIntent = this.GetClientIntentFromPropertyBag(propertyBag);
				}
				else if (foundVersionInTargetState)
				{
					foundShiftToTargetState = true;
					sourceVersionId = this.GetIdFromPropertyBag(propertyBag);
				}
				return !foundShiftToTargetState;
			});
			return new ClientIntentQuery.QueryResult(foundShiftToTargetState ? new ClientIntentFlags?(targetVersionClientIntent) : null, sourceVersionId);
		}
	}
}
