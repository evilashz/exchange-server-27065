using System;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B69 RID: 2921
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UseLicenseAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x060069C4 RID: 27076 RVA: 0x001C5282 File Offset: 0x001C3482
		public UseLicenseAsyncResult(RmsClientManagerContext context, Uri licenseUri, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			this.licenseUri = licenseUri;
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x001C5295 File Offset: 0x001C3495
		public UseLicenseAsyncResult(RmsClientManagerContext context, Uri licenseUri, XmlNode[] issuanceLicense, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			ArgumentValidator.ThrowIfNull("licenseUri", licenseUri);
			RmsClientManagerUtils.ThrowOnNullOrEmptyArrayArgument("issuanceLicense", issuanceLicense);
			this.licenseUri = licenseUri;
			this.issuanceLicense = issuanceLicense;
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x060069C6 RID: 27078 RVA: 0x001C52C6 File Offset: 0x001C34C6
		public Uri LicenseUri
		{
			get
			{
				return this.licenseUri;
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x060069C7 RID: 27079 RVA: 0x001C52CE File Offset: 0x001C34CE
		public XmlNode[] IssuanceLicense
		{
			get
			{
				return this.issuanceLicense;
			}
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x001C52D6 File Offset: 0x001C34D6
		public virtual void ReleaseWebManager()
		{
		}

		// Token: 0x04003C28 RID: 15400
		private readonly Uri licenseUri;

		// Token: 0x04003C29 RID: 15401
		private readonly XmlNode[] issuanceLicense;
	}
}
