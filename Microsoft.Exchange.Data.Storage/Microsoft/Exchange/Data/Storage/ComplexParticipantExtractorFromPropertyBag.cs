using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C46 RID: 3142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ComplexParticipantExtractorFromPropertyBag : ComplexParticipantExtractorBase<string>
	{
		// Token: 0x06006F2D RID: 28461 RVA: 0x001DE4F0 File Offset: 0x001DC6F0
		protected override bool ExtractReplyTo(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<string>.ParticipantConverter converter, out IList<string> replyToRecipients)
		{
			return Participant.TryGetParticipantsFromDisplayNameProperty(propertyBag, ComplexParticipantExtractorFromPropertyBag.ReplyToNamesPropertyDefinition, out replyToRecipients);
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x001DE500 File Offset: 0x001DC700
		protected override bool ExtractToAndCC(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<string>.ParticipantConverter converter, IEqualityComparer<string> comparer, out IDictionary<RecipientItemType, HashSet<string>> recipientTable)
		{
			recipientTable = ComplexParticipantExtractorBase<string>.InstantiateResultType(comparer);
			IList<string> other;
			if (!Participant.TryGetParticipantsFromDisplayNameProperty(propertyBag, InternalSchema.DisplayToInternal, out other))
			{
				return false;
			}
			IList<string> other2;
			if (!Participant.TryGetParticipantsFromDisplayNameProperty(propertyBag, InternalSchema.DisplayCcInternal, out other2))
			{
				return false;
			}
			recipientTable[RecipientItemType.To].UnionWith(other);
			recipientTable[RecipientItemType.Cc].UnionWith(other2);
			return true;
		}

		// Token: 0x0400423E RID: 16958
		private static PropertyTagPropertyDefinition ReplyToNamesPropertyDefinition = InternalSchema.MapiReplyToNames;
	}
}
