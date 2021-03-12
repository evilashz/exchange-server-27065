using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003CA RID: 970
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(UserExtended))]
	[DataContract(Name = "User", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class User : IExtensibleDataObject
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0008C77B File Offset: 0x0008A97B
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x0008C783 File Offset: 0x0008A983
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x0008C78C File Offset: 0x0008A98C
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x0008C794 File Offset: 0x0008A994
		[DataMember]
		public string[] AlternateEmailAddresses
		{
			get
			{
				return this.AlternateEmailAddressesField;
			}
			set
			{
				this.AlternateEmailAddressesField = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0008C79D File Offset: 0x0008A99D
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x0008C7A5 File Offset: 0x0008A9A5
		[DataMember]
		public bool? BlockCredential
		{
			get
			{
				return this.BlockCredentialField;
			}
			set
			{
				this.BlockCredentialField = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x0008C7AE File Offset: 0x0008A9AE
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x0008C7B6 File Offset: 0x0008A9B6
		[DataMember]
		public string City
		{
			get
			{
				return this.CityField;
			}
			set
			{
				this.CityField = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x0008C7BF File Offset: 0x0008A9BF
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x0008C7C7 File Offset: 0x0008A9C7
		[DataMember]
		public string Country
		{
			get
			{
				return this.CountryField;
			}
			set
			{
				this.CountryField = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x0008C7D0 File Offset: 0x0008A9D0
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0008C7D8 File Offset: 0x0008A9D8
		[DataMember]
		public string Department
		{
			get
			{
				return this.DepartmentField;
			}
			set
			{
				this.DepartmentField = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0008C7E1 File Offset: 0x0008A9E1
		// (set) Token: 0x06001758 RID: 5976 RVA: 0x0008C7E9 File Offset: 0x0008A9E9
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.DisplayNameField;
			}
			set
			{
				this.DisplayNameField = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0008C7F2 File Offset: 0x0008A9F2
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x0008C7FA File Offset: 0x0008A9FA
		[DataMember]
		public ValidationError[] Errors
		{
			get
			{
				return this.ErrorsField;
			}
			set
			{
				this.ErrorsField = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0008C803 File Offset: 0x0008AA03
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x0008C80B File Offset: 0x0008AA0B
		[DataMember]
		public string Fax
		{
			get
			{
				return this.FaxField;
			}
			set
			{
				this.FaxField = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0008C814 File Offset: 0x0008AA14
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x0008C81C File Offset: 0x0008AA1C
		[DataMember]
		public string FirstName
		{
			get
			{
				return this.FirstNameField;
			}
			set
			{
				this.FirstNameField = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0008C825 File Offset: 0x0008AA25
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x0008C82D File Offset: 0x0008AA2D
		[DataMember]
		public string ImmutableId
		{
			get
			{
				return this.ImmutableIdField;
			}
			set
			{
				this.ImmutableIdField = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0008C836 File Offset: 0x0008AA36
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x0008C83E File Offset: 0x0008AA3E
		[DataMember]
		public bool? IsBlackberryUser
		{
			get
			{
				return this.IsBlackberryUserField;
			}
			set
			{
				this.IsBlackberryUserField = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0008C847 File Offset: 0x0008AA47
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x0008C84F File Offset: 0x0008AA4F
		[DataMember]
		public bool? IsLicensed
		{
			get
			{
				return this.IsLicensedField;
			}
			set
			{
				this.IsLicensedField = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0008C858 File Offset: 0x0008AA58
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0008C860 File Offset: 0x0008AA60
		[DataMember]
		public DateTime? LastDirSyncTime
		{
			get
			{
				return this.LastDirSyncTimeField;
			}
			set
			{
				this.LastDirSyncTimeField = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0008C869 File Offset: 0x0008AA69
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x0008C871 File Offset: 0x0008AA71
		[DataMember]
		public string LastName
		{
			get
			{
				return this.LastNameField;
			}
			set
			{
				this.LastNameField = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0008C87A File Offset: 0x0008AA7A
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x0008C882 File Offset: 0x0008AA82
		[DataMember]
		public bool? LicenseReconciliationNeeded
		{
			get
			{
				return this.LicenseReconciliationNeededField;
			}
			set
			{
				this.LicenseReconciliationNeededField = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0008C88B File Offset: 0x0008AA8B
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x0008C893 File Offset: 0x0008AA93
		[DataMember]
		public UserLicense[] Licenses
		{
			get
			{
				return this.LicensesField;
			}
			set
			{
				this.LicensesField = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x0008C89C File Offset: 0x0008AA9C
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x0008C8A4 File Offset: 0x0008AAA4
		[DataMember]
		public string LiveId
		{
			get
			{
				return this.LiveIdField;
			}
			set
			{
				this.LiveIdField = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x0008C8AD File Offset: 0x0008AAAD
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x0008C8B5 File Offset: 0x0008AAB5
		[DataMember]
		public string MobilePhone
		{
			get
			{
				return this.MobilePhoneField;
			}
			set
			{
				this.MobilePhoneField = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x0008C8BE File Offset: 0x0008AABE
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x0008C8C6 File Offset: 0x0008AAC6
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0008C8CF File Offset: 0x0008AACF
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0008C8D7 File Offset: 0x0008AAD7
		[DataMember]
		public string Office
		{
			get
			{
				return this.OfficeField;
			}
			set
			{
				this.OfficeField = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0008C8E0 File Offset: 0x0008AAE0
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x0008C8E8 File Offset: 0x0008AAE8
		[DataMember]
		public ProvisioningStatus OverallProvisioningStatus
		{
			get
			{
				return this.OverallProvisioningStatusField;
			}
			set
			{
				this.OverallProvisioningStatusField = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0008C8F1 File Offset: 0x0008AAF1
		// (set) Token: 0x06001778 RID: 6008 RVA: 0x0008C8F9 File Offset: 0x0008AAF9
		[DataMember]
		public bool? PasswordNeverExpires
		{
			get
			{
				return this.PasswordNeverExpiresField;
			}
			set
			{
				this.PasswordNeverExpiresField = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0008C902 File Offset: 0x0008AB02
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x0008C90A File Offset: 0x0008AB0A
		[DataMember]
		public string PhoneNumber
		{
			get
			{
				return this.PhoneNumberField;
			}
			set
			{
				this.PhoneNumberField = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0008C913 File Offset: 0x0008AB13
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x0008C91B File Offset: 0x0008AB1B
		[DataMember]
		public string PostalCode
		{
			get
			{
				return this.PostalCodeField;
			}
			set
			{
				this.PostalCodeField = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0008C924 File Offset: 0x0008AB24
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x0008C92C File Offset: 0x0008AB2C
		[DataMember]
		public string PreferredLanguage
		{
			get
			{
				return this.PreferredLanguageField;
			}
			set
			{
				this.PreferredLanguageField = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0008C935 File Offset: 0x0008AB35
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x0008C93D File Offset: 0x0008AB3D
		[DataMember]
		public DateTime? SoftDeletionTimestamp
		{
			get
			{
				return this.SoftDeletionTimestampField;
			}
			set
			{
				this.SoftDeletionTimestampField = value;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0008C946 File Offset: 0x0008AB46
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x0008C94E File Offset: 0x0008AB4E
		[DataMember]
		public string State
		{
			get
			{
				return this.StateField;
			}
			set
			{
				this.StateField = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0008C957 File Offset: 0x0008AB57
		// (set) Token: 0x06001784 RID: 6020 RVA: 0x0008C95F File Offset: 0x0008AB5F
		[DataMember]
		public string StreetAddress
		{
			get
			{
				return this.StreetAddressField;
			}
			set
			{
				this.StreetAddressField = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0008C968 File Offset: 0x0008AB68
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x0008C970 File Offset: 0x0008AB70
		[DataMember]
		public bool? StrongPasswordRequired
		{
			get
			{
				return this.StrongPasswordRequiredField;
			}
			set
			{
				this.StrongPasswordRequiredField = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0008C979 File Offset: 0x0008AB79
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x0008C981 File Offset: 0x0008AB81
		[DataMember]
		public string Title
		{
			get
			{
				return this.TitleField;
			}
			set
			{
				this.TitleField = value;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x0008C98A File Offset: 0x0008AB8A
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x0008C992 File Offset: 0x0008AB92
		[DataMember]
		public string UsageLocation
		{
			get
			{
				return this.UsageLocationField;
			}
			set
			{
				this.UsageLocationField = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0008C99B File Offset: 0x0008AB9B
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x0008C9A3 File Offset: 0x0008ABA3
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0008C9AC File Offset: 0x0008ABAC
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x0008C9B4 File Offset: 0x0008ABB4
		[DataMember]
		public ValidationStatus? ValidationStatus
		{
			get
			{
				return this.ValidationStatusField;
			}
			set
			{
				this.ValidationStatusField = value;
			}
		}

		// Token: 0x0400108E RID: 4238
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400108F RID: 4239
		private string[] AlternateEmailAddressesField;

		// Token: 0x04001090 RID: 4240
		private bool? BlockCredentialField;

		// Token: 0x04001091 RID: 4241
		private string CityField;

		// Token: 0x04001092 RID: 4242
		private string CountryField;

		// Token: 0x04001093 RID: 4243
		private string DepartmentField;

		// Token: 0x04001094 RID: 4244
		private string DisplayNameField;

		// Token: 0x04001095 RID: 4245
		private ValidationError[] ErrorsField;

		// Token: 0x04001096 RID: 4246
		private string FaxField;

		// Token: 0x04001097 RID: 4247
		private string FirstNameField;

		// Token: 0x04001098 RID: 4248
		private string ImmutableIdField;

		// Token: 0x04001099 RID: 4249
		private bool? IsBlackberryUserField;

		// Token: 0x0400109A RID: 4250
		private bool? IsLicensedField;

		// Token: 0x0400109B RID: 4251
		private DateTime? LastDirSyncTimeField;

		// Token: 0x0400109C RID: 4252
		private string LastNameField;

		// Token: 0x0400109D RID: 4253
		private bool? LicenseReconciliationNeededField;

		// Token: 0x0400109E RID: 4254
		private UserLicense[] LicensesField;

		// Token: 0x0400109F RID: 4255
		private string LiveIdField;

		// Token: 0x040010A0 RID: 4256
		private string MobilePhoneField;

		// Token: 0x040010A1 RID: 4257
		private Guid? ObjectIdField;

		// Token: 0x040010A2 RID: 4258
		private string OfficeField;

		// Token: 0x040010A3 RID: 4259
		private ProvisioningStatus OverallProvisioningStatusField;

		// Token: 0x040010A4 RID: 4260
		private bool? PasswordNeverExpiresField;

		// Token: 0x040010A5 RID: 4261
		private string PhoneNumberField;

		// Token: 0x040010A6 RID: 4262
		private string PostalCodeField;

		// Token: 0x040010A7 RID: 4263
		private string PreferredLanguageField;

		// Token: 0x040010A8 RID: 4264
		private DateTime? SoftDeletionTimestampField;

		// Token: 0x040010A9 RID: 4265
		private string StateField;

		// Token: 0x040010AA RID: 4266
		private string StreetAddressField;

		// Token: 0x040010AB RID: 4267
		private bool? StrongPasswordRequiredField;

		// Token: 0x040010AC RID: 4268
		private string TitleField;

		// Token: 0x040010AD RID: 4269
		private string UsageLocationField;

		// Token: 0x040010AE RID: 4270
		private string UserPrincipalNameField;

		// Token: 0x040010AF RID: 4271
		private ValidationStatus? ValidationStatusField;
	}
}
