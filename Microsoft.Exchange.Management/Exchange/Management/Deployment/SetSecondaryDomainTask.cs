using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000254 RID: 596
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Set", "SecondaryDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class SetSecondaryDomainTask : SecondaryDomainTaskBase
	{
		// Token: 0x06001663 RID: 5731 RVA: 0x0005D969 File Offset: 0x0005BB69
		public SetSecondaryDomainTask()
		{
			base.Fields["InstallationMode"] = InstallationModes.BuildToBuildUpgrade;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0005D987 File Offset: 0x0005BB87
		protected override bool ShouldExecuteComponentTasks()
		{
			return true;
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0005D98A File Offset: 0x0005BB8A
		protected override LocalizedString Description
		{
			get
			{
				return Strings.SetSecondaryDomainDescription;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0005D991 File Offset: 0x0005BB91
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetSecondaryDomain(this.Identity.ToString());
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005D9A3 File Offset: 0x0005BBA3
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0005D9BA File Offset: 0x0005BBBA
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

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0005D9CD File Offset: 0x0005BBCD
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0005D9EE File Offset: 0x0005BBEE
		[Parameter(Mandatory = true)]
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

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x0005DA06 File Offset: 0x0005BC06
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x0005DA27 File Offset: 0x0005BC27
		[Parameter(Mandatory = false)]
		public bool OutBoundOnly
		{
			get
			{
				return (bool)(base.Fields["OutBoundOnly"] ?? false);
			}
			set
			{
				base.Fields["OutBoundOnly"] = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0005DA3F File Offset: 0x0005BC3F
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0005DA60 File Offset: 0x0005BC60
		[Parameter(Mandatory = false)]
		public bool MakeDefault
		{
			get
			{
				return (bool)(base.Fields["MakeDefault"] ?? false);
			}
			set
			{
				base.Fields["MakeDefault"] = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0005DA78 File Offset: 0x0005BC78
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0005DA99 File Offset: 0x0005BC99
		private bool PartnerMode
		{
			get
			{
				return (bool)(base.Fields["PartnerMode"] ?? false);
			}
			set
			{
				base.Fields["PartnerMode"] = value;
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0005DAB1 File Offset: 0x0005BCB1
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.ExchangeRunspaceConfig != null)
			{
				this.PartnerMode = base.ExchangeRunspaceConfig.PartnerMode;
			}
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0005DAD4 File Offset: 0x0005BCD4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.acceptedDomain = OrganizationTaskHelper.GetAcceptedDomain(this.Identity, base.Session, new Task.TaskErrorLoggingDelegate(base.WriteError), true);
			if (this.acceptedDomain.PendingRemoval)
			{
				base.WriteError(new CannotOperateOnAcceptedDomainPendingRemovalException(this.acceptedDomain.DomainName.ToString()), ErrorCategory.InvalidOperation, null);
			}
			this.orgOuDN = this.acceptedDomain.OrganizationId.OrganizationalUnit.DistinguishedName;
			this.organizationIdParam = new OrganizationIdParameter(this.orgOuDN);
			if (!OrganizationTaskHelper.CanProceedWithOrganizationTask(this.organizationIdParam, base.Session, SetSecondaryDomainTask.IgnorableFlagsOnStatusTimeout, new Task.TaskErrorLoggingDelegate(base.WriteError)))
			{
				base.WriteError(new OrganizationPendingOperationException(Strings.ErrorCannotOperateOnOrgInCurrentState), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0005DBA4 File Offset: 0x0005BDA4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Fields["PrimaryOrganization"] = this.orgOuDN;
			base.Fields["SecondarySmtpDomainName"] = this.acceptedDomain.DomainName.ToString();
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x040009B1 RID: 2481
		private OrganizationIdParameter organizationIdParam;

		// Token: 0x040009B2 RID: 2482
		private AcceptedDomain acceptedDomain;

		// Token: 0x040009B3 RID: 2483
		private string orgOuDN;

		// Token: 0x040009B4 RID: 2484
		private static readonly OrganizationStatus[] IgnorableFlagsOnStatusTimeout = new OrganizationStatus[]
		{
			OrganizationStatus.PendingAcceptedDomainAddition,
			OrganizationStatus.PendingAcceptedDomainRemoval,
			OrganizationStatus.Suspended,
			OrganizationStatus.LockedOut
		};
	}
}
