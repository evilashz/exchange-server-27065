using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000023 RID: 35
	internal class SubmissionReadOnlyMailItem : IReadOnlyMailItem, ISystemProbeTraceable
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000096B3 File Offset: 0x000078B3
		public SubmissionReadOnlyMailItem(TransportMailItem mailItem, MailItemType mailItemType)
		{
			this.mailItem = mailItem;
			this.mailItemType = mailItemType;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000096C9 File Offset: 0x000078C9
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				return this.mailItem.ADRecipientCache;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000096D6 File Offset: 0x000078D6
		public string Auth
		{
			get
			{
				return this.mailItem.Auth;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000096E3 File Offset: 0x000078E3
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.mailItem.AuthMethod;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000096F0 File Offset: 0x000078F0
		public BodyType BodyType
		{
			get
			{
				return this.mailItem.BodyType;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000096FD File Offset: 0x000078FD
		public DateTime DateReceived
		{
			get
			{
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000970A File Offset: 0x0000790A
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00009717 File Offset: 0x00007917
		public DeferReason DeferReason
		{
			get
			{
				return this.mailItem.DeferReason;
			}
			set
			{
				this.mailItem.DeferReason = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00009725 File Offset: 0x00007925
		public MailDirectionality Directionality
		{
			get
			{
				return this.mailItem.Directionality;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00009732 File Offset: 0x00007932
		public string ExoAccountForest
		{
			get
			{
				return this.mailItem.ExoAccountForest;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000973F File Offset: 0x0000793F
		public string ExoTenantContainer
		{
			get
			{
				return this.mailItem.ExoTenantContainer;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000974C File Offset: 0x0000794C
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.mailItem.ExternalOrganizationId;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00009759 File Offset: 0x00007959
		public DsnFormat DsnFormat
		{
			get
			{
				return this.mailItem.DsnFormat;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00009766 File Offset: 0x00007966
		public DsnParameters DsnParameters
		{
			get
			{
				return this.mailItem.DsnParameters;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009773 File Offset: 0x00007973
		public string EnvId
		{
			get
			{
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00009780 File Offset: 0x00007980
		public IReadOnlyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.mailItem.ExtendedProperties;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000978D File Offset: 0x0000798D
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				return this.mailItem.ExtensionToExpiryDuration;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000979A File Offset: 0x0000799A
		public RoutingAddress From
		{
			get
			{
				return this.mailItem.From;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000097A7 File Offset: 0x000079A7
		public string HeloDomain
		{
			get
			{
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000097B4 File Offset: 0x000079B4
		public string InternetMessageId
		{
			get
			{
				return this.mailItem.InternetMessageId;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000097C1 File Offset: 0x000079C1
		public Guid NetworkMessageId
		{
			get
			{
				return this.mailItem.NetworkMessageId;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000097CE File Offset: 0x000079CE
		public Guid SystemProbeId
		{
			get
			{
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000097DB File Offset: 0x000079DB
		public bool IsProbe
		{
			get
			{
				return this.mailItem.IsProbe;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000097E8 File Offset: 0x000079E8
		public string ProbeName
		{
			get
			{
				return this.mailItem.ProbeName;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000097F5 File Offset: 0x000079F5
		public bool PersistProbeTrace
		{
			get
			{
				return this.mailItem.PersistProbeTrace;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00009802 File Offset: 0x00007A02
		public bool IsHeartbeat
		{
			get
			{
				return this.mailItem.IsHeartbeat;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000980F File Offset: 0x00007A0F
		public bool IsActive
		{
			get
			{
				return this.mailItem.IsActive;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000981C File Offset: 0x00007A1C
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				return this.mailItem.LockReasonHistory;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00009829 File Offset: 0x00007A29
		public LatencyTracker LatencyTracker
		{
			get
			{
				return this.mailItem.LatencyTracker;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00009836 File Offset: 0x00007A36
		public byte[] LegacyXexch50Blob
		{
			get
			{
				return this.mailItem.LegacyXexch50Blob;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009843 File Offset: 0x00007A43
		public EmailMessage Message
		{
			get
			{
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00009850 File Offset: 0x00007A50
		public string Oorg
		{
			get
			{
				return this.mailItem.Oorg;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000985D File Offset: 0x00007A5D
		public RiskLevel RiskLevel
		{
			get
			{
				return this.mailItem.RiskLevel;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000986A File Offset: 0x00007A6A
		public DeliveryPriority Priority
		{
			get
			{
				return this.mailItem.Priority;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00009877 File Offset: 0x00007A77
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00009884 File Offset: 0x00007A84
		public TransportMailItem MailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000988C File Offset: 0x00007A8C
		public MailItemType MailItemType
		{
			get
			{
				return this.mailItemType;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00009894 File Offset: 0x00007A94
		public MimeDocument MimeDocument
		{
			get
			{
				return this.mailItem.MimeDocument;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000098A1 File Offset: 0x00007AA1
		public string MimeFrom
		{
			get
			{
				return this.mailItem.MimeFrom;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000098AE File Offset: 0x00007AAE
		public RoutingAddress MimeSender
		{
			get
			{
				return this.mailItem.MimeSender;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000098BB File Offset: 0x00007ABB
		public long MimeSize
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000098BF File Offset: 0x00007ABF
		public OrganizationId OrganizationId
		{
			get
			{
				return this.mailItem.OrganizationId;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000098CC File Offset: 0x00007ACC
		public RoutingAddress OriginalFrom
		{
			get
			{
				return this.mailItem.OriginalFrom;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000098D9 File Offset: 0x00007AD9
		public int PoisonCount
		{
			get
			{
				return this.mailItem.PoisonCount;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000098E6 File Offset: 0x00007AE6
		public int PoisonForRemoteCount
		{
			get
			{
				return this.poisonForRemoteCount;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000098EE File Offset: 0x00007AEE
		public bool IsPoison
		{
			get
			{
				return this.mailItem.IsPoison;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000098FB File Offset: 0x00007AFB
		public string ReceiveConnectorName
		{
			get
			{
				return this.mailItem.ReceiveConnectorName;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00009908 File Offset: 0x00007B08
		public IReadOnlyMailRecipientCollection Recipients
		{
			get
			{
				return this.mailItem.Recipients;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009915 File Offset: 0x00007B15
		public IEnumerable<MailRecipient> RecipientList
		{
			get
			{
				return this.mailItem.Recipients.All;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00009927 File Offset: 0x00007B27
		public long RecordId
		{
			get
			{
				return this.mailItem.RecordId;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00009934 File Offset: 0x00007B34
		public bool RetryDeliveryIfRejected
		{
			get
			{
				throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00009940 File Offset: 0x00007B40
		public MimePart RootPart
		{
			get
			{
				return this.mailItem.RootPart;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000994D File Offset: 0x00007B4D
		public int Scl
		{
			get
			{
				return this.mailItem.Scl;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000995A File Offset: 0x00007B5A
		public Guid ShadowMessageId
		{
			get
			{
				return this.mailItem.ShadowMessageId;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00009967 File Offset: 0x00007B67
		public string ShadowServerContext
		{
			get
			{
				return this.mailItem.ShadowServerContext;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009974 File Offset: 0x00007B74
		public string ShadowServerDiscardId
		{
			get
			{
				return this.mailItem.ShadowServerDiscardId;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009981 File Offset: 0x00007B81
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.mailItem.SourceIPAddress;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000998E File Offset: 0x00007B8E
		public string Subject
		{
			get
			{
				return this.mailItem.Subject;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000999B File Offset: 0x00007B9B
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000099A8 File Offset: 0x00007BA8
		public bool SuppressBodyInDsn
		{
			get
			{
				return this.mailItem.SuppressBodyInDsn;
			}
			set
			{
				this.mailItem.SuppressBodyInDsn = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000099B6 File Offset: 0x00007BB6
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000099C3 File Offset: 0x00007BC3
		public LazyBytes FastIndexBlob
		{
			get
			{
				return this.mailItem.FastIndexBlob;
			}
			set
			{
				throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000099CF File Offset: 0x00007BCF
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				return this.mailItem.TransportSettings;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000099DC File Offset: 0x00007BDC
		public void CacheTransportSettings()
		{
			throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000099E8 File Offset: 0x00007BE8
		public bool IsJournalReport()
		{
			throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000099F4 File Offset: 0x00007BF4
		public bool IsPfReplica()
		{
			return this.mailItem.IsPfReplica();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009A01 File Offset: 0x00007C01
		public bool IsShadowed()
		{
			throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009A0D File Offset: 0x00007C0D
		public bool IsDelayedAck()
		{
			throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00009A19 File Offset: 0x00007C19
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009A25 File Offset: 0x00007C25
		public Stream OpenMimeReadStream()
		{
			return this.mailItem.OpenMimeReadStream();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009A32 File Offset: 0x00007C32
		public Stream OpenMimeReadStream(bool downConvert)
		{
			return this.mailItem.OpenMimeReadStream(downConvert);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009A40 File Offset: 0x00007C40
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtp, connectComponent, this.LatencyTracker);
			LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtp, this.LatencyTracker);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009A5E File Offset: 0x00007C5E
		public void AddDsnParameters(string key, object value)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00009A60 File Offset: 0x00007C60
		public void IncrementPoisonForRemoteCount()
		{
			this.poisonForRemoteCount++;
		}

		// Token: 0x04000098 RID: 152
		private TransportMailItem mailItem;

		// Token: 0x04000099 RID: 153
		private MailItemType mailItemType;

		// Token: 0x0400009A RID: 154
		private int poisonForRemoteCount;
	}
}
