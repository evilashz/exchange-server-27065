using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.Management.Extension;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001DF RID: 479
	public static class ExtensionUtility
	{
		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x00073709 File Offset: 0x00071909
		public static string UserInstallScope
		{
			get
			{
				if (!RbacPrincipal.Current.RbacConfiguration.HasRoleOfType(RoleType.MyReadWriteMailboxApps))
				{
					return "1";
				}
				return "3";
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x00073729 File Offset: 0x00071929
		public static string OrganizationInstallScope
		{
			get
			{
				return "2";
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00073730 File Offset: 0x00071930
		public static string Application
		{
			get
			{
				return "outlook.exe";
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x00073737 File Offset: 0x00071937
		public static string Version
		{
			get
			{
				return "15";
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x0007373E File Offset: 0x0007193E
		public static string DefaultInputForQueryString
		{
			get
			{
				return "0";
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00073745 File Offset: 0x00071945
		public static string ClickContext
		{
			get
			{
				return "4";
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x0007374C File Offset: 0x0007194C
		public static string HomePageTargetCode
		{
			get
			{
				return "HP";
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x00073753 File Offset: 0x00071953
		public static string EndNodeTargetCode
		{
			get
			{
				return "WA";
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x0007375A File Offset: 0x0007195A
		public static string ReviewsTargetCode
		{
			get
			{
				return "RV";
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x00073761 File Offset: 0x00071961
		public static string Clid
		{
			get
			{
				return Util.GetLCIDInDecimal();
			}
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x00073768 File Offset: 0x00071968
		public static string ClientFullVersion
		{
			get
			{
				return ExtensionData.ClientFullVersion;
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x0007376F File Offset: 0x0007196F
		public static string MarketplaceLandingPageUrl
		{
			get
			{
				return ExtensionData.LandingPageUrl;
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x00073776 File Offset: 0x00071976
		public static string MyAppsPageUrl
		{
			get
			{
				return ExtensionData.MyAppsPageUrl;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0007377D File Offset: 0x0007197D
		public static string MarketplaceServicesUrl
		{
			get
			{
				return ExtensionData.ConfigServiceUrl;
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x00073784 File Offset: 0x00071984
		public static string OfficeCallBackUrl
		{
			get
			{
				string url = EcpFeature.InstallExtensionCallBack.GetFeatureDescriptor().AbsoluteUrl.ToEscapedString();
				return ExtensionData.AppendEncodedQueryParameterForEcpCallback(url, "DeployId", ExtensionUtility.DeploymentId);
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000737B4 File Offset: 0x000719B4
		public static string OfficeCallBackUrlForOrg
		{
			get
			{
				string url = EcpFeature.OrgInstallExtensionCallBack.GetFeatureDescriptor().AbsoluteUrl.ToEscapedString();
				return ExtensionData.AppendEncodedQueryParameterForEcpCallback(url, "DeployId", ExtensionUtility.DeploymentId);
			}
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x000737E3 File Offset: 0x000719E3
		public static string UrlEncodedOfficeCallBackUrl
		{
			get
			{
				return HttpUtility.UrlEncode(ExtensionUtility.OfficeCallBackUrl);
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000737F0 File Offset: 0x000719F0
		public static string DeploymentId
		{
			get
			{
				ExchangeRunspaceConfiguration rbacConfiguration = RbacPrincipal.Current.RbacConfiguration;
				string domain = rbacConfiguration.ExecutingUserPrimarySmtpAddress.IsValidAddress ? rbacConfiguration.ExecutingUserPrimarySmtpAddress.Domain : string.Empty;
				return ExtensionDataHelper.GetDeploymentId(domain);
			}
		}

		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x060025B4 RID: 9652 RVA: 0x00073834 File Offset: 0x00071A34
		public static string UrlEncodedOfficeCallBackUrlForOrg
		{
			get
			{
				return HttpUtility.UrlEncode(ExtensionUtility.OfficeCallBackUrlForOrg);
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00073840 File Offset: 0x00071A40
		public static string GetRequirementsValueString(object requirements, bool isOrgScope)
		{
			string result = string.Empty;
			RequestedCapabilities valueOrDefault = ((RequestedCapabilities?)requirements).GetValueOrDefault();
			RequestedCapabilities? requestedCapabilities;
			if (requestedCapabilities != null)
			{
				switch (valueOrDefault)
				{
				case RequestedCapabilities.Restricted:
					result = (isOrgScope ? Strings.RequirementsRestrictedValue : OwaOptionStrings.RequirementsRestrictedValue);
					break;
				case RequestedCapabilities.ReadItem:
					result = (isOrgScope ? Strings.RequirementsReadItemValue : OwaOptionStrings.RequirementsReadItemValue);
					break;
				case RequestedCapabilities.ReadWriteMailbox:
					result = (isOrgScope ? Strings.RequirementsReadWriteMailboxValue : OwaOptionStrings.RequirementsReadWriteMailboxValue);
					break;
				case RequestedCapabilities.ReadWriteItem:
					result = (isOrgScope ? Strings.RequirementsReadWriteItemValue : OwaOptionStrings.RequirementsReadWriteItemValue);
					break;
				}
			}
			return result;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000738E0 File Offset: 0x00071AE0
		public static string GetRequirementsDescriptionString(object requirements, bool isOrgScope)
		{
			string result = string.Empty;
			RequestedCapabilities valueOrDefault = ((RequestedCapabilities?)requirements).GetValueOrDefault();
			RequestedCapabilities? requestedCapabilities;
			if (requestedCapabilities != null)
			{
				switch (valueOrDefault)
				{
				case RequestedCapabilities.Restricted:
					result = (isOrgScope ? Strings.RequirementsRestrictedDescription : OwaOptionStrings.RequirementsRestrictedDescription);
					break;
				case RequestedCapabilities.ReadItem:
					result = (isOrgScope ? Strings.RequirementsReadItemDescription : OwaOptionStrings.RequirementsReadItemDescription);
					break;
				case RequestedCapabilities.ReadWriteMailbox:
					result = (isOrgScope ? Strings.RequirementsReadWriteMailboxDescription : OwaOptionStrings.RequirementsReadWriteMailboxDescription);
					break;
				case RequestedCapabilities.ReadWriteItem:
					result = (isOrgScope ? Strings.RequirementsReadWriteItemDescription : OwaOptionStrings.RequirementsReadWriteItemDescription);
					break;
				}
			}
			return result;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00073980 File Offset: 0x00071B80
		internal static void ExtensionGetPostAction(bool isOrgScope, DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ExtensionType extensionType = (ExtensionType)dataRow["Type"];
				if (ExtensionType.MarketPlace == extensionType)
				{
					dataRow["IsMarketplaceExtension"] = true;
					string text = string.Format("{0}?app={1}&ver={2}&clid={3}&p1={4}&p2={5}&p3={6}&p4={7}&p5={8}&Scope={9}&CallBackURL={10}&DeployId={11}", new object[]
					{
						ExtensionUtility.MarketplaceLandingPageUrl,
						ExtensionUtility.Application,
						ExtensionUtility.Version,
						Util.GetLCIDInDecimal(),
						ExtensionUtility.ClientFullVersion,
						ExtensionUtility.ClickContext,
						ExtensionUtility.DefaultInputForQueryString,
						ExtensionUtility.EndNodeTargetCode,
						dataRow["MarketplaceContentmarket"] + "\\" + dataRow["MarketplaceAssetID"],
						isOrgScope ? ExtensionUtility.OrganizationInstallScope : ExtensionUtility.UserInstallScope,
						isOrgScope ? ExtensionUtility.UrlEncodedOfficeCallBackUrlForOrg : ExtensionUtility.UrlEncodedOfficeCallBackUrl,
						ExtensionUtility.DeploymentId
					});
					string myAppsPageLink = string.Format("{0}?app={1}&ver={2}&clid={3}&p1={4}&p2={5}&p3={6}&p4={7}&p5={8}&Scope={9}&CallBackURL={10}&DeployId={11}", new object[]
					{
						ExtensionUtility.MyAppsPageUrl,
						ExtensionUtility.Application,
						ExtensionUtility.Version,
						Util.GetLCIDInDecimal(),
						ExtensionUtility.ClientFullVersion,
						ExtensionUtility.ClickContext,
						ExtensionUtility.DefaultInputForQueryString,
						ExtensionUtility.EndNodeTargetCode,
						dataRow["MarketplaceContentmarket"] + "\\" + dataRow["MarketplaceAssetID"],
						isOrgScope ? ExtensionUtility.OrganizationInstallScope : ExtensionUtility.UserInstallScope,
						isOrgScope ? ExtensionUtility.UrlEncodedOfficeCallBackUrlForOrg : ExtensionUtility.UrlEncodedOfficeCallBackUrl,
						ExtensionUtility.DeploymentId
					});
					string directCallbackLink = string.Format("{0}&Scope={1}&lc={2}&clientToken={3}&AssetId={4}", new object[]
					{
						isOrgScope ? ExtensionUtility.OfficeCallBackUrlForOrg : ExtensionUtility.OfficeCallBackUrl,
						isOrgScope ? ExtensionUtility.OrganizationInstallScope : ExtensionUtility.UserInstallScope,
						dataRow["MarketplaceContentmarket"],
						dataRow["Etoken"],
						dataRow["MarketplaceAssetID"]
					});
					dataRow["DetailsUrl"] = text;
					dataRow["ReviewUrl"] = string.Format("{0}?app={1}&ver={2}&clid={3}&p1={4}&p2={5}&p3={6}&p4={7}&p5={8}&Scope={9}&CallBackURL={10}&DeployId={11}", new object[]
					{
						ExtensionUtility.MarketplaceLandingPageUrl,
						ExtensionUtility.Application,
						ExtensionUtility.Version,
						Util.GetLCIDInDecimal(),
						ExtensionUtility.ClientFullVersion,
						ExtensionUtility.ClickContext,
						ExtensionUtility.DefaultInputForQueryString,
						ExtensionUtility.ReviewsTargetCode,
						dataRow["MarketplaceContentMarket"] + "\\" + dataRow["MarketplaceAssetID"],
						isOrgScope ? ExtensionUtility.OrganizationInstallScope : ExtensionUtility.UserInstallScope,
						isOrgScope ? ExtensionUtility.UrlEncodedOfficeCallBackUrlForOrg : ExtensionUtility.UrlEncodedOfficeCallBackUrl,
						ExtensionUtility.DeploymentId
					});
					string text2 = (dataRow["AppStatus"] != null && !DBNull.Value.Equals(dataRow["AppStatus"])) ? ((string)dataRow["AppStatus"]) : string.Empty;
					if (!string.IsNullOrWhiteSpace(text2))
					{
						dataRow["ShowNotification"] = true;
						ExtensionUtility.SetErrorDescriptionAndNotificationLink(text2, dataRow, text, directCallbackLink, myAppsPageLink);
					}
					string value = (dataRow["LicenseType"] != null && !DBNull.Value.Equals(dataRow["LicenseType"])) ? ((string)dataRow["LicenseType"]) : string.Empty;
					if (Microsoft.Exchange.Management.Extension.LicenseType.Trial.ToString().Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						dataRow["ShowTrialReminder"] = true;
						dataRow["TrialReminderActionLinkUrl"] = text;
						dataRow["ShowTrialReminderActionLink"] = true;
					}
				}
				ExtensionInstallScope extensionInstallScope = (ExtensionInstallScope)dataRow["Scope"];
				if (isOrgScope)
				{
					dataRow["ExtensionCanBeUninstalled"] = (extensionInstallScope == ExtensionInstallScope.Organization);
					dataRow["ShowEnableDisable"] = true;
				}
				else
				{
					dataRow["ExtensionCanBeUninstalled"] = (extensionInstallScope == ExtensionInstallScope.User);
					DefaultStateForUser? defaultStateForUser = dataRow["DefaultStateForUser"] as DefaultStateForUser?;
					if (defaultStateForUser != null && defaultStateForUser.Value == DefaultStateForUser.AlwaysEnabled)
					{
						dataRow["ExtensionCanNotBeUninstalledMessage"] = OwaOptionStrings.ExtensionCanNotBeDisabledNorUninstalled;
						dataRow["ShowEnableDisable"] = false;
					}
					else
					{
						dataRow["ExtensionCanNotBeUninstalledMessage"] = OwaOptionStrings.ExtensionCanNotBeUninstalled;
						dataRow["ShowEnableDisable"] = true;
					}
					if (extensionInstallScope == ExtensionInstallScope.Organization)
					{
						dataRow["ShowNotificationLink"] = false;
						dataRow["ShowTrialReminderActionLink"] = false;
					}
				}
				if (dataRow["IconURL"].IsNullValue())
				{
					string themeResource = ThemeResource.GetThemeResource(ExtensionUtility.pagesSection.Theme, "extensiondefaulticon.png");
					dataRow["IconURL"] = new Uri(themeResource);
				}
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00073EC0 File Offset: 0x000720C0
		internal static void SetErrorDescriptionAndNotificationLink(string errorCode, DataRow row, string appPageLink, string directCallbackLink, string myAppsPageLink)
		{
			switch (errorCode)
			{
			case "1.0":
				row["NotificationText"] = Strings.AppErrorCode1_0;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickToUpdateAppText;
				row["NotificationLinkUrl"] = directCallbackLink;
				return;
			case "1.1":
				row["NotificationText"] = Strings.AppErrorCode1_1;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickToUpdateAppText;
				row["NotificationLinkUrl"] = directCallbackLink;
				return;
			case "1.2":
				row["NotificationText"] = Strings.AppErrorCode1_2;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickToUpdateAppText;
				row["NotificationLinkUrl"] = appPageLink;
				return;
			case "2.0":
				row["NotificationText"] = Strings.AppErrorCode2_0;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickToUpdateAppLincenseText;
				row["NotificationLinkUrl"] = myAppsPageLink;
				return;
			case "2.1":
				row["NotificationText"] = Strings.AppErrorCode2_1;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickToUpdateAppLincenseText;
				row["NotificationLinkUrl"] = myAppsPageLink;
				return;
			case "3.0":
				row["NotificationText"] = Strings.AppErrorCode3_0;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickForMoreAppDetailsText;
				row["NotificationLinkUrl"] = appPageLink;
				return;
			case "3.1":
				row["NotificationText"] = Strings.AppErrorCode3_1;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickForMoreAppDetailsText;
				row["NotificationLinkUrl"] = appPageLink;
				return;
			case "3.2":
				row["NotificationText"] = Strings.AppErrorCode3_2;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickForMoreAppDetailsText;
				row["NotificationLinkUrl"] = appPageLink;
				return;
			case "3.3":
				row["NotificationText"] = Strings.AppErrorCode3_3;
				row["ShowNotificationLink"] = true;
				row["NotificationLinkText"] = Strings.ClickForMoreAppDetailsText;
				row["NotificationLinkUrl"] = appPageLink;
				return;
			case "4.0":
				row["NotificationText"] = Strings.AppErrorCode4_0;
				row["ShowNotificationLink"] = false;
				return;
			case "4.1":
				row["NotificationText"] = Strings.AppErrorCode4_1;
				row["ShowNotificationLink"] = false;
				break;

				return;
			}
		}

		// Token: 0x04001F11 RID: 7953
		private static readonly PagesSection pagesSection = (PagesSection)ConfigurationManager.GetSection("system.web/pages");
	}
}
