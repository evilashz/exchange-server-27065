using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.RbacTasks;
using Microsoft.Exchange.Management.Tasks.ManagementScopeExtensions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000689 RID: 1673
	[Cmdlet("Set", "ManagementScope", SupportsShouldProcess = true, DefaultParameterSetName = "RecipientFilter")]
	public sealed class SetManagementScope : SetSystemConfigurationObjectTask<ManagementScopeIdParameter, ManagementScope>
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x000FBF4F File Offset: 0x000FA14F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetManagementScope(this.DataObject.Identity.ToString());
			}
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000FBF66 File Offset: 0x000FA166
		// (set) Token: 0x06003B3D RID: 15165 RVA: 0x000FBF8C File Offset: 0x000FA18C
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000FBFA4 File Offset: 0x000FA1A4
		// (set) Token: 0x06003B3F RID: 15167 RVA: 0x000FBFAC File Offset: 0x000FA1AC
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override ManagementScopeIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000FBFB5 File Offset: 0x000FA1B5
		// (set) Token: 0x06003B41 RID: 15169 RVA: 0x000FBFCC File Offset: 0x000FA1CC
		[Parameter(Mandatory = false, ParameterSetName = "RecipientFilter")]
		public string RecipientRestrictionFilter
		{
			get
			{
				return (string)base.Fields["RecipientRestrictionFilter"];
			}
			set
			{
				base.Fields["RecipientRestrictionFilter"] = value;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000FBFDF File Offset: 0x000FA1DF
		// (set) Token: 0x06003B43 RID: 15171 RVA: 0x000FBFF6 File Offset: 0x000FA1F6
		[Parameter(Mandatory = false, ParameterSetName = "RecipientFilter")]
		public OrganizationalUnitIdParameter RecipientRoot
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["RecipientRoot"];
			}
			set
			{
				base.Fields["RecipientRoot"] = value;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000FC009 File Offset: 0x000FA209
		// (set) Token: 0x06003B45 RID: 15173 RVA: 0x000FC020 File Offset: 0x000FA220
		[Parameter(Mandatory = true, ParameterSetName = "ServerFilter")]
		public string ServerRestrictionFilter
		{
			get
			{
				return (string)base.Fields["ServerRestrictionFilter"];
			}
			set
			{
				base.Fields["ServerRestrictionFilter"] = value;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000FC033 File Offset: 0x000FA233
		// (set) Token: 0x06003B47 RID: 15175 RVA: 0x000FC04A File Offset: 0x000FA24A
		[Parameter(Mandatory = true, ParameterSetName = "DatabaseFilter")]
		public string DatabaseRestrictionFilter
		{
			get
			{
				return (string)base.Fields["DatabaseRestrictionFilter"];
			}
			set
			{
				base.Fields["DatabaseRestrictionFilter"] = value;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000FC05D File Offset: 0x000FA25D
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x000FC074 File Offset: 0x000FA274
		[Parameter(Mandatory = true, ParameterSetName = "PartnerFilter")]
		public string PartnerDelegatedTenantRestrictionFilter
		{
			get
			{
				return (string)base.Fields["PartnerDelegatedTenantRestrictionFilter"];
			}
			set
			{
				base.Fields["PartnerDelegatedTenantRestrictionFilter"] = value;
			}
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000FC088 File Offset: 0x000FA288
		protected override void InternalValidate()
		{
			base.InternalValidate();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			if (base.ExchangeRunspaceConfig != null && this.DataObject.ObjectState != ObjectState.Unchanged)
			{
				base.WriteVerbose(Strings.VerboseLoadingAssignmentsByScope(this.DataObject.Id.ToString()));
				ExchangeRoleAssignment[] array = configurationSession.FindAssignmentsForManagementScope(this.DataObject, true);
				if (array.Length != 0)
				{
					ManagementScope managementScope = (ManagementScope)this.DataObject.GetOriginalObject();
					ManagementScope managementScope2 = (ManagementScope)this.DataObject.Clone();
					managementScope2.SetId(new ADObjectId("CN=TemporaryNewScope" + Guid.NewGuid()));
					managementScope2.ResetChangeTracking();
					Dictionary<ADObjectId, ManagementScope> dictionary = new Dictionary<ADObjectId, ManagementScope>(base.ExchangeRunspaceConfig.ScopesCache);
					if (!dictionary.ContainsKey(managementScope.Id))
					{
						dictionary.Add(managementScope.Id, managementScope);
					}
					dictionary.Add(managementScope2.Id, managementScope2);
					RoleHelper.LoadScopesByAssignmentsToNewCache(dictionary, array, configurationSession);
					foreach (ExchangeRoleAssignment exchangeRoleAssignment in array)
					{
						if (ADObjectId.Equals(exchangeRoleAssignment.CustomRecipientWriteScope, this.DataObject.Id))
						{
							exchangeRoleAssignment.CustomRecipientWriteScope = managementScope2.Id;
						}
						if (ADObjectId.Equals(exchangeRoleAssignment.CustomConfigWriteScope, this.DataObject.Id))
						{
							exchangeRoleAssignment.CustomConfigWriteScope = managementScope2.Id;
						}
						base.WriteVerbose(Strings.VerboseSetScopeValidateNewScopedAssignment(this.DataObject.Id.ToString(), exchangeRoleAssignment.Id.ToString()));
						if (!RoleHelper.HasDelegatingHierarchicalRoleAssignment(base.ExecutingUserOrganizationId, base.ExchangeRunspaceConfig.RoleAssignments, dictionary, exchangeRoleAssignment, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)))
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorSetScopeAddPermission(this.DataObject.Id.ToString(), exchangeRoleAssignment.Id.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
						}
					}
					List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>(base.ExchangeRunspaceConfig.RoleAssignments);
					List<ExchangeRoleAssignment> list2 = new List<ExchangeRoleAssignment>(array.Length);
					List<ADObjectId> list3 = new List<ADObjectId>(list.Count);
					foreach (ExchangeRoleAssignment exchangeRoleAssignment2 in array)
					{
						for (int k = list.Count - 1; k >= 0; k--)
						{
							if (ADObjectId.Equals(list[k].Id, exchangeRoleAssignment2.Id))
							{
								list.RemoveAt(k);
								list.Add(exchangeRoleAssignment2);
								list3.Add(exchangeRoleAssignment2.Id);
								break;
							}
						}
						list2.Add((ExchangeRoleAssignment)exchangeRoleAssignment2.GetOriginalObject());
					}
					foreach (ExchangeRoleAssignment exchangeRoleAssignment3 in list2)
					{
						base.WriteVerbose(Strings.VerboseSetScopeValidateRemoveOriginalScopedAssignment(this.DataObject.Id.ToString(), exchangeRoleAssignment3.Id.ToString()));
						if (!RoleHelper.HasDelegatingHierarchicalRoleAssignment(base.ExecutingUserOrganizationId, list, dictionary, exchangeRoleAssignment3, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)))
						{
							if (list3.Contains(exchangeRoleAssignment3.Id))
							{
								base.WriteError(new InvalidOperationException(Strings.ErrorSetScopeToBlockSelf(this.DataObject.Id.ToString(), exchangeRoleAssignment3.Id.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
							}
							else
							{
								base.WriteError(new InvalidOperationException(Strings.ErrorSetScopeNeedHierarchicalRoleAssignment(this.DataObject.Id.ToString(), exchangeRoleAssignment3.Id.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Id);
							}
						}
					}
				}
			}
			ManagementScope[] array4 = configurationSession.FindSimilarManagementScope(this.DataObject);
			if (array4.Length > 0)
			{
				base.WriteError(new ArgumentException(Strings.SimilarScopeFound(array4[0].Name)), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000FC490 File Offset: 0x000FA690
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (ManagementScope)base.PrepareDataObject();
			if (base.Fields.IsModified("RecipientRoot"))
			{
				if (this.RecipientRoot == null)
				{
					this.DataObject.RecipientRoot = null;
				}
				else
				{
					this.DataObject.RecipientRoot = this.GetADObjectIdFromOrganizationalUnitIdParameter((IConfigurationSession)base.DataSession, this.RecipientRoot);
				}
			}
			if (base.Fields.IsModified("RecipientRestrictionFilter"))
			{
				this.ValidateAndSetFilterOnDataObject("RecipientRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified("ServerRestrictionFilter"))
			{
				this.ValidateAndSetFilterOnDataObject("ServerRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified("PartnerDelegatedTenantRestrictionFilter"))
			{
				this.ValidateAndSetFilterOnDataObject("PartnerDelegatedTenantRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified("DatabaseRestrictionFilter"))
			{
				this.ValidateAndSetFilterOnDataObject("DatabaseRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x000FC5C8 File Offset: 0x000FA7C8
		private ADObjectId GetADObjectIdFromOrganizationalUnitIdParameter(IConfigurationSession configurationSession, OrganizationalUnitIdParameter root)
		{
			OrganizationId organizationId = OrganizationId.ForestWideOrgId;
			if (configurationSession is ITenantConfigurationSession)
			{
				organizationId = TaskHelper.ResolveOrganizationId(this.DataObject.Id, ManagementScope.RdnScopesContainerToOrganization, (ITenantConfigurationSession)configurationSession);
			}
			bool useConfigNC = configurationSession.UseConfigNC;
			bool useGlobalCatalog = configurationSession.UseGlobalCatalog;
			ADObjectId id;
			try
			{
				configurationSession.UseConfigNC = false;
				configurationSession.UseGlobalCatalog = true;
				IConfigurable configurable = (ADConfigurationObject)base.GetDataObject<ExchangeOrganizationalUnit>(root, configurationSession, (null == organizationId) ? null : organizationId.OrganizationalUnit, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(root.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(root.ToString())));
				id = ((ADObject)configurable).Id;
			}
			finally
			{
				configurationSession.UseConfigNC = useConfigNC;
				configurationSession.UseGlobalCatalog = useGlobalCatalog;
			}
			return id;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000FC68C File Offset: 0x000FA88C
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000FC6AC File Offset: 0x000FA8AC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
