using System;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000F RID: 15
	internal class MailboxGuidAnchorMailbox : ArchiveSupportedAnchorMailbox
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003E50 File Offset: 0x00002050
		public MailboxGuidAnchorMailbox(Guid mailboxGuid, string domain, IRequestContext requestContext) : base(AnchorSource.MailboxGuid, mailboxGuid, requestContext)
		{
			this.Domain = domain;
			base.NotFoundExceptionCreator = delegate()
			{
				base.UpdateNegativeCache(new NegativeAnchorMailboxCacheEntry
				{
					ErrorCode = HttpStatusCode.NotFound,
					SubErrorCode = HttpProxySubErrorCode.MailboxGuidWithDomainNotFound,
					SourceObject = this.ToCacheKey()
				});
				string message = string.Format("Cannot find mailbox {0} with domain {1}.", this.MailboxGuid, this.Domain);
				return new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.MailboxGuidWithDomainNotFound, message);
			};
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003E8B File Offset: 0x0000208B
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)base.SourceObject;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003E98 File Offset: 0x00002098
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003EA0 File Offset: 0x000020A0
		public string Domain { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003EA9 File Offset: 0x000020A9
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003EB1 File Offset: 0x000020B1
		public string FallbackSmtp { get; set; }

		// Token: 0x06000075 RID: 117 RVA: 0x00003EBC File Offset: 0x000020BC
		public override string GetOrganizationNameForLogging()
		{
			string organizationNameForLogging = base.GetOrganizationNameForLogging();
			if (string.IsNullOrEmpty(organizationNameForLogging) && !string.IsNullOrEmpty(this.Domain))
			{
				return this.Domain;
			}
			return organizationNameForLogging;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003F38 File Offset: 0x00002138
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession session = null;
			if (!string.IsNullOrEmpty(this.Domain) && SmtpAddress.IsValidDomain(this.Domain))
			{
				try
				{
					session = DirectoryHelper.GetRecipientSessionFromDomain(base.RequestContext.LatencyTracker, this.Domain, false);
					goto IL_95;
				}
				catch (CannotResolveTenantNameException)
				{
					base.UpdateNegativeCache(new NegativeAnchorMailboxCacheEntry
					{
						ErrorCode = HttpStatusCode.NotFound,
						SubErrorCode = HttpProxySubErrorCode.DomainNotFound,
						SourceObject = this.ToCacheKey()
					});
					throw;
				}
			}
			session = DirectoryHelper.GetRootOrgRecipientSession();
			IL_95:
			ADRawEntry adrawEntry;
			if (base.IsArchive != null)
			{
				adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => session.FindByExchangeGuidIncludingAlternate(this.MailboxGuid, this.PropertySet));
			}
			else
			{
				adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => session.FindByExchangeGuidIncludingAlternate(this.MailboxGuid, MailboxGuidAnchorMailbox.ADRawEntryWithArchivePropertySet));
				if (adrawEntry != null && ((Guid)adrawEntry[ADUserSchema.ArchiveGuid]).Equals(this.MailboxGuid))
				{
					base.IsArchive = new bool?(true);
				}
			}
			if (adrawEntry == null && !string.IsNullOrEmpty(this.FallbackSmtp) && SmtpAddress.IsValidSmtpAddress(this.FallbackSmtp))
			{
				adrawEntry = new SmtpAnchorMailbox(this.FallbackSmtp, base.RequestContext)
				{
					IsArchive = base.IsArchive,
					NotFoundExceptionCreator = null
				}.GetADRawEntry();
			}
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(adrawEntry);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000040CC File Offset: 0x000022CC
		protected override string ToCacheKey()
		{
			return this.MailboxGuid.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000040ED File Offset: 0x000022ED
		protected override IRoutingKey GetRoutingKey()
		{
			return new MailboxGuidRoutingKey(this.MailboxGuid, this.Domain);
		}

		// Token: 0x0400002B RID: 43
		protected static readonly ADPropertyDefinition[] ADRawEntryWithArchivePropertySet = new ADPropertyDefinition[]
		{
			ADObjectSchema.OrganizationId,
			ADUserSchema.ArchiveGuid,
			ADMailboxRecipientSchema.Database,
			ADUserSchema.ArchiveDatabase,
			ADRecipientSchema.PrimarySmtpAddress
		};
	}
}
