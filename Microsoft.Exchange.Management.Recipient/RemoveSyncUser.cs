using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D8 RID: 216
	[Cmdlet("Remove", "SyncUser", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncUser : RemoveRecipientObjectTask<NonMailEnabledUserIdParameter, ADUser>
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003C78E File Offset: 0x0003A98E
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x0003C796 File Offset: 0x0003A996
		[Parameter(Mandatory = false)]
		public new SwitchParameter ForReconciliation
		{
			get
			{
				return base.ForReconciliation;
			}
			set
			{
				base.ForReconciliation = value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0003C79F File Offset: 0x0003A99F
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0003C7C0 File Offset: 0x0003A9C0
		[Parameter]
		public bool Permanent
		{
			get
			{
				return (bool)(base.Fields["Permanent"] ?? false);
			}
			set
			{
				base.Fields["Permanent"] = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0003C7D8 File Offset: 0x0003A9D8
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x0003C7FE File Offset: 0x0003A9FE
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

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003C816 File Offset: 0x0003AA16
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUser(this.Identity.ToString());
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003C828 File Offset: 0x0003AA28
		protected override bool ShouldSoftDeleteObject()
		{
			ADRecipient dataObject = base.DataObject;
			return dataObject != null && !(dataObject.OrganizationId == null) && dataObject.OrganizationId.ConfigurationUnit != null && !this.Permanent && Globals.IsMicrosoftHostedOnly && SoftDeletedTaskHelper.MSOSyncEnabled(this.ConfigurationSession, dataObject.OrganizationId);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003C87C File Offset: 0x0003AA7C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.Permanent || this.ForReconciliation)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, null);
			}
			return recipientSession;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003C8B4 File Offset: 0x0003AAB4
		protected override void InternalProcessRecord()
		{
			if (Globals.IsMicrosoftHostedOnly)
			{
				ADUser dataObject = base.DataObject;
				if (this.ShouldSoftDeleteObject())
				{
					SoftDeletedTaskHelper.UpdateRecipientForSoftDelete(base.DataSession as IRecipientSession, dataObject, this.ForReconciliation);
				}
				else
				{
					dataObject.propertyBag.SetField(ADRecipientSchema.RecipientSoftDeletedStatus, 0);
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003C912 File Offset: 0x0003AB12
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x040002FA RID: 762
		private const string ParameterPermanent = "Permanent";

		// Token: 0x040002FB RID: 763
		private const string ParameterKeepWindowsLiveID = "KeepWindowsLiveID";
	}
}
