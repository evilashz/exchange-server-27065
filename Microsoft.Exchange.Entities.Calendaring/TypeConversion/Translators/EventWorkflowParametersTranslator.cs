using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators
{
	// Token: 0x020000A5 RID: 165
	internal class EventWorkflowParametersTranslator<TParameters, TSchema> : StorageTranslator<MessageItem, TParameters> where TParameters : EventWorkflowParameters<TSchema>, new() where TSchema : EventWorkflowParametersSchema, new()
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000ED6B File Offset: 0x0000CF6B
		private EventWorkflowParametersTranslator() : base(EventWorkflowParametersTranslator<TParameters, TSchema>.CreateTranslationRules())
		{
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public static EventWorkflowParametersTranslator<TParameters, TSchema> Instance
		{
			get
			{
				return EventWorkflowParametersTranslator<TParameters, TSchema>.SingletonInstance;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000ED7F File Offset: 0x0000CF7F
		protected override TParameters CreateEntity()
		{
			return Activator.CreateInstance<TParameters>();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000ED88 File Offset: 0x0000CF88
		private static IList<ITranslationRule<MessageItem, TParameters>> CreateTranslationRules()
		{
			return new List<ITranslationRule<MessageItem, TParameters>>
			{
				ItemAccessors<MessageItem>.Body.MapTo(EventWorkflowParameters<TSchema>.Accessors.Notes),
				ItemAccessors<MessageItem>.Importance.MapTo(EventWorkflowParameters<TSchema>.Accessors.Importance, default(ImportanceConverter))
			};
		}

		// Token: 0x04000175 RID: 373
		private static readonly EventWorkflowParametersTranslator<TParameters, TSchema> SingletonInstance = new EventWorkflowParametersTranslator<TParameters, TSchema>();
	}
}
