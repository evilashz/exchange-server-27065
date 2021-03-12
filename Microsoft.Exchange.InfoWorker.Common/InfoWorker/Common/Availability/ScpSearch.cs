using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000CE RID: 206
	internal class ScpSearch
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00017053 File Offset: 0x00015253
		private ScpSearch(ADServiceConnectionPoint[] results)
		{
			this.results = results;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00017062 File Offset: 0x00015262
		private ScpSearch(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00017071 File Offset: 0x00015271
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00017079 File Offset: 0x00015279
		public Exception Exception { get; private set; }

		// Token: 0x06000548 RID: 1352 RVA: 0x00017084 File Offset: 0x00015284
		public static ScpSearch FindLocal()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 114, "FindLocal", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\RequestDispatch\\ScpSearch.cs");
			ScpSearch scpSearch = ScpSearch.Find(tenantOrTopologyConfigurationSession, ScpSearch.localQueryFilter);
			if (scpSearch != null && scpSearch.Exception != null)
			{
				Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_SCPErrorSearchingLocalADForSCP, null, new object[]
				{
					Globals.ProcessId,
					scpSearch.Exception
				});
			}
			return scpSearch;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00017148 File Offset: 0x00015348
		public Uri FindRemote(string domainName, NetworkCredential networkCredential)
		{
			if (this.results == null || this.results.Length == 0)
			{
				return null;
			}
			ADServiceConnectionPoint[] orderedByPriority = ScpSearch.GetOrderedByPriority(this.results, (ADServiceConnectionPoint scp) => ScpSearch.CalculatePriorityForLocal(scp, domainName));
			List<ScpSearch.ServiceBindingInformation> serviceBindingInformation = ScpSearch.GetServiceBindingInformation(orderedByPriority);
			if (serviceBindingInformation == null)
			{
				Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_SCPMisconfiguredRemoteServiceBindings, null, new object[]
				{
					Globals.ProcessId,
					domainName
				});
				return null;
			}
			using (List<ScpSearch.ServiceBindingInformation>.Enumerator enumerator = serviceBindingInformation.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ScpSearch.<>c__DisplayClass4 CS$<>8__locals2 = new ScpSearch.<>c__DisplayClass4();
					CS$<>8__locals2.serviceBindingInformation = enumerator.Current;
					ScpSearch.ConfigurationTracer.TraceDebug<string>(0L, "Creating AD remote session to {0}", CS$<>8__locals2.serviceBindingInformation.ServiceBinding.Host);
					IConfigurationSession remoteSession = null;
					Exception ex = ScpSearch.PerformRetryableAdOperation("remote AD session", delegate
					{
						remoteSession = ADSystemConfigurationSession.CreateRemoteForestSession(CS$<>8__locals2.serviceBindingInformation.ServiceBinding.Host, networkCredential);
					});
					if (ex == null)
					{
						ScpSearch.ConfigurationTracer.TraceDebug<string>(0L, "Successful creation of AD remote session to {0}", CS$<>8__locals2.serviceBindingInformation.ServiceBinding.Host);
						ScpSearch scpSearch = ScpSearch.Find(remoteSession, ScpSearch.remoteQueryFilter);
						if (scpSearch.Exception == null)
						{
							ADServiceConnectionPoint[] orderedByPriority2 = ScpSearch.GetOrderedByPriority(scpSearch.results, new Converter<ADServiceConnectionPoint, int>(ScpSearch.CalculatePriorityForRemote));
							List<ScpSearch.ServiceBindingInformation> serviceBindingInformation2 = ScpSearch.GetServiceBindingInformation(orderedByPriority2);
							if (serviceBindingInformation2 == null || serviceBindingInformation2.Count == 0)
							{
								Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_SCPMisconfiguredRemoteServiceBindings, null, new object[]
								{
									Globals.ProcessId,
									CS$<>8__locals2.serviceBindingInformation.ServiceBinding.Host
								});
								return null;
							}
							return serviceBindingInformation2[0].ServiceBinding;
						}
						else
						{
							Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_SCPErrorSearchingForRemoteSCP, null, new object[]
							{
								Globals.ProcessId,
								CS$<>8__locals2.serviceBindingInformation.ServiceBinding.Host,
								ex
							});
						}
					}
					else
					{
						Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_SCPCannotConnectToRemoteDirectory, null, new object[]
						{
							Globals.ProcessId,
							CS$<>8__locals2.serviceBindingInformation.ServiceBinding.Host,
							CS$<>8__locals2.serviceBindingInformation.SCP.DistinguishedName,
							ex
						});
					}
				}
			}
			return null;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001741C File Offset: 0x0001561C
		private static List<ScpSearch.ServiceBindingInformation> GetServiceBindingInformation(ADServiceConnectionPoint[] scpArray)
		{
			if (scpArray == null || scpArray.Length == 0)
			{
				ScpSearch.ConfigurationTracer.TraceError(0L, "No SCP objects returned from the AD query.");
				return null;
			}
			List<ScpSearch.ServiceBindingInformation> list = new List<ScpSearch.ServiceBindingInformation>(scpArray.Length);
			foreach (ADServiceConnectionPoint adserviceConnectionPoint in scpArray)
			{
				using (MultiValuedProperty<string>.Enumerator enumerator = adserviceConnectionPoint.ServiceBindingInformation.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string serviceBindingInformationItem = enumerator.Current;
						if (Uri.IsWellFormedUriString(serviceBindingInformationItem, UriKind.Absolute))
						{
							if (!list.Exists((ScpSearch.ServiceBindingInformation listItem) => StringComparer.InvariantCultureIgnoreCase.Equals(listItem, serviceBindingInformationItem)))
							{
								list.Add(new ScpSearch.ServiceBindingInformation
								{
									ServiceBinding = new Uri(serviceBindingInformationItem),
									SCP = adserviceConnectionPoint
								});
							}
						}
					}
				}
			}
			if (list.Count == 0)
			{
				ScpSearch.ConfigurationTracer.TraceError(0L, "SCP objects returned from the AD query have no valid service binding information.");
				return null;
			}
			return list;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00017558 File Offset: 0x00015758
		private static ScpSearch Find(IConfigurationSession session, QueryFilter filter)
		{
			ScpSearch.ConfigurationTracer.TraceDebug<string, QueryFilter>(0L, "Searching for ADServiceConnectionPoint objects in the AD at {0} using filter {1}", session.DomainController ?? "<any local DC>", filter);
			ADServiceConnectionPoint[] results = null;
			Exception ex = null;
			try
			{
				ex = ScpSearch.PerformRetryableAdOperation("query for SCP", delegate
				{
					results = session.Find<ADServiceConnectionPoint>(null, QueryScope.SubTree, filter, null, 400);
				});
			}
			catch (DataValidationException ex2)
			{
				ex = ex2;
			}
			catch (ADOperationException ex3)
			{
				ex = ex3;
			}
			catch (UriFormatException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ScpSearch.ConfigurationTracer.TraceError<Exception>(0L, "Failed to find ADServiceConnectionPoint objects in the AD due exception: {0}.", ex);
				return new ScpSearch(ex);
			}
			if (ScpSearch.ConfigurationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ScpSearch.ConfigurationTracer.TraceDebug<int>(0L, "Search for ADServiceConnectionPoint objects in the AD returned {0} objects.", (results == null) ? 0 : results.Length);
				foreach (ADServiceConnectionPoint adserviceConnectionPoint in results)
				{
					ScpSearch.ConfigurationTracer.TraceDebug<ADObjectId, string, string>(0L, "Found ADServiceConnectionPoint object {0}, Keywords={1}, ServiceBindingInformation={2}", adserviceConnectionPoint.Id, string.Join(";", adserviceConnectionPoint.Keywords.ToArray()), string.Join(";", adserviceConnectionPoint.ServiceBindingInformation.ToArray()));
				}
			}
			return new ScpSearch(results);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000176D4 File Offset: 0x000158D4
		private static ADServiceConnectionPoint[] GetOrderedByPriority(ADServiceConnectionPoint[] scps, Converter<ADServiceConnectionPoint, int> converter)
		{
			ADServiceConnectionPoint[] array = new ADServiceConnectionPoint[scps.Length];
			Array.Copy(scps, array, scps.Length);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			Array.Sort<int, ADServiceConnectionPoint>(array2, array);
			return array;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00017720 File Offset: 0x00015920
		private static int CalculatePriorityForLocal(ADServiceConnectionPoint scp, string domainName)
		{
			if (scp.Keywords != null && scp.Keywords.Count > 0)
			{
				string y = "domain=" + domainName;
				bool flag = false;
				foreach (string text in scp.Keywords)
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (StringComparer.InvariantCultureIgnoreCase.Equals(text, y))
						{
							return 0;
						}
						if (text.StartsWith("domain=", StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					return 1;
				}
				return 2;
			}
			return 2;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000177C4 File Offset: 0x000159C4
		private static int CalculatePriorityForRemote(ADServiceConnectionPoint scp)
		{
			if (scp.Keywords != null && scp.Keywords.Count > 0)
			{
				string y = "site=" + ScpSearch.LocalSiteName;
				bool flag = false;
				foreach (string text in scp.Keywords)
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (!string.IsNullOrEmpty(ScpSearch.LocalSiteName) && StringComparer.InvariantCultureIgnoreCase.Equals(text, y))
						{
							return 0;
						}
						if (text.StartsWith("site=", StringComparison.OrdinalIgnoreCase))
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					return 1;
				}
				return 2;
			}
			return 2;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001787C File Offset: 0x00015A7C
		private static string LocalSiteName
		{
			get
			{
				if (ScpSearch.localSiteName == null)
				{
					try
					{
						ScpSearch.localSiteName = NativeHelpers.GetSiteName(false);
						if (ScpSearch.localSiteName == null)
						{
							ScpSearch.localSiteName = string.Empty;
						}
					}
					catch (CannotGetSiteInfoException arg)
					{
						ScpSearch.localSiteName = string.Empty;
						ScpSearch.ConfigurationTracer.TraceDebug<CannotGetSiteInfoException>(0L, "Failed to get LocalSiteName. Exception: {0}", arg);
						Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_CannotGetLocalSiteName, null, new object[]
						{
							Globals.ProcessId
						});
					}
					ScpSearch.ConfigurationTracer.TraceDebug<string>(0L, "Using LocalSiteName={0}", ScpSearch.localSiteName);
				}
				return ScpSearch.localSiteName;
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00017920 File Offset: 0x00015B20
		private static Exception PerformRetryableAdOperation(string operationDescription, ScpSearch.RetryableAdOperation operation)
		{
			DateTime t = DateTime.UtcNow + ScpSearch.AdOperationRetryTimeout;
			Exception result = null;
			do
			{
				try
				{
					operation();
					return null;
				}
				catch (ADTransientException ex)
				{
					ScpSearch.ConfigurationTracer.TraceError<string, ADTransientException>(0L, "Failed AD operation {0} with exception {1}, retrying.", operationDescription, ex);
					result = ex;
				}
				Thread.Sleep(ScpSearch.AdOperationRetryWait);
			}
			while (DateTime.UtcNow < t);
			return result;
		}

		// Token: 0x04000311 RID: 785
		private const int MaximumScpPointerCount = 400;

		// Token: 0x04000312 RID: 786
		private const string DomainPrefix = "domain=";

		// Token: 0x04000313 RID: 787
		private const string SitePrefix = "site=";

		// Token: 0x04000314 RID: 788
		private static readonly Trace ConfigurationTracer = ExTraceGlobals.ConfigurationTracer;

		// Token: 0x04000315 RID: 789
		private static readonly TimeSpan AdOperationRetryTimeout = TimeSpan.FromSeconds(20.0);

		// Token: 0x04000316 RID: 790
		private static readonly TimeSpan AdOperationRetryWait = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000317 RID: 791
		private static readonly QueryFilter localQueryFilter = ExchangeScpObjects.AutodiscoverDomainPointerKeyword.Filter;

		// Token: 0x04000318 RID: 792
		private static readonly QueryFilter remoteQueryFilter = ExchangeScpObjects.AutodiscoverUrlKeyword.Filter;

		// Token: 0x04000319 RID: 793
		private static string localSiteName;

		// Token: 0x0400031A RID: 794
		private ADServiceConnectionPoint[] results;

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x06000553 RID: 1363
		private delegate void RetryableAdOperation();

		// Token: 0x020000D0 RID: 208
		private class ServiceBindingInformation
		{
			// Token: 0x0400031C RID: 796
			public Uri ServiceBinding;

			// Token: 0x0400031D RID: 797
			public ADServiceConnectionPoint SCP;
		}
	}
}
