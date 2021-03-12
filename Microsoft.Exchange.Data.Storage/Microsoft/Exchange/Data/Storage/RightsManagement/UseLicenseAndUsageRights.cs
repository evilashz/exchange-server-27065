using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B59 RID: 2905
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UseLicenseAndUsageRights
	{
		// Token: 0x06006945 RID: 26949 RVA: 0x001C42C0 File Offset: 0x001C24C0
		internal UseLicenseAndUsageRights(string useLicense, ContentRight usageRights, ExDateTime expiryTime, byte[] drmPropsSignature, OrganizationId organizationId, string publishingLicense, Uri licensingUri)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("useLicense", useLicense);
			ArgumentValidator.ThrowIfNull("drmPropsSignature", drmPropsSignature);
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			ArgumentValidator.ThrowIfNullOrEmpty("publishingLicense", publishingLicense);
			ArgumentValidator.ThrowIfNull("licensingUri", licensingUri);
			this.UseLicense = useLicense;
			this.UsageRights = usageRights;
			this.ExpiryTime = expiryTime;
			this.DRMPropsSignature = drmPropsSignature;
			this.OrganizationId = organizationId;
			this.PublishingLicense = publishingLicense;
			this.LicensingUri = licensingUri;
		}

		// Token: 0x04003BF0 RID: 15344
		public readonly string UseLicense;

		// Token: 0x04003BF1 RID: 15345
		public readonly ContentRight UsageRights;

		// Token: 0x04003BF2 RID: 15346
		public readonly ExDateTime ExpiryTime;

		// Token: 0x04003BF3 RID: 15347
		public readonly byte[] DRMPropsSignature;

		// Token: 0x04003BF4 RID: 15348
		public readonly OrganizationId OrganizationId;

		// Token: 0x04003BF5 RID: 15349
		public readonly string PublishingLicense;

		// Token: 0x04003BF6 RID: 15350
		public readonly Uri LicensingUri;
	}
}
