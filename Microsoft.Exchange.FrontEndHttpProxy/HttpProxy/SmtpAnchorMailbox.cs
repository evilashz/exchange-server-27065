using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000017 RID: 23
	internal class SmtpAnchorMailbox : ArchiveSupportedAnchorMailbox
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00004C41 File Offset: 0x00002E41
		public SmtpAnchorMailbox(string smtp, IRequestContext requestContext) : base(AnchorSource.Smtp, smtp, requestContext)
		{
			this.FailOnDomainNotFound = true;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004C53 File Offset: 0x00002E53
		public string Smtp
		{
			get
			{
				return (string)base.SourceObject;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004C60 File Offset: 0x00002E60
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004C68 File Offset: 0x00002E68
		public bool FailOnDomainNotFound { get; set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00004C74 File Offset: 0x00002E74
		public override string GetOrganizationNameForLogging()
		{
			string text = base.GetOrganizationNameForLogging();
			if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.Smtp) && SmtpAddress.IsValidSmtpAddress(this.Smtp))
			{
				text = SmtpAddress.Parse(this.Smtp).Domain;
			}
			return text;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004D0C File Offset: 0x00002F0C
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession session = DirectoryHelper.GetRecipientSessionFromSmtpOrLiveId(base.RequestContext.LatencyTracker, this.Smtp, !this.FailOnDomainNotFound);
			ADRawEntry ret = null;
			if (session != null)
			{
				ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, delegate
				{
					bool flag = FaultInjection.TraceTest<bool>(FaultInjection.LIDs.ShouldFailSmtpAnchorMailboxADLookup);
					if (flag)
					{
						return null;
					}
					return session.FindByProxyAddress(new SmtpProxyAddress(this.Smtp, true), this.PropertySet);
				});
			}
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004D80 File Offset: 0x00002F80
		protected override IRoutingKey GetRoutingKey()
		{
			return new SmtpRoutingKey(new SmtpAddress(this.Smtp));
		}
	}
}
