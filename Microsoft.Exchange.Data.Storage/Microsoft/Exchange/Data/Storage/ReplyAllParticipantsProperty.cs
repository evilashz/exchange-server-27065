using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CBB RID: 3259
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReplyAllParticipantsProperty : ReplyAllParticipantsRepresentationProperty<IParticipant>
	{
		// Token: 0x06007161 RID: 29025 RVA: 0x001F702E File Offset: 0x001F522E
		internal ReplyAllParticipantsProperty() : base("ReplyAllParticipantsProperty")
		{
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x06007162 RID: 29026 RVA: 0x001F703B File Offset: 0x001F523B
		public override IEqualityComparer<IParticipant> ParticipantRepresentationComparer
		{
			get
			{
				return ParticipantComparer.EmailAddress;
			}
		}

		// Token: 0x06007163 RID: 29027 RVA: 0x001F7048 File Offset: 0x001F5248
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			IDictionary<RecipientItemType, HashSet<IParticipant>> recipients = null;
			if (ReplyAllParticipantsProperty.Extractor.ShouldExtract(propertyBag))
			{
				IList<IParticipant> replyTo;
				if (ReplyAllParticipantsProperty.Extractor.Extract(propertyBag, (Participant participant) => participant, this.ParticipantRepresentationComparer, out recipients, out replyTo))
				{
					IParticipant simpleParticipant = base.GetSimpleParticipant(InternalSchema.Sender, propertyBag);
					IParticipant simpleParticipant2 = base.GetSimpleParticipant(InternalSchema.From, propertyBag);
					return ReplyAllParticipantsRepresentationProperty<IParticipant>.BuildReplyAllRecipients<IParticipant>(simpleParticipant, simpleParticipant2, replyTo, recipients, this.ParticipantRepresentationComparer);
				}
			}
			return ReplyAllParticipantsRepresentationProperty<IParticipant>.InstantiateResultType(this.ParticipantRepresentationComparer);
		}

		// Token: 0x04004EA9 RID: 20137
		private static ComplexParticipantExtractorBase<IParticipant> Extractor = new ComplexParticipantExtractorFromICoreItem<IParticipant>();
	}
}
