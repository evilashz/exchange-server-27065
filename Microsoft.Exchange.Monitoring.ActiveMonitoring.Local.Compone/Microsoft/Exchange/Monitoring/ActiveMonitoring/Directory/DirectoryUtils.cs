using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Interop.ActiveDS;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x0200013E RID: 318
	public class DirectoryUtils
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x000386E0 File Offset: 0x000368E0
		public static ProbeResult GetLastProbeResult(ProbeWorkItem probe, IProbeWorkBroker broker, CancellationToken cancellationToken)
		{
			ProbeResult lastProbeResult = null;
			if (broker != null)
			{
				IOrderedEnumerable<ProbeResult> query = from r in broker.GetProbeResults(probe.Definition, probe.Result.ExecutionStartTime.AddSeconds((double)(-3 * probe.Definition.RecurrenceIntervalSeconds)))
				orderby r.ExecutionStartTime descending
				select r;
				Task<int> task = broker.AsDataAccessQuery<ProbeResult>(query).ExecuteAsync(delegate(ProbeResult r)
				{
					if (lastProbeResult == null)
					{
						lastProbeResult = r;
					}
				}, cancellationToken, DirectoryUtils.traceContext);
				task.Wait(cancellationToken);
				return lastProbeResult;
			}
			if (ExEnvironment.IsTest)
			{
				return null;
			}
			throw new ArgumentNullException("broker");
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00038790 File Offset: 0x00036990
		public static bool IsRidMaster()
		{
			bool result;
			using (Domain computerDomain = Domain.GetComputerDomain())
			{
				result = computerDomain.RidRoleOwner.Name.StartsWith(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase);
			}
			return result;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000387D8 File Offset: 0x000369D8
		public static bool IsPrimaryActiveManager()
		{
			bool result = false;
			if (ExEnvironment.IsTest)
			{
				return true;
			}
			try
			{
				IADServer localServer = CachedAdReader.Instance.LocalServer;
				if (localServer == null || localServer.DatabaseAvailabilityGroup == null)
				{
					return false;
				}
				IADDatabaseAvailabilityGroup localDAG = CachedAdReader.Instance.LocalDAG;
				if (localDAG != null)
				{
					AmServerName primaryActiveManagerNode = DagTaskHelper.GetPrimaryActiveManagerNode(localDAG);
					if (primaryActiveManagerNode != null)
					{
						result = primaryActiveManagerNode.IsLocalComputerName;
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00038844 File Offset: 0x00036A44
		public static int GetRidsLeft()
		{
			int result;
			using (DirectoryEntry directoryEntry = new DirectoryEntry())
			{
				using (DirectoryEntry directoryEntry2 = new DirectoryEntry("LDAP://CN=RID Manager$,CN=System," + directoryEntry.Properties["distinguishedName"].Value.ToString()))
				{
					IADsLargeInteger iadsLargeInteger = (IADsLargeInteger)directoryEntry2.Properties["rIDAvailablePool"].Value;
					result = iadsLargeInteger.HighPart - iadsLargeInteger.LowPart;
				}
			}
			return result;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000388E0 File Offset: 0x00036AE0
		public static bool GetCredentials(out string username, out string password, out string domain, ProbeWorkItem probe)
		{
			username = string.Empty;
			password = string.Empty;
			domain = string.Empty;
			bool result;
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				ICollection<MailboxDatabaseInfo> collection;
				if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					collection = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
				}
				else
				{
					collection = instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe;
				}
				if (collection.Count == 0)
				{
					probe.Result.StateAttribute2 = DirectoryUtils.ExceptionType.None.ToString();
					result = false;
				}
				else
				{
					Random random = new Random();
					int index = random.Next(0, collection.Count);
					MailboxDatabaseInfo mailboxDatabaseInfo = ((List<MailboxDatabaseInfo>)collection)[index];
					if (string.IsNullOrEmpty(mailboxDatabaseInfo.MonitoringAccount) || string.IsNullOrEmpty(mailboxDatabaseInfo.MonitoringAccountPassword) || string.IsNullOrEmpty(mailboxDatabaseInfo.MonitoringAccountDomain))
					{
						probe.Result.StateAttribute2 = DirectoryUtils.ExceptionType.None.ToString();
						result = false;
					}
					else
					{
						username = mailboxDatabaseInfo.MonitoringAccount + "@" + mailboxDatabaseInfo.MonitoringAccountDomain;
						password = mailboxDatabaseInfo.MonitoringAccountPassword;
						domain = mailboxDatabaseInfo.MonitoringAccountDomain;
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				probe.Result.StateAttribute2 = DirectoryUtils.ExceptionType.None.ToString();
				string stateAttribute = string.Format("GetCredentials get exception for user {0} with psd {1} in domain {2}: {3}", new object[]
				{
					username,
					password,
					domain,
					ex.ToString()
				});
				probe.Result.StateAttribute4 = stateAttribute;
				result = false;
			}
			return result;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00038B24 File Offset: 0x00036D24
		public static bool GetLiveIdProbeCredentials(out string username, out string password, out string domain, ProbeWorkItem probe)
		{
			username = string.Empty;
			password = string.Empty;
			domain = string.Empty;
			bool result;
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				ICollection<MailboxDatabaseInfo> databaseCollection = null;
				if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					databaseCollection = instance.MailboxDatabaseEndpoint.UnverifiedMailboxDatabaseInfoCollectionForBackendLiveIdAuthenticationProbe;
				}
				else
				{
					databaseCollection = instance.MailboxDatabaseEndpoint.UnverifiedMailboxDatabaseInfoCollectionForCafeLiveIdAuthenticationProbe;
				}
				if (databaseCollection.Count == 0)
				{
					probe.Result.StateAttribute2 = DirectoryUtils.ExceptionType.None.ToString();
					result = false;
				}
				else
				{
					Random rand = new Random();
					int randomIndex = rand.Next(0, databaseCollection.Count);
					MailboxDatabaseInfo randomDatabase = ((List<MailboxDatabaseInfo>)databaseCollection)[randomIndex];
					bool flag = DirectoryGeneralUtils.Retry(delegate(bool lastAttempt)
					{
						if (randomDatabase.AuthenticationResult != LiveIdAuthResult.UserNotFoundInAD && randomDatabase.AuthenticationResult != LiveIdAuthResult.ExpiredCreds && randomDatabase.AuthenticationResult != LiveIdAuthResult.InvalidCreds && randomDatabase.AuthenticationResult != LiveIdAuthResult.RecoverableAuthFailure && randomDatabase.AuthenticationResult != LiveIdAuthResult.AmbigiousMailboxFoundFailure && !string.IsNullOrEmpty(randomDatabase.MonitoringAccount) && !string.IsNullOrEmpty(randomDatabase.MonitoringAccountDomain))
						{
							return true;
						}
						randomIndex = rand.Next(0, databaseCollection.Count);
						randomDatabase = ((List<MailboxDatabaseInfo>)databaseCollection)[randomIndex];
						return false;
					}, databaseCollection.Count, TimeSpan.FromMilliseconds(1.0));
					if (flag)
					{
						username = randomDatabase.MonitoringAccountUserPrincipalName;
						domain = randomDatabase.MonitoringAccountDomain;
						try
						{
							password = randomDatabase.MonitoringAccountPassword;
						}
						catch (MailboxNotValidatedException ex)
						{
							password = ex.Password;
						}
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			catch (Exception ex2)
			{
				probe.Result.StateAttribute2 = DirectoryUtils.ExceptionType.None.ToString();
				string stateAttribute = string.Format("GetCredentials get exception for user {0} with psd {1} in domain {2}: {3}", new object[]
				{
					username,
					password,
					domain,
					ex2.ToString()
				});
				probe.Result.StateAttribute4 = stateAttribute;
				result = false;
			}
			return result;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00038CF8 File Offset: 0x00036EF8
		public static string GenerateRandomString(int len)
		{
			if (len > 0 && len <= 32)
			{
				return Guid.NewGuid().ToString("N").Substring(0, len);
			}
			return Guid.NewGuid().ToString("N");
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00038E08 File Offset: 0x00037008
		public static void InvokeBaseResponderMethodIfRequired(ResponderWorkItem responder, Action<CancellationToken> baseDoResponderWork, TracingContext traceContext, CancellationToken cancellationToken)
		{
			IResponderWorkBroker broker = (IResponderWorkBroker)responder.Broker;
			string exceptionType;
			responder.Definition.Attributes.TryGetValue("ExceptionType", out exceptionType);
			if (string.IsNullOrEmpty(exceptionType))
			{
				baseDoResponderWork(cancellationToken);
				return;
			}
			IDataAccessQuery<ResponderResult> lastSuccessfulResponderResult = broker.GetLastSuccessfulResponderResult(responder.Definition);
			Task<ResponderResult> task = lastSuccessfulResponderResult.ExecuteAsync(cancellationToken, traceContext);
			task.Continue(delegate(ResponderResult lastResponderResult)
			{
				DateTime startTime = DateTime.MinValue;
				if (lastResponderResult != null)
				{
					startTime = lastResponderResult.ExecutionStartTime;
				}
				IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = broker.GetLastSuccessfulMonitorResult(responder.Definition.AlertMask, startTime, responder.Result.ExecutionStartTime);
				Task<MonitorResult> task2 = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, traceContext);
				task2.Continue(delegate(MonitorResult lastMonitorResult)
				{
					if (lastMonitorResult != null && lastMonitorResult.IsAlert)
					{
						string stateAttribute = lastMonitorResult.StateAttribute2;
						if (!string.IsNullOrEmpty(stateAttribute) && stateAttribute.IndexOf(exceptionType, StringComparison.InvariantCultureIgnoreCase) >= 0)
						{
							baseDoResponderWork(cancellationToken);
						}
					}
				}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00038ED8 File Offset: 0x000370D8
		public static string GetDomainControllerSite(DirectoryEntry dcEntry)
		{
			string text = string.Empty;
			if (dcEntry != null && dcEntry.Properties.Contains("serverReferenceBL") && dcEntry.Properties["serverReferenceBL"].Value != null)
			{
				string text2 = dcEntry.Properties["serverReferenceBL"].Value.ToString();
				if (text2 != null)
				{
					string[] array = Regex.Split(text2, "CN=");
					if (array.Length > 3)
					{
						text = array[3];
						text = text.Substring(0, text.Length - 1);
					}
				}
			}
			return text;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00038F5C File Offset: 0x0003715C
		public static void CheckSharedConfigurationTenants()
		{
			List<string> mailboxServerVersions = DirectoryUtils.GetMailboxServerVersions();
			PartitionId[] allAccountPartitionIds = ADAccountPartitionLocator.GetAllAccountPartitionIds();
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			string item = string.Empty;
			List<string> list = new List<string>();
			if (allAccountPartitionIds != null && mailboxServerVersions.Count > 0)
			{
				stringBuilder.Append("SCT is not found for the following Version/Offer.\n\nPartitionId, Version, Offer\n");
				PartitionId[] array = allAccountPartitionIds;
				int i = 0;
				while (i < array.Length)
				{
					PartitionId partitionId = array[i];
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 732, "CheckSharedConfigurationTenants", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\DirectoryUtils.cs");
					AndFilter filter = new AndFilter(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, OrganizationSchema.EnableAsSharedConfiguration, true),
						new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.Active)
					});
					ExchangeConfigurationUnit[] array2 = tenantConfigurationSession.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, filter, null, 0);
					if (array2 != null)
					{
						foreach (ExchangeConfigurationUnit exchangeConfigurationUnit in array2)
						{
							SharedConfigurationInfo sharedConfigurationInfo = exchangeConfigurationUnit.SharedConfigurationInfo;
							ServerVersion currentVersion = sharedConfigurationInfo.CurrentVersion;
							item = string.Format("{0}.{1}.{2}.{3}_{4}", new object[]
							{
								currentVersion.Major,
								currentVersion.Minor,
								currentVersion.Build,
								currentVersion.Revision,
								sharedConfigurationInfo.OfferId
							});
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
						using (List<string>.Enumerator enumerator = mailboxServerVersions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								foreach (string text2 in DirectoryUtils.SupportedOffers)
								{
									item = string.Format("{0}_{1}", text, text2);
									if (!list.Contains(item))
									{
										flag = true;
										stringBuilder.AppendFormat("{0},{1},{2}\n", partitionId.ForestFQDN, text.ToString(), text2);
									}
								}
							}
							goto IL_23F;
						}
						goto IL_219;
					}
					goto IL_219;
					IL_23F:
					i++;
					continue;
					IL_219:
					flag2 = true;
					stringBuilder2.AppendFormat("No SCTs found in this AccountPartition: {0}.  Expected SCTs to be created for versions:  {1}\n", partitionId.ForestFQDN, string.Join(",", mailboxServerVersions.ToArray()));
					goto IL_23F;
				}
			}
			StringBuilder stringBuilder3 = new StringBuilder();
			if (flag)
			{
				stringBuilder3.AppendFormat("{0}\n", stringBuilder.ToString());
			}
			if (flag2)
			{
				stringBuilder3.Append(stringBuilder2.ToString());
			}
			if (flag || flag2)
			{
				throw new Exception(stringBuilder3.ToString());
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00039218 File Offset: 0x00037418
		public static List<string> GetMailboxServerVersions()
		{
			List<string> list = new List<string>();
			ADTopologyConfigurationSession adtopologyConfigurationSession = new ADTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet());
			BitMaskAndFilter filter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 2UL);
			ADPagedReader<MiniServer> adpagedReader = adtopologyConfigurationSession.FindPagedMiniServer(null, QueryScope.SubTree, filter, null, 1, null);
			foreach (MiniServer miniServer in adpagedReader)
			{
				string item = string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					miniServer.AdminDisplayVersion.Major,
					miniServer.AdminDisplayVersion.Minor,
					miniServer.AdminDisplayVersion.Build,
					miniServer.AdminDisplayVersion.Revision
				});
				if (!list.Contains(item) && miniServer.AdminDisplayVersion.Major == 15)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00039328 File Offset: 0x00037528
		public static int GetADConnectivityProbeThresholdByEnviornment(int readADConnectivityThreshold)
		{
			if (ExEnvironment.IsTest)
			{
				return 10000;
			}
			return readADConnectivityThreshold;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00039338 File Offset: 0x00037538
		public static bool StartLocalService(string serviceName)
		{
			bool result = false;
			TimeSpan timeout = TimeSpan.FromSeconds(30.0);
			try
			{
				using (ServiceController serviceController = new ServiceController(serviceName))
				{
					if (serviceController != null)
					{
						if (serviceController.Status == ServiceControllerStatus.Running)
						{
							throw new Exception(string.Format("Serivce  {0} not found on local server", serviceName));
						}
						serviceController.Start();
						serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
						result = (serviceController.Status == ServiceControllerStatus.Running);
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000393CC File Offset: 0x000375CC
		public static bool StopLocalService(string serviceName)
		{
			bool result = false;
			TimeSpan timeout = TimeSpan.FromSeconds(30.0);
			try
			{
				using (ServiceController serviceController = new ServiceController(serviceName))
				{
					if (serviceController != null)
					{
						if (serviceController.Status == ServiceControllerStatus.Stopped)
						{
							throw new Exception(string.Format("Serivce  {0} not found on local server", serviceName));
						}
						serviceController.Stop();
						serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
						result = (serviceController.Status == ServiceControllerStatus.Stopped);
					}
				}
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00039460 File Offset: 0x00037660
		public static ServiceControllerStatus GetLocalServiceStatus(string serviceName)
		{
			ServiceControllerStatus status;
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				if (serviceController == null)
				{
					throw new Exception(string.Format("Serivce  {0} not found on local server", serviceName));
				}
				status = serviceController.Status;
			}
			return status;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00039500 File Offset: 0x00037700
		public static void GetServiceIntoExpectedStatus(string serviceName, ServiceControllerStatus expectStatus, int retryCount)
		{
			DirectoryGeneralUtils.Retry(delegate(bool lastAttempt)
			{
				ServiceControllerStatus expectStatus2 = expectStatus;
				if (expectStatus2 != ServiceControllerStatus.Stopped)
				{
					if (expectStatus2 == ServiceControllerStatus.Running)
					{
						DirectoryUtils.StartLocalService(serviceName);
					}
				}
				else
				{
					DirectoryUtils.StopLocalService(serviceName);
				}
				return expectStatus == DirectoryUtils.GetLocalServiceStatus(serviceName);
			}, retryCount, TimeSpan.FromSeconds(10.0));
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00039544 File Offset: 0x00037744
		internal static string GetRegKeyValue(string path, string key, string defaultValue)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(path))
				{
					if (registryKey != null)
					{
						string value = registryKey.GetValue(key) as string;
						if (!string.IsNullOrWhiteSpace(value))
						{
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return defaultValue;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x000395A8 File Offset: 0x000377A8
		public static string GetExchangeBinPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					object value = registryKey.GetValue("MsiInstallPath");
					registryKey.Close();
					if (value == null)
					{
						result = string.Empty;
					}
					else
					{
						result = Path.Combine(value.ToString(), "Bin");
					}
				}
			}
			return result;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0003961C File Offset: 0x0003781C
		internal static void Logger(ProbeWorkItem workitem, StxLogType logType, Action method)
		{
			string errorString = string.Empty;
			bool status = false;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				method();
				status = true;
			}
			catch (Exception ex)
			{
				errorString = ex.ToString();
				throw;
			}
			finally
			{
				stopwatch.Stop();
				StxLoggerBase.GetLoggerInstance(logType).BeginAppend(Dns.GetHostName(), status, stopwatch.Elapsed, 0, errorString, workitem.Result.StateAttribute1, workitem.Result.StateAttribute2, workitem.Result.StateAttribute3, workitem.Result.StateAttribute4);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000396B8 File Offset: 0x000378B8
		internal static string GetReplicationXml(string DomainControllerName, string partition)
		{
			string path = "LDAP://" + DomainControllerName + "/" + partition;
			DirectoryEntry directoryEntry = null;
			string result = string.Empty;
			try
			{
				directoryEntry = new DirectoryEntry(path);
				directoryEntry.RefreshCache();
				string[] propertyNames = new string[]
				{
					"msDS-NCReplInboundNeighbors"
				};
				directoryEntry.RefreshCache(propertyNames);
				StringBuilder stringBuilder = new StringBuilder();
				if (directoryEntry.Properties["msDS-NCReplInboundNeighbors"].Value != null && directoryEntry.Properties["msDS-NCReplInboundNeighbors"].Count > 0)
				{
					int count = directoryEntry.Properties["msDS-NCReplInboundNeighbors"].Count;
					if (count == 1)
					{
						stringBuilder.Append(directoryEntry.Properties["msDS-NCReplInboundNeighbors"].Value.ToString());
					}
					else
					{
						object[] array = (object[])directoryEntry.Properties["msDS-NCReplInboundNeighbors"].Value;
						foreach (object obj in array)
						{
							stringBuilder.Append(obj.ToString());
						}
					}
					result = string.Format("<REPL>{0}</REPL>", stringBuilder.ToString());
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Could not make an LDAP connection to the Domain Controller {0}.  Got exception {1}", DomainControllerName, ex.ToString()));
			}
			finally
			{
				try
				{
					if (directoryEntry != null)
					{
						directoryEntry.Dispose();
					}
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00039858 File Offset: 0x00037A58
		internal static bool VerifyDomainControllerLDAPRead(string dcName, string dnNameToRead)
		{
			string path = string.Format("LDAP://{0}/{1}", dcName, dnNameToRead);
			bool result = false;
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(path))
				{
					if (directoryEntry != null)
					{
						directoryEntry.Properties["distinguishedName"].Value.ToString();
						result = true;
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x000398CC File Offset: 0x00037ACC
		internal static string ListToString(List<string> listToConvert)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in listToConvert)
			{
				stringBuilder.AppendFormat("{0},", arg);
			}
			return stringBuilder.ToString().TrimEnd(new char[]
			{
				','
			});
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00039940 File Offset: 0x00037B40
		internal static string GetDomainControllerOUFormatString(string connectToDC)
		{
			string str = string.Empty;
			string str2 = string.Empty;
			using (DirectoryEntry directoryEntry = new DirectoryEntry())
			{
				if (directoryEntry.Properties.Contains("distinguishedName") && directoryEntry.Properties["distinguishedName"].Value != null)
				{
					str = "OU=Domain Controllers," + directoryEntry.Properties["distinguishedName"].Value.ToString();
					str2 = string.Format("LDAP://{0}/CN=", connectToDC);
					return str2 + "{0}," + str;
				}
			}
			throw new Exception("Could not instantiate DirectoryEntry object which is required to create a LDAP format string.");
		}

		// Token: 0x040006A2 RID: 1698
		public const int DefaultADConnectivityTestThreshold = 10000;

		// Token: 0x040006A3 RID: 1699
		public const int DefaultServiceStartTimeoutInSecs = 30;

		// Token: 0x040006A4 RID: 1700
		public const int DefaultServiceStartRetryInterval = 10;

		// Token: 0x040006A5 RID: 1701
		public const int DefaultLoopMaxRetryCount = 2;

		// Token: 0x040006A6 RID: 1702
		public const string RidThresholdAttribute = "RidsLeftThreshold";

		// Token: 0x040006A7 RID: 1703
		public const string RidsLeftLimitAttribute = "RidsLeftLimit";

		// Token: 0x040006A8 RID: 1704
		public const string RidsLeftLimitLowValueAttribute = "RidsLeftLimitLowValue";

		// Token: 0x040006A9 RID: 1705
		public const string RidsLeftLimitSDFAttribute = "RidsLeftLimitSDF";

		// Token: 0x040006AA RID: 1706
		public const string ExceptionTypAttribute = "ExceptionType";

		// Token: 0x040006AB RID: 1707
		public const string ADConnectivityThresholdAttribute = "ADConnectivityThreshold";

		// Token: 0x040006AC RID: 1708
		public const string KDCStartOnProvisionDCEnabledAttribute = "KDCStartOnProvisionDCEnabled";

		// Token: 0x040006AD RID: 1709
		public const string KDCStopOnMMDCEnabledAttribute = "KDCStopOnMMDCEnabled";

		// Token: 0x040006AE RID: 1710
		public const string ServiceStartStopRetryCountAttribute = "ServiceStartStopRetryCount";

		// Token: 0x040006AF RID: 1711
		public const string LiveIdProbeLatencyThresholdAttribute = "LiveIdProbeLatencyThreshold";

		// Token: 0x040006B0 RID: 1712
		public const string ReplicationThresholdInMinsAttribute = "ReplicationThresholdInMins";

		// Token: 0x040006B1 RID: 1713
		public const string PercentageOfDCsThresholdExcludedForADHealthAttribute = "PercentageOfDCsThresholdExcludedForADHealth";

		// Token: 0x040006B2 RID: 1714
		public const string ApplicationLog = "Application";

		// Token: 0x040006B3 RID: 1715
		public const string MSExchangeADAccessSource = "MSExchange ADAccess";

		// Token: 0x040006B4 RID: 1716
		public const string MSExchangeISSource = "MSExchangeIS*";

		// Token: 0x040006B5 RID: 1717
		public const string MSExchangeLiveIdBasicAuthenticationSource = "MSExchange LiveIdBasicAuthentication";

		// Token: 0x040006B6 RID: 1718
		public const string DirectoryService = "Directory Service";

		// Token: 0x040006B7 RID: 1719
		public const string SystemLog = "System";

		// Token: 0x040006B8 RID: 1720
		public const string NTDSGeneral = "NTDS General";

		// Token: 0x040006B9 RID: 1721
		public const string NTDSSDPROP = "NTDS SDPROP";

		// Token: 0x040006BA RID: 1722
		public const string NTDSReplication = "NTDS Replication";

		// Token: 0x040006BB RID: 1723
		public const string NTDSDatabase = "NTDS Database";

		// Token: 0x040006BC RID: 1724
		public const string NTDSBackup = "NTDS Backup";

		// Token: 0x040006BD RID: 1725
		public const string ActiveDirectoryDomainService = "*ActiveDirectory_DomainService";

		// Token: 0x040006BE RID: 1726
		public const string NTDSISAM = "NTDS ISAM";

		// Token: 0x040006BF RID: 1727
		public const string Kerberos = "Microsoft-Windows-Security-Kerberos";

		// Token: 0x040006C0 RID: 1728
		public const string Adaptec = "Adaptec Storage Manager Agent";

		// Token: 0x040006C1 RID: 1729
		public const string NTDSKCC = "NTDS KCC";

		// Token: 0x040006C2 RID: 1730
		public const string SharedConfigTenantRecovery = "MSExchange Shared Configuration Tenant Recovery";

		// Token: 0x040006C3 RID: 1731
		public const string SharedConfigTenantStateMonitor = "MSExchange Shared Configuration Tenant State Monitor";

		// Token: 0x040006C4 RID: 1732
		public const string MSExchDCMMSource = "MSExchange Domain Controller Maintenance Mode";

		// Token: 0x040006C5 RID: 1733
		public const string MSExchProvisionDCSource = "MSExchange Monitoring Provisioned DCs";

		// Token: 0x040006C6 RID: 1734
		public const string MSExchZombieDCSource = "MSExchange Monitoring ZombieDCs";

		// Token: 0x040006C7 RID: 1735
		public const string MSExchFSMOSource = "MSExchange FSMO Roles";

		// Token: 0x040006C8 RID: 1736
		public const string BPASource = "BPA";

		// Token: 0x040006C9 RID: 1737
		public const string MSExchangeProtectedServiceHost = "MSExchangeProtectedServiceHost";

		// Token: 0x040006CA RID: 1738
		public const string All = "*";

		// Token: 0x040006CB RID: 1739
		public const string AdminDescriptionProperty = "adminDescription";

		// Token: 0x040006CC RID: 1740
		public const string ADHealthProperty = "msExchExtensionAttribute45";

		// Token: 0x040006CD RID: 1741
		public const string SupportedProgramId = "MSOnline";

		// Token: 0x040006CE RID: 1742
		public const string ProvisioningFlagProperty = "msExchProvisioningFlags";

		// Token: 0x040006CF RID: 1743
		public static List<string> SupportedOffers = new List<string>
		{
			"BPOS_Basic_CustomDomain_Hydrated",
			"BPOS_L_Hydrated",
			"BPOS_S_Hydrated",
			"BPOS_M_Hydrated",
			"BPOS_L_Pilot_Hydrated",
			"BPOS_M_Pilot_Hydrated",
			"BPOS_S_Pilot_Hydrated"
		};

		// Token: 0x040006D0 RID: 1744
		private static readonly TracingContext traceContext = TracingContext.Default;

		// Token: 0x0200013F RID: 319
		public enum ExceptionType
		{
			// Token: 0x040006D3 RID: 1747
			None,
			// Token: 0x040006D4 RID: 1748
			AuthenticationFailureNotServiceIssue,
			// Token: 0x040006D5 RID: 1749
			ProtectedServiceHostIssue,
			// Token: 0x040006D6 RID: 1750
			KDCNotRunningOnProvisionedDC,
			// Token: 0x040006D7 RID: 1751
			KDCNotStoppedOnMaintenanceDC
		}

		// Token: 0x02000140 RID: 320
		public enum ResponderChainType
		{
			// Token: 0x040006D9 RID: 1753
			Default,
			// Token: 0x040006DA RID: 1754
			LiveId,
			// Token: 0x040006DB RID: 1755
			DomainController,
			// Token: 0x040006DC RID: 1756
			EscalateOnly,
			// Token: 0x040006DD RID: 1757
			NonUrgentEscalate,
			// Token: 0x040006DE RID: 1758
			Scheduled,
			// Token: 0x040006DF RID: 1759
			PutDCInMM,
			// Token: 0x040006E0 RID: 1760
			PutMultipleDCInMM,
			// Token: 0x040006E1 RID: 1761
			RenameNTDSPowerOff,
			// Token: 0x040006E2 RID: 1762
			DoMT,
			// Token: 0x040006E3 RID: 1763
			TraceAndEscalate,
			// Token: 0x040006E4 RID: 1764
			TraceAndPutInMM,
			// Token: 0x040006E5 RID: 1765
			KDCNotRightStatus,
			// Token: 0x040006E6 RID: 1766
			None
		}
	}
}
