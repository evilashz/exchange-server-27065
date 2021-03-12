using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class CreateSingleEventBase : CreateEventBase
	{
		// Token: 0x06000177 RID: 375 RVA: 0x000064AC File Offset: 0x000046AC
		protected override Event OnExecute()
		{
			Event result;
			try
			{
				this.Scope.EventDataProvider.StoreObjectSaved += this.OnStoreObjectSaved;
				this.ValidateParameters();
				Event @event = this.CreateNewEvent();
				result = @event;
			}
			finally
			{
				this.Scope.EventDataProvider.StoreObjectSaved -= this.OnStoreObjectSaved;
			}
			return result;
		}

		// Token: 0x06000178 RID: 376
		protected abstract Event CreateNewEvent();

		// Token: 0x06000179 RID: 377 RVA: 0x00006514 File Offset: 0x00004714
		private void OnStoreObjectSaved(object sender, ICalendarItemBase calendarItemBase)
		{
			this.Scope.EventDataProvider.TryLogCalendarEventActivity(ActivityId.CreateCalendarEvent, calendarItemBase.Id.ObjectId);
			bool flag;
			CalendarItemAccessors.IsDraft.TryGetValue(calendarItemBase, out flag);
			if (!flag && calendarItemBase.AttendeeCollection != null && calendarItemBase.AttendeeCollection.Count > 0)
			{
				calendarItemBase.SendMeetingMessages(true, null, false, true, null, null);
				calendarItemBase.Load();
			}
		}
	}
}
