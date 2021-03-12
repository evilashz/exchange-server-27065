using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CF9 RID: 3321
	[Cmdlet("Test", "ExchangeNotification", SupportsShouldProcess = true)]
	public sealed class TestExchangeNotification : DataAccessTask<ExchangeConfigurationUnit>
	{
		// Token: 0x170027A0 RID: 10144
		// (get) Token: 0x06007FB3 RID: 32691 RVA: 0x00209C4F File Offset: 0x00207E4F
		// (set) Token: 0x06007FB4 RID: 32692 RVA: 0x00209C66 File Offset: 0x00207E66
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true)]
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

		// Token: 0x170027A1 RID: 10145
		// (get) Token: 0x06007FB5 RID: 32693 RVA: 0x00209C7C File Offset: 0x00207E7C
		// (set) Token: 0x06007FB6 RID: 32694 RVA: 0x00209CBA File Offset: 0x00207EBA
		[ValidateCount(0, 100)]
		[Parameter(Mandatory = false)]
		public string[] InsertionStrings
		{
			get
			{
				string[] result;
				if ((result = (string[])base.Fields["InsertionStrings"]) == null)
				{
					result = new string[]
					{
						string.Empty,
						string.Empty
					};
				}
				return result;
			}
			set
			{
				base.Fields["InsertionStrings"] = value;
			}
		}

		// Token: 0x170027A2 RID: 10146
		// (get) Token: 0x06007FB7 RID: 32695 RVA: 0x00209CCD File Offset: 0x00207ECD
		// (set) Token: 0x06007FB8 RID: 32696 RVA: 0x00209CED File Offset: 0x00207EED
		[Parameter(Mandatory = false)]
		public string PeriodicKey
		{
			get
			{
				return ((string)base.Fields["PeriodicKey"]) ?? string.Empty;
			}
			set
			{
				base.Fields["PeriodicKey"] = value;
			}
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x00209D00 File Offset: 0x00207F00
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			OrganizationId organizationId = this.FindOrganizationId();
			ExEventLog exEventLog = new ExEventLog(TestExchangeNotification.ComponentId, "MSExchange Common");
			exEventLog.LogEvent(organizationId, CommonEventLogConstants.Tuple_TenantMonitoringTestEvent, this.PeriodicKey, this.InsertionStrings);
			base.WriteObject(new MonitoringData
			{
				Events = 
				{
					this.CreateSuccessEvent()
				}
			});
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x00209D64 File Offset: 0x00207F64
		protected override IConfigDataProvider CreateSession()
		{
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerIdForLocalForest, base.CurrentOrganizationId ?? base.ExecutingUserOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 133, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\TenantMonitoring\\TestExchangeNotification.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = true;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = false;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06007FBB RID: 32699 RVA: 0x00209DC0 File Offset: 0x00207FC0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.TenantNotificationTestConfirmationPrompt(this.Organization.ToString());
			}
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x00209DD4 File Offset: 0x00207FD4
		private OrganizationId FindOrganizationId()
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = (ExchangeConfigurationUnit)base.GetDataObject<ExchangeConfigurationUnit>(this.Organization, base.DataSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
			if (exchangeConfigurationUnit.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				this.WriteError(new TenantNotificationTestFirstOrgNotSupportedException(), (ErrorCategory)1000, this, true);
				return null;
			}
			return exchangeConfigurationUnit.OrganizationId;
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x00209E50 File Offset: 0x00208050
		private MonitoringEvent CreateSuccessEvent()
		{
			string arg = string.Empty;
			if (this.InsertionStrings != null && this.InsertionStrings.Length > 0)
			{
				arg = string.Join(", ", this.InsertionStrings);
			}
			return new MonitoringEvent("MSExchange Monitoring ExchangeNotification", 100, EventTypeEnumeration.Success, string.Format("Test event with periodic-key='{0}' and insertion-strings='{1}' was successfully logged.", this.PeriodicKey, arg));
		}

		// Token: 0x04003EA1 RID: 16033
		private const int MonitoringEventSuccessId = 100;

		// Token: 0x04003EA2 RID: 16034
		private const string MonitoringEventSource = "MSExchange Monitoring ExchangeNotification";

		// Token: 0x04003EA3 RID: 16035
		private static readonly Guid ComponentId = new Guid("2f07db75-cff9-4e7e-9195-e8c1991aa251");
	}
}
