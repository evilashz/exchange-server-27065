using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.Calendaring.EntitySets.EventCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.Calendaring.EntitySets
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Events : StorageEntitySet<Events, Event, IEventCommandFactory, IStoreSession>, IEvents, IEntitySet<Event>
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00009656 File Offset: 0x00007856
		protected internal Events(IStorageEntitySetScope<IStoreSession> parentScope, ILocalCalendarReference localCalendar) : base(parentScope, "Events", EventCommandFactory.Instance)
		{
			this.LocalCalendar = localCalendar;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009670 File Offset: 0x00007870
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00009678 File Offset: 0x00007878
		public ILocalCalendarReference LocalCalendar { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009684 File Offset: 0x00007884
		public virtual EventDataProvider EventDataProvider
		{
			get
			{
				EventDataProvider result;
				if ((result = this.eventDataProvider) == null)
				{
					result = (this.eventDataProvider = new EventDataProvider(this, this.LocalCalendar.GetCalendarFolderId()));
				}
				return result;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000096B8 File Offset: 0x000078B8
		public virtual SeriesEventDataProvider SeriesEventDataProvider
		{
			get
			{
				SeriesEventDataProvider result;
				if ((result = this.seriesEventDataProvider) == null)
				{
					result = (this.seriesEventDataProvider = new SeriesEventDataProvider(this, this.LocalCalendar.GetCalendarFolderId()));
				}
				return result;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000096EC File Offset: 0x000078EC
		public virtual EventTimeAdjuster TimeAdjuster
		{
			get
			{
				EventTimeAdjuster result;
				if ((result = this.timeAdjuster) == null)
				{
					result = (this.timeAdjuster = new EventTimeAdjuster(new DateTimeHelper()));
				}
				return result;
			}
		}

		// Token: 0x17000094 RID: 148
		public IEventReference this[string eventId]
		{
			get
			{
				return new EventReference(this, eventId);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009720 File Offset: 0x00007920
		public void Cancel(string key, CancelEventParameters parameters, CommandContext context = null)
		{
			CancelEventBase cancelEventBase = base.CommandFactory.CreateCancelCommand(key, this);
			cancelEventBase.Parameters = parameters;
			cancelEventBase.Execute(context);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000974C File Offset: 0x0000794C
		public void Forward(string key, ForwardEventParameters parameters, CommandContext context = null)
		{
			ForwardEventBase forwardEventBase = base.CommandFactory.CreateForwardCommand(key, this);
			forwardEventBase.Parameters = parameters;
			forwardEventBase.Execute(context);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009778 File Offset: 0x00007978
		public ExpandedEvent Expand(string key, ExpandEventParameters parameters, CommandContext context = null)
		{
			ExpandSeries expandSeries = base.CommandFactory.CreateExpandCommand(key, this);
			expandSeries.Parameters = parameters;
			return expandSeries.Execute(context);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000097A4 File Offset: 0x000079A4
		public IEnumerable<Event> GetCalendarView(ICalendarViewParameters parameters, CommandContext context = null)
		{
			GetCalendarView getCalendarView = base.CommandFactory.CreateGetCalendarViewCommand(parameters, this);
			return getCalendarView.Execute(context);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000097C8 File Offset: 0x000079C8
		public void Respond(string key, RespondToEventParameters parameters, CommandContext context = null)
		{
			RespondToEventBase respondToEventBase = base.CommandFactory.CreateRespondToCommand(key, this);
			respondToEventBase.Parameters = parameters;
			respondToEventBase.Execute(context);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000097F4 File Offset: 0x000079F4
		public Event Update(string key, Event entity, UpdateEventParameters updateEventParameters, CommandContext context = null)
		{
			UpdateEventBase updateEventBase = base.CommandFactory.CreateUpdateCommand(key, entity, this, updateEventParameters);
			return updateEventBase.Execute(context);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000981C File Offset: 0x00007A1C
		public Event ConvertSingleEventToNprSeries(string key, IList<Event> additionalInstancesToAdd, string clientId, CommandContext context = null)
		{
			ConvertSingleEventToNprSeries convertSingleEventToNprSeries = base.CommandFactory.CreateConvertSingleEventToNprCommand(key, this);
			convertSingleEventToNprSeries.AdditionalInstancesToAdd = additionalInstancesToAdd;
			convertSingleEventToNprSeries.ClientId = clientId;
			return convertSingleEventToNprSeries.Execute(context);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009850 File Offset: 0x00007A50
		internal virtual EventDataProvider GetDataProvider(StoreId storeId)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			if (storeObjectId.ObjectType != StoreObjectType.CalendarItemSeries)
			{
				return this.EventDataProvider;
			}
			return this.SeriesEventDataProvider;
		}

		// Token: 0x040000A5 RID: 165
		private EventDataProvider eventDataProvider;

		// Token: 0x040000A6 RID: 166
		private SeriesEventDataProvider seriesEventDataProvider;

		// Token: 0x040000A7 RID: 167
		private EventTimeAdjuster timeAdjuster;
	}
}
