using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C8 RID: 1480
	[DataServiceKey("objectId")]
	public class Device : DirectoryObject
	{
		// Token: 0x06001753 RID: 5971 RVA: 0x0002EA94 File Offset: 0x0002CC94
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Device CreateDevice(string objectId, Collection<AlternativeSecurityId> alternativeSecurityIds, Collection<string> devicePhysicalIds, Collection<string> exchangeActiveSyncId)
		{
			Device device = new Device();
			device.objectId = objectId;
			if (alternativeSecurityIds == null)
			{
				throw new ArgumentNullException("alternativeSecurityIds");
			}
			device.alternativeSecurityIds = alternativeSecurityIds;
			if (devicePhysicalIds == null)
			{
				throw new ArgumentNullException("devicePhysicalIds");
			}
			device.devicePhysicalIds = devicePhysicalIds;
			if (exchangeActiveSyncId == null)
			{
				throw new ArgumentNullException("exchangeActiveSyncId");
			}
			device.exchangeActiveSyncId = exchangeActiveSyncId;
			return device;
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x0002EAEE File Offset: 0x0002CCEE
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x0002EAF6 File Offset: 0x0002CCF6
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? accountEnabled
		{
			get
			{
				return this._accountEnabled;
			}
			set
			{
				this._accountEnabled = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0002EAFF File Offset: 0x0002CCFF
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x0002EB07 File Offset: 0x0002CD07
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<AlternativeSecurityId> alternativeSecurityIds
		{
			get
			{
				return this._alternativeSecurityIds;
			}
			set
			{
				this._alternativeSecurityIds = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0002EB10 File Offset: 0x0002CD10
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x0002EB18 File Offset: 0x0002CD18
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? approximateLastLogonTimestamp
		{
			get
			{
				return this._approximateLastLogonTimestamp;
			}
			set
			{
				this._approximateLastLogonTimestamp = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0002EB21 File Offset: 0x0002CD21
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x0002EB29 File Offset: 0x0002CD29
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? deviceId
		{
			get
			{
				return this._deviceId;
			}
			set
			{
				this._deviceId = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0002EB32 File Offset: 0x0002CD32
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x0002EB3A File Offset: 0x0002CD3A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? deviceObjectVersion
		{
			get
			{
				return this._deviceObjectVersion;
			}
			set
			{
				this._deviceObjectVersion = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0002EB43 File Offset: 0x0002CD43
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x0002EB4B File Offset: 0x0002CD4B
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string deviceOSType
		{
			get
			{
				return this._deviceOSType;
			}
			set
			{
				this._deviceOSType = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0002EB54 File Offset: 0x0002CD54
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x0002EB5C File Offset: 0x0002CD5C
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string deviceOSVersion
		{
			get
			{
				return this._deviceOSVersion;
			}
			set
			{
				this._deviceOSVersion = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0002EB65 File Offset: 0x0002CD65
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x0002EB6D File Offset: 0x0002CD6D
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> devicePhysicalIds
		{
			get
			{
				return this._devicePhysicalIds;
			}
			set
			{
				this._devicePhysicalIds = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0002EB76 File Offset: 0x0002CD76
		// (set) Token: 0x06001765 RID: 5989 RVA: 0x0002EB7E File Offset: 0x0002CD7E
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

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0002EB87 File Offset: 0x0002CD87
		// (set) Token: 0x06001767 RID: 5991 RVA: 0x0002EB8F File Offset: 0x0002CD8F
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

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0002EB98 File Offset: 0x0002CD98
		// (set) Token: 0x06001769 RID: 5993 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> exchangeActiveSyncId
		{
			get
			{
				return this._exchangeActiveSyncId;
			}
			set
			{
				this._exchangeActiveSyncId = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x0002EBA9 File Offset: 0x0002CDA9
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x0002EBB1 File Offset: 0x0002CDB1
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? isCompliant
		{
			get
			{
				return this._isCompliant;
			}
			set
			{
				this._isCompliant = value;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0002EBBA File Offset: 0x0002CDBA
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0002EBC2 File Offset: 0x0002CDC2
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? isManaged
		{
			get
			{
				return this._isManaged;
			}
			set
			{
				this._isManaged = value;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0002EBCB File Offset: 0x0002CDCB
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x0002EBD3 File Offset: 0x0002CDD3
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		// (set) Token: 0x06001771 RID: 6001 RVA: 0x0002EBE4 File Offset: 0x0002CDE4
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> registeredOwners
		{
			get
			{
				return this._registeredOwners;
			}
			set
			{
				if (value != null)
				{
					this._registeredOwners = value;
				}
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0002EBF0 File Offset: 0x0002CDF0
		// (set) Token: 0x06001773 RID: 6003 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<DirectoryObject> registeredUsers
		{
			get
			{
				return this._registeredUsers;
			}
			set
			{
				if (value != null)
				{
					this._registeredUsers = value;
				}
			}
		}

		// Token: 0x04001A95 RID: 6805
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001A96 RID: 6806
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AlternativeSecurityId> _alternativeSecurityIds = new Collection<AlternativeSecurityId>();

		// Token: 0x04001A97 RID: 6807
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _approximateLastLogonTimestamp;

		// Token: 0x04001A98 RID: 6808
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _deviceId;

		// Token: 0x04001A99 RID: 6809
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _deviceObjectVersion;

		// Token: 0x04001A9A RID: 6810
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _deviceOSType;

		// Token: 0x04001A9B RID: 6811
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _deviceOSVersion;

		// Token: 0x04001A9C RID: 6812
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _devicePhysicalIds = new Collection<string>();

		// Token: 0x04001A9D RID: 6813
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001A9E RID: 6814
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001A9F RID: 6815
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _exchangeActiveSyncId = new Collection<string>();

		// Token: 0x04001AA0 RID: 6816
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isCompliant;

		// Token: 0x04001AA1 RID: 6817
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _isManaged;

		// Token: 0x04001AA2 RID: 6818
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001AA3 RID: 6819
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredOwners = new Collection<DirectoryObject>();

		// Token: 0x04001AA4 RID: 6820
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredUsers = new Collection<DirectoryObject>();
	}
}
