using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200064A RID: 1610
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public abstract class InstallCannedRbacObjectsTaskBase : SetupTaskBase
	{
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000E9130 File Offset: 0x000E7330
		// (set) Token: 0x0600384C RID: 14412 RVA: 0x000E9138 File Offset: 0x000E7338
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "Organization")]
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

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000E9141 File Offset: 0x000E7341
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000E9158 File Offset: 0x000E7358
		[Parameter(Mandatory = true, ParameterSetName = "Organization")]
		[ValidateNotNullOrEmpty]
		public ServicePlan ServicePlanSettings
		{
			get
			{
				return (ServicePlan)base.Fields["ServicePlanSettings"];
			}
			set
			{
				base.Fields["ServicePlanSettings"] = value;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000E916B File Offset: 0x000E736B
		// (set) Token: 0x06003850 RID: 14416 RVA: 0x000E918C File Offset: 0x000E738C
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public InvocationMode InvocationMode
		{
			get
			{
				return (InvocationMode)(base.Fields["InvocationMode"] ?? InvocationMode.Install);
			}
			set
			{
				base.Fields["InvocationMode"] = value;
			}
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x000E91A4 File Offset: 0x000E73A4
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			try
			{
				this.rolesContainerId = base.OrgContainerId.GetDescendantId(ExchangeRole.RdnContainer);
				this.roleAssignmentContainerId = base.OrgContainerId.GetDescendantId(ExchangeRoleAssignment.RdnContainer);
			}
			catch (OrgContainerNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			catch (TenantOrgContainerNotFoundException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ObjectNotFound, null);
			}
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000E921C File Offset: 0x000E741C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.rolesContainerId = null;
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x000E922C File Offset: 0x000E742C
		private bool FindAnyRoleAssignment()
		{
			ExchangeRoleAssignment[] array = this.configurationSession.Find<ExchangeRoleAssignment>(base.OrgContainerId, QueryScope.SubTree, null, null, 1);
			int num = 0;
			if (num >= array.Length)
			{
				return false;
			}
			ExchangeRoleAssignment exchangeRoleAssignment = array[num];
			return true;
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x000E9264 File Offset: 0x000E7464
		protected ExchangeBuild GetCurrentRBACConfigVersion(Container rbacContainer)
		{
			if (rbacContainer == null)
			{
				throw new ArgumentNullException("rbacContainer");
			}
			ExchangeBuild exchangeBuild = rbacContainer.ExchangeVersion.ExchangeBuild;
			if (!(exchangeBuild <= RbacContainer.InitialRBACBuild))
			{
				return exchangeBuild;
			}
			if (this.FindAnyRoleAssignment())
			{
				return RbacContainer.E14RTMBuild;
			}
			return RbacContainer.FirstRGRABuild;
		}

		// Token: 0x040025CA RID: 9674
		protected ADObjectId rolesContainerId;

		// Token: 0x040025CB RID: 9675
		protected ADObjectId roleAssignmentContainerId;

		// Token: 0x040025CC RID: 9676
		internal static string[] ObsoleteRoleNamesEnterprise = new string[]
		{
			"ApplicationImpersonation_Enterprise",
			"CustomScripts_Enterprise",
			"Helpdesk_Enterprise",
			"RecordsManagement_Enterprise",
			"Reset Password",
			"UMPromptManagement",
			"UmRecipientManagement",
			"UnScopedRoleManagement",
			"MyMailSubscriptions",
			"GALSynchronizationManagement",
			"Reporting"
		};

		// Token: 0x040025CD RID: 9677
		internal static string[] ObsoleteRoleNamesDatacenter = new string[]
		{
			"UnScopedRoleManagement",
			"CustomScripts"
		};

		// Token: 0x040025CE RID: 9678
		internal static string[] ObsoleteRoleNamesTenant = new string[]
		{
			"CustomScripts",
			"UMPromptManagement",
			"UmRecipientManagement"
		};

		// Token: 0x040025CF RID: 9679
		internal static string[] ObsoleteRoleNamesHosting = new string[0];

		// Token: 0x040025D0 RID: 9680
		internal static string[] ObsoleteRoleNamesHostedTenant = new string[0];

		// Token: 0x040025D1 RID: 9681
		internal static RoleNameMappingCollection RoleNameMappingEnterpriseR4 = new RoleNameMappingCollection
		{
			new RoleNameMapping("RecipientManagement", new string[]
			{
				"Distribution Groups",
				"Mail Enabled Public Folders",
				"Mail Recipient Creation",
				"Mail Recipients",
				"Message Tracking",
				"Migration",
				"Move Mailboxes",
				"Recipient Management",
				"Recipient Policies",
				"Reset Password"
			}),
			new RoleNameMapping("UmManagement", new string[]
			{
				"UM Mailboxes",
				"UM Prompts",
				"Unified Messaging"
			}),
			new RoleNameMapping("RecordsManagement", new string[]
			{
				"Audit Logs",
				"Journaling",
				"Message Tracking",
				"Retention Management",
				"Transport Rules"
			}),
			new RoleNameMapping("DiscoveryManagement", new string[]
			{
				"Legal Hold",
				"Mailbox Search"
			}),
			new RoleNameMapping("ViewOnlyOrganizationManagement", new string[]
			{
				"View-Only Recipients",
				"View-Only Configuration",
				"Monitoring"
			}),
			new RoleNameMapping("CAS Mailbox Policies", "Recipient Policies"),
			new RoleNameMapping("OrganizationManagement", new string[]
			{
				"Active Directory Permissions",
				"Address Lists",
				"Audit Logs",
				"Cmdlet Extension Agents",
				"Database Availability Groups",
				"Database Copies",
				"Databases",
				"DataCenter Operations",
				"Disaster Recovery",
				"Distribution Groups",
				"Edge Subscriptions",
				"E-Mail Address Policies",
				"Exchange Connectors",
				"Exchange Server Certificates",
				"Exchange Servers",
				"Exchange Virtual Directories",
				"Federated Sharing",
				"Information Rights Management",
				"Journaling",
				"Legal Hold",
				"Mail Enabled Public Folders",
				"Mail Recipient Creation",
				"Mail Recipients",
				"Mail Tips",
				"Mailbox Search",
				"Message Tracking",
				"Migration",
				"Monitoring",
				"Move Mailboxes",
				"Organization Client Access",
				"Organization Configuration",
				"Organization Transport Settings",
				"POP3 And IMAP4 Protocols",
				"Public Folders",
				"Receive Connectors",
				"Recipient Policies",
				"Remote and Accepted Domains",
				"Reset Password",
				"Retention Management",
				"Role Management",
				"Security Group Creation and Membership",
				"Send Connectors",
				"Supervision",
				"Support Diagnostics",
				"Transport Agents",
				"Transport Hygiene",
				"Transport Queues",
				"Transport Rules",
				"UM Mailboxes",
				"UM Prompts",
				"Unified Messaging",
				"User Options",
				"View-Only Configuration",
				"View-Only Recipients"
			}),
			new RoleNameMapping("MyOptions", new string[]
			{
				"MyBaseOptions",
				"MyProfileInformation",
				"MyMailSubscriptions",
				"MyContactInformation",
				"MyRetentionPolicies",
				"MyTextMessaging",
				"MyVoiceMail"
			}),
			new RoleNameMapping("Public Folder Replication", new string[]
			{
				"Mail Enabled Public Folders",
				"Public Folders"
			})
		};

		// Token: 0x040025D2 RID: 9682
		internal static RoleNameMappingCollection RoleNameMappingDatacenterR4 = new RoleNameMappingCollection
		{
			new RoleNameMapping("MyOptions", new string[]
			{
				"MyBaseOptions",
				"MyContactInformation",
				"MyMailSubscriptions",
				"MyProfileInformation",
				"MyRetentionPolicies",
				"MyTextMessaging",
				"MyVoiceMail"
			}),
			new RoleNameMapping("CAS Mailbox Policies", "Recipient Policies"),
			new RoleNameMapping("Public Folder Replication", new string[]
			{
				"Mail Enabled Public Folders",
				"Public Folders"
			})
		};

		// Token: 0x040025D3 RID: 9683
		internal static RoleNameMappingCollection RoleNameMappingTenantR4 = new RoleNameMappingCollection();

		// Token: 0x040025D4 RID: 9684
		internal static RoleNameMappingCollection RoleNameMappingHostingR4 = new RoleNameMappingCollection();

		// Token: 0x040025D5 RID: 9685
		internal static RoleNameMappingCollection RoleNameMappingHostedTenantR4 = new RoleNameMappingCollection();
	}
}
