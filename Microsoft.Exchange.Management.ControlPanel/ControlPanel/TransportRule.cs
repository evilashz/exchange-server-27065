using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000456 RID: 1110
	[DataContract]
	[KnownType(typeof(TransportRule))]
	public class TransportRule : RuleRow
	{
		// Token: 0x0600364E RID: 13902 RVA: 0x000A7D44 File Offset: 0x000A5F44
		public TransportRule(Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule rule) : base(rule)
		{
			this.Rule = rule;
			base.DescriptionObject = rule.Description;
			base.ConditionDescriptions = base.DescriptionObject.ConditionDescriptions.ToArray();
			base.ActionDescriptions = base.DescriptionObject.ActionDescriptions.ToArray();
			base.ExceptionDescriptions = base.DescriptionObject.ExceptionDescriptions.ToArray();
			base.ExpiryDateDescription = base.DescriptionObject.RuleDescriptionExpiry;
			base.ActivationDateDescription = base.DescriptionObject.RuleDescriptionActivation;
		}

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000A7DCF File Offset: 0x000A5FCF
		// (set) Token: 0x06003650 RID: 13904 RVA: 0x000A7DD7 File Offset: 0x000A5FD7
		public Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Rule Rule { get; private set; }

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000A7DE0 File Offset: 0x000A5FE0
		// (set) Token: 0x06003652 RID: 13906 RVA: 0x000A7DED File Offset: 0x000A5FED
		[DataMember]
		public DateTime? ActivationDate
		{
			get
			{
				return this.Rule.ActivationDate;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000A7DF4 File Offset: 0x000A5FF4
		// (set) Token: 0x06003654 RID: 13908 RVA: 0x000A7E01 File Offset: 0x000A6001
		[DataMember]
		public DateTime? ExpiryDate
		{
			get
			{
				return this.Rule.ExpiryDate;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000A7E08 File Offset: 0x000A6008
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x000A7E1F File Offset: 0x000A601F
		[DataMember]
		public string Mode
		{
			get
			{
				return this.Rule.Mode.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x000A7E26 File Offset: 0x000A6026
		// (set) Token: 0x06003658 RID: 13912 RVA: 0x000A7E38 File Offset: 0x000A6038
		[DataMember]
		public string Comments
		{
			get
			{
				return this.Rule.Comments.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000A7E3F File Offset: 0x000A603F
		// (set) Token: 0x0600365A RID: 13914 RVA: 0x000A7E56 File Offset: 0x000A6056
		[DataMember]
		public string RuleErrorAction
		{
			get
			{
				return this.Rule.RuleErrorAction.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000A7E5D File Offset: 0x000A605D
		// (set) Token: 0x0600365C RID: 13916 RVA: 0x000A7E74 File Offset: 0x000A6074
		[DataMember]
		public string SenderAddressLocation
		{
			get
			{
				return this.Rule.SenderAddressLocation.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000A7E7B File Offset: 0x000A607B
		// (set) Token: 0x0600365E RID: 13918 RVA: 0x000A7E8D File Offset: 0x000A608D
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] From
		{
			get
			{
				return this.Rule.From.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000A7E94 File Offset: 0x000A6094
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x000A7EA6 File Offset: 0x000A60A6
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] FromMemberOf
		{
			get
			{
				return this.Rule.FromMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x000A7EAD File Offset: 0x000A60AD
		// (set) Token: 0x06003662 RID: 13922 RVA: 0x000A7EC4 File Offset: 0x000A60C4
		[DataMember(EmitDefaultValue = false)]
		public string FromScope
		{
			get
			{
				return this.Rule.FromScope.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000A7ECB File Offset: 0x000A60CB
		// (set) Token: 0x06003664 RID: 13924 RVA: 0x000A7EDD File Offset: 0x000A60DD
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] SentTo
		{
			get
			{
				return this.Rule.SentTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000A7EE4 File Offset: 0x000A60E4
		// (set) Token: 0x06003666 RID: 13926 RVA: 0x000A7EF6 File Offset: 0x000A60F6
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] SentToMemberOf
		{
			get
			{
				return this.Rule.SentToMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000A7EFD File Offset: 0x000A60FD
		// (set) Token: 0x06003668 RID: 13928 RVA: 0x000A7F14 File Offset: 0x000A6114
		[DataMember(EmitDefaultValue = false)]
		public string SentToScope
		{
			get
			{
				return this.Rule.SentToScope.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000A7F1B File Offset: 0x000A611B
		// (set) Token: 0x0600366A RID: 13930 RVA: 0x000A7F2D File Offset: 0x000A612D
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] BetweenMemberOf1
		{
			get
			{
				return this.Rule.BetweenMemberOf1.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000A7F34 File Offset: 0x000A6134
		// (set) Token: 0x0600366C RID: 13932 RVA: 0x000A7F46 File Offset: 0x000A6146
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] BetweenMemberOf2
		{
			get
			{
				return this.Rule.BetweenMemberOf2.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x000A7F4D File Offset: 0x000A614D
		// (set) Token: 0x0600366E RID: 13934 RVA: 0x000A7F5F File Offset: 0x000A615F
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ManagerAddresses
		{
			get
			{
				return this.Rule.ManagerAddresses.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000A7F66 File Offset: 0x000A6166
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x000A7F7D File Offset: 0x000A617D
		[DataMember(EmitDefaultValue = false)]
		public string ManagerForEvaluatedUser
		{
			get
			{
				return this.Rule.ManagerForEvaluatedUser.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000A7F84 File Offset: 0x000A6184
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x000A7F9B File Offset: 0x000A619B
		[DataMember(EmitDefaultValue = false)]
		public string SenderManagementRelationship
		{
			get
			{
				return this.Rule.SenderManagementRelationship.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000A7FA2 File Offset: 0x000A61A2
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x000A7FB9 File Offset: 0x000A61B9
		[DataMember(EmitDefaultValue = false)]
		public string ADComparisonAttribute
		{
			get
			{
				return this.Rule.ADComparisonAttribute.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000A7FC0 File Offset: 0x000A61C0
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x000A7FD7 File Offset: 0x000A61D7
		[DataMember(EmitDefaultValue = false)]
		public string ADComparisonOperator
		{
			get
			{
				return this.Rule.ADComparisonOperator.ToStringWithNull();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000A7FDE File Offset: 0x000A61DE
		// (set) Token: 0x06003678 RID: 13944 RVA: 0x000A7FF0 File Offset: 0x000A61F0
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AnyOfToHeader
		{
			get
			{
				return this.Rule.AnyOfToHeader.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000A7FF7 File Offset: 0x000A61F7
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x000A8009 File Offset: 0x000A6209
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AnyOfToHeaderMemberOf
		{
			get
			{
				return this.Rule.AnyOfToHeaderMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000A8010 File Offset: 0x000A6210
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x000A8022 File Offset: 0x000A6222
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AnyOfCcHeader
		{
			get
			{
				return this.Rule.AnyOfCcHeader.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000A8029 File Offset: 0x000A6229
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000A803B File Offset: 0x000A623B
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AnyOfCcHeaderMemberOf
		{
			get
			{
				return this.Rule.AnyOfCcHeaderMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000A8042 File Offset: 0x000A6242
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000A8054 File Offset: 0x000A6254
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AnyOfToCcHeader
		{
			get
			{
				return this.Rule.AnyOfToCcHeader.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000A805B File Offset: 0x000A625B
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x000A806D File Offset: 0x000A626D
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AnyOfToCcHeaderMemberOf
		{
			get
			{
				return this.Rule.AnyOfToCcHeaderMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000A8074 File Offset: 0x000A6274
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x000A8086 File Offset: 0x000A6286
		[DataMember(EmitDefaultValue = false)]
		public Identity HasClassification
		{
			get
			{
				return this.Rule.HasClassification.ToIdentity();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000A8090 File Offset: 0x000A6290
		// (set) Token: 0x06003686 RID: 13958 RVA: 0x000A80C1 File Offset: 0x000A62C1
		[DataMember(EmitDefaultValue = false)]
		public Identity SenderInRecipientList
		{
			get
			{
				string[] array = this.Rule.SenderInRecipientList.ToStringArray();
				if (array == null)
				{
					return null;
				}
				string text = array.ToCommaSeperatedString();
				return new Identity(text, text);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x000A80C8 File Offset: 0x000A62C8
		// (set) Token: 0x06003688 RID: 13960 RVA: 0x000A80F9 File Offset: 0x000A62F9
		[DataMember(EmitDefaultValue = false)]
		public Identity RecipientInSenderList
		{
			get
			{
				string[] array = this.Rule.RecipientInSenderList.ToStringArray();
				if (array == null)
				{
					return null;
				}
				string text = array.ToCommaSeperatedString();
				return new Identity(text, text);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x000A8100 File Offset: 0x000A6300
		// (set) Token: 0x0600368A RID: 13962 RVA: 0x000A8112 File Offset: 0x000A6312
		[DataMember(EmitDefaultValue = false)]
		public string[] SubjectContainsWords
		{
			get
			{
				return this.Rule.SubjectContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x000A8119 File Offset: 0x000A6319
		// (set) Token: 0x0600368C RID: 13964 RVA: 0x000A812B File Offset: 0x000A632B
		[DataMember(EmitDefaultValue = false)]
		public string[] SubjectOrBodyContainsWords
		{
			get
			{
				return this.Rule.SubjectOrBodyContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000A8132 File Offset: 0x000A6332
		// (set) Token: 0x0600368E RID: 13966 RVA: 0x000A8149 File Offset: 0x000A6349
		[DataMember(EmitDefaultValue = false)]
		public string HeaderContainsMessageHeader
		{
			get
			{
				return this.Rule.HeaderContainsMessageHeader.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000A8150 File Offset: 0x000A6350
		// (set) Token: 0x06003690 RID: 13968 RVA: 0x000A8162 File Offset: 0x000A6362
		[DataMember(EmitDefaultValue = false)]
		public string[] HeaderContainsWords
		{
			get
			{
				return this.Rule.HeaderContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x000A8169 File Offset: 0x000A6369
		// (set) Token: 0x06003692 RID: 13970 RVA: 0x000A817B File Offset: 0x000A637B
		[DataMember(EmitDefaultValue = false)]
		public string[] FromAddressContainsWords
		{
			get
			{
				return this.Rule.FromAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000A8182 File Offset: 0x000A6382
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x000A8194 File Offset: 0x000A6394
		[DataMember(EmitDefaultValue = false)]
		public string[] RecipientAddressContainsWords
		{
			get
			{
				return this.Rule.RecipientAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x000A819B File Offset: 0x000A639B
		// (set) Token: 0x06003696 RID: 13974 RVA: 0x000A81AD File Offset: 0x000A63AD
		[DataMember(EmitDefaultValue = false)]
		public string[] AnyOfRecipientAddressContainsWords
		{
			get
			{
				return this.Rule.AnyOfRecipientAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000A81B4 File Offset: 0x000A63B4
		// (set) Token: 0x06003698 RID: 13976 RVA: 0x000A81C6 File Offset: 0x000A63C6
		[DataMember(EmitDefaultValue = false)]
		public string[] AttachmentContainsWords
		{
			get
			{
				return this.Rule.AttachmentContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000A81D0 File Offset: 0x000A63D0
		// (set) Token: 0x0600369A RID: 13978 RVA: 0x000A81FA File Offset: 0x000A63FA
		[DataMember(EmitDefaultValue = false)]
		public bool? HasNoClassification
		{
			get
			{
				if (!this.Rule.HasNoClassification)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000A8204 File Offset: 0x000A6404
		// (set) Token: 0x0600369C RID: 13980 RVA: 0x000A822E File Offset: 0x000A642E
		[DataMember(EmitDefaultValue = false)]
		public bool? AttachmentIsUnsupported
		{
			get
			{
				if (!this.Rule.AttachmentIsUnsupported)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000A8235 File Offset: 0x000A6435
		// (set) Token: 0x0600369E RID: 13982 RVA: 0x000A8247 File Offset: 0x000A6447
		[DataMember(EmitDefaultValue = false)]
		public string[] SenderADAttributeContainsWords
		{
			get
			{
				return this.Rule.SenderADAttributeContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x000A824E File Offset: 0x000A644E
		// (set) Token: 0x060036A0 RID: 13984 RVA: 0x000A8260 File Offset: 0x000A6460
		[DataMember(EmitDefaultValue = false)]
		public string[] RecipientADAttributeContainsWords
		{
			get
			{
				return this.Rule.RecipientADAttributeContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000A8267 File Offset: 0x000A6467
		// (set) Token: 0x060036A2 RID: 13986 RVA: 0x000A828E File Offset: 0x000A648E
		[DataMember(EmitDefaultValue = false)]
		public Hashtable[] MessageContainsDataClassifications
		{
			get
			{
				if (this.Rule.MessageContainsDataClassifications == null)
				{
					return null;
				}
				return this.ParseDataClassifications(this.Rule.MessageContainsDataClassifications.ToStringArray());
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x000A8295 File Offset: 0x000A6495
		// (set) Token: 0x060036A4 RID: 13988 RVA: 0x000A82A7 File Offset: 0x000A64A7
		[DataMember(EmitDefaultValue = false)]
		public string[] SenderDomainIs
		{
			get
			{
				return this.Rule.SenderDomainIs.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x000A82AE File Offset: 0x000A64AE
		// (set) Token: 0x060036A6 RID: 13990 RVA: 0x000A82C0 File Offset: 0x000A64C0
		[DataMember(EmitDefaultValue = false)]
		public string[] RecipientDomainIs
		{
			get
			{
				return this.Rule.RecipientDomainIs.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x000A82C7 File Offset: 0x000A64C7
		// (set) Token: 0x060036A8 RID: 13992 RVA: 0x000A82D9 File Offset: 0x000A64D9
		[DataMember(EmitDefaultValue = false)]
		public string[] ContentCharacterSetContainsWords
		{
			get
			{
				return this.Rule.ContentCharacterSetContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000A82E0 File Offset: 0x000A64E0
		// (set) Token: 0x060036AA RID: 13994 RVA: 0x000A82F2 File Offset: 0x000A64F2
		[DataMember(EmitDefaultValue = false)]
		public string[] SubjectMatchesPatterns
		{
			get
			{
				return this.Rule.SubjectMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000A82F9 File Offset: 0x000A64F9
		// (set) Token: 0x060036AC RID: 13996 RVA: 0x000A830B File Offset: 0x000A650B
		[DataMember(EmitDefaultValue = false)]
		public string[] SubjectOrBodyMatchesPatterns
		{
			get
			{
				return this.Rule.SubjectOrBodyMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x000A8312 File Offset: 0x000A6512
		// (set) Token: 0x060036AE RID: 13998 RVA: 0x000A8329 File Offset: 0x000A6529
		[DataMember(EmitDefaultValue = false)]
		public string HeaderMatchesMessageHeader
		{
			get
			{
				return this.Rule.HeaderMatchesMessageHeader.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000A8330 File Offset: 0x000A6530
		// (set) Token: 0x060036B0 RID: 14000 RVA: 0x000A8342 File Offset: 0x000A6542
		[DataMember(EmitDefaultValue = false)]
		public string[] HeaderMatchesPatterns
		{
			get
			{
				return this.Rule.HeaderMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x000A8349 File Offset: 0x000A6549
		// (set) Token: 0x060036B2 RID: 14002 RVA: 0x000A835B File Offset: 0x000A655B
		[DataMember(EmitDefaultValue = false)]
		public string[] FromAddressMatchesPatterns
		{
			get
			{
				return this.Rule.FromAddressMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x000A8362 File Offset: 0x000A6562
		// (set) Token: 0x060036B4 RID: 14004 RVA: 0x000A8374 File Offset: 0x000A6574
		[DataMember(EmitDefaultValue = false)]
		public string[] AttachmentNameMatchesPatterns
		{
			get
			{
				return this.Rule.AttachmentNameMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x000A837B File Offset: 0x000A657B
		// (set) Token: 0x060036B6 RID: 14006 RVA: 0x000A838D File Offset: 0x000A658D
		[DataMember(EmitDefaultValue = false)]
		public string[] AttachmentMatchesPatterns
		{
			get
			{
				return this.Rule.AttachmentMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000A8394 File Offset: 0x000A6594
		// (set) Token: 0x060036B8 RID: 14008 RVA: 0x000A83A6 File Offset: 0x000A65A6
		[DataMember(EmitDefaultValue = false)]
		public string[] RecipientAddressMatchesPatterns
		{
			get
			{
				return this.Rule.RecipientAddressMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x000A83AD File Offset: 0x000A65AD
		// (set) Token: 0x060036BA RID: 14010 RVA: 0x000A83BF File Offset: 0x000A65BF
		[DataMember(EmitDefaultValue = false)]
		public string[] AnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return this.Rule.AnyOfRecipientAddressMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x000A83C6 File Offset: 0x000A65C6
		// (set) Token: 0x060036BC RID: 14012 RVA: 0x000A83D8 File Offset: 0x000A65D8
		[DataMember(EmitDefaultValue = false)]
		public string[] SenderADAttributeMatchesPatterns
		{
			get
			{
				return this.Rule.SenderADAttributeMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x000A83DF File Offset: 0x000A65DF
		// (set) Token: 0x060036BE RID: 14014 RVA: 0x000A83F1 File Offset: 0x000A65F1
		[DataMember(EmitDefaultValue = false)]
		public string[] RecipientADAttributeMatchesPatterns
		{
			get
			{
				return this.Rule.RecipientADAttributeMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000A83F8 File Offset: 0x000A65F8
		// (set) Token: 0x060036C0 RID: 14016 RVA: 0x000A840A File Offset: 0x000A660A
		[DataMember(EmitDefaultValue = false)]
		public string[] AttachmentExtensionMatchesWords
		{
			get
			{
				return this.Rule.AttachmentExtensionMatchesWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000A8411 File Offset: 0x000A6611
		// (set) Token: 0x060036C2 RID: 14018 RVA: 0x000A8423 File Offset: 0x000A6623
		[DataMember(EmitDefaultValue = false)]
		public string[] SenderIpRanges
		{
			get
			{
				return this.Rule.SenderIpRanges.ToStringArray<IPRange>();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000A842C File Offset: 0x000A662C
		// (set) Token: 0x060036C4 RID: 14020 RVA: 0x000A8471 File Offset: 0x000A6671
		[DataMember(EmitDefaultValue = false)]
		public string SCLOver
		{
			get
			{
				if (this.Rule.SCLOver != null)
				{
					return this.Rule.SCLOver.Value.ToString();
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000A8478 File Offset: 0x000A6678
		// (set) Token: 0x060036C6 RID: 14022 RVA: 0x000A84C4 File Offset: 0x000A66C4
		[DataMember(EmitDefaultValue = false)]
		public long? AttachmentSizeOver
		{
			get
			{
				if (this.Rule.AttachmentSizeOver != null)
				{
					return new long?((long)this.Rule.AttachmentSizeOver.Value.ToKB());
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000A84CB File Offset: 0x000A66CB
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x000A84D8 File Offset: 0x000A66D8
		[DataMember(EmitDefaultValue = false)]
		public bool AttachmentProcessingLimitExceeded
		{
			get
			{
				return this.Rule.AttachmentProcessingLimitExceeded;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000A84DF File Offset: 0x000A66DF
		// (set) Token: 0x060036CA RID: 14026 RVA: 0x000A84F6 File Offset: 0x000A66F6
		[DataMember(EmitDefaultValue = false)]
		public string WithImportance
		{
			get
			{
				return this.Rule.WithImportance.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000A84FD File Offset: 0x000A66FD
		// (set) Token: 0x060036CC RID: 14028 RVA: 0x000A8514 File Offset: 0x000A6714
		[DataMember(EmitDefaultValue = false)]
		public string MessageTypeMatches
		{
			get
			{
				return this.Rule.MessageTypeMatches.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x000A851C File Offset: 0x000A671C
		// (set) Token: 0x060036CE RID: 14030 RVA: 0x000A8568 File Offset: 0x000A6768
		[DataMember(EmitDefaultValue = false)]
		public long? MessageSizeOver
		{
			get
			{
				if (this.Rule.MessageSizeOver != null)
				{
					return new long?((long)this.Rule.MessageSizeOver.Value.ToKB());
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x000A856F File Offset: 0x000A676F
		// (set) Token: 0x060036D0 RID: 14032 RVA: 0x000A857C File Offset: 0x000A677C
		[DataMember(EmitDefaultValue = false)]
		public bool HasSenderOverride
		{
			get
			{
				return this.Rule.HasSenderOverride;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000A8583 File Offset: 0x000A6783
		// (set) Token: 0x060036D2 RID: 14034 RVA: 0x000A8590 File Offset: 0x000A6790
		[DataMember(EmitDefaultValue = false)]
		public bool AttachmentHasExecutableContent
		{
			get
			{
				return this.Rule.AttachmentHasExecutableContent;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x060036D3 RID: 14035 RVA: 0x000A8597 File Offset: 0x000A6797
		// (set) Token: 0x060036D4 RID: 14036 RVA: 0x000A85A4 File Offset: 0x000A67A4
		[DataMember(EmitDefaultValue = false)]
		public bool AttachmentIsPasswordProtected
		{
			get
			{
				return this.Rule.AttachmentIsPasswordProtected;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000A85AB File Offset: 0x000A67AB
		// (set) Token: 0x060036D6 RID: 14038 RVA: 0x000A85BD File Offset: 0x000A67BD
		[DataMember(EmitDefaultValue = false)]
		public string PrependSubject
		{
			get
			{
				return this.Rule.PrependSubject.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x060036D7 RID: 14039 RVA: 0x000A85C4 File Offset: 0x000A67C4
		// (set) Token: 0x060036D8 RID: 14040 RVA: 0x000A85D6 File Offset: 0x000A67D6
		[DataMember(EmitDefaultValue = false)]
		public Identity ApplyClassification
		{
			get
			{
				return this.Rule.ApplyClassification.ToIdentity();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000A85DD File Offset: 0x000A67DD
		// (set) Token: 0x060036DA RID: 14042 RVA: 0x000A85F4 File Offset: 0x000A67F4
		[DataMember(EmitDefaultValue = false)]
		public string ApplyHtmlDisclaimerLocation
		{
			get
			{
				return this.Rule.ApplyHtmlDisclaimerLocation.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000A85FB File Offset: 0x000A67FB
		// (set) Token: 0x060036DC RID: 14044 RVA: 0x000A8612 File Offset: 0x000A6812
		[DataMember(EmitDefaultValue = false)]
		public string ApplyHtmlDisclaimerText
		{
			get
			{
				return this.Rule.ApplyHtmlDisclaimerText.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x000A8619 File Offset: 0x000A6819
		// (set) Token: 0x060036DE RID: 14046 RVA: 0x000A8630 File Offset: 0x000A6830
		[DataMember(EmitDefaultValue = false)]
		public string ApplyHtmlDisclaimerFallbackAction
		{
			get
			{
				return this.Rule.ApplyHtmlDisclaimerFallbackAction.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x000A8638 File Offset: 0x000A6838
		// (set) Token: 0x060036E0 RID: 14048 RVA: 0x000A867D File Offset: 0x000A687D
		[DataMember(EmitDefaultValue = false)]
		public string SetSCL
		{
			get
			{
				if (this.Rule.SetSCL != null)
				{
					return this.Rule.SetSCL.Value.ToString();
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x000A8684 File Offset: 0x000A6884
		// (set) Token: 0x060036E2 RID: 14050 RVA: 0x000A869B File Offset: 0x000A689B
		[DataMember(EmitDefaultValue = false)]
		public string SetHeaderName
		{
			get
			{
				return this.Rule.SetHeaderName.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000A86A2 File Offset: 0x000A68A2
		// (set) Token: 0x060036E4 RID: 14052 RVA: 0x000A86B9 File Offset: 0x000A68B9
		[DataMember(EmitDefaultValue = false)]
		public string SetHeaderValue
		{
			get
			{
				return this.Rule.SetHeaderValue.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000A86C0 File Offset: 0x000A68C0
		// (set) Token: 0x060036E6 RID: 14054 RVA: 0x000A86D7 File Offset: 0x000A68D7
		[DataMember(EmitDefaultValue = false)]
		public string RemoveHeader
		{
			get
			{
				return this.Rule.RemoveHeader.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000A86E0 File Offset: 0x000A68E0
		// (set) Token: 0x060036E8 RID: 14056 RVA: 0x000A8733 File Offset: 0x000A6933
		[DataMember(EmitDefaultValue = false)]
		public Identity ApplyRightsProtectionTemplate
		{
			get
			{
				if (this.Rule.ApplyRightsProtectionTemplate == null || string.IsNullOrEmpty(this.Rule.ApplyRightsProtectionTemplate.TemplateName))
				{
					return null;
				}
				return this.Rule.ApplyRightsProtectionTemplate.ToIdentity(this.Rule.ApplyRightsProtectionTemplate.TemplateName);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000A873A File Offset: 0x000A693A
		// (set) Token: 0x060036EA RID: 14058 RVA: 0x000A874C File Offset: 0x000A694C
		[DataMember(EmitDefaultValue = false)]
		public string SetAuditSeverity
		{
			get
			{
				return this.Rule.SetAuditSeverity.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x000A8754 File Offset: 0x000A6954
		// (set) Token: 0x060036EC RID: 14060 RVA: 0x000A877E File Offset: 0x000A697E
		[DataMember(EmitDefaultValue = false)]
		public bool? StopRuleProcessing
		{
			get
			{
				if (!this.Rule.StopRuleProcessing)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700218B RID: 8587
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x000A8785 File Offset: 0x000A6985
		// (set) Token: 0x060036EE RID: 14062 RVA: 0x000A8797 File Offset: 0x000A6997
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] AddToRecipients
		{
			get
			{
				return this.Rule.AddToRecipients.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700218C RID: 8588
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x000A879E File Offset: 0x000A699E
		// (set) Token: 0x060036F0 RID: 14064 RVA: 0x000A87B0 File Offset: 0x000A69B0
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] CopyTo
		{
			get
			{
				return this.Rule.CopyTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700218D RID: 8589
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x000A87B7 File Offset: 0x000A69B7
		// (set) Token: 0x060036F2 RID: 14066 RVA: 0x000A87C9 File Offset: 0x000A69C9
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] BlindCopyTo
		{
			get
			{
				return this.Rule.BlindCopyTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700218E RID: 8590
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x000A87D0 File Offset: 0x000A69D0
		// (set) Token: 0x060036F4 RID: 14068 RVA: 0x000A87E7 File Offset: 0x000A69E7
		[DataMember(EmitDefaultValue = false)]
		public string AddManagerAsRecipientType
		{
			get
			{
				return this.Rule.AddManagerAsRecipientType.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700218F RID: 8591
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x000A87F0 File Offset: 0x000A69F0
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x000A8859 File Offset: 0x000A6A59
		[DataMember(EmitDefaultValue = false)]
		public SenderNotifySettings SenderNotifySettings
		{
			get
			{
				if (this.Rule.SenderNotificationType == null)
				{
					return null;
				}
				return new SenderNotifySettings
				{
					NotifySender = this.Rule.SenderNotificationType.Value.ToStringWithNull(),
					RejectMessage = this.Rule.RejectMessageReasonText.ToStringWithNull()
				};
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002190 RID: 8592
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000A8860 File Offset: 0x000A6A60
		// (set) Token: 0x060036F8 RID: 14072 RVA: 0x000A8872 File Offset: 0x000A6A72
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ModerateMessageByUser
		{
			get
			{
				return this.Rule.ModerateMessageByUser.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002191 RID: 8593
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x000A887C File Offset: 0x000A6A7C
		// (set) Token: 0x060036FA RID: 14074 RVA: 0x000A88A6 File Offset: 0x000A6AA6
		[DataMember(EmitDefaultValue = false)]
		public bool? ModerateMessageByManager
		{
			get
			{
				if (!this.Rule.ModerateMessageByManager)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000A88AD File Offset: 0x000A6AAD
		// (set) Token: 0x060036FC RID: 14076 RVA: 0x000A88BF File Offset: 0x000A6ABF
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] RedirectMessageTo
		{
			get
			{
				return this.Rule.RedirectMessageTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000A88C8 File Offset: 0x000A6AC8
		// (set) Token: 0x060036FE RID: 14078 RVA: 0x000A8998 File Offset: 0x000A6B98
		[DataMember(EmitDefaultValue = false)]
		public string RejectMessageEnhancedStatusCode
		{
			get
			{
				bool flag = this.Rule.RejectMessageEnhancedStatusCode != null && this.Rule.RejectMessageEnhancedStatusCode.Value == Utils.DefaultEnhancedStatusCode.Value;
				bool flag2 = this.Rule.RejectMessageReasonText != null && string.Compare(this.Rule.RejectMessageReasonText.Value.Value, Utils.DefaultRejectText.Value, StringComparison.InvariantCultureIgnoreCase) != 0;
				if (this.Rule.SenderNotificationType != null || (flag && flag2))
				{
					return null;
				}
				return this.Rule.RejectMessageEnhancedStatusCode.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000A89A0 File Offset: 0x000A6BA0
		// (set) Token: 0x06003700 RID: 14080 RVA: 0x000A89E6 File Offset: 0x000A6BE6
		[DataMember(EmitDefaultValue = false)]
		public string RejectMessageReasonText
		{
			get
			{
				if (this.Rule.SenderNotificationType != null || !string.IsNullOrEmpty(this.RejectMessageEnhancedStatusCode))
				{
					return null;
				}
				return this.Rule.RejectMessageReasonText.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000A89F0 File Offset: 0x000A6BF0
		// (set) Token: 0x06003702 RID: 14082 RVA: 0x000A8A1A File Offset: 0x000A6C1A
		[DataMember(EmitDefaultValue = false)]
		public bool? DeleteMessage
		{
			get
			{
				if (!this.Rule.DeleteMessage)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000A8A24 File Offset: 0x000A6C24
		// (set) Token: 0x06003704 RID: 14084 RVA: 0x000A8A4E File Offset: 0x000A6C4E
		[DataMember(EmitDefaultValue = false)]
		public bool? RouteMessageOutboundRequireTls
		{
			get
			{
				if (!this.Rule.RouteMessageOutboundRequireTls)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000A8A58 File Offset: 0x000A6C58
		// (set) Token: 0x06003706 RID: 14086 RVA: 0x000A8A82 File Offset: 0x000A6C82
		[DataMember(EmitDefaultValue = false)]
		public bool? ApplyOME
		{
			get
			{
				if (!this.Rule.ApplyOME)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000A8A8C File Offset: 0x000A6C8C
		// (set) Token: 0x06003708 RID: 14088 RVA: 0x000A8AB6 File Offset: 0x000A6CB6
		[DataMember(EmitDefaultValue = false)]
		public bool? RemoveOME
		{
			get
			{
				if (!this.Rule.RemoveOME)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x06003709 RID: 14089 RVA: 0x000A8AC0 File Offset: 0x000A6CC0
		// (set) Token: 0x0600370A RID: 14090 RVA: 0x000A8B18 File Offset: 0x000A6D18
		[DataMember(EmitDefaultValue = false)]
		public Identity GenerateIncidentReport
		{
			get
			{
				if (this.Rule.GenerateIncidentReport != null)
				{
					ADRecipientOrAddress[] array = new ADRecipientOrAddress[]
					{
						this.Rule.GenerateIncidentReport.ToADRecipientOrAddress()
					};
					if (array.Length == 1)
					{
						PeopleIdentity peopleIdentity = array.ToPeopleIdentityArray()[0];
						return new Identity(peopleIdentity.SMTPAddress, peopleIdentity.DisplayName);
					}
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000A8B20 File Offset: 0x000A6D20
		// (set) Token: 0x0600370C RID: 14092 RVA: 0x000A8B61 File Offset: 0x000A6D61
		[DataMember(EmitDefaultValue = false)]
		public string IncidentReportOriginalMail
		{
			get
			{
				if (this.Rule.IncidentReportOriginalMail == null)
				{
					return null;
				}
				return this.Rule.IncidentReportOriginalMail.Value.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x0600370D RID: 14093 RVA: 0x000A8B78 File Offset: 0x000A6D78
		// (set) Token: 0x0600370E RID: 14094 RVA: 0x000A8BC6 File Offset: 0x000A6DC6
		[DataMember(EmitDefaultValue = false)]
		public string[] IncidentReportContent
		{
			get
			{
				if (this.Rule.IncidentReportContent != null)
				{
					return (from s in this.Rule.IncidentReportContent
					select s.ToString()).ToArray<string>();
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x0600370F RID: 14095 RVA: 0x000A8BCD File Offset: 0x000A6DCD
		// (set) Token: 0x06003710 RID: 14096 RVA: 0x000A8BE4 File Offset: 0x000A6DE4
		[DataMember(EmitDefaultValue = false)]
		public string GenerateNotification
		{
			get
			{
				return this.Rule.GenerateNotification.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000A8BEC File Offset: 0x000A6DEC
		// (set) Token: 0x06003712 RID: 14098 RVA: 0x000A8C16 File Offset: 0x000A6E16
		[DataMember(EmitDefaultValue = false)]
		public bool? Quarantine
		{
			get
			{
				if (!this.Rule.Quarantine)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000A8C1D File Offset: 0x000A6E1D
		// (set) Token: 0x06003714 RID: 14100 RVA: 0x000A8C43 File Offset: 0x000A6E43
		[DataMember(EmitDefaultValue = false)]
		public Identity RouteMessageOutboundConnector
		{
			get
			{
				if (string.IsNullOrEmpty(this.Rule.RouteMessageOutboundConnector))
				{
					return null;
				}
				return new Identity(this.Rule.RouteMessageOutboundConnector);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000A8C4A File Offset: 0x000A6E4A
		// (set) Token: 0x06003716 RID: 14102 RVA: 0x000A8C5C File Offset: 0x000A6E5C
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfFrom
		{
			get
			{
				return this.Rule.ExceptIfFrom.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000A8C63 File Offset: 0x000A6E63
		// (set) Token: 0x06003718 RID: 14104 RVA: 0x000A8C75 File Offset: 0x000A6E75
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfFromMemberOf
		{
			get
			{
				return this.Rule.ExceptIfFromMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000A8C7C File Offset: 0x000A6E7C
		// (set) Token: 0x0600371A RID: 14106 RVA: 0x000A8C93 File Offset: 0x000A6E93
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfFromScope
		{
			get
			{
				return this.Rule.ExceptIfFromScope.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000A8C9A File Offset: 0x000A6E9A
		// (set) Token: 0x0600371C RID: 14108 RVA: 0x000A8CAC File Offset: 0x000A6EAC
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfSentTo
		{
			get
			{
				return this.Rule.ExceptIfSentTo.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x0600371D RID: 14109 RVA: 0x000A8CB3 File Offset: 0x000A6EB3
		// (set) Token: 0x0600371E RID: 14110 RVA: 0x000A8CC5 File Offset: 0x000A6EC5
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfSentToMemberOf
		{
			get
			{
				return this.Rule.ExceptIfSentToMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000A8CCC File Offset: 0x000A6ECC
		// (set) Token: 0x06003720 RID: 14112 RVA: 0x000A8CE3 File Offset: 0x000A6EE3
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfSentToScope
		{
			get
			{
				return this.Rule.ExceptIfSentToScope.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A5 RID: 8613
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000A8CEA File Offset: 0x000A6EEA
		// (set) Token: 0x06003722 RID: 14114 RVA: 0x000A8CFC File Offset: 0x000A6EFC
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfBetweenMemberOf1
		{
			get
			{
				return this.Rule.ExceptIfBetweenMemberOf1.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A6 RID: 8614
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000A8D03 File Offset: 0x000A6F03
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x000A8D15 File Offset: 0x000A6F15
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfBetweenMemberOf2
		{
			get
			{
				return this.Rule.ExceptIfBetweenMemberOf2.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A7 RID: 8615
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000A8D1C File Offset: 0x000A6F1C
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x000A8D2E File Offset: 0x000A6F2E
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfManagerAddresses
		{
			get
			{
				return this.Rule.ExceptIfManagerAddresses.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A8 RID: 8616
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000A8D35 File Offset: 0x000A6F35
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x000A8D4C File Offset: 0x000A6F4C
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfManagerForEvaluatedUser
		{
			get
			{
				return this.Rule.ExceptIfManagerForEvaluatedUser.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021A9 RID: 8617
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x000A8D53 File Offset: 0x000A6F53
		// (set) Token: 0x0600372A RID: 14122 RVA: 0x000A8D6A File Offset: 0x000A6F6A
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfSenderManagementRelationship
		{
			get
			{
				return this.Rule.ExceptIfSenderManagementRelationship.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x000A8D71 File Offset: 0x000A6F71
		// (set) Token: 0x0600372C RID: 14124 RVA: 0x000A8D88 File Offset: 0x000A6F88
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfADComparisonAttribute
		{
			get
			{
				return this.Rule.ExceptIfADComparisonAttribute.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000A8D8F File Offset: 0x000A6F8F
		// (set) Token: 0x0600372E RID: 14126 RVA: 0x000A8DA6 File Offset: 0x000A6FA6
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfADComparisonOperator
		{
			get
			{
				return this.Rule.ExceptIfADComparisonOperator.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021AC RID: 8620
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x000A8DAD File Offset: 0x000A6FAD
		// (set) Token: 0x06003730 RID: 14128 RVA: 0x000A8DBF File Offset: 0x000A6FBF
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfAnyOfToHeader
		{
			get
			{
				return this.Rule.ExceptIfAnyOfToHeader.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021AD RID: 8621
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x000A8DC6 File Offset: 0x000A6FC6
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x000A8DD8 File Offset: 0x000A6FD8
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfAnyOfToHeaderMemberOf
		{
			get
			{
				return this.Rule.ExceptIfAnyOfToHeaderMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021AE RID: 8622
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000A8DDF File Offset: 0x000A6FDF
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x000A8DF1 File Offset: 0x000A6FF1
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfAnyOfCcHeader
		{
			get
			{
				return this.Rule.ExceptIfAnyOfCcHeader.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021AF RID: 8623
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000A8DF8 File Offset: 0x000A6FF8
		// (set) Token: 0x06003736 RID: 14134 RVA: 0x000A8E0A File Offset: 0x000A700A
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfAnyOfCcHeaderMemberOf
		{
			get
			{
				return this.Rule.ExceptIfAnyOfCcHeaderMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B0 RID: 8624
		// (get) Token: 0x06003737 RID: 14135 RVA: 0x000A8E11 File Offset: 0x000A7011
		// (set) Token: 0x06003738 RID: 14136 RVA: 0x000A8E23 File Offset: 0x000A7023
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfAnyOfToCcHeader
		{
			get
			{
				return this.Rule.ExceptIfAnyOfToCcHeader.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B1 RID: 8625
		// (get) Token: 0x06003739 RID: 14137 RVA: 0x000A8E2A File Offset: 0x000A702A
		// (set) Token: 0x0600373A RID: 14138 RVA: 0x000A8E3C File Offset: 0x000A703C
		[DataMember(EmitDefaultValue = false)]
		public PeopleIdentity[] ExceptIfAnyOfToCcHeaderMemberOf
		{
			get
			{
				return this.Rule.ExceptIfAnyOfToCcHeaderMemberOf.ToPeopleIdentityArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B2 RID: 8626
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000A8E43 File Offset: 0x000A7043
		// (set) Token: 0x0600373C RID: 14140 RVA: 0x000A8E55 File Offset: 0x000A7055
		[DataMember(EmitDefaultValue = false)]
		public Identity ExceptIfHasClassification
		{
			get
			{
				return this.Rule.ExceptIfHasClassification.ToIdentity();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B3 RID: 8627
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000A8E5C File Offset: 0x000A705C
		// (set) Token: 0x0600373E RID: 14142 RVA: 0x000A8E6E File Offset: 0x000A706E
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSubjectContainsWords
		{
			get
			{
				return this.Rule.ExceptIfSubjectContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B4 RID: 8628
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000A8E75 File Offset: 0x000A7075
		// (set) Token: 0x06003740 RID: 14144 RVA: 0x000A8E87 File Offset: 0x000A7087
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return this.Rule.ExceptIfSubjectOrBodyContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B5 RID: 8629
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000A8E8E File Offset: 0x000A708E
		// (set) Token: 0x06003742 RID: 14146 RVA: 0x000A8EA5 File Offset: 0x000A70A5
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfHeaderContainsMessageHeader
		{
			get
			{
				return this.Rule.ExceptIfHeaderContainsMessageHeader.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B6 RID: 8630
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000A8EAC File Offset: 0x000A70AC
		// (set) Token: 0x06003744 RID: 14148 RVA: 0x000A8EBE File Offset: 0x000A70BE
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfHeaderContainsWords
		{
			get
			{
				return this.Rule.ExceptIfHeaderContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x000A8EC5 File Offset: 0x000A70C5
		// (set) Token: 0x06003746 RID: 14150 RVA: 0x000A8ED7 File Offset: 0x000A70D7
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return this.Rule.ExceptIfFromAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000A8EDE File Offset: 0x000A70DE
		// (set) Token: 0x06003748 RID: 14152 RVA: 0x000A8EF0 File Offset: 0x000A70F0
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return this.Rule.ExceptIfRecipientAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021B9 RID: 8633
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000A8EF8 File Offset: 0x000A70F8
		// (set) Token: 0x0600374A RID: 14154 RVA: 0x000A8F29 File Offset: 0x000A7129
		[DataMember(EmitDefaultValue = false)]
		public Identity ExceptIfSenderInRecipientList
		{
			get
			{
				string[] array = this.Rule.ExceptIfSenderInRecipientList.ToStringArray();
				if (array == null)
				{
					return null;
				}
				string text = array.ToCommaSeperatedString();
				return new Identity(text, text);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000A8F30 File Offset: 0x000A7130
		// (set) Token: 0x0600374C RID: 14156 RVA: 0x000A8F61 File Offset: 0x000A7161
		[DataMember(EmitDefaultValue = false)]
		public Identity ExceptIfRecipientInSenderList
		{
			get
			{
				string[] array = this.Rule.ExceptIfRecipientInSenderList.ToStringArray();
				if (array == null)
				{
					return null;
				}
				string text = array.ToCommaSeperatedString();
				return new Identity(text, text);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000A8F68 File Offset: 0x000A7168
		// (set) Token: 0x0600374E RID: 14158 RVA: 0x000A8F7A File Offset: 0x000A717A
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfAnyOfRecipientAddressContainsWords
		{
			get
			{
				return this.Rule.ExceptIfAnyOfRecipientAddressContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000A8F81 File Offset: 0x000A7181
		// (set) Token: 0x06003750 RID: 14160 RVA: 0x000A8F93 File Offset: 0x000A7193
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfAttachmentContainsWords
		{
			get
			{
				return this.Rule.ExceptIfAttachmentContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000A8F9C File Offset: 0x000A719C
		// (set) Token: 0x06003752 RID: 14162 RVA: 0x000A8FC6 File Offset: 0x000A71C6
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfHasNoClassification
		{
			get
			{
				if (!this.Rule.ExceptIfHasNoClassification)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000A8FD0 File Offset: 0x000A71D0
		// (set) Token: 0x06003754 RID: 14164 RVA: 0x000A8FFA File Offset: 0x000A71FA
		[DataMember(EmitDefaultValue = false)]
		public bool? ExceptIfAttachmentIsUnsupported
		{
			get
			{
				if (!this.Rule.ExceptIfAttachmentIsUnsupported)
				{
					return null;
				}
				return new bool?(true);
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000A9001 File Offset: 0x000A7201
		// (set) Token: 0x06003756 RID: 14166 RVA: 0x000A9013 File Offset: 0x000A7213
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSenderADAttributeContainsWords
		{
			get
			{
				return this.Rule.ExceptIfSenderADAttributeContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000A901A File Offset: 0x000A721A
		// (set) Token: 0x06003758 RID: 14168 RVA: 0x000A902C File Offset: 0x000A722C
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfRecipientADAttributeContainsWords
		{
			get
			{
				return this.Rule.ExceptIfRecipientADAttributeContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000A9033 File Offset: 0x000A7233
		// (set) Token: 0x0600375A RID: 14170 RVA: 0x000A9045 File Offset: 0x000A7245
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSenderDomainIs
		{
			get
			{
				return this.Rule.ExceptIfSenderDomainIs.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000A904C File Offset: 0x000A724C
		// (set) Token: 0x0600375C RID: 14172 RVA: 0x000A905E File Offset: 0x000A725E
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfRecipientDomainIs
		{
			get
			{
				return this.Rule.ExceptIfRecipientDomainIs.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000A9065 File Offset: 0x000A7265
		// (set) Token: 0x0600375E RID: 14174 RVA: 0x000A9077 File Offset: 0x000A7277
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfContentCharacterSetContainsWords
		{
			get
			{
				return this.Rule.ExceptIfContentCharacterSetContainsWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000A907E File Offset: 0x000A727E
		// (set) Token: 0x06003760 RID: 14176 RVA: 0x000A90A5 File Offset: 0x000A72A5
		[DataMember(EmitDefaultValue = false)]
		public Hashtable[] ExceptIfMessageContainsDataClassifications
		{
			get
			{
				if (this.Rule.ExceptIfMessageContainsDataClassifications == null)
				{
					return null;
				}
				return this.ParseDataClassifications(this.Rule.ExceptIfMessageContainsDataClassifications.ToStringArray());
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000A90AC File Offset: 0x000A72AC
		// (set) Token: 0x06003762 RID: 14178 RVA: 0x000A90BE File Offset: 0x000A72BE
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSubjectMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfSubjectMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000A90C5 File Offset: 0x000A72C5
		// (set) Token: 0x06003764 RID: 14180 RVA: 0x000A90D7 File Offset: 0x000A72D7
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSubjectOrBodyMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfSubjectOrBodyMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x000A90DE File Offset: 0x000A72DE
		// (set) Token: 0x06003766 RID: 14182 RVA: 0x000A90F5 File Offset: 0x000A72F5
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfHeaderMatchesMessageHeader
		{
			get
			{
				return this.Rule.ExceptIfHeaderMatchesMessageHeader.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x000A90FC File Offset: 0x000A72FC
		// (set) Token: 0x06003768 RID: 14184 RVA: 0x000A910E File Offset: 0x000A730E
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfHeaderMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfHeaderMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x000A9115 File Offset: 0x000A7315
		// (set) Token: 0x0600376A RID: 14186 RVA: 0x000A9127 File Offset: 0x000A7327
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfFromAddressMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfFromAddressMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000A912E File Offset: 0x000A732E
		// (set) Token: 0x0600376C RID: 14188 RVA: 0x000A9140 File Offset: 0x000A7340
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfAttachmentNameMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfAttachmentNameMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021CB RID: 8651
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000A9147 File Offset: 0x000A7347
		// (set) Token: 0x0600376E RID: 14190 RVA: 0x000A9159 File Offset: 0x000A7359
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfAttachmentMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfAttachmentMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021CC RID: 8652
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000A9160 File Offset: 0x000A7360
		// (set) Token: 0x06003770 RID: 14192 RVA: 0x000A9172 File Offset: 0x000A7372
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfRecipientAddressMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfRecipientAddressMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021CD RID: 8653
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000A9179 File Offset: 0x000A7379
		// (set) Token: 0x06003772 RID: 14194 RVA: 0x000A918B File Offset: 0x000A738B
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfAnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfAnyOfRecipientAddressMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021CE RID: 8654
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000A9192 File Offset: 0x000A7392
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x000A91A4 File Offset: 0x000A73A4
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSenderADAttributeMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfSenderADAttributeMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021CF RID: 8655
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000A91AB File Offset: 0x000A73AB
		// (set) Token: 0x06003776 RID: 14198 RVA: 0x000A91BD File Offset: 0x000A73BD
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfRecipientADAttributeMatchesPatterns
		{
			get
			{
				return this.Rule.ExceptIfRecipientADAttributeMatchesPatterns.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D0 RID: 8656
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000A91C4 File Offset: 0x000A73C4
		// (set) Token: 0x06003778 RID: 14200 RVA: 0x000A91D6 File Offset: 0x000A73D6
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfAttachmentExtensionMatchesWords
		{
			get
			{
				return this.Rule.ExceptIfAttachmentExtensionMatchesWords.ToStringArray();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D1 RID: 8657
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000A91DD File Offset: 0x000A73DD
		// (set) Token: 0x0600377A RID: 14202 RVA: 0x000A91EF File Offset: 0x000A73EF
		[DataMember(EmitDefaultValue = false)]
		public string[] ExceptIfSenderIpRanges
		{
			get
			{
				return this.Rule.ExceptIfSenderIpRanges.ToStringArray<IPRange>();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D2 RID: 8658
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000A91F8 File Offset: 0x000A73F8
		// (set) Token: 0x0600377C RID: 14204 RVA: 0x000A923D File Offset: 0x000A743D
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfSCLOver
		{
			get
			{
				if (this.Rule.ExceptIfSCLOver != null)
				{
					return this.Rule.ExceptIfSCLOver.Value.ToString();
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D3 RID: 8659
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000A9244 File Offset: 0x000A7444
		// (set) Token: 0x0600377E RID: 14206 RVA: 0x000A9290 File Offset: 0x000A7490
		[DataMember(EmitDefaultValue = false)]
		public long? ExceptIfAttachmentSizeOver
		{
			get
			{
				if (this.Rule.ExceptIfAttachmentSizeOver != null)
				{
					return new long?((long)this.Rule.ExceptIfAttachmentSizeOver.Value.ToKB());
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D4 RID: 8660
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000A9297 File Offset: 0x000A7497
		// (set) Token: 0x06003780 RID: 14208 RVA: 0x000A92A4 File Offset: 0x000A74A4
		[DataMember(EmitDefaultValue = false)]
		public bool ExceptIfAttachmentProcessingLimitExceeded
		{
			get
			{
				return this.Rule.ExceptIfAttachmentProcessingLimitExceeded;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D5 RID: 8661
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000A92AB File Offset: 0x000A74AB
		// (set) Token: 0x06003782 RID: 14210 RVA: 0x000A92C2 File Offset: 0x000A74C2
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfWithImportance
		{
			get
			{
				return this.Rule.ExceptIfWithImportance.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D6 RID: 8662
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x000A92C9 File Offset: 0x000A74C9
		// (set) Token: 0x06003784 RID: 14212 RVA: 0x000A92E0 File Offset: 0x000A74E0
		[DataMember(EmitDefaultValue = false)]
		public string ExceptIfMessageTypeMatches
		{
			get
			{
				return this.Rule.ExceptIfMessageTypeMatches.ToStringWithNull();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D7 RID: 8663
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000A92E8 File Offset: 0x000A74E8
		// (set) Token: 0x06003786 RID: 14214 RVA: 0x000A9334 File Offset: 0x000A7534
		[DataMember(EmitDefaultValue = false)]
		public long? ExceptIfMessageSizeOver
		{
			get
			{
				if (this.Rule.ExceptIfMessageSizeOver != null)
				{
					return new long?((long)this.Rule.ExceptIfMessageSizeOver.Value.ToKB());
				}
				return null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D8 RID: 8664
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x000A933B File Offset: 0x000A753B
		// (set) Token: 0x06003788 RID: 14216 RVA: 0x000A9348 File Offset: 0x000A7548
		[DataMember(EmitDefaultValue = false)]
		public bool ExceptIfHasSenderOverride
		{
			get
			{
				return this.Rule.ExceptIfHasSenderOverride;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021D9 RID: 8665
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000A934F File Offset: 0x000A754F
		// (set) Token: 0x0600378A RID: 14218 RVA: 0x000A935C File Offset: 0x000A755C
		[DataMember(EmitDefaultValue = false)]
		public bool ExceptIfAttachmentHasExecutableContent
		{
			get
			{
				return this.Rule.ExceptIfAttachmentHasExecutableContent;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170021DA RID: 8666
		// (get) Token: 0x0600378B RID: 14219 RVA: 0x000A9363 File Offset: 0x000A7563
		// (set) Token: 0x0600378C RID: 14220 RVA: 0x000A9370 File Offset: 0x000A7570
		[DataMember(EmitDefaultValue = false)]
		public bool ExceptIfAttachmentIsPasswordProtected
		{
			get
			{
				return this.Rule.ExceptIfAttachmentIsPasswordProtected;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000A9377 File Offset: 0x000A7577
		internal void UpdateName(string newName)
		{
			base.Name = newName;
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000A9380 File Offset: 0x000A7580
		private Hashtable[] ParseDataClassifications(string[] classifications)
		{
			List<Hashtable> list = new List<Hashtable>();
			foreach (string text in classifications)
			{
				Hashtable hashtable = new Hashtable();
				int startIndex = 0;
				int length = text.Length - 1;
				if (text.StartsWith("{") && text.EndsWith("}"))
				{
					startIndex = 1;
					length = text.Length - 2;
				}
				try
				{
					string[] array = text.Substring(startIndex, length).Split(new char[]
					{
						','
					});
					for (int j = 0; j < array.Length; j++)
					{
						string[] array2 = array[j].Split(new char[]
						{
							':'
						});
						if (array2[1].StartsWith("\"") && array2[1].EndsWith("\""))
						{
							array2[1] = array2[1].Substring(1, array2[1].Length - 2);
						}
						string text2 = array2[1].Trim();
						if (text2.ToLower() != "infinity" && text2.ToLower() != "recommended")
						{
							hashtable[array2[0].Trim()] = text2;
						}
					}
					hashtable["name"] = hashtable["id"];
					hashtable.Remove("id");
					list.Add(hashtable);
				}
				catch (Exception ex)
				{
					ExTraceGlobals.WebServiceTracer.TraceInformation<string, string, string>(0, 0L, "Error parsing Data classifications: {0}, Exception: {1}\r\b{2}", classifications.StringArrayJoin(","), ex.GetFullMessage(), ex.StackTrace.ToString());
				}
			}
			return list.ToArray();
		}
	}
}
