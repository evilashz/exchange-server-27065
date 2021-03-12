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
	// Token: 0x0200038E RID: 910
	internal class ShadowRoutedMailItem : IReadOnlyMailItem, ISystemProbeTraceable
	{
		// Token: 0x060027ED RID: 10221 RVA: 0x0009C89C File Offset: 0x0009AA9C
		public ShadowRoutedMailItem(TransportMailItem mailItem)
		{
			this.mailItem = mailItem;
			this.recipients = new MailRecipientCollection(mailItem, new List<MailRecipient>(mailItem.Recipients.All));
			this.bodyType = this.mailItem.BodyType;
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x0009C8D8 File Offset: 0x0009AAD8
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x0009C8E4 File Offset: 0x0009AAE4
		public string Auth
		{
			get
			{
				return this.mailItem.Auth;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x0009C8F1 File Offset: 0x0009AAF1
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.mailItem.AuthMethod;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x0009C8FE File Offset: 0x0009AAFE
		public BodyType BodyType
		{
			get
			{
				return this.bodyType;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x0009C906 File Offset: 0x0009AB06
		public DateTime DateReceived
		{
			get
			{
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x0009C913 File Offset: 0x0009AB13
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x0009C91F File Offset: 0x0009AB1F
		public DeferReason DeferReason
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
			set
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x0009C92B File Offset: 0x0009AB2B
		public MailDirectionality Directionality
		{
			get
			{
				return this.mailItem.Directionality;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x0009C938 File Offset: 0x0009AB38
		public DsnFormat DsnFormat
		{
			get
			{
				return this.mailItem.DsnFormat;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x0009C945 File Offset: 0x0009AB45
		public DsnParameters DsnParameters
		{
			get
			{
				return this.mailItem.DsnParameters;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060027F8 RID: 10232 RVA: 0x0009C952 File Offset: 0x0009AB52
		public string EnvId
		{
			get
			{
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x0009C95F File Offset: 0x0009AB5F
		public IReadOnlyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.mailItem.ExtendedProperties;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x0009C96C File Offset: 0x0009AB6C
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				return this.mailItem.ExtensionToExpiryDuration;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x0009C979 File Offset: 0x0009AB79
		public RoutingAddress From
		{
			get
			{
				return this.mailItem.From;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x0009C986 File Offset: 0x0009AB86
		public string HeloDomain
		{
			get
			{
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x0009C993 File Offset: 0x0009AB93
		public string InternetMessageId
		{
			get
			{
				return this.mailItem.InternetMessageId;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x0009C9A0 File Offset: 0x0009ABA0
		public Guid NetworkMessageId
		{
			get
			{
				return this.mailItem.NetworkMessageId;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x0009C9AD File Offset: 0x0009ABAD
		public Guid SystemProbeId
		{
			get
			{
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x0009C9BA File Offset: 0x0009ABBA
		public bool IsProbe
		{
			get
			{
				return this.mailItem.IsProbe;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002801 RID: 10241 RVA: 0x0009C9C7 File Offset: 0x0009ABC7
		public string ProbeName
		{
			get
			{
				return this.mailItem.ProbeName;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x0009C9D4 File Offset: 0x0009ABD4
		public bool PersistProbeTrace
		{
			get
			{
				return this.mailItem.PersistProbeTrace;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x0009C9E1 File Offset: 0x0009ABE1
		public bool IsHeartbeat
		{
			get
			{
				return this.mailItem.IsHeartbeat;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x0009C9EE File Offset: 0x0009ABEE
		public bool IsActive
		{
			get
			{
				return this.mailItem.IsActive;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002805 RID: 10245 RVA: 0x0009C9FB File Offset: 0x0009ABFB
		public LatencyTracker LatencyTracker
		{
			get
			{
				return this.mailItem.LatencyTracker;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x0009CA08 File Offset: 0x0009AC08
		public byte[] LegacyXexch50Blob
		{
			get
			{
				return this.mailItem.LegacyXexch50Blob;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x0009CA15 File Offset: 0x0009AC15
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x0009CA18 File Offset: 0x0009AC18
		public EmailMessage Message
		{
			get
			{
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x0009CA25 File Offset: 0x0009AC25
		public string Oorg
		{
			get
			{
				return this.mailItem.Oorg;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x0009CA32 File Offset: 0x0009AC32
		public string ExoAccountForest
		{
			get
			{
				return this.mailItem.ExoAccountForest;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x0009CA3F File Offset: 0x0009AC3F
		public string ExoTenantContainer
		{
			get
			{
				return this.mailItem.ExoTenantContainer;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x0009CA4C File Offset: 0x0009AC4C
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.mailItem.ExternalOrganizationId;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600280D RID: 10253 RVA: 0x0009CA59 File Offset: 0x0009AC59
		public RiskLevel RiskLevel
		{
			get
			{
				return this.mailItem.RiskLevel;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x0009CA66 File Offset: 0x0009AC66
		public DeliveryPriority Priority
		{
			get
			{
				return this.mailItem.Priority;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x0600280F RID: 10255 RVA: 0x0009CA73 File Offset: 0x0009AC73
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x0009CA80 File Offset: 0x0009AC80
		public MimeDocument MimeDocument
		{
			get
			{
				return this.mailItem.MimeDocument;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x0009CA8D File Offset: 0x0009AC8D
		public string MimeFrom
		{
			get
			{
				return this.mailItem.MimeFrom;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x0009CA9A File Offset: 0x0009AC9A
		public RoutingAddress MimeSender
		{
			get
			{
				return this.mailItem.MimeSender;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x0009CAA7 File Offset: 0x0009ACA7
		public long MimeSize
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x0009CAAB File Offset: 0x0009ACAB
		public OrganizationId OrganizationId
		{
			get
			{
				return this.mailItem.OrganizationId;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x0009CAB8 File Offset: 0x0009ACB8
		public RoutingAddress OriginalFrom
		{
			get
			{
				return this.mailItem.OriginalFrom;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x0009CAC5 File Offset: 0x0009ACC5
		public int PoisonCount
		{
			get
			{
				return this.mailItem.PoisonCount;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002817 RID: 10263 RVA: 0x0009CAD2 File Offset: 0x0009ACD2
		public int PoisonForRemoteCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x0009CAD9 File Offset: 0x0009ACD9
		public bool IsPoison
		{
			get
			{
				return this.mailItem.IsPoison;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002819 RID: 10265 RVA: 0x0009CAE6 File Offset: 0x0009ACE6
		public string ReceiveConnectorName
		{
			get
			{
				return this.mailItem.ReceiveConnectorName;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x0009CAF3 File Offset: 0x0009ACF3
		public IReadOnlyMailRecipientCollection Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x0009CAFB File Offset: 0x0009ACFB
		public IEnumerable<MailRecipient> RecipientList
		{
			get
			{
				return this.recipients.All;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x0009CB08 File Offset: 0x0009AD08
		public long RecordId
		{
			get
			{
				return this.mailItem.RecordId;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x0009CB15 File Offset: 0x0009AD15
		public bool RetryDeliveryIfRejected
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x0009CB21 File Offset: 0x0009AD21
		public MimePart RootPart
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x0009CB2D File Offset: 0x0009AD2D
		public int Scl
		{
			get
			{
				return this.mailItem.Scl;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x0009CB3A File Offset: 0x0009AD3A
		public Guid ShadowMessageId
		{
			get
			{
				return this.mailItem.ShadowMessageId;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x0009CB47 File Offset: 0x0009AD47
		public string ShadowServerContext
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002822 RID: 10274 RVA: 0x0009CB53 File Offset: 0x0009AD53
		public string ShadowServerDiscardId
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x0009CB5F File Offset: 0x0009AD5F
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.mailItem.SourceIPAddress;
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002824 RID: 10276 RVA: 0x0009CB6C File Offset: 0x0009AD6C
		public string Subject
		{
			get
			{
				return this.mailItem.Subject;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x0009CB79 File Offset: 0x0009AD79
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x0009CB85 File Offset: 0x0009AD85
		public bool SuppressBodyInDsn
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
			set
			{
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x0009CB87 File Offset: 0x0009AD87
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x0009CB93 File Offset: 0x0009AD93
		public LazyBytes FastIndexBlob
		{
			get
			{
				throw new NotSupportedException("This should never be called");
			}
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0009CB9F File Offset: 0x0009AD9F
		public void CacheTransportSettings()
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x0009CBAB File Offset: 0x0009ADAB
		public bool IsJournalReport()
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x0009CBB7 File Offset: 0x0009ADB7
		public bool IsPfReplica()
		{
			return this.mailItem.IsPfReplica();
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x0009CBC4 File Offset: 0x0009ADC4
		public bool IsShadowed()
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0009CBD0 File Offset: 0x0009ADD0
		public bool IsDelayedAck()
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x0009CBDC File Offset: 0x0009ADDC
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0009CBE8 File Offset: 0x0009ADE8
		public Stream OpenMimeReadStream()
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x0009CBF4 File Offset: 0x0009ADF4
		public Stream OpenMimeReadStream(bool downConvert)
		{
			throw new NotSupportedException("This should never be called");
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x0009CC00 File Offset: 0x0009AE00
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.Delivery, connectComponent, this.LatencyTracker);
			LatencyTracker.BeginTrackLatency(LatencyComponent.Delivery, this.LatencyTracker);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0009CC1E File Offset: 0x0009AE1E
		public void AddDsnParameters(string key, object value)
		{
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0009CC20 File Offset: 0x0009AE20
		public void IncrementPoisonForRemoteCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400144B RID: 5195
		private TransportMailItem mailItem;

		// Token: 0x0400144C RID: 5196
		private MailRecipientCollection recipients;

		// Token: 0x0400144D RID: 5197
		private BodyType bodyType;
	}
}
