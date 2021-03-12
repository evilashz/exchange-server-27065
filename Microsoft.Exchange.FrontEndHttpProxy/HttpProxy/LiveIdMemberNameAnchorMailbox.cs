using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Routing;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000E RID: 14
	internal class LiveIdMemberNameAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003C34 File Offset: 0x00001E34
		public LiveIdMemberNameAnchorMailbox(string liveIdMemberName, string organizationContext, IRequestContext requestContext) : base(AnchorSource.LiveIdMemberName, liveIdMemberName, requestContext)
		{
			if (string.IsNullOrEmpty(liveIdMemberName))
			{
				throw new ArgumentNullException("liveIdMemberName");
			}
			this.OrganizationContext = organizationContext;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003C5A File Offset: 0x00001E5A
		public string LiveIdMemberName
		{
			get
			{
				return (string)base.SourceObject;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003C67 File Offset: 0x00001E67
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003C6F File Offset: 0x00001E6F
		public string OrganizationContext { get; private set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00003CA4 File Offset: 0x00001EA4
		protected override ADRawEntry LoadADRawEntry()
		{
			string text = this.OrganizationContext;
			if (string.IsNullOrEmpty(text))
			{
				SmtpAddress smtpAddress = new SmtpAddress(this.LiveIdMemberName);
				text = smtpAddress.Domain;
			}
			bool flag = PuidAnchorMailbox.AllowMissingDirectoryObject.Value || AnchorMailbox.AllowMissingTenant.Value;
			ITenantRecipientSession session = (ITenantRecipientSession)DirectoryHelper.GetRecipientSessionFromDomain(base.RequestContext.LatencyTracker, text, flag);
			if (flag && session == null)
			{
				return null;
			}
			ExTraceGlobals.VerboseTracer.Information<string, string, string>((long)this.GetHashCode(), "Searching GC {0} for LiveIdMemberName {1}, OrganizationContext {2}", session.DomainController ?? "<null>", this.LiveIdMemberName, this.OrganizationContext ?? "<null>");
			ADRawEntry adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => session.FindByLiveIdMemberName(this.LiveIdMemberName, this.PropertySet));
			if (adrawEntry != null && base.RequestContext.HttpContext.User.Identity.Name.Equals(this.LiveIdMemberName, StringComparison.OrdinalIgnoreCase))
			{
				base.RequestContext.HttpContext.Items[Constants.CallerADRawEntryKeyName] = adrawEntry;
			}
			return adrawEntry;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DCF File Offset: 0x00001FCF
		protected override IRoutingKey GetRoutingKey()
		{
			return new LiveIdMemberNameRoutingKey(new SmtpAddress(this.LiveIdMemberName), this.OrganizationContext);
		}
	}
}
