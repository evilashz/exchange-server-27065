using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200011C RID: 284
	public class CafeUtils
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00032318 File Offset: 0x00030518
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x0003231F File Offset: 0x0003051F
		public static IPAddress CurrentCafeVipAddress { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00032327 File Offset: 0x00030527
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0003232E File Offset: 0x0003052E
		public static bool RunningUnderCafeVipMode
		{
			get
			{
				return CafeUtils.runningUnderCafeMode;
			}
			private set
			{
				CafeUtils.runningUnderCafeMode = false;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00032336 File Offset: 0x00030536
		public static bool OutlookDotComCertificateCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0003233C File Offset: 0x0003053C
		public static void DoWorkWithUnifiedNamespace(CafeUtils.DoWork doWork, CancellationToken cancellationToken, bool iterateVipsUntilSuccess, ProbeDefinition definition, IProbeWorkBroker broker, string endpointHostname)
		{
			CafeUtils.runningUnderCafeMode = true;
			CertificateValidationManager.RegisterCallback("UnifiedNamespaceAMProbe", new RemoteCertificateValidationCallback(CafeUtils.OutlookDotComCertificateCallback));
			Exception ex = null;
			foreach (object obj in new HostnameResolver(endpointHostname))
			{
				IPAddress currentCafeVipAddress = (IPAddress)obj;
				CafeUtils.CurrentCafeVipAddress = currentCafeVipAddress;
				try
				{
					doWork(cancellationToken);
					break;
				}
				catch (Exception ex2)
				{
					CafeUtils.PublishCafeVipResult(CafeUtils.CurrentCafeVipAddress, ResultType.Failed, definition, broker, ex2);
					ex = ex2;
				}
				if (!iterateVipsUntilSuccess)
				{
					break;
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			CafeUtils.PublishCafeVipResult(CafeUtils.CurrentCafeVipAddress, ResultType.Succeeded, definition, broker, null);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000323F8 File Offset: 0x000305F8
		public static void DoWorkWithUnifiedNamespace(CafeUtils.DoWork doWork, CancellationToken cancellationToken, bool iterateVipsUntilSuccess, ProbeDefinition definition, IProbeWorkBroker broker)
		{
			CafeUtils.DoWorkWithUnifiedNamespace(doWork, cancellationToken, iterateVipsUntilSuccess, definition, broker, "pilot.outlook.com");
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0003240C File Offset: 0x0003060C
		public static void PublishCafeVipResult(IPAddress vip, ResultType type, ProbeDefinition definition, IProbeWorkBroker broker, Exception exceptionDetails)
		{
			ProbeResult probeResult = new ProbeResult(definition);
			probeResult.ExecutionStartTime = DateTime.UtcNow;
			probeResult.ExecutionEndTime = DateTime.UtcNow;
			probeResult.ResultName = CafeUtils.TranslateVipToCafeResultName(vip, definition.TargetPartition);
			probeResult.ResultType = type;
			if (exceptionDetails != null)
			{
				probeResult.Exception = exceptionDetails.ToString();
				probeResult.ExecutionContext = string.Format("Linked protocol probe definition: {0} [{1}]: {2}", definition.Name, definition.ExecutionId, exceptionDetails);
			}
			broker.PublishResult(probeResult);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0003248A File Offset: 0x0003068A
		public static string TranslateVipToCafeResultName(IPAddress cafeVip, string targetPartition)
		{
			return string.Format("Cas15/{0}/{1}", CafeUtils.GetRegion(targetPartition), CafeUtils.TranslateVipToCafeHostname(cafeVip));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000324A4 File Offset: 0x000306A4
		public static string TranslateVipToCafeHostname(IPAddress cafeVip)
		{
			return cafeVip.ToString();
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000324B9 File Offset: 0x000306B9
		public static string GetRegion(string targetPartition)
		{
			return targetPartition.Substring(0, 3);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000324C4 File Offset: 0x000306C4
		internal static void InvokeResponderGivenComponentState(ResponderWorkItem responder, Action<CancellationToken> baseDoResponderWork, TracingContext traceContext, CancellationToken cancellationToken)
		{
			AttributeHelper attributeHelper = new AttributeHelper(responder.Definition);
			CafeUtils.TriggerConfig @enum = attributeHelper.GetEnum<CafeUtils.TriggerConfig>("ComponentStateTriggerConfig", true, CafeUtils.TriggerConfig.ExecuteIfOnline);
			ServerComponentEnum enum2 = attributeHelper.GetEnum<ServerComponentEnum>("ComponentStateServerComponentName", true, ServerComponentEnum.None);
			bool flag = ServerComponentStateManager.IsOnline(enum2);
			WTFDiagnostics.TraceDebug<string, string, string, string>(ExTraceGlobals.CafeTracer, traceContext, "{0}: ServerComponent={1}, TriggerConfig={2}, IsComponentOnline={3}", responder.Definition.Name, enum2.ToString(), @enum.ToString(), flag ? "True" : "False", null, "InvokeResponderGivenComponentState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CafeUtils.Responders.cs", 96);
			if ((@enum == CafeUtils.TriggerConfig.ExecuteIfOnline && flag) || (@enum == CafeUtils.TriggerConfig.ExecuteIfOffline && !flag))
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CafeTracer, traceContext, "{0}: Deciding to invoke baseDoResponderWork", responder.Definition.Name, null, "InvokeResponderGivenComponentState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CafeUtils.Responders.cs", 108);
				baseDoResponderWork(cancellationToken);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0003258C File Offset: 0x0003078C
		internal static void ConfigureResponderForCafeMinimumValues(ResponderWorkItem responder, Action<AttributeHelper> baseInitializeAttributes, Action<int> setMinimumReadyDelegate, TracingContext traceContext)
		{
			Tuple<string[], int> cafeServersInSameArrayOrSite = CafeUtils.GetCafeServersInSameArrayOrSite();
			int item = cafeServersInSameArrayOrSite.Item2;
			int num = item / 2 + 1;
			double num2 = -1.0;
			if (responder != null && baseInitializeAttributes != null)
			{
				AttributeHelper attributeHelper = new AttributeHelper(responder.Definition);
				responder.Definition.Attributes["ServersInGroup"] = string.Join(";", cafeServersInSameArrayOrSite.Item1);
				baseInitializeAttributes(attributeHelper);
				num2 = attributeHelper.GetDouble("CafeMinimumServerFractionOnline", false, -1.0, null, null);
				num = CafeUtils.GetMinimumRequiredServers(item, num2);
			}
			else
			{
				WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.CafeTracer, traceContext, "[CafeOffline] : Either responder ({0}) or baseInitializeAttributes ({0}) is null. Will only set minimumRequiredReady to default.", (responder == null) ? "<NULL>" : responder.Definition.Name, (baseInitializeAttributes == null) ? "<NULL>" : "Not null", null, "ConfigureResponderForCafeMinimumValues", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CafeUtils.Responders.cs", 157);
			}
			setMinimumReadyDelegate(num);
			WTFDiagnostics.TraceInformation<string, int, int, double>(ExTraceGlobals.CafeTracer, traceContext, "[CafeOffline] {0}: {1} out of {2} servers should remain online (inputFraction: {3})", (responder == null) ? "<NULL>" : responder.Definition.Name, num, item, num2, null, "ConfigureResponderForCafeMinimumValues", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CafeUtils.Responders.cs", 167);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000326AE File Offset: 0x000308AE
		internal static string[] GetCafeServersInSameArray()
		{
			return CafeUtils.GetCafeServersInSameArrayOrSite().Item1;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000326E4 File Offset: 0x000308E4
		internal static Tuple<string[], int> GetCafeServersInSameArrayOrSite()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 200, "GetCafeServersInSameArrayOrSite", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CafeUtils.Responders.cs");
			if (topologyConfigurationSession == null)
			{
				throw new ApplicationException("Couldn't create ADSession.");
			}
			ADObjectId adobjectId = null;
			ClientAccessArray clientAccessArray = null;
			bool flag = VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.CafeOfflineRespondersUseClientAccessArray.Enabled || CafeUtils.UseClientAccessArrayForTestOnly;
			if (flag)
			{
				Server server2 = topologyConfigurationSession.FindLocalServer();
				adobjectId = (ADObjectId)server2[ServerSchema.ClientAccessArray];
				if (adobjectId != null)
				{
					QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, adobjectId),
						QueryFilter.NotFilter(ClientAccessArray.PriorTo15ExchangeObjectVersionFilter)
					});
					clientAccessArray = topologyConfigurationSession.FindUnique<ClientAccessArray>(null, QueryScope.SubTree, filter);
					if (clientAccessArray != null)
					{
						int num = clientAccessArray.ServerCount;
						int count = clientAccessArray.Servers.Count;
						if (num < count)
						{
							ExTraceGlobals.CafeTracer.TraceInformation<int, int>(0, 0L, "[GetCafeServersInSameArrayOrSite] array.ServerCount {0} should be always larger than array.Servers.Count {1}! Set it to array.Servers.Count", num, count);
							num = count;
						}
						return Tuple.Create<string[], int>((from x in clientAccessArray.Servers
						select x.Name + "." + x.DomainId).ToArray<string>(), num);
					}
				}
			}
			ExTraceGlobals.CafeTracer.TraceInformation(0, 0L, "[GetCafeServersInSameArrayOrSite] Didn't get servers from array. useClientAccessArrayEnabled {0}, arrayId {1}, array {2}, array.ServerCount {3}. Will try site.", new object[]
			{
				flag,
				(adobjectId == null) ? "<NULL>" : adobjectId.ToString(),
				(clientAccessArray == null) ? "<NULL>" : clientAccessArray.Name,
				(clientAccessArray == null) ? "<UNKNOWN>" : clientAccessArray.ServerCount.ToString()
			});
			ADSite localSite = topologyConfigurationSession.GetLocalSite();
			if (localSite == null)
			{
				throw new ApplicationException("Couldn't get the local ADSite.");
			}
			IEnumerable<string> source = from server in topologyConfigurationSession.FindPaged<Server>(null, QueryScope.SubTree, QueryFilter.AndTogether(new QueryFilter[]
			{
				new BitMaskAndFilter(ServerSchema.CurrentServerRole, 1UL),
				new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, localSite.Id)
			}), null, ADGenericPagedReader<Server>.DefaultPageSize)
			where server.IsE15OrLater
			select server into x
			select x.Fqdn;
			string[] array = source.ToArray<string>();
			return Tuple.Create<string[], int>(array, array.Length);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00032934 File Offset: 0x00030B34
		private static int GetMinimumRequiredServers(int totalCafeServers, double fractionRequired)
		{
			int num;
			if (fractionRequired != -1.0)
			{
				if (fractionRequired < 0.0 || fractionRequired > 1.0)
				{
					throw new ArgumentException("Input fraction is outside of range 0.0 <= x <= 1.0", "fraction");
				}
				num = (int)((double)(totalCafeServers + 1) * fractionRequired);
			}
			else
			{
				fractionRequired = 0.8;
				switch (totalCafeServers)
				{
				case 1:
					num = 0;
					break;
				case 2:
				case 3:
					num = 1;
					break;
				case 4:
					num = 2;
					break;
				case 5:
					num = 3;
					break;
				case 6:
					num = 4;
					break;
				default:
					num = (int)((double)(totalCafeServers + 1) * fractionRequired);
					break;
				}
			}
			if (num > totalCafeServers)
			{
				return totalCafeServers;
			}
			return num;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000329D4 File Offset: 0x00030BD4
		internal static void ConfigureResponderForCafeFailureCorrelation(ResponderDefinition responderDefinition)
		{
			ProtocolDescriptor protocolDescriptor = CafeProtocols.Get(HttpProtocol.EWS);
			responderDefinition.ActionOnCorrelatedMonitors = CorrelatedMonitorAction.GenerateException;
			responderDefinition.CorrelatedMonitors = new CorrelatedMonitorInfo[]
			{
				new CorrelatedMonitorInfo(string.Format("{0}\\{1}{2}Monitor\\{3}", new object[]
				{
					protocolDescriptor.HealthSet.Name,
					protocolDescriptor.HealthSet.ShortName,
					ProbeType.ProxyTest.ToString(),
					protocolDescriptor.AppPool
				}), null, CorrelatedMonitorInfo.MatchMode.Wildcard)
			};
		}

		// Token: 0x040005D9 RID: 1497
		public const string E15OutlookHostName = "pilot.outlook.com";

		// Token: 0x040005DA RID: 1498
		public const string E15SdfOutlookHostName = "sdfpilot.outlook.com";

		// Token: 0x040005DB RID: 1499
		public const string ComponentId = "UnifiedNamespaceAMProbe";

		// Token: 0x040005DC RID: 1500
		private static bool runningUnderCafeMode = false;

		// Token: 0x040005DD RID: 1501
		internal static bool UseClientAccessArrayForTestOnly;

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x060008A0 RID: 2208
		public delegate void DoWork(CancellationToken cancellationToken);

		// Token: 0x0200011E RID: 286
		internal static class OfflineAttributeNames
		{
			// Token: 0x040005E2 RID: 1506
			internal const string MinimumServerFractionOnline = "CafeMinimumServerFractionOnline";
		}

		// Token: 0x0200011F RID: 287
		internal static class OfflineDefaultValues
		{
			// Token: 0x040005E3 RID: 1507
			internal const double MinimumServerFractionOnline = 0.8;

			// Token: 0x040005E4 RID: 1508
			internal const double UseDefaultValueFlag = -1.0;
		}

		// Token: 0x02000120 RID: 288
		internal static class ComponentStateAttributeNames
		{
			// Token: 0x040005E5 RID: 1509
			internal const string ServerComponentName = "ComponentStateServerComponentName";

			// Token: 0x040005E6 RID: 1510
			internal const string TriggerConfig = "ComponentStateTriggerConfig";
		}

		// Token: 0x02000121 RID: 289
		internal enum TriggerConfig
		{
			// Token: 0x040005E8 RID: 1512
			ExecuteIfOnline,
			// Token: 0x040005E9 RID: 1513
			ExecuteIfOffline
		}
	}
}
