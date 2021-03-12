using System;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Management.Transport;
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.CompliancePolicy;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000C0 RID: 192
	internal class SpPolicyCenterSite
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001D203 File Offset: 0x0001B403
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001D20B File Offset: 0x0001B40B
		public Uri SpSiteUrl { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001D214 File Offset: 0x0001B414
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0001D21C File Offset: 0x0001B41C
		public Uri SpAdminSiteUrl { get; private set; }

		// Token: 0x060006EE RID: 1774 RVA: 0x0001D225 File Offset: 0x0001B425
		public SpPolicyCenterSite(Uri spSiteUrl, Uri spAdminSiteUrl, ICredentials credentials)
		{
			ArgumentValidator.ThrowIfNull("spSiteUrl", spSiteUrl);
			ArgumentValidator.ThrowIfNull("spAdminSiteUrl", spAdminSiteUrl);
			ArgumentValidator.ThrowIfNull("credentials", credentials);
			this.SpSiteUrl = spSiteUrl;
			this.SpAdminSiteUrl = spAdminSiteUrl;
			this.credentials = credentials;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		public Uri GetPolicyCenterSite(bool throwException = true)
		{
			if (this.spPolicyCenterSiteUrl == null)
			{
				Utils.WrapSharePointCsomCall(this.SpSiteUrl, this.credentials, delegate(ClientContext context)
				{
					SPPolicyStoreProxy sppolicyStoreProxy = new SPPolicyStoreProxy(context, context.Site.RootWeb);
					context.Load<SPPolicyStoreProxy>(sppolicyStoreProxy, new Expression<Func<SPPolicyStoreProxy, object>>[0]);
					context.ExecuteQuery();
					Uri uri;
					if (Uri.TryCreate(sppolicyStoreProxy.PolicyStoreUrl, UriKind.Absolute, out uri))
					{
						this.spPolicyCenterSiteUrl = uri;
						return;
					}
					if (throwException)
					{
						throw new SpCsomCallException(Strings.ErrorInvalidPolicyCenterSiteUrl(sppolicyStoreProxy.PolicyStoreUrl));
					}
				});
			}
			return this.spPolicyCenterSiteUrl;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001D390 File Offset: 0x0001B590
		public void NotifyUnifiedPolicySync(string notificationId, string syncSvcUrl, string[] changeInfos, bool syncNow, bool fullSyncForTenant)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("notificationId", notificationId);
			ArgumentValidator.ThrowIfNullOrEmpty("syncSvcUrl", syncSvcUrl);
			ArgumentValidator.ThrowIfCollectionNullOrEmpty<string>("changeInfos", changeInfos);
			Utils.WrapSharePointCsomCall(this.GetPolicyCenterSite(true), this.credentials, delegate(ClientContext context)
			{
				SPPolicyStore sppolicyStore = new SPPolicyStore(context, context.Site.RootWeb);
				context.Load<SPPolicyStore>(sppolicyStore, new Expression<Func<SPPolicyStore, object>>[0]);
				sppolicyStore.NotifyUnifiedPolicySync(notificationId, syncSvcUrl, changeInfos, syncNow, fullSyncForTenant);
				context.ExecuteQuery();
			});
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001D418 File Offset: 0x0001B618
		public Uri GeneratePolicyCenterSiteUri(int? salt)
		{
			string text = "/sites/CompliancePolicyCenter";
			if (salt != null)
			{
				text += salt.ToString();
			}
			return new Uri(this.SpSiteUrl, text);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		public bool IsAnExistingSite(Uri siteUrl, out ServerException exception)
		{
			ArgumentValidator.ThrowIfNull("siteUrl", siteUrl);
			bool result = false;
			ServerException caughtException = null;
			Utils.WrapSharePointCsomCall(this.SpAdminSiteUrl, this.credentials, delegate(ClientContext context)
			{
				try
				{
					Tenant tenant = new Tenant(context);
					Site siteByUrl = tenant.GetSiteByUrl(siteUrl.AbsoluteUri);
					context.Load<Site>(siteByUrl, new Expression<Func<Site, object>>[0]);
					context.ExecuteQuery();
					result = true;
				}
				catch (ServerException caughtException)
				{
					caughtException = caughtException;
				}
			});
			exception = caughtException;
			return result;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001D580 File Offset: 0x0001B780
		public bool IsADeletedSite(Uri siteUrl, out ServerException exception)
		{
			ArgumentValidator.ThrowIfNull("siteUrl", siteUrl);
			bool result = false;
			ServerException caughtException = null;
			Utils.WrapSharePointCsomCall(this.SpAdminSiteUrl, this.credentials, delegate(ClientContext context)
			{
				try
				{
					Tenant tenant = new Tenant(context);
					DeletedSiteProperties deletedSitePropertiesByUrl = tenant.GetDeletedSitePropertiesByUrl(siteUrl.AbsoluteUri);
					context.Load<DeletedSiteProperties>(deletedSitePropertiesByUrl, new Expression<Func<DeletedSiteProperties, object>>[0]);
					context.ExecuteQuery();
					result = true;
				}
				catch (ServerException caughtException)
				{
					caughtException = caughtException;
				}
			});
			exception = caughtException;
			return result;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public void CreatePolicyCenterSite(Uri policyCenterSiteUrl, string siteOwner, long timeoutInMilliSeconds)
		{
			ArgumentValidator.ThrowIfNull("policyCenterSiteUrl", policyCenterSiteUrl);
			ArgumentValidator.ThrowIfNullOrEmpty("siteOwner", siteOwner);
			Utils.WrapSharePointCsomCall(this.SpAdminSiteUrl, this.credentials, delegate(ClientContext context)
			{
				SiteCreationProperties siteCreationProperties = new SiteCreationProperties
				{
					Url = policyCenterSiteUrl.AbsoluteUri,
					Owner = siteOwner,
					Template = "POLICYCTR#0",
					Title = "Compliance Policy Center"
				};
				Tenant tenant = new Tenant(context);
				SpoOperation spoOperation = tenant.CreateSite(siteCreationProperties);
				context.Load<SpoOperation>(spoOperation, new Expression<Func<SpoOperation, object>>[0]);
				context.ExecuteQuery();
				long num = timeoutInMilliSeconds;
				while (!spoOperation.IsComplete)
				{
					if (num <= 0L || spoOperation.HasTimedout)
					{
						throw new ErrorCreateSiteTimeOutException(policyCenterSiteUrl.AbsoluteUri);
					}
					int num2 = Math.Min(Math.Max(5000, spoOperation.PollingInterval), (int)num);
					num -= (long)num2;
					Thread.Sleep(num2);
					context.Load<SpoOperation>(spoOperation, new Expression<Func<SpoOperation, object>>[0]);
					context.ExecuteQuery();
				}
			});
		}

		// Token: 0x04000293 RID: 659
		private const string PolicyCenterSiteRelativeUrl = "/sites/CompliancePolicyCenter";

		// Token: 0x04000294 RID: 660
		private const string PolicyCenterTemplate = "POLICYCTR#0";

		// Token: 0x04000295 RID: 661
		private const string PolicyCenterTitle = "Compliance Policy Center";

		// Token: 0x04000296 RID: 662
		private ICredentials credentials;

		// Token: 0x04000297 RID: 663
		private Uri spPolicyCenterSiteUrl;
	}
}
