using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000DE RID: 222
	[DataContract]
	[KnownType(typeof(OrgPerson))]
	public class OrgPerson : MailboxRecipientRow
	{
		// Token: 0x06001DB2 RID: 7602 RVA: 0x0005AAFE File Offset: 0x00058CFE
		protected OrgPerson(MailEnabledOrgPerson recipient) : base(recipient)
		{
			this.MailEnabledOrgPerson = recipient;
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0005AB0E File Offset: 0x00058D0E
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0005AB16 File Offset: 0x00058D16
		private protected MailEnabledOrgPerson MailEnabledOrgPerson { protected get; private set; }

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0005AB1F File Offset: 0x00058D1F
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0005AB27 File Offset: 0x00058D27
		public OrgPersonPresentationObject OrgPersonObject { get; set; }

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0005AB30 File Offset: 0x00058D30
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0005AB47 File Offset: 0x00058D47
		[DataMember]
		public string ProfileCaption
		{
			get
			{
				return Strings.ProfileCaption(this.MailEnabledOrgPerson.DisplayName);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0005AB4E File Offset: 0x00058D4E
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0005AB65 File Offset: 0x00058D65
		[DataMember]
		public string ChangePhotoCaption
		{
			get
			{
				return Strings.ChangePhotoCaption(this.MailEnabledOrgPerson.DisplayName);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x0005AB6C File Offset: 0x00058D6C
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x0005AB87 File Offset: 0x00058D87
		[DataMember]
		public string FirstName
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.FirstName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0005AB8E File Offset: 0x00058D8E
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0005ABA9 File Offset: 0x00058DA9
		[DataMember]
		public string Initials
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Initials;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0005ABB0 File Offset: 0x00058DB0
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x0005ABCB File Offset: 0x00058DCB
		[DataMember]
		public string LastName
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.LastName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x0005ABD2 File Offset: 0x00058DD2
		// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x0005ABDA File Offset: 0x00058DDA
		[DataMember]
		public string EmailAddress { get; protected set; }

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0005ABE3 File Offset: 0x00058DE3
		// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x0005ABFE File Offset: 0x00058DFE
		[DataMember]
		public string StreetAddress
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.StreetAddress;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x0005AC05 File Offset: 0x00058E05
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x0005AC20 File Offset: 0x00058E20
		[DataMember]
		public string City
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.City;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0005AC27 File Offset: 0x00058E27
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x0005AC42 File Offset: 0x00058E42
		[DataMember]
		public string StateOrProvince
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.StateOrProvince;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0005AC49 File Offset: 0x00058E49
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0005AC64 File Offset: 0x00058E64
		[DataMember]
		public string PostalCode
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.PostalCode;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x0005AC6B File Offset: 0x00058E6B
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0005ACA4 File Offset: 0x00058EA4
		[DataMember]
		public string CountryOrRegion
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				if (!(null != this.OrgPersonObject.CountryOrRegion))
				{
					return string.Empty;
				}
				return this.OrgPersonObject.CountryOrRegion.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x0005ACAB File Offset: 0x00058EAB
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0005ACE0 File Offset: 0x00058EE0
		[DataMember]
		public string CountryOrRegionDisplayName
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return null;
				}
				if (!(null != this.OrgPersonObject.CountryOrRegion))
				{
					return string.Empty;
				}
				return this.OrgPersonObject.CountryOrRegion.DisplayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x0005ACE7 File Offset: 0x00058EE7
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0005AD02 File Offset: 0x00058F02
		[DataMember]
		public string Office
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Office;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0005AD09 File Offset: 0x00058F09
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x0005AD24 File Offset: 0x00058F24
		[DataMember]
		public string Phone
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Phone;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x0005AD2B File Offset: 0x00058F2B
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x0005AD46 File Offset: 0x00058F46
		[DataMember]
		public string Fax
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Fax;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0005AD4D File Offset: 0x00058F4D
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0005AD68 File Offset: 0x00058F68
		[DataMember]
		public string HomePhone
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.HomePhone;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0005AD6F File Offset: 0x00058F6F
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0005AD8A File Offset: 0x00058F8A
		[DataMember]
		public string MobilePhone
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.MobilePhone;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0005AD91 File Offset: 0x00058F91
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0005ADAC File Offset: 0x00058FAC
		[DataMember]
		public string Notes
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Notes;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0005ADB3 File Offset: 0x00058FB3
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0005ADCE File Offset: 0x00058FCE
		[DataMember]
		public string Title
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Title;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0005ADD5 File Offset: 0x00058FD5
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0005ADF0 File Offset: 0x00058FF0
		[DataMember]
		public string Department
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Department;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0005ADF7 File Offset: 0x00058FF7
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0005AE12 File Offset: 0x00059012
		[DataMember]
		public string Company
		{
			get
			{
				if (this.OrgPersonObject == null)
				{
					return string.Empty;
				}
				return this.OrgPersonObject.Company;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0005AE1C File Offset: 0x0005901C
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0005AE65 File Offset: 0x00059065
		[DataMember]
		public RecipientObjectResolverRow Manager
		{
			get
			{
				if (this.OrgPersonObject != null && this.OrgPersonObject.Manager != null)
				{
					return RecipientObjectResolver.Instance.ResolveObjects(new ADObjectId[]
					{
						this.OrgPersonObject.Manager
					}).FirstOrDefault<RecipientObjectResolverRow>();
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0005AE6C File Offset: 0x0005906C
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0005AE9A File Offset: 0x0005909A
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> DirectReports
		{
			get
			{
				if (this.OrgPersonObject != null && this.OrgPersonObject.DirectReports != null)
				{
					return RecipientObjectResolver.Instance.ResolveObjects(this.OrgPersonObject.DirectReports);
				}
				return null;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0005AED4 File Offset: 0x000590D4
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0005AF7E File Offset: 0x0005917E
		[DataMember]
		public IEnumerable<string> EmailAddresses
		{
			get
			{
				if (this.MailEnabledOrgPerson == null)
				{
					return null;
				}
				return from address in this.MailEnabledOrgPerson.EmailAddresses
				where address is SmtpProxyAddress
				where !address.IsPrimaryAddress
				orderby ((SmtpProxyAddress)address).SmtpAddress
				select ((SmtpProxyAddress)address).SmtpAddress;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0005AF88 File Offset: 0x00059188
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0005AFBC File Offset: 0x000591BC
		[DataMember]
		public string PrimaryEmailAddress
		{
			get
			{
				if (this.MailEnabledOrgPerson == null)
				{
					return string.Empty;
				}
				return this.MailEnabledOrgPerson.PrimarySmtpAddress.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0005AFC3 File Offset: 0x000591C3
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0005AFE3 File Offset: 0x000591E3
		[DataMember]
		public string MailTip
		{
			get
			{
				if (this.MailEnabledOrgPerson.MailTip != null)
				{
					return this.MailEnabledOrgPerson.MailTip;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
