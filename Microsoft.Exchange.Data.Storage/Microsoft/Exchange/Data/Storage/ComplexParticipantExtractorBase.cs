using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C43 RID: 3139
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ComplexParticipantExtractorBase<T>
	{
		// Token: 0x06006F19 RID: 28441 RVA: 0x001DE290 File Offset: 0x001DC490
		protected static IDictionary<RecipientItemType, HashSet<T>> InstantiateResultType(IEqualityComparer<T> participantRepresentationComparer)
		{
			return new Dictionary<RecipientItemType, HashSet<T>>
			{
				{
					RecipientItemType.To,
					new HashSet<T>(participantRepresentationComparer)
				},
				{
					RecipientItemType.Cc,
					new HashSet<T>(participantRepresentationComparer)
				}
			};
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x001DE2BE File Offset: 0x001DC4BE
		public virtual bool ShouldExtract(PropertyBag.BasicPropertyStore propertyBag)
		{
			return ComplexParticipantExtractorBase<T>.CanExtractRecipients(propertyBag);
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x001DE2C8 File Offset: 0x001DC4C8
		public static bool CanExtractRecipients(IStorePropertyBag propertyBag)
		{
			string itemClass = propertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
			return ComplexParticipantExtractorBase<T>.CanExtractRecipients(itemClass);
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x001DE2EC File Offset: 0x001DC4EC
		public static bool CanExtractRecipients(ICorePropertyBag propertyBag)
		{
			string itemClass = propertyBag.TryGetProperty(InternalSchema.ItemClass) as string;
			return ComplexParticipantExtractorBase<T>.CanExtractRecipients(itemClass);
		}

		// Token: 0x06006F1D RID: 28445 RVA: 0x001DE310 File Offset: 0x001DC510
		private static bool CanExtractRecipients(PropertyBag.BasicPropertyStore propertyBag)
		{
			string itemClass = propertyBag.GetValue(InternalSchema.ItemClass) as string;
			return ComplexParticipantExtractorBase<T>.CanExtractRecipients(itemClass);
		}

		// Token: 0x06006F1E RID: 28446 RVA: 0x001DE335 File Offset: 0x001DC535
		private static bool CanExtractRecipients(string itemClass)
		{
			return !string.IsNullOrEmpty(itemClass) && (ObjectClass.IsMessage(itemClass, true) || ObjectClass.IsCalendarItem(itemClass));
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x001DE352 File Offset: 0x001DC552
		public bool Extract(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<T>.ParticipantConverter converter, IEqualityComparer<T> comparer, out IDictionary<RecipientItemType, HashSet<T>> recipientTable, out IList<T> replyToRecipients)
		{
			replyToRecipients = null;
			recipientTable = null;
			return this.ExtractToAndCC(propertyBag, converter, comparer, out recipientTable) && (!this.CanExtractReplyTo(propertyBag) || this.ExtractReplyTo(propertyBag, converter, out replyToRecipients));
		}

		// Token: 0x06006F20 RID: 28448 RVA: 0x001DE380 File Offset: 0x001DC580
		public bool Extract(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<T>.ParticipantConverter converter, IEqualityComparer<T> comparer, out IDictionary<RecipientItemType, HashSet<T>> recipientTable)
		{
			return this.ExtractToAndCC(propertyBag, converter, comparer, out recipientTable);
		}

		// Token: 0x06006F21 RID: 28449
		protected abstract bool ExtractToAndCC(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<T>.ParticipantConverter converter, IEqualityComparer<T> comparer, out IDictionary<RecipientItemType, HashSet<T>> recipientTable);

		// Token: 0x06006F22 RID: 28450
		protected abstract bool ExtractReplyTo(PropertyBag.BasicPropertyStore propertyBag, ComplexParticipantExtractorBase<T>.ParticipantConverter converter, out IList<T> replyToRecipients);

		// Token: 0x06006F23 RID: 28451 RVA: 0x001DE390 File Offset: 0x001DC590
		private bool CanExtractReplyTo(PropertyBag.BasicPropertyStore propertyBag)
		{
			string text = propertyBag.GetValue(InternalSchema.ItemClass) as string;
			return !string.IsNullOrEmpty(text) && ObjectClass.IsMessage(text, false);
		}

		// Token: 0x02000C44 RID: 3140
		// (Invoke) Token: 0x06006F26 RID: 28454
		internal delegate T ParticipantConverter(Participant p);
	}
}
