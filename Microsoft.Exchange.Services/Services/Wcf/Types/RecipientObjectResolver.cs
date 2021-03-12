using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009FA RID: 2554
	internal class RecipientObjectResolver
	{
		// Token: 0x0600482F RID: 18479 RVA: 0x00101319 File Offset: 0x000FF519
		private RecipientObjectResolver()
		{
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x00101321 File Offset: 0x000FF521
		// (set) Token: 0x06004831 RID: 18481 RVA: 0x00101328 File Offset: 0x000FF528
		internal static RecipientObjectResolver Instance { get; set; } = new RecipientObjectResolver();

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x00101330 File Offset: 0x000FF530
		private ADSessionSettings TenantSessionSetting
		{
			get
			{
				return ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), CallContext.Current.AccessingPrincipal.MailboxInfo.OrganizationId, null, false);
			}
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0010136C File Offset: 0x000FF56C
		internal IEnumerable<ADRecipient> ResolveLegacyDNs(IEnumerable<string> legacyDNs)
		{
			if (legacyDNs != null && legacyDNs.Any<string>())
			{
				IRecipientSession recipientSession = (IRecipientSession)this.CreateAdSession();
				return from recipient in recipientSession.FindADRecipientsByLegacyExchangeDNs(legacyDNs.ToArray<string>())
				where recipient.Data != null
				select recipient.Data;
			}
			return null;
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x001013F8 File Offset: 0x000FF5F8
		internal IEnumerable<ADRecipient> ResolveObjects(IEnumerable<ADObjectId> identities)
		{
			IRecipientSession session = (IRecipientSession)this.CreateAdSession();
			return from e in identities
			select session.Read(e);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x00101440 File Offset: 0x000FF640
		internal IEnumerable<ADRecipient> ResolveSmtpAddress(IEnumerable<string> addresses)
		{
			if (addresses != null && addresses.Any<string>())
			{
				return from recipient in this.ResolveProxyAddresses(from address in addresses
				select ProxyAddress.Parse(address))
				where recipient != null
				select recipient;
			}
			return null;
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x001014B0 File Offset: 0x000FF6B0
		private IEnumerable<ADRecipient> ResolveProxyAddresses(IEnumerable<ProxyAddress> proxyAddresses)
		{
			if (proxyAddresses != null && proxyAddresses.Any<ProxyAddress>())
			{
				IRecipientSession recipientSession = (IRecipientSession)this.CreateAdSession();
				return from recipient in recipientSession.FindByProxyAddresses(proxyAddresses.ToArray<ProxyAddress>())
				select recipient.Data;
			}
			return null;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x00101504 File Offset: 0x000FF704
		private IDirectorySession CreateAdSession()
		{
			IDirectorySession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, this.TenantSessionSetting, 158, "CreateAdSession", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\jsonservice\\types\\RecipientObjectResolver.cs");
			tenantOrRootOrgRecipientSession.SessionSettings.IncludeInactiveMailbox = true;
			return tenantOrRootOrgRecipientSession;
		}
	}
}
