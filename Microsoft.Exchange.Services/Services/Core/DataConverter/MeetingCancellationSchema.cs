using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A5 RID: 421
	internal sealed class MeetingCancellationSchema : Schema
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x00039598 File Offset: 0x00037798
		static MeetingCancellationSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				MeetingCancellationSchema.Start,
				MeetingCancellationSchema.End,
				MeetingCancellationSchema.Location,
				CalendarItemSchema.AttendeeSpecific.Recurrence,
				MeetingCancellationSchema.CalendarItemType,
				MeetingCancellationSchema.EnhancedLocation
			};
			MeetingCancellationSchema.schema_Exchange2012AndLater = new MeetingCancellationSchema(xmlElements);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00039694 File Offset: 0x00037894
		private MeetingCancellationSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(MeetingCancellationSchema.EnhancedLocation);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000396BC File Offset: 0x000378BC
		public static Schema GetSchema()
		{
			if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
			{
				return MeetingMessageSchema.GetSchema();
			}
			return MeetingCancellationSchema.schema_Exchange2012AndLater;
		}

		// Token: 0x040008D5 RID: 2261
		public static readonly PropertyInformation Start = new PropertyInformation(PropertyUriEnum.Start, ExchangeVersion.Exchange2012, CalendarItemInstanceSchema.StartTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForStart), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008D6 RID: 2262
		public static readonly PropertyInformation End = new PropertyInformation(PropertyUriEnum.End, ExchangeVersion.Exchange2012, CalendarItemInstanceSchema.EndTime, new PropertyCommand.CreatePropertyCommand(StartEndProperty.CreateCommandForEnd), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008D7 RID: 2263
		public static readonly PropertyInformation Location = new PropertyInformation(PropertyUriEnum.Location, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.Location, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008D8 RID: 2264
		public static readonly PropertyInformation CalendarItemType = new PropertyInformation(PropertyUriEnum.CalendarItemType, ExchangeVersion.Exchange2012, CalendarItemBaseSchema.CalendarItemType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040008D9 RID: 2265
		public static readonly PropertyInformation EnhancedLocation = CalendarItemSchema.EnhancedLocation;

		// Token: 0x040008DA RID: 2266
		private static Schema schema_Exchange2012AndLater;
	}
}
