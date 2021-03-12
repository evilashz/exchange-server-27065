using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AEA RID: 2794
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MobileDeviceStatistics : OptionsPropertyChangeTracker
	{
		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06004F6E RID: 20334 RVA: 0x001089F5 File Offset: 0x00106BF5
		// (set) Token: 0x06004F6F RID: 20335 RVA: 0x001089FD File Offset: 0x00106BFD
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string FirstSyncTime
		{
			get
			{
				return this.firstSyncTime;
			}
			set
			{
				this.firstSyncTime = value;
				base.TrackPropertyChanged("FirstSyncTime");
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06004F70 RID: 20336 RVA: 0x00108A11 File Offset: 0x00106C11
		// (set) Token: 0x06004F71 RID: 20337 RVA: 0x00108A19 File Offset: 0x00106C19
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string LastPolicyUpdateTime
		{
			get
			{
				return this.lastPolicyUpdateTime;
			}
			set
			{
				this.lastPolicyUpdateTime = value;
				base.TrackPropertyChanged("LastPolicyUpdateTime");
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06004F72 RID: 20338 RVA: 0x00108A2D File Offset: 0x00106C2D
		// (set) Token: 0x06004F73 RID: 20339 RVA: 0x00108A35 File Offset: 0x00106C35
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string LastSyncAttemptTime
		{
			get
			{
				return this.lastSyncAttemptTime;
			}
			set
			{
				this.lastSyncAttemptTime = value;
				base.TrackPropertyChanged("LastSyncAttemptTime");
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x00108A49 File Offset: 0x00106C49
		// (set) Token: 0x06004F75 RID: 20341 RVA: 0x00108A51 File Offset: 0x00106C51
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string LastSuccessSync
		{
			get
			{
				return this.lastSuccessSync;
			}
			set
			{
				this.lastSuccessSync = value;
				base.TrackPropertyChanged("LastSuccessSync");
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x00108A65 File Offset: 0x00106C65
		// (set) Token: 0x06004F77 RID: 20343 RVA: 0x00108A6D File Offset: 0x00106C6D
		[DataMember]
		public string DeviceType
		{
			get
			{
				return this.deviceType;
			}
			set
			{
				this.deviceType = value;
				base.TrackPropertyChanged("DeviceType");
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00108A81 File Offset: 0x00106C81
		// (set) Token: 0x06004F79 RID: 20345 RVA: 0x00108A89 File Offset: 0x00106C89
		[DataMember]
		public string DeviceId
		{
			get
			{
				return this.deviceId;
			}
			set
			{
				this.deviceId = value;
				base.TrackPropertyChanged("DeviceId");
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x00108A9D File Offset: 0x00106C9D
		// (set) Token: 0x06004F7B RID: 20347 RVA: 0x00108AA5 File Offset: 0x00106CA5
		[DataMember]
		public string DeviceUserAgent
		{
			get
			{
				return this.deviceUserAgent;
			}
			set
			{
				this.deviceUserAgent = value;
				base.TrackPropertyChanged("DeviceUserAgent");
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x00108AB9 File Offset: 0x00106CB9
		// (set) Token: 0x06004F7D RID: 20349 RVA: 0x00108AC1 File Offset: 0x00106CC1
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string DeviceWipeSentTime
		{
			get
			{
				return this.deviceWipeSentTime;
			}
			set
			{
				this.deviceWipeSentTime = value;
				base.TrackPropertyChanged("DeviceWipeSentTime");
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004F7E RID: 20350 RVA: 0x00108AD5 File Offset: 0x00106CD5
		// (set) Token: 0x06004F7F RID: 20351 RVA: 0x00108ADD File Offset: 0x00106CDD
		[DataMember]
		public string DeviceModel
		{
			get
			{
				return this.deviceModel;
			}
			set
			{
				this.deviceModel = value;
				base.TrackPropertyChanged("DeviceModel");
			}
		}

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x00108AF1 File Offset: 0x00106CF1
		// (set) Token: 0x06004F81 RID: 20353 RVA: 0x00108AF9 File Offset: 0x00106CF9
		[DataMember]
		public string DeviceImei
		{
			get
			{
				return this.deviceImei;
			}
			set
			{
				this.deviceImei = value;
				base.TrackPropertyChanged("DeviceImei");
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06004F82 RID: 20354 RVA: 0x00108B0D File Offset: 0x00106D0D
		// (set) Token: 0x06004F83 RID: 20355 RVA: 0x00108B15 File Offset: 0x00106D15
		[DataMember]
		public string DeviceFriendlyName
		{
			get
			{
				return this.deviceFriendlyName;
			}
			set
			{
				this.deviceFriendlyName = value;
				base.TrackPropertyChanged("DeviceFriendlyName");
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x00108B29 File Offset: 0x00106D29
		// (set) Token: 0x06004F85 RID: 20357 RVA: 0x00108B31 File Offset: 0x00106D31
		[DataMember]
		public string DeviceOS
		{
			get
			{
				return this.deviceOS;
			}
			set
			{
				this.deviceOS = value;
				base.TrackPropertyChanged("DeviceOS");
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x00108B45 File Offset: 0x00106D45
		// (set) Token: 0x06004F87 RID: 20359 RVA: 0x00108B4D File Offset: 0x00106D4D
		[DataMember]
		public string DeviceOSLanguage
		{
			get
			{
				return this.deviceOSLanguage;
			}
			set
			{
				this.deviceOSLanguage = value;
				base.TrackPropertyChanged("DeviceOSLanguage");
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x00108B61 File Offset: 0x00106D61
		// (set) Token: 0x06004F89 RID: 20361 RVA: 0x00108B69 File Offset: 0x00106D69
		[DataMember]
		public string DevicePhoneNumber
		{
			get
			{
				return this.devicePhoneNumber;
			}
			set
			{
				this.devicePhoneNumber = value;
				base.TrackPropertyChanged("DevicePhoneNumber");
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004F8A RID: 20362 RVA: 0x00108B7D File Offset: 0x00106D7D
		// (set) Token: 0x06004F8B RID: 20363 RVA: 0x00108B85 File Offset: 0x00106D85
		[DataMember]
		public string DeviceMobileOperator
		{
			get
			{
				return this.deviceMobileOperator;
			}
			set
			{
				this.deviceMobileOperator = value;
				base.TrackPropertyChanged("DeviceMobileOperator");
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x00108B99 File Offset: 0x00106D99
		// (set) Token: 0x06004F8D RID: 20365 RVA: 0x00108BA1 File Offset: 0x00106DA1
		[DataMember]
		public string ClientVersion
		{
			get
			{
				return this.clientVersion;
			}
			set
			{
				this.clientVersion = value;
				base.TrackPropertyChanged("ClientVersion");
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x00108BB5 File Offset: 0x00106DB5
		// (set) Token: 0x06004F8F RID: 20367 RVA: 0x00108BBD File Offset: 0x00106DBD
		[IgnoreDataMember]
		public DeviceAccessState DeviceAccessState
		{
			get
			{
				return this.deviceAccessState;
			}
			set
			{
				this.deviceAccessState = value;
				base.TrackPropertyChanged("DeviceAccessState");
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06004F90 RID: 20368 RVA: 0x00108BD1 File Offset: 0x00106DD1
		// (set) Token: 0x06004F91 RID: 20369 RVA: 0x00108BDE File Offset: 0x00106DDE
		[DataMember(Name = "DeviceAccessState", IsRequired = false, EmitDefaultValue = false)]
		public string DeviceAccessStateString
		{
			get
			{
				return EnumUtilities.ToString<DeviceAccessState>(this.DeviceAccessState);
			}
			set
			{
				this.DeviceAccessState = EnumUtilities.Parse<DeviceAccessState>(value);
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x00108BEC File Offset: 0x00106DEC
		// (set) Token: 0x06004F93 RID: 20371 RVA: 0x00108BF4 File Offset: 0x00106DF4
		[IgnoreDataMember]
		public DeviceAccessStateReason DeviceAccessStateReason
		{
			get
			{
				return this.deviceAccessStateReason;
			}
			set
			{
				this.deviceAccessStateReason = value;
				base.TrackPropertyChanged("DeviceAccessStateReason");
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x00108C08 File Offset: 0x00106E08
		// (set) Token: 0x06004F95 RID: 20373 RVA: 0x00108C15 File Offset: 0x00106E15
		[DataMember(Name = "DeviceAccessStateReason", IsRequired = false, EmitDefaultValue = false)]
		public string DeviceAccessStateReasonString
		{
			get
			{
				return EnumUtilities.ToString<DeviceAccessStateReason>(this.DeviceAccessStateReason);
			}
			set
			{
				this.DeviceAccessStateReason = EnumUtilities.Parse<DeviceAccessStateReason>(value);
				base.TrackPropertyChanged("DeviceAccessStateReasonString");
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06004F96 RID: 20374 RVA: 0x00108C2E File Offset: 0x00106E2E
		// (set) Token: 0x06004F97 RID: 20375 RVA: 0x00108C36 File Offset: 0x00106E36
		[DataMember]
		public Identity DeviceAccessControlRule
		{
			get
			{
				return this.deviceAccessControlRule;
			}
			set
			{
				this.deviceAccessControlRule = value;
				base.TrackPropertyChanged("DeviceAccessControlRule");
			}
		}

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x06004F98 RID: 20376 RVA: 0x00108C4A File Offset: 0x00106E4A
		// (set) Token: 0x06004F99 RID: 20377 RVA: 0x00108C52 File Offset: 0x00106E52
		[IgnoreDataMember]
		public MobileClientType ClientType
		{
			get
			{
				return this.clientType;
			}
			set
			{
				this.clientType = value;
				base.TrackPropertyChanged("ClientType");
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x00108C66 File Offset: 0x00106E66
		// (set) Token: 0x06004F9B RID: 20379 RVA: 0x00108C73 File Offset: 0x00106E73
		[DataMember(Name = "ClientType", IsRequired = false, EmitDefaultValue = false)]
		public string ClientTypeString
		{
			get
			{
				return EnumUtilities.ToString<MobileClientType>(this.ClientType);
			}
			set
			{
				this.ClientType = EnumUtilities.Parse<MobileClientType>(value);
				base.TrackPropertyChanged("ClientTypeString");
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06004F9C RID: 20380 RVA: 0x00108C8C File Offset: 0x00106E8C
		// (set) Token: 0x06004F9D RID: 20381 RVA: 0x00108C94 File Offset: 0x00106E94
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string DeviceWipeRequestTime
		{
			get
			{
				return this.deviceWipeRequestTime;
			}
			set
			{
				this.deviceWipeRequestTime = value;
				base.TrackPropertyChanged("DeviceWipeRequestTime");
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06004F9E RID: 20382 RVA: 0x00108CA8 File Offset: 0x00106EA8
		// (set) Token: 0x06004F9F RID: 20383 RVA: 0x00108CB0 File Offset: 0x00106EB0
		[DateTimeString]
		[DataMember(EmitDefaultValue = false)]
		public string DeviceWipeAckTime
		{
			get
			{
				return this.deviceWipeAckTime;
			}
			set
			{
				this.deviceWipeAckTime = value;
				base.TrackPropertyChanged("DeviceWipeAckTime");
			}
		}

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06004FA0 RID: 20384 RVA: 0x00108CC4 File Offset: 0x00106EC4
		// (set) Token: 0x06004FA1 RID: 20385 RVA: 0x00108CCC File Offset: 0x00106ECC
		[DataMember]
		public uint? LastPingHeartBeat
		{
			get
			{
				return this.lastPingHeartBeat;
			}
			set
			{
				this.lastPingHeartBeat = value;
				base.TrackPropertyChanged("LastPingHeartBeat");
			}
		}

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x00108CE0 File Offset: 0x00106EE0
		// (set) Token: 0x06004FA3 RID: 20387 RVA: 0x00108CE8 File Offset: 0x00106EE8
		[DataMember]
		public string RecoveryPassword
		{
			get
			{
				return this.recoveryPassword;
			}
			set
			{
				this.recoveryPassword = value;
				base.TrackPropertyChanged("RecoveryPassword");
			}
		}

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x06004FA4 RID: 20388 RVA: 0x00108CFC File Offset: 0x00106EFC
		// (set) Token: 0x06004FA5 RID: 20389 RVA: 0x00108D04 File Offset: 0x00106F04
		[DataMember]
		public Identity Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
				base.TrackPropertyChanged("Identity");
			}
		}

		// Token: 0x170012F6 RID: 4854
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x00108D18 File Offset: 0x00106F18
		// (set) Token: 0x06004FA7 RID: 20391 RVA: 0x00108D20 File Offset: 0x00106F20
		[DataMember]
		public bool IsRemoteWipeSupported
		{
			get
			{
				return this.isRemoteWipeSupported;
			}
			set
			{
				this.isRemoteWipeSupported = value;
				base.TrackPropertyChanged("IsRemoteWipeSupported");
			}
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06004FA8 RID: 20392 RVA: 0x00108D34 File Offset: 0x00106F34
		// (set) Token: 0x06004FA9 RID: 20393 RVA: 0x00108D3C File Offset: 0x00106F3C
		[IgnoreDataMember]
		public DeviceRemoteWipeStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
				base.TrackPropertyChanged("Status");
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06004FAA RID: 20394 RVA: 0x00108D50 File Offset: 0x00106F50
		// (set) Token: 0x06004FAB RID: 20395 RVA: 0x00108D5D File Offset: 0x00106F5D
		[DataMember(Name = "Status", IsRequired = false, EmitDefaultValue = false)]
		public string DeviceRemoteWipeStatusString
		{
			get
			{
				return EnumUtilities.ToString<DeviceRemoteWipeStatus>(this.Status);
			}
			set
			{
				this.Status = EnumUtilities.Parse<DeviceRemoteWipeStatus>(value);
				base.TrackPropertyChanged("DeviceRemoteWipeStatusString");
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x00108D76 File Offset: 0x00106F76
		// (set) Token: 0x06004FAD RID: 20397 RVA: 0x00108D7E File Offset: 0x00106F7E
		[DataMember]
		public string StatusNote
		{
			get
			{
				return this.statusNote;
			}
			set
			{
				this.statusNote = value;
				base.TrackPropertyChanged("StatusNote");
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x00108D92 File Offset: 0x00106F92
		// (set) Token: 0x06004FAF RID: 20399 RVA: 0x00108D9A File Offset: 0x00106F9A
		[DataMember]
		public Identity DevicePolicyApplied
		{
			get
			{
				return this.devicePolicyApplied;
			}
			set
			{
				this.devicePolicyApplied = value;
				base.TrackPropertyChanged("DevicePolicyApplied");
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x00108DAE File Offset: 0x00106FAE
		// (set) Token: 0x06004FB1 RID: 20401 RVA: 0x00108DB6 File Offset: 0x00106FB6
		[DataMember]
		public int NumberOfFoldersSynced
		{
			get
			{
				return this.numberOfFoldersSynced;
			}
			set
			{
				this.numberOfFoldersSynced = value;
				base.TrackPropertyChanged("NumberOfFoldersSynced");
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x00108DCA File Offset: 0x00106FCA
		// (set) Token: 0x06004FB3 RID: 20403 RVA: 0x00108DD2 File Offset: 0x00106FD2
		[IgnoreDataMember]
		public DevicePolicyApplicationStatus DevicePolicyApplicationStatus
		{
			get
			{
				return this.devicePolicyApplicationStatus;
			}
			set
			{
				this.devicePolicyApplicationStatus = value;
				base.TrackPropertyChanged("DevicePolicyApplicationStatus");
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x00108DE6 File Offset: 0x00106FE6
		// (set) Token: 0x06004FB5 RID: 20405 RVA: 0x00108DF3 File Offset: 0x00106FF3
		[DataMember(Name = "DevicePolicyApplicationStatus", IsRequired = false, EmitDefaultValue = false)]
		public string DevicePolicyApplicationStatusString
		{
			get
			{
				return EnumUtilities.ToString<DevicePolicyApplicationStatus>(this.DevicePolicyApplicationStatus);
			}
			set
			{
				this.DevicePolicyApplicationStatus = EnumUtilities.Parse<DevicePolicyApplicationStatus>(value);
				base.TrackPropertyChanged("DevicePolicyApplicationStatusString");
			}
		}

		// Token: 0x04002C69 RID: 11369
		private string firstSyncTime;

		// Token: 0x04002C6A RID: 11370
		private string lastPolicyUpdateTime;

		// Token: 0x04002C6B RID: 11371
		private string lastSyncAttemptTime;

		// Token: 0x04002C6C RID: 11372
		private string lastSuccessSync;

		// Token: 0x04002C6D RID: 11373
		private string deviceType;

		// Token: 0x04002C6E RID: 11374
		private string deviceId;

		// Token: 0x04002C6F RID: 11375
		private string deviceUserAgent;

		// Token: 0x04002C70 RID: 11376
		private string deviceWipeSentTime;

		// Token: 0x04002C71 RID: 11377
		private string deviceModel;

		// Token: 0x04002C72 RID: 11378
		private string deviceImei;

		// Token: 0x04002C73 RID: 11379
		private string deviceFriendlyName;

		// Token: 0x04002C74 RID: 11380
		private string deviceOS;

		// Token: 0x04002C75 RID: 11381
		private string deviceOSLanguage;

		// Token: 0x04002C76 RID: 11382
		private string devicePhoneNumber;

		// Token: 0x04002C77 RID: 11383
		private string deviceMobileOperator;

		// Token: 0x04002C78 RID: 11384
		private string clientVersion;

		// Token: 0x04002C79 RID: 11385
		private DeviceAccessState deviceAccessState;

		// Token: 0x04002C7A RID: 11386
		private DeviceAccessStateReason deviceAccessStateReason;

		// Token: 0x04002C7B RID: 11387
		private Identity deviceAccessControlRule;

		// Token: 0x04002C7C RID: 11388
		private MobileClientType clientType;

		// Token: 0x04002C7D RID: 11389
		private string deviceWipeRequestTime;

		// Token: 0x04002C7E RID: 11390
		private string deviceWipeAckTime;

		// Token: 0x04002C7F RID: 11391
		private uint? lastPingHeartBeat;

		// Token: 0x04002C80 RID: 11392
		private string recoveryPassword;

		// Token: 0x04002C81 RID: 11393
		private Identity identity;

		// Token: 0x04002C82 RID: 11394
		private bool isRemoteWipeSupported;

		// Token: 0x04002C83 RID: 11395
		private DeviceRemoteWipeStatus status;

		// Token: 0x04002C84 RID: 11396
		private string statusNote;

		// Token: 0x04002C85 RID: 11397
		private Identity devicePolicyApplied;

		// Token: 0x04002C86 RID: 11398
		private DevicePolicyApplicationStatus devicePolicyApplicationStatus;

		// Token: 0x04002C87 RID: 11399
		private int numberOfFoldersSynced;
	}
}
