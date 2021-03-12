using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E8 RID: 232
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PersonaType
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x000206EA File Offset: 0x0001E8EA
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x000206F2 File Offset: 0x0001E8F2
		public ItemIdType PersonaId
		{
			get
			{
				return this.personaIdField;
			}
			set
			{
				this.personaIdField = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x000206FB File Offset: 0x0001E8FB
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00020703 File Offset: 0x0001E903
		[XmlElement("PersonaType")]
		public string PersonaType1
		{
			get
			{
				return this.personaType1Field;
			}
			set
			{
				this.personaType1Field = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002070C File Offset: 0x0001E90C
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00020714 File Offset: 0x0001E914
		public string PersonaObjectStatus
		{
			get
			{
				return this.personaObjectStatusField;
			}
			set
			{
				this.personaObjectStatusField = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002071D File Offset: 0x0001E91D
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00020725 File Offset: 0x0001E925
		public DateTime CreationTime
		{
			get
			{
				return this.creationTimeField;
			}
			set
			{
				this.creationTimeField = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002072E File Offset: 0x0001E92E
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00020736 File Offset: 0x0001E936
		[XmlIgnore]
		public bool CreationTimeSpecified
		{
			get
			{
				return this.creationTimeFieldSpecified;
			}
			set
			{
				this.creationTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002073F File Offset: 0x0001E93F
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00020747 File Offset: 0x0001E947
		[XmlArrayItem("BodyContentAttributedValue", IsNullable = false)]
		public BodyContentAttributedValueType[] Bodies
		{
			get
			{
				return this.bodiesField;
			}
			set
			{
				this.bodiesField = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00020750 File Offset: 0x0001E950
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00020758 File Offset: 0x0001E958
		public string DisplayNameFirstLastSortKey
		{
			get
			{
				return this.displayNameFirstLastSortKeyField;
			}
			set
			{
				this.displayNameFirstLastSortKeyField = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00020761 File Offset: 0x0001E961
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00020769 File Offset: 0x0001E969
		public string DisplayNameLastFirstSortKey
		{
			get
			{
				return this.displayNameLastFirstSortKeyField;
			}
			set
			{
				this.displayNameLastFirstSortKeyField = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00020772 File Offset: 0x0001E972
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0002077A File Offset: 0x0001E97A
		public string CompanyNameSortKey
		{
			get
			{
				return this.companyNameSortKeyField;
			}
			set
			{
				this.companyNameSortKeyField = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00020783 File Offset: 0x0001E983
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0002078B File Offset: 0x0001E98B
		public string HomeCitySortKey
		{
			get
			{
				return this.homeCitySortKeyField;
			}
			set
			{
				this.homeCitySortKeyField = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00020794 File Offset: 0x0001E994
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0002079C File Offset: 0x0001E99C
		public string WorkCitySortKey
		{
			get
			{
				return this.workCitySortKeyField;
			}
			set
			{
				this.workCitySortKeyField = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000207A5 File Offset: 0x0001E9A5
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000207AD File Offset: 0x0001E9AD
		public string DisplayNameFirstLastHeader
		{
			get
			{
				return this.displayNameFirstLastHeaderField;
			}
			set
			{
				this.displayNameFirstLastHeaderField = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000207B6 File Offset: 0x0001E9B6
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x000207BE File Offset: 0x0001E9BE
		public string DisplayNameLastFirstHeader
		{
			get
			{
				return this.displayNameLastFirstHeaderField;
			}
			set
			{
				this.displayNameLastFirstHeaderField = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000207C7 File Offset: 0x0001E9C7
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x000207CF File Offset: 0x0001E9CF
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x000207D8 File Offset: 0x0001E9D8
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x000207E0 File Offset: 0x0001E9E0
		public string DisplayNameFirstLast
		{
			get
			{
				return this.displayNameFirstLastField;
			}
			set
			{
				this.displayNameFirstLastField = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000207E9 File Offset: 0x0001E9E9
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x000207F1 File Offset: 0x0001E9F1
		public string DisplayNameLastFirst
		{
			get
			{
				return this.displayNameLastFirstField;
			}
			set
			{
				this.displayNameLastFirstField = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000207FA File Offset: 0x0001E9FA
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x00020802 File Offset: 0x0001EA02
		public string FileAs
		{
			get
			{
				return this.fileAsField;
			}
			set
			{
				this.fileAsField = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0002080B File Offset: 0x0001EA0B
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00020813 File Offset: 0x0001EA13
		public string FileAsId
		{
			get
			{
				return this.fileAsIdField;
			}
			set
			{
				this.fileAsIdField = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002081C File Offset: 0x0001EA1C
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00020824 File Offset: 0x0001EA24
		public string DisplayNamePrefix
		{
			get
			{
				return this.displayNamePrefixField;
			}
			set
			{
				this.displayNamePrefixField = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002082D File Offset: 0x0001EA2D
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00020835 File Offset: 0x0001EA35
		public string GivenName
		{
			get
			{
				return this.givenNameField;
			}
			set
			{
				this.givenNameField = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002083E File Offset: 0x0001EA3E
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00020846 File Offset: 0x0001EA46
		public string MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002084F File Offset: 0x0001EA4F
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x00020857 File Offset: 0x0001EA57
		public string Surname
		{
			get
			{
				return this.surnameField;
			}
			set
			{
				this.surnameField = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00020860 File Offset: 0x0001EA60
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00020868 File Offset: 0x0001EA68
		public string Generation
		{
			get
			{
				return this.generationField;
			}
			set
			{
				this.generationField = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00020871 File Offset: 0x0001EA71
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x00020879 File Offset: 0x0001EA79
		public string Nickname
		{
			get
			{
				return this.nicknameField;
			}
			set
			{
				this.nicknameField = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00020882 File Offset: 0x0001EA82
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0002088A File Offset: 0x0001EA8A
		public string YomiCompanyName
		{
			get
			{
				return this.yomiCompanyNameField;
			}
			set
			{
				this.yomiCompanyNameField = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00020893 File Offset: 0x0001EA93
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0002089B File Offset: 0x0001EA9B
		public string YomiFirstName
		{
			get
			{
				return this.yomiFirstNameField;
			}
			set
			{
				this.yomiFirstNameField = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000208A4 File Offset: 0x0001EAA4
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000208AC File Offset: 0x0001EAAC
		public string YomiLastName
		{
			get
			{
				return this.yomiLastNameField;
			}
			set
			{
				this.yomiLastNameField = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000208B5 File Offset: 0x0001EAB5
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x000208BD File Offset: 0x0001EABD
		public string Title
		{
			get
			{
				return this.titleField;
			}
			set
			{
				this.titleField = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000208C6 File Offset: 0x0001EAC6
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x000208CE File Offset: 0x0001EACE
		public string Department
		{
			get
			{
				return this.departmentField;
			}
			set
			{
				this.departmentField = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x000208D7 File Offset: 0x0001EAD7
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x000208DF File Offset: 0x0001EADF
		public string CompanyName
		{
			get
			{
				return this.companyNameField;
			}
			set
			{
				this.companyNameField = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000208E8 File Offset: 0x0001EAE8
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public string Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x000208F9 File Offset: 0x0001EAF9
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00020901 File Offset: 0x0001EB01
		public EmailAddressType EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002090A File Offset: 0x0001EB0A
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x00020912 File Offset: 0x0001EB12
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002091B File Offset: 0x0001EB1B
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x00020923 File Offset: 0x0001EB23
		public PersonaPhoneNumberType PhoneNumber
		{
			get
			{
				return this.phoneNumberField;
			}
			set
			{
				this.phoneNumberField = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002092C File Offset: 0x0001EB2C
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x00020934 File Offset: 0x0001EB34
		public string ImAddress
		{
			get
			{
				return this.imAddressField;
			}
			set
			{
				this.imAddressField = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0002093D File Offset: 0x0001EB3D
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00020945 File Offset: 0x0001EB45
		public string HomeCity
		{
			get
			{
				return this.homeCityField;
			}
			set
			{
				this.homeCityField = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0002094E File Offset: 0x0001EB4E
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00020956 File Offset: 0x0001EB56
		public string WorkCity
		{
			get
			{
				return this.workCityField;
			}
			set
			{
				this.workCityField = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002095F File Offset: 0x0001EB5F
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00020967 File Offset: 0x0001EB67
		public int RelevanceScore
		{
			get
			{
				return this.relevanceScoreField;
			}
			set
			{
				this.relevanceScoreField = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00020970 File Offset: 0x0001EB70
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00020978 File Offset: 0x0001EB78
		[XmlIgnore]
		public bool RelevanceScoreSpecified
		{
			get
			{
				return this.relevanceScoreFieldSpecified;
			}
			set
			{
				this.relevanceScoreFieldSpecified = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00020981 File Offset: 0x0001EB81
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00020989 File Offset: 0x0001EB89
		[XmlArrayItem("FolderId", IsNullable = false)]
		public FolderIdType[] FolderIds
		{
			get
			{
				return this.folderIdsField;
			}
			set
			{
				this.folderIdsField = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00020992 File Offset: 0x0001EB92
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0002099A File Offset: 0x0001EB9A
		[XmlArrayItem("Attribution", IsNullable = false)]
		public PersonaAttributionType[] Attributions
		{
			get
			{
				return this.attributionsField;
			}
			set
			{
				this.attributionsField = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000209A3 File Offset: 0x0001EBA3
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x000209AB File Offset: 0x0001EBAB
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] DisplayNames
		{
			get
			{
				return this.displayNamesField;
			}
			set
			{
				this.displayNamesField = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000209B4 File Offset: 0x0001EBB4
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x000209BC File Offset: 0x0001EBBC
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] FileAses
		{
			get
			{
				return this.fileAsesField;
			}
			set
			{
				this.fileAsesField = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000209C5 File Offset: 0x0001EBC5
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x000209CD File Offset: 0x0001EBCD
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] FileAsIds
		{
			get
			{
				return this.fileAsIdsField;
			}
			set
			{
				this.fileAsIdsField = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000209D6 File Offset: 0x0001EBD6
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000209DE File Offset: 0x0001EBDE
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] DisplayNamePrefixes
		{
			get
			{
				return this.displayNamePrefixesField;
			}
			set
			{
				this.displayNamePrefixesField = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000209E7 File Offset: 0x0001EBE7
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000209EF File Offset: 0x0001EBEF
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] GivenNames
		{
			get
			{
				return this.givenNamesField;
			}
			set
			{
				this.givenNamesField = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000209F8 File Offset: 0x0001EBF8
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00020A00 File Offset: 0x0001EC00
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] MiddleNames
		{
			get
			{
				return this.middleNamesField;
			}
			set
			{
				this.middleNamesField = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00020A09 File Offset: 0x0001EC09
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00020A11 File Offset: 0x0001EC11
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Surnames
		{
			get
			{
				return this.surnamesField;
			}
			set
			{
				this.surnamesField = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00020A1A File Offset: 0x0001EC1A
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x00020A22 File Offset: 0x0001EC22
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Generations
		{
			get
			{
				return this.generationsField;
			}
			set
			{
				this.generationsField = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00020A2B File Offset: 0x0001EC2B
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00020A33 File Offset: 0x0001EC33
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Nicknames
		{
			get
			{
				return this.nicknamesField;
			}
			set
			{
				this.nicknamesField = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00020A3C File Offset: 0x0001EC3C
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x00020A44 File Offset: 0x0001EC44
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00020A4D File Offset: 0x0001EC4D
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x00020A55 File Offset: 0x0001EC55
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] YomiCompanyNames
		{
			get
			{
				return this.yomiCompanyNamesField;
			}
			set
			{
				this.yomiCompanyNamesField = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00020A5E File Offset: 0x0001EC5E
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x00020A66 File Offset: 0x0001EC66
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] YomiFirstNames
		{
			get
			{
				return this.yomiFirstNamesField;
			}
			set
			{
				this.yomiFirstNamesField = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00020A6F File Offset: 0x0001EC6F
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x00020A77 File Offset: 0x0001EC77
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] YomiLastNames
		{
			get
			{
				return this.yomiLastNamesField;
			}
			set
			{
				this.yomiLastNamesField = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00020A80 File Offset: 0x0001EC80
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x00020A88 File Offset: 0x0001EC88
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] BusinessPhoneNumbers
		{
			get
			{
				return this.businessPhoneNumbersField;
			}
			set
			{
				this.businessPhoneNumbersField = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00020A91 File Offset: 0x0001EC91
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00020A99 File Offset: 0x0001EC99
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] BusinessPhoneNumbers2
		{
			get
			{
				return this.businessPhoneNumbers2Field;
			}
			set
			{
				this.businessPhoneNumbers2Field = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00020AA2 File Offset: 0x0001ECA2
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x00020AAA File Offset: 0x0001ECAA
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] HomePhones
		{
			get
			{
				return this.homePhonesField;
			}
			set
			{
				this.homePhonesField = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00020AB3 File Offset: 0x0001ECB3
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00020ABB File Offset: 0x0001ECBB
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] HomePhones2
		{
			get
			{
				return this.homePhones2Field;
			}
			set
			{
				this.homePhones2Field = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00020ACC File Offset: 0x0001ECCC
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] MobilePhones
		{
			get
			{
				return this.mobilePhonesField;
			}
			set
			{
				this.mobilePhonesField = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00020AD5 File Offset: 0x0001ECD5
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00020ADD File Offset: 0x0001ECDD
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] MobilePhones2
		{
			get
			{
				return this.mobilePhones2Field;
			}
			set
			{
				this.mobilePhones2Field = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00020AE6 File Offset: 0x0001ECE6
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00020AEE File Offset: 0x0001ECEE
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] AssistantPhoneNumbers
		{
			get
			{
				return this.assistantPhoneNumbersField;
			}
			set
			{
				this.assistantPhoneNumbersField = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00020AF7 File Offset: 0x0001ECF7
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00020AFF File Offset: 0x0001ECFF
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] CallbackPhones
		{
			get
			{
				return this.callbackPhonesField;
			}
			set
			{
				this.callbackPhonesField = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00020B08 File Offset: 0x0001ED08
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x00020B10 File Offset: 0x0001ED10
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] CarPhones
		{
			get
			{
				return this.carPhonesField;
			}
			set
			{
				this.carPhonesField = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00020B19 File Offset: 0x0001ED19
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x00020B21 File Offset: 0x0001ED21
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] HomeFaxes
		{
			get
			{
				return this.homeFaxesField;
			}
			set
			{
				this.homeFaxesField = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00020B2A File Offset: 0x0001ED2A
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x00020B32 File Offset: 0x0001ED32
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OrganizationMainPhones
		{
			get
			{
				return this.organizationMainPhonesField;
			}
			set
			{
				this.organizationMainPhonesField = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00020B3B File Offset: 0x0001ED3B
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x00020B43 File Offset: 0x0001ED43
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OtherFaxes
		{
			get
			{
				return this.otherFaxesField;
			}
			set
			{
				this.otherFaxesField = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00020B4C File Offset: 0x0001ED4C
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x00020B54 File Offset: 0x0001ED54
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OtherTelephones
		{
			get
			{
				return this.otherTelephonesField;
			}
			set
			{
				this.otherTelephonesField = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00020B5D File Offset: 0x0001ED5D
		// (set) Token: 0x06000ADE RID: 2782 RVA: 0x00020B65 File Offset: 0x0001ED65
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OtherPhones2
		{
			get
			{
				return this.otherPhones2Field;
			}
			set
			{
				this.otherPhones2Field = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00020B6E File Offset: 0x0001ED6E
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x00020B76 File Offset: 0x0001ED76
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] Pagers
		{
			get
			{
				return this.pagersField;
			}
			set
			{
				this.pagersField = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00020B7F File Offset: 0x0001ED7F
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00020B87 File Offset: 0x0001ED87
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] RadioPhones
		{
			get
			{
				return this.radioPhonesField;
			}
			set
			{
				this.radioPhonesField = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00020B90 File Offset: 0x0001ED90
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x00020B98 File Offset: 0x0001ED98
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] TelexNumbers
		{
			get
			{
				return this.telexNumbersField;
			}
			set
			{
				this.telexNumbersField = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00020BA1 File Offset: 0x0001EDA1
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00020BA9 File Offset: 0x0001EDA9
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] TTYTDDPhoneNumbers
		{
			get
			{
				return this.tTYTDDPhoneNumbersField;
			}
			set
			{
				this.tTYTDDPhoneNumbersField = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00020BB2 File Offset: 0x0001EDB2
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00020BBA File Offset: 0x0001EDBA
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] WorkFaxes
		{
			get
			{
				return this.workFaxesField;
			}
			set
			{
				this.workFaxesField = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00020BC3 File Offset: 0x0001EDC3
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00020BCB File Offset: 0x0001EDCB
		[XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
		public EmailAddressAttributedValueType[] Emails1
		{
			get
			{
				return this.emails1Field;
			}
			set
			{
				this.emails1Field = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00020BDC File Offset: 0x0001EDDC
		[XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
		public EmailAddressAttributedValueType[] Emails2
		{
			get
			{
				return this.emails2Field;
			}
			set
			{
				this.emails2Field = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00020BE5 File Offset: 0x0001EDE5
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00020BED File Offset: 0x0001EDED
		[XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
		public EmailAddressAttributedValueType[] Emails3
		{
			get
			{
				return this.emails3Field;
			}
			set
			{
				this.emails3Field = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00020BF6 File Offset: 0x0001EDF6
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00020BFE File Offset: 0x0001EDFE
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] BusinessHomePages
		{
			get
			{
				return this.businessHomePagesField;
			}
			set
			{
				this.businessHomePagesField = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00020C07 File Offset: 0x0001EE07
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x00020C0F File Offset: 0x0001EE0F
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] PersonalHomePages
		{
			get
			{
				return this.personalHomePagesField;
			}
			set
			{
				this.personalHomePagesField = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00020C18 File Offset: 0x0001EE18
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x00020C20 File Offset: 0x0001EE20
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] OfficeLocations
		{
			get
			{
				return this.officeLocationsField;
			}
			set
			{
				this.officeLocationsField = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00020C29 File Offset: 0x0001EE29
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x00020C31 File Offset: 0x0001EE31
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] ImAddresses
		{
			get
			{
				return this.imAddressesField;
			}
			set
			{
				this.imAddressesField = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00020C3A File Offset: 0x0001EE3A
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00020C42 File Offset: 0x0001EE42
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] ImAddresses2
		{
			get
			{
				return this.imAddresses2Field;
			}
			set
			{
				this.imAddresses2Field = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00020C4B File Offset: 0x0001EE4B
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00020C53 File Offset: 0x0001EE53
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] ImAddresses3
		{
			get
			{
				return this.imAddresses3Field;
			}
			set
			{
				this.imAddresses3Field = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00020C5C File Offset: 0x0001EE5C
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x00020C64 File Offset: 0x0001EE64
		[XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
		public PostalAddressAttributedValueType[] BusinessAddresses
		{
			get
			{
				return this.businessAddressesField;
			}
			set
			{
				this.businessAddressesField = value;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00020C6D File Offset: 0x0001EE6D
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00020C75 File Offset: 0x0001EE75
		[XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
		public PostalAddressAttributedValueType[] HomeAddresses
		{
			get
			{
				return this.homeAddressesField;
			}
			set
			{
				this.homeAddressesField = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00020C7E File Offset: 0x0001EE7E
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x00020C86 File Offset: 0x0001EE86
		[XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
		public PostalAddressAttributedValueType[] OtherAddresses
		{
			get
			{
				return this.otherAddressesField;
			}
			set
			{
				this.otherAddressesField = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00020C8F File Offset: 0x0001EE8F
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00020C97 File Offset: 0x0001EE97
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Titles
		{
			get
			{
				return this.titlesField;
			}
			set
			{
				this.titlesField = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00020CA0 File Offset: 0x0001EEA0
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Departments
		{
			get
			{
				return this.departmentsField;
			}
			set
			{
				this.departmentsField = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00020CB9 File Offset: 0x0001EEB9
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] CompanyNames
		{
			get
			{
				return this.companyNamesField;
			}
			set
			{
				this.companyNamesField = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00020CC2 File Offset: 0x0001EEC2
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00020CCA File Offset: 0x0001EECA
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Managers
		{
			get
			{
				return this.managersField;
			}
			set
			{
				this.managersField = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00020CD3 File Offset: 0x0001EED3
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00020CDB File Offset: 0x0001EEDB
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] AssistantNames
		{
			get
			{
				return this.assistantNamesField;
			}
			set
			{
				this.assistantNamesField = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00020CEC File Offset: 0x0001EEEC
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Professions
		{
			get
			{
				return this.professionsField;
			}
			set
			{
				this.professionsField = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00020CF5 File Offset: 0x0001EEF5
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00020CFD File Offset: 0x0001EEFD
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] SpouseNames
		{
			get
			{
				return this.spouseNamesField;
			}
			set
			{
				this.spouseNamesField = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00020D06 File Offset: 0x0001EF06
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00020D0E File Offset: 0x0001EF0E
		[XmlArrayItem("StringArrayAttributedValue", IsNullable = false)]
		public StringArrayAttributedValueType[] Children
		{
			get
			{
				return this.childrenField;
			}
			set
			{
				this.childrenField = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00020D17 File Offset: 0x0001EF17
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00020D1F File Offset: 0x0001EF1F
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Schools
		{
			get
			{
				return this.schoolsField;
			}
			set
			{
				this.schoolsField = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00020D28 File Offset: 0x0001EF28
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x00020D30 File Offset: 0x0001EF30
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Hobbies
		{
			get
			{
				return this.hobbiesField;
			}
			set
			{
				this.hobbiesField = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00020D39 File Offset: 0x0001EF39
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x00020D41 File Offset: 0x0001EF41
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] WeddingAnniversaries
		{
			get
			{
				return this.weddingAnniversariesField;
			}
			set
			{
				this.weddingAnniversariesField = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00020D4A File Offset: 0x0001EF4A
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00020D52 File Offset: 0x0001EF52
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Birthdays
		{
			get
			{
				return this.birthdaysField;
			}
			set
			{
				this.birthdaysField = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00020D5B File Offset: 0x0001EF5B
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00020D63 File Offset: 0x0001EF63
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Locations
		{
			get
			{
				return this.locationsField;
			}
			set
			{
				this.locationsField = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00020D6C File Offset: 0x0001EF6C
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00020D74 File Offset: 0x0001EF74
		[XmlArrayItem("ExtendedPropertyAttributedValue", IsNullable = false)]
		public ExtendedPropertyAttributedValueType[] ExtendedProperties
		{
			get
			{
				return this.extendedPropertiesField;
			}
			set
			{
				this.extendedPropertiesField = value;
			}
		}

		// Token: 0x04000783 RID: 1923
		private ItemIdType personaIdField;

		// Token: 0x04000784 RID: 1924
		private string personaType1Field;

		// Token: 0x04000785 RID: 1925
		private string personaObjectStatusField;

		// Token: 0x04000786 RID: 1926
		private DateTime creationTimeField;

		// Token: 0x04000787 RID: 1927
		private bool creationTimeFieldSpecified;

		// Token: 0x04000788 RID: 1928
		private BodyContentAttributedValueType[] bodiesField;

		// Token: 0x04000789 RID: 1929
		private string displayNameFirstLastSortKeyField;

		// Token: 0x0400078A RID: 1930
		private string displayNameLastFirstSortKeyField;

		// Token: 0x0400078B RID: 1931
		private string companyNameSortKeyField;

		// Token: 0x0400078C RID: 1932
		private string homeCitySortKeyField;

		// Token: 0x0400078D RID: 1933
		private string workCitySortKeyField;

		// Token: 0x0400078E RID: 1934
		private string displayNameFirstLastHeaderField;

		// Token: 0x0400078F RID: 1935
		private string displayNameLastFirstHeaderField;

		// Token: 0x04000790 RID: 1936
		private string displayNameField;

		// Token: 0x04000791 RID: 1937
		private string displayNameFirstLastField;

		// Token: 0x04000792 RID: 1938
		private string displayNameLastFirstField;

		// Token: 0x04000793 RID: 1939
		private string fileAsField;

		// Token: 0x04000794 RID: 1940
		private string fileAsIdField;

		// Token: 0x04000795 RID: 1941
		private string displayNamePrefixField;

		// Token: 0x04000796 RID: 1942
		private string givenNameField;

		// Token: 0x04000797 RID: 1943
		private string middleNameField;

		// Token: 0x04000798 RID: 1944
		private string surnameField;

		// Token: 0x04000799 RID: 1945
		private string generationField;

		// Token: 0x0400079A RID: 1946
		private string nicknameField;

		// Token: 0x0400079B RID: 1947
		private string yomiCompanyNameField;

		// Token: 0x0400079C RID: 1948
		private string yomiFirstNameField;

		// Token: 0x0400079D RID: 1949
		private string yomiLastNameField;

		// Token: 0x0400079E RID: 1950
		private string titleField;

		// Token: 0x0400079F RID: 1951
		private string departmentField;

		// Token: 0x040007A0 RID: 1952
		private string companyNameField;

		// Token: 0x040007A1 RID: 1953
		private string locationField;

		// Token: 0x040007A2 RID: 1954
		private EmailAddressType emailAddressField;

		// Token: 0x040007A3 RID: 1955
		private EmailAddressType[] emailAddressesField;

		// Token: 0x040007A4 RID: 1956
		private PersonaPhoneNumberType phoneNumberField;

		// Token: 0x040007A5 RID: 1957
		private string imAddressField;

		// Token: 0x040007A6 RID: 1958
		private string homeCityField;

		// Token: 0x040007A7 RID: 1959
		private string workCityField;

		// Token: 0x040007A8 RID: 1960
		private int relevanceScoreField;

		// Token: 0x040007A9 RID: 1961
		private bool relevanceScoreFieldSpecified;

		// Token: 0x040007AA RID: 1962
		private FolderIdType[] folderIdsField;

		// Token: 0x040007AB RID: 1963
		private PersonaAttributionType[] attributionsField;

		// Token: 0x040007AC RID: 1964
		private StringAttributedValueType[] displayNamesField;

		// Token: 0x040007AD RID: 1965
		private StringAttributedValueType[] fileAsesField;

		// Token: 0x040007AE RID: 1966
		private StringAttributedValueType[] fileAsIdsField;

		// Token: 0x040007AF RID: 1967
		private StringAttributedValueType[] displayNamePrefixesField;

		// Token: 0x040007B0 RID: 1968
		private StringAttributedValueType[] givenNamesField;

		// Token: 0x040007B1 RID: 1969
		private StringAttributedValueType[] middleNamesField;

		// Token: 0x040007B2 RID: 1970
		private StringAttributedValueType[] surnamesField;

		// Token: 0x040007B3 RID: 1971
		private StringAttributedValueType[] generationsField;

		// Token: 0x040007B4 RID: 1972
		private StringAttributedValueType[] nicknamesField;

		// Token: 0x040007B5 RID: 1973
		private StringAttributedValueType[] initialsField;

		// Token: 0x040007B6 RID: 1974
		private StringAttributedValueType[] yomiCompanyNamesField;

		// Token: 0x040007B7 RID: 1975
		private StringAttributedValueType[] yomiFirstNamesField;

		// Token: 0x040007B8 RID: 1976
		private StringAttributedValueType[] yomiLastNamesField;

		// Token: 0x040007B9 RID: 1977
		private PhoneNumberAttributedValueType[] businessPhoneNumbersField;

		// Token: 0x040007BA RID: 1978
		private PhoneNumberAttributedValueType[] businessPhoneNumbers2Field;

		// Token: 0x040007BB RID: 1979
		private PhoneNumberAttributedValueType[] homePhonesField;

		// Token: 0x040007BC RID: 1980
		private PhoneNumberAttributedValueType[] homePhones2Field;

		// Token: 0x040007BD RID: 1981
		private PhoneNumberAttributedValueType[] mobilePhonesField;

		// Token: 0x040007BE RID: 1982
		private PhoneNumberAttributedValueType[] mobilePhones2Field;

		// Token: 0x040007BF RID: 1983
		private PhoneNumberAttributedValueType[] assistantPhoneNumbersField;

		// Token: 0x040007C0 RID: 1984
		private PhoneNumberAttributedValueType[] callbackPhonesField;

		// Token: 0x040007C1 RID: 1985
		private PhoneNumberAttributedValueType[] carPhonesField;

		// Token: 0x040007C2 RID: 1986
		private PhoneNumberAttributedValueType[] homeFaxesField;

		// Token: 0x040007C3 RID: 1987
		private PhoneNumberAttributedValueType[] organizationMainPhonesField;

		// Token: 0x040007C4 RID: 1988
		private PhoneNumberAttributedValueType[] otherFaxesField;

		// Token: 0x040007C5 RID: 1989
		private PhoneNumberAttributedValueType[] otherTelephonesField;

		// Token: 0x040007C6 RID: 1990
		private PhoneNumberAttributedValueType[] otherPhones2Field;

		// Token: 0x040007C7 RID: 1991
		private PhoneNumberAttributedValueType[] pagersField;

		// Token: 0x040007C8 RID: 1992
		private PhoneNumberAttributedValueType[] radioPhonesField;

		// Token: 0x040007C9 RID: 1993
		private PhoneNumberAttributedValueType[] telexNumbersField;

		// Token: 0x040007CA RID: 1994
		private PhoneNumberAttributedValueType[] tTYTDDPhoneNumbersField;

		// Token: 0x040007CB RID: 1995
		private PhoneNumberAttributedValueType[] workFaxesField;

		// Token: 0x040007CC RID: 1996
		private EmailAddressAttributedValueType[] emails1Field;

		// Token: 0x040007CD RID: 1997
		private EmailAddressAttributedValueType[] emails2Field;

		// Token: 0x040007CE RID: 1998
		private EmailAddressAttributedValueType[] emails3Field;

		// Token: 0x040007CF RID: 1999
		private StringAttributedValueType[] businessHomePagesField;

		// Token: 0x040007D0 RID: 2000
		private StringAttributedValueType[] personalHomePagesField;

		// Token: 0x040007D1 RID: 2001
		private StringAttributedValueType[] officeLocationsField;

		// Token: 0x040007D2 RID: 2002
		private StringAttributedValueType[] imAddressesField;

		// Token: 0x040007D3 RID: 2003
		private StringAttributedValueType[] imAddresses2Field;

		// Token: 0x040007D4 RID: 2004
		private StringAttributedValueType[] imAddresses3Field;

		// Token: 0x040007D5 RID: 2005
		private PostalAddressAttributedValueType[] businessAddressesField;

		// Token: 0x040007D6 RID: 2006
		private PostalAddressAttributedValueType[] homeAddressesField;

		// Token: 0x040007D7 RID: 2007
		private PostalAddressAttributedValueType[] otherAddressesField;

		// Token: 0x040007D8 RID: 2008
		private StringAttributedValueType[] titlesField;

		// Token: 0x040007D9 RID: 2009
		private StringAttributedValueType[] departmentsField;

		// Token: 0x040007DA RID: 2010
		private StringAttributedValueType[] companyNamesField;

		// Token: 0x040007DB RID: 2011
		private StringAttributedValueType[] managersField;

		// Token: 0x040007DC RID: 2012
		private StringAttributedValueType[] assistantNamesField;

		// Token: 0x040007DD RID: 2013
		private StringAttributedValueType[] professionsField;

		// Token: 0x040007DE RID: 2014
		private StringAttributedValueType[] spouseNamesField;

		// Token: 0x040007DF RID: 2015
		private StringArrayAttributedValueType[] childrenField;

		// Token: 0x040007E0 RID: 2016
		private StringAttributedValueType[] schoolsField;

		// Token: 0x040007E1 RID: 2017
		private StringAttributedValueType[] hobbiesField;

		// Token: 0x040007E2 RID: 2018
		private StringAttributedValueType[] weddingAnniversariesField;

		// Token: 0x040007E3 RID: 2019
		private StringAttributedValueType[] birthdaysField;

		// Token: 0x040007E4 RID: 2020
		private StringAttributedValueType[] locationsField;

		// Token: 0x040007E5 RID: 2021
		private ExtendedPropertyAttributedValueType[] extendedPropertiesField;
	}
}
