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
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020003AC RID: 940
	internal class InboundProxyRoutedMailItem : IReadOnlyMailItem, ISystemProbeTraceable
	{
		// Token: 0x060029F8 RID: 10744 RVA: 0x000A7C8D File Offset: 0x000A5E8D
		public InboundProxyRoutedMailItem(TransportMailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.mailItem = mailItem;
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x000A7CAA File Offset: 0x000A5EAA
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x000A7CB6 File Offset: 0x000A5EB6
		public string Auth
		{
			get
			{
				return this.mailItem.Auth;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x000A7CC3 File Offset: 0x000A5EC3
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.mailItem.AuthMethod;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x000A7CD0 File Offset: 0x000A5ED0
		public BodyType BodyType
		{
			get
			{
				return this.mailItem.BodyType;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x000A7CDD File Offset: 0x000A5EDD
		public DateTime DateReceived
		{
			get
			{
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x000A7CEA File Offset: 0x000A5EEA
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x000A7CF6 File Offset: 0x000A5EF6
		public DeferReason DeferReason
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
			set
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000A7D02 File Offset: 0x000A5F02
		public MailDirectionality Directionality
		{
			get
			{
				return this.mailItem.Directionality;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000A7D0F File Offset: 0x000A5F0F
		public DsnFormat DsnFormat
		{
			get
			{
				return this.mailItem.DsnFormat;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		public DsnParameters DsnParameters
		{
			get
			{
				return this.mailItem.DsnParameters;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x000A7D29 File Offset: 0x000A5F29
		public string EnvId
		{
			get
			{
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000A7D36 File Offset: 0x000A5F36
		public IReadOnlyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.mailItem.ExtendedProperties;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x000A7D43 File Offset: 0x000A5F43
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				return this.mailItem.ExtensionToExpiryDuration;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000A7D50 File Offset: 0x000A5F50
		public RoutingAddress From
		{
			get
			{
				return this.mailItem.From;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x000A7D5D File Offset: 0x000A5F5D
		public string HeloDomain
		{
			get
			{
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000A7D6A File Offset: 0x000A5F6A
		public string InternetMessageId
		{
			get
			{
				return this.mailItem.InternetMessageId;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000A7D77 File Offset: 0x000A5F77
		public Guid NetworkMessageId
		{
			get
			{
				return this.mailItem.NetworkMessageId;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000A7D84 File Offset: 0x000A5F84
		public Guid SystemProbeId
		{
			get
			{
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000A7D91 File Offset: 0x000A5F91
		public bool IsProbe
		{
			get
			{
				return this.mailItem.IsProbe;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000A7D9E File Offset: 0x000A5F9E
		public string ProbeName
		{
			get
			{
				return this.mailItem.ProbeName;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002A0D RID: 10765 RVA: 0x000A7DAB File Offset: 0x000A5FAB
		public bool PersistProbeTrace
		{
			get
			{
				return this.mailItem.PersistProbeTrace;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x000A7DB8 File Offset: 0x000A5FB8
		public bool IsHeartbeat
		{
			get
			{
				return this.mailItem.IsHeartbeat;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000A7DC5 File Offset: 0x000A5FC5
		public bool IsActive
		{
			get
			{
				return this.mailItem.IsActive;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000A7DD2 File Offset: 0x000A5FD2
		public LatencyTracker LatencyTracker
		{
			get
			{
				return this.mailItem.LatencyTracker;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000A7DDF File Offset: 0x000A5FDF
		public byte[] LegacyXexch50Blob
		{
			get
			{
				return this.mailItem.LegacyXexch50Blob;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x000A7DEC File Offset: 0x000A5FEC
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				return this.mailItem.LockReasonHistory;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002A13 RID: 10771 RVA: 0x000A7DF9 File Offset: 0x000A5FF9
		public EmailMessage Message
		{
			get
			{
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x000A7E06 File Offset: 0x000A6006
		public string Oorg
		{
			get
			{
				return this.mailItem.Oorg;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002A15 RID: 10773 RVA: 0x000A7E13 File Offset: 0x000A6013
		public string ExoAccountForest
		{
			get
			{
				return this.mailItem.ExoAccountForest;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x000A7E20 File Offset: 0x000A6020
		public string ExoTenantContainer
		{
			get
			{
				return this.mailItem.ExoTenantContainer;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x000A7E2D File Offset: 0x000A602D
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.mailItem.ExternalOrganizationId;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000A7E3A File Offset: 0x000A603A
		public RiskLevel RiskLevel
		{
			get
			{
				return this.mailItem.RiskLevel;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000A7E47 File Offset: 0x000A6047
		public DeliveryPriority Priority
		{
			get
			{
				return this.mailItem.Priority;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000A7E54 File Offset: 0x000A6054
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000A7E61 File Offset: 0x000A6061
		public MimeDocument MimeDocument
		{
			get
			{
				return this.mailItem.MimeDocument;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000A7E6E File Offset: 0x000A606E
		public string MimeFrom
		{
			get
			{
				return this.mailItem.MimeFrom;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000A7E7B File Offset: 0x000A607B
		public RoutingAddress MimeSender
		{
			get
			{
				return this.mailItem.MimeSender;
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000A7E88 File Offset: 0x000A6088
		public long MimeSize
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000A7E8C File Offset: 0x000A608C
		public OrganizationId OrganizationId
		{
			get
			{
				return this.mailItem.OrganizationId;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000A7E99 File Offset: 0x000A6099
		public RoutingAddress OriginalFrom
		{
			get
			{
				return this.mailItem.OriginalFrom;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x000A7EA6 File Offset: 0x000A60A6
		public int PoisonCount
		{
			get
			{
				return this.mailItem.PoisonCount;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000A7EB3 File Offset: 0x000A60B3
		public int PoisonForRemoteCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002A23 RID: 10787 RVA: 0x000A7EBA File Offset: 0x000A60BA
		public bool IsPoison
		{
			get
			{
				return this.mailItem.IsPoison;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x000A7EC7 File Offset: 0x000A60C7
		public string ReceiveConnectorName
		{
			get
			{
				return this.mailItem.ReceiveConnectorName;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002A25 RID: 10789 RVA: 0x000A7ED4 File Offset: 0x000A60D4
		public IReadOnlyMailRecipientCollection Recipients
		{
			get
			{
				return this.mailItem.Recipients;
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x000A7EE1 File Offset: 0x000A60E1
		public IEnumerable<MailRecipient> RecipientList
		{
			get
			{
				return this.mailItem.Recipients.All;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000A7EF3 File Offset: 0x000A60F3
		public long RecordId
		{
			get
			{
				return this.mailItem.RecordId;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000A7F00 File Offset: 0x000A6100
		public bool RetryDeliveryIfRejected
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000A7F0C File Offset: 0x000A610C
		public MimePart RootPart
		{
			get
			{
				return this.mailItem.RootPart;
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000A7F19 File Offset: 0x000A6119
		public int Scl
		{
			get
			{
				return this.mailItem.Scl;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000A7F26 File Offset: 0x000A6126
		public Guid ShadowMessageId
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000A7F32 File Offset: 0x000A6132
		public string ShadowServerContext
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x000A7F3E File Offset: 0x000A613E
		public string ShadowServerDiscardId
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x000A7F4A File Offset: 0x000A614A
		public LazyBytes FastIndexBlob
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000A7F56 File Offset: 0x000A6156
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.mailItem.SourceIPAddress;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000A7F63 File Offset: 0x000A6163
		public string Subject
		{
			get
			{
				return this.mailItem.Subject;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000A7F70 File Offset: 0x000A6170
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x000A7F7C File Offset: 0x000A617C
		public bool SuppressBodyInDsn
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
			set
			{
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x000A7F7E File Offset: 0x000A617E
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000A7F8A File Offset: 0x000A618A
		public void CacheTransportSettings()
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000A7F96 File Offset: 0x000A6196
		public bool IsJournalReport()
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000A7FA2 File Offset: 0x000A61A2
		public bool IsPfReplica()
		{
			return this.mailItem.IsPfReplica();
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000A7FAF File Offset: 0x000A61AF
		public bool IsShadowed()
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000A7FBB File Offset: 0x000A61BB
		public bool IsDelayedAck()
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000A7FC7 File Offset: 0x000A61C7
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000A7FD3 File Offset: 0x000A61D3
		public Stream OpenMimeReadStream()
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000A7FDF File Offset: 0x000A61DF
		public Stream OpenMimeReadStream(bool downConvert)
		{
			throw new NotSupportedException("This should not be called on Front End");
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000A7FEB File Offset: 0x000A61EB
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.Delivery, connectComponent, this.LatencyTracker);
			LatencyTracker.BeginTrackLatency(LatencyComponent.Delivery, this.LatencyTracker);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000A8009 File Offset: 0x000A6209
		public void FinalizeDeliveryLatencyTracking(LatencyComponent deliveryComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.Delivery, deliveryComponent, this.LatencyTracker);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000A801A File Offset: 0x000A621A
		public void AddDsnParameters(string key, object value)
		{
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000A801C File Offset: 0x000A621C
		public void IncrementPoisonForRemoteCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000A8023 File Offset: 0x000A6223
		public void ReleaseFromActive()
		{
			this.mailItem.ReleaseFromActive();
		}

		// Token: 0x0400156C RID: 5484
		private TransportMailItem mailItem;
	}
}
