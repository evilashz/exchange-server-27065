using System;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;
using Microsoft.Exchange.Entities.Serialization;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x0200006C RID: 108
	internal class SeriesInlineInterop : SeriesInteropCommand
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000AAF9 File Offset: 0x00008CF9
		public SeriesInlineInterop(ICalendarInteropSeriesAction actionToPropagate, ICalendarInteropLog logger) : base(EventSeriesPropagationConfig.GetInlinePropagationConfig(), logger ?? CalendarInteropLog.Default)
		{
			this.actionToPropagate = actionToPropagate;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000AB17 File Offset: 0x00008D17
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.SeriesInlineInteropTracer;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000AB1E File Offset: 0x00008D1E
		public override Event CleanUp(Event master)
		{
			return this.actionToPropagate.CleanUp(master);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public override void ExecuteOnInstance(Event seriesInstance)
		{
			if (this.ShouldExecuteOnInstance(seriesInstance))
			{
				this.actionToPropagate.ExecuteOnInstance(seriesInstance);
				return;
			}
			this.propagationWasSkippedOnSomeInstances = true;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000AB4B File Offset: 0x00008D4B
		protected override bool IsPropagationComplete()
		{
			return base.IsPropagationComplete() && !this.propagationWasSkippedOnSomeInstances;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000AB60 File Offset: 0x00008D60
		protected override Event InitialMasterOperation()
		{
			string rawData = EntitySerializer.Serialize<ICalendarInteropSeriesAction>(this.actionToPropagate);
			Event initialMasterValue = this.actionToPropagate.GetInitialMasterValue();
			IActionQueue actionQueue = initialMasterValue;
			ActionInfo actionInfo = new ActionInfo(this.actionToPropagate.CommandId, DateTime.UtcNow, this.actionToPropagate.GetType().Name, rawData);
			actionQueue.ActionsToAdd = new ActionInfo[]
			{
				actionInfo
			};
			Event @event = base.ExecuteOnMasterWithConflictRetries(new Func<Event, Event>(this.actionToPropagate.InitialMasterOperation), initialMasterValue, true);
			this.precedingAction = SeriesInlineInterop.GetPrecedingActionFromQueue(@event);
			return @event;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		protected override bool ShouldExecuteOnInstance(Event seriesInstance)
		{
			return ((IActionPropagationState)seriesInstance).LastExecutedAction == this.precedingAction || (this.precedingAction == null && ((IActionPropagationState)seriesInstance).LastExecutedAction != null && ((IActionPropagationState)seriesInstance).LastExecutedAction.Value != this.actionToPropagate.CommandId);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000AC84 File Offset: 0x00008E84
		private static Guid? GetPrecedingActionFromQueue(IActionQueue queue)
		{
			if (queue.OriginalActions != null && queue.OriginalActions.Length > 1)
			{
				return new Guid?(queue.OriginalActions[queue.OriginalActions.Length - 2].Id);
			}
			return null;
		}

		// Token: 0x040000CD RID: 205
		private readonly ICalendarInteropSeriesAction actionToPropagate;

		// Token: 0x040000CE RID: 206
		private Guid? precedingAction;

		// Token: 0x040000CF RID: 207
		private bool propagationWasSkippedOnSomeInstances;
	}
}
