using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000AF RID: 175
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class AggregationSubscription : ISyncWorkerData
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00017176 File Offset: 0x00015376
		protected AggregationSubscription() : this(CommonLoggingHelper.SyncLogSession)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00017184 File Offset: 0x00015384
		protected AggregationSubscription(SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.syncLogSession = syncLogSession;
			this.LastSuccessfulSyncTime = null;
			this.LastSyncTime = null;
			this.lastSyncNowRequestTime = null;
			this.Status = AggregationStatus.InProgress;
			this.subscriptionIdentity = new AggregationSubscriptionIdentity();
			this.subscriptionIdentity.SubscriptionId = Guid.NewGuid();
			this.foldersToExclude = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
			this.subscriptionEvents = SubscriptionEvents.None;
			this.adjustedLastSuccessfulSyncTime = DateTime.UtcNow;
			this.version = AggregationSubscription.CurrentSupportedVersion;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00017259 File Offset: 0x00015459
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00017260 File Offset: 0x00015460
		public static long CurrentSupportedVersion
		{
			get
			{
				return AggregationSubscription.currentSupportedVersion;
			}
			set
			{
				AggregationSubscription.currentSupportedVersion = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00017268 File Offset: 0x00015468
		public bool IsNew
		{
			get
			{
				return this.SubscriptionMessageId == null;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00017273 File Offset: 0x00015473
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001727B File Offset: 0x0001547B
		public string Diagnostics
		{
			get
			{
				return this.diagnostics;
			}
			set
			{
				this.diagnostics = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00017284 File Offset: 0x00015484
		public AggregationSubscriptionIdentity SubscriptionIdentity
		{
			get
			{
				return this.subscriptionIdentity;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001728C File Offset: 0x0001548C
		public Guid SubscriptionGuid
		{
			get
			{
				return this.subscriptionIdentity.SubscriptionId;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00017299 File Offset: 0x00015499
		public string UserLegacyDN
		{
			get
			{
				return this.subscriptionIdentity.LegacyDN;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000172A6 File Offset: 0x000154A6
		public string PrimaryMailboxUserLegacyDN
		{
			get
			{
				return this.subscriptionIdentity.PrimaryMailboxLegacyDN;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x000172B3 File Offset: 0x000154B3
		public ADObjectId AdUserId
		{
			get
			{
				return this.subscriptionIdentity.AdUserId;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000172C0 File Offset: 0x000154C0
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x000172C8 File Offset: 0x000154C8
		public AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
			set
			{
				this.subscriptionType = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000172D1 File Offset: 0x000154D1
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x000172D9 File Offset: 0x000154D9
		public long Version
		{
			get
			{
				return this.version;
			}
			protected set
			{
				this.version = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000172E2 File Offset: 0x000154E2
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000172EA File Offset: 0x000154EA
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000172F2 File Offset: 0x000154F2
		public AggregationType AggregationType
		{
			get
			{
				return this.aggregationType;
			}
			set
			{
				if (AggregationType.Mirrored == value)
				{
					throw new SyncPropertyValidationException(AggregationSubscriptionMessageSchema.SharingAggregationType.ToString(), value.ToString(), new InvalidOperationException("Mirrored subscriptions are not supported"));
				}
				this.aggregationType = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00017325 File Offset: 0x00015525
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001732D File Offset: 0x0001552D
		public SyncPhase SyncPhase
		{
			get
			{
				return this.syncPhase;
			}
			set
			{
				if (value < this.syncPhase)
				{
					throw new InvalidOperationException("Sync phase can only move forward, not backwards");
				}
				this.syncPhase = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001734A File Offset: 0x0001554A
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00017352 File Offset: 0x00015552
		public SubscriptionCreationType CreationType
		{
			get
			{
				return this.creationType;
			}
			set
			{
				this.creationType = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001735B File Offset: 0x0001555B
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00017363 File Offset: 0x00015563
		public string UserExchangeMailboxDisplayName
		{
			get
			{
				return this.userExchangeMailboxDisplayName;
			}
			set
			{
				this.userExchangeMailboxDisplayName = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001736C File Offset: 0x0001556C
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00017374 File Offset: 0x00015574
		public string UserExchangeMailboxSmtpAddress
		{
			get
			{
				return this.userExchangeMailboxSmtpAddress;
			}
			set
			{
				this.userExchangeMailboxSmtpAddress = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001737D File Offset: 0x0001557D
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x00017385 File Offset: 0x00015585
		public int SubscriptionGeneralConfig
		{
			get
			{
				return this.subscriptionGeneralConfig;
			}
			set
			{
				this.subscriptionGeneralConfig = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001738E File Offset: 0x0001558E
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x00017396 File Offset: 0x00015596
		public int SubscriptionProtocolVersion
		{
			get
			{
				return this.subscriptionProtocolVersion;
			}
			set
			{
				this.subscriptionProtocolVersion = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001739F File Offset: 0x0001559F
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x000173A7 File Offset: 0x000155A7
		public string SubscriptionProtocolName
		{
			get
			{
				return this.subscriptionProtocolName;
			}
			set
			{
				this.subscriptionProtocolName = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000173B0 File Offset: 0x000155B0
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x000173B8 File Offset: 0x000155B8
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x000173C1 File Offset: 0x000155C1
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x000173C9 File Offset: 0x000155C9
		public AggregationStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x000173D2 File Offset: 0x000155D2
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x000173DA File Offset: 0x000155DA
		public DetailedAggregationStatus DetailedAggregationStatus
		{
			get
			{
				return this.detailedAggregationStatus;
			}
			set
			{
				if (!EnumValidator.IsValidValue<DetailedAggregationStatus>(value))
				{
					CommonLoggingHelper.SyncLogSession.ReportWatson(string.Format("Invalid DetailedAggregationStatus {0}", value));
					this.detailedAggregationStatus = DetailedAggregationStatus.None;
					return;
				}
				this.detailedAggregationStatus = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001740D File Offset: 0x0001560D
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x00017415 File Offset: 0x00015615
		public string PoisonCallstack
		{
			get
			{
				return this.poisonCallstack;
			}
			set
			{
				this.poisonCallstack = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001741E File Offset: 0x0001561E
		public DateTime LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00017426 File Offset: 0x00015626
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0001742E File Offset: 0x0001562E
		public DateTime? LastSyncTime
		{
			get
			{
				return this.lastSyncTime;
			}
			set
			{
				this.lastSyncTime = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00017437 File Offset: 0x00015637
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001743F File Offset: 0x0001563F
		public DateTime? LastSyncNowRequestTime
		{
			get
			{
				return this.lastSyncNowRequestTime;
			}
			set
			{
				this.lastSyncNowRequestTime = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00017448 File Offset: 0x00015648
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00017450 File Offset: 0x00015650
		public DateTime? LastSuccessfulSyncTime
		{
			get
			{
				return this.lastSuccessfulSyncTime;
			}
			set
			{
				this.lastSuccessfulSyncTime = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00017459 File Offset: 0x00015659
		public DateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00017461 File Offset: 0x00015661
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00017469 File Offset: 0x00015669
		public DateTime AdjustedLastSuccessfulSyncTime
		{
			get
			{
				return this.adjustedLastSuccessfulSyncTime;
			}
			set
			{
				this.adjustedLastSuccessfulSyncTime = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00017472 File Offset: 0x00015672
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0001747A File Offset: 0x0001567A
		public string OutageDetectionDiagnostics
		{
			get
			{
				return this.outageDetectionDiagnostics;
			}
			set
			{
				this.outageDetectionDiagnostics = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00017484 File Offset: 0x00015684
		public virtual int? EnumeratedItemsLimitPerConnection
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001749A File Offset: 0x0001569A
		public virtual bool SendAsCapable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001749D File Offset: 0x0001569D
		public string TypeFullName
		{
			get
			{
				if (this.instanceTypeFullName == null)
				{
					this.instanceTypeFullName = base.GetType().FullName;
				}
				return this.instanceTypeFullName;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000174BE File Offset: 0x000156BE
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x000174C6 File Offset: 0x000156C6
		public StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
			set
			{
				this.subscriptionMessageId = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000174CF File Offset: 0x000156CF
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x000174D7 File Offset: 0x000156D7
		public SubscriptionEvents SubscriptionEvents
		{
			get
			{
				return this.subscriptionEvents;
			}
			set
			{
				this.subscriptionEvents = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000174E0 File Offset: 0x000156E0
		public long ItemsSynced
		{
			get
			{
				return this.itemsSynced;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000174E8 File Offset: 0x000156E8
		public long ItemsSkipped
		{
			get
			{
				return this.itemsSkipped;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000174F0 File Offset: 0x000156F0
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001751F File Offset: 0x0001571F
		public long? TotalItemsInSourceMailbox
		{
			get
			{
				if (this.totalItemsInSourceMailbox == SyncUtilities.DataNotAvailable)
				{
					return null;
				}
				return new long?(this.totalItemsInSourceMailbox);
			}
			set
			{
				if (value != null)
				{
					this.totalItemsInSourceMailbox = value.Value;
					return;
				}
				this.totalItemsInSourceMailbox = SyncUtilities.DataNotAvailable;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00017544 File Offset: 0x00015744
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00017573 File Offset: 0x00015773
		public long? TotalSizeOfSourceMailbox
		{
			get
			{
				if (this.totalSizeOfSourceMailbox == SyncUtilities.DataNotAvailable)
				{
					return null;
				}
				return new long?(this.totalSizeOfSourceMailbox);
			}
			set
			{
				if (value != null)
				{
					this.totalSizeOfSourceMailbox = value.Value;
					return;
				}
				this.totalSizeOfSourceMailbox = SyncUtilities.DataNotAvailable;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00017597 File Offset: 0x00015797
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001759F File Offset: 0x0001579F
		public byte[] InstanceKey
		{
			get
			{
				return this.instanceKey;
			}
			set
			{
				this.instanceKey = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004B4 RID: 1204
		public abstract bool IsMirrored { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000175A8 File Offset: 0x000157A8
		public bool IsAggregation
		{
			get
			{
				return this.AggregationType == AggregationType.Aggregation;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000175B3 File Offset: 0x000157B3
		public bool IsMigration
		{
			get
			{
				return this.AggregationType == AggregationType.Migration;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000175BF File Offset: 0x000157BF
		public bool IsPartnerProtocol
		{
			get
			{
				return this.SubscriptionType == AggregationSubscriptionType.DeltaSyncMail;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000175CA File Offset: 0x000157CA
		public bool IsInitialSyncDone
		{
			get
			{
				return this.syncPhase != SyncPhase.Initial;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000175D8 File Offset: 0x000157D8
		public bool WasInitialSyncDone
		{
			get
			{
				return this.wasInitialSyncDone;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000175E0 File Offset: 0x000157E0
		public bool Inactive
		{
			get
			{
				return this.status == AggregationStatus.Disabled || this.status == AggregationStatus.Poisonous;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000175F6 File Offset: 0x000157F6
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000175FE File Offset: 0x000157FE
		public bool InitialSyncInRecoveryMode
		{
			get
			{
				return this.initialSyncInRecoveryMode;
			}
			internal set
			{
				this.initialSyncInRecoveryMode = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00017607 File Offset: 0x00015807
		public string FoldersToExclude
		{
			get
			{
				return AggregationSubscription.SerializeFoldersToExclude(this.foldersToExclude);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00017614 File Offset: 0x00015814
		public virtual string IncomingServerName
		{
			get
			{
				return this.SubscriptionType.ToString();
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00017626 File Offset: 0x00015826
		public virtual int IncomingServerPort
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00017629 File Offset: 0x00015829
		public virtual string AuthenticationType
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00017630 File Offset: 0x00015830
		public virtual string EncryptionType
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00017637 File Offset: 0x00015837
		public virtual SmtpAddress Email
		{
			get
			{
				return SmtpAddress.Empty;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001763E File Offset: 0x0001583E
		public virtual string Domain
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004C4 RID: 1220
		public abstract FolderSupport FolderSupport { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004C5 RID: 1221
		public abstract ItemSupport ItemSupport { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004C6 RID: 1222
		public abstract SyncQuirks SyncQuirks { get; }

		// Token: 0x060004C7 RID: 1223 RVA: 0x00017648 File Offset: 0x00015848
		public static AggregationSubscriptionType GetSubscriptionKind(MessageItem message)
		{
			string className = message.ClassName;
			return AggregationSubscription.GetSubscriptionKind(className);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00017664 File Offset: 0x00015864
		public static AggregationSubscriptionType GetSubscriptionKind(string messageClass)
		{
			if (messageClass.Equals("IPM.Aggregation.Pop", StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.Pop;
			}
			if (messageClass.Equals("IPM.Aggregation.DeltaSync", StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.DeltaSyncMail;
			}
			if (messageClass.Equals("IPM.Aggregation.IMAP", StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.IMAP;
			}
			if (messageClass.Equals("IPM.Aggregation.Facebook", StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.Facebook;
			}
			if (messageClass.Equals("IPM.Aggregation.LinkedIn", StringComparison.OrdinalIgnoreCase))
			{
				return AggregationSubscriptionType.LinkedIn;
			}
			return AggregationSubscriptionType.Unknown;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000176C8 File Offset: 0x000158C8
		public override bool Equals(object obj)
		{
			AggregationSubscription otherSubscription = obj as AggregationSubscription;
			return this.Equals(otherSubscription);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000176E3 File Offset: 0x000158E3
		public bool Equals(AggregationSubscription otherSubscription)
		{
			return otherSubscription != null && otherSubscription.SubscriptionGuid == this.SubscriptionGuid;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000176FC File Offset: 0x000158FC
		public override int GetHashCode()
		{
			return this.SubscriptionGuid.GetHashCode();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00017720 File Offset: 0x00015920
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} : {1}", new object[]
			{
				this.SubscriptionType.ToString(),
				this.UserExchangeMailboxDisplayName
			});
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00017760 File Offset: 0x00015960
		public void LoadSubscription(MessageItem message, ADObjectId adUserId, string legacyDN)
		{
			SyncUtilities.ThrowIfArgumentNull("message", message);
			this.isValid = false;
			bool flag = false;
			bool flag2 = false;
			if (!this.TryLoadSubscriptionVersion(message, out flag2))
			{
				flag = true;
			}
			this.baseLoadMinimumInfoCalled = false;
			this.subscriptionIdentity.AdUserId = adUserId;
			this.subscriptionIdentity.LegacyDN = legacyDN;
			this.subscriptionIdentity.PrimaryMailboxLegacyDN = legacyDN;
			bool flag3 = this.LoadMinimumInfo(message);
			if (!flag && flag3)
			{
				this.baseLoadPropertiesCalled = false;
				try
				{
					this.LoadProperties(message);
					this.isValid = true;
					return;
				}
				catch (SyncPropertyValidationException ex)
				{
					this.SetPropertyLoadError(ex.Property, ex.Value);
				}
			}
			if (!flag && flag2)
			{
				this.MarkSubscriptionInvalidVersion(message);
				return;
			}
			this.MarkSubscriptionCorrupted(message);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001781C File Offset: 0x00015A1C
		public void SetToMessageObject(MessageItem message)
		{
			SyncUtilities.ThrowIfArgumentNull("message", message);
			this.baseSetPropertiesToMessageObject = false;
			this.SetPropertiesToMessageObject(message);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00017838 File Offset: 0x00015A38
		public void SetFoldersToExclude(IEnumerable<string> foldersToExclude)
		{
			this.foldersToExclude.Clear();
			if (foldersToExclude != null)
			{
				foreach (string text in foldersToExclude)
				{
					if (!string.IsNullOrEmpty(text))
					{
						this.foldersToExclude.Add(text);
					}
				}
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001789C File Offset: 0x00015A9C
		public bool ShouldFolderBeExcluded(string folderName, char folderSeparator)
		{
			if (this.foldersToExclude.Contains(folderName))
			{
				return true;
			}
			foreach (string arg in this.foldersToExclude)
			{
				if (folderName.StartsWith(arg + folderSeparator, StringComparison.CurrentCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00017914 File Offset: 0x00015B14
		public void UpdateItemStatistics(long itemsSynced, long itemsSkipped)
		{
			SyncUtilities.ThrowIfArgumentLessThanZero("itemsSynced", itemsSynced);
			SyncUtilities.ThrowIfArgumentLessThanZero("itemsSkipped", itemsSkipped);
			this.itemsSynced += itemsSynced;
			this.itemsSkipped += itemsSkipped;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00017948 File Offset: 0x00015B48
		public void AppendOutageDetectionDiagnostics(string machineName, Guid databaseGuid, TimeSpan configuredOutageDetectionThreshold, TimeSpan observedOutageDuration)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("machineName", machineName);
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			this.outageDetectionDiagnostics += string.Format(CultureInfo.InvariantCulture, "Machine: {0} ,Date: {1}, Database: {2}, Threshold: {3}, Observed Duration: {4}{5}", new object[]
			{
				machineName,
				ExDateTime.UtcNow,
				databaseGuid,
				configuredOutageDetectionThreshold,
				observedOutageDuration,
				Environment.NewLine
			});
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000179C8 File Offset: 0x00015BC8
		internal static string SerializeFoldersToExclude(IEnumerable<string> foldersToExclude)
		{
			if (foldersToExclude == null)
			{
				throw new ArgumentException("FoldersToExclude cannot be null");
			}
			StringBuilder stringBuilder = new StringBuilder(265);
			foreach (string value in foldersToExclude)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(AggregationSubscription.FolderExclusionDelimiter);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00017A3C File Offset: 0x00015C3C
		internal static HashSet<string> DeserializeFoldersToExclude(string s)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (!string.IsNullOrEmpty(s))
			{
				string[] array = s.Split(new string[]
				{
					AggregationSubscription.FolderExclusionDelimiter
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string item in array)
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00017A94 File Offset: 0x00015C94
		internal static void DeserializeVersionAndSubscriptionType(BinaryReader reader, out long version, out AggregationSubscriptionType subscriptionType)
		{
			AggregationSubscription.SubscriptionDeserializer subscriptionDeserializer = new AggregationSubscription.SubscriptionDeserializer(reader);
			subscriptionDeserializer.DeserializeVersionAndSubscriptionType(out version, out subscriptionType);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00017AB0 File Offset: 0x00015CB0
		internal void Serialize(BinaryWriter writer)
		{
			AggregationSubscription.SubscriptionSerializer serializer = new AggregationSubscription.SubscriptionSerializer(writer);
			this.Serialize(serializer);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00017ACC File Offset: 0x00015CCC
		internal void Deserialize(BinaryReader reader)
		{
			AggregationSubscription.SubscriptionDeserializer deserializer = new AggregationSubscription.SubscriptionDeserializer(reader);
			this.Deserialize(deserializer);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00017AE7 File Offset: 0x00015CE7
		internal bool IsUpgradeRequired()
		{
			return this.isValid && AggregationSubscription.CurrentSupportedVersion > this.version;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017B00 File Offset: 0x00015D00
		internal void UpgradeSubscription()
		{
			if (!this.isValid)
			{
				throw new InvalidOperationException("Can't upgrade invalid subscription");
			}
			this.version = AggregationSubscription.CurrentSupportedVersion;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00017B20 File Offset: 0x00015D20
		internal void MarkSubscriptionInvalidVersion(MessageItem message)
		{
			this.diagnostics += string.Format(CultureInfo.InvariantCulture, "\r\nExpected version {0}, but Subscription has version {1}.", new object[]
			{
				AggregationSubscription.CurrentSupportedVersion,
				this.version
			});
			this.status = AggregationStatus.InvalidVersion;
			this.detailedAggregationStatus = DetailedAggregationStatus.None;
			message.OpenAsReadWrite();
			message[AggregationSubscriptionMessageSchema.SharingAggregationStatus] = AggregationStatus.InvalidVersion;
			this.isValid = false;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00017B9C File Offset: 0x00015D9C
		internal void MarkSubscriptionCorrupted(MessageItem message)
		{
			this.status = AggregationStatus.Poisonous;
			this.detailedAggregationStatus = DetailedAggregationStatus.Corrupted;
			try
			{
				message.OpenAsReadWrite();
				this.baseSetMinimumInfoToMessageObject = false;
				this.SetMinimumInfoToMessageObject(message);
				this.SaveMessage(message);
			}
			catch (LocalizedException ex)
			{
				this.syncLogSession.LogError((TSLID)41UL, "AggregationSubscription.MarkSubscriptionCorrupted: Failed to mark subscription as corrupted. Subscription ID {0}, Exception: {1}.", new object[]
				{
					message.Id.ObjectId,
					ex
				});
			}
			this.isValid = false;
		}

		// Token: 0x060004DC RID: 1244
		protected abstract void Serialize(AggregationSubscription.SubscriptionSerializer serializer);

		// Token: 0x060004DD RID: 1245
		protected abstract void Deserialize(AggregationSubscription.SubscriptionDeserializer deserializer);

		// Token: 0x060004DE RID: 1246 RVA: 0x00017C20 File Offset: 0x00015E20
		protected virtual void SetPropertiesToMessageObject(MessageItem message)
		{
			this.baseSetPropertiesToMessageObject = true;
			message[MessageItemSchema.SharingSubscriptionVersion] = AggregationSubscription.CurrentSupportedVersion;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionCreationType] = (int)this.CreationType;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionSyncPhase] = (int)this.SyncPhase;
			message[AggregationSubscriptionMessageSchema.SharingAggregationProtocolName] = this.SubscriptionProtocolName;
			message[AggregationSubscriptionMessageSchema.SharingAggregationProtocolVersion] = this.SubscriptionProtocolVersion;
			message[MessageItemSchema.SharingInstanceGuid] = this.SubscriptionGuid;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration] = this.SubscriptionGeneralConfig;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionName] = this.Name;
			message[MessageItemSchema.SharingDetailedStatus] = this.DetailedAggregationStatus;
			message[AggregationSubscriptionMessageSchema.SharingAggregationStatus] = this.Status;
			this.SetDiagnosticInfoToMessageObject(message);
			if (this.LastSuccessfulSyncTime == null)
			{
				message[AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime] = SyncUtilities.ExZeroTime;
			}
			else
			{
				message[AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime] = new ExDateTime(ExTimeZone.UtcTimeZone, this.LastSuccessfulSyncTime.Value.ToUniversalTime());
			}
			if (this.LastSyncTime == null)
			{
				message[MessageItemSchema.SharingLastSync] = SyncUtilities.ExZeroTime;
			}
			else
			{
				message[MessageItemSchema.SharingLastSync] = new ExDateTime(ExTimeZone.UtcTimeZone, this.LastSyncTime.Value.ToUniversalTime());
			}
			if (this.LastSyncNowRequestTime == null)
			{
				message[AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest] = SyncUtilities.ExZeroTime;
			}
			else
			{
				message[AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest] = new ExDateTime(ExTimeZone.UtcTimeZone, this.LastSyncNowRequestTime.Value.ToUniversalTime());
			}
			message[AggregationSubscriptionMessageSchema.SharingMigrationState] = ((this.IsInitialSyncDone ? 16 : 0) | (this.IsMigration ? 1 : 0));
			message[AggregationSubscriptionMessageSchema.SharingAggregationType] = (int)this.AggregationType;
			if (this.poisonCallstack != null && this.poisonCallstack.Length > 0)
			{
				Exception ex = null;
				string value = null;
				using (SecureString secureString = SyncUtilities.StringToSecureString(this.poisonCallstack))
				{
					if (this.TrySecureStringToEncryptedString(secureString, out value, out ex))
					{
						message[AggregationSubscriptionMessageSchema.SharingPoisonCallstack] = value;
					}
					else
					{
						this.syncLogSession.LogError((TSLID)39UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to Encrypt Callstack due to error: {0}, for Subscription: ID {1}, Name {2}, Protocol {3}.", new object[]
						{
							ex,
							this.SubscriptionGuid,
							this.Name,
							this.SubscriptionProtocolName
						});
					}
					goto IL_2DF;
				}
			}
			message[AggregationSubscriptionMessageSchema.SharingPoisonCallstack] = string.Empty;
			IL_2DF:
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionEvents] = this.subscriptionEvents;
			string value2 = AggregationSubscription.SerializeFoldersToExclude(this.foldersToExclude);
			if (!string.IsNullOrEmpty(value2))
			{
				message[AggregationSubscriptionMessageSchema.SharingSubscriptionExclusionFolders] = value2;
			}
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSynced] = this.itemsSynced;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSkipped] = this.itemsSkipped;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionTotalItemsInSourceMailbox] = this.totalItemsInSourceMailbox;
			message[AggregationSubscriptionMessageSchema.SharingSubscriptionTotalSizeOfSourceMailbox] = this.totalSizeOfSourceMailbox;
			message[AggregationSubscriptionMessageSchema.SharingAdjustedLastSuccessfulSyncTime] = new ExDateTime(ExTimeZone.UtcTimeZone, this.adjustedLastSuccessfulSyncTime.ToUniversalTime());
			message[AggregationSubscriptionMessageSchema.SharingOutageDetectionDiagnostics] = this.TruncateDiagnosticInformation(this.outageDetectionDiagnostics);
			message[AggregationSubscriptionMessageSchema.SharingInitialSyncInRecoveryMode] = this.initialSyncInRecoveryMode;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00017FFC File Offset: 0x000161FC
		protected virtual bool TrySecureStringToEncryptedString(SecureString secureString, out string encryptedString, out Exception exception)
		{
			return AggregationSubscription.ExchangeGroupKeyObject.TrySecureStringToEncryptedString(secureString, out encryptedString, out exception);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001800B File Offset: 0x0001620B
		protected virtual bool TryEncryptedStringToSecureString(string encryptedString, out SecureString secureString, out Exception exception)
		{
			return AggregationSubscription.ExchangeGroupKeyObject.TryEncryptedStringToSecureString(encryptedString, out secureString, out exception);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001801C File Offset: 0x0001621C
		protected virtual bool LoadMinimumInfo(MessageItem message)
		{
			this.baseLoadMinimumInfoCalled = true;
			bool result = true;
			this.subscriptionIdentity.SubscriptionId = SyncUtilities.SafeGetProperty<Guid>(message, MessageItemSchema.SharingInstanceGuid);
			this.diagnostics = SyncUtilities.SafeGetProperty<string>(message, MessageItemSchema.SharingDiagnostics, string.Empty);
			if (this.subscriptionIdentity.SubscriptionId == Guid.Empty)
			{
				this.SetPropertyLoadError("SharingInstanceGuid", this.subscriptionIdentity.SubscriptionId.ToString());
				this.subscriptionIdentity.SubscriptionId = Guid.NewGuid();
				result = false;
			}
			try
			{
				this.GetEnumProperty<AggregationStatus>(message, AggregationSubscriptionMessageSchema.SharingAggregationStatus, null, out this.status);
			}
			catch (SyncPropertyValidationException ex)
			{
				this.SetPropertyLoadError(ex.Property, ex.Value);
				result = false;
			}
			try
			{
				this.GetEnumProperty<DetailedAggregationStatus>(message, MessageItemSchema.SharingDetailedStatus, null, true, out this.detailedAggregationStatus);
			}
			catch (SyncPropertyValidationException ex2)
			{
				this.SetPropertyLoadError(ex2.Property, ex2.Value);
				result = false;
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00018134 File Offset: 0x00016334
		protected virtual void LoadProperties(MessageItem message)
		{
			this.baseLoadPropertiesCalled = true;
			this.SubscriptionType = AggregationSubscription.GetSubscriptionKind(message);
			this.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingAggregationProtocolName, true, null, null, out this.subscriptionProtocolName);
			this.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingAggregationProtocolVersion, new int?(0), null, out this.subscriptionProtocolVersion);
			this.GetIntProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration, null, null, out this.subscriptionGeneralConfig);
			this.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionName, true, null, null, out this.name);
			this.creationTime = (DateTime)message.CreationTime;
			this.lastModifiedTime = (DateTime)message.LastModifiedTime;
			ExDateTime? exDateTime;
			this.GetExDateTimeProperty(message, MessageItemSchema.SharingLastSync, out exDateTime);
			this.lastSyncTime = (DateTime?)exDateTime;
			ExDateTime? exDateTime2;
			this.GetExDateTimeProperty(message, AggregationSubscriptionMessageSchema.SharingLastSuccessSyncTime, out exDateTime2);
			this.lastSuccessfulSyncTime = (DateTime?)exDateTime2;
			bool flag = false;
			if (this.version >= 2L)
			{
				this.GetEnumProperty<SubscriptionCreationType>(message, AggregationSubscriptionMessageSchema.SharingSubscriptionCreationType, null, out this.creationType);
				int num = SyncUtilities.SafeGetProperty<int>(message, AggregationSubscriptionMessageSchema.SharingMigrationState);
				flag = (this.wasInitialSyncDone = ((num & 16) != 0));
				if ((num & 1) != 0)
				{
					this.AggregationType = AggregationType.Migration;
				}
				string text;
				this.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingPoisonCallstack, true, true, null, null, out text);
				if (!string.IsNullOrEmpty(text))
				{
					Exception ex = null;
					SecureString secureString = null;
					if (this.TryEncryptedStringToSecureString(text, out secureString, out ex))
					{
						this.poisonCallstack = SyncUtilities.SecureStringToString(secureString);
					}
					else
					{
						this.syncLogSession.LogError((TSLID)40UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to Decrypt poison callstack due to error: {0}, for Subscription: ID {1}, Name {2}, Protocol {3}.", new object[]
						{
							ex,
							this.SubscriptionGuid,
							this.Name,
							this.SubscriptionProtocolName
						});
					}
				}
			}
			if (this.version >= 4L)
			{
				this.GetEnumProperty<SubscriptionEvents>(message, AggregationSubscriptionMessageSchema.SharingSubscriptionEvents, null, out this.subscriptionEvents);
			}
			else
			{
				this.subscriptionEvents = SubscriptionEvents.None;
			}
			if (this.version >= 5L)
			{
				string empty = string.Empty;
				this.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionExclusionFolders, true, true, null, null, out empty);
				this.SetFoldersToExclude(AggregationSubscription.DeserializeFoldersToExclude(empty));
			}
			else
			{
				this.SetFoldersToExclude(null);
			}
			if (this.version >= 7L)
			{
				this.GetLongProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSynced, new long?(0L), null, out this.itemsSynced);
				this.GetLongProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionItemsSkipped, new long?(0L), null, out this.itemsSkipped);
			}
			else
			{
				this.itemsSynced = 0L;
				this.itemsSkipped = 0L;
			}
			if (this.version >= 8L)
			{
				ExDateTime exDateTime3;
				this.GetExDateTimeProperty(message, AggregationSubscriptionMessageSchema.SharingAdjustedLastSuccessfulSyncTime, out exDateTime3);
				this.adjustedLastSuccessfulSyncTime = (DateTime)exDateTime3;
				this.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingOutageDetectionDiagnostics, true, false, null, null, out this.outageDetectionDiagnostics);
			}
			else if (this.lastSuccessfulSyncTime != null)
			{
				this.adjustedLastSuccessfulSyncTime = this.lastSuccessfulSyncTime.Value;
			}
			else
			{
				this.adjustedLastSuccessfulSyncTime = this.creationTime;
			}
			if (this.version >= 9L)
			{
				this.GetEnumProperty<SyncPhase>(message, AggregationSubscriptionMessageSchema.SharingSubscriptionSyncPhase, null, out this.syncPhase);
			}
			else
			{
				this.syncPhase = (flag ? SyncPhase.Incremental : SyncPhase.Initial);
			}
			if (this.version >= 10L)
			{
				ExDateTime? exDateTime4;
				this.GetExDateTimeProperty(message, AggregationSubscriptionMessageSchema.SharingLastSyncNowRequest, out exDateTime4);
				this.lastSyncNowRequestTime = (DateTime?)exDateTime4;
			}
			if (this.Version >= 11L)
			{
				try
				{
					this.GetLongProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionTotalItemsInSourceMailbox, null, null, out this.totalItemsInSourceMailbox);
				}
				catch (SyncPropertyValidationException ex2)
				{
					this.syncLogSession.LogError((TSLID)322UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to load SharingSubscriptionTotalItemsInSourceMailbox property due to error: {0}, for Subscription: ID {1}, Name {2}, Protocol {3}.", new object[]
					{
						ex2,
						this.SubscriptionGuid,
						this.Name,
						this.SubscriptionProtocolName
					});
				}
				try
				{
					this.GetLongProperty(message, AggregationSubscriptionMessageSchema.SharingSubscriptionTotalSizeOfSourceMailbox, null, null, out this.totalSizeOfSourceMailbox);
				}
				catch (SyncPropertyValidationException ex3)
				{
					this.syncLogSession.LogError((TSLID)323UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to load SharingSubscriptionTotalSizeOfSourceMailbox property due to error: {0}, for Subscription: ID {1}, Name {2}, Protocol {3}.", new object[]
					{
						ex3,
						this.SubscriptionGuid,
						this.Name,
						this.SubscriptionProtocolName
					});
				}
			}
			if (this.Version >= 13L)
			{
				this.GetEnumProperty<AggregationType>(message, AggregationSubscriptionMessageSchema.SharingAggregationType, null, out this.aggregationType);
				object value;
				this.GetProperty(message, AggregationSubscriptionMessageSchema.SharingInitialSyncInRecoveryMode, out value);
				this.initialSyncInRecoveryMode = Convert.ToBoolean(value);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00018654 File Offset: 0x00016854
		protected virtual void SetMinimumInfoToMessageObject(MessageItem message)
		{
			this.baseSetMinimumInfoToMessageObject = true;
			message[MessageItemSchema.SharingInstanceGuid] = this.subscriptionIdentity.SubscriptionId;
			message[AggregationSubscriptionMessageSchema.SharingAggregationStatus] = (int)this.status;
			message[MessageItemSchema.SharingDetailedStatus] = (int)this.detailedAggregationStatus;
			this.SetDiagnosticInfoToMessageObject(message);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000186B8 File Offset: 0x000168B8
		protected void SetPropertyLoadError(string propertyName, string value)
		{
			this.diagnostics += string.Format(CultureInfo.InvariantCulture, "\r\nNot able to load {0} property or value loaded doesn't match expected type. Value loaded is: {1}", new object[]
			{
				propertyName,
				value
			});
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000186F5 File Offset: 0x000168F5
		protected void GetEnumProperty<T>(MessageItem message, StorePropertyDefinition propertyDefinition, int? enumMask, out T propertyValue) where T : struct
		{
			this.GetEnumProperty<T>(message, propertyDefinition, enumMask, false, out propertyValue);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00018704 File Offset: 0x00016904
		protected void GetEnumProperty<T>(MessageItem message, StorePropertyDefinition propertyDefinition, int? enumMask, bool useDefaultIfPropertyInvalid, out T propertyValue) where T : struct
		{
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				propertyValue = (T)((object)obj);
				if (enumMask != null)
				{
					int num = Convert.ToInt32(propertyValue) & enumMask.Value;
					propertyValue = (T)((object)num);
				}
				if (EnumValidator.IsValidValue<T>(propertyValue))
				{
					return;
				}
				if (useDefaultIfPropertyInvalid)
				{
					propertyValue = default(T);
					return;
				}
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000187B4 File Offset: 0x000169B4
		protected void GetIntProperty(MessageItem message, StorePropertyDefinition propertyDefinition, int? minValue, int? maxValue, out int propertyValue)
		{
			propertyValue = 0;
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				propertyValue = (int)obj;
				if ((minValue == null || !(propertyValue < minValue)) && (maxValue == null || !(propertyValue > maxValue)))
				{
					return;
				}
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00018864 File Offset: 0x00016A64
		protected void GetLongProperty(MessageItem message, StorePropertyDefinition propertyDefinition, long? minValue, long? maxValue, out long propertyValue)
		{
			propertyValue = 0L;
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				propertyValue = (long)obj;
				if ((minValue == null || !(propertyValue < minValue)) && (maxValue == null || !(propertyValue > maxValue)))
				{
					return;
				}
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00018914 File Offset: 0x00016B14
		protected void GetDoubleProperty(MessageItem message, StorePropertyDefinition propertyDefinition, double? minValue, double? maxValue, out double propertyValue)
		{
			propertyValue = 0.0;
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				propertyValue = (double)obj;
				if (minValue != null)
				{
					double num = propertyValue;
					double? num2 = minValue;
					if (num < num2.GetValueOrDefault() && num2 != null)
					{
						goto IL_76;
					}
				}
				if (maxValue != null)
				{
					double num3 = propertyValue;
					double? num4 = maxValue;
					if (num3 > num4.GetValueOrDefault() && num4 != null)
					{
						goto IL_76;
					}
				}
				return;
				IL_76:;
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000189CC File Offset: 0x00016BCC
		protected void GetSmtpAddressProperty(MessageItem message, StorePropertyDefinition propertyDefinition, out SmtpAddress propertyValue)
		{
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				SmtpAddress smtpAddress = new SmtpAddress(obj.ToString());
				if (smtpAddress.IsValidAddress)
				{
					propertyValue = smtpAddress;
					return;
				}
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00018A3C File Offset: 0x00016C3C
		protected void GetFqdnProperty(MessageItem message, StorePropertyDefinition propertyDefinition, out Fqdn server, out int port)
		{
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				string text = (string)obj;
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.IndexOf(':');
					if (num > 0 && Fqdn.TryParse(text.Remove(num), out server))
					{
						port = int.Parse(text.Remove(0, num + 1));
						if (port >= 0 && port <= 65535)
						{
							return;
						}
					}
				}
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00018ADC File Offset: 0x00016CDC
		protected void GetExDateTimeProperty(MessageItem message, StorePropertyDefinition propertyDefinition, out ExDateTime propertyValue)
		{
			object obj = null;
			this.GetProperty(message, propertyDefinition, out obj);
			ExDateTime? exDateTime = obj as ExDateTime?;
			if (exDateTime != null)
			{
				propertyValue = exDateTime.Value;
				return;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), obj.ToString());
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00018B2C File Offset: 0x00016D2C
		protected void GetExDateTimeProperty(MessageItem message, StorePropertyDefinition propertyDefinition, out ExDateTime? propertyValue)
		{
			ExDateTime exDateTime;
			this.GetExDateTimeProperty(message, propertyDefinition, out exDateTime);
			if (exDateTime == SyncUtilities.ExZeroTime)
			{
				propertyValue = null;
				return;
			}
			propertyValue = new ExDateTime?(exDateTime);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00018B64 File Offset: 0x00016D64
		protected void GetStringProperty(MessageItem message, StorePropertyDefinition propertyDefinition, bool canBeEmptyNull, uint? minLength, uint? maxLength, out string propertyValue)
		{
			propertyValue = null;
			object obj = null;
			Exception innerException = null;
			try
			{
				this.GetProperty(message, propertyDefinition, out obj);
				propertyValue = (string)obj;
				if (canBeEmptyNull)
				{
					return;
				}
				if (!string.IsNullOrEmpty(propertyValue))
				{
					if (minLength != null)
					{
						long num = (long)propertyValue.Length;
						uint? num2 = minLength;
						if (num < (long)((ulong)num2.GetValueOrDefault()) && num2 != null)
						{
							goto IL_8A;
						}
					}
					if (maxLength != null)
					{
						long num3 = (long)propertyValue.Length;
						uint? num4 = maxLength;
						if (num3 > (long)((ulong)num4.GetValueOrDefault()) && num4 != null)
						{
							goto IL_8A;
						}
					}
					return;
				}
				IL_8A:;
			}
			catch (FormatException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (obj == null) ? "Null" : obj.ToString(), innerException);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00018C30 File Offset: 0x00016E30
		protected void GetStringProperty(MessageItem message, StorePropertyDefinition propertyDefinition, bool canBeEmptyNull, bool canBeMissing, uint? minLength, uint? maxLength, out string propertyValue)
		{
			propertyValue = null;
			if (canBeMissing)
			{
				propertyValue = SyncUtilities.SafeGetProperty<string>(message, propertyDefinition);
				return;
			}
			this.GetStringProperty(message, propertyDefinition, canBeEmptyNull, minLength, maxLength, out propertyValue);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018C54 File Offset: 0x00016E54
		protected void GetProperty(MessageItem message, StorePropertyDefinition propertyDefinition, out object propertyValue)
		{
			propertyValue = null;
			Exception innerException = null;
			try
			{
				propertyValue = message.TryGetProperty(propertyDefinition);
				if (!(propertyValue is PropertyError))
				{
					return;
				}
			}
			catch (PropertyErrorException ex)
			{
				innerException = ex;
			}
			throw new SyncPropertyValidationException(propertyDefinition.ToString(), (propertyValue == null) ? "Null" : propertyValue.ToString(), innerException);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00018CB0 File Offset: 0x00016EB0
		protected virtual StoreObjectId GetMessageId(MessageItem message)
		{
			return message.Id.ObjectId;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00018CBD File Offset: 0x00016EBD
		protected virtual void SaveMessage(MessageItem message)
		{
			message.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00018CC8 File Offset: 0x00016EC8
		protected virtual void LoadSubscriptionIdentity(MailboxSession mailboxSession)
		{
			if (!mailboxSession.MailboxOwner.ObjectId.IsNullOrEmpty())
			{
				this.subscriptionIdentity.AdUserId = mailboxSession.MailboxOwner.ObjectId;
			}
			this.subscriptionIdentity.LegacyDN = mailboxSession.MailboxOwnerLegacyDN;
			this.subscriptionIdentity.PrimaryMailboxLegacyDN = mailboxSession.MailboxOwner.LegacyDn;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00018D24 File Offset: 0x00016F24
		protected virtual void OpenMessageForReadWrite(MessageItem message)
		{
			message.OpenAsReadWrite();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00018D2C File Offset: 0x00016F2C
		private void SetDiagnosticInfoToMessageObject(MessageItem message)
		{
			if (this.diagnostics == null)
			{
				message[MessageItemSchema.SharingDiagnostics] = string.Empty;
				return;
			}
			message[MessageItemSchema.SharingDiagnostics] = this.TruncateDiagnosticInformation(this.diagnostics);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00018D5E File Offset: 0x00016F5E
		private string TruncateDiagnosticInformation(string diagnosticInformation)
		{
			if (diagnosticInformation.Length <= 8192)
			{
				return diagnosticInformation;
			}
			return diagnosticInformation.Substring(diagnosticInformation.Length - 8192);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00018D84 File Offset: 0x00016F84
		private bool TryLoadSubscriptionVersion(MessageItem message, out bool isFutureVersion)
		{
			isFutureVersion = false;
			long num = SyncUtilities.SafeGetProperty<long>(message, MessageItemSchema.SharingSubscriptionVersion);
			if (num < 0L)
			{
				this.SetPropertyLoadError("SharingSubscriptionVersion", num.ToString());
				return false;
			}
			if (num > AggregationSubscription.CurrentSupportedVersion)
			{
				this.syncLogSession.LogVerbose((TSLID)507UL, "Setting version of the in-memory subscription object from {0} to {1}", new object[]
				{
					num,
					AggregationSubscription.CurrentSupportedVersion
				});
				this.version = AggregationSubscription.CurrentSupportedVersion;
				isFutureVersion = true;
			}
			else
			{
				this.version = num;
			}
			return true;
		}

		// Token: 0x04000293 RID: 659
		internal const int MaxDiagnosticsLength = 8192;

		// Token: 0x04000294 RID: 660
		private const string InvalidVersionMessage = "\r\nExpected version {0}, but Subscription has version {1}.";

		// Token: 0x04000295 RID: 661
		private const string PropertyLoadError = "\r\nNot able to load {0} property or value loaded doesn't match expected type. Value loaded is: {1}";

		// Token: 0x04000296 RID: 662
		private const int DefaultFoldersToExcludeLength = 265;

		// Token: 0x04000297 RID: 663
		public static readonly string FolderExclusionDelimiter = "\r\n";

		// Token: 0x04000298 RID: 664
		protected static readonly IExchangeGroupKey ExchangeGroupKeyObject = SyncUtilities.CreateExchangeGroupKey();

		// Token: 0x04000299 RID: 665
		[NonSerialized]
		private readonly SyncLogSession syncLogSession;

		// Token: 0x0400029A RID: 666
		private static long currentSupportedVersion = 13L;

		// Token: 0x0400029B RID: 667
		private HashSet<string> foldersToExclude;

		// Token: 0x0400029C RID: 668
		private AggregationSubscriptionIdentity subscriptionIdentity;

		// Token: 0x0400029D RID: 669
		private string instanceTypeFullName;

		// Token: 0x0400029E RID: 670
		private AggregationSubscriptionType subscriptionType;

		// Token: 0x0400029F RID: 671
		private long version;

		// Token: 0x040002A0 RID: 672
		private SubscriptionCreationType creationType = SubscriptionCreationType.Manual;

		// Token: 0x040002A1 RID: 673
		private AggregationType aggregationType;

		// Token: 0x040002A2 RID: 674
		private SyncPhase syncPhase;

		// Token: 0x040002A3 RID: 675
		private string userExchangeMailboxDisplayName;

		// Token: 0x040002A4 RID: 676
		private string userExchangeMailboxSmtpAddress;

		// Token: 0x040002A5 RID: 677
		private int subscriptionGeneralConfig;

		// Token: 0x040002A6 RID: 678
		private int subscriptionProtocolVersion;

		// Token: 0x040002A7 RID: 679
		private string subscriptionProtocolName;

		// Token: 0x040002A8 RID: 680
		private string name;

		// Token: 0x040002A9 RID: 681
		private AggregationStatus status;

		// Token: 0x040002AA RID: 682
		private DetailedAggregationStatus detailedAggregationStatus;

		// Token: 0x040002AB RID: 683
		private string diagnostics = string.Empty;

		// Token: 0x040002AC RID: 684
		private string poisonCallstack;

		// Token: 0x040002AD RID: 685
		private DateTime lastModifiedTime;

		// Token: 0x040002AE RID: 686
		private DateTime? lastSyncTime;

		// Token: 0x040002AF RID: 687
		private DateTime? lastSuccessfulSyncTime;

		// Token: 0x040002B0 RID: 688
		private DateTime? lastSyncNowRequestTime;

		// Token: 0x040002B1 RID: 689
		private DateTime creationTime = DateTime.MinValue.ToUniversalTime();

		// Token: 0x040002B2 RID: 690
		private DateTime adjustedLastSuccessfulSyncTime;

		// Token: 0x040002B3 RID: 691
		private StoreObjectId subscriptionMessageId;

		// Token: 0x040002B4 RID: 692
		private byte[] instanceKey;

		// Token: 0x040002B5 RID: 693
		private bool isValid = true;

		// Token: 0x040002B6 RID: 694
		private bool baseLoadPropertiesCalled;

		// Token: 0x040002B7 RID: 695
		private bool baseLoadMinimumInfoCalled;

		// Token: 0x040002B8 RID: 696
		private bool baseSetPropertiesToMessageObject;

		// Token: 0x040002B9 RID: 697
		private bool baseSetMinimumInfoToMessageObject;

		// Token: 0x040002BA RID: 698
		private bool wasInitialSyncDone;

		// Token: 0x040002BB RID: 699
		private SubscriptionEvents subscriptionEvents;

		// Token: 0x040002BC RID: 700
		private long itemsSynced;

		// Token: 0x040002BD RID: 701
		private long itemsSkipped;

		// Token: 0x040002BE RID: 702
		private string outageDetectionDiagnostics = string.Empty;

		// Token: 0x040002BF RID: 703
		private long totalItemsInSourceMailbox;

		// Token: 0x040002C0 RID: 704
		private long totalSizeOfSourceMailbox;

		// Token: 0x040002C1 RID: 705
		private bool initialSyncInRecoveryMode;

		// Token: 0x020000B0 RID: 176
		protected class SubscriptionDeserializer
		{
			// Token: 0x060004F9 RID: 1273 RVA: 0x00018E30 File Offset: 0x00017030
			internal SubscriptionDeserializer(BinaryReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x060004FA RID: 1274 RVA: 0x00018E3F File Offset: 0x0001703F
			internal void DeserializeVersionAndSubscriptionType(out long version, out AggregationSubscriptionType subscriptionType)
			{
				version = this.reader.ReadInt64();
				subscriptionType = this.GetEnumValue<AggregationSubscriptionType>();
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x00018EEC File Offset: 0x000170EC
			internal void DeserializePopSubscription(PopAggregationSubscription popAggregationSubscription)
			{
				this.DeserializeWithExceptionHandling(delegate
				{
					this.DeserializePimSubscription(popAggregationSubscription);
					popAggregationSubscription.PopServer = new Fqdn(this.reader.ReadString());
					popAggregationSubscription.PopPort = this.reader.ReadInt32();
					popAggregationSubscription.PopLogonName = this.reader.ReadString();
					popAggregationSubscription.Flags = this.GetEnumValue<PopAggregationFlags>();
				});
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x00018FD0 File Offset: 0x000171D0
			internal void DeserializeImapSubscription(IMAPAggregationSubscription imapAggregationSubscription)
			{
				this.DeserializeWithExceptionHandling(delegate
				{
					this.DeserializePimSubscription(imapAggregationSubscription);
					imapAggregationSubscription.IMAPServer = new Fqdn(this.reader.ReadString());
					imapAggregationSubscription.IMAPPort = this.reader.ReadInt32();
					imapAggregationSubscription.IMAPLogOnName = this.reader.ReadString();
					imapAggregationSubscription.Flags = this.GetEnumValue<IMAPAggregationFlags>();
					imapAggregationSubscription.ImapPathPrefix = this.reader.ReadString();
				});
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x00019120 File Offset: 0x00017320
			internal void DeserializeDeltaSyncSubscription(DeltaSyncAggregationSubscription deltaSyncAggregationSubscription)
			{
				this.DeserializeWithExceptionHandling(delegate
				{
					this.DeserializeWindowsLiveSubscription(deltaSyncAggregationSubscription);
					deltaSyncAggregationSubscription.MinSyncPollInterval = this.reader.ReadInt32();
					deltaSyncAggregationSubscription.MinSettingPollInterval = this.reader.ReadInt32();
					deltaSyncAggregationSubscription.SyncMultiplier = this.reader.ReadDouble();
					deltaSyncAggregationSubscription.MaxObjectInSync = this.reader.ReadInt32();
					deltaSyncAggregationSubscription.MaxNumberOfEmailAdds = this.reader.ReadInt32();
					deltaSyncAggregationSubscription.MaxNumberOfFolderAdds = this.reader.ReadInt32();
					deltaSyncAggregationSubscription.MaxAttachments = this.reader.ReadInt32();
					deltaSyncAggregationSubscription.MaxMessageSize = this.reader.ReadInt64();
					deltaSyncAggregationSubscription.MaxRecipients = this.reader.ReadInt32();
				});
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x000191B0 File Offset: 0x000173B0
			internal void DeserializeConnectSubscription(ConnectSubscription subscription)
			{
				this.DeserializeWithExceptionHandling(delegate
				{
					this.DeserializePimSubscription(subscription);
					subscription.EncryptedAccessToken = this.reader.ReadString();
					subscription.EncryptedAccessTokenSecret = this.reader.ReadString();
				});
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x000191E4 File Offset: 0x000173E4
			private void DeserializeWindowsLiveSubscription(WindowsLiveServiceAggregationSubscription subscription)
			{
				this.DeserializePimSubscription(subscription);
				subscription.IncommingServerUrl = this.reader.ReadString();
				subscription.AuthPolicy = this.reader.ReadString();
				subscription.Puid = this.reader.ReadString();
				subscription.LogonName = this.reader.ReadString();
				subscription.AuthToken = this.reader.ReadString();
				subscription.AuthTokenExpirationTime = (this.GetDateTimeValue() ?? subscription.AuthTokenExpirationTime);
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x00019274 File Offset: 0x00017474
			private void DeserializePimSubscription(PimAggregationSubscription subscription)
			{
				this.Deserialize(subscription);
				subscription.UserDisplayName = this.reader.ReadString();
				subscription.UserEmailAddress = new SmtpAddress(this.reader.ReadString());
				subscription.EncryptedLogonPassword = this.reader.ReadString();
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x000192C0 File Offset: 0x000174C0
			private void Deserialize(AggregationSubscription subscription)
			{
				this.DeserializeVersionAndSubscriptionType(out subscription.version, out subscription.subscriptionType);
				subscription.aggregationType = this.GetEnumValue<AggregationType>();
				string text = this.reader.ReadString();
				if (string.IsNullOrEmpty(text))
				{
					throw new SerializationException("Subscription Guid is not valid.");
				}
				Guid guid = new Guid(text);
				if (object.Equals(guid, Guid.Empty))
				{
					throw new SerializationException("Subscription Guid is Guid.Empty.");
				}
				string legacyDN = this.reader.ReadString();
				string primaryMailboxLegacyDN = this.reader.ReadString();
				subscription.subscriptionIdentity = new AggregationSubscriptionIdentity(guid, legacyDN, primaryMailboxLegacyDN);
				subscription.creationType = this.GetEnumValue<SubscriptionCreationType>();
				subscription.subscriptionMessageId = StoreObjectId.Deserialize(this.reader.ReadString());
				subscription.name = this.reader.ReadString();
				subscription.subscriptionEvents = this.GetEnumValue<SubscriptionEvents>();
				subscription.foldersToExclude = AggregationSubscription.DeserializeFoldersToExclude(this.reader.ReadString());
				subscription.lastSuccessfulSyncTime = this.GetDateTimeValue();
				subscription.lastSyncTime = this.GetDateTimeValue();
				subscription.adjustedLastSuccessfulSyncTime = this.GetDateTimeValue().Value;
				subscription.poisonCallstack = this.reader.ReadString();
				subscription.syncPhase = this.GetEnumValue<SyncPhase>();
				subscription.status = this.GetEnumValue<AggregationStatus>();
				subscription.detailedAggregationStatus = this.GetEnumValue<DetailedAggregationStatus>();
				subscription.itemsSynced = this.reader.ReadInt64();
				subscription.itemsSkipped = this.reader.ReadInt64();
				subscription.totalItemsInSourceMailbox = this.reader.ReadInt64();
				subscription.totalSizeOfSourceMailbox = this.reader.ReadInt64();
				subscription.outageDetectionDiagnostics = this.reader.ReadString();
				subscription.creationTime = this.GetDateTimeValue().Value;
				subscription.isValid = this.reader.ReadBoolean();
				subscription.diagnostics = this.reader.ReadString();
				subscription.userExchangeMailboxSmtpAddress = this.reader.ReadString();
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x000194B0 File Offset: 0x000176B0
			private void DeserializeWithExceptionHandling(Action action)
			{
				try
				{
					action();
				}
				catch (CorruptDataException innerException)
				{
					throw new SerializationException("Subscription is not valid.", innerException);
				}
				catch (IOException innerException2)
				{
					throw new SerializationException("Subscription is not valid.", innerException2);
				}
				catch (FormatException innerException3)
				{
					throw new SerializationException("Subscription is not valid.", innerException3);
				}
				catch (CryptographicException innerException4)
				{
					throw new SerializationException("Subscription is not valid.", innerException4);
				}
				catch (InvalidDataException innerException5)
				{
					throw new SerializationException("Subscription is not valid.", innerException5);
				}
				catch (Exception ex)
				{
					if (AggregationSubscription.ExchangeGroupKeyObject.IsDkmException(ex))
					{
						throw new SerializationException("Subscription is not valid.", ex);
					}
					throw;
				}
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x00019574 File Offset: 0x00017774
			private DateTime? GetDateTimeValue()
			{
				long num = this.reader.ReadInt64();
				if (num == 0L)
				{
					return null;
				}
				if (num < DateTime.MinValue.Ticks || num > DateTime.MaxValue.Ticks)
				{
					throw new SerializationException("Invalid dateTime in ticks.");
				}
				return new DateTime?(new DateTime(num, DateTimeKind.Utc));
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x000195D4 File Offset: 0x000177D4
			private T GetEnumValue<T>() where T : struct
			{
				int value = this.reader.ReadInt32();
				T t = (T)((object)Enum.ToObject(typeof(T), value));
				if (!EnumValidator.IsValidValue<T>(t))
				{
					throw new SerializationException(string.Format("{0} is invalid: {1}", typeof(T), t));
				}
				return t;
			}

			// Token: 0x040002C2 RID: 706
			private readonly BinaryReader reader;
		}

		// Token: 0x020000B1 RID: 177
		protected class SubscriptionSerializer
		{
			// Token: 0x06000505 RID: 1285 RVA: 0x0001962C File Offset: 0x0001782C
			internal SubscriptionSerializer(BinaryWriter writer)
			{
				this.writer = writer;
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x0001963C File Offset: 0x0001783C
			internal void SerializePopSubscription(PopAggregationSubscription popSubscription)
			{
				this.SerializePimSubscription(popSubscription);
				this.writer.Write(popSubscription.PopServer.ToString());
				this.writer.Write(popSubscription.PopPort);
				this.writer.Write(popSubscription.PopLogonName);
				this.writer.Write((int)popSubscription.Flags);
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x0001969C File Offset: 0x0001789C
			internal void SerializeImapSubscription(IMAPAggregationSubscription imapSubscription)
			{
				this.SerializePimSubscription(imapSubscription);
				this.writer.Write(imapSubscription.IMAPServer.ToString());
				this.writer.Write(imapSubscription.IMAPPort);
				this.writer.Write(imapSubscription.IMAPLogOnName);
				this.writer.Write((int)imapSubscription.Flags);
				this.writer.Write(string.IsNullOrEmpty(imapSubscription.ImapPathPrefix) ? string.Empty : imapSubscription.ImapPathPrefix);
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x00019720 File Offset: 0x00017920
			internal void SerializeDeltaSyncSubscription(DeltaSyncAggregationSubscription deltaSyncAggregationSubscription)
			{
				this.SerializeWindowsLiveSubscription(deltaSyncAggregationSubscription);
				this.writer.Write(deltaSyncAggregationSubscription.MinSyncPollInterval);
				this.writer.Write(deltaSyncAggregationSubscription.MinSettingPollInterval);
				this.writer.Write(deltaSyncAggregationSubscription.SyncMultiplier);
				this.writer.Write(deltaSyncAggregationSubscription.MaxObjectInSync);
				this.writer.Write(deltaSyncAggregationSubscription.MaxNumberOfEmailAdds);
				this.writer.Write(deltaSyncAggregationSubscription.MaxNumberOfFolderAdds);
				this.writer.Write(deltaSyncAggregationSubscription.MaxAttachments);
				this.writer.Write(deltaSyncAggregationSubscription.MaxMessageSize);
				this.writer.Write(deltaSyncAggregationSubscription.MaxRecipients);
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x000197CD File Offset: 0x000179CD
			internal void SerializeConnectSubscription(ConnectSubscription subscription)
			{
				this.SerializePimSubscription(subscription);
				this.writer.Write(subscription.EncryptedAccessToken);
				this.writer.Write(subscription.EncryptedAccessTokenSecret ?? string.Empty);
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x00019804 File Offset: 0x00017A04
			private void SerializeWindowsLiveSubscription(WindowsLiveServiceAggregationSubscription subscription)
			{
				this.SerializePimSubscription(subscription);
				this.writer.Write(subscription.IncommingServerUrl);
				this.writer.Write(subscription.AuthPolicy);
				this.writer.Write(subscription.Puid ?? string.Empty);
				this.writer.Write(subscription.LogonName);
				this.writer.Write(subscription.AuthToken ?? string.Empty);
				this.WriteDateTime(new DateTime?(subscription.AuthTokenExpirationTime));
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x00019890 File Offset: 0x00017A90
			private void SerializePimSubscription(PimAggregationSubscription subscription)
			{
				this.Serialize(subscription);
				this.writer.Write(subscription.UserDisplayName);
				this.writer.Write(subscription.UserEmailAddress.ToString());
				this.writer.Write(subscription.EncryptedLogonPassword ?? string.Empty);
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x000198F0 File Offset: 0x00017AF0
			private void Serialize(AggregationSubscription subscription)
			{
				this.writer.Write(subscription.version);
				this.writer.Write((int)subscription.SubscriptionType);
				this.writer.Write((int)subscription.AggregationType);
				this.writer.Write(subscription.SubscriptionGuid.ToString("N"));
				this.writer.Write(subscription.UserLegacyDN);
				this.writer.Write(subscription.PrimaryMailboxUserLegacyDN);
				this.writer.Write((int)subscription.CreationType);
				this.writer.Write(subscription.SubscriptionMessageId.ToBase64String());
				this.writer.Write(subscription.Name);
				this.writer.Write((int)subscription.SubscriptionEvents);
				this.writer.Write(AggregationSubscription.SerializeFoldersToExclude(subscription.foldersToExclude));
				this.WriteDateTime(subscription.LastSuccessfulSyncTime);
				this.WriteDateTime(subscription.LastSyncTime);
				this.WriteDateTime(new DateTime?(subscription.AdjustedLastSuccessfulSyncTime));
				this.writer.Write(subscription.PoisonCallstack ?? string.Empty);
				this.writer.Write((int)subscription.SyncPhase);
				this.writer.Write((int)subscription.Status);
				this.writer.Write((int)subscription.DetailedAggregationStatus);
				this.writer.Write(subscription.ItemsSynced);
				this.writer.Write(subscription.ItemsSkipped);
				this.WriteNullableLong(subscription.TotalItemsInSourceMailbox);
				this.WriteNullableLong(subscription.TotalSizeOfSourceMailbox);
				this.writer.Write(subscription.TruncateDiagnosticInformation(subscription.OutageDetectionDiagnostics));
				this.WriteDateTime(new DateTime?(subscription.CreationTime));
				this.writer.Write(subscription.IsValid);
				this.writer.Write(subscription.TruncateDiagnosticInformation(subscription.Diagnostics));
				this.writer.Write(subscription.UserExchangeMailboxSmtpAddress);
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x00019AE0 File Offset: 0x00017CE0
			private void WriteDateTime(DateTime? dateTime)
			{
				if (dateTime != null)
				{
					this.writer.Write(dateTime.Value.ToUniversalTime().Ticks);
					return;
				}
				this.writer.Write(0L);
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x00019B26 File Offset: 0x00017D26
			private void WriteNullableLong(long? longValue)
			{
				if (longValue != null)
				{
					this.writer.Write(longValue.Value);
					return;
				}
				this.writer.Write(SyncUtilities.DataNotAvailable);
			}

			// Token: 0x040002C3 RID: 707
			private readonly BinaryWriter writer;
		}
	}
}
