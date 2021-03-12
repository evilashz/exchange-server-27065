using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CBC RID: 3260
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReplyDisplayNamesProperty : ReplyAllParticipantsRepresentationProperty<string>
	{
		// Token: 0x06007166 RID: 29030 RVA: 0x001F70D9 File Offset: 0x001F52D9
		internal ReplyDisplayNamesProperty() : base("ReplyDisplayNames")
		{
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x06007167 RID: 29031 RVA: 0x001F70E6 File Offset: 0x001F52E6
		public override IEqualityComparer<string> ParticipantRepresentationComparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x06007168 RID: 29032 RVA: 0x001F70F8 File Offset: 0x001F52F8
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			IDictionary<RecipientItemType, HashSet<string>> recipients = null;
			foreach (ComplexParticipantExtractorBase<string> complexParticipantExtractorBase in ReplyDisplayNamesProperty.Extractors)
			{
				if (complexParticipantExtractorBase.ShouldExtract(propertyBag))
				{
					object result;
					if (complexParticipantExtractorBase.Extract(propertyBag, (Participant participant) => participant.DisplayName, this.ParticipantRepresentationComparer, out recipients))
					{
						result = ReplyAllParticipantsRepresentationProperty<string>.BuildToAndCCRecipients<string>(recipients, this.ParticipantRepresentationComparer);
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

		// Token: 0x04004EAB RID: 20139
		private static ComplexParticipantExtractorBase<string>[] Extractors = new ComplexParticipantExtractorBase<string>[]
		{
			new ComplexParticipantExtractorFromICoreItem<string>(),
			new ComplexParticipantExtractorFromPropertyBag()
		};
	}
}
