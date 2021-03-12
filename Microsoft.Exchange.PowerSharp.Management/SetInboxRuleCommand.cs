using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C55 RID: 3157
	public class SetInboxRuleCommand : SyntheticCommandWithPipelineInputNoOutput<InboxRule>
	{
		// Token: 0x06009A68 RID: 39528 RVA: 0x000E0322 File Offset: 0x000DE522
		private SetInboxRuleCommand() : base("Set-InboxRule")
		{
		}

		// Token: 0x06009A69 RID: 39529 RVA: 0x000E032F File Offset: 0x000DE52F
		public SetInboxRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009A6A RID: 39530 RVA: 0x000E033E File Offset: 0x000DE53E
		public virtual SetInboxRuleCommand SetParameters(SetInboxRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009A6B RID: 39531 RVA: 0x000E0348 File Offset: 0x000DE548
		public virtual SetInboxRuleCommand SetParameters(SetInboxRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C56 RID: 3158
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006D77 RID: 28023
			// (set) Token: 0x06009A6C RID: 39532 RVA: 0x000E0352 File Offset: 0x000DE552
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006D78 RID: 28024
			// (set) Token: 0x06009A6D RID: 39533 RVA: 0x000E036A File Offset: 0x000DE56A
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006D79 RID: 28025
			// (set) Token: 0x06009A6E RID: 39534 RVA: 0x000E0382 File Offset: 0x000DE582
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006D7A RID: 28026
			// (set) Token: 0x06009A6F RID: 39535 RVA: 0x000E03A0 File Offset: 0x000DE5A0
			public virtual RecipientIdParameter From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x17006D7B RID: 28027
			// (set) Token: 0x06009A70 RID: 39536 RVA: 0x000E03B3 File Offset: 0x000DE5B3
			public virtual RecipientIdParameter ExceptIfFrom
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFrom"] = value;
				}
			}

			// Token: 0x17006D7C RID: 28028
			// (set) Token: 0x06009A71 RID: 39537 RVA: 0x000E03C6 File Offset: 0x000DE5C6
			public virtual MessageClassificationIdParameter HasClassification
			{
				set
				{
					base.PowerSharpParameters["HasClassification"] = value;
				}
			}

			// Token: 0x17006D7D RID: 28029
			// (set) Token: 0x06009A72 RID: 39538 RVA: 0x000E03D9 File Offset: 0x000DE5D9
			public virtual MessageClassificationIdParameter ExceptIfHasClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasClassification"] = value;
				}
			}

			// Token: 0x17006D7E RID: 28030
			// (set) Token: 0x06009A73 RID: 39539 RVA: 0x000E03EC File Offset: 0x000DE5EC
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17006D7F RID: 28031
			// (set) Token: 0x06009A74 RID: 39540 RVA: 0x000E03FF File Offset: 0x000DE5FF
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17006D80 RID: 28032
			// (set) Token: 0x06009A75 RID: 39541 RVA: 0x000E0412 File Offset: 0x000DE612
			public virtual string CopyToFolder
			{
				set
				{
					base.PowerSharpParameters["CopyToFolder"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17006D81 RID: 28033
			// (set) Token: 0x06009A76 RID: 39542 RVA: 0x000E0430 File Offset: 0x000DE630
			public virtual RecipientIdParameter ForwardAsAttachmentTo
			{
				set
				{
					base.PowerSharpParameters["ForwardAsAttachmentTo"] = value;
				}
			}

			// Token: 0x17006D82 RID: 28034
			// (set) Token: 0x06009A77 RID: 39543 RVA: 0x000E0443 File Offset: 0x000DE643
			public virtual RecipientIdParameter ForwardTo
			{
				set
				{
					base.PowerSharpParameters["ForwardTo"] = value;
				}
			}

			// Token: 0x17006D83 RID: 28035
			// (set) Token: 0x06009A78 RID: 39544 RVA: 0x000E0456 File Offset: 0x000DE656
			public virtual string MoveToFolder
			{
				set
				{
					base.PowerSharpParameters["MoveToFolder"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17006D84 RID: 28036
			// (set) Token: 0x06009A79 RID: 39545 RVA: 0x000E0474 File Offset: 0x000DE674
			public virtual RecipientIdParameter RedirectTo
			{
				set
				{
					base.PowerSharpParameters["RedirectTo"] = value;
				}
			}

			// Token: 0x17006D85 RID: 28037
			// (set) Token: 0x06009A7A RID: 39546 RVA: 0x000E0487 File Offset: 0x000DE687
			public virtual AggregationSubscriptionIdentity FromSubscription
			{
				set
				{
					base.PowerSharpParameters["FromSubscription"] = value;
				}
			}

			// Token: 0x17006D86 RID: 28038
			// (set) Token: 0x06009A7B RID: 39547 RVA: 0x000E049A File Offset: 0x000DE69A
			public virtual AggregationSubscriptionIdentity ExceptIfFromSubscription
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromSubscription"] = value;
				}
			}

			// Token: 0x17006D87 RID: 28039
			// (set) Token: 0x06009A7C RID: 39548 RVA: 0x000E04AD File Offset: 0x000DE6AD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006D88 RID: 28040
			// (set) Token: 0x06009A7D RID: 39549 RVA: 0x000E04C0 File Offset: 0x000DE6C0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006D89 RID: 28041
			// (set) Token: 0x06009A7E RID: 39550 RVA: 0x000E04D3 File Offset: 0x000DE6D3
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17006D8A RID: 28042
			// (set) Token: 0x06009A7F RID: 39551 RVA: 0x000E04EB File Offset: 0x000DE6EB
			public virtual MultiValuedProperty<string> BodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["BodyContainsWords"] = value;
				}
			}

			// Token: 0x17006D8B RID: 28043
			// (set) Token: 0x06009A80 RID: 39552 RVA: 0x000E04FE File Offset: 0x000DE6FE
			public virtual MultiValuedProperty<string> ExceptIfBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006D8C RID: 28044
			// (set) Token: 0x06009A81 RID: 39553 RVA: 0x000E0511 File Offset: 0x000DE711
			public virtual string FlaggedForAction
			{
				set
				{
					base.PowerSharpParameters["FlaggedForAction"] = value;
				}
			}

			// Token: 0x17006D8D RID: 28045
			// (set) Token: 0x06009A82 RID: 39554 RVA: 0x000E0524 File Offset: 0x000DE724
			public virtual string ExceptIfFlaggedForAction
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFlaggedForAction"] = value;
				}
			}

			// Token: 0x17006D8E RID: 28046
			// (set) Token: 0x06009A83 RID: 39555 RVA: 0x000E0537 File Offset: 0x000DE737
			public virtual MultiValuedProperty<string> FromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["FromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006D8F RID: 28047
			// (set) Token: 0x06009A84 RID: 39556 RVA: 0x000E054A File Offset: 0x000DE74A
			public virtual MultiValuedProperty<string> ExceptIfFromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006D90 RID: 28048
			// (set) Token: 0x06009A85 RID: 39557 RVA: 0x000E055D File Offset: 0x000DE75D
			public virtual bool HasAttachment
			{
				set
				{
					base.PowerSharpParameters["HasAttachment"] = value;
				}
			}

			// Token: 0x17006D91 RID: 28049
			// (set) Token: 0x06009A86 RID: 39558 RVA: 0x000E0575 File Offset: 0x000DE775
			public virtual bool ExceptIfHasAttachment
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasAttachment"] = value;
				}
			}

			// Token: 0x17006D92 RID: 28050
			// (set) Token: 0x06009A87 RID: 39559 RVA: 0x000E058D File Offset: 0x000DE78D
			public virtual MultiValuedProperty<string> HeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsWords"] = value;
				}
			}

			// Token: 0x17006D93 RID: 28051
			// (set) Token: 0x06009A88 RID: 39560 RVA: 0x000E05A0 File Offset: 0x000DE7A0
			public virtual MultiValuedProperty<string> ExceptIfHeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsWords"] = value;
				}
			}

			// Token: 0x17006D94 RID: 28052
			// (set) Token: 0x06009A89 RID: 39561 RVA: 0x000E05B3 File Offset: 0x000DE7B3
			public virtual InboxRuleMessageType? MessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["MessageTypeMatches"] = value;
				}
			}

			// Token: 0x17006D95 RID: 28053
			// (set) Token: 0x06009A8A RID: 39562 RVA: 0x000E05CB File Offset: 0x000DE7CB
			public virtual InboxRuleMessageType? ExceptIfMessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageTypeMatches"] = value;
				}
			}

			// Token: 0x17006D96 RID: 28054
			// (set) Token: 0x06009A8B RID: 39563 RVA: 0x000E05E3 File Offset: 0x000DE7E3
			public virtual bool MyNameInCcBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInCcBox"] = value;
				}
			}

			// Token: 0x17006D97 RID: 28055
			// (set) Token: 0x06009A8C RID: 39564 RVA: 0x000E05FB File Offset: 0x000DE7FB
			public virtual bool ExceptIfMyNameInCcBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInCcBox"] = value;
				}
			}

			// Token: 0x17006D98 RID: 28056
			// (set) Token: 0x06009A8D RID: 39565 RVA: 0x000E0613 File Offset: 0x000DE813
			public virtual bool MyNameInToBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInToBox"] = value;
				}
			}

			// Token: 0x17006D99 RID: 28057
			// (set) Token: 0x06009A8E RID: 39566 RVA: 0x000E062B File Offset: 0x000DE82B
			public virtual bool ExceptIfMyNameInToBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInToBox"] = value;
				}
			}

			// Token: 0x17006D9A RID: 28058
			// (set) Token: 0x06009A8F RID: 39567 RVA: 0x000E0643 File Offset: 0x000DE843
			public virtual bool MyNameInToOrCcBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInToOrCcBox"] = value;
				}
			}

			// Token: 0x17006D9B RID: 28059
			// (set) Token: 0x06009A90 RID: 39568 RVA: 0x000E065B File Offset: 0x000DE85B
			public virtual bool ExceptIfMyNameInToOrCcBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInToOrCcBox"] = value;
				}
			}

			// Token: 0x17006D9C RID: 28060
			// (set) Token: 0x06009A91 RID: 39569 RVA: 0x000E0673 File Offset: 0x000DE873
			public virtual bool MyNameNotInToBox
			{
				set
				{
					base.PowerSharpParameters["MyNameNotInToBox"] = value;
				}
			}

			// Token: 0x17006D9D RID: 28061
			// (set) Token: 0x06009A92 RID: 39570 RVA: 0x000E068B File Offset: 0x000DE88B
			public virtual bool ExceptIfMyNameNotInToBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameNotInToBox"] = value;
				}
			}

			// Token: 0x17006D9E RID: 28062
			// (set) Token: 0x06009A93 RID: 39571 RVA: 0x000E06A3 File Offset: 0x000DE8A3
			public virtual ExDateTime? ReceivedAfterDate
			{
				set
				{
					base.PowerSharpParameters["ReceivedAfterDate"] = value;
				}
			}

			// Token: 0x17006D9F RID: 28063
			// (set) Token: 0x06009A94 RID: 39572 RVA: 0x000E06BB File Offset: 0x000DE8BB
			public virtual ExDateTime? ExceptIfReceivedAfterDate
			{
				set
				{
					base.PowerSharpParameters["ExceptIfReceivedAfterDate"] = value;
				}
			}

			// Token: 0x17006DA0 RID: 28064
			// (set) Token: 0x06009A95 RID: 39573 RVA: 0x000E06D3 File Offset: 0x000DE8D3
			public virtual ExDateTime? ReceivedBeforeDate
			{
				set
				{
					base.PowerSharpParameters["ReceivedBeforeDate"] = value;
				}
			}

			// Token: 0x17006DA1 RID: 28065
			// (set) Token: 0x06009A96 RID: 39574 RVA: 0x000E06EB File Offset: 0x000DE8EB
			public virtual ExDateTime? ExceptIfReceivedBeforeDate
			{
				set
				{
					base.PowerSharpParameters["ExceptIfReceivedBeforeDate"] = value;
				}
			}

			// Token: 0x17006DA2 RID: 28066
			// (set) Token: 0x06009A97 RID: 39575 RVA: 0x000E0703 File Offset: 0x000DE903
			public virtual MultiValuedProperty<string> RecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006DA3 RID: 28067
			// (set) Token: 0x06009A98 RID: 39576 RVA: 0x000E0716 File Offset: 0x000DE916
			public virtual MultiValuedProperty<string> ExceptIfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006DA4 RID: 28068
			// (set) Token: 0x06009A99 RID: 39577 RVA: 0x000E0729 File Offset: 0x000DE929
			public virtual bool SentOnlyToMe
			{
				set
				{
					base.PowerSharpParameters["SentOnlyToMe"] = value;
				}
			}

			// Token: 0x17006DA5 RID: 28069
			// (set) Token: 0x06009A9A RID: 39578 RVA: 0x000E0741 File Offset: 0x000DE941
			public virtual bool ExceptIfSentOnlyToMe
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentOnlyToMe"] = value;
				}
			}

			// Token: 0x17006DA6 RID: 28070
			// (set) Token: 0x06009A9B RID: 39579 RVA: 0x000E0759 File Offset: 0x000DE959
			public virtual MultiValuedProperty<string> SubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectContainsWords"] = value;
				}
			}

			// Token: 0x17006DA7 RID: 28071
			// (set) Token: 0x06009A9C RID: 39580 RVA: 0x000E076C File Offset: 0x000DE96C
			public virtual MultiValuedProperty<string> ExceptIfSubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectContainsWords"] = value;
				}
			}

			// Token: 0x17006DA8 RID: 28072
			// (set) Token: 0x06009A9D RID: 39581 RVA: 0x000E077F File Offset: 0x000DE97F
			public virtual MultiValuedProperty<string> SubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006DA9 RID: 28073
			// (set) Token: 0x06009A9E RID: 39582 RVA: 0x000E0792 File Offset: 0x000DE992
			public virtual MultiValuedProperty<string> ExceptIfSubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006DAA RID: 28074
			// (set) Token: 0x06009A9F RID: 39583 RVA: 0x000E07A5 File Offset: 0x000DE9A5
			public virtual Importance? WithImportance
			{
				set
				{
					base.PowerSharpParameters["WithImportance"] = value;
				}
			}

			// Token: 0x17006DAB RID: 28075
			// (set) Token: 0x06009AA0 RID: 39584 RVA: 0x000E07BD File Offset: 0x000DE9BD
			public virtual Importance? ExceptIfWithImportance
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithImportance"] = value;
				}
			}

			// Token: 0x17006DAC RID: 28076
			// (set) Token: 0x06009AA1 RID: 39585 RVA: 0x000E07D5 File Offset: 0x000DE9D5
			public virtual ByteQuantifiedSize? WithinSizeRangeMaximum
			{
				set
				{
					base.PowerSharpParameters["WithinSizeRangeMaximum"] = value;
				}
			}

			// Token: 0x17006DAD RID: 28077
			// (set) Token: 0x06009AA2 RID: 39586 RVA: 0x000E07ED File Offset: 0x000DE9ED
			public virtual ByteQuantifiedSize? ExceptIfWithinSizeRangeMaximum
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithinSizeRangeMaximum"] = value;
				}
			}

			// Token: 0x17006DAE RID: 28078
			// (set) Token: 0x06009AA3 RID: 39587 RVA: 0x000E0805 File Offset: 0x000DEA05
			public virtual ByteQuantifiedSize? WithinSizeRangeMinimum
			{
				set
				{
					base.PowerSharpParameters["WithinSizeRangeMinimum"] = value;
				}
			}

			// Token: 0x17006DAF RID: 28079
			// (set) Token: 0x06009AA4 RID: 39588 RVA: 0x000E081D File Offset: 0x000DEA1D
			public virtual ByteQuantifiedSize? ExceptIfWithinSizeRangeMinimum
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithinSizeRangeMinimum"] = value;
				}
			}

			// Token: 0x17006DB0 RID: 28080
			// (set) Token: 0x06009AA5 RID: 39589 RVA: 0x000E0835 File Offset: 0x000DEA35
			public virtual Sensitivity? WithSensitivity
			{
				set
				{
					base.PowerSharpParameters["WithSensitivity"] = value;
				}
			}

			// Token: 0x17006DB1 RID: 28081
			// (set) Token: 0x06009AA6 RID: 39590 RVA: 0x000E084D File Offset: 0x000DEA4D
			public virtual Sensitivity? ExceptIfWithSensitivity
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithSensitivity"] = value;
				}
			}

			// Token: 0x17006DB2 RID: 28082
			// (set) Token: 0x06009AA7 RID: 39591 RVA: 0x000E0865 File Offset: 0x000DEA65
			public virtual MultiValuedProperty<string> ApplyCategory
			{
				set
				{
					base.PowerSharpParameters["ApplyCategory"] = value;
				}
			}

			// Token: 0x17006DB3 RID: 28083
			// (set) Token: 0x06009AA8 RID: 39592 RVA: 0x000E0878 File Offset: 0x000DEA78
			public virtual bool DeleteMessage
			{
				set
				{
					base.PowerSharpParameters["DeleteMessage"] = value;
				}
			}

			// Token: 0x17006DB4 RID: 28084
			// (set) Token: 0x06009AA9 RID: 39593 RVA: 0x000E0890 File Offset: 0x000DEA90
			public virtual bool MarkAsRead
			{
				set
				{
					base.PowerSharpParameters["MarkAsRead"] = value;
				}
			}

			// Token: 0x17006DB5 RID: 28085
			// (set) Token: 0x06009AAA RID: 39594 RVA: 0x000E08A8 File Offset: 0x000DEAA8
			public virtual Importance? MarkImportance
			{
				set
				{
					base.PowerSharpParameters["MarkImportance"] = value;
				}
			}

			// Token: 0x17006DB6 RID: 28086
			// (set) Token: 0x06009AAB RID: 39595 RVA: 0x000E08C0 File Offset: 0x000DEAC0
			public virtual MultiValuedProperty<E164Number> SendTextMessageNotificationTo
			{
				set
				{
					base.PowerSharpParameters["SendTextMessageNotificationTo"] = value;
				}
			}

			// Token: 0x17006DB7 RID: 28087
			// (set) Token: 0x06009AAC RID: 39596 RVA: 0x000E08D3 File Offset: 0x000DEAD3
			public virtual bool StopProcessingRules
			{
				set
				{
					base.PowerSharpParameters["StopProcessingRules"] = value;
				}
			}

			// Token: 0x17006DB8 RID: 28088
			// (set) Token: 0x06009AAD RID: 39597 RVA: 0x000E08EB File Offset: 0x000DEAEB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006DB9 RID: 28089
			// (set) Token: 0x06009AAE RID: 39598 RVA: 0x000E0903 File Offset: 0x000DEB03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006DBA RID: 28090
			// (set) Token: 0x06009AAF RID: 39599 RVA: 0x000E091B File Offset: 0x000DEB1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006DBB RID: 28091
			// (set) Token: 0x06009AB0 RID: 39600 RVA: 0x000E0933 File Offset: 0x000DEB33
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006DBC RID: 28092
			// (set) Token: 0x06009AB1 RID: 39601 RVA: 0x000E094B File Offset: 0x000DEB4B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C57 RID: 3159
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006DBD RID: 28093
			// (set) Token: 0x06009AB3 RID: 39603 RVA: 0x000E096B File Offset: 0x000DEB6B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InboxRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17006DBE RID: 28094
			// (set) Token: 0x06009AB4 RID: 39604 RVA: 0x000E0989 File Offset: 0x000DEB89
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006DBF RID: 28095
			// (set) Token: 0x06009AB5 RID: 39605 RVA: 0x000E09A1 File Offset: 0x000DEBA1
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006DC0 RID: 28096
			// (set) Token: 0x06009AB6 RID: 39606 RVA: 0x000E09B9 File Offset: 0x000DEBB9
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006DC1 RID: 28097
			// (set) Token: 0x06009AB7 RID: 39607 RVA: 0x000E09D7 File Offset: 0x000DEBD7
			public virtual RecipientIdParameter From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x17006DC2 RID: 28098
			// (set) Token: 0x06009AB8 RID: 39608 RVA: 0x000E09EA File Offset: 0x000DEBEA
			public virtual RecipientIdParameter ExceptIfFrom
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFrom"] = value;
				}
			}

			// Token: 0x17006DC3 RID: 28099
			// (set) Token: 0x06009AB9 RID: 39609 RVA: 0x000E09FD File Offset: 0x000DEBFD
			public virtual MessageClassificationIdParameter HasClassification
			{
				set
				{
					base.PowerSharpParameters["HasClassification"] = value;
				}
			}

			// Token: 0x17006DC4 RID: 28100
			// (set) Token: 0x06009ABA RID: 39610 RVA: 0x000E0A10 File Offset: 0x000DEC10
			public virtual MessageClassificationIdParameter ExceptIfHasClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasClassification"] = value;
				}
			}

			// Token: 0x17006DC5 RID: 28101
			// (set) Token: 0x06009ABB RID: 39611 RVA: 0x000E0A23 File Offset: 0x000DEC23
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17006DC6 RID: 28102
			// (set) Token: 0x06009ABC RID: 39612 RVA: 0x000E0A36 File Offset: 0x000DEC36
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17006DC7 RID: 28103
			// (set) Token: 0x06009ABD RID: 39613 RVA: 0x000E0A49 File Offset: 0x000DEC49
			public virtual string CopyToFolder
			{
				set
				{
					base.PowerSharpParameters["CopyToFolder"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17006DC8 RID: 28104
			// (set) Token: 0x06009ABE RID: 39614 RVA: 0x000E0A67 File Offset: 0x000DEC67
			public virtual RecipientIdParameter ForwardAsAttachmentTo
			{
				set
				{
					base.PowerSharpParameters["ForwardAsAttachmentTo"] = value;
				}
			}

			// Token: 0x17006DC9 RID: 28105
			// (set) Token: 0x06009ABF RID: 39615 RVA: 0x000E0A7A File Offset: 0x000DEC7A
			public virtual RecipientIdParameter ForwardTo
			{
				set
				{
					base.PowerSharpParameters["ForwardTo"] = value;
				}
			}

			// Token: 0x17006DCA RID: 28106
			// (set) Token: 0x06009AC0 RID: 39616 RVA: 0x000E0A8D File Offset: 0x000DEC8D
			public virtual string MoveToFolder
			{
				set
				{
					base.PowerSharpParameters["MoveToFolder"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17006DCB RID: 28107
			// (set) Token: 0x06009AC1 RID: 39617 RVA: 0x000E0AAB File Offset: 0x000DECAB
			public virtual RecipientIdParameter RedirectTo
			{
				set
				{
					base.PowerSharpParameters["RedirectTo"] = value;
				}
			}

			// Token: 0x17006DCC RID: 28108
			// (set) Token: 0x06009AC2 RID: 39618 RVA: 0x000E0ABE File Offset: 0x000DECBE
			public virtual AggregationSubscriptionIdentity FromSubscription
			{
				set
				{
					base.PowerSharpParameters["FromSubscription"] = value;
				}
			}

			// Token: 0x17006DCD RID: 28109
			// (set) Token: 0x06009AC3 RID: 39619 RVA: 0x000E0AD1 File Offset: 0x000DECD1
			public virtual AggregationSubscriptionIdentity ExceptIfFromSubscription
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromSubscription"] = value;
				}
			}

			// Token: 0x17006DCE RID: 28110
			// (set) Token: 0x06009AC4 RID: 39620 RVA: 0x000E0AE4 File Offset: 0x000DECE4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006DCF RID: 28111
			// (set) Token: 0x06009AC5 RID: 39621 RVA: 0x000E0AF7 File Offset: 0x000DECF7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006DD0 RID: 28112
			// (set) Token: 0x06009AC6 RID: 39622 RVA: 0x000E0B0A File Offset: 0x000DED0A
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17006DD1 RID: 28113
			// (set) Token: 0x06009AC7 RID: 39623 RVA: 0x000E0B22 File Offset: 0x000DED22
			public virtual MultiValuedProperty<string> BodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["BodyContainsWords"] = value;
				}
			}

			// Token: 0x17006DD2 RID: 28114
			// (set) Token: 0x06009AC8 RID: 39624 RVA: 0x000E0B35 File Offset: 0x000DED35
			public virtual MultiValuedProperty<string> ExceptIfBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006DD3 RID: 28115
			// (set) Token: 0x06009AC9 RID: 39625 RVA: 0x000E0B48 File Offset: 0x000DED48
			public virtual string FlaggedForAction
			{
				set
				{
					base.PowerSharpParameters["FlaggedForAction"] = value;
				}
			}

			// Token: 0x17006DD4 RID: 28116
			// (set) Token: 0x06009ACA RID: 39626 RVA: 0x000E0B5B File Offset: 0x000DED5B
			public virtual string ExceptIfFlaggedForAction
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFlaggedForAction"] = value;
				}
			}

			// Token: 0x17006DD5 RID: 28117
			// (set) Token: 0x06009ACB RID: 39627 RVA: 0x000E0B6E File Offset: 0x000DED6E
			public virtual MultiValuedProperty<string> FromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["FromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006DD6 RID: 28118
			// (set) Token: 0x06009ACC RID: 39628 RVA: 0x000E0B81 File Offset: 0x000DED81
			public virtual MultiValuedProperty<string> ExceptIfFromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006DD7 RID: 28119
			// (set) Token: 0x06009ACD RID: 39629 RVA: 0x000E0B94 File Offset: 0x000DED94
			public virtual bool HasAttachment
			{
				set
				{
					base.PowerSharpParameters["HasAttachment"] = value;
				}
			}

			// Token: 0x17006DD8 RID: 28120
			// (set) Token: 0x06009ACE RID: 39630 RVA: 0x000E0BAC File Offset: 0x000DEDAC
			public virtual bool ExceptIfHasAttachment
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasAttachment"] = value;
				}
			}

			// Token: 0x17006DD9 RID: 28121
			// (set) Token: 0x06009ACF RID: 39631 RVA: 0x000E0BC4 File Offset: 0x000DEDC4
			public virtual MultiValuedProperty<string> HeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsWords"] = value;
				}
			}

			// Token: 0x17006DDA RID: 28122
			// (set) Token: 0x06009AD0 RID: 39632 RVA: 0x000E0BD7 File Offset: 0x000DEDD7
			public virtual MultiValuedProperty<string> ExceptIfHeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsWords"] = value;
				}
			}

			// Token: 0x17006DDB RID: 28123
			// (set) Token: 0x06009AD1 RID: 39633 RVA: 0x000E0BEA File Offset: 0x000DEDEA
			public virtual InboxRuleMessageType? MessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["MessageTypeMatches"] = value;
				}
			}

			// Token: 0x17006DDC RID: 28124
			// (set) Token: 0x06009AD2 RID: 39634 RVA: 0x000E0C02 File Offset: 0x000DEE02
			public virtual InboxRuleMessageType? ExceptIfMessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageTypeMatches"] = value;
				}
			}

			// Token: 0x17006DDD RID: 28125
			// (set) Token: 0x06009AD3 RID: 39635 RVA: 0x000E0C1A File Offset: 0x000DEE1A
			public virtual bool MyNameInCcBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInCcBox"] = value;
				}
			}

			// Token: 0x17006DDE RID: 28126
			// (set) Token: 0x06009AD4 RID: 39636 RVA: 0x000E0C32 File Offset: 0x000DEE32
			public virtual bool ExceptIfMyNameInCcBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInCcBox"] = value;
				}
			}

			// Token: 0x17006DDF RID: 28127
			// (set) Token: 0x06009AD5 RID: 39637 RVA: 0x000E0C4A File Offset: 0x000DEE4A
			public virtual bool MyNameInToBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInToBox"] = value;
				}
			}

			// Token: 0x17006DE0 RID: 28128
			// (set) Token: 0x06009AD6 RID: 39638 RVA: 0x000E0C62 File Offset: 0x000DEE62
			public virtual bool ExceptIfMyNameInToBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInToBox"] = value;
				}
			}

			// Token: 0x17006DE1 RID: 28129
			// (set) Token: 0x06009AD7 RID: 39639 RVA: 0x000E0C7A File Offset: 0x000DEE7A
			public virtual bool MyNameInToOrCcBox
			{
				set
				{
					base.PowerSharpParameters["MyNameInToOrCcBox"] = value;
				}
			}

			// Token: 0x17006DE2 RID: 28130
			// (set) Token: 0x06009AD8 RID: 39640 RVA: 0x000E0C92 File Offset: 0x000DEE92
			public virtual bool ExceptIfMyNameInToOrCcBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameInToOrCcBox"] = value;
				}
			}

			// Token: 0x17006DE3 RID: 28131
			// (set) Token: 0x06009AD9 RID: 39641 RVA: 0x000E0CAA File Offset: 0x000DEEAA
			public virtual bool MyNameNotInToBox
			{
				set
				{
					base.PowerSharpParameters["MyNameNotInToBox"] = value;
				}
			}

			// Token: 0x17006DE4 RID: 28132
			// (set) Token: 0x06009ADA RID: 39642 RVA: 0x000E0CC2 File Offset: 0x000DEEC2
			public virtual bool ExceptIfMyNameNotInToBox
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMyNameNotInToBox"] = value;
				}
			}

			// Token: 0x17006DE5 RID: 28133
			// (set) Token: 0x06009ADB RID: 39643 RVA: 0x000E0CDA File Offset: 0x000DEEDA
			public virtual ExDateTime? ReceivedAfterDate
			{
				set
				{
					base.PowerSharpParameters["ReceivedAfterDate"] = value;
				}
			}

			// Token: 0x17006DE6 RID: 28134
			// (set) Token: 0x06009ADC RID: 39644 RVA: 0x000E0CF2 File Offset: 0x000DEEF2
			public virtual ExDateTime? ExceptIfReceivedAfterDate
			{
				set
				{
					base.PowerSharpParameters["ExceptIfReceivedAfterDate"] = value;
				}
			}

			// Token: 0x17006DE7 RID: 28135
			// (set) Token: 0x06009ADD RID: 39645 RVA: 0x000E0D0A File Offset: 0x000DEF0A
			public virtual ExDateTime? ReceivedBeforeDate
			{
				set
				{
					base.PowerSharpParameters["ReceivedBeforeDate"] = value;
				}
			}

			// Token: 0x17006DE8 RID: 28136
			// (set) Token: 0x06009ADE RID: 39646 RVA: 0x000E0D22 File Offset: 0x000DEF22
			public virtual ExDateTime? ExceptIfReceivedBeforeDate
			{
				set
				{
					base.PowerSharpParameters["ExceptIfReceivedBeforeDate"] = value;
				}
			}

			// Token: 0x17006DE9 RID: 28137
			// (set) Token: 0x06009ADF RID: 39647 RVA: 0x000E0D3A File Offset: 0x000DEF3A
			public virtual MultiValuedProperty<string> RecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006DEA RID: 28138
			// (set) Token: 0x06009AE0 RID: 39648 RVA: 0x000E0D4D File Offset: 0x000DEF4D
			public virtual MultiValuedProperty<string> ExceptIfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17006DEB RID: 28139
			// (set) Token: 0x06009AE1 RID: 39649 RVA: 0x000E0D60 File Offset: 0x000DEF60
			public virtual bool SentOnlyToMe
			{
				set
				{
					base.PowerSharpParameters["SentOnlyToMe"] = value;
				}
			}

			// Token: 0x17006DEC RID: 28140
			// (set) Token: 0x06009AE2 RID: 39650 RVA: 0x000E0D78 File Offset: 0x000DEF78
			public virtual bool ExceptIfSentOnlyToMe
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentOnlyToMe"] = value;
				}
			}

			// Token: 0x17006DED RID: 28141
			// (set) Token: 0x06009AE3 RID: 39651 RVA: 0x000E0D90 File Offset: 0x000DEF90
			public virtual MultiValuedProperty<string> SubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectContainsWords"] = value;
				}
			}

			// Token: 0x17006DEE RID: 28142
			// (set) Token: 0x06009AE4 RID: 39652 RVA: 0x000E0DA3 File Offset: 0x000DEFA3
			public virtual MultiValuedProperty<string> ExceptIfSubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectContainsWords"] = value;
				}
			}

			// Token: 0x17006DEF RID: 28143
			// (set) Token: 0x06009AE5 RID: 39653 RVA: 0x000E0DB6 File Offset: 0x000DEFB6
			public virtual MultiValuedProperty<string> SubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006DF0 RID: 28144
			// (set) Token: 0x06009AE6 RID: 39654 RVA: 0x000E0DC9 File Offset: 0x000DEFC9
			public virtual MultiValuedProperty<string> ExceptIfSubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17006DF1 RID: 28145
			// (set) Token: 0x06009AE7 RID: 39655 RVA: 0x000E0DDC File Offset: 0x000DEFDC
			public virtual Importance? WithImportance
			{
				set
				{
					base.PowerSharpParameters["WithImportance"] = value;
				}
			}

			// Token: 0x17006DF2 RID: 28146
			// (set) Token: 0x06009AE8 RID: 39656 RVA: 0x000E0DF4 File Offset: 0x000DEFF4
			public virtual Importance? ExceptIfWithImportance
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithImportance"] = value;
				}
			}

			// Token: 0x17006DF3 RID: 28147
			// (set) Token: 0x06009AE9 RID: 39657 RVA: 0x000E0E0C File Offset: 0x000DF00C
			public virtual ByteQuantifiedSize? WithinSizeRangeMaximum
			{
				set
				{
					base.PowerSharpParameters["WithinSizeRangeMaximum"] = value;
				}
			}

			// Token: 0x17006DF4 RID: 28148
			// (set) Token: 0x06009AEA RID: 39658 RVA: 0x000E0E24 File Offset: 0x000DF024
			public virtual ByteQuantifiedSize? ExceptIfWithinSizeRangeMaximum
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithinSizeRangeMaximum"] = value;
				}
			}

			// Token: 0x17006DF5 RID: 28149
			// (set) Token: 0x06009AEB RID: 39659 RVA: 0x000E0E3C File Offset: 0x000DF03C
			public virtual ByteQuantifiedSize? WithinSizeRangeMinimum
			{
				set
				{
					base.PowerSharpParameters["WithinSizeRangeMinimum"] = value;
				}
			}

			// Token: 0x17006DF6 RID: 28150
			// (set) Token: 0x06009AEC RID: 39660 RVA: 0x000E0E54 File Offset: 0x000DF054
			public virtual ByteQuantifiedSize? ExceptIfWithinSizeRangeMinimum
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithinSizeRangeMinimum"] = value;
				}
			}

			// Token: 0x17006DF7 RID: 28151
			// (set) Token: 0x06009AED RID: 39661 RVA: 0x000E0E6C File Offset: 0x000DF06C
			public virtual Sensitivity? WithSensitivity
			{
				set
				{
					base.PowerSharpParameters["WithSensitivity"] = value;
				}
			}

			// Token: 0x17006DF8 RID: 28152
			// (set) Token: 0x06009AEE RID: 39662 RVA: 0x000E0E84 File Offset: 0x000DF084
			public virtual Sensitivity? ExceptIfWithSensitivity
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithSensitivity"] = value;
				}
			}

			// Token: 0x17006DF9 RID: 28153
			// (set) Token: 0x06009AEF RID: 39663 RVA: 0x000E0E9C File Offset: 0x000DF09C
			public virtual MultiValuedProperty<string> ApplyCategory
			{
				set
				{
					base.PowerSharpParameters["ApplyCategory"] = value;
				}
			}

			// Token: 0x17006DFA RID: 28154
			// (set) Token: 0x06009AF0 RID: 39664 RVA: 0x000E0EAF File Offset: 0x000DF0AF
			public virtual bool DeleteMessage
			{
				set
				{
					base.PowerSharpParameters["DeleteMessage"] = value;
				}
			}

			// Token: 0x17006DFB RID: 28155
			// (set) Token: 0x06009AF1 RID: 39665 RVA: 0x000E0EC7 File Offset: 0x000DF0C7
			public virtual bool MarkAsRead
			{
				set
				{
					base.PowerSharpParameters["MarkAsRead"] = value;
				}
			}

			// Token: 0x17006DFC RID: 28156
			// (set) Token: 0x06009AF2 RID: 39666 RVA: 0x000E0EDF File Offset: 0x000DF0DF
			public virtual Importance? MarkImportance
			{
				set
				{
					base.PowerSharpParameters["MarkImportance"] = value;
				}
			}

			// Token: 0x17006DFD RID: 28157
			// (set) Token: 0x06009AF3 RID: 39667 RVA: 0x000E0EF7 File Offset: 0x000DF0F7
			public virtual MultiValuedProperty<E164Number> SendTextMessageNotificationTo
			{
				set
				{
					base.PowerSharpParameters["SendTextMessageNotificationTo"] = value;
				}
			}

			// Token: 0x17006DFE RID: 28158
			// (set) Token: 0x06009AF4 RID: 39668 RVA: 0x000E0F0A File Offset: 0x000DF10A
			public virtual bool StopProcessingRules
			{
				set
				{
					base.PowerSharpParameters["StopProcessingRules"] = value;
				}
			}

			// Token: 0x17006DFF RID: 28159
			// (set) Token: 0x06009AF5 RID: 39669 RVA: 0x000E0F22 File Offset: 0x000DF122
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006E00 RID: 28160
			// (set) Token: 0x06009AF6 RID: 39670 RVA: 0x000E0F3A File Offset: 0x000DF13A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006E01 RID: 28161
			// (set) Token: 0x06009AF7 RID: 39671 RVA: 0x000E0F52 File Offset: 0x000DF152
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006E02 RID: 28162
			// (set) Token: 0x06009AF8 RID: 39672 RVA: 0x000E0F6A File Offset: 0x000DF16A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006E03 RID: 28163
			// (set) Token: 0x06009AF9 RID: 39673 RVA: 0x000E0F82 File Offset: 0x000DF182
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
