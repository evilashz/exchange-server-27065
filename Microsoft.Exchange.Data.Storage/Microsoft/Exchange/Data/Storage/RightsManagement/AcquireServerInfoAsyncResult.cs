using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.SOAP.Server;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B64 RID: 2916
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AcquireServerInfoAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x06006999 RID: 27033 RVA: 0x001C503C File Offset: 0x001C323C
		public AcquireServerInfoAsyncResult(RmsClientManagerContext context, Uri licenseUri, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			ArgumentValidator.ThrowIfNull("licenseUri", licenseUri);
			this.licenseUri = licenseUri;
			this.serverInfo = new ExternalRMSServerInfo(licenseUri);
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x0600699A RID: 27034 RVA: 0x001C5066 File Offset: 0x001C3266
		// (set) Token: 0x0600699B RID: 27035 RVA: 0x001C506E File Offset: 0x001C326E
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

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x0600699C RID: 27036 RVA: 0x001C5077 File Offset: 0x001C3277
		// (set) Token: 0x0600699D RID: 27037 RVA: 0x001C507F File Offset: 0x001C327F
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

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x0600699E RID: 27038 RVA: 0x001C5088 File Offset: 0x001C3288
		// (set) Token: 0x0600699F RID: 27039 RVA: 0x001C5090 File Offset: 0x001C3290
		public ServiceLocationResponse[] ServiceLocationResponses
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

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060069A0 RID: 27040 RVA: 0x001C5099 File Offset: 0x001C3299
		// (set) Token: 0x060069A1 RID: 27041 RVA: 0x001C50A1 File Offset: 0x001C32A1
		public ServerWSManager ServerWSManager
		{
			get
			{
				return this.serverWSManager;
			}
			set
			{
				this.serverWSManager = value;
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x060069A2 RID: 27042 RVA: 0x001C50AA File Offset: 0x001C32AA
		// (set) Token: 0x060069A3 RID: 27043 RVA: 0x001C50B2 File Offset: 0x001C32B2
		public HttpClient HttpClient
		{
			get
			{
				return this.httpClient;
			}
			set
			{
				this.httpClient = value;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x060069A4 RID: 27044 RVA: 0x001C50BB File Offset: 0x001C32BB
		// (set) Token: 0x060069A5 RID: 27045 RVA: 0x001C50C3 File Offset: 0x001C32C3
		public Uri ServerLicensingMExUri
		{
			get
			{
				return this.serverLicensingMExUri;
			}
			set
			{
				this.serverLicensingMExUri = value;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x060069A6 RID: 27046 RVA: 0x001C50CC File Offset: 0x001C32CC
		// (set) Token: 0x060069A7 RID: 27047 RVA: 0x001C50D4 File Offset: 0x001C32D4
		public Uri CertificationMExUri
		{
			get
			{
				return this.certificationMExUri;
			}
			set
			{
				this.certificationMExUri = value;
			}
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x001C50DD File Offset: 0x001C32DD
		public void Release()
		{
			if (this.ServerWSManager != null)
			{
				this.ServerWSManager.Dispose();
				this.ServerWSManager = null;
			}
			if (this.httpClient != null)
			{
				this.httpClient.Dispose();
				this.httpClient = null;
			}
		}

		// Token: 0x04003C17 RID: 15383
		private ServerWSManager serverWSManager;

		// Token: 0x04003C18 RID: 15384
		private Uri licenseUri;

		// Token: 0x04003C19 RID: 15385
		private ServiceLocationResponse[] responses;

		// Token: 0x04003C1A RID: 15386
		private HttpClient httpClient;

		// Token: 0x04003C1B RID: 15387
		private ExternalRMSServerInfo serverInfo;

		// Token: 0x04003C1C RID: 15388
		private Uri serverLicensingMExUri;

		// Token: 0x04003C1D RID: 15389
		private Uri certificationMExUri;
	}
}
