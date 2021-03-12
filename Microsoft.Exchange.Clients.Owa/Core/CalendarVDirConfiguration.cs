using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200029D RID: 669
	public sealed class CalendarVDirConfiguration : Configuration
	{
		// Token: 0x060019DE RID: 6622 RVA: 0x00095E6C File Offset: 0x0009406C
		internal CalendarVDirConfiguration()
		{
			base.PhoneticSupportEnabled = true;
			this.formsAuthenticationEnabled = 0;
			AttachmentPolicy.Level treatUnknownTypeAs = AttachmentPolicy.Level.Block;
			AttachmentPolicy attachmentPolicy = new AttachmentPolicy(new string[0], new string[0], new string[0], new string[0], new string[0], new string[0], treatUnknownTypeAs, false, false, false, false, false, false, new string[0], new string[0], new string[0], new string[0], false);
			base.AttachmentPolicy = attachmentPolicy;
			base.DefaultClientLanguage = Globals.ServerCulture.LCID;
			this.filterWebBeaconsAndHtmlForms = WebBeaconFilterLevels.ForceFilter;
			base.LogonAndErrorLanguage = Globals.ServerCulture.LCID;
			this.logonFormat = LogonFormats.FullDomain;
			this.defaultDomain = string.Empty;
			this.notificationInterval = -1;
			this.sessionTimeout = -1;
			this.redirectToOptimalOWAServer = true;
			base.DefaultTheme = string.Empty;
			this.clientAuthCleanupLevel = ClientAuthCleanupLevels.High;
			this.isSMimeEnabledOnCurrentServerr = false;
			this.documentAccessAllowedServers = new string[0];
			this.documentAccessBlockedServers = new string[0];
			this.documentAccessInternalDomainSuffixList = new string[0];
			RemoteDocumentsActions? remoteDocumentsActions = new RemoteDocumentsActions?(RemoteDocumentsActions.Block);
			if (remoteDocumentsActions != null)
			{
				if (remoteDocumentsActions == RemoteDocumentsActions.Allow)
				{
					this.remoteDocumentsActionForUnknownServers = RemoteDocumentsActions.Allow;
				}
				else
				{
					this.remoteDocumentsActionForUnknownServers = RemoteDocumentsActions.Block;
				}
			}
			base.InternalAuthenticationMethod = AuthenticationMethod.None;
			base.ExternalAuthenticationMethod = AuthenticationMethod.None;
			base.Exchange2003Url = null;
			base.LegacyRedirectType = LegacyRedirectTypeOptions.Manual;
			base.SegmentationFlags = 536871426UL;
			base.InstantMessagingType = InstantMessagingTypeOptions.None;
			this.defaultAcceptedDomain = null;
			this.publicFoldersEnabledOnThisVdir = false;
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x00095FE0 File Offset: 0x000941E0
		internal override AcceptedDomain DefaultAcceptedDomain
		{
			get
			{
				throw new OwaNotSupportedException("Default accepted domain is not supported for Calendar anonymous vdir configuration object.");
			}
		}
	}
}
