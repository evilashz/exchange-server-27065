using System;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class App : XsoMailboxConfigurationObject
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000248A File Offset: 0x0000068A
		public App()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002494 File Offset: 0x00000694
		public App(DefaultStateForUser? defaultStateForUser, string marketplaceAssetID, string marketplaceContentMarket, string providerName, Uri iconURL, string extensionId, string version, ExtensionType? type, ExtensionInstallScope? scope, RequestedCapabilities? requirements, string displayName, string description, bool enabled, string manifestXml, ADObjectId mailboxOwnerId, string eToken, EntitlementTokenData eTokenData, string appStatus)
		{
			this.DefaultStateForUser = defaultStateForUser;
			this.MarketplaceAssetID = marketplaceAssetID;
			this.MarketplaceContentMarket = marketplaceContentMarket;
			this.ProviderName = providerName;
			this.IconURL = iconURL;
			this.AppId = extensionId;
			this.AppVersion = version;
			this.Type = type;
			this.Scope = scope;
			this.Requirements = requirements;
			this.DisplayName = displayName;
			this.Description = description;
			this.Enabled = enabled;
			this.ManifestXml = manifestXml;
			base.MailboxOwnerId = mailboxOwnerId;
			this.Etoken = eToken;
			this.AppStatus = appStatus;
			if (eTokenData != null)
			{
				base.SetExchangeVersion(ExchangeObjectVersion.Current);
				this.LicensePurchaser = eTokenData.LicensePurchaser;
				this.EtokenExpirationDate = eTokenData.EtokenExpirationDate.ToString();
				this.LicenseType = new LicenseType?((LicenseType)eTokenData.LicenseType);
				this.SeatsPurchased = eTokenData.SeatsPurchased.ToString();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000258A File Offset: 0x0000078A
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return App.schema;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002591 File Offset: 0x00000791
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002598 File Offset: 0x00000798
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000025A0 File Offset: 0x000007A0
		internal bool IsDownloadOnly { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000025A9 File Offset: 0x000007A9
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000025BB File Offset: 0x000007BB
		public string AppStatus
		{
			get
			{
				return (string)this[OWAExtensionSchema.AppStatus];
			}
			set
			{
				this[OWAExtensionSchema.AppStatus] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000025C9 File Offset: 0x000007C9
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000025DB File Offset: 0x000007DB
		public string MarketplaceAssetID
		{
			get
			{
				return (string)this[OWAExtensionSchema.MarketplaceAssetID];
			}
			set
			{
				this[OWAExtensionSchema.MarketplaceAssetID] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000025E9 File Offset: 0x000007E9
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000025FB File Offset: 0x000007FB
		public string MarketplaceContentMarket
		{
			get
			{
				return (string)this[OWAExtensionSchema.MarketplaceContentMarket];
			}
			set
			{
				this[OWAExtensionSchema.MarketplaceContentMarket] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002609 File Offset: 0x00000809
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000261B File Offset: 0x0000081B
		public string ProviderName
		{
			get
			{
				return (string)this[OWAExtensionSchema.ProviderName];
			}
			set
			{
				this[OWAExtensionSchema.ProviderName] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002629 File Offset: 0x00000829
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000263B File Offset: 0x0000083B
		public Uri IconURL
		{
			get
			{
				return (Uri)this[OWAExtensionSchema.IconURL];
			}
			set
			{
				this[OWAExtensionSchema.IconURL] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002649 File Offset: 0x00000849
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000265B File Offset: 0x0000085B
		public string AppId
		{
			get
			{
				return (string)this[OWAExtensionSchema.AppId];
			}
			set
			{
				this[OWAExtensionSchema.AppId] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002669 File Offset: 0x00000869
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000267B File Offset: 0x0000087B
		public ExtensionType? Type
		{
			get
			{
				return (ExtensionType?)this[OWAExtensionSchema.Type];
			}
			set
			{
				this[OWAExtensionSchema.Type] = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000268E File Offset: 0x0000088E
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000026A0 File Offset: 0x000008A0
		public string AppVersion
		{
			get
			{
				return (string)this[OWAExtensionSchema.AppVersion];
			}
			set
			{
				this[OWAExtensionSchema.AppVersion] = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000026AE File Offset: 0x000008AE
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000026C0 File Offset: 0x000008C0
		public ExtensionInstallScope? Scope
		{
			get
			{
				return (ExtensionInstallScope?)this[OWAExtensionSchema.Scope];
			}
			set
			{
				this[OWAExtensionSchema.Scope] = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000026D3 File Offset: 0x000008D3
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000026E5 File Offset: 0x000008E5
		public RequestedCapabilities? Requirements
		{
			get
			{
				return (RequestedCapabilities?)this[OWAExtensionSchema.Requirements];
			}
			set
			{
				this[OWAExtensionSchema.Requirements] = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000026F8 File Offset: 0x000008F8
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000270A File Offset: 0x0000090A
		public DefaultStateForUser? DefaultStateForUser
		{
			get
			{
				return (DefaultStateForUser?)this[OWAExtensionSchema.DefaultStateForUser];
			}
			set
			{
				this[OWAExtensionSchema.DefaultStateForUser] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000271D File Offset: 0x0000091D
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000272F File Offset: 0x0000092F
		public bool Enabled
		{
			get
			{
				return (bool)this[OWAExtensionSchema.Enabled];
			}
			set
			{
				this[OWAExtensionSchema.Enabled] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002742 File Offset: 0x00000942
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002754 File Offset: 0x00000954
		public string DisplayName
		{
			get
			{
				return (string)this[OWAExtensionSchema.DisplayName];
			}
			set
			{
				this[OWAExtensionSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002762 File Offset: 0x00000962
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002774 File Offset: 0x00000974
		public string Description
		{
			get
			{
				return (string)this[OWAExtensionSchema.Description];
			}
			set
			{
				this[OWAExtensionSchema.Description] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002782 File Offset: 0x00000982
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002794 File Offset: 0x00000994
		public string ManifestXml
		{
			get
			{
				return (string)this[OWAExtensionSchema.ManifestXml];
			}
			set
			{
				this[OWAExtensionSchema.ManifestXml] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000027A2 File Offset: 0x000009A2
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000027B4 File Offset: 0x000009B4
		public string EtokenExpirationDate
		{
			get
			{
				return (string)this[OWAExtensionSchema.EtokenExpirationDate];
			}
			set
			{
				this[OWAExtensionSchema.EtokenExpirationDate] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000027C2 File Offset: 0x000009C2
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000027D4 File Offset: 0x000009D4
		public LicenseType? LicenseType
		{
			get
			{
				return (LicenseType?)this[OWAExtensionSchema.LicenseType];
			}
			set
			{
				this[OWAExtensionSchema.LicenseType] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027E7 File Offset: 0x000009E7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000027F9 File Offset: 0x000009F9
		public string SeatsPurchased
		{
			get
			{
				return (string)this[OWAExtensionSchema.SeatsPurchased];
			}
			set
			{
				this[OWAExtensionSchema.SeatsPurchased] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002807 File Offset: 0x00000A07
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002819 File Offset: 0x00000A19
		public string LicensePurchaser
		{
			get
			{
				return (string)this[OWAExtensionSchema.LicensePurchaser];
			}
			set
			{
				this[OWAExtensionSchema.LicensePurchaser] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002827 File Offset: 0x00000A27
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002839 File Offset: 0x00000A39
		public string Etoken
		{
			get
			{
				return (string)this[OWAExtensionSchema.Etoken];
			}
			set
			{
				this[OWAExtensionSchema.Etoken] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002847 File Offset: 0x00000A47
		public override ObjectId Identity
		{
			get
			{
				return (ObjectId)this[OWAExtensionSchema.Identity];
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000285C File Offset: 0x00000A5C
		internal virtual ExtensionData GetExtensionDataForInstall(IRecipientSession adRecipientSession)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.LoadXml(this.ManifestXml);
			return ExtensionData.CreateForXmlStorage(this.AppId, this.MarketplaceAssetID, this.MarketplaceContentMarket, this.Type, this.Scope, this.Enabled, this.AppVersion, DisableReasonType.NotDisabled, safeXmlDocument, this.AppStatus, this.Etoken);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028C0 File Offset: 0x00000AC0
		internal static object IdentityGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[XsoMailboxConfigurationObjectSchema.MailboxOwnerId];
			string extensionId = (string)propertyBag[OWAExtensionSchema.AppId];
			string displayName = (string)propertyBag[OWAExtensionSchema.DisplayName];
			if (adobjectId != null)
			{
				return new AppId(adobjectId, displayName, extensionId);
			}
			return null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000290D File Offset: 0x00000B0D
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			if (!string.IsNullOrEmpty(this.DisplayName))
			{
				return this.DisplayName;
			}
			return base.ToString();
		}

		// Token: 0x0400001A RID: 26
		private static OWAExtensionSchema schema = ObjectSchema.GetInstance<OWAExtensionSchema>();
	}
}
