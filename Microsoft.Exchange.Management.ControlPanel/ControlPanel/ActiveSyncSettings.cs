using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002FE RID: 766
	[DataContract]
	public class ActiveSyncSettings : BaseRow
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x0008C479 File Offset: 0x0008A679
		public ActiveSyncSettings(ActiveSyncOrganizationSettings settings) : base(settings)
		{
			this.settings = settings;
			if (settings.AdminMailRecipients == null)
			{
				this.recipients = null;
				return;
			}
			this.recipients = RecipientObjectResolver.Instance.ResolveSmtpAddress(settings.AdminMailRecipients.ToArray());
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x0008C4B4 File Offset: 0x0008A6B4
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x0008C4C0 File Offset: 0x0008A6C0
		[DataMember]
		public string Caption
		{
			get
			{
				return Strings.EditActiveSyncSettingsCaption;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x0008C4C7 File Offset: 0x0008A6C7
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x0008C4DE File Offset: 0x0008A6DE
		[DataMember]
		public string DefaultAccessLevel
		{
			get
			{
				return this.settings.DefaultAccessLevel.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x0008C4E8 File Offset: 0x0008A6E8
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x0008C53B File Offset: 0x0008A73B
		[DataMember]
		public string DefaultAccessLevelDescription
		{
			get
			{
				switch (this.settings.DefaultAccessLevel)
				{
				case DeviceAccessLevel.Allow:
					return Strings.AccessLevelDescriptionAllow;
				case DeviceAccessLevel.Block:
					return Strings.AccessLevelDescriptionBlock;
				case DeviceAccessLevel.Quarantine:
					return Strings.AccessLevelDescriptionQuarantine;
				default:
					throw new NotSupportedException();
				}
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x0008C542 File Offset: 0x0008A742
		// (set) Token: 0x06002E1C RID: 11804 RVA: 0x0008C54A File Offset: 0x0008A74A
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> AdminMailRecipients
		{
			get
			{
				return this.recipients;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x0008C551 File Offset: 0x0008A751
		// (set) Token: 0x06002E1E RID: 11806 RVA: 0x0008C57D File Offset: 0x0008A77D
		[DataMember]
		public string AdminMailRecipientsDescription
		{
			get
			{
				if (this.recipients != null && this.recipients.Any<RecipientObjectResolverRow>())
				{
					return Strings.QNoteEmailsDescriptionYes;
				}
				return Strings.QNoteEmailsDescriptionNo;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x0008C584 File Offset: 0x0008A784
		// (set) Token: 0x06002E20 RID: 11808 RVA: 0x0008C591 File Offset: 0x0008A791
		[DataMember]
		public string UserMailInsert
		{
			get
			{
				return this.settings.UserMailInsert;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x0008C598 File Offset: 0x0008A798
		// (set) Token: 0x06002E22 RID: 11810 RVA: 0x0008C5C1 File Offset: 0x0008A7C1
		[DataMember]
		public string UserMailInsertDescription
		{
			get
			{
				if (string.IsNullOrEmpty(this.settings.UserMailInsert))
				{
					return Strings.EmailInsertDescriptionNo;
				}
				return Strings.EmailInsertDescriptionYes;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002279 RID: 8825
		private readonly ActiveSyncOrganizationSettings settings;

		// Token: 0x0400227A RID: 8826
		private readonly IEnumerable<RecipientObjectResolverRow> recipients;
	}
}
