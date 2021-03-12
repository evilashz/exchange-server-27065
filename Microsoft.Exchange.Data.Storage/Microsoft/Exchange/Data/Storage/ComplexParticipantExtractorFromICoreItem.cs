using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C45 RID: 3141
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ComplexParticipantExtractorFromICoreItem<T> : ComplexParticipantExtractorBase<T>
	{
		// Token: 0x06006F29 RID: 28457 RVA: 0x001DE3C8 File Offset: 0x001DC5C8
		public override bool ShouldExtract(PropertyBag.BasicPropertyStore propertyBag)
		{
			return base.ShouldExtract(propertyBag) && propertyBag.Context.CoreObject is ICoreItem;
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x001DE3EC File Offset: 0x001DC5EC
		protected override bool ExtractToAndCC(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<T>.ParticipantConverter converter, IEqualityComparer<T> comparer, out IDictionary<RecipientItemType, HashSet<T>> recipientTable)
		{
			ICoreItem coreItem = propertyBag.Context.CoreObject as ICoreItem;
			recipientTable = null;
			if (coreItem != null)
			{
				CoreRecipientCollection recipientCollection = coreItem.GetRecipientCollection(true);
				if (recipientCollection != null)
				{
					recipientTable = ComplexParticipantExtractorBase<T>.InstantiateResultType(comparer);
					foreach (CoreRecipient coreRecipient in recipientCollection)
					{
						if (coreRecipient.RecipientItemType == RecipientItemType.To || coreRecipient.RecipientItemType == RecipientItemType.Cc)
						{
							recipientTable[coreRecipient.RecipientItemType].Add(converter(coreRecipient.Participant));
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x001DE4A8 File Offset: 0x001DC6A8
		protected override bool ExtractReplyTo(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<T>.ParticipantConverter converter, out IList<T> replyToRecipients)
		{
			ReplyTo first = new ReplyTo((PropertyBag)propertyBag);
			replyToRecipients = new List<T>(from participant in first
			select converter(participant));
			return true;
		}
	}
}
