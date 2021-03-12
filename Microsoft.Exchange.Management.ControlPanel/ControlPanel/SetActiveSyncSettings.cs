using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002FF RID: 767
	[DataContract]
	public class SetActiveSyncSettings : SetObjectProperties
	{
		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x0008C5C8 File Offset: 0x0008A7C8
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Set-ActiveSyncOrganizationSettings";
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x0008C5CF File Offset: 0x0008A7CF
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x0008C5D6 File Offset: 0x0008A7D6
		// (set) Token: 0x06002E26 RID: 11814 RVA: 0x0008C5E8 File Offset: 0x0008A7E8
		[DataMember]
		public string DefaultAccessLevel
		{
			get
			{
				return (string)base["DefaultAccessLevel"];
			}
			set
			{
				base["DefaultAccessLevel"] = value;
			}
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x0008C5F6 File Offset: 0x0008A7F6
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x0008C608 File Offset: 0x0008A808
		[DataMember]
		public string[] AdminMailRecipients
		{
			get
			{
				return (string[])base[ActiveSyncOrganizationSettingsSchema.AdminMailRecipients];
			}
			set
			{
				base[ActiveSyncOrganizationSettingsSchema.AdminMailRecipients] = value;
			}
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x0008C616 File Offset: 0x0008A816
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x0008C628 File Offset: 0x0008A828
		[DataMember]
		public string UserMailInsert
		{
			get
			{
				return (string)base[ActiveSyncOrganizationSettingsSchema.UserMailInsert];
			}
			set
			{
				base[ActiveSyncOrganizationSettingsSchema.UserMailInsert] = value;
			}
		}
	}
}
