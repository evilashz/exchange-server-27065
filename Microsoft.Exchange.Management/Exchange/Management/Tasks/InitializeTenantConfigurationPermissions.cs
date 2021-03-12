using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002C9 RID: 713
	[Cmdlet("Initialize", "TenantConfigurationPermissions", SupportsShouldProcess = true)]
	public sealed class InitializeTenantConfigurationPermissions : SetupTaskBase
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x000700A8 File Offset: 0x0006E2A8
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x000700B0 File Offset: 0x0006E2B0
		[Parameter(Mandatory = true)]
		public override OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000700BC File Offset: 0x0006E2BC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			SecurityIdentifier identity = new SecurityIdentifier("AU");
			List<ActiveDirectoryAccessRule> list = new List<ActiveDirectoryAccessRule>();
			list.Add(new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.ListChildren, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All));
			if (base.ShouldProcess(this.addressListsContainer.DistinguishedName, Strings.InfoProcessAction(this.addressListsContainer.DistinguishedName), null))
			{
				DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, this.addressListsContainer, list.ToArray());
			}
			if (base.ShouldProcess(this.offlineAddressListsContainer.DistinguishedName, Strings.InfoProcessAction(this.offlineAddressListsContainer.DistinguishedName), null))
			{
				ActiveDirectoryAccessRule[] aces = new ActiveDirectoryAccessRule[]
				{
					new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, WellKnownGuid.DownloadOABExtendedRightGuid, ActiveDirectorySecurityInheritance.All)
				};
				DirectoryCommon.SetAces(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), null, this.offlineAddressListsContainer, aces);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0007019C File Offset: 0x0006E39C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.tenantCU = this.configurationSession.Read<ExchangeConfigurationUnit>(this.organization.ConfigurationUnit);
			if (this.tenantCU == null)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorOrganizationNotFound(this.organization.ConfigurationUnit.ToString())), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.tenantCU);
			ADObjectId childId = this.tenantCU.Id.GetChildId("Address Lists Container");
			this.addressListsContainer = this.configurationSession.Read<Container>(childId);
			if (this.addressListsContainer == null)
			{
				base.ThrowTerminatingError(new DirectoryObjectNotFoundException(childId.DistinguishedName), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.addressListsContainer);
			ADObjectId childId2 = childId.GetChildId("Offline Address Lists");
			this.offlineAddressListsContainer = this.configurationSession.Read<Container>(childId2);
			if (this.offlineAddressListsContainer == null)
			{
				base.ThrowTerminatingError(new DirectoryObjectNotFoundException(childId2.DistinguishedName), ErrorCategory.InvalidData, null);
			}
			base.LogReadObject(this.offlineAddressListsContainer);
			TaskLogger.LogExit();
		}

		// Token: 0x04000AF4 RID: 2804
		private Container addressListsContainer;

		// Token: 0x04000AF5 RID: 2805
		private Container offlineAddressListsContainer;

		// Token: 0x04000AF6 RID: 2806
		private ExchangeConfigurationUnit tenantCU;
	}
}
