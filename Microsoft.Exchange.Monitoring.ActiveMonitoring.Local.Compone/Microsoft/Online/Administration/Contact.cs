using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E8 RID: 1000
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Contact", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class Contact : IExtensibleDataObject
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0008D091 File Offset: 0x0008B291
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x0008D099 File Offset: 0x0008B299
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

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0008D0A2 File Offset: 0x0008B2A2
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x0008D0AA File Offset: 0x0008B2AA
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

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001862 RID: 6242 RVA: 0x0008D0B3 File Offset: 0x0008B2B3
		// (set) Token: 0x06001863 RID: 6243 RVA: 0x0008D0BB File Offset: 0x0008B2BB
		[DataMember]
		public string CompanyName
		{
			get
			{
				return this.CompanyNameField;
			}
			set
			{
				this.CompanyNameField = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0008D0C4 File Offset: 0x0008B2C4
		// (set) Token: 0x06001865 RID: 6245 RVA: 0x0008D0CC File Offset: 0x0008B2CC
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

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0008D0D5 File Offset: 0x0008B2D5
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0008D0DD File Offset: 0x0008B2DD
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

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0008D0E6 File Offset: 0x0008B2E6
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0008D0EE File Offset: 0x0008B2EE
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

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600186A RID: 6250 RVA: 0x0008D0F7 File Offset: 0x0008B2F7
		// (set) Token: 0x0600186B RID: 6251 RVA: 0x0008D0FF File Offset: 0x0008B2FF
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return this.EmailAddressField;
			}
			set
			{
				this.EmailAddressField = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600186C RID: 6252 RVA: 0x0008D108 File Offset: 0x0008B308
		// (set) Token: 0x0600186D RID: 6253 RVA: 0x0008D110 File Offset: 0x0008B310
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

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0008D119 File Offset: 0x0008B319
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x0008D121 File Offset: 0x0008B321
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

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x0008D12A File Offset: 0x0008B32A
		// (set) Token: 0x06001871 RID: 6257 RVA: 0x0008D132 File Offset: 0x0008B332
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

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x0008D13B File Offset: 0x0008B33B
		// (set) Token: 0x06001873 RID: 6259 RVA: 0x0008D143 File Offset: 0x0008B343
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

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x0008D14C File Offset: 0x0008B34C
		// (set) Token: 0x06001875 RID: 6261 RVA: 0x0008D154 File Offset: 0x0008B354
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

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0008D15D File Offset: 0x0008B35D
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x0008D165 File Offset: 0x0008B365
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

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0008D16E File Offset: 0x0008B36E
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x0008D176 File Offset: 0x0008B376
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

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0008D17F File Offset: 0x0008B37F
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x0008D187 File Offset: 0x0008B387
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

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0008D190 File Offset: 0x0008B390
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x0008D198 File Offset: 0x0008B398
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

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0008D1A1 File Offset: 0x0008B3A1
		// (set) Token: 0x0600187F RID: 6271 RVA: 0x0008D1A9 File Offset: 0x0008B3A9
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

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0008D1B2 File Offset: 0x0008B3B2
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0008D1BA File Offset: 0x0008B3BA
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

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0008D1C3 File Offset: 0x0008B3C3
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0008D1CB File Offset: 0x0008B3CB
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

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0008D1D4 File Offset: 0x0008B3D4
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x0008D1DC File Offset: 0x0008B3DC
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

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0008D1E5 File Offset: 0x0008B3E5
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x0008D1ED File Offset: 0x0008B3ED
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

		// Token: 0x0400112B RID: 4395
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400112C RID: 4396
		private string CityField;

		// Token: 0x0400112D RID: 4397
		private string CompanyNameField;

		// Token: 0x0400112E RID: 4398
		private string CountryField;

		// Token: 0x0400112F RID: 4399
		private string DepartmentField;

		// Token: 0x04001130 RID: 4400
		private string DisplayNameField;

		// Token: 0x04001131 RID: 4401
		private string EmailAddressField;

		// Token: 0x04001132 RID: 4402
		private ValidationError[] ErrorsField;

		// Token: 0x04001133 RID: 4403
		private string FaxField;

		// Token: 0x04001134 RID: 4404
		private string FirstNameField;

		// Token: 0x04001135 RID: 4405
		private DateTime? LastDirSyncTimeField;

		// Token: 0x04001136 RID: 4406
		private string LastNameField;

		// Token: 0x04001137 RID: 4407
		private string MobilePhoneField;

		// Token: 0x04001138 RID: 4408
		private Guid? ObjectIdField;

		// Token: 0x04001139 RID: 4409
		private string OfficeField;

		// Token: 0x0400113A RID: 4410
		private string PhoneNumberField;

		// Token: 0x0400113B RID: 4411
		private string PostalCodeField;

		// Token: 0x0400113C RID: 4412
		private string StateField;

		// Token: 0x0400113D RID: 4413
		private string StreetAddressField;

		// Token: 0x0400113E RID: 4414
		private string TitleField;

		// Token: 0x0400113F RID: 4415
		private ValidationStatus? ValidationStatusField;
	}
}
