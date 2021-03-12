using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Directory.TopologyDiscovery
{
	// Token: 0x020006C9 RID: 1737
	internal sealed class SuitabilityVerifier
	{
		// Token: 0x06005049 RID: 20553 RVA: 0x001275C0 File Offset: 0x001257C0
		private static bool IsSetupOrTestContext()
		{
			ADDriverContext processADContext = ADSessionSettings.GetProcessADContext();
			return (processADContext != null && (processADContext.Mode == ContextMode.Setup || processADContext.Mode == ContextMode.Test)) || ExEnvironment.IsTestDomain;
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x001275F0 File Offset: 0x001257F0
		internal static bool IsOSVersionSuitable(string serverName, string osVersion, string osServicePack)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentNullException("serverName");
			}
			if (string.IsNullOrEmpty(osVersion))
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string>(0L, "{0} - IsOSVersionSuitable was invoked with a null or empty osVersion parameter.", serverName);
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			Match match = new Regex("^(\\d+)\\.(\\d+) \\(\\d+\\)$").Match(osVersion);
			if (!match.Success)
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>(0L, "{0} - osVersion '{1}' does not match the OSVersionPattern.", serverName, osVersion);
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			int num;
			try
			{
				num = (int)short.Parse(match.Groups[1].Value);
			}
			catch (OverflowException)
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>(0L, "{0} - Parsing major version number '{1}' has thrown an OverflowException.", serverName, match.Groups[1].Value);
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			if (num > 5)
			{
				return true;
			}
			if (num < 5)
			{
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			int num2;
			try
			{
				num2 = (int)short.Parse(match.Groups[2].Value);
			}
			catch (OverflowException)
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>(0L, "{0} - Parsing minor version number '{1}' has thrown an OverflowException.", serverName, match.Groups[2].Value);
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			if (num2 > 2)
			{
				return true;
			}
			if (num2 < 2)
			{
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			if (string.IsNullOrEmpty(osServicePack))
			{
				SuitabilityVerifier.LogNoSuitableOsEvent(serverName, osVersion, osServicePack);
				return false;
			}
			return true;
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x00127758 File Offset: 0x00125958
		private static void LogNoSuitableOsEvent(string serverName, string osVersion, string spVersion)
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_BAD_OS_VERSION, "OsSuitability" + serverName, new object[]
			{
				serverName,
				string.IsNullOrEmpty(osVersion) ? "<null>" : osVersion,
				string.IsNullOrEmpty(spVersion) ? "<null>" : spVersion
			});
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x001277B0 File Offset: 0x001259B0
		private static void LogEventSyncFailed(string serverName, int errorCode, string errorMessage, SearchRequest request)
		{
			string text;
			object obj;
			object obj2;
			if (request != null)
			{
				text = request.DistinguishedName;
				obj = request.Filter;
				obj2 = request.Scope;
			}
			else
			{
				text = string.Empty;
				obj = string.Empty;
				obj2 = string.Empty;
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_SYNC_FAILED, "LdapSearchFailedOn" + serverName, new object[]
			{
				serverName,
				errorCode,
				text,
				obj,
				obj2,
				errorMessage
			});
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0012782C File Offset: 0x00125A2C
		private static PooledLdapConnection CreateConnectionAndBind(string serverFqdn, NetworkCredential networkCredential, int portNumber)
		{
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, int>(0L, "{0} - CreateConnectionAndBind. Port Number {1}", serverFqdn, portNumber);
			ADServerRole role = (3268 == portNumber) ? ADServerRole.GlobalCatalog : ADServerRole.DomainController;
			ADServerInfo serverInfo = new ADServerInfo(serverFqdn, portNumber, null, 100, AuthType.Negotiate);
			PooledLdapConnection pooledLdapConnection = new PooledLdapConnection(serverInfo, role, false, networkCredential);
			int num = 0;
			ADErrorRecord aderrorRecord;
			for (;;)
			{
				aderrorRecord = null;
				if (pooledLdapConnection.TryBindWithRetry(1, out aderrorRecord))
				{
					break;
				}
				num++;
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_BIND_FAILED, "BindFailedOn" + serverFqdn, new object[]
				{
					serverFqdn,
					(int)aderrorRecord.LdapError,
					portNumber,
					aderrorRecord.Message
				});
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError(0L, "CreateConnectionAndBind. Failed to create and bind AD connection against server '{0}' to perform the suitability check. Connection: {1}, Retry attempt: '{2}'. Error: {3}", new object[]
				{
					serverFqdn,
					pooledLdapConnection,
					num,
					aderrorRecord
				});
				if (aderrorRecord.LdapError != LdapError.InvalidCredentials)
				{
					goto IL_10E;
				}
				if (aderrorRecord.HandlingType == HandlingType.Throw || num == 3)
				{
					goto IL_F7;
				}
				Thread.Sleep(3000);
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, int>(0L, "CreateConnectionAndBind. Successfully created and bound AD connection against server '{0}' to perform the suitability check. Port: '{1}'", serverFqdn, portNumber);
			return pooledLdapConnection;
			IL_F7:
			throw aderrorRecord.InnerException;
			IL_10E:
			throw new SuitabilityDirectoryException(serverFqdn, (int)aderrorRecord.LdapError, aderrorRecord.Message, aderrorRecord.InnerException)
			{
				ServerFqdn = serverFqdn
			};
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00128304 File Offset: 0x00126504
		private static IEnumerable<Task> SuitabilityChecks(SuitabilityVerifier.SuitabilityCheckContext context, NetworkCredential credential, bool doFullCheck, bool isInitialDiscovery)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug((long)context.GetHashCode(), "{0} - Starting suitabilities checks. DoFullCheck {1}. IsInitialDiscovery {2}. NetworkCredentials {3}", new object[]
			{
				context.ServerFqdn,
				doFullCheck,
				isInitialDiscovery,
				(credential == null) ? "<NULL>" : credential.Domain
			});
			Task activeTask = null;
			if (doFullCheck)
			{
				activeTask = SuitabilityVerifier.CheckDNS(context);
				yield return activeTask;
			}
			else
			{
				context.SuitabilityResult.IsDNSEntryAvailable = true;
			}
			if (!context.SuitabilityResult.IsDNSEntryAvailable)
			{
				yield return DnsTroubleshooter.DiagnoseDnsProblemForDomainController(context.ServerFqdn);
				if (activeTask.IsFaulted)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
					{
						context.ServerFqdn,
						activeTask.Exception.ToString() + " (in CheckDNS)"
					});
					throw activeTask.Exception;
				}
			}
			else
			{
				if (doFullCheck)
				{
					foreach (Task task in SuitabilityVerifier.PerformReachabilityChecks(context))
					{
						activeTask = task;
						yield return task;
					}
					if (activeTask.IsFaulted)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
						{
							context.ServerFqdn,
							activeTask.Exception.ToString() + " (in PerformReachabilityChecks)"
						});
						throw activeTask.Exception;
					}
				}
				else
				{
					ADServerRole adserverRole = ADServerRole.None;
					if (context.GCPort >= 0)
					{
						adserverRole |= ADServerRole.GlobalCatalog;
					}
					if (context.DCPort >= 0)
					{
						adserverRole |= (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController);
					}
					context.SuitabilityResult.IsReachableByTCPConnection = adserverRole;
				}
				SuitabilityVerifier.CheckCreateConnection(context, credential);
				activeTask = SuitabilityVerifier.GetDefaultNCResponse(context);
				yield return activeTask;
				if (activeTask.IsFaulted)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
					{
						context.ServerFqdn,
						activeTask.Exception.ToString() + " (in GetDefaultNCResponse)"
					});
					throw activeTask.Exception;
				}
				SuitabilityVerifier.CheckIsSyncronized(context);
				bool isForestLocal = SuitabilityVerifier.IsForestLocal(context.ServerFqdn);
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)context.GetHashCode(), "{0} - SACL and critical data checks {1} be performed", context.ServerFqdn, (doFullCheck && isForestLocal) ? "WILL" : "WON'T");
				if (doFullCheck && isForestLocal)
				{
					activeTask = SuitabilityVerifier.CheckSACLRight(context);
					yield return activeTask;
					if (activeTask.IsFaulted)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
						{
							context.ServerFqdn,
							activeTask.Exception.ToString() + " (in CheckSACLRight)"
						});
						throw activeTask.Exception;
					}
					activeTask = SuitabilityVerifier.CheckCriticalData(context);
					yield return activeTask;
					if (activeTask.IsFaulted)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
						{
							context.ServerFqdn,
							activeTask.Exception.ToString() + " (in CheckCriticalData)"
						});
						throw activeTask.Exception;
					}
				}
				else
				{
					context.SuitabilityResult.IsSACLRightAvailable = true;
					context.SuitabilityResult.IsCriticalDataAvailable = true;
				}
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)context.GetHashCode(), "{0} - OS version checks {1} be performed", context.ServerFqdn, (!context.AllowPreW2KSP3DC) ? "WILL" : "WON'T");
				if (!context.AllowPreW2KSP3DC)
				{
					activeTask = SuitabilityVerifier.CheckOperatingSystemSuitable(context);
					yield return activeTask;
					if (activeTask.IsFaulted)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
						{
							context.ServerFqdn,
							activeTask.Exception.ToString() + " (in CheckOperatingSystemSuitable)"
						});
						throw activeTask.Exception;
					}
				}
				else
				{
					context.SuitabilityResult.IsOSVersionSuitable = true;
				}
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)context.GetHashCode(), "{0} - PDC checks {1} be performed", context.ServerFqdn, context.IsPDCCheckEnabled ? "WILL" : "WONT");
				if (context.IsPDCCheckEnabled)
				{
					activeTask = SuitabilityVerifier.CheckPDC(context);
					yield return activeTask;
					if (activeTask.IsFaulted)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
						{
							context.ServerFqdn,
							activeTask.Exception.ToString() + " (in CheckPDC)"
						});
						throw activeTask.Exception;
					}
				}
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)context.GetHashCode(), "{0} - MM checks {1} be performed", context.ServerFqdn, (Globals.IsDatacenter && !context.AllowPreW2KSP3DC) ? "WILL" : "WONT");
				if (Globals.IsDatacenter && !context.AllowPreW2KSP3DC)
				{
					activeTask = SuitabilityVerifier.CheckIsDCInMaintenanceMode(context);
					yield return activeTask;
					if (activeTask.IsFaulted)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
						{
							context.ServerFqdn,
							activeTask.Exception.ToString() + " (in CheckIsDCInMaintenanceMode)"
						});
						throw activeTask.Exception;
					}
				}
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)context.GetHashCode(), "{0} - Net logon checks {1} be performed", context.ServerFqdn, (doFullCheck && !isInitialDiscovery && !context.DisableNetLogonCheck) ? "WILL" : "WON'T");
				if (doFullCheck && !isInitialDiscovery && !context.DisableNetLogonCheck)
				{
					SuitabilityVerifier.CheckNetLogon(context);
				}
				else
				{
					context.SuitabilityResult.IsNetlogonAllowed = (ADServerRole.GlobalCatalog | ADServerRole.DomainController | ADServerRole.ConfigurationDomainController);
				}
			}
			yield break;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00128338 File Offset: 0x00126538
		private static bool InternalTryCheckIsServerSuitable(string fqdn, bool isGlobalCatalog, NetworkCredential credential, out string writableNC, out string siteName, out Exception exception)
		{
			siteName = null;
			writableNC = null;
			exception = null;
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = new SuitabilityVerifier.SuitabilityCheckContext(fqdn, isGlobalCatalog, false, false, false);
			try
			{
				Task task = Task.Factory.Iterate(SuitabilityVerifier.SuitabilityChecks(suitabilityCheckContext, credential, false, false));
				task.Wait();
			}
			catch (AggregateException ex)
			{
				Exception ex2 = ex.Flatten();
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>(0L, "{0} Suitability Error: {1}", suitabilityCheckContext.ServerFqdn, ex2.ToString());
				ex2 = ex2.InnerException;
				if (!(ex2 is SuitabilityException) && !(ex2 is ADOperationException) && !(ex2 is ADExternalException) && !(ex2 is ADTransientException) && !(ex2 is LdapException) && !(ex2 is DirectoryOperationException) && !(ex2 is ADServerNotSuitableException))
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_SUITABILITY_CHECK_FAILED, null, new object[]
					{
						suitabilityCheckContext.ServerFqdn,
						ex2.ToString()
					});
					throw ex2;
				}
				exception = ex2;
			}
			finally
			{
				suitabilityCheckContext.CloseDCConnection();
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, SuitabilityCheckResult>(0L, "{0} Suitabilities for server {1}", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.SuitabilityResult);
			writableNC = suitabilityCheckContext.SuitabilityResult.WritableNC;
			siteName = suitabilityCheckContext.SiteName;
			return suitabilityCheckContext.SuitabilityResult.IsSuitable(isGlobalCatalog ? ADServerRole.GlobalCatalog : ADServerRole.DomainController);
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00128480 File Offset: 0x00126680
		private static Task<IPHostEntry> StartCheckDNSData(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartCheckDNSData", context.ServerFqdn);
			Tuple<SuitabilityVerifier.SuitabilityCheckContext, int> state = new Tuple<SuitabilityVerifier.SuitabilityCheckContext, int>(context, Environment.TickCount);
			return Task.Factory.FromAsync<string, IPHostEntry>(new Func<string, AsyncCallback, object, IAsyncResult>(Dns.BeginGetHostEntry), new Func<IAsyncResult, IPHostEntry>(Dns.EndGetHostEntry), context.ServerFqdn, state, TaskCreationOptions.AttachedToParent);
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x001284EC File Offset: 0x001266EC
		private static void EndCheckDNSData(Task<IPHostEntry> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<Tuple<SuitabilityVerifier.SuitabilityCheckContext, int>>("Async Context", task.AsyncState);
			Tuple<SuitabilityVerifier.SuitabilityCheckContext, int> tuple = (Tuple<SuitabilityVerifier.SuitabilityCheckContext, int>)task.AsyncState;
			SuitabilityVerifier.SuitabilityCheckContext item = tuple.Item1;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string>((long)item.GetHashCode(), "{0} - Check DNS Entry", item.ServerFqdn);
			int item2 = tuple.Item2;
			ADProviderPerf.UpdateDCCounter(item.ServerFqdn, Counter.DCTimeGethostbyname, UpdateType.Update, Convert.ToUInt32(Globals.GetTickDifference(item2, Environment.TickCount)));
			if (task.Status == TaskStatus.RanToCompletion)
			{
				IPHostEntry result = task.Result;
				if (result != null && result.AddressList != null && result.AddressList.Length > 0)
				{
					item.SuitabilityResult.IsDNSEntryAvailable = true;
					item.HostName = result.HostName;
					item.IPAddresses = result.AddressList;
				}
				else
				{
					item.SuitabilityResult.IsDNSEntryAvailable = false;
				}
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, bool>((long)item.GetHashCode(), "{0} - Check DNS Entry. Value {1}", item.ServerFqdn, item.SuitabilityResult.IsDNSEntryAvailable);
			Exception ex = null;
			if (task.Status != TaskStatus.RanToCompletion)
			{
				item.SuitabilityResult.IsDNSEntryAvailable = false;
				ex = ((task.Exception != null) ? task.Exception.GetBaseException() : null);
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>((long)item.GetHashCode(), "{0} - Check DNS Failed. Error {1}", item.ServerFqdn, (ex == null) ? "UnKnown" : ex.Message);
				if (ex != null)
				{
					SocketException ex2 = ex as SocketException;
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_GETHOSTBYNAME_FAILED, null, new object[]
					{
						item.ServerFqdn,
						(ex2 != null) ? ex2.NativeErrorCode : -1,
						ex.Message
					});
				}
			}
			if (!item.SuitabilityResult.IsDNSEntryAvailable)
			{
				LocalizedString errorMessage = DirectoryStrings.SuitabilityErrorDNS(item.ServerFqdn, (ex != null) ? ex.Message : "Unknown");
				SuitabilityVerifier.ThrowException(ex, errorMessage, item.ServerFqdn);
			}
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x001286CC File Offset: 0x001268CC
		private static Task CheckDNS(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckDNS", context.ServerFqdn);
			Task<IPHostEntry> task = SuitabilityVerifier.StartCheckDNSData(context);
			return task.ContinueWith(new Action<Task<IPHostEntry>>(SuitabilityVerifier.EndCheckDNSData), TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0012871C File Offset: 0x0012691C
		private static Task StartCheckReachabilityForPort(SuitabilityVerifier.SuitabilityCheckContext context, IPAddress[] addresses, int port)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNegative("port", port);
			ArgumentValidator.ThrowIfNull("addresses", addresses);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string, int>((long)context.GetHashCode(), "{0} - StartCheckReachabilityForPort {1}", context.ServerFqdn, port);
			if (string.IsNullOrEmpty(context.HostName))
			{
				throw new InvalidOperationException("HostName shouldn't be null at this point.");
			}
			if (addresses.Length == 0)
			{
				throw new InvalidOperationException("IpAddresses should have at least one ipAddress.");
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, AddressFamily>((long)context.GetHashCode(), "{0} - Reachability checks will be done using {1}", context.ServerFqdn, addresses[0].AddressFamily);
			TcpClient tcpClient = new TcpClient(addresses[0].AddressFamily);
			Tuple<SuitabilityVerifier.SuitabilityCheckContext, TcpClient, int> state = new Tuple<SuitabilityVerifier.SuitabilityCheckContext, TcpClient, int>(context, tcpClient, port);
			return Task.Factory.FromAsync<IPAddress[], int>(new Func<IPAddress[], int, AsyncCallback, object, IAsyncResult>(tcpClient.BeginConnect), new Action<IAsyncResult>(tcpClient.EndConnect), addresses, port, state, TaskCreationOptions.AttachedToParent);
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x001287F0 File Offset: 0x001269F0
		private static void EndCheckReachabilityForPort(Task task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<Tuple<SuitabilityVerifier.SuitabilityCheckContext, TcpClient, int>>("SuitabilityContext", task.AsyncState);
			Tuple<SuitabilityVerifier.SuitabilityCheckContext, TcpClient, int> tuple = (Tuple<SuitabilityVerifier.SuitabilityCheckContext, TcpClient, int>)task.AsyncState;
			SuitabilityVerifier.SuitabilityCheckContext item = tuple.Item1;
			TcpClient item2 = tuple.Item2;
			int item3 = tuple.Item3;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)item.GetHashCode(), "{0} - EndCheckReachabilityForPort", item.ServerFqdn);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, int, bool>((long)item.GetHashCode(), "{0} - EndCheckReachabilityForPort {1}. TCP Client Is Connected {2}", item.ServerFqdn, item3, item2.Connected);
			if (item2.Connected)
			{
				using (NetworkStream stream = item2.GetStream())
				{
					ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug((long)item.GetHashCode(), "{0} - EndCheckReachabilityForPort {1}. CanRead {2} CanWrite {3}", new object[]
					{
						item.ServerFqdn,
						item3,
						stream.CanRead,
						stream.CanWrite
					});
					if (stream.CanRead && stream.CanWrite)
					{
						if (item3 == item.DCPort)
						{
							lock (item.SuitabilityResult)
							{
								item.SuitabilityResult.IsReachableByTCPConnection |= (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController);
								goto IL_15A;
							}
						}
						lock (item.SuitabilityResult)
						{
							item.SuitabilityResult.IsReachableByTCPConnection |= ADServerRole.GlobalCatalog;
						}
					}
					IL_15A:;
				}
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, int, ADServerRole>((long)item.GetHashCode(), "{0} - CheckReachabilityForPort {1}. Value {2}", item.ServerFqdn, item3, item.SuitabilityResult.IsReachableByTCPConnection);
			item2.Close();
			((IDisposable)item2).Dispose();
			if (task.Status != TaskStatus.RanToCompletion && task.Exception != null)
			{
				Exception baseException = task.Exception.GetBaseException();
				SuitabilityVerifier.ThrowException(baseException, DirectoryStrings.SuitabilityReachabilityError(item.ServerFqdn, item3.ToString(), baseException.Message), item.ServerFqdn);
			}
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x00128A08 File Offset: 0x00126C08
		private static Task CheckReachabilityForPort(SuitabilityVerifier.SuitabilityCheckContext context, IPAddress[] addresses, int port)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("addresses", addresses);
			ArgumentValidator.ThrowIfNegative("port", port);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string, int>((long)context.GetHashCode(), "{0} - CheckReachabilityForPort {1}", context.ServerFqdn, port);
			Task task = SuitabilityVerifier.StartCheckReachabilityForPort(context, addresses, port);
			return task.ContinueWith(new Action<Task>(SuitabilityVerifier.EndCheckReachabilityForPort), TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x00128FC4 File Offset: 0x001271C4
		private static IEnumerable<Task> PerformReachabilityChecks(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - PerformReachabilityChecks", context.ServerFqdn);
			if (context.IPAddresses == null)
			{
				throw new InvalidOperationException("IpAddresses shouldn't be null at this point.");
			}
			if (context.IPAddresses.Length == 0)
			{
				throw new InvalidOperationException("IpAddresses should have at least one ipAddress.");
			}
			List<IPAddress> ipv6 = new List<IPAddress>(Math.Max(context.IPAddresses.Length / 2, 1));
			List<IPAddress> ipv7 = new List<IPAddress>(Math.Max(context.IPAddresses.Length / 2, 1));
			foreach (IPAddress ipaddress in context.IPAddresses)
			{
				if (AddressFamily.InterNetworkV6 == ipaddress.AddressFamily)
				{
					ipv6.Add(ipaddress);
				}
				else if (AddressFamily.InterNetwork == ipaddress.AddressFamily)
				{
					ipv7.Add(ipaddress);
				}
			}
			List<List<IPAddress>> ipAddressesByFamily = new List<List<IPAddress>>((ipv6.Count > 0 && ipv7.Count > 0) ? 2 : 1);
			if (ipv7.Count > 0)
			{
				ipAddressesByFamily.Add(ipv7);
			}
			if (ipv6.Count > 0)
			{
				ipAddressesByFamily.Add(ipv6);
			}
			string aggregatedErrorMessage = string.Empty;
			foreach (List<IPAddress> ipAddresses in ipAddressesByFamily)
			{
				List<Task> reachabilityChecks = new List<Task>(2);
				if (context.DCPort >= 0)
				{
					ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string>((long)context.GetHashCode(), "{0} - DC Reachability will be checked", context.ServerFqdn);
					Task item = SuitabilityVerifier.CheckReachabilityForPort(context, ipAddresses.ToArray(), context.DCPort);
					reachabilityChecks.Add(item);
				}
				if (context.GCPort >= 0)
				{
					ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string>((long)context.GetHashCode(), "{0} - GC Reachability will be checked", context.ServerFqdn);
					Task item2 = SuitabilityVerifier.CheckReachabilityForPort(context, ipAddresses.ToArray(), context.GCPort);
					reachabilityChecks.Add(item2);
				}
				yield return Task.WhenAll(reachabilityChecks.ToArray());
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, AddressFamily>((long)context.GetHashCode(), "{0} - Validating Reachability checks for Address Family {1}", context.ServerFqdn, ipAddresses[0].AddressFamily);
				bool checkNextAddressFamily = false;
				foreach (Task task in reachabilityChecks)
				{
					checkNextAddressFamily |= (task.Status != TaskStatus.RanToCompletion);
					if (task.Exception != null)
					{
						ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, Exception>((long)context.GetHashCode(), "{0} - Reachability checks failed. Error {1}", context.ServerFqdn, task.Exception.GetBaseException());
						Exception baseException = task.Exception.GetBaseException();
						aggregatedErrorMessage = string.Format("{0}{1}{2}", aggregatedErrorMessage, Environment.NewLine, baseException.Message);
					}
				}
				if (!checkNextAddressFamily)
				{
					break;
				}
			}
			if (context.SuitabilityResult.IsReachableByTCPConnection == ADServerRole.None)
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string>((long)context.GetHashCode(), "{0} - All Reachability checks failed", context.ServerFqdn);
				throw new SuitabilityException(DirectoryStrings.ComposedSuitabilityReachabilityError(context.ServerFqdn, aggregatedErrorMessage), context.ServerFqdn);
			}
			yield break;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00128FE4 File Offset: 0x001271E4
		private static Task<DirectoryResponse> StartGetDefaultNamingContexts(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartGetDefaultNamingContexts", context.ServerFqdn);
			SearchRequest request = new SearchRequest(string.Empty, "(objectclass=*)", SearchScope.Base, SuitabilityVerifier.DefaultNamingContextProperties);
			return SuitabilityVerifier.SendAsyncLdapRequest(context.DCConnection, request, context);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0012903C File Offset: 0x0012723C
		private static void EndGetDefaultNCResponse(Task<DirectoryResponse> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<SuitabilityVerifier.SuitabilityCheckContext>("Async Context", task.AsyncState);
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = (SuitabilityVerifier.SuitabilityCheckContext)task.AsyncState;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndGetDefaultNCResponse", suitabilityCheckContext.ServerFqdn);
			SearchResponse searchResponse = (SearchResponse)task.Result;
			if (searchResponse == null || searchResponse.Entries.Count <= 0)
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckDefaultNCResponse. Error trying to retrieve all the necessary information to continue with suitability checks", suitabilityCheckContext.ServerFqdn);
				throw new ADExternalException(DirectoryStrings.ErrorIsServerSuitableMissingDefaultNamingContext(suitabilityCheckContext.DCConnection.SessionOptions.HostName));
			}
			if (!searchResponse.Entries[0].Attributes.Contains("configurationNamingContext") || !searchResponse.Entries[0].Attributes.Contains("defaultNamingContext") || !searchResponse.Entries[0].Attributes.Contains("supportedCapabilities") || !searchResponse.Entries[0].Attributes.Contains("schemaNamingContext"))
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckDefaultNCResponse. Could not retrieve all the necessary information to continue with suitability checks", suitabilityCheckContext.ServerFqdn);
				throw new ADExternalException(DirectoryStrings.ErrorIsServerSuitableMissingDefaultNamingContext(suitabilityCheckContext.DCConnection.SessionOptions.HostName));
			}
			string[] array = (string[])searchResponse.Entries[0].Attributes["supportedCapabilities"].GetValues(typeof(string));
			foreach (string a in array)
			{
				if (string.Equals(a, "1.2.840.113556.1.4.1920", StringComparison.OrdinalIgnoreCase))
				{
					ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckOperatingSystemSuitable. DC is RODC", suitabilityCheckContext.ServerFqdn);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_RODC_FOUND, "OsSuitability" + suitabilityCheckContext.DCConnection.SessionOptions.HostName, new object[]
					{
						suitabilityCheckContext.DCConnection.SessionOptions.HostName
					});
					suitabilityCheckContext.SuitabilityResult.IsReadOnlyDC = true;
					throw new ADServerNotSuitableException(DirectoryStrings.ErrorIsServerSuitableRODC(suitabilityCheckContext.DCConnection.SessionOptions.HostName), suitabilityCheckContext.DCConnection.SessionOptions.HostName);
				}
			}
			suitabilityCheckContext.DefaultNCResponse = searchResponse;
			suitabilityCheckContext.SuitabilityResult.RootNC = (string)suitabilityCheckContext.DefaultNCResponse.Entries[0].Attributes["defaultNamingContext"].GetValues(typeof(string))[0];
			suitabilityCheckContext.SuitabilityResult.ConfigNC = (string)suitabilityCheckContext.DefaultNCResponse.Entries[0].Attributes["configurationNamingContext"].GetValues(typeof(string))[0];
			suitabilityCheckContext.SuitabilityResult.SchemaNC = (string)suitabilityCheckContext.DefaultNCResponse.Entries[0].Attributes["schemaNamingContext"].GetValues(typeof(string))[0];
			if (!suitabilityCheckContext.SuitabilityResult.IsReadOnlyDC)
			{
				suitabilityCheckContext.SuitabilityResult.WritableNC = suitabilityCheckContext.SuitabilityResult.RootNC;
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug((long)suitabilityCheckContext.GetHashCode(), "{0} - PopulateDefaultNamingContexts. RootNC {1} ConfigNC {2} SchemaNC {3}", new object[]
			{
				suitabilityCheckContext.ServerFqdn,
				suitabilityCheckContext.SuitabilityResult.RootNC,
				suitabilityCheckContext.SuitabilityResult.ConfigNC,
				suitabilityCheckContext.SuitabilityResult.SchemaNC
			});
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x001293C4 File Offset: 0x001275C4
		private static Task GetDefaultNCResponse(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - GetDefaultNCResponse", context.ServerFqdn);
			Task<DirectoryResponse> task = SuitabilityVerifier.StartGetDefaultNamingContexts(context);
			return task.ContinueWith(new Action<Task<DirectoryResponse>>(SuitabilityVerifier.EndGetDefaultNCResponse), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00129418 File Offset: 0x00127618
		private static Task<DirectoryResponse> StartCheckSACLRight(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartCheckSACLRight", context.ServerFqdn);
			SearchRequest request = new SearchRequest(context.SuitabilityResult.ConfigNC, "(objectclass=configuration)", SearchScope.Base, SuitabilityVerifier.SaclRightProperties);
			return SuitabilityVerifier.SendAsyncLdapRequest(context.DCConnection, request, context);
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x00129478 File Offset: 0x00127678
		private static void EndCheckSACLRight(Task<DirectoryResponse> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<SuitabilityVerifier.SuitabilityCheckContext>("Async Context", task.AsyncState);
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = (SuitabilityVerifier.SuitabilityCheckContext)task.AsyncState;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndCheckSACLRight", suitabilityCheckContext.ServerFqdn);
			SearchResponse searchResponse = (SearchResponse)task.Result;
			if (searchResponse != null && searchResponse.Entries.Count > 0 && searchResponse.Entries[0].Attributes.Contains("ntSecurityDescriptor"))
			{
				suitabilityCheckContext.SuitabilityResult.IsSACLRightAvailable = true;
			}
			else
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_NO_SACL, null, new object[]
				{
					suitabilityCheckContext.ServerFqdn,
					suitabilityCheckContext.ServerFqdn
				});
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, bool>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckSACLRight. Value {1}", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.SuitabilityResult.IsSACLRightAvailable);
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x00129554 File Offset: 0x00127754
		private static Task CheckSACLRight(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckSACLRight", context.ServerFqdn);
			Task<DirectoryResponse> task = SuitabilityVerifier.StartCheckSACLRight(context);
			return task.ContinueWith(new Action<Task<DirectoryResponse>>(SuitabilityVerifier.EndCheckSACLRight), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x001295A8 File Offset: 0x001277A8
		private static Task<DirectoryResponse> StartCheckCriticalData(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartCheckCriticalData", context.ServerFqdn);
			SearchRequest request = new SearchRequest(context.SuitabilityResult.ConfigNC, string.Format("(&(objectCategory=msExchExchangeServer)(cn={0}))", NativeHelpers.GetLocalComputerFqdn(true).Split(new char[]
			{
				'.'
			})[0]), SearchScope.Subtree, SuitabilityVerifier.CriticalDataProperties);
			return SuitabilityVerifier.SendAsyncLdapRequest(context.DCConnection, request, context);
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x00129624 File Offset: 0x00127824
		private static void EndCheckCriticalData(Task<DirectoryResponse> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<SuitabilityVerifier.SuitabilityCheckContext>("Async Context", task.AsyncState);
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = (SuitabilityVerifier.SuitabilityCheckContext)task.AsyncState;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndCheckCriticalData", suitabilityCheckContext.ServerFqdn);
			SearchResponse searchResponse = (SearchResponse)task.Result;
			if ((searchResponse != null & searchResponse.Entries.Count > 0) && searchResponse.Entries[0].Attributes.Contains("objectClass"))
			{
				suitabilityCheckContext.SuitabilityResult.IsCriticalDataAvailable = true;
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, bool>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckCriticalData. Value {1}", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.SuitabilityResult.IsCriticalDataAvailable);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x001296E0 File Offset: 0x001278E0
		private static Task CheckCriticalData(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckCriticalData", context.ServerFqdn);
			Task<DirectoryResponse> task = SuitabilityVerifier.StartCheckCriticalData(context);
			return task.ContinueWith(new Action<Task<DirectoryResponse>>(SuitabilityVerifier.EndCheckCriticalData), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00129734 File Offset: 0x00127934
		private static Task<DirectoryResponse> StartCheckOperatingSystemSuitable(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartCheckOperatingSystemSuitable", context.ServerFqdn);
			SearchRequest request = new SearchRequest(context.SuitabilityResult.RootNC, string.Format("(&(objectClass=computer)(servicePrincipalName=HOST/{0}))", context.ServerFqdn.Trim()), SearchScope.Subtree, SuitabilityVerifier.OSSuitableProperties);
			return SuitabilityVerifier.SendAsyncLdapRequest(context.DCConnection, request, context);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x001297A4 File Offset: 0x001279A4
		private static void EndCheckOperatingSystemSuitable(Task<DirectoryResponse> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<SuitabilityVerifier.SuitabilityCheckContext>("Async Context", task.AsyncState);
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = (SuitabilityVerifier.SuitabilityCheckContext)task.AsyncState;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndCheckOperatingSystemSuitable", suitabilityCheckContext.ServerFqdn);
			SearchResponse searchResponse = (SearchResponse)task.Result;
			if (searchResponse == null || searchResponse.Entries.Count == 0 || !searchResponse.Entries[0].Attributes.Contains("operatingSystemVersion"))
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckOperatingSystemSuitable. Could not retrieve all the necessary information from '{1}' to perform the suitability check", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.DCConnection.SessionOptions.HostName);
				throw new ADExternalException(DirectoryStrings.ErrorIsServerSuitableMissingComputerData(suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.DCConnection.SessionOptions.HostName));
			}
			suitabilityCheckContext.OperatingSystemResponse = searchResponse;
			string osVersion = (string)suitabilityCheckContext.OperatingSystemResponse.Entries[0].Attributes["operatingSystemVersion"].GetValues(typeof(string))[0];
			string osServicePack = string.Empty;
			if (suitabilityCheckContext.OperatingSystemResponse.Entries[0].Attributes.Contains("operatingSystemServicePack"))
			{
				osServicePack = (string)suitabilityCheckContext.OperatingSystemResponse.Entries[0].Attributes["operatingSystemServicePack"].GetValues(typeof(string))[0];
			}
			suitabilityCheckContext.SuitabilityResult.IsOSVersionSuitable = SuitabilityVerifier.IsOSVersionSuitable(suitabilityCheckContext.ServerFqdn, osVersion, osServicePack);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, bool>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckOperatingSystemSuitable. Value {1}", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.SuitabilityResult.IsOSVersionSuitable);
			if (!suitabilityCheckContext.SuitabilityResult.IsOSVersionSuitable)
			{
				throw new ADServerNotSuitableException(DirectoryStrings.ErrorIsServerSuitableInvalidOSVersion(suitabilityCheckContext.DCConnection.SessionOptions.HostName, osVersion, osServicePack, "5.2 (3790)", "Service Pack 1"), suitabilityCheckContext.DCConnection.SessionOptions.HostName);
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00129990 File Offset: 0x00127B90
		private static Task CheckOperatingSystemSuitable(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckOperatingSystemSuitable", context.ServerFqdn);
			Task<DirectoryResponse> task = SuitabilityVerifier.StartCheckOperatingSystemSuitable(context);
			return task.ContinueWith(new Action<Task<DirectoryResponse>>(SuitabilityVerifier.EndCheckOperatingSystemSuitable), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x001299E4 File Offset: 0x00127BE4
		private static Task<DirectoryResponse> StartCheckPDC(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartCheckPDC", context.ServerFqdn);
			SearchRequest request = new SearchRequest(context.SuitabilityResult.WritableNC, "(objectclass=domain)", SearchScope.Base, SuitabilityVerifier.PDCSuitabilityProperties);
			return SuitabilityVerifier.SendAsyncLdapRequest(context.DCConnection, request, context);
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00129A44 File Offset: 0x00127C44
		private static void EndCheckPDC(Task<DirectoryResponse> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<SuitabilityVerifier.SuitabilityCheckContext>("Async Context", task.AsyncState);
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = (SuitabilityVerifier.SuitabilityCheckContext)task.AsyncState;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndCheckSACLRight", suitabilityCheckContext.ServerFqdn);
			SearchResponse searchResponse = (SearchResponse)task.Result;
			if (searchResponse == null || searchResponse.Entries.Count == 0 || !searchResponse.Entries[0].Attributes.Contains("fSMORoleOwner"))
			{
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckPDC. Could not retrieve all the necessary information from '{1}' to perform the suitability check", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.DCConnection.SessionOptions.HostName);
				throw new ADExternalException(DirectoryStrings.ErrorIsServerSuitableMissingComputerData(suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.DCConnection.SessionOptions.HostName));
			}
			string distinguishedName = (string)searchResponse.Entries[0].Attributes["fSMORoleOwner"].GetValues(typeof(string))[0];
			ADObjectId adobjectId = new ADObjectId(distinguishedName);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<int, string, ADObjectId>((long)suitabilityCheckContext.GetHashCode(), "{0} - Successfully got Fsmo role information {1}", suitabilityCheckContext.GetHashCode(), suitabilityCheckContext.ServerFqdn, adobjectId);
			if (adobjectId.Parent.Name.Equals(suitabilityCheckContext.ServerFqdn.Split(new char[]
			{
				'.'
			})[0], StringComparison.OrdinalIgnoreCase))
			{
				suitabilityCheckContext.SuitabilityResult.IsPDC = true;
			}
			else
			{
				suitabilityCheckContext.SuitabilityResult.IsPDC = false;
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, bool>((long)suitabilityCheckContext.GetHashCode(), "{0} - CheckPDC. Value {1}", suitabilityCheckContext.ServerFqdn, suitabilityCheckContext.SuitabilityResult.IsPDC);
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00129BDC File Offset: 0x00127DDC
		private static Task CheckPDC(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckPDC", context.ServerFqdn);
			Task<DirectoryResponse> task = SuitabilityVerifier.StartCheckPDC(context);
			return task.ContinueWith(new Action<Task<DirectoryResponse>>(SuitabilityVerifier.EndCheckPDC), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00129C30 File Offset: 0x00127E30
		private static Task<DirectoryResponse> StartReadDCMaintenanceModeAttributeSchema(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - StartReadDCMaintenanceModeAttributeSchema", context.ServerFqdn);
			SearchRequest request = new SearchRequest(string.Format("CN=ms-Exch-Provisioning-Flags,{0}", context.SuitabilityResult.SchemaNC), "(objectclass=*)", SearchScope.Base, new string[0]);
			return SuitabilityVerifier.SendAsyncLdapRequest(context.DCConnection, request, context);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x00129C98 File Offset: 0x00127E98
		private static void EndReadDCMaintenanceModeAttributeSchema(Task<DirectoryResponse> task)
		{
			ArgumentValidator.ThrowIfTypeInvalid<SuitabilityVerifier.SuitabilityCheckContext>("Async Context", task.AsyncState);
			SuitabilityVerifier.SuitabilityCheckContext suitabilityCheckContext = (SuitabilityVerifier.SuitabilityCheckContext)task.AsyncState;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndReadDCMaintenanceModeAttributeSchema", suitabilityCheckContext.ServerFqdn);
			SearchResponse searchResponse = (SearchResponse)task.Result;
			bool flag = false;
			if (searchResponse != null && searchResponse.Entries.Count > 0)
			{
				flag = true;
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)suitabilityCheckContext.GetHashCode(), "{0} - EndReadDCMaintenanceModeAttributeSchema. Schema attribute ms-Exch-Provisioning-Flags {1}found.", suitabilityCheckContext.ServerFqdn, flag ? string.Empty : " NOT ");
			Exception ex;
			if (!SuitabilityVerifier.TryCheckIsDCInMaintenanceMode(suitabilityCheckContext, flag, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00129D38 File Offset: 0x00127F38
		private static bool TryCheckIsDCInMaintenanceMode(SuitabilityVerifier.SuitabilityCheckContext context, bool isSchemaAttributePresent, out Exception exception)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			exception = null;
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string, bool>((long)context.GetHashCode(), "{0} - TryCheckIsDCInMaintenanceMode. isSchemaAttributePresent {1}", context.ServerFqdn, isSchemaAttributePresent);
			if (context.OperatingSystemResponse == null)
			{
				throw new ADExternalException(DirectoryStrings.ErrorIsServerSuitableMissingOperatingSystemResponse(context.ServerFqdn));
			}
			context.SuitabilityResult.IsInMM = true;
			if (context.OperatingSystemResponse.Entries[0].Attributes.Contains(SuitabilityVerifier.DcMaintenanceModeFlagAttr))
			{
				string s = (string)context.OperatingSystemResponse.Entries[0].Attributes[SuitabilityVerifier.DcMaintenanceModeFlagAttr].GetValues(typeof(string))[0];
				context.SuitabilityResult.IsInMM = (0 != int.Parse(s));
			}
			else if (!isSchemaAttributePresent)
			{
				context.SuitabilityResult.IsInMM = false;
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, bool>((long)context.GetHashCode(), "{0} - CheckIsDCInMaintenanceMode. Value {1}", context.ServerFqdn, context.SuitabilityResult.IsInMM);
			if (context.SuitabilityResult.IsInMM)
			{
				exception = new ServerInMMException(context.ServerFqdn)
				{
					ServerFqdn = context.ServerFqdn
				};
			}
			return !context.SuitabilityResult.IsInMM;
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00129E74 File Offset: 0x00128074
		private static Task CheckIsDCInMaintenanceMode(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckIsDCInMaintenanceMode", context.ServerFqdn);
			bool flag = SuitabilityVerifier.IsSetupOrTestContext();
			if (flag || (context.OperatingSystemResponse != null && context.OperatingSystemResponse.Entries[0].Attributes.Contains(SuitabilityVerifier.DcMaintenanceModeFlagAttr)))
			{
				TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>(context, TaskCreationOptions.AttachedToParent);
				Exception exception;
				if (SuitabilityVerifier.TryCheckIsDCInMaintenanceMode(context, !flag, out exception))
				{
					taskCompletionSource.TrySetResult(string.Empty);
				}
				else
				{
					taskCompletionSource.TrySetException(exception);
				}
				return taskCompletionSource.Task;
			}
			Task<DirectoryResponse> task = SuitabilityVerifier.StartReadDCMaintenanceModeAttributeSchema(context);
			return task.ContinueWith(new Action<Task<DirectoryResponse>>(SuitabilityVerifier.EndReadDCMaintenanceModeAttributeSchema), TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x00129F2C File Offset: 0x0012812C
		private static void CheckCreateConnection(SuitabilityVerifier.SuitabilityCheckContext context, NetworkCredential networkCredential)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckCreateConnection", context.ServerFqdn);
			context.DCConnection = SuitabilityVerifier.CreateConnectionAndBind(context.ServerFqdn, networkCredential, (context.DCPort > 0) ? context.DCPort : 389);
			if (context.GCPort > 0 && (context.SuitabilityResult.IsReachableByTCPConnection & ADServerRole.GlobalCatalog) != ADServerRole.None)
			{
				PooledLdapConnection pooledLdapConnection = null;
				try
				{
					pooledLdapConnection = SuitabilityVerifier.CreateConnectionAndBind(context.ServerFqdn, networkCredential, context.GCPort);
					if (context.DCConnection != null && pooledLdapConnection != null)
					{
						context.SuitabilityResult.IsEnabled = true;
					}
					goto IL_B3;
				}
				finally
				{
					if (pooledLdapConnection != null)
					{
						pooledLdapConnection.Dispose();
					}
				}
			}
			if (context.DCConnection != null)
			{
				context.SuitabilityResult.IsEnabled = true;
			}
			IL_B3:
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug((long)context.GetHashCode(), "{0} - CheckCreateConnection. Value {1} . DC port {2} GC port {3}.", new object[]
			{
				context.ServerFqdn,
				context.SuitabilityResult.IsEnabled,
				context.DCPort,
				context.GCPort
			});
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x0012A204 File Offset: 0x00128404
		private static Task<DirectoryResponse> SendAsyncLdapRequest(PooledLdapConnection connection, SearchRequest request, SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("connection", connection);
			ArgumentValidator.ThrowIfNull("request", request);
			ArgumentValidator.ThrowIfNull("context", context);
			Task<DirectoryResponse> task2 = Task.Factory.FromAsync<DirectoryRequest, TimeSpan, PartialResultProcessing, DirectoryResponse>(new Func<DirectoryRequest, TimeSpan, PartialResultProcessing, AsyncCallback, object, IAsyncResult>(connection.BeginSendRequest), new Func<IAsyncResult, DirectoryResponse>(connection.EndSendRequest), request, ConnectionPoolManager.ClientSideSearchTimeout, PartialResultProcessing.NoPartialResultSupport, context, TaskCreationOptions.AttachedToParent);
			TaskCompletionSource<DirectoryResponse> tcs = new TaskCompletionSource<DirectoryResponse>(context, TaskCreationOptions.AttachedToParent);
			task2.ContinueWith(delegate(Task<DirectoryResponse> task)
			{
				if (task.Status != TaskStatus.RanToCompletion)
				{
					Exception ex = (task.Exception != null) ? task.Exception.GetBaseException() : null;
					ExTraceGlobals.SuitabilityVerifierTracer.TraceError((long)context.GetHashCode(), "{0} Request failed on server '{1}'. DN {2} Filter {3} Scope {4}", new object[]
					{
						context.ServerFqdn,
						connection.SessionOptions.HostName,
						request.DistinguishedName,
						request.Filter,
						request.Scope
					});
					DirectoryException ex2 = (ex != null) ? (ex as DirectoryException) : null;
					if (ex2 == null)
					{
						tcs.SetException(new SuitabilityException(DirectoryStrings.SuitabilityExceptionLdapSearch(context.ServerFqdn, (ex != null) ? ex.Message : "Unknown"), context.ServerFqdn));
						return;
					}
					ADErrorRecord aderrorRecord = context.DCConnection.AnalyzeDirectoryError(ex2);
					SuitabilityVerifier.LogEventSyncFailed(context.ServerFqdn, (int)aderrorRecord.LdapError, aderrorRecord.Message, request);
					if (aderrorRecord.LdapError != LdapError.InvalidCredentials)
					{
						tcs.SetException(new SuitabilityDirectoryException(context.ServerFqdn, (int)aderrorRecord.LdapError, aderrorRecord.Message, aderrorRecord.InnerException)
						{
							ServerFqdn = context.ServerFqdn
						});
						return;
					}
					if (aderrorRecord.HandlingType == HandlingType.Throw)
					{
						tcs.SetException(aderrorRecord.InnerException);
						return;
					}
				}
				else
				{
					tcs.SetResult(task.Result);
				}
			}, TaskContinuationOptions.AttachedToParent);
			return tcs.Task;
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x0012A2D0 File Offset: 0x001284D0
		private static void CheckIsSyncronized(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckIsSyncronized.", context.ServerFqdn);
			if (context.DefaultNCResponse == null)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
				{
					context.ServerFqdn,
					DirectoryStrings.ErrorIsServerSuitableMissingDefaultNamingContext(context.ServerFqdn) + " (in CheckIsSyncronized)"
				});
				throw new ADExternalException(DirectoryStrings.ErrorIsServerSuitableMissingDefaultNamingContext(context.ServerFqdn));
			}
			if (context.DefaultNCResponse.Entries[0].Attributes.Contains("isSynchronized"))
			{
				context.SuitabilityResult.IsSynchronized = (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController);
			}
			if (context.DefaultNCResponse.Entries[0].Attributes.Contains("isGlobalCatalogReady"))
			{
				context.SuitabilityResult.IsSynchronized |= ADServerRole.GlobalCatalog;
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, ADServerRole>((long)context.GetHashCode(), "{0} - CheckIsSyncronized Value {1}", context.ServerFqdn, context.SuitabilityResult.IsSynchronized);
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0012A3E4 File Offset: 0x001285E4
		private static void CheckNetLogon(SuitabilityVerifier.SuitabilityCheckContext context)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ExTraceGlobals.SuitabilityVerifierTracer.TraceFunction<string>((long)context.GetHashCode(), "{0} - CheckNetLogon.", context.ServerFqdn);
			int tickCount = Environment.TickCount;
			try
			{
				SafeDomainControllerInfoHandle safeDomainControllerInfoHandle;
				NativeMethods.DsGetDcName(context.ServerFqdn, null, null, NativeMethods.DsGetDCNameFlags.ReturnDNSName, out safeDomainControllerInfoHandle);
				string a = string.Empty;
				if (safeDomainControllerInfoHandle != null && !string.IsNullOrEmpty(safeDomainControllerInfoHandle.GetDomainControllerInfo().DomainControllerName))
				{
					a = safeDomainControllerInfoHandle.GetDomainControllerInfo().DomainControllerName.Replace("\\", string.Empty);
				}
				if (string.Equals(a, context.ServerFqdn, StringComparison.OrdinalIgnoreCase))
				{
					context.SuitabilityResult.IsNetlogonAllowed |= (ADServerRole.DomainController | ADServerRole.ConfigurationDomainController);
					if ((safeDomainControllerInfoHandle.GetDomainControllerInfo().Flags & 4U) > 0U)
					{
						context.SuitabilityResult.IsNetlogonAllowed |= ADServerRole.GlobalCatalog;
					}
					ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, string>((long)context.GetHashCode(), "{0} - Successfully called DsGetDcName() against server '{0}' to perform the suitability check for NetLogon. DomainController info: {1}", context.ServerFqdn, safeDomainControllerInfoHandle.GetDomainControllerInfo().DomainControllerName);
				}
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_INTERNAL_SUITABILITY_CHECK_FAILED, null, new object[]
				{
					context.ServerFqdn,
					ex.ToString() + " (in CheckNetLogon)"
				});
				ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, Exception>((long)context.GetHashCode(), "Failed to call DsGetDcName() against server '{0}' to perform the suitability check for NetLogon. Error: {1}", context.ServerFqdn, ex);
			}
			finally
			{
				ADProviderPerf.UpdateDCCounter(context.HostName, Counter.DCTimeNetlogon, UpdateType.Update, Convert.ToUInt32(Globals.GetTickDifference(tickCount, Environment.TickCount)));
			}
			ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, ADServerRole>((long)context.GetHashCode(), "{0} - CheckNetLogon. Value {1}", context.ServerFqdn, context.SuitabilityResult.IsNetlogonAllowed);
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0012A5AC File Offset: 0x001287AC
		private static void ThrowException(Exception exception, LocalizedString errorMessage, string serverFqdn)
		{
			if (exception != null)
			{
				if (exception is ArgumentNullException)
				{
					throw exception;
				}
				if (exception is ArgumentException)
				{
					throw exception;
				}
				if (exception is ObjectDisposedException)
				{
					throw exception;
				}
				if (exception is NotSupportedException)
				{
					throw exception;
				}
			}
			if (exception == null)
			{
				throw new ADServerNotSuitableException(errorMessage, serverFqdn);
			}
			throw new SuitabilityException(errorMessage, serverFqdn);
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x0012A5EC File Offset: 0x001287EC
		private static string GetDomainNameFromServerFqdn(string serverFqdn)
		{
			string result = null;
			int num = serverFqdn.IndexOf('.');
			if (num > 0)
			{
				result = serverFqdn.Substring(num);
			}
			return result;
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0012A614 File Offset: 0x00128814
		private static bool IsForestLocal(string serverFqdn)
		{
			string localForestFqdn = TopologyProvider.LocalForestFqdn;
			string text = null;
			if (Globals.IsMicrosoftHostedOnly)
			{
				text = SuitabilityVerifier.GetDomainNameFromServerFqdn(serverFqdn);
			}
			else
			{
				try
				{
					text = NativeHelpers.GetDomainForestNameFromServer(serverFqdn);
				}
				catch (Exception arg)
				{
					ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, Exception>(0L, "Failed to call GetDomainForestNameFromServer() against server '{0}'. Error: {1}", serverFqdn, arg);
					if (string.IsNullOrEmpty(text))
					{
						text = SuitabilityVerifier.GetDomainNameFromServerFqdn(serverFqdn);
					}
				}
			}
			return !string.IsNullOrEmpty(text) && text.Equals(localForestFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0012A68C File Offset: 0x0012888C
		internal static bool IsServerSuitableIgnoreExceptions(string fqdn, bool isGlobalCatalog, NetworkCredential credential, out string writableNC, out string siteName, out LocalizedString errorMessage)
		{
			Exception ex;
			if (SuitabilityVerifier.InternalTryCheckIsServerSuitable(fqdn, isGlobalCatalog, credential, out writableNC, out siteName, out ex))
			{
				errorMessage = LocalizedString.Empty;
				return true;
			}
			errorMessage = ((ex != null) ? new LocalizedString(ex.ToString()) : LocalizedString.Empty);
			return false;
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0012A6D4 File Offset: 0x001288D4
		internal static bool IsServerSuitableIgnoreExceptions(string fqdn, bool isGlobalCatalog, NetworkCredential credential, out string writableNC, out LocalizedString errorMessage)
		{
			string text;
			Exception ex;
			if (SuitabilityVerifier.InternalTryCheckIsServerSuitable(fqdn, isGlobalCatalog, credential, out writableNC, out text, out ex))
			{
				errorMessage = LocalizedString.Empty;
				return true;
			}
			errorMessage = ((ex != null) ? new LocalizedString(ex.ToString()) : LocalizedString.Empty);
			return false;
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0012A71C File Offset: 0x0012891C
		internal static bool IsAdamServerSuitable(string fqdn, int portNumber, NetworkCredential credential, ref LocalizedString errorMessage)
		{
			using (SuitabilityVerifier.CreateConnectionAndBind(fqdn, credential, portNumber))
			{
			}
			return true;
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x0012A750 File Offset: 0x00128950
		internal static void CheckIsServerSuitable(string fqdn, bool isGlobalCatalog, NetworkCredential credentials, out string writableNC)
		{
			string text;
			Exception ex;
			if (!SuitabilityVerifier.InternalTryCheckIsServerSuitable(fqdn, isGlobalCatalog, credentials, out writableNC, out text, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x0012A800 File Offset: 0x00128A00
		internal static Task CheckAndUpdateServerSuitabilities(DirectoryServer server, CancellationToken cancellationToken, bool isInitialDiscovery, bool allowPreW2KSP3DC, bool isPDCCheckEnabled, bool disableNetLogonCheck)
		{
			SuitabilityVerifier.SuitabilityCheckContext context = new SuitabilityVerifier.SuitabilityCheckContext(server, allowPreW2KSP3DC, isPDCCheckEnabled, disableNetLogonCheck);
			Task task = SuitabilityVerifier.PerformServerSuitabilities(context, cancellationToken, isInitialDiscovery);
			return task.ContinueWith(delegate(Task t)
			{
				context.CloseDCConnection();
				if (t.Exception != null)
				{
					ExTraceGlobals.SuitabilityVerifierTracer.TraceError<string, string>((long)context.GetHashCode(), "{0} Final Error: {1}", context.ServerFqdn, t.Exception.Flatten().ToString());
				}
				ExTraceGlobals.SuitabilityVerifierTracer.TraceDebug<string, SuitabilityCheckResult>((long)context.GetHashCode(), "{0} Suitabilities for server {1}", context.ServerFqdn, context.SuitabilityResult);
			}, cancellationToken);
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0012A845 File Offset: 0x00128A45
		internal static Task PerformServerSuitabilities(SuitabilityVerifier.SuitabilityCheckContext context, CancellationToken cancellationToken, bool isInitialDiscovery)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("cancellationToken", cancellationToken);
			return Task.Factory.Iterate(SuitabilityVerifier.SuitabilityChecks(context, null, true, isInitialDiscovery), cancellationToken);
		}

		// Token: 0x04003698 RID: 13976
		private const string DefaultNamingContextAttr = "defaultNamingContext";

		// Token: 0x04003699 RID: 13977
		private const string ConfigurationNamingContextAttr = "configurationNamingContext";

		// Token: 0x0400369A RID: 13978
		private const string SchemaNamingContextAttr = "schemaNamingContext";

		// Token: 0x0400369B RID: 13979
		private const string SupportedCapabilitiesAttr = "supportedCapabilities";

		// Token: 0x0400369C RID: 13980
		private const string OperatingSystemLdapAttr = "operatingSystemVersion";

		// Token: 0x0400369D RID: 13981
		private const string OperatingSystemServicePackLdapAttr = "operatingSystemServicePack";

		// Token: 0x0400369E RID: 13982
		private const string SaclRightAttr = "ntSecurityDescriptor";

		// Token: 0x0400369F RID: 13983
		private const string CriticalDataAttr = "objectClass";

		// Token: 0x040036A0 RID: 13984
		private const string IsSynchronizedAttr = "isSynchronized";

		// Token: 0x040036A1 RID: 13985
		private const string IsGlobalCatalogReadyAttr = "isGlobalCatalogReady";

		// Token: 0x040036A2 RID: 13986
		private const string FSMORoleAttr = "fSMORoleOwner";

		// Token: 0x040036A3 RID: 13987
		private const string ServerNameAttr = "serverName";

		// Token: 0x040036A4 RID: 13988
		private const string DefaultNamingContextFilter = "(objectclass=*)";

		// Token: 0x040036A5 RID: 13989
		private const string ConfigurationNamingContextFilter = "(objectclass=configuration)";

		// Token: 0x040036A6 RID: 13990
		private const string DomainControllerFilter = "(&(objectClass=computer)(servicePrincipalName=HOST/{0}))";

		// Token: 0x040036A7 RID: 13991
		private const string CriticalDataFilter = "(&(objectCategory=msExchExchangeServer)(cn={0}))";

		// Token: 0x040036A8 RID: 13992
		private const string DomainFilter = "(objectclass=domain)";

		// Token: 0x040036A9 RID: 13993
		private const string ActiveDirectoryPartialSecrets = "1.2.840.113556.1.4.1920";

		// Token: 0x040036AA RID: 13994
		private const string DCMaintenanceModeAttributeDNFormat = "CN=ms-Exch-Provisioning-Flags,{0}";

		// Token: 0x040036AB RID: 13995
		private const int MaxRetryAttempts = 3;

		// Token: 0x040036AC RID: 13996
		private const int RetrySleep = 3000;

		// Token: 0x040036AD RID: 13997
		private const string MinOperatingSystemVersion = "5.2 (3790)";

		// Token: 0x040036AE RID: 13998
		private const string MinOperatingSystemServicePack = "Service Pack 1";

		// Token: 0x040036AF RID: 13999
		private const string OSVersionPattern = "^(\\d+)\\.(\\d+) \\(\\d+\\)$";

		// Token: 0x040036B0 RID: 14000
		private const int MinMajorVersion = 5;

		// Token: 0x040036B1 RID: 14001
		private const int MinMinorVersion = 2;

		// Token: 0x040036B2 RID: 14002
		private const uint GcFlag = 4U;

		// Token: 0x040036B3 RID: 14003
		private static readonly string DcMaintenanceModeFlagAttr = SharedPropertyDefinitions.ProvisioningFlags.LdapDisplayName;

		// Token: 0x040036B4 RID: 14004
		private static readonly string[] DefaultNamingContextProperties = new string[]
		{
			"isSynchronized",
			"isGlobalCatalogReady",
			"configurationNamingContext",
			"defaultNamingContext",
			"schemaNamingContext",
			"supportedCapabilities",
			"serverName"
		};

		// Token: 0x040036B5 RID: 14005
		private static readonly string[] SaclRightProperties = new string[]
		{
			"ntSecurityDescriptor"
		};

		// Token: 0x040036B6 RID: 14006
		private static readonly string[] CriticalDataProperties = new string[]
		{
			"objectClass"
		};

		// Token: 0x040036B7 RID: 14007
		private static readonly string[] OSSuitableProperties = new string[]
		{
			"operatingSystemVersion",
			"operatingSystemServicePack",
			SuitabilityVerifier.DcMaintenanceModeFlagAttr
		};

		// Token: 0x040036B8 RID: 14008
		private static readonly string[] PDCSuitabilityProperties = new string[]
		{
			"fSMORoleOwner"
		};

		// Token: 0x020006CA RID: 1738
		internal class SuitabilityCheckContext
		{
			// Token: 0x06005079 RID: 20601 RVA: 0x0012A948 File Offset: 0x00128B48
			internal SuitabilityCheckContext(string serverFqdn, bool isGlobalCatalog, bool allowPreW2KSP3DC = false, bool isPDCCheckEnabled = false, bool disableNetLogonCheck = false)
			{
				if (string.IsNullOrEmpty(serverFqdn))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				this.ServerFqdn = serverFqdn.Trim();
				this.DCPort = 389;
				this.GCPort = (isGlobalCatalog ? 3268 : -1);
				this.AllowPreW2KSP3DC = allowPreW2KSP3DC;
				this.IsPDCCheckEnabled = isPDCCheckEnabled;
				this.DisableNetLogonCheck = disableNetLogonCheck;
				this.SuitabilityResult = new SuitabilityCheckResult();
			}

			// Token: 0x0600507A RID: 20602 RVA: 0x0012A9B8 File Offset: 0x00128BB8
			internal SuitabilityCheckContext(DirectoryServer dsServer, bool allowPreW2KSP3DC = false, bool isPDCCheckEnabled = false, bool disableNetLogonCheck = false) : this(dsServer.DnsName, dsServer.IsGC, allowPreW2KSP3DC, isPDCCheckEnabled, disableNetLogonCheck)
			{
				if (dsServer == null)
				{
					throw new ArgumentNullException("dsServer");
				}
				this.SuitabilityResult = dsServer.SuitabilityResult;
			}

			// Token: 0x17001A60 RID: 6752
			// (get) Token: 0x0600507B RID: 20603 RVA: 0x0012A9EA File Offset: 0x00128BEA
			// (set) Token: 0x0600507C RID: 20604 RVA: 0x0012A9F2 File Offset: 0x00128BF2
			public string ServerFqdn { get; private set; }

			// Token: 0x17001A61 RID: 6753
			// (get) Token: 0x0600507D RID: 20605 RVA: 0x0012A9FB File Offset: 0x00128BFB
			// (set) Token: 0x0600507E RID: 20606 RVA: 0x0012AA03 File Offset: 0x00128C03
			public SuitabilityCheckResult SuitabilityResult { get; private set; }

			// Token: 0x17001A62 RID: 6754
			// (get) Token: 0x0600507F RID: 20607 RVA: 0x0012AA0C File Offset: 0x00128C0C
			// (set) Token: 0x06005080 RID: 20608 RVA: 0x0012AA14 File Offset: 0x00128C14
			public int GCPort { get; private set; }

			// Token: 0x17001A63 RID: 6755
			// (get) Token: 0x06005081 RID: 20609 RVA: 0x0012AA1D File Offset: 0x00128C1D
			// (set) Token: 0x06005082 RID: 20610 RVA: 0x0012AA25 File Offset: 0x00128C25
			public int DCPort { get; private set; }

			// Token: 0x17001A64 RID: 6756
			// (get) Token: 0x06005083 RID: 20611 RVA: 0x0012AA2E File Offset: 0x00128C2E
			// (set) Token: 0x06005084 RID: 20612 RVA: 0x0012AA36 File Offset: 0x00128C36
			public string HostName { get; set; }

			// Token: 0x17001A65 RID: 6757
			// (get) Token: 0x06005085 RID: 20613 RVA: 0x0012AA3F File Offset: 0x00128C3F
			// (set) Token: 0x06005086 RID: 20614 RVA: 0x0012AA47 File Offset: 0x00128C47
			public PooledLdapConnection DCConnection { get; set; }

			// Token: 0x17001A66 RID: 6758
			// (get) Token: 0x06005087 RID: 20615 RVA: 0x0012AA50 File Offset: 0x00128C50
			// (set) Token: 0x06005088 RID: 20616 RVA: 0x0012AA58 File Offset: 0x00128C58
			public SearchResponse DefaultNCResponse { get; set; }

			// Token: 0x17001A67 RID: 6759
			// (get) Token: 0x06005089 RID: 20617 RVA: 0x0012AA61 File Offset: 0x00128C61
			// (set) Token: 0x0600508A RID: 20618 RVA: 0x0012AA69 File Offset: 0x00128C69
			public SearchResponse OperatingSystemResponse { get; set; }

			// Token: 0x17001A68 RID: 6760
			// (get) Token: 0x0600508B RID: 20619 RVA: 0x0012AA72 File Offset: 0x00128C72
			// (set) Token: 0x0600508C RID: 20620 RVA: 0x0012AA7A File Offset: 0x00128C7A
			public IPAddress[] IPAddresses { get; set; }

			// Token: 0x17001A69 RID: 6761
			// (get) Token: 0x0600508D RID: 20621 RVA: 0x0012AA83 File Offset: 0x00128C83
			// (set) Token: 0x0600508E RID: 20622 RVA: 0x0012AA8B File Offset: 0x00128C8B
			public bool AllowPreW2KSP3DC { get; private set; }

			// Token: 0x17001A6A RID: 6762
			// (get) Token: 0x0600508F RID: 20623 RVA: 0x0012AA94 File Offset: 0x00128C94
			// (set) Token: 0x06005090 RID: 20624 RVA: 0x0012AA9C File Offset: 0x00128C9C
			public bool IsPDCCheckEnabled { get; private set; }

			// Token: 0x17001A6B RID: 6763
			// (get) Token: 0x06005091 RID: 20625 RVA: 0x0012AAA5 File Offset: 0x00128CA5
			// (set) Token: 0x06005092 RID: 20626 RVA: 0x0012AAAD File Offset: 0x00128CAD
			public bool DisableNetLogonCheck { get; private set; }

			// Token: 0x17001A6C RID: 6764
			// (get) Token: 0x06005093 RID: 20627 RVA: 0x0012AAB6 File Offset: 0x00128CB6
			public string SiteName
			{
				get
				{
					this.PopulateSiteName();
					return this.siteName;
				}
			}

			// Token: 0x06005094 RID: 20628 RVA: 0x0012AAC4 File Offset: 0x00128CC4
			public void CloseDCConnection()
			{
				if (this.DCConnection != null)
				{
					this.DCConnection.Dispose();
					this.DCConnection = null;
				}
			}

			// Token: 0x06005095 RID: 20629 RVA: 0x0012AAE0 File Offset: 0x00128CE0
			private void PopulateSiteName()
			{
				if (this.DefaultNCResponse == null || this.DefaultNCResponse.Entries.Count == 0)
				{
					return;
				}
				if (!string.IsNullOrEmpty(this.siteName))
				{
					return;
				}
				if (this.DefaultNCResponse.Entries[0].Attributes.Contains("serverName"))
				{
					string text = (string)this.DefaultNCResponse.Entries[0].Attributes["serverName"].GetValues(typeof(string))[0];
					if (!string.IsNullOrEmpty(text))
					{
						ADObjectId adobjectId = new ADObjectId(text);
						if (adobjectId.Parent != null && adobjectId.Parent.Parent != null)
						{
							this.siteName = adobjectId.Parent.Parent.Name;
						}
					}
				}
			}

			// Token: 0x040036B9 RID: 14009
			private string siteName;
		}
	}
}
