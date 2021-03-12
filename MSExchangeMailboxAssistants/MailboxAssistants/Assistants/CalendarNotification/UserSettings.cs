using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000FA RID: 250
	internal class UserSettings
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00044245 File Offset: 0x00042445
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0004424D File Offset: 0x0004244D
		public string LegacyDN { get; private set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00044256 File Offset: 0x00042456
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x0004425E File Offset: 0x0004245E
		public Guid ExternalDirectoryOrganizationId { get; private set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00044267 File Offset: 0x00042467
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x0004426F File Offset: 0x0004246F
		public ExTimeZoneValue TimeZone { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00044278 File Offset: 0x00042478
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00044280 File Offset: 0x00042480
		public TextNotificationSettings Text { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00044289 File Offset: 0x00042489
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00044291 File Offset: 0x00042491
		public VoiceNotificationSettings Voice { get; set; }

		// Token: 0x06000A5C RID: 2652 RVA: 0x0004429A File Offset: 0x0004249A
		public UserSettings(string legacyDN, Guid externalDirectoryOrganizationId)
		{
			this.LegacyDN = legacyDN;
			this.ExternalDirectoryOrganizationId = externalDirectoryOrganizationId;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000442B0 File Offset: 0x000424B0
		public UserSettings(MailboxSession session)
		{
			this.LegacyDN = session.MailboxOwnerLegacyDN;
			this.ExternalDirectoryOrganizationId = UserSettings.GetExternalDirectoryOrganizationId(session);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000442D0 File Offset: 0x000424D0
		public UserSettings(InfoFromUserMailboxSession info)
		{
			this.LegacyDN = info.UserLegacyDN;
			this.ExternalDirectoryOrganizationId = info.ExternalDirectoryOrganizationId;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000442F4 File Offset: 0x000424F4
		public ADSessionSettings GetADSettings()
		{
			if (!this.ExternalDirectoryOrganizationId.Equals(Guid.Empty))
			{
				return ADSessionSettings.FromExternalDirectoryOrganizationId(this.ExternalDirectoryOrganizationId);
			}
			return ADSessionSettings.FromRootOrgScopeSet();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00044328 File Offset: 0x00042528
		internal static Guid GetExternalDirectoryOrganizationId(MailboxSession session)
		{
			if (session.MailboxOwner.MailboxInfo.OrganizationId == null || OrganizationId.ForestWideOrgId.Equals(session.MailboxOwner.MailboxInfo.OrganizationId))
			{
				return Guid.Empty;
			}
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(session.MailboxOwner.MailboxInfo.OrganizationId);
			return iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId();
		}
	}
}
