using System;
using System.Configuration;

namespace Microsoft.Exchange.Management.ControlPanel.Psws
{
	// Token: 0x0200036D RID: 877
	internal class TokenIssuerSettings
	{
		// Token: 0x06003003 RID: 12291 RVA: 0x00092449 File Offset: 0x00090649
		internal TokenIssuerSettings()
		{
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x00092451 File Offset: 0x00090651
		internal TokenIssuerSettings(string partnerId, string serviceId, string serviceHostName, string acsId, string acsUrl, string certificateSubject)
		{
			this.PartnerId = partnerId;
			this.ServiceId = serviceId;
			this.ServiceHostName = serviceHostName;
			this.AcsId = acsId;
			this.AcsUrl = new Uri(acsUrl);
			this.CertificateSubject = certificateSubject;
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x0009248B File Offset: 0x0009068B
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x00092493 File Offset: 0x00090693
		internal string PartnerId { get; set; }

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x0009249C File Offset: 0x0009069C
		// (set) Token: 0x06003008 RID: 12296 RVA: 0x000924A4 File Offset: 0x000906A4
		internal string ServiceId { get; set; }

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000924AD File Offset: 0x000906AD
		// (set) Token: 0x0600300A RID: 12298 RVA: 0x000924B5 File Offset: 0x000906B5
		internal string ServiceHostName { get; set; }

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000924BE File Offset: 0x000906BE
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x000924C6 File Offset: 0x000906C6
		internal string AcsId { get; set; }

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x000924CF File Offset: 0x000906CF
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x000924D7 File Offset: 0x000906D7
		internal Uri AcsUrl { get; set; }

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000924E0 File Offset: 0x000906E0
		// (set) Token: 0x06003010 RID: 12304 RVA: 0x000924E8 File Offset: 0x000906E8
		internal string CertificateSubject { get; set; }

		// Token: 0x06003011 RID: 12305 RVA: 0x000924F4 File Offset: 0x000906F4
		internal static TokenIssuerSettings CreateFromConfiguration()
		{
			return new TokenIssuerSettings(ConfigurationManager.AppSettings["PswsPartnerId"], ConfigurationManager.AppSettings["PswsServiceId"], ConfigurationManager.AppSettings["PswsServiceHostName"], ConfigurationManager.AppSettings["PswsAcsId"], ConfigurationManager.AppSettings["PswsAcsUrl"], ConfigurationManager.AppSettings["PswsCertSubject"]);
		}
	}
}
