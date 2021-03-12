using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x02000283 RID: 643
	public sealed class GlsWebServiceDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x00040B94 File Offset: 0x0003ED94
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!this.IsSupportedRoleInstalled())
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.DoWork]: None of the supported roles({0}) installed on this server.", string.Join(",", GlsWebServiceDiscovery.supportedRoles), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 96);
				base.Result.StateAttribute1 = string.Format("GlsWebServiceDiscovery: None of the supported roles({0}) installed on this server.", string.Join(",", GlsWebServiceDiscovery.supportedRoles));
				return;
			}
			XmlNode definitionNode = GenericWorkItemHelper.GetDefinitionNode("GlsWebService.xml", GlsWebServiceDiscovery.traceContext);
			this.UpdateEndpoints(definitionNode);
			GenericWorkItemHelper.CreateCustomDefinitions(definitionNode, base.Broker, GlsWebServiceDiscovery.traceContext, base.Result);
			if (FfoLocalEndpointManager.IsDomainNameServerRoleInstalled)
			{
				GenericWorkItemHelper.CreateAllDefinitions(new List<string>
				{
					"FFODNS_GlobalLocatorService.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
			else
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.DoWork]: DomainNameServer role is not installed on this server, skipping FFODNS_GlobalLocatorService.xml", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 128);
				base.Result.StateAttribute1 = "GlsWebServiceDiscovery: DomainNameServer role is not installed on this server, skipping FFODNS_GlobalLocatorService.xml";
				GenericWorkItemHelper.CompleteDiscovery(GlsWebServiceDiscovery.traceContext);
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.DoWork]: GLS work item definitions created", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 138);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00040CC0 File Offset: 0x0003EEC0
		private bool IsSupportedRoleInstalled()
		{
			foreach (string role in GlsWebServiceDiscovery.supportedRoles)
			{
				if (FfoLocalEndpointManager.GetRole(role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00040CF4 File Offset: 0x0003EEF4
		private void UpdateEndpoints(XmlNode contextRoot)
		{
			GlsWebServiceDiscovery.GlsEndPoint endPoint = this.GetEndPoint();
			foreach (object obj in contextRoot.SelectNodes("CustomWorkItem/Probe/ExtensionAttributes/WorkContext/WebServiceConfiguration"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				string mandatoryXmlAttribute = Utils.GetMandatoryXmlAttribute<string>(xmlNode.ParentNode.ParentNode.ParentNode, "Name");
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("Uri[@Address='']");
				if (xmlNode2 != null)
				{
					XmlElement xmlElement = Utils.CheckXmlElement(xmlNode2, "WebserviceConfiguration/Uri");
					xmlElement.SetAttribute("Address", endPoint.Uri);
					xmlElement.SetAttribute("IgnoreInvalidSslCertificateError", endPoint.IgnoreInvalidCertificate.ToString());
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.UpdateEndpoints]: Updating Uri for Probe={0}", mandatoryXmlAttribute, null, "UpdateEndpoints", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 178);
				}
				else
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.UpdateEndpoints]: Skipping Uri update for Probe={0}", mandatoryXmlAttribute, null, "UpdateEndpoints", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 182);
				}
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("Proxy/Credentials[@FindValue='']");
				if (xmlNode3 != null)
				{
					XmlElement xmlElement2 = Utils.CheckXmlElement(xmlNode3, "WebserviceConfiguration/Proxy/Credentials");
					xmlElement2.SetAttribute("FindValue", endPoint.CertificateSubjectName);
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.UpdateEndpoints]: Updating Certificate Subject name for Probe={0}", mandatoryXmlAttribute, null, "UpdateEndpoints", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 191);
				}
				else
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.UpdateEndpoints]: Skipping Certificate Subject name update for Probe={0}", mandatoryXmlAttribute, null, "UpdateEndpoints", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 195);
				}
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00040E98 File Offset: 0x0003F098
		private GlsWebServiceDiscovery.GlsEndPoint GetEndPoint()
		{
			GlsWebServiceDiscovery.GlsEndPoint result;
			if (FfoLocalEndpointManager.IsDomainNameServerRoleInstalled)
			{
				result = this.GetEndpointFromDomainNameServer();
			}
			else if (FfoLocalEndpointManager.IsBackgroundRoleInstalled)
			{
				result = this.GetEndPointFromBackground();
			}
			else
			{
				if (!FfoLocalEndpointManager.IsFrontendTransportRoleInstalled)
				{
					throw new Exception("The current role is not supported");
				}
				result = this.GetEndPointFromFrontDoor();
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.WebServiceTracer, GlsWebServiceDiscovery.traceContext, "[GlsWebServiceDiscovery.GetEndPoint]: {0}", base.Result.StateAttribute5, null, "GetEndPoint", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs", 225);
			return result;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00040F10 File Offset: 0x0003F110
		private GlsWebServiceDiscovery.GlsEndPoint GetEndpointFromDomainNameServer()
		{
			GlsWebServiceDiscovery.GlsEndPoint result = new GlsWebServiceDiscovery.GlsEndPoint
			{
				Uri = "https://localhost/GLS/service.svc",
				CertificateSubjectName = Utils.GetStringValueFromRegistry("SOFTWARE\\Microsoft\\FfoDomainNameServer\\Config", "GlsLAMCertFindValue"),
				IgnoreInvalidCertificate = true
			};
			base.Result.StateAttribute5 = string.Format("Using default Endpoint, {0}:{1}", result.Uri, result.CertificateSubjectName);
			return result;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00040F78 File Offset: 0x0003F178
		private GlsWebServiceDiscovery.GlsEndPoint GetEndPointFromFrontDoor()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 252, "GetEndPointFromFrontDoor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\WebService\\GlsWebServiceDiscovery.cs");
			ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
			ServiceEndpoint endpoint = endpointContainer.GetEndpoint("GlobalLocatorService");
			GlsWebServiceDiscovery.GlsEndPoint result = new GlsWebServiceDiscovery.GlsEndPoint
			{
				Uri = endpoint.Uri.OriginalString,
				CertificateSubjectName = endpoint.CertificateSubject,
				IgnoreInvalidCertificate = false
			};
			base.Result.StateAttribute5 = string.Format("Using Endpoint from FrontDoor, {0}:{1}", result.Uri, result.CertificateSubjectName);
			return result;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00041010 File Offset: 0x0003F210
		private GlsWebServiceDiscovery.GlsEndPoint GetEndPointFromBackground()
		{
			string text = Path.Combine(Utils.GetStringValueFromRegistry("SOFTWARE\\Microsoft\\FfoBackground\\Setup", "MsiInstallPath"), "Microsoft.Exchange.Hygiene.AsyncQueueDaemon.exe");
			LocatorServiceClientConfiguration locatorServiceClientConfiguration = ConfigurationManager.OpenExeConfiguration(text).GetSection("globalLocatorService") as LocatorServiceClientConfiguration;
			if (locatorServiceClientConfiguration == null)
			{
				throw new Exception(string.Format("Unable to get GLS endpoint setting from configuration file of {0}", text));
			}
			GlsWebServiceDiscovery.GlsEndPoint result = new GlsWebServiceDiscovery.GlsEndPoint
			{
				Uri = locatorServiceClientConfiguration.EndpointUri,
				CertificateSubjectName = locatorServiceClientConfiguration.EndpointCertSubject,
				IgnoreInvalidCertificate = false
			};
			base.Result.StateAttribute5 = string.Format("Using Endpoint from Background, {0}:{1}", result.Uri, result.CertificateSubjectName);
			return result;
		}

		// Token: 0x04000A38 RID: 2616
		private const string EscalationTeam = "FFO Web Service";

		// Token: 0x04000A39 RID: 2617
		private const string DefaultGlsUri = "https://localhost/GLS/service.svc";

		// Token: 0x04000A3A RID: 2618
		private const string GlsEndpointNameInAD = "GlobalLocatorService";

		// Token: 0x04000A3B RID: 2619
		private const string DefaultCertificateRegPath = "SOFTWARE\\Microsoft\\FfoDomainNameServer\\Config";

		// Token: 0x04000A3C RID: 2620
		private const string DefaultCertificateRegKey = "GlsLAMCertFindValue";

		// Token: 0x04000A3D RID: 2621
		private const string BackgroundApplicationInstallRegPath = "SOFTWARE\\Microsoft\\FfoBackground\\Setup";

		// Token: 0x04000A3E RID: 2622
		private const string BackgroundApplicationInstallRegKey = "MsiInstallPath";

		// Token: 0x04000A3F RID: 2623
		private const string BackgroundApplicationName = "Microsoft.Exchange.Hygiene.AsyncQueueDaemon.exe";

		// Token: 0x04000A40 RID: 2624
		private const string BackgroundApplicationSectionName = "globalLocatorService";

		// Token: 0x04000A41 RID: 2625
		private static readonly string[] supportedRoles = new string[]
		{
			"DomainNameServer",
			"FrontendTransport",
			"Background"
		};

		// Token: 0x04000A42 RID: 2626
		private static TracingContext traceContext = new TracingContext();

		// Token: 0x02000284 RID: 644
		private struct GlsEndPoint
		{
			// Token: 0x04000A43 RID: 2627
			public string Uri;

			// Token: 0x04000A44 RID: 2628
			public string CertificateSubjectName;

			// Token: 0x04000A45 RID: 2629
			public bool IgnoreInvalidCertificate;
		}
	}
}
