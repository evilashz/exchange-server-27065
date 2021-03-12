using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x02000035 RID: 53
	public abstract class EventWorkflowParameters<TSchema> : SchematizedObject<TSchema> where TSchema : EventWorkflowParametersSchema, new()
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003890 File Offset: 0x00001A90
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000038B8 File Offset: 0x00001AB8
		public Importance Importance
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<Importance>(schema.ImportanceProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<Importance>(schema.ImportanceProperty, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000038E0 File Offset: 0x00001AE0
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00003908 File Offset: 0x00001B08
		public ItemBody Notes
		{
			get
			{
				TSchema schema = base.Schema;
				return base.GetPropertyValueOrDefault<ItemBody>(schema.NotesProperty);
			}
			set
			{
				TSchema schema = base.Schema;
				base.SetPropertyValue<ItemBody>(schema.NotesProperty, value);
			}
		}

		// Token: 0x02000036 RID: 54
		public static class Accessors
		{
			// Token: 0x06000116 RID: 278 RVA: 0x0000395C File Offset: 0x00001B5C
			// Note: this type is marked as 'beforefieldinit'.
			static Accessors()
			{
				TSchema schemaInstance = SchematizedObject<TSchema>.SchemaInstance;
				EventWorkflowParameters<TSchema>.Accessors.Importance = new EntityPropertyAccessor<EventWorkflowParameters<TSchema>, Importance>(schemaInstance.ImportanceProperty, (EventWorkflowParameters<TSchema> parameters) => parameters.Importance, delegate(EventWorkflowParameters<TSchema> parameters, Importance importance)
				{
					parameters.Importance = importance;
				});
				TSchema schemaInstance2 = SchematizedObject<TSchema>.SchemaInstance;
				EventWorkflowParameters<TSchema>.Accessors.Notes = new EntityPropertyAccessor<EventWorkflowParameters<TSchema>, ItemBody>(schemaInstance2.NotesProperty, (EventWorkflowParameters<TSchema> parameters) => parameters.Notes, delegate(EventWorkflowParameters<TSchema> parameters, ItemBody body)
				{
					parameters.Notes = body;
				});
			}

			// Token: 0x04000074 RID: 116
			public static readonly EntityPropertyAccessor<EventWorkflowParameters<TSchema>, Importance> Importance;

			// Token: 0x04000075 RID: 117
			public static readonly EntityPropertyAccessor<EventWorkflowParameters<TSchema>, ItemBody> Notes;
		}
	}
}
