using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000AC RID: 172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ABDiscoveryManager
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0001701E File Offset: 0x0001521E
		public static IABSessionSettings GetSessionSettings(ExchangePrincipal exchangePrincipal, int? lcid, ConsistencyMode? consistencyMode, SyncLog syncLog, ClientSecurityContext clientSecurityContext)
		{
			return ABDiscoveryManager.GetSessionSettingsInternal(exchangePrincipal, null, lcid, consistencyMode, syncLog, clientSecurityContext);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001702C File Offset: 0x0001522C
		public static IABSessionSettings GetSessionSettings(MailboxSession mailboxSession, int? lcid, ConsistencyMode? consistencyMode, SyncLog syncLog, ClientSecurityContext clientSecurityContext)
		{
			return ABDiscoveryManager.GetSessionSettingsInternal(mailboxSession.MailboxOwner, mailboxSession, lcid, consistencyMode, syncLog, clientSecurityContext);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00017040 File Offset: 0x00015240
		private static IABSessionSettings GetSessionSettingsInternal(IExchangePrincipal exchangePrincipal, MailboxSession mailboxSession, int? lcid, ConsistencyMode? consistencyMode, SyncLog syncLog, ClientSecurityContext clientSecurityContext)
		{
			return ABDiscoveryManager.CreateSessionSettingsForAD(exchangePrincipal, mailboxSession, lcid, consistencyMode, clientSecurityContext);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001705C File Offset: 0x0001525C
		private static IABSessionSettings CreateSessionSettingsForAD(IExchangePrincipal exchangePrincipal, MailboxSession mailboxSession, int? lcid, ConsistencyMode? consistencyMode, ClientSecurityContext clientSecurityContext)
		{
			int num;
			if (lcid != null)
			{
				num = lcid.Value;
			}
			else if (mailboxSession != null)
			{
				num = mailboxSession.Culture.LCID;
			}
			else
			{
				num = CultureInfo.CurrentCulture.LCID;
			}
			ConsistencyMode consistencyMode2;
			if (consistencyMode != null)
			{
				consistencyMode2 = consistencyMode.Value;
			}
			else
			{
				consistencyMode2 = ConsistencyMode.FullyConsistent;
			}
			ABSessionSettings absessionSettings = new ABSessionSettings();
			absessionSettings.Set("Provider", "AD");
			absessionSettings.Set("OrganizationId", exchangePrincipal.MailboxInfo.OrganizationId);
			absessionSettings.Set("Lcid", num);
			absessionSettings.Set("ConsistencyMode", consistencyMode2);
			absessionSettings.Set("ClientSecurityContext", clientSecurityContext);
			if (exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy != null)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 175, "CreateSessionSettingsForAD", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Common\\Subscription\\ABDiscoveryManager.cs");
				absessionSettings.Set("SearchRoot", DirectoryHelper.GetGlobalAddressListFromAddressBookPolicy(exchangePrincipal.MailboxInfo.Configuration.AddressBookPolicy, tenantOrTopologyConfigurationSession));
			}
			else
			{
				absessionSettings.Set("SearchRoot", null);
			}
			return absessionSettings;
		}
	}
}
