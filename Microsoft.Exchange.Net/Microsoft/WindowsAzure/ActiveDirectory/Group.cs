using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005AB RID: 1451
	[DataServiceKey("objectId")]
	public class Group : DirectoryObject
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x0002D0A0 File Offset: 0x0002B2A0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Group CreateGroup(string objectId, Collection<string> exchangeResources, Collection<ProvisioningError> provisioningErrors, Collection<string> proxyAddresses, Collection<string> sharepointResources)
		{
			Group group = new Group();
			group.objectId = objectId;
			if (exchangeResources == null)
			{
				throw new ArgumentNullException("exchangeResources");
			}
			group.exchangeResources = exchangeResources;
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

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0002D111 File Offset: 0x0002B311
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0002D119 File Offset: 0x0002B319
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

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0002D122 File Offset: 0x0002B322
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0002D12A File Offset: 0x0002B32A
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

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0002D133 File Offset: 0x0002B333
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0002D13B File Offset: 0x0002B33B
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

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0002D144 File Offset: 0x0002B344
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0002D14C File Offset: 0x0002B34C
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

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0002D155 File Offset: 0x0002B355
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0002D15D File Offset: 0x0002B35D
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

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0002D166 File Offset: 0x0002B366
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0002D16E File Offset: 0x0002B36E
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

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0002D177 File Offset: 0x0002B377
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0002D17F File Offset: 0x0002B37F
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

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0002D188 File Offset: 0x0002B388
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0002D190 File Offset: 0x0002B390
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

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x0002D199 File Offset: 0x0002B399
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x0002D1A1 File Offset: 0x0002B3A1
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

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x0002D1AA File Offset: 0x0002B3AA
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x0002D1B2 File Offset: 0x0002B3B2
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

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0002D1BB File Offset: 0x0002B3BB
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x0002D1C3 File Offset: 0x0002B3C3
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

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0002D1CC File Offset: 0x0002B3CC
		// (set) Token: 0x0600155B RID: 5467 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600155C RID: 5468 RVA: 0x0002D1DD File Offset: 0x0002B3DD
		// (set) Token: 0x0600155D RID: 5469 RVA: 0x0002D1E5 File Offset: 0x0002B3E5
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0002D1EE File Offset: 0x0002B3EE
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x0002D1F6 File Offset: 0x0002B3F6
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

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0002D1FF File Offset: 0x0002B3FF
		// (set) Token: 0x06001561 RID: 5473 RVA: 0x0002D207 File Offset: 0x0002B407
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

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0002D213 File Offset: 0x0002B413
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0002D21B File Offset: 0x0002B41B
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

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0002D227 File Offset: 0x0002B427
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x0002D22F File Offset: 0x0002B42F
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

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0002D23B File Offset: 0x0002B43B
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x0002D243 File Offset: 0x0002B443
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

		// Token: 0x040019A1 RID: 6561
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _exchangeResources = new Collection<string>();

		// Token: 0x040019A2 RID: 6562
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _description;

		// Token: 0x040019A3 RID: 6563
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x040019A4 RID: 6564
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x040019A5 RID: 6565
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _groupType;

		// Token: 0x040019A6 RID: 6566
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isPublic;

		// Token: 0x040019A7 RID: 6567
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x040019A8 RID: 6568
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mail;

		// Token: 0x040019A9 RID: 6569
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _mailNickname;

		// Token: 0x040019AA RID: 6570
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _mailEnabled;

		// Token: 0x040019AB RID: 6571
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<ProvisioningError> _provisioningErrors = new Collection<ProvisioningError>();

		// Token: 0x040019AC RID: 6572
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _proxyAddresses = new Collection<string>();

		// Token: 0x040019AD RID: 6573
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _securityEnabled;

		// Token: 0x040019AE RID: 6574
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _sharepointResources = new Collection<string>();

		// Token: 0x040019AF RID: 6575
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectAccessGrant> _directAccessGrants = new Collection<DirectAccessGrant>();

		// Token: 0x040019B0 RID: 6576
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _pendingMembers = new Collection<DirectoryObject>();

		// Token: 0x040019B1 RID: 6577
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _allowAccessTo = new Collection<DirectoryObject>();

		// Token: 0x040019B2 RID: 6578
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _hasAccessTo = new Collection<DirectoryObject>();
	}
}
