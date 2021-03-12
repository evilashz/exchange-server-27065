using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators
{
	// Token: 0x020000A6 RID: 166
	internal class MeetingMessageTranslator : ItemTranslator<MeetingMessage, MeetingMessage, MeetingMessageSchema>
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		protected MeetingMessageTranslator(IEnumerable<ITranslationRule<MeetingMessage, MeetingMessage>> additionalRules = null) : base(MeetingMessageTranslator.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public new static MeetingMessageTranslator Instance
		{
			get
			{
				return MeetingMessageTranslator.SingletonInstance;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000EDF8 File Offset: 0x0000CFF8
		private static List<ITranslationRule<MeetingMessage, MeetingMessage>> CreateTranslationRules()
		{
			return new List<ITranslationRule<MeetingMessage, MeetingMessage>>
			{
				MeetingMessageAccessors.OccurrencesExceptionalViewProperties.MapTo(MeetingMessage.Accessors.OccurrencesExceptionalViewProperties),
				MeetingMessageAccessors.Type.MapTo(MeetingMessage.Accessors.Type)
			};
		}

		// Token: 0x04000176 RID: 374
		private static readonly MeetingMessageTranslator SingletonInstance = new MeetingMessageTranslator(null);
	}
}
