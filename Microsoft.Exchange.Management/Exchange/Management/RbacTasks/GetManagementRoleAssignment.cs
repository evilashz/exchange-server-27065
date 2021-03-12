using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200065F RID: 1631
	[Cmdlet("Get", "ManagementRoleAssignment", DefaultParameterSetName = "Identity")]
	public sealed class GetManagementRoleAssignment : GetMultitenancySystemConfigurationObjectTask<RoleAssignmentIdParameter, ExchangeRoleAssignment>
	{
		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000EEBBD File Offset: 0x000ECDBD
		// (set) Token: 0x06003903 RID: 14595 RVA: 0x000EEBC5 File Offset: 0x000ECDC5
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000EEBCE File Offset: 0x000ECDCE
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000EEBE0 File Offset: 0x000ECDE0
		public static ADScope GetRecipientWriteADScope(ExchangeRoleAssignmentPresentation roleAssignment, ADRawEntry assignee, ManagementScope managementScope)
		{
			Dictionary<ADObjectId, ManagementScope> dictionary = new Dictionary<ADObjectId, ManagementScope>();
			if (managementScope != null)
			{
				dictionary.Add(managementScope.Id, managementScope);
			}
			if (roleAssignment.RecipientWriteScope == RecipientWriteScopeType.MyGAL)
			{
				throw new ArgumentException("Role assignment with MyGAL scope is not supported");
			}
			ExchangeRoleAssignment exchangeRoleAssignment = (ExchangeRoleAssignment)roleAssignment.DataObject;
			RbacScope recipientWriteRbacScope = ExchangeRoleAssignment.GetRecipientWriteRbacScope(roleAssignment.RecipientWriteScope, roleAssignment.CustomRecipientWriteScope, dictionary, null, exchangeRoleAssignment.IsFromEndUserRole);
			if (recipientWriteRbacScope != null)
			{
				recipientWriteRbacScope.PopulateRootAndFilter((OrganizationId)assignee[ADObjectSchema.OrganizationId], assignee);
			}
			return recipientWriteRbacScope;
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06003906 RID: 14598 RVA: 0x000EEC58 File Offset: 0x000ECE58
		protected override ObjectId RootId
		{
			get
			{
				if (this.sharedConfiguration != null)
				{
					return this.sharedConfiguration.SharedConfigurationCU.Id;
				}
				if (this.Organization == null && GetManagementRoleAssignment.IsDatacenter && this.assignee != null)
				{
					return this.assignee.OrganizationId.ConfigurationUnit;
				}
				if (this.Organization == null && GetManagementRoleAssignment.IsDatacenter && this.anyRole != null)
				{
					return this.anyRole.OrganizationId.ConfigurationUnit;
				}
				return base.RootId;
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x000EECD4 File Offset: 0x000ECED4
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x000EECEB File Offset: 0x000ECEEB
		[Parameter(ValueFromPipeline = true, ParameterSetName = "RoleAssignee")]
		public RoleIdParameter Role
		{
			get
			{
				return (RoleIdParameter)base.Fields[RbacCommonParameters.ParameterRole];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterRole] = value;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x000EECFE File Offset: 0x000ECEFE
		// (set) Token: 0x0600390A RID: 14602 RVA: 0x000EED15 File Offset: 0x000ECF15
		[Parameter(ParameterSetName = "RoleAssignee")]
		public RoleAssigneeIdParameter RoleAssignee
		{
			get
			{
				return (RoleAssigneeIdParameter)base.Fields[RbacCommonParameters.ParameterRoleAssignee];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterRoleAssignee] = value;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000EED28 File Offset: 0x000ECF28
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x000EED3F File Offset: 0x000ECF3F
		[Parameter(ParameterSetName = "RoleAssignee")]
		public AssignmentMethod[] AssignmentMethod
		{
			get
			{
				return (AssignmentMethod[])base.Fields[RbacCommonParameters.ParameterAssignmentMethod];
			}
			set
			{
				base.VerifyValues<AssignmentMethod>(GetManagementRoleAssignment.AllowedAssignmentMethods, value);
				base.Fields[RbacCommonParameters.ParameterAssignmentMethod] = value;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000EED5E File Offset: 0x000ECF5E
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x000EED75 File Offset: 0x000ECF75
		[Parameter]
		public RoleAssigneeType RoleAssigneeType
		{
			get
			{
				return (RoleAssigneeType)base.Fields[RbacCommonParameters.ParameterRoleAssigneeType];
			}
			set
			{
				base.VerifyValues<RoleAssigneeType>((RoleAssigneeType[])Enum.GetValues(typeof(RoleAssigneeType)), value);
				base.Fields[RbacCommonParameters.ParameterRoleAssigneeType] = value;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000EEDA8 File Offset: 0x000ECFA8
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x000EEDBF File Offset: 0x000ECFBF
		[Parameter]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields[RbacCommonParameters.ParameterEnabled];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterEnabled] = value;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000EEDD7 File Offset: 0x000ECFD7
		// (set) Token: 0x06003912 RID: 14610 RVA: 0x000EEDEE File Offset: 0x000ECFEE
		[Parameter]
		public bool Delegating
		{
			get
			{
				return (bool)base.Fields[RbacCommonParameters.ParameterDelegating];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterDelegating] = value;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000EEE06 File Offset: 0x000ED006
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x000EEE1D File Offset: 0x000ED01D
		[Parameter]
		public bool Exclusive
		{
			get
			{
				return (bool)base.Fields["Exclusive"];
			}
			set
			{
				base.Fields["Exclusive"] = value;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000EEE35 File Offset: 0x000ED035
		// (set) Token: 0x06003916 RID: 14614 RVA: 0x000EEE56 File Offset: 0x000ED056
		[Parameter]
		public RecipientWriteScopeType RecipientWriteScope
		{
			get
			{
				return (RecipientWriteScopeType)(base.Fields[RbacCommonParameters.ParameterRecipientWriteScope] ?? -1);
			}
			set
			{
				base.VerifyValues<RecipientWriteScopeType>((RecipientWriteScopeType[])Enum.GetValues(typeof(RecipientWriteScopeType)), value);
				base.Fields[RbacCommonParameters.ParameterRecipientWriteScope] = value;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x000EEE89 File Offset: 0x000ED089
		// (set) Token: 0x06003918 RID: 14616 RVA: 0x000EEEA0 File Offset: 0x000ED0A0
		[ValidateNotNullOrEmpty]
		[Parameter]
		public ManagementScopeIdParameter CustomRecipientWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields[RbacCommonParameters.ParameterCustomRecipientWriteScope];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterCustomRecipientWriteScope] = value;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x000EEEB3 File Offset: 0x000ED0B3
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x000EEECA File Offset: 0x000ED0CA
		[Parameter]
		[ValidateNotNullOrEmpty]
		public OrganizationalUnitIdParameter RecipientOrganizationalUnitScope
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterRecipientOrganizationalUnitScope] = value;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000EEEDD File Offset: 0x000ED0DD
		// (set) Token: 0x0600391C RID: 14620 RVA: 0x000EEEFE File Offset: 0x000ED0FE
		[Parameter]
		public ConfigWriteScopeType ConfigWriteScope
		{
			get
			{
				return (ConfigWriteScopeType)(base.Fields[RbacCommonParameters.ParameterConfigWriteScope] ?? -1);
			}
			set
			{
				base.VerifyValues<ConfigWriteScopeType>((ConfigWriteScopeType[])Enum.GetValues(typeof(ConfigWriteScopeType)), value);
				base.Fields[RbacCommonParameters.ParameterConfigWriteScope] = value;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000EEF31 File Offset: 0x000ED131
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x000EEF48 File Offset: 0x000ED148
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ManagementScopeIdParameter CustomConfigWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields[RbacCommonParameters.ParameterCustomConfigWriteScope];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterCustomConfigWriteScope] = value;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x000EEF5B File Offset: 0x000ED15B
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x000EEF72 File Offset: 0x000ED172
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ManagementScopeIdParameter ExclusiveRecipientWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields["ExclusiveRecipientWriteScope"];
			}
			set
			{
				base.Fields["ExclusiveRecipientWriteScope"] = value;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000EEF85 File Offset: 0x000ED185
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x000EEF9C File Offset: 0x000ED19C
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ManagementScopeIdParameter ExclusiveConfigWriteScope
		{
			get
			{
				return (ManagementScopeIdParameter)base.Fields["ExclusiveConfigWriteScope"];
			}
			set
			{
				base.Fields["ExclusiveConfigWriteScope"] = value;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000EEFAF File Offset: 0x000ED1AF
		// (set) Token: 0x06003924 RID: 14628 RVA: 0x000EEFD5 File Offset: 0x000ED1D5
		[Parameter(Mandatory = false)]
		public SwitchParameter GetEffectiveUsers
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetEffectiveUsers"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GetEffectiveUsers"] = value;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000EEFED File Offset: 0x000ED1ED
		// (set) Token: 0x06003926 RID: 14630 RVA: 0x000EF004 File Offset: 0x000ED204
		[ValidateNotNullOrEmpty]
		[Parameter]
		public GeneralRecipientIdParameter WritableRecipient
		{
			get
			{
				return (GeneralRecipientIdParameter)base.Fields["WritableRecipient"];
			}
			set
			{
				base.Fields["WritableRecipient"] = value;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000EF017 File Offset: 0x000ED217
		// (set) Token: 0x06003928 RID: 14632 RVA: 0x000EF02E File Offset: 0x000ED22E
		[Parameter]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter WritableServer
		{
			get
			{
				return (ServerIdParameter)base.Fields["WritableServer"];
			}
			set
			{
				base.Fields["WritableServer"] = value;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000EF041 File Offset: 0x000ED241
		// (set) Token: 0x0600392A RID: 14634 RVA: 0x000EF058 File Offset: 0x000ED258
		[Parameter]
		[ValidateNotNullOrEmpty]
		public DatabaseIdParameter WritableDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["ParameterWritableDatabase"];
			}
			set
			{
				base.Fields["ParameterWritableDatabase"] = value;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x000EF06B File Offset: 0x000ED26B
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000EF070 File Offset: 0x000ED270
		protected override IConfigDataProvider CreateSession()
		{
			if (this.sharedConfiguration != null)
			{
				return this.sharedSystemConfigSession;
			}
			IConfigurationSession configurationSession;
			if (this.RoleAssignee != null || this.Role != null)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 408, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleAssignment\\GetManagementRoleAssignment.cs");
			}
			else
			{
				configurationSession = (IConfigurationSession)base.CreateSession();
			}
			configurationSession.SessionSettings.IsSharedConfigChecked = true;
			return configurationSession;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000EF0E8 File Offset: 0x000ED2E8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.ReScopeSharedConfigAndSharedSessionIfNecessary(base.CurrentOrganizationId);
			if (base.Fields.IsModified(RbacCommonParameters.ParameterAssignmentMethod) && !base.Fields.IsModified(RbacCommonParameters.ParameterRoleAssignee))
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorAssignmentMethodWithoutRoleAssignee), ErrorCategory.InvalidArgument, null);
			}
			RbacRoleAssignmentCommon.CheckMutuallyExclusiveParameters(this);
			List<QueryFilter> list = new List<QueryFilter>();
			if (this.RoleAssignee != null)
			{
				AssignmentMethod assignmentMethod = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.None;
				if (base.Fields.IsModified(RbacCommonParameters.ParameterAssignmentMethod))
				{
					foreach (AssignmentMethod assignmentMethod3 in this.AssignmentMethod)
					{
						assignmentMethod |= assignmentMethod3;
					}
					this.assigneeIds = new List<ADObjectId>();
				}
				else
				{
					assignmentMethod = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.All;
				}
				if (this.sharedConfiguration != null)
				{
					this.assignee = RoleAssigneeIdParameter.GetRawRoleAssignee(this.RoleAssignee, this.sharedSystemConfigSession, base.TenantGlobalCatalogSession);
				}
				else
				{
					this.assignee = RoleAssigneeIdParameter.GetRawRoleAssignee(this.RoleAssignee, this.ConfigurationSession, base.TenantGlobalCatalogSession);
				}
				if ((base.CurrentOrganizationId == null || base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId)) && this.assignee != null && this.assignee.OrganizationId != null && !this.assignee.OrganizationId.Equals(base.CurrentOrganizationId))
				{
					base.CurrentOrganizationId = this.assignee.OrganizationId;
					this.ReScopeSharedConfigAndSharedSessionIfNecessary(this.assignee.OrganizationId);
				}
				if (!(this.assignee is RoleAssignmentPolicy))
				{
					base.WriteVerbose(Strings.VerboseResolvingSecurityPrinciplals);
					if (((ADRecipient)this.assignee).RecipientTypeDetails != RecipientTypeDetails.MailboxPlan)
					{
						RoleHelper.ValidateRoleAssignmentUser((ADRecipient)this.assignee, new Task.TaskErrorLoggingDelegate(base.ThrowTerminatingError), true);
					}
					else
					{
						assignmentMethod &= ~Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.MailboxPlan;
					}
					ADObjectId roleAssignmentPolicy = ((ADRecipient)this.assignee).RoleAssignmentPolicy;
					if (this.AssignmentMethod != null && this.AssignmentMethod.Length == 1 && this.AssignmentMethod[0] == Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleAssignmentPolicy && roleAssignmentPolicy == null)
					{
						base.ThrowTerminatingError(new ArgumentException(Strings.ErrorUserNotHaveRoleAssignmentPolicy(this.RoleAssignee.ToString())), ErrorCategory.InvalidArgument, null);
					}
					List<string> tokenSids = base.TenantGlobalCatalogSession.GetTokenSids((ADRecipient)this.assignee, assignmentMethod);
					if (tokenSids == null || tokenSids.Count == 0)
					{
						if (this.AssignmentMethod != null && this.AssignmentMethod.Length == 1)
						{
							if (this.AssignmentMethod[0] == Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleGroup)
							{
								base.ThrowTerminatingError(new ArgumentException(Strings.ErrorUserNotInRoleGroups(this.RoleAssignee.ToString())), ErrorCategory.InvalidArgument, null);
							}
							else if (this.AssignmentMethod[0] == Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.SecurityGroup)
							{
								base.ThrowTerminatingError(new ArgumentException(Strings.ErrorUserNotInSecurityGroups(this.RoleAssignee.ToString())), ErrorCategory.InvalidArgument, null);
							}
							else if (this.AssignmentMethod[0] == Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.MailboxPlan)
							{
								base.ThrowTerminatingError(new ArgumentException(Strings.ErrorUserNotHaveMailboxPlan(this.RoleAssignee.ToString())), ErrorCategory.InvalidArgument, null);
							}
						}
					}
					else
					{
						ADObjectId[] array = base.TenantGlobalCatalogSession.ResolveSidsToADObjectIds(tokenSids.ToArray());
						if (this.sharedConfiguration != null)
						{
							ADObjectId[] sharedRoleGroupIds = this.sharedConfiguration.GetSharedRoleGroupIds(array);
							this.assigneeIds = new List<ADObjectId>();
							if (!sharedRoleGroupIds.IsNullOrEmpty<ADObjectId>())
							{
								this.assigneeIds.AddRange(sharedRoleGroupIds);
							}
						}
						else
						{
							this.assigneeIds = new List<ADObjectId>(array);
						}
					}
					if ((this.sharedConfiguration != null || roleAssignmentPolicy != null) && (assignmentMethod & Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleAssignmentPolicy) != Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.None)
					{
						if (this.assigneeIds == null)
						{
							this.assigneeIds = new List<ADObjectId>(1);
						}
						if (this.sharedConfiguration == null && roleAssignmentPolicy != null)
						{
							this.assigneeIds.Add(roleAssignmentPolicy);
						}
						else
						{
							this.assigneeIds.Add(this.sharedConfiguration.GetSharedRoleAssignmentPolicy());
						}
					}
				}
				else if ((assignmentMethod & Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.Direct) != Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.None)
				{
					if (this.assigneeIds == null)
					{
						this.assigneeIds = new List<ADObjectId>(1);
					}
					if (this.sharedConfiguration == null)
					{
						this.assigneeIds.Add(this.assignee.Id);
					}
					else
					{
						this.assigneeIds.Add(this.sharedConfiguration.GetSharedRoleAssignmentPolicy());
					}
				}
			}
			if (this.AssignmentMethod != null && this.AssignmentMethod.Contains(Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleGroup) && !this.AssignmentMethod.Contains(Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.SecurityGroup))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RoleAssigneeType, RoleAssigneeType.RoleGroup));
			}
			if (this.CustomConfigWriteScope != null)
			{
				ManagementScope managementScope = (ManagementScope)base.GetDataObject<ManagementScope>(this.CustomConfigWriteScope, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorScopeNotFound(this.CustomConfigWriteScope.ToString())), new LocalizedString?(Strings.ErrorScopeNotUnique(this.CustomConfigWriteScope.ToString())));
				if (managementScope.Exclusive)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorScopeExclusive(managementScope.Id.ToString(), RbacCommonParameters.ParameterCustomConfigWriteScope)), ErrorCategory.InvalidArgument, null);
				}
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, ConfigWriteScopeType.CustomConfigScope));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomConfigWriteScope, managementScope.Id));
			}
			if (this.ExclusiveConfigWriteScope != null)
			{
				ManagementScope managementScope2 = (ManagementScope)base.GetDataObject<ManagementScope>(this.ExclusiveConfigWriteScope, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorScopeNotFound(this.ExclusiveConfigWriteScope.ToString())), new LocalizedString?(Strings.ErrorScopeNotUnique(this.ExclusiveConfigWriteScope.ToString())));
				if (!managementScope2.Exclusive)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorScopeNotExclusive(managementScope2.Id.ToString(), "ExclusiveConfigWriteScope")), ErrorCategory.InvalidArgument, null);
				}
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, ConfigWriteScopeType.ExclusiveConfigScope));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomConfigWriteScope, managementScope2.Id));
			}
			if (this.CustomRecipientWriteScope != null)
			{
				base.WriteVerbose(Strings.VerboseResolvingCustomRecipientWriteScope);
				ManagementScope managementScope3 = (ManagementScope)base.GetDataObject<ManagementScope>(this.CustomRecipientWriteScope, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorScopeNotFound(this.CustomRecipientWriteScope.ToString())), new LocalizedString?(Strings.ErrorScopeNotUnique(this.CustomRecipientWriteScope.ToString())));
				if (managementScope3.Exclusive)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorScopeExclusive(managementScope3.Id.ToString(), RbacCommonParameters.ParameterCustomRecipientWriteScope)), ErrorCategory.InvalidArgument, null);
				}
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RecipientWriteScope, RecipientWriteScopeType.CustomRecipientScope));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomRecipientWriteScope, managementScope3.Id));
			}
			if (this.ExclusiveRecipientWriteScope != null)
			{
				base.WriteVerbose(Strings.VerboseResolvingExclusiveRecipientWriteScope);
				ManagementScope managementScope4 = (ManagementScope)base.GetDataObject<ManagementScope>(this.ExclusiveRecipientWriteScope, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorScopeNotFound(this.ExclusiveRecipientWriteScope.ToString())), new LocalizedString?(Strings.ErrorScopeNotUnique(this.ExclusiveRecipientWriteScope.ToString())));
				if (!managementScope4.Exclusive)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorScopeNotExclusive(managementScope4.Id.ToString(), "ExclusiveRecipientWriteScope")), ErrorCategory.InvalidArgument, null);
				}
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RecipientWriteScope, RecipientWriteScopeType.ExclusiveRecipientScope));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomRecipientWriteScope, managementScope4.Id));
			}
			if (this.RecipientOrganizationalUnitScope != null)
			{
				base.WriteVerbose(Strings.VerboseResolvingRecipientOrganizationalUnitScope);
				bool useConfigNC = this.ConfigurationSession.UseConfigNC;
				bool useGlobalCatalog = this.ConfigurationSession.UseGlobalCatalog;
				try
				{
					this.ConfigurationSession.UseConfigNC = false;
					this.ConfigurationSession.UseGlobalCatalog = true;
					ExchangeOrganizationalUnit exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(this.RecipientOrganizationalUnitScope, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.RecipientOrganizationalUnitScope.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.RecipientOrganizationalUnitScope.ToString())));
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RecipientWriteScope, RecipientWriteScopeType.OU));
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.CustomRecipientWriteScope, exchangeOrganizationalUnit.Id));
				}
				finally
				{
					this.ConfigurationSession.UseConfigNC = useConfigNC;
					this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
				}
			}
			this.InitializeWritableReportingObjectIfNecessary();
			if (base.Fields.IsModified(RbacCommonParameters.ParameterDelegating))
			{
				if (!this.Delegating)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType, RoleAssignmentDelegationType.Regular));
				}
				else
				{
					list.Add(new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType, RoleAssignmentDelegationType.Delegating),
						new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RoleAssignmentDelegationType, RoleAssignmentDelegationType.DelegatingOrgWide)
					}));
				}
			}
			if (base.Fields.IsModified(RbacCommonParameters.ParameterRecipientWriteScope))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RecipientWriteScope, this.RecipientWriteScope));
			}
			if (base.Fields.IsModified(RbacCommonParameters.ParameterConfigWriteScope))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, this.ConfigWriteScope));
			}
			if (base.Fields.IsModified("Exclusive"))
			{
				if (this.Exclusive)
				{
					list.Add(new OrFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RecipientWriteScope, RecipientWriteScopeType.ExclusiveRecipientScope),
						new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, ConfigWriteScopeType.ExclusiveConfigScope)
					}));
				}
				else
				{
					list.Add(new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RecipientWriteScope, RecipientWriteScopeType.ExclusiveRecipientScope)));
					list.Add(new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, ConfigWriteScopeType.ExclusiveConfigScope)));
				}
			}
			else if (base.Fields.IsModified(RbacCommonParameters.ParameterCustomConfigWriteScope))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.ConfigWriteScope, ConfigWriteScopeType.CustomConfigScope));
			}
			if (base.Fields.IsModified(RbacCommonParameters.ParameterRoleAssigneeType))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.RoleAssigneeType, this.RoleAssigneeType));
			}
			if (1 < list.Count)
			{
				this.internalFilterForNonPipelineParameter = new AndFilter(list.ToArray());
			}
			else if (1 == list.Count)
			{
				this.internalFilterForNonPipelineParameter = list[0];
			}
			else
			{
				this.internalFilterForNonPipelineParameter = null;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000EFAE8 File Offset: 0x000EDCE8
		protected override IEnumerable<ExchangeRoleAssignment> GetPagedData()
		{
			List<ADObjectId> sharedTenantAssigneeIds = this.assigneeIds;
			if (sharedTenantAssigneeIds == null && this.sharedConfiguration != null)
			{
				sharedTenantAssigneeIds = this.GetSharedTenantAssigneeIds();
			}
			IEnumerable<ExchangeRoleAssignment> enumerable;
			if (sharedTenantAssigneeIds == null)
			{
				enumerable = base.GetPagedData();
			}
			else
			{
				if (sharedTenantAssigneeIds.Count == 0)
				{
					return null;
				}
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				if (this.sharedConfiguration == null)
				{
					configurationSession = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(configurationSession, this.assignee.OrganizationId, true);
				}
				List<ExchangeRoleAssignment> list = new List<ExchangeRoleAssignment>();
				Result<ExchangeRoleAssignment>[] array = configurationSession.FindRoleAssignmentsByUserIds(sharedTenantAssigneeIds.ToArray(), this.InternalFilter);
				foreach (Result<ExchangeRoleAssignment> result in array)
				{
					list.Add(result.Data);
				}
				enumerable = list;
			}
			if (GetManagementRoleAssignment.WritableObjectType.NotApplicable != this.writableObjectType)
			{
				this.InitializeManagementReportingIfNecessary();
			}
			switch (this.writableObjectType)
			{
			case GetManagementRoleAssignment.WritableObjectType.Recipient:
				enumerable = this.managementReporting.FindRoleAssignmentsWithWritableRecipient(this.writableObject, enumerable);
				break;
			case GetManagementRoleAssignment.WritableObjectType.Server:
				enumerable = this.managementReporting.FindRoleAssignmentsWithWritableServer((Server)this.writableObject, enumerable);
				break;
			case GetManagementRoleAssignment.WritableObjectType.Database:
				enumerable = this.managementReporting.FindRoleAssignmentsWithWritableDatabase((Database)this.writableObject, enumerable);
				break;
			}
			return enumerable;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000EFC20 File Offset: 0x000EDE20
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.anyRole = null;
			if (this.Role != null)
			{
				ADObjectId rootID = null;
				IConfigurationSession configurationSession;
				if (this.sharedSystemConfigSession != null)
				{
					configurationSession = this.sharedSystemConfigSession;
					rootID = SharedConfiguration.GetSharedConfiguration(base.CurrentOrganizationId).SharedConfigurationCU.ConfigurationUnit;
				}
				else
				{
					configurationSession = this.ConfigurationSession;
					if (GetManagementRoleAssignment.IsDatacenter)
					{
						if (this.assignee != null)
						{
							rootID = this.assignee.OrganizationId.ConfigurationUnit;
						}
						else if ((base.CurrentOrganizationId == null || base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId)) && this.Role.InternalADObjectId == null)
						{
							throw new ArgumentException("In datacenter you can not search for role assignments based on a role name in the root organization. Please search for the role and use the identity in this cmdlet.");
						}
					}
				}
				IEnumerable<ExchangeRole> dataObjects = base.GetDataObjects<ExchangeRole>(this.Role, configurationSession, rootID);
				List<QueryFilter> list = new List<QueryFilter>();
				foreach (ExchangeRole exchangeRole in dataObjects)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleAssignmentSchema.Role, exchangeRole.Id));
					this.anyRole = exchangeRole;
				}
				if (list.Count == 0)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorRoleNotFound(this.Role.ToString())), (ErrorCategory)1000, null);
				}
				this.internalFilterForPipelineParameter = QueryFilter.OrTogether(list.ToArray());
			}
			else
			{
				this.internalFilterForPipelineParameter = null;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000EFD8C File Offset: 0x000EDF8C
		protected override void InternalValidate()
		{
			if (this.Identity != null)
			{
				this.Identity.InternalFilter = this.InternalFilter;
			}
			base.InternalValidate();
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x000EFDB0 File Offset: 0x000EDFB0
		protected override QueryFilter InternalFilter
		{
			get
			{
				List<QueryFilter> list = new List<QueryFilter>();
				if (base.InternalFilter != null)
				{
					list.Add(base.InternalFilter);
				}
				if (this.internalFilterForNonPipelineParameter != null)
				{
					list.Add(this.internalFilterForNonPipelineParameter);
				}
				if (this.internalFilterForPipelineParameter != null)
				{
					list.Add(this.internalFilterForPipelineParameter);
				}
				if (1 < list.Count)
				{
					return new AndFilter(list.ToArray());
				}
				if (1 == list.Count)
				{
					return list[0];
				}
				return null;
			}
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000EFE28 File Offset: 0x000EE028
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ExchangeRoleAssignment exchangeRoleAssignment = (ExchangeRoleAssignment)dataObject;
			bool flag = false;
			if (base.Fields.IsModified(RbacCommonParameters.ParameterEnabled))
			{
				flag |= (this.Enabled != exchangeRoleAssignment.Enabled);
			}
			if (base.Fields.IsModified(RbacCommonParameters.ParameterDelegating))
			{
				if (this.Delegating)
				{
					flag |= ((exchangeRoleAssignment.RoleAssignmentDelegationType & RoleAssignmentDelegationType.Delegating) == (RoleAssignmentDelegationType)0);
				}
				else
				{
					flag |= ((exchangeRoleAssignment.RoleAssignmentDelegationType & RoleAssignmentDelegationType.Regular) == (RoleAssignmentDelegationType)0);
				}
			}
			if (base.Fields.IsModified(RbacCommonParameters.ParameterRecipientWriteScope))
			{
				flag |= (this.RecipientWriteScope != exchangeRoleAssignment.RecipientWriteScope);
			}
			if (base.Fields.IsModified(RbacCommonParameters.ParameterConfigWriteScope))
			{
				flag |= (this.ConfigWriteScope != exchangeRoleAssignment.ConfigWriteScope);
			}
			if (flag)
			{
				base.WriteVerbose(Strings.VerboseSkipObject(exchangeRoleAssignment.DistinguishedName));
			}
			else
			{
				ExchangeRoleAssignmentPresentation exchangeRoleAssignmentPresentation = (ExchangeRoleAssignmentPresentation)this.ConvertDataObjectToPresentationObject(dataObject);
				base.WriteResult(exchangeRoleAssignmentPresentation);
				if ((exchangeRoleAssignmentPresentation.RoleAssigneeType == RoleAssigneeType.SecurityGroup || exchangeRoleAssignmentPresentation.RoleAssigneeType == RoleAssigneeType.RoleGroup) && this.GetEffectiveUsers)
				{
					this.HandleEffectiveUsersWriteResult(dataObject, exchangeRoleAssignment, this.GetAssignmentMethod(exchangeRoleAssignment));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000EFF60 File Offset: 0x000EE160
		private void HandleEffectiveUsersWriteResult(IConfigurable dataObject, ExchangeRoleAssignment roleAssignement, AssignmentMethod assignmentMethod)
		{
			if (this.roleAssignmentExpansion == null)
			{
				this.roleAssignmentExpansion = new RoleAssignmentExpansion(base.TenantGlobalCatalogSession, base.CurrentOrganizationId);
			}
			List<ADObjectId> effectiveUsersForRoleAssignment = this.roleAssignmentExpansion.GetEffectiveUsersForRoleAssignment(roleAssignement);
			foreach (ADObjectId adobjectId in effectiveUsersForRoleAssignment)
			{
				if (this.assignee == null || (this.assignee != null && this.assignee.Id.Equals(adobjectId)))
				{
					MultiValuedProperty<FormattedADObjectIdCollection> assignmentChains = this.roleAssignmentExpansion.GetAssignmentChains(roleAssignement.User, adobjectId);
					ExchangeRoleAssignmentPresentation exchangeRoleAssignmentPresentation = (ExchangeRoleAssignmentPresentation)this.ConvertDataObjectToPresentationObject(dataObject);
					exchangeRoleAssignmentPresentation.UpdatePresentationObjectWithEffectiveUser(adobjectId, assignmentChains, this.GetEffectiveUsers, assignmentMethod);
					base.WriteResult(exchangeRoleAssignmentPresentation);
				}
			}
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000F0034 File Offset: 0x000EE234
		private AssignmentMethod GetAssignmentMethod(ExchangeRoleAssignment roleAssignment)
		{
			AssignmentMethod result = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.Direct;
			RoleAssigneeType roleAssigneeType = roleAssignment.RoleAssigneeType;
			switch (roleAssigneeType)
			{
			case RoleAssigneeType.SecurityGroup:
				result = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.SecurityGroup;
				break;
			case (RoleAssigneeType)3:
			case (RoleAssigneeType)5:
				break;
			case RoleAssigneeType.RoleAssignmentPolicy:
				result = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleAssignmentPolicy;
				break;
			case RoleAssigneeType.MailboxPlan:
				result = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.MailboxPlan;
				break;
			default:
				switch (roleAssigneeType)
				{
				case RoleAssigneeType.RoleGroup:
				case RoleAssigneeType.LinkedRoleGroup:
					result = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleGroup;
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000F0090 File Offset: 0x000EE290
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			AssignmentMethod assignmentMethod = Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.Direct;
			ExchangeRoleAssignment exchangeRoleAssignment = (ExchangeRoleAssignment)dataObject;
			ADObjectId adobjectId = exchangeRoleAssignment.User;
			ADObjectId adobjectId2 = exchangeRoleAssignment.User;
			if (this.sharedConfiguration != null && adobjectId2 != null)
			{
				RoleAssigneeType roleAssigneeType = exchangeRoleAssignment.RoleAssigneeType;
				switch (roleAssigneeType)
				{
				case RoleAssigneeType.SecurityGroup:
				case (RoleAssigneeType)3:
					break;
				case RoleAssigneeType.RoleAssignmentPolicy:
					adobjectId2 = (adobjectId = this.GetTinyTenantLocalRap());
					goto IL_A1;
				default:
					if (roleAssigneeType != RoleAssigneeType.RoleGroup)
					{
					}
					break;
				}
				ADObjectId[] tinyTenantGroupIds = this.sharedConfiguration.GetTinyTenantGroupIds(new ADObjectId[]
				{
					adobjectId2
				});
				if (!tinyTenantGroupIds.IsNullOrEmpty<ADObjectId>())
				{
					adobjectId2 = (adobjectId = tinyTenantGroupIds[0]);
				}
				else if (this.sharedConfiguration.GetSharedRoleAssignmentPolicy().Equals(adobjectId2))
				{
					adobjectId2 = (adobjectId = this.GetTinyTenantLocalRap());
				}
			}
			IL_A1:
			if (this.RoleAssignee != null && !this.assignee.Id.Equals(adobjectId))
			{
				adobjectId = this.assignee.Id;
				if (exchangeRoleAssignment.RoleAssigneeType == RoleAssigneeType.User)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorUnknownAssignmentMethod(exchangeRoleAssignment.RoleAssigneeType.ToString())), ErrorCategory.InvalidData, null);
				}
				else
				{
					assignmentMethod = this.GetAssignmentMethod(exchangeRoleAssignment);
				}
			}
			string userName = string.Empty;
			RoleAssigneeType roleAssigneeType2 = exchangeRoleAssignment.RoleAssigneeType;
			switch (roleAssigneeType2)
			{
			case RoleAssigneeType.SecurityGroup:
				break;
			case (RoleAssigneeType)3:
				goto IL_177;
			case RoleAssigneeType.RoleAssignmentPolicy:
				userName = Strings.AllPolicyAssignees;
				goto IL_18C;
			default:
				switch (roleAssigneeType2)
				{
				case RoleAssigneeType.ForeignSecurityPrincipal:
					userName = Strings.AllForeignAccounts;
					goto IL_18C;
				case (RoleAssigneeType)9:
				case RoleAssigneeType.PartnerLinkedRoleGroup:
					goto IL_177;
				case RoleAssigneeType.RoleGroup:
					break;
				case RoleAssigneeType.LinkedRoleGroup:
					userName = Strings.AllLinkedGroupMembers;
					goto IL_18C;
				default:
					goto IL_177;
				}
				break;
			}
			userName = Strings.AllGroupMembers;
			goto IL_18C;
			IL_177:
			if (exchangeRoleAssignment.User != null)
			{
				userName = exchangeRoleAssignment.User.Name;
			}
			IL_18C:
			ExchangeRoleAssignmentPresentation result = new ExchangeRoleAssignmentPresentation(exchangeRoleAssignment, adobjectId, assignmentMethod, userName, (this.sharedConfiguration != null) ? adobjectId2 : null, (this.sharedConfiguration != null) ? this.sharedConfiguration.SharedConfigurationCU.OrganizationId : null);
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000F0264 File Offset: 0x000EE464
		private void InitializeWritableReportingObjectIfNecessary()
		{
			if (this.WritableServer != null)
			{
				this.writableObject = (Server)base.GetDataObject<Server>(this.WritableServer, this.ConfigurationSession, null, new LocalizedString?(Strings.WritableServerNotFound(this.WritableServer.ToString())), new LocalizedString?(Strings.WritableServerNotUnique(this.WritableServer.ToString())));
				this.writableObjectType = GetManagementRoleAssignment.WritableObjectType.Server;
			}
			if (this.WritableDatabase != null)
			{
				this.writableObject = (Database)base.GetDataObject<Database>(this.WritableDatabase, this.ConfigurationSession, null, new LocalizedString?(Strings.WritableDatabaseNotFound(this.WritableDatabase.ToString())), new LocalizedString?(Strings.WritableDatabaseNotUnique(this.WritableDatabase.ToString())));
				this.writableObjectType = GetManagementRoleAssignment.WritableObjectType.Database;
			}
			if (this.WritableRecipient != null)
			{
				this.writableObject = (ADRecipient)base.GetDataObject<ADRecipient>(this.WritableRecipient, base.TenantGlobalCatalogSession, null, GetManagementRoleAssignment.AllowedRecipientTypes, new LocalizedString?(Strings.WritableRecipientNotFound(this.WritableRecipient.ToString())), new LocalizedString?(Strings.WritableRecipientNotUnique(this.WritableRecipient.ToString())));
				this.writableObjectType = GetManagementRoleAssignment.WritableObjectType.Recipient;
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000F037B File Offset: 0x000EE57B
		private void InitializeManagementReportingIfNecessary()
		{
			if (this.managementReporting == null)
			{
				this.managementReporting = new ManagementReporting(base.DataSession as IConfigurationSession, base.TenantGlobalCatalogSession, base.CurrentOrganizationId, this.sharedConfiguration, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000F03BC File Offset: 0x000EE5BC
		private List<ADObjectId> GetSharedTenantAssigneeIds()
		{
			List<ADObjectId> list = new List<ADObjectId>(this.sharedConfiguration.SharedConfigurationCU.OtherWellKnownObjects.Count + 1);
			foreach (DNWithBinary dnwithBinary in this.sharedConfiguration.SharedConfigurationCU.OtherWellKnownObjects)
			{
				list.Add(new ADObjectId(dnwithBinary.DistinguishedName));
			}
			list.Add(this.sharedConfiguration.GetSharedRoleAssignmentPolicy());
			return list;
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000F0454 File Offset: 0x000EE654
		private ADObjectId GetTinyTenantLocalRap()
		{
			if (this.tinyTenantLocalRAP == null)
			{
				RoleAssignmentPolicy[] array = (this.sharedSystemConfigSession ?? this.ConfigurationSession).Find<RoleAssignmentPolicy>(null, QueryScope.SubTree, null, null, 1);
				this.tinyTenantLocalRAP = array[0].Id;
			}
			return this.tinyTenantLocalRAP;
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000F0498 File Offset: 0x000EE698
		private void ReScopeSharedConfigAndSharedSessionIfNecessary(OrganizationId organizationId)
		{
			if (this.IgnoreDehydratedFlag)
			{
				return;
			}
			this.sharedConfiguration = SharedConfiguration.GetSharedConfiguration(organizationId);
			if (this.sharedConfiguration != null)
			{
				this.sharedSystemConfigSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, this.sharedConfiguration.GetSharedConfigurationSessionSettings(), 1367, "ReScopeSharedConfigAndSharedSessionIfNecessary", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RBAC\\RoleAssignment\\GetManagementRoleAssignment.cs");
			}
		}

		// Token: 0x040025F9 RID: 9721
		private const string ParameterSetRole = "Role";

		// Token: 0x040025FA RID: 9722
		private const string ParameterSetRoleAssignee = "RoleAssignee";

		// Token: 0x040025FB RID: 9723
		private static bool IsDatacenter = Datacenter.IsMicrosoftHostedOnly(true);

		// Token: 0x040025FC RID: 9724
		private QueryFilter internalFilterForNonPipelineParameter;

		// Token: 0x040025FD RID: 9725
		private QueryFilter internalFilterForPipelineParameter;

		// Token: 0x040025FE RID: 9726
		private ADObject assignee;

		// Token: 0x040025FF RID: 9727
		private List<ADObjectId> assigneeIds;

		// Token: 0x04002600 RID: 9728
		private ADObject anyRole;

		// Token: 0x04002601 RID: 9729
		private ManagementReporting managementReporting;

		// Token: 0x04002602 RID: 9730
		private ADObject writableObject;

		// Token: 0x04002603 RID: 9731
		private GetManagementRoleAssignment.WritableObjectType writableObjectType = GetManagementRoleAssignment.WritableObjectType.NotApplicable;

		// Token: 0x04002604 RID: 9732
		private RoleAssignmentExpansion roleAssignmentExpansion;

		// Token: 0x04002605 RID: 9733
		private SharedConfiguration sharedConfiguration;

		// Token: 0x04002606 RID: 9734
		private IConfigurationSession sharedSystemConfigSession;

		// Token: 0x04002607 RID: 9735
		private ADObjectId tinyTenantLocalRAP;

		// Token: 0x04002608 RID: 9736
		private static readonly AssignmentMethod[] AllowedAssignmentMethods = new AssignmentMethod[]
		{
			Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.Direct,
			Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.SecurityGroup,
			Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.MailboxPlan,
			Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleAssignmentPolicy,
			Microsoft.Exchange.Data.Directory.SystemConfiguration.AssignmentMethod.RoleGroup
		};

		// Token: 0x04002609 RID: 9737
		private static readonly OptionalIdentityData AllowedRecipientTypes = new OptionalIdentityData
		{
			AdditionalFilter = QueryFilter.OrTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.User),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.UserMailbox),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.Contact),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailContact),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.SystemMailbox)
			})
		};

		// Token: 0x02000660 RID: 1632
		private enum WritableObjectType
		{
			// Token: 0x0400260C RID: 9740
			NotApplicable = 1,
			// Token: 0x0400260D RID: 9741
			Recipient = 3,
			// Token: 0x0400260E RID: 9742
			Server,
			// Token: 0x0400260F RID: 9743
			Database = 6
		}
	}
}
