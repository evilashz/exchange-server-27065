using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200008F RID: 143
	public abstract class SetRecipientObjectTask<TIdentity, TPublicObject, TDataObject> : SetADTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : ADRecipient, new()
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000158E4 File Offset: 0x00013AE4
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000158EC File Offset: 0x00013AEC
		internal LazilyInitialized<SharedTenantConfigurationState> CurrentOrgState { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000158F5 File Offset: 0x00013AF5
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000158F8 File Offset: 0x00013AF8
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001591E File Offset: 0x00013B1E
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreDefaultScope"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreDefaultScope"] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00015936 File Offset: 0x00013B36
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001593E File Offset: 0x00013B3E
		protected RecipientType DesiredRecipientType
		{
			get
			{
				return this.recipientType;
			}
			set
			{
				this.recipientType = value;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015947 File Offset: 0x00013B47
		protected virtual bool ShouldCheckAcceptedDomains()
		{
			return true;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001594C File Offset: 0x00013B4C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 305, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
			if (this.IgnoreDefaultScope)
			{
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			}
			tenantOrRootOrgRecipientSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(base.SessionSettings.GetAccountOrResourceForestFqdn());
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000159B1 File Offset: 0x00013BB1
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000159B4 File Offset: 0x00013BB4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADRecipient adrecipient = (ADRecipient)base.PrepareDataObject();
			if (adrecipient.IsChanged(ADRecipientSchema.PrimarySmtpAddress) && adrecipient.PrimarySmtpAddress != adrecipient.OriginalPrimarySmtpAddress && adrecipient.EmailAddressPolicyEnabled)
			{
				this.WriteWarning(Strings.WarningCannotSetPrimarySmtpAddressWhenEapEnabled);
			}
			if (RecipientTaskHelper.IsMailEnabledRecipientType(adrecipient.RecipientType) && !adrecipient.EmailAddressPolicyEnabled && adrecipient.WindowsEmailAddress != adrecipient.OriginalWindowsEmailAddress && adrecipient.PrimarySmtpAddress == adrecipient.OriginalPrimarySmtpAddress)
			{
				adrecipient.PrimarySmtpAddress = adrecipient.WindowsEmailAddress;
			}
			if (adrecipient.RecipientType == RecipientType.MailUser && (RecipientTypeDetails)adrecipient[ADRecipientSchema.RecipientTypeDetailsValue] == RecipientTypeDetails.None)
			{
				adrecipient.RecipientTypeDetails = RecipientTypeDetails.MailUser;
			}
			RecipientTaskHelper.RemoveEmptyValueFromEmailAddresses(adrecipient);
			TaskLogger.LogExit();
			return adrecipient;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00015A84 File Offset: 0x00013C84
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.Validate(this, this.SharedTenantConfigurationMode, this.CurrentOrgState, null);
			ADObject adobject = (ADObject)RecipientTaskHelper.ResolveDataObject<TDataObject>(base.DataSession, base.TenantGlobalCatalogSession, base.ServerSettings, this.Identity, this.RootId, base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<TDataObject>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, adobject))
			{
				base.UnderscopeDataSession(adobject.OrganizationId);
				base.CurrentOrganizationId = adobject.OrganizationId;
			}
			return adobject;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00015B40 File Offset: 0x00013D40
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(() => SharedConfiguration.GetSharedConfigurationState(base.CurrentOrganizationId));
			if (this.IgnoreDefaultScope && base.DomainController != null)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorIgnoreDefaultScopeAndDCSetTogether), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00015BA0 File Offset: 0x00013DA0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject
			});
			ADRecipient adrecipient = (ADRecipient)dataObject;
			this.DesiredRecipientType = adrecipient.RecipientType;
			base.StampChangesOn(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00015BE0 File Offset: 0x00013DE0
		protected override void InternalValidate()
		{
			SharedTenantConfigurationMode sharedTenantConfigurationMode = this.SharedTenantConfigurationMode;
			LazilyInitialized<SharedTenantConfigurationState> currentOrgState = this.CurrentOrgState;
			TIdentity identity = this.Identity;
			SharedConfigurationTaskHelper.Validate(this, sharedTenantConfigurationMode, currentOrgState, identity.ToString());
			ADObjectId adobjectId;
			if (this.IgnoreDefaultScope && !RecipientTaskHelper.IsValidDistinguishedName(this.Identity, out adobjectId))
			{
				base.WriteError(new ArgumentException(Strings.ErrorOnlyDNSupportedWithIgnoreDefaultScope), (ErrorCategory)1000, this.Identity);
			}
			base.InternalValidate();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015C5F File Offset: 0x00013E5F
		internal sealed override void PreInternalProcessRecord()
		{
			if (base.IsProvisioningLayerAvailable)
			{
				ProvisioningLayer.PreInternalProcessRecord(this, this.ConvertDataObjectToPresentationObject(this.DataObject), false);
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00015CAC File Offset: 0x00013EAC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			OrganizationId currentOrganizationId = base.CurrentOrganizationId;
			TDataObject dataObject = this.DataObject;
			if (!currentOrganizationId.Equals(dataObject.OrganizationId))
			{
				this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(delegate()
				{
					TDataObject dataObject17 = this.DataObject;
					return SharedConfiguration.GetSharedConfigurationState(dataObject17.OrganizationId);
				});
			}
			ADRecipient adrecipient = this.DataObject;
			bool flag = adrecipient != null && adrecipient.RecipientSoftDeletedStatus > 0;
			if (RecipientTaskHelper.IsMailEnabledRecipientType(this.DesiredRecipientType) && !flag)
			{
				if (!base.IsProvisioningLayerAvailable)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorNoProvisioningHandlerAvailable), (ErrorCategory)1001, null);
				}
				TDataObject dataObject2 = this.DataObject;
				if (dataObject2.IsModified(ADRecipientSchema.EmailAddresses))
				{
					TDataObject dataObject3 = this.DataObject;
					if (dataObject3.EmailAddresses.Count > 0)
					{
						if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && this.ShouldCheckAcceptedDomains())
						{
							IDirectorySession configurationSession = this.ConfigurationSession;
							TDataObject dataObject4 = this.DataObject;
							IConfigurationSession configurationSession2 = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(configurationSession, dataObject4.OrganizationId, true);
							IConfigurationSession cfgSession = configurationSession2;
							TDataObject dataObject5 = this.DataObject;
							RecipientTaskHelper.ValidateSmtpAddress(cfgSession, dataObject5.EmailAddresses, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
						}
						ADObjectId rootOrgContainerId = base.RootOrgContainerId;
						TDataObject dataObject6 = this.DataObject;
						ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, dataObject6.OrganizationId, base.ExecutingUserOrganizationId, false);
						IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, string.IsNullOrEmpty(base.DomainController) ? null : base.NetCredential, sessionSettings, 557, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
						IRecipientSession tenantCatalogSession = tenantOrRootOrgRecipientSession;
						TDataObject dataObject7 = this.DataObject;
						RecipientTaskHelper.ValidateEmailAddressErrorOut(tenantCatalogSession, dataObject7.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerReThrowDelegate(this.WriteError));
					}
				}
			}
			TDataObject dataObject8 = this.DataObject;
			if (dataObject8.IsChanged(ADObjectSchema.Id))
			{
				IDirectorySession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.OrgWideSessionSettings, ConfigScopes.TenantSubTree, 579, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = ((IDirectorySession)base.DataSession).UseConfigNC;
				TDataObject dataObject9 = this.DataObject;
				ADObjectId parent = dataObject9.Id.Parent;
				ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(parent, new PropertyDefinition[]
				{
					ADObjectSchema.ExchangeVersion
				});
				ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)adrawEntry[ADObjectSchema.ExchangeVersion];
				TDataObject dataObject10 = this.DataObject;
				if (dataObject10.ExchangeVersion.IsOlderThan(exchangeObjectVersion))
				{
					TDataObject dataObject11 = this.DataObject;
					string name = dataObject11.Name;
					TDataObject dataObject12 = this.DataObject;
					base.WriteError(new TaskException(Strings.ErrorParentHasNewerVersion(name, dataObject12.ExchangeVersion.ToString(), exchangeObjectVersion.ToString())), (ErrorCategory)1004, null);
				}
			}
			TDataObject dataObject13 = this.DataObject;
			if (dataObject13.RecipientType != this.DesiredRecipientType && this.DesiredRecipientType != RecipientType.Invalid)
			{
				TDataObject dataObject14 = this.DataObject;
				string id = dataObject14.Identity.ToString();
				string oldType = this.DesiredRecipientType.ToString();
				TDataObject dataObject15 = this.DataObject;
				Exception exception = new InvalidOperationException(Strings.ErrorSetTaskChangeRecipientType(id, oldType, dataObject15.RecipientType.ToString()));
				ErrorCategory category = (ErrorCategory)1000;
				TDataObject dataObject16 = this.DataObject;
				base.WriteError(exception, category, dataObject16.Identity);
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x04000132 RID: 306
		private RecipientType recipientType;
	}
}
