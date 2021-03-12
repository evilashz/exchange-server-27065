using System;
using System.Web;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B9 RID: 185
	public static class OwaUserAgentUtilities
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x00016641 File Offset: 0x00014841
		public static UserAgent CreateUserAgentAnonymous(HttpContext context)
		{
			return OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context, OwaUserAgentUtilities.GetLayoutString(context), false);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00016650 File Offset: 0x00014850
		public static UserAgent CreateUserAgentWithLayoutOverride(HttpContext context)
		{
			return OwaUserAgentUtilities.CreateUserAgentWithLayoutOverride(context, OwaUserAgentUtilities.GetLayoutString(context), true);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00016660 File Offset: 0x00014860
		public static UserAgent CreateUserAgentWithLayoutOverride(HttpContext context, string layout, bool userContextAvailable = true)
		{
			bool changeLayoutFeatureEnabled = false;
			if (userContextAvailable)
			{
				try
				{
					changeLayoutFeatureEnabled = UserContextManager.GetUserContext(context).FeaturesManager.ClientServerSettings.ChangeLayout.Enabled;
				}
				catch (OwaLockException)
				{
				}
				catch (OwaIdentityException)
				{
				}
				catch (NullReferenceException)
				{
				}
			}
			UserAgent userAgent = new UserAgent(context.Request.UserAgent, changeLayoutFeatureEnabled, context.Request.Cookies);
			userAgent.SetLayoutFromString(layout);
			return userAgent;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000166E8 File Offset: 0x000148E8
		private static string GetLayoutString(HttpContext context)
		{
			if (RequestDispatcherUtilities.GetStringUrlParameter(context, "sharepointapp") == "true")
			{
				return "mouse";
			}
			string text = RequestDispatcherUtilities.GetStringUrlParameter(context, "layout");
			if (string.IsNullOrEmpty(text))
			{
				text = OwaUserAgentUtilities.GetAppCacheManiestLayoutCookieValue(context);
			}
			return text;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00016730 File Offset: 0x00014930
		private static string GetAppCacheManiestLayoutCookieValue(HttpContext context)
		{
			HttpCookie httpCookie = context.Request.Cookies["ManifestLayout"];
			return (httpCookie != null) ? httpCookie.Value : null;
		}

		// Token: 0x040003FC RID: 1020
		public const string SharepointAppParamName = "sharepointapp";

		// Token: 0x040003FD RID: 1021
		public const string ManifestLayoutCookieName = "ManifestLayout";
	}
}
