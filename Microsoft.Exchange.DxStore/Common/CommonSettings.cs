using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000034 RID: 52
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	public class CommonSettings
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000355C File Offset: 0x0000175C
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00003564 File Offset: 0x00001764
		[DataMember]
		public string InstanceProcessName { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000356D File Offset: 0x0000176D
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003575 File Offset: 0x00001775
		[DataMember]
		public int TruncationLimit { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000357E File Offset: 0x0000177E
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00003586 File Offset: 0x00001786
		[DataMember]
		public int TruncationPaddingLength { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000358F File Offset: 0x0000178F
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00003597 File Offset: 0x00001797
		[DataMember]
		public int AccessEndpointPortNumber { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000035A0 File Offset: 0x000017A0
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000035A8 File Offset: 0x000017A8
		[DataMember]
		public string AccessEndpointProtocolName { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000035B1 File Offset: 0x000017B1
		// (set) Token: 0x06000170 RID: 368 RVA: 0x000035B9 File Offset: 0x000017B9
		[DataMember]
		public int InstanceEndpointPortNumber { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000035C2 File Offset: 0x000017C2
		// (set) Token: 0x06000172 RID: 370 RVA: 0x000035CA File Offset: 0x000017CA
		[DataMember]
		public string InstanceEndpointProtocolName { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000035D3 File Offset: 0x000017D3
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000035DB File Offset: 0x000017DB
		[DataMember]
		public int MaxEntriesToKeep { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000035E4 File Offset: 0x000017E4
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000035EC File Offset: 0x000017EC
		[DataMember]
		public int MaximumAllowedInstanceNumberLag { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000035F5 File Offset: 0x000017F5
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000035FD File Offset: 0x000017FD
		[DataMember]
		public int DefaultHealthCheckRequiredNodePercent { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003606 File Offset: 0x00001806
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000360E File Offset: 0x0000180E
		[DataMember]
		public int MaxAllowedLagToCatchup { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00003617 File Offset: 0x00001817
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000361F File Offset: 0x0000181F
		[DataMember]
		public string DefaultSnapshotFileName { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003628 File Offset: 0x00001828
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00003630 File Offset: 0x00001830
		[DataMember]
		public bool IsAllowDynamicReconfig { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00003639 File Offset: 0x00001839
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00003641 File Offset: 0x00001841
		[DataMember]
		public bool IsAppendOnlyMembership { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000364A File Offset: 0x0000184A
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003652 File Offset: 0x00001852
		[DataMember]
		public bool IsKillInstanceProcessWhenParentDies { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000365B File Offset: 0x0000185B
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00003663 File Offset: 0x00001863
		[DataMember]
		public WcfTimeout StoreAccessWcfTimeout { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000366C File Offset: 0x0000186C
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00003674 File Offset: 0x00001874
		[DataMember]
		public int StoreAccessHttpTimeoutInMSec { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000367D File Offset: 0x0000187D
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00003685 File Offset: 0x00001885
		[DataMember]
		public WcfTimeout StoreInstanceWcfTimeout { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000368E File Offset: 0x0000188E
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00003696 File Offset: 0x00001896
		[DataMember]
		public TimeSpan TruncationPeriodicCheckInterval { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000369F File Offset: 0x0000189F
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000036A7 File Offset: 0x000018A7
		[DataMember]
		public TimeSpan InstanceHealthCheckPeriodicInterval { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000036B0 File Offset: 0x000018B0
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000036B8 File Offset: 0x000018B8
		[DataMember]
		public TimeSpan DurationToWaitBeforeRestart { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000036C1 File Offset: 0x000018C1
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000036C9 File Offset: 0x000018C9
		[DataMember]
		public TimeSpan StateMachineStopTimeout { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000036D2 File Offset: 0x000018D2
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000036DA File Offset: 0x000018DA
		[DataMember]
		public TimeSpan LeaderPromotionTimeout { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000036E3 File Offset: 0x000018E3
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000036EB File Offset: 0x000018EB
		[DataMember]
		public TimeSpan PaxosCommandExecutionTimeout { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000036F4 File Offset: 0x000018F4
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000036FC File Offset: 0x000018FC
		[DataMember]
		public TimeSpan GroupHealthCheckDuration { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003705 File Offset: 0x00001905
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000370D File Offset: 0x0000190D
		[DataMember]
		public TimeSpan GroupHealthCheckAggressiveDuration { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003716 File Offset: 0x00001916
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000371E File Offset: 0x0000191E
		[DataMember]
		public TimeSpan GroupStatusWaitTimeout { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003727 File Offset: 0x00001927
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000372F File Offset: 0x0000192F
		[DataMember]
		public TimeSpan MemberReconfigureTimeout { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003738 File Offset: 0x00001938
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00003740 File Offset: 0x00001940
		[DataMember]
		public TimeSpan PaxosUpdateTimeout { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00003749 File Offset: 0x00001949
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00003751 File Offset: 0x00001951
		[DataMember]
		public TimeSpan SnapshotUpdateInterval { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000375A File Offset: 0x0000195A
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00003762 File Offset: 0x00001962
		[DataMember]
		public TimeSpan PeriodicExceptionLoggingDuration { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000376B File Offset: 0x0000196B
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00003773 File Offset: 0x00001973
		[DataMember]
		public TimeSpan PeriodicTimeoutLoggingDuration { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000377C File Offset: 0x0000197C
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00003784 File Offset: 0x00001984
		[DataMember]
		public TimeSpan ServiceHostCloseTimeout { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00003795 File Offset: 0x00001995
		[DataMember]
		public TimeSpan InstanceStartSilenceDuration { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000379E File Offset: 0x0000199E
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000037A6 File Offset: 0x000019A6
		[DataMember]
		public int InstanceStartHoldupDurationMaxAllowedStarts { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000037AF File Offset: 0x000019AF
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000037B7 File Offset: 0x000019B7
		[DataMember]
		public TimeSpan InstanceStartHoldUpDuration { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000037C0 File Offset: 0x000019C0
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000037C8 File Offset: 0x000019C8
		[DataMember]
		public int InstanceMemoryCommitSizeLimitInMb { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000037D1 File Offset: 0x000019D1
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000037D9 File Offset: 0x000019D9
		[DataMember]
		public int AdditionalLogOptionsAsInt { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000037E2 File Offset: 0x000019E2
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000037EA File Offset: 0x000019EA
		[DataMember]
		public bool IsUseHttpTransportForInstanceCommunication { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000037F3 File Offset: 0x000019F3
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000037FB File Offset: 0x000019FB
		[DataMember]
		public bool IsUseHttpTransportForClientCommunication { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00003804 File Offset: 0x00001A04
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000380C File Offset: 0x00001A0C
		[DataMember]
		public bool IsUseBinarySerializerForClientCommunication { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00003815 File Offset: 0x00001A15
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000381D File Offset: 0x00001A1D
		[DataMember]
		public bool IsUseEncryption { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00003826 File Offset: 0x00001A26
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000382E File Offset: 0x00001A2E
		[DataMember]
		public TimeSpan StartupDelay { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00003837 File Offset: 0x00001A37
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000383F File Offset: 0x00001A3F
		[IgnoreDataMember]
		public LogOptions AdditionalLogOptions
		{
			get
			{
				return (LogOptions)this.AdditionalLogOptionsAsInt;
			}
			set
			{
				this.AdditionalLogOptionsAsInt = (int)value;
			}
		}

		// Token: 0x02000035 RID: 53
		public static class PropertyNames
		{
			// Token: 0x040000E9 RID: 233
			public const string AccessEndpointPortNumber = "AccessEndpointPortNumber";

			// Token: 0x040000EA RID: 234
			public const string AccessEndpointProtocolName = "AccessEndpointProtocolName";

			// Token: 0x040000EB RID: 235
			public const string AdditionalLogOptions = "AdditionalLogOptions";

			// Token: 0x040000EC RID: 236
			public const string DefaultHealthCheckRequiredNodePercent = "DefaultHealthCheckRequiredNodePercent";

			// Token: 0x040000ED RID: 237
			public const string DefaultSnapshotFileName = "DefaultSnapshotFileName";

			// Token: 0x040000EE RID: 238
			public const string DurationToWaitBeforeRestartInMSec = "DurationToWaitBeforeRestartInMSec";

			// Token: 0x040000EF RID: 239
			public const string GroupHealthCheckDurationInMSec = "GroupHealthCheckDurationInMSec";

			// Token: 0x040000F0 RID: 240
			public const string GroupHealthCheckAggressiveDurationInMSec = "GroupHealthCheckAggressiveDurationInMSec";

			// Token: 0x040000F1 RID: 241
			public const string GroupStatusWaitTimeoutInMSec = "GroupStatusWaitTimeoutInMSec";

			// Token: 0x040000F2 RID: 242
			public const string InstanceEndpointPortNumber = "InstanceEndpointPortNumber";

			// Token: 0x040000F3 RID: 243
			public const string InstanceEndpointProtocolName = "InstanceEndpointProtocolName";

			// Token: 0x040000F4 RID: 244
			public const string InstanceHealthCheckPeriodicIntervalInMSec = "InstanceHealthCheckPeriodicIntervalInMSec";

			// Token: 0x040000F5 RID: 245
			public const string InstanceProcessName = "InstanceProcessName";

			// Token: 0x040000F6 RID: 246
			public const string IsAllowDynamicReconfig = "IsAllowDynamicReconfig";

			// Token: 0x040000F7 RID: 247
			public const string IsAppendOnlyMembership = "IsAppendOnlyMembership";

			// Token: 0x040000F8 RID: 248
			public const string IsKillInstanceProcessWhenParentDies = "IsKillInstanceProcessWhenParentDies";

			// Token: 0x040000F9 RID: 249
			public const string LeaderPromotionTimeoutInMSec = "LeaderPromotionTimeoutInMSec";

			// Token: 0x040000FA RID: 250
			public const string MaxAllowedLagToCatchup = "MaxAllowedLagToCatchup";

			// Token: 0x040000FB RID: 251
			public const string MaxEntriesToKeep = "MaxEntriesToKeep";

			// Token: 0x040000FC RID: 252
			public const string MaximumAllowedInstanceNumberLag = "MaximumAllowedInstanceNumberLag";

			// Token: 0x040000FD RID: 253
			public const string MemberReconfigureTimeoutInMSec = "MemberReconfigureTimeoutInMSec";

			// Token: 0x040000FE RID: 254
			public const string PaxosCommandExecutionTimeoutInMSec = "PaxosCommandExecutionTimeoutInMSec";

			// Token: 0x040000FF RID: 255
			public const string PaxosUpdateTimeoutInMSec = "PaxosUpdateTimeoutInMSec";

			// Token: 0x04000100 RID: 256
			public const string PeriodicExceptionLoggingDurationInMSec = "PeriodicExceptionLoggingDurationInMSec";

			// Token: 0x04000101 RID: 257
			public const string PeriodicTimeoutLoggingDurationInMSec = "PeriodicTimeoutLoggingDurationInMSec";

			// Token: 0x04000102 RID: 258
			public const string ServiceHostCloseTimeoutInMSec = "ServiceHostCloseTimeoutInMSec";

			// Token: 0x04000103 RID: 259
			public const string SnapshotUpdateIntervalInMSec = "SnapshotUpdateIntervalInMSec";

			// Token: 0x04000104 RID: 260
			public const string StateMachineStopTimeoutInMSec = "StateMachineStopTimeoutInMSec";

			// Token: 0x04000105 RID: 261
			public const string StoreAccessWcfTimeout = "StoreAccessWcfTimeout";

			// Token: 0x04000106 RID: 262
			public const string StoreInstanceWcfTimeout = "StoreInstanceWcfTimeout";

			// Token: 0x04000107 RID: 263
			public const string StoreAccessHttpTimeoutInMSec = "StoreAccessHttpTimeoutInMSec";

			// Token: 0x04000108 RID: 264
			public const string TruncationLimit = "TruncationLimit";

			// Token: 0x04000109 RID: 265
			public const string TruncationPaddingLength = "TruncationPaddingLength";

			// Token: 0x0400010A RID: 266
			public const string TruncationPeriodicCheckIntervalInMSec = "TruncationPeriodicCheckIntervalInMSec";

			// Token: 0x0400010B RID: 267
			public const string InstanceStartSilenceDurationInMSec = "InstanceStartSilenceDurationInMSec";

			// Token: 0x0400010C RID: 268
			public const string InstanceStartHoldupDurationMaxAllowedStarts = "InstanceStartHoldupDurationMaxAllowedStarts";

			// Token: 0x0400010D RID: 269
			public const string InstanceStartHoldUpDurationInMSec = "InstanceStartHoldUpDurationInMSec";

			// Token: 0x0400010E RID: 270
			public const string InstanceMemoryCommitSizeLimitInMb = "InstanceMemoryCommitSizeLimitInMb";

			// Token: 0x0400010F RID: 271
			public const string IsUseHttpTransportForInstanceCommunication = "IsUseHttpTransportForInstanceCommunication";

			// Token: 0x04000110 RID: 272
			public const string IsUseHttpTransportForClientCommunication = "IsUseHttpTransportForClientCommunication";

			// Token: 0x04000111 RID: 273
			public const string IsUseBinarySerializerForClientCommunication = "IsUseBinarySerializerForClientCommunication";

			// Token: 0x04000112 RID: 274
			public const string IsUseEncryption = "IsUseEncryption";

			// Token: 0x04000113 RID: 275
			public const string StartupDelayInMSec = "StartupDelayInMSec";
		}
	}
}
