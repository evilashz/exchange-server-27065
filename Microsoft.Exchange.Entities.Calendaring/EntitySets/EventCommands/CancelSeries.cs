using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.Interop;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200003B RID: 59
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class CancelSeries : CancelEventBase, ICalendarInteropSeriesAction
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006229 File Offset: 0x00004429
		public Guid CommandId
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006231 File Offset: 0x00004431
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.CancelSeriesTracer;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006238 File Offset: 0x00004438
		public void RestoreExecutionContext(Events entitySet, SeriesInteropCommand seriesInteropCommand)
		{
			this.seriesPropagation = seriesInteropCommand;
			base.RestoreScope(entitySet);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006248 File Offset: 0x00004448
		public Event CleanUp(Event master)
		{
			this.Scope.EventDataProvider.Delete(master.StoreId, DeleteItemFlags.MoveToDeletedItems);
			return null;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006262 File Offset: 0x00004462
		public void ExecuteOnInstance(Event seriesInstance)
		{
			this.Scope.EventDataProvider.CancelEvent(seriesInstance.StoreId, base.Parameters, new int?(this.seriesSequenceNumber), true, null, this.masterGoid);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006293 File Offset: 0x00004493
		public Event GetInitialMasterValue()
		{
			return this.Scope.Read(base.EntityKey, null);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000062A8 File Offset: 0x000044A8
		public Event InitialMasterOperation(Event updateToMaster)
		{
			this.Scope.EventDataProvider.CancelEvent(updateToMaster.StoreId, base.Parameters, new int?(this.seriesSequenceNumber), false, updateToMaster, null);
			return this.Scope.SeriesEventDataProvider.Read(this.Scope.IdConverter.ToStoreObjectId(updateToMaster.Id));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006308 File Offset: 0x00004508
		protected override VoidResult OnExecute()
		{
			Event initialMasterValue = this.GetInitialMasterValue();
			IEventInternal eventInternal = initialMasterValue;
			IEventInternal eventInternal2 = this.Scope.Read(initialMasterValue.Id, this.Context);
			eventInternal.SeriesSequenceNumber = eventInternal2.SeriesSequenceNumber + 1;
			this.seriesSequenceNumber = eventInternal.SeriesSequenceNumber;
			this.masterGoid = ((eventInternal2.GlobalObjectId != null) ? new GlobalObjectId(eventInternal2.GlobalObjectId).Bytes : null);
			this.seriesPropagation = this.CreateInteropPropagationCommand(null);
			this.seriesPropagation.Execute(null);
			return VoidResult.Value;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006390 File Offset: 0x00004590
		protected virtual SeriesInlineInterop CreateInteropPropagationCommand(ICalendarInteropLog logger = null)
		{
			return new SeriesInlineInterop(this, logger)
			{
				EntityKey = base.EntityKey,
				Scope = this.Scope
			};
		}

		// Token: 0x04000071 RID: 113
		private SeriesInteropCommand seriesPropagation;

		// Token: 0x04000072 RID: 114
		[DataMember]
		private int seriesSequenceNumber;

		// Token: 0x04000073 RID: 115
		[DataMember]
		private byte[] masterGoid;
	}
}
