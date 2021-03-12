using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.DocumentLibrary;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002CC RID: 716
	public static class ListViewColumns
	{
		// Token: 0x06001BD1 RID: 7121 RVA: 0x0009E7BC File Offset: 0x0009C9BC
		public static Column GetColumn(ColumnId columnId)
		{
			return ListViewColumns.columns[(int)columnId];
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		public static bool IsSupportedColumnId(ColumnId columnId)
		{
			return columnId >= ColumnId.MailIcon && columnId < (ColumnId)ListViewColumns.columns.Length;
		}

		// Token: 0x04001413 RID: 5139
		private static readonly ColumnBehavior AliasBehavior = new ColumnBehavior(15, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001414 RID: 5140
		private static readonly ColumnBehavior CapacityBehavior = new ColumnBehavior(HorizontalAlign.Right, 9, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001415 RID: 5141
		private static readonly ColumnBehavior CheckBoxBehavior = new ColumnBehavior(HorizontalAlign.Center, 20, true, SortOrder.Descending, GroupType.None);

		// Token: 0x04001416 RID: 5142
		private static readonly ColumnBehavior CheckBoxContactBehavior = new ColumnBehavior(HorizontalAlign.Center, 2, false, SortOrder.Descending, GroupType.None);

		// Token: 0x04001417 RID: 5143
		private static readonly ColumnBehavior CheckBoxADBehavior = new ColumnBehavior(HorizontalAlign.Center, 2, false, SortOrder.Descending, GroupType.None);

		// Token: 0x04001418 RID: 5144
		private static readonly ColumnBehavior CompanyBehavior = new ColumnBehavior(16, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001419 RID: 5145
		private static readonly ColumnBehavior ContactIconBehavior = new ColumnBehavior(HorizontalAlign.Center, 2, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x0400141A RID: 5146
		private static readonly ColumnBehavior ConversationUnreadCountBehavior = new ColumnBehavior(16, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400141B RID: 5147
		private static readonly ColumnBehavior DepartmentBehavior = new ColumnBehavior(16, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400141C RID: 5148
		private static readonly ColumnBehavior EmailAddressBehavior = new ColumnBehavior(20, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400141D RID: 5149
		private static readonly ColumnBehavior DistributionListMemberEmailAddressBehavior = new ColumnBehavior(20, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400141E RID: 5150
		private static readonly ColumnBehavior FileAsBehavior = new ColumnBehavior(15, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400141F RID: 5151
		private static readonly ColumnBehavior FlagBehavior = new ColumnBehavior(20, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001420 RID: 5152
		private static readonly ColumnBehavior ContactFlagBehavior = new ColumnBehavior(2, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001421 RID: 5153
		private static readonly ColumnBehavior TaskFlagBehavior = new ColumnBehavior(20, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001422 RID: 5154
		private static readonly ColumnBehavior CategoryBehavior = new ColumnBehavior(HorizontalAlign.Right, 24, true);

		// Token: 0x04001423 RID: 5155
		private static readonly ColumnBehavior ContactCategoryBehavior = new ColumnBehavior(HorizontalAlign.Right, 2, true);

		// Token: 0x04001424 RID: 5156
		private static readonly ColumnBehavior FullNameBehavior = new ColumnBehavior(18, false, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001425 RID: 5157
		private static readonly ColumnBehavior HalfName = new ColumnBehavior(20, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001426 RID: 5158
		private static readonly ColumnBehavior HasAttachmentsBehavior = new ColumnBehavior(HorizontalAlign.Center, 16, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001427 RID: 5159
		private static readonly ColumnBehavior ImportanceBehavior = new ColumnBehavior(HorizontalAlign.Center, 15, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001428 RID: 5160
		private static readonly ColumnBehavior MailIconBehavior = new ColumnBehavior(HorizontalAlign.Center, 20, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001429 RID: 5161
		private static readonly ColumnBehavior MarkCompleteCheckboxBehavior = new ColumnBehavior(HorizontalAlign.Center, 20, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400142A RID: 5162
		private static readonly ColumnBehavior OfficeBehavior = new ColumnBehavior(12, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400142B RID: 5163
		private static readonly ColumnBehavior PhoneNumberBehavior = new ColumnBehavior(11, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400142C RID: 5164
		private static readonly ColumnBehavior SizeBehavior = new ColumnBehavior(HorizontalAlign.Right, 6, false, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x0400142D RID: 5165
		private static readonly ColumnBehavior SubjectBehavior = new ColumnBehavior(44, false, SortOrder.Ascending, GroupType.Collapsed);

		// Token: 0x0400142E RID: 5166
		private static readonly ColumnBehavior TaskIconBehavior = new ColumnBehavior(HorizontalAlign.Center, 20, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x0400142F RID: 5167
		private static readonly ColumnBehavior TimeBehavior = new ColumnBehavior(90, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001430 RID: 5168
		private static readonly ColumnBehavior DumpsterTimeBehavior = new ColumnBehavior(160, true, SortOrder.Descending, GroupType.Expanded);

		// Token: 0x04001431 RID: 5169
		private static readonly ColumnBehavior TitleBehavior = new ColumnBehavior(15, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001432 RID: 5170
		private static readonly ColumnBehavior SharepointDocumentIconBehavior = new ColumnBehavior(24, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001433 RID: 5171
		private static readonly ColumnBehavior SharepointDocumentDisplayNameBehavior = new ColumnBehavior(100, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001434 RID: 5172
		private static readonly ColumnBehavior SharepointDocumentLastModifiedBehavior = new ColumnBehavior(39, false, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001435 RID: 5173
		private static readonly ColumnBehavior SharepointDocumentModifiedByBehavior = new ColumnBehavior(39, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001436 RID: 5174
		private static readonly ColumnBehavior SharepointDocumentCheckedOutToBehavior = new ColumnBehavior(39, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001437 RID: 5175
		private static readonly ColumnBehavior SharepointDocumentFileSizeBehavior = new ColumnBehavior(39, false, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001438 RID: 5176
		private static readonly ColumnBehavior UncDocumentIconBehavior = new ColumnBehavior(24, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001439 RID: 5177
		private static readonly ColumnBehavior UncDocumentDisplayNameBehavior = new ColumnBehavior(250, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400143A RID: 5178
		private static readonly ColumnBehavior UncDocumentLastModifiedBehavior = new ColumnBehavior(39, false, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x0400143B RID: 5179
		private static readonly ColumnBehavior UncDocumentFileSizeBehavior = new ColumnBehavior(39, false, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x0400143C RID: 5180
		private static readonly ColumnBehavior SharepointDocumentLibraryIconBehavior = new ColumnBehavior(24, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400143D RID: 5181
		private static readonly ColumnBehavior SharepointDocumentLibraryDisplayNameBehavior = new ColumnBehavior(200, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400143E RID: 5182
		private static readonly ColumnBehavior SharepointDocumentLibraryDescriptionBehavior = new ColumnBehavior(250, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x0400143F RID: 5183
		private static readonly ColumnBehavior SharepointDocumentLibraryItemCountBehavior = new ColumnBehavior(40, false, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001440 RID: 5184
		private static readonly ColumnBehavior SharepointDocumentLibraryLastModifiedBehavior = new ColumnBehavior(150, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001441 RID: 5185
		private static readonly ColumnBehavior YomiCompanyBehavior = new ColumnBehavior(16, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001442 RID: 5186
		private static readonly ColumnBehavior YomiDepartmentBehavior = new ColumnBehavior(16, true, SortOrder.Ascending, GroupType.None);

		// Token: 0x04001443 RID: 5187
		private static readonly ColumnBehavior YomiFullNameBehavior = new ColumnBehavior(23, false, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001444 RID: 5188
		private static readonly ColumnBehavior YomiHalfName = new ColumnBehavior(20, true, SortOrder.Ascending, GroupType.Expanded);

		// Token: 0x04001445 RID: 5189
		private static readonly Column ADIcon = new Column(ColumnId.ADIcon, ListViewColumns.ContactIconBehavior, false, new ColumnHeader(ThemeFileId.AddressBookIcon), new PropertyDefinition[]
		{
			ADObjectSchema.ObjectCategory
		});

		// Token: 0x04001446 RID: 5190
		private static readonly Column AliasAD = new Column(ColumnId.AliasAD, ListViewColumns.AliasBehavior, false, new ColumnHeader(-638921847), new PropertyDefinition[]
		{
			ADRecipientSchema.Alias
		});

		// Token: 0x04001447 RID: 5191
		private static readonly Column BusinessFaxAD = new Column(ColumnId.BusinessFaxAD, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-714530665), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Fax
		});

		// Token: 0x04001448 RID: 5192
		private static readonly Column BusinessPhoneAD = new Column(ColumnId.BusinessPhoneAD, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-296653598), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Phone
		});

		// Token: 0x04001449 RID: 5193
		private static readonly Column CheckBoxAD = new Column(ColumnId.CheckBoxAD, ListViewColumns.CheckBoxADBehavior, false, new ColumnHeader(true), new SortBoundaries(-1795472081, -629880559, 464390861), new PropertyDefinition[]
		{
			ADObjectSchema.Guid
		});

		// Token: 0x0400144A RID: 5194
		private static readonly Column CompanyAD = new Column(ColumnId.CompanyAD, ListViewColumns.CompanyBehavior, false, new ColumnHeader(1750481828), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Company
		});

		// Token: 0x0400144B RID: 5195
		private static readonly Column DepartmentAD = new Column(ColumnId.DepartmentAD, ListViewColumns.DepartmentBehavior, false, new ColumnHeader(-977196825), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Department
		});

		// Token: 0x0400144C RID: 5196
		private static readonly Column DisplayNameAD = new Column(ColumnId.DisplayNameAD, ListViewColumns.FullNameBehavior, false, new ColumnHeader(-228177434), new SortBoundaries(1445002207, -155175775, 878694989), new PropertyDefinition[]
		{
			ADRecipientSchema.DisplayName
		});

		// Token: 0x0400144D RID: 5197
		private static readonly Column EmailAddressAD = new Column(ColumnId.EmailAddressAD, ListViewColumns.EmailAddressBehavior, false, new ColumnHeader(1162538767), new PropertyDefinition[]
		{
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.RecipientDisplayType
		});

		// Token: 0x0400144E RID: 5198
		private static readonly Column HomePhoneAD = new Column(ColumnId.HomePhoneAD, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(1664796201), new PropertyDefinition[]
		{
			ADOrgPersonSchema.HomePhone
		});

		// Token: 0x0400144F RID: 5199
		private static readonly Column MobilePhoneAD = new Column(ColumnId.MobilePhoneAD, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-1298303582), new PropertyDefinition[]
		{
			ADOrgPersonSchema.MobilePhone
		});

		// Token: 0x04001450 RID: 5200
		private static readonly Column OfficeAD = new Column(ColumnId.OfficeAD, ListViewColumns.OfficeBehavior, false, new ColumnHeader(1053060679), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Office
		});

		// Token: 0x04001451 RID: 5201
		private static readonly Column PhoneAD = new Column(ColumnId.PhoneAD, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-2111177296), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Phone
		});

		// Token: 0x04001452 RID: 5202
		private static readonly Column ResourceCapacityAD = new Column(ColumnId.ResourceCapacityAD, ListViewColumns.CapacityBehavior, false, new ColumnHeader(1799661901), new PropertyDefinition[]
		{
			ADRecipientSchema.ResourceCapacity
		});

		// Token: 0x04001453 RID: 5203
		private static readonly Column TitleAD = new Column(ColumnId.TitleAD, ListViewColumns.TitleBehavior, false, new ColumnHeader(-1029833905), new PropertyDefinition[]
		{
			ADOrgPersonSchema.Title
		});

		// Token: 0x04001454 RID: 5204
		private static readonly Column YomiDisplayNameAD = new Column(ColumnId.YomiDisplayNameAD, ListViewColumns.YomiFullNameBehavior, false, new ColumnHeader(-1991902276), new PropertyDefinition[]
		{
			ADRecipientSchema.PhoneticDisplayName
		});

		// Token: 0x04001455 RID: 5205
		private static readonly Column YomiDepartmentAD = new Column(ColumnId.YomiDepartmentAD, ListViewColumns.YomiDepartmentBehavior, false, new ColumnHeader(1590675473), new PropertyDefinition[]
		{
			ADRecipientSchema.PhoneticDepartment
		});

		// Token: 0x04001456 RID: 5206
		private static readonly Column YomiCompanyAD = new Column(ColumnId.YomiCompanyAD, ListViewColumns.YomiCompanyBehavior, false, new ColumnHeader(1292568250), new PropertyDefinition[]
		{
			ADRecipientSchema.PhoneticCompany
		});

		// Token: 0x04001457 RID: 5207
		private static readonly Column BusinessFax = new Column(ColumnId.BusinessFax, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-714530665), new SortBoundaries(1192139348, 1839024124, 696226150), new PropertyDefinition[]
		{
			ContactSchema.WorkFax
		});

		// Token: 0x04001458 RID: 5208
		private static readonly Column BusinessPhone = new Column(ColumnId.BusinessPhone, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-296653598), new SortBoundaries(-123259807, 1839024124, 696226150), new PropertyDefinition[]
		{
			ContactSchema.BusinessPhoneNumber
		});

		// Token: 0x04001459 RID: 5209
		private static readonly Column Categories = new Column(ColumnId.Categories, ListViewColumns.CategoryBehavior, false, new ColumnHeader(ThemeFileId.CategoriesHeader), new PropertyDefinition[]
		{
			ItemSchema.Categories,
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.IsToDoItem
		});

		// Token: 0x0400145A RID: 5210
		private static readonly Column ContactCategories = new Column(ColumnId.ContactCategories, ListViewColumns.ContactCategoryBehavior, false, new ColumnHeader(ThemeFileId.CategoriesHeader), new PropertyDefinition[]
		{
			ItemSchema.Categories,
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.IsToDoItem
		});

		// Token: 0x0400145B RID: 5211
		private static readonly Column CheckBox = new Column(ColumnId.CheckBox, ListViewColumns.CheckBoxBehavior, false, new ColumnHeader(true), new SortBoundaries(-1795472081, -629880559, 464390861), new PropertyDefinition[]
		{
			ItemSchema.Id
		});

		// Token: 0x0400145C RID: 5212
		private static readonly Column CheckBoxContact = new Column(ColumnId.CheckBoxContact, ListViewColumns.CheckBoxContactBehavior, false, new ColumnHeader(true), new SortBoundaries(-1795472081, -629880559, 464390861), new PropertyDefinition[]
		{
			ItemSchema.Id
		});

		// Token: 0x0400145D RID: 5213
		private static readonly Column CompanyName = new Column(ColumnId.CompanyName, ListViewColumns.CompanyBehavior, true, new ColumnHeader(1750481828), new SortBoundaries(-826838917, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.CompanyName
		});

		// Token: 0x0400145E RID: 5214
		private static readonly Column DistributionListMemberDisplayName = new Column(ColumnId.MemberDisplayName, ListViewColumns.FullNameBehavior, false, new ColumnHeader(-228177434), new SortBoundaries(1445002207, -155175775, 878694989), new PropertyDefinition[]
		{
			StoreObjectSchema.DisplayName
		});

		// Token: 0x0400145F RID: 5215
		private static readonly Column DistributionListMemberIcon = new Column(ColumnId.MemberIcon, ListViewColumns.MailIconBehavior, true, new ColumnHeader(ThemeFileId.Contact), new SortBoundaries(785343019, -927268579, -1832517975), new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		});

		// Token: 0x04001460 RID: 5216
		private static readonly Column ContactIcon = new Column(ColumnId.ContactIcon, ListViewColumns.ContactIconBehavior, true, new ColumnHeader(ThemeFileId.Contact), new SortBoundaries(785343019, -927268579, -1832517975), new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		});

		// Token: 0x04001461 RID: 5217
		private static readonly Column Department = new Column(ColumnId.Department, ListViewColumns.DepartmentBehavior, true, new ColumnHeader(-611050349), new SortBoundaries(-611050349, -155175775, 878694989), false, new PropertyDefinition[]
		{
			ContactSchema.Department
		});

		// Token: 0x04001462 RID: 5218
		private static readonly Column DueDate = new Column(ColumnId.DueDate, ListViewColumns.TimeBehavior, true, new ColumnHeader(1629617734), new SortBoundaries(-66960209, -629880559, 464390861), new PropertyDefinition[]
		{
			TaskSchema.DueDate
		});

		// Token: 0x04001463 RID: 5219
		private static readonly Column EmailAddresses = new Column(ColumnId.EmailAddresses, ListViewColumns.EmailAddressBehavior, false, new ColumnHeader(961315020), new SortBoundaries(173930168, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactSchema.ContactBusinessFax,
			ContactSchema.ContactHomeFax,
			ContactSchema.ContactOtherFax,
			ContactSchema.Email1,
			ContactSchema.Email2,
			ContactSchema.Email3
		});

		// Token: 0x04001464 RID: 5220
		private static readonly Column DistributionListMemberEmail = new Column(ColumnId.MemberEmail, ListViewColumns.DistributionListMemberEmailAddressBehavior, false, new ColumnHeader(-748689469), new SortBoundaries(173930168, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Email1,
			ParticipantSchema.EmailAddress,
			ParticipantSchema.RoutingType,
			ItemSchema.RecipientType
		});

		// Token: 0x04001465 RID: 5221
		private static readonly Column Email1 = new Column(ColumnId.Email1, ListViewColumns.EmailAddressBehavior, false, new ColumnHeader(-748689469), new SortBoundaries(173930168, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Email1
		});

		// Token: 0x04001466 RID: 5222
		private static readonly Column Email2 = new Column(ColumnId.Email2, ListViewColumns.EmailAddressBehavior, false, new ColumnHeader(-345404942), new SortBoundaries(173930169, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Email2
		});

		// Token: 0x04001467 RID: 5223
		private static readonly Column Email3 = new Column(ColumnId.Email3, ListViewColumns.EmailAddressBehavior, false, new ColumnHeader(-1911488883), new SortBoundaries(173930170, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Email3
		});

		// Token: 0x04001468 RID: 5224
		private static readonly Column FileAs = new Column(ColumnId.FileAs, ListViewColumns.FileAsBehavior, false, new ColumnHeader(1178724274), new SortBoundaries(13085779, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactBaseSchema.FileAs
		});

		// Token: 0x04001469 RID: 5225
		private static readonly Column GivenName = new Column(ColumnId.GivenName, ListViewColumns.HalfName, true, new ColumnHeader(2145983474), new SortBoundaries(-1876431821, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.GivenName
		});

		// Token: 0x0400146A RID: 5226
		private static readonly Column From = new Column(ColumnId.From, ListViewColumns.FullNameBehavior, true, new ColumnHeader(-1656488396), new SortBoundaries(1309845117, -155175775, 878694989), new PropertyDefinition[]
		{
			ItemSchema.SentRepresentingDisplayName
		});

		// Token: 0x0400146B RID: 5227
		private static readonly Column HasAttachments = new Column(ColumnId.HasAttachment, ListViewColumns.HasAttachmentsBehavior, true, new ColumnHeader(ThemeFileId.Attachment2), new SortBoundaries(1072079569, 1348123951, 1845030095), new PropertyDefinition[]
		{
			ItemSchema.HasAttachment
		});

		// Token: 0x0400146C RID: 5228
		private static readonly Column HomePhone = new Column(ColumnId.HomePhone, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(1664796201), new SortBoundaries(326004004, 1839024124, 696226150), new PropertyDefinition[]
		{
			ContactSchema.HomePhone
		});

		// Token: 0x0400146D RID: 5229
		private static readonly Column Importance = new Column(ColumnId.Importance, ListViewColumns.ImportanceBehavior, true, new ColumnHeader(ThemeFileId.ImportanceHigh), new SortBoundaries(1569168155, 544952141, 975394505), new PropertyDefinition[]
		{
			ItemSchema.Importance
		});

		// Token: 0x0400146E RID: 5230
		private static readonly Column Surname = new Column(ColumnId.Surname, ListViewColumns.HalfName, true, new ColumnHeader(1200027237), new SortBoundaries(1759499200, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Surname
		});

		// Token: 0x0400146F RID: 5231
		private static readonly Column MailIcon = new Column(ColumnId.MailIcon, ListViewColumns.MailIconBehavior, true, new ColumnHeader(ThemeFileId.EMail), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		});

		// Token: 0x04001470 RID: 5232
		private static readonly Column MarkCompleteCheckbox = new Column(ColumnId.MarkCompleteCheckbox, ListViewColumns.MarkCompleteCheckboxBehavior, false, new ColumnHeader(ThemeFileId.CheckboxHeader), new SortBoundaries(-153493007, 499380967, -1587174585), new PropertyDefinition[]
		{
			ItemSchema.IsComplete
		});

		// Token: 0x04001471 RID: 5233
		private static readonly Column MobilePhone = new Column(ColumnId.MobilePhone, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-1298303582), new SortBoundaries(-2076111245, 1839024124, 696226150), new PropertyDefinition[]
		{
			ContactSchema.MobilePhone
		});

		// Token: 0x04001472 RID: 5234
		private static readonly Column PhoneNumbers = new Column(ColumnId.PhoneNumbers, ListViewColumns.PhoneNumberBehavior, false, new ColumnHeader(-2111177296), new SortBoundaries(-123259807, 1839024124, 696226150), new PropertyDefinition[]
		{
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.PrimaryTelephoneNumber,
			ContactSchema.BusinessPhoneNumber2,
			ContactSchema.MobilePhone,
			ContactSchema.HomePhone,
			ContactSchema.HomePhone2,
			ContactSchema.HomeFax,
			ContactSchema.WorkFax,
			ContactSchema.OtherFax,
			ContactSchema.AssistantPhoneNumber,
			ContactSchema.CallbackPhone,
			ContactSchema.CarPhone,
			ContactSchema.Pager,
			ContactSchema.OtherTelephone,
			ContactSchema.RadioPhone,
			ContactSchema.TtyTddPhoneNumber
		});

		// Token: 0x04001473 RID: 5235
		private static readonly Column ReceivedTime = new Column(ColumnId.DeliveryTime, ListViewColumns.TimeBehavior, true, new ColumnHeader(-375576855), new SortBoundaries(-1795472081, -629880559, 464390861), new PropertyDefinition[]
		{
			ItemSchema.ReceivedTime
		});

		// Token: 0x04001474 RID: 5236
		private static readonly Column SharepointDocumentIcon = new Column(ColumnId.SharepointDocumentIcon, ListViewColumns.SharepointDocumentIconBehavior, false, new ColumnHeader(ThemeFileId.FlatDocument), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			DocumentSchema.FileType
		});

		// Token: 0x04001475 RID: 5237
		private static readonly Column SharepointDocumentDisplayName = new Column(ColumnId.SharepointDocumentDisplayName, ListViewColumns.SharepointDocumentDisplayNameBehavior, false, new ColumnHeader(-1966747349), new SortBoundaries(-839171991, -155175775, 878694989), new PropertyDefinition[]
		{
			SharepointDocumentLibraryItemSchema.Name
		});

		// Token: 0x04001476 RID: 5238
		private static readonly Column SharepointDocumentLastModified = new Column(ColumnId.SharepointDocumentLastModified, ListViewColumns.SharepointDocumentLastModifiedBehavior, true, new ColumnHeader(869905365), new SortBoundaries(869905365, -629880559, 464390861), new PropertyDefinition[]
		{
			SharepointDocumentLibraryItemSchema.LastModifiedTime
		});

		// Token: 0x04001477 RID: 5239
		private static readonly Column SharepointDocumentModifiedBy = new Column(ColumnId.SharepointDocumentModifiedBy, ListViewColumns.SharepointDocumentModifiedByBehavior, false, new ColumnHeader(1276881056), new SortBoundaries(1276881056, -155175775, 878694989), new PropertyDefinition[]
		{
			SharepointDocumentLibraryItemSchema.Editor
		});

		// Token: 0x04001478 RID: 5240
		private static readonly Column SharepointDocumentCheckedOutTo = new Column(ColumnId.SharepointDocumentCheckedOutTo, ListViewColumns.SharepointDocumentCheckedOutToBehavior, false, new ColumnHeader(-580782680), new SortBoundaries(-580782680, -155175775, 878694989), new PropertyDefinition[]
		{
			SharepointDocumentSchema.CheckedOutUserId
		});

		// Token: 0x04001479 RID: 5241
		private static readonly Column SharepointDocumentFileSize = new Column(ColumnId.SharepointDocumentFileSize, ListViewColumns.SharepointDocumentFileSizeBehavior, true, new ColumnHeader(-837446919), new SortBoundaries(1128018090, 499418978, -1417517224), new PropertyDefinition[]
		{
			SharepointDocumentSchema.FileSize
		});

		// Token: 0x0400147A RID: 5242
		private static readonly Column UncDocumentIcon = new Column(ColumnId.UncDocumentIcon, ListViewColumns.UncDocumentIconBehavior, false, new ColumnHeader(ThemeFileId.FlatDocument), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			DocumentSchema.FileType
		});

		// Token: 0x0400147B RID: 5243
		private static readonly Column UncDocumentLibraryIcon = new Column(ColumnId.UncDocumentLibraryIcon, ListViewColumns.UncDocumentIconBehavior, false, new ColumnHeader(ThemeFileId.FlatDocument), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			DocumentSchema.FileType
		});

		// Token: 0x0400147C RID: 5244
		private static readonly Column UncDocumentDisplayName = new Column(ColumnId.UncDocumentDisplayName, ListViewColumns.UncDocumentDisplayNameBehavior, false, new ColumnHeader(-1966747349), new SortBoundaries(-839171991, -155175775, 878694989), new PropertyDefinition[]
		{
			UncItemSchema.DisplayName
		});

		// Token: 0x0400147D RID: 5245
		private static readonly Column UncDocumentLastModified = new Column(ColumnId.UncDocumentLastModified, ListViewColumns.UncDocumentLastModifiedBehavior, true, new ColumnHeader(869905365), new SortBoundaries(869905365, -629880559, 464390861), new PropertyDefinition[]
		{
			UncItemSchema.LastModifiedDate
		});

		// Token: 0x0400147E RID: 5246
		private static readonly Column UncDocumentFileSize = new Column(ColumnId.UncDocumentFileSize, ListViewColumns.UncDocumentFileSizeBehavior, true, new ColumnHeader(-837446919), new SortBoundaries(1128018090, 499418978, -1417517224), new PropertyDefinition[]
		{
			UncDocumentSchema.FileSize
		});

		// Token: 0x0400147F RID: 5247
		private static readonly Column SharepointDocumentLibraryIcon = new Column(ColumnId.SharepointDocumentLibraryIcon, ListViewColumns.SharepointDocumentLibraryIconBehavior, false, new ColumnHeader(ThemeFileId.FlatDocument), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			DocumentSchema.FileType
		});

		// Token: 0x04001480 RID: 5248
		private static readonly Column SharepointDocumentLibraryDisplayName = new Column(ColumnId.SharepointDocumentLibraryDisplayName, ListViewColumns.SharepointDocumentLibraryDisplayNameBehavior, false, new ColumnHeader(-1966747349), new SortBoundaries(-839171991, -155175775, 878694989), new PropertyDefinition[]
		{
			SharepointListSchema.Title
		});

		// Token: 0x04001481 RID: 5249
		private static readonly Column SharepointDocumentLibraryDescription = new Column(ColumnId.SharepointDocumentLibraryDescription, ListViewColumns.SharepointDocumentLibraryDescriptionBehavior, false, new ColumnHeader(873740972), new SortBoundaries(873740972, -155175775, 878694989), new PropertyDefinition[]
		{
			SharepointListSchema.Description
		});

		// Token: 0x04001482 RID: 5250
		private static readonly Column SharepointDocumentLibraryItemCount = new Column(ColumnId.SharepointDocumentLibraryItemCount, ListViewColumns.SharepointDocumentLibraryItemCountBehavior, false, new ColumnHeader(780414746), new SortBoundaries(780414746, -1178532886, -1942189936), new PropertyDefinition[]
		{
			SharepointListSchema.ItemCount
		});

		// Token: 0x04001483 RID: 5251
		private static readonly Column SharepointDocumentLibraryLastModified = new Column(ColumnId.SharepointDocumentLibraryLastModified, ListViewColumns.SharepointDocumentLibraryLastModifiedBehavior, true, new ColumnHeader(869905365), new SortBoundaries(869905365, -629880559, 464390861), new PropertyDefinition[]
		{
			SharepointListSchema.LastModifiedTime
		});

		// Token: 0x04001484 RID: 5252
		private static readonly Column Size = new Column(ColumnId.Size, ListViewColumns.SizeBehavior, true, new ColumnHeader(-943768673), new SortBoundaries(1128018090, 499418978, -1417517224), new PropertyDefinition[]
		{
			ItemSchema.Size
		});

		// Token: 0x04001485 RID: 5253
		private static readonly Column SentTime = new Column(ColumnId.SentTime, ListViewColumns.TimeBehavior, true, new ColumnHeader(-2005811526), new SortBoundaries(-1795472081, -629880559, 464390861), new PropertyDefinition[]
		{
			ItemSchema.SentTime
		});

		// Token: 0x04001486 RID: 5254
		private static readonly Column Subject = new Column(ColumnId.Subject, ListViewColumns.SubjectBehavior, true, new ColumnHeader(601895112), new SortBoundaries(2014646167, -155175775, 878694989), new PropertyDefinition[]
		{
			ItemSchema.Subject
		});

		// Token: 0x04001487 RID: 5255
		private static readonly Column TaskIcon = new Column(ColumnId.TaskIcon, ListViewColumns.TaskIconBehavior, true, new ColumnHeader(ThemeFileId.Task), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass
		});

		// Token: 0x04001488 RID: 5256
		private static readonly Column Title = new Column(ColumnId.Title, ListViewColumns.TitleBehavior, true, new ColumnHeader(355579046), new SortBoundaries(1436331631, -155175775, 878694989), new PropertyDefinition[]
		{
			ContactSchema.Title
		});

		// Token: 0x04001489 RID: 5257
		private static readonly Column To = new Column(ColumnId.To, ListViewColumns.FullNameBehavior, true, new ColumnHeader(2145670281), new SortBoundaries(262509582, -155175775, 878694989), new PropertyDefinition[]
		{
			ItemSchema.DisplayTo
		});

		// Token: 0x0400148A RID: 5258
		private static readonly Column YomiCompanyName = new Column(ColumnId.YomiCompanyName, ListViewColumns.YomiCompanyBehavior, true, new ColumnHeader(1292568250), new SortBoundaries(-1574585415, -1732685949, -577489661), new PropertyDefinition[]
		{
			ContactSchema.YomiCompany
		});

		// Token: 0x0400148B RID: 5259
		private static readonly Column YomiFirstName = new Column(ColumnId.YomiFirstName, ListViewColumns.YomiHalfName, true, new ColumnHeader(1215703485), new SortBoundaries(1506085866, -1732685949, -577489661), new PropertyDefinition[]
		{
			ContactSchema.YomiFirstName
		});

		// Token: 0x0400148C RID: 5260
		private static readonly Column YomiFullName = new Column(ColumnId.YomiFullName, ListViewColumns.YomiFullNameBehavior, true, new ColumnHeader(-1644089428), new SortBoundaries(1703559301, -1732685949, -577489661), new PropertyDefinition[]
		{
			ContactSchema.YomiLastName,
			ContactSchema.YomiFirstName
		});

		// Token: 0x0400148D RID: 5261
		private static readonly Column YomiLastName = new Column(ColumnId.YomiLastName, ListViewColumns.YomiHalfName, true, new ColumnHeader(-1420908403), new SortBoundaries(-1514589286, -1732685949, -577489661), new PropertyDefinition[]
		{
			ContactSchema.YomiLastName
		});

		// Token: 0x0400148E RID: 5262
		private static readonly Column FlagDueDate = new Column(ColumnId.FlagDueDate, ListViewColumns.FlagBehavior, true, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(1587370059, 571886510, -568934371), new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.IsComplete
		});

		// Token: 0x0400148F RID: 5263
		private static readonly Column FlagStartDate = new Column(ColumnId.FlagStartDate, ListViewColumns.FlagBehavior, true, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(1580556595, 571886510, -568934371), new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.IsComplete
		});

		// Token: 0x04001490 RID: 5264
		private static readonly Column FlagDueDateContact = new Column(ColumnId.ContactFlagDueDate, ListViewColumns.ContactFlagBehavior, true, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(1587370059, 571886510, -568934371), new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.IsComplete
		});

		// Token: 0x04001491 RID: 5265
		private static readonly Column FlagStartDateContact = new Column(ColumnId.ContactFlagStartDate, ListViewColumns.ContactFlagBehavior, true, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(1580556595, 571886510, -568934371), new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.IsComplete
		});

		// Token: 0x04001492 RID: 5266
		private static readonly Column TaskFlag = new Column(ColumnId.TaskFlag, ListViewColumns.TaskFlagBehavior, false, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(1587370059, 571886510, -568934371), new PropertyDefinition[]
		{
			ItemSchema.IsComplete,
			ItemSchema.FlagStatus,
			ItemSchema.ItemColor,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate
		});

		// Token: 0x04001493 RID: 5267
		private static readonly Column DeletedOnTime = new Column(ColumnId.DeletedOnTime, ListViewColumns.DumpsterTimeBehavior, false, new ColumnHeader(-56656194), new SortBoundaries(1932142663, -629880559, 464390861), new PropertyDefinition[]
		{
			StoreObjectSchema.LastModifiedTime
		});

		// Token: 0x04001494 RID: 5268
		private static readonly Column DumpsterReceivedTime = new Column(ColumnId.DumpsterReceivedTime, ListViewColumns.DumpsterTimeBehavior, false, new ColumnHeader(-375576855), new PropertyDefinition[]
		{
			ItemSchema.ReceivedTime
		});

		// Token: 0x04001495 RID: 5269
		private static readonly Column ObjectDisplayName = new Column(ColumnId.ObjectDisplayName, ListViewColumns.SubjectBehavior, false, new ColumnHeader(601895112), new PropertyDefinition[]
		{
			ItemSchema.Subject,
			FolderSchema.DisplayName
		});

		// Token: 0x04001496 RID: 5270
		private static readonly Column ObjectIcon = new Column(ColumnId.ObjectIcon, ListViewColumns.MailIconBehavior, true, new ColumnHeader(ThemeFileId.EMail), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			FolderSchema.Id
		});

		// Token: 0x04001497 RID: 5271
		private static readonly Column ConversationLastDeliveryTime = new Column(ColumnId.ConversationLastDeliveryTime, ListViewColumns.TimeBehavior, true, new ColumnHeader(-1795472081), new SortBoundaries(-1795472081, -629880559, 464390861), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationLastDeliveryTime
		});

		// Token: 0x04001498 RID: 5272
		private static readonly Column ConversationIcon = new Column(ColumnId.ConversationIcon, ListViewColumns.MailIconBehavior, true, new ColumnHeader(ThemeFileId.EMail), new SortBoundaries(785343019, -1759181059, 1822314281), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationMessageClasses
		});

		// Token: 0x04001499 RID: 5273
		private static readonly Column ConversationSubject = new Column(ColumnId.ConversationSubject, ListViewColumns.SubjectBehavior, true, new ColumnHeader(601895112), new SortBoundaries(2014646167, -155175775, 878694989), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationTopic
		});

		// Token: 0x0400149A RID: 5274
		private static readonly Column ConversationUnreadCount = new Column(ColumnId.ConversationUnreadCount, new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationUnreadMessageCount,
			ConversationItemSchema.ConversationMessageCount
		});

		// Token: 0x0400149B RID: 5275
		private static readonly Column ConversationHasAttachment = new Column(ColumnId.ConversationHasAttachment, ListViewColumns.HasAttachmentsBehavior, true, new ColumnHeader(ThemeFileId.Attachment2), new SortBoundaries(1072079569, 1348123951, 1845030095), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationHasAttach
		});

		// Token: 0x0400149C RID: 5276
		private static readonly Column ConversationSenderList = new Column(ColumnId.ConversationSenderList, ListViewColumns.FullNameBehavior, true, new ColumnHeader(-1656488396), new SortBoundaries(1309845117, -155175775, 878694989), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationMVFrom
		});

		// Token: 0x0400149D RID: 5277
		private static readonly Column ConversationImportance = new Column(ColumnId.ConversationImportance, ListViewColumns.ImportanceBehavior, true, new ColumnHeader(ThemeFileId.ImportanceHigh), new SortBoundaries(1569168155, 544952141, 975394505), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationImportance
		});

		// Token: 0x0400149E RID: 5278
		private static readonly Column ConversationCategories = new Column(ColumnId.ConversationCategories, new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationCategories
		});

		// Token: 0x0400149F RID: 5279
		private static readonly Column ConversationSize = new Column(ColumnId.ConversationSize, ListViewColumns.SizeBehavior, true, new ColumnHeader(-943768673), new SortBoundaries(1128018090, 499418978, -1417517224), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationMessageSize
		});

		// Token: 0x040014A0 RID: 5280
		private static readonly Column ConversationFlagStatus = new Column(ColumnId.ConversationFlagStatus, ListViewColumns.FlagBehavior, true, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(-568934371, 571886510, -568934371), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationFlagStatus,
			ConversationItemSchema.ConversationFlagCompleteTime
		});

		// Token: 0x040014A1 RID: 5281
		private static readonly Column ConversationFlagDueDate = new Column(ColumnId.ConversationFlagStatus, ListViewColumns.FlagBehavior, true, new ColumnHeader(ThemeFileId.FlagEmpty), new SortBoundaries(1587370059, 571886510, -568934371), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationFlagStatus,
			ConversationItemSchema.ConversationFlagCompleteTime
		});

		// Token: 0x040014A2 RID: 5282
		private static readonly Column ConversationToList = new Column(ColumnId.ConversationToList, ListViewColumns.FullNameBehavior, true, new ColumnHeader(2145670281), new SortBoundaries(262509582, -155175775, 878694989), new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationMVTo
		});

		// Token: 0x040014A3 RID: 5283
		private static readonly Column IMAddress = new Column(ColumnId.IMAddress, new PropertyDefinition[]
		{
			ContactSchema.IMAddress
		});

		// Token: 0x040014A4 RID: 5284
		private static readonly Column[] columns = new Column[]
		{
			ListViewColumns.MailIcon,
			ListViewColumns.From,
			ListViewColumns.To,
			ListViewColumns.Subject,
			ListViewColumns.Department,
			ListViewColumns.HasAttachments,
			ListViewColumns.Importance,
			ListViewColumns.ReceivedTime,
			ListViewColumns.SentTime,
			ListViewColumns.Size,
			ListViewColumns.ContactIcon,
			ListViewColumns.FileAs,
			ListViewColumns.Title,
			ListViewColumns.CompanyName,
			ListViewColumns.PhoneNumbers,
			ListViewColumns.BusinessPhone,
			ListViewColumns.BusinessFax,
			ListViewColumns.MobilePhone,
			ListViewColumns.HomePhone,
			ListViewColumns.EmailAddresses,
			ListViewColumns.Email1,
			ListViewColumns.Email2,
			ListViewColumns.Email3,
			ListViewColumns.GivenName,
			ListViewColumns.Surname,
			ListViewColumns.Categories,
			ListViewColumns.ContactCategories,
			ListViewColumns.SharepointDocumentIcon,
			ListViewColumns.SharepointDocumentDisplayName,
			ListViewColumns.SharepointDocumentLastModified,
			ListViewColumns.SharepointDocumentModifiedBy,
			ListViewColumns.SharepointDocumentCheckedOutTo,
			ListViewColumns.SharepointDocumentFileSize,
			ListViewColumns.UncDocumentIcon,
			ListViewColumns.UncDocumentLibraryIcon,
			ListViewColumns.UncDocumentDisplayName,
			ListViewColumns.UncDocumentLastModified,
			ListViewColumns.UncDocumentFileSize,
			ListViewColumns.SharepointDocumentLibraryIcon,
			ListViewColumns.SharepointDocumentLibraryDisplayName,
			ListViewColumns.SharepointDocumentLibraryDescription,
			ListViewColumns.SharepointDocumentLibraryItemCount,
			ListViewColumns.SharepointDocumentLibraryLastModified,
			ListViewColumns.CheckBox,
			ListViewColumns.CheckBoxContact,
			ListViewColumns.ADIcon,
			ListViewColumns.AliasAD,
			ListViewColumns.BusinessFaxAD,
			ListViewColumns.BusinessPhoneAD,
			ListViewColumns.CheckBoxAD,
			ListViewColumns.CompanyAD,
			ListViewColumns.DepartmentAD,
			ListViewColumns.DisplayNameAD,
			ListViewColumns.EmailAddressAD,
			ListViewColumns.HomePhoneAD,
			ListViewColumns.MobilePhoneAD,
			ListViewColumns.OfficeAD,
			ListViewColumns.PhoneAD,
			ListViewColumns.TitleAD,
			ListViewColumns.YomiCompanyName,
			ListViewColumns.YomiCompanyAD,
			ListViewColumns.YomiFirstName,
			ListViewColumns.YomiFullName,
			ListViewColumns.YomiLastName,
			ListViewColumns.YomiDisplayNameAD,
			ListViewColumns.YomiDepartmentAD,
			ListViewColumns.ResourceCapacityAD,
			ListViewColumns.FlagDueDate,
			ListViewColumns.FlagStartDate,
			ListViewColumns.FlagDueDateContact,
			ListViewColumns.FlagStartDateContact,
			ListViewColumns.TaskFlag,
			ListViewColumns.TaskIcon,
			ListViewColumns.MarkCompleteCheckbox,
			ListViewColumns.DueDate,
			ListViewColumns.DistributionListMemberDisplayName,
			ListViewColumns.DistributionListMemberEmail,
			ListViewColumns.DistributionListMemberIcon,
			ListViewColumns.DeletedOnTime,
			ListViewColumns.DumpsterReceivedTime,
			ListViewColumns.ObjectDisplayName,
			ListViewColumns.ObjectIcon,
			ListViewColumns.ConversationLastDeliveryTime,
			ListViewColumns.ConversationIcon,
			ListViewColumns.ConversationSubject,
			ListViewColumns.ConversationUnreadCount,
			ListViewColumns.ConversationHasAttachment,
			ListViewColumns.ConversationSenderList,
			ListViewColumns.ConversationImportance,
			ListViewColumns.ConversationCategories,
			ListViewColumns.ConversationSize,
			ListViewColumns.ConversationFlagStatus,
			ListViewColumns.ConversationFlagDueDate,
			ListViewColumns.IMAddress,
			ListViewColumns.ConversationToList
		};
	}
}
