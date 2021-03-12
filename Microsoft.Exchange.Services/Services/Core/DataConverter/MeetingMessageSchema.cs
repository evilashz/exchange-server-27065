using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A6 RID: 422
	internal sealed class MeetingMessageSchema : Schema
	{
		// Token: 0x06000B9F RID: 2975 RVA: 0x000396DC File Offset: 0x000378DC
		static MeetingMessageSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				MeetingMessageSchema.AssociatedCalendarItemId,
				MeetingMessageSchema.IsDelegated,
				MeetingMessageSchema.IsOutOfDate,
				MeetingMessageSchema.HasBeenProcessed,
				MeetingMessageSchema.ResponseType,
				MeetingMessageSchema.ICalendarUid,
				CalendarItemSchema.ICalendarRecurrenceId,
				CalendarItemSchema.ICalendarDateTimeStamp,
				CalendarItemSchema.IsOrganizer
			};
			MeetingMessageSchema.schema = new MeetingMessageSchema(xmlElements);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00039852 File Offset: 0x00037A52
		private MeetingMessageSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003985B File Offset: 0x00037A5B
		public static Schema GetSchema()
		{
			return MeetingMessageSchema.schema;
		}

		// Token: 0x040008DB RID: 2267
		public static readonly PropertyInformation AssociatedCalendarItemId = new PropertyInformation(PropertyUriEnum.AssociatedCalendarItemId, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(AssociatedCalendarItemIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008DC RID: 2268
		public static readonly PropertyInformation HasBeenProcessed = new PropertyInformation(PropertyUriEnum.HasBeenProcessed, ExchangeVersion.Exchange2007, MeetingMessageSchema.CalendarProcessed, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008DD RID: 2269
		public static readonly PropertyInformation IsOutOfDate = new PropertyInformation(PropertyUriEnum.IsOutOfDate, ExchangeVersion.Exchange2007, MeetingMessageSchema.IsOutOfDate, new PropertyCommand.CreatePropertyCommand(IsOutOfDateProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008DE RID: 2270
		public static readonly PropertyInformation IsDelegated = new PropertyInformation(PropertyUriEnum.IsDelegated, ExchangeVersion.Exchange2007, null, new PropertyCommand.CreatePropertyCommand(IsDelegatedProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x040008DF RID: 2271
		public static readonly PropertyInformation ResponseType = new PropertyInformation(PropertyUriEnum.ResponseType, ExchangeVersion.Exchange2007, MeetingResponseSchema.ResponseType, new PropertyCommand.CreatePropertyCommand(ResponseTypeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E0 RID: 2272
		public static readonly PropertyInformation ICalendarUid = new PropertyInformation(PropertyUriEnum.UID, ExchangeVersion.Exchange2007SP1, ICalendar.UidProperty.PropertyToLoad, new PropertyCommand.CreatePropertyCommand(ICalendar.UidProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E1 RID: 2273
		public static readonly PropertyInformation IsOrganizer = new PropertyInformation(PropertyUriEnum.IsOrganizer, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.IsOrganizer, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008E2 RID: 2274
		private static Schema schema;
	}
}
