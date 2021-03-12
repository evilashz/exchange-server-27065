using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CBA RID: 3258
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReplyAllDisplayNamesProperty : ReplyAllParticipantsRepresentationProperty<string>
	{
		// Token: 0x0600715C RID: 29020 RVA: 0x001F6F1C File Offset: 0x001F511C
		internal ReplyAllDisplayNamesProperty() : base("ReplyAllDisplayNames")
		{
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x0600715D RID: 29021 RVA: 0x001F6F29 File Offset: 0x001F5129
		public override IEqualityComparer<string> ParticipantRepresentationComparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x0600715E RID: 29022 RVA: 0x001F6F38 File Offset: 0x001F5138
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			IDictionary<RecipientItemType, HashSet<string>> recipients = null;
			foreach (ComplexParticipantExtractorBase<string> complexParticipantExtractorBase in ReplyAllDisplayNamesProperty.Extractors)
			{
				if (complexParticipantExtractorBase.ShouldExtract(propertyBag))
				{
					IList<string> replyTo;
					object result;
					if (complexParticipantExtractorBase.Extract(propertyBag, (Participant participant) => participant.DisplayName, this.ParticipantRepresentationComparer, out recipients, out replyTo))
					{
						IParticipant simpleParticipant = base.GetSimpleParticipant(InternalSchema.Sender, propertyBag);
						IParticipant simpleParticipant2 = base.GetSimpleParticipant(InternalSchema.From, propertyBag);
						result = ReplyAllParticipantsRepresentationProperty<string>.BuildReplyAllRecipients<string>((simpleParticipant == null) ? null : simpleParticipant.DisplayName, (simpleParticipant2 == null) ? null : simpleParticipant2.DisplayName, replyTo, recipients, this.ParticipantRepresentationComparer);
					}
					else
					{
						result = new PropertyError(this, PropertyErrorCode.GetCalculatedPropertyError);
					}
					return result;
				}
			}
			return new PropertyError(this, PropertyErrorCode.GetCalculatedPropertyError);
		}

		// Token: 0x04004EA7 RID: 20135
		private static ComplexParticipantExtractorBase<string>[] Extractors = new ComplexParticipantExtractorBase<string>[]
		{
			new ComplexParticipantExtractorFromICoreItem<string>(),
			new ComplexParticipantExtractorFromPropertyBag()
		};
	}
}
