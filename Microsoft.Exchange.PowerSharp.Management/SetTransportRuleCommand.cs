using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008F9 RID: 2297
	public class SetTransportRuleCommand : SyntheticCommandWithPipelineInputNoOutput<Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule>
	{
		// Token: 0x0600737E RID: 29566 RVA: 0x000ADAEA File Offset: 0x000ABCEA
		private SetTransportRuleCommand() : base("Set-TransportRule")
		{
		}

		// Token: 0x0600737F RID: 29567 RVA: 0x000ADAF7 File Offset: 0x000ABCF7
		public SetTransportRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007380 RID: 29568 RVA: 0x000ADB06 File Offset: 0x000ABD06
		public virtual SetTransportRuleCommand SetParameters(SetTransportRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007381 RID: 29569 RVA: 0x000ADB10 File Offset: 0x000ABD10
		public virtual SetTransportRuleCommand SetParameters(SetTransportRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008FA RID: 2298
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004D45 RID: 19781
			// (set) Token: 0x06007382 RID: 29570 RVA: 0x000ADB1A File Offset: 0x000ABD1A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004D46 RID: 19782
			// (set) Token: 0x06007383 RID: 29571 RVA: 0x000ADB2D File Offset: 0x000ABD2D
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004D47 RID: 19783
			// (set) Token: 0x06007384 RID: 29572 RVA: 0x000ADB45 File Offset: 0x000ABD45
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17004D48 RID: 19784
			// (set) Token: 0x06007385 RID: 29573 RVA: 0x000ADB5D File Offset: 0x000ABD5D
			public virtual string Comments
			{
				set
				{
					base.PowerSharpParameters["Comments"] = value;
				}
			}

			// Token: 0x17004D49 RID: 19785
			// (set) Token: 0x06007386 RID: 29574 RVA: 0x000ADB70 File Offset: 0x000ABD70
			public virtual string DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17004D4A RID: 19786
			// (set) Token: 0x06007387 RID: 29575 RVA: 0x000ADB83 File Offset: 0x000ABD83
			public virtual RecipientIdParameter From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x17004D4B RID: 19787
			// (set) Token: 0x06007388 RID: 29576 RVA: 0x000ADB96 File Offset: 0x000ABD96
			public virtual RecipientIdParameter FromMemberOf
			{
				set
				{
					base.PowerSharpParameters["FromMemberOf"] = value;
				}
			}

			// Token: 0x17004D4C RID: 19788
			// (set) Token: 0x06007389 RID: 29577 RVA: 0x000ADBA9 File Offset: 0x000ABDA9
			public virtual FromUserScope? FromScope
			{
				set
				{
					base.PowerSharpParameters["FromScope"] = value;
				}
			}

			// Token: 0x17004D4D RID: 19789
			// (set) Token: 0x0600738A RID: 29578 RVA: 0x000ADBC1 File Offset: 0x000ABDC1
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17004D4E RID: 19790
			// (set) Token: 0x0600738B RID: 29579 RVA: 0x000ADBD4 File Offset: 0x000ABDD4
			public virtual RecipientIdParameter SentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["SentToMemberOf"] = value;
				}
			}

			// Token: 0x17004D4F RID: 19791
			// (set) Token: 0x0600738C RID: 29580 RVA: 0x000ADBE7 File Offset: 0x000ABDE7
			public virtual ToUserScope? SentToScope
			{
				set
				{
					base.PowerSharpParameters["SentToScope"] = value;
				}
			}

			// Token: 0x17004D50 RID: 19792
			// (set) Token: 0x0600738D RID: 29581 RVA: 0x000ADBFF File Offset: 0x000ABDFF
			public virtual RecipientIdParameter BetweenMemberOf1
			{
				set
				{
					base.PowerSharpParameters["BetweenMemberOf1"] = value;
				}
			}

			// Token: 0x17004D51 RID: 19793
			// (set) Token: 0x0600738E RID: 29582 RVA: 0x000ADC12 File Offset: 0x000ABE12
			public virtual RecipientIdParameter BetweenMemberOf2
			{
				set
				{
					base.PowerSharpParameters["BetweenMemberOf2"] = value;
				}
			}

			// Token: 0x17004D52 RID: 19794
			// (set) Token: 0x0600738F RID: 29583 RVA: 0x000ADC25 File Offset: 0x000ABE25
			public virtual RecipientIdParameter ManagerAddresses
			{
				set
				{
					base.PowerSharpParameters["ManagerAddresses"] = value;
				}
			}

			// Token: 0x17004D53 RID: 19795
			// (set) Token: 0x06007390 RID: 29584 RVA: 0x000ADC38 File Offset: 0x000ABE38
			public virtual EvaluatedUser? ManagerForEvaluatedUser
			{
				set
				{
					base.PowerSharpParameters["ManagerForEvaluatedUser"] = value;
				}
			}

			// Token: 0x17004D54 RID: 19796
			// (set) Token: 0x06007391 RID: 29585 RVA: 0x000ADC50 File Offset: 0x000ABE50
			public virtual ManagementRelationship? SenderManagementRelationship
			{
				set
				{
					base.PowerSharpParameters["SenderManagementRelationship"] = value;
				}
			}

			// Token: 0x17004D55 RID: 19797
			// (set) Token: 0x06007392 RID: 29586 RVA: 0x000ADC68 File Offset: 0x000ABE68
			public virtual ADAttribute? ADComparisonAttribute
			{
				set
				{
					base.PowerSharpParameters["ADComparisonAttribute"] = value;
				}
			}

			// Token: 0x17004D56 RID: 19798
			// (set) Token: 0x06007393 RID: 29587 RVA: 0x000ADC80 File Offset: 0x000ABE80
			public virtual Evaluation? ADComparisonOperator
			{
				set
				{
					base.PowerSharpParameters["ADComparisonOperator"] = value;
				}
			}

			// Token: 0x17004D57 RID: 19799
			// (set) Token: 0x06007394 RID: 29588 RVA: 0x000ADC98 File Offset: 0x000ABE98
			public virtual Word SenderADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["SenderADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004D58 RID: 19800
			// (set) Token: 0x06007395 RID: 29589 RVA: 0x000ADCB0 File Offset: 0x000ABEB0
			public virtual Pattern SenderADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SenderADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D59 RID: 19801
			// (set) Token: 0x06007396 RID: 29590 RVA: 0x000ADCC8 File Offset: 0x000ABEC8
			public virtual Word RecipientADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004D5A RID: 19802
			// (set) Token: 0x06007397 RID: 29591 RVA: 0x000ADCE0 File Offset: 0x000ABEE0
			public virtual Pattern RecipientADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["RecipientADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D5B RID: 19803
			// (set) Token: 0x06007398 RID: 29592 RVA: 0x000ADCF8 File Offset: 0x000ABEF8
			public virtual RecipientIdParameter AnyOfToHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfToHeader"] = value;
				}
			}

			// Token: 0x17004D5C RID: 19804
			// (set) Token: 0x06007399 RID: 29593 RVA: 0x000ADD0B File Offset: 0x000ABF0B
			public virtual RecipientIdParameter AnyOfToHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfToHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004D5D RID: 19805
			// (set) Token: 0x0600739A RID: 29594 RVA: 0x000ADD1E File Offset: 0x000ABF1E
			public virtual RecipientIdParameter AnyOfCcHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfCcHeader"] = value;
				}
			}

			// Token: 0x17004D5E RID: 19806
			// (set) Token: 0x0600739B RID: 29595 RVA: 0x000ADD31 File Offset: 0x000ABF31
			public virtual RecipientIdParameter AnyOfCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004D5F RID: 19807
			// (set) Token: 0x0600739C RID: 29596 RVA: 0x000ADD44 File Offset: 0x000ABF44
			public virtual RecipientIdParameter AnyOfToCcHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfToCcHeader"] = value;
				}
			}

			// Token: 0x17004D60 RID: 19808
			// (set) Token: 0x0600739D RID: 29597 RVA: 0x000ADD57 File Offset: 0x000ABF57
			public virtual RecipientIdParameter AnyOfToCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfToCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004D61 RID: 19809
			// (set) Token: 0x0600739E RID: 29598 RVA: 0x000ADD6A File Offset: 0x000ABF6A
			public virtual string HasClassification
			{
				set
				{
					base.PowerSharpParameters["HasClassification"] = value;
				}
			}

			// Token: 0x17004D62 RID: 19810
			// (set) Token: 0x0600739F RID: 29599 RVA: 0x000ADD7D File Offset: 0x000ABF7D
			public virtual bool HasNoClassification
			{
				set
				{
					base.PowerSharpParameters["HasNoClassification"] = value;
				}
			}

			// Token: 0x17004D63 RID: 19811
			// (set) Token: 0x060073A0 RID: 29600 RVA: 0x000ADD95 File Offset: 0x000ABF95
			public virtual Word SubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectContainsWords"] = value;
				}
			}

			// Token: 0x17004D64 RID: 19812
			// (set) Token: 0x060073A1 RID: 29601 RVA: 0x000ADDAD File Offset: 0x000ABFAD
			public virtual Word SubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17004D65 RID: 19813
			// (set) Token: 0x060073A2 RID: 29602 RVA: 0x000ADDC5 File Offset: 0x000ABFC5
			public virtual HeaderName? HeaderContainsMessageHeader
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsMessageHeader"] = value;
				}
			}

			// Token: 0x17004D66 RID: 19814
			// (set) Token: 0x060073A3 RID: 29603 RVA: 0x000ADDDD File Offset: 0x000ABFDD
			public virtual Word HeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsWords"] = value;
				}
			}

			// Token: 0x17004D67 RID: 19815
			// (set) Token: 0x060073A4 RID: 29604 RVA: 0x000ADDF5 File Offset: 0x000ABFF5
			public virtual Word FromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["FromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004D68 RID: 19816
			// (set) Token: 0x060073A5 RID: 29605 RVA: 0x000ADE0D File Offset: 0x000AC00D
			public virtual Word SenderDomainIs
			{
				set
				{
					base.PowerSharpParameters["SenderDomainIs"] = value;
				}
			}

			// Token: 0x17004D69 RID: 19817
			// (set) Token: 0x060073A6 RID: 29606 RVA: 0x000ADE25 File Offset: 0x000AC025
			public virtual Word RecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["RecipientDomainIs"] = value;
				}
			}

			// Token: 0x17004D6A RID: 19818
			// (set) Token: 0x060073A7 RID: 29607 RVA: 0x000ADE3D File Offset: 0x000AC03D
			public virtual Pattern SubjectMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SubjectMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D6B RID: 19819
			// (set) Token: 0x060073A8 RID: 29608 RVA: 0x000ADE55 File Offset: 0x000AC055
			public virtual Pattern SubjectOrBodyMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D6C RID: 19820
			// (set) Token: 0x060073A9 RID: 29609 RVA: 0x000ADE6D File Offset: 0x000AC06D
			public virtual HeaderName? HeaderMatchesMessageHeader
			{
				set
				{
					base.PowerSharpParameters["HeaderMatchesMessageHeader"] = value;
				}
			}

			// Token: 0x17004D6D RID: 19821
			// (set) Token: 0x060073AA RID: 29610 RVA: 0x000ADE85 File Offset: 0x000AC085
			public virtual Pattern HeaderMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["HeaderMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D6E RID: 19822
			// (set) Token: 0x060073AB RID: 29611 RVA: 0x000ADE9D File Offset: 0x000AC09D
			public virtual Pattern FromAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["FromAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D6F RID: 19823
			// (set) Token: 0x060073AC RID: 29612 RVA: 0x000ADEB5 File Offset: 0x000AC0B5
			public virtual Pattern AttachmentNameMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AttachmentNameMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D70 RID: 19824
			// (set) Token: 0x060073AD RID: 29613 RVA: 0x000ADECD File Offset: 0x000AC0CD
			public virtual Word AttachmentExtensionMatchesWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentExtensionMatchesWords"] = value;
				}
			}

			// Token: 0x17004D71 RID: 19825
			// (set) Token: 0x060073AE RID: 29614 RVA: 0x000ADEE5 File Offset: 0x000AC0E5
			public virtual Word AttachmentPropertyContainsWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentPropertyContainsWords"] = value;
				}
			}

			// Token: 0x17004D72 RID: 19826
			// (set) Token: 0x060073AF RID: 29615 RVA: 0x000ADEFD File Offset: 0x000AC0FD
			public virtual Word ContentCharacterSetContainsWords
			{
				set
				{
					base.PowerSharpParameters["ContentCharacterSetContainsWords"] = value;
				}
			}

			// Token: 0x17004D73 RID: 19827
			// (set) Token: 0x060073B0 RID: 29616 RVA: 0x000ADF15 File Offset: 0x000AC115
			public virtual SclValue? SCLOver
			{
				set
				{
					base.PowerSharpParameters["SCLOver"] = value;
				}
			}

			// Token: 0x17004D74 RID: 19828
			// (set) Token: 0x060073B1 RID: 29617 RVA: 0x000ADF2D File Offset: 0x000AC12D
			public virtual ByteQuantifiedSize? AttachmentSizeOver
			{
				set
				{
					base.PowerSharpParameters["AttachmentSizeOver"] = value;
				}
			}

			// Token: 0x17004D75 RID: 19829
			// (set) Token: 0x060073B2 RID: 29618 RVA: 0x000ADF45 File Offset: 0x000AC145
			public virtual ByteQuantifiedSize? MessageSizeOver
			{
				set
				{
					base.PowerSharpParameters["MessageSizeOver"] = value;
				}
			}

			// Token: 0x17004D76 RID: 19830
			// (set) Token: 0x060073B3 RID: 29619 RVA: 0x000ADF5D File Offset: 0x000AC15D
			public virtual Importance? WithImportance
			{
				set
				{
					base.PowerSharpParameters["WithImportance"] = value;
				}
			}

			// Token: 0x17004D77 RID: 19831
			// (set) Token: 0x060073B4 RID: 29620 RVA: 0x000ADF75 File Offset: 0x000AC175
			public virtual MessageType? MessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["MessageTypeMatches"] = value;
				}
			}

			// Token: 0x17004D78 RID: 19832
			// (set) Token: 0x060073B5 RID: 29621 RVA: 0x000ADF8D File Offset: 0x000AC18D
			public virtual Word RecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004D79 RID: 19833
			// (set) Token: 0x060073B6 RID: 29622 RVA: 0x000ADFA5 File Offset: 0x000AC1A5
			public virtual Pattern RecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D7A RID: 19834
			// (set) Token: 0x060073B7 RID: 29623 RVA: 0x000ADFBD File Offset: 0x000AC1BD
			public virtual Word SenderInRecipientList
			{
				set
				{
					base.PowerSharpParameters["SenderInRecipientList"] = value;
				}
			}

			// Token: 0x17004D7B RID: 19835
			// (set) Token: 0x060073B8 RID: 29624 RVA: 0x000ADFD5 File Offset: 0x000AC1D5
			public virtual Word RecipientInSenderList
			{
				set
				{
					base.PowerSharpParameters["RecipientInSenderList"] = value;
				}
			}

			// Token: 0x17004D7C RID: 19836
			// (set) Token: 0x060073B9 RID: 29625 RVA: 0x000ADFED File Offset: 0x000AC1ED
			public virtual Word AttachmentContainsWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentContainsWords"] = value;
				}
			}

			// Token: 0x17004D7D RID: 19837
			// (set) Token: 0x060073BA RID: 29626 RVA: 0x000AE005 File Offset: 0x000AC205
			public virtual Pattern AttachmentMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AttachmentMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D7E RID: 19838
			// (set) Token: 0x060073BB RID: 29627 RVA: 0x000AE01D File Offset: 0x000AC21D
			public virtual bool AttachmentIsUnsupported
			{
				set
				{
					base.PowerSharpParameters["AttachmentIsUnsupported"] = value;
				}
			}

			// Token: 0x17004D7F RID: 19839
			// (set) Token: 0x060073BC RID: 29628 RVA: 0x000AE035 File Offset: 0x000AC235
			public virtual bool AttachmentProcessingLimitExceeded
			{
				set
				{
					base.PowerSharpParameters["AttachmentProcessingLimitExceeded"] = value;
				}
			}

			// Token: 0x17004D80 RID: 19840
			// (set) Token: 0x060073BD RID: 29629 RVA: 0x000AE04D File Offset: 0x000AC24D
			public virtual bool AttachmentHasExecutableContent
			{
				set
				{
					base.PowerSharpParameters["AttachmentHasExecutableContent"] = value;
				}
			}

			// Token: 0x17004D81 RID: 19841
			// (set) Token: 0x060073BE RID: 29630 RVA: 0x000AE065 File Offset: 0x000AC265
			public virtual bool AttachmentIsPasswordProtected
			{
				set
				{
					base.PowerSharpParameters["AttachmentIsPasswordProtected"] = value;
				}
			}

			// Token: 0x17004D82 RID: 19842
			// (set) Token: 0x060073BF RID: 29631 RVA: 0x000AE07D File Offset: 0x000AC27D
			public virtual Word AnyOfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["AnyOfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004D83 RID: 19843
			// (set) Token: 0x060073C0 RID: 29632 RVA: 0x000AE095 File Offset: 0x000AC295
			public virtual Pattern AnyOfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AnyOfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D84 RID: 19844
			// (set) Token: 0x060073C1 RID: 29633 RVA: 0x000AE0AD File Offset: 0x000AC2AD
			public virtual RecipientIdParameter ExceptIfFrom
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFrom"] = value;
				}
			}

			// Token: 0x17004D85 RID: 19845
			// (set) Token: 0x060073C2 RID: 29634 RVA: 0x000AE0C0 File Offset: 0x000AC2C0
			public virtual bool HasSenderOverride
			{
				set
				{
					base.PowerSharpParameters["HasSenderOverride"] = value;
				}
			}

			// Token: 0x17004D86 RID: 19846
			// (set) Token: 0x060073C3 RID: 29635 RVA: 0x000AE0D8 File Offset: 0x000AC2D8
			public virtual Hashtable MessageContainsDataClassifications
			{
				set
				{
					base.PowerSharpParameters["MessageContainsDataClassifications"] = value;
				}
			}

			// Token: 0x17004D87 RID: 19847
			// (set) Token: 0x060073C4 RID: 29636 RVA: 0x000AE0EB File Offset: 0x000AC2EB
			public virtual MultiValuedProperty<IPRange> SenderIpRanges
			{
				set
				{
					base.PowerSharpParameters["SenderIpRanges"] = value;
				}
			}

			// Token: 0x17004D88 RID: 19848
			// (set) Token: 0x060073C5 RID: 29637 RVA: 0x000AE0FE File Offset: 0x000AC2FE
			public virtual RecipientIdParameter ExceptIfFromMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromMemberOf"] = value;
				}
			}

			// Token: 0x17004D89 RID: 19849
			// (set) Token: 0x060073C6 RID: 29638 RVA: 0x000AE111 File Offset: 0x000AC311
			public virtual FromUserScope? ExceptIfFromScope
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromScope"] = value;
				}
			}

			// Token: 0x17004D8A RID: 19850
			// (set) Token: 0x060073C7 RID: 29639 RVA: 0x000AE129 File Offset: 0x000AC329
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17004D8B RID: 19851
			// (set) Token: 0x060073C8 RID: 29640 RVA: 0x000AE13C File Offset: 0x000AC33C
			public virtual RecipientIdParameter ExceptIfSentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToMemberOf"] = value;
				}
			}

			// Token: 0x17004D8C RID: 19852
			// (set) Token: 0x060073C9 RID: 29641 RVA: 0x000AE14F File Offset: 0x000AC34F
			public virtual ToUserScope? ExceptIfSentToScope
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToScope"] = value;
				}
			}

			// Token: 0x17004D8D RID: 19853
			// (set) Token: 0x060073CA RID: 29642 RVA: 0x000AE167 File Offset: 0x000AC367
			public virtual RecipientIdParameter ExceptIfBetweenMemberOf1
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBetweenMemberOf1"] = value;
				}
			}

			// Token: 0x17004D8E RID: 19854
			// (set) Token: 0x060073CB RID: 29643 RVA: 0x000AE17A File Offset: 0x000AC37A
			public virtual RecipientIdParameter ExceptIfBetweenMemberOf2
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBetweenMemberOf2"] = value;
				}
			}

			// Token: 0x17004D8F RID: 19855
			// (set) Token: 0x060073CC RID: 29644 RVA: 0x000AE18D File Offset: 0x000AC38D
			public virtual RecipientIdParameter ExceptIfManagerAddresses
			{
				set
				{
					base.PowerSharpParameters["ExceptIfManagerAddresses"] = value;
				}
			}

			// Token: 0x17004D90 RID: 19856
			// (set) Token: 0x060073CD RID: 29645 RVA: 0x000AE1A0 File Offset: 0x000AC3A0
			public virtual EvaluatedUser? ExceptIfManagerForEvaluatedUser
			{
				set
				{
					base.PowerSharpParameters["ExceptIfManagerForEvaluatedUser"] = value;
				}
			}

			// Token: 0x17004D91 RID: 19857
			// (set) Token: 0x060073CE RID: 29646 RVA: 0x000AE1B8 File Offset: 0x000AC3B8
			public virtual ManagementRelationship? ExceptIfSenderManagementRelationship
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderManagementRelationship"] = value;
				}
			}

			// Token: 0x17004D92 RID: 19858
			// (set) Token: 0x060073CF RID: 29647 RVA: 0x000AE1D0 File Offset: 0x000AC3D0
			public virtual ADAttribute? ExceptIfADComparisonAttribute
			{
				set
				{
					base.PowerSharpParameters["ExceptIfADComparisonAttribute"] = value;
				}
			}

			// Token: 0x17004D93 RID: 19859
			// (set) Token: 0x060073D0 RID: 29648 RVA: 0x000AE1E8 File Offset: 0x000AC3E8
			public virtual Evaluation? ExceptIfADComparisonOperator
			{
				set
				{
					base.PowerSharpParameters["ExceptIfADComparisonOperator"] = value;
				}
			}

			// Token: 0x17004D94 RID: 19860
			// (set) Token: 0x060073D1 RID: 29649 RVA: 0x000AE200 File Offset: 0x000AC400
			public virtual Word ExceptIfSenderADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004D95 RID: 19861
			// (set) Token: 0x060073D2 RID: 29650 RVA: 0x000AE218 File Offset: 0x000AC418
			public virtual Pattern ExceptIfSenderADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D96 RID: 19862
			// (set) Token: 0x060073D3 RID: 29651 RVA: 0x000AE230 File Offset: 0x000AC430
			public virtual Word ExceptIfRecipientADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004D97 RID: 19863
			// (set) Token: 0x060073D4 RID: 29652 RVA: 0x000AE248 File Offset: 0x000AC448
			public virtual Pattern ExceptIfRecipientADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004D98 RID: 19864
			// (set) Token: 0x060073D5 RID: 29653 RVA: 0x000AE260 File Offset: 0x000AC460
			public virtual RecipientIdParameter ExceptIfAnyOfToHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToHeader"] = value;
				}
			}

			// Token: 0x17004D99 RID: 19865
			// (set) Token: 0x060073D6 RID: 29654 RVA: 0x000AE273 File Offset: 0x000AC473
			public virtual RecipientIdParameter ExceptIfAnyOfToHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004D9A RID: 19866
			// (set) Token: 0x060073D7 RID: 29655 RVA: 0x000AE286 File Offset: 0x000AC486
			public virtual RecipientIdParameter ExceptIfAnyOfCcHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfCcHeader"] = value;
				}
			}

			// Token: 0x17004D9B RID: 19867
			// (set) Token: 0x060073D8 RID: 29656 RVA: 0x000AE299 File Offset: 0x000AC499
			public virtual RecipientIdParameter ExceptIfAnyOfCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004D9C RID: 19868
			// (set) Token: 0x060073D9 RID: 29657 RVA: 0x000AE2AC File Offset: 0x000AC4AC
			public virtual RecipientIdParameter ExceptIfAnyOfToCcHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToCcHeader"] = value;
				}
			}

			// Token: 0x17004D9D RID: 19869
			// (set) Token: 0x060073DA RID: 29658 RVA: 0x000AE2BF File Offset: 0x000AC4BF
			public virtual RecipientIdParameter ExceptIfAnyOfToCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004D9E RID: 19870
			// (set) Token: 0x060073DB RID: 29659 RVA: 0x000AE2D2 File Offset: 0x000AC4D2
			public virtual string ExceptIfHasClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasClassification"] = value;
				}
			}

			// Token: 0x17004D9F RID: 19871
			// (set) Token: 0x060073DC RID: 29660 RVA: 0x000AE2E5 File Offset: 0x000AC4E5
			public virtual bool ExceptIfHasNoClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasNoClassification"] = value;
				}
			}

			// Token: 0x17004DA0 RID: 19872
			// (set) Token: 0x060073DD RID: 29661 RVA: 0x000AE2FD File Offset: 0x000AC4FD
			public virtual Word ExceptIfSubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectContainsWords"] = value;
				}
			}

			// Token: 0x17004DA1 RID: 19873
			// (set) Token: 0x060073DE RID: 29662 RVA: 0x000AE315 File Offset: 0x000AC515
			public virtual Word ExceptIfSubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17004DA2 RID: 19874
			// (set) Token: 0x060073DF RID: 29663 RVA: 0x000AE32D File Offset: 0x000AC52D
			public virtual HeaderName? ExceptIfHeaderContainsMessageHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsMessageHeader"] = value;
				}
			}

			// Token: 0x17004DA3 RID: 19875
			// (set) Token: 0x060073E0 RID: 29664 RVA: 0x000AE345 File Offset: 0x000AC545
			public virtual Word ExceptIfHeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsWords"] = value;
				}
			}

			// Token: 0x17004DA4 RID: 19876
			// (set) Token: 0x060073E1 RID: 29665 RVA: 0x000AE35D File Offset: 0x000AC55D
			public virtual Word ExceptIfFromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004DA5 RID: 19877
			// (set) Token: 0x060073E2 RID: 29666 RVA: 0x000AE375 File Offset: 0x000AC575
			public virtual Word ExceptIfSenderDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderDomainIs"] = value;
				}
			}

			// Token: 0x17004DA6 RID: 19878
			// (set) Token: 0x060073E3 RID: 29667 RVA: 0x000AE38D File Offset: 0x000AC58D
			public virtual Word ExceptIfRecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientDomainIs"] = value;
				}
			}

			// Token: 0x17004DA7 RID: 19879
			// (set) Token: 0x060073E4 RID: 29668 RVA: 0x000AE3A5 File Offset: 0x000AC5A5
			public virtual Pattern ExceptIfSubjectMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DA8 RID: 19880
			// (set) Token: 0x060073E5 RID: 29669 RVA: 0x000AE3BD File Offset: 0x000AC5BD
			public virtual Pattern ExceptIfSubjectOrBodyMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DA9 RID: 19881
			// (set) Token: 0x060073E6 RID: 29670 RVA: 0x000AE3D5 File Offset: 0x000AC5D5
			public virtual HeaderName? ExceptIfHeaderMatchesMessageHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderMatchesMessageHeader"] = value;
				}
			}

			// Token: 0x17004DAA RID: 19882
			// (set) Token: 0x060073E7 RID: 29671 RVA: 0x000AE3ED File Offset: 0x000AC5ED
			public virtual Pattern ExceptIfHeaderMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DAB RID: 19883
			// (set) Token: 0x060073E8 RID: 29672 RVA: 0x000AE405 File Offset: 0x000AC605
			public virtual Pattern ExceptIfFromAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DAC RID: 19884
			// (set) Token: 0x060073E9 RID: 29673 RVA: 0x000AE41D File Offset: 0x000AC61D
			public virtual Pattern ExceptIfAttachmentNameMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentNameMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DAD RID: 19885
			// (set) Token: 0x060073EA RID: 29674 RVA: 0x000AE435 File Offset: 0x000AC635
			public virtual Word ExceptIfAttachmentExtensionMatchesWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentExtensionMatchesWords"] = value;
				}
			}

			// Token: 0x17004DAE RID: 19886
			// (set) Token: 0x060073EB RID: 29675 RVA: 0x000AE44D File Offset: 0x000AC64D
			public virtual Word ExceptIfAttachmentPropertyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentPropertyContainsWords"] = value;
				}
			}

			// Token: 0x17004DAF RID: 19887
			// (set) Token: 0x060073EC RID: 29676 RVA: 0x000AE465 File Offset: 0x000AC665
			public virtual Word ExceptIfContentCharacterSetContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfContentCharacterSetContainsWords"] = value;
				}
			}

			// Token: 0x17004DB0 RID: 19888
			// (set) Token: 0x060073ED RID: 29677 RVA: 0x000AE47D File Offset: 0x000AC67D
			public virtual SclValue? ExceptIfSCLOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSCLOver"] = value;
				}
			}

			// Token: 0x17004DB1 RID: 19889
			// (set) Token: 0x060073EE RID: 29678 RVA: 0x000AE495 File Offset: 0x000AC695
			public virtual ByteQuantifiedSize? ExceptIfAttachmentSizeOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentSizeOver"] = value;
				}
			}

			// Token: 0x17004DB2 RID: 19890
			// (set) Token: 0x060073EF RID: 29679 RVA: 0x000AE4AD File Offset: 0x000AC6AD
			public virtual ByteQuantifiedSize? ExceptIfMessageSizeOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageSizeOver"] = value;
				}
			}

			// Token: 0x17004DB3 RID: 19891
			// (set) Token: 0x060073F0 RID: 29680 RVA: 0x000AE4C5 File Offset: 0x000AC6C5
			public virtual Importance? ExceptIfWithImportance
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithImportance"] = value;
				}
			}

			// Token: 0x17004DB4 RID: 19892
			// (set) Token: 0x060073F1 RID: 29681 RVA: 0x000AE4DD File Offset: 0x000AC6DD
			public virtual MessageType? ExceptIfMessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageTypeMatches"] = value;
				}
			}

			// Token: 0x17004DB5 RID: 19893
			// (set) Token: 0x060073F2 RID: 29682 RVA: 0x000AE4F5 File Offset: 0x000AC6F5
			public virtual Word ExceptIfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004DB6 RID: 19894
			// (set) Token: 0x060073F3 RID: 29683 RVA: 0x000AE50D File Offset: 0x000AC70D
			public virtual Pattern ExceptIfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DB7 RID: 19895
			// (set) Token: 0x060073F4 RID: 29684 RVA: 0x000AE525 File Offset: 0x000AC725
			public virtual Word ExceptIfSenderInRecipientList
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderInRecipientList"] = value;
				}
			}

			// Token: 0x17004DB8 RID: 19896
			// (set) Token: 0x060073F5 RID: 29685 RVA: 0x000AE53D File Offset: 0x000AC73D
			public virtual Word ExceptIfRecipientInSenderList
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientInSenderList"] = value;
				}
			}

			// Token: 0x17004DB9 RID: 19897
			// (set) Token: 0x060073F6 RID: 29686 RVA: 0x000AE555 File Offset: 0x000AC755
			public virtual Word ExceptIfAttachmentContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentContainsWords"] = value;
				}
			}

			// Token: 0x17004DBA RID: 19898
			// (set) Token: 0x060073F7 RID: 29687 RVA: 0x000AE56D File Offset: 0x000AC76D
			public virtual Pattern ExceptIfAttachmentMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DBB RID: 19899
			// (set) Token: 0x060073F8 RID: 29688 RVA: 0x000AE585 File Offset: 0x000AC785
			public virtual bool ExceptIfAttachmentIsUnsupported
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentIsUnsupported"] = value;
				}
			}

			// Token: 0x17004DBC RID: 19900
			// (set) Token: 0x060073F9 RID: 29689 RVA: 0x000AE59D File Offset: 0x000AC79D
			public virtual bool ExceptIfAttachmentProcessingLimitExceeded
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentProcessingLimitExceeded"] = value;
				}
			}

			// Token: 0x17004DBD RID: 19901
			// (set) Token: 0x060073FA RID: 29690 RVA: 0x000AE5B5 File Offset: 0x000AC7B5
			public virtual bool ExceptIfAttachmentHasExecutableContent
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentHasExecutableContent"] = value;
				}
			}

			// Token: 0x17004DBE RID: 19902
			// (set) Token: 0x060073FB RID: 29691 RVA: 0x000AE5CD File Offset: 0x000AC7CD
			public virtual Word ExceptIfAnyOfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004DBF RID: 19903
			// (set) Token: 0x060073FC RID: 29692 RVA: 0x000AE5E5 File Offset: 0x000AC7E5
			public virtual bool ExceptIfAttachmentIsPasswordProtected
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentIsPasswordProtected"] = value;
				}
			}

			// Token: 0x17004DC0 RID: 19904
			// (set) Token: 0x060073FD RID: 29693 RVA: 0x000AE5FD File Offset: 0x000AC7FD
			public virtual Pattern ExceptIfAnyOfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004DC1 RID: 19905
			// (set) Token: 0x060073FE RID: 29694 RVA: 0x000AE615 File Offset: 0x000AC815
			public virtual bool ExceptIfHasSenderOverride
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasSenderOverride"] = value;
				}
			}

			// Token: 0x17004DC2 RID: 19906
			// (set) Token: 0x060073FF RID: 29695 RVA: 0x000AE62D File Offset: 0x000AC82D
			public virtual Hashtable ExceptIfMessageContainsDataClassifications
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageContainsDataClassifications"] = value;
				}
			}

			// Token: 0x17004DC3 RID: 19907
			// (set) Token: 0x06007400 RID: 29696 RVA: 0x000AE640 File Offset: 0x000AC840
			public virtual MultiValuedProperty<IPRange> ExceptIfSenderIpRanges
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderIpRanges"] = value;
				}
			}

			// Token: 0x17004DC4 RID: 19908
			// (set) Token: 0x06007401 RID: 29697 RVA: 0x000AE653 File Offset: 0x000AC853
			public virtual SubjectPrefix? PrependSubject
			{
				set
				{
					base.PowerSharpParameters["PrependSubject"] = value;
				}
			}

			// Token: 0x17004DC5 RID: 19909
			// (set) Token: 0x06007402 RID: 29698 RVA: 0x000AE66B File Offset: 0x000AC86B
			public virtual string SetAuditSeverity
			{
				set
				{
					base.PowerSharpParameters["SetAuditSeverity"] = value;
				}
			}

			// Token: 0x17004DC6 RID: 19910
			// (set) Token: 0x06007403 RID: 29699 RVA: 0x000AE67E File Offset: 0x000AC87E
			public virtual string ApplyClassification
			{
				set
				{
					base.PowerSharpParameters["ApplyClassification"] = value;
				}
			}

			// Token: 0x17004DC7 RID: 19911
			// (set) Token: 0x06007404 RID: 29700 RVA: 0x000AE691 File Offset: 0x000AC891
			public virtual DisclaimerLocation? ApplyHtmlDisclaimerLocation
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerLocation"] = value;
				}
			}

			// Token: 0x17004DC8 RID: 19912
			// (set) Token: 0x06007405 RID: 29701 RVA: 0x000AE6A9 File Offset: 0x000AC8A9
			public virtual DisclaimerText? ApplyHtmlDisclaimerText
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerText"] = value;
				}
			}

			// Token: 0x17004DC9 RID: 19913
			// (set) Token: 0x06007406 RID: 29702 RVA: 0x000AE6C1 File Offset: 0x000AC8C1
			public virtual DisclaimerFallbackAction? ApplyHtmlDisclaimerFallbackAction
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerFallbackAction"] = value;
				}
			}

			// Token: 0x17004DCA RID: 19914
			// (set) Token: 0x06007407 RID: 29703 RVA: 0x000AE6D9 File Offset: 0x000AC8D9
			public virtual string ApplyRightsProtectionTemplate
			{
				set
				{
					base.PowerSharpParameters["ApplyRightsProtectionTemplate"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17004DCB RID: 19915
			// (set) Token: 0x06007408 RID: 29704 RVA: 0x000AE6F7 File Offset: 0x000AC8F7
			public virtual SclValue? SetSCL
			{
				set
				{
					base.PowerSharpParameters["SetSCL"] = value;
				}
			}

			// Token: 0x17004DCC RID: 19916
			// (set) Token: 0x06007409 RID: 29705 RVA: 0x000AE70F File Offset: 0x000AC90F
			public virtual HeaderName? SetHeaderName
			{
				set
				{
					base.PowerSharpParameters["SetHeaderName"] = value;
				}
			}

			// Token: 0x17004DCD RID: 19917
			// (set) Token: 0x0600740A RID: 29706 RVA: 0x000AE727 File Offset: 0x000AC927
			public virtual HeaderValue? SetHeaderValue
			{
				set
				{
					base.PowerSharpParameters["SetHeaderValue"] = value;
				}
			}

			// Token: 0x17004DCE RID: 19918
			// (set) Token: 0x0600740B RID: 29707 RVA: 0x000AE73F File Offset: 0x000AC93F
			public virtual HeaderName? RemoveHeader
			{
				set
				{
					base.PowerSharpParameters["RemoveHeader"] = value;
				}
			}

			// Token: 0x17004DCF RID: 19919
			// (set) Token: 0x0600740C RID: 29708 RVA: 0x000AE757 File Offset: 0x000AC957
			public virtual RecipientIdParameter AddToRecipients
			{
				set
				{
					base.PowerSharpParameters["AddToRecipients"] = value;
				}
			}

			// Token: 0x17004DD0 RID: 19920
			// (set) Token: 0x0600740D RID: 29709 RVA: 0x000AE76A File Offset: 0x000AC96A
			public virtual RecipientIdParameter CopyTo
			{
				set
				{
					base.PowerSharpParameters["CopyTo"] = value;
				}
			}

			// Token: 0x17004DD1 RID: 19921
			// (set) Token: 0x0600740E RID: 29710 RVA: 0x000AE77D File Offset: 0x000AC97D
			public virtual RecipientIdParameter BlindCopyTo
			{
				set
				{
					base.PowerSharpParameters["BlindCopyTo"] = value;
				}
			}

			// Token: 0x17004DD2 RID: 19922
			// (set) Token: 0x0600740F RID: 29711 RVA: 0x000AE790 File Offset: 0x000AC990
			public virtual AddedRecipientType? AddManagerAsRecipientType
			{
				set
				{
					base.PowerSharpParameters["AddManagerAsRecipientType"] = value;
				}
			}

			// Token: 0x17004DD3 RID: 19923
			// (set) Token: 0x06007410 RID: 29712 RVA: 0x000AE7A8 File Offset: 0x000AC9A8
			public virtual RecipientIdParameter ModerateMessageByUser
			{
				set
				{
					base.PowerSharpParameters["ModerateMessageByUser"] = value;
				}
			}

			// Token: 0x17004DD4 RID: 19924
			// (set) Token: 0x06007411 RID: 29713 RVA: 0x000AE7BB File Offset: 0x000AC9BB
			public virtual bool ModerateMessageByManager
			{
				set
				{
					base.PowerSharpParameters["ModerateMessageByManager"] = value;
				}
			}

			// Token: 0x17004DD5 RID: 19925
			// (set) Token: 0x06007412 RID: 29714 RVA: 0x000AE7D3 File Offset: 0x000AC9D3
			public virtual RecipientIdParameter RedirectMessageTo
			{
				set
				{
					base.PowerSharpParameters["RedirectMessageTo"] = value;
				}
			}

			// Token: 0x17004DD6 RID: 19926
			// (set) Token: 0x06007413 RID: 29715 RVA: 0x000AE7E6 File Offset: 0x000AC9E6
			public virtual NotifySenderType? NotifySender
			{
				set
				{
					base.PowerSharpParameters["NotifySender"] = value;
				}
			}

			// Token: 0x17004DD7 RID: 19927
			// (set) Token: 0x06007414 RID: 29716 RVA: 0x000AE7FE File Offset: 0x000AC9FE
			public virtual RejectEnhancedStatus? RejectMessageEnhancedStatusCode
			{
				set
				{
					base.PowerSharpParameters["RejectMessageEnhancedStatusCode"] = value;
				}
			}

			// Token: 0x17004DD8 RID: 19928
			// (set) Token: 0x06007415 RID: 29717 RVA: 0x000AE816 File Offset: 0x000ACA16
			public virtual DsnText? RejectMessageReasonText
			{
				set
				{
					base.PowerSharpParameters["RejectMessageReasonText"] = value;
				}
			}

			// Token: 0x17004DD9 RID: 19929
			// (set) Token: 0x06007416 RID: 29718 RVA: 0x000AE82E File Offset: 0x000ACA2E
			public virtual bool DeleteMessage
			{
				set
				{
					base.PowerSharpParameters["DeleteMessage"] = value;
				}
			}

			// Token: 0x17004DDA RID: 19930
			// (set) Token: 0x06007417 RID: 29719 RVA: 0x000AE846 File Offset: 0x000ACA46
			public virtual bool Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17004DDB RID: 19931
			// (set) Token: 0x06007418 RID: 29720 RVA: 0x000AE85E File Offset: 0x000ACA5E
			public virtual bool Quarantine
			{
				set
				{
					base.PowerSharpParameters["Quarantine"] = value;
				}
			}

			// Token: 0x17004DDC RID: 19932
			// (set) Token: 0x06007419 RID: 29721 RVA: 0x000AE876 File Offset: 0x000ACA76
			public virtual RejectText? SmtpRejectMessageRejectText
			{
				set
				{
					base.PowerSharpParameters["SmtpRejectMessageRejectText"] = value;
				}
			}

			// Token: 0x17004DDD RID: 19933
			// (set) Token: 0x0600741A RID: 29722 RVA: 0x000AE88E File Offset: 0x000ACA8E
			public virtual RejectStatusCode? SmtpRejectMessageRejectStatusCode
			{
				set
				{
					base.PowerSharpParameters["SmtpRejectMessageRejectStatusCode"] = value;
				}
			}

			// Token: 0x17004DDE RID: 19934
			// (set) Token: 0x0600741B RID: 29723 RVA: 0x000AE8A6 File Offset: 0x000ACAA6
			public virtual DateTime? ActivationDate
			{
				set
				{
					base.PowerSharpParameters["ActivationDate"] = value;
				}
			}

			// Token: 0x17004DDF RID: 19935
			// (set) Token: 0x0600741C RID: 29724 RVA: 0x000AE8BE File Offset: 0x000ACABE
			public virtual DateTime? ExpiryDate
			{
				set
				{
					base.PowerSharpParameters["ExpiryDate"] = value;
				}
			}

			// Token: 0x17004DE0 RID: 19936
			// (set) Token: 0x0600741D RID: 29725 RVA: 0x000AE8D6 File Offset: 0x000ACAD6
			public virtual RuleSubType RuleSubType
			{
				set
				{
					base.PowerSharpParameters["RuleSubType"] = value;
				}
			}

			// Token: 0x17004DE1 RID: 19937
			// (set) Token: 0x0600741E RID: 29726 RVA: 0x000AE8EE File Offset: 0x000ACAEE
			public virtual RuleErrorAction RuleErrorAction
			{
				set
				{
					base.PowerSharpParameters["RuleErrorAction"] = value;
				}
			}

			// Token: 0x17004DE2 RID: 19938
			// (set) Token: 0x0600741F RID: 29727 RVA: 0x000AE906 File Offset: 0x000ACB06
			public virtual SenderAddressLocation SenderAddressLocation
			{
				set
				{
					base.PowerSharpParameters["SenderAddressLocation"] = value;
				}
			}

			// Token: 0x17004DE3 RID: 19939
			// (set) Token: 0x06007420 RID: 29728 RVA: 0x000AE91E File Offset: 0x000ACB1E
			public virtual EventLogText? LogEventText
			{
				set
				{
					base.PowerSharpParameters["LogEventText"] = value;
				}
			}

			// Token: 0x17004DE4 RID: 19940
			// (set) Token: 0x06007421 RID: 29729 RVA: 0x000AE936 File Offset: 0x000ACB36
			public virtual bool StopRuleProcessing
			{
				set
				{
					base.PowerSharpParameters["StopRuleProcessing"] = value;
				}
			}

			// Token: 0x17004DE5 RID: 19941
			// (set) Token: 0x06007422 RID: 29730 RVA: 0x000AE94E File Offset: 0x000ACB4E
			public virtual string GenerateIncidentReport
			{
				set
				{
					base.PowerSharpParameters["GenerateIncidentReport"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17004DE6 RID: 19942
			// (set) Token: 0x06007423 RID: 29731 RVA: 0x000AE96C File Offset: 0x000ACB6C
			public virtual IncidentReportOriginalMail? IncidentReportOriginalMail
			{
				set
				{
					base.PowerSharpParameters["IncidentReportOriginalMail"] = value;
				}
			}

			// Token: 0x17004DE7 RID: 19943
			// (set) Token: 0x06007424 RID: 29732 RVA: 0x000AE984 File Offset: 0x000ACB84
			public virtual IncidentReportContent IncidentReportContent
			{
				set
				{
					base.PowerSharpParameters["IncidentReportContent"] = value;
				}
			}

			// Token: 0x17004DE8 RID: 19944
			// (set) Token: 0x06007425 RID: 29733 RVA: 0x000AE99C File Offset: 0x000ACB9C
			public virtual DisclaimerText? GenerateNotification
			{
				set
				{
					base.PowerSharpParameters["GenerateNotification"] = value;
				}
			}

			// Token: 0x17004DE9 RID: 19945
			// (set) Token: 0x06007426 RID: 29734 RVA: 0x000AE9B4 File Offset: 0x000ACBB4
			public virtual OutboundConnectorIdParameter RouteMessageOutboundConnector
			{
				set
				{
					base.PowerSharpParameters["RouteMessageOutboundConnector"] = value;
				}
			}

			// Token: 0x17004DEA RID: 19946
			// (set) Token: 0x06007427 RID: 29735 RVA: 0x000AE9C7 File Offset: 0x000ACBC7
			public virtual bool RouteMessageOutboundRequireTls
			{
				set
				{
					base.PowerSharpParameters["RouteMessageOutboundRequireTls"] = value;
				}
			}

			// Token: 0x17004DEB RID: 19947
			// (set) Token: 0x06007428 RID: 29736 RVA: 0x000AE9DF File Offset: 0x000ACBDF
			public virtual bool ApplyOME
			{
				set
				{
					base.PowerSharpParameters["ApplyOME"] = value;
				}
			}

			// Token: 0x17004DEC RID: 19948
			// (set) Token: 0x06007429 RID: 29737 RVA: 0x000AE9F7 File Offset: 0x000ACBF7
			public virtual bool RemoveOME
			{
				set
				{
					base.PowerSharpParameters["RemoveOME"] = value;
				}
			}

			// Token: 0x17004DED RID: 19949
			// (set) Token: 0x0600742A RID: 29738 RVA: 0x000AEA0F File Offset: 0x000ACC0F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004DEE RID: 19950
			// (set) Token: 0x0600742B RID: 29739 RVA: 0x000AEA22 File Offset: 0x000ACC22
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004DEF RID: 19951
			// (set) Token: 0x0600742C RID: 29740 RVA: 0x000AEA3A File Offset: 0x000ACC3A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004DF0 RID: 19952
			// (set) Token: 0x0600742D RID: 29741 RVA: 0x000AEA52 File Offset: 0x000ACC52
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004DF1 RID: 19953
			// (set) Token: 0x0600742E RID: 29742 RVA: 0x000AEA6A File Offset: 0x000ACC6A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004DF2 RID: 19954
			// (set) Token: 0x0600742F RID: 29743 RVA: 0x000AEA82 File Offset: 0x000ACC82
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008FB RID: 2299
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004DF3 RID: 19955
			// (set) Token: 0x06007431 RID: 29745 RVA: 0x000AEAA2 File Offset: 0x000ACCA2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004DF4 RID: 19956
			// (set) Token: 0x06007432 RID: 29746 RVA: 0x000AEAC0 File Offset: 0x000ACCC0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004DF5 RID: 19957
			// (set) Token: 0x06007433 RID: 29747 RVA: 0x000AEAD3 File Offset: 0x000ACCD3
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004DF6 RID: 19958
			// (set) Token: 0x06007434 RID: 29748 RVA: 0x000AEAEB File Offset: 0x000ACCEB
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17004DF7 RID: 19959
			// (set) Token: 0x06007435 RID: 29749 RVA: 0x000AEB03 File Offset: 0x000ACD03
			public virtual string Comments
			{
				set
				{
					base.PowerSharpParameters["Comments"] = value;
				}
			}

			// Token: 0x17004DF8 RID: 19960
			// (set) Token: 0x06007436 RID: 29750 RVA: 0x000AEB16 File Offset: 0x000ACD16
			public virtual string DlpPolicy
			{
				set
				{
					base.PowerSharpParameters["DlpPolicy"] = value;
				}
			}

			// Token: 0x17004DF9 RID: 19961
			// (set) Token: 0x06007437 RID: 29751 RVA: 0x000AEB29 File Offset: 0x000ACD29
			public virtual RecipientIdParameter From
			{
				set
				{
					base.PowerSharpParameters["From"] = value;
				}
			}

			// Token: 0x17004DFA RID: 19962
			// (set) Token: 0x06007438 RID: 29752 RVA: 0x000AEB3C File Offset: 0x000ACD3C
			public virtual RecipientIdParameter FromMemberOf
			{
				set
				{
					base.PowerSharpParameters["FromMemberOf"] = value;
				}
			}

			// Token: 0x17004DFB RID: 19963
			// (set) Token: 0x06007439 RID: 29753 RVA: 0x000AEB4F File Offset: 0x000ACD4F
			public virtual FromUserScope? FromScope
			{
				set
				{
					base.PowerSharpParameters["FromScope"] = value;
				}
			}

			// Token: 0x17004DFC RID: 19964
			// (set) Token: 0x0600743A RID: 29754 RVA: 0x000AEB67 File Offset: 0x000ACD67
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17004DFD RID: 19965
			// (set) Token: 0x0600743B RID: 29755 RVA: 0x000AEB7A File Offset: 0x000ACD7A
			public virtual RecipientIdParameter SentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["SentToMemberOf"] = value;
				}
			}

			// Token: 0x17004DFE RID: 19966
			// (set) Token: 0x0600743C RID: 29756 RVA: 0x000AEB8D File Offset: 0x000ACD8D
			public virtual ToUserScope? SentToScope
			{
				set
				{
					base.PowerSharpParameters["SentToScope"] = value;
				}
			}

			// Token: 0x17004DFF RID: 19967
			// (set) Token: 0x0600743D RID: 29757 RVA: 0x000AEBA5 File Offset: 0x000ACDA5
			public virtual RecipientIdParameter BetweenMemberOf1
			{
				set
				{
					base.PowerSharpParameters["BetweenMemberOf1"] = value;
				}
			}

			// Token: 0x17004E00 RID: 19968
			// (set) Token: 0x0600743E RID: 29758 RVA: 0x000AEBB8 File Offset: 0x000ACDB8
			public virtual RecipientIdParameter BetweenMemberOf2
			{
				set
				{
					base.PowerSharpParameters["BetweenMemberOf2"] = value;
				}
			}

			// Token: 0x17004E01 RID: 19969
			// (set) Token: 0x0600743F RID: 29759 RVA: 0x000AEBCB File Offset: 0x000ACDCB
			public virtual RecipientIdParameter ManagerAddresses
			{
				set
				{
					base.PowerSharpParameters["ManagerAddresses"] = value;
				}
			}

			// Token: 0x17004E02 RID: 19970
			// (set) Token: 0x06007440 RID: 29760 RVA: 0x000AEBDE File Offset: 0x000ACDDE
			public virtual EvaluatedUser? ManagerForEvaluatedUser
			{
				set
				{
					base.PowerSharpParameters["ManagerForEvaluatedUser"] = value;
				}
			}

			// Token: 0x17004E03 RID: 19971
			// (set) Token: 0x06007441 RID: 29761 RVA: 0x000AEBF6 File Offset: 0x000ACDF6
			public virtual ManagementRelationship? SenderManagementRelationship
			{
				set
				{
					base.PowerSharpParameters["SenderManagementRelationship"] = value;
				}
			}

			// Token: 0x17004E04 RID: 19972
			// (set) Token: 0x06007442 RID: 29762 RVA: 0x000AEC0E File Offset: 0x000ACE0E
			public virtual ADAttribute? ADComparisonAttribute
			{
				set
				{
					base.PowerSharpParameters["ADComparisonAttribute"] = value;
				}
			}

			// Token: 0x17004E05 RID: 19973
			// (set) Token: 0x06007443 RID: 29763 RVA: 0x000AEC26 File Offset: 0x000ACE26
			public virtual Evaluation? ADComparisonOperator
			{
				set
				{
					base.PowerSharpParameters["ADComparisonOperator"] = value;
				}
			}

			// Token: 0x17004E06 RID: 19974
			// (set) Token: 0x06007444 RID: 29764 RVA: 0x000AEC3E File Offset: 0x000ACE3E
			public virtual Word SenderADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["SenderADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004E07 RID: 19975
			// (set) Token: 0x06007445 RID: 29765 RVA: 0x000AEC56 File Offset: 0x000ACE56
			public virtual Pattern SenderADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SenderADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E08 RID: 19976
			// (set) Token: 0x06007446 RID: 29766 RVA: 0x000AEC6E File Offset: 0x000ACE6E
			public virtual Word RecipientADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004E09 RID: 19977
			// (set) Token: 0x06007447 RID: 29767 RVA: 0x000AEC86 File Offset: 0x000ACE86
			public virtual Pattern RecipientADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["RecipientADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E0A RID: 19978
			// (set) Token: 0x06007448 RID: 29768 RVA: 0x000AEC9E File Offset: 0x000ACE9E
			public virtual RecipientIdParameter AnyOfToHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfToHeader"] = value;
				}
			}

			// Token: 0x17004E0B RID: 19979
			// (set) Token: 0x06007449 RID: 29769 RVA: 0x000AECB1 File Offset: 0x000ACEB1
			public virtual RecipientIdParameter AnyOfToHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfToHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004E0C RID: 19980
			// (set) Token: 0x0600744A RID: 29770 RVA: 0x000AECC4 File Offset: 0x000ACEC4
			public virtual RecipientIdParameter AnyOfCcHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfCcHeader"] = value;
				}
			}

			// Token: 0x17004E0D RID: 19981
			// (set) Token: 0x0600744B RID: 29771 RVA: 0x000AECD7 File Offset: 0x000ACED7
			public virtual RecipientIdParameter AnyOfCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004E0E RID: 19982
			// (set) Token: 0x0600744C RID: 29772 RVA: 0x000AECEA File Offset: 0x000ACEEA
			public virtual RecipientIdParameter AnyOfToCcHeader
			{
				set
				{
					base.PowerSharpParameters["AnyOfToCcHeader"] = value;
				}
			}

			// Token: 0x17004E0F RID: 19983
			// (set) Token: 0x0600744D RID: 29773 RVA: 0x000AECFD File Offset: 0x000ACEFD
			public virtual RecipientIdParameter AnyOfToCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["AnyOfToCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004E10 RID: 19984
			// (set) Token: 0x0600744E RID: 29774 RVA: 0x000AED10 File Offset: 0x000ACF10
			public virtual string HasClassification
			{
				set
				{
					base.PowerSharpParameters["HasClassification"] = value;
				}
			}

			// Token: 0x17004E11 RID: 19985
			// (set) Token: 0x0600744F RID: 29775 RVA: 0x000AED23 File Offset: 0x000ACF23
			public virtual bool HasNoClassification
			{
				set
				{
					base.PowerSharpParameters["HasNoClassification"] = value;
				}
			}

			// Token: 0x17004E12 RID: 19986
			// (set) Token: 0x06007450 RID: 29776 RVA: 0x000AED3B File Offset: 0x000ACF3B
			public virtual Word SubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectContainsWords"] = value;
				}
			}

			// Token: 0x17004E13 RID: 19987
			// (set) Token: 0x06007451 RID: 29777 RVA: 0x000AED53 File Offset: 0x000ACF53
			public virtual Word SubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17004E14 RID: 19988
			// (set) Token: 0x06007452 RID: 29778 RVA: 0x000AED6B File Offset: 0x000ACF6B
			public virtual HeaderName? HeaderContainsMessageHeader
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsMessageHeader"] = value;
				}
			}

			// Token: 0x17004E15 RID: 19989
			// (set) Token: 0x06007453 RID: 29779 RVA: 0x000AED83 File Offset: 0x000ACF83
			public virtual Word HeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["HeaderContainsWords"] = value;
				}
			}

			// Token: 0x17004E16 RID: 19990
			// (set) Token: 0x06007454 RID: 29780 RVA: 0x000AED9B File Offset: 0x000ACF9B
			public virtual Word FromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["FromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004E17 RID: 19991
			// (set) Token: 0x06007455 RID: 29781 RVA: 0x000AEDB3 File Offset: 0x000ACFB3
			public virtual Word SenderDomainIs
			{
				set
				{
					base.PowerSharpParameters["SenderDomainIs"] = value;
				}
			}

			// Token: 0x17004E18 RID: 19992
			// (set) Token: 0x06007456 RID: 29782 RVA: 0x000AEDCB File Offset: 0x000ACFCB
			public virtual Word RecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["RecipientDomainIs"] = value;
				}
			}

			// Token: 0x17004E19 RID: 19993
			// (set) Token: 0x06007457 RID: 29783 RVA: 0x000AEDE3 File Offset: 0x000ACFE3
			public virtual Pattern SubjectMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SubjectMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E1A RID: 19994
			// (set) Token: 0x06007458 RID: 29784 RVA: 0x000AEDFB File Offset: 0x000ACFFB
			public virtual Pattern SubjectOrBodyMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["SubjectOrBodyMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E1B RID: 19995
			// (set) Token: 0x06007459 RID: 29785 RVA: 0x000AEE13 File Offset: 0x000AD013
			public virtual HeaderName? HeaderMatchesMessageHeader
			{
				set
				{
					base.PowerSharpParameters["HeaderMatchesMessageHeader"] = value;
				}
			}

			// Token: 0x17004E1C RID: 19996
			// (set) Token: 0x0600745A RID: 29786 RVA: 0x000AEE2B File Offset: 0x000AD02B
			public virtual Pattern HeaderMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["HeaderMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E1D RID: 19997
			// (set) Token: 0x0600745B RID: 29787 RVA: 0x000AEE43 File Offset: 0x000AD043
			public virtual Pattern FromAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["FromAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E1E RID: 19998
			// (set) Token: 0x0600745C RID: 29788 RVA: 0x000AEE5B File Offset: 0x000AD05B
			public virtual Pattern AttachmentNameMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AttachmentNameMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E1F RID: 19999
			// (set) Token: 0x0600745D RID: 29789 RVA: 0x000AEE73 File Offset: 0x000AD073
			public virtual Word AttachmentExtensionMatchesWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentExtensionMatchesWords"] = value;
				}
			}

			// Token: 0x17004E20 RID: 20000
			// (set) Token: 0x0600745E RID: 29790 RVA: 0x000AEE8B File Offset: 0x000AD08B
			public virtual Word AttachmentPropertyContainsWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentPropertyContainsWords"] = value;
				}
			}

			// Token: 0x17004E21 RID: 20001
			// (set) Token: 0x0600745F RID: 29791 RVA: 0x000AEEA3 File Offset: 0x000AD0A3
			public virtual Word ContentCharacterSetContainsWords
			{
				set
				{
					base.PowerSharpParameters["ContentCharacterSetContainsWords"] = value;
				}
			}

			// Token: 0x17004E22 RID: 20002
			// (set) Token: 0x06007460 RID: 29792 RVA: 0x000AEEBB File Offset: 0x000AD0BB
			public virtual SclValue? SCLOver
			{
				set
				{
					base.PowerSharpParameters["SCLOver"] = value;
				}
			}

			// Token: 0x17004E23 RID: 20003
			// (set) Token: 0x06007461 RID: 29793 RVA: 0x000AEED3 File Offset: 0x000AD0D3
			public virtual ByteQuantifiedSize? AttachmentSizeOver
			{
				set
				{
					base.PowerSharpParameters["AttachmentSizeOver"] = value;
				}
			}

			// Token: 0x17004E24 RID: 20004
			// (set) Token: 0x06007462 RID: 29794 RVA: 0x000AEEEB File Offset: 0x000AD0EB
			public virtual ByteQuantifiedSize? MessageSizeOver
			{
				set
				{
					base.PowerSharpParameters["MessageSizeOver"] = value;
				}
			}

			// Token: 0x17004E25 RID: 20005
			// (set) Token: 0x06007463 RID: 29795 RVA: 0x000AEF03 File Offset: 0x000AD103
			public virtual Importance? WithImportance
			{
				set
				{
					base.PowerSharpParameters["WithImportance"] = value;
				}
			}

			// Token: 0x17004E26 RID: 20006
			// (set) Token: 0x06007464 RID: 29796 RVA: 0x000AEF1B File Offset: 0x000AD11B
			public virtual MessageType? MessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["MessageTypeMatches"] = value;
				}
			}

			// Token: 0x17004E27 RID: 20007
			// (set) Token: 0x06007465 RID: 29797 RVA: 0x000AEF33 File Offset: 0x000AD133
			public virtual Word RecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004E28 RID: 20008
			// (set) Token: 0x06007466 RID: 29798 RVA: 0x000AEF4B File Offset: 0x000AD14B
			public virtual Pattern RecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["RecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E29 RID: 20009
			// (set) Token: 0x06007467 RID: 29799 RVA: 0x000AEF63 File Offset: 0x000AD163
			public virtual Word SenderInRecipientList
			{
				set
				{
					base.PowerSharpParameters["SenderInRecipientList"] = value;
				}
			}

			// Token: 0x17004E2A RID: 20010
			// (set) Token: 0x06007468 RID: 29800 RVA: 0x000AEF7B File Offset: 0x000AD17B
			public virtual Word RecipientInSenderList
			{
				set
				{
					base.PowerSharpParameters["RecipientInSenderList"] = value;
				}
			}

			// Token: 0x17004E2B RID: 20011
			// (set) Token: 0x06007469 RID: 29801 RVA: 0x000AEF93 File Offset: 0x000AD193
			public virtual Word AttachmentContainsWords
			{
				set
				{
					base.PowerSharpParameters["AttachmentContainsWords"] = value;
				}
			}

			// Token: 0x17004E2C RID: 20012
			// (set) Token: 0x0600746A RID: 29802 RVA: 0x000AEFAB File Offset: 0x000AD1AB
			public virtual Pattern AttachmentMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AttachmentMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E2D RID: 20013
			// (set) Token: 0x0600746B RID: 29803 RVA: 0x000AEFC3 File Offset: 0x000AD1C3
			public virtual bool AttachmentIsUnsupported
			{
				set
				{
					base.PowerSharpParameters["AttachmentIsUnsupported"] = value;
				}
			}

			// Token: 0x17004E2E RID: 20014
			// (set) Token: 0x0600746C RID: 29804 RVA: 0x000AEFDB File Offset: 0x000AD1DB
			public virtual bool AttachmentProcessingLimitExceeded
			{
				set
				{
					base.PowerSharpParameters["AttachmentProcessingLimitExceeded"] = value;
				}
			}

			// Token: 0x17004E2F RID: 20015
			// (set) Token: 0x0600746D RID: 29805 RVA: 0x000AEFF3 File Offset: 0x000AD1F3
			public virtual bool AttachmentHasExecutableContent
			{
				set
				{
					base.PowerSharpParameters["AttachmentHasExecutableContent"] = value;
				}
			}

			// Token: 0x17004E30 RID: 20016
			// (set) Token: 0x0600746E RID: 29806 RVA: 0x000AF00B File Offset: 0x000AD20B
			public virtual bool AttachmentIsPasswordProtected
			{
				set
				{
					base.PowerSharpParameters["AttachmentIsPasswordProtected"] = value;
				}
			}

			// Token: 0x17004E31 RID: 20017
			// (set) Token: 0x0600746F RID: 29807 RVA: 0x000AF023 File Offset: 0x000AD223
			public virtual Word AnyOfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["AnyOfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004E32 RID: 20018
			// (set) Token: 0x06007470 RID: 29808 RVA: 0x000AF03B File Offset: 0x000AD23B
			public virtual Pattern AnyOfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["AnyOfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E33 RID: 20019
			// (set) Token: 0x06007471 RID: 29809 RVA: 0x000AF053 File Offset: 0x000AD253
			public virtual RecipientIdParameter ExceptIfFrom
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFrom"] = value;
				}
			}

			// Token: 0x17004E34 RID: 20020
			// (set) Token: 0x06007472 RID: 29810 RVA: 0x000AF066 File Offset: 0x000AD266
			public virtual bool HasSenderOverride
			{
				set
				{
					base.PowerSharpParameters["HasSenderOverride"] = value;
				}
			}

			// Token: 0x17004E35 RID: 20021
			// (set) Token: 0x06007473 RID: 29811 RVA: 0x000AF07E File Offset: 0x000AD27E
			public virtual Hashtable MessageContainsDataClassifications
			{
				set
				{
					base.PowerSharpParameters["MessageContainsDataClassifications"] = value;
				}
			}

			// Token: 0x17004E36 RID: 20022
			// (set) Token: 0x06007474 RID: 29812 RVA: 0x000AF091 File Offset: 0x000AD291
			public virtual MultiValuedProperty<IPRange> SenderIpRanges
			{
				set
				{
					base.PowerSharpParameters["SenderIpRanges"] = value;
				}
			}

			// Token: 0x17004E37 RID: 20023
			// (set) Token: 0x06007475 RID: 29813 RVA: 0x000AF0A4 File Offset: 0x000AD2A4
			public virtual RecipientIdParameter ExceptIfFromMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromMemberOf"] = value;
				}
			}

			// Token: 0x17004E38 RID: 20024
			// (set) Token: 0x06007476 RID: 29814 RVA: 0x000AF0B7 File Offset: 0x000AD2B7
			public virtual FromUserScope? ExceptIfFromScope
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromScope"] = value;
				}
			}

			// Token: 0x17004E39 RID: 20025
			// (set) Token: 0x06007477 RID: 29815 RVA: 0x000AF0CF File Offset: 0x000AD2CF
			public virtual RecipientIdParameter ExceptIfSentTo
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentTo"] = value;
				}
			}

			// Token: 0x17004E3A RID: 20026
			// (set) Token: 0x06007478 RID: 29816 RVA: 0x000AF0E2 File Offset: 0x000AD2E2
			public virtual RecipientIdParameter ExceptIfSentToMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToMemberOf"] = value;
				}
			}

			// Token: 0x17004E3B RID: 20027
			// (set) Token: 0x06007479 RID: 29817 RVA: 0x000AF0F5 File Offset: 0x000AD2F5
			public virtual ToUserScope? ExceptIfSentToScope
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSentToScope"] = value;
				}
			}

			// Token: 0x17004E3C RID: 20028
			// (set) Token: 0x0600747A RID: 29818 RVA: 0x000AF10D File Offset: 0x000AD30D
			public virtual RecipientIdParameter ExceptIfBetweenMemberOf1
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBetweenMemberOf1"] = value;
				}
			}

			// Token: 0x17004E3D RID: 20029
			// (set) Token: 0x0600747B RID: 29819 RVA: 0x000AF120 File Offset: 0x000AD320
			public virtual RecipientIdParameter ExceptIfBetweenMemberOf2
			{
				set
				{
					base.PowerSharpParameters["ExceptIfBetweenMemberOf2"] = value;
				}
			}

			// Token: 0x17004E3E RID: 20030
			// (set) Token: 0x0600747C RID: 29820 RVA: 0x000AF133 File Offset: 0x000AD333
			public virtual RecipientIdParameter ExceptIfManagerAddresses
			{
				set
				{
					base.PowerSharpParameters["ExceptIfManagerAddresses"] = value;
				}
			}

			// Token: 0x17004E3F RID: 20031
			// (set) Token: 0x0600747D RID: 29821 RVA: 0x000AF146 File Offset: 0x000AD346
			public virtual EvaluatedUser? ExceptIfManagerForEvaluatedUser
			{
				set
				{
					base.PowerSharpParameters["ExceptIfManagerForEvaluatedUser"] = value;
				}
			}

			// Token: 0x17004E40 RID: 20032
			// (set) Token: 0x0600747E RID: 29822 RVA: 0x000AF15E File Offset: 0x000AD35E
			public virtual ManagementRelationship? ExceptIfSenderManagementRelationship
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderManagementRelationship"] = value;
				}
			}

			// Token: 0x17004E41 RID: 20033
			// (set) Token: 0x0600747F RID: 29823 RVA: 0x000AF176 File Offset: 0x000AD376
			public virtual ADAttribute? ExceptIfADComparisonAttribute
			{
				set
				{
					base.PowerSharpParameters["ExceptIfADComparisonAttribute"] = value;
				}
			}

			// Token: 0x17004E42 RID: 20034
			// (set) Token: 0x06007480 RID: 29824 RVA: 0x000AF18E File Offset: 0x000AD38E
			public virtual Evaluation? ExceptIfADComparisonOperator
			{
				set
				{
					base.PowerSharpParameters["ExceptIfADComparisonOperator"] = value;
				}
			}

			// Token: 0x17004E43 RID: 20035
			// (set) Token: 0x06007481 RID: 29825 RVA: 0x000AF1A6 File Offset: 0x000AD3A6
			public virtual Word ExceptIfSenderADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004E44 RID: 20036
			// (set) Token: 0x06007482 RID: 29826 RVA: 0x000AF1BE File Offset: 0x000AD3BE
			public virtual Pattern ExceptIfSenderADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E45 RID: 20037
			// (set) Token: 0x06007483 RID: 29827 RVA: 0x000AF1D6 File Offset: 0x000AD3D6
			public virtual Word ExceptIfRecipientADAttributeContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientADAttributeContainsWords"] = value;
				}
			}

			// Token: 0x17004E46 RID: 20038
			// (set) Token: 0x06007484 RID: 29828 RVA: 0x000AF1EE File Offset: 0x000AD3EE
			public virtual Pattern ExceptIfRecipientADAttributeMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientADAttributeMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E47 RID: 20039
			// (set) Token: 0x06007485 RID: 29829 RVA: 0x000AF206 File Offset: 0x000AD406
			public virtual RecipientIdParameter ExceptIfAnyOfToHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToHeader"] = value;
				}
			}

			// Token: 0x17004E48 RID: 20040
			// (set) Token: 0x06007486 RID: 29830 RVA: 0x000AF219 File Offset: 0x000AD419
			public virtual RecipientIdParameter ExceptIfAnyOfToHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004E49 RID: 20041
			// (set) Token: 0x06007487 RID: 29831 RVA: 0x000AF22C File Offset: 0x000AD42C
			public virtual RecipientIdParameter ExceptIfAnyOfCcHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfCcHeader"] = value;
				}
			}

			// Token: 0x17004E4A RID: 20042
			// (set) Token: 0x06007488 RID: 29832 RVA: 0x000AF23F File Offset: 0x000AD43F
			public virtual RecipientIdParameter ExceptIfAnyOfCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004E4B RID: 20043
			// (set) Token: 0x06007489 RID: 29833 RVA: 0x000AF252 File Offset: 0x000AD452
			public virtual RecipientIdParameter ExceptIfAnyOfToCcHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToCcHeader"] = value;
				}
			}

			// Token: 0x17004E4C RID: 20044
			// (set) Token: 0x0600748A RID: 29834 RVA: 0x000AF265 File Offset: 0x000AD465
			public virtual RecipientIdParameter ExceptIfAnyOfToCcHeaderMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfToCcHeaderMemberOf"] = value;
				}
			}

			// Token: 0x17004E4D RID: 20045
			// (set) Token: 0x0600748B RID: 29835 RVA: 0x000AF278 File Offset: 0x000AD478
			public virtual string ExceptIfHasClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasClassification"] = value;
				}
			}

			// Token: 0x17004E4E RID: 20046
			// (set) Token: 0x0600748C RID: 29836 RVA: 0x000AF28B File Offset: 0x000AD48B
			public virtual bool ExceptIfHasNoClassification
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasNoClassification"] = value;
				}
			}

			// Token: 0x17004E4F RID: 20047
			// (set) Token: 0x0600748D RID: 29837 RVA: 0x000AF2A3 File Offset: 0x000AD4A3
			public virtual Word ExceptIfSubjectContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectContainsWords"] = value;
				}
			}

			// Token: 0x17004E50 RID: 20048
			// (set) Token: 0x0600748E RID: 29838 RVA: 0x000AF2BB File Offset: 0x000AD4BB
			public virtual Word ExceptIfSubjectOrBodyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyContainsWords"] = value;
				}
			}

			// Token: 0x17004E51 RID: 20049
			// (set) Token: 0x0600748F RID: 29839 RVA: 0x000AF2D3 File Offset: 0x000AD4D3
			public virtual HeaderName? ExceptIfHeaderContainsMessageHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsMessageHeader"] = value;
				}
			}

			// Token: 0x17004E52 RID: 20050
			// (set) Token: 0x06007490 RID: 29840 RVA: 0x000AF2EB File Offset: 0x000AD4EB
			public virtual Word ExceptIfHeaderContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderContainsWords"] = value;
				}
			}

			// Token: 0x17004E53 RID: 20051
			// (set) Token: 0x06007491 RID: 29841 RVA: 0x000AF303 File Offset: 0x000AD503
			public virtual Word ExceptIfFromAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004E54 RID: 20052
			// (set) Token: 0x06007492 RID: 29842 RVA: 0x000AF31B File Offset: 0x000AD51B
			public virtual Word ExceptIfSenderDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderDomainIs"] = value;
				}
			}

			// Token: 0x17004E55 RID: 20053
			// (set) Token: 0x06007493 RID: 29843 RVA: 0x000AF333 File Offset: 0x000AD533
			public virtual Word ExceptIfRecipientDomainIs
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientDomainIs"] = value;
				}
			}

			// Token: 0x17004E56 RID: 20054
			// (set) Token: 0x06007494 RID: 29844 RVA: 0x000AF34B File Offset: 0x000AD54B
			public virtual Pattern ExceptIfSubjectMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E57 RID: 20055
			// (set) Token: 0x06007495 RID: 29845 RVA: 0x000AF363 File Offset: 0x000AD563
			public virtual Pattern ExceptIfSubjectOrBodyMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSubjectOrBodyMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E58 RID: 20056
			// (set) Token: 0x06007496 RID: 29846 RVA: 0x000AF37B File Offset: 0x000AD57B
			public virtual HeaderName? ExceptIfHeaderMatchesMessageHeader
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderMatchesMessageHeader"] = value;
				}
			}

			// Token: 0x17004E59 RID: 20057
			// (set) Token: 0x06007497 RID: 29847 RVA: 0x000AF393 File Offset: 0x000AD593
			public virtual Pattern ExceptIfHeaderMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHeaderMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E5A RID: 20058
			// (set) Token: 0x06007498 RID: 29848 RVA: 0x000AF3AB File Offset: 0x000AD5AB
			public virtual Pattern ExceptIfFromAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfFromAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E5B RID: 20059
			// (set) Token: 0x06007499 RID: 29849 RVA: 0x000AF3C3 File Offset: 0x000AD5C3
			public virtual Pattern ExceptIfAttachmentNameMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentNameMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E5C RID: 20060
			// (set) Token: 0x0600749A RID: 29850 RVA: 0x000AF3DB File Offset: 0x000AD5DB
			public virtual Word ExceptIfAttachmentExtensionMatchesWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentExtensionMatchesWords"] = value;
				}
			}

			// Token: 0x17004E5D RID: 20061
			// (set) Token: 0x0600749B RID: 29851 RVA: 0x000AF3F3 File Offset: 0x000AD5F3
			public virtual Word ExceptIfAttachmentPropertyContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentPropertyContainsWords"] = value;
				}
			}

			// Token: 0x17004E5E RID: 20062
			// (set) Token: 0x0600749C RID: 29852 RVA: 0x000AF40B File Offset: 0x000AD60B
			public virtual Word ExceptIfContentCharacterSetContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfContentCharacterSetContainsWords"] = value;
				}
			}

			// Token: 0x17004E5F RID: 20063
			// (set) Token: 0x0600749D RID: 29853 RVA: 0x000AF423 File Offset: 0x000AD623
			public virtual SclValue? ExceptIfSCLOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSCLOver"] = value;
				}
			}

			// Token: 0x17004E60 RID: 20064
			// (set) Token: 0x0600749E RID: 29854 RVA: 0x000AF43B File Offset: 0x000AD63B
			public virtual ByteQuantifiedSize? ExceptIfAttachmentSizeOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentSizeOver"] = value;
				}
			}

			// Token: 0x17004E61 RID: 20065
			// (set) Token: 0x0600749F RID: 29855 RVA: 0x000AF453 File Offset: 0x000AD653
			public virtual ByteQuantifiedSize? ExceptIfMessageSizeOver
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageSizeOver"] = value;
				}
			}

			// Token: 0x17004E62 RID: 20066
			// (set) Token: 0x060074A0 RID: 29856 RVA: 0x000AF46B File Offset: 0x000AD66B
			public virtual Importance? ExceptIfWithImportance
			{
				set
				{
					base.PowerSharpParameters["ExceptIfWithImportance"] = value;
				}
			}

			// Token: 0x17004E63 RID: 20067
			// (set) Token: 0x060074A1 RID: 29857 RVA: 0x000AF483 File Offset: 0x000AD683
			public virtual MessageType? ExceptIfMessageTypeMatches
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageTypeMatches"] = value;
				}
			}

			// Token: 0x17004E64 RID: 20068
			// (set) Token: 0x060074A2 RID: 29858 RVA: 0x000AF49B File Offset: 0x000AD69B
			public virtual Word ExceptIfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004E65 RID: 20069
			// (set) Token: 0x060074A3 RID: 29859 RVA: 0x000AF4B3 File Offset: 0x000AD6B3
			public virtual Pattern ExceptIfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E66 RID: 20070
			// (set) Token: 0x060074A4 RID: 29860 RVA: 0x000AF4CB File Offset: 0x000AD6CB
			public virtual Word ExceptIfSenderInRecipientList
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderInRecipientList"] = value;
				}
			}

			// Token: 0x17004E67 RID: 20071
			// (set) Token: 0x060074A5 RID: 29861 RVA: 0x000AF4E3 File Offset: 0x000AD6E3
			public virtual Word ExceptIfRecipientInSenderList
			{
				set
				{
					base.PowerSharpParameters["ExceptIfRecipientInSenderList"] = value;
				}
			}

			// Token: 0x17004E68 RID: 20072
			// (set) Token: 0x060074A6 RID: 29862 RVA: 0x000AF4FB File Offset: 0x000AD6FB
			public virtual Word ExceptIfAttachmentContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentContainsWords"] = value;
				}
			}

			// Token: 0x17004E69 RID: 20073
			// (set) Token: 0x060074A7 RID: 29863 RVA: 0x000AF513 File Offset: 0x000AD713
			public virtual Pattern ExceptIfAttachmentMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E6A RID: 20074
			// (set) Token: 0x060074A8 RID: 29864 RVA: 0x000AF52B File Offset: 0x000AD72B
			public virtual bool ExceptIfAttachmentIsUnsupported
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentIsUnsupported"] = value;
				}
			}

			// Token: 0x17004E6B RID: 20075
			// (set) Token: 0x060074A9 RID: 29865 RVA: 0x000AF543 File Offset: 0x000AD743
			public virtual bool ExceptIfAttachmentProcessingLimitExceeded
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentProcessingLimitExceeded"] = value;
				}
			}

			// Token: 0x17004E6C RID: 20076
			// (set) Token: 0x060074AA RID: 29866 RVA: 0x000AF55B File Offset: 0x000AD75B
			public virtual bool ExceptIfAttachmentHasExecutableContent
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentHasExecutableContent"] = value;
				}
			}

			// Token: 0x17004E6D RID: 20077
			// (set) Token: 0x060074AB RID: 29867 RVA: 0x000AF573 File Offset: 0x000AD773
			public virtual Word ExceptIfAnyOfRecipientAddressContainsWords
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfRecipientAddressContainsWords"] = value;
				}
			}

			// Token: 0x17004E6E RID: 20078
			// (set) Token: 0x060074AC RID: 29868 RVA: 0x000AF58B File Offset: 0x000AD78B
			public virtual bool ExceptIfAttachmentIsPasswordProtected
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAttachmentIsPasswordProtected"] = value;
				}
			}

			// Token: 0x17004E6F RID: 20079
			// (set) Token: 0x060074AD RID: 29869 RVA: 0x000AF5A3 File Offset: 0x000AD7A3
			public virtual Pattern ExceptIfAnyOfRecipientAddressMatchesPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptIfAnyOfRecipientAddressMatchesPatterns"] = value;
				}
			}

			// Token: 0x17004E70 RID: 20080
			// (set) Token: 0x060074AE RID: 29870 RVA: 0x000AF5BB File Offset: 0x000AD7BB
			public virtual bool ExceptIfHasSenderOverride
			{
				set
				{
					base.PowerSharpParameters["ExceptIfHasSenderOverride"] = value;
				}
			}

			// Token: 0x17004E71 RID: 20081
			// (set) Token: 0x060074AF RID: 29871 RVA: 0x000AF5D3 File Offset: 0x000AD7D3
			public virtual Hashtable ExceptIfMessageContainsDataClassifications
			{
				set
				{
					base.PowerSharpParameters["ExceptIfMessageContainsDataClassifications"] = value;
				}
			}

			// Token: 0x17004E72 RID: 20082
			// (set) Token: 0x060074B0 RID: 29872 RVA: 0x000AF5E6 File Offset: 0x000AD7E6
			public virtual MultiValuedProperty<IPRange> ExceptIfSenderIpRanges
			{
				set
				{
					base.PowerSharpParameters["ExceptIfSenderIpRanges"] = value;
				}
			}

			// Token: 0x17004E73 RID: 20083
			// (set) Token: 0x060074B1 RID: 29873 RVA: 0x000AF5F9 File Offset: 0x000AD7F9
			public virtual SubjectPrefix? PrependSubject
			{
				set
				{
					base.PowerSharpParameters["PrependSubject"] = value;
				}
			}

			// Token: 0x17004E74 RID: 20084
			// (set) Token: 0x060074B2 RID: 29874 RVA: 0x000AF611 File Offset: 0x000AD811
			public virtual string SetAuditSeverity
			{
				set
				{
					base.PowerSharpParameters["SetAuditSeverity"] = value;
				}
			}

			// Token: 0x17004E75 RID: 20085
			// (set) Token: 0x060074B3 RID: 29875 RVA: 0x000AF624 File Offset: 0x000AD824
			public virtual string ApplyClassification
			{
				set
				{
					base.PowerSharpParameters["ApplyClassification"] = value;
				}
			}

			// Token: 0x17004E76 RID: 20086
			// (set) Token: 0x060074B4 RID: 29876 RVA: 0x000AF637 File Offset: 0x000AD837
			public virtual DisclaimerLocation? ApplyHtmlDisclaimerLocation
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerLocation"] = value;
				}
			}

			// Token: 0x17004E77 RID: 20087
			// (set) Token: 0x060074B5 RID: 29877 RVA: 0x000AF64F File Offset: 0x000AD84F
			public virtual DisclaimerText? ApplyHtmlDisclaimerText
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerText"] = value;
				}
			}

			// Token: 0x17004E78 RID: 20088
			// (set) Token: 0x060074B6 RID: 29878 RVA: 0x000AF667 File Offset: 0x000AD867
			public virtual DisclaimerFallbackAction? ApplyHtmlDisclaimerFallbackAction
			{
				set
				{
					base.PowerSharpParameters["ApplyHtmlDisclaimerFallbackAction"] = value;
				}
			}

			// Token: 0x17004E79 RID: 20089
			// (set) Token: 0x060074B7 RID: 29879 RVA: 0x000AF67F File Offset: 0x000AD87F
			public virtual string ApplyRightsProtectionTemplate
			{
				set
				{
					base.PowerSharpParameters["ApplyRightsProtectionTemplate"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17004E7A RID: 20090
			// (set) Token: 0x060074B8 RID: 29880 RVA: 0x000AF69D File Offset: 0x000AD89D
			public virtual SclValue? SetSCL
			{
				set
				{
					base.PowerSharpParameters["SetSCL"] = value;
				}
			}

			// Token: 0x17004E7B RID: 20091
			// (set) Token: 0x060074B9 RID: 29881 RVA: 0x000AF6B5 File Offset: 0x000AD8B5
			public virtual HeaderName? SetHeaderName
			{
				set
				{
					base.PowerSharpParameters["SetHeaderName"] = value;
				}
			}

			// Token: 0x17004E7C RID: 20092
			// (set) Token: 0x060074BA RID: 29882 RVA: 0x000AF6CD File Offset: 0x000AD8CD
			public virtual HeaderValue? SetHeaderValue
			{
				set
				{
					base.PowerSharpParameters["SetHeaderValue"] = value;
				}
			}

			// Token: 0x17004E7D RID: 20093
			// (set) Token: 0x060074BB RID: 29883 RVA: 0x000AF6E5 File Offset: 0x000AD8E5
			public virtual HeaderName? RemoveHeader
			{
				set
				{
					base.PowerSharpParameters["RemoveHeader"] = value;
				}
			}

			// Token: 0x17004E7E RID: 20094
			// (set) Token: 0x060074BC RID: 29884 RVA: 0x000AF6FD File Offset: 0x000AD8FD
			public virtual RecipientIdParameter AddToRecipients
			{
				set
				{
					base.PowerSharpParameters["AddToRecipients"] = value;
				}
			}

			// Token: 0x17004E7F RID: 20095
			// (set) Token: 0x060074BD RID: 29885 RVA: 0x000AF710 File Offset: 0x000AD910
			public virtual RecipientIdParameter CopyTo
			{
				set
				{
					base.PowerSharpParameters["CopyTo"] = value;
				}
			}

			// Token: 0x17004E80 RID: 20096
			// (set) Token: 0x060074BE RID: 29886 RVA: 0x000AF723 File Offset: 0x000AD923
			public virtual RecipientIdParameter BlindCopyTo
			{
				set
				{
					base.PowerSharpParameters["BlindCopyTo"] = value;
				}
			}

			// Token: 0x17004E81 RID: 20097
			// (set) Token: 0x060074BF RID: 29887 RVA: 0x000AF736 File Offset: 0x000AD936
			public virtual AddedRecipientType? AddManagerAsRecipientType
			{
				set
				{
					base.PowerSharpParameters["AddManagerAsRecipientType"] = value;
				}
			}

			// Token: 0x17004E82 RID: 20098
			// (set) Token: 0x060074C0 RID: 29888 RVA: 0x000AF74E File Offset: 0x000AD94E
			public virtual RecipientIdParameter ModerateMessageByUser
			{
				set
				{
					base.PowerSharpParameters["ModerateMessageByUser"] = value;
				}
			}

			// Token: 0x17004E83 RID: 20099
			// (set) Token: 0x060074C1 RID: 29889 RVA: 0x000AF761 File Offset: 0x000AD961
			public virtual bool ModerateMessageByManager
			{
				set
				{
					base.PowerSharpParameters["ModerateMessageByManager"] = value;
				}
			}

			// Token: 0x17004E84 RID: 20100
			// (set) Token: 0x060074C2 RID: 29890 RVA: 0x000AF779 File Offset: 0x000AD979
			public virtual RecipientIdParameter RedirectMessageTo
			{
				set
				{
					base.PowerSharpParameters["RedirectMessageTo"] = value;
				}
			}

			// Token: 0x17004E85 RID: 20101
			// (set) Token: 0x060074C3 RID: 29891 RVA: 0x000AF78C File Offset: 0x000AD98C
			public virtual NotifySenderType? NotifySender
			{
				set
				{
					base.PowerSharpParameters["NotifySender"] = value;
				}
			}

			// Token: 0x17004E86 RID: 20102
			// (set) Token: 0x060074C4 RID: 29892 RVA: 0x000AF7A4 File Offset: 0x000AD9A4
			public virtual RejectEnhancedStatus? RejectMessageEnhancedStatusCode
			{
				set
				{
					base.PowerSharpParameters["RejectMessageEnhancedStatusCode"] = value;
				}
			}

			// Token: 0x17004E87 RID: 20103
			// (set) Token: 0x060074C5 RID: 29893 RVA: 0x000AF7BC File Offset: 0x000AD9BC
			public virtual DsnText? RejectMessageReasonText
			{
				set
				{
					base.PowerSharpParameters["RejectMessageReasonText"] = value;
				}
			}

			// Token: 0x17004E88 RID: 20104
			// (set) Token: 0x060074C6 RID: 29894 RVA: 0x000AF7D4 File Offset: 0x000AD9D4
			public virtual bool DeleteMessage
			{
				set
				{
					base.PowerSharpParameters["DeleteMessage"] = value;
				}
			}

			// Token: 0x17004E89 RID: 20105
			// (set) Token: 0x060074C7 RID: 29895 RVA: 0x000AF7EC File Offset: 0x000AD9EC
			public virtual bool Disconnect
			{
				set
				{
					base.PowerSharpParameters["Disconnect"] = value;
				}
			}

			// Token: 0x17004E8A RID: 20106
			// (set) Token: 0x060074C8 RID: 29896 RVA: 0x000AF804 File Offset: 0x000ADA04
			public virtual bool Quarantine
			{
				set
				{
					base.PowerSharpParameters["Quarantine"] = value;
				}
			}

			// Token: 0x17004E8B RID: 20107
			// (set) Token: 0x060074C9 RID: 29897 RVA: 0x000AF81C File Offset: 0x000ADA1C
			public virtual RejectText? SmtpRejectMessageRejectText
			{
				set
				{
					base.PowerSharpParameters["SmtpRejectMessageRejectText"] = value;
				}
			}

			// Token: 0x17004E8C RID: 20108
			// (set) Token: 0x060074CA RID: 29898 RVA: 0x000AF834 File Offset: 0x000ADA34
			public virtual RejectStatusCode? SmtpRejectMessageRejectStatusCode
			{
				set
				{
					base.PowerSharpParameters["SmtpRejectMessageRejectStatusCode"] = value;
				}
			}

			// Token: 0x17004E8D RID: 20109
			// (set) Token: 0x060074CB RID: 29899 RVA: 0x000AF84C File Offset: 0x000ADA4C
			public virtual DateTime? ActivationDate
			{
				set
				{
					base.PowerSharpParameters["ActivationDate"] = value;
				}
			}

			// Token: 0x17004E8E RID: 20110
			// (set) Token: 0x060074CC RID: 29900 RVA: 0x000AF864 File Offset: 0x000ADA64
			public virtual DateTime? ExpiryDate
			{
				set
				{
					base.PowerSharpParameters["ExpiryDate"] = value;
				}
			}

			// Token: 0x17004E8F RID: 20111
			// (set) Token: 0x060074CD RID: 29901 RVA: 0x000AF87C File Offset: 0x000ADA7C
			public virtual RuleSubType RuleSubType
			{
				set
				{
					base.PowerSharpParameters["RuleSubType"] = value;
				}
			}

			// Token: 0x17004E90 RID: 20112
			// (set) Token: 0x060074CE RID: 29902 RVA: 0x000AF894 File Offset: 0x000ADA94
			public virtual RuleErrorAction RuleErrorAction
			{
				set
				{
					base.PowerSharpParameters["RuleErrorAction"] = value;
				}
			}

			// Token: 0x17004E91 RID: 20113
			// (set) Token: 0x060074CF RID: 29903 RVA: 0x000AF8AC File Offset: 0x000ADAAC
			public virtual SenderAddressLocation SenderAddressLocation
			{
				set
				{
					base.PowerSharpParameters["SenderAddressLocation"] = value;
				}
			}

			// Token: 0x17004E92 RID: 20114
			// (set) Token: 0x060074D0 RID: 29904 RVA: 0x000AF8C4 File Offset: 0x000ADAC4
			public virtual EventLogText? LogEventText
			{
				set
				{
					base.PowerSharpParameters["LogEventText"] = value;
				}
			}

			// Token: 0x17004E93 RID: 20115
			// (set) Token: 0x060074D1 RID: 29905 RVA: 0x000AF8DC File Offset: 0x000ADADC
			public virtual bool StopRuleProcessing
			{
				set
				{
					base.PowerSharpParameters["StopRuleProcessing"] = value;
				}
			}

			// Token: 0x17004E94 RID: 20116
			// (set) Token: 0x060074D2 RID: 29906 RVA: 0x000AF8F4 File Offset: 0x000ADAF4
			public virtual string GenerateIncidentReport
			{
				set
				{
					base.PowerSharpParameters["GenerateIncidentReport"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17004E95 RID: 20117
			// (set) Token: 0x060074D3 RID: 29907 RVA: 0x000AF912 File Offset: 0x000ADB12
			public virtual IncidentReportOriginalMail? IncidentReportOriginalMail
			{
				set
				{
					base.PowerSharpParameters["IncidentReportOriginalMail"] = value;
				}
			}

			// Token: 0x17004E96 RID: 20118
			// (set) Token: 0x060074D4 RID: 29908 RVA: 0x000AF92A File Offset: 0x000ADB2A
			public virtual IncidentReportContent IncidentReportContent
			{
				set
				{
					base.PowerSharpParameters["IncidentReportContent"] = value;
				}
			}

			// Token: 0x17004E97 RID: 20119
			// (set) Token: 0x060074D5 RID: 29909 RVA: 0x000AF942 File Offset: 0x000ADB42
			public virtual DisclaimerText? GenerateNotification
			{
				set
				{
					base.PowerSharpParameters["GenerateNotification"] = value;
				}
			}

			// Token: 0x17004E98 RID: 20120
			// (set) Token: 0x060074D6 RID: 29910 RVA: 0x000AF95A File Offset: 0x000ADB5A
			public virtual OutboundConnectorIdParameter RouteMessageOutboundConnector
			{
				set
				{
					base.PowerSharpParameters["RouteMessageOutboundConnector"] = value;
				}
			}

			// Token: 0x17004E99 RID: 20121
			// (set) Token: 0x060074D7 RID: 29911 RVA: 0x000AF96D File Offset: 0x000ADB6D
			public virtual bool RouteMessageOutboundRequireTls
			{
				set
				{
					base.PowerSharpParameters["RouteMessageOutboundRequireTls"] = value;
				}
			}

			// Token: 0x17004E9A RID: 20122
			// (set) Token: 0x060074D8 RID: 29912 RVA: 0x000AF985 File Offset: 0x000ADB85
			public virtual bool ApplyOME
			{
				set
				{
					base.PowerSharpParameters["ApplyOME"] = value;
				}
			}

			// Token: 0x17004E9B RID: 20123
			// (set) Token: 0x060074D9 RID: 29913 RVA: 0x000AF99D File Offset: 0x000ADB9D
			public virtual bool RemoveOME
			{
				set
				{
					base.PowerSharpParameters["RemoveOME"] = value;
				}
			}

			// Token: 0x17004E9C RID: 20124
			// (set) Token: 0x060074DA RID: 29914 RVA: 0x000AF9B5 File Offset: 0x000ADBB5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004E9D RID: 20125
			// (set) Token: 0x060074DB RID: 29915 RVA: 0x000AF9C8 File Offset: 0x000ADBC8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004E9E RID: 20126
			// (set) Token: 0x060074DC RID: 29916 RVA: 0x000AF9E0 File Offset: 0x000ADBE0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004E9F RID: 20127
			// (set) Token: 0x060074DD RID: 29917 RVA: 0x000AF9F8 File Offset: 0x000ADBF8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004EA0 RID: 20128
			// (set) Token: 0x060074DE RID: 29918 RVA: 0x000AFA10 File Offset: 0x000ADC10
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004EA1 RID: 20129
			// (set) Token: 0x060074DF RID: 29919 RVA: 0x000AFA28 File Offset: 0x000ADC28
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
