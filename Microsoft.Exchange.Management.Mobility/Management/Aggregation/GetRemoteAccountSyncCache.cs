using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200001F RID: 31
	[Cmdlet("Get", "RemoteAccountSyncCache", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class GetRemoteAccountSyncCache : GetTenantADObjectWithIdentityTaskBase<CacheIdParameter, SubscriptionsCache>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00007396 File Offset: 0x00005596
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000073AD File Offset: 0x000055AD
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override CacheIdParameter Identity
		{
			get
			{
				return (CacheIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000073C0 File Offset: 0x000055C0
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000073F0 File Offset: 0x000055F0
		[Parameter(Mandatory = false)]
		public bool ValidateCache
		{
			get
			{
				if (!base.Fields.Contains("ValidateCache"))
				{
					this.ValidateCache = true;
				}
				return (bool)base.Fields["ValidateCache"];
			}
			set
			{
				base.Fields["ValidateCache"] = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007408 File Offset: 0x00005608
		protected override IConfigDataProvider CreateSession()
		{
			ADSessionSettings sessionSettings = base.SessionSettings;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.IgnoreInvalid, sessionSettings, 88, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\GetRemoteAccountSyncCache.cs");
			string idStringValue = this.Identity.ToString();
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Identity.MailboxId, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(idStringValue)), new LocalizedString?(Strings.ErrorUserNotUnique(idStringValue)));
			ADSessionSettings adSettings = ADSessionSettings.RescopeToOrganization(base.SessionSettings, aduser.OrganizationId, true);
			SubscriptionCacheAction cacheAction = SubscriptionCacheAction.None;
			if (this.ValidateCache)
			{
				cacheAction = SubscriptionCacheAction.Validate;
			}
			ExchangePrincipal userPrincipal = null;
			try
			{
				userPrincipal = ExchangePrincipal.FromLegacyDN(adSettings, aduser.LegacyExchangeDN, RemotingOptions.AllowCrossSite);
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, this.Identity.MailboxId);
			}
			return new CacheDataProvider(cacheAction, userPrincipal);
		}
	}
}
