using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000227 RID: 551
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Remove", "SecondaryDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveSecondaryDomainTask : SecondaryDomainTaskBase
	{
		// Token: 0x060012B7 RID: 4791 RVA: 0x000523BC File Offset: 0x000505BC
		public RemoveSecondaryDomainTask()
		{
			base.Fields["InstallationMode"] = InstallationModes.Uninstall;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000523DA File Offset: 0x000505DA
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x000523F1 File Offset: 0x000505F1
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public AcceptedDomainIdParameter Identity
		{
			get
			{
				return (AcceptedDomainIdParameter)base.Fields["SecondaryDomainIdentity"];
			}
			set
			{
				base.Fields["SecondaryDomainIdentity"] = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00052404 File Offset: 0x00050604
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x0005242A File Offset: 0x0005062A
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipRecipients
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipRecipients"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipRecipients"] = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00052442 File Offset: 0x00050642
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00052468 File Offset: 0x00050668
		[Parameter]
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

		// Token: 0x060012BE RID: 4798 RVA: 0x00052480 File Offset: 0x00050680
		protected override bool ShouldExecuteComponentTasks()
		{
			return true;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00052483 File Offset: 0x00050683
		protected override LocalizedString Description
		{
			get
			{
				return Strings.RemoveSecondaryDomainDescription;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0005248A File Offset: 0x0005068A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveSecondaryDomain(this.Identity.ToString());
			}
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0005249C File Offset: 0x0005069C
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new RemoveSecondaryDomainTaskModuleFactory();
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000524A4 File Offset: 0x000506A4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.acceptedDomain = OrganizationTaskHelper.GetAcceptedDomain(this.Identity, base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
			this.organizationIdParam = new OrganizationIdParameter(this.acceptedDomain.OrganizationId);
			if (!OrganizationTaskHelper.CanProceedWithOrganizationTask(this.organizationIdParam, base.Session, RemoveSecondaryDomainTask.IgnorableFlagsOnStatusTimeout, new Task.TaskErrorLoggingDelegate(base.WriteError)))
			{
				base.WriteError(new OrganizationPendingOperationException(Strings.ErrorCannotOperateOnOrgInCurrentState), ErrorCategory.InvalidOperation, null);
			}
			RemoveAcceptedDomain.CheckDomainForRemoval(this.acceptedDomain, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00052588 File Offset: 0x00050788
		protected override void FilterComponents()
		{
			base.FilterComponents();
			if (this.SkipRecipients)
			{
				foreach (SetupComponentInfo setupComponentInfo in base.ComponentInfoList)
				{
					setupComponentInfo.Tasks.RemoveAll(delegate(TaskInfo taskInfo)
					{
						OrgTaskInfo orgTaskInfo = taskInfo as OrgTaskInfo;
						return orgTaskInfo != null && orgTaskInfo.Uninstall != null && orgTaskInfo.Uninstall.Tenant != null && orgTaskInfo.Uninstall.Tenant.RecipientOperation;
					});
				}
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00052610 File Offset: 0x00050810
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Fields["PrimaryOrganization"] = this.acceptedDomain.OrganizationId.OrganizationalUnit.DistinguishedName;
			base.Fields["SecondarySmtpDomainName"] = this.acceptedDomain.DomainName.ToString();
			ExchangeConfigurationUnit exchangeConfigUnitFromOrganizationId = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(this.organizationIdParam, base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
			base.Fields["TenantExternalDirectoryOrganizationId"] = exchangeConfigUnitFromOrganizationId.ExternalDirectoryOrganizationId;
			IConfigurationSession session = (IConfigurationSession)TaskHelper.UnderscopeSessionToOrganization(base.Session, exchangeConfigUnitFromOrganizationId.OrganizationId, true);
			if (exchangeConfigUnitFromOrganizationId.OrganizationStatus == OrganizationStatus.PendingAcceptedDomainAddition || exchangeConfigUnitFromOrganizationId.OrganizationStatus == OrganizationStatus.PendingAcceptedDomainRemoval)
			{
				OrganizationTaskHelper.SetOrganizationStatus(this.organizationIdParam, session, OrganizationStatus.Active, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x040007F4 RID: 2036
		private OrganizationIdParameter organizationIdParam;

		// Token: 0x040007F5 RID: 2037
		private AcceptedDomain acceptedDomain;

		// Token: 0x040007F6 RID: 2038
		private static readonly OrganizationStatus[] IgnorableFlagsOnStatusTimeout = new OrganizationStatus[]
		{
			OrganizationStatus.PendingAcceptedDomainAddition,
			OrganizationStatus.PendingAcceptedDomainRemoval,
			OrganizationStatus.ReadyForRemoval,
			OrganizationStatus.SoftDeleted
		};
	}
}
