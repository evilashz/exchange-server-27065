using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ABContact : ABObject
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002492 File Offset: 0x00000692
		public ABContact(ABSession ownerSession) : base(ownerSession, ABContact.allContactPropertiesCollection)
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024A0 File Offset: 0x000006A0
		public override ABObjectSchema Schema
		{
			get
			{
				return ABContact.schema;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024A7 File Offset: 0x000006A7
		public Uri WebPage
		{
			get
			{
				return (Uri)this[ABContactSchema.WebPage];
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024B9 File Offset: 0x000006B9
		public string BusinessPhoneNumber
		{
			get
			{
				return (string)this[ABContactSchema.BusinessPhoneNumber];
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024CB File Offset: 0x000006CB
		public string CompanyName
		{
			get
			{
				return (string)this[ABContactSchema.CompanyName];
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024DD File Offset: 0x000006DD
		public string DepartmentName
		{
			get
			{
				return (string)this[ABContactSchema.DepartmentName];
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024EF File Offset: 0x000006EF
		public string BusinessFaxNumber
		{
			get
			{
				return (string)this[ABContactSchema.BusinessFaxNumber];
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002501 File Offset: 0x00000701
		public string GivenName
		{
			get
			{
				return (string)this[ABContactSchema.GivenName];
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002513 File Offset: 0x00000713
		public string HomePhoneNumber
		{
			get
			{
				return (string)this[ABContactSchema.HomePhoneNumber];
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002525 File Offset: 0x00000725
		public string Initials
		{
			get
			{
				return (string)this[ABContactSchema.Initials];
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002537 File Offset: 0x00000737
		public ABObjectId Manager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000253A File Offset: 0x0000073A
		public string MobilePhoneNumber
		{
			get
			{
				return (string)this[ABContactSchema.MobilePhoneNumber];
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000254C File Offset: 0x0000074C
		public string OfficeLocation
		{
			get
			{
				return (string)this[ABContactSchema.OfficeLocation];
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000255E File Offset: 0x0000075E
		public string Surname
		{
			get
			{
				return (string)this[ABContactSchema.Surname];
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002570 File Offset: 0x00000770
		public string Title
		{
			get
			{
				return (string)this[ABContactSchema.Title];
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002582 File Offset: 0x00000782
		public string WorkAddressPostOfficeBox
		{
			get
			{
				return (string)this[ABContactSchema.WorkAddressPostOfficeBox];
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002594 File Offset: 0x00000794
		public string WorkAddressStreet
		{
			get
			{
				return (string)this[ABContactSchema.WorkAddressStreet];
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000025A6 File Offset: 0x000007A6
		public string WorkAddressCity
		{
			get
			{
				return (string)this[ABContactSchema.WorkAddressCity];
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000025B8 File Offset: 0x000007B8
		public string WorkAddressState
		{
			get
			{
				return (string)this[ABContactSchema.WorkAddressState];
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000025CA File Offset: 0x000007CA
		public string WorkAddressPostalCode
		{
			get
			{
				return (string)this[ABContactSchema.WorkAddressPostalCode];
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025DC File Offset: 0x000007DC
		public string WorkAddressCountry
		{
			get
			{
				return (string)this[ABContactSchema.WorkAddressCountry];
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025EE File Offset: 0x000007EE
		public byte[] Picture
		{
			get
			{
				return (byte[])this[ABContactSchema.Picture];
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002600 File Offset: 0x00000800
		protected virtual string GetHomePhoneNumber()
		{
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002603 File Offset: 0x00000803
		protected virtual string GetBusinessPhoneNumber()
		{
			return null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002606 File Offset: 0x00000806
		protected virtual string GetCompanyName()
		{
			return null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002609 File Offset: 0x00000809
		protected virtual string GetDepartmentName()
		{
			return null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000260C File Offset: 0x0000080C
		protected virtual string GetBusinessFaxNumber()
		{
			return null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000260F File Offset: 0x0000080F
		protected virtual string GetGivenName()
		{
			return null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002612 File Offset: 0x00000812
		protected virtual string GetInitials()
		{
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002615 File Offset: 0x00000815
		protected virtual string GetMobilePhoneNumber()
		{
			return null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002618 File Offset: 0x00000818
		protected virtual string GetOfficeLocation()
		{
			return null;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000261B File Offset: 0x0000081B
		protected virtual string GetSurname()
		{
			return null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000261E File Offset: 0x0000081E
		protected virtual string GetTitle()
		{
			return null;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002621 File Offset: 0x00000821
		protected virtual string GetWorkAddressStreet()
		{
			return null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002624 File Offset: 0x00000824
		protected virtual string GetWorkAddressCity()
		{
			return null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002627 File Offset: 0x00000827
		protected virtual string GetWorkAddressCountry()
		{
			return null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000262A File Offset: 0x0000082A
		protected virtual string GetWorkAddressState()
		{
			return null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000262D File Offset: 0x0000082D
		protected virtual string GetWorkAddressPostalCode()
		{
			return null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002630 File Offset: 0x00000830
		protected virtual string GetWorkAddressPostOfficeBox()
		{
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002633 File Offset: 0x00000833
		protected virtual Uri GetWebPage()
		{
			return null;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002636 File Offset: 0x00000836
		protected virtual byte[] GetPicture()
		{
			return null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000263C File Offset: 0x0000083C
		protected override bool InternalTryGetValue(ABPropertyDefinition property, out object value)
		{
			if (property == ABContactSchema.BusinessPhoneNumber)
			{
				value = this.GetBusinessPhoneNumber();
				return true;
			}
			if (property == ABContactSchema.CompanyName)
			{
				value = this.GetCompanyName();
				return true;
			}
			if (property == ABContactSchema.DepartmentName)
			{
				value = this.GetDepartmentName();
				return true;
			}
			if (property == ABContactSchema.BusinessFaxNumber)
			{
				value = this.GetBusinessFaxNumber();
				return true;
			}
			if (property == ABContactSchema.GivenName)
			{
				value = this.GetGivenName();
				return true;
			}
			if (property == ABContactSchema.HomePhoneNumber)
			{
				value = this.GetHomePhoneNumber();
				return true;
			}
			if (property == ABContactSchema.Initials)
			{
				value = this.GetInitials();
				return true;
			}
			if (property == ABContactSchema.MobilePhoneNumber)
			{
				value = this.GetMobilePhoneNumber();
				return true;
			}
			if (property == ABContactSchema.OfficeLocation)
			{
				value = this.GetOfficeLocation();
				return true;
			}
			if (property == ABContactSchema.Surname)
			{
				value = this.GetSurname();
				return true;
			}
			if (property == ABContactSchema.Title)
			{
				value = this.GetTitle();
				return true;
			}
			if (property == ABContactSchema.WorkAddressCity)
			{
				value = this.GetWorkAddressCity();
				return true;
			}
			if (property == ABContactSchema.WorkAddressCountry)
			{
				value = this.GetWorkAddressCountry();
				return true;
			}
			if (property == ABContactSchema.WorkAddressPostalCode)
			{
				value = this.GetWorkAddressPostalCode();
				return true;
			}
			if (property == ABContactSchema.WorkAddressPostOfficeBox)
			{
				value = this.GetWorkAddressPostOfficeBox();
				return true;
			}
			if (property == ABContactSchema.WorkAddressState)
			{
				value = this.GetWorkAddressState();
				return true;
			}
			if (property == ABContactSchema.WorkAddressStreet)
			{
				value = this.GetWorkAddressStreet();
				return true;
			}
			if (property == ABContactSchema.WebPage)
			{
				value = this.GetWebPage();
				return true;
			}
			if (property == ABContactSchema.Picture)
			{
				value = this.GetPicture();
				return true;
			}
			return base.InternalTryGetValue(property, out value);
		}

		// Token: 0x04000003 RID: 3
		private static ABContactSchema schema = new ABContactSchema();

		// Token: 0x04000004 RID: 4
		private static ABPropertyDefinitionCollection allContactPropertiesCollection = ABPropertyDefinitionCollection.FromPropertyDefinitionCollection(ABContact.schema.AllProperties);
	}
}
