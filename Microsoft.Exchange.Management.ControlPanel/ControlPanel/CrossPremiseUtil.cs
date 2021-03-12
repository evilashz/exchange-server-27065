using System;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000252 RID: 594
	public static class CrossPremiseUtil
	{
		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x0007FF34 File Offset: 0x0007E134
		private static string DefaultRealmParameter
		{
			get
			{
				if (CrossPremiseUtil.defaultRealmParameter == null)
				{
					CrossPremiseUtil.defaultRealmParameter = (ConfigurationManager.AppSettings["CrossPremiseDefaultRealmParameter"] ?? "microsoftonline.com");
				}
				return CrossPremiseUtil.defaultRealmParameter;
			}
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x0007FF60 File Offset: 0x0007E160
		public static string UserFeatureAtCurrentOrg
		{
			get
			{
				RbacPrincipal rbacPrincipal = RbacPrincipal.Current;
				return string.Format("{0}{1}", '0', rbacPrincipal.IsInRole("UserOptions+OrgMgmControlPanel") ? '1' : '0');
			}
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x060028A4 RID: 10404 RVA: 0x0007FF9E File Offset: 0x0007E19E
		public static string OnPremiseLinkToOffice365
		{
			get
			{
				return CrossPremiseUtil.InternalOnPremiseLinkToOffice365(OrganizationCache.ServiceInstance);
			}
		}

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0007FFAA File Offset: 0x0007E1AA
		public static string OnPremiseLinkToOffice365WorldWide
		{
			get
			{
				return CrossPremiseUtil.InternalOnPremiseLinkToOffice365("0");
			}
		}

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0007FFB6 File Offset: 0x0007E1B6
		public static string OnPremiseLinkToOffice365Gallatin
		{
			get
			{
				return CrossPremiseUtil.InternalOnPremiseLinkToOffice365("1");
			}
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x0007FFC2 File Offset: 0x0007E1C2
		public static string GetLinkToCrossPremise(HttpContext context, HttpRequest request)
		{
			return CrossPremiseUtil.InternalGetLinkToCrossPremise(context, request, OrganizationCache.ServiceInstance);
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x0007FFD0 File Offset: 0x0007E1D0
		private static string InternalOnPremiseLinkToOffice365(string serviceInstance)
		{
			if (EacEnvironment.Instance.IsDataCenter)
			{
				return string.Empty;
			}
			string url = CrossPremiseUtil.InternalGetLinkToCrossPremise(HttpContext.Current, HttpContext.Current.Request, serviceInstance);
			return EcpUrl.AppendQueryParameter(url, "ov", "1");
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00080018 File Offset: 0x0007E218
		private static string InternalGetLinkToCrossPremise(HttpContext context, HttpRequest request, string serviceInstance)
		{
			string text = request.QueryString["cross"];
			bool flag = (!string.IsNullOrEmpty(text) && text != "0") || request.IsAuthenticatedByAdfs();
			string text2;
			if (flag)
			{
				text2 = RbacPrincipal.Current.RbacConfiguration.ExecutingUserPrincipalName;
				text2 = text2.Substring(text2.IndexOf('@') + 1);
			}
			else if (OrganizationCache.EntHasTargetDeliveryDomain)
			{
				text2 = OrganizationCache.EntTargetDeliveryDomain;
			}
			else
			{
				text2 = CrossPremiseUtil.DefaultRealmParameter;
			}
			string format;
			if (serviceInstance != null)
			{
				if (serviceInstance == "0")
				{
					format = OrganizationCache.CrossPremiseUrlFormatWorldWide;
					goto IL_A7;
				}
				if (serviceInstance == "1")
				{
					format = OrganizationCache.CrossPremiseUrlFormatGallatin;
					goto IL_A7;
				}
			}
			format = OrganizationCache.CrossPremiseUrlFormat;
			IL_A7:
			return string.Format(format, context.GetRequestUrl().Host, CrossPremiseUtil.UserFeatureAtCurrentOrg, text2);
		}

		// Token: 0x04002078 RID: 8312
		private const string CrossPremiseDefaultRealmParameterKey = "CrossPremiseDefaultRealmParameter";

		// Token: 0x04002079 RID: 8313
		private const string CrossPremiseDefaultRealmValue = "microsoftonline.com";

		// Token: 0x0400207A RID: 8314
		private static string defaultRealmParameter;
	}
}
