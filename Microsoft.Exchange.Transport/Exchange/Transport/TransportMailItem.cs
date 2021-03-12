using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A8 RID: 424
	internal class TransportMailItem : IReadOnlyMailItem, ISystemProbeTraceable, ITransportMailItemFacade, ILockableItem, IQueueItem, IQueueQuotaMailItem
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600121D RID: 4637 RVA: 0x00049A0C File Offset: 0x00047C0C
		// (remove) Token: 0x0600121E RID: 4638 RVA: 0x00049A44 File Offset: 0x00047C44
		public event Action<TransportMailItem> OnReleaseFromActive;

		// Token: 0x0600121F RID: 4639 RVA: 0x00049A7C File Offset: 0x00047C7C
		private TransportMailItem(LatencyComponent sourceComponent) : this(null, sourceComponent, null, MailDirectionality.Undefined, default(Guid))
		{
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00049A9C File Offset: 0x00047C9C
		private TransportMailItem(ADRecipientCache<TransportMiniRecipient> recipientCache, LatencyComponent sourceComponent, MailDirectionality directionality, Guid externalOrgId) : this(recipientCache, sourceComponent, null, directionality, externalOrgId)
		{
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00049AAC File Offset: 0x00047CAC
		private TransportMailItem(ADRecipientCache<TransportMiniRecipient> recipientCache, LatencyComponent sourceComponent, IMailItemStorage storage, MailDirectionality directionality = MailDirectionality.Undefined, Guid externalOrgId = default(Guid))
		{
			this.audit = new Breadcrumbs(8);
			this.deferUntil = DateTime.MinValue;
			this.nextHopSolutionTable = new Dictionary<NextHopSolutionKey, NextHopSolution>();
			this.exposeMessage = true;
			this.exposeMessageHeaders = true;
			this.queueQuotaTrackingBits = new QueueQuotaTrackingBits();
			this.latencyStartTime = DateTime.MinValue;
			base..ctor();
			this.storage = (storage ?? TransportMailItem.Database.NewMailItemStorage(true));
			this.storage.Recipients = new MailRecipientCollection(this);
			this.routeForHighAvailability = true;
			this.routingTimeStamp = DateTime.MinValue;
			this.snapshotState = new SnapshotWriterState();
			this.latencyTracker = LatencyTracker.CreateInstance(sourceComponent);
			this.loadedInRestart = (sourceComponent == LatencyComponent.ServiceRestart);
			this.recipientCache = recipientCache;
			if (directionality != MailDirectionality.Undefined)
			{
				this.Directionality = directionality;
			}
			else if (storage != null)
			{
				this.Directionality = storage.Directionality;
			}
			if (MultiTenantTransport.MultiTenancyEnabled)
			{
				if (externalOrgId != Guid.Empty)
				{
					this.ExternalOrganizationId = externalOrgId;
				}
				else if (storage != null)
				{
					this.ExternalOrganizationId = storage.ExternalOrganizationId;
				}
			}
			if (sourceComponent == LatencyComponent.Heartbeat)
			{
				this.ShadowMessageId = Guid.Empty;
			}
			if (storage == null)
			{
				this.SetMimeDefaultEncoding();
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00049BCC File Offset: 0x00047DCC
		private TransportMailItem(TransportMailItem rhs, bool shareRecipientCache, LatencyTracker latencyTrackerToClone)
		{
			this.audit = new Breadcrumbs(8);
			this.deferUntil = DateTime.MinValue;
			this.nextHopSolutionTable = new Dictionary<NextHopSolutionKey, NextHopSolution>();
			this.exposeMessage = true;
			this.exposeMessageHeaders = true;
			this.queueQuotaTrackingBits = new QueueQuotaTrackingBits();
			this.latencyStartTime = DateTime.MinValue;
			base..ctor();
			this.storage = rhs.storage.Clone();
			this.storage.Recipients = new MailRecipientCollection(this);
			this.RemoveFirewalledProperties(this.ExtendedPropertyDictionary);
			this.CurrentCondition = rhs.CurrentCondition;
			if (rhs.ThrottlingContext != null)
			{
				this.ThrottlingContext = rhs.ThrottlingContext;
				this.ThrottlingContext.AddMemoryCost(new ByteQuantifiedSize((ulong)this.MimeSize));
			}
			if (rhs.messageTrackingAgentInfo != null)
			{
				this.messageTrackingAgentInfo = new List<List<KeyValuePair<string, string>>>(rhs.messageTrackingAgentInfo);
			}
			this.audit.Drop(Breadcrumb.CloneItem);
			this.snapshotState = new SnapshotWriterState();
			if (shareRecipientCache)
			{
				this.recipientCache = rhs.ADRecipientCache;
			}
			else if (rhs.ADRecipientCache != null)
			{
				this.recipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, rhs.ADRecipientCache.OrganizationId);
			}
			this.Directionality = rhs.Directionality;
			this.ExternalOrganizationId = rhs.ExternalOrganizationId;
			this.latencyStartTime = rhs.latencyStartTime;
			this.transportSettings = rhs.transportSettings;
			this.routeForHighAvailability = rhs.routeForHighAvailability;
			this.routingTimeStamp = rhs.routingTimeStamp;
			this.dsnParameters = rhs.DsnParameters;
			if (rhs.PipelineTracingEnabled)
			{
				this.pipelineTracingPath = rhs.PipelineTracingPath;
				this.pipelineTracingEnabled = new bool?(rhs.PipelineTracingEnabled);
				this.snapshotState.IsOriginalWritten = true;
			}
			this.latencyTracker = LatencyTracker.Clone(latencyTrackerToClone);
			if (rhs.lockReasonHistory != null)
			{
				this.lockReasonHistory = new List<string>();
				this.lockReasonHistory.AddRange(rhs.lockReasonHistory);
			}
			this.FallbackToRawOverride = rhs.FallbackToRawOverride;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00049DF4 File Offset: 0x00047FF4
		private TransportMailItem(TransportMailItem rhs, bool shareRecipientCache, LatencyTracker latencyTrackerToClone, ForkCount transportRulesForkCount, bool copyStorage = false)
		{
			this.audit = new Breadcrumbs(8);
			this.deferUntil = DateTime.MinValue;
			this.nextHopSolutionTable = new Dictionary<NextHopSolutionKey, NextHopSolution>();
			this.exposeMessage = true;
			this.exposeMessageHeaders = true;
			this.queueQuotaTrackingBits = new QueueQuotaTrackingBits();
			this.latencyStartTime = DateTime.MinValue;
			base..ctor();
			this.transportRulesForkCount = transportRulesForkCount;
			this.CurrentCondition = rhs.CurrentCondition;
			if (rhs.messageTrackingAgentInfo != null)
			{
				this.messageTrackingAgentInfo = new List<List<KeyValuePair<string, string>>>(rhs.messageTrackingAgentInfo);
			}
			this.snapshotState = new SnapshotWriterState();
			if (shareRecipientCache)
			{
				this.recipientCache = rhs.ADRecipientCache;
			}
			else if (rhs.ADRecipientCache != null)
			{
				this.recipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, rhs.ADRecipientCache.OrganizationId);
			}
			this.latencyStartTime = rhs.latencyStartTime;
			this.transportSettings = rhs.transportSettings;
			this.routeForHighAvailability = rhs.routeForHighAvailability;
			this.routingTimeStamp = rhs.routingTimeStamp;
			this.dsnParameters = rhs.DsnParameters;
			if (rhs.PipelineTracingEnabled)
			{
				this.pipelineTracingPath = rhs.PipelineTracingPath;
				this.pipelineTracingEnabled = new bool?(rhs.PipelineTracingEnabled);
				this.snapshotState.IsOriginalWritten = true;
			}
			this.latencyTracker = LatencyTracker.Clone(latencyTrackerToClone);
			if (rhs.lockReasonHistory != null)
			{
				this.lockReasonHistory = new List<string>();
				this.lockReasonHistory.AddRange(rhs.lockReasonHistory);
			}
			this.audit.Drop(Breadcrumb.CloneItem);
			if (copyStorage)
			{
				this.storage = rhs.storage.CopyCommitted(delegate(IMailItemStorage copy)
				{
					copy.Recipients = new MailRecipientCollection(this);
					this.RemoveFirewalledProperties(copy.ExtendedProperties as IDictionary<string, object>);
				});
			}
			else
			{
				this.storage = rhs.storage.CloneCommitted(delegate(IMailItemStorage clone)
				{
					clone.Recipients = new MailRecipientCollection(this);
					this.RemoveFirewalledProperties(clone.ExtendedProperties as IDictionary<string, object>);
				});
			}
			if (rhs.ThrottlingContext != null)
			{
				this.ThrottlingContext = rhs.ThrottlingContext;
				this.ThrottlingContext.AddMemoryCost(new ByteQuantifiedSize((ulong)this.MimeSize));
			}
			this.FallbackToRawOverride = rhs.FallbackToRawOverride;
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00049FEB File Offset: 0x000481EB
		public static IMessagingDatabase Database
		{
			get
			{
				return TransportMailItem.database;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00049FF2 File Offset: 0x000481F2
		public bool LoadedInRestart
		{
			get
			{
				return this.loadedInRestart;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00049FFA File Offset: 0x000481FA
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x0004A002 File Offset: 0x00048202
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				return this.recipientCache;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.recipientCache = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0004A020 File Offset: 0x00048220
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x0004A057 File Offset: 0x00048257
		public DeliveryPriority Priority
		{
			get
			{
				if (this.storage.Priority == null)
				{
					return DeliveryPriority.Normal;
				}
				return this.storage.Priority.Value;
			}
			set
			{
				this.storage.Priority = new DeliveryPriority?(value);
				if (TransportMailItem.configuration.AppConfig.DeliveryQueuePrioritizationConfiguration.PriorityHeaderPromotionEnabled)
				{
					Util.EncodeAndSetPriorityAsHeader(this.RootPart.Headers, value, this.PrioritizationReason);
				}
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004A097 File Offset: 0x00048297
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x0004A0A4 File Offset: 0x000482A4
		public DeliveryPriority BootloadingPriority
		{
			get
			{
				return this.storage.BootloadingPriority;
			}
			set
			{
				this.storage.BootloadingPriority = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0004A0B2 File Offset: 0x000482B2
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x0004A0BA File Offset: 0x000482BA
		public bool RouteForHighAvailability
		{
			get
			{
				return this.routeForHighAvailability;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.routeForHighAvailability = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004A0CF File Offset: 0x000482CF
		public bool IsReplayMessage
		{
			get
			{
				return this.RootPart != null && this.RootPart.Headers != null && this.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-ResubmittedMessage") != null;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004A103 File Offset: 0x00048303
		public OrganizationId OrganizationId
		{
			get
			{
				if (this.recipientCache != null)
				{
					return this.recipientCache.OrganizationId;
				}
				return null;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004A11A File Offset: 0x0004831A
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				if (this.transportSettings == null)
				{
					throw new InvalidOperationException("Trying to access TransportSettings before the value is set. This field should be accessed only after calling CacheTransportSettings method");
				}
				return this.transportSettings;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004A135 File Offset: 0x00048335
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0004A13D File Offset: 0x0004833D
		public LatencyTracker LatencyTracker
		{
			get
			{
				return this.latencyTracker;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.latencyTracker = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004A152 File Offset: 0x00048352
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x0004A15A File Offset: 0x0004835A
		public IActivityScope ActivityScope
		{
			get
			{
				return this.activityScope;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.activityScope = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004A16F File Offset: 0x0004836F
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x0004A17C File Offset: 0x0004837C
		public DateTime DateReceived
		{
			get
			{
				return this.storage.DateReceived;
			}
			set
			{
				this.storage.DateReceived = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004A18A File Offset: 0x0004838A
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x0004A197 File Offset: 0x00048397
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				return this.storage.ExtensionToExpiryDuration;
			}
			private set
			{
				this.storage.ExtensionToExpiryDuration = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004A1A5 File Offset: 0x000483A5
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x0004A1B2 File Offset: 0x000483B2
		public string PerfCounterAttribution
		{
			get
			{
				return this.storage.PerfCounterAttribution;
			}
			set
			{
				this.storage.PerfCounterAttribution = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004A1C0 File Offset: 0x000483C0
		// (set) Token: 0x0600123C RID: 4668 RVA: 0x0004A1C8 File Offset: 0x000483C8
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.authMethod;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.authMethod = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0004A1DD File Offset: 0x000483DD
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x0004A1E5 File Offset: 0x000483E5
		public string MessageTrackingSecurityInfo
		{
			get
			{
				return this.messageTrackingSecurityInfo;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.messageTrackingSecurityInfo = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004A1FA File Offset: 0x000483FA
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x0004A202 File Offset: 0x00048402
		public bool ExposeMessage
		{
			get
			{
				return this.exposeMessage;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.exposeMessage = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004A217 File Offset: 0x00048417
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0004A21F File Offset: 0x0004841F
		public bool ExposeMessageHeaders
		{
			get
			{
				return this.exposeMessageHeaders;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.exposeMessageHeaders = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0004A234 File Offset: 0x00048434
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x0004A244 File Offset: 0x00048444
		public RoutingAddress From
		{
			get
			{
				return new RoutingAddress(this.FromAddress);
			}
			set
			{
				if (value == RoutingAddress.Empty)
				{
					throw new ArgumentException("From property cannot be set to an empty RoutingAddress value.");
				}
				if (!value.IsValid)
				{
					throw new FormatException("Can not set From property to an invalid RoutingAddress value.");
				}
				if (string.Equals("<>", value.LocalPart) && !string.IsNullOrEmpty(value.DomainPart))
				{
					throw new ArgumentException("The Domain part is not empty and the local part is <>", value.ToString());
				}
				this.FromAddress = value.ToString();
				this.SetMimeDefaultEncoding();
				if (this.pipelineTracingEnabled == null && TransportMailItem.configuration != null)
				{
					Server transportServer = TransportMailItem.configuration.LocalServer.TransportServer;
					if (transportServer != null && transportServer.PipelineTracingEnabled && transportServer.PipelineTracingSenderAddress != null && transportServer.PipelineTracingPath != null && !string.IsNullOrEmpty(transportServer.PipelineTracingPath.PathName))
					{
						this.pipelineTracingEnabled = new bool?(!transportServer.PipelineTracingSenderAddress.Equals(SmtpAddress.Empty) && transportServer.PipelineTracingSenderAddress.ToString().Equals(this.FromAddress));
						if (this.pipelineTracingEnabled.Value)
						{
							this.pipelineTracingPath = transportServer.PipelineTracingPath.PathName;
							TransportMailItem.Logger.LogEvent(TransportEventLogConstants.Tuple_PipelineTracingActive, "TransportPipelineTracingActive", new object[0]);
							return;
						}
					}
					else
					{
						this.pipelineTracingEnabled = new bool?(false);
					}
				}
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0004A3D8 File Offset: 0x000485D8
		public RoutingAddress OriginalFrom
		{
			get
			{
				string attributedFromAddress = this.storage.AttributedFromAddress;
				if (!string.IsNullOrEmpty(attributedFromAddress))
				{
					return new RoutingAddress(attributedFromAddress);
				}
				if (!string.IsNullOrEmpty(this.FromAddress))
				{
					return new RoutingAddress(this.FromAddress);
				}
				return RoutingAddress.Empty;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0004A420 File Offset: 0x00048620
		public string OriginalFromAddress
		{
			get
			{
				return this.OriginalFrom.ToString();
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x0004A441 File Offset: 0x00048641
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0004A44E File Offset: 0x0004864E
		public string MimeFrom
		{
			get
			{
				return this.storage.MimeFrom;
			}
			set
			{
				this.storage.MimeFrom = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004A45C File Offset: 0x0004865C
		public RemoteDomainEntry FromDomainConfig
		{
			get
			{
				PerTenantRemoteDomainTable orgRemoteDomains = this.GetOrgRemoteDomains();
				if (orgRemoteDomains == null)
				{
					return null;
				}
				return orgRemoteDomains.RemoteDomainTable.GetDomainEntry(SmtpDomain.GetDomainPart(this.From));
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004A48B File Offset: 0x0004868B
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0004A498 File Offset: 0x00048698
		public RoutingAddress MimeSender
		{
			get
			{
				return new RoutingAddress(this.MimeSenderAddress);
			}
			set
			{
				if (value != RoutingAddress.Empty && !value.IsValid)
				{
					throw new FormatException("Can not set the Mime Sender property to an invalid RoutingAddress value.");
				}
				this.MimeSenderAddress = value.ToString();
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0004A4CE File Offset: 0x000486CE
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x0004A4DB File Offset: 0x000486DB
		public DsnFormat DsnFormat
		{
			get
			{
				return this.storage.DsnFormat;
			}
			set
			{
				this.storage.DsnFormat = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004A4E9 File Offset: 0x000486E9
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x0004A4F1 File Offset: 0x000486F1
		public DsnParameters DsnParameters
		{
			get
			{
				return this.dsnParameters;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.dsnParameters = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0004A506 File Offset: 0x00048706
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x0004A511 File Offset: 0x00048711
		public bool SuppressBodyInDsn
		{
			get
			{
				return this.DsnFormat == DsnFormat.Headers;
			}
			set
			{
				this.DsnFormat = (value ? DsnFormat.Headers : DsnFormat.Full);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0004A520 File Offset: 0x00048720
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x0004A52D File Offset: 0x0004872D
		public bool IsDiscardPending
		{
			get
			{
				return this.storage.IsDiscardPending;
			}
			set
			{
				this.storage.IsDiscardPending = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0004A53B File Offset: 0x0004873B
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x0004A548 File Offset: 0x00048748
		public int Scl
		{
			get
			{
				return this.storage.Scl;
			}
			set
			{
				if (value < -1 || value > 10)
				{
					throw new ArgumentException("Invalid Scl value:" + value);
				}
				this.storage.Scl = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0004A575 File Offset: 0x00048775
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x0004A582 File Offset: 0x00048782
		public MailDirectionality Directionality
		{
			get
			{
				return this.storage.Directionality;
			}
			set
			{
				this.storage.Directionality = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0004A590 File Offset: 0x00048790
		public IExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.storage.ExtendedProperties;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004A59D File Offset: 0x0004879D
		public IDictionary<string, object> ExtendedPropertyDictionary
		{
			get
			{
				this.ThrowIfDeleted();
				return this.ExtendedProperties as IDictionary<string, object>;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0004A5B0 File Offset: 0x000487B0
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x0004A5E1 File Offset: 0x000487E1
		public Guid? OrganizationScope
		{
			get
			{
				Guid value;
				if (this.ExtendedProperties.TryGetValue<Guid>("Microsoft.Exchange.Transport.MailRecipient.OrganizationScope", out value))
				{
					return new Guid?(value);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.ExtendedProperties.Remove("Microsoft.Exchange.Transport.MailRecipient.OrganizationScope");
					return;
				}
				this.ExtendedProperties.SetValue<Guid>("Microsoft.Exchange.Transport.MailRecipient.OrganizationScope", value.Value);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0004A618 File Offset: 0x00048818
		public Status Status
		{
			get
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				foreach (MailRecipient mailRecipient in this.Recipients)
				{
					switch (mailRecipient.Status)
					{
					case Status.Ready:
						return Status.Ready;
					case Status.Retry:
						flag2 = true;
						break;
					case Status.Handled:
						flag3 = true;
						break;
					case Status.Locked:
						flag = true;
						break;
					}
				}
				if (flag)
				{
					return Status.Locked;
				}
				if (flag2)
				{
					return Status.Retry;
				}
				if (flag3)
				{
					return Status.Handled;
				}
				return Status.Complete;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x0004A6B0 File Offset: 0x000488B0
		public bool QueuedForDelivery
		{
			get
			{
				return this.queuedForDelivery;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x0004A6B8 File Offset: 0x000488B8
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x0004A6C5 File Offset: 0x000488C5
		public string HeloDomain
		{
			get
			{
				return this.storage.HeloDomain;
			}
			set
			{
				if (value != null && !RoutingAddress.IsValidDomain(value) && !RoutingAddress.IsDomainIPLiteral(value) && !HeloCommandEventArgs.IsValidIpv6WindowsAddress(value))
				{
					throw new FormatException("HeloDomain property has an invalid SMTP domain value: " + value);
				}
				this.storage.HeloDomain = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0004A6FF File Offset: 0x000488FF
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0004A70C File Offset: 0x0004890C
		public string Auth
		{
			get
			{
				return this.storage.Auth;
			}
			set
			{
				if (value != null && value.Length > 500)
				{
					throw new ArgumentException(Strings.ValueIsTooLarge(value.Length, 500));
				}
				this.storage.Auth = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0004A745 File Offset: 0x00048945
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x0004A752 File Offset: 0x00048952
		public string EnvId
		{
			get
			{
				return this.storage.EnvId;
			}
			set
			{
				if (value != null && value.Length > 100)
				{
					throw new ArgumentException(Strings.ValueIsTooLarge(value.Length, 100));
				}
				this.storage.EnvId = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0004A785 File Offset: 0x00048985
		// (set) Token: 0x06001265 RID: 4709 RVA: 0x0004A792 File Offset: 0x00048992
		public BodyType BodyType
		{
			get
			{
				return this.storage.BodyType;
			}
			set
			{
				this.storage.BodyType = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004A7A0 File Offset: 0x000489A0
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x0004A7B0 File Offset: 0x000489B0
		public string Oorg
		{
			get
			{
				return this.storage.Oorg;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !RoutingAddress.IsValidDomain(value))
				{
					throw new ArgumentException(string.Format("Invalid originator organization value '{0}'. Originator organizations should be valid SMTP domains, like 'contoso.com'", value));
				}
				this.storage.Oorg = value;
				if (this.exposeMessageHeaders)
				{
					Util.SetOorgHeaders(this.RootPart.Headers, value);
				}
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0004A803 File Offset: 0x00048A03
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x0004A810 File Offset: 0x00048A10
		public string ReceiveConnectorName
		{
			get
			{
				return this.storage.ReceiveConnectorName;
			}
			set
			{
				this.storage.ReceiveConnectorName = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0004A81E File Offset: 0x00048A1E
		public bool IsInAsyncCommit
		{
			get
			{
				return this.storage.IsInAsyncCommit;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0004A82B File Offset: 0x00048A2B
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x0004A838 File Offset: 0x00048A38
		public int PoisonCount
		{
			get
			{
				return this.storage.PoisonCount;
			}
			set
			{
				this.storage.PoisonCount = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0004A846 File Offset: 0x00048A46
		public int PoisonForRemoteCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0004A84D File Offset: 0x00048A4D
		public bool IsPoison
		{
			get
			{
				return this.PoisonCount >= Components.Configuration.LocalServer.TransportServer.PoisonThreshold;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0004A86E File Offset: 0x00048A6E
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x0004A876 File Offset: 0x00048A76
		public Dictionary<NextHopSolutionKey, NextHopSolution> NextHopSolutions
		{
			get
			{
				return this.nextHopSolutionTable;
			}
			set
			{
				this.nextHopSolutionTable = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0004A87F File Offset: 0x00048A7F
		// (set) Token: 0x06001272 RID: 4722 RVA: 0x0004A88C File Offset: 0x00048A8C
		public MimeDocument MimeDocument
		{
			get
			{
				return this.storage.MimeDocument;
			}
			set
			{
				this.storage.MimeDocument = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004A89A File Offset: 0x00048A9A
		public EmailMessage Message
		{
			get
			{
				if (!this.ExposeMessage)
				{
					throw new InvalidOperationException("Message can not be exposed here");
				}
				return this.storage.Message;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0004A8BA File Offset: 0x00048ABA
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0004A8C7 File Offset: 0x00048AC7
		public string PrioritizationReason
		{
			get
			{
				return this.storage.PrioritizationReason;
			}
			set
			{
				this.storage.PrioritizationReason = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0004A8D5 File Offset: 0x00048AD5
		public MimePart RootPart
		{
			get
			{
				return this.storage.RootPart;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0004A8E2 File Offset: 0x00048AE2
		// (set) Token: 0x06001278 RID: 4728 RVA: 0x0004A8F4 File Offset: 0x00048AF4
		public byte[] LegacyXexch50Blob
		{
			get
			{
				return this.storage.XExch50Blob.Value;
			}
			set
			{
				this.storage.XExch50Blob.Value = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0004A907 File Offset: 0x00048B07
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x0004A90F File Offset: 0x00048B0F
		public DateTime RoutingTimeStamp
		{
			get
			{
				return this.routingTimeStamp;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.routingTimeStamp = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0004A924 File Offset: 0x00048B24
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x0004A931 File Offset: 0x00048B31
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.storage.SourceIPAddress;
			}
			set
			{
				this.storage.SourceIPAddress = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004A93F File Offset: 0x00048B3F
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x0004A94C File Offset: 0x00048B4C
		public long MimeSize
		{
			get
			{
				return this.storage.MimeSize;
			}
			set
			{
				this.storage.MimeSize = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0004A95A File Offset: 0x00048B5A
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x0004A967 File Offset: 0x00048B67
		public bool MimeNotSequential
		{
			get
			{
				return this.storage.MimeNotSequential;
			}
			set
			{
				this.storage.MimeNotSequential = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004A975 File Offset: 0x00048B75
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x0004A982 File Offset: 0x00048B82
		public bool FallbackToRawOverride
		{
			get
			{
				return this.storage.FallbackToRawOverride;
			}
			set
			{
				this.storage.FallbackToRawOverride = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004A990 File Offset: 0x00048B90
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x0004A99D File Offset: 0x00048B9D
		public string Subject
		{
			get
			{
				return this.storage.Subject;
			}
			set
			{
				this.storage.Subject = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0004A9AB File Offset: 0x00048BAB
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x0004A9B8 File Offset: 0x00048BB8
		public string InternetMessageId
		{
			get
			{
				return this.storage.InternetMessageId;
			}
			set
			{
				this.storage.InternetMessageId = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0004A9C6 File Offset: 0x00048BC6
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x0004A9D3 File Offset: 0x00048BD3
		public Guid NetworkMessageId
		{
			get
			{
				return this.storage.NetworkMessageId;
			}
			set
			{
				this.storage.NetworkMessageId = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0004A9E4 File Offset: 0x00048BE4
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x0004AA52 File Offset: 0x00048C52
		public string ExoAccountForest
		{
			get
			{
				if (!string.IsNullOrEmpty(this.storage.ExoAccountForest))
				{
					return this.storage.ExoAccountForest;
				}
				if (this.OrganizationId != null)
				{
					if (!this.IsReadOnly)
					{
						this.storage.ExoAccountForest = this.OrganizationId.PartitionId.ForestFQDN;
					}
					return this.OrganizationId.PartitionId.ForestFQDN;
				}
				return null;
			}
			set
			{
				this.storage.ExoAccountForest = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004AA60 File Offset: 0x00048C60
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0004AA6D File Offset: 0x00048C6D
		public string ExoTenantContainer
		{
			get
			{
				return this.storage.ExoTenantContainer;
			}
			set
			{
				this.storage.ExoTenantContainer = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0004AA7B File Offset: 0x00048C7B
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x0004AA88 File Offset: 0x00048C88
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.storage.ExternalOrganizationId;
			}
			set
			{
				this.storage.ExternalOrganizationId = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0004AA96 File Offset: 0x00048C96
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x0004AAA3 File Offset: 0x00048CA3
		public Guid SystemProbeId
		{
			get
			{
				return this.storage.SystemProbeId;
			}
			set
			{
				this.storage.SystemProbeId = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0004AAB1 File Offset: 0x00048CB1
		public bool IsProbe
		{
			get
			{
				return this.SystemProbeId != Guid.Empty || !string.IsNullOrEmpty(this.storage.ProbeName);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0004AADC File Offset: 0x00048CDC
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x0004AAE9 File Offset: 0x00048CE9
		public string ProbeName
		{
			get
			{
				return this.storage.ProbeName;
			}
			set
			{
				this.storage.ProbeName = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0004AAF7 File Offset: 0x00048CF7
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x0004AB04 File Offset: 0x00048D04
		public bool PersistProbeTrace
		{
			get
			{
				return this.storage.PersistProbeTrace;
			}
			set
			{
				this.storage.PersistProbeTrace = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0004AB12 File Offset: 0x00048D12
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x0004AB1F File Offset: 0x00048D1F
		public Guid ShadowMessageId
		{
			get
			{
				return this.storage.ShadowMessageId;
			}
			set
			{
				this.storage.ShadowMessageId = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004AB2D File Offset: 0x00048D2D
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x0004AB3A File Offset: 0x00048D3A
		public string ShadowServerContext
		{
			get
			{
				return this.storage.ShadowServerContext;
			}
			set
			{
				this.storage.ShadowServerContext = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0004AB48 File Offset: 0x00048D48
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x0004AB55 File Offset: 0x00048D55
		public string ShadowServerDiscardId
		{
			get
			{
				return this.storage.ShadowServerDiscardId;
			}
			set
			{
				this.storage.ShadowServerDiscardId = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0004AB63 File Offset: 0x00048D63
		public SnapshotWriterState SnapshotWriterState
		{
			get
			{
				return this.snapshotState;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0004AB6B File Offset: 0x00048D6B
		public bool PipelineTracingEnabled
		{
			get
			{
				return this.pipelineTracingEnabled != null && this.pipelineTracingEnabled.Value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0004AB87 File Offset: 0x00048D87
		public string PipelineTracingPath
		{
			get
			{
				return this.pipelineTracingPath;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0004AB8F File Offset: 0x00048D8F
		public bool RetryDeliveryIfRejected
		{
			get
			{
				return this.storage.RetryDeliveryIfRejected;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004AB9C File Offset: 0x00048D9C
		public bool IsHeartbeat
		{
			get
			{
				return this.ShadowMessageId == Guid.Empty;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0004ABAE File Offset: 0x00048DAE
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x0004ABB6 File Offset: 0x00048DB6
		public DeferReason DeferReason
		{
			get
			{
				return this.deferReason;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.deferReason = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x0004ABCB File Offset: 0x00048DCB
		public DateTime Expiry
		{
			get
			{
				return this.GetExpiryTime(true);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004ABD4 File Offset: 0x00048DD4
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0004ABE6 File Offset: 0x00048DE6
		public MailRecipientCollection Recipients
		{
			get
			{
				return this.storage.Recipients as MailRecipientCollection;
			}
			private set
			{
				this.storage.Recipients = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004ABF4 File Offset: 0x00048DF4
		public long MsgId
		{
			get
			{
				return this.storage.MsgId;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004AC01 File Offset: 0x00048E01
		public long RecordId
		{
			get
			{
				return this.MsgId;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0004AC09 File Offset: 0x00048E09
		public bool MimeWriteStreamOpen
		{
			get
			{
				return this.storage.MimeWriteStreamOpen;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x0004AC16 File Offset: 0x00048E16
		public bool IsReadOnly
		{
			get
			{
				return this.storage.IsReadOnly;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004AC23 File Offset: 0x00048E23
		public bool IsRowDeleted
		{
			get
			{
				return this.storage.IsDeleted;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x0004AC30 File Offset: 0x00048E30
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x0004AC38 File Offset: 0x00048E38
		public AccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
			internal set
			{
				this.accessToken = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0004AC41 File Offset: 0x00048E41
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x0004AC49 File Offset: 0x00048E49
		public ThrottlingContext ThrottlingContext
		{
			get
			{
				return this.throttlingContext;
			}
			set
			{
				this.throttlingContext = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0004AC52 File Offset: 0x00048E52
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0004AC5A File Offset: 0x00048E5A
		public WaitCondition CurrentCondition
		{
			get
			{
				return this.waitCondition;
			}
			set
			{
				this.waitCondition = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0004AC63 File Offset: 0x00048E63
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x0004AC6B File Offset: 0x00048E6B
		public QueuedRecipientsByAgeToken QueuedRecipientsByAgeToken
		{
			get
			{
				return this.queuedRecipientsByAgeToken;
			}
			set
			{
				this.queuedRecipientsByAgeToken = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0004AC74 File Offset: 0x00048E74
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x0004AC7C File Offset: 0x00048E7C
		public DateTimeOffset LockExpirationTime
		{
			get
			{
				return this.lockExpirationTime;
			}
			set
			{
				this.lockExpirationTime = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0004AC85 File Offset: 0x00048E85
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x0004AC8D File Offset: 0x00048E8D
		public string LockReason
		{
			get
			{
				return this.lockReason;
			}
			set
			{
				this.lockReason = value;
				if (!string.IsNullOrEmpty(value))
				{
					if (this.lockReasonHistory == null)
					{
						this.lockReasonHistory = new List<string>();
					}
					this.lockReasonHistory.Add(value);
				}
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0004ACBD File Offset: 0x00048EBD
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0004ACC5 File Offset: 0x00048EC5
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				return this.lockReasonHistory;
			}
			internal set
			{
				this.lockReasonHistory = ((value == null) ? null : new List<string>(value));
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0004ACD9 File Offset: 0x00048ED9
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x0004ACE6 File Offset: 0x00048EE6
		public List<string> MoveToHosts
		{
			get
			{
				return this.storage.MoveToHosts;
			}
			set
			{
				this.storage.MoveToHosts = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0004ACF4 File Offset: 0x00048EF4
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x0004ACFC File Offset: 0x00048EFC
		public DateTime DeferUntil
		{
			get
			{
				return this.deferUntil;
			}
			set
			{
				this.ThrowIfDeletedOrReadOnly();
				this.ThrowIfInAsyncCommit();
				this.deferUntil = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004AD11 File Offset: 0x00048F11
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x0004AD1E File Offset: 0x00048F1E
		public RiskLevel RiskLevel
		{
			get
			{
				return this.storage.RiskLevel;
			}
			set
			{
				this.storage.RiskLevel = value;
				Util.SetAsciiHeader(this.RootPart.Headers, "X-MS-Exchange-Organization-Spam-Filter-Enumerated-Risk", value.ToString());
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004AD4D File Offset: 0x00048F4D
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x0004AD55 File Offset: 0x00048F55
		public ForkCount TransportRulesForkCount
		{
			get
			{
				return this.transportRulesForkCount;
			}
			set
			{
				this.transportRulesForkCount = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0004AD60 File Offset: 0x00048F60
		private static ExEventLog Logger
		{
			get
			{
				if (TransportMailItem.eventLogger == null)
				{
					ExEventLog value = new ExEventLog(TransportMailItem.componentGuid, TransportEventLog.GetEventSource());
					Interlocked.CompareExchange<ExEventLog>(ref TransportMailItem.eventLogger, value, null);
				}
				return TransportMailItem.eventLogger;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0004AD96 File Offset: 0x00048F96
		object ITransportMailItemFacade.ADRecipientCacheAsObject
		{
			get
			{
				return this.recipientCache;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004AD9E File Offset: 0x00048F9E
		object ITransportMailItemFacade.OrganizationIdAsObject
		{
			get
			{
				return this.OrganizationId;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0004ADA6 File Offset: 0x00048FA6
		bool ITransportMailItemFacade.IsOriginating
		{
			get
			{
				return this.Directionality == MailDirectionality.Originating;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0004ADB1 File Offset: 0x00048FB1
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x0004ADB9 File Offset: 0x00048FB9
		bool ITransportMailItemFacade.FallbackToRawOverride
		{
			get
			{
				return this.FallbackToRawOverride;
			}
			set
			{
				this.FallbackToRawOverride = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004ADC2 File Offset: 0x00048FC2
		ITransportSettingsFacade ITransportMailItemFacade.TransportSettings
		{
			get
			{
				return this.transportSettings;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0004ADCA File Offset: 0x00048FCA
		IReadOnlyExtendedPropertyCollection IReadOnlyMailItem.ExtendedProperties
		{
			get
			{
				return this.ExtendedProperties;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004ADD2 File Offset: 0x00048FD2
		IMailRecipientCollectionFacade ITransportMailItemFacade.Recipients
		{
			get
			{
				return this.Recipients;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x0004ADDC File Offset: 0x00048FDC
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x0004AE13 File Offset: 0x00049013
		DeliveryPriority IQueueItem.Priority
		{
			get
			{
				if (this.storage.Priority == null)
				{
					return DeliveryPriority.Normal;
				}
				return this.storage.Priority.Value;
			}
			set
			{
				this.storage.Priority = new DeliveryPriority?(value);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0004AE26 File Offset: 0x00049026
		IReadOnlyMailRecipientCollection IReadOnlyMailItem.Recipients
		{
			get
			{
				return this.Recipients;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x0004AE2E File Offset: 0x0004902E
		MimeDocument ITransportMailItemFacade.MimeDocument
		{
			set
			{
				this.MimeDocument = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004AE37 File Offset: 0x00049037
		QueueQuotaTrackingBits IQueueQuotaMailItem.QueueQuotaTrackingBits
		{
			get
			{
				return this.queueQuotaTrackingBits;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004AE3F File Offset: 0x0004903F
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x0004AE4C File Offset: 0x0004904C
		private string FromAddress
		{
			get
			{
				return this.storage.FromAddress;
			}
			set
			{
				this.storage.FromAddress = value;
				if (this.storage.AttributedFromAddress == null)
				{
					this.storage.AttributedFromAddress = value;
				}
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004AE73 File Offset: 0x00049073
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0004AE80 File Offset: 0x00049080
		private string MimeSenderAddress
		{
			get
			{
				return this.storage.MimeSenderAddress;
			}
			set
			{
				this.storage.MimeSenderAddress = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0004AE90 File Offset: 0x00049090
		private bool IsInDelivery
		{
			get
			{
				foreach (KeyValuePair<NextHopSolutionKey, NextHopSolution> keyValuePair in this.nextHopSolutionTable)
				{
					if (keyValuePair.Value.DeliveryStatus == DeliveryStatus.InDelivery)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0004AEF4 File Offset: 0x000490F4
		public bool IsActive
		{
			get
			{
				return this.storage.IsActive;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004AF01 File Offset: 0x00049101
		public bool IsNew
		{
			get
			{
				return this.storage.IsNew;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0004AF0E File Offset: 0x0004910E
		public bool PendingDatabaseUpdates
		{
			get
			{
				return this.storage.PendingDatabaseUpdates;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0004AF1B File Offset: 0x0004911B
		public LazyBytes FastIndexBlob
		{
			get
			{
				return this.storage.FastIndexBlob;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0004AF28 File Offset: 0x00049128
		public DateTime LatencyStartTime
		{
			get
			{
				if (this.latencyStartTime == DateTime.MinValue)
				{
					DateTime dateTime;
					if (Util.TryGetLastResubmitTime(this, out dateTime) || Util.TryGetOrganizationalMessageArrivalTime(this, out dateTime))
					{
						this.latencyStartTime = dateTime.ToUniversalTime();
					}
					else
					{
						this.latencyStartTime = DateTime.UtcNow;
					}
				}
				return this.latencyStartTime;
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0004AF7C File Offset: 0x0004917C
		public static SmtpResponse ReplaceFailWithRetryResponse(SmtpResponse originalResponse)
		{
			string text = "The server responded with: " + originalResponse + ". The failure was replaced by a retry response because the message was marked for retry if rejected.";
			return new SmtpResponse("400", "4.4.7", new string[]
			{
				text
			});
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0004AFBA File Offset: 0x000491BA
		public static TransportMailItem NewMailItem(LatencyComponent sourceComponent)
		{
			return TransportMailItem.NewMailItem(null, sourceComponent, MailDirectionality.Undefined, Guid.Empty);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0004AFC9 File Offset: 0x000491C9
		public static TransportMailItem NewMailItem(OrganizationId organizationId, LatencyComponent sourceComponent, MailDirectionality directionality = MailDirectionality.Undefined, Guid externalOrgId = default(Guid))
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			return TransportMailItem.NewMailItem(new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, organizationId), sourceComponent, directionality, externalOrgId);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0004AFEC File Offset: 0x000491EC
		public static TransportMailItem NewMailItem(ADRecipientCache<TransportMiniRecipient> recipientCache, LatencyComponent sourceComponent, MailDirectionality directionality = MailDirectionality.Undefined, Guid externalOrgId = default(Guid))
		{
			TransportMailItem transportMailItem = new TransportMailItem(recipientCache, sourceComponent, directionality, externalOrgId);
			transportMailItem.audit.Drop(Breadcrumb.NewItem);
			return transportMailItem;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0004B014 File Offset: 0x00049214
		public static TransportMailItem NewMailItem(IMailItemStorage storage, LatencyComponent sourceComponent)
		{
			ArgumentValidator.ThrowIfNull("storage", storage);
			return new TransportMailItem(null, sourceComponent, storage, MailDirectionality.Undefined, default(Guid));
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0004B03E File Offset: 0x0004923E
		public static TransportMailItem NewAgentMailItem(ITransportMailItemFacade originalMailItem)
		{
			ArgumentValidator.ThrowIfNull("originalMailItem", originalMailItem);
			return TransportMailItem.NewSideEffectMailItem(null, LatencyComponent.Agent, MailDirectionality.Originating, originalMailItem.ExternalOrganizationId, originalMailItem.OriginalFrom);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0004B060 File Offset: 0x00049260
		public static TransportMailItem NewSideEffectMailItem(IReadOnlyMailItem originalMailItem)
		{
			if (originalMailItem == null)
			{
				throw new ArgumentNullException("originalMailItem");
			}
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = null;
			if (originalMailItem.OrganizationId != null)
			{
				adrecipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, originalMailItem.OrganizationId);
			}
			return TransportMailItem.NewSideEffectMailItem(adrecipientCache, LatencyComponent.Agent, MailDirectionality.Originating, originalMailItem.ExternalOrganizationId, originalMailItem.OriginalFrom);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0004B0B2 File Offset: 0x000492B2
		public static TransportMailItem NewSideEffectMailItem(IReadOnlyMailItem originalMailItem, OrganizationId organizationId, LatencyComponent sourceComponent, MailDirectionality directionality = MailDirectionality.Undefined, Guid externalOrgId = default(Guid))
		{
			if (originalMailItem == null)
			{
				throw new ArgumentNullException("originalMailItem");
			}
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			return TransportMailItem.NewSideEffectMailItem(new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, organizationId), sourceComponent, directionality, externalOrgId, originalMailItem.OriginalFrom);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0004B0F4 File Offset: 0x000492F4
		public static TransportMailItem LoadFromMsgId(long msgId, LatencyComponent sourceComponent)
		{
			IMailItemStorage mailItemStorage = TransportMailItem.Database.LoadMailItemFromId(msgId);
			if (mailItemStorage == null)
			{
				return null;
			}
			TransportMailItem transportMailItem = new TransportMailItem(null, sourceComponent, mailItemStorage, MailDirectionality.Undefined, default(Guid));
			foreach (IMailRecipientStorage recipStorage in TransportMailItem.Database.LoadMailRecipientsFromMessageId(msgId))
			{
				transportMailItem.AddRecipient(recipStorage);
			}
			return transportMailItem;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0004B170 File Offset: 0x00049370
		public static TransportMailItem FromMessageEnvelope(MessageEnvelope messageEnvelope, LatencyComponent sourceComponent)
		{
			return TransportMailItem.LoadFromMsgId(messageEnvelope.MsgId, sourceComponent);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0004B17E File Offset: 0x0004937E
		public static void SetComponents(ITransportConfiguration configuration, ResourceManager resourceManager, ShadowRedundancyComponent shadowRedundancyComponent, IMessagingDatabase messagingDatabase)
		{
			TransportMailItem.configuration = configuration;
			TransportMailItem.resourceManager = resourceManager;
			TransportMailItem.shadowRedundancyComponent = shadowRedundancyComponent;
			TransportMailItem.database = messagingDatabase;
			if (TransportMailItem.configuration != null)
			{
				MimeCache.SetConfig(TransportMailItem.configuration.AppConfig.DeliveryQueuePrioritizationConfiguration.PriorityHeaderPromotionEnabled);
			}
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0004B1B8 File Offset: 0x000493B8
		public static void TrackAsyncMessage(string internetMessageId)
		{
			PoisonMessage.AddAsyncMessage(internetMessageId);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0004B1C0 File Offset: 0x000493C0
		public static void TrackAsyncMessageCompleted(string internetMessageId)
		{
			PoisonMessage.RemoveAsyncMessage(internetMessageId);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004B1C8 File Offset: 0x000493C8
		public static bool SetPoisonContext(long recordId, string internetMessageId, MessageProcessingSource source)
		{
			if (PoisonMessage.Context != null)
			{
				MessageContext messageContext = PoisonMessage.Context as MessageContext;
				if (messageContext != null && messageContext.MessageId == recordId && ((messageContext.InternetMessageId != null && messageContext.InternetMessageId.Equals(internetMessageId)) || messageContext.InternetMessageId == internetMessageId) && messageContext.Source == source)
				{
					return false;
				}
			}
			PoisonMessage.Context = new MessageContext(recordId, internetMessageId, source);
			return true;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004B230 File Offset: 0x00049430
		private static TransportMailItem NewSideEffectMailItem(ADRecipientCache<TransportMiniRecipient> recipientCache, LatencyComponent sourceComponent, MailDirectionality directionality, Guid externalOrgId, RoutingAddress originalFromAddress)
		{
			TransportMailItem transportMailItem = new TransportMailItem(recipientCache, sourceComponent, directionality, externalOrgId);
			transportMailItem.audit.Drop(Breadcrumb.NewSideEffectItem);
			transportMailItem.ExternalOrganizationId = externalOrgId;
			transportMailItem.Directionality = directionality;
			transportMailItem.storage.AttributedFromAddress = originalFromAddress.ToString();
			if (originalFromAddress == RoutingAddress.Empty)
			{
				ExTraceGlobals.GeneralTracer.TraceError<long>(0L, "Side-effect message with id {0} created without an original from address", transportMailItem.MsgId);
			}
			return transportMailItem;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0004B2A4 File Offset: 0x000494A4
		public WaitCondition GetCondition()
		{
			if (this.CurrentCondition != null)
			{
				return this.CurrentCondition;
			}
			WaitCondition waitCondition;
			if (this.IsReplayMessage)
			{
				waitCondition = TransportMailItem.MessageRepositoryResubmitterCondition;
			}
			else if (Components.TransportAppConfig.ThrottlingConfig.CategorizerTenantThrottlingEnabled)
			{
				waitCondition = new TenantBasedCondition(this.ExternalOrganizationId);
			}
			else
			{
				if (!Components.TransportAppConfig.ThrottlingConfig.CategorizerSenderThrottlingEnabled)
				{
					return null;
				}
				waitCondition = new SenderBasedCondition(TransportMailItem.GetSourceID(this));
			}
			this.CurrentCondition = waitCondition;
			return waitCondition;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0004B324 File Offset: 0x00049524
		public MessageEnvelope GetMessageEnvelope()
		{
			List<string> recipients = (from mailRecipient in this.Recipients
			select mailRecipient.ToString()).ToList<string>();
			MessageEnvelope messageEnvelope = new MessageEnvelope(this.Priority, this.ExternalOrganizationId, this.DateReceived, this.From, this.Directionality, this.MimeDocument, this.MimeSize, this.Subject, this.MsgId, recipients);
			if (this.ExoAccountForest != null)
			{
				messageEnvelope.AddProperty<string>(MessageEnvelope.AccountForestProperty, this.ExoAccountForest);
			}
			return messageEnvelope;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0004B3B6 File Offset: 0x000495B6
		public bool IsJournalReport()
		{
			return this.storage.IsJournalReport;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0004B3C4 File Offset: 0x000495C4
		public bool IsPfReplica()
		{
			int num;
			return this.ExtendedProperties.TryGetValue<int>("Microsoft.Exchange.Transport.DirectoryData.RecipientType", out num) && num == 12;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0004B3EC File Offset: 0x000495EC
		public void PrepareJournalReport()
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			if (this.From.Equals(RoutingAddress.NullReversePath) && VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SetMustDeliverJournalReport.Enabled)
			{
				this.SetMustDeliver();
			}
			this.ExtendedProperties.SetValue<string>("Microsoft.Exchange.ContentIdentifier", "EXJournalData");
			HeaderList headers = this.RootPart.Headers;
			if (headers.FindFirst("X-MS-Exchange-Organization-Journal-Report") == null)
			{
				headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-Journal-Report", null));
			}
			if (headers.FindFirst("X-MS-Journal-Report") == null)
			{
				headers.AppendChild(new TextHeader("X-MS-Journal-Report", null));
			}
			if (this.Priority == DeliveryPriority.Normal)
			{
				this.PrioritizationReason = "JournalReport";
				this.Priority = DeliveryPriority.Low;
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004B4B7 File Offset: 0x000496B7
		public void SetMustDeliver()
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			this.storage.RetryDeliveryIfRejected = true;
			this.storage.TransportPropertiesHeader.SetBoolValue("MustDeliver", true);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004B4E7 File Offset: 0x000496E7
		public void SetQueuedForDelivery(bool value)
		{
			if (this.queuedForDelivery == value)
			{
				return;
			}
			this.storage.IsReadOnly = value;
			this.queuedForDelivery = value;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004B508 File Offset: 0x00049708
		public bool ValidateDeliveryPriority(out SmtpResponse failureResponse)
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			failureResponse = SmtpResponse.Empty;
			if (!TransportMailItem.configuration.LocalServer.IsBridgehead)
			{
				return true;
			}
			if (!TransportMailItem.configuration.AppConfig.RemoteDelivery.PriorityQueuingEnabled)
			{
				return true;
			}
			this.CheckDeliveryPriority();
			if (((IQueueItem)this).Priority == DeliveryPriority.High && TransportMailItem.configuration.AppConfig.RemoteDelivery.MaxHighPriorityMessageSize.ToBytes() < (ulong)this.MimeSize)
			{
				((IQueueItem)this).Priority = DeliveryPriority.Normal;
				ExTraceGlobals.GeneralTracer.TraceDebug<long>(0L, "Message with Id {0} exceeds the maximum size allowed for high-priority messages. Priority downgraded to Normal.", this.RecordId);
			}
			return true;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004B5A8 File Offset: 0x000497A8
		public Stream OpenMimeReadStream()
		{
			return this.OpenMimeReadStream(false);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004B5B1 File Offset: 0x000497B1
		public Stream OpenMimeReadStream(bool downConvert)
		{
			return this.storage.OpenMimeReadStream(downConvert);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004B5BF File Offset: 0x000497BF
		public Stream OpenMimeWriteStream()
		{
			return this.OpenMimeWriteStream(MimeLimits.Unlimited, true);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0004B5CD File Offset: 0x000497CD
		public Stream OpenMimeWriteStream(MimeLimits mimeLimits)
		{
			return this.OpenMimeWriteStream(mimeLimits, true);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0004B5D7 File Offset: 0x000497D7
		public Stream OpenMimeWriteStream(MimeLimits mimeLimits, bool expectBinaryContent)
		{
			return this.storage.OpenMimeWriteStream(mimeLimits, expectBinaryContent);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0004B5E6 File Offset: 0x000497E6
		public long GetCurrrentMimeSize()
		{
			return this.storage.GetCurrrentMimeSize();
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0004B5F3 File Offset: 0x000497F3
		public long RefreshMimeSize()
		{
			return this.storage.RefreshMimeSize();
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004B600 File Offset: 0x00049800
		public void ResetMimeParserEohCallback()
		{
			this.storage.ResetMimeParserEohCallback();
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004B610 File Offset: 0x00049810
		public void AddDsnParameters(string key, object value)
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			if (this.dsnParameters == null)
			{
				DsnParameters value2 = new DsnParameters();
				Interlocked.CompareExchange<DsnParameters>(ref this.dsnParameters, value2, null);
			}
			lock (this.dsnParameters)
			{
				this.dsnParameters[key] = value;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0004B680 File Offset: 0x00049880
		public void IncrementPoisonForRemoteCount()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0004B688 File Offset: 0x00049888
		public void Ack(AckStatus ackStatus, SmtpResponse smtpResponse, IEnumerable<MailRecipient> solutionRecipients, Queue<AckStatusAndResponse> recipientResponses)
		{
			IList<MailRecipient> recipientsToBeResubmitted = new List<MailRecipient>();
			this.audit.Drop(Breadcrumb.AcknowledgeA);
			bool flag;
			bool flag2;
			this.Ack(ackStatus, smtpResponse, null, solutionRecipients, AdminActionStatus.None, recipientResponses, null, recipientsToBeResubmitted, out flag, out flag2);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0004B6BE File Offset: 0x000498BE
		public bool TryCreateExportStream(out Stream stream)
		{
			return ExportStream.TryCreate(this, this.Recipients, false, out stream);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0004B6D0 File Offset: 0x000498D0
		public void Ack(AckStatus messageAckStatus, SmtpResponse smtpResponse, AckDetails ackDetails, IEnumerable<MailRecipient> recipients, AdminActionStatus adminActionStatus, Queue<AckStatusAndResponse> recipientResponses, Destination deliveredDestination, IList<MailRecipient> recipientsToBeResubmitted, out bool shouldEnqueueActive, out bool shouldEnqueueRetry)
		{
			this.audit.Drop(Breadcrumb.AcknowledgeB);
			shouldEnqueueActive = false;
			shouldEnqueueRetry = false;
			bool flag = this.RetryDeliveryIfRejected && adminActionStatus != AdminActionStatus.PendingDeleteWithOutNDR && adminActionStatus != AdminActionStatus.PendingDeleteWithNDR;
			if (flag && messageAckStatus == AckStatus.Fail)
			{
				messageAckStatus = AckStatus.Retry;
				smtpResponse = TransportMailItem.ReplaceFailWithRetryResponse(smtpResponse);
				if (recipientResponses == null)
				{
					recipientResponses = new Queue<AckStatusAndResponse>(0);
				}
			}
			if (recipientResponses == null && messageAckStatus != AckStatus.Fail)
			{
				throw new ArgumentException("ackStatus must be fail when recipientResponses is null");
			}
			if (adminActionStatus == AdminActionStatus.PendingDeleteWithOutNDR)
			{
				foreach (MailRecipient mailRecipient in recipients)
				{
					if (mailRecipient.Status == Status.Ready || mailRecipient.Status == Status.Retry || mailRecipient.Status == Status.Locked)
					{
						mailRecipient.DsnRequested = DsnRequestedFlags.Never;
					}
				}
			}
			bool flag2 = recipientResponses != null && recipientResponses.Count > 0;
			bool flag5;
			if (messageAckStatus == AckStatus.Pending || messageAckStatus == AckStatus.Retry)
			{
				bool flag3 = messageAckStatus == AckStatus.Pending && smtpResponse.Equals(SmtpResponse.Empty) && (recipientResponses == null || recipientResponses.Count == 0);
				IEnumerator<MailRecipient> enumerator2 = recipients.GetEnumerator();
				bool flag4 = messageAckStatus == AckStatus.Pending;
				TransportMailItem.DetermineEnqueueActiveOrRetry(flag4, !flag4, flag, out shouldEnqueueActive, out shouldEnqueueRetry, out flag5);
				IL_1EB:
				while (recipientResponses != null)
				{
					if (recipientResponses.Count <= 0)
					{
						break;
					}
					AckStatusAndResponse ackStatusAndResponse = recipientResponses.Dequeue();
					while (enumerator2.MoveNext())
					{
						MailRecipient mailRecipient2 = enumerator2.Current;
						if (mailRecipient2.Status == Status.Ready)
						{
							if (ackStatusAndResponse.AckStatus == AckStatus.Resubmit)
							{
								recipientsToBeResubmitted.Add(mailRecipient2);
								mailRecipient2.Ack(ackStatusAndResponse.AckStatus, ackStatusAndResponse.SmtpResponse);
								goto IL_1EB;
							}
							if (ackStatusAndResponse.AckStatus == AckStatus.Fail)
							{
								if (flag)
								{
									mailRecipient2.Ack(AckStatus.Retry, TransportMailItem.ReplaceFailWithRetryResponse(ackStatusAndResponse.SmtpResponse));
									goto IL_1EB;
								}
								mailRecipient2.Ack(ackStatusAndResponse.AckStatus, ackStatusAndResponse.SmtpResponse);
								goto IL_1EB;
							}
							else
							{
								if (ackStatusAndResponse.AckStatus == AckStatus.Retry && ackStatusAndResponse.SmtpResponse.SmtpResponseType != SmtpResponseType.Success && messageAckStatus != AckStatus.Pending)
								{
									mailRecipient2.Ack(ackStatusAndResponse.AckStatus, ackStatusAndResponse.SmtpResponse);
									goto IL_1EB;
								}
								mailRecipient2.Ack(messageAckStatus, smtpResponse);
								goto IL_1EB;
							}
						}
					}
					throw new InvalidOperationException("recipient collection must have changed");
				}
				while (enumerator2.MoveNext())
				{
					MailRecipient mailRecipient2 = enumerator2.Current;
					if (mailRecipient2.Status == Status.Ready && !flag3)
					{
						mailRecipient2.Ack(messageAckStatus, smtpResponse);
					}
				}
			}
			else
			{
				if (messageAckStatus == AckStatus.Success)
				{
					TransportMailItem.DetermineEnqueueActiveOrRetry(recipientResponses, flag, out shouldEnqueueActive, out shouldEnqueueRetry, out flag5);
					IEnumerator<MailRecipient> enumerator3 = recipients.GetEnumerator();
					IL_318:
					while (recipientResponses.Count > 0)
					{
						AckStatusAndResponse ackStatusAndResponse2 = recipientResponses.Dequeue();
						while (enumerator3.MoveNext())
						{
							MailRecipient mailRecipient3 = enumerator3.Current;
							if (mailRecipient3.Status == Status.Ready)
							{
								if (flag && ackStatusAndResponse2.AckStatus == AckStatus.Fail)
								{
									ackStatusAndResponse2 = new AckStatusAndResponse(AckStatus.Retry, TransportMailItem.ReplaceFailWithRetryResponse(ackStatusAndResponse2.SmtpResponse));
								}
								if (ackStatusAndResponse2.AckStatus == AckStatus.Resubmit)
								{
									recipientsToBeResubmitted.Add(mailRecipient3);
									mailRecipient3.Ack(ackStatusAndResponse2.AckStatus, ackStatusAndResponse2.SmtpResponse);
									goto IL_318;
								}
								if (ackStatusAndResponse2.AckStatus == AckStatus.Retry && shouldEnqueueActive)
								{
									mailRecipient3.Ack(AckStatus.Pending, smtpResponse);
									goto IL_318;
								}
								if ((ackStatusAndResponse2.AckStatus == AckStatus.Success || ackStatusAndResponse2.AckStatus == AckStatus.SuccessNoDsn) && deliveredDestination != null)
								{
									mailRecipient3.DeliveredDestination = deliveredDestination;
								}
								mailRecipient3.Ack(ackStatusAndResponse2.AckStatus, ackStatusAndResponse2.SmtpResponse);
								goto IL_318;
							}
						}
						throw new InvalidOperationException("recipient collection must have changed");
					}
					if (flag2)
					{
						while (enumerator3.MoveNext())
						{
							MailRecipient mailRecipient3 = enumerator3.Current;
							if (mailRecipient3.Status == Status.Ready)
							{
								mailRecipient3.Ack(AckStatus.Pending, smtpResponse);
								shouldEnqueueActive = true;
							}
						}
					}
					if (!this.IsHeartbeat)
					{
						goto IL_4E7;
					}
					using (IEnumerator<MailRecipient> enumerator4 = this.Recipients.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							MailRecipient mailRecipient4 = enumerator4.Current;
							mailRecipient4.Ack(AckStatus.SuccessNoDsn, SmtpResponse.Empty);
						}
						goto IL_4E7;
					}
				}
				bool flag6 = smtpResponse.Equals(SmtpResponse.NoRecipientSucceeded);
				TransportMailItem.DetermineEnqueueActiveOrRetry(recipientResponses, flag, out shouldEnqueueActive, out shouldEnqueueRetry, out flag5);
				foreach (MailRecipient mailRecipient5 in recipients)
				{
					if (mailRecipient5.Status == Status.Ready)
					{
						if (recipientResponses != null && recipientResponses.Count > 0)
						{
							AckStatusAndResponse ackStatusAndResponse3 = recipientResponses.Dequeue();
							if (ackStatusAndResponse3.AckStatus == AckStatus.Resubmit)
							{
								recipientsToBeResubmitted.Add(mailRecipient5);
								mailRecipient5.Ack(ackStatusAndResponse3.AckStatus, ackStatusAndResponse3.SmtpResponse);
							}
							else if (flag6 || ackStatusAndResponse3.AckStatus == AckStatus.Fail)
							{
								mailRecipient5.Ack(ackStatusAndResponse3.AckStatus, ackStatusAndResponse3.SmtpResponse);
							}
							else if (ackStatusAndResponse3.AckStatus == AckStatus.Pending || ackStatusAndResponse3.AckStatus == AckStatus.Retry)
							{
								if (shouldEnqueueActive && shouldEnqueueRetry)
								{
									mailRecipient5.Ack(AckStatus.Pending, smtpResponse);
								}
								else
								{
									mailRecipient5.Ack(ackStatusAndResponse3.AckStatus, ackStatusAndResponse3.SmtpResponse);
								}
							}
							else
							{
								mailRecipient5.Ack(AckStatus.Fail, smtpResponse);
							}
						}
						else if (flag2)
						{
							mailRecipient5.Ack(AckStatus.Pending, smtpResponse);
						}
						else
						{
							mailRecipient5.Ack(AckStatus.Fail, smtpResponse);
						}
					}
					else if (mailRecipient5.Status == Status.Retry || mailRecipient5.Status == Status.Locked)
					{
						mailRecipient5.Ack(AckStatus.Fail, smtpResponse);
					}
				}
			}
			IL_4E7:
			if (flag5)
			{
				TransportMailItem.Logger.LogEvent(TransportEventLogConstants.Tuple_RetryDeliveryIfRejected, "RetryDeliveryIfRejected", new object[]
				{
					this.MsgId
				});
			}
			if ((shouldEnqueueActive || shouldEnqueueRetry) && (adminActionStatus == AdminActionStatus.PendingDeleteWithNDR || adminActionStatus == AdminActionStatus.PendingDeleteWithOutNDR))
			{
				shouldEnqueueActive = false;
				shouldEnqueueRetry = false;
				foreach (MailRecipient mailRecipient6 in recipients)
				{
					if (mailRecipient6.Status == Status.Ready || mailRecipient6.Status == Status.Retry || mailRecipient6.Status == Status.Locked)
					{
						mailRecipient6.Ack(AckStatus.Fail, AckReason.MessageDelayedDeleteByAdmin);
					}
				}
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004BCCC File Offset: 0x00049ECC
		public void MinimizeMemory()
		{
			this.storage.MinimizeMemory();
			if (this.ADRecipientCache != null)
			{
				this.ADRecipientCache.Clear();
			}
			TransportMailItem.resourceManager.HintGCCollectCouldBeEffective();
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0004BCF6 File Offset: 0x00049EF6
		public void CommitLazyAndDehydrate(Breadcrumb breadcrumb)
		{
			this.audit.Drop(breadcrumb);
			this.CommitAndDehydrate(TransactionCommitMode.MediumLatencyLazy);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004BD0B File Offset: 0x00049F0B
		public void CommitLazyAndDehydrateMessageIfPossible(Breadcrumb breadcrumb)
		{
			this.audit.Drop(breadcrumb);
			this.CommitAndDehydrateIfPossible(TransactionCommitMode.MediumLatencyLazy);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0004BD3C File Offset: 0x00049F3C
		public void CommitLazyAndDehydrate(Breadcrumb breadcrumb, NextHopSolution solution)
		{
			if (this.IsRowDeleted && solution != null)
			{
				throw new InvalidOperationException("Commit and dehydration for a specific solution cannot be performed when deleting a mail item");
			}
			this.RunScopedDatabaseOperation(solution, delegate
			{
				this.CommitLazyAndDehydrate(breadcrumb);
			});
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004BDA4 File Offset: 0x00049FA4
		public void CommitLazyAndDehydrateMessageIfPossible(Breadcrumb breadcrumb, NextHopSolution solution)
		{
			if (this.IsRowDeleted && solution != null)
			{
				throw new InvalidOperationException("Commit and dehydration if possible for a specific solution cannot be performed when deleting a mail item");
			}
			this.RunScopedDatabaseOperation(solution, delegate
			{
				this.CommitLazyAndDehydrateMessageIfPossible(breadcrumb);
			});
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0004BDF0 File Offset: 0x00049FF0
		public void UpdateNextHopSolutionTable(NextHopSolutionKey key, MailRecipient recipient)
		{
			NextHopSolution nextHopSolution;
			if (!this.nextHopSolutionTable.TryGetValue(key, out nextHopSolution))
			{
				nextHopSolution = ((key.NextHopType != NextHopType.Unreachable) ? new NextHopSolution(key) : new UnreachableSolution(key));
				this.nextHopSolutionTable.Add(key, nextHopSolution);
			}
			nextHopSolution.AddRecipient(recipient);
			UnreachableSolution unreachableSolution = nextHopSolution as UnreachableSolution;
			if (unreachableSolution == null)
			{
				return;
			}
			if (recipient.UnreachableReason != UnreachableReason.None)
			{
				unreachableSolution.AddUnreachableReason(recipient.UnreachableReason);
				return;
			}
			throw new InvalidOperationException("Recipient does not have unreachable reason");
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0004BE70 File Offset: 0x0004A070
		public NextHopSolution UpdateNextHopSolutionTable(NextHopSolutionKey key, IEnumerable<MailRecipient> recipients, bool shouldCloneSolutionsTable)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			NextHopSolution nextHopSolution;
			if (!this.nextHopSolutionTable.TryGetValue(key, out nextHopSolution))
			{
				nextHopSolution = ((key.NextHopType != NextHopType.Unreachable) ? new NextHopSolution(key) : new UnreachableSolution(key));
				if (shouldCloneSolutionsTable)
				{
					this.nextHopSolutionTable = new Dictionary<NextHopSolutionKey, NextHopSolution>(this.NextHopSolutions)
					{
						{
							key,
							nextHopSolution
						}
					};
				}
				else
				{
					this.nextHopSolutionTable.Add(key, nextHopSolution);
				}
			}
			nextHopSolution.AddRecipients(recipients);
			UnreachableSolution unreachableSolution = nextHopSolution as UnreachableSolution;
			if (unreachableSolution != null)
			{
				foreach (MailRecipient mailRecipient in recipients)
				{
					if (mailRecipient.UnreachableReason == UnreachableReason.None)
					{
						throw new InvalidOperationException("Recipient does not have unreachable reason");
					}
					unreachableSolution.AddUnreachableReason(mailRecipient.UnreachableReason);
				}
			}
			return nextHopSolution;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004BF58 File Offset: 0x0004A158
		public void BumpExpirationTime()
		{
			this.ExtensionToExpiryDuration = DateTime.UtcNow - this.DateReceived;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004BF70 File Offset: 0x0004A170
		public void SetExpirationTime(DateTime requestedExpiryTime)
		{
			DateTime expiry = this.Expiry;
			if (expiry != DateTime.MaxValue)
			{
				this.ExtensionToExpiryDuration = requestedExpiryTime - expiry + this.ExtensionToExpiryDuration;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004BFA9 File Offset: 0x0004A1A9
		void IQueueItem.Update()
		{
			this.CommitLazyAndDehydrate(Breadcrumb.DehydrateOnMailItemUpdate);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004BFB6 File Offset: 0x0004A1B6
		MessageContext IQueueItem.GetMessageContext(MessageProcessingSource source)
		{
			return new MessageContext(this.RecordId, this.InternetMessageId, source);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004BFCC File Offset: 0x0004A1CC
		public DateTime GetExpiryTime(bool honorRetryIfNotDelivered)
		{
			if (honorRetryIfNotDelivered && this.storage.RetryDeliveryIfRejected)
			{
				return DateTime.MaxValue;
			}
			TimeSpan t;
			if (TransportMailItem.configuration.AppConfig.RemoteDelivery.PriorityQueuingEnabled)
			{
				t = TransportMailItem.configuration.AppConfig.RemoteDelivery.MessageExpirationTimeout(((IQueueItem)this).Priority);
			}
			else
			{
				t = TransportMailItem.configuration.LocalServer.TransportServer.MessageExpirationTimeout;
			}
			return this.DateReceived + this.ExtensionToExpiryDuration + t;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0004C053 File Offset: 0x0004A253
		public void CommitImmediate()
		{
			this.Commit(TransactionCommitMode.Immediate);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004C05C File Offset: 0x0004A25C
		public void CommitLazy()
		{
			this.Commit(TransactionCommitMode.MediumLatencyLazy);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004C06D File Offset: 0x0004A26D
		public void CommitLazy(NextHopSolution solution)
		{
			if (this.IsRowDeleted && solution != null)
			{
				throw new InvalidOperationException("Commit for a specific solution cannot be performed when deleting a mail item");
			}
			this.RunScopedDatabaseOperation(solution, delegate
			{
				this.CommitLazy();
			});
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004C098 File Offset: 0x0004A298
		public IAsyncResult BeginCommitForReceive(AsyncCallback asyncCallback, object asyncState)
		{
			if (this.IsInAsyncCommit)
			{
				throw new InvalidOperationException("This mail item is already in an async commit.");
			}
			this.audit.Drop(Breadcrumb.CommitForReceive);
			return this.storage.BeginCommit(asyncCallback, asyncState);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004C0D7 File Offset: 0x0004A2D7
		public bool EndCommitForReceive(IAsyncResult ar, out Exception exception)
		{
			return this.storage.EndCommit(ar, out exception);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004C0E6 File Offset: 0x0004A2E6
		IAsyncResult ITransportMailItemFacade.BeginCommitForReceive(AsyncCallback asyncCallback, object asyncState)
		{
			return this.BeginCommitForReceive(asyncCallback, asyncState);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
		bool ITransportMailItemFacade.EndCommitForReceive(IAsyncResult ar, out Exception exception)
		{
			return this.EndCommitForReceive(ar, out exception);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004C0FA File Offset: 0x0004A2FA
		public TransportMailItem NewCloneWithoutRecipients()
		{
			return this.NewCloneWithoutRecipients(true, this.latencyTracker);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004C109 File Offset: 0x0004A309
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			return this.NewCloneWithoutRecipients(shareRecipientCache, this.latencyTracker);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004C118 File Offset: 0x0004A318
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache, LatencyTracker latencyTrackerToClone)
		{
			this.audit.Drop(Breadcrumb.CommitNow);
			TransportMailItem transportMailItem;
			lock (this)
			{
				if (Components.TransportAppConfig.QueueDatabase.CloneInOriginalGeneration)
				{
					transportMailItem = new TransportMailItem(this, shareRecipientCache, latencyTrackerToClone, this.transportRulesForkCount, false);
				}
				else
				{
					using (Transaction transaction = TransportMailItem.Database.BeginNewTransaction())
					{
						this.storage.Materialize(transaction);
						transportMailItem = new TransportMailItem(this, shareRecipientCache, latencyTrackerToClone);
						transportMailItem.audit.Drop(Breadcrumb.CommitNow);
						transportMailItem.transportRulesForkCount = this.transportRulesForkCount;
						transportMailItem.storage.Materialize(transaction);
						transaction.Commit();
					}
				}
			}
			if (this.IsShadowed() && TransportMailItem.shadowRedundancyComponent != null)
			{
				TransportMailItem.shadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemBifurcated(this.ShadowServerContext, this.ShadowServerDiscardId);
			}
			return transportMailItem;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004C230 File Offset: 0x0004A430
		public TransportMailItem CreateNewCopyWithoutRecipients(NextHopSolution scopingSolution)
		{
			TransportMailItem copy = null;
			this.RunScopedDatabaseOperation(scopingSolution, delegate
			{
				copy = this.CreateNewCopyWithoutRecipients();
			});
			return copy;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004C26C File Offset: 0x0004A46C
		public TransportMailItem CreateNewCopyWithoutRecipients()
		{
			this.audit.Drop(Breadcrumb.CommitNow);
			TransportMailItem result;
			lock (this)
			{
				result = new TransportMailItem(this, false, null, this.transportRulesForkCount, true);
			}
			if (this.IsShadowed() && TransportMailItem.shadowRedundancyComponent != null)
			{
				TransportMailItem.shadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemBifurcated(this.ShadowServerContext, this.ShadowServerDiscardId);
			}
			return result;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004C314 File Offset: 0x0004A514
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache, LatencyTracker latencyTrackerToClone, NextHopSolution scopingSolution)
		{
			TransportMailItem clone = null;
			this.RunScopedDatabaseOperation(scopingSolution, delegate
			{
				clone = this.NewCloneWithoutRecipients(shareRecipientCache, latencyTrackerToClone);
			});
			return clone;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004C35C File Offset: 0x0004A55C
		public void UpdateDirectionalityAndScopeHeaders()
		{
			string value;
			switch (this.Directionality)
			{
			case MailDirectionality.Undefined:
				throw new InvalidOperationException("Cannot set directionality to Undefined");
			case MailDirectionality.Originating:
				value = MultiTenantTransport.OriginatingStr;
				break;
			case MailDirectionality.Incoming:
				value = MultiTenantTransport.IncomingStr;
				break;
			default:
				throw new InvalidOperationException(string.Format("Unexpected directionality: {0}", this.Directionality));
			}
			Util.SetAsciiHeader(this.RootPart.Headers, "X-MS-Exchange-Organization-MessageDirectionality", value);
			Util.SetAsciiHeader(this.RootPart.Headers, "X-MS-Exchange-Organization-Id", this.ExternalOrganizationId.ToString());
			Util.SetScopeHeaders(this.RootPart.Headers, this.OrganizationScope);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004C413 File Offset: 0x0004A613
		public void ReleaseFromActiveMaterializedLazy()
		{
			this.audit.Drop(Breadcrumb.MailItemDeleted);
			if (this.IsShadowed() && TransportMailItem.shadowRedundancyComponent != null)
			{
				TransportMailItem.shadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemReleased(this);
			}
			this.ReleaseFromActive();
			this.CommitLazy();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004C450 File Offset: 0x0004A650
		public void ReleaseFromActive()
		{
			this.Recipients.Clear();
			if (this.IsActive)
			{
				this.storage.ReleaseFromActive();
				if (this.OnReleaseFromActive != null)
				{
					this.OnReleaseFromActive(this);
				}
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004C484 File Offset: 0x0004A684
		public void ResetToActive()
		{
			if (!this.IsActive)
			{
				this.storage.IsActive = true;
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004C49C File Offset: 0x0004A69C
		public void ReleaseFromShadowRedundancy()
		{
			if (this.IsShadow())
			{
				throw new InvalidOperationException("ReleaseFromShadowRedundancy() is called on MailItem while IsShadow() is true.");
			}
			bool flag = this.Status == Status.Complete || this.NextHopSolutions.Count == 0;
			if ((this.IsHeartbeat || flag) && this.IsActive)
			{
				this.ReleaseFromActive();
			}
			if (flag)
			{
				this.CommitLazyAndDehydrate(Breadcrumb.DehydrateOnReleaseFromShadowRedundancy, null);
				return;
			}
			this.CommitLazy();
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004C508 File Offset: 0x0004A708
		public void ReleaseFromRemoteDelivery()
		{
			if (this.Status != Status.Complete)
			{
				throw new InvalidOperationException("ReleaseFromRemoteDelivery() is called on MailItem while Status != Status.Complete.");
			}
			this.audit.Drop(Breadcrumb.MailItemDelivered);
			this.SetQueuedForDelivery(false);
			if (this.IsShadowed() && TransportMailItem.shadowRedundancyComponent != null)
			{
				TransportMailItem.shadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemDelivered(this);
			}
			if (!this.IsShadow() && this.IsActive && !this.IsPoison)
			{
				this.ReleaseFromActive();
			}
			this.CommitLazyAndDehydrate(Breadcrumb.DehydrateOnReleaseFromRemoteDelivery);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004C588 File Offset: 0x0004A788
		public bool IsShadow()
		{
			if (this.NextHopSolutions.Count > 0)
			{
				using (Dictionary<NextHopSolutionKey, NextHopSolution>.KeyCollection.Enumerator enumerator = this.NextHopSolutions.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NextHopSolutionKey nextHopSolutionKey = enumerator.Current;
						if (nextHopSolutionKey.NextHopType.DeliveryType == DeliveryType.ShadowRedundancy)
						{
							return true;
						}
					}
					return false;
				}
			}
			foreach (MailRecipient mailRecipient in this.Recipients)
			{
				if (!string.IsNullOrEmpty(mailRecipient.PrimaryServerFqdnGuid))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0004C650 File Offset: 0x0004A850
		public bool IsShadowed()
		{
			return !string.IsNullOrEmpty(this.ShadowServerContext);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004C660 File Offset: 0x0004A860
		public bool IsDelayedAck()
		{
			return this.IsShadowed() && string.Equals(this.ShadowServerContext, "$localhost$", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0004C67D File Offset: 0x0004A87D
		public bool IsShadowedByXShadow()
		{
			return this.IsShadowed() && !this.IsDelayedAck();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0004C694 File Offset: 0x0004A894
		public void AddRecipient(IMailRecipientStorage recipStorage)
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			MailRecipient mailRecipient = MailRecipient.NewMessageRecipient(this, recipStorage);
			if (this.LoadedInRestart && (mailRecipient.Status == Status.Retry || mailRecipient.Status == Status.Locked))
			{
				mailRecipient.Status = Status.Ready;
			}
			if (mailRecipient.AdminActionStatus == AdminActionStatus.SuspendedInSubmissionQueue)
			{
				((IQueueItem)this).DeferUntil = DateTime.MaxValue;
				if (this.Recipients.Count > 0)
				{
					this.Recipients.Prepend(mailRecipient);
					return;
				}
			}
			this.Recipients.Add(mailRecipient);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004C711 File Offset: 0x0004A911
		public void CacheTransportSettings()
		{
			if (this.transportSettings == null)
			{
				if (this.recipientCache == null)
				{
					throw new InvalidOperationException("ADRecipient cache should be set before calling this method");
				}
				Interlocked.Exchange<PerTenantTransportSettings>(ref this.transportSettings, Components.Configuration.GetTransportSettings(this.recipientCache.OrganizationId));
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0004C74F File Offset: 0x0004A94F
		public void ClearTransportSettings()
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			this.transportSettings = null;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004C764 File Offset: 0x0004A964
		public void RestoreLastSavedMime(string agentName, string eventName)
		{
			this.storage.RestoreLastSavedMime();
			string messageId = this.Message.MessageId;
			TransportMailItem.Logger.LogEvent(TransportEventLogConstants.Tuple_AgentDidNotCloseMimeStream, null, new object[]
			{
				agentName,
				eventName,
				messageId
			});
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004C7AD File Offset: 0x0004A9AD
		public void Suspend()
		{
			if (this.Recipients[0].AdminActionStatus != AdminActionStatus.SuspendedInSubmissionQueue)
			{
				this.DeferUntil = DateTime.MaxValue;
				this.Recipients[0].AdminActionStatus = AdminActionStatus.SuspendedInSubmissionQueue;
				this.CommitLazy();
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004C7E6 File Offset: 0x0004A9E6
		public void Resume()
		{
			if (this.Recipients[0].AdminActionStatus == AdminActionStatus.SuspendedInSubmissionQueue)
			{
				this.DeferUntil = DateTime.MinValue;
				this.Recipients[0].AdminActionStatus = AdminActionStatus.None;
				this.CommitLazy();
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004C81F File Offset: 0x0004AA1F
		public void UpdateCachedHeaders()
		{
			this.storage.UpdateCachedHeaders();
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004C82C File Offset: 0x0004AA2C
		public string GetReceiveConnectorName()
		{
			string text = this.ReceiveConnectorName;
			string text2 = "Replay:";
			if (text.StartsWith(text2, StringComparison.Ordinal) && text.Length > text2.Length)
			{
				text = text.Substring(text2.Length);
			}
			text2 = "SMTP:";
			if (text.StartsWith(text2, StringComparison.Ordinal) && text.Length > text2.Length)
			{
				text = text.Substring(text2.Length);
			}
			return text;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004C898 File Offset: 0x0004AA98
		internal PerTenantRemoteDomainTable GetOrgRemoteDomains()
		{
			PerTenantRemoteDomainTable result = null;
			if (TransportMailItem.configuration == null)
			{
				return result;
			}
			if (this.OrganizationId == null || !TransportMailItem.configuration.TryGetRemoteDomainTable(this.OrganizationId, out result))
			{
				result = TransportMailItem.configuration.GetRemoteDomainTable(OrganizationId.ForestWideOrgId);
			}
			return result;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004C8E4 File Offset: 0x0004AAE4
		internal RemoteDomainEntry GetDefaultDomain()
		{
			PerTenantRemoteDomainTable orgRemoteDomains = this.GetOrgRemoteDomains();
			if (orgRemoteDomains == null)
			{
				return null;
			}
			return orgRemoteDomains.RemoteDomainTable.Star;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004C908 File Offset: 0x0004AB08
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004C90F File Offset: 0x0004AB0F
		public void DropBreadcrumb(Breadcrumb breadcrumb)
		{
			this.audit.Drop(breadcrumb);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004C91D File Offset: 0x0004AB1D
		public void DropCatBreadcrumb(CategorizerBreadcrumb breadcrumb)
		{
			if (this.throttlingContext == null)
			{
				return;
			}
			this.throttlingContext.AddBreadcrumb(breadcrumb);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004C934 File Offset: 0x0004AB34
		internal static LatencyComponent GetDeferLatencyComponent(DeferReason deferReason)
		{
			LatencyComponent result;
			if (deferReason != DeferReason.ReroutedByStoreDriver)
			{
				if (deferReason != DeferReason.RecipientThreadLimitExceeded)
				{
					result = LatencyComponent.Deferral;
				}
				else
				{
					result = LatencyComponent.DeliveryQueueMailboxRecipientThreadLimitExceeded;
				}
			}
			else
			{
				result = LatencyComponent.MailboxMove;
			}
			return result;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004C95C File Offset: 0x0004AB5C
		internal static string GetSourceID(TransportMailItem mailItem)
		{
			HeaderList headers = mailItem.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-AuthAs");
			bool flag = header != null && "Internal" == header.Value;
			string result;
			if (RoutingAddress.NullReversePath != mailItem.From && RoutingAddress.Empty != mailItem.From)
			{
				result = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					mailItem.From.ToString().ToLowerInvariant(),
					flag ? null : "-u"
				});
			}
			else
			{
				Header header2 = headers.FindFirst(HeaderId.From);
				if (header2 == null || string.IsNullOrEmpty(header2.Value))
				{
					header2 = headers.FindFirst(HeaderId.Sender);
				}
				if (header2 != null && !string.IsNullOrEmpty(header2.Value))
				{
					result = string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
					{
						header2.Value.ToLowerInvariant(),
						flag ? null : "-u"
					});
				}
				else
				{
					result = ((mailItem.InternetMessageId == null) ? "NoMessageID" : mailItem.InternetMessageId.ToLowerInvariant());
				}
			}
			return result;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004CA98 File Offset: 0x0004AC98
		internal void AddAgentInfo(string agentName, string eventName, List<KeyValuePair<string, string>> data)
		{
			if (string.IsNullOrEmpty(agentName))
			{
				throw new ArgumentNullException("agentName");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (this.messageTrackingAgentInfo == null)
			{
				this.messageTrackingAgentInfo = new List<List<KeyValuePair<string, string>>>();
			}
			data.Insert(0, new KeyValuePair<string, string>(agentName, eventName));
			this.messageTrackingAgentInfo.Add(data);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004CAF4 File Offset: 0x0004ACF4
		internal List<List<KeyValuePair<string, string>>> ClaimAgentInfo()
		{
			this.MoveComponentCostToAgentInfo();
			List<List<KeyValuePair<string, string>>> result = this.messageTrackingAgentInfo;
			this.messageTrackingAgentInfo = null;
			return result;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004CB18 File Offset: 0x0004AD18
		internal void SetMimeDefaultEncoding()
		{
			RemoteDomainEntry fromDomainConfig = this.FromDomainConfig;
			RemoteDomainEntry defaultDomain = this.GetDefaultDomain();
			string text = null;
			string arg = string.Empty;
			if (fromDomainConfig != null && fromDomainConfig.NonMimeCharacterSet != null)
			{
				text = fromDomainConfig.NonMimeCharacterSet;
				arg = fromDomainConfig.DomainName.ToString();
			}
			else if (defaultDomain != null && defaultDomain.NonMimeCharacterSet != null)
			{
				text = defaultDomain.NonMimeCharacterSet;
				arg = defaultDomain.DomainName.ToString();
			}
			Encoding encoding;
			if (text != null && Charset.TryGetEncoding(text, out encoding))
			{
				Encoding mimeDefaultEncoding = this.storage.MimeDefaultEncoding;
				MimeDocument mimeDocument = this.MimeDocument;
				if (!encoding.Equals(mimeDefaultEncoding))
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string, Encoding>(0L, "Default encoding for content from domain {0} was configured as {1}", arg, encoding);
					this.storage.MimeDefaultEncoding = encoding;
				}
				if (mimeDocument != null && !encoding.Equals(mimeDocument.HeaderDecodingOptions.CharsetEncoding))
				{
					DecodingOptions decodingOptions = new DecodingOptions(mimeDocument.HeaderDecodingOptions.DecodingFlags, encoding);
					MimeInternalHelpers.SetDocumentDecodingOptions(mimeDocument, decodingOptions);
				}
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004CC08 File Offset: 0x0004AE08
		private static void CheckPendingAndRetryRecipients(Queue<AckStatusAndResponse> recipientResponses, bool mustDeliverJournalReport, out bool hasPendingRecipient, out bool hasRetryRecipient)
		{
			hasPendingRecipient = false;
			hasRetryRecipient = false;
			if (recipientResponses == null || recipientResponses.Count == 0)
			{
				return;
			}
			foreach (object obj in recipientResponses)
			{
				AckStatusAndResponse ackStatusAndResponse = (AckStatusAndResponse)obj;
				if (ackStatusAndResponse.AckStatus == AckStatus.Pending)
				{
					hasPendingRecipient = true;
				}
				else if (ackStatusAndResponse.AckStatus == AckStatus.Retry || (mustDeliverJournalReport && ackStatusAndResponse.AckStatus == AckStatus.Fail))
				{
					hasRetryRecipient = true;
				}
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0004CC71 File Offset: 0x0004AE71
		private static void DetermineEnqueueActiveOrRetry(bool hasPendingRecipient, bool hasRetryRecipient, bool mustDeliverJournalReport, out bool shouldEnqueueActive, out bool shouldEnqueueRetry, out bool specialMessageGoesInRetry)
		{
			shouldEnqueueActive = false;
			shouldEnqueueRetry = false;
			specialMessageGoesInRetry = false;
			if (hasPendingRecipient)
			{
				shouldEnqueueActive = true;
				return;
			}
			if (hasRetryRecipient)
			{
				shouldEnqueueRetry = true;
				if (mustDeliverJournalReport)
				{
					specialMessageGoesInRetry = true;
				}
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004CC94 File Offset: 0x0004AE94
		private static void DetermineEnqueueActiveOrRetry(Queue<AckStatusAndResponse> recipientResponses, bool mustDeliverJournalReport, out bool shouldEnqueueActive, out bool shouldEnqueueRetry, out bool specialMessageGoesInRetry)
		{
			bool hasPendingRecipient;
			bool hasRetryRecipient;
			TransportMailItem.CheckPendingAndRetryRecipients(recipientResponses, mustDeliverJournalReport, out hasPendingRecipient, out hasRetryRecipient);
			TransportMailItem.DetermineEnqueueActiveOrRetry(hasPendingRecipient, hasRetryRecipient, mustDeliverJournalReport, out shouldEnqueueActive, out shouldEnqueueRetry, out specialMessageGoesInRetry);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004CCB8 File Offset: 0x0004AEB8
		private void MoveComponentCostToAgentInfo()
		{
			int length = "Microsoft.Exchange.Transport.TransportMailItem.NonforkingComponentCost.".Length;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			List<string> list2 = new List<string>();
			foreach (KeyValuePair<string, object> keyValuePair in this.ExtendedPropertyDictionary)
			{
				if (keyValuePair.Key.StartsWith("Microsoft.Exchange.Transport.TransportMailItem.NonforkingComponentCost."))
				{
					list.Add(new KeyValuePair<string, string>(keyValuePair.Key.Substring(length), ((long)keyValuePair.Value).ToString()));
					list2.Add(keyValuePair.Key);
				}
			}
			if (list.Count > 0)
			{
				this.AddAgentInfo("CompCost", string.Empty, list);
				foreach (string key in list2)
				{
					this.ExtendedPropertyDictionary.Remove(key);
				}
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004CDC8 File Offset: 0x0004AFC8
		private DeliveryPriority EmailImportanceToDeliveryPriority()
		{
			switch (this.Message.Importance)
			{
			case Importance.Normal:
				return DeliveryPriority.Normal;
			case Importance.High:
				return DeliveryPriority.High;
			case Importance.Low:
				return DeliveryPriority.Low;
			default:
				throw new ArgumentException("Invalid EmailMessage.Importance value:" + this.Message.Importance);
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004CE1C File Offset: 0x0004B01C
		private DeliveryPriority EmailPriorityToDeliveryPriority()
		{
			switch (this.Message.Priority)
			{
			case Microsoft.Exchange.Data.Transport.Email.Priority.Normal:
				return DeliveryPriority.Normal;
			case Microsoft.Exchange.Data.Transport.Email.Priority.Urgent:
				return DeliveryPriority.High;
			case Microsoft.Exchange.Data.Transport.Email.Priority.NonUrgent:
				return DeliveryPriority.Low;
			default:
				throw new ArgumentException("Invalid EmailMessage.Priority value:" + this.Message.Priority);
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004CE70 File Offset: 0x0004B070
		private DeliveryPriority GetRequestedMessagePriority()
		{
			DeliveryPriority deliveryPriority = this.EmailImportanceToDeliveryPriority();
			DeliveryPriority deliveryPriority2 = this.EmailPriorityToDeliveryPriority();
			if (deliveryPriority >= deliveryPriority2)
			{
				return deliveryPriority2;
			}
			return deliveryPriority;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004CE94 File Offset: 0x0004B094
		private void CheckDeliveryPriority()
		{
			this.ThrowIfDeletedOrReadOnly();
			this.ThrowIfInAsyncCommit();
			if (this.storage.Priority != null)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug<long>(0L, "Message with Id {0} has the internal priority already set.", this.RecordId);
				return;
			}
			DeliveryPriority requestedMessagePriority = this.GetRequestedMessagePriority();
			if (requestedMessagePriority != DeliveryPriority.High)
			{
				((IQueueItem)this).Priority = requestedMessagePriority;
				ExTraceGlobals.GeneralTracer.TraceDebug<long, DeliveryPriority>(0L, "Message with Id {0} has {1} priority. No other checks are needed.", this.RecordId, requestedMessagePriority);
				return;
			}
			if (!MultilevelAuth.IsAuthenticated(this))
			{
				((IQueueItem)this).Priority = DeliveryPriority.Normal;
				ExTraceGlobals.GeneralTracer.TraceDebug<long>(0L, "Message with Id {0} requested High delivery priority but was not sent from an authenticated source. Priority downgraded to Normal.", this.RecordId);
				return;
			}
			RoutingAddress outer;
			if (!Util.TryGetP2Sender(this.RootPart.Headers, out outer))
			{
				((IQueueItem)this).Priority = DeliveryPriority.Normal;
				ExTraceGlobals.GeneralTracer.TraceDebug<long>(0L, "Message with Id {0} requested High delivery priority but had no valid P2 sender. Priority downgraded to Normal.", this.RecordId);
				return;
			}
			ProxyAddress innermostAddress = Sender.GetInnermostAddress(outer);
			try
			{
				Result<TransportMiniRecipient> result = this.ADRecipientCache.FindAndCacheRecipient(innermostAddress);
				if (result.Data == null)
				{
					((IQueueItem)this).Priority = requestedMessagePriority;
					ExTraceGlobals.GeneralTracer.TraceDebug<ProxyAddress, long>(0L, "Sender {0} of message with Id {1} not found in AD. The message was sent from an authenticated source; the High delivery priority is honored.", innermostAddress, this.RecordId);
				}
				else
				{
					bool downgradeHighPriorityMessagesEnabled = result.Data.DowngradeHighPriorityMessagesEnabled;
					if (downgradeHighPriorityMessagesEnabled)
					{
						((IQueueItem)this).Priority = DeliveryPriority.Normal;
						ExTraceGlobals.GeneralTracer.TraceDebug<ProxyAddress, long>(0L, "Sender {0} of message with Id {1} does not have permissions to send High-priority messages. Priority downgraded to Normal.", innermostAddress, this.RecordId);
					}
					else
					{
						((IQueueItem)this).Priority = requestedMessagePriority;
						ExTraceGlobals.GeneralTracer.TraceDebug<ProxyAddress, long>(0L, "Sender {0}  of message with Id {1} has permissions to send High-priority messages.", innermostAddress, this.RecordId);
					}
				}
			}
			catch (ADTransientException)
			{
				ExTraceGlobals.GeneralTracer.TraceError<ProxyAddress, long>(0L, "Cannot verify if sender {0} of message {1} has permission to send High-priority email because of transient AD errors. Priority downgraded to Normal.", innermostAddress, this.RecordId);
				((IQueueItem)this).Priority = DeliveryPriority.Normal;
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004D024 File Offset: 0x0004B224
		private void RunScopedDatabaseOperation(NextHopSolution scopingSolution, Action operation)
		{
			if (scopingSolution == null)
			{
				operation();
				return;
			}
			MailRecipientCollection recipients = new MailRecipientCollection(this, scopingSolution.Recipients);
			MailRecipientCollection recipients2 = this.Recipients;
			this.Recipients = recipients;
			this.audit.Drop(Breadcrumb.ScopedRecipients);
			try
			{
				operation();
			}
			finally
			{
				this.Recipients = recipients2;
				this.audit.Drop(Breadcrumb.UnscopedRecipients);
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004D098 File Offset: 0x0004B298
		private void Commit(TransactionCommitMode commitMode)
		{
			this.audit.Drop((commitMode == TransactionCommitMode.Lazy) ? Breadcrumb.CommitLazy : Breadcrumb.CommitNow);
			lock (this)
			{
				this.storage.Commit(commitMode);
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004D0F4 File Offset: 0x0004B2F4
		private void CommitAndDehydrateIfPossible(TransactionCommitMode commitMode)
		{
			if (Monitor.TryEnter(this))
			{
				try
				{
					if (this.IsInDelivery)
					{
						this.audit.Drop(Breadcrumb.DehydrationSkippedItemInDelivery);
					}
					else
					{
						this.CommitAndDehydrate(commitMode);
					}
					return;
				}
				finally
				{
					Monitor.Exit(this);
				}
			}
			this.audit.Drop(Breadcrumb.DehydrationSkippedItemLock);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004D154 File Offset: 0x0004B354
		private void CommitAndDehydrate(TransactionCommitMode commitMode)
		{
			lock (this)
			{
				this.Commit(commitMode);
				if (!this.IsRowDeleted)
				{
					this.MinimizeMemory();
					this.audit.Drop(Breadcrumb.DehydrateMinimizedMemory);
				}
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0004D1B0 File Offset: 0x0004B3B0
		private void RemoveFirewalledProperties(IDictionary<string, object> extendedProperties)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, object> keyValuePair in extendedProperties)
			{
				if (keyValuePair.Key.StartsWith("Microsoft.Exchange.Transport.TransportMailItem.Nonforking"))
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (string key in list)
			{
				extendedProperties.Remove(key);
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004D258 File Offset: 0x0004B458
		private void ThrowIfReadOnly()
		{
			if (this.storage.IsReadOnly)
			{
				throw new InvalidOperationException("This operation cannot be performed after mail item has been queued for delivery.");
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0004D272 File Offset: 0x0004B472
		private void ThrowIfInAsyncCommit()
		{
			if (this.IsInAsyncCommit)
			{
				throw new InvalidOperationException("This operation cannot be performed when mail item is in Async Commit");
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004D287 File Offset: 0x0004B487
		private void ThrowIfDeletedOrReadOnly()
		{
			this.ThrowIfDeleted();
			this.ThrowIfReadOnly();
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004D295 File Offset: 0x0004B495
		private void ThrowIfDeleted()
		{
			if (this.IsRowDeleted)
			{
				throw new InvalidOperationException("operations not allowed on a deleted mail item");
			}
		}

		// Token: 0x040009CA RID: 2506
		public const string SmtpInReceiveConnectorNamePrefix = "SMTP:";

		// Token: 0x040009CB RID: 2507
		public const string ReplayReceiveConnectorNamePrefix = "Replay:";

		// Token: 0x040009CC RID: 2508
		public const string AgentReceiveConnectorNamePrefix = "Agent:";

		// Token: 0x040009CD RID: 2509
		public const string DeliveryPriorityStamped = "Microsoft.Exchange.Transport.TransportMailItem.DeliveryPriority";

		// Token: 0x040009CE RID: 2510
		public const string PrioritizationReasonLabel = "Microsoft.Exchange.Transport.TransportMailItem.PrioritizationReason";

		// Token: 0x040009CF RID: 2511
		public const string RiskLevelLabel = "Microsoft.Exchange.Transport.TransportMailItem.RiskLevel";

		// Token: 0x040009D0 RID: 2512
		public const string ExoAccountForestLabel = "Microsoft.Exchange.Transport.TransportMailItem.ExoAccountForest";

		// Token: 0x040009D1 RID: 2513
		public const string ExoTenantContainerLabel = "Microsoft.Exchange.Transport.TransportMailItem.ExoTenantContainer";

		// Token: 0x040009D2 RID: 2514
		public const string ExternalOrganizationIdLabel = "Microsoft.Exchange.Transport.TransportMailItem.ExternalOrganizationId";

		// Token: 0x040009D3 RID: 2515
		public const string SystemProbeIdLabel = "Microsoft.Exchange.Transport.TransportMailItem.SystemProbeId";

		// Token: 0x040009D4 RID: 2516
		public const string InboundProxySequenceNumberLabel = "Microsoft.Exchange.Transport.TransportMailItem.InboundProxySequenceNumber";

		// Token: 0x040009D5 RID: 2517
		public const string AttributedFromAddressLabel = "Microsoft.Exchange.Transport.TransportMailItem.AttributedFromAddress";

		// Token: 0x040009D6 RID: 2518
		public const string ComponentCostLabel = "Microsoft.Exchange.Transport.TransportMailItem.NonforkingComponentCost.";

		// Token: 0x040009D7 RID: 2519
		private const string NonforkingPropertyPrefix = "Microsoft.Exchange.Transport.TransportMailItem.Nonforking";

		// Token: 0x040009D9 RID: 2521
		protected bool loadedInRestart;

		// Token: 0x040009DA RID: 2522
		protected Breadcrumbs audit;

		// Token: 0x040009DB RID: 2523
		private static readonly Guid componentGuid = new Guid("{b53361f0-113c-4cfa-b12f-70781b6f187d}");

		// Token: 0x040009DC RID: 2524
		private static readonly ResubmitterBasedCondition MessageRepositoryResubmitterCondition = new ResubmitterBasedCondition("MessageRepository");

		// Token: 0x040009DD RID: 2525
		private static IMessagingDatabase database;

		// Token: 0x040009DE RID: 2526
		private static ITransportConfiguration configuration;

		// Token: 0x040009DF RID: 2527
		private static ResourceManager resourceManager;

		// Token: 0x040009E0 RID: 2528
		private static ShadowRedundancyComponent shadowRedundancyComponent;

		// Token: 0x040009E1 RID: 2529
		private static ExEventLog eventLogger;

		// Token: 0x040009E2 RID: 2530
		private IMailItemStorage storage;

		// Token: 0x040009E3 RID: 2531
		private QueuedRecipientsByAgeToken queuedRecipientsByAgeToken;

		// Token: 0x040009E4 RID: 2532
		private DateTime deferUntil;

		// Token: 0x040009E5 RID: 2533
		private DeferReason deferReason;

		// Token: 0x040009E6 RID: 2534
		private Dictionary<NextHopSolutionKey, NextHopSolution> nextHopSolutionTable;

		// Token: 0x040009E7 RID: 2535
		private SnapshotWriterState snapshotState;

		// Token: 0x040009E8 RID: 2536
		private bool? pipelineTracingEnabled;

		// Token: 0x040009E9 RID: 2537
		private string pipelineTracingPath;

		// Token: 0x040009EA RID: 2538
		private ADRecipientCache<TransportMiniRecipient> recipientCache;

		// Token: 0x040009EB RID: 2539
		private bool exposeMessage;

		// Token: 0x040009EC RID: 2540
		private bool exposeMessageHeaders;

		// Token: 0x040009ED RID: 2541
		private MultilevelAuthMechanism authMethod;

		// Token: 0x040009EE RID: 2542
		private string messageTrackingSecurityInfo;

		// Token: 0x040009EF RID: 2543
		private List<List<KeyValuePair<string, string>>> messageTrackingAgentInfo;

		// Token: 0x040009F0 RID: 2544
		private bool queuedForDelivery;

		// Token: 0x040009F1 RID: 2545
		private DateTime routingTimeStamp;

		// Token: 0x040009F2 RID: 2546
		private LatencyTracker latencyTracker;

		// Token: 0x040009F3 RID: 2547
		private IActivityScope activityScope;

		// Token: 0x040009F4 RID: 2548
		private PerTenantTransportSettings transportSettings;

		// Token: 0x040009F5 RID: 2549
		private DsnParameters dsnParameters;

		// Token: 0x040009F6 RID: 2550
		private bool routeForHighAvailability;

		// Token: 0x040009F7 RID: 2551
		private AccessToken accessToken;

		// Token: 0x040009F8 RID: 2552
		private DateTimeOffset lockExpirationTime;

		// Token: 0x040009F9 RID: 2553
		private ThrottlingContext throttlingContext;

		// Token: 0x040009FA RID: 2554
		private WaitCondition waitCondition;

		// Token: 0x040009FB RID: 2555
		private string lockReason;

		// Token: 0x040009FC RID: 2556
		private List<string> lockReasonHistory;

		// Token: 0x040009FD RID: 2557
		private ForkCount transportRulesForkCount;

		// Token: 0x040009FE RID: 2558
		private readonly QueueQuotaTrackingBits queueQuotaTrackingBits;

		// Token: 0x040009FF RID: 2559
		private DateTime latencyStartTime;
	}
}
