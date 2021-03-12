using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F5 RID: 1525
	[DataServiceKey("objectId")]
	public class Group : DirectoryObject
	{
		// Token: 0x06001A76 RID: 6774 RVA: 0x000311DC File Offset: 0x0002F3DC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Group CreateGroup(string objectId, Collection<string> exchangeResources, Collection<LicenseAssignment> licenseAssignment, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses, Collection<string> sharepointResources)
		{
			Group group = new Group();
			group.objectId = objectId;
			if (exchangeResources == null)
			{
				throw new ArgumentNullException("exchangeResources");
			}
			group.exchangeResources = exchangeResources;
			if (licenseAssignment == null)
			{
				throw new ArgumentNullException("licenseAssignment");
			}
			group.licenseAssignment = licenseAssignment;
			if (provisioningErrors == null)
			{
				throw new ArgumentNullException("provisioningErrors");
			}
			group.provisioningErrors = provisioningErrors;
			if (proxyAddresses == null)
			{
				throw new ArgumentNullException("proxyAddresses");
			}
			group.proxyAddresses = proxyAddresses;
			if (sharepointResources == null)
			{
				throw new ArgumentNullException("sharepointResources");
			}
			group.sharepointResources = sharepointResources;
			return group;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00031264 File Offset: 0x0002F464
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x0003128E File Offset: 0x0002F48E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public AppMetadata appMetadata
		{
			get
			{
				if (this._appMetadata == null && !this._appMetadataInitialized)
				{
					this._appMetadata = new AppMetadata();
					this._appMetadataInitialized = true;
				}
				return this._appMetadata;
			}
			set
			{
				this._appMetadata = value;
				this._appMetadataInitialized = true;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0003129E File Offset: 0x0002F49E
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x000312A6 File Offset: 0x0002F4A6
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> exchangeResources
		{
			get
			{
				return this._exchangeResources;
			}
			set
			{
				this._exchangeResources = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000312AF File Offset: 0x0002F4AF
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x000312B7 File Offset: 0x0002F4B7
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x000312C0 File Offset: 0x0002F4C0
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x000312C8 File Offset: 0x0002F4C8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? dirSyncEnabled
		{
			get
			{
				return this._dirSyncEnabled;
			}
			set
			{
				this._dirSyncEnabled = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000312D1 File Offset: 0x0002F4D1
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x000312D9 File Offset: 0x0002F4D9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string displayName
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x000312E2 File Offset: 0x0002F4E2
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x000312EA File Offset: 0x0002F4EA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string groupType
		{
			get
			{
				return this._groupType;
			}
			set
			{
				this._groupType = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x000312F3 File Offset: 0x0002F4F3
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x000312FB File Offset: 0x0002F4FB
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? isPublic
		{
			get
			{
				return this._isPublic;
			}
			set
			{
				this._isPublic = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00031304 File Offset: 0x0002F504
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x0003130C File Offset: 0x0002F50C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? lastDirSyncTime
		{
			get
			{
				return this._lastDirSyncTime;
			}
			set
			{
				this._lastDirSyncTime = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x00031315 File Offset: 0x0002F515
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0003131D File Offset: 0x0002F51D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<LicenseAssignment> licenseAssignment
		{
			get
			{
				return this._licenseAssignment;
			}
			set
			{
				this._licenseAssignment = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00031326 File Offset: 0x0002F526
		// (set) Token: 0x06001A8A RID: 6794 RVA: 0x0003132E File Offset: 0x0002F52E
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mail
		{
			get
			{
				return this._mail;
			}
			set
			{
				this._mail = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x00031337 File Offset: 0x0002F537
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x0003133F File Offset: 0x0002F53F
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string mailNickname
		{
			get
			{
				return this._mailNickname;
			}
			set
			{
				this._mailNickname = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x00031348 File Offset: 0x0002F548
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x00031350 File Offset: 0x0002F550
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? mailEnabled
		{
			get
			{
				return this._mailEnabled;
			}
			set
			{
				this._mailEnabled = value;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x00031359 File Offset: 0x0002F559
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x00031375 File Offset: 0x0002F575
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public byte[] onPremiseSecurityIdentifier
		{
			get
			{
				if (this._onPremiseSecurityIdentifier != null)
				{
					return (byte[])this._onPremiseSecurityIdentifier.Clone();
				}
				return null;
			}
			set
			{
				this._onPremiseSecurityIdentifier = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0003137E File Offset: 0x0002F57E
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x00031386 File Offset: 0x0002F586
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<ProvisioningError> provisioningErrors
		{
			get
			{
				return this._provisioningErrors;
			}
			set
			{
				this._provisioningErrors = value;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0003138F File Offset: 0x0002F58F
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x00031397 File Offset: 0x0002F597
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> proxyAddresses
		{
			get
			{
				return this._proxyAddresses;
			}
			set
			{
				this._proxyAddresses = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x000313A0 File Offset: 0x0002F5A0
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x000313A8 File Offset: 0x0002F5A8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? securityEnabled
		{
			get
			{
				return this._securityEnabled;
			}
			set
			{
				this._securityEnabled = value;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x000313B1 File Offset: 0x0002F5B1
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x000313B9 File Offset: 0x0002F5B9
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> sharepointResources
		{
			get
			{
				return this._sharepointResources;
			}
			set
			{
				this._sharepointResources = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000313C2 File Offset: 0x0002F5C2
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x000313CA File Offset: 0x0002F5CA
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectAccessGrant> directAccessGrants
		{
			get
			{
				return this._directAccessGrants;
			}
			set
			{
				if (value != null)
				{
					this._directAccessGrants = value;
				}
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x000313D6 File Offset: 0x0002F5D6
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x000313DE File Offset: 0x0002F5DE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> pendingMembers
		{
			get
			{
				return this._pendingMembers;
			}
			set
			{
				if (value != null)
				{
					this._pendingMembers = value;
				}
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000313EA File Offset: 0x0002F5EA
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x000313F2 File Offset: 0x0002F5F2
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> allowAccessTo
		{
			get
			{
				return this._allowAccessTo;
			}
			set
			{
				if (value != null)
				{
					this._allowAccessTo = value;
				}
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x000313FE File Offset: 0x0002F5FE
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x00031406 File Offset: 0x0002F606
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> hasAccessTo
		{
			get
			{
				return this._hasAccessTo;
			}
			set
			{
				if (value != null)
				{
					this._hasAccessTo = value;
				}
			}
		}

		// Token: 0x04001C08 RID: 7176
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private AppMetadata _appMetadata;

		// Token: 0x04001C09 RID: 7177
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool _appMetadataInitialized;

		// Token: 0x04001C0A RID: 7178
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _exchangeResources = new Collection<string>();

		// Token: 0x04001C0B RID: 7179
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x04001C0C RID: 7180
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001C0D RID: 7181
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001C0E RID: 7182
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _groupType;

		// Token: 0x04001C0F RID: 7183
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isPublic;

		// Token: 0x04001C10 RID: 7184
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001C11 RID: 7185
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<LicenseAssignment> _licenseAssignment = new Collection<LicenseAssignment>();

		// Token: 0x04001C12 RID: 7186
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x04001C13 RID: 7187
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x04001C14 RID: 7188
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _mailEnabled;

		// Token: 0x04001C15 RID: 7189
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _onPremiseSecurityIdentifier;

		// Token: 0x04001C16 RID: 7190
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x04001C17 RID: 7191
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x04001C18 RID: 7192
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _securityEnabled;

		// Token: 0x04001C19 RID: 7193
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _sharepointResources = new Collection<string>();

		// Token: 0x04001C1A RID: 7194
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrants = new Collection<DirectAccessGrant>();

		// Token: 0x04001C1B RID: 7195
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _pendingMembers = new Collection<DirectoryObject>();

		// Token: 0x04001C1C RID: 7196
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _allowAccessTo = new Collection<DirectoryObject>();

		// Token: 0x04001C1D RID: 7197
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _hasAccessTo = new Collection<DirectoryObject>();
	}
}
