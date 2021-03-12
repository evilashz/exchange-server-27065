using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000008 RID: 8
	public abstract class EnableRecipientObjectTask<TIdentity, TDataObject> : RecipientObjectActionTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ADRecipient, new()
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003768 File Offset: 0x00001968
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000377F File Offset: 0x0000197F
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Alias
		{
			get
			{
				return (string)base.Fields["Alias"];
			}
			set
			{
				base.Fields["Alias"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003792 File Offset: 0x00001992
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000037A9 File Offset: 0x000019A9
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string DisplayName
		{
			get
			{
				return (string)base.Fields["DisplayName"];
			}
			set
			{
				base.Fields["DisplayName"] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000037BC File Offset: 0x000019BC
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000037E1 File Offset: 0x000019E1
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields["PrimarySmtpAddress"] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields["PrimarySmtpAddress"] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000037F9 File Offset: 0x000019F9
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000381F File Offset: 0x00001A1F
		[Parameter]
		public SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return (SwitchParameter)(base.Fields["OverrideRecipientQuotas"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OverrideRecipientQuotas"] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003837 File Offset: 0x00001A37
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003858 File Offset: 0x00001A58
		public virtual Capability SKUCapability
		{
			get
			{
				return (Capability)(base.Fields["SKUCapability"] ?? Capability.None);
			}
			set
			{
				base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value);
				base.Fields["SKUCapability"] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000389C File Offset: 0x00001A9C
		public virtual MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return (MultiValuedProperty<Capability>)(base.Fields["AddOnSKUCapability"] ?? new MultiValuedProperty<Capability>());
			}
			set
			{
				if (value != null)
				{
					base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value.ToArray());
				}
				base.Fields["AddOnSKUCapability"] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000038C3 File Offset: 0x00001AC3
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000038E4 File Offset: 0x00001AE4
		public virtual bool SKUAssigned
		{
			get
			{
				return (bool)(base.Fields[ADRecipientSchema.SKUAssigned] ?? false);
			}
			set
			{
				base.Fields[ADRecipientSchema.SKUAssigned] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00003913 File Offset: 0x00001B13
		public virtual CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)base.Fields[ADRecipientSchema.UsageLocation];
			}
			set
			{
				base.Fields[ADRecipientSchema.UsageLocation] = value;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003928 File Offset: 0x00001B28
		protected override IConfigurable ResolveDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable configurable = base.ResolveDataObject();
			if (configurable != null && base.IsProvisioningLayerAvailable)
			{
				ADRecipient adrecipient = configurable as ADRecipient;
				if (adrecipient != null)
				{
					if (!this.SkipPrepareDataObject())
					{
						adrecipient.SetExchangeVersion(adrecipient.MaximumSupportedExchangeObjectVersion);
					}
					base.CurrentOrganizationId = adrecipient.OrganizationId;
				}
			}
			TaskLogger.LogExit();
			return configurable;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000397C File Offset: 0x00001B7C
		internal virtual bool SkipPrepareDataObject()
		{
			return false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003980 File Offset: 0x00001B80
		protected sealed override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			this.PrepareRecipientObject(ref tdataObject);
			if (!this.SkipPrepareDataObject())
			{
				this.PrepareRecipientAlias(tdataObject);
				if (this.PrimarySmtpAddress != SmtpAddress.Empty)
				{
					tdataObject.PrimarySmtpAddress = this.PrimarySmtpAddress;
					tdataObject.EmailAddressPolicyEnabled = false;
				}
				if (!string.IsNullOrEmpty(this.DisplayName))
				{
					tdataObject.DisplayName = this.DisplayName;
				}
				if (base.IsProvisioningLayerAvailable)
				{
					ProvisioningLayer.UpdateAffectedIConfigurable(this, this.ConvertDataObjectToPresentationObject(tdataObject), false);
				}
				else
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
				}
				if (tdataObject.EmailAddresses.Count > 0)
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						RecipientTaskHelper.ValidateSmtpAddress(this.ConfigurationSession, tdataObject.EmailAddresses, tdataObject, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
					}
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, tdataObject.OrganizationId, base.CurrentOrganizationId, false);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential, sessionSettings, 243, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\common\\EnableRecipientObjectTask.cs");
					RecipientTaskHelper.ValidateEmailAddressErrorOut(tenantOrRootOrgRecipientSession, tdataObject.EmailAddresses, tdataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
				}
			}
			else if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.UpdateAffectedIConfigurable(this, this.ConvertDataObjectToPresentationObject(tdataObject), false);
			}
			else
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
			return tdataObject;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003B78 File Offset: 0x00001D78
		protected override void ProvisioningUpdateConfigurationObject()
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003B7C File Offset: 0x00001D7C
		protected virtual void PrepareRecipientObject(ref TDataObject dataObject)
		{
			ADUser aduser = dataObject as ADUser;
			if (aduser != null)
			{
				if (base.Fields.IsModified("SKUCapability"))
				{
					aduser.SKUCapability = new Capability?(this.SKUCapability);
				}
				if (base.Fields.IsModified("AddOnSKUCapability"))
				{
					CapabilityHelper.SetAddOnSKUCapabilities(this.AddOnSKUCapability, aduser.PersistedCapabilities);
					RecipientTaskHelper.UpgradeArchiveQuotaOnArchiveAddOnSKU(aduser, aduser.PersistedCapabilities);
				}
				if (base.Fields.IsModified(ADRecipientSchema.SKUAssigned))
				{
					aduser.SKUAssigned = new bool?(this.SKUAssigned);
				}
				if (base.Fields.IsModified(ADRecipientSchema.UsageLocation))
				{
					aduser.UsageLocation = this.UsageLocation;
				}
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003C34 File Offset: 0x00001E34
		protected virtual void PrepareRecipientAlias(TDataObject dataObject)
		{
			if (string.IsNullOrEmpty(this.Alias))
			{
				dataObject.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, dataObject.OrganizationId, dataObject.Name, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				return;
			}
			dataObject.Alias = this.Alias;
		}
	}
}
