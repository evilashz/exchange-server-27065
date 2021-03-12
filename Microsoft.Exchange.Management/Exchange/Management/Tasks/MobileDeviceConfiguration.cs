using System;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public class MobileDeviceConfiguration : IConfigurable
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public MobileDeviceConfiguration()
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000AC54 File Offset: 0x00008E54
		public MobileDeviceConfiguration(DeviceInfo deviceInfo)
		{
			ArgumentValidator.ThrowIfNull("deviceInfo", deviceInfo);
			ArgumentValidator.ThrowIfNull("deviceInfo.DeviceIdentity", deviceInfo.DeviceIdentity);
			MobileClientType clientType;
			if (!deviceInfo.DeviceIdentity.TryGetMobileClientType(out clientType))
			{
				throw new ArgumentException("deviceInfo.Protocol", "Protocol can only be EAS or MOWA.");
			}
			this.ClientType = clientType;
			this.recoveryPassword = deviceInfo.RecoveryPassword;
			this.FirstSyncTime = ((deviceInfo.FirstSyncTime != null) ? ((DateTime?)deviceInfo.FirstSyncTime) : null);
			this.LastPolicyUpdateTime = ((deviceInfo.LastPolicyUpdateTime != null) ? ((DateTime?)deviceInfo.LastPolicyUpdateTime) : null);
			this.LastSyncAttemptTime = ((deviceInfo.LastSyncAttemptTime != null) ? ((DateTime?)deviceInfo.LastSyncAttemptTime) : null);
			this.LastSuccessSync = ((deviceInfo.LastSyncSuccessTime != null) ? ((DateTime?)deviceInfo.LastSyncSuccessTime) : null);
			this.DeviceID = deviceInfo.DeviceIdentity.DeviceId;
			this.DeviceUserAgent = (deviceInfo.UserAgent ?? string.Empty);
			this.DeviceWipeAckTime = ((deviceInfo.WipeAckTime != null) ? ((DateTime?)deviceInfo.WipeAckTime) : null);
			this.DeviceWipeRequestTime = ((deviceInfo.WipeRequestTime != null) ? ((DateTime?)deviceInfo.WipeRequestTime) : null);
			this.DeviceWipeSentTime = ((deviceInfo.WipeSentTime != null) ? ((DateTime?)deviceInfo.WipeSentTime) : null);
			this.LastPingHeartbeat = deviceInfo.LastPingHeartbeat;
			this.DeviceModel = deviceInfo.DeviceModel;
			this.DeviceImei = deviceInfo.DeviceImei;
			this.DeviceFriendlyName = deviceInfo.DeviceFriendlyName;
			this.DeviceOS = deviceInfo.DeviceOS;
			if (this.ClientType == MobileClientType.MOWA)
			{
				string format = Strings.MOWADeviceTypePrefix;
				string arg;
				if ((arg = deviceInfo.DeviceFriendlyName) == null && (arg = deviceInfo.DeviceModel) == null)
				{
					arg = (deviceInfo.DeviceIdentity.DeviceType ?? deviceInfo.DeviceOS);
				}
				this.DeviceType = string.Format(format, arg);
				this.IsRemoteWipeSupported = true;
			}
			else
			{
				this.DeviceType = deviceInfo.DeviceIdentity.DeviceType;
				this.IsRemoteWipeSupported = deviceInfo.IsRemoteWipeSupported;
			}
			this.DeviceOSLanguage = deviceInfo.DeviceOSLanguage;
			this.DeviceEnableOutboundSMS = deviceInfo.DeviceEnableOutboundSMS;
			this.DeviceMobileOperator = deviceInfo.DeviceMobileOperator;
			this.DeviceAccessState = deviceInfo.DeviceAccessState;
			this.DeviceAccessStateReason = deviceInfo.DeviceAccessStateReason;
			this.DeviceAccessControlRule = deviceInfo.DeviceAccessControlRule;
			this.DevicePolicyApplied = deviceInfo.DevicePolicyApplied;
			this.DevicePolicyApplicationStatus = deviceInfo.DevicePolicyApplicationStatus;
			this.LastDeviceWipeRequestor = deviceInfo.LastDeviceWipeRequestor;
			this.ClientVersion = deviceInfo.ClientVersion;
			this.NumberOfFoldersSynced = deviceInfo.NumberOfFoldersSynced;
			this.SyncStateUpgradeTime = ((deviceInfo.SSUpgradeDateTime != null) ? ((DateTime?)deviceInfo.SSUpgradeDateTime) : null);
			this.obfuscatedPhoneNumber = DeviceInfo.ObfuscatePhoneNumber(deviceInfo.DevicePhoneNumber);
			this.identity = deviceInfo.DeviceADObjectId;
			this.Guid = deviceInfo.DeviceADObjectId.ObjectGuid;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000AFA2 File Offset: 0x000091A2
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000AFAA File Offset: 0x000091AA
		public DateTime? FirstSyncTime { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000AFB3 File Offset: 0x000091B3
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000AFBB File Offset: 0x000091BB
		public DateTime? LastPolicyUpdateTime { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000AFC4 File Offset: 0x000091C4
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000AFCC File Offset: 0x000091CC
		public DateTime? LastSyncAttemptTime { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000AFD5 File Offset: 0x000091D5
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000AFDD File Offset: 0x000091DD
		public DateTime? LastSuccessSync { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AFE6 File Offset: 0x000091E6
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000AFEE File Offset: 0x000091EE
		public string DeviceType { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000AFF7 File Offset: 0x000091F7
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000AFFF File Offset: 0x000091FF
		public string DeviceID { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000B008 File Offset: 0x00009208
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000B010 File Offset: 0x00009210
		public string DeviceUserAgent { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B019 File Offset: 0x00009219
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000B021 File Offset: 0x00009221
		public DateTime? DeviceWipeSentTime { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000B02A File Offset: 0x0000922A
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000B032 File Offset: 0x00009232
		public DateTime? DeviceWipeRequestTime { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B03B File Offset: 0x0000923B
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000B043 File Offset: 0x00009243
		public DateTime? DeviceWipeAckTime { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B04C File Offset: 0x0000924C
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000B054 File Offset: 0x00009254
		public uint? LastPingHeartbeat { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B05D File Offset: 0x0000925D
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000B065 File Offset: 0x00009265
		public string RecoveryPassword
		{
			get
			{
				return this.recoveryPassword;
			}
			set
			{
				this.recoveryPassword = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B06E File Offset: 0x0000926E
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000B076 File Offset: 0x00009276
		public string DeviceModel { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B07F File Offset: 0x0000927F
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000B087 File Offset: 0x00009287
		public string DeviceImei { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B090 File Offset: 0x00009290
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000B098 File Offset: 0x00009298
		public string DeviceFriendlyName { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B0A1 File Offset: 0x000092A1
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000B0A9 File Offset: 0x000092A9
		public string DeviceOS { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000B0B2 File Offset: 0x000092B2
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000B0BA File Offset: 0x000092BA
		public string DeviceOSLanguage { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B0C3 File Offset: 0x000092C3
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000B0CB File Offset: 0x000092CB
		public string DevicePhoneNumber
		{
			get
			{
				return this.obfuscatedPhoneNumber;
			}
			set
			{
				this.obfuscatedPhoneNumber = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000B0D4 File Offset: 0x000092D4
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000B0DC File Offset: 0x000092DC
		public string MailboxLogReport
		{
			get
			{
				return this.mailboxLogReport;
			}
			set
			{
				this.mailboxLogReport = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000B0E5 File Offset: 0x000092E5
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000B0ED File Offset: 0x000092ED
		public bool DeviceEnableOutboundSMS { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B0F6 File Offset: 0x000092F6
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000B0FE File Offset: 0x000092FE
		public string DeviceMobileOperator { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B107 File Offset: 0x00009307
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000B10F File Offset: 0x0000930F
		public ObjectId Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.identity = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B126 File Offset: 0x00009326
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000B12E File Offset: 0x0000932E
		public Guid Guid { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B137 File Offset: 0x00009337
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000B13F File Offset: 0x0000933F
		public bool IsRemoteWipeSupported { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B148 File Offset: 0x00009348
		public DeviceRemoteWipeStatus Status
		{
			get
			{
				if (this.DeviceWipeAckTime != null)
				{
					return DeviceRemoteWipeStatus.DeviceWipeSucceeded;
				}
				if (this.DeviceWipeRequestTime == null && this.DeviceWipeSentTime == null)
				{
					return DeviceRemoteWipeStatus.DeviceOk;
				}
				if (!this.IsRemoteWipeSupported)
				{
					return DeviceRemoteWipeStatus.DeviceBlocked;
				}
				return DeviceRemoteWipeStatus.DeviceWipePending;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B194 File Offset: 0x00009394
		public string StatusNote
		{
			get
			{
				string result = string.Empty;
				switch (this.Status)
				{
				case DeviceRemoteWipeStatus.DeviceWipePending:
					result = Strings.WipePendingNote;
					break;
				case DeviceRemoteWipeStatus.DeviceWipeSucceeded:
					result = Strings.WipeSucceededNote;
					break;
				}
				return result;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000B1D9 File Offset: 0x000093D9
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000B1E1 File Offset: 0x000093E1
		public DeviceAccessState DeviceAccessState { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B1EA File Offset: 0x000093EA
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000B1F2 File Offset: 0x000093F2
		public DeviceAccessStateReason DeviceAccessStateReason { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B1FB File Offset: 0x000093FB
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000B203 File Offset: 0x00009403
		public ADObjectId DeviceAccessControlRule { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B20C File Offset: 0x0000940C
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000B214 File Offset: 0x00009414
		public ADObjectId DevicePolicyApplied { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B21D File Offset: 0x0000941D
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000B225 File Offset: 0x00009425
		public DevicePolicyApplicationStatus DevicePolicyApplicationStatus { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B22E File Offset: 0x0000942E
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B236 File Offset: 0x00009436
		public string LastDeviceWipeRequestor { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B23F File Offset: 0x0000943F
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B247 File Offset: 0x00009447
		public string ClientVersion { get; protected set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B250 File Offset: 0x00009450
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B258 File Offset: 0x00009458
		public int NumberOfFoldersSynced { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B261 File Offset: 0x00009461
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B269 File Offset: 0x00009469
		public DateTime? SyncStateUpgradeTime { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B272 File Offset: 0x00009472
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000B27A File Offset: 0x0000947A
		public MobileClientType ClientType { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B283 File Offset: 0x00009483
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B286 File Offset: 0x00009486
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B289 File Offset: 0x00009489
		ValidationError[] IConfigurable.Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B290 File Offset: 0x00009490
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B297 File Offset: 0x00009497
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000154 RID: 340
		private string recoveryPassword;

		// Token: 0x04000155 RID: 341
		private string obfuscatedPhoneNumber;

		// Token: 0x04000156 RID: 342
		private ObjectId identity;

		// Token: 0x04000157 RID: 343
		private string mailboxLogReport;
	}
}
