using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B66 RID: 2918
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PreLicenseAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x060069B1 RID: 27057 RVA: 0x001C5175 File Offset: 0x001C3375
		// (set) Token: 0x060069B2 RID: 27058 RVA: 0x001C517D File Offset: 0x001C337D
		public LicenseResponse[] Responses { get; set; }

		// Token: 0x060069B3 RID: 27059 RVA: 0x001C5186 File Offset: 0x001C3386
		public PreLicenseAsyncResult(RmsClientManagerContext context, PreLicenseWSManager preLicenseManager, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			this.preLicenseManager = preLicenseManager;
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x060069B4 RID: 27060 RVA: 0x001C5199 File Offset: 0x001C3399
		// (set) Token: 0x060069B5 RID: 27061 RVA: 0x001C51A1 File Offset: 0x001C33A1
		public PreLicenseWSManager PreLicenseManager
		{
			get
			{
				return this.preLicenseManager;
			}
			set
			{
				this.preLicenseManager = value;
			}
		}

		// Token: 0x060069B6 RID: 27062 RVA: 0x001C51AA File Offset: 0x001C33AA
		public void ReleaseManagers()
		{
			if (this.preLicenseManager != null)
			{
				this.preLicenseManager.Dispose();
				this.preLicenseManager = null;
			}
		}

		// Token: 0x04003C21 RID: 15393
		private PreLicenseWSManager preLicenseManager;
	}
}
