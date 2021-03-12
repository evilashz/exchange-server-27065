using System;
using System.Threading;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200010F RID: 271
	internal class StaticLinkTopics
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x000228E8 File Offset: 0x00020AE8
		public static string GetPatchDownloadUrl(string downloadVersion)
		{
			return string.Format("{0}&version={1}&locale={2}", "http://go.microsoft.com/fwlink/?LinkId=179178", downloadVersion, Thread.CurrentThread.CurrentUICulture.Name);
		}

		// Token: 0x0400043F RID: 1087
		public const string MicrosoftDownload = "http://www.microsoft.com/download";

		// Token: 0x04000440 RID: 1088
		public const string ToolsWebsite = "http://go.microsoft.com/fwlink/?LinkId=186692";

		// Token: 0x04000441 RID: 1089
		public const string MoreCEIPUrl = "http://go.microsoft.com/fwlink/?LinkId=50163";

		// Token: 0x04000442 RID: 1090
		public const string NewCertificateRecommendDocuments = "http://go.microsoft.com/fwlink/?LinkID=115184";

		// Token: 0x04000443 RID: 1091
		public const string CertificateRequestHelpURL = "http://go.microsoft.com/fwlink/?LinkId=115674";

		// Token: 0x04000444 RID: 1092
		public const string SelfSignedCertificateHelpURL = "http://go.microsoft.com/fwlink/?LinkId=119806";

		// Token: 0x04000445 RID: 1093
		public const string WildCardCertificateHelpURL = "http://go.microsoft.com/fwlink/?LinkId=115674";

		// Token: 0x04000446 RID: 1094
		public const string ExchangeBlogLink = "http://go.microsoft.com/fwlink/?LinkId=92313";

		// Token: 0x04000447 RID: 1095
		public const string PatchDownloader = "http://go.microsoft.com/fwlink/?LinkId=179178";

		// Token: 0x04000448 RID: 1096
		public const string SubmitfeedbackLink = "http://go.microsoft.com/fwlink/?LinkId=71967";

		// Token: 0x04000449 RID: 1097
		public const string CeipProgramLink = "http://go.microsoft.com/fwlink/?LinkID=64471";

		// Token: 0x0400044A RID: 1098
		public const string CeipPrivacyStatementLink = "http://go.microsoft.com/fwlink/?linkid=52097";

		// Token: 0x0400044B RID: 1099
		public const string ExchangeTechNetLink = "http://go.microsoft.com/fwlink/?LinkId=130589";

		// Token: 0x0400044C RID: 1100
		public const string ExchangeOnlineLearnMoreLink = "http://go.microsoft.com/fwlink/?LinkId=187621";

		// Token: 0x0400044D RID: 1101
		public const string RemoteMailboxLicenseInformationMoreLink = "http://go.microsoft.com/fwlink/?LinkId=183883";

		// Token: 0x0400044E RID: 1102
		public const string IDCRLComponentDownloadLink = "http://go.microsoft.com/fwlink/?LinkId=200953";

		// Token: 0x0400044F RID: 1103
		public const string Office365HelpLink = "http://go.microsoft.com/fwlink/p/?LinkId=258351";
	}
}
