using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200045D RID: 1117
	internal class SupportedConditionNames
	{
		// Token: 0x040025F8 RID: 9720
		public const string From = "From";

		// Token: 0x040025F9 RID: 9721
		public const string SentTo = "SentTo";

		// Token: 0x040025FA RID: 9722
		public const string FromScope = "FromScope";

		// Token: 0x040025FB RID: 9723
		public const string SentToScope = "SentToScope";

		// Token: 0x040025FC RID: 9724
		public const string FromMemberOf = "FromMemberOf";

		// Token: 0x040025FD RID: 9725
		public const string SentToMemberOf = "SentToMemberOf";

		// Token: 0x040025FE RID: 9726
		public const string SubjectOrBodyContains = "SubjectOrBodyContains";

		// Token: 0x040025FF RID: 9727
		public const string FromAddressContains = "FromAddressContains";

		// Token: 0x04002600 RID: 9728
		public const string RecipientAddressContains = "RecipientAddressContains";

		// Token: 0x04002601 RID: 9729
		public const string AttachmentContainsWords = "AttachmentContainsWords";

		// Token: 0x04002602 RID: 9730
		public const string AttachmentMatchesPatterns = "AttachmentMatchesPatterns";

		// Token: 0x04002603 RID: 9731
		public const string AttachmentIsUnsupported = "AttachmentIsUnsupported";

		// Token: 0x04002604 RID: 9732
		public const string SubjectOrBodyMatchesPatterns = "SubjectOrBodyMatchesPatterns";

		// Token: 0x04002605 RID: 9733
		public const string FromAddressMatchesPatterns = "FromAddressMatchesPatterns";

		// Token: 0x04002606 RID: 9734
		public const string RecipientAddressMatchesPatterns = "RecipientAddressMatchesPatterns";

		// Token: 0x04002607 RID: 9735
		public const string AttachmentNameMatchesPatterns = "AttachmentNameMatchesPatterns";

		// Token: 0x04002608 RID: 9736
		public const string MessageTypeMatches = "MessageTypeMatches";

		// Token: 0x04002609 RID: 9737
		public const string HasClassification = "HasClassification";

		// Token: 0x0400260A RID: 9738
		public const string HasNoClassification = "HasNoClassification";

		// Token: 0x0400260B RID: 9739
		public const string SubjectContainsWords = "SubjectContainsWords";

		// Token: 0x0400260C RID: 9740
		public const string SubjectMatchesPatterns = "SubjectMatchesPatterns";

		// Token: 0x0400260D RID: 9741
		public const string AnyOfToHeader = "AnyOfToHeader";

		// Token: 0x0400260E RID: 9742
		public const string AnyOfToHeaderMemberOf = "AnyOfToHeaderMemberOf";

		// Token: 0x0400260F RID: 9743
		public const string AnyOfCcHeader = "AnyOfCcHeader";

		// Token: 0x04002610 RID: 9744
		public const string AnyOfCcHeaderMemberOf = "AnyOfCcHeaderMemberOf";

		// Token: 0x04002611 RID: 9745
		public const string AnyOfToCcHeader = "AnyOfToCcHeader";

		// Token: 0x04002612 RID: 9746
		public const string AnyOfToCcHeaderMemberOf = "AnyOfToCcHeaderMemberOf";

		// Token: 0x04002613 RID: 9747
		public const string SenderManagementRelationship = "SenderManagementRelationship";

		// Token: 0x04002614 RID: 9748
		public const string SCLOver = "SCLOver";

		// Token: 0x04002615 RID: 9749
		public const string WithImportance = "WithImportance";

		// Token: 0x04002616 RID: 9750
		public const string SenderInRecipientList = "SenderInRecipientList";

		// Token: 0x04002617 RID: 9751
		public const string RecipientInSenderList = "RecipientInSenderList";

		// Token: 0x04002618 RID: 9752
		public const string HeaderContains = "HeaderContains";

		// Token: 0x04002619 RID: 9753
		public const string BetweenMemberOf = "BetweenMemberOf";

		// Token: 0x0400261A RID: 9754
		public const string HeaderMatches = "HeaderMatches";

		// Token: 0x0400261B RID: 9755
		public const string ManagerForEvaluatedUser = "ManagerForEvaluatedUser";

		// Token: 0x0400261C RID: 9756
		public const string ADComparisonAttribute = "ADComparisonAttribute";

		// Token: 0x0400261D RID: 9757
		public const string AttachmentSizeOver = "AttachmentSizeOver";

		// Token: 0x0400261E RID: 9758
		public const string AttachmentProcessingLimitsExceeded = "AttachmentProcessingLimitExceeded";

		// Token: 0x0400261F RID: 9759
		public const string SenderADAttributeContainsWords = "SenderADAttributeContainsWords";

		// Token: 0x04002620 RID: 9760
		public const string SenderADAttributeMatchesPatterns = "SenderADAttributeMatchesPatterns";

		// Token: 0x04002621 RID: 9761
		public const string RecipientADAttributeContainsWords = "RecipientADAttributeContainsWords";

		// Token: 0x04002622 RID: 9762
		public const string RecipientADAttributeMatchesPatterns = "RecipientADAttributeMatchesPatterns";

		// Token: 0x04002623 RID: 9763
		public const string MessageContainsDataClassifications = "MessageContainsDataClassifications";

		// Token: 0x04002624 RID: 9764
		public const string AttachmentExtensionMatchesWords = "AttachmentExtensionMatchesWords";

		// Token: 0x04002625 RID: 9765
		public const string MessageSizeOver = "MessageSizeOver";

		// Token: 0x04002626 RID: 9766
		public const string HasSenderOverride = "HasSenderOverride";

		// Token: 0x04002627 RID: 9767
		public const string AttachmentHasExecutableContent = "AttachmentHasExecutableContent";

		// Token: 0x04002628 RID: 9768
		public const string SenderIpRange = "SenderIpRange";

		// Token: 0x04002629 RID: 9769
		public const string SenderDomainIs = "SenderDomainIs";

		// Token: 0x0400262A RID: 9770
		public const string RecipientDomainIs = "RecipientDomainIs";

		// Token: 0x0400262B RID: 9771
		public const string ContentCharacterSetContainsWords = "ContentCharacterSetContainsWords";

		// Token: 0x0400262C RID: 9772
		public const string AttachmentIsPasswordProtected = "AttachmentIsPasswordProtected";

		// Token: 0x0400262D RID: 9773
		public const string AnyOfRecipientAddressContainsWords = "AnyOfRecipientAddressContainsWords";

		// Token: 0x0400262E RID: 9774
		public const string AnyOfRecipientAddressMatchesPatterns = "AnyOfRecipientAddressMatchesPatterns";
	}
}
