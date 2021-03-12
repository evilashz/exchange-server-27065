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
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000016 RID: 22
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorRunspaceProxy : DisposeTrackableBase, IAnchorRunspaceProxy, IDisposable
	{
		// Token: 0x06000101 RID: 257 RVA: 0x0000455D File Offset: 0x0000275D
		private AnchorRunspaceProxy(AnchorContext context, AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity runspaceFactory)
		{
			AnchorUtil.ThrowOnNullArgument(context, "context");
			AnchorUtil.ThrowOnNullArgument(runspaceFactory, "runspaceFactory");
			this.Context = context;
			this.runspaceProxy = new RunspaceProxy(new RunspaceMediator(runspaceFactory, new EmptyRunspaceCache()));
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004598 File Offset: 0x00002798
		public RunspaceProxy RunspaceProxy
		{
			get
			{
				return this.runspaceProxy;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000045A0 File Offset: 0x000027A0
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000045A8 File Offset: 0x000027A8
		internal AnchorContext Context { get; private set; }

		// Token: 0x06000105 RID: 261 RVA: 0x000045B1 File Offset: 0x000027B1
		public static AnchorRunspaceProxy CreateRunspaceForDatacenterAdmin(AnchorContext context, ADObjectId ownerId)
		{
			AnchorUtil.ThrowOnNullArgument(ownerId, "ownerId");
			return AnchorRunspaceProxy.CreateRunspaceForDatacenterAdmin(context, ownerId.ToString());
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000045CA File Offset: 0x000027CA
		public static AnchorRunspaceProxy CreateRunspaceForDatacenterAdmin(AnchorContext context, string ownerId)
		{
			AnchorUtil.ThrowOnNullArgument(ownerId, "ownerId");
			context.Logger.Log(MigrationEventType.Verbose, "Creating runspace proxy for datacenter admin", new object[0]);
			return new AnchorRunspaceProxy(context, AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateUnrestrictedFactory(ownerId));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000045FC File Offset: 0x000027FC
		public static AnchorRunspaceProxy CreateRunspaceForTenantAdmin(AnchorContext context, ADObjectId ownerId, ADUser tenantAdmin)
		{
			AnchorUtil.ThrowOnNullArgument(tenantAdmin, "tenantAdmin");
			AnchorUtil.ThrowOnNullArgument(ownerId, "ownerId");
			context.Logger.Log(MigrationEventType.Verbose, "AnchorRunspaceProxy. Creating runspace proxy for user {0}", new object[]
			{
				tenantAdmin.Name
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.None);
			return new AnchorRunspaceProxy(context, AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(context, ownerId.ToString(), new GenericSidIdentity(tenantAdmin.Name, string.Empty, tenantAdmin.Sid), configSettings));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004674 File Offset: 0x00002874
		public static AnchorRunspaceProxy CreateRunspaceForDelegatedTenantAdmin(AnchorContext context, DelegatedPrincipal delegatedTenantAdmin)
		{
			AnchorUtil.ThrowOnNullArgument(delegatedTenantAdmin, "delegatedTenantAdmin");
			context.Logger.Log(MigrationEventType.Verbose, "AnchorRunspaceProxy. Creating delegated runspace proxy for user {0}", new object[]
			{
				delegatedTenantAdmin
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.None);
			return new AnchorRunspaceProxy(context, AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(context, delegatedTenantAdmin.ToString(), delegatedTenantAdmin.Identity, configSettings));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000046CC File Offset: 0x000028CC
		public static AnchorRunspaceProxy CreateRunspaceForPartner(AnchorContext context, ADObjectId ownerId, ADUser tenantAdmin, string tenantOrganization)
		{
			AnchorUtil.ThrowOnNullArgument(ownerId, "ownerId");
			AnchorUtil.ThrowOnNullArgument(tenantAdmin, "tenantAdmin");
			AnchorUtil.ThrowOnNullOrEmptyArgument(tenantOrganization, "tenantOrganization");
			context.Logger.Log(MigrationEventType.Verbose, "AnchorRunspaceProxy. Creating partner runspace proxy for user {0}", new object[]
			{
				tenantAdmin.Name
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, tenantOrganization, ExchangeRunspaceConfigurationSettings.GetDefaultInstance().CurrentSerializationLevel);
			return new AnchorRunspaceProxy(context, AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(context, ownerId.ToString(), new GenericSidIdentity(tenantAdmin.Name, string.Empty, tenantAdmin.Sid), configSettings));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004758 File Offset: 0x00002958
		public static AnchorRunspaceProxy CreateRunspaceForDelegatedPartner(AnchorContext context, DelegatedPrincipal delegatedPartnerAdmin, string tenantOrganization)
		{
			AnchorUtil.ThrowOnNullArgument(delegatedPartnerAdmin, "delegatedTenantAdmin");
			AnchorUtil.ThrowOnNullOrEmptyArgument(tenantOrganization, "tenantOrganization");
			context.Logger.Log(MigrationEventType.Verbose, "AnchorRunspaceProxy. Creating delegated partner runspace proxy for user {0}", new object[]
			{
				delegatedPartnerAdmin
			});
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, tenantOrganization, ExchangeRunspaceConfigurationSettings.GetDefaultInstance().CurrentSerializationLevel);
			return new AnchorRunspaceProxy(context, AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(context, delegatedPartnerAdmin.ToString(), delegatedPartnerAdmin.Identity, configSettings));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000047C4 File Offset: 0x000029C4
		public static string GetCommandString(PSCommand command)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Command command2 in command.Commands)
			{
				stringBuilder.Append(command2.CommandText);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004830 File Offset: 0x00002A30
		public T RunPSCommandSingleOrDefault<T>(PSCommand command, out ErrorRecord error)
		{
			Collection<T> source = this.RunPSCommand<T>(command, out error);
			if (error != null)
			{
				return default(T);
			}
			return source.FirstOrDefault<T>();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000485C File Offset: 0x00002A5C
		public T RunPSCommandSingleOrDefault<T>(PSCommand command) where T : class
		{
			ErrorRecord errorRecord = null;
			string commandString = AnchorRunspaceProxy.GetCommandString(command);
			try
			{
				T result = this.RunPSCommandSingleOrDefault<T>(command, out errorRecord);
				if (errorRecord == null)
				{
					this.Context.Logger.Log(MigrationEventType.Verbose, "Running PS command {0}", new object[]
					{
						commandString
					});
					return result;
				}
			}
			catch (ParameterBindingException ex)
			{
				return this.HandleException<T>(commandString, ex);
			}
			catch (CmdletInvocationException ex2)
			{
				return this.HandleException<T>(commandString, ex2);
			}
			AnchorUtil.AssertOrThrow(errorRecord != null, "expect to have an error at this point", new object[0]);
			if (errorRecord.Exception != null)
			{
				return this.HandleException<T>(commandString, errorRecord.Exception);
			}
			throw new MigrationPermanentException(ServerStrings.MigrationRunspaceError(commandString, errorRecord.ToString()));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004958 File Offset: 0x00002B58
		public Collection<T> RunPSCommand<T>(PSCommand command, out ErrorRecord error)
		{
			PowerShellProxy powerShellProxy = null;
			Collection<T> result = AnchorUtil.RunTimedOperation<Collection<T>>(this.Context, delegate()
			{
				powerShellProxy = new PowerShellProxy(this.runspaceProxy, command);
				return powerShellProxy.Invoke<T>();
			}, AnchorRunspaceProxy.GetCommandString(command));
			if (powerShellProxy.Failed)
			{
				error = powerShellProxy.Errors[0];
				return null;
			}
			error = null;
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000049CC File Offset: 0x00002BCC
		public Collection<T> RunPSCommand<T>(PSCommand command) where T : class
		{
			ErrorRecord errorRecord = null;
			string commandString = AnchorRunspaceProxy.GetCommandString(command);
			try
			{
				Collection<T> result = this.RunPSCommand<T>(command, out errorRecord);
				if (errorRecord == null)
				{
					this.Context.Logger.Log(MigrationEventType.Verbose, "Running PS command {0}", new object[]
					{
						commandString
					});
					return result;
				}
			}
			catch (ParameterBindingException ex)
			{
				this.HandleException<T>(commandString, ex);
			}
			catch (CmdletInvocationException ex2)
			{
				this.HandleException<T>(commandString, ex2);
			}
			AnchorUtil.AssertOrThrow(errorRecord != null, "expect to have an error at this point", new object[0]);
			if (errorRecord.Exception != null)
			{
				this.HandleException<T>(commandString, errorRecord.Exception);
			}
			throw new MigrationPermanentException(ServerStrings.MigrationRunspaceError(commandString, errorRecord.ToString()));
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004A94 File Offset: 0x00002C94
		protected virtual T HandleException<T>(string commandString, Exception ex)
		{
			throw new MigrationPermanentException(ServerStrings.MigrationRunspaceError(commandString, ex.Message), ex);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004AA8 File Offset: 0x00002CA8
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

		// Token: 0x06000112 RID: 274 RVA: 0x00004AC7 File Offset: 0x00002CC7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorRunspaceProxy>(this);
		}

		// Token: 0x0400005C RID: 92
		private RunspaceProxy runspaceProxy;

		// Token: 0x02000017 RID: 23
		private class RunspaceFactoryWithDCAffinity : RunspaceFactory
		{
			// Token: 0x06000113 RID: 275 RVA: 0x00004ACF File Offset: 0x00002CCF
			private RunspaceFactoryWithDCAffinity(string affinityToken, InitialSessionStateFactory issFactory, PSHostFactory hostFactory) : base(issFactory, hostFactory, true)
			{
				AnchorUtil.ThrowOnNullOrEmptyArgument(affinityToken, "affinityToken");
				this.affinityToken = affinityToken;
			}

			// Token: 0x06000114 RID: 276 RVA: 0x00004AEC File Offset: 0x00002CEC
			private RunspaceFactoryWithDCAffinity(string affinityToken, AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory configurationFactory, PSHostFactory hostFactory) : base(configurationFactory, hostFactory)
			{
				AnchorUtil.ThrowOnNullOrEmptyArgument(affinityToken, "affinityToken");
				this.affinityToken = affinityToken;
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00004B08 File Offset: 0x00002D08
			public static AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity CreateUnrestrictedFactory(string affinityToken)
			{
				return new AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity(affinityToken, AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory.GetInstance(), new BasicPSHostFactory(typeof(RunspaceHost), true));
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00004B28 File Offset: 0x00002D28
			public static AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity CreateRbacFactory(AnchorContext context, string affinityToken, IIdentity tenantIdentity, ExchangeRunspaceConfigurationSettings configSettings)
			{
				InitialSessionState initialSessionState;
				try
				{
					initialSessionState = new ExchangeExpiringRunspaceConfiguration(tenantIdentity, configSettings).CreateInitialSessionState();
					initialSessionState.LanguageMode = PSLanguageMode.FullLanguage;
				}
				catch (CmdletAccessDeniedException ex)
				{
					context.Logger.Log(MigrationEventType.Warning, ex, "AnchorRunspaceProxy. error creating session for user {0}", new object[]
					{
						tenantIdentity
					});
					throw new UserDoesNotHaveRBACException(tenantIdentity.ToString(), ex);
				}
				catch (AuthzException ex2)
				{
					context.Logger.Log(MigrationEventType.Error, ex2, "AnchorRunspaceProxy. authorization error creating session for user {0}", new object[]
					{
						tenantIdentity
					});
					throw new UserDoesNotHaveRBACException(tenantIdentity.ToString(), ex2);
				}
				return new AnchorRunspaceProxy.RunspaceFactoryWithDCAffinity(affinityToken, new BasicInitialSessionStateFactory(initialSessionState), new BasicPSHostFactory(typeof(RunspaceHost), true));
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00004BE0 File Offset: 0x00002DE0
			protected override void InitializeRunspace(Runspace runspace)
			{
				base.InitializeRunspace(runspace);
				RunspaceServerSettings value = RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(this.affinityToken, false);
				runspace.SessionStateProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, value);
			}

			// Token: 0x0400005E RID: 94
			private readonly string affinityToken;
		}

		// Token: 0x02000018 RID: 24
		private class FullExchangeRunspaceConfigurationFactory : RunspaceConfigurationFactory
		{
			// Token: 0x06000118 RID: 280 RVA: 0x00004C12 File Offset: 0x00002E12
			public static AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory GetInstance()
			{
				if (AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance == null)
				{
					AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance = new AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory();
				}
				return AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance;
			}

			// Token: 0x06000119 RID: 281 RVA: 0x00004C2C File Offset: 0x00002E2C
			public override RunspaceConfiguration CreateRunspaceConfiguration()
			{
				RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
				AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory.AddPSSnapIn(runspaceConfiguration, "Microsoft.Exchange.Management.PowerShell.E2010");
				return runspaceConfiguration;
			}

			// Token: 0x0600011A RID: 282 RVA: 0x00004C4C File Offset: 0x00002E4C
			private static void AddPSSnapIn(RunspaceConfiguration runspaceConfiguration, string mshSnapInName)
			{
				PSSnapInException ex = null;
				runspaceConfiguration.AddPSSnapIn(mshSnapInName, out ex);
				if (ex != null)
				{
					throw new CouldNotAddExchangeSnapInTransientException(mshSnapInName, ex);
				}
			}

			// Token: 0x0400005F RID: 95
			private static AnchorRunspaceProxy.FullExchangeRunspaceConfigurationFactory instance;
		}
	}
}
