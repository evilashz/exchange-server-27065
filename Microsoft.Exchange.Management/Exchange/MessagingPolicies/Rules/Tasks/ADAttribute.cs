using System;
using Microsoft.Exchange.Core.RuleTasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000BA3 RID: 2979
	public enum ADAttribute
	{
		// Token: 0x040039E2 RID: 14818
		[LocDescription(RulesTasksStrings.IDs.ADAttributeName)]
		DisplayName,
		// Token: 0x040039E3 RID: 14819
		[LocDescription(RulesTasksStrings.IDs.ADAttributeFirstName)]
		FirstName,
		// Token: 0x040039E4 RID: 14820
		[LocDescription(RulesTasksStrings.IDs.ADAttributeInitials)]
		Initials,
		// Token: 0x040039E5 RID: 14821
		[LocDescription(RulesTasksStrings.IDs.ADAttributeLastName)]
		LastName,
		// Token: 0x040039E6 RID: 14822
		[LocDescription(RulesTasksStrings.IDs.ADAttributeOffice)]
		Office,
		// Token: 0x040039E7 RID: 14823
		[LocDescription(RulesTasksStrings.IDs.ADAttributePhoneNumber)]
		PhoneNumber,
		// Token: 0x040039E8 RID: 14824
		[LocDescription(RulesTasksStrings.IDs.ADAttributeOtherPhoneNumber)]
		OtherPhoneNumber,
		// Token: 0x040039E9 RID: 14825
		[LocDescription(RulesTasksStrings.IDs.ADAttributeEmail)]
		Email,
		// Token: 0x040039EA RID: 14826
		[LocDescription(RulesTasksStrings.IDs.ADAttributeStreet)]
		Street,
		// Token: 0x040039EB RID: 14827
		[LocDescription(RulesTasksStrings.IDs.ADAttributePOBox)]
		POBox,
		// Token: 0x040039EC RID: 14828
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCity)]
		City,
		// Token: 0x040039ED RID: 14829
		[LocDescription(RulesTasksStrings.IDs.ADAttributeState)]
		State,
		// Token: 0x040039EE RID: 14830
		[LocDescription(RulesTasksStrings.IDs.ADAttributeZipCode)]
		ZipCode,
		// Token: 0x040039EF RID: 14831
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCountry)]
		Country,
		// Token: 0x040039F0 RID: 14832
		[LocDescription(RulesTasksStrings.IDs.ADAttributeUserLogonName)]
		UserLogonName,
		// Token: 0x040039F1 RID: 14833
		[LocDescription(RulesTasksStrings.IDs.ADAttributeHomePhoneNumber)]
		HomePhoneNumber,
		// Token: 0x040039F2 RID: 14834
		[LocDescription(RulesTasksStrings.IDs.ADAttributeOtherHomePhoneNumber)]
		OtherHomePhoneNumber,
		// Token: 0x040039F3 RID: 14835
		[LocDescription(RulesTasksStrings.IDs.ADAttributePagerNumber)]
		PagerNumber,
		// Token: 0x040039F4 RID: 14836
		[LocDescription(RulesTasksStrings.IDs.ADAttributeMobileNumber)]
		MobileNumber,
		// Token: 0x040039F5 RID: 14837
		[LocDescription(RulesTasksStrings.IDs.ADAttributeFaxNumber)]
		FaxNumber,
		// Token: 0x040039F6 RID: 14838
		[LocDescription(RulesTasksStrings.IDs.ADAttributeOtherFaxNumber)]
		OtherFaxNumber,
		// Token: 0x040039F7 RID: 14839
		[LocDescription(RulesTasksStrings.IDs.ADAttributeNotes)]
		Notes,
		// Token: 0x040039F8 RID: 14840
		[LocDescription(RulesTasksStrings.IDs.ADAttributeTitle)]
		Title,
		// Token: 0x040039F9 RID: 14841
		[LocDescription(RulesTasksStrings.IDs.ADAttributeDepartment)]
		Department,
		// Token: 0x040039FA RID: 14842
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCompany)]
		Company,
		// Token: 0x040039FB RID: 14843
		[LocDescription(RulesTasksStrings.IDs.ADAttributeManager)]
		Manager,
		// Token: 0x040039FC RID: 14844
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute1)]
		CustomAttribute1,
		// Token: 0x040039FD RID: 14845
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute2)]
		CustomAttribute2,
		// Token: 0x040039FE RID: 14846
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute3)]
		CustomAttribute3,
		// Token: 0x040039FF RID: 14847
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute4)]
		CustomAttribute4,
		// Token: 0x04003A00 RID: 14848
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute5)]
		CustomAttribute5,
		// Token: 0x04003A01 RID: 14849
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute6)]
		CustomAttribute6,
		// Token: 0x04003A02 RID: 14850
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute7)]
		CustomAttribute7,
		// Token: 0x04003A03 RID: 14851
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute8)]
		CustomAttribute8,
		// Token: 0x04003A04 RID: 14852
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute9)]
		CustomAttribute9,
		// Token: 0x04003A05 RID: 14853
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute10)]
		CustomAttribute10,
		// Token: 0x04003A06 RID: 14854
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute11)]
		CustomAttribute11,
		// Token: 0x04003A07 RID: 14855
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute12)]
		CustomAttribute12,
		// Token: 0x04003A08 RID: 14856
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute13)]
		CustomAttribute13,
		// Token: 0x04003A09 RID: 14857
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute14)]
		CustomAttribute14,
		// Token: 0x04003A0A RID: 14858
		[LocDescription(RulesTasksStrings.IDs.ADAttributeCustomAttribute15)]
		CustomAttribute15
	}
}
