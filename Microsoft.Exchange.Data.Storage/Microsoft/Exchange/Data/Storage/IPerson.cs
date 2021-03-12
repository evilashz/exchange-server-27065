using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F6 RID: 1270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPerson
	{
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060036F9 RID: 14073
		PersonId PersonId { get; }

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060036FA RID: 14074
		PersonType PersonType { get; }

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060036FB RID: 14075
		ExDateTime CreationTime { get; }

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060036FC RID: 14076
		string DisplayName { get; }

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060036FD RID: 14077
		string FileAs { get; }

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060036FE RID: 14078
		string FileAsId { get; }

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060036FF RID: 14079
		string DisplayNamePrefix { get; }

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06003700 RID: 14080
		string GivenName { get; }

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06003701 RID: 14081
		string MiddleName { get; }

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06003702 RID: 14082
		string Surname { get; }

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06003703 RID: 14083
		string Generation { get; }

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06003704 RID: 14084
		string Nickname { get; }

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06003705 RID: 14085
		string Alias { get; }

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06003706 RID: 14086
		string YomiFirstName { get; }

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06003707 RID: 14087
		string YomiLastName { get; }

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06003708 RID: 14088
		string Title { get; }

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06003709 RID: 14089
		string Department { get; }

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x0600370A RID: 14090
		string CompanyName { get; }

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x0600370B RID: 14091
		string Location { get; }

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x0600370C RID: 14092
		Participant EmailAddress { get; }

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x0600370D RID: 14093
		IEnumerable<Participant> EmailAddresses { get; }

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x0600370E RID: 14094
		PhoneNumber PhoneNumber { get; }

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600370F RID: 14095
		string ImAddress { get; }

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06003710 RID: 14096
		string HomeCity { get; }

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06003711 RID: 14097
		IEnumerable<StoreObjectId> FolderIds { get; }

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06003712 RID: 14098
		IEnumerable<Attribution> Attributions { get; }

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06003713 RID: 14099
		IEnumerable<AttributedValue<string>> DisplayNames { get; }

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06003714 RID: 14100
		IEnumerable<AttributedValue<string>> FileAses { get; }

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06003715 RID: 14101
		IEnumerable<AttributedValue<string>> FileAsIds { get; }

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06003716 RID: 14102
		IEnumerable<AttributedValue<string>> DisplayNamePrefixes { get; }

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06003717 RID: 14103
		IEnumerable<AttributedValue<string>> GivenNames { get; }

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06003718 RID: 14104
		IEnumerable<AttributedValue<string>> MiddleNames { get; }

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x06003719 RID: 14105
		IEnumerable<AttributedValue<string>> Surnames { get; }

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x0600371A RID: 14106
		IEnumerable<AttributedValue<string>> Generations { get; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x0600371B RID: 14107
		IEnumerable<AttributedValue<string>> Nicknames { get; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x0600371C RID: 14108
		IEnumerable<AttributedValue<string>> Initials { get; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x0600371D RID: 14109
		IEnumerable<AttributedValue<string>> YomiFirstNames { get; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600371E RID: 14110
		IEnumerable<AttributedValue<string>> YomiLastNames { get; }

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600371F RID: 14111
		IEnumerable<AttributedValue<PhoneNumber>> BusinessPhoneNumbers { get; }

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06003720 RID: 14112
		IEnumerable<AttributedValue<PhoneNumber>> BusinessPhoneNumbers2 { get; }

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06003721 RID: 14113
		IEnumerable<AttributedValue<PhoneNumber>> HomePhones { get; }

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06003722 RID: 14114
		IEnumerable<AttributedValue<PhoneNumber>> HomePhones2 { get; }

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06003723 RID: 14115
		IEnumerable<AttributedValue<PhoneNumber>> MobilePhones { get; }

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06003724 RID: 14116
		IEnumerable<AttributedValue<PhoneNumber>> MobilePhones2 { get; }

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06003725 RID: 14117
		IEnumerable<AttributedValue<PhoneNumber>> AssistantPhoneNumbers { get; }

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06003726 RID: 14118
		IEnumerable<AttributedValue<PhoneNumber>> CallbackPhones { get; }

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06003727 RID: 14119
		IEnumerable<AttributedValue<PhoneNumber>> CarPhones { get; }

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06003728 RID: 14120
		IEnumerable<AttributedValue<PhoneNumber>> HomeFaxes { get; }

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06003729 RID: 14121
		IEnumerable<AttributedValue<PhoneNumber>> OrganizationMainPhones { get; }

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x0600372A RID: 14122
		IEnumerable<AttributedValue<PhoneNumber>> OtherFaxes { get; }

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x0600372B RID: 14123
		IEnumerable<AttributedValue<PhoneNumber>> OtherTelephones { get; }

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x0600372C RID: 14124
		IEnumerable<AttributedValue<PhoneNumber>> OtherPhones2 { get; }

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x0600372D RID: 14125
		IEnumerable<AttributedValue<PhoneNumber>> Pagers { get; }

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x0600372E RID: 14126
		IEnumerable<AttributedValue<PhoneNumber>> RadioPhones { get; }

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x0600372F RID: 14127
		IEnumerable<AttributedValue<PhoneNumber>> TelexNumbers { get; }

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06003730 RID: 14128
		IEnumerable<AttributedValue<PhoneNumber>> TTYTDDPhoneNumbers { get; }

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06003731 RID: 14129
		IEnumerable<AttributedValue<PhoneNumber>> WorkFaxes { get; }

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06003732 RID: 14130
		IEnumerable<AttributedValue<Participant>> Emails1 { get; }

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06003733 RID: 14131
		IEnumerable<AttributedValue<Participant>> Emails2 { get; }

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06003734 RID: 14132
		IEnumerable<AttributedValue<Participant>> Emails3 { get; }

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06003735 RID: 14133
		IEnumerable<AttributedValue<string>> BusinessHomePages { get; }

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06003736 RID: 14134
		IEnumerable<AttributedValue<string>> PersonalHomePages { get; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06003737 RID: 14135
		IEnumerable<AttributedValue<string>> OfficeLocations { get; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06003738 RID: 14136
		IEnumerable<AttributedValue<string>> ImAddresses { get; }

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06003739 RID: 14137
		IEnumerable<AttributedValue<PostalAddress>> BusinessAddresses { get; }

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x0600373A RID: 14138
		IEnumerable<AttributedValue<PostalAddress>> HomeAddresses { get; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x0600373B RID: 14139
		IEnumerable<AttributedValue<PostalAddress>> OtherAddresses { get; }

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x0600373C RID: 14140
		IEnumerable<AttributedValue<string>> Titles { get; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x0600373D RID: 14141
		IEnumerable<AttributedValue<string>> Departments { get; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600373E RID: 14142
		IEnumerable<AttributedValue<string>> CompanyNames { get; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600373F RID: 14143
		IEnumerable<AttributedValue<string>> Managers { get; }

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06003740 RID: 14144
		IEnumerable<AttributedValue<string>> AssistantNames { get; }

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06003741 RID: 14145
		IEnumerable<AttributedValue<string>> Professions { get; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06003742 RID: 14146
		IEnumerable<AttributedValue<string>> SpouseNames { get; }

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06003743 RID: 14147
		IEnumerable<AttributedValue<string[]>> Children { get; }

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06003744 RID: 14148
		IEnumerable<AttributedValue<string>> Hobbies { get; }

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06003745 RID: 14149
		IEnumerable<AttributedValue<ExDateTime>> WeddingAnniversaries { get; }

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06003746 RID: 14150
		IEnumerable<AttributedValue<ExDateTime>> Birthdays { get; }

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06003747 RID: 14151
		IEnumerable<AttributedValue<ExDateTime>> WeddingAnniversariesLocal { get; }

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06003748 RID: 14152
		IEnumerable<AttributedValue<ExDateTime>> BirthdaysLocal { get; }

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06003749 RID: 14153
		IEnumerable<AttributedValue<string>> Locations { get; }
	}
}
