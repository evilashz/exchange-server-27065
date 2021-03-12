using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E0 RID: 224
	[DataContract]
	public abstract class SetOrgPerson : OrgPersonBasicProperties
	{
		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0005B072 File Offset: 0x00059272
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0005B084 File Offset: 0x00059284
		[DataMember]
		public string StreetAddress
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.StreetAddress];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.StreetAddress] = value;
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0005B092 File Offset: 0x00059292
		// (set) Token: 0x06001DFB RID: 7675 RVA: 0x0005B0A4 File Offset: 0x000592A4
		[DataMember]
		public string City
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.City];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.City] = value;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0005B0B2 File Offset: 0x000592B2
		// (set) Token: 0x06001DFD RID: 7677 RVA: 0x0005B0C4 File Offset: 0x000592C4
		[DataMember]
		public string StateOrProvince
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.StateOrProvince];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0005B0D2 File Offset: 0x000592D2
		// (set) Token: 0x06001DFF RID: 7679 RVA: 0x0005B0E4 File Offset: 0x000592E4
		[DataMember]
		public string PostalCode
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.PostalCode];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.PostalCode] = value;
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0005B0F2 File Offset: 0x000592F2
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x0005B104 File Offset: 0x00059304
		[DataMember]
		public string CountryOrRegion
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.CountryOrRegion];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.CountryOrRegion] = (string.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0005B11D File Offset: 0x0005931D
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x0005B12F File Offset: 0x0005932F
		[DataMember]
		public string Office
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Office];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Office] = value;
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0005B13D File Offset: 0x0005933D
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0005B14F File Offset: 0x0005934F
		[DataMember]
		public string Phone
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Phone];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Phone] = value;
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0005B15D File Offset: 0x0005935D
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x0005B16F File Offset: 0x0005936F
		[DataMember]
		public string Fax
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Fax];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Fax] = value;
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0005B17D File Offset: 0x0005937D
		// (set) Token: 0x06001E09 RID: 7689 RVA: 0x0005B18F File Offset: 0x0005938F
		[DataMember]
		public string HomePhone
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.HomePhone];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.HomePhone] = value;
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0005B19D File Offset: 0x0005939D
		// (set) Token: 0x06001E0B RID: 7691 RVA: 0x0005B1AF File Offset: 0x000593AF
		[DataMember]
		public string MobilePhone
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.MobilePhone];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.MobilePhone] = value;
			}
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0005B1BD File Offset: 0x000593BD
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x0005B1CF File Offset: 0x000593CF
		[DataMember]
		public string Notes
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Notes];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Notes] = value;
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0005B1DD File Offset: 0x000593DD
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0005B1EF File Offset: 0x000593EF
		[DataMember]
		public string Title
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Title];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Title] = value;
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0005B1FD File Offset: 0x000593FD
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x0005B20F File Offset: 0x0005940F
		[DataMember]
		public string Department
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Department];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Department] = value;
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0005B21D File Offset: 0x0005941D
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x0005B22F File Offset: 0x0005942F
		[DataMember]
		public string Company
		{
			get
			{
				return (string)base[OrgPersonPresentationObjectSchema.Company];
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Company] = value;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x0005B23D File Offset: 0x0005943D
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x0005B24F File Offset: 0x0005944F
		[DataMember]
		public Identity Manager
		{
			get
			{
				return Identity.FromIdParameter(base[OrgPersonPresentationObjectSchema.Manager]);
			}
			set
			{
				base[OrgPersonPresentationObjectSchema.Manager] = value.ToIdParameter();
			}
		}
	}
}
