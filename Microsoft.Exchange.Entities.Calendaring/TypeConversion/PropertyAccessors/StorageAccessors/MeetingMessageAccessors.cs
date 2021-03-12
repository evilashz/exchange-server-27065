﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x02000093 RID: 147
	internal static class MeetingMessageAccessors
	{
		// Token: 0x04000149 RID: 329
		public static readonly IStoragePropertyAccessor<MeetingMessage, List<Event>> OccurrencesExceptionalViewProperties = new DelegatedStoragePropertyAccessor<MeetingMessage, List<Event>>(delegate(MeetingMessage meetingMessage, out List<Event> occurrencesExceptionalViewProperties)
		{
			occurrencesExceptionalViewProperties = null;
			return meetingMessage is MeetingRequest;
		}, null, null, null, new PropertyDefinition[0]);

		// Token: 0x0400014A RID: 330
		public static readonly IStoragePropertyAccessor<MeetingMessage, MeetingMessageType> Type = new DelegatedStoragePropertyAccessor<MeetingMessage, MeetingMessageType>(delegate(MeetingMessage meetingMessage, out MeetingMessageType type)
		{
			type = MeetingMessageType.Unknown;
			string value;
			if (StoreObjectAccessors.ItemClassAccessor.TryGetValue(meetingMessage, out value))
			{
				type = default(MeetingMessageTypeConverter).StorageToEntitiesConverter.Convert(value);
				return true;
			}
			return false;
		}, null, null, null, new PropertyDefinition[0]);
	}
}
