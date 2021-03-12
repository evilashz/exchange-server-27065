using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.EntitySets;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets.Commands;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000068 RID: 104
	internal abstract class SeriesInteropCommand : KeyedEntityCommand<Events, Event>
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000A026 File Offset: 0x00008226
		protected SeriesInteropCommand(EventSeriesPropagationConfig interopConfiguration, ICalendarInteropLog logger)
		{
			this.InteropConfiguration = interopConfiguration;
			this.Logger = logger;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000A047 File Offset: 0x00008247
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000A04F File Offset: 0x0000824F
		public ICalendarInteropLog Logger { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000A058 File Offset: 0x00008258
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000A060 File Offset: 0x00008260
		private protected EventSeriesPropagationConfig InteropConfiguration { protected get; private set; }

		// Token: 0x060002B1 RID: 689
		public abstract Event CleanUp(Event master);

		// Token: 0x060002B2 RID: 690
		public abstract void ExecuteOnInstance(Event seriesInstance);

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A080 File Offset: 0x00008280
		internal Event RemoveActionFromPendingActionQueue(Event masterToCleanup, Guid actionId)
		{
			Event @event = new Event
			{
				Id = masterToCleanup.Id,
				ChangeKey = masterToCleanup.ChangeKey
			};
			IActionQueue actionQueue = @event;
			actionQueue.ActionsToRemove = new Guid[]
			{
				actionId
			};
			return this.ExecuteOnMasterWithConflictRetries((Event theEvent) => this.Scope.EventDataProvider.Update(theEvent, null), @event, false);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000A0DE File Offset: 0x000082DE
		protected sealed override Event OnExecute()
		{
			this.master = this.InitialMasterOperation();
			this.PropagateToSeriesInstances();
			this.TryCleanUp();
			return this.master;
		}

		// Token: 0x060002B5 RID: 693
		protected abstract Event InitialMasterOperation();

		// Token: 0x060002B6 RID: 694
		protected abstract bool ShouldExecuteOnInstance(Event seriesInstance);

		// Token: 0x060002B7 RID: 695 RVA: 0x0000A15C File Offset: 0x0000835C
		protected Event ExecuteOnMasterWithConflictRetries(Func<Event, Event> masterInitialAction, Event eventToUpdate, bool throwOnNonInternalPropertyConflict = false)
		{
			Event result = null;
			this.ExecuteWithConflictRetries(delegate
			{
				result = masterInitialAction(eventToUpdate);
			}, delegate
			{
				Event @event = this.Scope.Read(eventToUpdate.Id, null);
				eventToUpdate.ChangeKey = @event.ChangeKey;
			}, throwOnNonInternalPropertyConflict);
			return result;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A1B0 File Offset: 0x000083B0
		protected virtual bool IsPropagationComplete()
		{
			return this.propagationAttemptedOnAllInstances;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A1B8 File Offset: 0x000083B8
		protected virtual void PropagateToSeriesInstances()
		{
			try
			{
				SortBy sortOrder = SeriesInteropCommand.InstancesQuerySortByDate;
				if (this.InteropConfiguration.ReversePropagationOrder)
				{
					sortOrder = SeriesInteropCommand.InstancesQueryReverseSortByDate;
				}
				this.elapsedPropagationTime.Start();
				this.remainingPropagationCount = this.InteropConfiguration.PropagationCountLimit;
				this.propagationAttemptedOnAllInstances = this.Scope.EventDataProvider.ForEachSeriesItem(this.master, new Func<Event, bool>(this.PropagateToInstance), new Func<IStorePropertyBag, Event>(this.GetInstancePropagationData), null, sortOrder, null, new PropertyDefinition[]
				{
					CalendarItemSchema.LastExecutedCalendarInteropAction
				});
			}
			catch (Exception ex)
			{
				this.propagationAttemptedOnAllInstances = false;
				this.Trace.TraceError<string, Exception>((long)this.GetHashCode(), "Error propagating changes to series instances for series {0}. Error {1}", this.master.SeriesId, ex);
				this.Logger.SafeLogEntry(this.Scope.Session, ex, false, "Error propagating changes to series instances for series {0}", new object[]
				{
					this.master.SeriesId
				});
				if (!this.InteropConfiguration.IgnorePropagationErrors)
				{
					throw;
				}
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A2C4 File Offset: 0x000084C4
		protected virtual void TryCleanUp()
		{
			if (this.IsPropagationComplete() && this.InteropConfiguration.ShouldCleanup)
			{
				this.master = this.CleanUp(this.master);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A2ED File Offset: 0x000084ED
		private static bool IsInternalProperty(PropertyDefinition propertyDefinition)
		{
			return propertyDefinition == CalendarItemSeriesSchema.CalendarInteropActionQueueHasDataInternal || propertyDefinition == CalendarItemSeriesSchema.CalendarInteropActionQueueInternal;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A304 File Offset: 0x00008504
		private void ExecuteWithConflictRetries(Action action, Action dataRefresh, bool throwOnNonInternalPropertyConflict = false)
		{
			uint num = this.InteropConfiguration.MaxCollisionRetryCount;
			try
			{
				IL_0C:
				action();
			}
			catch (IrresolvableConflictException ex)
			{
				if (num == 0U)
				{
					throw;
				}
				num -= 1U;
				if (throwOnNonInternalPropertyConflict && ex.ConflictResolutionResult != null)
				{
					foreach (PropertyConflict propertyConflict in ex.ConflictResolutionResult.PropertyConflicts)
					{
						if (!propertyConflict.ConflictResolvable && !SeriesInteropCommand.IsInternalProperty(propertyConflict.PropertyDefinition))
						{
							throw;
						}
					}
				}
				dataRefresh();
				goto IL_0C;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A3CC File Offset: 0x000085CC
		private void TryExecuteOnInstance(Event seriesInstance)
		{
			this.ExecuteWithConflictRetries(delegate
			{
				this.ExecuteOnInstance(seriesInstance);
			}, delegate
			{
				seriesInstance = this.Scope.Read(seriesInstance.Id, null);
			}, false);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A40C File Offset: 0x0000860C
		private Event GetInstancePropagationData(IStorePropertyBag propertyBag)
		{
			Event basicSeriesEventData = EventExtensions.GetBasicSeriesEventData(propertyBag, this.Scope);
			IActionPropagationState actionPropagationState = basicSeriesEventData;
			actionPropagationState.LastExecutedAction = propertyBag.GetValueOrDefault<Guid?>(CalendarItemSchema.LastExecutedCalendarInteropAction, null);
			return basicSeriesEventData;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A443 File Offset: 0x00008643
		private bool PropagateToInstance(Event seriesInstance)
		{
			if (this.remainingPropagationCount == 0U || this.elapsedPropagationTime.Elapsed > this.InteropConfiguration.PropagationTimeLimit)
			{
				return false;
			}
			this.TryExecuteOnInstance(seriesInstance);
			this.remainingPropagationCount -= 1U;
			return true;
		}

		// Token: 0x040000BF RID: 191
		private static readonly SortBy InstancesQueryReverseSortByDate = new SortBy(CalendarItemInstanceSchema.StartTime, SortOrder.Descending);

		// Token: 0x040000C0 RID: 192
		private static readonly SortBy InstancesQuerySortByDate = new SortBy(CalendarItemInstanceSchema.StartTime, SortOrder.Ascending);

		// Token: 0x040000C1 RID: 193
		private readonly Stopwatch elapsedPropagationTime = new Stopwatch();

		// Token: 0x040000C2 RID: 194
		private uint remainingPropagationCount;

		// Token: 0x040000C3 RID: 195
		private bool propagationAttemptedOnAllInstances;

		// Token: 0x040000C4 RID: 196
		private Event master;
	}
}
