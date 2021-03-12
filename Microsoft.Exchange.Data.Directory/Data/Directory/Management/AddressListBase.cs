using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006D7 RID: 1751
	[Serializable]
	public abstract class AddressListBase : ADPresentationObject, ISupportRecipientFilter
	{
		// Token: 0x060050E0 RID: 20704 RVA: 0x0012C4A6 File Offset: 0x0012A6A6
		public AddressListBase()
		{
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0012C4AE File Offset: 0x0012A6AE
		public AddressListBase(AddressBookBase dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001A7B RID: 6779
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x0012C4B7 File Offset: 0x0012A6B7
		// (set) Token: 0x060050E3 RID: 20707 RVA: 0x0012C4C9 File Offset: 0x0012A6C9
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return (string)this[AddressListBaseSchema.Name];
			}
			set
			{
				this[AddressListBaseSchema.Name] = value;
			}
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x060050E4 RID: 20708 RVA: 0x0012C4D7 File Offset: 0x0012A6D7
		public string RecipientFilter
		{
			get
			{
				return (string)this[AddressListBaseSchema.RecipientFilter];
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x060050E5 RID: 20709 RVA: 0x0012C4E9 File Offset: 0x0012A6E9
		public string LdapRecipientFilter
		{
			get
			{
				return (string)this[AddressListBaseSchema.LdapRecipientFilter];
			}
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x0012C4FB File Offset: 0x0012A6FB
		public string LastUpdatedRecipientFilter
		{
			get
			{
				return (string)this[AddressListBaseSchema.LastUpdatedRecipientFilter];
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x060050E7 RID: 20711 RVA: 0x0012C50D File Offset: 0x0012A70D
		public bool RecipientFilterApplied
		{
			get
			{
				return (bool)this[AddressListBaseSchema.RecipientFilterApplied];
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x0012C51F File Offset: 0x0012A71F
		// (set) Token: 0x060050E9 RID: 20713 RVA: 0x0012C531 File Offset: 0x0012A731
		[Parameter]
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return (WellKnownRecipientType?)this[AddressListBaseSchema.IncludedRecipients];
			}
			set
			{
				this[AddressListBaseSchema.IncludedRecipients] = value;
			}
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x060050EA RID: 20714 RVA: 0x0012C544 File Offset: 0x0012A744
		// (set) Token: 0x060050EB RID: 20715 RVA: 0x0012C556 File Offset: 0x0012A756
		[Parameter]
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalDepartment];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalDepartment] = value;
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x0012C564 File Offset: 0x0012A764
		// (set) Token: 0x060050ED RID: 20717 RVA: 0x0012C576 File Offset: 0x0012A776
		[Parameter]
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCompany];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCompany] = value;
			}
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x060050EE RID: 20718 RVA: 0x0012C584 File Offset: 0x0012A784
		// (set) Token: 0x060050EF RID: 20719 RVA: 0x0012C596 File Offset: 0x0012A796
		[Parameter]
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalStateOrProvince];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalStateOrProvince] = value;
			}
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x060050F0 RID: 20720 RVA: 0x0012C5A4 File Offset: 0x0012A7A4
		// (set) Token: 0x060050F1 RID: 20721 RVA: 0x0012C5B6 File Offset: 0x0012A7B6
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute1];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute1] = value;
			}
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x0012C5C4 File Offset: 0x0012A7C4
		// (set) Token: 0x060050F3 RID: 20723 RVA: 0x0012C5D6 File Offset: 0x0012A7D6
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute2];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute2] = value;
			}
		}

		// Token: 0x17001A86 RID: 6790
		// (get) Token: 0x060050F4 RID: 20724 RVA: 0x0012C5E4 File Offset: 0x0012A7E4
		// (set) Token: 0x060050F5 RID: 20725 RVA: 0x0012C5F6 File Offset: 0x0012A7F6
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute3];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute3] = value;
			}
		}

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x060050F6 RID: 20726 RVA: 0x0012C604 File Offset: 0x0012A804
		// (set) Token: 0x060050F7 RID: 20727 RVA: 0x0012C616 File Offset: 0x0012A816
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute4];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute4] = value;
			}
		}

		// Token: 0x17001A88 RID: 6792
		// (get) Token: 0x060050F8 RID: 20728 RVA: 0x0012C624 File Offset: 0x0012A824
		// (set) Token: 0x060050F9 RID: 20729 RVA: 0x0012C636 File Offset: 0x0012A836
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute5];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute5] = value;
			}
		}

		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x0012C644 File Offset: 0x0012A844
		// (set) Token: 0x060050FB RID: 20731 RVA: 0x0012C656 File Offset: 0x0012A856
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute6];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute6] = value;
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x0012C664 File Offset: 0x0012A864
		// (set) Token: 0x060050FD RID: 20733 RVA: 0x0012C676 File Offset: 0x0012A876
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute7];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute7] = value;
			}
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x0012C684 File Offset: 0x0012A884
		// (set) Token: 0x060050FF RID: 20735 RVA: 0x0012C696 File Offset: 0x0012A896
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute8];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute8] = value;
			}
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x0012C6A4 File Offset: 0x0012A8A4
		// (set) Token: 0x06005101 RID: 20737 RVA: 0x0012C6B6 File Offset: 0x0012A8B6
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute9];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute9] = value;
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x0012C6C4 File Offset: 0x0012A8C4
		// (set) Token: 0x06005103 RID: 20739 RVA: 0x0012C6D6 File Offset: 0x0012A8D6
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute10];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute10] = value;
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x0012C6E4 File Offset: 0x0012A8E4
		// (set) Token: 0x06005105 RID: 20741 RVA: 0x0012C6F6 File Offset: 0x0012A8F6
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute11];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute11] = value;
			}
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06005106 RID: 20742 RVA: 0x0012C704 File Offset: 0x0012A904
		// (set) Token: 0x06005107 RID: 20743 RVA: 0x0012C716 File Offset: 0x0012A916
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute12];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute12] = value;
			}
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06005108 RID: 20744 RVA: 0x0012C724 File Offset: 0x0012A924
		// (set) Token: 0x06005109 RID: 20745 RVA: 0x0012C736 File Offset: 0x0012A936
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute13];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute13] = value;
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x0012C744 File Offset: 0x0012A944
		// (set) Token: 0x0600510B RID: 20747 RVA: 0x0012C756 File Offset: 0x0012A956
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute14];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute14] = value;
			}
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x0012C764 File Offset: 0x0012A964
		// (set) Token: 0x0600510D RID: 20749 RVA: 0x0012C776 File Offset: 0x0012A976
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressListBaseSchema.ConditionalCustomAttribute15];
			}
			set
			{
				this[AddressListBaseSchema.ConditionalCustomAttribute15] = value;
			}
		}

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x0012C784 File Offset: 0x0012A984
		// (set) Token: 0x0600510F RID: 20751 RVA: 0x0012C796 File Offset: 0x0012A996
		public ADObjectId RecipientContainer
		{
			get
			{
				return (ADObjectId)this[AddressListBaseSchema.RecipientContainer];
			}
			set
			{
				this[AddressListBaseSchema.RecipientContainer] = value;
			}
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06005110 RID: 20752 RVA: 0x0012C7A4 File Offset: 0x0012A9A4
		public WellKnownRecipientFilterType RecipientFilterType
		{
			get
			{
				return (WellKnownRecipientFilterType)this[AddressListBaseSchema.RecipientFilterType];
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x0012C7B6 File Offset: 0x0012A9B6
		ADPropertyDefinition ISupportRecipientFilter.RecipientFilterSchema
		{
			get
			{
				return AddressBookBaseSchema.RecipientFilter;
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x0012C7BD File Offset: 0x0012A9BD
		ADPropertyDefinition ISupportRecipientFilter.LdapRecipientFilterSchema
		{
			get
			{
				return AddressBookBaseSchema.LdapRecipientFilter;
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x0012C7C4 File Offset: 0x0012A9C4
		ADPropertyDefinition ISupportRecipientFilter.IncludedRecipientsSchema
		{
			get
			{
				return AddressBookBaseSchema.IncludedRecipients;
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x0012C7CB File Offset: 0x0012A9CB
		ADPropertyDefinition ISupportRecipientFilter.ConditionalDepartmentSchema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalDepartment;
			}
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x0012C7D2 File Offset: 0x0012A9D2
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCompanySchema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCompany;
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x06005116 RID: 20758 RVA: 0x0012C7D9 File Offset: 0x0012A9D9
		ADPropertyDefinition ISupportRecipientFilter.ConditionalStateOrProvinceSchema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalStateOrProvince;
			}
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x0012C7E0 File Offset: 0x0012A9E0
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute1Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute1;
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x0012C7E7 File Offset: 0x0012A9E7
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute2Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute2;
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x0012C7EE File Offset: 0x0012A9EE
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute3Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute3;
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x0600511A RID: 20762 RVA: 0x0012C7F5 File Offset: 0x0012A9F5
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute4Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute4;
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x0012C7FC File Offset: 0x0012A9FC
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute5Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute5;
			}
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x0600511C RID: 20764 RVA: 0x0012C803 File Offset: 0x0012AA03
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute6Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute6;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x0600511D RID: 20765 RVA: 0x0012C80A File Offset: 0x0012AA0A
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute7Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute7;
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x0600511E RID: 20766 RVA: 0x0012C811 File Offset: 0x0012AA11
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute8Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute8;
			}
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x0012C818 File Offset: 0x0012AA18
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute9Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute9;
			}
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06005120 RID: 20768 RVA: 0x0012C81F File Offset: 0x0012AA1F
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute10Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute10;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x0012C826 File Offset: 0x0012AA26
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute11Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute11;
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06005122 RID: 20770 RVA: 0x0012C82D File Offset: 0x0012AA2D
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute12Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute12;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x0012C834 File Offset: 0x0012AA34
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute13Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute13;
			}
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06005124 RID: 20772 RVA: 0x0012C83B File Offset: 0x0012AA3B
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute14Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute14;
			}
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x0012C842 File Offset: 0x0012AA42
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute15Schema
		{
			get
			{
				return AddressBookBaseSchema.ConditionalCustomAttribute15;
			}
		}
	}
}
