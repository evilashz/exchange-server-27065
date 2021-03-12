using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000479 RID: 1145
	public class SMimeOptions : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x06002BDC RID: 11228 RVA: 0x000F5223 File Offset: 0x000F3423
		protected override void OnLoad(EventArgs e)
		{
			if (!base.UserContext.IsFeatureEnabled(Feature.SMime))
			{
				throw new OwaSegmentationException("SMime is disabled.");
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000F5243 File Offset: 0x000F3443
		protected static string SMimeClientControlPath
		{
			get
			{
				if (SMimeOptions.path == null)
				{
					SMimeOptions.path = "smime/owasmime.msi?v=" + Utilities.ReadSMimeControlVersionOnServer();
				}
				return SMimeOptions.path;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000F5265 File Offset: 0x000F3465
		protected bool ManuallyPickCertificate
		{
			get
			{
				return base.UserContext.UserOptions.ManuallyPickCertificate;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000F5277 File Offset: 0x000F3477
		protected string SigningCertificateSubject
		{
			get
			{
				return base.UserContext.UserOptions.SigningCertificateSubject ?? string.Empty;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000F5292 File Offset: 0x000F3492
		protected string SigningCertificateId
		{
			get
			{
				return base.UserContext.UserOptions.SigningCertificateId ?? string.Empty;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000F52AD File Offset: 0x000F34AD
		protected bool HasSelectCertificateSection
		{
			get
			{
				return OwaRegistryKeys.AllowUserChoiceOfSigningCertificate;
			}
		}

		// Token: 0x04001D04 RID: 7428
		private static string path;
	}
}
