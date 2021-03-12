using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A7 RID: 1447
	[DataServiceKey("objectId")]
	public class Device : DirectoryObject
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x0002CD5C File Offset: 0x0002AF5C
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

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0002CDA1 File Offset: 0x0002AFA1
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x0002CDA9 File Offset: 0x0002AFA9
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0002CDB2 File Offset: 0x0002AFB2
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0002CDBA File Offset: 0x0002AFBA
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0002CDC3 File Offset: 0x0002AFC3
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0002CDCB File Offset: 0x0002AFCB
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0002CDDC File Offset: 0x0002AFDC
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

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0002CDE5 File Offset: 0x0002AFE5
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0002CDED File Offset: 0x0002AFED
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

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0002CDF6 File Offset: 0x0002AFF6
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x0002CDFE File Offset: 0x0002AFFE
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

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0002CE07 File Offset: 0x0002B007
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0002CE0F File Offset: 0x0002B00F
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

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0002CE18 File Offset: 0x0002B018
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0002CE20 File Offset: 0x0002B020
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

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x0002CE29 File Offset: 0x0002B029
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x0002CE31 File Offset: 0x0002B031
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

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x0002CE3A File Offset: 0x0002B03A
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x0002CE42 File Offset: 0x0002B042
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0002CE4B File Offset: 0x0002B04B
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x0002CE53 File Offset: 0x0002B053
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x0002CE5C File Offset: 0x0002B05C
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x0002CE64 File Offset: 0x0002B064
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0002CE70 File Offset: 0x0002B070
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x0002CE78 File Offset: 0x0002B078
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

		// Token: 0x04001982 RID: 6530
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _accountEnabled;

		// Token: 0x04001983 RID: 6531
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<AlternativeSecurityId> _alternativeSecurityIds = new Collection<AlternativeSecurityId>();

		// Token: 0x04001984 RID: 6532
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _approximateLastLogonTimestamp;

		// Token: 0x04001985 RID: 6533
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _deviceId;

		// Token: 0x04001986 RID: 6534
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _deviceObjectVersion;

		// Token: 0x04001987 RID: 6535
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _deviceOSType;

		// Token: 0x04001988 RID: 6536
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _deviceOSVersion;

		// Token: 0x04001989 RID: 6537
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _devicePhysicalIds = new Collection<string>();

		// Token: 0x0400198A RID: 6538
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _dirSyncEnabled;

		// Token: 0x0400198B RID: 6539
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _displayName;

		// Token: 0x0400198C RID: 6540
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastDirSyncTime;

		// Token: 0x0400198D RID: 6541
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredOwners = new Collection<DirectoryObject>();

		// Token: 0x0400198E RID: 6542
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<DirectoryObject> _registeredUsers = new Collection<DirectoryObject>();
	}
}
