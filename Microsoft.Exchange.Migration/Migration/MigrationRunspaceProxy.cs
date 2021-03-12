using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationRunspaceProxy : DisposeTrackableBase, IMigrationRunspaceProxy, IDisposable
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00005D1C File Offset: 0x00003F1C
		private MigrationRunspaceProxy(MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity runspaceFactory)
		{
			MigrationUtil.ThrowOnNullArgument(runspaceFactory, "runspaceFactory");
			this.runspaceProxy = new RunspaceProxy(new RunspaceMediator(runspaceFactory, new EmptyRunspaceCache()));
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00005D45 File Offset: 0x00003F45
		public RunspaceProxy RunspaceProxy
		{
			get
			{
				return this.runspaceProxy;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005D4D File Offset: 0x00003F4D
		public static MigrationRunspaceProxy CreateRunspaceForDatacenterAdmin(OrganizationId organizationId)
		{
			MigrationUtil.ThrowOnNullArgument(organizationId, "organizationId");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationRunspaceProxy. Creating runspace proxy for datacenter admin", new object[0]);
			return new MigrationRunspaceProxy(MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateUnrestrictedFactory(organizationId));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005D78 File Offset: 0x00003F78
		public static MigrationRunspaceProxy CreateRunspaceForTenantAdmin(ADObjectId ownerId, ADUser tenantAdmin)
		{
			MigrationUtil.ThrowOnNullArgument(tenantAdmin, "tenantAdmin");
			MigrationUtil.ThrowOnNullArgument(ownerId, "ownerId");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationRunspaceProxy. Creating runspace proxy for user {0}", new object[]
			{
				tenantAdmin.Name
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.None);
			return new MigrationRunspaceProxy(MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(tenantAdmin.OrganizationId, new GenericSidIdentity(tenantAdmin.Name, string.Empty, tenantAdmin.Sid), configSettings));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public static MigrationRunspaceProxy CreateRunspaceForDelegatedTenantAdmin(DelegatedPrincipal delegatedTenantAdmin)
		{
			MigrationUtil.ThrowOnNullArgument(delegatedTenantAdmin, "delegatedTenantAdmin");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationRunspaceProxy. Creating delegated runspace proxy for user {0}", new object[]
			{
				delegatedTenantAdmin
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.None);
			return new MigrationRunspaceProxy(MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(MigrationADProvider.GetOrganization(delegatedTenantAdmin.DelegatedOrganization), delegatedTenantAdmin.Identity, configSettings));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005E40 File Offset: 0x00004040
		public static MigrationRunspaceProxy CreateRunspaceForPartner(ADObjectId ownerId, ADUser tenantAdmin, string tenantOrganization)
		{
			MigrationUtil.ThrowOnNullArgument(ownerId, "ownerId");
			MigrationUtil.ThrowOnNullArgument(tenantAdmin, "tenantAdmin");
			MigrationUtil.ThrowOnNullOrEmptyArgument(tenantOrganization, "tenantOrganization");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationRunspaceProxy. Creating partner runspace proxy for user {0}", new object[]
			{
				tenantAdmin.Name
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, tenantOrganization, ExchangeRunspaceConfigurationSettings.GetDefaultInstance().CurrentSerializationLevel);
			return new MigrationRunspaceProxy(MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(tenantAdmin.OrganizationId, new GenericSidIdentity(tenantAdmin.Name, string.Empty, tenantAdmin.Sid), configSettings));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005EC4 File Offset: 0x000040C4
		public static MigrationRunspaceProxy CreateRunspaceForDelegatedPartner(DelegatedPrincipal delegatedPartnerAdmin, string tenantOrganization)
		{
			MigrationUtil.ThrowOnNullArgument(delegatedPartnerAdmin, "delegatedTenantAdmin");
			MigrationUtil.ThrowOnNullOrEmptyArgument(tenantOrganization, "tenantOrganization");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationRunspaceProxy. Creating delegated partner runspace proxy for user {0}", new object[]
			{
				delegatedPartnerAdmin
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, tenantOrganization, ExchangeRunspaceConfigurationSettings.GetDefaultInstance().CurrentSerializationLevel);
			return new MigrationRunspaceProxy(MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(MigrationADProvider.GetOrganization(tenantOrganization), delegatedPartnerAdmin.Identity, configSettings));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005F28 File Offset: 0x00004128
		public static string GetCommandString(PSCommand command)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Command command2 in command.Commands)
			{
				stringBuilder.Append(command2.CommandText);
				foreach (CommandParameter commandParameter in command2.Parameters)
				{
					stringBuilder.AppendFormat(" -{0}", commandParameter.Name);
				}
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006014 File Offset: 0x00004214
		public T RunPSCommand<T>(PSCommand command, out ErrorRecord error)
		{
			PowerShellProxy powerShellProxy = null;
			Collection<T> source = MigrationUtil.RunTimedOperation<Collection<T>>(delegate()
			{
				powerShellProxy = new PowerShellProxy(this.runspaceProxy, command);
				return powerShellProxy.Invoke<T>();
			}, MigrationRunspaceProxy.GetCommandString(command));
			if (powerShellProxy.Failed)
			{
				error = powerShellProxy.Errors[0];
				return default(T);
			}
			error = null;
			return source.FirstOrDefault<T>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000608C File Offset: 0x0000428C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.runspaceProxy != null)
				{
					this.runspaceProxy.Dispose();
				}
				this.runspaceProxy = null;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000060AB File Offset: 0x000042AB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationRunspaceProxy>(this);
		}

		// Token: 0x0400003A RID: 58
		private RunspaceProxy runspaceProxy;

		// Token: 0x0200001D RID: 29
		private class RunspaceFactoryWithDCAffinity : RunspaceFactory
		{
			// Token: 0x060000AA RID: 170 RVA: 0x000060B3 File Offset: 0x000042B3
			private RunspaceFactoryWithDCAffinity(OrganizationId organizationId, InitialSessionStateFactory issFactory, PSHostFactory hostFactory) : base(issFactory, hostFactory, true)
			{
				MigrationUtil.ThrowOnNullArgument(organizationId, "organizationId");
				this.organizationId = organizationId;
			}

			// Token: 0x060000AB RID: 171 RVA: 0x000060D0 File Offset: 0x000042D0
			private RunspaceFactoryWithDCAffinity(OrganizationId organizationId, MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory configurationFactory, PSHostFactory hostFactory) : base(configurationFactory, hostFactory)
			{
				MigrationUtil.ThrowOnNullArgument(organizationId, "organizationId");
				this.organizationId = organizationId;
			}

			// Token: 0x060000AC RID: 172 RVA: 0x000060EC File Offset: 0x000042EC
			public static MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity CreateUnrestrictedFactory(OrganizationId organizationId)
			{
				return new MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity(organizationId, MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory.GetInstance(), new BasicPSHostFactory(typeof(RunspaceHost), true));
			}

			// Token: 0x060000AD RID: 173 RVA: 0x0000610C File Offset: 0x0000430C
			public static MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity CreateRbacFactory(OrganizationId organizationId, IIdentity tenantIdentity, ExchangeRunspaceConfigurationSettings configSettings)
			{
				InitialSessionState initialSessionState;
				try
				{
					initialSessionState = new ExchangeExpiringRunspaceConfiguration(tenantIdentity, configSettings).CreateInitialSessionState();
					initialSessionState.LanguageMode = PSLanguageMode.FullLanguage;
				}
				catch (CmdletAccessDeniedException ex)
				{
					MigrationLogger.Log(MigrationEventType.Warning, ex, "MigrationRunspaceProxy. error creating session for user {0}", new object[]
					{
						tenantIdentity
					});
					throw new UserDoesNotHaveRBACException(tenantIdentity.ToString(), ex);
				}
				catch (AuthzException ex2)
				{
					MigrationLogger.Log(MigrationEventType.Error, ex2, "MigrationRunspaceProxy. authorization error creating session for user {0}", new object[]
					{
						tenantIdentity
					});
					throw new UserDoesNotHaveRBACException(tenantIdentity.ToString(), ex2);
				}
				return new MigrationRunspaceProxy.RunspaceFactoryWithDCAffinity(organizationId, new BasicInitialSessionStateFactory(initialSessionState), new BasicPSHostFactory(typeof(RunspaceHost), true));
			}

			// Token: 0x060000AE RID: 174 RVA: 0x000061B8 File Offset: 0x000043B8
			protected override void InitializeRunspace(Runspace runspace)
			{
				base.InitializeRunspace(runspace);
				MigrationLogger.Log(MigrationEventType.Verbose, "Initializing runspace for organization {0}", new object[]
				{
					this.organizationId
				});
				string token = (this.organizationId == OrganizationId.ForestWideOrgId) ? "RootOrg" : RunspaceServerSettings.GetTokenForOrganization(this.organizationId);
				RunspaceServerSettings runspaceServerSettings;
				if (this.organizationId != null && !this.organizationId.PartitionId.IsLocalForestPartition())
				{
					runspaceServerSettings = RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(token, this.organizationId.PartitionId.ForestFQDN, false);
					runspaceServerSettings.RecipientViewRoot = ADSystemConfigurationSession.GetRootOrgContainerId(null, null).DomainId;
				}
				else
				{
					runspaceServerSettings = RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(token, false);
				}
				runspace.SessionStateProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, runspaceServerSettings);
			}

			// Token: 0x0400003B RID: 59
			private readonly OrganizationId organizationId;
		}

		// Token: 0x0200001E RID: 30
		private class FullExchangeRunspaceConfigurationFactory : RunspaceConfigurationFactory
		{
			// Token: 0x060000AF RID: 175 RVA: 0x00006272 File Offset: 0x00004472
			public static MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory GetInstance()
			{
				if (MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance == null)
				{
					MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance = new MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory();
				}
				return MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance;
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x0000628C File Offset: 0x0000448C
			public override RunspaceConfiguration CreateRunspaceConfiguration()
			{
				RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
				MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory.AddPSSnapIn(runspaceConfiguration, "Microsoft.Exchange.Management.PowerShell.E2010");
				return runspaceConfiguration;
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x000062AC File Offset: 0x000044AC
			private static void AddPSSnapIn(RunspaceConfiguration runspaceConfiguration, string mshSnapInName)
			{
				PSSnapInException ex;
				runspaceConfiguration.AddPSSnapIn(mshSnapInName, out ex);
				if (ex != null)
				{
					MigrationLogger.Log(MigrationEventType.Warning, ex, "MigrationRunspaceProxy.AddPSSnapIn: error creating loading exchange snappin {0}", new object[]
					{
						mshSnapInName
					});
					throw new CouldNotAddExchangeSnapInTransientException(mshSnapInName, ex);
				}
			}

			// Token: 0x0400003C RID: 60
			private static MigrationRunspaceProxy.FullExchangeRunspaceConfigurationFactory instance;
		}
	}
}
