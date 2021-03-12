using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.CalendarDiagnostics
{
	// Token: 0x02000367 RID: 871
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TransitionalClientIntentQuery : ClientIntentQuery
	{
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x0009ABFF File Offset: 0x00098DFF
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x0009AC07 File Offset: 0x00098E07
		public ICalendarItemStateDefinition InitialState { get; private set; }

		// Token: 0x060026A5 RID: 9893 RVA: 0x0009AC10 File Offset: 0x00098E10
		public TransitionalClientIntentQuery(GlobalObjectId globalObjectId, ICalendarItemStateDefinition initialState, ICalendarItemStateDefinition targetState) : base(globalObjectId, targetState)
		{
			Util.ThrowOnNullArgument(initialState, "initialState");
			if (!initialState.SchemaKey.Equals(targetState.SchemaKey))
			{
				throw new ArgumentException(string.Format("Cannot query client intent for a transition between heterogeneous states (Initial: {0}; Target: {1}).", initialState.SchemaKey, targetState.SchemaKey));
			}
			this.InitialState = initialState;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0009ADBC File Offset: 0x00098FBC
		public override ClientIntentQuery.QueryResult Execute(MailboxSession session, CalendarVersionStoreGateway cvsGateway)
		{
			bool isChainComplete = false;
			CalendarItemState previousState = null;
			bool foundVersionInTargetState = false;
			VersionedId sourceVersionId = null;
			ClientIntentFlags accumulatedFlags = ClientIntentFlags.None;
			ClientIntentFlags intentToAccumulate = ClientIntentFlags.None;
			base.RunQuery(session, cvsGateway, delegate(PropertyBag propertyBag)
			{
				CalendarItemState calendarItemState = new CalendarItemState();
				if (!foundVersionInTargetState)
				{
					if (this.TargetState.Evaluate(calendarItemState, propertyBag, session))
					{
						foundVersionInTargetState = true;
						intentToAccumulate = this.GetClientIntentFromPropertyBag(propertyBag);
						accumulatedFlags = intentToAccumulate;
					}
				}
				else if (this.InitialState.Evaluate(calendarItemState, propertyBag, session))
				{
					isChainComplete = true;
					accumulatedFlags &= intentToAccumulate;
					sourceVersionId = this.GetIdFromPropertyBag(propertyBag);
				}
				else if (this.TargetState.Evaluate(calendarItemState, propertyBag, session))
				{
					intentToAccumulate = this.GetClientIntentFromPropertyBag(propertyBag);
					accumulatedFlags = intentToAccumulate;
				}
				else if (this.TargetState.Equals(calendarItemState, previousState))
				{
					intentToAccumulate = this.GetClientIntentFromPropertyBag(propertyBag);
				}
				else
				{
					accumulatedFlags &= intentToAccumulate;
					intentToAccumulate = this.GetClientIntentFromPropertyBag(propertyBag);
				}
				previousState = calendarItemState;
				return !isChainComplete;
			});
			return new ClientIntentQuery.QueryResult(isChainComplete ? new ClientIntentFlags?(accumulatedFlags) : null, sourceVersionId);
		}
	}
}
