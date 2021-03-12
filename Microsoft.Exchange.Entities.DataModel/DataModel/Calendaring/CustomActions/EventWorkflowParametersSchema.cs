using System;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions
{
	// Token: 0x02000038 RID: 56
	public abstract class EventWorkflowParametersSchema : TypeSchema
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00003A1F File Offset: 0x00001C1F
		protected EventWorkflowParametersSchema()
		{
			base.RegisterPropertyDefinition(EventWorkflowParametersSchema.StaticImportanceProperty);
			base.RegisterPropertyDefinition(EventWorkflowParametersSchema.StaticNotesProperty);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003A3D File Offset: 0x00001C3D
		public TypedPropertyDefinition<Importance> ImportanceProperty
		{
			get
			{
				return EventWorkflowParametersSchema.StaticImportanceProperty;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003A44 File Offset: 0x00001C44
		public TypedPropertyDefinition<ItemBody> NotesProperty
		{
			get
			{
				return EventWorkflowParametersSchema.StaticNotesProperty;
			}
		}

		// Token: 0x0400007A RID: 122
		private static readonly TypedPropertyDefinition<Importance> StaticImportanceProperty = new TypedPropertyDefinition<Importance>("EventWorkflowParameters.Importance", Importance.Low, true);

		// Token: 0x0400007B RID: 123
		private static readonly TypedPropertyDefinition<ItemBody> StaticNotesProperty = new TypedPropertyDefinition<ItemBody>("EventWorkflowParameters.Notes", null, true);
	}
}
