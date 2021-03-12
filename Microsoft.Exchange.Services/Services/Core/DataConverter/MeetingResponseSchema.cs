using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001AA RID: 426
	internal sealed class MeetingResponseSchema : Schema
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x00039F88 File Offset: 0x00038188
		static MeetingResponseSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				MeetingResponseSchema.Start,
				MeetingResponseSchema.End,
				MeetingResponseSchema.Location,
				CalendarItemSchema.AttendeeSpecific.Recurrence,
				MeetingResponseSchema.CalendarItemType,
				MeetingResponseSchema.ProposedStart,
				MeetingResponseSchema.ProposedEnd,
				MeetingResponseSchema.EnhancedLocation
			};
			MeetingResponseSchema.schema_Exchange2012AndLater = new MeetingResponseSchema(xmlElements);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0003A0E0 File Offset: 0x000382E0
		private MeetingResponseSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(MeetingResponseSchema.EnhancedLocation);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0003A108 File Offset: 0x00038308
		public static Schema GetSchema()
		{
			if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				return MeetingMessageSchema.GetSchema();
			}
			return MeetingResponseSchema.schema_Exchange2012AndLater;
		}

		// Token: 0x040008F2 RID: 2290
		public static readonly PropertyInformation Start = new PropertyInformation(PropertyUriEnum.Start, ExchangeVersion.Exchange2012, CalendarItemInstanceSchema.StartTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForStart), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008F3 RID: 2291
		public static readonly PropertyInformation End = new PropertyInformation(PropertyUriEnum.End, ExchangeVersion.Exchange2012, CalendarItemInstanceSchema.EndTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForEnd), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008F4 RID: 2292
		public static readonly PropertyInformation Location = new PropertyInformation(PropertyUriEnum.Location, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.Location, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008F5 RID: 2293
		public static readonly PropertyInformation CalendarItemType = new PropertyInformation(PropertyUriEnum.CalendarItemType, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.CalendarItemType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008F6 RID: 2294
		public static readonly PropertyInformation ProposedStart = new PropertyInformation(PropertyUriEnum.ProposedStart, ExchangeVersion.Exchange2013, MeetingResponseSchema.AppointmentCounterStartWhole, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForProposedStart), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008F7 RID: 2295
		public static readonly PropertyInformation ProposedEnd = new PropertyInformation(PropertyUriEnum.ProposedEnd, ExchangeVersion.Exchange2013, MeetingResponseSchema.AppointmentCounterEndWhole, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForProposedEnd), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008F8 RID: 2296
		public static readonly PropertyInformation EnhancedLocation = CalendarItemSchema.EnhancedLocation;

		// Token: 0x040008F9 RID: 2297
		private static Schema schema_Exchange2012AndLater;
	}
}
