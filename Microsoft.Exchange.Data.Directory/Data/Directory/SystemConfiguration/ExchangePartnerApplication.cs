using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000692 RID: 1682
	public class ExchangePartnerApplication
	{
		// Token: 0x06004E30 RID: 20016 RVA: 0x00120073 File Offset: 0x0011E273
		private ExchangePartnerApplication(string appId, string appName, string authMetadataUrl, string[] actAsPermissions)
		{
			this.appName = appName;
			this.appId = appId;
			this.authMetadataUrl = authMetadataUrl;
			this.actAsPermissions = actAsPermissions;
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x00120098 File Offset: 0x0011E298
		private ExchangePartnerApplication(string appId, string appName, string[] roles)
		{
			this.appName = appName;
			this.appId = appId;
			this.roles = roles;
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x001200B5 File Offset: 0x0011E2B5
		public string AppName
		{
			get
			{
				return this.appName;
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06004E33 RID: 20019 RVA: 0x001200BD File Offset: 0x0011E2BD
		public string AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06004E34 RID: 20020 RVA: 0x001200C5 File Offset: 0x0011E2C5
		public string[] Roles
		{
			get
			{
				return this.roles;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06004E35 RID: 20021 RVA: 0x001200CD File Offset: 0x0011E2CD
		public string AuthMetadataUrl
		{
			get
			{
				return this.authMetadataUrl;
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x001200D5 File Offset: 0x0011E2D5
		public string[] ActAsPermissions
		{
			get
			{
				return this.actAsPermissions;
			}
		}

		// Token: 0x04003539 RID: 13625
		private string appName;

		// Token: 0x0400353A RID: 13626
		private string appId;

		// Token: 0x0400353B RID: 13627
		private string[] roles;

		// Token: 0x0400353C RID: 13628
		private string authMetadataUrl;

		// Token: 0x0400353D RID: 13629
		private string[] actAsPermissions;

		// Token: 0x0400353E RID: 13630
		public static readonly ExchangePartnerApplication[] Office365CrossServiceFirstPartyAppList = new ExchangePartnerApplication[]
		{
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.Lync, "Lync Online", new string[]
			{
				"UserApplication",
				"ArchiveApplication"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.SharePoint, "SharePoint Online", new string[]
			{
				"UserApplication",
				"LegalHoldApplication",
				"Mailbox Search",
				"TeamMailboxLifecycleApplication",
				"Legal Hold",
				"ExchangeCrossServiceIntegration"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.CRM, "CRM Online", new string[]
			{
				"UserApplication",
				"PartnerDelegatedTenantManagement"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.Intune, "Intune Online", new string[]
			{
				"PartnerDelegatedTenantManagement"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.OfficeServiceManager, "Office ServiceManager", new string[]
			{
				"UserApplication"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.ExchangeOnlineProtection, "Exchange Online Protection", new string[]
			{
				"PartnerDelegatedTenantManagement"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.Office365Portal, "Office 365 Portal Online", new string[]
			{
				"UserApplication",
				"Organization Configuration"
			}),
			new ExchangePartnerApplication(WellknownPartnerApplicationIdentifiers.MicrosoftErp, "Microsoft.Erp", new string[]
			{
				"UserApplication"
			})
		};
	}
}
