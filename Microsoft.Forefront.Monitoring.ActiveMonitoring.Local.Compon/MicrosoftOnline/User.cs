using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020001A4 RID: 420
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://www.ccs.com/TestServices/")]
	[DesignerCategory("code")]
	[Serializable]
	public class User
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00023097 File Offset: 0x00021297
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0002309F File Offset: 0x0002129F
		public string Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000230A8 File Offset: 0x000212A8
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x000230B0 File Offset: 0x000212B0
		public string JobTitle
		{
			get
			{
				return this.jobTitleField;
			}
			set
			{
				this.jobTitleField = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x000230B9 File Offset: 0x000212B9
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x000230C1 File Offset: 0x000212C1
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x000230CA File Offset: 0x000212CA
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x000230D2 File Offset: 0x000212D2
		public string EmailAddress
		{
			get
			{
				return this.emailAddressField;
			}
			set
			{
				this.emailAddressField = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x000230DB File Offset: 0x000212DB
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x000230E3 File Offset: 0x000212E3
		public string GivenName
		{
			get
			{
				return this.givenNameField;
			}
			set
			{
				this.givenNameField = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x000230EC File Offset: 0x000212EC
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x000230F4 File Offset: 0x000212F4
		public string MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000230FD File Offset: 0x000212FD
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00023105 File Offset: 0x00021305
		public string Surname
		{
			get
			{
				return this.surnameField;
			}
			set
			{
				this.surnameField = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0002310E File Offset: 0x0002130E
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x00023116 File Offset: 0x00021316
		public string PreferredLanguage
		{
			get
			{
				return this.preferredLanguageField;
			}
			set
			{
				this.preferredLanguageField = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0002311F File Offset: 0x0002131F
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00023127 File Offset: 0x00021327
		public string UsageLocation
		{
			get
			{
				return this.usageLocationField;
			}
			set
			{
				this.usageLocationField = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00023130 File Offset: 0x00021330
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00023138 File Offset: 0x00021338
		public string UserName
		{
			get
			{
				return this.userNameField;
			}
			set
			{
				this.userNameField = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00023141 File Offset: 0x00021341
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00023149 File Offset: 0x00021349
		public string NewUserPassword
		{
			get
			{
				return this.newUserPasswordField;
			}
			set
			{
				this.newUserPasswordField = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00023152 File Offset: 0x00021352
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0002315A File Offset: 0x0002135A
		public string StreetAddress
		{
			get
			{
				return this.streetAddressField;
			}
			set
			{
				this.streetAddressField = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00023163 File Offset: 0x00021363
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0002316B File Offset: 0x0002136B
		public string City
		{
			get
			{
				return this.cityField;
			}
			set
			{
				this.cityField = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00023174 File Offset: 0x00021374
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0002317C File Offset: 0x0002137C
		public string State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00023185 File Offset: 0x00021385
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0002318D File Offset: 0x0002138D
		public string PostalCode
		{
			get
			{
				return this.postalCodeField;
			}
			set
			{
				this.postalCodeField = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00023196 File Offset: 0x00021396
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0002319E File Offset: 0x0002139E
		public string CountryLetterCode
		{
			get
			{
				return this.countryLetterCodeField;
			}
			set
			{
				this.countryLetterCodeField = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000231A7 File Offset: 0x000213A7
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x000231AF File Offset: 0x000213AF
		public string Mobile
		{
			get
			{
				return this.mobileField;
			}
			set
			{
				this.mobileField = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000231B8 File Offset: 0x000213B8
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x000231C0 File Offset: 0x000213C0
		public string TelephoneNumber
		{
			get
			{
				return this.telephoneNumberField;
			}
			set
			{
				this.telephoneNumberField = value;
			}
		}

		// Token: 0x040006BC RID: 1724
		private string initialsField;

		// Token: 0x040006BD RID: 1725
		private string jobTitleField;

		// Token: 0x040006BE RID: 1726
		private string displayNameField;

		// Token: 0x040006BF RID: 1727
		private string emailAddressField;

		// Token: 0x040006C0 RID: 1728
		private string givenNameField;

		// Token: 0x040006C1 RID: 1729
		private string middleNameField;

		// Token: 0x040006C2 RID: 1730
		private string surnameField;

		// Token: 0x040006C3 RID: 1731
		private string preferredLanguageField;

		// Token: 0x040006C4 RID: 1732
		private string usageLocationField;

		// Token: 0x040006C5 RID: 1733
		private string userNameField;

		// Token: 0x040006C6 RID: 1734
		private string newUserPasswordField;

		// Token: 0x040006C7 RID: 1735
		private string streetAddressField;

		// Token: 0x040006C8 RID: 1736
		private string cityField;

		// Token: 0x040006C9 RID: 1737
		private string stateField;

		// Token: 0x040006CA RID: 1738
		private string postalCodeField;

		// Token: 0x040006CB RID: 1739
		private string countryLetterCodeField;

		// Token: 0x040006CC RID: 1740
		private string mobileField;

		// Token: 0x040006CD RID: 1741
		private string telephoneNumberField;
	}
}
