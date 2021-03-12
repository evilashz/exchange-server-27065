using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B65 RID: 2917
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FederationRacAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x060069A9 RID: 27049 RVA: 0x001C5113 File Offset: 0x001C3313
		public FederationRacAsyncResult(RmsClientManagerContext context, Uri licenseUri, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			this.licenseUri = licenseUri;
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x060069AA RID: 27050 RVA: 0x001C5126 File Offset: 0x001C3326
		// (set) Token: 0x060069AB RID: 27051 RVA: 0x001C512E File Offset: 0x001C332E
		public Uri LicenseUri
		{
			get
			{
				return this.licenseUri;
			}
			set
			{
				this.licenseUri = value;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x060069AC RID: 27052 RVA: 0x001C5137 File Offset: 0x001C3337
		// (set) Token: 0x060069AD RID: 27053 RVA: 0x001C513F File Offset: 0x001C333F
		public ServerCertificationWCFManager Manager
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

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x060069AE RID: 27054 RVA: 0x001C5148 File Offset: 0x001C3348
		// (set) Token: 0x060069AF RID: 27055 RVA: 0x001C5150 File Offset: 0x001C3350
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

		// Token: 0x060069B0 RID: 27056 RVA: 0x001C5159 File Offset: 0x001C3359
		public void ReleaseWebManager()
		{
			if (this.manager != null)
			{
				this.manager.Dispose();
				this.manager = null;
			}
		}

		// Token: 0x04003C1E RID: 15390
		private Uri licenseUri;

		// Token: 0x04003C1F RID: 15391
		private ServerCertificationWCFManager manager;

		// Token: 0x04003C20 RID: 15392
		private ExternalRMSServerInfo serverInfo;
	}
}
