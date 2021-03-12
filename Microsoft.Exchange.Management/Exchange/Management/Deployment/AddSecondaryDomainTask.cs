using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200016D RID: 365
	[Cmdlet("Add", "SecondaryDomain", SupportsShouldProcess = true, DefaultParameterSetName = "OrgScopedParameterSet")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class AddSecondaryDomainTask : SecondaryDomainTaskBase
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003EDAB File Offset: 0x0003CFAB
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x0003EDC2 File Offset: 0x0003CFC2
		private ExchangeConfigurationUnit TenantCU
		{
			get
			{
				return (ExchangeConfigurationUnit)base.Fields["TenantCU"];
			}
			set
			{
				base.Fields["TenantCU"] = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003EDD5 File Offset: 0x0003CFD5
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0003EDF6 File Offset: 0x0003CFF6
		[Parameter(Mandatory = false)]
		public AuthenticationType AuthenticationType
		{
			get
			{
				return (AuthenticationType)(base.Fields["AuthenticationType"] ?? AuthenticationType.Managed);
			}
			set
			{
				base.Fields["AuthenticationType"] = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0003EE0E File Offset: 0x0003D00E
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0003EE2F File Offset: 0x0003D02F
		[Parameter(Mandatory = false)]
		public LiveIdInstanceType LiveIdInstanceType
		{
			get
			{
				return (LiveIdInstanceType)(base.Fields["LiveIdInstanceType"] ?? LiveIdInstanceType.Consumer);
			}
			set
			{
				base.Fields["LiveIdInstanceType"] = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0003EE47 File Offset: 0x0003D047
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0003EE4F File Offset: 0x0003D04F
		[Parameter(Mandatory = false)]
		public bool OutBoundOnly { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0003EE58 File Offset: 0x0003D058
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x0003EE60 File Offset: 0x0003D060
		[Parameter(Mandatory = false)]
		public bool MakeDefault { get; set; }

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003EE69 File Offset: 0x0003D069
		public AddSecondaryDomainTask()
		{
			base.Fields["InstallationMode"] = InstallationModes.Install;
			base.Fields["PrepareOrganization"] = true;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0003EE9D File Offset: 0x0003D09D
		protected override LocalizedString Description
		{
			get
			{
				return Strings.AddSecondaryDomainDescription;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddSecondaryDomain(this.Organization.ToString(), this.DomainName.ToString());
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003EEC1 File Offset: 0x0003D0C1
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0003EED8 File Offset: 0x0003D0D8
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "DefaultParameterSet")]
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "OrgScopedParameterSet")]
		public string Name
		{
			get
			{
				return (string)base.Fields["SecondaryDomainName"];
			}
			set
			{
				base.Fields["SecondaryDomainName"] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003EEEB File Offset: 0x0003D0EB
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0003EF02 File Offset: 0x0003D102
		[Parameter(Mandatory = true, ParameterSetName = "DefaultParameterSet")]
		[Parameter(Mandatory = true, ParameterSetName = "OrgScopedParameterSet")]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["SecondarySmtpDomainName"];
			}
			set
			{
				base.Fields["SecondarySmtpDomainName"] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0003EF15 File Offset: 0x0003D115
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0003EF2C File Offset: 0x0003D12C
		[Parameter(Mandatory = true, ParameterSetName = "DefaultParameterSet")]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["PrimaryOrganization"];
			}
			set
			{
				base.Fields["PrimaryOrganization"] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003EF3F File Offset: 0x0003D13F
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0003EF65 File Offset: 0x0003D165
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "OrgScopedParameterSet")]
		public SwitchParameter DomainOwnershipVerified
		{
			get
			{
				return (SwitchParameter)(base.Fields["DomainOwnershipVerified"] ?? false);
			}
			set
			{
				base.Fields["DomainOwnershipVerified"] = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0003EF7D File Offset: 0x0003D17D
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0003EF9E File Offset: 0x0003D19E
		[Parameter(Mandatory = false, ParameterSetName = "DefaultParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "OrgScopedParameterSet")]
		public AcceptedDomainType DomainType
		{
			get
			{
				return (AcceptedDomainType)(base.Fields["DomainType"] ?? AcceptedDomainType.Authoritative);
			}
			set
			{
				base.Fields["DomainType"] = value;
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0003EFB6 File Offset: 0x0003D1B6
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new AddSecondaryDomainTaskModuleFactory();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003EFC0 File Offset: 0x0003D1C0
		private void WriteWrappedError(Exception exception, ErrorCategory category, object target)
		{
			OrganizationValidationException exception2 = new OrganizationValidationException(Strings.ErrorValidationException(exception.ToString()), exception);
			base.WriteError(exception2, category, target);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0003EFE8 File Offset: 0x0003D1E8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			LocalizedString empty = LocalizedString.Empty;
			this.Name = MailboxTaskHelper.GetNameOfAcceptableLengthForMultiTenantMode(this.Name, out empty);
			if (empty != LocalizedString.Empty)
			{
				this.WriteWarning(empty);
			}
			base.InternalBeginProcessing();
			if (this.Organization == null)
			{
				if (base.CurrentOrganizationId == OrganizationId.ForestWideOrgId)
				{
					base.WriteError(new ArgumentException(Strings.ErrorOrganizationParameterRequired), ErrorCategory.InvalidOperation, null);
				}
				else
				{
					this.Organization = new OrganizationIdParameter(base.CurrentOrganizationId.OrganizationalUnit.Name);
				}
			}
			base.CurrentOrganizationId = this.ResolveCurrentOrganization();
			OrganizationTaskHelper.ValidateParamString("Name", this.Name, new Task.TaskErrorLoggingDelegate(base.WriteError));
			if (base.Fields["AuthenticationType"] == null)
			{
				this.AuthenticationType = AuthenticationType.Managed;
			}
			if (base.Fields["LiveIdInstanceType"] == null)
			{
				this.LiveIdInstanceType = LiveIdInstanceType.Consumer;
			}
			base.Fields["OutBoundOnly"] = this.OutBoundOnly;
			base.Fields["MakeDefault"] = this.MakeDefault;
			string value = string.Empty;
			if (this.Organization != null)
			{
				PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(this.Organization.RawIdentity);
				if (partitionIdByAcceptedDomainName != null)
				{
					value = base.ServerSettings.PreferredGlobalCatalog(partitionIdByAcceptedDomainName.ForestFQDN);
				}
			}
			base.Fields["PreferredServer"] = value;
			TaskLogger.LogExit();
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003F160 File Offset: 0x0003D360
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.CheckForDuplicateExistingDomain();
			AcceptedDomain acceptedDomain = new AcceptedDomain();
			acceptedDomain.Name = this.Name;
			acceptedDomain.DomainName = new SmtpDomainWithSubdomains(this.DomainName, false);
			acceptedDomain.DomainType = this.DomainType;
			acceptedDomain.SetId(base.Session, this.DomainName.ToString());
			acceptedDomain.OrganizationId = base.CurrentOrganizationId;
			NewAcceptedDomain.ValidateDomainName(acceptedDomain, new Task.TaskErrorLoggingDelegate(this.WriteWrappedError));
			if (!this.DomainOwnershipVerified && this.AuthenticationType == AuthenticationType.Federated)
			{
				bool flag = false;
				AcceptedDomainIdParameter acceptedDomainIdParameter = AcceptedDomainIdParameter.Parse("*");
				IEnumerable<AcceptedDomain> objects = acceptedDomainIdParameter.GetObjects<AcceptedDomain>(this.TenantCU.Id, base.Session);
				foreach (AcceptedDomain acceptedDomain2 in objects)
				{
					SmtpDomainWithSubdomains smtpDomainWithSubdomains = new SmtpDomainWithSubdomains(acceptedDomain2.DomainName.Domain.ToString(), true);
					if (smtpDomainWithSubdomains.Match(this.DomainName.ToString()) == acceptedDomain2.DomainName.Domain.Length)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					base.WriteError(new OrganizationTaskException(Strings.ErrorTenantAdminsCanOnlyAddSubdomains(this.DomainName.ToString())), ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003F2C8 File Offset: 0x0003D4C8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.TenantCU.OrganizationStatus == OrganizationStatus.PendingAcceptedDomainAddition || this.TenantCU.OrganizationStatus == OrganizationStatus.PendingAcceptedDomainRemoval)
			{
				OrganizationTaskHelper.SetOrganizationStatus(base.Session, this.TenantCU, OrganizationStatus.Active);
			}
			try
			{
				base.InternalProcessRecord();
			}
			catch (Exception)
			{
				this.CleanupSecondaryDomain();
				throw;
			}
			if (!base.HasErrors)
			{
				AcceptedDomain acceptedDomain = OrganizationTaskHelper.GetAcceptedDomain(AcceptedDomainIdParameter.Parse(this.Name), base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
				base.WriteObject(acceptedDomain);
			}
			else
			{
				this.CleanupSecondaryDomain();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003F36C File Offset: 0x0003D56C
		private void CheckForDuplicateExistingDomain()
		{
			AcceptedDomain acceptedDomain = OrganizationTaskHelper.GetAcceptedDomain(AcceptedDomainIdParameter.Parse(this.Name), base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), false);
			if (acceptedDomain != null)
			{
				base.WriteError(new ManagementObjectAlreadyExistsException(Strings.ErrorAcceptedDomainExists(this.Name)), ErrorCategory.ResourceExists, null);
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003F3BC File Offset: 0x0003D5BC
		private void CleanupSecondaryDomain()
		{
			AcceptedDomain acceptedDomain = OrganizationTaskHelper.GetAcceptedDomain(AcceptedDomainIdParameter.Parse(this.Name), base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), false);
			if (acceptedDomain != null)
			{
				try
				{
					base.Session.Delete(acceptedDomain);
				}
				catch (Exception ex)
				{
					this.WriteWarning(Strings.ErrorNonActiveOrganizationFound(ex.ToString()));
				}
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003F424 File Offset: 0x0003D624
		internal override IConfigurationSession CreateSession()
		{
			PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(this.Organization.RawIdentity);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerId(partitionIdByAcceptedDomainName.ForestFQDN, null, null), base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.RescopeToSubtree(sessionSettings), 480, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\AddSecondaryDomainTask.cs");
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003F494 File Offset: 0x0003D694
		private OrganizationId ResolveCurrentOrganization()
		{
			base.Session = this.CreateSession();
			if (!OrganizationTaskHelper.CanProceedWithOrganizationTask(this.Organization, base.Session, AddSecondaryDomainTask.IgnorableFlagsOnStatusTimeout, new Task.TaskErrorLoggingDelegate(base.WriteError)))
			{
				base.WriteError(new OrganizationPendingOperationException(Strings.ErrorCannotOperateOnOrgInCurrentState), ErrorCategory.InvalidOperation, null);
			}
			base.Session.UseConfigNC = false;
			ADOrganizationalUnit oufromOrganizationId = OrganizationTaskHelper.GetOUFromOrganizationId(this.Organization, base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
			base.Session.UseConfigNC = true;
			this.TenantCU = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(this.Organization, base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
			base.Fields["TenantExternalDirectoryOrganizationId"] = this.TenantCU.ExternalDirectoryOrganizationId;
			return oufromOrganizationId.OrganizationId;
		}

		// Token: 0x04000698 RID: 1688
		private const string OrgScopedParameterSet = "OrgScopedParameterSet";

		// Token: 0x04000699 RID: 1689
		private const string DefaultParameterSet = "DefaultParameterSet";

		// Token: 0x0400069A RID: 1690
		private static readonly OrganizationStatus[] IgnorableFlagsOnStatusTimeout = new OrganizationStatus[]
		{
			OrganizationStatus.PendingAcceptedDomainAddition,
			OrganizationStatus.PendingAcceptedDomainRemoval
		};
	}
}
