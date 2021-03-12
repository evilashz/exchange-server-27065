using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000185 RID: 389
	internal class MailRecipientWrapper : EnvelopeRecipient, IMailRecipientWrapperFacade
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x00045191 File Offset: 0x00043391
		public MailRecipientWrapper(MailRecipient mailRecipient, IReadOnlyMailItem mailItem)
		{
			if (mailRecipient == null)
			{
				throw new ArgumentNullException("mailRecipient");
			}
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.mailRecipient = mailRecipient;
			this.mailItem = mailItem;
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x000451C3 File Offset: 0x000433C3
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x000451D6 File Offset: 0x000433D6
		public override RoutingAddress Address
		{
			get
			{
				this.DisposeValidation();
				return this.mailRecipient.Email;
			}
			set
			{
				this.DisposeValidation();
				this.mailRecipient.Email = value;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x000451EA File Offset: 0x000433EA
		[Obsolete("Use ResolvedMessageEventSource.GetRoutingOverride() instead")]
		public override RoutingDomain RoutingOverride
		{
			get
			{
				this.DisposeValidation();
				if (this.mailRecipient.RoutingOverride == null)
				{
					return RoutingDomain.Empty;
				}
				return this.mailRecipient.RoutingOverride.RoutingDomain;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00045215 File Offset: 0x00043415
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00045228 File Offset: 0x00043428
		public override string OriginalRecipient
		{
			get
			{
				this.DisposeValidation();
				return this.mailRecipient.ORcpt;
			}
			set
			{
				this.DisposeValidation();
				this.mailRecipient.ORcpt = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0004523C File Offset: 0x0004343C
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00045254 File Offset: 0x00043454
		public override DsnTypeRequested RequestedReports
		{
			get
			{
				this.DisposeValidation();
				return EnumConverter.InternalToPublic(this.mailRecipient.DsnRequested);
			}
			set
			{
				this.DisposeValidation();
				this.mailRecipient.DsnRequested = EnumConverter.PublicToInternal(value);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0004526D File Offset: 0x0004346D
		public override IDictionary<string, object> Properties
		{
			get
			{
				this.DisposeValidation();
				return this.mailRecipient.ExtendedPropertyDictionary;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00045280 File Offset: 0x00043480
		public override DeliveryMethod OutboundDeliveryMethod
		{
			get
			{
				this.DisposeValidation();
				switch (this.mailRecipient.NextHop.NextHopType.DeliveryType)
				{
				case DeliveryType.DnsConnectorDelivery:
				case DeliveryType.SmartHostConnectorDelivery:
				case DeliveryType.SmtpRelayToRemoteAdSite:
				case DeliveryType.SmtpRelayToTiRg:
				case DeliveryType.SmtpRelayWithinAdSite:
				case DeliveryType.SmtpRelayWithinAdSiteToEdge:
				case DeliveryType.SmtpRelayToDag:
				case DeliveryType.SmtpRelayToMailboxDeliveryGroup:
				case DeliveryType.SmtpRelayToConnectorSourceServers:
				case DeliveryType.SmtpRelayToServers:
					return DeliveryMethod.Smtp;
				case DeliveryType.MapiDelivery:
				case DeliveryType.SmtpDeliveryToMailbox:
					return DeliveryMethod.Mailbox;
				case DeliveryType.NonSmtpGatewayDelivery:
					return DeliveryMethod.File;
				case DeliveryType.DeliveryAgent:
					return DeliveryMethod.DeliveryAgent;
				}
				return DeliveryMethod.Unknown;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00045308 File Offset: 0x00043508
		public override RecipientCategory RecipientCategory
		{
			get
			{
				this.DisposeValidation();
				switch (this.mailItem.Directionality)
				{
				case MailDirectionality.Originating:
				{
					PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
					if (!Components.Configuration.TryGetAcceptedDomainTable(this.mailItem.ADRecipientCache.OrganizationId, out perTenantAcceptedDomainTable))
					{
						return RecipientCategory.Unknown;
					}
					if (perTenantAcceptedDomainTable.AcceptedDomainTable.GetDomainEntry(SmtpDomain.GetDomainPart(this.Address)) != null)
					{
						return RecipientCategory.InSameOrganization;
					}
					return RecipientCategory.InDifferentOrganization;
				}
				case MailDirectionality.Incoming:
					return RecipientCategory.Incoming;
				}
				return RecipientCategory.Unknown;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0004537A File Offset: 0x0004357A
		IMailRecipientFacade IMailRecipientWrapperFacade.MailRecipient
		{
			get
			{
				return this.mailRecipient;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00045382 File Offset: 0x00043582
		internal MailRecipient MailRecipient
		{
			get
			{
				return this.mailRecipient;
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0004538C File Offset: 0x0004358C
		[Obsolete("Use ResolvedMessageEventSource.SetRoutingOverride() instead")]
		public override void SetRoutingOverride(RoutingDomain routingDomain)
		{
			this.DisposeValidation();
			RoutingOverride routingOverride = (routingDomain == RoutingDomain.Empty) ? null : new RoutingOverride(routingDomain, DeliveryQueueDomain.UseOverrideDomain);
			this.mailRecipient.SetRoutingOverride(routingOverride, null, null);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000453C8 File Offset: 0x000435C8
		internal override bool IsPublicFolderRecipient()
		{
			if (this.mailItem.ADRecipientCache == null)
			{
				return false;
			}
			SmtpProxyAddress proxyAddress = new SmtpProxyAddress(this.Address.ToString(), true);
			Result<TransportMiniRecipient> result = this.mailItem.ADRecipientCache.FindAndCacheRecipient(proxyAddress);
			if (result.Data != null)
			{
				Microsoft.Exchange.Data.Directory.Recipient.RecipientType recipientType = result.Data.RecipientType;
				if (Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicFolder == recipientType || Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicDatabase == recipientType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00045434 File Offset: 0x00043634
		internal void DisposeValidation()
		{
			if (!this.mailRecipient.IsActive || this.mailRecipient.IsProcessed)
			{
				throw new ObjectDisposedException(Strings.EnvelopRecipientDisposed);
			}
		}

		// Token: 0x0400091D RID: 2333
		private MailRecipient mailRecipient;

		// Token: 0x0400091E RID: 2334
		private IReadOnlyMailItem mailItem;
	}
}
