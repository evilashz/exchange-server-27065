using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Management.RightsManagement;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B70 RID: 2928
	[Cmdlet("Set", "TransportRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetTransportRule : SetSystemConfigurationObjectTask<RuleIdParameter, Rule, TransportRule>
	{
		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x001BD2DC File Offset: 0x001BB4DC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTransportRule(this.Identity.ToString());
			}
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x001BD2EE File Offset: 0x001BB4EE
		public SetTransportRule()
		{
			this.ruleCollectionName = Utils.RuleCollectionNameFromRole();
			this.supportedPredicates = TransportRulePredicate.GetAvailablePredicateMappings();
			this.supportedActions = TransportRuleAction.GetAvailableActionMappings();
			this.Priority = 0;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x001BD329 File Offset: 0x001BB529
		// (set) Token: 0x06006CC6 RID: 27846 RVA: 0x001BD340 File Offset: 0x001BB540
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x06006CC7 RID: 27847 RVA: 0x001BD353 File Offset: 0x001BB553
		// (set) Token: 0x06006CC8 RID: 27848 RVA: 0x001BD36A File Offset: 0x001BB56A
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)base.Fields["Priority"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Strings.NegativePriority);
				}
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x06006CC9 RID: 27849 RVA: 0x001BD396 File Offset: 0x001BB596
		// (set) Token: 0x06006CCA RID: 27850 RVA: 0x001BD3AD File Offset: 0x001BB5AD
		[Parameter(Mandatory = false)]
		public RuleMode Mode
		{
			get
			{
				return (RuleMode)base.Fields["Mode"];
			}
			set
			{
				base.Fields["Mode"] = value;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x06006CCB RID: 27851 RVA: 0x001BD3C5 File Offset: 0x001BB5C5
		// (set) Token: 0x06006CCC RID: 27852 RVA: 0x001BD3DC File Offset: 0x001BB5DC
		[Parameter(Mandatory = false)]
		public string Comments
		{
			get
			{
				return (string)base.Fields["Comments"];
			}
			set
			{
				base.Fields["Comments"] = value;
			}
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x06006CCD RID: 27853 RVA: 0x001BD3EF File Offset: 0x001BB5EF
		// (set) Token: 0x06006CCE RID: 27854 RVA: 0x001BD406 File Offset: 0x001BB606
		[Parameter(Mandatory = false)]
		public string DlpPolicy
		{
			get
			{
				return (string)base.Fields["DlpPolicy"];
			}
			set
			{
				base.Fields["DlpPolicy"] = value;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x06006CCF RID: 27855 RVA: 0x001BD419 File Offset: 0x001BB619
		// (set) Token: 0x06006CD0 RID: 27856 RVA: 0x001BD430 File Offset: 0x001BB630
		public TransportRulePredicate[] Conditions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Conditions"];
			}
			set
			{
				base.Fields["Conditions"] = value;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x001BD443 File Offset: 0x001BB643
		// (set) Token: 0x06006CD2 RID: 27858 RVA: 0x001BD45A File Offset: 0x001BB65A
		public TransportRulePredicate[] Exceptions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Exceptions"];
			}
			set
			{
				base.Fields["Exceptions"] = value;
			}
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x06006CD3 RID: 27859 RVA: 0x001BD46D File Offset: 0x001BB66D
		// (set) Token: 0x06006CD4 RID: 27860 RVA: 0x001BD484 File Offset: 0x001BB684
		public TransportRuleAction[] Actions
		{
			get
			{
				return (TransportRuleAction[])base.Fields["Actions"];
			}
			set
			{
				base.Fields["Actions"] = value;
			}
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x06006CD5 RID: 27861 RVA: 0x001BD497 File Offset: 0x001BB697
		// (set) Token: 0x06006CD6 RID: 27862 RVA: 0x001BD4AE File Offset: 0x001BB6AE
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] From
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["From"];
			}
			set
			{
				base.Fields["From"] = value;
			}
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x06006CD7 RID: 27863 RVA: 0x001BD4C1 File Offset: 0x001BB6C1
		// (set) Token: 0x06006CD8 RID: 27864 RVA: 0x001BD4D8 File Offset: 0x001BB6D8
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] FromMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["FromMemberOf"];
			}
			set
			{
				base.Fields["FromMemberOf"] = value;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x001BD4EB File Offset: 0x001BB6EB
		// (set) Token: 0x06006CDA RID: 27866 RVA: 0x001BD502 File Offset: 0x001BB702
		[Parameter(Mandatory = false)]
		public FromUserScope? FromScope
		{
			get
			{
				return (FromUserScope?)base.Fields["FromScope"];
			}
			set
			{
				base.Fields["FromScope"] = value;
			}
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x06006CDB RID: 27867 RVA: 0x001BD51A File Offset: 0x001BB71A
		// (set) Token: 0x06006CDC RID: 27868 RVA: 0x001BD531 File Offset: 0x001BB731
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SentTo"];
			}
			set
			{
				base.Fields["SentTo"] = value;
			}
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x06006CDD RID: 27869 RVA: 0x001BD544 File Offset: 0x001BB744
		// (set) Token: 0x06006CDE RID: 27870 RVA: 0x001BD55B File Offset: 0x001BB75B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentToMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SentToMemberOf"];
			}
			set
			{
				base.Fields["SentToMemberOf"] = value;
			}
		}

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x06006CDF RID: 27871 RVA: 0x001BD56E File Offset: 0x001BB76E
		// (set) Token: 0x06006CE0 RID: 27872 RVA: 0x001BD585 File Offset: 0x001BB785
		[Parameter(Mandatory = false)]
		public ToUserScope? SentToScope
		{
			get
			{
				return (ToUserScope?)base.Fields["SentToScope"];
			}
			set
			{
				base.Fields["SentToScope"] = value;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x06006CE1 RID: 27873 RVA: 0x001BD59D File Offset: 0x001BB79D
		// (set) Token: 0x06006CE2 RID: 27874 RVA: 0x001BD5B4 File Offset: 0x001BB7B4
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BetweenMemberOf1
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BetweenMemberOf1"];
			}
			set
			{
				base.Fields["BetweenMemberOf1"] = value;
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x06006CE3 RID: 27875 RVA: 0x001BD5C7 File Offset: 0x001BB7C7
		// (set) Token: 0x06006CE4 RID: 27876 RVA: 0x001BD5DE File Offset: 0x001BB7DE
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BetweenMemberOf2
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BetweenMemberOf2"];
			}
			set
			{
				base.Fields["BetweenMemberOf2"] = value;
			}
		}

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x06006CE5 RID: 27877 RVA: 0x001BD5F1 File Offset: 0x001BB7F1
		// (set) Token: 0x06006CE6 RID: 27878 RVA: 0x001BD608 File Offset: 0x001BB808
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ManagerAddresses
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ManagerAddresses"];
			}
			set
			{
				base.Fields["ManagerAddresses"] = value;
			}
		}

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x001BD61B File Offset: 0x001BB81B
		// (set) Token: 0x06006CE8 RID: 27880 RVA: 0x001BD632 File Offset: 0x001BB832
		[Parameter(Mandatory = false)]
		public EvaluatedUser? ManagerForEvaluatedUser
		{
			get
			{
				return (EvaluatedUser?)base.Fields["ManagerForEvaluatedUser"];
			}
			set
			{
				base.Fields["ManagerForEvaluatedUser"] = value;
			}
		}

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x001BD64A File Offset: 0x001BB84A
		// (set) Token: 0x06006CEA RID: 27882 RVA: 0x001BD661 File Offset: 0x001BB861
		[Parameter(Mandatory = false)]
		public ManagementRelationship? SenderManagementRelationship
		{
			get
			{
				return (ManagementRelationship?)base.Fields["SenderManagementRelationship"];
			}
			set
			{
				base.Fields["SenderManagementRelationship"] = value;
			}
		}

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x06006CEB RID: 27883 RVA: 0x001BD679 File Offset: 0x001BB879
		// (set) Token: 0x06006CEC RID: 27884 RVA: 0x001BD690 File Offset: 0x001BB890
		[Parameter(Mandatory = false)]
		public ADAttribute? ADComparisonAttribute
		{
			get
			{
				return (ADAttribute?)base.Fields["ADComparisonAttribute"];
			}
			set
			{
				base.Fields["ADComparisonAttribute"] = value;
			}
		}

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x06006CED RID: 27885 RVA: 0x001BD6A8 File Offset: 0x001BB8A8
		// (set) Token: 0x06006CEE RID: 27886 RVA: 0x001BD6BF File Offset: 0x001BB8BF
		[Parameter(Mandatory = false)]
		public Evaluation? ADComparisonOperator
		{
			get
			{
				return (Evaluation?)base.Fields["ADComparisonOperator"];
			}
			set
			{
				base.Fields["ADComparisonOperator"] = value;
			}
		}

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x06006CEF RID: 27887 RVA: 0x001BD6D7 File Offset: 0x001BB8D7
		// (set) Token: 0x06006CF0 RID: 27888 RVA: 0x001BD6EE File Offset: 0x001BB8EE
		[Parameter(Mandatory = false)]
		public Word[] SenderADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["SenderADAttributeContainsWords"];
			}
			set
			{
				base.Fields["SenderADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x06006CF1 RID: 27889 RVA: 0x001BD701 File Offset: 0x001BB901
		// (set) Token: 0x06006CF2 RID: 27890 RVA: 0x001BD718 File Offset: 0x001BB918
		[Parameter(Mandatory = false)]
		public Pattern[] SenderADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["SenderADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["SenderADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x06006CF3 RID: 27891 RVA: 0x001BD72B File Offset: 0x001BB92B
		// (set) Token: 0x06006CF4 RID: 27892 RVA: 0x001BD742 File Offset: 0x001BB942
		[Parameter(Mandatory = false)]
		public Word[] RecipientADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["RecipientADAttributeContainsWords"];
			}
			set
			{
				base.Fields["RecipientADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x06006CF5 RID: 27893 RVA: 0x001BD755 File Offset: 0x001BB955
		// (set) Token: 0x06006CF6 RID: 27894 RVA: 0x001BD76C File Offset: 0x001BB96C
		[Parameter(Mandatory = false)]
		public Pattern[] RecipientADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["RecipientADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["RecipientADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x06006CF7 RID: 27895 RVA: 0x001BD77F File Offset: 0x001BB97F
		// (set) Token: 0x06006CF8 RID: 27896 RVA: 0x001BD796 File Offset: 0x001BB996
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToHeader"];
			}
			set
			{
				base.Fields["AnyOfToHeader"] = value;
			}
		}

		// Token: 0x17002219 RID: 8729
		// (get) Token: 0x06006CF9 RID: 27897 RVA: 0x001BD7A9 File Offset: 0x001BB9A9
		// (set) Token: 0x06006CFA RID: 27898 RVA: 0x001BD7C0 File Offset: 0x001BB9C0
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToHeaderMemberOf"];
			}
			set
			{
				base.Fields["AnyOfToHeaderMemberOf"] = value;
			}
		}

		// Token: 0x1700221A RID: 8730
		// (get) Token: 0x06006CFB RID: 27899 RVA: 0x001BD7D3 File Offset: 0x001BB9D3
		// (set) Token: 0x06006CFC RID: 27900 RVA: 0x001BD7EA File Offset: 0x001BB9EA
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfCcHeader"];
			}
			set
			{
				base.Fields["AnyOfCcHeader"] = value;
			}
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x06006CFD RID: 27901 RVA: 0x001BD7FD File Offset: 0x001BB9FD
		// (set) Token: 0x06006CFE RID: 27902 RVA: 0x001BD814 File Offset: 0x001BBA14
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["AnyOfCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x06006CFF RID: 27903 RVA: 0x001BD827 File Offset: 0x001BBA27
		// (set) Token: 0x06006D00 RID: 27904 RVA: 0x001BD83E File Offset: 0x001BBA3E
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToCcHeader"];
			}
			set
			{
				base.Fields["AnyOfToCcHeader"] = value;
			}
		}

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x06006D01 RID: 27905 RVA: 0x001BD851 File Offset: 0x001BBA51
		// (set) Token: 0x06006D02 RID: 27906 RVA: 0x001BD868 File Offset: 0x001BBA68
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["AnyOfToCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x06006D03 RID: 27907 RVA: 0x001BD87B File Offset: 0x001BBA7B
		// (set) Token: 0x06006D04 RID: 27908 RVA: 0x001BD892 File Offset: 0x001BBA92
		[Parameter(Mandatory = false)]
		public string HasClassification
		{
			get
			{
				return (string)base.Fields["HasClassification"];
			}
			set
			{
				base.Fields["HasClassification"] = value;
			}
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x06006D05 RID: 27909 RVA: 0x001BD8A5 File Offset: 0x001BBAA5
		// (set) Token: 0x06006D06 RID: 27910 RVA: 0x001BD8BC File Offset: 0x001BBABC
		[Parameter(Mandatory = false)]
		public bool HasNoClassification
		{
			get
			{
				return (bool)base.Fields["HasNoClassification"];
			}
			set
			{
				base.Fields["HasNoClassification"] = value;
			}
		}

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x06006D07 RID: 27911 RVA: 0x001BD8D4 File Offset: 0x001BBAD4
		// (set) Token: 0x06006D08 RID: 27912 RVA: 0x001BD8EB File Offset: 0x001BBAEB
		[Parameter(Mandatory = false)]
		public Word[] SubjectContainsWords
		{
			get
			{
				return (Word[])base.Fields["SubjectContainsWords"];
			}
			set
			{
				base.Fields["SubjectContainsWords"] = value;
			}
		}

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x06006D09 RID: 27913 RVA: 0x001BD8FE File Offset: 0x001BBAFE
		// (set) Token: 0x06006D0A RID: 27914 RVA: 0x001BD915 File Offset: 0x001BBB15
		[Parameter(Mandatory = false)]
		public Word[] SubjectOrBodyContainsWords
		{
			get
			{
				return (Word[])base.Fields["SubjectOrBodyContainsWords"];
			}
			set
			{
				base.Fields["SubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x06006D0B RID: 27915 RVA: 0x001BD928 File Offset: 0x001BBB28
		// (set) Token: 0x06006D0C RID: 27916 RVA: 0x001BD93F File Offset: 0x001BBB3F
		[Parameter(Mandatory = false)]
		public HeaderName? HeaderContainsMessageHeader
		{
			get
			{
				return (HeaderName?)base.Fields["HeaderContainsMessageHeader"];
			}
			set
			{
				base.Fields["HeaderContainsMessageHeader"] = value;
			}
		}

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x06006D0D RID: 27917 RVA: 0x001BD957 File Offset: 0x001BBB57
		// (set) Token: 0x06006D0E RID: 27918 RVA: 0x001BD96E File Offset: 0x001BBB6E
		[Parameter(Mandatory = false)]
		public Word[] HeaderContainsWords
		{
			get
			{
				return (Word[])base.Fields["HeaderContainsWords"];
			}
			set
			{
				base.Fields["HeaderContainsWords"] = value;
			}
		}

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x06006D0F RID: 27919 RVA: 0x001BD981 File Offset: 0x001BBB81
		// (set) Token: 0x06006D10 RID: 27920 RVA: 0x001BD998 File Offset: 0x001BBB98
		[Parameter(Mandatory = false)]
		public Word[] FromAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["FromAddressContainsWords"];
			}
			set
			{
				base.Fields["FromAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x06006D11 RID: 27921 RVA: 0x001BD9AB File Offset: 0x001BBBAB
		// (set) Token: 0x06006D12 RID: 27922 RVA: 0x001BD9C2 File Offset: 0x001BBBC2
		[Parameter(Mandatory = false)]
		public Word[] SenderDomainIs
		{
			get
			{
				return (Word[])base.Fields["SenderDomainIs"];
			}
			set
			{
				base.Fields["SenderDomainIs"] = value;
			}
		}

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x06006D13 RID: 27923 RVA: 0x001BD9D5 File Offset: 0x001BBBD5
		// (set) Token: 0x06006D14 RID: 27924 RVA: 0x001BD9EC File Offset: 0x001BBBEC
		[Parameter(Mandatory = false)]
		public Word[] RecipientDomainIs
		{
			get
			{
				return (Word[])base.Fields["RecipientDomainIs"];
			}
			set
			{
				base.Fields["RecipientDomainIs"] = value;
			}
		}

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x06006D15 RID: 27925 RVA: 0x001BD9FF File Offset: 0x001BBBFF
		// (set) Token: 0x06006D16 RID: 27926 RVA: 0x001BDA16 File Offset: 0x001BBC16
		[Parameter(Mandatory = false)]
		public Pattern[] SubjectMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["SubjectMatchesPatterns"];
			}
			set
			{
				base.Fields["SubjectMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x06006D17 RID: 27927 RVA: 0x001BDA29 File Offset: 0x001BBC29
		// (set) Token: 0x06006D18 RID: 27928 RVA: 0x001BDA40 File Offset: 0x001BBC40
		[Parameter(Mandatory = false)]
		public Pattern[] SubjectOrBodyMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["SubjectOrBodyMatchesPatterns"];
			}
			set
			{
				base.Fields["SubjectOrBodyMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x06006D19 RID: 27929 RVA: 0x001BDA53 File Offset: 0x001BBC53
		// (set) Token: 0x06006D1A RID: 27930 RVA: 0x001BDA6A File Offset: 0x001BBC6A
		[Parameter(Mandatory = false)]
		public HeaderName? HeaderMatchesMessageHeader
		{
			get
			{
				return (HeaderName?)base.Fields["HeaderMatchesMessageHeader"];
			}
			set
			{
				base.Fields["HeaderMatchesMessageHeader"] = value;
			}
		}

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x06006D1B RID: 27931 RVA: 0x001BDA82 File Offset: 0x001BBC82
		// (set) Token: 0x06006D1C RID: 27932 RVA: 0x001BDA99 File Offset: 0x001BBC99
		[Parameter(Mandatory = false)]
		public Pattern[] HeaderMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["HeaderMatchesPatterns"];
			}
			set
			{
				base.Fields["HeaderMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x06006D1D RID: 27933 RVA: 0x001BDAAC File Offset: 0x001BBCAC
		// (set) Token: 0x06006D1E RID: 27934 RVA: 0x001BDAC3 File Offset: 0x001BBCC3
		[Parameter(Mandatory = false)]
		public Pattern[] FromAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["FromAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["FromAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x06006D1F RID: 27935 RVA: 0x001BDAD6 File Offset: 0x001BBCD6
		// (set) Token: 0x06006D20 RID: 27936 RVA: 0x001BDAED File Offset: 0x001BBCED
		[Parameter(Mandatory = false)]
		public Pattern[] AttachmentNameMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["AttachmentNameMatchesPatterns"];
			}
			set
			{
				base.Fields["AttachmentNameMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06006D21 RID: 27937 RVA: 0x001BDB00 File Offset: 0x001BBD00
		// (set) Token: 0x06006D22 RID: 27938 RVA: 0x001BDB17 File Offset: 0x001BBD17
		[Parameter(Mandatory = false)]
		public Word[] AttachmentExtensionMatchesWords
		{
			get
			{
				return (Word[])base.Fields["AttachmentExtensionMatchesWords"];
			}
			set
			{
				base.Fields["AttachmentExtensionMatchesWords"] = value;
			}
		}

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06006D23 RID: 27939 RVA: 0x001BDB2A File Offset: 0x001BBD2A
		// (set) Token: 0x06006D24 RID: 27940 RVA: 0x001BDB41 File Offset: 0x001BBD41
		[Parameter(Mandatory = false)]
		public Word[] AttachmentPropertyContainsWords
		{
			get
			{
				return (Word[])base.Fields["AttachmentPropertyContainsWords"];
			}
			set
			{
				base.Fields["AttachmentPropertyContainsWords"] = value;
			}
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x06006D25 RID: 27941 RVA: 0x001BDB54 File Offset: 0x001BBD54
		// (set) Token: 0x06006D26 RID: 27942 RVA: 0x001BDB6B File Offset: 0x001BBD6B
		[Parameter(Mandatory = false)]
		public Word[] ContentCharacterSetContainsWords
		{
			get
			{
				return (Word[])base.Fields["ContentCharacterSetContainsWords"];
			}
			set
			{
				base.Fields["ContentCharacterSetContainsWords"] = value;
			}
		}

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x06006D27 RID: 27943 RVA: 0x001BDB7E File Offset: 0x001BBD7E
		// (set) Token: 0x06006D28 RID: 27944 RVA: 0x001BDB95 File Offset: 0x001BBD95
		[Parameter(Mandatory = false)]
		public SclValue? SCLOver
		{
			get
			{
				return (SclValue?)base.Fields["SCLOver"];
			}
			set
			{
				base.Fields["SCLOver"] = value;
			}
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x06006D29 RID: 27945 RVA: 0x001BDBAD File Offset: 0x001BBDAD
		// (set) Token: 0x06006D2A RID: 27946 RVA: 0x001BDBC4 File Offset: 0x001BBDC4
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? AttachmentSizeOver
		{
			get
			{
				return (ByteQuantifiedSize?)base.Fields["AttachmentSizeOver"];
			}
			set
			{
				base.Fields["AttachmentSizeOver"] = value;
			}
		}

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x06006D2B RID: 27947 RVA: 0x001BDBDC File Offset: 0x001BBDDC
		// (set) Token: 0x06006D2C RID: 27948 RVA: 0x001BDBF8 File Offset: 0x001BBDF8
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? MessageSizeOver
		{
			get
			{
				return new ByteQuantifiedSize?((ByteQuantifiedSize)base.Fields["MessageSizeOver"]);
			}
			set
			{
				base.Fields["MessageSizeOver"] = value;
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x06006D2D RID: 27949 RVA: 0x001BDC10 File Offset: 0x001BBE10
		// (set) Token: 0x06006D2E RID: 27950 RVA: 0x001BDC27 File Offset: 0x001BBE27
		[Parameter(Mandatory = false)]
		public Importance? WithImportance
		{
			get
			{
				return (Importance?)base.Fields["WithImportance"];
			}
			set
			{
				base.Fields["WithImportance"] = value;
			}
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x06006D2F RID: 27951 RVA: 0x001BDC3F File Offset: 0x001BBE3F
		// (set) Token: 0x06006D30 RID: 27952 RVA: 0x001BDC56 File Offset: 0x001BBE56
		[Parameter(Mandatory = false)]
		public MessageType? MessageTypeMatches
		{
			get
			{
				return (MessageType?)base.Fields["MessageTypeMatches"];
			}
			set
			{
				base.Fields["MessageTypeMatches"] = value;
			}
		}

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x06006D31 RID: 27953 RVA: 0x001BDC6E File Offset: 0x001BBE6E
		// (set) Token: 0x06006D32 RID: 27954 RVA: 0x001BDC85 File Offset: 0x001BBE85
		[Parameter(Mandatory = false)]
		public Word[] RecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["RecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["RecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x06006D33 RID: 27955 RVA: 0x001BDC98 File Offset: 0x001BBE98
		// (set) Token: 0x06006D34 RID: 27956 RVA: 0x001BDCAF File Offset: 0x001BBEAF
		[Parameter(Mandatory = false)]
		public Pattern[] RecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["RecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["RecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06006D35 RID: 27957 RVA: 0x001BDCC2 File Offset: 0x001BBEC2
		// (set) Token: 0x06006D36 RID: 27958 RVA: 0x001BDCD9 File Offset: 0x001BBED9
		[Parameter(Mandatory = false)]
		public Word[] SenderInRecipientList
		{
			get
			{
				return (Word[])base.Fields["SenderInRecipientList"];
			}
			set
			{
				base.Fields["SenderInRecipientList"] = value;
			}
		}

		// Token: 0x17002238 RID: 8760
		// (get) Token: 0x06006D37 RID: 27959 RVA: 0x001BDCEC File Offset: 0x001BBEEC
		// (set) Token: 0x06006D38 RID: 27960 RVA: 0x001BDD03 File Offset: 0x001BBF03
		[Parameter(Mandatory = false)]
		public Word[] RecipientInSenderList
		{
			get
			{
				return (Word[])base.Fields["RecipientInSenderList"];
			}
			set
			{
				base.Fields["RecipientInSenderList"] = value;
			}
		}

		// Token: 0x17002239 RID: 8761
		// (get) Token: 0x06006D39 RID: 27961 RVA: 0x001BDD16 File Offset: 0x001BBF16
		// (set) Token: 0x06006D3A RID: 27962 RVA: 0x001BDD2D File Offset: 0x001BBF2D
		[Parameter(Mandatory = false)]
		public Word[] AttachmentContainsWords
		{
			get
			{
				return (Word[])base.Fields["AttachmentContainsWords"];
			}
			set
			{
				base.Fields["AttachmentContainsWords"] = value;
			}
		}

		// Token: 0x1700223A RID: 8762
		// (get) Token: 0x06006D3B RID: 27963 RVA: 0x001BDD40 File Offset: 0x001BBF40
		// (set) Token: 0x06006D3C RID: 27964 RVA: 0x001BDD57 File Offset: 0x001BBF57
		[Parameter(Mandatory = false)]
		public Pattern[] AttachmentMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["AttachmentMatchesPatterns"];
			}
			set
			{
				base.Fields["AttachmentMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700223B RID: 8763
		// (get) Token: 0x06006D3D RID: 27965 RVA: 0x001BDD6A File Offset: 0x001BBF6A
		// (set) Token: 0x06006D3E RID: 27966 RVA: 0x001BDD81 File Offset: 0x001BBF81
		[Parameter(Mandatory = false)]
		public bool AttachmentIsUnsupported
		{
			get
			{
				return (bool)base.Fields["AttachmentIsUnsupported"];
			}
			set
			{
				base.Fields["AttachmentIsUnsupported"] = value;
			}
		}

		// Token: 0x1700223C RID: 8764
		// (get) Token: 0x06006D3F RID: 27967 RVA: 0x001BDD99 File Offset: 0x001BBF99
		// (set) Token: 0x06006D40 RID: 27968 RVA: 0x001BDDB0 File Offset: 0x001BBFB0
		[Parameter(Mandatory = false)]
		public bool AttachmentProcessingLimitExceeded
		{
			get
			{
				return (bool)base.Fields["AttachmentProcessingLimitExceeded"];
			}
			set
			{
				base.Fields["AttachmentProcessingLimitExceeded"] = value;
			}
		}

		// Token: 0x1700223D RID: 8765
		// (get) Token: 0x06006D41 RID: 27969 RVA: 0x001BDDC8 File Offset: 0x001BBFC8
		// (set) Token: 0x06006D42 RID: 27970 RVA: 0x001BDDDF File Offset: 0x001BBFDF
		[Parameter(Mandatory = false)]
		public bool AttachmentHasExecutableContent
		{
			get
			{
				return (bool)base.Fields["AttachmentHasExecutableContent"];
			}
			set
			{
				base.Fields["AttachmentHasExecutableContent"] = value;
			}
		}

		// Token: 0x1700223E RID: 8766
		// (get) Token: 0x06006D43 RID: 27971 RVA: 0x001BDDF7 File Offset: 0x001BBFF7
		// (set) Token: 0x06006D44 RID: 27972 RVA: 0x001BDE0E File Offset: 0x001BC00E
		[Parameter(Mandatory = false)]
		public bool AttachmentIsPasswordProtected
		{
			get
			{
				return (bool)base.Fields["AttachmentIsPasswordProtected"];
			}
			set
			{
				base.Fields["AttachmentIsPasswordProtected"] = value;
			}
		}

		// Token: 0x1700223F RID: 8767
		// (get) Token: 0x06006D45 RID: 27973 RVA: 0x001BDE26 File Offset: 0x001BC026
		// (set) Token: 0x06006D46 RID: 27974 RVA: 0x001BDE3D File Offset: 0x001BC03D
		[Parameter(Mandatory = false)]
		public Word[] AnyOfRecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["AnyOfRecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["AnyOfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x06006D47 RID: 27975 RVA: 0x001BDE50 File Offset: 0x001BC050
		// (set) Token: 0x06006D48 RID: 27976 RVA: 0x001BDE67 File Offset: 0x001BC067
		[Parameter(Mandatory = false)]
		public Pattern[] AnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["AnyOfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["AnyOfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x06006D49 RID: 27977 RVA: 0x001BDE7A File Offset: 0x001BC07A
		// (set) Token: 0x06006D4A RID: 27978 RVA: 0x001BDE91 File Offset: 0x001BC091
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfFrom
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfFrom"];
			}
			set
			{
				base.Fields["ExceptIfFrom"] = value;
			}
		}

		// Token: 0x17002242 RID: 8770
		// (get) Token: 0x06006D4B RID: 27979 RVA: 0x001BDEA4 File Offset: 0x001BC0A4
		// (set) Token: 0x06006D4C RID: 27980 RVA: 0x001BDEBB File Offset: 0x001BC0BB
		[Parameter(Mandatory = false)]
		public bool HasSenderOverride
		{
			get
			{
				return (bool)base.Fields["HasSenderOverride"];
			}
			set
			{
				base.Fields["HasSenderOverride"] = value;
			}
		}

		// Token: 0x17002243 RID: 8771
		// (get) Token: 0x06006D4D RID: 27981 RVA: 0x001BDED3 File Offset: 0x001BC0D3
		// (set) Token: 0x06006D4E RID: 27982 RVA: 0x001BDEEA File Offset: 0x001BC0EA
		[Parameter(Mandatory = false)]
		public Hashtable[] MessageContainsDataClassifications
		{
			get
			{
				return (Hashtable[])base.Fields["MessageContainsDataClassifications"];
			}
			set
			{
				base.Fields["MessageContainsDataClassifications"] = value;
			}
		}

		// Token: 0x17002244 RID: 8772
		// (get) Token: 0x06006D4F RID: 27983 RVA: 0x001BDEFD File Offset: 0x001BC0FD
		// (set) Token: 0x06006D50 RID: 27984 RVA: 0x001BDF14 File Offset: 0x001BC114
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> SenderIpRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)base.Fields["SenderIpRanges"];
			}
			set
			{
				base.Fields["SenderIpRanges"] = value;
			}
		}

		// Token: 0x17002245 RID: 8773
		// (get) Token: 0x06006D51 RID: 27985 RVA: 0x001BDF27 File Offset: 0x001BC127
		// (set) Token: 0x06006D52 RID: 27986 RVA: 0x001BDF3E File Offset: 0x001BC13E
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfFromMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfFromMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfFromMemberOf"] = value;
			}
		}

		// Token: 0x17002246 RID: 8774
		// (get) Token: 0x06006D53 RID: 27987 RVA: 0x001BDF51 File Offset: 0x001BC151
		// (set) Token: 0x06006D54 RID: 27988 RVA: 0x001BDF68 File Offset: 0x001BC168
		[Parameter(Mandatory = false)]
		public FromUserScope? ExceptIfFromScope
		{
			get
			{
				return (FromUserScope?)base.Fields["ExceptIfFromScope"];
			}
			set
			{
				base.Fields["ExceptIfFromScope"] = value;
			}
		}

		// Token: 0x17002247 RID: 8775
		// (get) Token: 0x06006D55 RID: 27989 RVA: 0x001BDF80 File Offset: 0x001BC180
		// (set) Token: 0x06006D56 RID: 27990 RVA: 0x001BDF97 File Offset: 0x001BC197
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfSentTo"];
			}
			set
			{
				base.Fields["ExceptIfSentTo"] = value;
			}
		}

		// Token: 0x17002248 RID: 8776
		// (get) Token: 0x06006D57 RID: 27991 RVA: 0x001BDFAA File Offset: 0x001BC1AA
		// (set) Token: 0x06006D58 RID: 27992 RVA: 0x001BDFC1 File Offset: 0x001BC1C1
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentToMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfSentToMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfSentToMemberOf"] = value;
			}
		}

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x06006D59 RID: 27993 RVA: 0x001BDFD4 File Offset: 0x001BC1D4
		// (set) Token: 0x06006D5A RID: 27994 RVA: 0x001BDFEB File Offset: 0x001BC1EB
		[Parameter(Mandatory = false)]
		public ToUserScope? ExceptIfSentToScope
		{
			get
			{
				return (ToUserScope?)base.Fields["ExceptIfSentToScope"];
			}
			set
			{
				base.Fields["ExceptIfSentToScope"] = value;
			}
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x06006D5B RID: 27995 RVA: 0x001BE003 File Offset: 0x001BC203
		// (set) Token: 0x06006D5C RID: 27996 RVA: 0x001BE01A File Offset: 0x001BC21A
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfBetweenMemberOf1
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfBetweenMemberOf1"];
			}
			set
			{
				base.Fields["ExceptIfBetweenMemberOf1"] = value;
			}
		}

		// Token: 0x1700224B RID: 8779
		// (get) Token: 0x06006D5D RID: 27997 RVA: 0x001BE02D File Offset: 0x001BC22D
		// (set) Token: 0x06006D5E RID: 27998 RVA: 0x001BE044 File Offset: 0x001BC244
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfBetweenMemberOf2
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfBetweenMemberOf2"];
			}
			set
			{
				base.Fields["ExceptIfBetweenMemberOf2"] = value;
			}
		}

		// Token: 0x1700224C RID: 8780
		// (get) Token: 0x06006D5F RID: 27999 RVA: 0x001BE057 File Offset: 0x001BC257
		// (set) Token: 0x06006D60 RID: 28000 RVA: 0x001BE06E File Offset: 0x001BC26E
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfManagerAddresses
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfManagerAddresses"];
			}
			set
			{
				base.Fields["ExceptIfManagerAddresses"] = value;
			}
		}

		// Token: 0x1700224D RID: 8781
		// (get) Token: 0x06006D61 RID: 28001 RVA: 0x001BE081 File Offset: 0x001BC281
		// (set) Token: 0x06006D62 RID: 28002 RVA: 0x001BE098 File Offset: 0x001BC298
		[Parameter(Mandatory = false)]
		public EvaluatedUser? ExceptIfManagerForEvaluatedUser
		{
			get
			{
				return (EvaluatedUser?)base.Fields["ExceptIfManagerForEvaluatedUser"];
			}
			set
			{
				base.Fields["ExceptIfManagerForEvaluatedUser"] = value;
			}
		}

		// Token: 0x1700224E RID: 8782
		// (get) Token: 0x06006D63 RID: 28003 RVA: 0x001BE0B0 File Offset: 0x001BC2B0
		// (set) Token: 0x06006D64 RID: 28004 RVA: 0x001BE0C7 File Offset: 0x001BC2C7
		[Parameter(Mandatory = false)]
		public ManagementRelationship? ExceptIfSenderManagementRelationship
		{
			get
			{
				return (ManagementRelationship?)base.Fields["ExceptIfSenderManagementRelationship"];
			}
			set
			{
				base.Fields["ExceptIfSenderManagementRelationship"] = value;
			}
		}

		// Token: 0x1700224F RID: 8783
		// (get) Token: 0x06006D65 RID: 28005 RVA: 0x001BE0DF File Offset: 0x001BC2DF
		// (set) Token: 0x06006D66 RID: 28006 RVA: 0x001BE0F6 File Offset: 0x001BC2F6
		[Parameter(Mandatory = false)]
		public ADAttribute? ExceptIfADComparisonAttribute
		{
			get
			{
				return (ADAttribute?)base.Fields["ExceptIfADComparisonAttribute"];
			}
			set
			{
				base.Fields["ExceptIfADComparisonAttribute"] = value;
			}
		}

		// Token: 0x17002250 RID: 8784
		// (get) Token: 0x06006D67 RID: 28007 RVA: 0x001BE10E File Offset: 0x001BC30E
		// (set) Token: 0x06006D68 RID: 28008 RVA: 0x001BE125 File Offset: 0x001BC325
		[Parameter(Mandatory = false)]
		public Evaluation? ExceptIfADComparisonOperator
		{
			get
			{
				return (Evaluation?)base.Fields["ExceptIfADComparisonOperator"];
			}
			set
			{
				base.Fields["ExceptIfADComparisonOperator"] = value;
			}
		}

		// Token: 0x17002251 RID: 8785
		// (get) Token: 0x06006D69 RID: 28009 RVA: 0x001BE13D File Offset: 0x001BC33D
		// (set) Token: 0x06006D6A RID: 28010 RVA: 0x001BE154 File Offset: 0x001BC354
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSenderADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSenderADAttributeContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfSenderADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x17002252 RID: 8786
		// (get) Token: 0x06006D6B RID: 28011 RVA: 0x001BE167 File Offset: 0x001BC367
		// (set) Token: 0x06006D6C RID: 28012 RVA: 0x001BE17E File Offset: 0x001BC37E
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfSenderADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfSenderADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfSenderADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002253 RID: 8787
		// (get) Token: 0x06006D6D RID: 28013 RVA: 0x001BE191 File Offset: 0x001BC391
		// (set) Token: 0x06006D6E RID: 28014 RVA: 0x001BE1A8 File Offset: 0x001BC3A8
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientADAttributeContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfRecipientADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x17002254 RID: 8788
		// (get) Token: 0x06006D6F RID: 28015 RVA: 0x001BE1BB File Offset: 0x001BC3BB
		// (set) Token: 0x06006D70 RID: 28016 RVA: 0x001BE1D2 File Offset: 0x001BC3D2
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfRecipientADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfRecipientADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfRecipientADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002255 RID: 8789
		// (get) Token: 0x06006D71 RID: 28017 RVA: 0x001BE1E5 File Offset: 0x001BC3E5
		// (set) Token: 0x06006D72 RID: 28018 RVA: 0x001BE1FC File Offset: 0x001BC3FC
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToHeader"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToHeader"] = value;
			}
		}

		// Token: 0x17002256 RID: 8790
		// (get) Token: 0x06006D73 RID: 28019 RVA: 0x001BE20F File Offset: 0x001BC40F
		// (set) Token: 0x06006D74 RID: 28020 RVA: 0x001BE226 File Offset: 0x001BC426
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToHeaderMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToHeaderMemberOf"] = value;
			}
		}

		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x06006D75 RID: 28021 RVA: 0x001BE239 File Offset: 0x001BC439
		// (set) Token: 0x06006D76 RID: 28022 RVA: 0x001BE250 File Offset: 0x001BC450
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfCcHeader"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfCcHeader"] = value;
			}
		}

		// Token: 0x17002258 RID: 8792
		// (get) Token: 0x06006D77 RID: 28023 RVA: 0x001BE263 File Offset: 0x001BC463
		// (set) Token: 0x06006D78 RID: 28024 RVA: 0x001BE27A File Offset: 0x001BC47A
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x17002259 RID: 8793
		// (get) Token: 0x06006D79 RID: 28025 RVA: 0x001BE28D File Offset: 0x001BC48D
		// (set) Token: 0x06006D7A RID: 28026 RVA: 0x001BE2A4 File Offset: 0x001BC4A4
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToCcHeader"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToCcHeader"] = value;
			}
		}

		// Token: 0x1700225A RID: 8794
		// (get) Token: 0x06006D7B RID: 28027 RVA: 0x001BE2B7 File Offset: 0x001BC4B7
		// (set) Token: 0x06006D7C RID: 28028 RVA: 0x001BE2CE File Offset: 0x001BC4CE
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x06006D7D RID: 28029 RVA: 0x001BE2E1 File Offset: 0x001BC4E1
		// (set) Token: 0x06006D7E RID: 28030 RVA: 0x001BE2F8 File Offset: 0x001BC4F8
		[Parameter(Mandatory = false)]
		public string ExceptIfHasClassification
		{
			get
			{
				return (string)base.Fields["ExceptIfHasClassification"];
			}
			set
			{
				base.Fields["ExceptIfHasClassification"] = value;
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x06006D7F RID: 28031 RVA: 0x001BE30B File Offset: 0x001BC50B
		// (set) Token: 0x06006D80 RID: 28032 RVA: 0x001BE322 File Offset: 0x001BC522
		[Parameter(Mandatory = false)]
		public bool ExceptIfHasNoClassification
		{
			get
			{
				return (bool)base.Fields["ExceptIfHasNoClassification"];
			}
			set
			{
				base.Fields["ExceptIfHasNoClassification"] = value;
			}
		}

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x06006D81 RID: 28033 RVA: 0x001BE33A File Offset: 0x001BC53A
		// (set) Token: 0x06006D82 RID: 28034 RVA: 0x001BE351 File Offset: 0x001BC551
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSubjectContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSubjectContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfSubjectContainsWords"] = value;
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x06006D83 RID: 28035 RVA: 0x001BE364 File Offset: 0x001BC564
		// (set) Token: 0x06006D84 RID: 28036 RVA: 0x001BE37B File Offset: 0x001BC57B
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSubjectOrBodyContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfSubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x06006D85 RID: 28037 RVA: 0x001BE38E File Offset: 0x001BC58E
		// (set) Token: 0x06006D86 RID: 28038 RVA: 0x001BE3A5 File Offset: 0x001BC5A5
		[Parameter(Mandatory = false)]
		public HeaderName? ExceptIfHeaderContainsMessageHeader
		{
			get
			{
				return (HeaderName?)base.Fields["ExceptIfHeaderContainsMessageHeader"];
			}
			set
			{
				base.Fields["ExceptIfHeaderContainsMessageHeader"] = value;
			}
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x06006D87 RID: 28039 RVA: 0x001BE3BD File Offset: 0x001BC5BD
		// (set) Token: 0x06006D88 RID: 28040 RVA: 0x001BE3D4 File Offset: 0x001BC5D4
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfHeaderContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfHeaderContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfHeaderContainsWords"] = value;
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x06006D89 RID: 28041 RVA: 0x001BE3E7 File Offset: 0x001BC5E7
		// (set) Token: 0x06006D8A RID: 28042 RVA: 0x001BE3FE File Offset: 0x001BC5FE
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfFromAddressContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfFromAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x06006D8B RID: 28043 RVA: 0x001BE411 File Offset: 0x001BC611
		// (set) Token: 0x06006D8C RID: 28044 RVA: 0x001BE428 File Offset: 0x001BC628
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSenderDomainIs
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSenderDomainIs"];
			}
			set
			{
				base.Fields["ExceptIfSenderDomainIs"] = value;
			}
		}

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06006D8D RID: 28045 RVA: 0x001BE43B File Offset: 0x001BC63B
		// (set) Token: 0x06006D8E RID: 28046 RVA: 0x001BE452 File Offset: 0x001BC652
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientDomainIs
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientDomainIs"];
			}
			set
			{
				base.Fields["ExceptIfRecipientDomainIs"] = value;
			}
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x06006D8F RID: 28047 RVA: 0x001BE465 File Offset: 0x001BC665
		// (set) Token: 0x06006D90 RID: 28048 RVA: 0x001BE47C File Offset: 0x001BC67C
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfSubjectMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfSubjectMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfSubjectMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x06006D91 RID: 28049 RVA: 0x001BE48F File Offset: 0x001BC68F
		// (set) Token: 0x06006D92 RID: 28050 RVA: 0x001BE4A6 File Offset: 0x001BC6A6
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfSubjectOrBodyMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfSubjectOrBodyMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfSubjectOrBodyMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x06006D93 RID: 28051 RVA: 0x001BE4B9 File Offset: 0x001BC6B9
		// (set) Token: 0x06006D94 RID: 28052 RVA: 0x001BE4D0 File Offset: 0x001BC6D0
		[Parameter(Mandatory = false)]
		public HeaderName? ExceptIfHeaderMatchesMessageHeader
		{
			get
			{
				return (HeaderName?)base.Fields["ExceptIfHeaderMatchesMessageHeader"];
			}
			set
			{
				base.Fields["ExceptIfHeaderMatchesMessageHeader"] = value;
			}
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x06006D95 RID: 28053 RVA: 0x001BE4E8 File Offset: 0x001BC6E8
		// (set) Token: 0x06006D96 RID: 28054 RVA: 0x001BE4FF File Offset: 0x001BC6FF
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfHeaderMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfHeaderMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfHeaderMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x06006D97 RID: 28055 RVA: 0x001BE512 File Offset: 0x001BC712
		// (set) Token: 0x06006D98 RID: 28056 RVA: 0x001BE529 File Offset: 0x001BC729
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfFromAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfFromAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfFromAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x06006D99 RID: 28057 RVA: 0x001BE53C File Offset: 0x001BC73C
		// (set) Token: 0x06006D9A RID: 28058 RVA: 0x001BE553 File Offset: 0x001BC753
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfAttachmentNameMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfAttachmentNameMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentNameMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x06006D9B RID: 28059 RVA: 0x001BE566 File Offset: 0x001BC766
		// (set) Token: 0x06006D9C RID: 28060 RVA: 0x001BE57D File Offset: 0x001BC77D
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAttachmentExtensionMatchesWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAttachmentExtensionMatchesWords"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentExtensionMatchesWords"] = value;
			}
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x06006D9D RID: 28061 RVA: 0x001BE590 File Offset: 0x001BC790
		// (set) Token: 0x06006D9E RID: 28062 RVA: 0x001BE5A7 File Offset: 0x001BC7A7
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAttachmentPropertyContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAttachmentPropertyContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentPropertyContainsWords"] = value;
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x06006D9F RID: 28063 RVA: 0x001BE5BA File Offset: 0x001BC7BA
		// (set) Token: 0x06006DA0 RID: 28064 RVA: 0x001BE5D1 File Offset: 0x001BC7D1
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfContentCharacterSetContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfContentCharacterSetContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfContentCharacterSetContainsWords"] = value;
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x06006DA1 RID: 28065 RVA: 0x001BE5E4 File Offset: 0x001BC7E4
		// (set) Token: 0x06006DA2 RID: 28066 RVA: 0x001BE5FB File Offset: 0x001BC7FB
		[Parameter(Mandatory = false)]
		public SclValue? ExceptIfSCLOver
		{
			get
			{
				return (SclValue?)base.Fields["ExceptIfSCLOver"];
			}
			set
			{
				base.Fields["ExceptIfSCLOver"] = value;
			}
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x06006DA3 RID: 28067 RVA: 0x001BE613 File Offset: 0x001BC813
		// (set) Token: 0x06006DA4 RID: 28068 RVA: 0x001BE62A File Offset: 0x001BC82A
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? ExceptIfAttachmentSizeOver
		{
			get
			{
				return (ByteQuantifiedSize?)base.Fields["ExceptIfAttachmentSizeOver"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentSizeOver"] = value;
			}
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x06006DA5 RID: 28069 RVA: 0x001BE642 File Offset: 0x001BC842
		// (set) Token: 0x06006DA6 RID: 28070 RVA: 0x001BE65E File Offset: 0x001BC85E
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? ExceptIfMessageSizeOver
		{
			get
			{
				return new ByteQuantifiedSize?((ByteQuantifiedSize)base.Fields["ExceptIfMessageSizeOver"]);
			}
			set
			{
				base.Fields["ExceptIfMessageSizeOver"] = value;
			}
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x06006DA7 RID: 28071 RVA: 0x001BE676 File Offset: 0x001BC876
		// (set) Token: 0x06006DA8 RID: 28072 RVA: 0x001BE68D File Offset: 0x001BC88D
		[Parameter(Mandatory = false)]
		public Importance? ExceptIfWithImportance
		{
			get
			{
				return (Importance?)base.Fields["ExceptIfWithImportance"];
			}
			set
			{
				base.Fields["ExceptIfWithImportance"] = value;
			}
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x06006DA9 RID: 28073 RVA: 0x001BE6A5 File Offset: 0x001BC8A5
		// (set) Token: 0x06006DAA RID: 28074 RVA: 0x001BE6BC File Offset: 0x001BC8BC
		[Parameter(Mandatory = false)]
		public MessageType? ExceptIfMessageTypeMatches
		{
			get
			{
				return (MessageType?)base.Fields["ExceptIfMessageTypeMatches"];
			}
			set
			{
				base.Fields["ExceptIfMessageTypeMatches"] = value;
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x06006DAB RID: 28075 RVA: 0x001BE6D4 File Offset: 0x001BC8D4
		// (set) Token: 0x06006DAC RID: 28076 RVA: 0x001BE6EB File Offset: 0x001BC8EB
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x06006DAD RID: 28077 RVA: 0x001BE6FE File Offset: 0x001BC8FE
		// (set) Token: 0x06006DAE RID: 28078 RVA: 0x001BE715 File Offset: 0x001BC915
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfRecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x06006DAF RID: 28079 RVA: 0x001BE728 File Offset: 0x001BC928
		// (set) Token: 0x06006DB0 RID: 28080 RVA: 0x001BE73F File Offset: 0x001BC93F
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSenderInRecipientList
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSenderInRecipientList"];
			}
			set
			{
				base.Fields["ExceptIfSenderInRecipientList"] = value;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x06006DB1 RID: 28081 RVA: 0x001BE752 File Offset: 0x001BC952
		// (set) Token: 0x06006DB2 RID: 28082 RVA: 0x001BE769 File Offset: 0x001BC969
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientInSenderList
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientInSenderList"];
			}
			set
			{
				base.Fields["ExceptIfRecipientInSenderList"] = value;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x06006DB3 RID: 28083 RVA: 0x001BE77C File Offset: 0x001BC97C
		// (set) Token: 0x06006DB4 RID: 28084 RVA: 0x001BE793 File Offset: 0x001BC993
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAttachmentContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAttachmentContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentContainsWords"] = value;
			}
		}

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x06006DB5 RID: 28085 RVA: 0x001BE7A6 File Offset: 0x001BC9A6
		// (set) Token: 0x06006DB6 RID: 28086 RVA: 0x001BE7BD File Offset: 0x001BC9BD
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfAttachmentMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfAttachmentMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x06006DB7 RID: 28087 RVA: 0x001BE7D0 File Offset: 0x001BC9D0
		// (set) Token: 0x06006DB8 RID: 28088 RVA: 0x001BE7E7 File Offset: 0x001BC9E7
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentIsUnsupported
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentIsUnsupported"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentIsUnsupported"] = value;
			}
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x06006DB9 RID: 28089 RVA: 0x001BE7FF File Offset: 0x001BC9FF
		// (set) Token: 0x06006DBA RID: 28090 RVA: 0x001BE816 File Offset: 0x001BCA16
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentProcessingLimitExceeded
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentProcessingLimitExceeded"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentProcessingLimitExceeded"] = value;
			}
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x06006DBB RID: 28091 RVA: 0x001BE82E File Offset: 0x001BCA2E
		// (set) Token: 0x06006DBC RID: 28092 RVA: 0x001BE845 File Offset: 0x001BCA45
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentHasExecutableContent
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentHasExecutableContent"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentHasExecutableContent"] = value;
			}
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x06006DBD RID: 28093 RVA: 0x001BE85D File Offset: 0x001BCA5D
		// (set) Token: 0x06006DBE RID: 28094 RVA: 0x001BE874 File Offset: 0x001BCA74
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAnyOfRecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAnyOfRecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x06006DBF RID: 28095 RVA: 0x001BE887 File Offset: 0x001BCA87
		// (set) Token: 0x06006DC0 RID: 28096 RVA: 0x001BE89E File Offset: 0x001BCA9E
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentIsPasswordProtected
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentIsPasswordProtected"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentIsPasswordProtected"] = value;
			}
		}

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x06006DC1 RID: 28097 RVA: 0x001BE8B6 File Offset: 0x001BCAB6
		// (set) Token: 0x06006DC2 RID: 28098 RVA: 0x001BE8CD File Offset: 0x001BCACD
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfAnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfAnyOfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x06006DC3 RID: 28099 RVA: 0x001BE8E0 File Offset: 0x001BCAE0
		// (set) Token: 0x06006DC4 RID: 28100 RVA: 0x001BE8F7 File Offset: 0x001BCAF7
		[Parameter(Mandatory = false)]
		public bool ExceptIfHasSenderOverride
		{
			get
			{
				return (bool)base.Fields["ExceptIfHasSenderOverride"];
			}
			set
			{
				base.Fields["ExceptIfHasSenderOverride"] = value;
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x06006DC5 RID: 28101 RVA: 0x001BE90F File Offset: 0x001BCB0F
		// (set) Token: 0x06006DC6 RID: 28102 RVA: 0x001BE926 File Offset: 0x001BCB26
		[Parameter(Mandatory = false)]
		public Hashtable[] ExceptIfMessageContainsDataClassifications
		{
			get
			{
				return (Hashtable[])base.Fields["ExceptIfMessageContainsDataClassifications"];
			}
			set
			{
				base.Fields["ExceptIfMessageContainsDataClassifications"] = value;
			}
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x06006DC7 RID: 28103 RVA: 0x001BE939 File Offset: 0x001BCB39
		// (set) Token: 0x06006DC8 RID: 28104 RVA: 0x001BE950 File Offset: 0x001BCB50
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> ExceptIfSenderIpRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)base.Fields["ExceptIfSenderIpRanges"];
			}
			set
			{
				base.Fields["ExceptIfSenderIpRanges"] = value;
			}
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x06006DC9 RID: 28105 RVA: 0x001BE963 File Offset: 0x001BCB63
		// (set) Token: 0x06006DCA RID: 28106 RVA: 0x001BE97A File Offset: 0x001BCB7A
		[Parameter(Mandatory = false)]
		public SubjectPrefix? PrependSubject
		{
			get
			{
				return (SubjectPrefix?)base.Fields["PrependSubject"];
			}
			set
			{
				base.Fields["PrependSubject"] = value;
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x06006DCB RID: 28107 RVA: 0x001BE992 File Offset: 0x001BCB92
		// (set) Token: 0x06006DCC RID: 28108 RVA: 0x001BE9A9 File Offset: 0x001BCBA9
		[Parameter(Mandatory = false)]
		public string SetAuditSeverity
		{
			get
			{
				return (string)base.Fields["SetAuditSeverity"];
			}
			set
			{
				base.Fields["SetAuditSeverity"] = value;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x06006DCD RID: 28109 RVA: 0x001BE9BC File Offset: 0x001BCBBC
		// (set) Token: 0x06006DCE RID: 28110 RVA: 0x001BE9D3 File Offset: 0x001BCBD3
		[Parameter(Mandatory = false)]
		public string ApplyClassification
		{
			get
			{
				return (string)base.Fields["ApplyClassification"];
			}
			set
			{
				base.Fields["ApplyClassification"] = value;
			}
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x06006DCF RID: 28111 RVA: 0x001BE9E6 File Offset: 0x001BCBE6
		// (set) Token: 0x06006DD0 RID: 28112 RVA: 0x001BE9FD File Offset: 0x001BCBFD
		[Parameter(Mandatory = false)]
		public DisclaimerLocation? ApplyHtmlDisclaimerLocation
		{
			get
			{
				return (DisclaimerLocation?)base.Fields["ApplyHtmlDisclaimerLocation"];
			}
			set
			{
				base.Fields["ApplyHtmlDisclaimerLocation"] = value;
			}
		}

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x06006DD1 RID: 28113 RVA: 0x001BEA15 File Offset: 0x001BCC15
		// (set) Token: 0x06006DD2 RID: 28114 RVA: 0x001BEA2C File Offset: 0x001BCC2C
		[Parameter(Mandatory = false)]
		public DisclaimerText? ApplyHtmlDisclaimerText
		{
			get
			{
				return (DisclaimerText?)base.Fields["ApplyHtmlDisclaimerText"];
			}
			set
			{
				base.Fields["ApplyHtmlDisclaimerText"] = value;
			}
		}

		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x06006DD3 RID: 28115 RVA: 0x001BEA44 File Offset: 0x001BCC44
		// (set) Token: 0x06006DD4 RID: 28116 RVA: 0x001BEA5B File Offset: 0x001BCC5B
		[Parameter(Mandatory = false)]
		public DisclaimerFallbackAction? ApplyHtmlDisclaimerFallbackAction
		{
			get
			{
				return (DisclaimerFallbackAction?)base.Fields["ApplyHtmlDisclaimerFallbackAction"];
			}
			set
			{
				base.Fields["ApplyHtmlDisclaimerFallbackAction"] = value;
			}
		}

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x06006DD5 RID: 28117 RVA: 0x001BEA73 File Offset: 0x001BCC73
		// (set) Token: 0x06006DD6 RID: 28118 RVA: 0x001BEA8A File Offset: 0x001BCC8A
		[Parameter(Mandatory = false)]
		public RmsTemplateIdParameter ApplyRightsProtectionTemplate
		{
			get
			{
				return (RmsTemplateIdParameter)base.Fields["ApplyRightsProtectionTemplate"];
			}
			set
			{
				base.Fields["ApplyRightsProtectionTemplate"] = value;
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x06006DD7 RID: 28119 RVA: 0x001BEA9D File Offset: 0x001BCC9D
		// (set) Token: 0x06006DD8 RID: 28120 RVA: 0x001BEAB4 File Offset: 0x001BCCB4
		[Parameter(Mandatory = false)]
		public SclValue? SetSCL
		{
			get
			{
				return (SclValue?)base.Fields["SetSCL"];
			}
			set
			{
				base.Fields["SetSCL"] = value;
			}
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x06006DD9 RID: 28121 RVA: 0x001BEACC File Offset: 0x001BCCCC
		// (set) Token: 0x06006DDA RID: 28122 RVA: 0x001BEAE3 File Offset: 0x001BCCE3
		[Parameter(Mandatory = false)]
		public HeaderName? SetHeaderName
		{
			get
			{
				return (HeaderName?)base.Fields["SetHeaderName"];
			}
			set
			{
				base.Fields["SetHeaderName"] = value;
			}
		}

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x06006DDB RID: 28123 RVA: 0x001BEAFB File Offset: 0x001BCCFB
		// (set) Token: 0x06006DDC RID: 28124 RVA: 0x001BEB12 File Offset: 0x001BCD12
		[Parameter(Mandatory = false)]
		public HeaderValue? SetHeaderValue
		{
			get
			{
				return (HeaderValue?)base.Fields["SetHeaderValue"];
			}
			set
			{
				base.Fields["SetHeaderValue"] = value;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x06006DDD RID: 28125 RVA: 0x001BEB2A File Offset: 0x001BCD2A
		// (set) Token: 0x06006DDE RID: 28126 RVA: 0x001BEB41 File Offset: 0x001BCD41
		[Parameter(Mandatory = false)]
		public HeaderName? RemoveHeader
		{
			get
			{
				return (HeaderName?)base.Fields["RemoveHeader"];
			}
			set
			{
				base.Fields["RemoveHeader"] = value;
			}
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x06006DDF RID: 28127 RVA: 0x001BEB59 File Offset: 0x001BCD59
		// (set) Token: 0x06006DE0 RID: 28128 RVA: 0x001BEB70 File Offset: 0x001BCD70
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AddToRecipients
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AddToRecipients"];
			}
			set
			{
				base.Fields["AddToRecipients"] = value;
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x06006DE1 RID: 28129 RVA: 0x001BEB83 File Offset: 0x001BCD83
		// (set) Token: 0x06006DE2 RID: 28130 RVA: 0x001BEB9A File Offset: 0x001BCD9A
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] CopyTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["CopyTo"];
			}
			set
			{
				base.Fields["CopyTo"] = value;
			}
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x06006DE3 RID: 28131 RVA: 0x001BEBAD File Offset: 0x001BCDAD
		// (set) Token: 0x06006DE4 RID: 28132 RVA: 0x001BEBC4 File Offset: 0x001BCDC4
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BlindCopyTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BlindCopyTo"];
			}
			set
			{
				base.Fields["BlindCopyTo"] = value;
			}
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x06006DE5 RID: 28133 RVA: 0x001BEBD7 File Offset: 0x001BCDD7
		// (set) Token: 0x06006DE6 RID: 28134 RVA: 0x001BEBF3 File Offset: 0x001BCDF3
		[Parameter(Mandatory = false)]
		public AddedRecipientType? AddManagerAsRecipientType
		{
			get
			{
				return new AddedRecipientType?((AddedRecipientType)base.Fields["AddManagerAsRecipientType"]);
			}
			set
			{
				base.Fields["AddManagerAsRecipientType"] = value;
			}
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x06006DE7 RID: 28135 RVA: 0x001BEC0B File Offset: 0x001BCE0B
		// (set) Token: 0x06006DE8 RID: 28136 RVA: 0x001BEC22 File Offset: 0x001BCE22
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ModerateMessageByUser
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ModerateMessageByUser"];
			}
			set
			{
				base.Fields["ModerateMessageByUser"] = value;
			}
		}

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x001BEC35 File Offset: 0x001BCE35
		// (set) Token: 0x06006DEA RID: 28138 RVA: 0x001BEC4C File Offset: 0x001BCE4C
		[Parameter(Mandatory = false)]
		public bool ModerateMessageByManager
		{
			get
			{
				return (bool)base.Fields["ModerateMessageByManager"];
			}
			set
			{
				base.Fields["ModerateMessageByManager"] = value;
			}
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x06006DEB RID: 28139 RVA: 0x001BEC64 File Offset: 0x001BCE64
		// (set) Token: 0x06006DEC RID: 28140 RVA: 0x001BEC7B File Offset: 0x001BCE7B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] RedirectMessageTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RedirectMessageTo"];
			}
			set
			{
				base.Fields["RedirectMessageTo"] = value;
			}
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x06006DED RID: 28141 RVA: 0x001BEC8E File Offset: 0x001BCE8E
		// (set) Token: 0x06006DEE RID: 28142 RVA: 0x001BECAA File Offset: 0x001BCEAA
		[Parameter(Mandatory = false)]
		public NotifySenderType? NotifySender
		{
			get
			{
				return new NotifySenderType?((NotifySenderType)base.Fields["NotifySender"]);
			}
			set
			{
				base.Fields["NotifySender"] = value;
			}
		}

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x06006DEF RID: 28143 RVA: 0x001BECC2 File Offset: 0x001BCEC2
		// (set) Token: 0x06006DF0 RID: 28144 RVA: 0x001BECD9 File Offset: 0x001BCED9
		[Parameter(Mandatory = false)]
		public RejectEnhancedStatus? RejectMessageEnhancedStatusCode
		{
			get
			{
				return (RejectEnhancedStatus?)base.Fields["RejectMessageEnhancedStatusCode"];
			}
			set
			{
				base.Fields["RejectMessageEnhancedStatusCode"] = value;
			}
		}

		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x06006DF1 RID: 28145 RVA: 0x001BECF1 File Offset: 0x001BCEF1
		// (set) Token: 0x06006DF2 RID: 28146 RVA: 0x001BED08 File Offset: 0x001BCF08
		[Parameter(Mandatory = false)]
		public DsnText? RejectMessageReasonText
		{
			get
			{
				return (DsnText?)base.Fields["RejectMessageReasonText"];
			}
			set
			{
				base.Fields["RejectMessageReasonText"] = value;
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x06006DF3 RID: 28147 RVA: 0x001BED20 File Offset: 0x001BCF20
		// (set) Token: 0x06006DF4 RID: 28148 RVA: 0x001BED37 File Offset: 0x001BCF37
		[Parameter(Mandatory = false)]
		public bool DeleteMessage
		{
			get
			{
				return (bool)base.Fields["DeleteMessage"];
			}
			set
			{
				base.Fields["DeleteMessage"] = value;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x06006DF5 RID: 28149 RVA: 0x001BED4F File Offset: 0x001BCF4F
		// (set) Token: 0x06006DF6 RID: 28150 RVA: 0x001BED66 File Offset: 0x001BCF66
		[Parameter(Mandatory = false)]
		public bool Disconnect
		{
			get
			{
				return (bool)base.Fields["Disconnect"];
			}
			set
			{
				base.Fields["Disconnect"] = value;
			}
		}

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x06006DF7 RID: 28151 RVA: 0x001BED7E File Offset: 0x001BCF7E
		// (set) Token: 0x06006DF8 RID: 28152 RVA: 0x001BED95 File Offset: 0x001BCF95
		[Parameter(Mandatory = false)]
		public bool Quarantine
		{
			get
			{
				return (bool)base.Fields["Quarantine"];
			}
			set
			{
				base.Fields["Quarantine"] = value;
			}
		}

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x001BEDAD File Offset: 0x001BCFAD
		// (set) Token: 0x06006DFA RID: 28154 RVA: 0x001BEDC4 File Offset: 0x001BCFC4
		[Parameter(Mandatory = false)]
		public RejectText? SmtpRejectMessageRejectText
		{
			get
			{
				return (RejectText?)base.Fields["SmtpRejectMessageRejectText"];
			}
			set
			{
				base.Fields["SmtpRejectMessageRejectText"] = value;
			}
		}

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x06006DFB RID: 28155 RVA: 0x001BEDDC File Offset: 0x001BCFDC
		// (set) Token: 0x06006DFC RID: 28156 RVA: 0x001BEDF3 File Offset: 0x001BCFF3
		[Parameter(Mandatory = false)]
		public RejectStatusCode? SmtpRejectMessageRejectStatusCode
		{
			get
			{
				return (RejectStatusCode?)base.Fields["SmtpRejectMessageRejectStatusCode"];
			}
			set
			{
				base.Fields["SmtpRejectMessageRejectStatusCode"] = value;
			}
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x06006DFD RID: 28157 RVA: 0x001BEE0B File Offset: 0x001BD00B
		// (set) Token: 0x06006DFE RID: 28158 RVA: 0x001BEE22 File Offset: 0x001BD022
		[Parameter(Mandatory = false)]
		public DateTime? ActivationDate
		{
			get
			{
				return (DateTime?)base.Fields["ActivationDate"];
			}
			set
			{
				base.Fields["ActivationDate"] = value;
			}
		}

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x06006DFF RID: 28159 RVA: 0x001BEE3A File Offset: 0x001BD03A
		// (set) Token: 0x06006E00 RID: 28160 RVA: 0x001BEE51 File Offset: 0x001BD051
		[Parameter(Mandatory = false)]
		public DateTime? ExpiryDate
		{
			get
			{
				return (DateTime?)base.Fields["ExpiryDate"];
			}
			set
			{
				base.Fields["ExpiryDate"] = value;
			}
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x06006E01 RID: 28161 RVA: 0x001BEE69 File Offset: 0x001BD069
		// (set) Token: 0x06006E02 RID: 28162 RVA: 0x001BEE80 File Offset: 0x001BD080
		[Parameter(Mandatory = false)]
		public RuleSubType RuleSubType
		{
			get
			{
				return (RuleSubType)base.Fields["RuleSubType"];
			}
			set
			{
				base.Fields["RuleSubType"] = value;
			}
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x06006E03 RID: 28163 RVA: 0x001BEE98 File Offset: 0x001BD098
		// (set) Token: 0x06006E04 RID: 28164 RVA: 0x001BEEAF File Offset: 0x001BD0AF
		[Parameter(Mandatory = false)]
		public RuleErrorAction RuleErrorAction
		{
			get
			{
				return (RuleErrorAction)base.Fields["RuleErrorAction"];
			}
			set
			{
				base.Fields["RuleErrorAction"] = value;
			}
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x06006E05 RID: 28165 RVA: 0x001BEEC7 File Offset: 0x001BD0C7
		// (set) Token: 0x06006E06 RID: 28166 RVA: 0x001BEEDE File Offset: 0x001BD0DE
		[Parameter(Mandatory = false)]
		public SenderAddressLocation SenderAddressLocation
		{
			get
			{
				return (SenderAddressLocation)base.Fields["SenderAddressLocation"];
			}
			set
			{
				base.Fields["SenderAddressLocation"] = value;
			}
		}

		// Token: 0x170022A0 RID: 8864
		// (get) Token: 0x06006E07 RID: 28167 RVA: 0x001BEEF6 File Offset: 0x001BD0F6
		// (set) Token: 0x06006E08 RID: 28168 RVA: 0x001BEF0D File Offset: 0x001BD10D
		[Parameter(Mandatory = false)]
		public EventLogText? LogEventText
		{
			get
			{
				return (EventLogText?)base.Fields["LogEventText"];
			}
			set
			{
				base.Fields["LogEventText"] = value;
			}
		}

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x06006E09 RID: 28169 RVA: 0x001BEF25 File Offset: 0x001BD125
		// (set) Token: 0x06006E0A RID: 28170 RVA: 0x001BEF3C File Offset: 0x001BD13C
		[Parameter(Mandatory = false)]
		public bool StopRuleProcessing
		{
			get
			{
				return (bool)base.Fields["StopRuleProcessing"];
			}
			set
			{
				base.Fields["StopRuleProcessing"] = value;
			}
		}

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x06006E0B RID: 28171 RVA: 0x001BEF54 File Offset: 0x001BD154
		// (set) Token: 0x06006E0C RID: 28172 RVA: 0x001BEF6B File Offset: 0x001BD16B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter GenerateIncidentReport
		{
			get
			{
				return (RecipientIdParameter)base.Fields["GenerateIncidentReport"];
			}
			set
			{
				base.Fields["GenerateIncidentReport"] = value;
			}
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x06006E0D RID: 28173 RVA: 0x001BEF7E File Offset: 0x001BD17E
		// (set) Token: 0x06006E0E RID: 28174 RVA: 0x001BEF95 File Offset: 0x001BD195
		[Parameter(Mandatory = false)]
		public IncidentReportOriginalMail? IncidentReportOriginalMail
		{
			get
			{
				return (IncidentReportOriginalMail?)base.Fields["IncidentReportOriginalMail"];
			}
			set
			{
				base.Fields["IncidentReportOriginalMail"] = value;
			}
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x06006E0F RID: 28175 RVA: 0x001BEFAD File Offset: 0x001BD1AD
		// (set) Token: 0x06006E10 RID: 28176 RVA: 0x001BEFC4 File Offset: 0x001BD1C4
		[Parameter(Mandatory = false)]
		public IncidentReportContent[] IncidentReportContent
		{
			get
			{
				return (IncidentReportContent[])base.Fields["IncidentReportContent"];
			}
			set
			{
				base.Fields["IncidentReportContent"] = value;
			}
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x06006E11 RID: 28177 RVA: 0x001BEFD7 File Offset: 0x001BD1D7
		// (set) Token: 0x06006E12 RID: 28178 RVA: 0x001BEFEE File Offset: 0x001BD1EE
		[Parameter(Mandatory = false)]
		public DisclaimerText? GenerateNotification
		{
			get
			{
				return (DisclaimerText?)base.Fields["GenerateNotification"];
			}
			set
			{
				base.Fields["GenerateNotification"] = value;
			}
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x06006E13 RID: 28179 RVA: 0x001BF006 File Offset: 0x001BD206
		// (set) Token: 0x06006E14 RID: 28180 RVA: 0x001BF01D File Offset: 0x001BD21D
		[Parameter(Mandatory = false)]
		public OutboundConnectorIdParameter RouteMessageOutboundConnector
		{
			get
			{
				return (OutboundConnectorIdParameter)base.Fields["RouteMessageOutboundConnector"];
			}
			set
			{
				base.Fields["RouteMessageOutboundConnector"] = value;
			}
		}

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x06006E15 RID: 28181 RVA: 0x001BF030 File Offset: 0x001BD230
		// (set) Token: 0x06006E16 RID: 28182 RVA: 0x001BF047 File Offset: 0x001BD247
		[Parameter(Mandatory = false)]
		public bool RouteMessageOutboundRequireTls
		{
			get
			{
				return (bool)base.Fields["RouteMessageOutboundRequireTls"];
			}
			set
			{
				base.Fields["RouteMessageOutboundRequireTls"] = value;
			}
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x06006E17 RID: 28183 RVA: 0x001BF05F File Offset: 0x001BD25F
		// (set) Token: 0x06006E18 RID: 28184 RVA: 0x001BF076 File Offset: 0x001BD276
		[Parameter(Mandatory = false)]
		public bool ApplyOME
		{
			get
			{
				return (bool)base.Fields["ApplyOME"];
			}
			set
			{
				base.Fields["ApplyOME"] = value;
			}
		}

		// Token: 0x170022A9 RID: 8873
		// (get) Token: 0x06006E19 RID: 28185 RVA: 0x001BF08E File Offset: 0x001BD28E
		// (set) Token: 0x06006E1A RID: 28186 RVA: 0x001BF0A5 File Offset: 0x001BD2A5
		[Parameter(Mandatory = false)]
		public bool RemoveOME
		{
			get
			{
				return (bool)base.Fields["RemoveOME"];
			}
			set
			{
				base.Fields["RemoveOME"] = value;
			}
		}

		// Token: 0x06006E1B RID: 28187 RVA: 0x001BF130 File Offset: 0x001BD330
		protected override void InternalValidate()
		{
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = RuleIdParameter.GetRuleCollectionRdn(this.ruleCollectionName);
			}
			this.DataObject = (TransportRule)this.ResolveDataObject();
			if (!this.DataObject.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && !((IDirectorySession)base.DataSession).SessionSettings.CurrentOrganizationId.Equals(this.DataObject.OrganizationId))
			{
				base.UnderscopeDataSession(this.DataObject.OrganizationId);
			}
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsChildOfRuleContainer(this.Identity, this.ruleCollectionName))
			{
				throw new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(RuleIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null));
			}
			TransportRule transportRule = null;
			try
			{
				transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(this.DataObject.Xml);
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
			if (transportRule.IsTooAdvancedToParse)
			{
				base.WriteError(new InvalidOperationException(Strings.CannotModifyRuleDueToVersion(transportRule.Name)), ErrorCategory.InvalidOperation, this.Name);
			}
			Exception exception2;
			string target;
			if (!Utils.ValidateParametersForRole(base.Fields, out exception2, out target))
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, target);
			}
			ArgumentException exception3;
			if (!Utils.ValidateRuleComments(this.Comments, out exception3))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, this.Comments);
			}
			if (!Utils.ValidateRestrictedHeaders(base.Fields, true, out exception3, out target))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target);
			}
			string target2;
			if (!Utils.ValidateParameterGroups(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateMessageClassification(base.Fields, out exception3, out target2, base.DataSession))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateContainsWordsPredicate(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateMatchesPatternsPredicate(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateAdAttributePredicate(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidatePropertyContainsWordsPredicates(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
				return;
			}
			Exception exception4;
			if (!Utils.ValidateRecipientIdParameters(base.Fields, base.TenantGlobalCatalogSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), out exception4, out target2))
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateMessageDataClassification(base.Fields, base.CurrentOrganizationId, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateAuditSeverityLevel(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (!Utils.ValidateDlpPolicy(base.DataSession, base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
			if (base.Fields.IsModified("DlpPolicy") && !this.DlpPolicy.Equals(string.Empty))
			{
				ADComplianceProgram adcomplianceProgram = DlpUtils.GetInstalledTenantDlpPolicies(base.DataSession, this.DlpPolicy).First<ADComplianceProgram>();
				this.dlpPolicyId = adcomplianceProgram.ImmutableId;
				this.DlpPolicy = adcomplianceProgram.Name;
			}
			if (base.Fields.IsModified("ApplyRightsProtectionTemplate") && base.Fields["ApplyRightsProtectionTemplate"] != null)
			{
				RmsTemplateDataProvider session = new RmsTemplateDataProvider((IConfigurationSession)base.DataSession);
				string name = (this.ApplyRightsProtectionTemplate != null) ? this.ApplyRightsProtectionTemplate.ToString() : string.Empty;
				RmsTemplatePresentation rmsTemplatePresentation = (RmsTemplatePresentation)base.GetDataObject<RmsTemplatePresentation>(this.ApplyRightsProtectionTemplate, session, null, new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotFound(name)), new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotUnique(name)));
				base.Fields["ResolvedRmsTemplateIdentity"] = rmsTemplatePresentation.Identity;
			}
			if ((base.Fields.IsModified("ApplyOME") && this.ApplyOME) || (base.Fields.IsModified("RemoveOME") && this.RemoveOME))
			{
				IRMConfiguration irmconfiguration = IRMConfiguration.Read((IConfigurationSession)base.DataSession);
				if (irmconfiguration == null || !irmconfiguration.InternalLicensingEnabled)
				{
					base.WriteError(new E4eLicensingIsDisabledExceptionSetRule(), ErrorCategory.InvalidArgument, null);
				}
				if (RmsClientManager.IRMConfig.GetRmsTemplate(((IDirectorySession)base.DataSession).SessionSettings.CurrentOrganizationId, RmsTemplate.InternetConfidential.Id) == null)
				{
					base.WriteError(new E4eRuleRmsTemplateNotFoundException(RmsTemplate.InternetConfidential.Name), ErrorCategory.InvalidArgument, null);
				}
			}
			if (base.Fields.IsModified("GenerateIncidentReport"))
			{
				GenerateIncidentReport generateIncidentReport = (GenerateIncidentReport)transportRule.Actions.FirstOrDefault((Action action) => action.GetType() == typeof(GenerateIncidentReport));
				if (generateIncidentReport != null && generateIncidentReport.IsLegacyFormat(null))
				{
					this.WriteWarning(Strings.LegacyIncludeOriginalMailParameterWillBeUpgraded);
				}
			}
			if (base.Fields.IsModified("RedirectMessageTo") && this.RedirectMessageTo != null)
			{
				int num;
				this.RedirectMessageTo = Utils.RemoveDuplicateRecipients(this.RedirectMessageTo, out num);
				if (num > 0)
				{
					this.WriteWarning(Strings.RemovedDuplicateRecipients(num, "RedirectMessageTo"));
				}
			}
			if (!Utils.ValidateConnectorParameter(base.Fields, base.DataSession, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateSentToScope(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateDomainIsPredicates(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateAttachmentExtensionMatchesWordParameter(base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
				return;
			}
			Rule rule = Rule.CreateFromInternalRule(this.supportedPredicates, this.supportedActions, transportRule, 0, this.DataObject);
			try
			{
				Utils.BuildConditionsAndExceptionsFromParameters(base.Fields, base.TenantGlobalCatalogSession, base.DataSession, rule.UseLegacyRegex, out this.conditionTypesToUpdate, out this.exceptionTypesToUpdate, out this.conditionsSetByParameters, out this.exceptionsSetByParameters);
				this.actionsSetByParameters = Utils.BuildActionsFromParameters(base.Fields, base.TenantGlobalCatalogSession, base.DataSession, out this.actionTypesToUpdate);
			}
			catch (TransientException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidArgument, this.Name);
			}
			catch (DataValidationException exception6)
			{
				base.WriteError(exception6, ErrorCategory.InvalidArgument, this.Name);
			}
			catch (ArgumentException exception7)
			{
				base.WriteError(exception7, ErrorCategory.InvalidArgument, this.Name);
			}
			PredicatesAndActionsWrapper predicatesAndActionsToVerify = this.GetPredicatesAndActionsToVerify();
			if (predicatesAndActionsToVerify.Actions != null)
			{
				if (predicatesAndActionsToVerify.Actions.FirstOrDefault((TransportRuleAction action) => action.GetType() == typeof(GenerateIncidentReportAction)) == null && base.Fields.IsModified("IncidentReportContent") && this.IncidentReportContent != null)
				{
					base.WriteError(new ArgumentException(Strings.InvalidIncidentReportContent), ErrorCategory.InvalidArgument, this.Name);
				}
				if (predicatesAndActionsToVerify.Actions.Any((TransportRuleAction action1) => action1.GetType() == typeof(NotifySenderAction)))
				{
					if (predicatesAndActionsToVerify.Actions.Any((TransportRuleAction action2) => action2.GetType() == typeof(RejectMessageAction)))
					{
						if (this.actionsSetByParameters.Any((TransportRuleAction ac) => ac.GetType() == typeof(NotifySenderAction)))
						{
							this.WriteWarning(Strings.RejectMessageActionIsBeingOverridded);
						}
						else
						{
							this.WriteWarning(Strings.NotifySenderActionIsBeingOverridded);
						}
					}
				}
				ArgumentException exception8;
				if (!Utils.ValidateNotifySender(predicatesAndActionsToVerify.Conditions, predicatesAndActionsToVerify.Exceptions, predicatesAndActionsToVerify.Actions, new Action<LocalizedString>(this.WriteWarning), out exception8))
				{
					base.WriteError(exception8, ErrorCategory.InvalidArgument, this.Name);
				}
				if (Utils.IsNotifySenderIgnoringRejectParameters(base.Fields))
				{
					this.RejectMessageEnhancedStatusCode = null;
					this.RejectMessageReasonText = null;
					this.WriteWarning(Strings.RejectMessageParameterWillBeIgnored);
				}
				foreach (TransportRuleAction transportRuleAction in predicatesAndActionsToVerify.Actions)
				{
					if (transportRuleAction is RejectMessageAction)
					{
						RejectMessageAction rejectMessageAction = (RejectMessageAction)transportRuleAction;
						string text = rejectMessageAction.EnhancedStatusCode.ToString();
						if (!Utils.IsCustomizedDsnConfigured(text))
						{
							this.WriteWarning(Strings.CustomizedDsnNotConfigured(text));
						}
					}
					else if (transportRuleAction is ApplyHtmlDisclaimerAction)
					{
						ApplyHtmlDisclaimerAction applyHtmlDisclaimerAction = (ApplyHtmlDisclaimerAction)transportRuleAction;
						string disclaimerText = applyHtmlDisclaimerAction.Text.ToString();
						string text2 = TransportUtils.CheckForInvalidMacroName(disclaimerText);
						if (!string.IsNullOrEmpty(text2))
						{
							base.WriteError(new ArgumentException(Strings.InvalidDisclaimerMacroName(text2)), ErrorCategory.InvalidArgument, this.Name);
							return;
						}
					}
					else if (transportRuleAction is RightsProtectMessageAction)
					{
						RmsTemplateDataProvider session2 = new RmsTemplateDataProvider((IConfigurationSession)base.DataSession);
						RightsProtectMessageAction rightsProtectMessageAction = (RightsProtectMessageAction)transportRuleAction;
						RmsTemplateIdentity template = rightsProtectMessageAction.Template;
						base.GetDataObject<RmsTemplatePresentation>(new RmsTemplateIdParameter(template), session2, null, new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotFound(template.TemplateName)), new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotUnique(template.TemplateName)));
						base.Fields["ResolvedRmsTemplateIdentity"] = template;
					}
					if (!Utils.ValidateSingletonAction(predicatesAndActionsToVerify.Actions) && Utils.ActionWhichMustBeSingleton.ContainsKey(transportRuleAction.GetType()))
					{
						base.WriteError(new ArgumentException(Utils.ActionWhichMustBeSingleton[transportRuleAction.GetType()]), ErrorCategory.InvalidArgument, this.Name);
						return;
					}
				}
			}
			Utils.ValidateTransportRuleRegexCpuTimeLimit((IConfigurationSession)base.DataSession, base.Fields);
			if (!Utils.ValidateActivationAndExpiryDates(new Action<LocalizedString>(this.WriteWarning), transportRule, base.Fields, out exception3, out target2))
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, target2);
			}
		}

		// Token: 0x06006E1C RID: 28188 RVA: 0x001BFADC File Offset: 0x001BDCDC
		protected override void InternalProcessRecord()
		{
			ADRuleStorageManager adruleStorageManager;
			try
			{
				IConfigDataProvider session = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
				adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, session);
			}
			catch (RuleCollectionNotInAdException)
			{
				base.WriteError(new ArgumentException(Strings.RuleNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, this.Identity);
				return;
			}
			try
			{
				if (base.Fields.IsModified("Priority"))
				{
					this.SetRuleWithPriorityChange(adruleStorageManager);
				}
				else
				{
					this.SetRuleWithoutPriorityChange(adruleStorageManager);
				}
				if (Utils.Exchange12HubServersExist(this))
				{
					this.WriteWarning(Strings.SetRuleSyncAcrossDifferentVersionsNeeded);
				}
			}
			catch (ParserException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x06006E1D RID: 28189 RVA: 0x001BFB94 File Offset: 0x001BDD94
		private PredicatesAndActionsWrapper GetPredicatesAndActionsToVerify()
		{
			if (this.Actions != null)
			{
				return new PredicatesAndActionsWrapper(this.Conditions, this.Exceptions, this.Actions);
			}
			TransportRule transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(this.DataObject.Xml);
			Rule rule = Rule.CreateFromInternalRule(this.supportedPredicates, this.supportedActions, transportRule, 0, this.DataObject);
			try
			{
				this.UpdateRuleFromParameters(rule);
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, this.Name);
			}
			TransportRuleAction[] actions = rule.Actions;
			TransportRulePredicate[] conditions = this.Conditions ?? rule.Conditions;
			TransportRulePredicate[] exceptions = this.Exceptions ?? rule.Exceptions;
			if (!base.Fields.IsModified("RuleErrorAction"))
			{
				this.RuleErrorAction = transportRule.ErrorAction;
			}
			if (!base.Fields.IsModified("SenderAddressLocation"))
			{
				this.SenderAddressLocation = transportRule.SenderAddressLocation;
			}
			if (!base.Fields.IsModified("RuleSubType"))
			{
				this.RuleSubType = transportRule.SubType;
			}
			ArgumentException exception2;
			if (!Utils.ValidateSubtypes(this.RuleSubType, conditions, exceptions, actions, out exception2))
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, this.Name);
			}
			return new PredicatesAndActionsWrapper(conditions, exceptions, actions);
		}

		// Token: 0x06006E1E RID: 28190 RVA: 0x001BFCD4 File Offset: 0x001BDED4
		private void SetRuleWithPriorityChange(ADRuleStorageManager storedRules)
		{
			storedRules.LoadRuleCollection();
			TransportRule transportRule;
			int priority;
			storedRules.GetRule(this.DataObject.Identity, out transportRule, out priority);
			TransportRule dataObject = this.DataObject;
			if (transportRule == null)
			{
				base.WriteError(new ArgumentException(Strings.RuleNotFound(this.Identity.ToString()), "Identity"), ErrorCategory.InvalidArgument, this.Identity);
				return;
			}
			Rule rule = Rule.CreateFromInternalRule(this.supportedPredicates, this.supportedActions, transportRule, priority, dataObject);
			if (rule.ManuallyModified)
			{
				if (!this.UpdateManuallyModifiedInternalRuleFromParameters(transportRule))
				{
					return;
				}
				rule.Priority = this.Priority;
			}
			else
			{
				try
				{
					this.UpdateRuleFromParameters(rule);
					transportRule = rule.ToInternalRule();
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
					return;
				}
				catch (RulesValidationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			try
			{
				OrganizationId organizationId = this.DataObject.OrganizationId;
				if (organizationId != OrganizationId.ForestWideOrgId)
				{
					InvalidOperationException ex = Utils.CheckRuleForOrganizationLimits((IConfigurationSession)base.DataSession, base.TenantGlobalCatalogSession, storedRules, organizationId, transportRule, false);
					if (ex != null)
					{
						base.WriteError(ex, ErrorCategory.InvalidOperation, this.Name);
						return;
					}
				}
				storedRules.UpdateRule(transportRule, rule.Identity, rule.Priority);
			}
			catch (RulesValidationException)
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExist, "Name"), ErrorCategory.InvalidArgument, this.Name);
			}
			catch (InvalidPriorityException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06006E1F RID: 28191 RVA: 0x001BFE64 File Offset: 0x001BE064
		private void SetRuleWithoutPriorityChange(ADRuleStorageManager storedRules)
		{
			TransportRule transportRule = (TransportRule)TransportRuleParser.Instance.GetRule(this.DataObject.Xml);
			Rule rule = Rule.CreateFromInternalRule(this.supportedPredicates, this.supportedActions, transportRule, -1, this.DataObject);
			string name = rule.Name;
			if (rule.ManuallyModified)
			{
				transportRule.Name = rule.Name;
				if (!this.UpdateManuallyModifiedInternalRuleFromParameters(transportRule))
				{
					return;
				}
			}
			else
			{
				try
				{
					this.UpdateRuleFromParameters(rule);
					transportRule = rule.ToInternalRule();
				}
				catch (ArgumentException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
					return;
				}
				catch (RulesValidationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			OrganizationId organizationId = this.DataObject.OrganizationId;
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				storedRules.LoadRuleCollection();
				InvalidOperationException ex = Utils.CheckRuleForOrganizationLimits((IConfigurationSession)base.DataSession, base.TenantGlobalCatalogSession, storedRules, organizationId, transportRule, false);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, this.Name);
					return;
				}
			}
			string xml = TransportRuleSerializer.Instance.SaveRuleToString(transportRule);
			this.DataObject.Xml = xml;
			if (!storedRules.CanRename((ADObjectId)this.DataObject.Identity, name, transportRule.Name))
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExist, "Name"), ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			this.DataObject.Name = transportRule.Name;
			storedRules.UpdateRule(this.DataObject);
		}

		// Token: 0x06006E20 RID: 28192 RVA: 0x001BFFDC File Offset: 0x001BE1DC
		private void UpdateRuleFromParameters(Rule rule)
		{
			if (this.Name != null)
			{
				rule.Name = this.Name;
			}
			if (base.Fields.IsModified("Priority"))
			{
				rule.Priority = this.Priority;
			}
			if (this.Comments != null)
			{
				rule.Comments = this.Comments;
			}
			if (base.Fields.IsModified("RuleErrorAction"))
			{
				rule.RuleErrorAction = this.RuleErrorAction;
			}
			if (base.Fields.IsModified("SenderAddressLocation"))
			{
				rule.SenderAddressLocation = this.SenderAddressLocation;
			}
			if (base.Fields.IsModified("RuleSubType"))
			{
				rule.RuleSubType = this.RuleSubType;
			}
			if (base.Fields.IsModified("ActivationDate"))
			{
				rule.ActivationDate = ((this.ActivationDate != null && this.ActivationDate.Value.ToUniversalTime() < DateTime.UtcNow) ? null : this.ActivationDate);
			}
			if (base.Fields.IsModified("ExpiryDate"))
			{
				rule.ExpiryDate = this.ExpiryDate;
			}
			if (this.DlpPolicy != null)
			{
				if (this.DlpPolicy.Equals(string.Empty))
				{
					rule.DlpPolicy = null;
					rule.DlpPolicyId = Guid.Empty;
				}
				else
				{
					rule.DlpPolicyId = this.dlpPolicyId;
					ADComplianceProgram adcomplianceProgram = DlpUtils.GetInstalledTenantDlpPolicies(base.DataSession, this.DlpPolicy).First<ADComplianceProgram>();
					rule.DlpPolicy = adcomplianceProgram.Name;
					Tuple<RuleState, RuleMode> tuple = DlpUtils.DlpStateToRuleState(adcomplianceProgram.State);
					rule.State = tuple.Item1;
					if (base.Fields.IsModified("Mode") && rule.Mode != tuple.Item2)
					{
						this.WriteWarning(Strings.DlpPolicyModeIsOverridenByModeParameter(this.Mode.ToString(), tuple.Item2.ToString()));
					}
					if (!base.Fields.IsModified("Mode"))
					{
						rule.Mode = tuple.Item2;
					}
				}
			}
			if (base.Fields.IsModified("Mode"))
			{
				rule.Mode = this.Mode;
			}
			if (this.Conditions != null)
			{
				rule.Conditions = this.Conditions;
			}
			else
			{
				List<TransportRulePredicate> list = new List<TransportRulePredicate>();
				if (rule.Conditions != null)
				{
					foreach (TransportRulePredicate transportRulePredicate in rule.Conditions)
					{
						if (!this.conditionTypesToUpdate.Contains(transportRulePredicate.GetType()))
						{
							Utils.InsertPredicateSorted(transportRulePredicate, list);
						}
					}
				}
				foreach (TransportRulePredicate predicate in this.conditionsSetByParameters)
				{
					Utils.InsertPredicateSorted(predicate, list);
				}
				if (list.Count > 0)
				{
					rule.Conditions = list.ToArray();
				}
				else
				{
					rule.Conditions = null;
				}
			}
			if (this.Exceptions != null)
			{
				rule.Exceptions = this.Exceptions;
			}
			else
			{
				List<TransportRulePredicate> list2 = new List<TransportRulePredicate>();
				if (rule.Exceptions != null)
				{
					foreach (TransportRulePredicate transportRulePredicate2 in rule.Exceptions)
					{
						if (!this.exceptionTypesToUpdate.Contains(transportRulePredicate2.GetType()))
						{
							Utils.InsertPredicateSorted(transportRulePredicate2, list2);
						}
					}
				}
				foreach (TransportRulePredicate predicate2 in this.exceptionsSetByParameters)
				{
					Utils.InsertPredicateSorted(predicate2, list2);
				}
				if (list2.Count > 0)
				{
					rule.Exceptions = list2.ToArray();
				}
				else
				{
					rule.Exceptions = null;
				}
			}
			if (this.Actions != null)
			{
				rule.Actions = this.Actions;
				return;
			}
			SetTransportRule.UpdateActionsFromParameters(this.actionsSetByParameters, this.actionTypesToUpdate, rule);
		}

		// Token: 0x06006E21 RID: 28193 RVA: 0x001C038C File Offset: 0x001BE58C
		internal static void UpdateActionsFromParameters(IEnumerable<TransportRuleAction> actionsSetByParameters, List<Type> actionsToBeUpdated, Rule rule)
		{
			List<TransportRuleAction> list = new List<TransportRuleAction>();
			if (rule.Actions != null)
			{
				foreach (TransportRuleAction transportRuleAction in rule.Actions)
				{
					if (!actionsToBeUpdated.Contains(transportRuleAction.GetType()) && !SetTransportRule.IsActionOverriden(transportRuleAction, actionsSetByParameters))
					{
						Utils.InsertActionSorted(transportRuleAction, list);
					}
				}
			}
			foreach (TransportRuleAction action in actionsSetByParameters)
			{
				Utils.InsertActionSorted(action, list);
			}
			if (list.Count > 0)
			{
				rule.Actions = list.ToArray();
			}
		}

		// Token: 0x06006E22 RID: 28194 RVA: 0x001C0438 File Offset: 0x001BE638
		private bool UpdateManuallyModifiedInternalRuleFromParameters(TransportRule rule)
		{
			if (this.Name != null)
			{
				rule.Name = this.Name;
			}
			if (this.Comments != null)
			{
				rule.Comments = this.Comments;
			}
			if (this.Conditions != null)
			{
				base.WriteError(new ArgumentException(Strings.CannotEditManuallyModifiedRule, "Conditions"), ErrorCategory.InvalidArgument, this.Conditions);
				return false;
			}
			if (this.Exceptions != null)
			{
				base.WriteError(new ArgumentException(Strings.CannotEditManuallyModifiedRule, "Exceptions"), ErrorCategory.InvalidArgument, this.Exceptions);
				return false;
			}
			if (this.Actions != null)
			{
				base.WriteError(new ArgumentException(Strings.CannotEditManuallyModifiedRule, "Actions"), ErrorCategory.InvalidArgument, this.Actions);
				return false;
			}
			return true;
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x001C04EF File Offset: 0x001BE6EF
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception) || exception is ValidationArgumentException;
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x001C053C File Offset: 0x001BE73C
		internal static bool IsActionOverriden(TransportRuleAction action, IEnumerable<TransportRuleAction> actionsSetByParameters)
		{
			bool flag = actionsSetByParameters.Any((TransportRuleAction ac) => ac.GetType() == typeof(NotifySenderAction));
			if (action.GetType() == typeof(RejectMessageAction) && flag)
			{
				return true;
			}
			bool flag2 = actionsSetByParameters.Any((TransportRuleAction ac) => ac.GetType() == typeof(RejectMessageAction));
			return action.GetType() == typeof(NotifySenderAction) && flag2;
		}

		// Token: 0x04003873 RID: 14451
		private readonly string ruleCollectionName;

		// Token: 0x04003874 RID: 14452
		private readonly TypeMapping[] supportedPredicates;

		// Token: 0x04003875 RID: 14453
		private readonly TypeMapping[] supportedActions;

		// Token: 0x04003876 RID: 14454
		private TransportRulePredicate[] conditionsSetByParameters;

		// Token: 0x04003877 RID: 14455
		private TransportRulePredicate[] exceptionsSetByParameters;

		// Token: 0x04003878 RID: 14456
		private TransportRuleAction[] actionsSetByParameters;

		// Token: 0x04003879 RID: 14457
		private List<Type> conditionTypesToUpdate;

		// Token: 0x0400387A RID: 14458
		private List<Type> exceptionTypesToUpdate;

		// Token: 0x0400387B RID: 14459
		private List<Type> actionTypesToUpdate;

		// Token: 0x0400387C RID: 14460
		private Guid dlpPolicyId;
	}
}
