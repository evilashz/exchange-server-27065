using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000D9 RID: 217
	[Cmdlet("Undo", "SyncSoftDeletedUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class UndoSyncSoftDeletedUser : NewGeneralRecipientObjectTask<ADUser>
	{
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0003C927 File Offset: 0x0003AB27
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRecoveringSoftDeletedObject(this.SoftDeletedObject.ToString());
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0003C939 File Offset: 0x0003AB39
		// (set) Token: 0x060010C7 RID: 4295 RVA: 0x0003C941 File Offset: 0x0003AB41
		[Parameter(Mandatory = false)]
		public WindowsLiveId WindowsLiveID { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x0003C94A File Offset: 0x0003AB4A
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x0003C957 File Offset: 0x0003AB57
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
		public new NonMailEnabledUserIdParameter SoftDeletedObject
		{
			get
			{
				return (NonMailEnabledUserIdParameter)base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x0003C960 File Offset: 0x0003AB60
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x0003C968 File Offset: 0x0003AB68
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0003C971 File Offset: 0x0003AB71
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x0003C988 File Offset: 0x0003AB88
		[Parameter(Mandatory = false)]
		public SecureString Password
		{
			get
			{
				return (SecureString)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0003C99B File Offset: 0x0003AB9B
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x0003C9C1 File Offset: 0x0003ABC1
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BypassLiveId"] = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x0003C9D9 File Offset: 0x0003ABD9
		protected override bool EnforceExchangeObjectVersion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003C9DC File Offset: 0x0003ABDC
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			string name = this.DataObject.Name;
			string displayName = this.DataObject.DisplayName;
			this.DataObject = SoftDeletedTaskHelper.GetSoftDeletedADUser(base.CurrentOrganizationId, this.SoftDeletedObject, new Task.ErrorLoggerDelegate(base.WriteError));
			this.previousExchangeVersion = this.DataObject.ExchangeVersion;
			this.DataObject.SetExchangeVersion(ADRecipientSchema.WhenSoftDeleted.VersionAdded);
			if (this.WindowsLiveID != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty && this.WindowsLiveID.SmtpAddress != this.DataObject.WindowsLiveID)
			{
				this.DataObject.EmailAddressPolicyEnabled = false;
				this.DataObject.WindowsLiveID = this.WindowsLiveID.SmtpAddress;
				this.DataObject.UserPrincipalName = this.WindowsLiveID.SmtpAddress.ToString();
				this.DataObject.PrimarySmtpAddress = this.WindowsLiveID.SmtpAddress;
			}
			if (!string.IsNullOrEmpty(name))
			{
				this.DataObject.Name = name;
			}
			this.DataObject.Name = SoftDeletedTaskHelper.GetUniqueNameForRecovery((IRecipientSession)base.DataSession, this.DataObject.Name, this.DataObject.Id);
			if (!string.IsNullOrEmpty(displayName))
			{
				this.DataObject.DisplayName = displayName;
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003CB44 File Offset: 0x0003AD44
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			if (this.WindowsLiveID != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
			{
				MailboxTaskHelper.IsLiveIdExists((IRecipientSession)base.DataSession, user.WindowsLiveID, user.NetID, new Task.ErrorLoggerDelegate(base.WriteError));
				user.UserPrincipalName = user.WindowsLiveID.ToString();
			}
			if (!user.IsModified(ADRecipientSchema.DisplayName))
			{
				user.DisplayName = user.Name;
			}
			SoftDeletedTaskHelper.UpdateShadowWhenSoftDeletedProperty((IRecipientSession)base.DataSession, this.ConfigurationSession, base.CurrentOrganizationId, this.DataObject);
			this.DataObject.RecipientSoftDeletedStatus = 0;
			this.DataObject.WhenSoftDeleted = null;
			this.DataObject.InternalOnly = false;
			this.DataObject.propertyBag.MarkAsChanged(ADRecipientSchema.RecipientSoftDeletedStatus);
			this.DataObject.propertyBag.MarkAsChanged(ADRecipientSchema.WhenSoftDeleted);
			this.DataObject.propertyBag.MarkAsChanged(ADRecipientSchema.TransportSettingFlags);
			TaskLogger.LogExit();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003CC69 File Offset: 0x0003AE69
		internal override void PreInternalProcessRecord()
		{
			if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.PreInternalProcessRecord(this, this.ConvertDataObjectToPresentationObject(this.DataObject), false);
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003CC87 File Offset: 0x0003AE87
		protected override void InternalProcessRecord()
		{
			this.DataObject.SetExchangeVersion(this.previousExchangeVersion);
			base.InternalProcessRecord();
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003CCA0 File Offset: 0x0003AEA0
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0003CCAD File Offset: 0x0003AEAD
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0003CCB5 File Offset: 0x0003AEB5
		private new string ExternalDirectoryObjectId
		{
			get
			{
				return base.ExternalDirectoryObjectId;
			}
		}

		// Token: 0x040002FC RID: 764
		private ExchangeObjectVersion previousExchangeVersion;
	}
}
