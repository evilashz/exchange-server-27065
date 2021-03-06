using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000F8 RID: 248
	internal class OTABootstrapMail
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0004AE00 File Offset: 0x00049000
		internal static bool NeedToSendBootstrapMailForWM61(IAirSyncContext airSyncContext, GlobalInfo globalInfo)
		{
			if (airSyncContext == null)
			{
				throw new ArgumentNullException("airSyncContext");
			}
			if (globalInfo == null)
			{
				throw new ArgumentNullException("globalInfo");
			}
			PolicyData policyData = ADNotificationManager.GetPolicyData(airSyncContext.User);
			if (policyData == null || !policyData.AllowMobileOTAUpdate)
			{
				return false;
			}
			if (airSyncContext.Request.Version != 121)
			{
				return false;
			}
			if (globalInfo.HaveSentBoostrapMailForWM61)
			{
				return false;
			}
			if (globalInfo.BootstrapMailForWM61TriggeredTime != null)
			{
				return false;
			}
			string deviceOS = globalInfo.DeviceOS;
			if (deviceOS == null || !deviceOS.StartsWith("Windows CE", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			try
			{
				Version v = new Version(deviceOS.Substring("Windows CE".Length).Trim());
				if (v < OTABootstrapMail.WM61VersionStartRange || v > OTABootstrapMail.WM61VersionEndRange)
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is ArgumentOutOfRangeException || ex is FormatException || ex is OverflowException)
				{
					return false;
				}
				throw ex;
			}
			return GlobalSettings.BootstrapCABForWM61HostingURL != null && GlobalSettings.MobileUpdateInformationURL != null;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0004AF14 File Offset: 0x00049114
		internal static void SendBootstrapMailForWM61(IAirSyncUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (user.ExchangePrincipal == null)
			{
				throw new ArgumentNullException("user.ExchangePrincipal");
			}
			using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(user.ExchangePrincipal, CultureInfo.InvariantCulture, "Client=ActiveSync"))
			{
				CultureInfo preferedCulture = mailboxSession.PreferedCulture;
				string subject = Strings.BootstrapMailForWM61Subject.ToString(preferedCulture);
				string text = string.Format("<a href=\"{0}\">{1}</a>", GlobalSettings.MobileUpdateInformationURL, AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body7.ToString(preferedCulture), false));
				string text2 = string.Format("<a href=\"{0}\">{1}</a>", GlobalSettings.BootstrapCABForWM61HostingURL, AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body8.ToString(preferedCulture), false));
				string text3 = string.Empty;
				IOrganizationSettingsData organizationSettingsData = ADNotificationManager.GetOrganizationSettingsData(user);
				if (organizationSettingsData != null && !string.IsNullOrEmpty(organizationSettingsData.OtaNotificationMailInsert))
				{
					text3 = string.Format("<p>{0}</p><hr>", organizationSettingsData.OtaNotificationMailInsert);
				}
				string body = string.Format("<html><head><style>p, li {{font-size:10pt; font-family:Tahoma;}}</style></head><body><p style=\"font-size:12pt; color:darkblue;\"><b>{0}</b></p><p>{1}</p><ul><li>{2}</li><li>{3}</li><li>{4}</li><li>{5}</li><li>{6}</li></ul><p><span style=\"font-size:12pt;\"><b>{7}</b></span><br>{8}</p><hr>{9}<p style=\"font-size:8pt; color:gray;\">{10}</p></body></html>", new object[]
				{
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body1.ToString(preferedCulture), false),
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body2.ToString(preferedCulture), false),
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body3.ToString(preferedCulture), false),
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body4.ToString(preferedCulture), false),
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body5.ToString(preferedCulture), false),
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body6.ToString(preferedCulture), false),
					text,
					text2,
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body9.ToString(preferedCulture), false),
					text3,
					AirSyncUtility.HtmlEncode(Strings.BootstrapMailForWM61Body10.ToString(preferedCulture), false)
				});
				SystemMessageHelper.PostMessage(mailboxSession, subject, body, "IPM.Note", Importance.High);
			}
		}

		// Token: 0x0400088B RID: 2187
		private const string WM61Prefix = "Windows CE";

		// Token: 0x0400088C RID: 2188
		private const string BootstrapMailBody = "<html><head><style>p, li {{font-size:10pt; font-family:Tahoma;}}</style></head><body><p style=\"font-size:12pt; color:darkblue;\"><b>{0}</b></p><p>{1}</p><ul><li>{2}</li><li>{3}</li><li>{4}</li><li>{5}</li><li>{6}</li></ul><p><span style=\"font-size:12pt;\"><b>{7}</b></span><br>{8}</p><hr>{9}<p style=\"font-size:8pt; color:gray;\">{10}</p></body></html>";

		// Token: 0x0400088D RID: 2189
		private const string AnchorFormat = "<a href=\"{0}\">{1}</a>";

		// Token: 0x0400088E RID: 2190
		private const string FooterFormat = "<p>{0}</p><hr>";

		// Token: 0x0400088F RID: 2191
		private static readonly Version WM61VersionStartRange = new Version(5, 2, 19202);

		// Token: 0x04000890 RID: 2192
		private static readonly Version WM61VersionEndRange = new Version(5, 2, 21141);
	}
}
