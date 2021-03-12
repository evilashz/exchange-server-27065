using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AB8 RID: 2744
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class NewInboxRuleData : OptionsPropertyChangeTracker
	{
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x00106E27 File Offset: 0x00105027
		// (set) Token: 0x06004D53 RID: 19795 RVA: 0x00106E2F File Offset: 0x0010502F
		[DataMember]
		public string[] ApplyCategory
		{
			get
			{
				return this.applyCategory;
			}
			set
			{
				this.applyCategory = value;
				base.TrackPropertyChanged("ApplyCategory");
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x00106E43 File Offset: 0x00105043
		// (set) Token: 0x06004D55 RID: 19797 RVA: 0x00106E4B File Offset: 0x0010504B
		[DataMember]
		public string[] BodyContainsWords
		{
			get
			{
				return this.bodyContainsWords;
			}
			set
			{
				this.bodyContainsWords = value;
				base.TrackPropertyChanged("BodyContainsWords");
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x00106E5F File Offset: 0x0010505F
		// (set) Token: 0x06004D57 RID: 19799 RVA: 0x00106E67 File Offset: 0x00105067
		[DataMember]
		public Identity CopyToFolder
		{
			get
			{
				return this.copyToFolder;
			}
			set
			{
				this.copyToFolder = value;
				base.TrackPropertyChanged("CopyToFolder");
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06004D58 RID: 19800 RVA: 0x00106E7B File Offset: 0x0010507B
		// (set) Token: 0x06004D59 RID: 19801 RVA: 0x00106E83 File Offset: 0x00105083
		[DataMember]
		public bool DeleteMessage
		{
			get
			{
				return this.deleteMessage;
			}
			set
			{
				this.deleteMessage = value;
				base.TrackPropertyChanged("DeleteMessage");
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x00106E97 File Offset: 0x00105097
		// (set) Token: 0x06004D5B RID: 19803 RVA: 0x00106E9F File Offset: 0x0010509F
		[DataMember]
		public string[] ExceptIfBodyContainsWords
		{
			get
			{
				return this.exceptIfBodyContainsWords;
			}
			set
			{
				this.exceptIfBodyContainsWords = value;
				base.TrackPropertyChanged("ExceptIfBodyContainsWords");
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x00106EB3 File Offset: 0x001050B3
		// (set) Token: 0x06004D5D RID: 19805 RVA: 0x00106EBB File Offset: 0x001050BB
		[DataMember]
		public string ExceptIfFlaggedForAction
		{
			get
			{
				return this.exceptIfFlaggedForAction;
			}
			set
			{
				this.exceptIfFlaggedForAction = value;
				base.TrackPropertyChanged("ExceptIfFlaggedForAction");
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004D5E RID: 19806 RVA: 0x00106ECF File Offset: 0x001050CF
		// (set) Token: 0x06004D5F RID: 19807 RVA: 0x00106ED7 File Offset: 0x001050D7
		[DataMember]
		public PeopleIdentity[] ExceptIfFrom
		{
			get
			{
				return this.exceptIfFrom;
			}
			set
			{
				this.exceptIfFrom = value;
				base.TrackPropertyChanged("ExceptIfFrom");
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x00106EEB File Offset: 0x001050EB
		// (set) Token: 0x06004D61 RID: 19809 RVA: 0x00106EF3 File Offset: 0x001050F3
		[DataMember]
		public string[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return this.exceptIfFromAddressContainsWords;
			}
			set
			{
				this.exceptIfFromAddressContainsWords = value;
				base.TrackPropertyChanged("ExceptIfFromAddressContainsWords");
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x00106F07 File Offset: 0x00105107
		// (set) Token: 0x06004D63 RID: 19811 RVA: 0x00106F0F File Offset: 0x0010510F
		[DataMember]
		public Identity[] ExceptIfFromSubscription
		{
			get
			{
				return this.exceptIfFromSubscription;
			}
			set
			{
				this.exceptIfFromSubscription = value;
				base.TrackPropertyChanged("ExceptIfFromSubscription");
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x00106F23 File Offset: 0x00105123
		// (set) Token: 0x06004D65 RID: 19813 RVA: 0x00106F2B File Offset: 0x0010512B
		[DataMember]
		public bool ExceptIfHasAttachment
		{
			get
			{
				return this.exceptIfHasAttachment;
			}
			set
			{
				this.exceptIfHasAttachment = value;
				base.TrackPropertyChanged("ExceptIfHasAttachment");
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x00106F3F File Offset: 0x0010513F
		// (set) Token: 0x06004D67 RID: 19815 RVA: 0x00106F47 File Offset: 0x00105147
		[DataMember]
		public Identity[] ExceptIfHasClassification
		{
			get
			{
				return this.exceptIfHasClassification;
			}
			set
			{
				this.exceptIfHasClassification = value;
				base.TrackPropertyChanged("ExceptIfHasClassification");
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x00106F5B File Offset: 0x0010515B
		// (set) Token: 0x06004D69 RID: 19817 RVA: 0x00106F63 File Offset: 0x00105163
		[DataMember]
		public string[] ExceptIfHeaderContainsWords
		{
			get
			{
				return this.exceptIfHeaderContainsWords;
			}
			set
			{
				this.exceptIfHeaderContainsWords = value;
				base.TrackPropertyChanged("ExceptIfHeaderContainsWords");
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x00106F77 File Offset: 0x00105177
		// (set) Token: 0x06004D6B RID: 19819 RVA: 0x00106F7F File Offset: 0x0010517F
		[IgnoreDataMember]
		public NullableInboxRuleMessageType ExceptIfMessageTypeMatches
		{
			get
			{
				return this.exceptIfMessageTypeMatches;
			}
			set
			{
				this.exceptIfMessageTypeMatches = value;
				base.TrackPropertyChanged("ExceptIfMessageTypeMatches");
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x00106F93 File Offset: 0x00105193
		// (set) Token: 0x06004D6D RID: 19821 RVA: 0x00106FA0 File Offset: 0x001051A0
		[DataMember(Name = "ExceptIfMessageTypeMatches", IsRequired = false, EmitDefaultValue = false)]
		public string ExceptIfMessageTypeMatchesString
		{
			get
			{
				return EnumUtilities.ToString<NullableInboxRuleMessageType>(this.ExceptIfMessageTypeMatches);
			}
			set
			{
				this.ExceptIfMessageTypeMatches = EnumUtilities.Parse<NullableInboxRuleMessageType>(value);
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x00106FAE File Offset: 0x001051AE
		// (set) Token: 0x06004D6F RID: 19823 RVA: 0x00106FB6 File Offset: 0x001051B6
		[DataMember]
		public bool ExceptIfMyNameInCcBox
		{
			get
			{
				return this.exceptIfMyNameInCcBox;
			}
			set
			{
				this.exceptIfMyNameInCcBox = value;
				base.TrackPropertyChanged("ExceptIfMyNameInCcBox");
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x00106FCA File Offset: 0x001051CA
		// (set) Token: 0x06004D71 RID: 19825 RVA: 0x00106FD2 File Offset: 0x001051D2
		[DataMember]
		public bool ExceptIfMyNameInToBox
		{
			get
			{
				return this.exceptIfMyNameInToBox;
			}
			set
			{
				this.exceptIfMyNameInToBox = value;
				base.TrackPropertyChanged("ExceptIfMyNameInToBox");
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x00106FE6 File Offset: 0x001051E6
		// (set) Token: 0x06004D73 RID: 19827 RVA: 0x00106FEE File Offset: 0x001051EE
		[DataMember]
		public bool ExceptIfMyNameInToOrCcBox
		{
			get
			{
				return this.exceptIfMyNameInToOrCcBox;
			}
			set
			{
				this.exceptIfMyNameInToOrCcBox = value;
				base.TrackPropertyChanged("ExceptIfMyNameInToOrCcBox");
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004D74 RID: 19828 RVA: 0x00107002 File Offset: 0x00105202
		// (set) Token: 0x06004D75 RID: 19829 RVA: 0x0010700A File Offset: 0x0010520A
		[DataMember]
		public bool ExceptIfMyNameNotInToBox
		{
			get
			{
				return this.exceptIfMyNameNotInToBox;
			}
			set
			{
				this.exceptIfMyNameNotInToBox = value;
				base.TrackPropertyChanged("ExceptIfMyNameNotInToBox");
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06004D76 RID: 19830 RVA: 0x0010701E File Offset: 0x0010521E
		// (set) Token: 0x06004D77 RID: 19831 RVA: 0x00107026 File Offset: 0x00105226
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ExceptIfReceivedAfterDate
		{
			get
			{
				return this.exceptIfReceivedAfterDate;
			}
			set
			{
				this.exceptIfReceivedAfterDate = value;
				base.TrackPropertyChanged("ExceptIfReceivedAfterDate");
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x0010703A File Offset: 0x0010523A
		// (set) Token: 0x06004D79 RID: 19833 RVA: 0x00107042 File Offset: 0x00105242
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ExceptIfReceivedBeforeDate
		{
			get
			{
				return this.exceptIfReceivedBeforeDate;
			}
			set
			{
				this.exceptIfReceivedBeforeDate = value;
				base.TrackPropertyChanged("ExceptIfReceivedBeforeDate");
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x00107056 File Offset: 0x00105256
		// (set) Token: 0x06004D7B RID: 19835 RVA: 0x0010705E File Offset: 0x0010525E
		[DataMember]
		public string[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return this.exceptIfRecipientAddressContainsWords;
			}
			set
			{
				this.exceptIfRecipientAddressContainsWords = value;
				base.TrackPropertyChanged("ExceptIfRecipientAddressContainsWords");
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x00107072 File Offset: 0x00105272
		// (set) Token: 0x06004D7D RID: 19837 RVA: 0x0010707A File Offset: 0x0010527A
		[DataMember]
		public bool ExceptIfSentOnlyToMe
		{
			get
			{
				return this.exceptIfSentOnlyToMe;
			}
			set
			{
				this.exceptIfSentOnlyToMe = value;
				base.TrackPropertyChanged("ExceptIfSentOnlyToMe");
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06004D7E RID: 19838 RVA: 0x0010708E File Offset: 0x0010528E
		// (set) Token: 0x06004D7F RID: 19839 RVA: 0x00107096 File Offset: 0x00105296
		[DataMember]
		public PeopleIdentity[] ExceptIfSentTo
		{
			get
			{
				return this.exceptIfSentTo;
			}
			set
			{
				this.exceptIfSentTo = value;
				base.TrackPropertyChanged("ExceptIfSentTo");
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06004D80 RID: 19840 RVA: 0x001070AA File Offset: 0x001052AA
		// (set) Token: 0x06004D81 RID: 19841 RVA: 0x001070B2 File Offset: 0x001052B2
		[DataMember]
		public string[] ExceptIfSubjectContainsWords
		{
			get
			{
				return this.exceptIfSubjectContainsWords;
			}
			set
			{
				this.exceptIfSubjectContainsWords = value;
				base.TrackPropertyChanged("ExceptIfSubjectContainsWords");
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06004D82 RID: 19842 RVA: 0x001070C6 File Offset: 0x001052C6
		// (set) Token: 0x06004D83 RID: 19843 RVA: 0x001070CE File Offset: 0x001052CE
		[DataMember]
		public string[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return this.exceptIfSubjectOrBodyContainsWords;
			}
			set
			{
				this.exceptIfSubjectOrBodyContainsWords = value;
				base.TrackPropertyChanged("ExceptIfSubjectOrBodyContainsWords");
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x001070E2 File Offset: 0x001052E2
		// (set) Token: 0x06004D85 RID: 19845 RVA: 0x001070EA File Offset: 0x001052EA
		[IgnoreDataMember]
		public NullableImportance ExceptIfWithImportance
		{
			get
			{
				return this.exceptIfWithImportance;
			}
			set
			{
				this.exceptIfWithImportance = value;
				base.TrackPropertyChanged("ExceptIfWithImportance");
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x001070FE File Offset: 0x001052FE
		// (set) Token: 0x06004D87 RID: 19847 RVA: 0x0010710B File Offset: 0x0010530B
		[DataMember(Name = "ExceptIfWithImportance", IsRequired = false, EmitDefaultValue = false)]
		public string ExceptIfWithImportanceString
		{
			get
			{
				return EnumUtilities.ToString<NullableImportance>(this.ExceptIfWithImportance);
			}
			set
			{
				this.ExceptIfWithImportance = EnumUtilities.Parse<NullableImportance>(value);
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06004D88 RID: 19848 RVA: 0x00107119 File Offset: 0x00105319
		// (set) Token: 0x06004D89 RID: 19849 RVA: 0x00107121 File Offset: 0x00105321
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ulong? ExceptIfWithinSizeRangeMaximum
		{
			get
			{
				return this.exceptIfWithinSizeRangeMaximum;
			}
			set
			{
				this.exceptIfWithinSizeRangeMaximum = value;
				base.TrackPropertyChanged("ExceptIfWithinSizeRangeMaximum");
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x00107135 File Offset: 0x00105335
		// (set) Token: 0x06004D8B RID: 19851 RVA: 0x0010713D File Offset: 0x0010533D
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ulong? ExceptIfWithinSizeRangeMinimum
		{
			get
			{
				return this.exceptIfWithinSizeRangeMinimum;
			}
			set
			{
				this.exceptIfWithinSizeRangeMinimum = value;
				base.TrackPropertyChanged("ExceptIfWithinSizeRangeMinimum");
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x00107151 File Offset: 0x00105351
		// (set) Token: 0x06004D8D RID: 19853 RVA: 0x00107159 File Offset: 0x00105359
		[IgnoreDataMember]
		public NullableSensitivity ExceptIfWithSensitivity
		{
			get
			{
				return this.exceptIfWithSensitivity;
			}
			set
			{
				this.exceptIfWithSensitivity = value;
				base.TrackPropertyChanged("ExceptIfWithSensitivity");
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06004D8E RID: 19854 RVA: 0x0010716D File Offset: 0x0010536D
		// (set) Token: 0x06004D8F RID: 19855 RVA: 0x0010717A File Offset: 0x0010537A
		[DataMember(Name = "ExceptIfWithSensitivity", IsRequired = false, EmitDefaultValue = false)]
		public string ExceptIfWithSensitivityString
		{
			get
			{
				return EnumUtilities.ToString<NullableSensitivity>(this.ExceptIfWithSensitivity);
			}
			set
			{
				this.ExceptIfWithSensitivity = EnumUtilities.Parse<NullableSensitivity>(value);
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06004D90 RID: 19856 RVA: 0x00107188 File Offset: 0x00105388
		// (set) Token: 0x06004D91 RID: 19857 RVA: 0x00107190 File Offset: 0x00105390
		[DataMember]
		public string FlaggedForAction
		{
			get
			{
				return this.flaggedForAction;
			}
			set
			{
				this.flaggedForAction = value;
				base.TrackPropertyChanged("FlaggedForAction");
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06004D92 RID: 19858 RVA: 0x001071A4 File Offset: 0x001053A4
		// (set) Token: 0x06004D93 RID: 19859 RVA: 0x001071AC File Offset: 0x001053AC
		[DataMember]
		public PeopleIdentity[] ForwardAsAttachmentTo
		{
			get
			{
				return this.forwardAsAttachmentTo;
			}
			set
			{
				this.forwardAsAttachmentTo = value;
				base.TrackPropertyChanged("ForwardAsAttachmentTo");
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06004D94 RID: 19860 RVA: 0x001071C0 File Offset: 0x001053C0
		// (set) Token: 0x06004D95 RID: 19861 RVA: 0x001071C8 File Offset: 0x001053C8
		[DataMember]
		public PeopleIdentity[] ForwardTo
		{
			get
			{
				return this.forwardTo;
			}
			set
			{
				this.forwardTo = value;
				base.TrackPropertyChanged("ForwardTo");
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06004D96 RID: 19862 RVA: 0x001071DC File Offset: 0x001053DC
		// (set) Token: 0x06004D97 RID: 19863 RVA: 0x001071E4 File Offset: 0x001053E4
		[DataMember]
		public PeopleIdentity[] From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
				base.TrackPropertyChanged("From");
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x001071F8 File Offset: 0x001053F8
		// (set) Token: 0x06004D99 RID: 19865 RVA: 0x00107200 File Offset: 0x00105400
		[DataMember]
		public string[] FromAddressContainsWords
		{
			get
			{
				return this.fromAddressContainsWords;
			}
			set
			{
				this.fromAddressContainsWords = value;
				base.TrackPropertyChanged("FromAddressContainsWords");
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06004D9A RID: 19866 RVA: 0x00107214 File Offset: 0x00105414
		// (set) Token: 0x06004D9B RID: 19867 RVA: 0x0010721C File Offset: 0x0010541C
		[DataMember]
		public Identity[] FromSubscription
		{
			get
			{
				return this.fromSubscription;
			}
			set
			{
				this.fromSubscription = value;
				base.TrackPropertyChanged("FromSubscription");
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x00107230 File Offset: 0x00105430
		// (set) Token: 0x06004D9D RID: 19869 RVA: 0x00107238 File Offset: 0x00105438
		[DataMember]
		public bool HasAttachment
		{
			get
			{
				return this.hasAttachment;
			}
			set
			{
				this.hasAttachment = value;
				base.TrackPropertyChanged("HasAttachment");
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06004D9E RID: 19870 RVA: 0x0010724C File Offset: 0x0010544C
		// (set) Token: 0x06004D9F RID: 19871 RVA: 0x00107254 File Offset: 0x00105454
		[DataMember]
		public Identity[] HasClassification
		{
			get
			{
				return this.hasClassification;
			}
			set
			{
				this.hasClassification = value;
				base.TrackPropertyChanged("HasClassification");
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06004DA0 RID: 19872 RVA: 0x00107268 File Offset: 0x00105468
		// (set) Token: 0x06004DA1 RID: 19873 RVA: 0x00107270 File Offset: 0x00105470
		[DataMember]
		public string[] HeaderContainsWords
		{
			get
			{
				return this.headerContainsWords;
			}
			set
			{
				this.headerContainsWords = value;
				base.TrackPropertyChanged("HeaderContainsWords");
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06004DA2 RID: 19874 RVA: 0x00107284 File Offset: 0x00105484
		// (set) Token: 0x06004DA3 RID: 19875 RVA: 0x0010728C File Offset: 0x0010548C
		[DataMember]
		public bool MarkAsRead
		{
			get
			{
				return this.markAsRead;
			}
			set
			{
				this.markAsRead = value;
				base.TrackPropertyChanged("MarkAsRead");
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06004DA4 RID: 19876 RVA: 0x001072A0 File Offset: 0x001054A0
		// (set) Token: 0x06004DA5 RID: 19877 RVA: 0x001072A8 File Offset: 0x001054A8
		[IgnoreDataMember]
		public NullableImportance MarkImportance
		{
			get
			{
				return this.markImportance;
			}
			set
			{
				this.markImportance = value;
				base.TrackPropertyChanged("MarkImportance");
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x001072BC File Offset: 0x001054BC
		// (set) Token: 0x06004DA7 RID: 19879 RVA: 0x001072C9 File Offset: 0x001054C9
		[DataMember(Name = "MarkImportance", IsRequired = false, EmitDefaultValue = false)]
		public string MarkImportanceString
		{
			get
			{
				return EnumUtilities.ToString<NullableImportance>(this.MarkImportance);
			}
			set
			{
				this.MarkImportance = EnumUtilities.Parse<NullableImportance>(value);
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x001072D7 File Offset: 0x001054D7
		// (set) Token: 0x06004DA9 RID: 19881 RVA: 0x001072DF File Offset: 0x001054DF
		[IgnoreDataMember]
		public NullableInboxRuleMessageType MessageTypeMatches
		{
			get
			{
				return this.messageTypeMatches;
			}
			set
			{
				this.messageTypeMatches = value;
				base.TrackPropertyChanged("MessageTypeMatches");
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06004DAA RID: 19882 RVA: 0x001072F3 File Offset: 0x001054F3
		// (set) Token: 0x06004DAB RID: 19883 RVA: 0x00107300 File Offset: 0x00105500
		[DataMember(Name = "MessageTypeMatches", IsRequired = false, EmitDefaultValue = false)]
		public string MessageTypeMatchesString
		{
			get
			{
				return EnumUtilities.ToString<NullableInboxRuleMessageType>(this.MessageTypeMatches);
			}
			set
			{
				this.MessageTypeMatches = EnumUtilities.Parse<NullableInboxRuleMessageType>(value);
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x0010730E File Offset: 0x0010550E
		// (set) Token: 0x06004DAD RID: 19885 RVA: 0x00107316 File Offset: 0x00105516
		[DataMember]
		public bool MyNameInCcBox
		{
			get
			{
				return this.myNameInCcBox;
			}
			set
			{
				this.myNameInCcBox = value;
				base.TrackPropertyChanged("MyNameInCcBox");
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x0010732A File Offset: 0x0010552A
		// (set) Token: 0x06004DAF RID: 19887 RVA: 0x00107332 File Offset: 0x00105532
		[DataMember]
		public bool MyNameInToBox
		{
			get
			{
				return this.myNameInToBox;
			}
			set
			{
				this.myNameInToBox = value;
				base.TrackPropertyChanged("MyNameInToBox");
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x00107346 File Offset: 0x00105546
		// (set) Token: 0x06004DB1 RID: 19889 RVA: 0x0010734E File Offset: 0x0010554E
		[DataMember]
		public bool MyNameInToOrCcBox
		{
			get
			{
				return this.myNameInToOrCcBox;
			}
			set
			{
				this.myNameInToOrCcBox = value;
				base.TrackPropertyChanged("MyNameInToOrCcBox");
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06004DB2 RID: 19890 RVA: 0x00107362 File Offset: 0x00105562
		// (set) Token: 0x06004DB3 RID: 19891 RVA: 0x0010736A File Offset: 0x0010556A
		[DataMember]
		public bool MyNameNotInToBox
		{
			get
			{
				return this.myNameNotInToBox;
			}
			set
			{
				this.myNameNotInToBox = value;
				base.TrackPropertyChanged("MyNameNotInToBox");
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x0010737E File Offset: 0x0010557E
		// (set) Token: 0x06004DB5 RID: 19893 RVA: 0x00107386 File Offset: 0x00105586
		[DataMember]
		public Identity MoveToFolder
		{
			get
			{
				return this.moveToFolder;
			}
			set
			{
				this.moveToFolder = value;
				base.TrackPropertyChanged("MoveToFolder");
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x0010739A File Offset: 0x0010559A
		// (set) Token: 0x06004DB7 RID: 19895 RVA: 0x001073A2 File Offset: 0x001055A2
		[DataMember]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				base.TrackPropertyChanged("Name");
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x001073B6 File Offset: 0x001055B6
		// (set) Token: 0x06004DB9 RID: 19897 RVA: 0x001073BE File Offset: 0x001055BE
		[DataMember]
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
				base.TrackPropertyChanged("Priority");
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06004DBA RID: 19898 RVA: 0x001073D2 File Offset: 0x001055D2
		// (set) Token: 0x06004DBB RID: 19899 RVA: 0x001073DA File Offset: 0x001055DA
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string ReceivedAfterDate
		{
			get
			{
				return this.receivedAfterDate;
			}
			set
			{
				this.receivedAfterDate = value;
				base.TrackPropertyChanged("ReceivedAfterDate");
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004DBC RID: 19900 RVA: 0x001073EE File Offset: 0x001055EE
		// (set) Token: 0x06004DBD RID: 19901 RVA: 0x001073F6 File Offset: 0x001055F6
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string ReceivedBeforeDate
		{
			get
			{
				return this.receivedBeforeDate;
			}
			set
			{
				this.receivedBeforeDate = value;
				base.TrackPropertyChanged("ReceivedBeforeDate");
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06004DBE RID: 19902 RVA: 0x0010740A File Offset: 0x0010560A
		// (set) Token: 0x06004DBF RID: 19903 RVA: 0x00107412 File Offset: 0x00105612
		[DataMember]
		public string[] RecipientAddressContainsWords
		{
			get
			{
				return this.recipientAddressContainsWords;
			}
			set
			{
				this.recipientAddressContainsWords = value;
				base.TrackPropertyChanged("RecipientAddressContainsWords");
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x00107426 File Offset: 0x00105626
		// (set) Token: 0x06004DC1 RID: 19905 RVA: 0x0010742E File Offset: 0x0010562E
		[DataMember]
		public PeopleIdentity[] RedirectTo
		{
			get
			{
				return this.redirectTo;
			}
			set
			{
				this.redirectTo = value;
				base.TrackPropertyChanged("RedirectTo");
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00107442 File Offset: 0x00105642
		// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x0010744A File Offset: 0x0010564A
		[DataMember]
		public string[] SendTextMessageNotificationTo
		{
			get
			{
				return this.sendTextMessageNotificationTo;
			}
			set
			{
				this.sendTextMessageNotificationTo = value;
				base.TrackPropertyChanged("SendTextMessageNotificationTo");
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x0010745E File Offset: 0x0010565E
		// (set) Token: 0x06004DC5 RID: 19909 RVA: 0x00107466 File Offset: 0x00105666
		[DataMember]
		public bool SentOnlyToMe
		{
			get
			{
				return this.sentOnlyToMe;
			}
			set
			{
				this.sentOnlyToMe = value;
				base.TrackPropertyChanged("SentOnlyToMe");
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x0010747A File Offset: 0x0010567A
		// (set) Token: 0x06004DC7 RID: 19911 RVA: 0x00107482 File Offset: 0x00105682
		[DataMember]
		public PeopleIdentity[] SentTo
		{
			get
			{
				return this.sentTo;
			}
			set
			{
				this.sentTo = value;
				base.TrackPropertyChanged("SentTo");
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06004DC8 RID: 19912 RVA: 0x00107496 File Offset: 0x00105696
		// (set) Token: 0x06004DC9 RID: 19913 RVA: 0x0010749E File Offset: 0x0010569E
		[DataMember]
		public bool StopProcessingRules
		{
			get
			{
				return this.stopProcessingRules;
			}
			set
			{
				this.stopProcessingRules = value;
				base.TrackPropertyChanged("StopProcessingRules");
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x001074B2 File Offset: 0x001056B2
		// (set) Token: 0x06004DCB RID: 19915 RVA: 0x001074BA File Offset: 0x001056BA
		[DataMember]
		public string[] SubjectContainsWords
		{
			get
			{
				return this.subjectContainsWords;
			}
			set
			{
				this.subjectContainsWords = value;
				base.TrackPropertyChanged("SubjectContainsWords");
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x001074CE File Offset: 0x001056CE
		// (set) Token: 0x06004DCD RID: 19917 RVA: 0x001074D6 File Offset: 0x001056D6
		[DataMember]
		public string[] SubjectOrBodyContainsWords
		{
			get
			{
				return this.subjectOrBodyContainsWords;
			}
			set
			{
				this.subjectOrBodyContainsWords = value;
				base.TrackPropertyChanged("SubjectOrBodyContainsWords");
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x001074EA File Offset: 0x001056EA
		// (set) Token: 0x06004DCF RID: 19919 RVA: 0x001074F2 File Offset: 0x001056F2
		[IgnoreDataMember]
		public NullableImportance WithImportance
		{
			get
			{
				return this.withImportance;
			}
			set
			{
				this.withImportance = value;
				base.TrackPropertyChanged("WithImportance");
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x00107506 File Offset: 0x00105706
		// (set) Token: 0x06004DD1 RID: 19921 RVA: 0x00107513 File Offset: 0x00105713
		[DataMember(Name = "WithImportance", IsRequired = false, EmitDefaultValue = false)]
		public string WithImportanceString
		{
			get
			{
				return EnumUtilities.ToString<NullableImportance>(this.WithImportance);
			}
			set
			{
				this.WithImportance = EnumUtilities.Parse<NullableImportance>(value);
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x00107521 File Offset: 0x00105721
		// (set) Token: 0x06004DD3 RID: 19923 RVA: 0x00107529 File Offset: 0x00105729
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ulong? WithinSizeRangeMaximum
		{
			get
			{
				return this.withinSizeRangeMaximum;
			}
			set
			{
				this.withinSizeRangeMaximum = value;
				base.TrackPropertyChanged("WithinSizeRangeMaximum");
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x0010753D File Offset: 0x0010573D
		// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x00107545 File Offset: 0x00105745
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public ulong? WithinSizeRangeMinimum
		{
			get
			{
				return this.withinSizeRangeMinimum;
			}
			set
			{
				this.withinSizeRangeMinimum = value;
				base.TrackPropertyChanged("WithinSizeRangeMinimum");
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06004DD6 RID: 19926 RVA: 0x00107559 File Offset: 0x00105759
		// (set) Token: 0x06004DD7 RID: 19927 RVA: 0x00107561 File Offset: 0x00105761
		[IgnoreDataMember]
		public NullableSensitivity WithSensitivity
		{
			get
			{
				return this.withSensitivity;
			}
			set
			{
				this.withSensitivity = value;
				base.TrackPropertyChanged("WithSensitivity");
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x00107575 File Offset: 0x00105775
		// (set) Token: 0x06004DD9 RID: 19929 RVA: 0x00107582 File Offset: 0x00105782
		[DataMember(Name = "WithSensitivity", IsRequired = false, EmitDefaultValue = false)]
		public string WithSensitivityString
		{
			get
			{
				return EnumUtilities.ToString<NullableSensitivity>(this.WithSensitivity);
			}
			set
			{
				this.WithSensitivity = EnumUtilities.Parse<NullableSensitivity>(value);
			}
		}

		// Token: 0x04002B90 RID: 11152
		private string[] applyCategory;

		// Token: 0x04002B91 RID: 11153
		private string[] bodyContainsWords;

		// Token: 0x04002B92 RID: 11154
		private Identity copyToFolder;

		// Token: 0x04002B93 RID: 11155
		private bool deleteMessage;

		// Token: 0x04002B94 RID: 11156
		private string[] exceptIfBodyContainsWords;

		// Token: 0x04002B95 RID: 11157
		private string exceptIfFlaggedForAction;

		// Token: 0x04002B96 RID: 11158
		private PeopleIdentity[] exceptIfFrom;

		// Token: 0x04002B97 RID: 11159
		private string[] exceptIfFromAddressContainsWords;

		// Token: 0x04002B98 RID: 11160
		private Identity[] exceptIfFromSubscription;

		// Token: 0x04002B99 RID: 11161
		private bool exceptIfHasAttachment;

		// Token: 0x04002B9A RID: 11162
		private Identity[] exceptIfHasClassification;

		// Token: 0x04002B9B RID: 11163
		private string[] exceptIfHeaderContainsWords;

		// Token: 0x04002B9C RID: 11164
		private NullableInboxRuleMessageType exceptIfMessageTypeMatches;

		// Token: 0x04002B9D RID: 11165
		private bool exceptIfMyNameInCcBox;

		// Token: 0x04002B9E RID: 11166
		private bool exceptIfMyNameInToBox;

		// Token: 0x04002B9F RID: 11167
		private bool exceptIfMyNameInToOrCcBox;

		// Token: 0x04002BA0 RID: 11168
		private bool exceptIfMyNameNotInToBox;

		// Token: 0x04002BA1 RID: 11169
		private string exceptIfReceivedAfterDate;

		// Token: 0x04002BA2 RID: 11170
		private string exceptIfReceivedBeforeDate;

		// Token: 0x04002BA3 RID: 11171
		private string[] exceptIfRecipientAddressContainsWords;

		// Token: 0x04002BA4 RID: 11172
		private bool exceptIfSentOnlyToMe;

		// Token: 0x04002BA5 RID: 11173
		private PeopleIdentity[] exceptIfSentTo;

		// Token: 0x04002BA6 RID: 11174
		private string[] exceptIfSubjectContainsWords;

		// Token: 0x04002BA7 RID: 11175
		private string[] exceptIfSubjectOrBodyContainsWords;

		// Token: 0x04002BA8 RID: 11176
		private NullableImportance exceptIfWithImportance;

		// Token: 0x04002BA9 RID: 11177
		private NullableSensitivity exceptIfWithSensitivity;

		// Token: 0x04002BAA RID: 11178
		private ulong? exceptIfWithinSizeRangeMaximum;

		// Token: 0x04002BAB RID: 11179
		private ulong? exceptIfWithinSizeRangeMinimum;

		// Token: 0x04002BAC RID: 11180
		private string flaggedForAction;

		// Token: 0x04002BAD RID: 11181
		private PeopleIdentity[] forwardAsAttachmentTo;

		// Token: 0x04002BAE RID: 11182
		private PeopleIdentity[] forwardTo;

		// Token: 0x04002BAF RID: 11183
		private PeopleIdentity[] from;

		// Token: 0x04002BB0 RID: 11184
		private string[] fromAddressContainsWords;

		// Token: 0x04002BB1 RID: 11185
		private Identity[] fromSubscription;

		// Token: 0x04002BB2 RID: 11186
		private bool hasAttachment;

		// Token: 0x04002BB3 RID: 11187
		private Identity[] hasClassification;

		// Token: 0x04002BB4 RID: 11188
		private string[] headerContainsWords;

		// Token: 0x04002BB5 RID: 11189
		private bool markAsRead;

		// Token: 0x04002BB6 RID: 11190
		private NullableImportance markImportance;

		// Token: 0x04002BB7 RID: 11191
		private NullableInboxRuleMessageType messageTypeMatches;

		// Token: 0x04002BB8 RID: 11192
		private Identity moveToFolder;

		// Token: 0x04002BB9 RID: 11193
		private bool myNameInCcBox;

		// Token: 0x04002BBA RID: 11194
		private bool myNameInToBox;

		// Token: 0x04002BBB RID: 11195
		private bool myNameInToOrCcBox;

		// Token: 0x04002BBC RID: 11196
		private bool myNameNotInToBox;

		// Token: 0x04002BBD RID: 11197
		private string name;

		// Token: 0x04002BBE RID: 11198
		private int priority;

		// Token: 0x04002BBF RID: 11199
		private string receivedAfterDate;

		// Token: 0x04002BC0 RID: 11200
		private string receivedBeforeDate;

		// Token: 0x04002BC1 RID: 11201
		private string[] recipientAddressContainsWords;

		// Token: 0x04002BC2 RID: 11202
		private PeopleIdentity[] redirectTo;

		// Token: 0x04002BC3 RID: 11203
		private string[] sendTextMessageNotificationTo;

		// Token: 0x04002BC4 RID: 11204
		private bool sentOnlyToMe;

		// Token: 0x04002BC5 RID: 11205
		private PeopleIdentity[] sentTo;

		// Token: 0x04002BC6 RID: 11206
		private bool stopProcessingRules;

		// Token: 0x04002BC7 RID: 11207
		private string[] subjectContainsWords;

		// Token: 0x04002BC8 RID: 11208
		private string[] subjectOrBodyContainsWords;

		// Token: 0x04002BC9 RID: 11209
		private NullableImportance withImportance;

		// Token: 0x04002BCA RID: 11210
		private NullableSensitivity withSensitivity;

		// Token: 0x04002BCB RID: 11211
		private ulong? withinSizeRangeMaximum;

		// Token: 0x04002BCC RID: 11212
		private ulong? withinSizeRangeMinimum;
	}
}
