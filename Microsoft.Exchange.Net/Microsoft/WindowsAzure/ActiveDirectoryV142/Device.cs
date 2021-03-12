using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005F0 RID: 1520
	[DataServiceKey("objectId")]
	public class Device : DirectoryObject
	{
		// Token: 0x06001A2B RID: 6699 RVA: 0x00030E70 File Offset: 0x0002F070
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static Device CreateDevice(string objectId, Collection<AlternativeSecurityId> alternativeSecurityIds, Collection<string> devicePhysicalIds)
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
			return device;
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x00030EB5 File Offset: 0x0002F0B5
		// (set) Token: 0x06001A2D RID: 6701 RVA: 0x00030EBD File Offset: 0x0002F0BD
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

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00030EC6 File Offset: 0x0002F0C6
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00030ECE File Offset: 0x0002F0CE
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

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x00030ED7 File Offset: 0x0002F0D7
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x00030EDF File Offset: 0x0002F0DF
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

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00030EE8 File Offset: 0x0002F0E8
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x00030EF0 File Offset: 0x0002F0F0
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

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00030EF9 File Offset: 0x0002F0F9
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x00030F01 File Offset: 0x0002F101
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

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00030F0A File Offset: 0x0002F10A
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00030F12 File Offset: 0x0002F112
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

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00030F1B File Offset: 0x0002F11B
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x00030F23 File Offset: 0x0002F123
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

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x00030F2C File Offset: 0x0002F12C
		// (set) Token: 0x06001A3B RID: 6715 RVA: 0x00030F34 File Offset: 0x0002F134
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

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x00030F3D File Offset: 0x0002F13D
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x00030F45 File Offset: 0x0002F145
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

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00030F4E File Offset: 0x0002F14E
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00030F56 File Offset: 0x0002F156
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

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00030F5F File Offset: 0x0002F15F
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00030F67 File Offset: 0x0002F167
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

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00030F70 File Offset: 0x0002F170
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00030F78 File Offset: 0x0002F178
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

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x00030F84 File Offset: 0x0002F184
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00030F8C File Offset: 0x0002F18C
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

		// Token: 0x04001BE7 RID: 7143
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001BE8 RID: 7144
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AlternativeSecurityId> _alternativeSecurityIds = new Collection<AlternativeSecurityId>();

		// Token: 0x04001BE9 RID: 7145
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _approximateLastLogonTimestamp;

		// Token: 0x04001BEA RID: 7146
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _deviceId;

		// Token: 0x04001BEB RID: 7147
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _deviceObjectVersion;

		// Token: 0x04001BEC RID: 7148
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _deviceOSType;

		// Token: 0x04001BED RID: 7149
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _deviceOSVersion;

		// Token: 0x04001BEE RID: 7150
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _devicePhysicalIds = new Collection<string>();

		// Token: 0x04001BEF RID: 7151
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x04001BF0 RID: 7152
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x04001BF1 RID: 7153
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x04001BF2 RID: 7154
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredOwners = new Collection<DirectoryObject>();

		// Token: 0x04001BF3 RID: 7155
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredUsers = new Collection<DirectoryObject>();
	}
}
