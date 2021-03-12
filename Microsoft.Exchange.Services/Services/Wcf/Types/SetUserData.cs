using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A7E RID: 2686
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetUserData : OptionsPropertyChangeTracker
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06004C04 RID: 19460 RVA: 0x00105FC4 File Offset: 0x001041C4
		// (set) Token: 0x06004C05 RID: 19461 RVA: 0x00105FCC File Offset: 0x001041CC
		[DataMember]
		public string City
		{
			get
			{
				return this.city;
			}
			set
			{
				this.city = value;
				base.TrackPropertyChanged("City");
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06004C06 RID: 19462 RVA: 0x00105FE0 File Offset: 0x001041E0
		// (set) Token: 0x06004C07 RID: 19463 RVA: 0x00105FE8 File Offset: 0x001041E8
		[DataMember]
		public string CountryOrRegion
		{
			get
			{
				return this.countryOrRegion;
			}
			set
			{
				this.countryOrRegion = value;
				base.TrackPropertyChanged("CountryOrRegion");
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x00105FFC File Offset: 0x001041FC
		// (set) Token: 0x06004C09 RID: 19465 RVA: 0x00106004 File Offset: 0x00104204
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
				base.TrackPropertyChanged("DisplayName");
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06004C0A RID: 19466 RVA: 0x00106018 File Offset: 0x00104218
		// (set) Token: 0x06004C0B RID: 19467 RVA: 0x00106020 File Offset: 0x00104220
		[DataMember]
		public string Fax
		{
			get
			{
				return this.fax;
			}
			set
			{
				this.fax = value;
				base.TrackPropertyChanged("Fax");
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06004C0C RID: 19468 RVA: 0x00106034 File Offset: 0x00104234
		// (set) Token: 0x06004C0D RID: 19469 RVA: 0x0010603C File Offset: 0x0010423C
		[DataMember]
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
				base.TrackPropertyChanged("FirstName");
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x00106050 File Offset: 0x00104250
		// (set) Token: 0x06004C0F RID: 19471 RVA: 0x00106058 File Offset: 0x00104258
		[DataMember]
		public string HomePhone
		{
			get
			{
				return this.homePhone;
			}
			set
			{
				this.homePhone = value;
				base.TrackPropertyChanged("HomePhone");
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x0010606C File Offset: 0x0010426C
		// (set) Token: 0x06004C11 RID: 19473 RVA: 0x00106074 File Offset: 0x00104274
		[DataMember]
		public string Initials
		{
			get
			{
				return this.initials;
			}
			set
			{
				this.initials = value;
				base.TrackPropertyChanged("Initials");
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x00106088 File Offset: 0x00104288
		// (set) Token: 0x06004C13 RID: 19475 RVA: 0x00106090 File Offset: 0x00104290
		[DataMember]
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
				base.TrackPropertyChanged("LastName");
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x001060A4 File Offset: 0x001042A4
		// (set) Token: 0x06004C15 RID: 19477 RVA: 0x001060AC File Offset: 0x001042AC
		[DataMember]
		public string MobilePhone
		{
			get
			{
				return this.mobilePhone;
			}
			set
			{
				this.mobilePhone = value;
				base.TrackPropertyChanged("MobilePhone");
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x001060C0 File Offset: 0x001042C0
		// (set) Token: 0x06004C17 RID: 19479 RVA: 0x001060C8 File Offset: 0x001042C8
		[DataMember]
		public string Office
		{
			get
			{
				return this.office;
			}
			set
			{
				this.office = value;
				base.TrackPropertyChanged("Office");
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x001060DC File Offset: 0x001042DC
		// (set) Token: 0x06004C19 RID: 19481 RVA: 0x001060E4 File Offset: 0x001042E4
		[DataMember]
		public string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
				base.TrackPropertyChanged("Phone");
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x001060F8 File Offset: 0x001042F8
		// (set) Token: 0x06004C1B RID: 19483 RVA: 0x00106100 File Offset: 0x00104300
		[DataMember]
		public string PostalCode
		{
			get
			{
				return this.postalCode;
			}
			set
			{
				this.postalCode = value;
				base.TrackPropertyChanged("PostalCode");
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x00106114 File Offset: 0x00104314
		// (set) Token: 0x06004C1D RID: 19485 RVA: 0x0010611C File Offset: 0x0010431C
		[DataMember]
		public string StateOrProvince
		{
			get
			{
				return this.stateOrProvince;
			}
			set
			{
				this.stateOrProvince = value;
				base.TrackPropertyChanged("StateOrProvince");
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06004C1E RID: 19486 RVA: 0x00106130 File Offset: 0x00104330
		// (set) Token: 0x06004C1F RID: 19487 RVA: 0x00106138 File Offset: 0x00104338
		[DataMember]
		public string StreetAddress
		{
			get
			{
				return this.streetAddress;
			}
			set
			{
				this.streetAddress = value;
				base.TrackPropertyChanged("StreetAddress");
			}
		}

		// Token: 0x04002B1B RID: 11035
		private string city;

		// Token: 0x04002B1C RID: 11036
		private string countryOrRegion;

		// Token: 0x04002B1D RID: 11037
		private string displayName;

		// Token: 0x04002B1E RID: 11038
		private string fax;

		// Token: 0x04002B1F RID: 11039
		private string firstName;

		// Token: 0x04002B20 RID: 11040
		private string homePhone;

		// Token: 0x04002B21 RID: 11041
		private string initials;

		// Token: 0x04002B22 RID: 11042
		private string lastName;

		// Token: 0x04002B23 RID: 11043
		private string mobilePhone;

		// Token: 0x04002B24 RID: 11044
		private string office;

		// Token: 0x04002B25 RID: 11045
		private string phone;

		// Token: 0x04002B26 RID: 11046
		private string postalCode;

		// Token: 0x04002B27 RID: 11047
		private string stateOrProvince;

		// Token: 0x04002B28 RID: 11048
		private string streetAddress;
	}
}
