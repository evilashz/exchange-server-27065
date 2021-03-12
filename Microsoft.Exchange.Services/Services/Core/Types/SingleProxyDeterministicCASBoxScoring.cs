﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.EventLogs;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000250 RID: 592
	internal static class SingleProxyDeterministicCASBoxScoring
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0004C330 File Offset: 0x0004A530
		private static WebServicesInfo[] SortServicesBasedOnCASAffinity(BaseServerIdInfo serverInfo, WebServicesInfo[] casBoxes)
		{
			if (casBoxes != null && casBoxes.Length == 1)
			{
				return casBoxes;
			}
			string value = null;
			MailboxIdServerInfo mailboxIdServerInfo = serverInfo as MailboxIdServerInfo;
			if (mailboxIdServerInfo != null)
			{
				if (mailboxIdServerInfo.MailboxId.MailboxGuid != null)
				{
					value = mailboxIdServerInfo.MailboxId.MailboxGuid;
				}
				else
				{
					value = mailboxIdServerInfo.MailboxId.SmtpAddress;
				}
			}
			int num = (int)CASAffinityHasher.ComputeIndex(value, casBoxes.Length);
			if (num == 0)
			{
				return casBoxes;
			}
			List<WebServicesInfo> list = new List<WebServicesInfo>(casBoxes);
			WebServicesInfo item = list[num];
			list.RemoveAt(num);
			list.Insert(0, item);
			return list.ToArray();
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0004C3B1 File Offset: 0x0004A5B1
		private static void LogAndThrowFaultException(Exception exception, string serverFQDN)
		{
			ProxyEventLogHelper.LogServicesDiscoveryFailure(serverFQDN);
			ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, string, string>(0L, "[SingleProxyDeterministicCASBoxScoring::LogAndThrowFaultException] Encountered exception while calling ServicesDiscovery for serverFQDN '{0}'.  Exception Type: {1}, Message: {2}", serverFQDN, exception.GetType().FullName, exception.Message);
			throw FaultExceptionUtilities.CreateFault(new ServiceDiscoveryFailedException(exception), FaultParty.Receiver);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0004C4F0 File Offset: 0x0004A6F0
		private static SingleProxyDeterministicCASBoxScoring.SiteSearchResult FindAcceptableServicesInCurrentTopology(BaseServerIdInfo baseServerIdInfo, out WebServicesInfo[] services)
		{
			services = null;
			if (baseServerIdInfo.IsLocalServer)
			{
				return SingleProxyDeterministicCASBoxScoring.SiteSearchResult.ServiceLocally;
			}
			bool badCASCache = false;
			List<WebServicesInfo> tempServices = new List<WebServicesInfo>();
			if (baseServerIdInfo.ServerVersion >= Server.E15MinVersion || baseServerIdInfo.IsFromDifferentResourceForest)
			{
				Uri url = null;
				Exception ex = null;
				try
				{
					if (baseServerIdInfo.IsFromDifferentResourceForest)
					{
						url = FrontEndLocator.GetFrontEndWebServicesUrl(baseServerIdInfo.CafeFQDN);
					}
					else
					{
						BackEndServer backEndServer = new BackEndServer(baseServerIdInfo.ServerFQDN, baseServerIdInfo.ServerVersion);
						url = BackEndLocator.GetBackEndWebServicesUrl(backEndServer);
					}
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
				catch (DataSourceOperationException ex3)
				{
					ex = ex3;
				}
				catch (DataValidationException ex4)
				{
					ex = ex4;
				}
				catch (BackEndLocatorException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, string>(0L, "[SingleProxyDeterministicCASBoxScoring::FindAcceptableServicesInCurrentTopology] GetBackEndWebServicesUrl failed with '{0}', Message: '{1}'.", ex.GetType().FullName, ex.Message);
					throw FaultExceptionUtilities.CreateFault(new ServiceDiscoveryFailedException(ex), FaultParty.Receiver);
				}
				tempServices.Add(WebServicesInfo.Create(url, baseServerIdInfo.IsFromDifferentResourceForest ? baseServerIdInfo.CafeFQDN : baseServerIdInfo.ServerFQDN, baseServerIdInfo.ServerVersion, baseServerIdInfo.IsFromDifferentResourceForest));
			}
			else
			{
				ServiceTopology currentLegacyServiceTopology;
				try
				{
					currentLegacyServiceTopology = ServiceTopology.GetCurrentLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\RequestProxying\\singleproxydeterministiccasboxscoring.cs", "FindAcceptableServicesInCurrentTopology", 219);
				}
				catch (ServiceDiscoveryTransientException ex6)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, string>(0L, "[SingleProxyDeterministicCASBoxScoring::FindAcceptableServicesInCurrentTopology] GetCurrentServiceToplogy failed with '{0}', Message: '{1}'.", ex6.GetType().FullName, ex6.Message);
					throw FaultExceptionUtilities.CreateFault(new ServiceDiscoveryFailedException(ex6), FaultParty.Receiver);
				}
				Func<WebServicesService, BaseServerIdInfo, bool> checkCondition = (WebServicesService service, BaseServerIdInfo serverInfo) => !service.IsOutOfService && service.ClientAccessType == ClientAccessType.InternalNLBBypass && (service.AuthenticationMethod & AuthenticationMethod.WindowsIntegrated) == AuthenticationMethod.WindowsIntegrated && ExchangeVersionDeterminer.IsCurrentOrOlderThanLocalServer(service.ServerVersionNumber) && ExchangeVersionDeterminer.AreSameVersion(serverInfo.ServerVersion, service.ServerVersionNumber) && ExchangeVersionDeterminer.ServerSupportsRequestVersion(service.ServerVersionNumber) && (service.Url.Scheme.ToLower() == Uri.UriSchemeHttps || EWSSettings.AllowProxyingWithoutSSL);
				Site mailboxSite;
				try
				{
					FaultInjection.GenerateFault((FaultInjection.LIDs)2703633725U);
					mailboxSite = currentLegacyServiceTopology.GetSite(baseServerIdInfo.ServerFQDN, "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\RequestProxying\\singleproxydeterministiccasboxscoring.cs", "FindAcceptableServicesInCurrentTopology", 260);
				}
				catch (ServiceDiscoveryPermanentException ex7)
				{
					ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_ServiceDiscoveryForServerFailure, "EWSProxy_ServiceDiscoveryForServerFailure", new object[]
					{
						LocalServer.GetServer().Name,
						baseServerIdInfo.ServerFQDN
					});
					ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string, string>(0L, "[SingleProxyDeterministricCASBoxScoring::FindAcceptableServicesInCurrentTopology] GetSite failed with '{0}', Message: '{1}'", ex7.GetType().FullName, ex7.Message);
					throw FaultExceptionUtilities.CreateFault(new ServiceDiscoveryFailedException(ex7), FaultParty.Receiver);
				}
				currentLegacyServiceTopology.ForEach<WebServicesService>(delegate(WebServicesService service)
				{
					if (ServiceTopology.IsOnSite(service, mailboxSite, "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\RequestProxying\\singleproxydeterministiccasboxscoring.cs", "FindAcceptableServicesInCurrentTopology", 287))
					{
						if (!BadCASCache.Singleton.Contains(service.ServerFullyQualifiedDomainName))
						{
							if (checkCondition(service, baseServerIdInfo))
							{
								tempServices.Add(WebServicesInfo.CreateFromWebServicesService(service));
								return;
							}
						}
						else
						{
							badCASCache = true;
						}
					}
				}, "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\RequestProxying\\singleproxydeterministiccasboxscoring.cs", "FindAcceptableServicesInCurrentTopology", 285);
			}
			if (tempServices.Count == 0)
			{
				if (!badCASCache)
				{
					return SingleProxyDeterministicCASBoxScoring.SiteSearchResult.NoGoodCASFound;
				}
				return SingleProxyDeterministicCASBoxScoring.SiteSearchResult.CASInBadCache;
			}
			else
			{
				services = tempServices.ToArray();
				if (baseServerIdInfo.IsFromDifferentResourceForest)
				{
					return SingleProxyDeterministicCASBoxScoring.SiteSearchResult.NeedToProxyToCafe;
				}
				return SingleProxyDeterministicCASBoxScoring.SiteSearchResult.NeedToProxy;
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0004C818 File Offset: 0x0004AA18
		public static SingleProxyDeterministicCASBoxScoring.SiteSearchResult GetBestEwsBoxesForRequest(BaseServerIdInfo serverIdInfo, out WebServicesInfo[] services)
		{
			services = null;
			if (!serverIdInfo.IsFromDifferentResourceForest)
			{
				if (!ExchangeVersionDeterminer.ServerSupportsRequestVersion(serverIdInfo.ServerVersion))
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[SingleProxyDeterministicCASBoxScoring::GetBestEwsBoxesForRequest] Mailbox in question does not support the RequestServerVersion of the request.  Failing request.");
					throw FaultExceptionUtilities.CreateFault(new InvalidSchemaVersionForMailboxVersionException(), FaultParty.Sender);
				}
				if (string.IsNullOrEmpty(serverIdInfo.ServerFQDN))
				{
					return SingleProxyDeterministicCASBoxScoring.SiteSearchResult.ServiceLocally;
				}
			}
			SingleProxyDeterministicCASBoxScoring.SiteSearchResult siteSearchResult = SingleProxyDeterministicCASBoxScoring.SiteSearchResult.NoGoodCASFound;
			try
			{
				siteSearchResult = SingleProxyDeterministicCASBoxScoring.FindAcceptableServicesInCurrentTopology(serverIdInfo, out services);
			}
			catch (DataSourceTransientException exception)
			{
				SingleProxyDeterministicCASBoxScoring.LogAndThrowFaultException(exception, serverIdInfo.ServerFQDN);
			}
			catch (DataSourceOperationException exception2)
			{
				SingleProxyDeterministicCASBoxScoring.LogAndThrowFaultException(exception2, serverIdInfo.ServerFQDN);
			}
			if (siteSearchResult == SingleProxyDeterministicCASBoxScoring.SiteSearchResult.NeedToProxy)
			{
				services = SingleProxyDeterministicCASBoxScoring.SortServicesBasedOnCASAffinity(serverIdInfo, services);
			}
			return siteSearchResult;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0004C8C0 File Offset: 0x0004AAC0
		internal static WebServicesInfo GetCASServiceForServer(string serverFQDN)
		{
			return WebServiceByFQDNCache.Singleton.Get(serverFQDN);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0004C8D0 File Offset: 0x0004AAD0
		internal static string GetSiteIdForServer(string serverFQDN)
		{
			ADObjectId adobjectId;
			try
			{
				adobjectId = ServerSiteCache.Singleton.Get(serverFQDN);
			}
			catch (DataValidationException exception)
			{
				return SingleProxyDeterministicCASBoxScoring.FindServerByFQDNExceptionTrace(serverFQDN, exception);
			}
			catch (DataSourceOperationException exception2)
			{
				return SingleProxyDeterministicCASBoxScoring.FindServerByFQDNExceptionTrace(serverFQDN, exception2);
			}
			catch (DataSourceTransientException exception3)
			{
				return SingleProxyDeterministicCASBoxScoring.FindServerByFQDNExceptionTrace(serverFQDN, exception3);
			}
			if (adobjectId == null)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[SingleProxyDeterministicCASBoxScoring::GetSiteIdForServer] No server found for FQDN: {0}", serverFQDN);
				return SingleProxyDeterministicCASBoxScoring.resourceManager.Member.GetString(CoreResources.IDs.NoServer.ToString(), EWSSettings.ServerCulture);
			}
			return adobjectId.DistinguishedName;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0004C97C File Offset: 0x0004AB7C
		private static string FindServerByFQDNExceptionTrace(string fqdn, Exception exception)
		{
			ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string, string>(0L, "[SingleProxyDeterministicCASBoxScoring::GetSiteIdForServer] FindServerByFQDN failed for FQDN: '{0}'. Exception class: {1}, Message: {2}", (fqdn == null) ? "<NULL>" : fqdn, exception.GetType().FullName, exception.Message);
			return SingleProxyDeterministicCASBoxScoring.resourceManager.Member.GetString(CoreResources.IDs.NoServer.ToString(), EWSSettings.ServerCulture);
		}

		// Token: 0x04000BC6 RID: 3014
		private static LazyMember<ExchangeResourceManager> resourceManager = new LazyMember<ExchangeResourceManager>(() => ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Services.CoreResources", Assembly.GetExecutingAssembly()));

		// Token: 0x02000251 RID: 593
		internal enum SiteSearchResult
		{
			// Token: 0x04000BCA RID: 3018
			ServiceLocally,
			// Token: 0x04000BCB RID: 3019
			NeedToProxy,
			// Token: 0x04000BCC RID: 3020
			NoGoodCASFound,
			// Token: 0x04000BCD RID: 3021
			CASInBadCache,
			// Token: 0x04000BCE RID: 3022
			NeedToProxyToCafe
		}
	}
}
