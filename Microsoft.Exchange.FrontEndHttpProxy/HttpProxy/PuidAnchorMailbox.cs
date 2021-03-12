using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000013 RID: 19
	internal class PuidAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00004384 File Offset: 0x00002584
		public PuidAnchorMailbox(string puid, string liveIdMemberName, IRequestContext requestContext) : base(AnchorSource.Puid, puid, requestContext)
		{
			if (string.IsNullOrEmpty(liveIdMemberName))
			{
				throw new ArgumentNullException("liveIdMemberName");
			}
			this.LiveIdMemberName = liveIdMemberName;
			base.NotFoundExceptionCreator = delegate()
			{
				string message = string.Format("Cannot find user {0} with puid {1}.", liveIdMemberName, puid);
				return new HttpProxyException(HttpStatusCode.Forbidden, HttpProxySubErrorCode.UserNotFound, message);
			};
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000043F1 File Offset: 0x000025F1
		public string Puid
		{
			get
			{
				return (string)base.SourceObject;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000043FE File Offset: 0x000025FE
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004406 File Offset: 0x00002606
		public string LiveIdMemberName { get; private set; }

		// Token: 0x0600008C RID: 140 RVA: 0x00004410 File Offset: 0x00002610
		public override BackEndCookieEntryBase BuildCookieEntryForTarget(BackEndServer routingTarget, bool proxyToDownLevel, bool useResourceForest)
		{
			if (routingTarget == null)
			{
				throw new ArgumentNullException("routingTarget");
			}
			if (!proxyToDownLevel && !base.UseServerCookie && PuidAnchorMailbox.AllowMissingDirectoryObject.Value && this.isMissingDirectoryUserObject)
			{
				string resourceForest;
				Utilities.TryExtractForestFqdnFromServerFqdn(routingTarget.Fqdn, out resourceForest);
				return this.BuildCookieEntryForTargetFromBackEndHints(resourceForest, useResourceForest);
			}
			return base.BuildCookieEntryForTarget(routingTarget, proxyToDownLevel, useResourceForest);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000044CC File Offset: 0x000026CC
		protected override ADRawEntry LoadADRawEntry()
		{
			ADRawEntry adrawEntry = null;
			bool ignoreCannotResolveTenantNameException = PuidAnchorMailbox.AllowMissingDirectoryObject.Value || AnchorMailbox.AllowMissingTenant.Value;
			ITenantRecipientSession tenantRecipientSessionFromSmtpOrLiveId = DirectoryHelper.GetTenantRecipientSessionFromSmtpOrLiveId(base.RequestContext.LatencyTracker, this.LiveIdMemberName, ignoreCannotResolveTenantNameException);
			if (tenantRecipientSessionFromSmtpOrLiveId != null)
			{
				adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => tenantRecipientSessionFromSmtpOrLiveId.FindUniqueEntryByNetID(this.Puid, null, this.PropertySet));
			}
			if (!PuidAnchorMailbox.AllowMissingDirectoryObject.Value)
			{
				base.CheckForNullAndThrowIfApplicable<ADRawEntry>(adrawEntry);
			}
			else if (adrawEntry == null)
			{
				if (tenantRecipientSessionFromSmtpOrLiveId != null)
				{
					ExTraceGlobals.VerboseTracer.TraceWarning<PuidAnchorMailbox, string, ADScope>((long)this.GetHashCode(), "[PuidAnchorMailbox::LoadADRawEntry]: Anchor mailbox {0} not found for a user with MemberName {1} in {2}.", this, this.LiveIdMemberName, (tenantRecipientSessionFromSmtpOrLiveId.SessionSettings != null) ? tenantRecipientSessionFromSmtpOrLiveId.SessionSettings.ConfigReadScope : null);
				}
				ITenantRecipientSession tenantRecipientSessionByMSAUserNetID = DirectoryHelper.GetTenantRecipientSessionByMSAUserNetID(base.RequestContext.LatencyTracker, this.Puid, PuidAnchorMailbox.AllowMissingDirectoryObject.Value, PuidAnchorMailbox.AllowMissingDirectoryObject.Value) as ITenantRecipientSession;
				if (tenantRecipientSessionByMSAUserNetID != null)
				{
					adrawEntry = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => tenantRecipientSessionByMSAUserNetID.FindUniqueEntryByNetID(this.Puid, null, this.PropertySet));
					base.CheckForNullAndThrowIfApplicable<ADRawEntry>(adrawEntry);
				}
				else
				{
					this.isMissingDirectoryUserObject = true;
					base.RequestContext.HttpContext.Items[Constants.MissingDirectoryUserObjectKey] = PuidAnchorMailbox.BooleanTrue;
				}
			}
			if (adrawEntry != null && base.RequestContext.HttpContext.User.Identity.Name.Equals(this.LiveIdMemberName, StringComparison.OrdinalIgnoreCase))
			{
				base.RequestContext.HttpContext.Items[Constants.CallerADRawEntryKeyName] = adrawEntry;
			}
			return adrawEntry;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000046A1 File Offset: 0x000028A1
		protected override bool ShouldAddEntryToAnchorMailboxCache(AnchorMailboxCacheEntry cacheEntry)
		{
			return !PuidAnchorMailbox.AllowMissingDirectoryObject.Value || (cacheEntry != null && cacheEntry.Database != null);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000046C2 File Offset: 0x000028C2
		protected override void OnPopulateCacheEntry(AnchorMailboxCacheEntry cacheEntry)
		{
			if (PuidAnchorMailbox.AllowMissingDirectoryObject.Value && cacheEntry != null && !string.IsNullOrEmpty(cacheEntry.DomainName))
			{
				base.RequestContext.HttpContext.Items[Constants.OrganizationContextKey] = cacheEntry.DomainName;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004700 File Offset: 0x00002900
		private BackEndCookieEntryBase BuildCookieEntryForTargetFromBackEndHints(string resourceForest, bool useResourceForest)
		{
			if (base.RequestContext.HttpContext != null)
			{
				HttpResponse response = base.RequestContext.HttpContext.Response;
				if (response != null)
				{
					string text = response.Headers[WellKnownHeader.BackEndOriginatingMailboxDatabase];
					if (text != null)
					{
						Guid empty = Guid.Empty;
						string text2 = null;
						if (SmtpAddress.IsValidSmtpAddress(text))
						{
							SmtpAddress smtpAddress = SmtpAddress.Parse(text);
							Guid.TryParse(smtpAddress.Local, out empty);
							text2 = smtpAddress.Domain;
						}
						response.Headers.Remove(WellKnownHeader.BackEndOriginatingMailboxDatabase);
						if (empty != Guid.Empty && text2 != null)
						{
							if (useResourceForest)
							{
								return new BackEndDatabaseResourceForestCookieEntry(empty, text2, resourceForest);
							}
							return new BackEndDatabaseCookieEntry(empty, text2);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000031 RID: 49
		internal static readonly BoolAppSettingsEntry AllowMissingDirectoryObject = new BoolAppSettingsEntry("PuidAnchorMailbox.AllowMissingDirectoryObject", false, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000032 RID: 50
		private static readonly string BooleanTrue = true.ToString();

		// Token: 0x04000033 RID: 51
		private bool isMissingDirectoryUserObject;
	}
}
