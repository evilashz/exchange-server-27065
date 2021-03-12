using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B15 RID: 2837
	[Cmdlet("New", "RemoteAccountPolicy", SupportsShouldProcess = true)]
	public sealed class NewRemoteAccountPolicy : NewMultitenancySystemConfigurationObjectTask<RemoteAccountPolicy>
	{
		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x001A4A69 File Offset: 0x001A2C69
		// (set) Token: 0x060064BF RID: 25791 RVA: 0x001A4A76 File Offset: 0x001A2C76
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan PollingInterval
		{
			get
			{
				return this.DataObject.PollingInterval;
			}
			set
			{
				this.DataObject.PollingInterval = value;
			}
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x001A4A84 File Offset: 0x001A2C84
		// (set) Token: 0x060064C1 RID: 25793 RVA: 0x001A4A91 File Offset: 0x001A2C91
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TimeBeforeInactive
		{
			get
			{
				return this.DataObject.TimeBeforeInactive;
			}
			set
			{
				this.DataObject.TimeBeforeInactive = value;
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x001A4A9F File Offset: 0x001A2C9F
		// (set) Token: 0x060064C3 RID: 25795 RVA: 0x001A4AAC File Offset: 0x001A2CAC
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TimeBeforeDormant
		{
			get
			{
				return this.DataObject.TimeBeforeDormant;
			}
			set
			{
				this.DataObject.TimeBeforeDormant = value;
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x001A4ABA File Offset: 0x001A2CBA
		// (set) Token: 0x060064C5 RID: 25797 RVA: 0x001A4AC7 File Offset: 0x001A2CC7
		[Parameter(Mandatory = false)]
		public int MaxSyncAccounts
		{
			get
			{
				return this.DataObject.MaxSyncAccounts;
			}
			set
			{
				this.DataObject.MaxSyncAccounts = value;
			}
		}

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x060064C6 RID: 25798 RVA: 0x001A4AD5 File Offset: 0x001A2CD5
		// (set) Token: 0x060064C7 RID: 25799 RVA: 0x001A4AE2 File Offset: 0x001A2CE2
		[Parameter(Mandatory = false)]
		public bool SyncNowAllowed
		{
			get
			{
				return this.DataObject.SyncNowAllowed;
			}
			set
			{
				this.DataObject.SyncNowAllowed = value;
			}
		}

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x060064C8 RID: 25800 RVA: 0x001A4AF0 File Offset: 0x001A2CF0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRemoteAccountPolicy(base.Name);
			}
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x001A4B00 File Offset: 0x001A2D00
		protected override IConfigurable PrepareDataObject()
		{
			RemoteAccountPolicy remoteAccountPolicy = (RemoteAccountPolicy)base.PrepareDataObject();
			remoteAccountPolicy.SetId((IConfigurationSession)base.DataSession, base.Name);
			return remoteAccountPolicy;
		}
	}
}
