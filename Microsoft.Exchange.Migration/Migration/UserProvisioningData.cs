using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200008B RID: 139
	internal class UserProvisioningData : RecipientProvisioningData
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x00023516 File Offset: 0x00021716
		internal UserProvisioningData()
		{
			base.Action = ProvisioningAction.CreateNew;
			base.ProvisioningType = ProvisioningType.User;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0002352C File Offset: 0x0002172C
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0002353E File Offset: 0x0002173E
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

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002354C File Offset: 0x0002174C
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0002355E File Offset: 0x0002175E
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0002356C File Offset: 0x0002176C
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0002357E File Offset: 0x0002177E
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

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002358C File Offset: 0x0002178C
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0002359E File Offset: 0x0002179E
		public string MailboxPlan
		{
			get
			{
				return (string)base[ADRecipientSchema.MailboxPlan];
			}
			set
			{
				base[ADRecipientSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000235AC File Offset: 0x000217AC
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x000235D0 File Offset: 0x000217D0
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000235E3 File Offset: 0x000217E3
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x000235F5 File Offset: 0x000217F5
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00023603 File Offset: 0x00021803
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00023615 File Offset: 0x00021815
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00023624 File Offset: 0x00021824
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00023648 File Offset: 0x00021848
		public bool UseExistingLiveId
		{
			get
			{
				object obj = base["UseExistingLiveId"];
				return obj != null && (bool)obj;
			}
			set
			{
				base["UseExistingLiveId"] = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0002365C File Offset: 0x0002185C
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00023680 File Offset: 0x00021880
		public bool UseExistingMicrosoftOnlineServicesID
		{
			get
			{
				object obj = base["UseExistingMicrosoftOnlineServicesID"];
				return obj != null && (bool)obj;
			}
			set
			{
				base["UseExistingMicrosoftOnlineServicesID"] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00023693 File Offset: 0x00021893
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x000236A5 File Offset: 0x000218A5
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

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x000236B3 File Offset: 0x000218B3
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x000236C5 File Offset: 0x000218C5
		public string FederatedIdentity
		{
			get
			{
				return (string)base["FederatedIdentity"];
			}
			set
			{
				base["FederatedIdentity"] = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000236D4 File Offset: 0x000218D4
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x000236F8 File Offset: 0x000218F8
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0002370C File Offset: 0x0002190C
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00023730 File Offset: 0x00021930
		public bool ImportLiveId
		{
			get
			{
				object obj = base["ImportLiveID"];
				return obj != null && (bool)obj;
			}
			set
			{
				base["ImportLiveID"] = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00023743 File Offset: 0x00021943
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00023755 File Offset: 0x00021955
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00023763 File Offset: 0x00021963
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00023775 File Offset: 0x00021975
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00023783 File Offset: 0x00021983
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00023795 File Offset: 0x00021995
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000237A3 File Offset: 0x000219A3
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x000237B5 File Offset: 0x000219B5
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x000237C3 File Offset: 0x000219C3
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x000237D5 File Offset: 0x000219D5
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x000237E3 File Offset: 0x000219E3
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x000237F5 File Offset: 0x000219F5
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00023803 File Offset: 0x00021A03
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00023815 File Offset: 0x00021A15
		public string[] Languages
		{
			get
			{
				return (string[])base[ADOrgPersonSchema.Languages];
			}
			set
			{
				base[ADOrgPersonSchema.Languages] = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00023823 File Offset: 0x00021A23
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00023835 File Offset: 0x00021A35
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

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00023843 File Offset: 0x00021A43
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00023855 File Offset: 0x00021A55
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

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00023863 File Offset: 0x00021A63
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00023875 File Offset: 0x00021A75
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

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00023883 File Offset: 0x00021A83
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00023895 File Offset: 0x00021A95
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000238A3 File Offset: 0x00021AA3
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000238B5 File Offset: 0x00021AB5
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000238C3 File Offset: 0x00021AC3
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x000238D5 File Offset: 0x00021AD5
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

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x000238E3 File Offset: 0x00021AE3
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x000238F5 File Offset: 0x00021AF5
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00023903 File Offset: 0x00021B03
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00023915 File Offset: 0x00021B15
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00023923 File Offset: 0x00021B23
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00023935 File Offset: 0x00021B35
		public string WebPage
		{
			get
			{
				return (string)base[ADRecipientSchema.WebPage];
			}
			set
			{
				base[ADRecipientSchema.WebPage] = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00023943 File Offset: 0x00021B43
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00023955 File Offset: 0x00021B55
		public string ResourceType
		{
			get
			{
				return (string)base[ADRecipientSchema.ResourceType];
			}
			set
			{
				base[ADRecipientSchema.ResourceType] = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00023963 File Offset: 0x00021B63
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00023975 File Offset: 0x00021B75
		public byte[] UMSpokenName
		{
			get
			{
				return (byte[])base[ADRecipientSchema.UMSpokenName];
			}
			set
			{
				base[ADRecipientSchema.UMSpokenName] = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00023984 File Offset: 0x00021B84
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x000239A8 File Offset: 0x00021BA8
		public int ResourceCapacity
		{
			get
			{
				object obj = base[ADRecipientSchema.ResourceCapacity];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				base[ADRecipientSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000239BC File Offset: 0x00021BBC
		public static UserProvisioningData Create(string name, string firstName, string lastName, string id, string password, bool isBPOS)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			MigrationUtil.ThrowOnNullOrEmptyArgument(firstName, "firstName");
			MigrationUtil.ThrowOnNullOrEmptyArgument(lastName, "lastName");
			MigrationUtil.ThrowOnNullOrEmptyArgument(id, "id");
			MigrationUtil.ThrowOnNullOrEmptyArgument(password, "password");
			UserProvisioningData userProvisioningData = new UserProvisioningData();
			userProvisioningData.Name = name;
			userProvisioningData.FirstName = firstName;
			userProvisioningData.LastName = lastName;
			UserProvisioningData.SetBposAwareProvisioningParameters(isBPOS, id, password, false, userProvisioningData);
			return userProvisioningData;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00023A2C File Offset: 0x00021C2C
		public static UserProvisioningData Create(string name, string id, bool useExistingId, string password, bool isBPOS)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			MigrationUtil.ThrowOnNullOrEmptyArgument(id, "id");
			if (useExistingId && !string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("Password should not be provided if useExistingId is set to true");
			}
			if (!useExistingId && string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("Password should be provided if useExistingId is set to false");
			}
			UserProvisioningData userProvisioningData = new UserProvisioningData();
			userProvisioningData.Name = name;
			UserProvisioningData.SetBposAwareProvisioningParameters(isBPOS, id, password, useExistingId, userProvisioningData);
			return userProvisioningData;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00023A94 File Offset: 0x00021C94
		public static UserProvisioningData CreateResource(string name, string displayName, string resourceType, int resourceCapacity, string password)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			MigrationUtil.ThrowOnNullOrEmptyArgument(displayName, "displayName");
			MigrationUtil.ThrowOnNullOrEmptyArgument(resourceType, "resourceType");
			MigrationUtil.ThrowOnLessThanZeroArgument((long)resourceCapacity, "resourceCapacity");
			MigrationUtil.ThrowOnNullOrEmptyArgument(password, "password");
			UserProvisioningData userProvisioningData = new UserProvisioningData();
			userProvisioningData.Name = name;
			userProvisioningData.DisplayName = displayName;
			userProvisioningData.ResourceType = resourceType;
			userProvisioningData.ResourceCapacity = resourceCapacity;
			userProvisioningData.Password = password;
			userProvisioningData[ADRecipientSchema.IsResource] = true;
			return userProvisioningData;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00023B18 File Offset: 0x00021D18
		public static UserProvisioningData CreateWithFederatedIdentity(string name, string firstName, string lastName, string id, string federatedIdentity, bool isBPOS)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(name, "name");
			MigrationUtil.ThrowOnNullOrEmptyArgument(firstName, "firstName");
			MigrationUtil.ThrowOnNullOrEmptyArgument(lastName, "lastName");
			MigrationUtil.ThrowOnNullOrEmptyArgument(id, "id");
			MigrationUtil.ThrowOnNullOrEmptyArgument(federatedIdentity, "federatedIdentity");
			UserProvisioningData userProvisioningData = new UserProvisioningData();
			userProvisioningData.Name = name;
			userProvisioningData.FirstName = firstName;
			userProvisioningData.LastName = lastName;
			userProvisioningData.FederatedIdentity = federatedIdentity;
			UserProvisioningData.SetBposAwareProvisioningParameters(isBPOS, id, null, false, userProvisioningData);
			return userProvisioningData;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00023B8C File Offset: 0x00021D8C
		private static void SetBposAwareProvisioningParameters(bool isBpos, string id, string password, bool useExistingId, UserProvisioningData data)
		{
			if (isBpos)
			{
				data.IsBPOS = isBpos;
				data.MicrosoftOnlineServicesID = id;
				if (useExistingId)
				{
					data.UseExistingMicrosoftOnlineServicesID = useExistingId;
				}
			}
			else
			{
				data.WindowsLiveID = id;
				if (useExistingId)
				{
					data.UseExistingLiveId = useExistingId;
				}
			}
			if (!useExistingId && !string.IsNullOrEmpty(password))
			{
				data.Password = password;
			}
		}

		// Token: 0x04000347 RID: 839
		public const string UseExistingLiveIdParameterName = "UseExistingLiveId";

		// Token: 0x04000348 RID: 840
		public const string UseExistingMicrosoftOnlineServicesIDParameterName = "UseExistingMicrosoftOnlineServicesID";

		// Token: 0x04000349 RID: 841
		public const string FederatedIdentityParameterName = "FederatedIdentity";

		// Token: 0x0400034A RID: 842
		public const string EvictLiveIDParameterName = "EvictLiveID";

		// Token: 0x0400034B RID: 843
		public const string ImportLiveIDParameterName = "ImportLiveID";
	}
}
