using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Autodiscover;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200055B RID: 1371
	internal class DirectoryAccessor
	{
		// Token: 0x0600221A RID: 8730 RVA: 0x000CDCA0 File Offset: 0x000CBEA0
		private DirectoryAccessor()
		{
			try
			{
				this.sessionSelector = new DirectoryAccessor.SessionSelector();
				this.server = this.sessionSelector.TopologyConfigurationSession.FindServerByName(Environment.MachineName);
				if (this.server == null)
				{
					this.computer = this.sessionSelector.GcScopedConfigurationSession.FindLocalComputer();
				}
				else
				{
					this.sessionSelector.InitializeMonitoringTenants();
				}
			}
			catch (InvalidOperationException)
			{
				if (!FfoLocalEndpointManager.IsCentralAdminRoleInstalled)
				{
					throw;
				}
			}
			this.getRpcHttpVirtualDirectory = new DirectoryAccessor.GetRpcHttpVirtualDirectoryStrategy(this.GetLocalRpcHttpVirtualDirectories);
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x000CDD80 File Offset: 0x000CBF80
		public static DirectoryAccessor Instance
		{
			get
			{
				if (DirectoryAccessor.instance == null)
				{
					lock (DirectoryAccessor.locker)
					{
						if (DirectoryAccessor.instance == null)
						{
							DirectoryAccessor.instance = new DirectoryAccessor();
						}
					}
				}
				return DirectoryAccessor.instance;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000CDDD8 File Offset: 0x000CBFD8
		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000CDDE0 File Offset: 0x000CBFE0
		public ADComputer Computer
		{
			get
			{
				return this.computer;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000CDDE8 File Offset: 0x000CBFE8
		public AcceptedDomain DefaultMonitoringDomain
		{
			get
			{
				return this.sessionSelector.AcceptedDomain;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000CDDF5 File Offset: 0x000CBFF5
		public string MonitoringTenantPartitionId
		{
			get
			{
				if (this.sessionSelector.MonitoringTenantInfo != null)
				{
					return this.sessionSelector.MonitoringTenantInfo.MonitoringTenantPartitionId;
				}
				return null;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000CDE16 File Offset: 0x000CC016
		public OrganizationId MonitoringTenantOrganizationId
		{
			get
			{
				if (this.sessionSelector.MonitoringTenantInfo != null)
				{
					return this.sessionSelector.MonitoringTenantInfo.MonitoringTenantOrganizationId;
				}
				return null;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000CDE37 File Offset: 0x000CC037
		public bool CanMonitoringMailboxBeProvisioned
		{
			get
			{
				return !DirectoryAccessor.RunningInDatacenter || (this.sessionSelector.MonitoringTenantInfo != null && this.sessionSelector.MonitoringTenantInfo.MonitoringTenantReady);
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000CDE61 File Offset: 0x000CC061
		public string MonitoringTenantForestFqdn
		{
			get
			{
				if (this.sessionSelector.MonitoringTenantInfo != null)
				{
					return this.sessionSelector.MonitoringTenantInfo.MonitoringTenantForestFqdn;
				}
				return null;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000CDE82 File Offset: 0x000CC082
		internal bool TracingCredentials
		{
			get
			{
				return Settings.TracingCredentials;
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000CDE8C File Offset: 0x000CC08C
		public static SecurityIdentifier GetManagedAvailabilityServersUsgSid()
		{
			IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 361, "GetManagedAvailabilityServersUsgSid", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
			return rootOrganizationRecipientSession.GetWellKnownExchangeGroupSid(WellKnownGuid.MaSWkGuid);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000CDEC8 File Offset: 0x000CC0C8
		public bool IsServerCompatible(string serverName)
		{
			Server exchangeServerByName = this.GetExchangeServerByName(serverName);
			return exchangeServerByName != null && this.IsServerCompatible(exchangeServerByName);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000CDEEC File Offset: 0x000CC0EC
		public bool IsServerCompatible(Server target)
		{
			this.RefreshServerOrComputerObject();
			WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Server {0} serial number is {1}, monitoring group is {2}.", target.Name, target.SerialNumber, target.MonitoringGroup, null, "IsServerCompatible", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 395);
			return string.Compare(target.MonitoringGroup, (this.server == null) ? this.computer.MonitoringGroup : this.server.MonitoringGroup, true) == 0;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000CDF68 File Offset: 0x000CC168
		public bool IsServerCompatible(ADComputer target)
		{
			this.RefreshServerOrComputerObject();
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Server {0} monitoring group is {1}", target.Name, target.MonitoringGroup, null, "IsServerCompatible", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 410);
			return string.Compare(target.MonitoringGroup, (this.server == null) ? this.computer.MonitoringGroup : this.server.MonitoringGroup, true) == 0;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000CDFDB File Offset: 0x000CC1DB
		public Server GetExchangeServerByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return null;
			}
			return this.sessionSelector.TopologyConfigurationSession.FindServerByName(this.FqdnToName(name));
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000CDFFE File Offset: 0x000CC1FE
		public ADComputer GetNonExchangeServerByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return null;
			}
			return this.sessionSelector.GcScopedConfigurationSession.FindComputerByHostName(this.FqdnToName(name));
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000CE024 File Offset: 0x000CC224
		public void LoadGlobalOverrides()
		{
			Container globalOverridesContainer = this.GetGlobalOverridesContainer();
			if (globalOverridesContainer == null)
			{
				return;
			}
			if (globalOverridesContainer.EncryptionKey0 != null && globalOverridesContainer.EncryptionKey0.Length == 16)
			{
				this.globalOverrideWaterMark = new Guid?(new Guid(globalOverridesContainer.EncryptionKey0));
			}
			ProbeDefinition.GlobalOverrides = this.LoadGlobalOverridesForType<ProbeDefinition>(globalOverridesContainer);
			MonitorDefinition.GlobalOverrides = this.LoadGlobalOverridesForType<MonitorDefinition>(globalOverridesContainer);
			ResponderDefinition.GlobalOverrides = this.LoadGlobalOverridesForType<ResponderDefinition>(globalOverridesContainer);
			MaintenanceDefinition.GlobalOverrides = this.LoadGlobalOverridesForType<MaintenanceDefinition>(globalOverridesContainer);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000CE098 File Offset: 0x000CC298
		public bool IsGlobalOverridesChanged()
		{
			Container globalOverridesContainer = this.GetGlobalOverridesContainer();
			if (globalOverridesContainer != null && globalOverridesContainer.EncryptionKey0 != null && globalOverridesContainer.EncryptionKey0.Length == 16)
			{
				Guid guid = new Guid(globalOverridesContainer.EncryptionKey0);
				if (guid != this.globalOverrideWaterMark)
				{
					this.globalOverrideWaterMark = new Guid?(guid);
					return this.IsGlobalOverridesChangedForType<ProbeDefinition>(ProbeDefinition.GlobalOverrides, globalOverridesContainer) || this.IsGlobalOverridesChangedForType<MonitorDefinition>(MonitorDefinition.GlobalOverrides, globalOverridesContainer) || this.IsGlobalOverridesChangedForType<ResponderDefinition>(ResponderDefinition.GlobalOverrides, globalOverridesContainer) || this.IsGlobalOverridesChangedForType<MaintenanceDefinition>(MaintenanceDefinition.GlobalOverrides, globalOverridesContainer);
				}
			}
			return false;
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000CE140 File Offset: 0x000CC340
		public DatabaseCopy[] GetMailboxDatabaseCopies()
		{
			return this.GetMailboxDatabaseCopies(Environment.MachineName);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000CE150 File Offset: 0x000CC350
		public DatabaseCopy[] GetMailboxDatabaseCopies(string serverName)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serverName);
			return this.sessionSelector.TopologyConfigurationSession.Find<DatabaseCopy>(null, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000CE184 File Offset: 0x000CC384
		public MailboxDatabase GetMailboxDatabaseFromGuid(Guid mdbGuid)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, mdbGuid);
			MailboxDatabase[] array = this.sessionSelector.TopologyConfigurationSession.Find<MailboxDatabase>(null, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000CE1C8 File Offset: 0x000CC3C8
		public MailboxDatabase GetMailboxDatabaseFromName(string databaseName)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DatabaseSchema.Name, databaseName);
			MailboxDatabase[] array = this.sessionSelector.TopologyConfigurationSession.Find<MailboxDatabase>(null, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000CE205 File Offset: 0x000CC405
		public MailboxDatabase GetMailboxDatabaseFromCopy(DatabaseCopy copy)
		{
			return this.sessionSelector.TopologyConfigurationSession.Read<MailboxDatabase>(((ADObjectId)copy.Identity).Parent);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000CE227 File Offset: 0x000CC427
		internal ADUser SearchMonitoringMailbox(string displayName, string userPrincipalName, ref MailboxDatabase database)
		{
			return this.SearchMonitoringMailboxInternal(displayName, userPrincipalName, ref database, this.sessionSelector.RecipientSession);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000CE240 File Offset: 0x000CC440
		public IEnumerable<ADUser> SearchMonitoringMailboxesByDisplayName(string displayName)
		{
			if (string.IsNullOrWhiteSpace(displayName))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Invalid DisplayName", null, "SearchMonitoringMailboxesByDisplayName", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 616);
				throw new ArgumentException("Invalid DisplayName");
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Searching for monitoring mailbox with DisplayName {0}", displayName, null, "SearchMonitoringMailboxesByDisplayName", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 621);
			QueryFilter queryFilter = this.CreateWildcardOrEqualFilter(ADRecipientSchema.DisplayName, displayName);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MonitoringMailbox);
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
			return this.sessionSelector.RecipientSession.FindADUser(null, QueryScope.SubTree, filter, null, 1000);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000CE308 File Offset: 0x000CC508
		internal QueryFilter CreateWildcardOrEqualFilter(ADPropertyDefinition schemaProperty, string identityString)
		{
			if (string.IsNullOrEmpty(identityString))
			{
				throw new ArgumentException("identityString");
			}
			string text;
			MatchOptions matchOptions;
			if (identityString.StartsWith("*") && identityString.EndsWith("*"))
			{
				if (identityString.Length <= 2)
				{
					return null;
				}
				text = identityString.Substring(1, identityString.Length - 2);
				matchOptions = MatchOptions.SubString;
			}
			else if (identityString.EndsWith("*"))
			{
				text = identityString.Substring(0, identityString.Length - 1);
				matchOptions = MatchOptions.Prefix;
			}
			else
			{
				if (!identityString.StartsWith("*"))
				{
					return new ComparisonFilter(ComparisonOperator.Equal, schemaProperty, identityString);
				}
				text = identityString.Substring(1);
				matchOptions = MatchOptions.Suffix;
			}
			return new TextFilter(schemaProperty, text, matchOptions, MatchFlags.IgnoreCase);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x000CE3B4 File Offset: 0x000CC5B4
		internal ADUser StampProvisioningConstraint(string userPrincipalName)
		{
			MailboxDatabase mailboxDatabase = null;
			ADUser aduser = this.SearchMonitoringMailboxInternal(null, userPrincipalName, ref mailboxDatabase, this.sessionSelector.WritableRecipientSession);
			if (aduser == null)
			{
				return null;
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PinMonitoringMailboxesToDatabases.Enabled && (aduser.MailboxProvisioningConstraint == null || string.IsNullOrWhiteSpace(aduser.MailboxProvisioningConstraint.Value)))
			{
				string text = string.Format("{{DatabaseName -eq '{0}'}}", mailboxDatabase.Name);
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Stamping mailbox '{0}' with provisioning constraint '{1}'", aduser.Name, text, null, "StampProvisioningConstraint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 714);
				aduser.MailboxProvisioningConstraint = new MailboxProvisioningConstraint(text);
				this.sessionSelector.WritableRecipientSession.Save(aduser);
				aduser = (this.SearchMailbox(aduser.Name, this.sessionSelector.WritableRecipientSession, aduser.Id.Parent) as ADUser);
			}
			return aduser;
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000CE49C File Offset: 0x000CC69C
		internal void DeleteMonitoringMailbox(ADUser monitoringMailbox)
		{
			if (monitoringMailbox == null)
			{
				throw new ArgumentNullException("monitoringMailbox");
			}
			if (monitoringMailbox.RecipientTypeDetails != RecipientTypeDetails.MonitoringMailbox)
			{
				throw new InvalidOperationException("Mailbox needs to be monitoring mailbox");
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 749, "DeleteMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			ADOrganizationConfig adorganizationConfig = configurationSession.Read<ADOrganizationConfig>(orgContainerId);
			OrganizationId organizationId = adorganizationConfig.OrganizationId;
			AcceptedDomain defaultAcceptedDomain = configurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null || defaultAcceptedDomain.DomainName == null || defaultAcceptedDomain.DomainName.Domain == null)
			{
				WTFDiagnostics.TraceWarning(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Can't delete monitoring mailbox because can't find the default accepted domain for monitoring mailboxes", null, "DeleteMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 761);
				return;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Create the cmdlet to delete mailbox {0}", monitoringMailbox.UserPrincipalName, null, "DeleteMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 765);
			using (PSLocalTask<RemoveMailbox, Mailbox> pslocalTask = CmdletTaskFactory.Instance.CreateRemoveMailboxTask(organizationId, new SmtpAddress("admin@" + defaultAcceptedDomain)))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.Permanent = true;
				pslocalTask.Task.Force = true;
				pslocalTask.Task.Identity = new GeneralMailboxIdParameter(monitoringMailbox);
				pslocalTask.Task.Execute();
				if (pslocalTask.Error != null)
				{
					throw new Exception(pslocalTask.ErrorMessage);
				}
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000CE618 File Offset: 0x000CC818
		internal ADUser CreateMonitoringMailbox(string displayName, MailboxDatabase database, out string password)
		{
			password = null;
			if (!this.CanMonitoringMailboxBeProvisioned)
			{
				WTFDiagnostics.TraceWarning(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Can't create monitoring mailbox because monitoring tenant does not exist.", null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 804);
				return null;
			}
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Creating monitoring mailbox {0} on database {1}", displayName, database.Name, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 808);
			ADSessionSettings sessionSettings;
			IConfigurationSession configurationSession;
			IRecipientSession recipientSession;
			if (DirectoryAccessor.RunningInMultiTenantEnvironment)
			{
				string text;
				if (this.sessionSelector.MonitoringTenantInfo == null)
				{
					text = null;
				}
				else
				{
					text = this.sessionSelector.MonitoringTenantInfo.MonitoringTenantName;
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					text = MailboxTaskHelper.GetMonitoringTenantName("E15");
				}
				try
				{
					sessionSettings = ADSessionSettings.FromTenantCUName(text);
					configurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 842, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
					recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 843, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
				}
				catch (CannotResolveTenantNameException)
				{
					WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Can't create monitoring mailbox because monitoring tenant '{0}' does not exist.", text, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 848);
					return null;
				}
				ExchangeConfigurationUnit[] array = configurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, null, null, 0);
				if (array == null || array.Length == 0)
				{
					WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Can't create monitoring mailbox because monitoring tenant '{0}' does not exist.", text, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 857);
					return null;
				}
				ExchangeConfigurationUnit exchangeConfigurationUnit = array[0];
				if (exchangeConfigurationUnit.OrganizationStatus != OrganizationStatus.Active)
				{
					WTFDiagnostics.TraceWarning<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Can't create monitoring mailbox because monitoring tenant '{0}' is not active. Its status is '{1}'.", text, exchangeConfigurationUnit.OrganizationStatus.ToString(), null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 865);
					return null;
				}
				goto IL_1D5;
			}
			sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 873, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
			recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 874, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
			IL_1D5:
			AcceptedDomain defaultAcceptedDomain = configurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain == null || defaultAcceptedDomain.DomainName == null || defaultAcceptedDomain.DomainName.Domain == null)
			{
				throw new Exception("Can't find the default accepted domain for monitoring mailboxes");
			}
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			ADOrganizationConfig adorganizationConfig = configurationSession.Read<ADOrganizationConfig>(orgContainerId);
			OrganizationId organizationId = adorganizationConfig.OrganizationId;
			ADObjectId adobjectId = null;
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				adobjectId = organizationId.OrganizationalUnit;
			}
			else
			{
				bool useConfigNC = configurationSession.UseConfigNC;
				bool useGlobalCatalog = configurationSession.UseGlobalCatalog;
				ADComputer adcomputer;
				try
				{
					configurationSession.UseConfigNC = false;
					configurationSession.UseGlobalCatalog = true;
					Server server = database.GetServer();
					adcomputer = ((ITopologyConfigurationSession)configurationSession).FindComputerByHostName(server.Name);
				}
				finally
				{
					configurationSession.UseConfigNC = useConfigNC;
					configurationSession.UseGlobalCatalog = useGlobalCatalog;
				}
				if (adcomputer == null)
				{
					throw new Exception(string.Format("The Exchange server for the database '{0}' wasn't found in Active Directory Domain Services. The object may be corrupted.", database.Name));
				}
				ADObjectId adobjectId2 = adcomputer.Id.DomainId;
				adobjectId2 = adobjectId2.GetChildId("Microsoft Exchange System Objects");
				adobjectId = adobjectId2.GetChildId("Monitoring Mailboxes");
			}
			string text2 = string.Format("HealthMailbox{0}", Guid.NewGuid().ToString("N"));
			string str = defaultAcceptedDomain.DomainName.Domain.ToString();
			string text3 = text2 + "@" + str;
			password = LocalMonitoringMailboxManagement.GetStaticPassword(this.traceContext);
			if (string.IsNullOrEmpty(password))
			{
				if (DirectoryAccessor.RunningInDatacenter)
				{
					password = PasswordHelper.GetRandomPassword(text2, text3, 16);
				}
				else
				{
					password = PasswordHelper.GetRandomPassword(displayName, text2, 128);
				}
			}
			using (PSLocalTask<NewMailbox, Mailbox> pslocalTask = CmdletTaskFactory.Instance.CreateNewMonitoringMailboxTask(organizationId, new SmtpAddress("admin@" + defaultAcceptedDomain)))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.Name = text2;
				pslocalTask.Task.Alias = text2;
				pslocalTask.Task.DisplayName = displayName;
				pslocalTask.Task.Database = new DatabaseIdParameter(database);
				pslocalTask.Task.ResetPasswordOnNextLogon = false;
				pslocalTask.Task.SendModerationNotifications = TransportModerationNotificationFlags.Never;
				pslocalTask.Task.Archive = true;
				pslocalTask.Task.OrganizationalUnit = new OrganizationalUnitIdParameter(adobjectId);
				if (DirectoryAccessor.ShouldStampProvisioningConstraint)
				{
					pslocalTask.Task.MailboxProvisioningConstraint = new MailboxProvisioningConstraint(string.Format("{{DatabaseName -eq '{0}'}}", database.Name));
				}
				if (DirectoryAccessor.RunningInDatacenter)
				{
					pslocalTask.Task.WindowsLiveID = new WindowsLiveId(text3);
				}
				else
				{
					pslocalTask.Task.UserPrincipalName = text3;
				}
				if (DirectoryAccessor.RunningInMultiTenantEnvironment)
				{
					pslocalTask.Task.Organization = new OrganizationIdParameter(organizationId);
				}
				using (SecureString secureString = password.ConvertToSecureString())
				{
					pslocalTask.Task.Password = secureString;
					pslocalTask.Task.Execute();
				}
				if (pslocalTask.Error != null)
				{
					throw new Exception(pslocalTask.ErrorMessage);
				}
				Mailbox result = pslocalTask.Result;
				string originatingServer = result.OriginatingServer;
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox '{0}' created using Domain Controller '{1}'", text3, originatingServer, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1014);
				recipientSession.LinkResolutionServer = originatingServer;
				recipientSession.DomainController = originatingServer;
				recipientSession.UseGlobalCatalog = false;
			}
			Thread.Sleep(TimeSpan.FromSeconds(10.0));
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, text2);
			ADUser[] array2 = recipientSession.FindADUser(adobjectId, QueryScope.SubTree, filter, null, 1);
			if (array2 != null && array2.Length > 0)
			{
				ADUser aduser = array2[0];
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox '{0}' found in AD", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1032);
				if (DirectoryAccessor.RunningInDatacenter)
				{
					bool useBecAPIsforLiveId = ProvisioningTasksConfigImpl.UseBecAPIsforLiveId;
					if (useBecAPIsforLiveId)
					{
						if (aduser.ExternalDirectoryObjectId == null || string.IsNullOrWhiteSpace(aduser.ExternalDirectoryObjectId.ToString()))
						{
							WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox '{0}' failed to return valid ExternalDirectoryObjectId - abandoning it.", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1042);
							return null;
						}
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox '{0}' has a valid BEC ExternalDirectoryObjectId", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1047);
					}
					else
					{
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "BEC is not enabled; skipping ExternalDirectoryObjectId validation for Mailbox '{0}'", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1052);
					}
				}
				else
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Enterprise SKU; skipping ExternalDirectoryObjectId validation for Mailbox '{0}'", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1057);
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Attempting to create Search probe message in Mailbox '{0}'", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1061);
				try
				{
					string smtpAddress = text3;
					using (MailboxSession mailboxSession = SearchStoreHelper.GetMailboxSession(smtpAddress, true, "Monitoring"))
					{
						using (Folder inboxFolder = SearchStoreHelper.GetInboxFolder(mailboxSession))
						{
							SearchStoreHelper.CreateMessage(mailboxSession, inboxFolder, "SearchQueryStxProbe");
						}
					}
				}
				catch (Exception arg)
				{
					WTFDiagnostics.TraceWarning<Exception>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Exception is caught trying to create Search message in monitoring mailbox: {0}", arg, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1075);
				}
				return aduser;
			}
			WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox '{0}' could not be found in AD even though New-Mailbox returned successfully.", text3, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1082);
			return null;
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000CEDDC File Offset: 0x000CCFDC
		public ADUser SearchOrCreateMonitoringMailbox(bool canUpdate, ref MailboxDatabase database, Guid guid, string userPrincipalName = null)
		{
			string monitoringMailboxName = ADUser.GetMonitoringMailboxName(guid);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Searching monitoring mailbox {0}", monitoringMailboxName, null, "SearchOrCreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1107);
			ADUser aduser = this.SearchMonitoringMailboxInternal(monitoringMailboxName, userPrincipalName, ref database, this.sessionSelector.RecipientSession);
			if (canUpdate && aduser == null)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Monitoring mailbox {0} not found, try creating one", monitoringMailboxName, null, "SearchOrCreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1117);
				if (database == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox database not specified, try pick one", null, "SearchOrCreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1123);
					MailboxDatabase[] candidateMailboxDatabases = this.GetCandidateMailboxDatabases(1);
					if (candidateMailboxDatabases != null && candidateMailboxDatabases.Length > 0)
					{
						database = candidateMailboxDatabases[0];
					}
				}
				if (database != null)
				{
					WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Creating monitoring mailbox on database {0}", database.Id.ToString(), null, "SearchOrCreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1133);
					string text = null;
					aduser = this.CreateMonitoringMailbox(ADUser.GetMonitoringMailboxName(guid), database, out text);
				}
				else
				{
					WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Monitoring mailbox cannot be created because no mailbox database available", null, "SearchOrCreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1140);
				}
			}
			return aduser;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000CEF10 File Offset: 0x000CD110
		internal ADRecipient SearchMailbox(string account, IRecipientSession session, ADObjectId root = null)
		{
			ADRecipient adrecipient = DirectoryAccessor.SearchForRecipient(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, account), session, root);
			if (adrecipient != null)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox {0} found", account, null, "SearchMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1160);
			}
			return adrecipient;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000CEF5C File Offset: 0x000CD15C
		public Guid GetSystemMailboxGuid(Guid databaseGuid)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "SystemMailbox{{{0}}}", new object[]
			{
				databaseGuid.ToString()
			});
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Searching for System mailbox {0}", text, null, "GetSystemMailboxGuid", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1178);
			ADRecipient adrecipient = this.SearchMailbox(text, this.sessionSelector.RootorgRecipientSession, null);
			if (adrecipient == null || !(adrecipient is ADSystemMailbox))
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "System mailbox {0} not found", text, null, "GetSystemMailboxGuid", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1187);
				return Guid.Empty;
			}
			return ((ADSystemMailbox)adrecipient).ExchangeGuid;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000CF010 File Offset: 0x000CD210
		public string GeneratePasswordForMailbox(ADUser mailbox)
		{
			string text = LocalMonitoringMailboxManagement.GetStaticPassword(this.traceContext);
			if (string.IsNullOrWhiteSpace(text))
			{
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled)
				{
					text = PasswordHelper.GetRandomPassword(mailbox.DisplayName, mailbox.SamAccountName, 128);
				}
				else
				{
					text = PasswordHelper.GetRandomPassword(mailbox.Name, mailbox.UserPrincipalName, 16);
				}
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "GeneratePasswordForMailbox {0} password", text, null, "GeneratePasswordForMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1215);
			return text;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000CF0A8 File Offset: 0x000CD2A8
		private void ResetPasswordForMailbox(IRecipientSession recipientSession, IConfigurationSession configurationSession, ADUser mailbox, string password)
		{
			MailboxIdParameter identity = new MailboxIdParameter(mailbox.ObjectId);
			ADObjectId orgContainerId = configurationSession.GetOrgContainerId();
			ADOrganizationConfig adorganizationConfig = configurationSession.Read<ADOrganizationConfig>(orgContainerId);
			OrganizationId organizationId = adorganizationConfig.OrganizationId;
			using (PSLocalTask<SetMailbox, object> pslocalTask = CmdletTaskFactory.Instance.CreateSetMailboxTask(organizationId))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.Identity = identity;
				using (SecureString secureString = password.ConvertToSecureString())
				{
					pslocalTask.Task.Password = secureString;
					pslocalTask.Task.Execute();
				}
				if (pslocalTask.Error != null)
				{
					string errorMessage = pslocalTask.ErrorMessage;
					WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Failed to change password for mailbox '{0}'. Error message: {1}", mailbox.UserPrincipalName, errorMessage, null, "ResetPasswordForMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1260);
					throw new Exception(errorMessage);
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Successfully changed password for mailbox '{0}'", mailbox.UserPrincipalName, null, "ResetPasswordForMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1264);
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000CF1C8 File Offset: 0x000CD3C8
		public string GetMonitoringMailboxCredential(ADUser monitoringMailbox)
		{
			WTFDiagnostics.TraceFunction<ADUser>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Retrieving monitoring mailbox {0} password", monitoringMailbox, null, "GetMonitoringMailboxCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1280);
			string text = this.GeneratePasswordForMailbox(monitoringMailbox);
			if (!DirectoryAccessor.RunningInDatacenter)
			{
				this.ResetPasswordForMailbox(this.sessionSelector.WritableRecipientSession, this.sessionSelector.TopologyConfigurationSession, monitoringMailbox, text);
			}
			else
			{
				this.ResetPasswordForMailbox(this.sessionSelector.WritableRecipientSession, this.sessionSelector.MonitoringTenantInfo.TenantConfigurationSession, monitoringMailbox, text);
			}
			if (this.TracingCredentials)
			{
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Password for monitoring mailbox {0} is {1}", monitoringMailbox.UserPrincipalName, text, null, "GetMonitoringMailboxCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1299);
			}
			return text;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000CF284 File Offset: 0x000CD484
		public string GetServerFqdnForDatabase(Guid dbGuid)
		{
			DatabaseLocationInfo serverForDatabase = this.activeManager.Value.GetServerForDatabase(dbGuid);
			if (serverForDatabase != null)
			{
				return serverForDatabase.ServerFqdn;
			}
			return null;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000CF2AE File Offset: 0x000CD4AE
		public bool IsDatabaseCopyActiveOnLocalServer(Guid databaseGuid)
		{
			return this.IsDatabaseCopyActiveOnLocalServer(databaseGuid, null);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000CF2B8 File Offset: 0x000CD4B8
		public bool IsDatabaseCopyActiveOnLocalServer(MailboxDatabase database)
		{
			return this.IsDatabaseCopyActiveOnLocalServer(database.Guid, database);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000CF2C7 File Offset: 0x000CD4C7
		public string GetDatabaseActiveHost(MailboxDatabase database)
		{
			if (database == null || database.Server == null)
			{
				return null;
			}
			return database.Server.Name;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000CF2E4 File Offset: 0x000CD4E4
		public string GetDatabaseActiveHost(Guid mdbGuid)
		{
			MailboxDatabase mailboxDatabaseFromGuid = this.GetMailboxDatabaseFromGuid(mdbGuid);
			return this.GetDatabaseActiveHost(mailboxDatabaseFromGuid);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000CF300 File Offset: 0x000CD500
		public Server[] GetCandidateCafeServers(int max)
		{
			WTFDiagnostics.TraceFunction<int>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidateCafeServers: max count is {0}", max, null, "GetCandidateCafeServers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1375);
			QueryFilter compatibilityFilter;
			if (string.IsNullOrEmpty(this.server.MonitoringGroup))
			{
				compatibilityFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.IsCafeServer, true),
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.SerialNumber, DirectoryAccessor.versionCutoff.ToString(true)),
					new NotFilter(new ExistsFilter(ServerSchema.MonitoringGroup))
				});
			}
			else
			{
				compatibilityFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.IsCafeServer, true),
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.SerialNumber, DirectoryAccessor.versionCutoff.ToString(true)),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.MonitoringGroup, this.server.MonitoringGroup)
				});
			}
			return this.GetCandidates<Server>(compatibilityFilter, max);
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000CF3FC File Offset: 0x000CD5FC
		public List<string> GetCandidateObservers()
		{
			string text = (this.server != null) ? this.server.MonitoringGroup : this.computer.MonitoringGroup;
			QueryFilter queryFilter;
			if (string.IsNullOrEmpty(text))
			{
				queryFilter = new NotFilter(new ExistsFilter(ADComputerSchema.MonitoringGroup));
			}
			else
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADComputerSchema.MonitoringGroup, text);
			}
			queryFilter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADComputerSchema.MonitoringInstalled, (this.server != null) ? 1 : 2)
			});
			ADPagedReader<ADComputer> adpagedReader = this.sessionSelector.GcScopedConfigurationSession.FindPaged<ADComputer>(null, QueryScope.SubTree, queryFilter, null, 0);
			ADComputer[] array = adpagedReader.ReadAllPages();
			if (array == null || array.Length == 0)
			{
				return new List<string>();
			}
			return (from svr in array
			select svr.DnsHostName).ToList<string>();
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000CF4DC File Offset: 0x000CD6DC
		public bool IsMonitoringOffline(string serverName)
		{
			Server exchangeServerByName = this.GetExchangeServerByName(serverName);
			if (exchangeServerByName != null)
			{
				return this.IsMonitoringOffline(exchangeServerByName);
			}
			ADComputer nonExchangeServerByName = this.GetNonExchangeServerByName(serverName);
			if (nonExchangeServerByName != null)
			{
				return this.IsMonitoringOffline(nonExchangeServerByName);
			}
			throw new ServerNotFoundException("Invalid server name; No Exchange server AD object or computer object found", serverName);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000CF51A File Offset: 0x000CD71A
		public bool IsMonitoringOffline(Server server)
		{
			return !ServerComponentStates.IsRemoteComponentOnlineAccordingToAD(server, ServerComponentEnum.ServerWideOffline) || !ServerComponentStates.IsRemoteComponentOnlineAccordingToAD(server, ServerComponentEnum.Monitoring);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000CF531 File Offset: 0x000CD731
		public bool IsMonitoringOffline(ADComputer server)
		{
			return !ServerComponentStates.IsRemoteComponentOnlineAccordingToAD(server, ServerComponentEnum.ServerWideOffline) || !ServerComponentStates.IsRemoteComponentOnlineAccordingToAD(server, ServerComponentEnum.Monitoring);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000CF548 File Offset: 0x000CD748
		public bool IsRecoveryActionsEnabledOffline(string serverName)
		{
			return !this.IsServerComponentOnline(serverName, ServerComponentEnum.RecoveryActionsEnabled);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000CF558 File Offset: 0x000CD758
		internal bool IsServerComponentOnline(string serverName, ServerComponentEnum component)
		{
			Server exchangeServerByName = this.GetExchangeServerByName(serverName);
			if (exchangeServerByName != null)
			{
				return ServerComponentStates.IsRemoteComponentOnlineAccordingToAD(exchangeServerByName, component);
			}
			ADComputer nonExchangeServerByName = this.GetNonExchangeServerByName(serverName);
			if (nonExchangeServerByName != null)
			{
				return ServerComponentStates.IsRemoteComponentOnlineAccordingToAD(nonExchangeServerByName, component);
			}
			throw new ArgumentException("Invalid server name; No Exchange server AD object or computer object found");
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000CF598 File Offset: 0x000CD798
		public IEnumerable<ADOwaVirtualDirectory> GetLocalOwaVirtualDirectories()
		{
			if (this.Server == null)
			{
				return null;
			}
			return this.sessionSelector.TopologyConfigurationSession.Find<ADOwaVirtualDirectory>((ADObjectId)this.Server.Identity, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x000CF5E0 File Offset: 0x000CD7E0
		public Guid[] GetAllOfflineAddressBookGuids()
		{
			IConfigurationSession configurationSession = (this.sessionSelector.MonitoringTenantInfo != null) ? this.sessionSelector.MonitoringTenantInfo.TenantConfigurationSession : this.sessionSelector.TopologyConfigurationSession;
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2012);
			OfflineAddressBook[] array = configurationSession.Find<OfflineAddressBook>(null, QueryScope.SubTree, filter, null, 0);
			if (array == null)
			{
				return new Guid[0];
			}
			return (from n in array
			select n.Guid into n
			orderby n
			select n).ToArray<Guid>();
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x000CF68C File Offset: 0x000CD88C
		public List<ADUser> GetAllOrganizationMailboxes()
		{
			return OrganizationMailbox.GetOrganizationMailboxesByCapability(this.sessionSelector.RecipientSession, OrganizationCapability.OABGen);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000CF6C0 File Offset: 0x000CD8C0
		public Guid[] GetAllDatabaseGuidsForOrganizationMailboxes()
		{
			List<ADUser> allOrganizationMailboxes = this.GetAllOrganizationMailboxes();
			return (from x in allOrganizationMailboxes.ConvertAll<Guid>((ADUser n) => n.Database.ObjectGuid).Distinct<Guid>()
			orderby x
			select x).ToArray<Guid>();
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000CF724 File Offset: 0x000CD924
		public Container GetGlobalOverridesContainer()
		{
			if (this.sessionSelector.TopologyConfigurationSession == null)
			{
				return null;
			}
			ADObjectId rootOrgId = this.sessionSelector.TopologyConfigurationSession.SessionSettings.RootOrgId;
			ADObjectId descendantId = rootOrgId.GetDescendantId(MonitoringOverride.RdnContainer);
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MonitoringOverride.ContainerName);
			Container[] array = this.sessionSelector.TopologyConfigurationSession.Find<Container>(descendantId, QueryScope.SubTree, filter, null, 0);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000CF7B8 File Offset: 0x000CD9B8
		internal MailboxDatabase[] GetCandidateMailboxDatabases(int max)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidateMailboxDatabases called", null, "GetCandidateMailboxDatabases", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1637);
			DatabaseAvailabilityGroup[] candidates = this.GetCandidates<DatabaseAvailabilityGroup>(null, max);
			int num = 0;
			List<MailboxDatabase> list = new List<MailboxDatabase>();
			for (int i = 0; i < max; i++)
			{
				DatabaseAvailabilityGroup databaseAvailabilityGroup = null;
				if (candidates != null && candidates.Length > 0)
				{
					databaseAvailabilityGroup = candidates[num];
					num = (num + 1) % candidates.Length;
				}
				List<QueryFilter> list2 = new List<QueryFilter>();
				list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.IsMailboxServer, true));
				list2.Add(new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.SerialNumber, DirectoryAccessor.versionCutoff.ToString(true)));
				if (databaseAvailabilityGroup != null)
				{
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.DatabaseAvailabilityGroup, (ADObjectId)databaseAvailabilityGroup.Identity));
				}
				if (string.IsNullOrEmpty(this.server.MonitoringGroup))
				{
					list2.Add(new NotFilter(new ExistsFilter(ServerSchema.MonitoringGroup)));
				}
				else
				{
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.MonitoringGroup, this.server.MonitoringGroup));
				}
				QueryFilter compatibilityFilter = new AndFilter(list2.ToArray());
				Server[] candidates2 = this.GetCandidates<Server>(compatibilityFilter, 10);
				if (candidates2 != null && candidates2.Length != 0)
				{
					MailboxDatabase mailboxDatabase = null;
					int num2 = this.random.Next(candidates2.Length);
					int num3 = 0;
					do
					{
						int num4 = (num2 + num3) % candidates2.Length;
						Server server = candidates2[num4];
						WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidateMailboxDatabase: try find from server {0}", server.Name, null, "GetCandidateMailboxDatabases", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1705);
						if (this.IsMonitoringOffline(server))
						{
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidateMailboxDatabase: ignore server {0} because monitoring state is not active", server.Name, null, "GetCandidateMailboxDatabases", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1710);
						}
						else
						{
							DatabaseCopy[] mailboxDatabaseCopies = this.GetMailboxDatabaseCopies(server.Name);
							if (mailboxDatabaseCopies != null && mailboxDatabaseCopies.Length != 0)
							{
								int num5 = this.random.Next(mailboxDatabaseCopies.Length);
								int num6 = 0;
								do
								{
									int num7 = (num5 + num6) % mailboxDatabaseCopies.Length;
									DatabaseCopy copy = mailboxDatabaseCopies[num7];
									MailboxDatabase mdb = this.GetMailboxDatabaseFromCopy(copy);
									if (!list.Exists((MailboxDatabase element) => element.Guid == mdb.Guid) && mdb != null && !DatabaseTasksHelper.IsMailboxDatabaseExcludedFromMonitoring(mdb) && mdb.Server.ObjectGuid == server.Guid)
									{
										goto Block_12;
									}
								}
								while (++num6 < mailboxDatabaseCopies.Length);
								IL_2A6:
								if (mailboxDatabase == null)
								{
									goto IL_2AA;
								}
								break;
								Block_12:
								mailboxDatabase = CS$<>8__locals1.mdb;
								goto IL_2A6;
							}
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidateMailboxDatabase: ignore server {0} because no database copy is found there", server.Name, null, "GetCandidateMailboxDatabases", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1718);
						}
						IL_2AA:;
					}
					while (++num3 < candidates2.Length);
					if (mailboxDatabase != null)
					{
						list.Add(mailboxDatabase);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000CFA9C File Offset: 0x000CDC9C
		private ADUser SearchMonitoringMailboxInternal(string displayName, string userPrincipalName, ref MailboxDatabase database, IRecipientSession session)
		{
			if (session == null)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "SearchMonitoringMailboxInternal: invalid IRecipientSession", null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1794);
				throw new ArgumentException("SearchMonitoringMailboxInternal: invalid IRecipientSession");
			}
			if (userPrincipalName == null && displayName == null)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "SearchMonitoringMailboxInternal: invalid DisplayName and UPN", null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1800);
				throw new ArgumentException("SearchMonitoringMailboxInternal: invalid DisplayName and UPN");
			}
			if (!this.CanMonitoringMailboxBeProvisioned)
			{
				string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
				string message;
				if (domainName.Contains("extest.microsoft.com"))
				{
					message = "Can't find the Monitoring tenant for this test forest. Please examine your TDS deployment logs (especially the 'Datacenter PostReqs' step) to find out what went wrong.";
				}
				else
				{
					message = "Can't find the Monitoring tenant for this forest. Please page the Monitoring On Call Engineer, as this forest should never have gone live with this issue.";
				}
				WTFDiagnostics.TraceError(ExTraceGlobals.CommonComponentsTracer, this.traceContext, message, null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1818);
				throw new Exception(message);
			}
			QueryFilter queryFilter;
			if (string.IsNullOrWhiteSpace(userPrincipalName))
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Searching for monitoring mailbox with DisplayName {0}", displayName, null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1826);
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.DisplayName, displayName);
			}
			else
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Searching for monitoring mailbox with UPN {0}", userPrincipalName, null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1831);
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, userPrincipalName);
			}
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetailsValue, RecipientTypeDetails.MonitoringMailbox);
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
			ADUser[] array = session.FindADUser(null, QueryScope.SubTree, filter, null, 1000);
			if (array != null && array.Length > 0)
			{
				WTFDiagnostics.TraceInformation<int, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Found {0} monitorng mailboxes with display name {1}", array.Length, array[0].DisplayName, null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1853);
				foreach (ADUser aduser in array)
				{
					if (this.CanMonitoringMailboxBeUsed(aduser, ref database))
					{
						return aduser;
					}
				}
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Unable to find any usable monitorng mailboxes with name {0}", (!string.IsNullOrWhiteSpace(userPrincipalName)) ? userPrincipalName : displayName, null, "SearchMonitoringMailboxInternal", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1864);
			return null;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000CFCC4 File Offset: 0x000CDEC4
		private bool CanMonitoringMailboxBeUsed(ADUser monitoringMailbox, ref MailboxDatabase presumedDatabase)
		{
			if (DirectoryAccessor.RunningInDatacenter)
			{
				bool useBecAPIsforLiveId = ProvisioningTasksConfigImpl.UseBecAPIsforLiveId;
				if (useBecAPIsforLiveId && (monitoringMailbox.ExternalDirectoryObjectId == null || string.IsNullOrWhiteSpace(monitoringMailbox.ExternalDirectoryObjectId.ToString())))
				{
					WTFDiagnostics.TraceError<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox '{0}' does not have a valid ExternalDirectoryObjectId - abandoning it.", monitoringMailbox.UserPrincipalName, null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1884);
					return false;
				}
			}
			MailboxDatabase mailboxDatabase = this.sessionSelector.TopologyConfigurationSession.Read<MailboxDatabase>(monitoringMailbox.Database);
			if (mailboxDatabase == null)
			{
				WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Failed to look up database for Monitoring mailbox {0} ({1})", monitoringMailbox.UserPrincipalName, monitoringMailbox.DisplayName, null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1894);
				return false;
			}
			if (presumedDatabase != null && presumedDatabase.Guid != mailboxDatabase.Guid)
			{
				WTFDiagnostics.TraceError<string, string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Mailbox {0} ({1}) is supposed to live on database {2} but actually lives on database {3}", monitoringMailbox.UserPrincipalName, monitoringMailbox.DisplayName, presumedDatabase.Name, mailboxDatabase.Name, null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1901);
				return false;
			}
			string databaseActiveHost = DirectoryAccessor.Instance.GetDatabaseActiveHost(mailboxDatabase);
			if (string.IsNullOrWhiteSpace(databaseActiveHost))
			{
				WTFDiagnostics.TraceError<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Failed to look up active host for database {0} when verifying mailbox {1} ({2})", mailboxDatabase.Name, monitoringMailbox.UserPrincipalName, monitoringMailbox.DisplayName, null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1909);
				return false;
			}
			if (!string.Equals(databaseActiveHost, Dns.GetHostName(), StringComparison.OrdinalIgnoreCase))
			{
				Server exchangeServerByName = DirectoryAccessor.Instance.GetExchangeServerByName(databaseActiveHost);
				if (this.IsMonitoringOffline(exchangeServerByName))
				{
					WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Monitoring mailbox {0} ({1}) cannot be used because the monitoring component state of server {2} is offline", monitoringMailbox.UserPrincipalName, monitoringMailbox.DisplayName, databaseActiveHost, null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1922);
					return false;
				}
				if (!this.IsServerCompatible(exchangeServerByName))
				{
					WTFDiagnostics.TraceInformation<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Monitoring mailbox {0} ({1}) cannot be used because server {2} belongs to a different monitoring group", monitoringMailbox.UserPrincipalName, monitoringMailbox.DisplayName, databaseActiveHost, null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1929);
					return false;
				}
				if (string.Compare(exchangeServerByName.SerialNumber, DirectoryAccessor.versionCutoff.ToString(true), true) < 0)
				{
					WTFDiagnostics.TraceInformation<string, string, string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Monitoring mailbox {0} ({1}) cannot be used because server {2} is running build {3} which is older than {4}", monitoringMailbox.UserPrincipalName, monitoringMailbox.DisplayName, databaseActiveHost, exchangeServerByName.SerialNumber, DirectoryAccessor.versionCutoff.ToString(true), null, "CanMonitoringMailboxBeUsed", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1936);
					return false;
				}
			}
			if (presumedDatabase == null)
			{
				presumedDatabase = mailboxDatabase;
			}
			return true;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000CFF1C File Offset: 0x000CE11C
		private T[] GetCandidates<T>(QueryFilter compatibilityFilter, int max) where T : ADConfigurationObject, new()
		{
			T[] array = this.sessionSelector.TopologyConfigurationSession.Find<T>(null, QueryScope.SubTree, compatibilityFilter, new SortBy(ADObjectSchema.Guid, SortOrder.Ascending), 1);
			if (array == null || array.Length == 0)
			{
				return new T[0];
			}
			WTFDiagnostics.TraceInformation<string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidates: lower bound of object {0} with Guid {1}", array[0].Name, array[0].Guid, null, "GetCandidates", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1994);
			T[] array2 = this.sessionSelector.TopologyConfigurationSession.Find<T>(null, QueryScope.SubTree, compatibilityFilter, new SortBy(ADObjectSchema.Guid, SortOrder.Descending), 1);
			WTFDiagnostics.TraceInformation<string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidates: upper bound of object {0} with Guid {1}", array2[0].Name, array2[0].Guid, null, "GetCandidates", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 1996);
			string text = array[0].Guid.ToString("N");
			string text2 = array2[0].Guid.ToString("N");
			uint num = uint.Parse(text.Substring(0, 8), NumberStyles.HexNumber);
			uint num2 = uint.Parse(text2.Substring(0, 8), NumberStyles.HexNumber);
			num = (uint)IPAddress.HostToNetworkOrder((int)num);
			num2 = (uint)IPAddress.HostToNetworkOrder((int)num2);
			uint num3 = num;
			if (num < num2)
			{
				uint hashCode;
				if (this.server.DatabaseAvailabilityGroup != null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidates: server is on a DAG, using DAG guid to compute hash", null, "GetCandidates", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2020);
					hashCode = (uint)this.server.DatabaseAvailabilityGroup.ObjectGuid.GetHashCode();
				}
				else
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidates: server is not on a DAG, using server guid to compute hash", null, "GetCandidates", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2025);
					hashCode = (uint)this.server.Guid.GetHashCode();
				}
				num3 += hashCode % (num2 - num);
			}
			string input = ((uint)IPAddress.NetworkToHostOrder((int)num3)).ToString("x8") + "000000000000000000000000";
			Guid guid = Guid.Parse(input);
			WTFDiagnostics.TraceInformation<Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidates: Guid for filter {0}", guid, null, "GetCandidates", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2037);
			QueryFilter filter;
			if (compatibilityFilter != null)
			{
				filter = new AndFilter(new QueryFilter[]
				{
					compatibilityFilter,
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.Guid, guid)
				});
			}
			else
			{
				filter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.Guid, guid);
			}
			T[] array3 = this.sessionSelector.TopologyConfigurationSession.Find<T>(null, QueryScope.SubTree, filter, new SortBy(ADObjectSchema.Guid, SortOrder.Ascending), max);
			if (array3.Length < max)
			{
				WTFDiagnostics.TraceInformation<int, int>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.GetCandidates: Number of objects is {0} less than required max {1}. Looking up from beginning for the rest", array3.Length, max, null, "GetCandidates", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2055);
				QueryFilter filter2;
				if (compatibilityFilter != null)
				{
					filter2 = new AndFilter(new QueryFilter[]
					{
						compatibilityFilter,
						new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.Guid, array[0].Guid),
						new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.Guid, guid)
					});
				}
				else
				{
					filter2 = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.Guid, array[0].Guid),
						new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.Guid, guid)
					});
				}
				T[] second = this.sessionSelector.TopologyConfigurationSession.Find<T>(null, QueryScope.SubTree, filter2, new SortBy(ADObjectSchema.Guid, SortOrder.Ascending), max - array3.Length);
				array3 = array3.Union(second).ToArray<T>();
			}
			return array3;
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000D0334 File Offset: 0x000CE534
		internal List<AutodiscoverRpcHttpSettings> GetRpcHttpServiceSettings()
		{
			MiniVirtualDirectory[] virtualDirectories = null;
			List<AutodiscoverRpcHttpSettings> list = new List<AutodiscoverRpcHttpSettings>();
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				virtualDirectories = this.getRpcHttpVirtualDirectory();
			});
			if (!adoperationResult.Succeeded || virtualDirectories.Length == 0)
			{
				return list;
			}
			MiniVirtualDirectory virtualDirectory = virtualDirectories[0];
			this.AddRpcHttpSettingsIfAvailable(list, virtualDirectory, ClientAccessType.Internal);
			this.AddRpcHttpSettingsIfAvailable(list, virtualDirectory, ClientAccessType.External);
			return list;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000D039D File Offset: 0x000CE59D
		internal MiniVirtualDirectory[] GetLocalRpcHttpVirtualDirectories()
		{
			return this.sessionSelector.TopologyConfigurationSession.Find<MiniVirtualDirectory>((ADObjectId)this.Server.Identity, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, ADRpcHttpVirtualDirectory.MostDerivedClass), null, 1);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000D03D4 File Offset: 0x000CE5D4
		private void AddRpcHttpSettingsIfAvailable(List<AutodiscoverRpcHttpSettings> settings, MiniVirtualDirectory virtualDirectory, ClientAccessType clientAccessType)
		{
			Uri uri = (clientAccessType == ClientAccessType.Internal) ? virtualDirectory.InternalUrl : virtualDirectory.ExternalUrl;
			if (uri == null)
			{
				return;
			}
			TopologySite topologySite = new TopologySite(this.sessionSelector.TopologyConfigurationSession.GetLocalSite());
			Site site = new Site(topologySite);
			TopologyServer topologyServer = new TopologyServer(this.Server);
			TopologyServerInfo serverInfo = new TopologyServerInfo(site, topologyServer);
			Service service = null;
			if (!RpcHttpService.TryCreateRpcHttpService(virtualDirectory, serverInfo, uri, clientAccessType, AuthenticationMethod.None, out service))
			{
				return;
			}
			RpcHttpService service2 = (RpcHttpService)service;
			AutodiscoverRpcHttpSettings rpcHttpAuthSettingsFromService = AutodiscoverRpcHttpSettings.GetRpcHttpAuthSettingsFromService(service2, clientAccessType, new AutodiscoverRpcHttpSettings.AuthMethodGetter(AutodiscoverRpcHttpSettings.UseProvidedAuthenticationMethod));
			settings.Add(rpcHttpAuthSettingsFromService);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000D0469 File Offset: 0x000CE669
		public string CreatePersonalizedServerName(Guid mailboxGuid, string smtpDomain)
		{
			return ExchangeRpcClientAccess.CreatePersonalizedServer(mailboxGuid, smtpDomain);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000D0472 File Offset: 0x000CE672
		public string CreateAlternateMailboxLegDN(string parentLegacyDNString, Guid mailboxGuid)
		{
			return ADRecipient.CreateAlternateMailboxLegDN(parentLegacyDNString, mailboxGuid);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000D047B File Offset: 0x000CE67B
		internal void RefreshServerOrComputerObject()
		{
			this.server = this.GetExchangeServerByName(Environment.MachineName);
			if (this.server == null)
			{
				this.computer = this.GetNonExchangeServerByName(Environment.MachineName);
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000D04A8 File Offset: 0x000CE6A8
		private static ADRecipient SearchForRecipient(QueryFilter queryFilter, IRecipientSession session, ADObjectId root = null)
		{
			ADRecipient[] array = session.Find(root, QueryScope.SubTree, queryFilter, null, 2);
			if (array == null || array.Length <= 0)
			{
				return null;
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			throw new MultipleRecipientsFoundException(queryFilter.ToString());
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000D04E4 File Offset: 0x000CE6E4
		private bool IsDatabaseCopyActiveOnLocalServer(Guid databaseGuid, MailboxDatabase database)
		{
			bool result = false;
			DatabaseLocationInfo serverForDatabase = this.activeManager.Value.GetServerForDatabase(databaseGuid);
			if (serverForDatabase != null && serverForDatabase.ServerGuid == this.server.Guid)
			{
				result = true;
			}
			else if (serverForDatabase == null)
			{
				if (database == null)
				{
					database = this.GetMailboxDatabaseFromGuid(databaseGuid);
				}
				if (database != null && database.Server.ObjectGuid == this.server.Guid)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000D0558 File Offset: 0x000CE758
		private bool IsGlobalOverridesChangedForType<TWorkDefinition>(IEnumerable<WorkDefinitionOverride> overrides, Container container) where TWorkDefinition : WorkDefinition
		{
			Dictionary<string, WorkDefinitionOverride> dictionary = new Dictionary<string, WorkDefinitionOverride>();
			if (overrides != null)
			{
				foreach (WorkDefinitionOverride workDefinitionOverride in overrides)
				{
					dictionary[workDefinitionOverride.GetIdentityString()] = workDefinitionOverride;
				}
			}
			List<WorkDefinitionOverride> list = this.LoadGlobalOverridesForType<TWorkDefinition>(container);
			foreach (WorkDefinitionOverride workDefinitionOverride2 in list)
			{
				string identityString = workDefinitionOverride2.GetIdentityString();
				if (!dictionary.ContainsKey(identityString))
				{
					return true;
				}
				WorkDefinitionOverride workDefinitionOverride3 = dictionary[identityString];
				if (workDefinitionOverride3.NewPropertyValue != workDefinitionOverride2.NewPropertyValue || workDefinitionOverride3.ExpirationDate != workDefinitionOverride2.ExpirationDate)
				{
					return true;
				}
				dictionary.Remove(identityString);
			}
			return dictionary.Count != 0;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000D065C File Offset: 0x000CE85C
		private List<WorkDefinitionOverride> LoadGlobalOverridesForType<TWorkDefinition>(Container container) where TWorkDefinition : WorkDefinition
		{
			string text = typeof(TWorkDefinition).Name;
			text = text.Substring(0, text.IndexOf("Definition"));
			Container childContainer = container.GetChildContainer(text);
			WTFDiagnostics.TraceInformation<string, Container>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "DirectoryAccessor.LoadGlobalOverridesForType: Override container for {0} is {1}", typeof(TWorkDefinition).Name, childContainer, null, "LoadGlobalOverridesForType", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2338);
			MonitoringOverride[] array = this.sessionSelector.TopologyConfigurationSession.Find<MonitoringOverride>(childContainer.Id, QueryScope.SubTree, null, null, 1000);
			List<WorkDefinitionOverride> list = new List<WorkDefinitionOverride>();
			foreach (MonitoringOverride monitoringOverride in array)
			{
				if ((monitoringOverride.ExpirationTime == null || !(monitoringOverride.ExpirationTime.Value < DateTime.UtcNow)) && (!(monitoringOverride.ApplyVersion != null) || (this.Server != null && !(monitoringOverride.ApplyVersion != this.Server.AdminDisplayVersion))))
				{
					WorkDefinitionOverride item = new WorkDefinitionOverride
					{
						WorkDefinitionName = monitoringOverride.MonitoringItemName,
						ExpirationDate = (monitoringOverride.ExpirationTime ?? DateTime.MaxValue),
						ServiceName = monitoringOverride.HealthSet,
						PropertyName = monitoringOverride.PropertyName,
						NewPropertyValue = monitoringOverride.PropertyValue
					};
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000D07E8 File Offset: 0x000CE9E8
		private string FqdnToName(string fqdn)
		{
			int num = fqdn.IndexOf(".");
			if (num != -1)
			{
				return fqdn.Substring(0, num);
			}
			return fqdn;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000D0810 File Offset: 0x000CEA10
		private bool IsTenantInLocalForest(string tenantName)
		{
			bool flag = false;
			try
			{
				PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(tenantName);
				if (partitionIdByAcceptedDomainName != null)
				{
					flag = ADAccountPartitionLocator.IsKnownPartition(partitionIdByAcceptedDomainName);
					if (!flag)
					{
						WTFDiagnostics.TraceWarning<PartitionId, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Partition {0} from Tenant name : {1} ", partitionIdByAcceptedDomainName, tenantName, null, "IsTenantInLocalForest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2413);
						WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Tenant {0} is not in the local forest. Update is needed to relocate the tenant.", tenantName, null, "IsTenantInLocalForest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2414);
					}
				}
			}
			catch (CannotResolveTenantNameException)
			{
				WTFDiagnostics.TraceWarning<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Error when getting the partition ID with tenant name : {0}", tenantName, null, "IsTenantInLocalForest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs", 2420);
				flag = false;
			}
			return flag;
		}

		// Token: 0x040018B3 RID: 6323
		private const int MaxNumberOfGlobalOverrides = 1000;

		// Token: 0x040018B4 RID: 6324
		private const int MaxADPasswordLength = 128;

		// Token: 0x040018B5 RID: 6325
		private const int MaxLiveIdPasswordLength = 16;

		// Token: 0x040018B6 RID: 6326
		private const int NumOfDigitsForHashing = 8;

		// Token: 0x040018B7 RID: 6327
		private const string PaddingForHashing = "000000000000000000000000";

		// Token: 0x040018B8 RID: 6328
		private const string DatacenterComponentAssemblyName = "Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Datacenter.Components.dll";

		// Token: 0x040018B9 RID: 6329
		private const string LiveIdHelperTypeName = "Microsoft.Exchange.Monitoring.ActiveMonitoring.Common.Datacenter.LiveIdHelper";

		// Token: 0x040018BA RID: 6330
		private readonly Random random = new Random();

		// Token: 0x040018BB RID: 6331
		private static DirectoryAccessor instance = null;

		// Token: 0x040018BC RID: 6332
		private static object locker = new object();

		// Token: 0x040018BD RID: 6333
		private static ServerVersion versionCutoff = new ServerVersion(15, 0, 500, 0);

		// Token: 0x040018BE RID: 6334
		private Server server;

		// Token: 0x040018BF RID: 6335
		private ADComputer computer;

		// Token: 0x040018C0 RID: 6336
		private Guid? globalOverrideWaterMark = null;

		// Token: 0x040018C1 RID: 6337
		private Lazy<ActiveManager> activeManager = new Lazy<ActiveManager>(() => ActiveManager.GetCachingActiveManagerInstance());

		// Token: 0x040018C2 RID: 6338
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x040018C3 RID: 6339
		private DirectoryAccessor.SessionSelector sessionSelector;

		// Token: 0x040018C4 RID: 6340
		private static readonly bool RunningInMultiTenantEnvironment = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

		// Token: 0x040018C5 RID: 6341
		private static readonly bool RunningInDatacenter = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.DirectoryAccessor.Enabled;

		// Token: 0x040018C6 RID: 6342
		private static readonly bool ShouldStampProvisioningConstraint = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.PinMonitoringMailboxesToDatabases.Enabled;

		// Token: 0x040018C7 RID: 6343
		internal DirectoryAccessor.GetRpcHttpVirtualDirectoryStrategy getRpcHttpVirtualDirectory;

		// Token: 0x0200055C RID: 1372
		// (Invoke) Token: 0x06002266 RID: 8806
		internal delegate MiniVirtualDirectory[] GetRpcHttpVirtualDirectoryStrategy();

		// Token: 0x0200055D RID: 1373
		private class SessionSelector
		{
			// Token: 0x06002269 RID: 8809 RVA: 0x000D0948 File Offset: 0x000CEB48
			public SessionSelector()
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
				this.TopologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 2458, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
				this.GcScopedConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 2460, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
				this.GcScopedConfigurationSession.UseConfigNC = false;
				this.GcScopedConfigurationSession.UseGlobalCatalog = true;
				this.RootorgRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 2464, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled)
				{
					this.writableRootorgRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 2468, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
					this.rootOrgAcceptedDomain = this.TopologyConfigurationSession.GetDefaultAcceptedDomain();
				}
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x0600226A RID: 8810 RVA: 0x000D0A30 File Offset: 0x000CEC30
			// (set) Token: 0x0600226B RID: 8811 RVA: 0x000D0A38 File Offset: 0x000CEC38
			public ITopologyConfigurationSession TopologyConfigurationSession { get; private set; }

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x0600226C RID: 8812 RVA: 0x000D0A41 File Offset: 0x000CEC41
			// (set) Token: 0x0600226D RID: 8813 RVA: 0x000D0A49 File Offset: 0x000CEC49
			public ITopologyConfigurationSession GcScopedConfigurationSession { get; private set; }

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x0600226E RID: 8814 RVA: 0x000D0A52 File Offset: 0x000CEC52
			// (set) Token: 0x0600226F RID: 8815 RVA: 0x000D0A5A File Offset: 0x000CEC5A
			public IRecipientSession RootorgRecipientSession { get; private set; }

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06002270 RID: 8816 RVA: 0x000D0A64 File Offset: 0x000CEC64
			public IRecipientSession RecipientSession
			{
				get
				{
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled)
					{
						return this.MonitoringTenantInfo.RecipientSession;
					}
					return this.RootorgRecipientSession;
				}
			}

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x06002271 RID: 8817 RVA: 0x000D0AA4 File Offset: 0x000CECA4
			public IRecipientSession WritableRecipientSession
			{
				get
				{
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled)
					{
						return this.MonitoringTenantInfo.WritableRecipientSession;
					}
					return this.writableRootorgRecipientSession;
				}
			}

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x06002272 RID: 8818 RVA: 0x000D0AE4 File Offset: 0x000CECE4
			public AcceptedDomain AcceptedDomain
			{
				get
				{
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled)
					{
						return this.MonitoringTenantInfo.AcceptedDomain;
					}
					return this.rootOrgAcceptedDomain;
				}
			}

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x06002273 RID: 8819 RVA: 0x000D0B23 File Offset: 0x000CED23
			public DirectoryAccessor.MonitoringTenantInfo MonitoringTenantInfo
			{
				get
				{
					if (Settings.UseE14MonitoringTenant)
					{
						return this.e14Tenant;
					}
					return this.e15Tenant;
				}
			}

			// Token: 0x06002274 RID: 8820 RVA: 0x000D0B3C File Offset: 0x000CED3C
			public void InitializeMonitoringTenants()
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveMonitoring.DirectoryAccessor.Enabled)
				{
					this.e14Tenant = new DirectoryAccessor.MonitoringTenantInfo(string.Empty);
					this.e15Tenant = new DirectoryAccessor.MonitoringTenantInfo("E15");
				}
			}

			// Token: 0x040018CE RID: 6350
			private AcceptedDomain rootOrgAcceptedDomain;

			// Token: 0x040018CF RID: 6351
			private IRecipientSession writableRootorgRecipientSession;

			// Token: 0x040018D0 RID: 6352
			private DirectoryAccessor.MonitoringTenantInfo e14Tenant;

			// Token: 0x040018D1 RID: 6353
			private DirectoryAccessor.MonitoringTenantInfo e15Tenant;
		}

		// Token: 0x0200055E RID: 1374
		private class MonitoringTenantInfo
		{
			// Token: 0x06002275 RID: 8821 RVA: 0x000D0B8C File Offset: 0x000CED8C
			public MonitoringTenantInfo(string suffix)
			{
				this.MonitoringTenantName = MailboxTaskHelper.GetMonitoringTenantName(suffix);
				try
				{
					ADSessionSettings adsessionSettings = ADSessionSettings.FromTenantCUName(this.MonitoringTenantName);
					this.TenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 2599, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
					this.RecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, adsessionSettings, 2600, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
					this.WritableRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, adsessionSettings, 2601, ".ctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\DirectoryAccessor.cs");
					this.AcceptedDomain = this.TenantConfigurationSession.GetDefaultAcceptedDomain();
					this.MonitoringTenantPartitionId = adsessionSettings.PartitionId.ToString();
					this.MonitoringTenantForestFqdn = adsessionSettings.PartitionId.ForestFQDN;
					this.MonitoringTenantOrganizationId = adsessionSettings.CurrentOrganizationId;
					ExchangeConfigurationUnit[] array = this.TenantConfigurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, null, null, 0);
					if (array != null && array.Length == 1 && array[0].OrganizationStatus == OrganizationStatus.Active)
					{
						this.MonitoringTenantReady = true;
					}
				}
				catch (CannotResolveTenantNameException)
				{
				}
			}

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x06002276 RID: 8822 RVA: 0x000D0C9C File Offset: 0x000CEE9C
			// (set) Token: 0x06002277 RID: 8823 RVA: 0x000D0CA4 File Offset: 0x000CEEA4
			public string MonitoringTenantName { get; private set; }

			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x06002278 RID: 8824 RVA: 0x000D0CAD File Offset: 0x000CEEAD
			// (set) Token: 0x06002279 RID: 8825 RVA: 0x000D0CB5 File Offset: 0x000CEEB5
			public IConfigurationSession TenantConfigurationSession { get; private set; }

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x0600227A RID: 8826 RVA: 0x000D0CBE File Offset: 0x000CEEBE
			// (set) Token: 0x0600227B RID: 8827 RVA: 0x000D0CC6 File Offset: 0x000CEEC6
			public IRecipientSession RecipientSession { get; private set; }

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x0600227C RID: 8828 RVA: 0x000D0CCF File Offset: 0x000CEECF
			// (set) Token: 0x0600227D RID: 8829 RVA: 0x000D0CD7 File Offset: 0x000CEED7
			public IRecipientSession WritableRecipientSession { get; private set; }

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x0600227E RID: 8830 RVA: 0x000D0CE0 File Offset: 0x000CEEE0
			// (set) Token: 0x0600227F RID: 8831 RVA: 0x000D0CE8 File Offset: 0x000CEEE8
			public string MonitoringTenantPartitionId { get; private set; }

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06002280 RID: 8832 RVA: 0x000D0CF1 File Offset: 0x000CEEF1
			// (set) Token: 0x06002281 RID: 8833 RVA: 0x000D0CF9 File Offset: 0x000CEEF9
			public OrganizationId MonitoringTenantOrganizationId { get; private set; }

			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x06002282 RID: 8834 RVA: 0x000D0D02 File Offset: 0x000CEF02
			// (set) Token: 0x06002283 RID: 8835 RVA: 0x000D0D0A File Offset: 0x000CEF0A
			public bool MonitoringTenantReady { get; private set; }

			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x06002284 RID: 8836 RVA: 0x000D0D13 File Offset: 0x000CEF13
			// (set) Token: 0x06002285 RID: 8837 RVA: 0x000D0D1B File Offset: 0x000CEF1B
			public string MonitoringTenantForestFqdn { get; private set; }

			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x06002286 RID: 8838 RVA: 0x000D0D24 File Offset: 0x000CEF24
			// (set) Token: 0x06002287 RID: 8839 RVA: 0x000D0D2C File Offset: 0x000CEF2C
			public AcceptedDomain AcceptedDomain { get; private set; }
		}
	}
}
