using System;
using Microsoft.Exchange.Autodiscover.WCF;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000040 RID: 64
	public class DocumentSharingLocation
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x000087C0 File Offset: 0x000069C0
		public DocumentSharingLocation(string serviceUrl, string locationUrl, string displayName, FileExtensionCollection supportedFileExtensions, bool externalAccessAllowed, bool anonymousAccessAllowed, bool canModifyPermissions, bool isDefault)
		{
			Common.ThrowIfNullOrEmpty(serviceUrl, "serviceUrl");
			Common.ThrowIfNullOrEmpty(locationUrl, "locationUrl");
			Common.ThrowIfNullOrEmpty(displayName, "displayName");
			if (supportedFileExtensions == null)
			{
				throw new ArgumentNullException("supportedFileExtensions");
			}
			if (supportedFileExtensions.Count == 0)
			{
				throw new ArgumentException("supportedFileExtensions must not be empty.");
			}
			this.serviceUrl = serviceUrl;
			this.locationUrl = locationUrl;
			this.displayName = displayName;
			this.supportedFileExtensions = supportedFileExtensions;
			this.externalAccessAllowed = externalAccessAllowed;
			this.anonymousAccessAllowed = anonymousAccessAllowed;
			this.canModifyPermissions = canModifyPermissions;
			this.isDefault = isDefault;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008854 File Offset: 0x00006A54
		public string ServiceUrl
		{
			get
			{
				return this.serviceUrl;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000885C File Offset: 0x00006A5C
		public string LocationUrl
		{
			get
			{
				return this.locationUrl;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008864 File Offset: 0x00006A64
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000886C File Offset: 0x00006A6C
		public FileExtensionCollection SupportedFileExtensions
		{
			get
			{
				return this.supportedFileExtensions;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008874 File Offset: 0x00006A74
		public bool ExternalAccessAllowed
		{
			get
			{
				return this.externalAccessAllowed;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000887C File Offset: 0x00006A7C
		public bool AnonymousAccessAllowed
		{
			get
			{
				return this.anonymousAccessAllowed;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008884 File Offset: 0x00006A84
		public bool CanModifyPermissions
		{
			get
			{
				return this.canModifyPermissions;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000888C File Offset: 0x00006A8C
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
		}

		// Token: 0x0400019F RID: 415
		private string serviceUrl;

		// Token: 0x040001A0 RID: 416
		private string locationUrl;

		// Token: 0x040001A1 RID: 417
		private string displayName;

		// Token: 0x040001A2 RID: 418
		private FileExtensionCollection supportedFileExtensions;

		// Token: 0x040001A3 RID: 419
		private bool externalAccessAllowed;

		// Token: 0x040001A4 RID: 420
		private bool anonymousAccessAllowed;

		// Token: 0x040001A5 RID: 421
		private bool canModifyPermissions;

		// Token: 0x040001A6 RID: 422
		private bool isDefault;
	}
}
