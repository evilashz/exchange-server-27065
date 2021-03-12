using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Ehf;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000041 RID: 65
	[Cmdlet("Test", "EdgeSyncEhf", DefaultParameterSetName = "Health", SupportsShouldProcess = true)]
	public sealed class TestEdgeSyncEhf : TestEdgeSyncBase
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000878F File Offset: 0x0000698F
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000087A6 File Offset: 0x000069A6
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "CompareSyncedEntries")]
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000087B9 File Offset: 0x000069B9
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000087D0 File Offset: 0x000069D0
		[Parameter(Mandatory = false, ParameterSetName = "CompareSyncedEntries")]
		public EdgeSyncEhfConnectorIdParameter ConnectorId
		{
			get
			{
				return (EdgeSyncEhfConnectorIdParameter)base.Fields["ConnectorId"];
			}
			set
			{
				base.Fields["ConnectorId"] = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000087E3 File Offset: 0x000069E3
		protected override string CmdletMonitoringEventSource
		{
			get
			{
				return "MSExchange Monitoring EdgeSyncEhf";
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000087EA File Offset: 0x000069EA
		protected override string Service
		{
			get
			{
				return "EHF";
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000087F1 File Offset: 0x000069F1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestEdgeSyncEhf;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000087F8 File Offset: 0x000069F8
		internal override bool ReadConnectorLeasePath(IConfigurationSession session, ADObjectId rootId, out string primaryLeasePath, out string backupLeasePath, out bool hasOneConnectorEnabledInCurrentForest)
		{
			string text;
			backupLeasePath = (text = null);
			primaryLeasePath = text;
			hasOneConnectorEnabledInCurrentForest = false;
			EdgeSyncEhfConnector edgeSyncEhfConnector = base.FindSiteEdgeSyncConnector<EdgeSyncEhfConnector>(session, rootId, out hasOneConnectorEnabledInCurrentForest);
			if (edgeSyncEhfConnector == null)
			{
				return false;
			}
			primaryLeasePath = Path.Combine(edgeSyncEhfConnector.PrimaryLeaseLocation, "ehf.lease");
			backupLeasePath = Path.Combine(edgeSyncEhfConnector.BackupLeaseLocation, "ehf.lease");
			return true;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008848 File Offset: 0x00006A48
		internal override ADObjectId GetCookieContainerId(IConfigurationSession session)
		{
			return EhfTargetConnection.GetCookieContainerId(session);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008850 File Offset: 0x00006A50
		protected override EnhancedTimeSpan GetSyncInterval(EdgeSyncServiceConfig config)
		{
			return config.ConfigurationSyncInterval;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008858 File Offset: 0x00006A58
		protected override void InternalEndProcessing()
		{
			if (this.ehfService != null)
			{
				this.ehfService.Dispose();
				this.ehfService = null;
			}
			base.InternalEndProcessing();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000887C File Offset: 0x00006A7C
		protected override void InternalProcessRecord()
		{
			if (this.Organization != null)
			{
				if (this.ehfService == null)
				{
					this.globalSession = this.CreateGlobalSession();
					EhfTargetServerConfig config = Utils.CreateEhfTargetConfig(this.globalSession, this.ConnectorId, this);
					this.edgeSyncEhfConnector = Utils.GetConnector(this.globalSession, this.ConnectorId, this);
					this.ehfService = new EhfProvisioningService(config);
				}
				ExchangeConfigurationUnit currentOrganization = this.GetCurrentOrganization(this.globalSession);
				ExchangeTenantRecord exchangeTenantRecord = this.CreateExchangeTenantRecord(currentOrganization);
				EhfCompanyRecord ehfCompanyRecord = this.CreateEhfCompanyRecord(exchangeTenantRecord.PerimeterConfig);
				IList<object> list = EhfSyncDiffRecord<object>.Compare(exchangeTenantRecord, ehfCompanyRecord);
				using (IEnumerator<object> enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object sendToPipeline = enumerator.Current;
						base.WriteObject(sendToPipeline);
					}
					return;
				}
			}
			base.TestGeneralSyncHealth();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008A5C File Offset: 0x00006C5C
		private ExchangeTenantRecord CreateExchangeTenantRecord(ExchangeConfigurationUnit cu)
		{
			IConfigurationSession session = this.CreateScopedSession(cu);
			SyncedPerimeterConfig perimeterConfig = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADPagedReader<SyncedPerimeterConfig> adpagedReader = session.FindAllPaged<SyncedPerimeterConfig>();
				if (adpagedReader != null)
				{
					foreach (SyncedPerimeterConfig perimeterConfig in adpagedReader)
					{
						if (perimeterConfig != null)
						{
							this.WriteError(new InvalidDataException(string.Format("expected 1 perimeterconfig object for {0} organization but found more than 1", cu.Name)), ErrorCategory.InvalidData, null);
						}
						perimeterConfig = perimeterConfig;
					}
				}
			});
			if (!adoperationResult.Succeeded)
			{
				base.WriteError(adoperationResult.Exception, ErrorCategory.NotSpecified, null);
			}
			if (perimeterConfig == null)
			{
				base.WriteError(new InvalidDataException(string.Format("expected 1 perimeterconfig object for {0} organization but found 0", cu.Name)), ErrorCategory.InvalidData, null);
			}
			List<SyncedAcceptedDomain> syncedAcceptedDomains = new List<SyncedAcceptedDomain>();
			adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADPagedReader<SyncedAcceptedDomain> adpagedReader = session.FindAllPaged<SyncedAcceptedDomain>();
				if (adpagedReader != null)
				{
					foreach (SyncedAcceptedDomain syncedAcceptedDomain in adpagedReader)
					{
						if (syncedAcceptedDomain.DomainType == AcceptedDomainType.Authoritative || this.edgeSyncEhfConnector.Version > 1)
						{
							syncedAcceptedDomains.Add(syncedAcceptedDomain);
						}
					}
				}
			});
			if (!adoperationResult.Succeeded)
			{
				base.WriteError(adoperationResult.Exception, ErrorCategory.NotSpecified, null);
			}
			return new ExchangeTenantRecord(this.Organization, perimeterConfig, syncedAcceptedDomains);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008B34 File Offset: 0x00006D34
		private EhfCompanyRecord CreateEhfCompanyRecord(SyncedPerimeterConfig perimeterConfig)
		{
			EhfCompanyRecord result = null;
			Exception ex = null;
			try
			{
				Company company = null;
				int companyId;
				if (!this.ehfService.TryGetCompanyByGuid(perimeterConfig.Guid, out company) && int.TryParse(perimeterConfig.PerimeterOrgId, out companyId))
				{
					company = this.ehfService.GetCompany(companyId);
				}
				if (company != null)
				{
					Domain[] allDomains = this.ehfService.GetAllDomains(company.CompanyId);
					result = new EhfCompanyRecord(company, allDomains);
				}
			}
			catch (FaultException<ServiceFault> faultException)
			{
				ServiceFault detail = faultException.Detail;
				if (detail.Id == FaultId.UnableToConnectToDatabase)
				{
					ex = new InvalidOperationException("ServiceFault: EHF is unable to connect to its database", faultException);
				}
				else
				{
					if (detail.Id == FaultId.CompanyDoesNotExist)
					{
						return null;
					}
					ex = faultException;
				}
			}
			catch (MessageSecurityException ex2)
			{
				switch (EhfProvisioningService.DecodeMessageSecurityException(ex2))
				{
				case EhfProvisioningService.MessageSecurityExceptionReason.DatabaseFailure:
					ex = new InvalidOperationException("MessageSecurityException: EHF is unable to connect to its database", ex2.InnerException);
					goto IL_E6;
				case EhfProvisioningService.MessageSecurityExceptionReason.InvalidCredentials:
					ex = new InvalidOperationException("MessageSecurityException: EHF connector contains invalid credentials", ex2.InnerException);
					goto IL_E6;
				}
				ex = ex2;
				IL_E6:;
			}
			catch (CommunicationException ex3)
			{
				ex = ex3;
			}
			catch (TimeoutException ex4)
			{
				ex = ex4;
			}
			catch (EhfProvisioningService.ContractViolationException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, null);
			}
			return result;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008C90 File Offset: 0x00006E90
		private ExchangeConfigurationUnit GetCurrentOrganization(IConfigurationSession session)
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = null;
			IEnumerable<ExchangeConfigurationUnit> objects = this.Organization.GetObjects<ExchangeConfigurationUnit>(null, session);
			foreach (ExchangeConfigurationUnit exchangeConfigurationUnit2 in objects)
			{
				if (exchangeConfigurationUnit != null)
				{
					base.WriteError(new InvalidOperationException(string.Format("multiple organizations match {0} ", this.Organization.ToString())), ErrorCategory.InvalidArgument, null);
				}
				exchangeConfigurationUnit = exchangeConfigurationUnit2;
			}
			if (exchangeConfigurationUnit == null)
			{
				base.WriteError(new InvalidOperationException(string.Format("{0} is not found ", this.Organization.ToString())), ErrorCategory.InvalidArgument, null);
			}
			return exchangeConfigurationUnit;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008D30 File Offset: 0x00006F30
		private ITopologyConfigurationSession CreateGlobalSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 451, "CreateGlobalSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\TestEdgeSyncEhf.cs");
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008D6C File Offset: 0x00006F6C
		private IConfigurationSession CreateScopedSession(ExchangeConfigurationUnit cu)
		{
			ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(base.DomainController, null);
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, cu.OrganizationId, base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 473, "CreateScopedSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\TestEdgeSyncEhf.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = true;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x040000AE RID: 174
		private const string CmdletNoun = "EdgeSyncEhf";

		// Token: 0x040000AF RID: 175
		private const string ServiceName = "EHF";

		// Token: 0x040000B0 RID: 176
		private const string ParameterSetValidateEhfSync = "CompareSyncedEntries";

		// Token: 0x040000B1 RID: 177
		private EhfProvisioningService ehfService;

		// Token: 0x040000B2 RID: 178
		private ITopologyConfigurationSession globalSession;

		// Token: 0x040000B3 RID: 179
		private EdgeSyncEhfConnector edgeSyncEhfConnector;
	}
}
