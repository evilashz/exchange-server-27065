using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionCacheEntry
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002F18 File Offset: 0x00001118
		internal SubscriptionCacheEntry(GlobalSyncLogSession syncLogSession, Guid subscriptionGuid, StoreObjectId subscriptionMessageId, string userLegacyDn, Guid mailboxGuid, Guid tenantGuid, Guid externalDirectoryOrgId, AggregationSubscriptionType type, AggregationType aggregationType, DateTime? lastSyncCompletedTime, bool disabled, string incomingServerName, SyncPhase syncPhase, SerializedSubscription serializedSubscription)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("serializedSubscription", serializedSubscription);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNull("subscriptionMessageId", subscriptionMessageId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userLegacyDn", userLegacyDn);
			this.syncLogSession = syncLogSession;
			this.subscriptionGuid = subscriptionGuid;
			this.subscriptionMessageId = subscriptionMessageId;
			this.userLegacyDn = userLegacyDn;
			this.mailboxGuid = mailboxGuid;
			this.tenantGuid = tenantGuid;
			this.externalDirectoryOrgId = externalDirectoryOrgId;
			this.subscriptionType = type;
			this.aggregationType = aggregationType;
			this.disabled = disabled;
			this.incomingServerName = incomingServerName;
			this.syncPhase = syncPhase;
			this.serializedSubscription = serializedSubscription;
			if (lastSyncCompletedTime != null)
			{
				this.lastSyncCompletedTime = new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, lastSyncCompletedTime.Value));
				return;
			}
			lastSyncCompletedTime = null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000300E File Offset: 0x0000120E
		private SubscriptionCacheEntry(GlobalSyncLogSession syncLogSession)
		{
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00003028 File Offset: 0x00001228
		internal Guid SubscriptionGuid
		{
			get
			{
				return this.subscriptionGuid;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003030 File Offset: 0x00001230
		internal StoreObjectId SubscriptionMessageId
		{
			get
			{
				return this.subscriptionMessageId;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003038 File Offset: 0x00001238
		internal string UserLegacyDn
		{
			get
			{
				return this.userLegacyDn;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003040 File Offset: 0x00001240
		internal AggregationType AggregationType
		{
			get
			{
				return this.aggregationType;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003048 File Offset: 0x00001248
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00003050 File Offset: 0x00001250
		internal SyncPhase SyncPhase
		{
			get
			{
				return this.syncPhase;
			}
			set
			{
				this.syncPhase = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003059 File Offset: 0x00001259
		internal string IncomingServerName
		{
			get
			{
				return this.incomingServerName;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003061 File Offset: 0x00001261
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003069 File Offset: 0x00001269
		internal ExDateTime? LastSyncCompletedTime
		{
			get
			{
				return this.lastSyncCompletedTime;
			}
			set
			{
				this.lastSyncCompletedTime = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003072 File Offset: 0x00001272
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000307A File Offset: 0x0000127A
		internal Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003082 File Offset: 0x00001282
		internal Guid ExternalDirectoryOrgId
		{
			get
			{
				return this.externalDirectoryOrgId;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000308A File Offset: 0x0000128A
		internal AggregationSubscriptionType SubscriptionType
		{
			get
			{
				return this.subscriptionType;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003092 File Offset: 0x00001292
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000309A File Offset: 0x0000129A
		internal string HubServerDispatched
		{
			get
			{
				return this.hubServerDispatched;
			}
			set
			{
				this.hubServerDispatched = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000030A3 File Offset: 0x000012A3
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000030AB File Offset: 0x000012AB
		internal string LastHubServerDispatched
		{
			get
			{
				return this.lastHubServerDispatched;
			}
			set
			{
				this.lastHubServerDispatched = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000030B4 File Offset: 0x000012B4
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000030BC File Offset: 0x000012BC
		internal ExDateTime? FirstOutstandingDispatchTime
		{
			get
			{
				return this.firstOutstandingDispatchTime;
			}
			set
			{
				this.firstOutstandingDispatchTime = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000030C5 File Offset: 0x000012C5
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000030CD File Offset: 0x000012CD
		internal ExDateTime? LastSuccessfulDispatchTime
		{
			get
			{
				return this.lastSuccessfulDispatchTime;
			}
			set
			{
				this.lastSuccessfulDispatchTime = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000030DE File Offset: 0x000012DE
		internal bool RecoverySyncEnabled
		{
			get
			{
				return this.recoverySyncEnabled;
			}
			set
			{
				this.recoverySyncEnabled = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000030E7 File Offset: 0x000012E7
		internal bool IsMigration
		{
			get
			{
				return this.AggregationType == AggregationType.Migration;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000030F3 File Offset: 0x000012F3
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000030FB File Offset: 0x000012FB
		internal SerializedSubscription SerializedSubscription
		{
			get
			{
				return this.serializedSubscription;
			}
			set
			{
				this.serializedSubscription = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000310C File Offset: 0x0000130C
		internal bool Disabled
		{
			get
			{
				return this.disabled;
			}
			set
			{
				this.disabled = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003115 File Offset: 0x00001315
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000311D File Offset: 0x0000131D
		internal string Diagnostics
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003126 File Offset: 0x00001326
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003138 File Offset: 0x00001338
		internal string SyncWatermark
		{
			get
			{
				if (!this.syncWatermarkIsInitialized)
				{
					return null;
				}
				return this.syncWatermark;
			}
			set
			{
				SyncUtilities.ThrowIfArgumentNull("SyncWatermark", value);
				this.syncWatermark = value;
				this.syncWatermarkIsInitialized = true;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003154 File Offset: 0x00001354
		public override bool Equals(object obj)
		{
			SubscriptionCacheEntry otherCacheEntry = obj as SubscriptionCacheEntry;
			return this.Equals(otherCacheEntry);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000316F File Offset: 0x0000136F
		public bool Equals(SubscriptionCacheEntry otherCacheEntry)
		{
			return otherCacheEntry != null && this.SubscriptionGuid == otherCacheEntry.SubscriptionGuid;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003188 File Offset: 0x00001388
		public override int GetHashCode()
		{
			return this.SubscriptionGuid.GetHashCode();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031AC File Offset: 0x000013AC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "LegDn: {0}; HubDispatched: {1}; LastHubDispatched: {2}; LastSuccessfulDispatchTime: {3}; FirstOutstandingDispatchTime: {4}; RecoverySync: {5}; LastSyncCompletedTime: {6}; Disabled: {7}", new object[]
			{
				this.userLegacyDn,
				this.hubServerDispatched,
				this.lastHubServerDispatched,
				this.lastSuccessfulDispatchTime,
				this.firstOutstandingDispatchTime,
				this.recoverySyncEnabled,
				this.lastSyncCompletedTime,
				this.disabled
			});
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003234 File Offset: 0x00001434
		internal static SubscriptionCacheEntry FromSerialization(GlobalSyncLogSession syncLogSession, BinaryReader reader, byte version)
		{
			SubscriptionCacheEntry subscriptionCacheEntry = new SubscriptionCacheEntry(syncLogSession);
			subscriptionCacheEntry.Deserialize(reader, version);
			return subscriptionCacheEntry;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003254 File Offset: 0x00001454
		internal void Serialize(BinaryWriter writer)
		{
			writer.Write(this.subscriptionGuid.ToString("N"));
			writer.Write((short)this.subscriptionType);
			writer.Write(this.subscriptionMessageId.ToBase64String());
			writer.Write(this.userLegacyDn);
			writer.Write(this.mailboxGuid.ToString("N"));
			writer.Write((this.lastSyncCompletedTime != null) ? this.lastSyncCompletedTime.Value.UtcTicks : 0L);
			writer.Write(this.hubServerDispatched ?? string.Empty);
			writer.Write((this.firstOutstandingDispatchTime != null) ? this.firstOutstandingDispatchTime.Value.UtcTicks : 0L);
			writer.Write((this.lastSuccessfulDispatchTime != null) ? this.lastSuccessfulDispatchTime.Value.UtcTicks : 0L);
			writer.Write(this.recoverySyncEnabled);
			writer.Write(this.disabled);
			writer.Write(this.diagnostics ?? string.Empty);
			writer.Write(this.tenantGuid.ToString("N"));
			writer.Write(this.externalDirectoryOrgId.ToString("N"));
			writer.Write((int)this.aggregationType);
			writer.Write(this.incomingServerName ?? string.Empty);
			writer.Write((short)this.syncPhase);
			writer.Write((this.lastHubServerDispatched == null) ? string.Empty : this.lastHubServerDispatched);
			this.serializedSubscription.Serialize(writer);
			writer.Write(this.syncWatermarkIsInitialized);
			writer.Write(this.syncWatermark);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003410 File Offset: 0x00001610
		internal bool Validate(AggregationSubscription actualSubscription, Guid actualUserMailboxGuid, bool fix, out string inconsistencyInfo)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			if (this.subscriptionGuid != actualSubscription.SubscriptionGuid)
			{
				stringBuilder.Append(Strings.InvalidSubscriptionGuid);
			}
			if (!object.Equals(this.subscriptionMessageId, actualSubscription.SubscriptionMessageId))
			{
				stringBuilder.Append(Strings.InvalidSubscriptionMessageId);
			}
			if (!string.Equals(this.userLegacyDn, actualSubscription.UserLegacyDN, StringComparison.OrdinalIgnoreCase))
			{
				stringBuilder.Append(Strings.InvalidUserLegacyDn);
			}
			if (this.mailboxGuid != actualUserMailboxGuid)
			{
				stringBuilder.Append(Strings.InvalidUserMailboxGuid);
			}
			if (this.subscriptionType != actualSubscription.SubscriptionType)
			{
				stringBuilder.Append(Strings.InvalidSubscriptionType);
			}
			if (this.syncPhase != actualSubscription.SyncPhase)
			{
				stringBuilder.Append(Strings.InvalidSyncPhase);
			}
			bool inactive = actualSubscription.Inactive;
			if (this.disabled != inactive)
			{
				stringBuilder.Append(Strings.InvalidDisabledStatus);
			}
			if (this.serializedSubscription == null)
			{
				stringBuilder.Append(Strings.InvalidSubscription);
			}
			if (fix)
			{
				this.subscriptionGuid = actualSubscription.SubscriptionGuid;
				this.userLegacyDn = actualSubscription.UserLegacyDN;
				this.mailboxGuid = actualUserMailboxGuid;
				this.syncPhase = actualSubscription.SyncPhase;
				this.disabled = inactive;
				this.subscriptionType = actualSubscription.SubscriptionType;
				this.subscriptionMessageId = actualSubscription.SubscriptionMessageId;
				this.incomingServerName = actualSubscription.IncomingServerName;
				this.serializedSubscription = SerializedSubscription.FromSubscription(actualSubscription);
			}
			bool flag;
			if (stringBuilder.Length > 0)
			{
				inconsistencyInfo = stringBuilder.ToString();
				flag = false;
			}
			else
			{
				inconsistencyInfo = null;
				flag = true;
			}
			this.syncLogSession.LogDebugging((TSLID)42UL, actualSubscription.SubscriptionGuid, actualUserMailboxGuid, "Cache Entry Validation Result: {0}:'{1}' and Was Fixed: {2}", new object[]
			{
				flag,
				inconsistencyInfo,
				fix
			});
			return !flag;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000035F2 File Offset: 0x000017F2
		internal void UpdateSyncPhase(SyncPhase syncPhase)
		{
			this.syncPhase = syncPhase;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000035FC File Offset: 0x000017FC
		private void Deserialize(BinaryReader reader, byte version)
		{
			this.subscriptionGuid = this.ReadGuid(reader, false);
			this.subscriptionType = this.ReadEnumValue<AggregationSubscriptionType>(reader);
			if (!EnumValidator.IsValidValue<AggregationSubscriptionType>(this.subscriptionType) || this.subscriptionType == AggregationSubscriptionType.Unknown)
			{
				throw new SerializationException("AggregationSubscriptionType is invalid: " + this.subscriptionType);
			}
			string text = reader.ReadString();
			if (string.IsNullOrEmpty(text))
			{
				throw new SerializationException("Message ID is not valid.");
			}
			this.subscriptionMessageId = StoreObjectId.Deserialize(text);
			string value = reader.ReadString();
			if (string.IsNullOrEmpty(value))
			{
				throw new SerializationException("User Legacy DN is not valid.");
			}
			this.userLegacyDn = value;
			this.mailboxGuid = this.ReadGuid(reader, false);
			this.lastSyncCompletedTime = this.ReadDateTimeValue(reader);
			this.hubServerDispatched = reader.ReadString();
			this.firstOutstandingDispatchTime = this.ReadDateTimeValue(reader);
			this.lastSuccessfulDispatchTime = this.ReadDateTimeValue(reader);
			this.recoverySyncEnabled = reader.ReadBoolean();
			this.disabled = reader.ReadBoolean();
			if (!string.IsNullOrEmpty(this.hubServerDispatched) && (this.lastSuccessfulDispatchTime == null || this.firstOutstandingDispatchTime == null))
			{
				this.lastSuccessfulDispatchTime = null;
				this.firstOutstandingDispatchTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			this.diagnostics = reader.ReadString();
			this.tenantGuid = this.ReadGuid(reader, true);
			this.externalDirectoryOrgId = this.ReadGuid(reader, true);
			this.aggregationType = (AggregationType)reader.ReadInt32();
			this.incomingServerName = reader.ReadString();
			this.syncPhase = this.ReadEnumValue<SyncPhase>(reader);
			this.lastHubServerDispatched = reader.ReadString();
			this.serializedSubscription = SerializedSubscription.FromReader(reader);
			this.syncWatermarkIsInitialized = reader.ReadBoolean();
			this.syncWatermark = reader.ReadString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037B4 File Offset: 0x000019B4
		private Guid ReadGuid(BinaryReader reader, bool emptyIsValid)
		{
			string text = reader.ReadString();
			if (string.IsNullOrEmpty(text))
			{
				throw new SerializationException("Guid is not valid.");
			}
			Guid guid = new Guid(text);
			if (!emptyIsValid && object.Equals(guid, Guid.Empty))
			{
				throw new SerializationException("Guid is Guid.Empty.");
			}
			return guid;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000380C File Offset: 0x00001A0C
		private ExDateTime? ReadDateTimeValue(BinaryReader reader)
		{
			long num = reader.ReadInt64();
			if (num == 0L)
			{
				return null;
			}
			if (num < DateTime.MinValue.Ticks || num > DateTime.MaxValue.Ticks)
			{
				throw new SerializationException("Invalid dateTime in ticks.");
			}
			return new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, num));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000386C File Offset: 0x00001A6C
		private T ReadEnumValue<T>(BinaryReader reader) where T : struct
		{
			int value = (int)reader.ReadInt16();
			T t = (T)((object)Enum.ToObject(typeof(T), value));
			if (!EnumValidator.IsValidValue<T>(t))
			{
				throw new SerializationException(string.Format("{0} is invalid: {1}", typeof(T), t));
			}
			return t;
		}

		// Token: 0x0400001C RID: 28
		private readonly GlobalSyncLogSession syncLogSession;

		// Token: 0x0400001D RID: 29
		private Guid subscriptionGuid;

		// Token: 0x0400001E RID: 30
		private StoreObjectId subscriptionMessageId;

		// Token: 0x0400001F RID: 31
		private string userLegacyDn;

		// Token: 0x04000020 RID: 32
		private AggregationSubscriptionType subscriptionType;

		// Token: 0x04000021 RID: 33
		private AggregationType aggregationType;

		// Token: 0x04000022 RID: 34
		private SyncPhase syncPhase;

		// Token: 0x04000023 RID: 35
		private ExDateTime? lastSyncCompletedTime;

		// Token: 0x04000024 RID: 36
		private string incomingServerName;

		// Token: 0x04000025 RID: 37
		private Guid mailboxGuid;

		// Token: 0x04000026 RID: 38
		private SerializedSubscription serializedSubscription;

		// Token: 0x04000027 RID: 39
		private Guid tenantGuid;

		// Token: 0x04000028 RID: 40
		private Guid externalDirectoryOrgId;

		// Token: 0x04000029 RID: 41
		private string hubServerDispatched;

		// Token: 0x0400002A RID: 42
		private string lastHubServerDispatched;

		// Token: 0x0400002B RID: 43
		private ExDateTime? firstOutstandingDispatchTime;

		// Token: 0x0400002C RID: 44
		private ExDateTime? lastSuccessfulDispatchTime;

		// Token: 0x0400002D RID: 45
		private bool recoverySyncEnabled;

		// Token: 0x0400002E RID: 46
		private bool disabled;

		// Token: 0x0400002F RID: 47
		private string diagnostics;

		// Token: 0x04000030 RID: 48
		private string syncWatermark = string.Empty;

		// Token: 0x04000031 RID: 49
		private bool syncWatermarkIsInitialized;
	}
}
