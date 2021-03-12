using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F7 RID: 247
	[Cmdlet("New", "UMCallAnsweringRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewUMCallAnsweringRule : NewTenantADTaskBase<UMCallAnsweringRule>
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00042BB3 File Offset: 0x00040DB3
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00042BCA File Offset: 0x00040DCA
		[Parameter(Mandatory = false)]
		[ValidateNotNull]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00042BDD File Offset: 0x00040DDD
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00042BF4 File Offset: 0x00040DF4
		[Parameter]
		[ValidateNotNull]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00042C07 File Offset: 0x00040E07
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00042C14 File Offset: 0x00040E14
		[ValidateNotNullOrEmpty]
		[Parameter]
		public MultiValuedProperty<CallerIdItem> CallerIds
		{
			get
			{
				return this.DataObject.CallerIds;
			}
			set
			{
				this.DataObject.CallerIds = value;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00042C22 File Offset: 0x00040E22
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00042C2F File Offset: 0x00040E2F
		[Parameter]
		public bool CallersCanInterruptGreeting
		{
			get
			{
				return this.DataObject.CallersCanInterruptGreeting;
			}
			set
			{
				this.DataObject.CallersCanInterruptGreeting = value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00042C3D File Offset: 0x00040E3D
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x00042C4A File Offset: 0x00040E4A
		[Parameter]
		public bool CheckAutomaticReplies
		{
			get
			{
				return this.DataObject.CheckAutomaticReplies;
			}
			set
			{
				this.DataObject.CheckAutomaticReplies = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00042C58 File Offset: 0x00040E58
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x00042C65 File Offset: 0x00040E65
		public bool Enabled
		{
			get
			{
				return this.DataObject.Enabled;
			}
			internal set
			{
				this.DataObject.Enabled = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00042C73 File Offset: 0x00040E73
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00042C80 File Offset: 0x00040E80
		[ValidateNotNullOrEmpty]
		[Parameter]
		public MultiValuedProperty<string> ExtensionsDialed
		{
			get
			{
				return this.DataObject.ExtensionsDialed;
			}
			set
			{
				this.DataObject.ExtensionsDialed = value;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00042C8E File Offset: 0x00040E8E
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00042C9B File Offset: 0x00040E9B
		[ValidateNotNullOrEmpty]
		[Parameter]
		public MultiValuedProperty<KeyMapping> KeyMappings
		{
			get
			{
				return this.DataObject.KeyMappings;
			}
			set
			{
				this.DataObject.KeyMappings = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00042CA9 File Offset: 0x00040EA9
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00042CB6 File Offset: 0x00040EB6
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00042CC4 File Offset: 0x00040EC4
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00042CD1 File Offset: 0x00040ED1
		[Parameter]
		public int Priority
		{
			get
			{
				return this.DataObject.Priority;
			}
			set
			{
				this.DataObject.Priority = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00042CDF File Offset: 0x00040EDF
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00042CEC File Offset: 0x00040EEC
		[Parameter]
		public int ScheduleStatus
		{
			get
			{
				return this.DataObject.ScheduleStatus;
			}
			set
			{
				this.DataObject.ScheduleStatus = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00042CFA File Offset: 0x00040EFA
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00042D07 File Offset: 0x00040F07
		[Parameter]
		[ValidateNotNull]
		public TimeOfDay TimeOfDay
		{
			get
			{
				return this.DataObject.TimeOfDay;
			}
			set
			{
				this.DataObject.TimeOfDay = value;
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00042D20 File Offset: 0x00040F20
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 173, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\um\\UMCallAnsweringRule\\NewUMCallAnsweringRule.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00042DD2 File Offset: 0x00040FD2
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewCallAnsweringRule(this.Name);
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00042DE0 File Offset: 0x00040FE0
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId executingUserId;
			base.TryGetExecutingUserId(out executingUserId);
			return UMCallAnsweringRuleUtils.GetDataProviderForCallAnsweringRuleTasks(null, this.Mailbox, base.SessionSettings, base.TenantGlobalCatalogSession, executingUserId, "new-callansweringrule", new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00042E2C File Offset: 0x0004102C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			UMCallAnsweringRuleDataProvider umcallAnsweringRuleDataProvider = (UMCallAnsweringRuleDataProvider)base.DataSession;
			umcallAnsweringRuleDataProvider.ValidateUMCallAnsweringRuleProperties(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00042E63 File Offset: 0x00041063
		protected override bool IsKnownException(Exception exception)
		{
			return UMCallAnsweringRuleUtils.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00042E76 File Offset: 0x00041076
		protected override void InternalStateReset()
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.InternalStateReset();
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00042E89 File Offset: 0x00041089
		protected override void Dispose(bool disposing)
		{
			UMCallAnsweringRuleUtils.DisposeCallAnsweringRuleDataProvider(base.DataSession);
			base.Dispose(disposing);
		}
	}
}
