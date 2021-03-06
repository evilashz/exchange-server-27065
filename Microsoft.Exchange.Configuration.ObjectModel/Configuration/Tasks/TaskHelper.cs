using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Remoting;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000AC RID: 172
	public static class TaskHelper
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x00019D14 File Offset: 0x00017F14
		internal static string GetLocalServerFqdn(Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (string.IsNullOrEmpty(TaskHelper.localServerFqdn))
			{
				try
				{
					TaskHelper.localServerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
				}
				catch (CannotGetComputerNameException ex)
				{
					if (writeWarning != null)
					{
						writeWarning(Strings.WarningCannotGetLocalServerFqdn(ex.Message));
					}
				}
			}
			return TaskHelper.localServerFqdn;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00019D68 File Offset: 0x00017F68
		internal static bool ShouldPassDomainControllerToSession(string domainController, ADSessionSettings sessionSettings)
		{
			return string.IsNullOrEmpty(domainController) || !VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled || ADServerSettings.IsServerNamePartitionSameAsPartitionId(domainController, sessionSettings.CurrentOrganizationId.PartitionId.ForestFQDN);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00019DB0 File Offset: 0x00017FB0
		internal static string GetRemoteServerForADUser(ADUser user, Task.TaskVerboseLoggingDelegate writeVerbose, out int serverVersion)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			string result = null;
			serverVersion = Server.E15MinVersion;
			BackEndServer backEndServer = null;
			Exception ex = null;
			try
			{
				backEndServer = BackEndLocator.GetBackEndServer(user);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (backEndServer == null)
			{
				if (writeVerbose != null)
				{
					writeVerbose(Strings.VerboseCannotGetRemoteServerForUser(user.Id.ToString(), user.PrimarySmtpAddress.ToString(), (ex == null) ? "" : ex.ToString()));
				}
				return null;
			}
			serverVersion = backEndServer.Version;
			if (serverVersion < Server.E15MinVersion)
			{
				Uri uri = null;
				try
				{
					uri = BackEndLocator.GetBackEndWebServicesUrl(user);
				}
				catch (Exception ex3)
				{
					ex = ex3;
				}
				if (uri != null)
				{
					result = uri.DnsSafeHost;
				}
				else if (writeVerbose != null)
				{
					writeVerbose(Strings.VerboseCannotGetRemoteServiceUriForUser(user.Id.ToString(), user.PrimarySmtpAddress.ToString(), (ex == null) ? "" : ex.ToString()));
				}
			}
			else
			{
				result = backEndServer.Fqdn;
			}
			return result;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00019EC8 File Offset: 0x000180C8
		public static bool IsTaskKnownException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			for (int i = 0; i < TaskHelper.knownExceptionTypes.Length; i++)
			{
				if (TaskHelper.knownExceptionTypes[i].IsInstanceOfType(exception))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00019F08 File Offset: 0x00018108
		public static bool IsTaskUnhandledException(Exception exception)
		{
			bool flag;
			return !TaskHelper.IsTaskKnownException(exception) && TaskHelper.ShouldReportException(exception, out flag);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00019F28 File Offset: 0x00018128
		public static bool ShouldReportException(Exception e, out bool suppressTaskIO)
		{
			suppressTaskIO = false;
			string fullName = e.GetType().FullName;
			if (e is ThreadAbortException)
			{
				suppressTaskIO = true;
			}
			else if (!"System.Management.Automation.BreakException".Equals(fullName, StringComparison.OrdinalIgnoreCase) && !"System.Management.Automation.ContinueException".Equals(fullName, StringComparison.OrdinalIgnoreCase) && !(e is PipelineStoppedException) && !(e is HostException) && !(e is PSRemotingTransportException) && !(e is PSRemotingDataStructureException) && !(e is CmdletInvocationException))
			{
				return true;
			}
			return false;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00019F98 File Offset: 0x00018198
		public static Assembly[] LoadExchangeAssemblyAndReferences(string targetAssemblyName)
		{
			return TaskHelper.LoadExchangeAssemblyAndReferencesFromSpecificPath(targetAssemblyName, ConfigurationContext.Setup.BinPath);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00019FA8 File Offset: 0x000181A8
		public static Assembly[] LoadExchangeAssemblyAndReferencesFromSpecificPath(string targetAssemblyName, string binPath)
		{
			return TaskHelper.LoadExchangeAssemblyAndReferencesFromSpecificPathForAssemblies(binPath, new string[]
			{
				targetAssemblyName
			});
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00019FC8 File Offset: 0x000181C8
		public static Assembly[] LoadExchangeAssemblyAndReferencesFromSpecificPathForAssemblies(string binPath, params string[] targetAssemblyNames)
		{
			List<Assembly> list = new List<Assembly>();
			FileSearchUtil fileSearchUtil = new FileSearchUtil(binPath, "*.dll");
			List<string> filesRecurse = fileSearchUtil.GetFilesRecurse();
			fileSearchUtil = new FileSearchUtil(binPath, "*.exe");
			filesRecurse.AddRange(fileSearchUtil.GetFilesRecurse());
			fileSearchUtil = new FileSearchUtil(ConfigurationContext.Setup.InstallPath + "\\Public", "*.exe");
			filesRecurse.AddRange(fileSearchUtil.GetFilesRecurse());
			fileSearchUtil = new FileSearchUtil(ConfigurationContext.Setup.InstallPath + "\\Public", "*.dll");
			filesRecurse.AddRange(fileSearchUtil.GetFilesRecurse());
			List<AssemblyName> list2 = new List<AssemblyName>();
			foreach (string path in targetAssemblyNames)
			{
				Assembly assembly = Assembly.LoadFrom(Path.Combine(binPath, path));
				list.Add(assembly);
				foreach (AssemblyName item in assembly.GetReferencedAssemblies())
				{
					if (!list2.Contains(item))
					{
						list2.Add(item);
					}
				}
			}
			if (string.Equals(binPath, ConfigurationContext.Setup.BinPath, StringComparison.OrdinalIgnoreCase))
			{
				Assembly item2 = Assembly.LoadFrom(Path.Combine(binPath, "Microsoft.Isam.Esent.Interop.dll"));
				list.Add(item2);
			}
			foreach (string text in filesRecurse)
			{
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				for (int k = 0; k < list2.Count; k++)
				{
					AssemblyName assemblyName = list2[k];
					if (string.Equals(assemblyName.Name, fileNameWithoutExtension, StringComparison.OrdinalIgnoreCase))
					{
						try
						{
							Assembly assembly2 = Assembly.LoadFrom(text);
							AssemblyName name = assembly2.GetName();
							if (string.Equals(assemblyName.FullName, name.FullName, StringComparison.OrdinalIgnoreCase))
							{
								list.Add(assembly2);
								list2.RemoveAt(k);
								break;
							}
						}
						catch (FileLoadException)
						{
						}
						catch (SecurityException)
						{
						}
					}
				}
			}
			filesRecurse.Clear();
			return list.ToArray();
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001A1CC File Offset: 0x000183CC
		public static OrganizationId ResolveCurrentUserOrganization(out ADObjectId userId)
		{
			userId = null;
			try
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					if (current == null)
					{
						return null;
					}
					IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1263, "ResolveCurrentUserOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\TaskHelper.cs");
					ADRawEntry adrawEntry = rootOrganizationRecipientSession.FindADRawEntryBySid(current.User, new ADPropertyDefinition[]
					{
						ADObjectSchema.RawName,
						ADObjectSchema.Name,
						ADObjectSchema.Id,
						ADObjectSchema.ExchangeVersion,
						ADObjectSchema.OrganizationalUnitRoot,
						ADObjectSchema.ConfigurationUnit
					});
					if (adrawEntry == null)
					{
						return null;
					}
					userId = adrawEntry.Id;
					return (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
				}
			}
			catch (InvalidOperationException e)
			{
				TaskLogger.LogError(e);
			}
			catch (DataSourceOperationException e2)
			{
				TaskLogger.LogError(e2);
			}
			catch (TransientException e3)
			{
				TaskLogger.LogError(e3);
			}
			catch (DataValidationException e4)
			{
				TaskLogger.LogError(e4);
			}
			return null;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001A2F8 File Offset: 0x000184F8
		public static DNWithBinary FindWellKnownObjectEntry(IEnumerable<DNWithBinary> list, Guid wkGuid)
		{
			DNWithBinary result = null;
			byte[] array = wkGuid.ToByteArray();
			foreach (DNWithBinary dnwithBinary in list)
			{
				byte[] binary = dnwithBinary.Binary;
				if (array.Length == binary.Length)
				{
					bool flag = true;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != binary[i])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						result = dnwithBinary;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001A384 File Offset: 0x00018584
		internal static OrganizationId ResolveOrganizationId(ADObjectId objectId, ADObjectId rdnFromContainerToOrgContainer, ITenantConfigurationSession scSession)
		{
			if (scSession == null)
			{
				throw new ArgumentNullException("scSession");
			}
			if (ADObjectId.IsNullOrEmpty(objectId) || string.IsNullOrEmpty(objectId.DistinguishedName))
			{
				throw new ArgumentNullException("objectId");
			}
			if (ADObjectId.IsNullOrEmpty(rdnFromContainerToOrgContainer) || string.IsNullOrEmpty(rdnFromContainerToOrgContainer.DistinguishedName))
			{
				throw new ArgumentNullException("rdnFromContainerToOrgContainer");
			}
			string text = objectId.DistinguishedName;
			string text2 = rdnFromContainerToOrgContainer.DistinguishedName + ",";
			while (text != null)
			{
				int num = text.IndexOf(text2, 0, StringComparison.OrdinalIgnoreCase);
				if (-1 == num)
				{
					break;
				}
				text = text.Substring(num + text2.Length);
				ExchangeConfigurationUnit exchangeConfigurationUnit = scSession.Read<ExchangeConfigurationUnit>(new ADObjectId(text));
				if (exchangeConfigurationUnit != null)
				{
					return exchangeConfigurationUnit.OrganizationId;
				}
			}
			return OrganizationId.ForestWideOrgId;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001A435 File Offset: 0x00018635
		internal static IDirectorySession UnderscopeSessionToOrganization(IDirectorySession session, OrganizationId orgId, bool rehomeDataSession = true)
		{
			return TaskHelper.UnderscopeSessionToOrganization(session, orgId, null, rehomeDataSession);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001A440 File Offset: 0x00018640
		internal static IDirectorySession UnderscopeSessionToOrganization(IDirectorySession session, OrganizationId orgId, ADSessionSettings sSettings, bool rehomeDataSession = true)
		{
			ADSessionSettings adsessionSettings = sSettings ?? session.SessionSettings;
			ADSessionSettings adsessionSettings2 = ADSessionSettings.RescopeToOrganization(adsessionSettings, orgId, rehomeDataSession);
			if (adsessionSettings2 == adsessionSettings)
			{
				return session;
			}
			return ADSession.CreateScopedSession(session, adsessionSettings2);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001A46F File Offset: 0x0001866F
		internal static bool ShouldUnderscopeDataSessionToOrganization(IDirectorySession dataSession, ADObject dataObject)
		{
			return TaskHelper.ShouldUnderscopeDataSessionToOrganization(dataSession, dataObject.OrganizationId);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001A47D File Offset: 0x0001867D
		internal static bool ShouldUnderscopeDataSessionToOrganization(IDirectorySession dataSession, OrganizationId orgId)
		{
			return !orgId.Equals(OrganizationId.ForestWideOrgId) && dataSession.SessionSettings != null && !dataSession.SessionSettings.CurrentOrganizationId.Equals(orgId) && dataSession.ConfigScope == ConfigScopes.TenantLocal;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001A4B3 File Offset: 0x000186B3
		internal static bool IsForestWideADObject(ADObject adObject)
		{
			return adObject != null && OrganizationId.ForestWideOrgId.Equals(adObject.OrganizationId);
		}

		// Token: 0x04000185 RID: 389
		private static string localServerFqdn = null;

		// Token: 0x04000186 RID: 390
		private static Type[] knownExceptionTypes = new Type[]
		{
			typeof(DataSourceOperationException),
			typeof(TransientException),
			typeof(DataValidationException),
			typeof(ProvisioningException),
			typeof(AuthorizationException),
			typeof(CmdletProxyException),
			typeof(NoServersForDatabaseException),
			typeof(WrongServerException),
			typeof(CmdletNeedsProxyException),
			typeof(PSRemotingDataStructureException),
			typeof(PSRemotingTransportException),
			typeof(CmdletInvocationException),
			typeof(LdapException),
			typeof(DirectoryOperationException),
			typeof(ExecutingUserPropertyNotFoundException)
		};
	}
}
