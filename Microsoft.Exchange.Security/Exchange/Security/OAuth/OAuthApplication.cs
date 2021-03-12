using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000CE RID: 206
	internal sealed class OAuthApplication
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x000324CC File Offset: 0x000306CC
		public OAuthApplication(PartnerApplication partnerApplication)
		{
			OAuthCommon.VerifyNonNullArgument("partnerApplication", partnerApplication);
			this.PartnerApplication = partnerApplication;
			this.ApplicationType = OAuthApplicationType.S2SApp;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000324FA File Offset: 0x000306FA
		public OAuthApplication(OfficeExtensionInfo officeExtensionInfo)
		{
			OAuthCommon.VerifyNonNullArgument("officeExtensionInfo", officeExtensionInfo);
			this.OfficeExtension = officeExtensionInfo;
			this.ApplicationType = OAuthApplicationType.CallbackApp;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00032528 File Offset: 0x00030728
		public OAuthApplication(V1ProfileAppInfo v1ProfileAppInfo)
		{
			OAuthCommon.VerifyNonNullArgument("v1ProfileAppInfo", v1ProfileAppInfo);
			this.V1ProfileApp = v1ProfileAppInfo;
			this.ApplicationType = OAuthApplicationType.V1App;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00032558 File Offset: 0x00030758
		public OAuthApplication(V1ProfileAppInfo v1ProfileApp, PartnerApplication partnerApplication)
		{
			OAuthCommon.VerifyNonNullArgument("v1ProfileApp", v1ProfileApp);
			OAuthCommon.VerifyNonNullArgument("partnerApplication", partnerApplication);
			this.PartnerApplication = partnerApplication;
			this.V1ProfileApp = v1ProfileApp;
			this.ApplicationType = OAuthApplicationType.V1ExchangeSelfIssuedApp;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000325A4 File Offset: 0x000307A4
		public OAuthApplication()
		{
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000325B8 File Offset: 0x000307B8
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x000325C0 File Offset: 0x000307C0
		public PartnerApplication PartnerApplication { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000325C9 File Offset: 0x000307C9
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x000325D1 File Offset: 0x000307D1
		public OfficeExtensionInfo OfficeExtension { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x000325DA File Offset: 0x000307DA
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x000325E2 File Offset: 0x000307E2
		public V1ProfileAppInfo V1ProfileApp { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000325EB File Offset: 0x000307EB
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x000325F3 File Offset: 0x000307F3
		public OAuthApplicationType ApplicationType { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x000325FC File Offset: 0x000307FC
		public bool IsOfficeExtension
		{
			get
			{
				return this.OfficeExtension != null;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0003260A File Offset: 0x0003080A
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00032612 File Offset: 0x00030812
		public bool? IsFromSameOrgExchange
		{
			get
			{
				return this.isFromSameOrgExchange;
			}
			set
			{
				this.isFromSameOrgExchange = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0003261C File Offset: 0x0003081C
		public string Id
		{
			get
			{
				string result = "<unknown>";
				switch (this.ApplicationType)
				{
				case OAuthApplicationType.S2SApp:
					result = string.Format("S2S~{0}", this.PartnerApplication.ApplicationIdentifier);
					break;
				case OAuthApplicationType.CallbackApp:
					result = string.Format("CLB~{0}", this.OfficeExtension.ExtensionId);
					break;
				case OAuthApplicationType.V1App:
					result = string.Format("V1A~{0}", this.V1ProfileApp.AppId);
					break;
				case OAuthApplicationType.V1ExchangeSelfIssuedApp:
					result = string.Format("V1S~{0}", this.V1ProfileApp.AppId);
					break;
				}
				return result;
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000326B0 File Offset: 0x000308B0
		public void AddExtensionDataToCommonAccessToken(CommonAccessToken token)
		{
			token.ExtensionData["AppType"] = this.ApplicationType.ToString();
			switch (this.ApplicationType)
			{
			case OAuthApplicationType.S2SApp:
				token.ExtensionData["AppDn"] = this.PartnerApplication.DistinguishedName;
				token.ExtensionData["AppId"] = this.PartnerApplication.ApplicationIdentifier;
				token.ExtensionData["AppRealm"] = this.PartnerApplication.Realm;
				if (this.IsFromSameOrgExchange != null && this.IsFromSameOrgExchange.Value)
				{
					token.ExtensionData["IsFromSameOrgExchange"] = bool.TrueString;
					return;
				}
				break;
			case OAuthApplicationType.CallbackApp:
				token.ExtensionData["CallbackAppId"] = this.OfficeExtension.ExtensionId;
				if (this.OfficeExtension.IsScopedToken)
				{
					token.ExtensionData["Scope"] = this.OfficeExtension.Scope;
					return;
				}
				break;
			case OAuthApplicationType.V1App:
				token.ExtensionData["V1AppId"] = this.V1ProfileApp.AppId;
				token.ExtensionData["Scope"] = this.V1ProfileApp.Scope;
				token.ExtensionData["Role"] = this.V1ProfileApp.Role;
				return;
			case OAuthApplicationType.V1ExchangeSelfIssuedApp:
				token.ExtensionData["V1AppId"] = this.V1ProfileApp.AppId;
				token.ExtensionData["Scope"] = this.V1ProfileApp.Scope;
				token.ExtensionData["Role"] = this.V1ProfileApp.Role;
				token.ExtensionData["AppDn"] = this.PartnerApplication.DistinguishedName;
				token.ExtensionData["AppId"] = this.PartnerApplication.ApplicationIdentifier;
				token.ExtensionData["AppRealm"] = this.PartnerApplication.Realm;
				break;
			default:
				return;
			}
		}

		// Token: 0x0400067D RID: 1661
		private bool? isFromSameOrgExchange = null;
	}
}
