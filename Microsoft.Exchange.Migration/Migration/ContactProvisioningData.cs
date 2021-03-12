using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200003F RID: 63
	internal class ContactProvisioningData : RecipientProvisioningData
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000B0AF File Offset: 0x000092AF
		internal ContactProvisioningData()
		{
			base.Action = ProvisioningAction.CreateNew;
			base.ProvisioningType = ProvisioningType.Contact;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000B0C5 File Offset: 0x000092C5
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000B0D7 File Offset: 0x000092D7
		public string FirstName
		{
			get
			{
				return (string)base[ADOrgPersonSchema.FirstName];
			}
			set
			{
				base[ADOrgPersonSchema.FirstName] = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B0E5 File Offset: 0x000092E5
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000B0F7 File Offset: 0x000092F7
		public string Initials
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Initials];
			}
			set
			{
				base[ADOrgPersonSchema.Initials] = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B105 File Offset: 0x00009305
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000B117 File Offset: 0x00009317
		public string LastName
		{
			get
			{
				return (string)base[ADOrgPersonSchema.LastName];
			}
			set
			{
				base[ADOrgPersonSchema.LastName] = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B125 File Offset: 0x00009325
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000B137 File Offset: 0x00009337
		public string ExternalEmailAddress
		{
			get
			{
				return (string)base[ADRecipientSchema.ExternalEmailAddress];
			}
			set
			{
				base[ADRecipientSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B145 File Offset: 0x00009345
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000B157 File Offset: 0x00009357
		public string Company
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Company];
			}
			set
			{
				base[ADOrgPersonSchema.Company] = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B165 File Offset: 0x00009365
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B177 File Offset: 0x00009377
		public string Department
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Department];
			}
			set
			{
				base[ADOrgPersonSchema.Department] = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B185 File Offset: 0x00009385
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B197 File Offset: 0x00009397
		public string Fax
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Fax];
			}
			set
			{
				base[ADOrgPersonSchema.Fax] = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B1A5 File Offset: 0x000093A5
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B1B7 File Offset: 0x000093B7
		public string MobilePhone
		{
			get
			{
				return (string)base[ADOrgPersonSchema.MobilePhone];
			}
			set
			{
				base[ADOrgPersonSchema.MobilePhone] = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B1C5 File Offset: 0x000093C5
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B1D7 File Offset: 0x000093D7
		public string Office
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Office];
			}
			set
			{
				base[ADOrgPersonSchema.Office] = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B1E5 File Offset: 0x000093E5
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000B1F7 File Offset: 0x000093F7
		public string Phone
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Phone];
			}
			set
			{
				base[ADOrgPersonSchema.Phone] = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B205 File Offset: 0x00009405
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000B217 File Offset: 0x00009417
		public string Title
		{
			get
			{
				return (string)base[ADOrgPersonSchema.Title];
			}
			set
			{
				base[ADOrgPersonSchema.Title] = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B225 File Offset: 0x00009425
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000B237 File Offset: 0x00009437
		public string HomePhone
		{
			get
			{
				return (string)base[ADOrgPersonSchema.HomePhone];
			}
			set
			{
				base[ADOrgPersonSchema.HomePhone] = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B245 File Offset: 0x00009445
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000B257 File Offset: 0x00009457
		public string StreetAddress
		{
			get
			{
				return (string)base[ADOrgPersonSchema.StreetAddress];
			}
			set
			{
				base[ADOrgPersonSchema.StreetAddress] = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B265 File Offset: 0x00009465
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000B277 File Offset: 0x00009477
		public string City
		{
			get
			{
				return (string)base[ADOrgPersonSchema.City];
			}
			set
			{
				base[ADOrgPersonSchema.City] = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B285 File Offset: 0x00009485
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B297 File Offset: 0x00009497
		public string StateOrProvince
		{
			get
			{
				return (string)base[ADOrgPersonSchema.StateOrProvince];
			}
			set
			{
				base[ADOrgPersonSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B2A5 File Offset: 0x000094A5
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000B2B7 File Offset: 0x000094B7
		public string PostalCode
		{
			get
			{
				return (string)base[ADOrgPersonSchema.PostalCode];
			}
			set
			{
				base[ADOrgPersonSchema.PostalCode] = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B2C5 File Offset: 0x000094C5
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000B2D7 File Offset: 0x000094D7
		public string CountryOrRegion
		{
			get
			{
				return (string)base[ADOrgPersonSchema.CountryOrRegion];
			}
			set
			{
				base[ADOrgPersonSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000B2E5 File Offset: 0x000094E5
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000B2F7 File Offset: 0x000094F7
		public string Notes
		{
			get
			{
				return (string)base[ADRecipientSchema.Notes];
			}
			set
			{
				base[ADRecipientSchema.Notes] = value;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B308 File Offset: 0x00009508
		public static ContactProvisioningData Create(string name, string emailAddress)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			MigrationUtil.ThrowOnNullOrEmptyArgument(emailAddress, "emailAddress");
			return new ContactProvisioningData
			{
				Name = name,
				ExternalEmailAddress = emailAddress
			};
		}
	}
}
