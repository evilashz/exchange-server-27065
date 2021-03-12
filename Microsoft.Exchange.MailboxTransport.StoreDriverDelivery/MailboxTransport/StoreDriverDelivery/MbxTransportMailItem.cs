using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000002 RID: 2
	internal class MbxTransportMailItem : IReadOnlyMailItem, ISystemProbeTraceable, IReadOnlyMailRecipientCollection, IEnumerable<MailRecipient>, IEnumerable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MbxTransportMailItem(TransportMailItem mailItem)
		{
			this.mailItem = mailItem;
			this.Initialize();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E5 File Offset: 0x000002E5
		public int MailItemRecipientCount
		{
			get
			{
				return this.mailItem.Recipients.Count;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F7 File Offset: 0x000002F7
		public DateTime RoutingTimeStamp
		{
			get
			{
				return this.mailItem.RoutingTimeStamp;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public DateTime ClientSubmitTime
		{
			get
			{
				DateTime result;
				this.mailItem.Message.TryGetMapiProperty<DateTime>(TnefPropertyTag.ClientSubmitTime, out result);
				return result;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000212A File Offset: 0x0000032A
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				return this.mailItem.ADRecipientCache;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002137 File Offset: 0x00000337
		public string Auth
		{
			get
			{
				return this.mailItem.Auth;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.mailItem.AuthMethod;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002151 File Offset: 0x00000351
		public BodyType BodyType
		{
			get
			{
				return this.mailItem.BodyType;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000215E File Offset: 0x0000035E
		public DateTime DateReceived
		{
			get
			{
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000216B File Offset: 0x0000036B
		public DeferReason DeferReason
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.DeferReason");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002177 File Offset: 0x00000377
		public MailDirectionality Directionality
		{
			get
			{
				return this.mailItem.Directionality;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002184 File Offset: 0x00000384
		public string ExoAccountForest
		{
			get
			{
				return this.mailItem.ExoAccountForest;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002191 File Offset: 0x00000391
		public string ExoTenantContainer
		{
			get
			{
				return this.mailItem.ExoTenantContainer;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000219E File Offset: 0x0000039E
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.mailItem.ExternalOrganizationId;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021AB File Offset: 0x000003AB
		public DsnFormat DsnFormat
		{
			get
			{
				return this.mailItem.DsnFormat;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021B8 File Offset: 0x000003B8
		public DsnParameters DsnParameters
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021BB File Offset: 0x000003BB
		public string EnvId
		{
			get
			{
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021C8 File Offset: 0x000003C8
		public IReadOnlyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.mailItem.ExtendedProperties;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021D5 File Offset: 0x000003D5
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				return this.mailItem.ExtensionToExpiryDuration;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021E2 File Offset: 0x000003E2
		public RoutingAddress From
		{
			get
			{
				return this.mailItem.From;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021EF File Offset: 0x000003EF
		public string HeloDomain
		{
			get
			{
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021FC File Offset: 0x000003FC
		public string InternetMessageId
		{
			get
			{
				return this.mailItem.InternetMessageId;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002209 File Offset: 0x00000409
		public Guid NetworkMessageId
		{
			get
			{
				return this.mailItem.NetworkMessageId;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002216 File Offset: 0x00000416
		public Guid SystemProbeId
		{
			get
			{
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002223 File Offset: 0x00000423
		public bool IsProbe
		{
			get
			{
				return this.mailItem.IsProbe;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002230 File Offset: 0x00000430
		public string ProbeName
		{
			get
			{
				return this.mailItem.ProbeName;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000223D File Offset: 0x0000043D
		public bool PersistProbeTrace
		{
			get
			{
				return this.mailItem.PersistProbeTrace;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000224A File Offset: 0x0000044A
		public bool IsHeartbeat
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.IsHeartbeat");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002256 File Offset: 0x00000456
		public bool IsActive
		{
			get
			{
				return this.mailItem.IsActive;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002263 File Offset: 0x00000463
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				return this.mailItem.LockReasonHistory;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002270 File Offset: 0x00000470
		public LatencyTracker LatencyTracker
		{
			get
			{
				return this.mailItem.LatencyTracker;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000227D File Offset: 0x0000047D
		public byte[] LegacyXexch50Blob
		{
			get
			{
				return this.mailItem.LegacyXexch50Blob;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000228A File Offset: 0x0000048A
		public EmailMessage Message
		{
			get
			{
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002297 File Offset: 0x00000497
		public string Oorg
		{
			get
			{
				return this.mailItem.Oorg;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022A4 File Offset: 0x000004A4
		public RiskLevel RiskLevel
		{
			get
			{
				return this.mailItem.RiskLevel;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022B1 File Offset: 0x000004B1
		public DeliveryPriority Priority
		{
			get
			{
				return this.mailItem.Priority;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022BE File Offset: 0x000004BE
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000022CB File Offset: 0x000004CB
		public MimeDocument MimeDocument
		{
			get
			{
				return this.mailItem.MimeDocument;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022D8 File Offset: 0x000004D8
		public string MimeFrom
		{
			get
			{
				return this.mailItem.MimeFrom;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022E5 File Offset: 0x000004E5
		public RoutingAddress MimeSender
		{
			get
			{
				return this.mailItem.MimeSender;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022F2 File Offset: 0x000004F2
		public long MimeSize
		{
			get
			{
				return this.mailItem.MimeSize;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022FF File Offset: 0x000004FF
		public OrganizationId OrganizationId
		{
			get
			{
				return this.mailItem.OrganizationId;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000230C File Offset: 0x0000050C
		public RoutingAddress OriginalFrom
		{
			get
			{
				return this.mailItem.OriginalFrom;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002319 File Offset: 0x00000519
		public int PoisonCount
		{
			get
			{
				return this.mailItem.PoisonCount;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002326 File Offset: 0x00000526
		public int PoisonForRemoteCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000232D File Offset: 0x0000052D
		public bool IsPoison
		{
			get
			{
				return this.mailItem.IsPoison;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000233A File Offset: 0x0000053A
		public string ReceiveConnectorName
		{
			get
			{
				return this.mailItem.ReceiveConnectorName;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002347 File Offset: 0x00000547
		public IReadOnlyMailRecipientCollection Recipients
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000234A File Offset: 0x0000054A
		public long RecordId
		{
			get
			{
				return this.recordId;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002352 File Offset: 0x00000552
		public bool RetryDeliveryIfRejected
		{
			get
			{
				return this.mailItem.RetryDeliveryIfRejected;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000235F File Offset: 0x0000055F
		public MimePart RootPart
		{
			get
			{
				return this.mailItem.RootPart;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000236C File Offset: 0x0000056C
		public int Scl
		{
			get
			{
				return this.mailItem.Scl;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002379 File Offset: 0x00000579
		public Guid ShadowMessageId
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.ShadowMessageId");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002385 File Offset: 0x00000585
		public string ShadowServerContext
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.ShadowServerContext");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002391 File Offset: 0x00000591
		public string ShadowServerDiscardId
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.ShadowServerDiscardId");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000239D File Offset: 0x0000059D
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.mailItem.SourceIPAddress;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000023AA File Offset: 0x000005AA
		public string Subject
		{
			get
			{
				return this.mailItem.Subject;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000023B7 File Offset: 0x000005B7
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				return this.mailItem.TransportSettings;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000023C4 File Offset: 0x000005C4
		public bool Deferred
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.Deferred");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000023D0 File Offset: 0x000005D0
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000023DC File Offset: 0x000005DC
		public bool SuppressBodyInDsn
		{
			get
			{
				throw new NotImplementedException("MbxTransportMailItem.SuppressBodyInDsn");
			}
			set
			{
				throw new NotImplementedException("MbxTransportMailItem.SuppressBodyInDsn");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000023E8 File Offset: 0x000005E8
		int IReadOnlyMailRecipientCollection.Count
		{
			get
			{
				return this.mailItem.Recipients.Count;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000023FA File Offset: 0x000005FA
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002402 File Offset: 0x00000602
		internal DateTime SessionStartTime { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000240B File Offset: 0x0000060B
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002413 File Offset: 0x00000613
		internal Guid DatabaseGuid { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000241C File Offset: 0x0000061C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002424 File Offset: 0x00000624
		internal string DatabaseName { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000242D File Offset: 0x0000062D
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002435 File Offset: 0x00000635
		internal MessageAction MessageLevelAction { get; set; }

		// Token: 0x17000041 RID: 65
		MailRecipient IReadOnlyMailRecipientCollection.this[int index]
		{
			get
			{
				return this.mailItem.Recipients[index];
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002451 File Offset: 0x00000651
		IEnumerable<MailRecipient> IReadOnlyMailRecipientCollection.All
		{
			get
			{
				return this.mailItem.Recipients;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000245E File Offset: 0x0000065E
		IEnumerable<MailRecipient> IReadOnlyMailRecipientCollection.AllUnprocessed
		{
			get
			{
				return this.mailItem.Recipients.AllUnprocessed;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002470 File Offset: 0x00000670
		public LazyBytes FastIndexBlob
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000247C File Offset: 0x0000067C
		public void CacheTransportSettings()
		{
			this.mailItem.CacheTransportSettings();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002489 File Offset: 0x00000689
		public void IncrementPoisonForRemoteCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002490 File Offset: 0x00000690
		public bool IsJournalReport()
		{
			return this.mailItem.IsJournalReport();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000249D File Offset: 0x0000069D
		public bool IsPfReplica()
		{
			return this.mailItem.IsPfReplica();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000024AA File Offset: 0x000006AA
		public bool IsShadowed()
		{
			return this.mailItem.IsShadowed();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000024B7 File Offset: 0x000006B7
		public bool IsDelayedAck()
		{
			return this.mailItem.IsDelayedAck();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000024C4 File Offset: 0x000006C4
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			TransportMailItem result;
			lock (this.mailItem)
			{
				result = this.mailItem.NewCloneWithoutRecipients(shareRecipientCache, this.mailItem.LatencyTracker, null);
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002518 File Offset: 0x00000718
		public Stream OpenMimeReadStream()
		{
			return this.mailItem.OpenMimeReadStream();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002525 File Offset: 0x00000725
		public void FinalizeDeliveryLatencyTracking(LatencyComponent deliveryComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.Delivery, deliveryComponent, this.mailItem.LatencyTracker);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000253B File Offset: 0x0000073B
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			LatencyTracker.EndAndBeginTrackLatency(connectComponent, LatencyComponent.Delivery, this.mailItem.LatencyTracker);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002551 File Offset: 0x00000751
		public Stream OpenMimeReadStream(bool downConvert)
		{
			return this.mailItem.OpenMimeReadStream(downConvert);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000255F File Offset: 0x0000075F
		public void AddDsnParameters(string key, object value)
		{
			throw new ArgumentException("Not implemented in MbxTransportMailItem");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000256B File Offset: 0x0000076B
		public virtual void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.recipientResponses.Enqueue(new AckStatusAndResponse(ackStatus, smtpResponse));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000257F File Offset: 0x0000077F
		public IEnumerator<MailRecipient> GetEnumerator()
		{
			return this.mailItem.Recipients.GetEnumerator();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002591 File Offset: 0x00000791
		public void AddAgentInfo(string agentName, string eventName, List<KeyValuePair<string, string>> data)
		{
			this.mailItem.AddAgentInfo(agentName, eventName, data);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000025A1 File Offset: 0x000007A1
		public void TrackAgentInfo()
		{
			MessageTrackingLog.TrackAgentInfo(this.mailItem);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000025AE File Offset: 0x000007AE
		public SmtpResponse Response
		{
			get
			{
				return this.mailItemResponse;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000025B6 File Offset: 0x000007B6
		bool IReadOnlyMailRecipientCollection.Contains(MailRecipient item)
		{
			return this.mailItem.Recipients.Contains(item);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000025C9 File Offset: 0x000007C9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000025D1 File Offset: 0x000007D1
		internal virtual MailRecipient GetNextRecipient()
		{
			Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.FaultInjectionTracer.TraceTest(2883988797U);
			if (this.recipientEnumerator.MoveNext())
			{
				return this.recipientEnumerator.Current;
			}
			return null;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000025FC File Offset: 0x000007FC
		internal IList<MailRecipient> GetRecipients()
		{
			List<MailRecipient> list = new List<MailRecipient>(this.Recipients.Count);
			foreach (MailRecipient item in this.Recipients)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000265C File Offset: 0x0000085C
		internal virtual void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails ackDetails, TimeSpan? retryInterval, string messageTrackingSourceContext)
		{
			if (ackStatus == AckStatus.Retry && smtpResponse.SmtpResponseType != SmtpResponseType.TransientError)
			{
				smtpResponse = TransportMailItem.ReplaceFailWithRetryResponse(smtpResponse);
			}
			this.mailItem.Ack(ackStatus, smtpResponse, this.GetRecipients(), this.recipientResponses);
			this.FinalizeDeliveryLatencyTracking(LatencyComponent.StoreDriverDelivery);
			this.mailItemResponse = SmtpResponseGenerator.GenerateResponse(this.MessageLevelAction, this.Recipients, smtpResponse, retryInterval);
			foreach (MailRecipient mailRecipient in this.Recipients)
			{
				if (mailRecipient.ExtendedProperties.Contains("ExceptionAgentName"))
				{
					mailRecipient.ExtendedProperties.Remove("ExceptionAgentName");
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002718 File Offset: 0x00000918
		private void Initialize()
		{
			this.recipientResponses = new Queue<AckStatusAndResponse>();
			this.recipientEnumerator = this.mailItem.Recipients.GetEnumerator();
			this.mailItemResponse = SmtpResponse.Empty;
			this.RestoreInternalMessageId();
			this.MessageLevelAction = MessageAction.Success;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002754 File Offset: 0x00000954
		private void RestoreInternalMessageId()
		{
			ulong num;
			if (this.ExtendedProperties.TryGetValue<ulong>("Microsoft.Exchange.Transport.MailboxTransport.InternalMessageId", out num))
			{
				this.recordId = (long)num;
				return;
			}
			MbxTransportMailItem.Diag.TraceWarning<string>(0L, "Failed to get RecordId from Extended properties for message {0}.", this.InternetMessageId);
			this.recordId = 0L;
		}

		// Token: 0x04000001 RID: 1
		private static readonly Trace Diag = Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery.ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x04000002 RID: 2
		private IEnumerator<MailRecipient> recipientEnumerator;

		// Token: 0x04000003 RID: 3
		private Queue<AckStatusAndResponse> recipientResponses;

		// Token: 0x04000004 RID: 4
		private TransportMailItem mailItem;

		// Token: 0x04000005 RID: 5
		private SmtpResponse mailItemResponse;

		// Token: 0x04000006 RID: 6
		private long recordId;
	}
}
