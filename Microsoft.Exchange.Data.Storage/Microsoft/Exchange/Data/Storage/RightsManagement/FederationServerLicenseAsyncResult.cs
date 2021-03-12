using System;
using System.Xml;
using Microsoft.com.IPC.WSService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B6B RID: 2923
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FederationServerLicenseAsyncResult : UseLicenseAsyncResult
	{
		// Token: 0x060069CF RID: 27087 RVA: 0x001C5325 File Offset: 0x001C3525
		public FederationServerLicenseAsyncResult(RmsClientManagerContext context, Uri licenseUri, XmlNode[] issuanceLicense, LicenseIdentity[] identities, LicenseResponse[] responses, object callerState, AsyncCallback callerCallback) : base(context, licenseUri, issuanceLicense, callerState, callerCallback)
		{
			RmsClientManagerUtils.ThrowOnNullOrEmptyArrayArgument("identities", identities);
			this.identities = identities;
			this.responses = responses;
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x060069D0 RID: 27088 RVA: 0x001C5350 File Offset: 0x001C3550
		// (set) Token: 0x060069D1 RID: 27089 RVA: 0x001C5358 File Offset: 0x001C3558
		public ServerLicenseWCFManager Manager
		{
			get
			{
				return this.manager;
			}
			set
			{
				this.manager = value;
			}
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x060069D2 RID: 27090 RVA: 0x001C5361 File Offset: 0x001C3561
		// (set) Token: 0x060069D3 RID: 27091 RVA: 0x001C5369 File Offset: 0x001C3569
		public ExternalRMSServerInfo ServerInfo
		{
			get
			{
				return this.serverInfo;
			}
			set
			{
				this.serverInfo = value;
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x060069D4 RID: 27092 RVA: 0x001C5372 File Offset: 0x001C3572
		// (set) Token: 0x060069D5 RID: 27093 RVA: 0x001C537A File Offset: 0x001C357A
		public XrmlCertificateChain Rac
		{
			get
			{
				return this.rac;
			}
			set
			{
				this.rac = value;
			}
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x060069D6 RID: 27094 RVA: 0x001C5383 File Offset: 0x001C3583
		public LicenseIdentity[] Identities
		{
			get
			{
				return this.identities;
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x060069D7 RID: 27095 RVA: 0x001C538B File Offset: 0x001C358B
		// (set) Token: 0x060069D8 RID: 27096 RVA: 0x001C5393 File Offset: 0x001C3593
		public LicenseResponse[] Responses
		{
			get
			{
				return this.responses;
			}
			set
			{
				this.responses = value;
			}
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x001C539C File Offset: 0x001C359C
		public override void ReleaseWebManager()
		{
			if (this.manager != null)
			{
				this.manager.Dispose();
				this.manager = null;
			}
		}

		// Token: 0x04003C2C RID: 15404
		private readonly LicenseIdentity[] identities;

		// Token: 0x04003C2D RID: 15405
		private ServerLicenseWCFManager manager;

		// Token: 0x04003C2E RID: 15406
		private ExternalRMSServerInfo serverInfo;

		// Token: 0x04003C2F RID: 15407
		private XrmlCertificateChain rac;

		// Token: 0x04003C30 RID: 15408
		private LicenseResponse[] responses;
	}
}
