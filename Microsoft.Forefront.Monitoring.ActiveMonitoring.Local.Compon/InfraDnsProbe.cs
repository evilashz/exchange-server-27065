using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000088 RID: 136
	public class InfraDnsProbe : ProbeWorkItem
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00015AA8 File Offset: 0x00013CA8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Stopwatch stopwatch2 = Stopwatch.StartNew();
			try
			{
				IEnumerable<string> domainsToResolve = this.GetDomainsToResolve(base.Definition.ExtensionAttributes);
				if (domainsToResolve.Count<string>() == 0)
				{
					throw new InvalidDataException("No domains were identified for look up");
				}
				base.Result.ExecutionContext = string.Format("{0}:{1}", string.Join(Environment.NewLine, domainsToResolve), stopwatch2.ElapsedMilliseconds);
				foreach (string text in domainsToResolve)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException(cancellationToken);
					}
					try
					{
						this.TraceInformation(string.Format("Get host entry for '{0}'", text));
						stopwatch2.Restart();
						IPHostEntry hostEntry = Dns.GetHostEntry(text);
						if (hostEntry.AddressList.Count<IPAddress>() == 0)
						{
							this.failureMessages.Add(string.Format("{0}:zero addresses returned", text));
						}
						else
						{
							base.Result.ExecutionContext = string.Format("{0}:{1}:{2}", base.Result.ExecutionContext, text, stopwatch2.ElapsedMilliseconds);
						}
					}
					catch (SocketException ex)
					{
						this.failureMessages.Add(string.Format("{0}:{1}", text, ex.Message));
					}
				}
				if (this.failureMessages.Count > 0)
				{
					throw new DnsMonitorException(string.Join(Environment.NewLine, this.failureMessages), null);
				}
			}
			finally
			{
				stopwatch.Stop();
				stopwatch2.Stop();
				base.Result.SampleValue = (double)stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00015C78 File Offset: 0x00013E78
		private static string GetDomainName(string value)
		{
			Uri uri;
			if (Uri.TryCreate(value, UriKind.Absolute, out uri))
			{
				return uri.Host;
			}
			return value;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00015C98 File Offset: 0x00013E98
		private static bool CheckRoles(XmlElement element)
		{
			List<string> roles = InfraDnsProbe.GetRoles(element, "ExchangeRoles");
			List<string> roles2 = InfraDnsProbe.GetRoles(element, "FfoRoles");
			if (roles.Count == 0 && roles2.Count == 0)
			{
				throw new InvalidDataException(string.Format("Atleast one role should be specified for the node '{0}'", element.Name));
			}
			List<string> list = new List<string>();
			List<string> excludeFfoRoles = list;
			List<string> exchangeRoles = roles;
			return DiscoveryContext.CheckRoles(excludeFfoRoles, roles2, list, exchangeRoles);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00015CF4 File Offset: 0x00013EF4
		private static List<string> GetRoles(XmlElement element, string attributeName)
		{
			List<string> list = new List<string>();
			string optionalXmlAttribute = Utils.GetOptionalXmlAttribute<string>(element, attributeName, null);
			if (!string.IsNullOrWhiteSpace(optionalXmlAttribute))
			{
				list.AddRange(optionalXmlAttribute.Split(InfraDnsProbe.CommaSeparator, StringSplitOptions.RemoveEmptyEntries));
			}
			return list;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00015D2C File Offset: 0x00013F2C
		private IEnumerable<string> GetDomainsToResolve(string workContextXml)
		{
			if (string.IsNullOrWhiteSpace(workContextXml))
			{
				throw new ArgumentNullException("workContextXml");
			}
			List<string> list = new List<string>();
			this.TraceInformation("Trying to get domains to resolve from the work context");
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.LoadXml(workContextXml);
			XmlElement xmlElement = Utils.CheckXmlElement(safeXmlDocument.SelectSingleNode("//WorkContext"), "//WorkContext");
			this.LoadVariables(xmlElement);
			list.AddRange(this.GetDomainsFromConfigAppSettings(xmlElement));
			list.AddRange(this.GetDomainsFromConfigCustomSettings(xmlElement));
			list.AddRange(this.GetDomainsFromAD(xmlElement));
			list.AddRange(this.GetDomainsFromSendConnectorSmartHosts(xmlElement));
			return list;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015DD8 File Offset: 0x00013FD8
		private void LoadVariables(XmlElement workContext)
		{
			XmlElement xmlElement = workContext.SelectSingleNode("RegistryVariables") as XmlElement;
			if (xmlElement != null)
			{
				foreach (object obj in xmlElement.SelectNodes("Var"))
				{
					XmlNode node = (XmlNode)obj;
					XmlElement xmlElement2 = Utils.CheckXmlElement(node, "Var");
					if (InfraDnsProbe.CheckRoles(xmlElement2))
					{
						string key = string.Format("${{{0}}}", Utils.GetMandatoryXmlAttribute<string>(xmlElement2, "Name"));
						string stringValueFromRegistry = Utils.GetStringValueFromRegistry(Utils.GetMandatoryXmlAttribute<string>(xmlElement2, "Path"), Utils.GetMandatoryXmlAttribute<string>(xmlElement2, "Key"));
						this.variables[key] = stringValueFromRegistry;
					}
				}
				base.Result.StateAttribute25 = string.Join(",", from pair in this.variables
				select string.Format("[{0}={1}]", pair.Key, pair.Value));
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00015EE0 File Offset: 0x000140E0
		private IEnumerable<string> GetDomainsFromConfigAppSettings(XmlElement workContext)
		{
			this.TraceInformation("Getting domains from App setttings Config");
			List<string> list = new List<string>();
			foreach (object obj in workContext.SelectNodes("DomainNamesToResolve/ConfigAppSetting"))
			{
				XmlNode node = (XmlNode)obj;
				XmlElement element = Utils.CheckXmlElement(node, "DomainNamesToResolve/ConfigAppSetting");
				if (InfraDnsProbe.CheckRoles(element))
				{
					string appSettingValue = this.GetAppSettingValue(this.GetAttributeValue(element, "FileName"), this.GetAttributeValue(element, "AppSettingKey"));
					list.Add(InfraDnsProbe.GetDomainName(appSettingValue));
				}
			}
			return list;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00015F90 File Offset: 0x00014190
		private IEnumerable<string> GetDomainsFromConfigCustomSettings(XmlElement workContext)
		{
			this.TraceInformation("Getting domains from Custom settings Config");
			List<string> list = new List<string>();
			foreach (object obj in workContext.SelectNodes("DomainNamesToResolve/ConfigCustomSetting"))
			{
				XmlNode node = (XmlNode)obj;
				XmlElement element = Utils.CheckXmlElement(node, "DomainNamesToResolve/ConfigCustomSetting");
				if (InfraDnsProbe.CheckRoles(element))
				{
					string customSettingValue = this.GetCustomSettingValue(this.GetAttributeValue(element, "FileName"), this.GetAttributeValue(element, "SectionName"), this.GetAttributeValue(element, "Attribute"));
					list.Add(InfraDnsProbe.GetDomainName(customSettingValue));
				}
			}
			return list;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001605C File Offset: 0x0001425C
		private IEnumerable<string> GetDomainsFromSendConnectorSmartHosts(XmlElement workContextElement)
		{
			this.TraceInformation("Getting domains from SendConnector Smart hosts");
			List<string> list = new List<string>();
			List<ComparisonFilter> list2 = new List<ComparisonFilter>();
			foreach (object obj in workContextElement.SelectNodes("DomainNamesToResolve/SendConnectorSmarHosts"))
			{
				XmlNode node = (XmlNode)obj;
				XmlElement element = Utils.CheckXmlElement(node, "DomainNamesToResolve/ConfigAppSetting");
				if (InfraDnsProbe.CheckRoles(element))
				{
					string attributeValue = this.GetAttributeValue(element, "Identity");
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, attributeValue));
				}
			}
			if (list2.Count != 0)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 444, "GetDomainsFromSendConnectorSmartHosts", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\InfraDnsProbe.cs");
				SmtpSendConnectorConfig[] array = topologyConfigurationSession.Find<SmtpSendConnectorConfig>(null, QueryScope.SubTree, new OrFilter(list2.ToArray()), null, 0);
				if (array != null)
				{
					foreach (SmtpSendConnectorConfig smtpSendConnectorConfig in array)
					{
						MultiValuedProperty<SmartHost> smartHosts = smtpSendConnectorConfig.SmartHosts;
						list.AddRange(from s in smartHosts
						select s.Domain.HostnameString);
					}
				}
			}
			return list;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000161A4 File Offset: 0x000143A4
		private IEnumerable<string> GetDomainsFromAD(XmlElement workContext)
		{
			this.TraceInformation("Getting domains from AD");
			List<string> list = new List<string>();
			foreach (object obj in workContext.SelectNodes("DomainNamesToResolve/AdEndpoint"))
			{
				XmlNode node = (XmlNode)obj;
				XmlElement element = Utils.CheckXmlElement(node, "DomainNamesToResolve/AdEndpoint");
				string attributeValue = this.GetAttributeValue(element, "Name");
				if (InfraDnsProbe.CheckRoles(element))
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 487, "GetDomainsFromAD", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\InfraDnsProbe.cs");
					if (topologyConfigurationSession == null)
					{
						throw new InvalidOperationException("Failed to retrieve Topology Configuration Session");
					}
					ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
					if (endpointContainer == null)
					{
						throw new InvalidOperationException("Failed to retrieve endpoint container");
					}
					ServiceEndpoint serviceEndpoint = endpointContainer.GetEndpoint(attributeValue);
					if (serviceEndpoint == null)
					{
						throw new InvalidOperationException(string.Format("Failed to retrieve endpoint '{0}'", attributeValue));
					}
					if (serviceEndpoint.Uri == null)
					{
						string attributeValue2 = this.GetAttributeValue(element, "UriTemplateArgs");
						serviceEndpoint = serviceEndpoint.ApplyTemplate(attributeValue2.Split(InfraDnsProbe.CommaSeparator, StringSplitOptions.RemoveEmptyEntries));
					}
					list.Add(serviceEndpoint.Uri.Host);
				}
			}
			return list;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000162EC File Offset: 0x000144EC
		private string GetAttributeValue(XmlElement element, string attributeName)
		{
			string text = Utils.GetMandatoryXmlAttribute<string>(element, attributeName);
			int num;
			while ((num = text.IndexOf("${")) != -1)
			{
				int num2 = text.IndexOf("}", num);
				string text2 = text.Substring(num, num2 - num + 1);
				string newValue;
				if (!this.variables.TryGetValue(text2, out newValue))
				{
					throw new InvalidDataException(string.Format("Variable {0} has not been defined", text2));
				}
				text = text.Replace(text2, newValue);
			}
			return text;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001635C File Offset: 0x0001455C
		private string GetAppSettingValue(string fileName, string appSettingKeyName)
		{
			this.TraceInformation(string.Format("Checking '{0}' for appSetting='{1}'", fileName, appSettingKeyName));
			if (!File.Exists(fileName))
			{
				throw new ArgumentException(string.Format("The config file '{0}' was not found", fileName), "fileName");
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(fileName);
			string text = string.Format("//configuration/appSettings/add[@key='{0}']", appSettingKeyName);
			XmlElement definition = Utils.CheckXmlElement(safeXmlDocument.SelectSingleNode(text), fileName + ":" + text);
			string mandatoryXmlAttribute = Utils.GetMandatoryXmlAttribute<string>(definition, "value");
			this.TraceInformation(string.Format("{0}={1}", appSettingKeyName, mandatoryXmlAttribute));
			return mandatoryXmlAttribute;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000163EC File Offset: 0x000145EC
		private string GetCustomSettingValue(string fileName, string sectionName, string attributeName)
		{
			this.TraceInformation(string.Format("Checking '{0}' for '{1}:{2}'", fileName, sectionName, attributeName));
			if (!File.Exists(fileName))
			{
				throw new ArgumentException(string.Format("The config file '{0}' was not found", fileName), "fileName");
			}
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(fileName);
			string text = string.Format("//configuration/{0}", sectionName);
			XmlElement definition = Utils.CheckXmlElement(safeXmlDocument.SelectSingleNode(text), fileName + ":" + text);
			string mandatoryXmlAttribute = Utils.GetMandatoryXmlAttribute<string>(definition, attributeName);
			this.TraceInformation(string.Format("{0}:{1}={2}", sectionName, attributeName, mandatoryXmlAttribute));
			return mandatoryXmlAttribute;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00016478 File Offset: 0x00014678
		private void TraceInformation(string message)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DNSTracer, base.TraceContext, "InfraDnsProbe:{0}", message, null, "TraceInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\DNS\\Probes\\InfraDnsProbe.cs", 606);
		}

		// Token: 0x04000213 RID: 531
		private const string FfoRoles = "FfoRoles";

		// Token: 0x04000214 RID: 532
		private const string ExchangeRoles = "ExchangeRoles";

		// Token: 0x04000215 RID: 533
		private const string WorkContextNode = "//WorkContext";

		// Token: 0x04000216 RID: 534
		private const string ConfigAppSettingNode = "DomainNamesToResolve/ConfigAppSetting";

		// Token: 0x04000217 RID: 535
		private const string ConfigFileName = "FileName";

		// Token: 0x04000218 RID: 536
		private const string ConfigAppSettingKey = "AppSettingKey";

		// Token: 0x04000219 RID: 537
		private const string ConfigAppSettingNodeXPath = "//configuration/appSettings/add[@key='{0}']";

		// Token: 0x0400021A RID: 538
		private const string ConfigCustomSettingNodeXPath = "//configuration/{0}";

		// Token: 0x0400021B RID: 539
		private const string ConfigAppSettingValue = "value";

		// Token: 0x0400021C RID: 540
		private const string ConfigCustomSettingNode = "DomainNamesToResolve/ConfigCustomSetting";

		// Token: 0x0400021D RID: 541
		private const string ConfigCustomSettingSectionName = "SectionName";

		// Token: 0x0400021E RID: 542
		private const string ConfigCustomSettingAttribute = "Attribute";

		// Token: 0x0400021F RID: 543
		private const string SendConnectorSmarHostsNode = "DomainNamesToResolve/SendConnectorSmarHosts";

		// Token: 0x04000220 RID: 544
		private const string SendConnectorSmarHostsIdentity = "Identity";

		// Token: 0x04000221 RID: 545
		private const string AdEndpointNode = "DomainNamesToResolve/AdEndpoint";

		// Token: 0x04000222 RID: 546
		private const string AdEndpointAttribute = "Name";

		// Token: 0x04000223 RID: 547
		private const string AdEndpointUriTemplateArgs = "UriTemplateArgs";

		// Token: 0x04000224 RID: 548
		private const string RegsitryVariablesNode = "RegistryVariables";

		// Token: 0x04000225 RID: 549
		private const string VariableNode = "Var";

		// Token: 0x04000226 RID: 550
		private const string RegistryPath = "Path";

		// Token: 0x04000227 RID: 551
		private const string RegistryKey = "Key";

		// Token: 0x04000228 RID: 552
		private const string VariableName = "Name";

		// Token: 0x04000229 RID: 553
		private const string VariableNameFormat = "${{{0}}}";

		// Token: 0x0400022A RID: 554
		private const string VariableStartIndicator = "${";

		// Token: 0x0400022B RID: 555
		private const string VariableEndIndicator = "}";

		// Token: 0x0400022C RID: 556
		private static readonly char[] CommaSeparator = new char[]
		{
			','
		};

		// Token: 0x0400022D RID: 557
		private ConcurrentBag<string> failureMessages = new ConcurrentBag<string>();

		// Token: 0x0400022E RID: 558
		private Dictionary<string, string> variables = new Dictionary<string, string>();

		// Token: 0x02000089 RID: 137
		private class AsyncState
		{
			// Token: 0x060003BE RID: 958 RVA: 0x000164DD File Offset: 0x000146DD
			public AsyncState(string domainName)
			{
				this.DomainName = domainName;
				this.ResolveFinishedEvent = new ManualResetEvent(false);
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060003BF RID: 959 RVA: 0x000164F8 File Offset: 0x000146F8
			// (set) Token: 0x060003C0 RID: 960 RVA: 0x00016500 File Offset: 0x00014700
			public string DomainName { get; private set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060003C1 RID: 961 RVA: 0x00016509 File Offset: 0x00014709
			// (set) Token: 0x060003C2 RID: 962 RVA: 0x00016511 File Offset: 0x00014711
			public ManualResetEvent ResolveFinishedEvent { get; private set; }
		}
	}
}
