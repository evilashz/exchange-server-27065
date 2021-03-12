using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000046 RID: 70
	public class WorkDefinitionDeploymentFileReader
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x000121C4 File Offset: 0x000103C4
		public WorkDefinitionDeploymentFileReader(string deploymentFileFolderLocation, TracingContext traceContext)
		{
			this.traceContext = traceContext;
			this.deploymentFileFolderLocation = deploymentFileFolderLocation;
			if (!Path.IsPathRooted(this.deploymentFileFolderLocation))
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					string fileName = currentProcess.MainModule.FileName;
					string directoryName = Path.GetDirectoryName(fileName);
					this.deploymentFileFolderLocation = Path.Combine(directoryName, this.deploymentFileFolderLocation);
				}
			}
			if (!Directory.Exists(this.deploymentFileFolderLocation))
			{
				string message = string.Format("The Deployment file folder {0} does not exist.", this.deploymentFileFolderLocation);
				WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, ".ctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 83);
				throw new ArgumentException(message);
			}
			this.traceContext = traceContext;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00012620 File Offset: 0x00010820
		public IEnumerable<WorkDefinition> GetWorkDefinitions(List<string> fileList = null)
		{
			string nameSpace = base.GetType().Namespace;
			foreach (XmlNode workDefinitionXml in this.GetDeploymentElementsContainingFilter("Definition", fileList))
			{
				string fullTypeName = string.Format("{0}.{1}", nameSpace, workDefinitionXml.Name);
				XmlAttribute workDefinitionNameAttribute = workDefinitionXml.Attributes["Name"];
				string workDefinitionName = string.Format("{0}. Unable to read the Xml element's Name attribute to provide more information.", workDefinitionXml.Name);
				if (workDefinitionNameAttribute != null)
				{
					workDefinitionName = workDefinitionNameAttribute.Value;
				}
				WorkDefinition workDefinition = null;
				try
				{
					WTFDiagnostics.TraceInformation<string>(WTFLog.DataAccess, this.traceContext, "[WorkItemDeploymentFileReader.GetWorkDefinitions]: Attempting to populate Work Definition {0}", workDefinitionName, null, "GetWorkDefinitions", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 124);
					Type type = Type.GetType(fullTypeName);
					if (type == null)
					{
						string message = string.Format("Error loading WorkDefinition type {0}. The type was not found.", fullTypeName);
						WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "GetWorkDefinitions", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 131);
						throw new TypeLoadException(message);
					}
					workDefinition = (WorkDefinition)Activator.CreateInstance(type);
					workDefinition.FromXml(workDefinitionXml);
					WTFDiagnostics.TraceInformation<string>(WTFLog.DataAccess, this.traceContext, "[WorkItemDeploymentFileReader.GetWorkDefinitions]: Successfully populated Work Definition {0}", workDefinitionName, null, "GetWorkDefinitions", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 139);
				}
				catch (Exception ex)
				{
					string message2 = string.Format("[WorkItemDeploymentFileReader.GetWorkDefinitions]: Failed to populate WorkDefinition {0}. Skipping.\n{1}", workDefinitionName, ex);
					WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message2, null, "GetWorkDefinitions", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 144);
					this.exceptionList.Add(ex);
				}
				yield return workDefinition;
			}
			yield break;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000129CC File Offset: 0x00010BCC
		public IEnumerable<WorkDefinitionDeploymentFileReader.PerformanceCounter> GetPerformanceCounterFilters()
		{
			foreach (XmlNode perfCounterFilterXml in this.GetDeploymentElementsContainingFilter("PerformanceCounters", null))
			{
				foreach (object obj in perfCounterFilterXml.ChildNodes)
				{
					XmlNode perfCounterFilter = (XmlNode)obj;
					XmlAttribute objectName = perfCounterFilter.Attributes["Object"];
					XmlAttribute counterName = perfCounterFilter.Attributes["Counter"];
					XmlAttribute instanceName = perfCounterFilter.Attributes["Instance"];
					if (objectName == null || counterName == null || instanceName == null || string.IsNullOrWhiteSpace(objectName.Value.ToString()) || string.IsNullOrWhiteSpace(counterName.Value.ToString()) || string.IsNullOrWhiteSpace(instanceName.Value.ToString()))
					{
						string message = "[WorkItemDeploymentFileReader.GetPerformanceCounterFilters]: Failed to read Counter element from file. Either Object, Counter or Instance attribute was missing or empty.";
						WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "GetPerformanceCounterFilters", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 174);
						throw new XmlException(message);
					}
					yield return new WorkDefinitionDeploymentFileReader.PerformanceCounter
					{
						ObjectName = objectName.Value.ToString(),
						CounterName = counterName.Value.ToString(),
						InstanceName = instanceName.Value.ToString()
					};
				}
			}
			yield break;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000129EC File Offset: 0x00010BEC
		public XmlNode GetMappingsforInstance(string mappingFileName, string nodeName)
		{
			string filter = string.Format("Instance[@Name='{0}']", Settings.InstanceName);
			XmlNode xmlNode = this.GetDeploymentElementsContainingFilter(mappingFileName, "/Mapping", filter, null).FirstOrDefault<XmlNode>();
			if (xmlNode == null)
			{
				string message = string.Format("[WorkItemDeploymentFileReader.GetMappingsforInstance]: Failed to read Instance node for instance {0}.", Settings.InstanceName);
				WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "GetMappingsforInstance", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 205);
				throw new XmlException(message);
			}
			XmlNode xmlNode2 = xmlNode.SelectSingleNode(nodeName);
			if (xmlNode2 == null)
			{
				string message2 = string.Format("[WorkItemDeploymentFileReader.GetMappingsforInstance]: Failed to read {0} node for instance {1}.", nodeName, Settings.InstanceName);
				WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message2, null, "GetMappingsforInstance", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 215);
				throw new XmlException(message2);
			}
			return xmlNode2;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00012AA0 File Offset: 0x00010CA0
		public IEnumerable<string> GetTopologyScopeObjectsMappings(string topologyMappingFileName, out int aggregationLevel)
		{
			XmlNode mappingsforInstance = this.GetMappingsforInstance(topologyMappingFileName, "TopologyScopes");
			XmlAttribute xmlAttribute = mappingsforInstance.Attributes["AggregationLevel"];
			if (xmlAttribute == null || string.IsNullOrWhiteSpace(xmlAttribute.Value.ToString()))
			{
				string message = string.Format("[WorkItemDeploymentFileReader.GetInstanceMappings]: Failed to read AggregationLevel attribute from the TopologyScopes node for instance {0}.", Settings.InstanceName);
				WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "GetTopologyScopeObjectsMappings", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 238);
				throw new XmlException(message);
			}
			aggregationLevel = Convert.ToInt32(xmlAttribute.Value);
			return this.EnumerateTopologyScopeObjects(mappingsforInstance.ChildNodes);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012B34 File Offset: 0x00010D34
		public List<string> GetWorkItemFileMappings(string workItemMappingFileName)
		{
			List<string> list = new List<string>();
			XmlNode mappingsforInstance = this.GetMappingsforInstance(workItemMappingFileName, "WorkItemFiles");
			foreach (object obj in mappingsforInstance.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlAttribute xmlAttribute = xmlNode.Attributes["Name"];
				if (xmlAttribute == null || string.IsNullOrWhiteSpace(xmlAttribute.Value.ToString()))
				{
					string message = "[WorkItemDeploymentFileReader.GetWorkItemFileMappings]: Failed to read work item element from file. The Name attribute was missing or empty.";
					WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "GetWorkItemFileMappings", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 267);
					throw new XmlException(message);
				}
				list.Add(xmlAttribute.Value.ToString());
			}
			return list;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00012C0C File Offset: 0x00010E0C
		public IEnumerable<WorkDefinitionDeploymentFileReader.WorkItemManifest> GetWorkItemManifest()
		{
			List<WorkDefinitionDeploymentFileReader.WorkItemManifest> list = new List<WorkDefinitionDeploymentFileReader.WorkItemManifest>();
			foreach (XmlNode xmlNode in this.GetDeploymentElementsContainingFilter("*.xml", "/", "Definition", null))
			{
				if (this.XmlNodeHasAttribute(xmlNode, "AggregationLevel"))
				{
					string fileName = Path.GetFileName(xmlNode.BaseURI);
					WorkDefinitionDeploymentFileReader.WorkItemManifest workItemManifest = new WorkDefinitionDeploymentFileReader.WorkItemManifest(fileName);
					workItemManifest.AggregationLevel = Convert.ToInt32(this.ReadXmlAttribute(xmlNode, "AggregationLevel"));
					workItemManifest.IsProductionReady = Convert.ToBoolean(this.ReadXmlAttribute(xmlNode, "IsProductionReady"));
					XmlNodeList xmlNodeList = xmlNode.SelectNodes("SupportedEnvironments/Environment");
					if (xmlNodeList.Count == 0)
					{
						string message = string.Format("[WorkItemDeploymentFileReader.GetWorkItemManifest]: Failed to read the Envrionment nodes from file {0}.", fileName);
						throw new XmlException(message);
					}
					foreach (object obj in xmlNodeList)
					{
						XmlNode node = (XmlNode)obj;
						string item = this.ReadXmlAttribute(node, "Name");
						workItemManifest.SupportedEnvironments.Add(item);
					}
					XmlNode xmlNode2 = xmlNode.SelectSingleNode("MaintenanceDefinition");
					if (xmlNode2 == null)
					{
						string message2 = string.Format("[WorkItemDeploymentFileReader.GetWorkItemManifest]: Failed to read MaintenanceDefinition nodes from file {0}.", fileName);
						throw new XmlException(message2);
					}
					workItemManifest.MaintenanceDefinitionName = this.ReadXmlAttribute(xmlNode2, "Name");
					XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Discovery/Probe");
					if (xmlNodeList2.Count == 0)
					{
						string message3 = string.Format("[WorkItemDeploymentFileReader.GetWorkItemManifest]: Failed to read Probe nodes from file {0}.", fileName);
						throw new XmlException(message3);
					}
					foreach (object obj2 in xmlNodeList2)
					{
						XmlNode xmlNode3 = (XmlNode)obj2;
						XmlNode xmlNode4 = xmlNode3.SelectSingleNode("ExpectedNumberOfSamples");
						if (xmlNode4 == null)
						{
							string message4 = string.Format("[WorkItemDeploymentFileReader.GetWorkItemManifest]: Failed to read the ExpectedNumberOfSamples nodes from file {0}.", fileName);
							throw new XmlException(message4);
						}
						int probesPerScope = Convert.ToInt32(this.ReadXmlAttribute(xmlNode4, "Max"));
						int recurrenceInterval = Convert.ToInt32(this.ReadXmlAttribute(xmlNode3, "RecurrenceIntervalSeconds"));
						string name = this.ReadXmlAttribute(xmlNode3, "Name");
						WorkDefinitionDeploymentFileReader.ProbeRuntimeProperties item2 = new WorkDefinitionDeploymentFileReader.ProbeRuntimeProperties(name, recurrenceInterval, probesPerScope);
						workItemManifest.RuntimeProperties.Add(item2);
					}
					list.Add(workItemManifest);
				}
			}
			return list;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00012EA4 File Offset: 0x000110A4
		internal IEnumerable<XmlNode> GetDeploymentElementsContainingFilter(string filter, List<string> fileList = null)
		{
			string filter2 = string.Format("child::*[contains(name(),'{0}')]", filter);
			return this.GetDeploymentElementsContainingFilter("*.xml", "/Definition", filter2, fileList);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013240 File Offset: 0x00011440
		internal IEnumerable<XmlNode> GetDeploymentElementsContainingFilter(string fileNameFilter, string rootName, string filter, List<string> fileList = null)
		{
			string[] deploymentFiles = Directory.GetFiles(this.deploymentFileFolderLocation, fileNameFilter);
			foreach (string deploymentFile in deploymentFiles)
			{
				if (fileList == null || fileList.Contains(Path.GetFileName(deploymentFile)))
				{
					WTFDiagnostics.TraceDebug<string>(WTFLog.DataAccess, this.traceContext, "[WorkItemDeploymentFileReader.GetDeploymentElementsContainingFilter]: Attempting to read deployment file {0}", deploymentFile, null, "GetDeploymentElementsContainingFilter", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 403);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(deploymentFile);
					XmlElement root = xmlDocument.DocumentElement;
					if (root == null)
					{
						string message = string.Format("Error loading deployment element from file {0}. File is malformed and contains no root element.", deploymentFile);
						WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "GetDeploymentElementsContainingFilter", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 414);
						throw new XmlException(message);
					}
					XmlNode definitionNode = root.SelectSingleNode(rootName);
					if (definitionNode != null)
					{
						XmlNodeList deploymentNodes = definitionNode.SelectNodes(filter);
						foreach (object obj in deploymentNodes)
						{
							XmlNode deploymentElementXml = (XmlNode)obj;
							yield return deploymentElementXml;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001327C File Offset: 0x0001147C
		private string ReadXmlAttribute(XmlNode node, string attributeName)
		{
			XmlAttribute xmlAttribute = node.Attributes[attributeName];
			if (xmlAttribute == null || string.IsNullOrWhiteSpace(xmlAttribute.Value.ToString()))
			{
				string message = string.Format("[WorkItemDeploymentFileReader.ReadXmlAttribute]: Failed to read attribute {0} from {1}.", attributeName, node.BaseURI);
				WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "ReadXmlAttribute", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 448);
				throw new XmlException(message);
			}
			return xmlAttribute.Value.ToString();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000132F0 File Offset: 0x000114F0
		private bool XmlNodeHasAttribute(XmlNode node, string attributeName)
		{
			XmlAttribute xmlAttribute = node.Attributes[attributeName];
			return xmlAttribute != null && !string.IsNullOrWhiteSpace(xmlAttribute.Value.ToString());
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013554 File Offset: 0x00011754
		private IEnumerable<string> EnumerateTopologyScopeObjects(XmlNodeList nodeList)
		{
			foreach (object obj in nodeList)
			{
				XmlNode topologyNode = (XmlNode)obj;
				XmlAttribute name = topologyNode.Attributes["Name"];
				if (name == null || string.IsNullOrWhiteSpace(name.Value.ToString()))
				{
					string message = "[WorkItemDeploymentFileReader.EnumerateTopologyScopeObjects]: Failed to read topology element from file. The Name attribute was missing or empty.";
					WTFDiagnostics.TraceError(WTFLog.DataAccess, this.traceContext, message, null, "EnumerateTopologyScopeObjects", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkDefinitionDeploymentFileReader.cs", 489);
					throw new XmlException(message);
				}
				yield return name.Value.ToString();
			}
			yield break;
		}

		// Token: 0x04000377 RID: 887
		private const string TopologyScopesNode = "TopologyScopes";

		// Token: 0x04000378 RID: 888
		private const string WorkItemFilesNode = "WorkItemFiles";

		// Token: 0x04000379 RID: 889
		private readonly string deploymentFileFolderLocation = string.Empty;

		// Token: 0x0400037A RID: 890
		private readonly TracingContext traceContext = TracingContext.Default;

		// Token: 0x0400037B RID: 891
		internal List<Exception> exceptionList = new List<Exception>();

		// Token: 0x02000047 RID: 71
		public struct PerformanceCounter
		{
			// Token: 0x0400037C RID: 892
			public string ObjectName;

			// Token: 0x0400037D RID: 893
			public string CounterName;

			// Token: 0x0400037E RID: 894
			public string InstanceName;
		}

		// Token: 0x02000048 RID: 72
		public class WorkItemManifest
		{
			// Token: 0x0600050B RID: 1291 RVA: 0x00013578 File Offset: 0x00011778
			public WorkItemManifest(string fileName)
			{
				this.MaintenanceDefinitionName = string.Empty;
				this.FileName = fileName;
				this.SupportedEnvironments = new List<string>();
				this.RuntimeProperties = new List<WorkDefinitionDeploymentFileReader.ProbeRuntimeProperties>();
				this.AggregationLevel = -1;
				this.IsProductionReady = false;
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x000135B8 File Offset: 0x000117B8
			public double CalculateTotalCost()
			{
				double num = 0.0;
				foreach (WorkDefinitionDeploymentFileReader.ProbeRuntimeProperties probeRuntimeProperties in this.RuntimeProperties)
				{
					num += (double)probeRuntimeProperties.ProbesPerScope / (double)probeRuntimeProperties.RecurrenceIntervalSeconds * (double)probeRuntimeProperties.ProbeSize;
				}
				return num;
			}

			// Token: 0x0400037F RID: 895
			public string MaintenanceDefinitionName;

			// Token: 0x04000380 RID: 896
			public string FileName;

			// Token: 0x04000381 RID: 897
			public int AggregationLevel;

			// Token: 0x04000382 RID: 898
			public bool IsProductionReady;

			// Token: 0x04000383 RID: 899
			public List<string> SupportedEnvironments;

			// Token: 0x04000384 RID: 900
			public List<WorkDefinitionDeploymentFileReader.ProbeRuntimeProperties> RuntimeProperties;
		}

		// Token: 0x02000049 RID: 73
		public struct ProbeRuntimeProperties
		{
			// Token: 0x0600050D RID: 1293 RVA: 0x0001362C File Offset: 0x0001182C
			public ProbeRuntimeProperties(string name, int recurrenceInterval, int probesPerScope)
			{
				this.Name = name;
				this.RecurrenceIntervalSeconds = recurrenceInterval;
				this.ProbesPerScope = probesPerScope;
				this.ProbeSize = 1;
			}

			// Token: 0x04000385 RID: 901
			public string Name;

			// Token: 0x04000386 RID: 902
			public int RecurrenceIntervalSeconds;

			// Token: 0x04000387 RID: 903
			public int ProbesPerScope;

			// Token: 0x04000388 RID: 904
			public int ProbeSize;
		}
	}
}
