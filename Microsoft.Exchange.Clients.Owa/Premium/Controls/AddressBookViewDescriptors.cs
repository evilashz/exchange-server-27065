using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200030C RID: 780
	internal static class AddressBookViewDescriptors
	{
		// Token: 0x06001D87 RID: 7559 RVA: 0x000AB4E4 File Offset: 0x000A96E4
		private static Dictionary<AddressBookViewDescriptors.Key, ViewDescriptor> CreateDescriptorTable()
		{
			return new Dictionary<AddressBookViewDescriptors.Key, ViewDescriptor>
			{
				{
					AddressBookViewDescriptors.Key.ContactBrowse,
					AddressBookViewDescriptors.ContactBrowseSingleLine
				},
				{
					AddressBookViewDescriptors.Key.ContactBrowseSingleLineJapan,
					AddressBookViewDescriptors.ContactBrowseSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.ContactBrowseMultiLine,
					AddressBookViewDescriptors.ContactBrowseMultiLine
				},
				{
					AddressBookViewDescriptors.Key.ContactBrowseMultiLineJapan,
					AddressBookViewDescriptors.ContactBrowseMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.ContactPicker,
					AddressBookViewDescriptors.ContactPickerSingleLine
				},
				{
					AddressBookViewDescriptors.Key.ContactPickerSingleLineJapan,
					AddressBookViewDescriptors.ContactPickerSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.ContactPickerMultiLine,
					AddressBookViewDescriptors.ContactPickerMultiLine
				},
				{
					AddressBookViewDescriptors.Key.ContactPickerMultiLineJapan,
					AddressBookViewDescriptors.ContactPickerMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.ContactMobileNumberPickerSingleLine,
					AddressBookViewDescriptors.ContactMobileNumberPickerSingleLine
				},
				{
					AddressBookViewDescriptors.Key.ContactMobileNumberPickerSingleLineJapan,
					AddressBookViewDescriptors.ContactMobileNumberPickerSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.ContactMobileNumberPickerMultiLine,
					AddressBookViewDescriptors.ContactMobileNumberPickerMultiLine
				},
				{
					AddressBookViewDescriptors.Key.ContactMobileNumberPickerMultiLineJapan,
					AddressBookViewDescriptors.ContactMobileNumberPickerMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.ContactModule,
					AddressBookViewDescriptors.ContactModuleSingleLine
				},
				{
					AddressBookViewDescriptors.Key.Japan,
					AddressBookViewDescriptors.ContactModuleSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.MultiLine,
					AddressBookViewDescriptors.ContactModuleMultiLine
				},
				{
					AddressBookViewDescriptors.Key.ContactModuleMultiLineJapan,
					AddressBookViewDescriptors.ContactModuleMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.DirectoryBrowse,
					AddressBookViewDescriptors.DirectoryBrowseSingleLine
				},
				{
					AddressBookViewDescriptors.Key.DirectoryBrowseSingleLineJapan,
					AddressBookViewDescriptors.DirectoryBrowseSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.DirectoryBrowseMultiLine,
					AddressBookViewDescriptors.DirectoryBrowseMultiLine
				},
				{
					AddressBookViewDescriptors.Key.DirectoryBrowseMultiLineJapan,
					AddressBookViewDescriptors.DirectoryBrowseMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.DirectoryPicker,
					AddressBookViewDescriptors.DirectoryPickerSingleLine
				},
				{
					AddressBookViewDescriptors.Key.DirectoryPickerSingleLineJapan,
					AddressBookViewDescriptors.DirectoryPickerSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.DirectoryPickerMultiLine,
					AddressBookViewDescriptors.DirectoryPickerMultiLine
				},
				{
					AddressBookViewDescriptors.Key.DirectoryPickerMultiLineJapan,
					AddressBookViewDescriptors.DirectoryPickerMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.DirectoryMobileNumberPickerSingleLine,
					AddressBookViewDescriptors.DirectoryMobileNumberPickerSingleLine
				},
				{
					AddressBookViewDescriptors.Key.DirectoryMobileNumberPickerSingleLineJapan,
					AddressBookViewDescriptors.DirectoryMobileNumberPickerSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.DirectoryMobileNumberPickerMultiLine,
					AddressBookViewDescriptors.DirectoryMobileNumberPickerMultiLine
				},
				{
					AddressBookViewDescriptors.Key.DirectoryMobileNumberPickerMultiLineJapan,
					AddressBookViewDescriptors.DirectoryMobileNumberPickerMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.RoomBrowse,
					AddressBookViewDescriptors.RoomBrowseSingleLine
				},
				{
					AddressBookViewDescriptors.Key.RoomBrowseSingleLineJapan,
					AddressBookViewDescriptors.RoomBrowseSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.RoomBrowseMultiLine,
					AddressBookViewDescriptors.RoomBrowseMultiLine
				},
				{
					AddressBookViewDescriptors.Key.RoomBrowseMultiLineJapan,
					AddressBookViewDescriptors.RoomBrowseMultiLineJapan
				},
				{
					AddressBookViewDescriptors.Key.RoomPicker,
					AddressBookViewDescriptors.RoomPickerSingleLine
				},
				{
					AddressBookViewDescriptors.Key.RoomPickerSingleLineJapan,
					AddressBookViewDescriptors.RoomPickerSingleLineJapan
				},
				{
					AddressBookViewDescriptors.Key.RoomPickerMultiLine,
					AddressBookViewDescriptors.RoomPickerMultiLine
				},
				{
					AddressBookViewDescriptors.Key.RoomPickerMultiLineJapan,
					AddressBookViewDescriptors.RoomPickerMultiLineJapan
				}
			};
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000AB6C8 File Offset: 0x000A98C8
		internal static ViewDescriptor GetViewDescriptor(bool isMultiLine, bool isPhoneticNamesEnabled, bool isMobilePicker, ViewType viewType)
		{
			int num = 0;
			while (num < AddressBookViewDescriptors.viewTypes.Length && viewType != AddressBookViewDescriptors.viewTypes[num])
			{
				num++;
			}
			if (num == AddressBookViewDescriptors.viewTypes.Length)
			{
				throw new ArgumentException("Invalid ViewType: {0}");
			}
			AddressBookViewDescriptors.Key key = (AddressBookViewDescriptors.Key)num;
			if (isPhoneticNamesEnabled)
			{
				key |= AddressBookViewDescriptors.Key.Japan;
			}
			if (isMultiLine)
			{
				key |= AddressBookViewDescriptors.Key.MultiLine;
			}
			if (isMobilePicker)
			{
				key |= AddressBookViewDescriptors.Key.MobileNumber;
			}
			ViewDescriptor result;
			if (!AddressBookViewDescriptors.descriptorTable.TryGetValue(key, out result))
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"There is no ViewDescriptor that matches isMultiLine=",
					isMultiLine,
					", IsPhoneticNamesEnabled=",
					isPhoneticNamesEnabled,
					", viewType=",
					viewType
				}));
			}
			return result;
		}

		// Token: 0x040015CB RID: 5579
		private static readonly ViewDescriptor ContactModuleSingleLine = new ViewDescriptor(ColumnId.FileAs, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.BusinessPhone,
			ColumnId.BusinessFax,
			ColumnId.MobilePhone,
			ColumnId.HomePhone
		});

		// Token: 0x040015CC RID: 5580
		private static readonly ViewDescriptor ContactModuleMultiLine = new ViewDescriptor(ColumnId.FileAs, false, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.FileAs,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.BusinessPhone,
			ColumnId.MobilePhone,
			ColumnId.HomePhone
		});

		// Token: 0x040015CD RID: 5581
		private static readonly ViewDescriptor ContactBrowseSingleLine = new ViewDescriptor(ColumnId.FileAs, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.Title,
			ColumnId.BusinessPhone,
			ColumnId.BusinessFax,
			ColumnId.MobilePhone,
			ColumnId.HomePhone,
			ColumnId.CompanyName
		});

		// Token: 0x040015CE RID: 5582
		private static readonly ViewDescriptor ContactBrowseMultiLine = AddressBookViewDescriptors.ContactModuleMultiLine;

		// Token: 0x040015CF RID: 5583
		private static readonly ViewDescriptor ContactPickerSingleLine = new ViewDescriptor(ColumnId.FileAs, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.EmailAddresses,
			ColumnId.Title,
			ColumnId.CompanyName
		});

		// Token: 0x040015D0 RID: 5584
		private static readonly ViewDescriptor ContactPickerMultiLine = new ViewDescriptor(ColumnId.FileAs, false, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.FileAs,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.EmailAddresses
		});

		// Token: 0x040015D1 RID: 5585
		private static readonly ViewDescriptor ContactMobileNumberPickerSingleLine = new ViewDescriptor(ColumnId.FileAs, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.MobilePhone,
			ColumnId.Title,
			ColumnId.CompanyName
		});

		// Token: 0x040015D2 RID: 5586
		private static readonly ViewDescriptor ContactMobileNumberPickerMultiLine = new ViewDescriptor(ColumnId.FileAs, false, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.FileAs,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.MobilePhone
		});

		// Token: 0x040015D3 RID: 5587
		private static readonly ViewDescriptor ContactModuleSingleLineJapan = new ViewDescriptor(ColumnId.YomiLastName, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.YomiCompanyName,
			ColumnId.BusinessPhone,
			ColumnId.BusinessFax,
			ColumnId.MobilePhone,
			ColumnId.HomePhone
		});

		// Token: 0x040015D4 RID: 5588
		private static readonly ViewDescriptor ContactModuleMultiLineJapan = new ViewDescriptor(ColumnId.YomiLastName, false, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.FileAs,
			ColumnId.Surname,
			ColumnId.GivenName,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.YomiFullName,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.YomiCompanyName,
			ColumnId.BusinessPhone,
			ColumnId.MobilePhone,
			ColumnId.HomePhone
		});

		// Token: 0x040015D5 RID: 5589
		private static readonly ViewDescriptor ContactBrowseSingleLineJapan = new ViewDescriptor(ColumnId.YomiLastName, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.Title,
			ColumnId.BusinessPhone,
			ColumnId.BusinessFax,
			ColumnId.MobilePhone,
			ColumnId.HomePhone,
			ColumnId.CompanyName,
			ColumnId.YomiCompanyName
		});

		// Token: 0x040015D6 RID: 5590
		private static readonly ViewDescriptor ContactBrowseMultiLineJapan = AddressBookViewDescriptors.ContactModuleMultiLineJapan;

		// Token: 0x040015D7 RID: 5591
		private static readonly ViewDescriptor ContactPickerSingleLineJapan = new ViewDescriptor(ColumnId.YomiLastName, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.EmailAddresses,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.YomiCompanyName
		});

		// Token: 0x040015D8 RID: 5592
		private static readonly ViewDescriptor ContactPickerMultiLineJapan = new ViewDescriptor(ColumnId.YomiLastName, false, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.FileAs,
			ColumnId.Surname,
			ColumnId.GivenName,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.YomiFullName,
			ColumnId.YomiCompanyName,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.EmailAddresses
		});

		// Token: 0x040015D9 RID: 5593
		private static readonly ViewDescriptor ContactMobileNumberPickerSingleLineJapan = new ViewDescriptor(ColumnId.YomiLastName, true, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.ContactCategories,
			ColumnId.ContactFlagDueDate,
			ColumnId.FileAs,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.MobilePhone,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.YomiCompanyName
		});

		// Token: 0x040015DA RID: 5594
		private static readonly ViewDescriptor ContactMobileNumberPickerMultiLineJapan = new ViewDescriptor(ColumnId.YomiLastName, false, new ColumnId[]
		{
			ColumnId.ContactIcon,
			ColumnId.FileAs,
			ColumnId.Surname,
			ColumnId.GivenName,
			ColumnId.YomiLastName,
			ColumnId.YomiFirstName,
			ColumnId.YomiFullName,
			ColumnId.YomiCompanyName,
			ColumnId.Title,
			ColumnId.CompanyName,
			ColumnId.MobilePhone
		});

		// Token: 0x040015DB RID: 5595
		private static readonly ViewDescriptor DirectoryBrowseSingleLine = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.ADIcon,
			ColumnId.DisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.AliasAD,
			ColumnId.EmailAddressAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.BusinessFaxAD,
			ColumnId.MobilePhoneAD,
			ColumnId.HomePhoneAD,
			ColumnId.OfficeAD,
			ColumnId.CompanyAD
		});

		// Token: 0x040015DC RID: 5596
		private static readonly ViewDescriptor DirectoryBrowseMultiLine = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.MobilePhoneAD,
			ColumnId.HomePhoneAD
		});

		// Token: 0x040015DD RID: 5597
		private static readonly ViewDescriptor DirectoryPickerSingleLine = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.ADIcon,
			ColumnId.DisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.AliasAD,
			ColumnId.EmailAddressAD,
			ColumnId.CompanyAD
		});

		// Token: 0x040015DE RID: 5598
		private static readonly ViewDescriptor DirectoryPickerMultiLine = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.EmailAddressAD
		});

		// Token: 0x040015DF RID: 5599
		private static readonly ViewDescriptor DirectoryMobileNumberPickerSingleLine = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.ADIcon,
			ColumnId.DisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.AliasAD,
			ColumnId.MobilePhoneAD,
			ColumnId.CompanyAD
		});

		// Token: 0x040015E0 RID: 5600
		private static readonly ViewDescriptor DirectoryMobileNumberPickerMultiLine = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.MobilePhoneAD
		});

		// Token: 0x040015E1 RID: 5601
		private static readonly ViewDescriptor DirectoryBrowseSingleLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.ADIcon,
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.YomiDepartmentAD,
			ColumnId.AliasAD,
			ColumnId.EmailAddressAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.BusinessFaxAD,
			ColumnId.MobilePhoneAD,
			ColumnId.HomePhoneAD,
			ColumnId.OfficeAD,
			ColumnId.CompanyAD,
			ColumnId.YomiCompanyAD
		});

		// Token: 0x040015E2 RID: 5602
		private static readonly ViewDescriptor DirectoryBrowseMultiLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.MobilePhoneAD,
			ColumnId.HomePhoneAD
		});

		// Token: 0x040015E3 RID: 5603
		private static readonly ViewDescriptor DirectoryPickerSingleLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.ADIcon,
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.YomiDepartmentAD,
			ColumnId.AliasAD,
			ColumnId.EmailAddressAD,
			ColumnId.CompanyAD,
			ColumnId.YomiCompanyAD
		});

		// Token: 0x040015E4 RID: 5604
		private static readonly ViewDescriptor DirectoryPickerMultiLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.EmailAddressAD
		});

		// Token: 0x040015E5 RID: 5605
		private static readonly ViewDescriptor DirectoryMobileNumberPickerSingleLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.ADIcon,
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.YomiDepartmentAD,
			ColumnId.AliasAD,
			ColumnId.MobilePhoneAD,
			ColumnId.CompanyAD,
			ColumnId.YomiCompanyAD
		});

		// Token: 0x040015E6 RID: 5606
		private static readonly ViewDescriptor DirectoryMobileNumberPickerMultiLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.MobilePhoneAD
		});

		// Token: 0x040015E7 RID: 5607
		private static readonly ViewDescriptor RoomBrowseSingleLine = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.ResourceCapacityAD,
			ColumnId.OfficeAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.EmailAddressAD
		});

		// Token: 0x040015E8 RID: 5608
		private static readonly ViewDescriptor RoomBrowseMultiLine = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.OfficeAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.YomiDepartmentAD
		});

		// Token: 0x040015E9 RID: 5609
		private static readonly ViewDescriptor RoomPickerSingleLine = AddressBookViewDescriptors.RoomBrowseSingleLine;

		// Token: 0x040015EA RID: 5610
		private static readonly ViewDescriptor RoomPickerMultiLine = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.OfficeAD,
			ColumnId.EmailAddressAD,
			ColumnId.TitleAD,
			ColumnId.DepartmentAD,
			ColumnId.YomiDepartmentAD
		});

		// Token: 0x040015EB RID: 5611
		private static readonly ViewDescriptor RoomBrowseSingleLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, true, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.ResourceCapacityAD,
			ColumnId.OfficeAD,
			ColumnId.BusinessPhoneAD,
			ColumnId.EmailAddressAD
		});

		// Token: 0x040015EC RID: 5612
		private static readonly ViewDescriptor RoomBrowseMultiLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.OfficeAD,
			ColumnId.BusinessPhoneAD
		});

		// Token: 0x040015ED RID: 5613
		private static readonly ViewDescriptor RoomPickerSingleLineJapan = AddressBookViewDescriptors.RoomBrowseSingleLineJapan;

		// Token: 0x040015EE RID: 5614
		private static readonly ViewDescriptor RoomPickerMultiLineJapan = new ViewDescriptor(ColumnId.DisplayNameAD, false, new ColumnId[]
		{
			ColumnId.DisplayNameAD,
			ColumnId.YomiDisplayNameAD,
			ColumnId.OfficeAD,
			ColumnId.EmailAddressAD
		});

		// Token: 0x040015EF RID: 5615
		private static readonly Dictionary<AddressBookViewDescriptors.Key, ViewDescriptor> descriptorTable = AddressBookViewDescriptors.CreateDescriptorTable();

		// Token: 0x040015F0 RID: 5616
		private static readonly ViewType[] viewTypes = new ViewType[]
		{
			ViewType.ContactModule,
			ViewType.ContactBrowser,
			ViewType.ContactPicker,
			ViewType.DirectoryBrowser,
			ViewType.DirectoryPicker,
			ViewType.RoomBrowser,
			ViewType.RoomPicker
		};

		// Token: 0x0200030D RID: 781
		[Flags]
		private enum Key
		{
			// Token: 0x040015F2 RID: 5618
			ContactModule = 0,
			// Token: 0x040015F3 RID: 5619
			ContactBrowse = 1,
			// Token: 0x040015F4 RID: 5620
			ContactPicker = 2,
			// Token: 0x040015F5 RID: 5621
			DirectoryBrowse = 3,
			// Token: 0x040015F6 RID: 5622
			DirectoryPicker = 4,
			// Token: 0x040015F7 RID: 5623
			RoomBrowse = 5,
			// Token: 0x040015F8 RID: 5624
			RoomPicker = 6,
			// Token: 0x040015F9 RID: 5625
			Japan = 16,
			// Token: 0x040015FA RID: 5626
			MultiLine = 32,
			// Token: 0x040015FB RID: 5627
			MobileNumber = 64,
			// Token: 0x040015FC RID: 5628
			ContactModuleSingleLine = 0,
			// Token: 0x040015FD RID: 5629
			ContactModuleSingleLineJapan = 16,
			// Token: 0x040015FE RID: 5630
			ContactModuleMultiLine = 32,
			// Token: 0x040015FF RID: 5631
			ContactModuleMultiLineJapan = 48,
			// Token: 0x04001600 RID: 5632
			ContactBrowseSingleLine = 1,
			// Token: 0x04001601 RID: 5633
			ContactBrowseSingleLineJapan = 17,
			// Token: 0x04001602 RID: 5634
			ContactBrowseMultiLine = 33,
			// Token: 0x04001603 RID: 5635
			ContactBrowseMultiLineJapan = 49,
			// Token: 0x04001604 RID: 5636
			ContactPickerSingleLine = 2,
			// Token: 0x04001605 RID: 5637
			ContactPickerSingleLineJapan = 18,
			// Token: 0x04001606 RID: 5638
			ContactPickerMultiLine = 34,
			// Token: 0x04001607 RID: 5639
			ContactPickerMultiLineJapan = 50,
			// Token: 0x04001608 RID: 5640
			ContactMobileNumberPickerSingleLine = 66,
			// Token: 0x04001609 RID: 5641
			ContactMobileNumberPickerSingleLineJapan = 82,
			// Token: 0x0400160A RID: 5642
			ContactMobileNumberPickerMultiLine = 98,
			// Token: 0x0400160B RID: 5643
			ContactMobileNumberPickerMultiLineJapan = 114,
			// Token: 0x0400160C RID: 5644
			DirectoryBrowseSingleLine = 3,
			// Token: 0x0400160D RID: 5645
			DirectoryBrowseSingleLineJapan = 19,
			// Token: 0x0400160E RID: 5646
			DirectoryBrowseMultiLine = 35,
			// Token: 0x0400160F RID: 5647
			DirectoryBrowseMultiLineJapan = 51,
			// Token: 0x04001610 RID: 5648
			DirectoryPickerSingleLine = 4,
			// Token: 0x04001611 RID: 5649
			DirectoryPickerSingleLineJapan = 20,
			// Token: 0x04001612 RID: 5650
			DirectoryPickerMultiLine = 36,
			// Token: 0x04001613 RID: 5651
			DirectoryPickerMultiLineJapan = 52,
			// Token: 0x04001614 RID: 5652
			DirectoryMobileNumberPickerSingleLine = 68,
			// Token: 0x04001615 RID: 5653
			DirectoryMobileNumberPickerSingleLineJapan = 84,
			// Token: 0x04001616 RID: 5654
			DirectoryMobileNumberPickerMultiLine = 100,
			// Token: 0x04001617 RID: 5655
			DirectoryMobileNumberPickerMultiLineJapan = 116,
			// Token: 0x04001618 RID: 5656
			RoomBrowseSingleLine = 5,
			// Token: 0x04001619 RID: 5657
			RoomBrowseSingleLineJapan = 21,
			// Token: 0x0400161A RID: 5658
			RoomBrowseMultiLine = 37,
			// Token: 0x0400161B RID: 5659
			RoomBrowseMultiLineJapan = 53,
			// Token: 0x0400161C RID: 5660
			RoomPickerSingleLine = 6,
			// Token: 0x0400161D RID: 5661
			RoomPickerSingleLineJapan = 22,
			// Token: 0x0400161E RID: 5662
			RoomPickerMultiLine = 38,
			// Token: 0x0400161F RID: 5663
			RoomPickerMultiLineJapan = 54
		}
	}
}
