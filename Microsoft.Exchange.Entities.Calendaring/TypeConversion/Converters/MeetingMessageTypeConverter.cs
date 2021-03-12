using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.Converters;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters
{
	// Token: 0x0200007C RID: 124
	internal struct MeetingMessageTypeConverter : IConverter<string, MeetingMessageType>
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000B538 File Offset: 0x00009738
		public IConverter<string, MeetingMessageType> StorageToEntitiesConverter
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B548 File Offset: 0x00009748
		MeetingMessageType IConverter<string, MeetingMessageType>.Convert(string itemClass)
		{
			if (ObjectClass.IsMeetingRequest(itemClass))
			{
				return MeetingMessageType.SingleInstanceRequest;
			}
			if (ObjectClass.IsMeetingRequestSeries(itemClass))
			{
				return MeetingMessageType.SeriesRequest;
			}
			if (ObjectClass.IsMeetingCancellation(itemClass))
			{
				return MeetingMessageType.SingleInstanceCancel;
			}
			if (ObjectClass.IsMeetingCancellationSeries(itemClass))
			{
				return MeetingMessageType.SeriesCancel;
			}
			if (ObjectClass.IsMeetingResponse(itemClass))
			{
				return MeetingMessageType.SingleInstanceResponse;
			}
			if (ObjectClass.IsMeetingResponseSeries(itemClass))
			{
				return MeetingMessageType.SeriesResponse;
			}
			if (ObjectClass.IsMeetingForwardNotification(itemClass))
			{
				return MeetingMessageType.SingleInstanceForwardNotification;
			}
			throw new ArgumentOutOfRangeException("itemClass");
		}
	}
}
