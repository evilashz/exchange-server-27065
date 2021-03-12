using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.Interop;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.ReliableActions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UpdateEvent : UpdateEventBase
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008F06 File Offset: 0x00007106
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00008F0E File Offset: 0x0000710E
		public bool SendMeetingMessagesOnSave
		{
			get
			{
				return this.sendMeetingMessagesOnSave;
			}
			set
			{
				this.sendMeetingMessagesOnSave = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008F17 File Offset: 0x00007117
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00008F1F File Offset: 0x0000711F
		public int? SeriesSequenceNumber { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008F28 File Offset: 0x00007128
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00008F30 File Offset: 0x00007130
		public byte[] MasterGoid { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008F39 File Offset: 0x00007139
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00008F41 File Offset: 0x00007141
		public bool PropagationInProgress { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008F4A File Offset: 0x0000714A
		protected override ITracer Trace
		{
			get
			{
				return ExTraceGlobals.UpdateEventTracer;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008F54 File Offset: 0x00007154
		protected override Event OnExecute()
		{
			EventDataProvider eventDataProvider = this.Scope.EventDataProvider;
			Event result;
			try
			{
				eventDataProvider.StoreObjectSaved += this.OnStoreObjectSaved;
				eventDataProvider.BeforeStoreObjectSaved += this.DataProviderOnBeforeStoreObjectSaved;
				if (!this.PropagationInProgress)
				{
					using (ICalendarItemBase calendarItemBase = eventDataProvider.ValidateAndBindToWrite(base.Entity))
					{
						Event @event = eventDataProvider.ConvertToEntity(calendarItemBase);
						base.MergeAttendeesList(@event.Attendees);
						if (this.IsNprInstance(calendarItemBase))
						{
							this.ValidateSeriesMasterId(calendarItemBase as ICalendarItem);
							this.PreProcessNprInstance(@event);
						}
					}
				}
				result = eventDataProvider.Update(base.Entity, this.Context);
			}
			finally
			{
				eventDataProvider.StoreObjectSaved -= this.OnStoreObjectSaved;
				eventDataProvider.BeforeStoreObjectSaved -= this.DataProviderOnBeforeStoreObjectSaved;
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000903C File Offset: 0x0000723C
		protected virtual void DataProviderOnBeforeStoreObjectSaved(Event update, ICalendarItemBase itemToSave)
		{
			this.Scope.TimeAdjuster.AdjustTimeProperties(itemToSave);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009050 File Offset: 0x00007250
		protected virtual void PreProcessNprInstance(Event instanceFromStore)
		{
			Event master = this.ReadMasterFromInstance(instanceFromStore);
			if (this.IsPropagationPending(master, instanceFromStore))
			{
				this.ForcePropagation(master, instanceFromStore);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009078 File Offset: 0x00007278
		protected virtual void ForcePropagation(Event master, Event instance)
		{
			PropagateToInstance propagateToInstance = this.CreatePropagateToInstanceCommand(master, instance);
			propagateToInstance.Execute(this.Context);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000909C File Offset: 0x0000729C
		protected virtual PropagateToInstance CreatePropagateToInstanceCommand(Event master, Event instance)
		{
			return new PropagateToInstance(CalendarInteropLog.Default, null)
			{
				Entity = master,
				Instance = instance
			};
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000090C4 File Offset: 0x000072C4
		protected virtual Event ReadMasterFromInstance(Event instance)
		{
			Event result;
			if (!this.TryReadMasterFromSeriesMasterId(instance, out result))
			{
				ICalendarItemBase storeObject = this.Scope.EventDataProvider.BindToMasterFromSeriesId(instance.SeriesId);
				result = this.Scope.EventDataProvider.ConvertToEntity(storeObject);
			}
			return result;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009108 File Offset: 0x00007308
		protected virtual bool IsPropagationPending(IActionPropagationState master, IActionPropagationState instance)
		{
			Guid? lastExecutedAction = master.LastExecutedAction;
			if (lastExecutedAction == null)
			{
				return false;
			}
			Guid? lastExecutedAction2 = instance.LastExecutedAction;
			return lastExecutedAction2 == null || !lastExecutedAction.Value.Equals(lastExecutedAction2.Value);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009154 File Offset: 0x00007354
		protected virtual void ValidateSeriesMasterId(ICalendarItem storeObject)
		{
			string b;
			bool flag = this.TryGetSeriesMasterId(storeObject, out b);
			if (base.Entity.IsPropertySet(base.Entity.Schema.SeriesMasterIdProperty) && (!flag || base.Entity.SeriesMasterId != b))
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorCallerCantChangeSeriesMasterId);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000091A9 File Offset: 0x000073A9
		protected virtual bool TryGetSeriesMasterId(ICalendarItem storeObject, out string seriesMasterId)
		{
			return CalendarItemAccessors.SeriesMasterId.TryGetValue(storeObject, out seriesMasterId);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000091B8 File Offset: 0x000073B8
		private bool TryReadMasterFromSeriesMasterId(Event instance, out Event master)
		{
			master = null;
			string seriesMasterId = instance.SeriesMasterId;
			if (string.IsNullOrEmpty(seriesMasterId))
			{
				return false;
			}
			bool result;
			try
			{
				StoreId id = this.Scope.IdConverter.ToStoreObjectId(seriesMasterId);
				master = this.Scope.EventDataProvider.Read(id);
				result = true;
			}
			catch (ObjectNotFoundException arg)
			{
				this.Trace.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "Error while reading master based on SeriesMasterId. {0}", arg);
				result = false;
			}
			return result;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009230 File Offset: 0x00007430
		private bool IsNprInstance(ICalendarItemBase targetStoreObject)
		{
			return !string.IsNullOrEmpty(targetStoreObject.SeriesId) && !ObjectClass.IsCalendarItemSeries(targetStoreObject.ItemClass);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009250 File Offset: 0x00007450
		private void OnStoreObjectSaved(object sender, ICalendarItemBase calendarItemBase)
		{
			this.Scope.EventDataProvider.TryLogCalendarEventActivity(ActivityId.UpdateCalendarEvent, calendarItemBase.Id.ObjectId);
			if (this.SendMeetingMessagesOnSave && calendarItemBase.IsOrganizer())
			{
				bool flag;
				CalendarItemAccessors.IsDraft.TryGetValue(calendarItemBase, out flag);
				if (!flag && (calendarItemBase.IsMeeting || (calendarItemBase.AttendeeCollection != null && calendarItemBase.AttendeeCollection.Count > 0)))
				{
					calendarItemBase.SendMeetingMessages(true, this.SeriesSequenceNumber, false, true, null, this.MasterGoid);
					calendarItemBase.Load();
				}
			}
		}

		// Token: 0x0400009D RID: 157
		private bool sendMeetingMessagesOnSave = true;
	}
}
