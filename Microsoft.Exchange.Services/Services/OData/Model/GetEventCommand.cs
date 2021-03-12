using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.ExchangeService;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000F31 RID: 3889
	internal class GetEventCommand : EntityContainersCommand<GetEventRequest, GetEventResponse>
	{
		// Token: 0x06006328 RID: 25384 RVA: 0x0013533A File Offset: 0x0013353A
		public GetEventCommand(GetEventRequest request) : base(request)
		{
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x00135344 File Offset: 0x00133544
		protected override GetEventResponse InternalExecute()
		{
			IEvents events = base.GetCalendarContainer(null).Events;
			Event dataEntityEvent = events.Read(EwsIdConverter.ODataIdToEwsId(base.Request.Id), base.CreateCommandContext(null));
			return new GetEventResponse(base.Request)
			{
				Result = GetEventCommand.DataEntityEventToEntity(dataEntityEvent, base.Request.ODataQueryOptions, base.ExchangeService)
			};
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x001353A8 File Offset: 0x001335A8
		public static Event DataEntityEventToEntity(Event dataEntityEvent, ODataQueryOptions oDataQueryOptions, IExchangeService exchangeService)
		{
			ArgumentValidator.ThrowIfNull("dataEntityEvent", dataEntityEvent);
			ArgumentValidator.ThrowIfNull("oDataQueryOptions", oDataQueryOptions);
			ArgumentValidator.ThrowIfNull("exchangeService", exchangeService);
			Event @event = DataEntityObjectFactory.CreateEntity<Event>(dataEntityEvent);
			QueryAdapter queryAdapter = new DataEntityQueryAdpater(@event.Schema, oDataQueryOptions);
			foreach (PropertyDefinition propertyDefinition in queryAdapter.RequestedProperties)
			{
				propertyDefinition.DataEntityPropertyProvider.GetPropertyFromDataSource(@event, propertyDefinition, dataEntityEvent);
			}
			if (oDataQueryOptions.Expands(EventSchema.Calendar.Name) && dataEntityEvent.Calendar != null)
			{
				@event.Calendar = DataEntityObjectFactory.CreateEntity<Calendar>(dataEntityEvent);
				foreach (PropertyDefinition propertyDefinition2 in @event.Calendar.Schema.DefaultProperties)
				{
					propertyDefinition2.DataEntityPropertyProvider.GetPropertyFromDataSource(@event, propertyDefinition2, dataEntityEvent);
				}
			}
			if (oDataQueryOptions.Expands(ItemSchema.Attachments.Name))
			{
				if (dataEntityEvent.HasAttachments)
				{
					AttachmentProvider attachmentProvider = new AttachmentProvider(exchangeService);
					@event.Attachments = attachmentProvider.Find(@event.Id, null);
				}
				else
				{
					@event.Attachments = GetEventCommand.emptyAttachmentsList;
				}
			}
			return @event;
		}

		// Token: 0x0400350C RID: 13580
		private static readonly List<Attachment> emptyAttachmentsList = new List<Attachment>();
	}
}
