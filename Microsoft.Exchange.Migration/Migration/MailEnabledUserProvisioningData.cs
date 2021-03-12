using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000048 RID: 72
	internal class MailEnabledUserProvisioningData : RecipientProvisioningData
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000BB89 File Offset: 0x00009D89
		internal MailEnabledUserProvisioningData()
		{
			base.Action = ProvisioningAction.CreateNew;
			base.ProvisioningType = ProvisioningType.MailEnabledUser;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000BB9F File Offset: 0x00009D9F
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000BBB1 File Offset: 0x00009DB1
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

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000BBBF File Offset: 0x00009DBF
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000BBD1 File Offset: 0x00009DD1
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000BBDF File Offset: 0x00009DDF
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000BBF1 File Offset: 0x00009DF1
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000BBFF File Offset: 0x00009DFF
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000BC11 File Offset: 0x00009E11
		public string WindowsLiveID
		{
			get
			{
				return (string)base[ADRecipientSchema.WindowsLiveID];
			}
			set
			{
				base[ADRecipientSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BC1F File Offset: 0x00009E1F
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000BC31 File Offset: 0x00009E31
		public string MicrosoftOnlineServicesID
		{
			get
			{
				return (string)base["MicrosoftOnlineServicesID"];
			}
			set
			{
				base["MicrosoftOnlineServicesID"] = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000BC3F File Offset: 0x00009E3F
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000BC51 File Offset: 0x00009E51
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

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000BC5F File Offset: 0x00009E5F
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000BC71 File Offset: 0x00009E71
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

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000BC7F File Offset: 0x00009E7F
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000BC91 File Offset: 0x00009E91
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000BC9F File Offset: 0x00009E9F
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000BCB1 File Offset: 0x00009EB1
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

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000BCBF File Offset: 0x00009EBF
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000BCD1 File Offset: 0x00009ED1
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

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000BCDF File Offset: 0x00009EDF
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000BCF1 File Offset: 0x00009EF1
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000BCFF File Offset: 0x00009EFF
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000BD11 File Offset: 0x00009F11
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000BD1F File Offset: 0x00009F1F
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000BD31 File Offset: 0x00009F31
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000BD3F File Offset: 0x00009F3F
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000BD51 File Offset: 0x00009F51
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000BD5F File Offset: 0x00009F5F
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000BD71 File Offset: 0x00009F71
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

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000BD7F File Offset: 0x00009F7F
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000BD91 File Offset: 0x00009F91
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000BD9F File Offset: 0x00009F9F
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000BDB1 File Offset: 0x00009FB1
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000BDBF File Offset: 0x00009FBF
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000BDD1 File Offset: 0x00009FD1
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000BDDF File Offset: 0x00009FDF
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000BDF1 File Offset: 0x00009FF1
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000BDFF File Offset: 0x00009FFF
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000BE11 File Offset: 0x0000A011
		public string Password
		{
			get
			{
				return (string)base["Password"];
			}
			set
			{
				base["Password"] = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000BE20 File Offset: 0x0000A020
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000BE44 File Offset: 0x0000A044
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				object obj = base[ADUserSchema.ResetPasswordOnNextLogon];
				return obj != null && (bool)obj;
			}
			set
			{
				base[ADUserSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000BE58 File Offset: 0x0000A058
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000BE7C File Offset: 0x0000A07C
		public bool EvictLiveId
		{
			get
			{
				object obj = base["EvictLiveID"];
				return obj != null && (bool)obj;
			}
			set
			{
				base["EvictLiveID"] = value;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BE90 File Offset: 0x0000A090
		public static MailEnabledUserProvisioningData Create(string name, string id, string password, bool isBPOS)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			MigrationUtil.ThrowOnNullOrEmptyArgument(id, "id");
			MigrationUtil.ThrowOnNullOrEmptyArgument(password, "password");
			MailEnabledUserProvisioningData mailEnabledUserProvisioningData = new MailEnabledUserProvisioningData();
			mailEnabledUserProvisioningData.Name = name;
			if (isBPOS)
			{
				mailEnabledUserProvisioningData.MicrosoftOnlineServicesID = id;
			}
			else
			{
				mailEnabledUserProvisioningData.WindowsLiveID = id;
			}
			mailEnabledUserProvisioningData.Password = password;
			return mailEnabledUserProvisioningData;
		}
	}
}
