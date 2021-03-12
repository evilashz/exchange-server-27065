using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000CF RID: 207
	[Cmdlet("Remove", "SyncMailUser", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSyncMailUser : RemoveMailUserBase<MailUserIdParameter>
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0003ABE5 File Offset: 0x00038DE5
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x0003ABED File Offset: 0x00038DED
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

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0003ABF6 File Offset: 0x00038DF6
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x0003ABFE File Offset: 0x00038DFE
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter DisableWindowsLiveID { get; set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0003AC07 File Offset: 0x00038E07
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x0003AC28 File Offset: 0x00038E28
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

		// Token: 0x06001017 RID: 4119 RVA: 0x0003AC40 File Offset: 0x00038E40
		protected override bool ShouldSoftDeleteObject()
		{
			ADRecipient dataObject = base.DataObject;
			return dataObject != null && !(dataObject.OrganizationId == null) && dataObject.OrganizationId.ConfigurationUnit != null && !this.Permanent && Globals.IsMicrosoftHostedOnly && SoftDeletedTaskHelper.MSOSyncEnabled(this.ConfigurationSession, dataObject.OrganizationId);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003AC94 File Offset: 0x00038E94
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.Permanent || this.ForReconciliation)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, null);
			}
			return recipientSession;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0003ACCB File Offset: 0x00038ECB
		protected override void InternalValidate()
		{
			this.isSyncOperation = true;
			base.InternalValidate();
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0003ACDC File Offset: 0x00038EDC
		protected override void InternalProcessRecord()
		{
			if (Globals.IsMicrosoftHostedOnly)
			{
				ADUser dataObject = base.DataObject;
				if (this.ShouldSoftDeleteObject())
				{
					SoftDeletedTaskHelper.UpdateRecipientForSoftDelete(base.DataSession as IRecipientSession, dataObject, false);
					SoftDeletedTaskHelper.UpdateExchangeGuidForMailEnabledUser(dataObject);
				}
				else
				{
					dataObject.RecipientSoftDeletedStatus = 0;
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0003AD26 File Offset: 0x00038F26
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x040002E1 RID: 737
		private const string ParameterPermanent = "Permanent";
	}
}
