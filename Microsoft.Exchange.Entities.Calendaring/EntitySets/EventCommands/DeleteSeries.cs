using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.Interop;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class DeleteSeries : DeleteEventBase, ICalendarInteropSeriesAction
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000075D8 File Offset: 0x000057D8
		public Guid CommandId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000075E0 File Offset: 0x000057E0
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.DeleteSeriesTracer;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000075E7 File Offset: 0x000057E7
		void ICalendarInteropSeriesAction.RestoreExecutionContext(Events entitySet, SeriesInteropCommand seriesInteropCommand)
		{
			this.seriesPropagation = seriesInteropCommand;
			base.RestoreScope(entitySet);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000075F7 File Offset: 0x000057F7
		Event ICalendarInteropSeriesAction.CleanUp(Event master)
		{
			this.Scope.EventDataProvider.Delete(master.StoreId, DeleteItemFlags.MoveToDeletedItems);
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007611 File Offset: 0x00005811
		void ICalendarInteropSeriesAction.ExecuteOnInstance(Event seriesInstance)
		{
			this.Scope.EventDataProvider.Delete(seriesInstance.StoreId, DeleteItemFlags.MoveToDeletedItems);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000762A File Offset: 0x0000582A
		Event ICalendarInteropSeriesAction.GetInitialMasterValue()
		{
			return this.Scope.Read(base.EntityKey, null);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000763E File Offset: 0x0000583E
		Event ICalendarInteropSeriesAction.InitialMasterOperation(Event updateToMaster)
		{
			return this.Scope.EventDataProvider.Update(updateToMaster, this.Context);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007657 File Offset: 0x00005857
		protected override void DeleteCancelledEventFromAttendeesCalendar(Event eventToDelete)
		{
			this.seriesPropagation = this.CreateInteropPropagationCommand(null);
			this.seriesPropagation.Execute(null);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007674 File Offset: 0x00005874
		protected virtual SeriesInlineInterop CreateInteropPropagationCommand(ICalendarInteropLog logger = null)
		{
			return new SeriesInlineInterop(this, logger)
			{
				EntityKey = base.EntityKey,
				Scope = this.Scope
			};
		}

		// Token: 0x0400007D RID: 125
		private SeriesInteropCommand seriesPropagation;
	}
}
