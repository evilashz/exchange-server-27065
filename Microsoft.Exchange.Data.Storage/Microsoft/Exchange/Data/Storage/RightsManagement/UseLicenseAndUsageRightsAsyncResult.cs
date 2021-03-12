using System;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B6C RID: 2924
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class UseLicenseAndUsageRightsAsyncResult : RightsManagementAsyncResult
	{
		// Token: 0x060069DA RID: 27098 RVA: 0x001C53B8 File Offset: 0x001C35B8
		internal UseLicenseAndUsageRightsAsyncResult(RmsClientManagerContext context, Uri licensingUri, string publishLicense, XmlNode[] publishLicenseAsXmlNodes, string userIdentity, SecurityIdentifier userSid, bool isSuperUser, object callerState, AsyncCallback callerCallback) : base(context, callerState, callerCallback)
		{
			ArgumentValidator.ThrowIfNull("licensingUri", licensingUri);
			ArgumentValidator.ThrowIfNullOrEmpty("publishLicense", publishLicense);
			ArgumentValidator.ThrowIfNull("publishLicenseAsXmlNodes", publishLicenseAsXmlNodes);
			ArgumentValidator.ThrowIfNull("userIdentity", userIdentity);
			ArgumentValidator.ThrowIfNull("userSid", userSid);
			this.licensingUri = licensingUri;
			this.publishLicense = publishLicense;
			this.publishLicenseAsXmlNodes = publishLicenseAsXmlNodes;
			this.userIdentity = userIdentity;
			this.userSid = userSid;
			this.isSuperUser = isSuperUser;
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x060069DB RID: 27099 RVA: 0x001C5438 File Offset: 0x001C3638
		internal string PublishLicense
		{
			get
			{
				return this.publishLicense;
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x060069DC RID: 27100 RVA: 0x001C5440 File Offset: 0x001C3640
		internal XmlNode[] PublishLicenseAsXmlNodes
		{
			get
			{
				return this.publishLicenseAsXmlNodes;
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x060069DD RID: 27101 RVA: 0x001C5448 File Offset: 0x001C3648
		internal Uri LicensingUri
		{
			get
			{
				return this.licensingUri;
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x060069DE RID: 27102 RVA: 0x001C5450 File Offset: 0x001C3650
		internal string UserIdentity
		{
			get
			{
				return this.userIdentity;
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x060069DF RID: 27103 RVA: 0x001C5458 File Offset: 0x001C3658
		internal bool IsSuperUser
		{
			get
			{
				return this.isSuperUser;
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x060069E0 RID: 27104 RVA: 0x001C5460 File Offset: 0x001C3660
		internal SecurityIdentifier UserSid
		{
			get
			{
				return this.userSid;
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x060069E1 RID: 27105 RVA: 0x001C5468 File Offset: 0x001C3668
		// (set) Token: 0x060069E2 RID: 27106 RVA: 0x001C5470 File Offset: 0x001C3670
		internal string UseLicense
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

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x060069E3 RID: 27107 RVA: 0x001C5479 File Offset: 0x001C3679
		// (set) Token: 0x060069E4 RID: 27108 RVA: 0x001C5481 File Offset: 0x001C3681
		internal ContentRight UsageRights
		{
			get
			{
				return this.usageRights;
			}
			set
			{
				this.usageRights = value;
			}
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x060069E5 RID: 27109 RVA: 0x001C548A File Offset: 0x001C368A
		// (set) Token: 0x060069E6 RID: 27110 RVA: 0x001C5492 File Offset: 0x001C3692
		internal ExDateTime ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
			set
			{
				this.expiryTime = value;
			}
		}

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x060069E7 RID: 27111 RVA: 0x001C549B File Offset: 0x001C369B
		// (set) Token: 0x060069E8 RID: 27112 RVA: 0x001C54A3 File Offset: 0x001C36A3
		internal byte[] DRMPropsSignature
		{
			get
			{
				return this.drmPropsSignature;
			}
			set
			{
				this.drmPropsSignature = value;
			}
		}

		// Token: 0x04003C31 RID: 15409
		private readonly string publishLicense;

		// Token: 0x04003C32 RID: 15410
		private readonly XmlNode[] publishLicenseAsXmlNodes;

		// Token: 0x04003C33 RID: 15411
		private readonly Uri licensingUri;

		// Token: 0x04003C34 RID: 15412
		private readonly string userIdentity;

		// Token: 0x04003C35 RID: 15413
		private readonly SecurityIdentifier userSid;

		// Token: 0x04003C36 RID: 15414
		private readonly bool isSuperUser;

		// Token: 0x04003C37 RID: 15415
		private string useLicense;

		// Token: 0x04003C38 RID: 15416
		private ContentRight usageRights;

		// Token: 0x04003C39 RID: 15417
		private ExDateTime expiryTime;

		// Token: 0x04003C3A RID: 15418
		private byte[] drmPropsSignature;
	}
}
