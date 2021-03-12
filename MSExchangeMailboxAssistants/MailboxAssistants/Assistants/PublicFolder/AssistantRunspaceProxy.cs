using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AssistantRunspaceProxy : DisposeTrackableBase, IAssistantRunspaceProxy, IDisposable
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x00056D57 File Offset: 0x00054F57
		private AssistantRunspaceProxy(AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity runspaceFactory)
		{
			ArgumentValidator.ThrowIfNull("runspaceFactory", runspaceFactory);
			this.runspaceProxy = new RunspaceProxy(new RunspaceMediator(runspaceFactory, new EmptyRunspaceCache()));
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00056D80 File Offset: 0x00054F80
		public RunspaceProxy RunspaceProxy
		{
			get
			{
				return this.runspaceProxy;
			}
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00056D88 File Offset: 0x00054F88
		public static AssistantRunspaceProxy CreateRunspaceForDatacenterAdmin(OrganizationId organizationId)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			return new AssistantRunspaceProxy(AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateUnrestrictedFactory(organizationId));
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00056DA0 File Offset: 0x00054FA0
		public static AssistantRunspaceProxy CreateRunspaceForTenantAdmin(ADObjectId ownerId, ADUser tenantAdmin)
		{
			ArgumentValidator.ThrowIfNull("tenantAdmin", tenantAdmin);
			ArgumentValidator.ThrowIfNull("ownerId", ownerId);
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.None);
			return new AssistantRunspaceProxy(AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(tenantAdmin.OrganizationId, new GenericSidIdentity(tenantAdmin.Name, string.Empty, tenantAdmin.Sid), configSettings));
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00056DF4 File Offset: 0x00054FF4
		public static AssistantRunspaceProxy CreateRunspaceForPartner(ADObjectId ownerId, ADUser tenantAdmin, string tenantOrganization, IPublicFolderMailboxLoggerBase logger)
		{
			ArgumentValidator.ThrowIfNull("ownerId", ownerId);
			ArgumentValidator.ThrowIfNull("tenantAdmin", tenantAdmin);
			ArgumentValidator.ThrowIfNullOrEmpty("tenantOrganization", tenantOrganization);
			logger.LogEvent(LogEventType.Verbose, string.Format("AssistantRunspaceProxy. Creating partner runspace proxy for user {0}", tenantAdmin.Name));
			ExchangeRunspaceConfigurationSettings configSettings = new ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration, tenantOrganization, ExchangeRunspaceConfigurationSettings.GetDefaultInstance().CurrentSerializationLevel);
			return new AssistantRunspaceProxy(AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity.CreateRbacFactory(tenantAdmin.OrganizationId, new GenericSidIdentity(tenantAdmin.Name, string.Empty, tenantAdmin.Sid), configSettings));
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00056E74 File Offset: 0x00055074
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

		// Token: 0x06000E75 RID: 3701 RVA: 0x00056F2C File Offset: 0x0005512C
		public T RunPSCommand<T>(PSCommand command, out ErrorRecord error, IPublicFolderMailboxLoggerBase logger) where T : class
		{
			Collection<ErrorRecord> collection = null;
			Collection<T> collection2 = this.RunPSCommand<T>(command, out collection, logger);
			if (collection != null && collection.Count > 0)
			{
				error = collection[0];
			}
			else
			{
				error = null;
			}
			if (collection2 != null && collection2.Count > 0)
			{
				return collection2[0];
			}
			return default(T);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00056FB0 File Offset: 0x000551B0
		private Collection<T> RunPSCommand<T>(PSCommand command, out Collection<ErrorRecord> errors, IPublicFolderMailboxLoggerBase logger)
		{
			errors = null;
			PowerShellProxy powerShellProxy = null;
			Collection<T> result = AssistantRunspaceProxy.RunTimedOperation<Collection<T>>(delegate()
			{
				powerShellProxy = new PowerShellProxy(this.runspaceProxy, command);
				return powerShellProxy.Invoke<T>();
			}, AssistantRunspaceProxy.GetCommandString(command), logger);
			if (powerShellProxy.Failed)
			{
				errors = powerShellProxy.Errors;
			}
			return result;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00057014 File Offset: 0x00055214
		public Collection<PSObject> RunPowershellScript(string scriptFile, Dictionary<string, string> scriptParameters, out Collection<ErrorRecord> errors, IPublicFolderMailboxLoggerBase logger)
		{
			errors = null;
			Command command = new Command(scriptFile, false);
			if (scriptParameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in scriptParameters)
				{
					CommandParameter item;
					if (keyValuePair.Value.ToString().Contains("SwitchValue"))
					{
						item = new CommandParameter(keyValuePair.Key, new SwitchParameter(true));
					}
					else
					{
						item = new CommandParameter(keyValuePair.Key, keyValuePair.Value);
					}
					command.Parameters.Add(item);
				}
			}
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand(command);
			return this.RunPSCommand<PSObject>(pscommand, out errors, logger);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000570D8 File Offset: 0x000552D8
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

		// Token: 0x06000E79 RID: 3705 RVA: 0x000570F7 File Offset: 0x000552F7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AssistantRunspaceProxy>(this);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00057118 File Offset: 0x00055318
		private static void RunTimedOperation(Action operation, object debugInfo, IPublicFolderMailboxLoggerBase logger)
		{
			AssistantRunspaceProxy.RunTimedOperation<int>(delegate()
			{
				operation();
				return 0;
			}, debugInfo, logger);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00057148 File Offset: 0x00055348
		private static T RunTimedOperation<T>(Func<T> operation, object debugInfo, IPublicFolderMailboxLoggerBase logger)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			T result;
			try
			{
				result = operation();
			}
			finally
			{
				stopwatch.Stop();
				TimeSpan timeoutForSynchronousOperation = PublicFolderSplitConfig.Instance.TimeoutForSynchronousOperation;
				if (timeoutForSynchronousOperation < stopwatch.Elapsed)
				{
					logger.LogEvent(LogEventType.Error, string.Format("SLOW Operation: took {0}s using '{1}' stack trace {2}", stopwatch.Elapsed.Seconds, debugInfo, AssistantRunspaceProxy.GetCurrentStackTrace()));
				}
				else
				{
					logger.LogEvent(LogEventType.Verbose, string.Format("Operation: took {0} using '{1}'", stopwatch.Elapsed, debugInfo));
				}
			}
			return result;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000571E0 File Offset: 0x000553E0
		internal static string GetCurrentStackTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StackTrace stackTrace = new StackTrace(2, false);
			foreach (StackFrame stackFrame in stackTrace.GetFrames().Take(10))
			{
				MethodBase method = stackFrame.GetMethod();
				stringBuilder.AppendFormat("{0}:{1};", method.DeclaringType, method);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000946 RID: 2374
		private RunspaceProxy runspaceProxy;

		// Token: 0x02000166 RID: 358
		private class RunspaceFactoryWithDCAffinity : RunspaceFactory
		{
			// Token: 0x06000E7D RID: 3709 RVA: 0x00057260 File Offset: 0x00055460
			private RunspaceFactoryWithDCAffinity(OrganizationId organizationId, InitialSessionStateFactory issFactory, PSHostFactory hostFactory) : base(issFactory, hostFactory, true)
			{
				ArgumentValidator.ThrowIfNull("organizationId", organizationId);
				this.organizationId = organizationId;
			}

			// Token: 0x06000E7E RID: 3710 RVA: 0x0005727D File Offset: 0x0005547D
			private RunspaceFactoryWithDCAffinity(OrganizationId organizationId, AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory configurationFactory, PSHostFactory hostFactory) : base(configurationFactory, hostFactory)
			{
				ArgumentValidator.ThrowIfNull("organizationId", organizationId);
				this.organizationId = organizationId;
			}

			// Token: 0x06000E7F RID: 3711 RVA: 0x00057299 File Offset: 0x00055499
			public static AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity CreateUnrestrictedFactory(OrganizationId organizationId)
			{
				return new AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity(organizationId, AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory.GetInstance(), new BasicPSHostFactory(typeof(RunspaceHost), true));
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x000572B8 File Offset: 0x000554B8
			public static AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity CreateRbacFactory(OrganizationId organizationId, IIdentity tenantIdentity, ExchangeRunspaceConfigurationSettings configSettings)
			{
				InitialSessionState initialSessionState;
				try
				{
					initialSessionState = new ExchangeExpiringRunspaceConfiguration(tenantIdentity, configSettings).CreateInitialSessionState();
					initialSessionState.LanguageMode = PSLanguageMode.FullLanguage;
				}
				catch (CmdletAccessDeniedException innerException)
				{
					throw new UserDoesNotHaveRBACException(tenantIdentity.ToString(), innerException);
				}
				catch (AuthzException innerException2)
				{
					throw new UserDoesNotHaveRBACException(tenantIdentity.ToString(), innerException2);
				}
				return new AssistantRunspaceProxy.RunspaceFactoryWithDCAffinity(organizationId, new BasicInitialSessionStateFactory(initialSessionState), new BasicPSHostFactory(typeof(RunspaceHost), true));
			}

			// Token: 0x06000E81 RID: 3713 RVA: 0x00057330 File Offset: 0x00055530
			protected override void InitializeRunspace(Runspace runspace)
			{
				base.InitializeRunspace(runspace);
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

			// Token: 0x04000947 RID: 2375
			private readonly OrganizationId organizationId;
		}

		// Token: 0x02000167 RID: 359
		private class FullExchangeRunspaceConfigurationFactory : RunspaceConfigurationFactory
		{
			// Token: 0x06000E82 RID: 3714 RVA: 0x000573CE File Offset: 0x000555CE
			public static AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory GetInstance()
			{
				if (AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance == null)
				{
					AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance = new AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory();
				}
				return AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory.instance;
			}

			// Token: 0x06000E83 RID: 3715 RVA: 0x000573E8 File Offset: 0x000555E8
			public override RunspaceConfiguration CreateRunspaceConfiguration()
			{
				RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
				AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory.AddPSSnapIn(runspaceConfiguration, "Microsoft.Exchange.Management.PowerShell.E2010");
				return runspaceConfiguration;
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x00057408 File Offset: 0x00055608
			private static void AddPSSnapIn(RunspaceConfiguration runspaceConfiguration, string mshSnapInName)
			{
				PSSnapInException ex;
				runspaceConfiguration.AddPSSnapIn(mshSnapInName, out ex);
				if (ex != null)
				{
					throw new CouldNotAddExchangeSnapInTransientException(mshSnapInName, ex);
				}
			}

			// Token: 0x04000948 RID: 2376
			private static AssistantRunspaceProxy.FullExchangeRunspaceConfigurationFactory instance;
		}
	}
}
