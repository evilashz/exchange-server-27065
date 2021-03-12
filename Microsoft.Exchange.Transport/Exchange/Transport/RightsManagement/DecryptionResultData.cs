using System;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003D4 RID: 980
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DecryptionResultData
	{
		// Token: 0x06002CCA RID: 11466 RVA: 0x000B2646 File Offset: 0x000B0846
		public DecryptionResultData(EmailMessage decryptedMessage, string useLicense, Uri licenseUri)
		{
			this.decryptedMessage = decryptedMessage;
			this.useLicense = useLicense;
			this.licenseUri = licenseUri;
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000B2663 File Offset: 0x000B0863
		internal EmailMessage DecryptedMessage
		{
			get
			{
				return this.decryptedMessage;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000B266B File Offset: 0x000B086B
		internal string UseLicense
		{
			get
			{
				return this.useLicense;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x000B2673 File Offset: 0x000B0873
		internal Uri LicenseUri
		{
			get
			{
				return this.licenseUri;
			}
		}

		// Token: 0x04001659 RID: 5721
		private readonly EmailMessage decryptedMessage;

		// Token: 0x0400165A RID: 5722
		private readonly string useLicense;

		// Token: 0x0400165B RID: 5723
		private readonly Uri licenseUri;
	}
}
