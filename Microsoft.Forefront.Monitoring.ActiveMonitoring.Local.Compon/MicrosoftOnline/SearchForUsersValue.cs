using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E5 RID: 229
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SearchForUsersValue : DirectorySearch
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x0001F87C File Offset: 0x0001DA7C
		public SearchForUsersValue()
		{
			this.administratorsOnlyField = false;
			this.assignedLicenseUnsetOnlyField = false;
			this.besServiceInstanceSetOnlyField = false;
			this.licenseReconciliationNeededOnlyField = false;
			this.mailboxGuidSetOnlyField = false;
			this.softDeletedField = false;
			this.synchronizedOnlyField = false;
			this.validationErrorUnresolvedOnlyField = false;
			this.validationOrProvisionErrorOnlyField = false;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001F8CE File Offset: 0x0001DACE
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001F8D6 File Offset: 0x0001DAD6
		[XmlArrayItem("AssignedPlanFilter", IsNullable = false)]
		public AssignedPlanFilterValue[] AssignedPlanFilters
		{
			get
			{
				return this.assignedPlanFiltersField;
			}
			set
			{
				this.assignedPlanFiltersField = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001F8DF File Offset: 0x0001DADF
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001F8E7 File Offset: 0x0001DAE7
		[XmlAttribute]
		[DefaultValue(false)]
		public bool AdministratorsOnly
		{
			get
			{
				return this.administratorsOnlyField;
			}
			set
			{
				this.administratorsOnlyField = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001F8F0 File Offset: 0x0001DAF0
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		[XmlAttribute]
		public string[] AssignedLicenseFilter
		{
			get
			{
				return this.assignedLicenseFilterField;
			}
			set
			{
				this.assignedLicenseFilterField = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001F901 File Offset: 0x0001DB01
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001F909 File Offset: 0x0001DB09
		[XmlAttribute]
		[DefaultValue(false)]
		public bool AssignedLicenseUnsetOnly
		{
			get
			{
				return this.assignedLicenseUnsetOnlyField;
			}
			set
			{
				this.assignedLicenseUnsetOnlyField = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001F912 File Offset: 0x0001DB12
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001F91A File Offset: 0x0001DB1A
		[XmlAttribute]
		[DefaultValue(false)]
		public bool BesServiceInstanceSetOnly
		{
			get
			{
				return this.besServiceInstanceSetOnlyField;
			}
			set
			{
				this.besServiceInstanceSetOnlyField = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001F923 File Offset: 0x0001DB23
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001F92B File Offset: 0x0001DB2B
		[XmlAttribute]
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

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001F934 File Offset: 0x0001DB34
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001F93C File Offset: 0x0001DB3C
		[XmlAttribute]
		public string Country
		{
			get
			{
				return this.countryField;
			}
			set
			{
				this.countryField = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001F945 File Offset: 0x0001DB45
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001F94D File Offset: 0x0001DB4D
		[XmlAttribute]
		public string Department
		{
			get
			{
				return this.departmentField;
			}
			set
			{
				this.departmentField = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001F956 File Offset: 0x0001DB56
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001F95E File Offset: 0x0001DB5E
		[XmlAttribute]
		public string DomainInUse
		{
			get
			{
				return this.domainInUseField;
			}
			set
			{
				this.domainInUseField = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001F967 File Offset: 0x0001DB67
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001F96F File Offset: 0x0001DB6F
		[XmlAttribute]
		public bool Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001F978 File Offset: 0x0001DB78
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001F980 File Offset: 0x0001DB80
		[XmlIgnore]
		public bool EnabledSpecified
		{
			get
			{
				return this.enabledFieldSpecified;
			}
			set
			{
				this.enabledFieldSpecified = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001F989 File Offset: 0x0001DB89
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001F991 File Offset: 0x0001DB91
		[XmlAttribute]
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001F99A File Offset: 0x0001DB9A
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001F9A2 File Offset: 0x0001DBA2
		[XmlAttribute]
		[DefaultValue(false)]
		public bool LicenseReconciliationNeededOnly
		{
			get
			{
				return this.licenseReconciliationNeededOnlyField;
			}
			set
			{
				this.licenseReconciliationNeededOnlyField = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001F9AB File Offset: 0x0001DBAB
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001F9B3 File Offset: 0x0001DBB3
		[DefaultValue(false)]
		[XmlAttribute]
		public bool MailboxGuidSetOnly
		{
			get
			{
				return this.mailboxGuidSetOnlyField;
			}
			set
			{
				this.mailboxGuidSetOnlyField = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001F9BC File Offset: 0x0001DBBC
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001F9C4 File Offset: 0x0001DBC4
		[XmlAttribute]
		public int MigrationState
		{
			get
			{
				return this.migrationStateField;
			}
			set
			{
				this.migrationStateField = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001F9CD File Offset: 0x0001DBCD
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001F9D5 File Offset: 0x0001DBD5
		[XmlIgnore]
		public bool MigrationStateSpecified
		{
			get
			{
				return this.migrationStateFieldSpecified;
			}
			set
			{
				this.migrationStateFieldSpecified = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001F9DE File Offset: 0x0001DBDE
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001F9E6 File Offset: 0x0001DBE6
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001F9EF File Offset: 0x0001DBEF
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001F9F7 File Offset: 0x0001DBF7
		[XmlAttribute]
		public string SearchId
		{
			get
			{
				return this.searchIdField;
			}
			set
			{
				this.searchIdField = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001FA00 File Offset: 0x0001DC00
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001FA08 File Offset: 0x0001DC08
		[DefaultValue(false)]
		[XmlAttribute]
		public bool SoftDeleted
		{
			get
			{
				return this.softDeletedField;
			}
			set
			{
				this.softDeletedField = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001FA11 File Offset: 0x0001DC11
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001FA19 File Offset: 0x0001DC19
		[XmlAttribute]
		public DateTime SoftDeletionTimestampAfterOrAt
		{
			get
			{
				return this.softDeletionTimestampAfterOrAtField;
			}
			set
			{
				this.softDeletionTimestampAfterOrAtField = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001FA22 File Offset: 0x0001DC22
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001FA2A File Offset: 0x0001DC2A
		[XmlIgnore]
		public bool SoftDeletionTimestampAfterOrAtSpecified
		{
			get
			{
				return this.softDeletionTimestampAfterOrAtFieldSpecified;
			}
			set
			{
				this.softDeletionTimestampAfterOrAtFieldSpecified = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001FA33 File Offset: 0x0001DC33
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001FA3B File Offset: 0x0001DC3B
		[XmlAttribute]
		public DateTime SoftDeletionTimestampBeforeOrAt
		{
			get
			{
				return this.softDeletionTimestampBeforeOrAtField;
			}
			set
			{
				this.softDeletionTimestampBeforeOrAtField = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001FA44 File Offset: 0x0001DC44
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001FA4C File Offset: 0x0001DC4C
		[XmlIgnore]
		public bool SoftDeletionTimestampBeforeOrAtSpecified
		{
			get
			{
				return this.softDeletionTimestampBeforeOrAtFieldSpecified;
			}
			set
			{
				this.softDeletionTimestampBeforeOrAtFieldSpecified = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001FA55 File Offset: 0x0001DC55
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001FA5D File Offset: 0x0001DC5D
		[XmlAttribute]
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

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001FA66 File Offset: 0x0001DC66
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001FA6E File Offset: 0x0001DC6E
		[XmlAttribute]
		[DefaultValue(false)]
		public bool SynchronizedOnly
		{
			get
			{
				return this.synchronizedOnlyField;
			}
			set
			{
				this.synchronizedOnlyField = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001FA77 File Offset: 0x0001DC77
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001FA7F File Offset: 0x0001DC7F
		[XmlAttribute]
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

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001FA88 File Offset: 0x0001DC88
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001FA90 File Offset: 0x0001DC90
		[XmlAttribute]
		public string ValidationErrorServiceType
		{
			get
			{
				return this.validationErrorServiceTypeField;
			}
			set
			{
				this.validationErrorServiceTypeField = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001FA99 File Offset: 0x0001DC99
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001FAA1 File Offset: 0x0001DCA1
		[XmlAttribute]
		[DefaultValue(false)]
		public bool ValidationErrorUnresolvedOnly
		{
			get
			{
				return this.validationErrorUnresolvedOnlyField;
			}
			set
			{
				this.validationErrorUnresolvedOnlyField = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001FAAA File Offset: 0x0001DCAA
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0001FAB2 File Offset: 0x0001DCB2
		[DefaultValue(false)]
		[XmlAttribute]
		public bool ValidationOrProvisionErrorOnly
		{
			get
			{
				return this.validationOrProvisionErrorOnlyField;
			}
			set
			{
				this.validationOrProvisionErrorOnlyField = value;
			}
		}

		// Token: 0x04000394 RID: 916
		private AssignedPlanFilterValue[] assignedPlanFiltersField;

		// Token: 0x04000395 RID: 917
		private bool administratorsOnlyField;

		// Token: 0x04000396 RID: 918
		private string[] assignedLicenseFilterField;

		// Token: 0x04000397 RID: 919
		private bool assignedLicenseUnsetOnlyField;

		// Token: 0x04000398 RID: 920
		private bool besServiceInstanceSetOnlyField;

		// Token: 0x04000399 RID: 921
		private string cityField;

		// Token: 0x0400039A RID: 922
		private string countryField;

		// Token: 0x0400039B RID: 923
		private string departmentField;

		// Token: 0x0400039C RID: 924
		private string domainInUseField;

		// Token: 0x0400039D RID: 925
		private bool enabledField;

		// Token: 0x0400039E RID: 926
		private bool enabledFieldSpecified;

		// Token: 0x0400039F RID: 927
		private string jobTitleField;

		// Token: 0x040003A0 RID: 928
		private bool licenseReconciliationNeededOnlyField;

		// Token: 0x040003A1 RID: 929
		private bool mailboxGuidSetOnlyField;

		// Token: 0x040003A2 RID: 930
		private int migrationStateField;

		// Token: 0x040003A3 RID: 931
		private bool migrationStateFieldSpecified;

		// Token: 0x040003A4 RID: 932
		private string nameField;

		// Token: 0x040003A5 RID: 933
		private string searchIdField;

		// Token: 0x040003A6 RID: 934
		private bool softDeletedField;

		// Token: 0x040003A7 RID: 935
		private DateTime softDeletionTimestampAfterOrAtField;

		// Token: 0x040003A8 RID: 936
		private bool softDeletionTimestampAfterOrAtFieldSpecified;

		// Token: 0x040003A9 RID: 937
		private DateTime softDeletionTimestampBeforeOrAtField;

		// Token: 0x040003AA RID: 938
		private bool softDeletionTimestampBeforeOrAtFieldSpecified;

		// Token: 0x040003AB RID: 939
		private string stateField;

		// Token: 0x040003AC RID: 940
		private bool synchronizedOnlyField;

		// Token: 0x040003AD RID: 941
		private string usageLocationField;

		// Token: 0x040003AE RID: 942
		private string validationErrorServiceTypeField;

		// Token: 0x040003AF RID: 943
		private bool validationErrorUnresolvedOnlyField;

		// Token: 0x040003B0 RID: 944
		private bool validationOrProvisionErrorOnlyField;
	}
}
