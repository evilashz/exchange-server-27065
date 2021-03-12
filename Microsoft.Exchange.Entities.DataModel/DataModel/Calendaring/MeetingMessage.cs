using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Messaging;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000061 RID: 97
	public class MeetingMessage : EmailMessage<MeetingMessageSchema>
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00006334 File Offset: 0x00004534
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00006347 File Offset: 0x00004547
		public List<Event> OccurrencesExceptionalViewProperties
		{
			get
			{
				return base.GetPropertyValueOrDefault<List<Event>>(base.Schema.OccurrencesExceptionalViewPropertiesProperty);
			}
			set
			{
				base.SetPropertyValue<List<Event>>(base.Schema.OccurrencesExceptionalViewPropertiesProperty, value);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000635B File Offset: 0x0000455B
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000636E File Offset: 0x0000456E
		public MeetingMessageType Type
		{
			get
			{
				return base.GetPropertyValueOrDefault<MeetingMessageType>(base.Schema.TypeProperty);
			}
			set
			{
				base.SetPropertyValue<MeetingMessageType>(base.Schema.TypeProperty, value);
			}
		}

		// Token: 0x02000062 RID: 98
		public new static class Accessors
		{
			// Token: 0x04000160 RID: 352
			public static readonly EntityPropertyAccessor<MeetingMessage, List<Event>> OccurrencesExceptionalViewProperties = new EntityPropertyAccessor<MeetingMessage, List<Event>>(SchematizedObject<MeetingMessageSchema>.SchemaInstance.OccurrencesExceptionalViewPropertiesProperty, (MeetingMessage theMessage) => theMessage.OccurrencesExceptionalViewProperties, delegate(MeetingMessage theMessage, List<Event> occurencesExceptionalViewProperties)
			{
				theMessage.OccurrencesExceptionalViewProperties = occurencesExceptionalViewProperties;
			});

			// Token: 0x04000161 RID: 353
			public static readonly EntityPropertyAccessor<MeetingMessage, MeetingMessageType> Type = new EntityPropertyAccessor<MeetingMessage, MeetingMessageType>(SchematizedObject<MeetingMessageSchema>.SchemaInstance.TypeProperty, (MeetingMessage theMessage) => theMessage.Type, delegate(MeetingMessage theMessage, MeetingMessageType type)
			{
				theMessage.Type = type;
			});
		}
	}
}
