using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008F4 RID: 2292
	public class NewTransportRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x060072B4 RID: 29364 RVA: 0x000AC94A File Offset: 0x000AAB4A
		private NewTransportRuleCommand() : base("New-TransportRule")
		{
		}

		// Token: 0x060072B5 RID: 29365 RVA: 0x000AC957 File Offset: 0x000AAB57
		public NewTransportRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x000AC966 File Offset: 0x000AAB66
		public virtual NewTransportRuleCommand SetParameters(NewTransportRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008F5 RID: 2293
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004C85 RID: 19589
			// (set) Token: 0x060072B7 RID: 29367 RVA: 0x000AC970 File Offset: 0x000AAB70
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004C86 RID: 19590
			// (set) Token: 0x060072B8 RID: 29368 RVA: 0x000AC983 File Offset: 0x000AAB83
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004C87 RID: 19591
			// (set) Token: 0x060072B9 RID: 29369 RVA: 0x000AC99B File Offset: 0x000AAB9B
			public virtual string Comments
			{
				set
				{
					base.PowerSharpParameters["Comments"] = value;
				}
			}

			// Token: 0x17004C88 RID: 19592
			// (set) Token: 0x060072BA RID: 29370 RVA: 0x000AC9AE File Offset: 0x000AABAE
			public virtual bool UseLegacyRegex
			{
				set
				{
					base.PowerSharpParameters["UseLegacyRegex"] = value;
				}
			}

			// Token: 0x17004C89 RID: 19593
			// (set) Token: 0x060072BB RID: 29371 RVA: 0x000AC9C6 File Offset: 0x000AABC6
			public virtual string DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17004C8A RID: 19594
			// (set) Token: 0x060072BC RID: 29372 RVA: 0x000AC9D9 File Offset: 0x000AABD9
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004C8B RID: 19595
			// (set) Token: 0x060072BD RID: 29373 RVA: 0x000AC9F1 File Offset: 0x000AABF1
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17004C8C RID: 19596
			// (set) Token: 0x060072BE RID: 29374 RVA: 0x000ACA09 File Offset: 0x000AAC09
			public virtual RecipientIdParameter From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x17004C8D RID: 19597
			// (set) Token: 0x060072BF RID: 29375 RVA: 0x000ACA1C File Offset: 0x000AAC1C
			public virtual RecipientIdParameter FromMemberOf
			{
				set
				{
					base.PowerSharpParameters["FromMemberOf"] = value;
				}
			}

			// Token: 0x17004C8E RID: 19598
			// (set) Token: 0x060072C0 RID: 29376 RVA: 0x000ACA2F File Offset: 0x000AAC2F
			public virtual FromUserScope FromScope
			{
				set
				{
					base.PowerSharpParameters["FromScope"] = value;
				}
			}

			// Token: 0x17004C8F RID: 19599
			// (set) Token: 0x060072C1 RID: 29377 RVA: 0x000ACA47 File Offset: 0x000AAC47
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17004C90 RID: 19600
			// (set) Token: 0x060072C2 RID: 29378 RVA: 0x000ACA5A File Offset: 0x000AAC5A
			public virtual RecipientIdParameter SentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["SentToMemberOf"] = value;
				}
			}

			// Token: 0x17004C91 RID: 19601
			// (set) Token: 0x060072C3 RID: 29379 RVA: 0x000ACA6D File Offset: 0x000AAC6D
			public virtual ToUserScope SentToScope
			{
				set
				{
					base.PowerSharpParameters["SentToScope"] = value;
				}
			}

			// Token: 0x17004C92 RID: 19602
			// (set) Token: 0x060072C4 RID: 29380 RVA: 0x000ACA85 File Offset: 0x000AAC85
			public virtual RecipientIdParameter BetweenMemberOf1
			{
				set
				{
					base.PowerSharpParameters["BetweenMemberOf1"] = value;
				}
			}

			// Token: 0x17004C93 RID: 19603
			// (set) Token: 0x060072C5 RID: 29381 RVA: 0x000ACA98 File Offset: 0x000AAC98
			public virtual RecipientIdParameter BetweenMemberOf2
			{
				set
				{
					base.PowerSharpParameters["BetweenMemberOf2"] = value;
				}
			}

			// Token: 0x17004C94 RID: 19604
			// (set) Token: 0x060072C6 RID: 29382 RVA: 0x000ACAAB File Offset: 0x000AACAB
			public virtual RecipientIdParameter ManagerAddresses
			{
				set
				{
					base.PowerSharpParameters["ManagerAddresses"] = value;
				}
			}

			// Token: 0x17004C95 RID: 19605
			// (set) Token: 0x060072C7 RID: 29383 RVA: 0x000ACABE File Offset: 0x000AACBE
			public virtual EvaluatedUser ManagerForEvaluatedUser
			{
				set
				{
					base.PowerSharpParameters["ManagerForEvaluatedUser"] = value;
				}
			}

			// Token: 0x17004C96 RID: 19606
			// (set) Token: 0x060072C8 RID: 29384 RVA: 0x000ACAD6 File Offset: 0x000AACD6
			public virtual ManagementRelationship SenderManagementRelationship
			{
				set
				{
					base.PowerSharpParameters["SenderManagementRelationship"] = value;
				}
			}

			// Token: 0x17004C97 RID: 19607
			// (set) Token: 0x060072C9 RID: 29385 RVA: 0x000ACAEE File Offset: 0x000AACEE
			public virtual ADAttribute ADComparisonAttribute
			{
				set
				{
					base.PowerSharpParameters["ADComparisonAttribute"] = value;
				}
			}

			// Token: 0x17004C98 RID: 19608
			// (set) Token: 0x060072CA RID: 29386 RVA: 0x000ACB06 File Offset: 0x000AAD06
			public virtual Evaluation ADComparisonOperator
			{
				set
				{
					base.PowerSharpParameters["ADComparisonOperator"] = value;
				}
			}

			// Token: 0x17004C99 RID: 19609
			// (set) Token: 0x060072CB RID: 29387 RVA: 0x000ACB1E File Offset: 0x000AAD1E
			public virtual Word SenderADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["SenderADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004C9A RID: 19610
			// (set) Token: 0x060072CC RID: 29388 RVA: 0x000ACB36 File Offset: 0x000AAD36
			public virtual Pattern SenderADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SenderADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004C9B RID: 19611
			// (set) Token: 0x060072CD RID: 29389 RVA: 0x000ACB4E File Offset: 0x000AAD4E
			public virtual Word RecipientADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004C9C RID: 19612
			// (set) Token: 0x060072CE RID: 29390 RVA: 0x000ACB66 File Offset: 0x000AAD66
			public virtual Pattern RecipientADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["RecipientADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004C9D RID: 19613
			// (set) Token: 0x060072CF RID: 29391 RVA: 0x000ACB7E File Offset: 0x000AAD7E
			public virtual RecipientIdParameter AnyOfToHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfToHeader"] = value;
				}
			}

			// Token: 0x17004C9E RID: 19614
			// (set) Token: 0x060072D0 RID: 29392 RVA: 0x000ACB91 File Offset: 0x000AAD91
			public virtual RecipientIdParameter AnyOfToHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfToHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004C9F RID: 19615
			// (set) Token: 0x060072D1 RID: 29393 RVA: 0x000ACBA4 File Offset: 0x000AADA4
			public virtual RecipientIdParameter AnyOfCcHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfCcHeader"] = value;
				}
			}

			// Token: 0x17004CA0 RID: 19616
			// (set) Token: 0x060072D2 RID: 29394 RVA: 0x000ACBB7 File Offset: 0x000AADB7
			public virtual RecipientIdParameter AnyOfCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004CA1 RID: 19617
			// (set) Token: 0x060072D3 RID: 29395 RVA: 0x000ACBCA File Offset: 0x000AADCA
			public virtual RecipientIdParameter AnyOfToCcHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfToCcHeader"] = value;
				}
			}

			// Token: 0x17004CA2 RID: 19618
			// (set) Token: 0x060072D4 RID: 29396 RVA: 0x000ACBDD File Offset: 0x000AADDD
			public virtual RecipientIdParameter AnyOfToCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfToCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004CA3 RID: 19619
			// (set) Token: 0x060072D5 RID: 29397 RVA: 0x000ACBF0 File Offset: 0x000AADF0
			public virtual string HasClassification
			{
				set
				{
					base.PowerSharpParameters["HasClassification"] = value;
				}
			}

			// Token: 0x17004CA4 RID: 19620
			// (set) Token: 0x060072D6 RID: 29398 RVA: 0x000ACC03 File Offset: 0x000AAE03
			public virtual bool HasNoClassification
			{
				set
				{
					base.PowerSharpParameters["HasNoClassification"] = value;
				}
			}

			// Token: 0x17004CA5 RID: 19621
			// (set) Token: 0x060072D7 RID: 29399 RVA: 0x000ACC1B File Offset: 0x000AAE1B
			public virtual Word SubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectContainsWords"] = value;
				}
			}

			// Token: 0x17004CA6 RID: 19622
			// (set) Token: 0x060072D8 RID: 29400 RVA: 0x000ACC33 File Offset: 0x000AAE33
			public virtual Word SubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17004CA7 RID: 19623
			// (set) Token: 0x060072D9 RID: 29401 RVA: 0x000ACC4B File Offset: 0x000AAE4B
			public virtual HeaderName HeaderContainsMessageHeader
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsMessageHeader"] = value;
				}
			}

			// Token: 0x17004CA8 RID: 19624
			// (set) Token: 0x060072DA RID: 29402 RVA: 0x000ACC63 File Offset: 0x000AAE63
			public virtual Word HeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsWords"] = value;
				}
			}

			// Token: 0x17004CA9 RID: 19625
			// (set) Token: 0x060072DB RID: 29403 RVA: 0x000ACC7B File Offset: 0x000AAE7B
			public virtual Word FromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["FromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004CAA RID: 19626
			// (set) Token: 0x060072DC RID: 29404 RVA: 0x000ACC93 File Offset: 0x000AAE93
			public virtual Word SenderDomainIs
			{
				set
				{
					base.PowerSharpParameters["SenderDomainIs"] = value;
				}
			}

			// Token: 0x17004CAB RID: 19627
			// (set) Token: 0x060072DD RID: 29405 RVA: 0x000ACCAB File Offset: 0x000AAEAB
			public virtual Word RecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["RecipientDomainIs"] = value;
				}
			}

			// Token: 0x17004CAC RID: 19628
			// (set) Token: 0x060072DE RID: 29406 RVA: 0x000ACCC3 File Offset: 0x000AAEC3
			public virtual Pattern SubjectMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SubjectMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CAD RID: 19629
			// (set) Token: 0x060072DF RID: 29407 RVA: 0x000ACCDB File Offset: 0x000AAEDB
			public virtual Pattern SubjectOrBodyMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CAE RID: 19630
			// (set) Token: 0x060072E0 RID: 29408 RVA: 0x000ACCF3 File Offset: 0x000AAEF3
			public virtual HeaderName HeaderMatchesMessageHeader
			{
				set
				{
					base.PowerSharpParameters["HeaderMatchesMessageHeader"] = value;
				}
			}

			// Token: 0x17004CAF RID: 19631
			// (set) Token: 0x060072E1 RID: 29409 RVA: 0x000ACD0B File Offset: 0x000AAF0B
			public virtual Pattern HeaderMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["HeaderMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CB0 RID: 19632
			// (set) Token: 0x060072E2 RID: 29410 RVA: 0x000ACD23 File Offset: 0x000AAF23
			public virtual Pattern FromAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["FromAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CB1 RID: 19633
			// (set) Token: 0x060072E3 RID: 29411 RVA: 0x000ACD3B File Offset: 0x000AAF3B
			public virtual Pattern AttachmentNameMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AttachmentNameMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CB2 RID: 19634
			// (set) Token: 0x060072E4 RID: 29412 RVA: 0x000ACD53 File Offset: 0x000AAF53
			public virtual Word AttachmentExtensionMatchesWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentExtensionMatchesWords"] = value;
				}
			}

			// Token: 0x17004CB3 RID: 19635
			// (set) Token: 0x060072E5 RID: 29413 RVA: 0x000ACD6B File Offset: 0x000AAF6B
			public virtual Word AttachmentPropertyContainsWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentPropertyContainsWords"] = value;
				}
			}

			// Token: 0x17004CB4 RID: 19636
			// (set) Token: 0x060072E6 RID: 29414 RVA: 0x000ACD83 File Offset: 0x000AAF83
			public virtual Word ContentCharacterSetContainsWords
			{
				set
				{
					base.PowerSharpParameters["ContentCharacterSetContainsWords"] = value;
				}
			}

			// Token: 0x17004CB5 RID: 19637
			// (set) Token: 0x060072E7 RID: 29415 RVA: 0x000ACD9B File Offset: 0x000AAF9B
			public virtual SclValue SCLOver
			{
				set
				{
					base.PowerSharpParameters["SCLOver"] = value;
				}
			}

			// Token: 0x17004CB6 RID: 19638
			// (set) Token: 0x060072E8 RID: 29416 RVA: 0x000ACDB3 File Offset: 0x000AAFB3
			public virtual ByteQuantifiedSize AttachmentSizeOver
			{
				set
				{
					base.PowerSharpParameters["AttachmentSizeOver"] = value;
				}
			}

			// Token: 0x17004CB7 RID: 19639
			// (set) Token: 0x060072E9 RID: 29417 RVA: 0x000ACDCB File Offset: 0x000AAFCB
			public virtual ByteQuantifiedSize MessageSizeOver
			{
				set
				{
					base.PowerSharpParameters["MessageSizeOver"] = value;
				}
			}

			// Token: 0x17004CB8 RID: 19640
			// (set) Token: 0x060072EA RID: 29418 RVA: 0x000ACDE3 File Offset: 0x000AAFE3
			public virtual Importance WithImportance
			{
				set
				{
					base.PowerSharpParameters["WithImportance"] = value;
				}
			}

			// Token: 0x17004CB9 RID: 19641
			// (set) Token: 0x060072EB RID: 29419 RVA: 0x000ACDFB File Offset: 0x000AAFFB
			public virtual MessageType MessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["MessageTypeMatches"] = value;
				}
			}

			// Token: 0x17004CBA RID: 19642
			// (set) Token: 0x060072EC RID: 29420 RVA: 0x000ACE13 File Offset: 0x000AB013
			public virtual Word RecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004CBB RID: 19643
			// (set) Token: 0x060072ED RID: 29421 RVA: 0x000ACE2B File Offset: 0x000AB02B
			public virtual Pattern RecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CBC RID: 19644
			// (set) Token: 0x060072EE RID: 29422 RVA: 0x000ACE43 File Offset: 0x000AB043
			public virtual Word SenderInRecipientList
			{
				set
				{
					base.PowerSharpParameters["SenderInRecipientList"] = value;
				}
			}

			// Token: 0x17004CBD RID: 19645
			// (set) Token: 0x060072EF RID: 29423 RVA: 0x000ACE5B File Offset: 0x000AB05B
			public virtual Word RecipientInSenderList
			{
				set
				{
					base.PowerSharpParameters["RecipientInSenderList"] = value;
				}
			}

			// Token: 0x17004CBE RID: 19646
			// (set) Token: 0x060072F0 RID: 29424 RVA: 0x000ACE73 File Offset: 0x000AB073
			public virtual Word AttachmentContainsWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentContainsWords"] = value;
				}
			}

			// Token: 0x17004CBF RID: 19647
			// (set) Token: 0x060072F1 RID: 29425 RVA: 0x000ACE8B File Offset: 0x000AB08B
			public virtual Pattern AttachmentMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AttachmentMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CC0 RID: 19648
			// (set) Token: 0x060072F2 RID: 29426 RVA: 0x000ACEA3 File Offset: 0x000AB0A3
			public virtual bool AttachmentIsUnsupported
			{
				set
				{
					base.PowerSharpParameters["AttachmentIsUnsupported"] = value;
				}
			}

			// Token: 0x17004CC1 RID: 19649
			// (set) Token: 0x060072F3 RID: 29427 RVA: 0x000ACEBB File Offset: 0x000AB0BB
			public virtual bool AttachmentProcessingLimitExceeded
			{
				set
				{
					base.PowerSharpParameters["AttachmentProcessingLimitExceeded"] = value;
				}
			}

			// Token: 0x17004CC2 RID: 19650
			// (set) Token: 0x060072F4 RID: 29428 RVA: 0x000ACED3 File Offset: 0x000AB0D3
			public virtual bool AttachmentHasExecutableContent
			{
				set
				{
					base.PowerSharpParameters["AttachmentHasExecutableContent"] = value;
				}
			}

			// Token: 0x17004CC3 RID: 19651
			// (set) Token: 0x060072F5 RID: 29429 RVA: 0x000ACEEB File Offset: 0x000AB0EB
			public virtual bool AttachmentIsPasswordProtected
			{
				set
				{
					base.PowerSharpParameters["AttachmentIsPasswordProtected"] = value;
				}
			}

			// Token: 0x17004CC4 RID: 19652
			// (set) Token: 0x060072F6 RID: 29430 RVA: 0x000ACF03 File Offset: 0x000AB103
			public virtual Word AnyOfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["AnyOfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004CC5 RID: 19653
			// (set) Token: 0x060072F7 RID: 29431 RVA: 0x000ACF1B File Offset: 0x000AB11B
			public virtual Pattern AnyOfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AnyOfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CC6 RID: 19654
			// (set) Token: 0x060072F8 RID: 29432 RVA: 0x000ACF33 File Offset: 0x000AB133
			public virtual bool HasSenderOverride
			{
				set
				{
					base.PowerSharpParameters["HasSenderOverride"] = value;
				}
			}

			// Token: 0x17004CC7 RID: 19655
			// (set) Token: 0x060072F9 RID: 29433 RVA: 0x000ACF4B File Offset: 0x000AB14B
			public virtual bool ExceptIfHasSenderOverride
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasSenderOverride"] = value;
				}
			}

			// Token: 0x17004CC8 RID: 19656
			// (set) Token: 0x060072FA RID: 29434 RVA: 0x000ACF63 File Offset: 0x000AB163
			public virtual RecipientIdParameter ExceptIfFrom
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFrom"] = value;
				}
			}

			// Token: 0x17004CC9 RID: 19657
			// (set) Token: 0x060072FB RID: 29435 RVA: 0x000ACF76 File Offset: 0x000AB176
			public virtual Hashtable MessageContainsDataClassifications
			{
				set
				{
					base.PowerSharpParameters["MessageContainsDataClassifications"] = value;
				}
			}

			// Token: 0x17004CCA RID: 19658
			// (set) Token: 0x060072FC RID: 29436 RVA: 0x000ACF89 File Offset: 0x000AB189
			public virtual MultiValuedProperty<IPRange> SenderIpRanges
			{
				set
				{
					base.PowerSharpParameters["SenderIpRanges"] = value;
				}
			}

			// Token: 0x17004CCB RID: 19659
			// (set) Token: 0x060072FD RID: 29437 RVA: 0x000ACF9C File Offset: 0x000AB19C
			public virtual RecipientIdParameter ExceptIfFromMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromMemberOf"] = value;
				}
			}

			// Token: 0x17004CCC RID: 19660
			// (set) Token: 0x060072FE RID: 29438 RVA: 0x000ACFAF File Offset: 0x000AB1AF
			public virtual FromUserScope ExceptIfFromScope
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromScope"] = value;
				}
			}

			// Token: 0x17004CCD RID: 19661
			// (set) Token: 0x060072FF RID: 29439 RVA: 0x000ACFC7 File Offset: 0x000AB1C7
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17004CCE RID: 19662
			// (set) Token: 0x06007300 RID: 29440 RVA: 0x000ACFDA File Offset: 0x000AB1DA
			public virtual RecipientIdParameter ExceptIfSentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToMemberOf"] = value;
				}
			}

			// Token: 0x17004CCF RID: 19663
			// (set) Token: 0x06007301 RID: 29441 RVA: 0x000ACFED File Offset: 0x000AB1ED
			public virtual ToUserScope ExceptIfSentToScope
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToScope"] = value;
				}
			}

			// Token: 0x17004CD0 RID: 19664
			// (set) Token: 0x06007302 RID: 29442 RVA: 0x000AD005 File Offset: 0x000AB205
			public virtual RecipientIdParameter ExceptIfBetweenMemberOf1
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBetweenMemberOf1"] = value;
				}
			}

			// Token: 0x17004CD1 RID: 19665
			// (set) Token: 0x06007303 RID: 29443 RVA: 0x000AD018 File Offset: 0x000AB218
			public virtual RecipientIdParameter ExceptIfBetweenMemberOf2
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBetweenMemberOf2"] = value;
				}
			}

			// Token: 0x17004CD2 RID: 19666
			// (set) Token: 0x06007304 RID: 29444 RVA: 0x000AD02B File Offset: 0x000AB22B
			public virtual RecipientIdParameter ExceptIfManagerAddresses
			{
				set
				{
					base.PowerSharpParameters["ExceptIfManagerAddresses"] = value;
				}
			}

			// Token: 0x17004CD3 RID: 19667
			// (set) Token: 0x06007305 RID: 29445 RVA: 0x000AD03E File Offset: 0x000AB23E
			public virtual EvaluatedUser ExceptIfManagerForEvaluatedUser
			{
				set
				{
					base.PowerSharpParameters["ExceptIfManagerForEvaluatedUser"] = value;
				}
			}

			// Token: 0x17004CD4 RID: 19668
			// (set) Token: 0x06007306 RID: 29446 RVA: 0x000AD056 File Offset: 0x000AB256
			public virtual ManagementRelationship ExceptIfSenderManagementRelationship
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderManagementRelationship"] = value;
				}
			}

			// Token: 0x17004CD5 RID: 19669
			// (set) Token: 0x06007307 RID: 29447 RVA: 0x000AD06E File Offset: 0x000AB26E
			public virtual ADAttribute ExceptIfADComparisonAttribute
			{
				set
				{
					base.PowerSharpParameters["ExceptIfADComparisonAttribute"] = value;
				}
			}

			// Token: 0x17004CD6 RID: 19670
			// (set) Token: 0x06007308 RID: 29448 RVA: 0x000AD086 File Offset: 0x000AB286
			public virtual Evaluation ExceptIfADComparisonOperator
			{
				set
				{
					base.PowerSharpParameters["ExceptIfADComparisonOperator"] = value;
				}
			}

			// Token: 0x17004CD7 RID: 19671
			// (set) Token: 0x06007309 RID: 29449 RVA: 0x000AD09E File Offset: 0x000AB29E
			public virtual Word ExceptIfSenderADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004CD8 RID: 19672
			// (set) Token: 0x0600730A RID: 29450 RVA: 0x000AD0B6 File Offset: 0x000AB2B6
			public virtual Pattern ExceptIfSenderADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CD9 RID: 19673
			// (set) Token: 0x0600730B RID: 29451 RVA: 0x000AD0CE File Offset: 0x000AB2CE
			public virtual Word ExceptIfRecipientADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004CDA RID: 19674
			// (set) Token: 0x0600730C RID: 29452 RVA: 0x000AD0E6 File Offset: 0x000AB2E6
			public virtual Pattern ExceptIfRecipientADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CDB RID: 19675
			// (set) Token: 0x0600730D RID: 29453 RVA: 0x000AD0FE File Offset: 0x000AB2FE
			public virtual RecipientIdParameter ExceptIfAnyOfToHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToHeader"] = value;
				}
			}

			// Token: 0x17004CDC RID: 19676
			// (set) Token: 0x0600730E RID: 29454 RVA: 0x000AD111 File Offset: 0x000AB311
			public virtual RecipientIdParameter ExceptIfAnyOfToHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004CDD RID: 19677
			// (set) Token: 0x0600730F RID: 29455 RVA: 0x000AD124 File Offset: 0x000AB324
			public virtual RecipientIdParameter ExceptIfAnyOfCcHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfCcHeader"] = value;
				}
			}

			// Token: 0x17004CDE RID: 19678
			// (set) Token: 0x06007310 RID: 29456 RVA: 0x000AD137 File Offset: 0x000AB337
			public virtual RecipientIdParameter ExceptIfAnyOfCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004CDF RID: 19679
			// (set) Token: 0x06007311 RID: 29457 RVA: 0x000AD14A File Offset: 0x000AB34A
			public virtual RecipientIdParameter ExceptIfAnyOfToCcHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToCcHeader"] = value;
				}
			}

			// Token: 0x17004CE0 RID: 19680
			// (set) Token: 0x06007312 RID: 29458 RVA: 0x000AD15D File Offset: 0x000AB35D
			public virtual RecipientIdParameter ExceptIfAnyOfToCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004CE1 RID: 19681
			// (set) Token: 0x06007313 RID: 29459 RVA: 0x000AD170 File Offset: 0x000AB370
			public virtual string ExceptIfHasClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasClassification"] = value;
				}
			}

			// Token: 0x17004CE2 RID: 19682
			// (set) Token: 0x06007314 RID: 29460 RVA: 0x000AD183 File Offset: 0x000AB383
			public virtual bool ExceptIfHasNoClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasNoClassification"] = value;
				}
			}

			// Token: 0x17004CE3 RID: 19683
			// (set) Token: 0x06007315 RID: 29461 RVA: 0x000AD19B File Offset: 0x000AB39B
			public virtual Word ExceptIfSubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectContainsWords"] = value;
				}
			}

			// Token: 0x17004CE4 RID: 19684
			// (set) Token: 0x06007316 RID: 29462 RVA: 0x000AD1B3 File Offset: 0x000AB3B3
			public virtual Word ExceptIfSubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17004CE5 RID: 19685
			// (set) Token: 0x06007317 RID: 29463 RVA: 0x000AD1CB File Offset: 0x000AB3CB
			public virtual HeaderName ExceptIfHeaderContainsMessageHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsMessageHeader"] = value;
				}
			}

			// Token: 0x17004CE6 RID: 19686
			// (set) Token: 0x06007318 RID: 29464 RVA: 0x000AD1E3 File Offset: 0x000AB3E3
			public virtual Word ExceptIfHeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsWords"] = value;
				}
			}

			// Token: 0x17004CE7 RID: 19687
			// (set) Token: 0x06007319 RID: 29465 RVA: 0x000AD1FB File Offset: 0x000AB3FB
			public virtual Word ExceptIfFromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004CE8 RID: 19688
			// (set) Token: 0x0600731A RID: 29466 RVA: 0x000AD213 File Offset: 0x000AB413
			public virtual Word ExceptIfSenderDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderDomainIs"] = value;
				}
			}

			// Token: 0x17004CE9 RID: 19689
			// (set) Token: 0x0600731B RID: 29467 RVA: 0x000AD22B File Offset: 0x000AB42B
			public virtual Word ExceptIfRecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientDomainIs"] = value;
				}
			}

			// Token: 0x17004CEA RID: 19690
			// (set) Token: 0x0600731C RID: 29468 RVA: 0x000AD243 File Offset: 0x000AB443
			public virtual Pattern ExceptIfSubjectMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CEB RID: 19691
			// (set) Token: 0x0600731D RID: 29469 RVA: 0x000AD25B File Offset: 0x000AB45B
			public virtual Pattern ExceptIfSubjectOrBodyMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CEC RID: 19692
			// (set) Token: 0x0600731E RID: 29470 RVA: 0x000AD273 File Offset: 0x000AB473
			public virtual HeaderName ExceptIfHeaderMatchesMessageHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderMatchesMessageHeader"] = value;
				}
			}

			// Token: 0x17004CED RID: 19693
			// (set) Token: 0x0600731F RID: 29471 RVA: 0x000AD28B File Offset: 0x000AB48B
			public virtual Pattern ExceptIfHeaderMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CEE RID: 19694
			// (set) Token: 0x06007320 RID: 29472 RVA: 0x000AD2A3 File Offset: 0x000AB4A3
			public virtual Pattern ExceptIfFromAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CEF RID: 19695
			// (set) Token: 0x06007321 RID: 29473 RVA: 0x000AD2BB File Offset: 0x000AB4BB
			public virtual Pattern ExceptIfAttachmentNameMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentNameMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CF0 RID: 19696
			// (set) Token: 0x06007322 RID: 29474 RVA: 0x000AD2D3 File Offset: 0x000AB4D3
			public virtual Word ExceptIfAttachmentExtensionMatchesWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentExtensionMatchesWords"] = value;
				}
			}

			// Token: 0x17004CF1 RID: 19697
			// (set) Token: 0x06007323 RID: 29475 RVA: 0x000AD2EB File Offset: 0x000AB4EB
			public virtual Word ExceptIfAttachmentPropertyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentPropertyContainsWords"] = value;
				}
			}

			// Token: 0x17004CF2 RID: 19698
			// (set) Token: 0x06007324 RID: 29476 RVA: 0x000AD303 File Offset: 0x000AB503
			public virtual Word ExceptIfContentCharacterSetContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfContentCharacterSetContainsWords"] = value;
				}
			}

			// Token: 0x17004CF3 RID: 19699
			// (set) Token: 0x06007325 RID: 29477 RVA: 0x000AD31B File Offset: 0x000AB51B
			public virtual SclValue ExceptIfSCLOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSCLOver"] = value;
				}
			}

			// Token: 0x17004CF4 RID: 19700
			// (set) Token: 0x06007326 RID: 29478 RVA: 0x000AD333 File Offset: 0x000AB533
			public virtual ByteQuantifiedSize ExceptIfAttachmentSizeOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentSizeOver"] = value;
				}
			}

			// Token: 0x17004CF5 RID: 19701
			// (set) Token: 0x06007327 RID: 29479 RVA: 0x000AD34B File Offset: 0x000AB54B
			public virtual ByteQuantifiedSize ExceptIfMessageSizeOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageSizeOver"] = value;
				}
			}

			// Token: 0x17004CF6 RID: 19702
			// (set) Token: 0x06007328 RID: 29480 RVA: 0x000AD363 File Offset: 0x000AB563
			public virtual Importance ExceptIfWithImportance
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithImportance"] = value;
				}
			}

			// Token: 0x17004CF7 RID: 19703
			// (set) Token: 0x06007329 RID: 29481 RVA: 0x000AD37B File Offset: 0x000AB57B
			public virtual MessageType ExceptIfMessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageTypeMatches"] = value;
				}
			}

			// Token: 0x17004CF8 RID: 19704
			// (set) Token: 0x0600732A RID: 29482 RVA: 0x000AD393 File Offset: 0x000AB593
			public virtual Word ExceptIfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004CF9 RID: 19705
			// (set) Token: 0x0600732B RID: 29483 RVA: 0x000AD3AB File Offset: 0x000AB5AB
			public virtual Pattern ExceptIfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CFA RID: 19706
			// (set) Token: 0x0600732C RID: 29484 RVA: 0x000AD3C3 File Offset: 0x000AB5C3
			public virtual Word ExceptIfSenderInRecipientList
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderInRecipientList"] = value;
				}
			}

			// Token: 0x17004CFB RID: 19707
			// (set) Token: 0x0600732D RID: 29485 RVA: 0x000AD3DB File Offset: 0x000AB5DB
			public virtual Word ExceptIfRecipientInSenderList
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientInSenderList"] = value;
				}
			}

			// Token: 0x17004CFC RID: 19708
			// (set) Token: 0x0600732E RID: 29486 RVA: 0x000AD3F3 File Offset: 0x000AB5F3
			public virtual Word ExceptIfAttachmentContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentContainsWords"] = value;
				}
			}

			// Token: 0x17004CFD RID: 19709
			// (set) Token: 0x0600732F RID: 29487 RVA: 0x000AD40B File Offset: 0x000AB60B
			public virtual Pattern ExceptIfAttachmentMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004CFE RID: 19710
			// (set) Token: 0x06007330 RID: 29488 RVA: 0x000AD423 File Offset: 0x000AB623
			public virtual bool ExceptIfAttachmentIsUnsupported
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentIsUnsupported"] = value;
				}
			}

			// Token: 0x17004CFF RID: 19711
			// (set) Token: 0x06007331 RID: 29489 RVA: 0x000AD43B File Offset: 0x000AB63B
			public virtual bool ExceptIfAttachmentProcessingLimitExceeded
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentProcessingLimitExceeded"] = value;
				}
			}

			// Token: 0x17004D00 RID: 19712
			// (set) Token: 0x06007332 RID: 29490 RVA: 0x000AD453 File Offset: 0x000AB653
			public virtual bool ExceptIfAttachmentHasExecutableContent
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentHasExecutableContent"] = value;
				}
			}

			// Token: 0x17004D01 RID: 19713
			// (set) Token: 0x06007333 RID: 29491 RVA: 0x000AD46B File Offset: 0x000AB66B
			public virtual bool ExceptIfAttachmentIsPasswordProtected
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentIsPasswordProtected"] = value;
				}
			}

			// Token: 0x17004D02 RID: 19714
			// (set) Token: 0x06007334 RID: 29492 RVA: 0x000AD483 File Offset: 0x000AB683
			public virtual Word ExceptIfAnyOfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004D03 RID: 19715
			// (set) Token: 0x06007335 RID: 29493 RVA: 0x000AD49B File Offset: 0x000AB69B
			public virtual Pattern ExceptIfAnyOfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D04 RID: 19716
			// (set) Token: 0x06007336 RID: 29494 RVA: 0x000AD4B3 File Offset: 0x000AB6B3
			public virtual Hashtable ExceptIfMessageContainsDataClassifications
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageContainsDataClassifications"] = value;
				}
			}

			// Token: 0x17004D05 RID: 19717
			// (set) Token: 0x06007337 RID: 29495 RVA: 0x000AD4C6 File Offset: 0x000AB6C6
			public virtual MultiValuedProperty<IPRange> ExceptIfSenderIpRanges
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderIpRanges"] = value;
				}
			}

			// Token: 0x17004D06 RID: 19718
			// (set) Token: 0x06007338 RID: 29496 RVA: 0x000AD4D9 File Offset: 0x000AB6D9
			public virtual SubjectPrefix? PrependSubject
			{
				set
				{
					base.PowerSharpParameters["PrependSubject"] = value;
				}
			}

			// Token: 0x17004D07 RID: 19719
			// (set) Token: 0x06007339 RID: 29497 RVA: 0x000AD4F1 File Offset: 0x000AB6F1
			public virtual string SetAuditSeverity
			{
				set
				{
					base.PowerSharpParameters["SetAuditSeverity"] = value;
				}
			}

			// Token: 0x17004D08 RID: 19720
			// (set) Token: 0x0600733A RID: 29498 RVA: 0x000AD504 File Offset: 0x000AB704
			public virtual string ApplyClassification
			{
				set
				{
					base.PowerSharpParameters["ApplyClassification"] = value;
				}
			}

			// Token: 0x17004D09 RID: 19721
			// (set) Token: 0x0600733B RID: 29499 RVA: 0x000AD517 File Offset: 0x000AB717
			public virtual DisclaimerLocation ApplyHtmlDisclaimerLocation
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerLocation"] = value;
				}
			}

			// Token: 0x17004D0A RID: 19722
			// (set) Token: 0x0600733C RID: 29500 RVA: 0x000AD52F File Offset: 0x000AB72F
			public virtual DisclaimerText ApplyHtmlDisclaimerText
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerText"] = value;
				}
			}

			// Token: 0x17004D0B RID: 19723
			// (set) Token: 0x0600733D RID: 29501 RVA: 0x000AD547 File Offset: 0x000AB747
			public virtual DisclaimerFallbackAction ApplyHtmlDisclaimerFallbackAction
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerFallbackAction"] = value;
				}
			}

			// Token: 0x17004D0C RID: 19724
			// (set) Token: 0x0600733E RID: 29502 RVA: 0x000AD55F File Offset: 0x000AB75F
			public virtual string ApplyRightsProtectionTemplate
			{
				set
				{
					base.PowerSharpParameters["ApplyRightsProtectionTemplate"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17004D0D RID: 19725
			// (set) Token: 0x0600733F RID: 29503 RVA: 0x000AD57D File Offset: 0x000AB77D
			public virtual SclValue SetSCL
			{
				set
				{
					base.PowerSharpParameters["SetSCL"] = value;
				}
			}

			// Token: 0x17004D0E RID: 19726
			// (set) Token: 0x06007340 RID: 29504 RVA: 0x000AD595 File Offset: 0x000AB795
			public virtual HeaderName SetHeaderName
			{
				set
				{
					base.PowerSharpParameters["SetHeaderName"] = value;
				}
			}

			// Token: 0x17004D0F RID: 19727
			// (set) Token: 0x06007341 RID: 29505 RVA: 0x000AD5AD File Offset: 0x000AB7AD
			public virtual HeaderValue SetHeaderValue
			{
				set
				{
					base.PowerSharpParameters["SetHeaderValue"] = value;
				}
			}

			// Token: 0x17004D10 RID: 19728
			// (set) Token: 0x06007342 RID: 29506 RVA: 0x000AD5C5 File Offset: 0x000AB7C5
			public virtual HeaderName RemoveHeader
			{
				set
				{
					base.PowerSharpParameters["RemoveHeader"] = value;
				}
			}

			// Token: 0x17004D11 RID: 19729
			// (set) Token: 0x06007343 RID: 29507 RVA: 0x000AD5DD File Offset: 0x000AB7DD
			public virtual RecipientIdParameter AddToRecipients
			{
				set
				{
					base.PowerSharpParameters["AddToRecipients"] = value;
				}
			}

			// Token: 0x17004D12 RID: 19730
			// (set) Token: 0x06007344 RID: 29508 RVA: 0x000AD5F0 File Offset: 0x000AB7F0
			public virtual RecipientIdParameter CopyTo
			{
				set
				{
					base.PowerSharpParameters["CopyTo"] = value;
				}
			}

			// Token: 0x17004D13 RID: 19731
			// (set) Token: 0x06007345 RID: 29509 RVA: 0x000AD603 File Offset: 0x000AB803
			public virtual RecipientIdParameter BlindCopyTo
			{
				set
				{
					base.PowerSharpParameters["BlindCopyTo"] = value;
				}
			}

			// Token: 0x17004D14 RID: 19732
			// (set) Token: 0x06007346 RID: 29510 RVA: 0x000AD616 File Offset: 0x000AB816
			public virtual AddedRecipientType AddManagerAsRecipientType
			{
				set
				{
					base.PowerSharpParameters["AddManagerAsRecipientType"] = value;
				}
			}

			// Token: 0x17004D15 RID: 19733
			// (set) Token: 0x06007347 RID: 29511 RVA: 0x000AD62E File Offset: 0x000AB82E
			public virtual RecipientIdParameter ModerateMessageByUser
			{
				set
				{
					base.PowerSharpParameters["ModerateMessageByUser"] = value;
				}
			}

			// Token: 0x17004D16 RID: 19734
			// (set) Token: 0x06007348 RID: 29512 RVA: 0x000AD641 File Offset: 0x000AB841
			public virtual bool ModerateMessageByManager
			{
				set
				{
					base.PowerSharpParameters["ModerateMessageByManager"] = value;
				}
			}

			// Token: 0x17004D17 RID: 19735
			// (set) Token: 0x06007349 RID: 29513 RVA: 0x000AD659 File Offset: 0x000AB859
			public virtual RecipientIdParameter RedirectMessageTo
			{
				set
				{
					base.PowerSharpParameters["RedirectMessageTo"] = value;
				}
			}

			// Token: 0x17004D18 RID: 19736
			// (set) Token: 0x0600734A RID: 29514 RVA: 0x000AD66C File Offset: 0x000AB86C
			public virtual NotifySenderType NotifySender
			{
				set
				{
					base.PowerSharpParameters["NotifySender"] = value;
				}
			}

			// Token: 0x17004D19 RID: 19737
			// (set) Token: 0x0600734B RID: 29515 RVA: 0x000AD684 File Offset: 0x000AB884
			public virtual RejectEnhancedStatus? RejectMessageEnhancedStatusCode
			{
				set
				{
					base.PowerSharpParameters["RejectMessageEnhancedStatusCode"] = value;
				}
			}

			// Token: 0x17004D1A RID: 19738
			// (set) Token: 0x0600734C RID: 29516 RVA: 0x000AD69C File Offset: 0x000AB89C
			public virtual DsnText? RejectMessageReasonText
			{
				set
				{
					base.PowerSharpParameters["RejectMessageReasonText"] = value;
				}
			}

			// Token: 0x17004D1B RID: 19739
			// (set) Token: 0x0600734D RID: 29517 RVA: 0x000AD6B4 File Offset: 0x000AB8B4
			public virtual bool DeleteMessage
			{
				set
				{
					base.PowerSharpParameters["DeleteMessage"] = value;
				}
			}

			// Token: 0x17004D1C RID: 19740
			// (set) Token: 0x0600734E RID: 29518 RVA: 0x000AD6CC File Offset: 0x000AB8CC
			public virtual bool Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17004D1D RID: 19741
			// (set) Token: 0x0600734F RID: 29519 RVA: 0x000AD6E4 File Offset: 0x000AB8E4
			public virtual bool Quarantine
			{
				set
				{
					base.PowerSharpParameters["Quarantine"] = value;
				}
			}

			// Token: 0x17004D1E RID: 19742
			// (set) Token: 0x06007350 RID: 29520 RVA: 0x000AD6FC File Offset: 0x000AB8FC
			public virtual RejectText? SmtpRejectMessageRejectText
			{
				set
				{
					base.PowerSharpParameters["SmtpRejectMessageRejectText"] = value;
				}
			}

			// Token: 0x17004D1F RID: 19743
			// (set) Token: 0x06007351 RID: 29521 RVA: 0x000AD714 File Offset: 0x000AB914
			public virtual RejectStatusCode? SmtpRejectMessageRejectStatusCode
			{
				set
				{
					base.PowerSharpParameters["SmtpRejectMessageRejectStatusCode"] = value;
				}
			}

			// Token: 0x17004D20 RID: 19744
			// (set) Token: 0x06007352 RID: 29522 RVA: 0x000AD72C File Offset: 0x000AB92C
			public virtual EventLogText? LogEventText
			{
				set
				{
					base.PowerSharpParameters["LogEventText"] = value;
				}
			}

			// Token: 0x17004D21 RID: 19745
			// (set) Token: 0x06007353 RID: 29523 RVA: 0x000AD744 File Offset: 0x000AB944
			public virtual bool StopRuleProcessing
			{
				set
				{
					base.PowerSharpParameters["StopRuleProcessing"] = value;
				}
			}

			// Token: 0x17004D22 RID: 19746
			// (set) Token: 0x06007354 RID: 29524 RVA: 0x000AD75C File Offset: 0x000AB95C
			public virtual DateTime? ActivationDate
			{
				set
				{
					base.PowerSharpParameters["ActivationDate"] = value;
				}
			}

			// Token: 0x17004D23 RID: 19747
			// (set) Token: 0x06007355 RID: 29525 RVA: 0x000AD774 File Offset: 0x000AB974
			public virtual DateTime? ExpiryDate
			{
				set
				{
					base.PowerSharpParameters["ExpiryDate"] = value;
				}
			}

			// Token: 0x17004D24 RID: 19748
			// (set) Token: 0x06007356 RID: 29526 RVA: 0x000AD78C File Offset: 0x000AB98C
			public virtual OutboundConnectorIdParameter RouteMessageOutboundConnector
			{
				set
				{
					base.PowerSharpParameters["RouteMessageOutboundConnector"] = value;
				}
			}

			// Token: 0x17004D25 RID: 19749
			// (set) Token: 0x06007357 RID: 29527 RVA: 0x000AD79F File Offset: 0x000AB99F
			public virtual bool RouteMessageOutboundRequireTls
			{
				set
				{
					base.PowerSharpParameters["RouteMessageOutboundRequireTls"] = value;
				}
			}

			// Token: 0x17004D26 RID: 19750
			// (set) Token: 0x06007358 RID: 29528 RVA: 0x000AD7B7 File Offset: 0x000AB9B7
			public virtual bool ApplyOME
			{
				set
				{
					base.PowerSharpParameters["ApplyOME"] = value;
				}
			}

			// Token: 0x17004D27 RID: 19751
			// (set) Token: 0x06007359 RID: 29529 RVA: 0x000AD7CF File Offset: 0x000AB9CF
			public virtual bool RemoveOME
			{
				set
				{
					base.PowerSharpParameters["RemoveOME"] = value;
				}
			}

			// Token: 0x17004D28 RID: 19752
			// (set) Token: 0x0600735A RID: 29530 RVA: 0x000AD7E7 File Offset: 0x000AB9E7
			public virtual RuleSubType RuleSubType
			{
				set
				{
					base.PowerSharpParameters["RuleSubType"] = value;
				}
			}

			// Token: 0x17004D29 RID: 19753
			// (set) Token: 0x0600735B RID: 29531 RVA: 0x000AD7FF File Offset: 0x000AB9FF
			public virtual RuleErrorAction RuleErrorAction
			{
				set
				{
					base.PowerSharpParameters["RuleErrorAction"] = value;
				}
			}

			// Token: 0x17004D2A RID: 19754
			// (set) Token: 0x0600735C RID: 29532 RVA: 0x000AD817 File Offset: 0x000ABA17
			public virtual SenderAddressLocation SenderAddressLocation
			{
				set
				{
					base.PowerSharpParameters["SenderAddressLocation"] = value;
				}
			}

			// Token: 0x17004D2B RID: 19755
			// (set) Token: 0x0600735D RID: 29533 RVA: 0x000AD82F File Offset: 0x000ABA2F
			public virtual string GenerateIncidentReport
			{
				set
				{
					base.PowerSharpParameters["GenerateIncidentReport"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17004D2C RID: 19756
			// (set) Token: 0x0600735E RID: 29534 RVA: 0x000AD84D File Offset: 0x000ABA4D
			public virtual IncidentReportOriginalMail? IncidentReportOriginalMail
			{
				set
				{
					base.PowerSharpParameters["IncidentReportOriginalMail"] = value;
				}
			}

			// Token: 0x17004D2D RID: 19757
			// (set) Token: 0x0600735F RID: 29535 RVA: 0x000AD865 File Offset: 0x000ABA65
			public virtual IncidentReportContent IncidentReportContent
			{
				set
				{
					base.PowerSharpParameters["IncidentReportContent"] = value;
				}
			}

			// Token: 0x17004D2E RID: 19758
			// (set) Token: 0x06007360 RID: 29536 RVA: 0x000AD87D File Offset: 0x000ABA7D
			public virtual DisclaimerText? GenerateNotification
			{
				set
				{
					base.PowerSharpParameters["GenerateNotification"] = value;
				}
			}

			// Token: 0x17004D2F RID: 19759
			// (set) Token: 0x06007361 RID: 29537 RVA: 0x000AD895 File Offset: 0x000ABA95
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004D30 RID: 19760
			// (set) Token: 0x06007362 RID: 29538 RVA: 0x000AD8B3 File Offset: 0x000ABAB3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004D31 RID: 19761
			// (set) Token: 0x06007363 RID: 29539 RVA: 0x000AD8C6 File Offset: 0x000ABAC6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004D32 RID: 19762
			// (set) Token: 0x06007364 RID: 29540 RVA: 0x000AD8DE File Offset: 0x000ABADE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004D33 RID: 19763
			// (set) Token: 0x06007365 RID: 29541 RVA: 0x000AD8F6 File Offset: 0x000ABAF6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004D34 RID: 19764
			// (set) Token: 0x06007366 RID: 29542 RVA: 0x000AD90E File Offset: 0x000ABB0E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004D35 RID: 19765
			// (set) Token: 0x06007367 RID: 29543 RVA: 0x000AD926 File Offset: 0x000ABB26
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
