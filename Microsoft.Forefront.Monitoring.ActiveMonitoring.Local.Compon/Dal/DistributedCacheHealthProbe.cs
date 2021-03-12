using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Hygiene.Cache.Data;
using Microsoft.Exchange.Hygiene.Cache.DataProvider;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000064 RID: 100
	public class DistributedCacheHealthProbe : ProbeWorkItem
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000FBAA File Offset: 0x0000DDAA
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		private TimeSpan MaxPrimingPeriod { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000FBBB File Offset: 0x0000DDBB
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000FBC3 File Offset: 0x0000DDC3
		private TimeSpan MaxUnhealthyPeriod { get; set; }

		// Token: 0x0600028B RID: 651 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.ExecutionStartTime = DateTime.UtcNow;
			this.TraceInformation("Starting {0} distributed cache health probe.", new object[]
			{
				base.Definition.Name
			});
			try
			{
				this.PrepareConfiguration();
				this.ProbeDistributedCacheHealth(cancellationToken);
			}
			catch (Exception ex)
			{
				this.TraceError("Encountered an exception during '{0}' probe execution: {1}.", new object[]
				{
					base.Definition.Name,
					ex.Message
				});
				throw;
			}
			this.TraceInformation("Completed {0} distributed cache health probe in elapsed time {1}.", new object[]
			{
				base.Definition.Name,
				DateTime.UtcNow - base.Result.ExecutionStartTime
			});
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		protected void ProbeDistributedCacheHealth(CancellationToken cancellationToken)
		{
			CacheDataProvider provider = new CacheDataProvider();
			IEnumerable<CachePrimingInfo> source = from type in CacheDataProvider.GetSupportedCacheTypes()
			select provider.GetEntityPrimingInfoByObjectType(type);
			string message = "The caches: {0} have states {1}.";
			object[] array = new object[2];
			array[0] = string.Join(", ", (from info in source
			select info.CacheName).ToArray<string>());
			array[1] = string.Join(", ", (from info in source
			select info.PrimingState.ToString()).ToArray<string>());
			this.TraceInformation(message, array);
			IEnumerable<CachePrimingInfo> unhealthyCaches = from info in source
			where info.PrimingState != CachePrimingState.Healthy && info.PrimingState != CachePrimingState.Stale
			select info;
			this.PersistResults(unhealthyCaches);
			Exception[] array2 = this.GetFailedDistributedCaches(unhealthyCaches, this.GetLastResults(cancellationToken)).ToArray<Exception>();
			if (array2.Any<Exception>())
			{
				throw new AggregateException(array2);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		private static DateTime ConvertToDatetime(double attribute)
		{
			return new DateTime((long)attribute, DateTimeKind.Utc);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		private static string ConvertCacheHealthToString(CachePrimingInfo info)
		{
			return string.Format("CacheName:State = {0}:{1}, LastSyncTime = {2}, SyncWatermark = {3}", new object[]
			{
				info.CacheName,
				info.PrimingState,
				info.LastSyncTime,
				info.SyncWatermark
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010264 File Offset: 0x0000E464
		private IEnumerable<Exception> GetFailedDistributedCaches(IEnumerable<CachePrimingInfo> unhealthyCaches, IEnumerable<ProbeResult> lastRunResults)
		{
			using (IEnumerator<CachePrimingInfo> enumerator = unhealthyCaches.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CachePrimingInfo unhealthyCache = enumerator.Current;
					IEnumerable<ProbeResult> failures = lastRunResults.TakeWhile((ProbeResult result) => result.StateAttribute2.Contains(unhealthyCache.CacheName));
					IEnumerable<ProbeResult> unhealthy = lastRunResults.TakeWhile((ProbeResult result) => this.IsUnhealthy(result.StateAttribute2, unhealthyCache.CacheName));
					if (failures.Any<ProbeResult>())
					{
						this.TraceInformation("There were some failures in the history: {0}.", new object[]
						{
							this.PrepareHistory(failures)
						});
						DateTime oldestConsecutiveFailureTime = DistributedCacheHealthProbe.ConvertToDatetime(failures.Reverse<ProbeResult>().FirstOrDefault<ProbeResult>().StateAttribute6);
						if (DateTime.UtcNow - oldestConsecutiveFailureTime > this.MaxPrimingPeriod)
						{
							string message = string.Format("The distributed cache '{0}' is Unknown/Unhealthy/Unavailable/Priming. History for this probe: {1}.", unhealthyCache.CacheName, this.PrepareHistory(failures));
							this.TraceError(message, new object[0]);
							yield return new Exception(message);
						}
						else if (unhealthy.Any<ProbeResult>())
						{
							DateTime oldestConsecutiveUnhealthyTime = DistributedCacheHealthProbe.ConvertToDatetime(unhealthy.Reverse<ProbeResult>().FirstOrDefault<ProbeResult>().StateAttribute6);
							if (DateTime.UtcNow - oldestConsecutiveUnhealthyTime > this.MaxUnhealthyPeriod)
							{
								string message2 = string.Format("The distributed cache '{0}' is Unknown/Unhealthy/Unavailable. History for this probe: {1}.", unhealthyCache.CacheName, this.PrepareHistory(unhealthy));
								this.TraceError(message2, new object[0]);
								yield return new Exception(message2);
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000102C6 File Offset: 0x0000E4C6
		private string PrepareHistory(IEnumerable<ProbeResult> results)
		{
			return string.Join(";", (from result in results
			select DistributedCacheHealthProbe.ConvertToDatetime(result.StateAttribute6).ToString() + " -- " + result.StateAttribute2).ToArray<string>());
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000102FC File Offset: 0x0000E4FC
		private bool IsUnhealthy(string cacheHistory, string cacheName)
		{
			return Regex.IsMatch(cacheHistory, string.Format("{0}:({1}|{2}|{3})", new object[]
			{
				cacheName,
				CachePrimingState.Unhealthy,
				CachePrimingState.Unknown,
				CachePrimingState.Unavailable
			}), RegexOptions.IgnoreCase);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0001034C File Offset: 0x0000E54C
		private IEnumerable<ProbeResult> GetLastResults(CancellationToken cancellationToken)
		{
			if (base.Broker != null)
			{
				IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(base.Definition, base.Result.ExecutionStartTime - (this.MaxPrimingPeriod + TimeSpan.FromMinutes(10.0)));
				return from result in probeResults
				where this.IsVerifiableResult(result)
				select result;
			}
			return new ProbeResult[0];
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000103BC File Offset: 0x0000E5BC
		private bool IsVerifiableResult(ProbeResult result)
		{
			return result != null && result.StateAttribute2 != null;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000103D8 File Offset: 0x0000E5D8
		private void PersistResults(IEnumerable<CachePrimingInfo> unhealthyCaches)
		{
			base.Result.StateAttribute2 = string.Join(";", (from info in unhealthyCaches
			select DistributedCacheHealthProbe.ConvertCacheHealthToString(info)).ToArray<string>());
			base.Result.StateAttribute6 = (double)DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001043C File Offset: 0x0000E63C
		private void PrepareConfiguration()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(base.Definition.ExtensionAttributes);
			XmlAttribute xmlAttribute = xmlDocument.DocumentElement.SelectSingleNode("WorkContext/DistributedCacheHealth").Attributes["maxPrimingPeriod"];
			if (xmlAttribute == null)
			{
				throw new ArgumentException("Extension attributes is missing required maximum priming period attribute.");
			}
			this.MaxPrimingPeriod = TimeSpan.Parse(xmlAttribute.Value);
			xmlAttribute = xmlDocument.DocumentElement.SelectSingleNode("WorkContext/DistributedCacheHealth").Attributes["maxUnhealthyPeriod"];
			if (xmlAttribute == null)
			{
				throw new ArgumentException("Extension attributes is missing required maximum unhealthy period attribute.");
			}
			this.MaxUnhealthyPeriod = TimeSpan.Parse(xmlAttribute.Value);
			this.TraceInformation("Maximum Priming Period: {0} and Maximum Unhealthy Period: {1}", new object[]
			{
				this.MaxPrimingPeriod,
				this.MaxUnhealthyPeriod
			});
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010510 File Offset: 0x0000E710
		private void TraceInformation(string message, params object[] messageArgs)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + "Information: " + string.Format(message, messageArgs) + Environment.NewLine;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DALTracer, base.TraceContext, message, messageArgs, null, "TraceInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DAL\\Probes\\DistributedCacheHealthProbe.cs", 296);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010568 File Offset: 0x0000E768
		private void TraceError(string message, params object[] messageArgs)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + "Error: " + string.Format(message, messageArgs) + Environment.NewLine;
			WTFDiagnostics.TraceError(ExTraceGlobals.DALTracer, base.TraceContext, message, messageArgs, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DAL\\Probes\\DistributedCacheHealthProbe.cs", 311);
		}
	}
}
