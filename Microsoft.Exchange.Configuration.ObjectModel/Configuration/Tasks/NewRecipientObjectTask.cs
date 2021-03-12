using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000075 RID: 117
	public abstract class NewRecipientObjectTask<TDataObject> : NewGeneralRecipientObjectTask<TDataObject> where TDataObject : ADRecipient, new()
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00010C2C File Offset: 0x0000EE2C
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00010C50 File Offset: 0x0000EE50
		[Parameter]
		[ValidateNotNullOrEmpty]
		public string Alias
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.Alias;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.Alias = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00010C74 File Offset: 0x0000EE74
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x00010C98 File Offset: 0x0000EE98
		[Parameter]
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				TDataObject dataObject = this.DataObject;
				return dataObject.PrimarySmtpAddress;
			}
			set
			{
				TDataObject dataObject = this.DataObject;
				dataObject.PrimarySmtpAddress = value;
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010CBA File Offset: 0x0000EEBA
		internal sealed override void PreInternalProcessRecord()
		{
			if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.PreInternalProcessRecord(this, this.ConvertDataObjectToPresentationObject(this.DataObject), false);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00010CDD File Offset: 0x0000EEDD
		protected virtual bool ShouldCheckAcceptedDomains()
		{
			return true;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		protected virtual bool GetEmailAddressPolicyEnabledDefaultValue(IConfigurable dataObject)
		{
			TDataObject tdataObject = (TDataObject)((object)dataObject);
			return tdataObject.PrimarySmtpAddress == SmtpAddress.Empty;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00010D0C File Offset: 0x0000EF0C
		protected sealed override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			if (string.IsNullOrEmpty(tdataObject.Alias))
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "RecipientTaskHelper.GenerateUniqueAlias", LoggerHelper.CmdletPerfMonitors))
				{
					tdataObject.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, base.CurrentOrganizationId, base.Name, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			if (!this.GetEmailAddressPolicyEnabledDefaultValue(tdataObject))
			{
				tdataObject.EmailAddressPolicyEnabled = false;
			}
			if (string.IsNullOrEmpty(tdataObject.DisplayName))
			{
				tdataObject.DisplayName = tdataObject.Name;
			}
			if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.UpdateAffectedIConfigurable(this, this.ConvertDataObjectToPresentationObject(tdataObject), false);
			}
			else
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), (ErrorCategory)1001, null);
			}
			if (tdataObject.EmailAddresses.Count > 0)
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewRecipientObjectTask<TDataObject>.VerifyProxyAddress", LoggerHelper.CmdletPerfMonitors))
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, tdataObject.OrganizationId, base.ExecutingUserOrganizationId, false);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential, sessionSettings, 867, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
					bool flag = base.Fields["SoftDeletedObject"] != null;
					if (flag)
					{
						RecipientTaskHelper.StripInvalidSMTPAddress(this.ConfigurationSession, tdataObject, base.ProvisioningCache, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
						RecipientTaskHelper.StripConflictEmailAddress(tenantOrRootOrgRecipientSession, tdataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
					}
					else
					{
						if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && this.ShouldCheckAcceptedDomains())
						{
							RecipientTaskHelper.ValidateSmtpAddress(this.ConfigurationSession, tdataObject.EmailAddresses, tdataObject, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
						}
						RecipientTaskHelper.ValidateEmailAddressErrorOut(tenantOrRootOrgRecipientSession, tdataObject.EmailAddresses, tdataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
					}
				}
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				ADRecipient adrecipient = tdataObject;
				if ((RecipientTaskHelper.GetAcceptedRecipientTypes() & adrecipient.RecipientTypeDetails) != RecipientTypeDetails.None && string.IsNullOrEmpty(adrecipient.ExternalDirectoryObjectId))
				{
					adrecipient.ExternalDirectoryObjectId = Guid.NewGuid().ToString("D");
				}
			}
			TaskLogger.LogExit();
			return tdataObject;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001105C File Offset: 0x0000F25C
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}
	}
}
