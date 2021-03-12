using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000F6 RID: 246
	internal class MailRecipient : IMailRecipientFacade
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x000235F9 File Offset: 0x000217F9
		private MailRecipient(TransportMailItem mailItem, IMailRecipientStorage storage)
		{
			this.mailItem = mailItem;
			this.storage = storage;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00023625 File Offset: 0x00021825
		public long RecipientId
		{
			get
			{
				return this.storage.RecipId;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00023632 File Offset: 0x00021832
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0002363A File Offset: 0x0002183A
		public bool ExposeRoutingDomain
		{
			get
			{
				return this.exposeRoutingDomain;
			}
			set
			{
				this.exposeRoutingDomain = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00023643 File Offset: 0x00021843
		public bool IsProcessed
		{
			get
			{
				return this.Status == Status.Handled || this.Status == Status.Complete;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00023659 File Offset: 0x00021859
		public bool IsDelayDsnNeeded
		{
			get
			{
				return (this.Status == Status.Ready || this.Status == Status.Retry || this.Status == Status.Locked) && ((this.DsnRequested & DsnRequestedFlags.Delay) == DsnRequestedFlags.Delay || this.DsnRequested == DsnRequestedFlags.Default) && (this.DsnCompleted & DsnFlags.Delay) == DsnFlags.None;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00023695 File Offset: 0x00021895
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x000236A4 File Offset: 0x000218A4
		public RoutingAddress Email
		{
			get
			{
				return new RoutingAddress(this.PrivateEmail);
			}
			set
			{
				if (value == RoutingAddress.Empty)
				{
					throw new ArgumentException("Email property cannot be set to an empty RoutingAddress value.");
				}
				if (!value.IsValid)
				{
					throw new FormatException("Can not set Email property to an invalid RoutingAddress value.");
				}
				this.PrivateEmail = value.ToString();
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x000236F0 File Offset: 0x000218F0
		public IExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this.storage.ExtendedProperties;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000236FD File Offset: 0x000218FD
		[Obsolete("Use ExtendedProperties instead.")]
		public IDictionary<string, object> ExtendedPropertyDictionary
		{
			get
			{
				this.ThrowIfDeleted();
				return this.ExtendedProperties as IDictionary<string, object>;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00023710 File Offset: 0x00021910
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0002371D File Offset: 0x0002191D
		public string ORcpt
		{
			get
			{
				return this.storage.ORcpt;
			}
			set
			{
				this.storage.ORcpt = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0002372B File Offset: 0x0002192B
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x00023738 File Offset: 0x00021938
		public AdminActionStatus AdminActionStatus
		{
			get
			{
				return this.storage.AdminActionStatus;
			}
			set
			{
				this.storage.AdminActionStatus = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00023746 File Offset: 0x00021946
		public bool IsActive
		{
			get
			{
				return this.storage.IsActive;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00023753 File Offset: 0x00021953
		public bool IsRowDeleted
		{
			get
			{
				return this.storage.IsDeleted;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00023760 File Offset: 0x00021960
		public bool IsInSafetyNet
		{
			get
			{
				return this.storage.IsInSafetyNet;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0002376D File Offset: 0x0002196D
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x0002377A File Offset: 0x0002197A
		public DsnRequestedFlags DsnRequested
		{
			get
			{
				return this.storage.DsnRequested;
			}
			set
			{
				this.storage.DsnRequested = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00023788 File Offset: 0x00021988
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00023795 File Offset: 0x00021995
		public DsnFlags DsnNeeded
		{
			get
			{
				return this.storage.DsnNeeded;
			}
			set
			{
				this.storage.DsnNeeded = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x000237A3 File Offset: 0x000219A3
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x000237B0 File Offset: 0x000219B0
		public DsnFlags DsnCompleted
		{
			get
			{
				return this.storage.DsnCompleted;
			}
			set
			{
				this.storage.DsnCompleted = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x000237BE File Offset: 0x000219BE
		public DsnParameters DsnParameters
		{
			get
			{
				return this.dsnParameters;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x000237C6 File Offset: 0x000219C6
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000237CE File Offset: 0x000219CE
		public string DsnMessageId
		{
			get
			{
				return this.dsnMsgId;
			}
			set
			{
				this.ThrowIfDeleted();
				this.dsnMsgId = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x000237DD File Offset: 0x000219DD
		public bool SmtpDeferLogged
		{
			get
			{
				return this.smtpDeferLogged;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x000237E5 File Offset: 0x000219E5
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x000237F2 File Offset: 0x000219F2
		public Status Status
		{
			get
			{
				return this.storage.Status;
			}
			set
			{
				this.storage.Status = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00023800 File Offset: 0x00021A00
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0002380D File Offset: 0x00021A0D
		public Destination DeliveredDestination
		{
			get
			{
				return this.storage.DeliveredDestination;
			}
			set
			{
				this.storage.DeliveredDestination = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0002381B File Offset: 0x00021A1B
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00023828 File Offset: 0x00021A28
		public string PrimaryServerFqdnGuid
		{
			get
			{
				return this.storage.PrimaryServerFqdnGuid;
			}
			set
			{
				this.storage.PrimaryServerFqdnGuid = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00023836 File Offset: 0x00021A36
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x00023843 File Offset: 0x00021A43
		public DateTime? DeliveryTime
		{
			get
			{
				return this.storage.DeliveryTime;
			}
			set
			{
				this.storage.DeliveryTime = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00023851 File Offset: 0x00021A51
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00023859 File Offset: 0x00021A59
		public SmtpResponse SmtpResponse
		{
			get
			{
				return this.smtpResponse;
			}
			internal set
			{
				this.ThrowIfDeleted();
				this.smtpResponse = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00023868 File Offset: 0x00021A68
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00023875 File Offset: 0x00021A75
		public int RetryCount
		{
			get
			{
				return this.storage.RetryCount;
			}
			internal set
			{
				this.storage.RetryCount = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00023883 File Offset: 0x00021A83
		public AckStatus AckStatus
		{
			get
			{
				return this.ackStatus;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002388B File Offset: 0x00021A8B
		public long MsgRecordId
		{
			get
			{
				return this.storage.MsgId;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00023898 File Offset: 0x00021A98
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x000238A0 File Offset: 0x00021AA0
		public NextHopSolutionKey NextHop
		{
			get
			{
				return this.nextHop;
			}
			set
			{
				this.nextHop = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000238A9 File Offset: 0x00021AA9
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x000238B6 File Offset: 0x00021AB6
		public long MsgId
		{
			get
			{
				return this.storage.MsgId;
			}
			private set
			{
				this.storage.MsgId = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000238C4 File Offset: 0x00021AC4
		public bool PendingDatabaseUpdates
		{
			get
			{
				return this.storage.PendingDatabaseUpdates;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x000238D1 File Offset: 0x00021AD1
		public RoutingOverride RoutingOverride
		{
			get
			{
				this.ThrowIfDeleted();
				return this.routingOverride;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000238DF File Offset: 0x00021ADF
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x000238E7 File Offset: 0x00021AE7
		public string OverrideSource { get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000238F0 File Offset: 0x00021AF0
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x000238FD File Offset: 0x00021AFD
		internal int OutboundIPPool
		{
			get
			{
				return this.storage.OutboundIPPool;
			}
			set
			{
				this.storage.OutboundIPPool = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002390B File Offset: 0x00021B0B
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00023918 File Offset: 0x00021B18
		internal RequiredTlsAuthLevel? TlsAuthLevel
		{
			get
			{
				return this.storage.TlsAuthLevel;
			}
			set
			{
				this.storage.TlsAuthLevel = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00023926 File Offset: 0x00021B26
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0002392E File Offset: 0x00021B2E
		internal UnreachableReason UnreachableReason
		{
			get
			{
				return this.unreachableReason;
			}
			set
			{
				this.unreachableReason = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00023937 File Offset: 0x00021B37
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0002393F File Offset: 0x00021B3F
		internal MailRecipientType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00023948 File Offset: 0x00021B48
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00023950 File Offset: 0x00021B50
		internal string FinalDestination
		{
			get
			{
				return this.finalDestination;
			}
			set
			{
				this.finalDestination = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00023959 File Offset: 0x00021B59
		internal OrganizationId MailItemScopeOrganizationId
		{
			get
			{
				return this.mailItem.OrganizationId;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00023966 File Offset: 0x00021B66
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00023973 File Offset: 0x00021B73
		private string PrivateEmail
		{
			get
			{
				return this.storage.Email;
			}
			set
			{
				this.storage.Email = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00023981 File Offset: 0x00021B81
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x0002398E File Offset: 0x00021B8E
		private int PrivateRetryCount
		{
			get
			{
				return this.storage.RetryCount;
			}
			set
			{
				this.storage.RetryCount = value;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002399C File Offset: 0x00021B9C
		public static MailRecipient NewUnbound(string smtpAddress)
		{
			MailRecipient mailRecipient = new MailRecipient(null, TransportMailItem.Database.NewRecipientStorage(0L));
			mailRecipient.InitializeDefaults();
			mailRecipient.Email = new RoutingAddress(smtpAddress);
			return mailRecipient;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000239D0 File Offset: 0x00021BD0
		public void SetRoutingOverride(RoutingOverride routingOverride, string agentName, string overrideSource = null)
		{
			this.ThrowIfDeleted();
			if (!this.exposeRoutingDomain)
			{
				throw new InvalidOperationException(Strings.InvalidRoutingOverrideEvent);
			}
			if (this.routingOverride == null && routingOverride == null)
			{
				return;
			}
			string text = (routingOverride != null) ? routingOverride.AlternateDeliveryRoutingHostsString : string.Empty;
			if (this.routingOverride != null && routingOverride != null)
			{
				string alternateDeliveryRoutingHostsString = this.RoutingOverride.AlternateDeliveryRoutingHostsString;
				if (this.routingOverride.RoutingDomain.Equals(routingOverride.RoutingDomain) && alternateDeliveryRoutingHostsString.Equals(text, StringComparison.OrdinalIgnoreCase) && this.routingOverride.DeliveryQueueDomain.Equals(routingOverride.DeliveryQueueDomain))
				{
					return;
				}
			}
			this.routingOverride = routingOverride;
			this.OverrideSource = overrideSource;
			RoutingDomain redirectedConnectorDomain = (this.routingOverride == null) ? RoutingDomain.Empty : this.routingOverride.RoutingDomain;
			string redirectedDeliveryDestination = string.Empty;
			if (this.routingOverride != null)
			{
				switch (this.routingOverride.DeliveryQueueDomain)
				{
				case DeliveryQueueDomain.UseOverrideDomain:
					redirectedDeliveryDestination = redirectedConnectorDomain.ToString();
					break;
				case DeliveryQueueDomain.UseRecipientDomain:
					redirectedDeliveryDestination = this.Email.DomainPart;
					break;
				case DeliveryQueueDomain.UseAlternateDeliveryRoutingHosts:
					redirectedDeliveryDestination = text;
					break;
				}
			}
			MsgTrackRedirectInfo msgTrackInfo = new MsgTrackRedirectInfo(agentName, this.Email, redirectedConnectorDomain, redirectedDeliveryDestination, new long?(this.mailItem.MsgId));
			MessageTrackingLog.TrackRedirectToDomain(MessageTrackingSource.AGENT, this.mailItem, msgTrackInfo, this);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00023B28 File Offset: 0x00021D28
		public void SetTlsDomain(string tlsDomain)
		{
			this.ThrowIfDeleted();
			SmtpDomainWithSubdomains smtpDomainWithSubdomains;
			if (!SmtpDomainWithSubdomains.TryParse(tlsDomain, out smtpDomainWithSubdomains))
			{
				throw new ArgumentException("Given TLS Domain is not valid.");
			}
			if (smtpDomainWithSubdomains.IsStar)
			{
				throw new ArgumentException("Given domain cannot be \"*\"");
			}
			this.tlsDomain = tlsDomain;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00023B6A File Offset: 0x00021D6A
		public void ResetTlsDomain()
		{
			this.tlsDomain = null;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00023B73 File Offset: 0x00021D73
		public void ResetRoutingOverride()
		{
			this.routingOverride = null;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00023B7C File Offset: 0x00021D7C
		public void SetSmtpDeferLogged()
		{
			this.smtpDeferLogged = true;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00023B85 File Offset: 0x00021D85
		public string GetTlsDomain()
		{
			this.ThrowIfDeleted();
			return this.tlsDomain;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00023B93 File Offset: 0x00021D93
		public void AddDsnParameters(string key, object value)
		{
			if (this.dsnParameters == null)
			{
				this.dsnParameters = new DsnParameters();
			}
			this.dsnParameters[key] = value;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00023BB8 File Offset: 0x00021DB8
		public void Ack(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.SmtpResponse = smtpResponse;
			switch (ackStatus)
			{
			case AckStatus.Pending:
				this.DsnNeeded = DsnFlags.None;
				this.Status = Status.Ready;
				this.ackStatus = ackStatus;
				return;
			case AckStatus.Success:
			case AckStatus.Expand:
			case AckStatus.Relay:
			case AckStatus.SuccessNoDsn:
				if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub)
				{
					Components.MessageResubmissionComponent.StoreRecipient(this);
				}
				if ((this.DsnRequested & DsnRequestedFlags.Success) == DsnRequestedFlags.Default || ackStatus == AckStatus.SuccessNoDsn)
				{
					this.DsnNeeded = DsnFlags.None;
					this.Status = Status.Complete;
				}
				else
				{
					if (ackStatus == AckStatus.Relay)
					{
						this.DsnNeeded = DsnFlags.Relay;
					}
					else if (ackStatus == AckStatus.Expand)
					{
						this.DsnNeeded = DsnFlags.Expansion;
					}
					else
					{
						this.DsnNeeded = DsnFlags.Delivery;
					}
					this.Status = Status.Handled;
				}
				this.ackStatus = ackStatus;
				return;
			case AckStatus.Retry:
				this.PrivateRetryCount++;
				this.Status = Status.Retry;
				this.ackStatus = ackStatus;
				return;
			case AckStatus.Fail:
				if ((this.DsnRequested & DsnRequestedFlags.Failure) != DsnRequestedFlags.Default || this.DsnRequested == DsnRequestedFlags.Default)
				{
					this.DsnNeeded = DsnFlags.Failure;
					this.Status = Status.Handled;
				}
				else
				{
					this.DsnNeeded = DsnFlags.None;
					if (!this.recipientIsOnPoisonMessage)
					{
						this.Status = Status.Complete;
					}
				}
				this.ackStatus = ackStatus;
				return;
			case AckStatus.Resubmit:
				this.DsnNeeded = DsnFlags.None;
				this.Status = Status.Retry;
				this.ackStatus = ackStatus;
				return;
			case AckStatus.Quarantine:
				this.DsnNeeded = DsnFlags.Quarantine;
				this.Status = Status.Handled;
				this.ackStatus = ackStatus;
				return;
			default:
				throw new ArgumentException("AckStatus can not be set to the specified value");
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00023D10 File Offset: 0x00021F10
		public void MoveTo(TransportMailItem target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.storage = this.storage.MoveTo(target.MsgId);
			this.mailItem.Recipients.RemoveInternal(this);
			this.mailItem = target;
			target.Recipients.Add(this);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00023D68 File Offset: 0x00021F68
		public MailRecipient CopyTo(TransportMailItem target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			MailRecipient mailRecipient = new MailRecipient(target, this.storage.CopyTo(target.MsgId));
			target.Recipients.Add(mailRecipient);
			return mailRecipient;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00023DA8 File Offset: 0x00021FA8
		public void ReleaseFromActive()
		{
			if (this.IsActive)
			{
				this.Status = Status.Complete;
				this.storage.ReleaseFromActive();
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00023DC4 File Offset: 0x00021FC4
		public void MarkToDelete()
		{
			this.Status = Status.Complete;
			this.storage.MarkToDelete();
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00023DD8 File Offset: 0x00021FD8
		public void AddToSafetyNet()
		{
			this.storage.AddToSafetyNet();
			this.PublishAddToSafetyNetEvent();
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00023DEB File Offset: 0x00021FEB
		public void UpdateForPoisonMessage()
		{
			this.recipientIsOnPoisonMessage = true;
			this.DsnRequested = DsnRequestedFlags.Never;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00023DFC File Offset: 0x00021FFC
		public override string ToString()
		{
			return this.Email.ToString();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00023E20 File Offset: 0x00022020
		public Guid? GetMDBGuid()
		{
			MailboxItem mailboxItem = RecipientItem.Create(this) as MailboxItem;
			if (mailboxItem == null)
			{
				return null;
			}
			return new Guid?(mailboxItem.Database.ObjectGuid);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00023E58 File Offset: 0x00022058
		public string GetLastErrorDetails()
		{
			return new LastError(null, null, null, this.SmtpResponse).ToString();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00023E80 File Offset: 0x00022080
		internal static MailRecipient NewMessageRecipient(TransportMailItem mailItem, IMailRecipientStorage storageObject)
		{
			return new MailRecipient(mailItem, storageObject);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00023E8C File Offset: 0x0002208C
		internal static MailRecipient NewMessageRecipient(TransportMailItem mailItem)
		{
			MailRecipient mailRecipient = new MailRecipient(mailItem, TransportMailItem.Database.NewRecipientStorage(mailItem.MsgId));
			mailRecipient.InitializeDefaults();
			mailRecipient.MsgId = mailItem.RecordId;
			mailItem.Recipients.Add(mailRecipient);
			return mailRecipient;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00023ECF File Offset: 0x000220CF
		internal void Attach(TransportMailItem mailItem)
		{
			this.MsgId = mailItem.RecordId;
			this.mailItem = mailItem;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00023EE4 File Offset: 0x000220E4
		internal void Commit(Transaction transaction)
		{
			this.storage.Materialize(transaction);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00023EF2 File Offset: 0x000220F2
		internal void UpdateOwnerId()
		{
			this.MsgId = this.mailItem.RecordId;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00023F05 File Offset: 0x00022105
		internal void MinimizeMemory()
		{
			this.storage.MinimizeMemory();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00023F14 File Offset: 0x00022114
		private void InitializeDefaults()
		{
			this.PrivateEmail = string.Empty;
			this.DsnRequested = DsnRequestedFlags.Default;
			this.DsnNeeded = DsnFlags.None;
			this.DsnCompleted = DsnFlags.None;
			this.Status = Status.Ready;
			this.SmtpResponse = SmtpResponse.Empty;
			this.PrivateRetryCount = 0;
			this.AdminActionStatus = AdminActionStatus.None;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00023F61 File Offset: 0x00022161
		private void ThrowIfDeleted()
		{
			if (this.storage.IsDeleted)
			{
				throw new InvalidOperationException("operations not allowed on a deleted recipient");
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00023F7C File Offset: 0x0002217C
		private void PublishAddToSafetyNetEvent()
		{
			if (this.mailItem.SystemProbeId != Guid.Empty && this.DeliveryTime != null && this.DeliveredDestination != null && this.DeliveredDestination.Type == Destination.DestinationType.Mdb)
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.Transport.Name, Components.MessageResubmissionComponent.GetDiagnosticComponentName(), null, ResultSeverityLevel.Verbose);
				eventNotificationItem.AddCustomProperty("Key", "Operation");
				eventNotificationItem.AddCustomProperty("Value", "SafetyNetAdd");
				eventNotificationItem.AddCustomProperty("DeliveryTime", this.DeliveryTime.Value.Ticks);
				eventNotificationItem.AddCustomProperty("TargetMdb", this.DeliveredDestination.ToGuid());
				eventNotificationItem.StateAttribute1 = this.mailItem.SystemProbeId.ToString();
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x0400043D RID: 1085
		private TransportMailItem mailItem;

		// Token: 0x0400043E RID: 1086
		private NextHopSolutionKey nextHop;

		// Token: 0x0400043F RID: 1087
		private bool exposeRoutingDomain;

		// Token: 0x04000440 RID: 1088
		private RoutingOverride routingOverride;

		// Token: 0x04000441 RID: 1089
		private string tlsDomain;

		// Token: 0x04000442 RID: 1090
		private AckStatus ackStatus;

		// Token: 0x04000443 RID: 1091
		private string dsnMsgId;

		// Token: 0x04000444 RID: 1092
		private UnreachableReason unreachableReason;

		// Token: 0x04000445 RID: 1093
		private MailRecipientType type;

		// Token: 0x04000446 RID: 1094
		private string finalDestination = string.Empty;

		// Token: 0x04000447 RID: 1095
		private SmtpResponse smtpResponse = SmtpResponse.Empty;

		// Token: 0x04000448 RID: 1096
		private DsnParameters dsnParameters;

		// Token: 0x04000449 RID: 1097
		private IMailRecipientStorage storage;

		// Token: 0x0400044A RID: 1098
		private bool recipientIsOnPoisonMessage;

		// Token: 0x0400044B RID: 1099
		private bool smtpDeferLogged;
	}
}
