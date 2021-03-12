using System;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020008EE RID: 2286
	internal static class SearchElementName
	{
		// Token: 0x04002627 RID: 9767
		public const string Restriction = "Restriction";

		// Token: 0x04002628 RID: 9768
		public const string Exists = "Exists";

		// Token: 0x04002629 RID: 9769
		public const string IsEqualTo = "IsEqualTo";

		// Token: 0x0400262A RID: 9770
		public const string IsNotEqualTo = "IsNotEqualTo";

		// Token: 0x0400262B RID: 9771
		public const string IsGreaterThan = "IsGreaterThan";

		// Token: 0x0400262C RID: 9772
		public const string IsGreaterThanOrEqualTo = "IsGreaterThanOrEqualTo";

		// Token: 0x0400262D RID: 9773
		public const string IsLessThan = "IsLessThan";

		// Token: 0x0400262E RID: 9774
		public const string IsLessThanOrEqualTo = "IsLessThanOrEqualTo";

		// Token: 0x0400262F RID: 9775
		public const string Excludes = "Excludes";

		// Token: 0x04002630 RID: 9776
		public const string Contains = "Contains";

		// Token: 0x04002631 RID: 9777
		public const string ContainsValueAttribute = "Value";

		// Token: 0x04002632 RID: 9778
		public const string ContainmentModeAttribute = "ContainmentMode";

		// Token: 0x04002633 RID: 9779
		public const string ContainmentComparisonAttribute = "ContainmentComparison";

		// Token: 0x04002634 RID: 9780
		public const string FullStringContainmentMode = "FullString";

		// Token: 0x04002635 RID: 9781
		public const string PrefixedContainmentMode = "Prefixed";

		// Token: 0x04002636 RID: 9782
		public const string SubstringContainmentMode = "Substring";

		// Token: 0x04002637 RID: 9783
		public const string SuffixedContainmentMode = "Suffixed";

		// Token: 0x04002638 RID: 9784
		public const string PrefixOnWordsContainmentMode = "PrefixOnWords";

		// Token: 0x04002639 RID: 9785
		public const string ExactPhraseContainmentMode = "ExactPhrase";

		// Token: 0x0400263A RID: 9786
		public const string ExactComparisonType = "Exact";

		// Token: 0x0400263B RID: 9787
		public const string IgnoreCaseComparisonType = "IgnoreCase";

		// Token: 0x0400263C RID: 9788
		public const string IgnoreNonSpacingCharactersComparisonType = "IgnoreNonSpacingCharacters";

		// Token: 0x0400263D RID: 9789
		public const string LooseComparisonType = "Loose";

		// Token: 0x0400263E RID: 9790
		public const string IgnoreCaseAndNonSpacingCharactersComparisonType = "IgnoreCaseAndNonSpacingCharacters";

		// Token: 0x0400263F RID: 9791
		public const string LooseAndIgnoreCaseComparisonType = "LooseAndIgnoreCase";

		// Token: 0x04002640 RID: 9792
		public const string LooseAndIgnoreNonSpaceComparisonType = "LooseAndIgnoreNonSpace";

		// Token: 0x04002641 RID: 9793
		public const string LooseAndIgnoreCaseAndIgnoreNonSpaceComparisonType = "LooseAndIgnoreCaseAndIgnoreNonSpace";

		// Token: 0x04002642 RID: 9794
		public const string Not = "Not";

		// Token: 0x04002643 RID: 9795
		public const string And = "And";

		// Token: 0x04002644 RID: 9796
		public const string Or = "Or";

		// Token: 0x04002645 RID: 9797
		public const string ConstantElementName = "Constant";

		// Token: 0x04002646 RID: 9798
		public const string ConstantAttributeName = "Value";

		// Token: 0x04002647 RID: 9799
		public const string BitmaskElementName = "Bitmask";

		// Token: 0x04002648 RID: 9800
		public const string BitmaskAttributeName = "Value";

		// Token: 0x04002649 RID: 9801
		public const string FieldUriOrConstantElementName = "FieldURIOrConstant";

		// Token: 0x0400264A RID: 9802
		public const string ToRecipientsUri = "message:ToRecipients";

		// Token: 0x0400264B RID: 9803
		public const string CcRecipientsUri = "message:CcRecipients";

		// Token: 0x0400264C RID: 9804
		public const string BccRecipientsUri = "message:BccRecipients";

		// Token: 0x0400264D RID: 9805
		public const string RequiredAttendeesUri = "calendar:RequiredAttendees";

		// Token: 0x0400264E RID: 9806
		public const string OptionalAttendeesUri = "calendar:OptionalAttendees";

		// Token: 0x0400264F RID: 9807
		public const string ResourceAttendeesUri = "calendar:Resources";

		// Token: 0x04002650 RID: 9808
		public const string AttachmentsUri = "item:Attachments";

		// Token: 0x04002651 RID: 9809
		public const string InternetMessageHeadersFieldUri = "item:InternetMessageHeader";

		// Token: 0x04002652 RID: 9810
		public const string GroupedItemsElementName = "GroupedItems";

		// Token: 0x04002653 RID: 9811
		public const string GroupIndexElementName = "GroupIndex";

		// Token: 0x04002654 RID: 9812
		public const string ItemsElementName = "Items";
	}
}
