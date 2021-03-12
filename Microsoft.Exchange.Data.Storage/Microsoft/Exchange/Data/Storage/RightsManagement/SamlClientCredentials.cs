using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B60 RID: 2912
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SamlClientCredentials : ClientCredentials
	{
		// Token: 0x06006985 RID: 27013 RVA: 0x001C4B20 File Offset: 0x001C2D20
		public SamlClientCredentials(LicenseIdentity identity, OrganizationId organizationId, Uri targetUri, Offer offer, IRmsLatencyTracker latencyTracker)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (targetUri == null)
			{
				throw new ArgumentNullException("targetUri");
			}
			if (offer == null)
			{
				throw new ArgumentNullException("offer");
			}
			base.SupportInteractive = false;
			this.Identity = identity;
			this.OrganizationId = organizationId;
			this.TargetUri = targetUri;
			this.Offer = offer;
			base.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x001C4B99 File Offset: 0x001C2D99
		protected SamlClientCredentials(SamlClientCredentials other) : base(other)
		{
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x001C4BA2 File Offset: 0x001C2DA2
		protected override ClientCredentials CloneCore()
		{
			return new SamlClientCredentials(this);
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x001C4BAA File Offset: 0x001C2DAA
		public override SecurityTokenManager CreateSecurityTokenManager()
		{
			if (this.cachedSecurityTokenManager == null)
			{
				this.cachedSecurityTokenManager = new SamlSecurityTokenManager(this);
			}
			return this.cachedSecurityTokenManager;
		}

		// Token: 0x04003C05 RID: 15365
		internal LicenseIdentity Identity;

		// Token: 0x04003C06 RID: 15366
		internal OrganizationId OrganizationId;

		// Token: 0x04003C07 RID: 15367
		internal Uri TargetUri;

		// Token: 0x04003C08 RID: 15368
		internal Offer Offer;

		// Token: 0x04003C09 RID: 15369
		internal IRmsLatencyTracker RmsLatencyTracker;

		// Token: 0x04003C0A RID: 15370
		private SamlSecurityTokenManager cachedSecurityTokenManager;
	}
}
