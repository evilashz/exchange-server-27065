using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000069 RID: 105
	internal class SeriesPendingActionsInterop : SeriesInteropCommand
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public SeriesPendingActionsInterop(ICalendarInteropLog logger, ISeriesActionParser parser = null) : base(EventSeriesPropagationConfig.GetBackgroundPropagationConfig(), logger)
		{
			this.seriesActionParser = parser;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A4C4 File Offset: 0x000086C4
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000A4CC File Offset: 0x000086CC
		public Event Entity { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A4D5 File Offset: 0x000086D5
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.SeriesPendingActionsInteropTracer;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000A4DC File Offset: 0x000086DC
		private ISeriesActionParser SeriesActionParser
		{
			get
			{
				if (this.seriesActionParser == null)
				{
					this.seriesActionParser = new SeriesActionParser(base.Logger, this.Scope.Session);
				}
				return this.seriesActionParser;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000A508 File Offset: 0x00008708
		public override Event CleanUp(Event master)
		{
			foreach (ICalendarInteropSeriesAction calendarInteropSeriesAction in this.pendingActions)
			{
				master = calendarInteropSeriesAction.CleanUp(master);
			}
			return master;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000A560 File Offset: 0x00008760
		public override void ExecuteOnInstance(Event seriesInstance)
		{
			try
			{
				this.ApplyPendingActionsOnInstance(seriesInstance);
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.SeriesPendingActionsInteropTracer.TraceWarning<string, string>((long)this.GetHashCode(), "Series instance {0} from series {1} cannot be found any more - processing will be skipped for it.", seriesInstance.Id, this.Entity.SeriesId);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A5B0 File Offset: 0x000087B0
		protected override bool ShouldExecuteOnInstance(Event seriesInstance)
		{
			return true;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A5B4 File Offset: 0x000087B4
		protected override Event InitialMasterOperation()
		{
			Event entity = this.Entity;
			IActionQueue actionQueue = entity;
			if (actionQueue.OriginalActions != null)
			{
				bool flag = true;
				foreach (ActionInfo actionInfo in actionQueue.OriginalActions)
				{
					if (flag)
					{
						if (DateTime.UtcNow - actionInfo.Timestamp > base.InteropConfiguration.MaxActionLifetime)
						{
							this.pendingActions.Add(new MarkInstancesAsExceptionsCommand(actionInfo.Id)
							{
								EntityKey = entity.Id,
								Entity = entity
							});
						}
						else
						{
							this.pendingActions.Add(this.SeriesActionParser.DeserializeCommand(actionInfo, entity));
						}
						flag = false;
					}
					else
					{
						this.pendingActions.Add(this.SeriesActionParser.DeserializeCommand(actionInfo, entity));
					}
				}
				foreach (ICalendarInteropSeriesAction calendarInteropSeriesAction in this.pendingActions)
				{
					calendarInteropSeriesAction.RestoreExecutionContext(this.Scope, this);
				}
			}
			return entity;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A6D8 File Offset: 0x000088D8
		private void ApplyPendingActionsOnInstance(Event seriesInstance)
		{
			foreach (ICalendarInteropSeriesAction calendarInteropSeriesAction in this.GetRemainingActions(((IActionPropagationState)seriesInstance).LastExecutedAction))
			{
				try
				{
					calendarInteropSeriesAction.ExecuteOnInstance(seriesInstance);
				}
				catch (Exception ex)
				{
					if (this.Trace.IsTraceEnabled(TraceType.ErrorTrace))
					{
						this.Trace.TraceError<string, string, Exception>((long)this.GetHashCode(), "Error propagating action " + calendarInteropSeriesAction.CommandId + " to instance {0} of series {1}. Error: {2}.", seriesInstance.Id, this.Entity.SeriesId, ex);
					}
					base.Logger.SafeLogEntry(this.Scope.Session, ex, false, "Error propagating action {0} to instance {1} of series {2}.", new object[]
					{
						calendarInteropSeriesAction.CommandId,
						seriesInstance.Id,
						this.Entity.SeriesId
					});
					throw;
				}
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A7E4 File Offset: 0x000089E4
		private IEnumerable<ICalendarInteropSeriesAction> GetRemainingActions(Guid? lastInteropActionAppliedOnTarget)
		{
			List<ICalendarInteropSeriesAction> list = new List<ICalendarInteropSeriesAction>();
			bool flag = false;
			foreach (ICalendarInteropSeriesAction calendarInteropSeriesAction in this.pendingActions)
			{
				if (!flag)
				{
					if (lastInteropActionAppliedOnTarget != null && lastInteropActionAppliedOnTarget.Value == calendarInteropSeriesAction.CommandId)
					{
						flag = true;
					}
				}
				else
				{
					list.Add(calendarInteropSeriesAction);
				}
			}
			if (!flag)
			{
				return this.pendingActions;
			}
			return list;
		}

		// Token: 0x040000C7 RID: 199
		private readonly List<ICalendarInteropSeriesAction> pendingActions = new List<ICalendarInteropSeriesAction>();

		// Token: 0x040000C8 RID: 200
		private ISeriesActionParser seriesActionParser;
	}
}
