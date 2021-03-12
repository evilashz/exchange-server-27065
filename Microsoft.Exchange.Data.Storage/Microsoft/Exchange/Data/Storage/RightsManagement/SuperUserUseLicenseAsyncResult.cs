using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B6A RID: 2922
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SuperUserUseLicenseAsyncResult : UseLicenseAsyncResult
	{
		// Token: 0x060069C9 RID: 27081 RVA: 0x001C52D8 File Offset: 0x001C34D8
		public SuperUserUseLicenseAsyncResult(RmsClientManagerContext context, Uri licenseUri, XmlNode[] issuanceLicense, object callerState, AsyncCallback callerCallback) : base(context, licenseUri, issuanceLicense, callerState, callerCallback)
		{
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x001C52E7 File Offset: 0x001C34E7
		// (set) Token: 0x060069CB RID: 27083 RVA: 0x001C52EF File Offset: 0x001C34EF
		public LicenseWSManager Manager
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

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x001C52F8 File Offset: 0x001C34F8
		// (set) Token: 0x060069CD RID: 27085 RVA: 0x001C5300 File Offset: 0x001C3500
		public string UseLicense
		{
			get
			{
				return this.useLicense;
			}
			set
			{
				this.useLicense = value;
			}
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x001C5309 File Offset: 0x001C3509
		public override void ReleaseWebManager()
		{
			if (this.manager != null)
			{
				this.manager.Dispose();
				this.manager = null;
			}
		}

		// Token: 0x04003C2A RID: 15402
		private LicenseWSManager manager;

		// Token: 0x04003C2B RID: 15403
		private string useLicense;
	}
}
