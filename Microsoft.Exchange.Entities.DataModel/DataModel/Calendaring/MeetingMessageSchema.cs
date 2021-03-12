using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Messaging;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000064 RID: 100
	public class MeetingMessageSchema : EmailMessageSchema
	{
		// Token: 0x0600030B RID: 779 RVA: 0x00006482 File Offset: 0x00004682
		public MeetingMessageSchema()
		{
			base.RegisterPropertyDefinition(MeetingMessageSchema.StaticOccurrencesExceptionalViewPropertiesProperty);
			base.RegisterPropertyDefinition(MeetingMessageSchema.StaticTypeProperty);
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000064A0 File Offset: 0x000046A0
		public TypedPropertyDefinition<List<Event>> OccurrencesExceptionalViewPropertiesProperty
		{
			get
			{
				return MeetingMessageSchema.StaticOccurrencesExceptionalViewPropertiesProperty;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000064A7 File Offset: 0x000046A7
		public TypedPropertyDefinition<MeetingMessageType> TypeProperty
		{
			get
			{
				return MeetingMessageSchema.StaticTypeProperty;
			}
		}

		// Token: 0x04000167 RID: 359
		private static readonly TypedPropertyDefinition<List<Event>> StaticOccurrencesExceptionalViewPropertiesProperty = new TypedPropertyDefinition<List<Event>>("MeetingMessage.OccurrencesExceptionalViewProperties", null, true);

		// Token: 0x04000168 RID: 360
		private static readonly TypedPropertyDefinition<MeetingMessageType> StaticTypeProperty = new TypedPropertyDefinition<MeetingMessageType>("MeetingMessage.Type", MeetingMessageType.Unknown, true);
	}
}
