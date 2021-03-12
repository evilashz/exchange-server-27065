using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200008A RID: 138
	public abstract class RemoveMailUserBase<TIdentity> : RemoveRecipientObjectTask<TIdentity, ADUser> where TIdentity : MailUserIdParameterBase
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000285CB File Offset: 0x000267CB
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x000285F1 File Offset: 0x000267F1
		[Parameter(Mandatory = false)]
		public SwitchParameter KeepWindowsLiveID
		{
			get
			{
				return (SwitchParameter)(base.Fields["KeepWindowsLiveID"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["KeepWindowsLiveID"] = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00028609 File Offset: 0x00026809
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0002862F File Offset: 0x0002682F
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreLegalHold
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreLegalHold"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreLegalHold"] = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00028648 File Offset: 0x00026848
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (!(base.DataObject.NetID != null))
				{
					TIdentity identity = this.Identity;
					return Strings.ConfirmationMessageRemoveMailUser(identity.ToString());
				}
				if (this.KeepWindowsLiveID)
				{
					TIdentity identity2 = this.Identity;
					return Strings.ConfirmationMessageRemoveMailuserAndNotLiveId(identity2.ToString(), base.DataObject.WindowsLiveID.ToString());
				}
				TIdentity identity3 = this.Identity;
				return Strings.ConfirmationMessageRemoveMailuserAndLiveId(identity3.ToString(), base.DataObject.WindowsLiveID.ToString());
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000286F5 File Offset: 0x000268F5
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00028704 File Offset: 0x00026904
		protected override void InternalValidate()
		{
			this.latencyContext = ProvisioningPerformanceHelper.StartLatencyDetection(this);
			base.InternalValidate();
			MailboxTaskHelper.BlockRemoveOrDisableIfLitigationHoldEnabled(base.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), false, this.IgnoreLegalHold.ToBool());
			MailboxTaskHelper.BlockRemoveOrDisableIfDiscoveryHoldEnabled(base.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), false, this.IgnoreLegalHold.ToBool());
			if (ComplianceConfigImpl.JournalArchivingHardeningEnabled)
			{
				MailboxTaskHelper.BlockRemoveOrDisableMailUserIfJournalArchiveEnabled(base.DataSession as IRecipientSession, this.ConfigurationSession, base.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), false, this.isSyncOperation);
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000287A8 File Offset: 0x000269A8
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			finally
			{
				ProvisioningPerformanceHelper.StopLatencyDetection(this.latencyContext);
			}
		}

		// Token: 0x040001F0 RID: 496
		private const string ParameterKeepWindowsLiveID = "KeepWindowsLiveID";

		// Token: 0x040001F1 RID: 497
		private LatencyDetectionContext latencyContext;

		// Token: 0x040001F2 RID: 498
		protected bool isSyncOperation;
	}
}
