using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.EntitySets;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.Entities.Calendaring.MessageProcessing
{
	// Token: 0x0200006D RID: 109
	internal static class MeetingMessageProcessor
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000ACCC File Offset: 0x00008ECC
		public static void ProcessXsoMeetingMessage(IStoreSession session, MeetingMessage xsoMeetingMessage, CalendarItemBase xsoCalendarItem, IXSOFactory xsoFactory = null)
		{
			MeetingMessage message = MeetingMessageTranslator.Instance.ConvertToEntity(xsoMeetingMessage);
			Event @event = EventTranslator.Instance.ConvertToEntity(xsoCalendarItem);
			MeetingMessageProcessor.ProcessMeetingMessage(session, message, @event, xsoFactory);
			EventTranslator.Instance.SetPropertiesFromEntityOnStorageObject(@event, xsoCalendarItem);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AD08 File Offset: 0x00008F08
		public static void ProcessMeetingMessage(IStoreSession session, MeetingMessage message, Event @event, IXSOFactory xsoFactory = null)
		{
			try
			{
				MeetingMessageType type = message.Type;
				if (type != MeetingMessageType.SeriesRequest)
				{
					throw new NotSupportedException(string.Format("Not supported meeting message type: {0}", message.Type));
				}
				ProcessMeetingMessageCommand processMeetingMessageCommand = new ProcessSeriesMeetingRequest
				{
					MeetingMessage = message,
					Event = @event
				};
				CalendaringContainer calendaringContainer = new CalendaringContainer(session, xsoFactory);
				processMeetingMessageCommand.Scope = (calendaringContainer.Calendars.Default.Events as Events);
				processMeetingMessageCommand.Execute(null);
			}
			catch (Exception arg)
			{
				ExTraceGlobals.MeetingMessageProcessingTracer.TraceError<string, Exception>(0L, "Error processing meeting message for message '{0}'. Error {1}", message.Id ?? string.Empty, arg);
				throw;
			}
		}
	}
}
