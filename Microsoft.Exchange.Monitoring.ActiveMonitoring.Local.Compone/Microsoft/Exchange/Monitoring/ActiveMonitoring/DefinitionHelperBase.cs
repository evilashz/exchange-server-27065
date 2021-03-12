using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x02000074 RID: 116
	internal abstract class DefinitionHelperBase
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00018D88 File Offset: 0x00016F88
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00018D90 File Offset: 0x00016F90
		internal string AssemblyPath { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00018D99 File Offset: 0x00016F99
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00018DA1 File Offset: 0x00016FA1
		internal string TypeName { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00018DAA File Offset: 0x00016FAA
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00018DB2 File Offset: 0x00016FB2
		internal string Name { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00018DBB File Offset: 0x00016FBB
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00018DC3 File Offset: 0x00016FC3
		internal Component Component { get; set; }

		// Token: 0x060003E6 RID: 998 RVA: 0x00018DCC File Offset: 0x00016FCC
		internal void SetComponentByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException("name");
			}
			Component component = null;
			ExchangeComponent.WellKnownComponents.TryGetValue(name, out component);
			if (component == null)
			{
				string message = string.Format("Cannot find the component name {0} in the Exchange components table.", name);
				WTFDiagnostics.TraceError(ExTraceGlobals.GenericHelperTracer, this.TraceContext, message, null, "SetComponentByName", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 70);
				throw new XmlException(message);
			}
			this.Component = component;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00018E3D File Offset: 0x0001703D
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00018E45 File Offset: 0x00017045
		internal string WorkItemVersion { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00018E4E File Offset: 0x0001704E
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00018E56 File Offset: 0x00017056
		internal string ComponentName { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00018E5F File Offset: 0x0001705F
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00018E67 File Offset: 0x00017067
		internal string ServiceName { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00018E70 File Offset: 0x00017070
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00018E78 File Offset: 0x00017078
		internal int DeploymentId { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00018E81 File Offset: 0x00017081
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00018E89 File Offset: 0x00017089
		internal string ExecutionLocation { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00018E92 File Offset: 0x00017092
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00018E9A File Offset: 0x0001709A
		internal DateTime CreatedTime { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00018EA3 File Offset: 0x000170A3
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00018EAB File Offset: 0x000170AB
		internal bool Enabled { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00018EB4 File Offset: 0x000170B4
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00018EBC File Offset: 0x000170BC
		internal string TargetPartition { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00018EC5 File Offset: 0x000170C5
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00018ECD File Offset: 0x000170CD
		internal string TargetGroup { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00018ED6 File Offset: 0x000170D6
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00018EDE File Offset: 0x000170DE
		internal string TargetResource { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00018EE7 File Offset: 0x000170E7
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00018EEF File Offset: 0x000170EF
		internal string TargetExtension { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00018EF8 File Offset: 0x000170F8
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x00018F00 File Offset: 0x00017100
		internal string TargetVersion { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00018F09 File Offset: 0x00017109
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x00018F11 File Offset: 0x00017111
		internal int RecurrenceIntervalSeconds { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x00018F1A File Offset: 0x0001711A
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x00018F22 File Offset: 0x00017122
		internal int TimeoutSeconds { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00018F2B File Offset: 0x0001712B
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00018F33 File Offset: 0x00017133
		internal DateTime StartTime { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00018F3C File Offset: 0x0001713C
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00018F44 File Offset: 0x00017144
		internal DateTime UpdateTime { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00018F4D File Offset: 0x0001714D
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00018F55 File Offset: 0x00017155
		internal int MaxRetryAttempts { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00018F5E File Offset: 0x0001715E
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00018F66 File Offset: 0x00017166
		internal string ExtensionAttributes { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00018F6F File Offset: 0x0001716F
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00018F77 File Offset: 0x00017177
		internal Type WorkItemType { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00018F80 File Offset: 0x00017180
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00018F88 File Offset: 0x00017188
		internal XmlNode DefinitionNode { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00018F91 File Offset: 0x00017191
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00018F99 File Offset: 0x00017199
		internal DiscoveryContext DiscoveryContext { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00018FA2 File Offset: 0x000171A2
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00018FAA File Offset: 0x000171AA
		internal TracingContext TraceContext { get; set; }

		// Token: 0x06000413 RID: 1043 RVA: 0x00018FB4 File Offset: 0x000171B4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.AppendLine("AssemblyPath: " + this.AssemblyPath);
			stringBuilder.AppendLine("TypeName: " + this.TypeName);
			stringBuilder.AppendLine("Name: " + this.Name);
			stringBuilder.AppendLine("ComponentName: " + this.ComponentName);
			stringBuilder.AppendLine("MaxRetryAttempts: " + this.MaxRetryAttempts);
			stringBuilder.AppendLine("RecurrenceIntervalSeconds: " + this.RecurrenceIntervalSeconds);
			stringBuilder.AppendLine("TimeoutSeconds: " + this.TimeoutSeconds);
			stringBuilder.AppendLine("Enabled: " + this.Enabled);
			stringBuilder.AppendLine("ExtensionAttributes: " + this.ExtensionAttributes);
			stringBuilder.AppendLine("TargetPartition: " + this.TargetPartition);
			stringBuilder.AppendLine("TargetGroup: " + this.TargetGroup);
			stringBuilder.AppendLine("TargetResource: " + this.TargetResource);
			stringBuilder.AppendLine("TargetVersion: " + this.TargetVersion);
			stringBuilder.AppendLine("TargetExtension: " + this.TargetExtension);
			stringBuilder.AppendLine("WorkItemVersion: " + this.WorkItemVersion);
			stringBuilder.AppendLine("DeploymentId: " + this.DeploymentId);
			stringBuilder.AppendLine("ExecutionLocation: " + this.ExecutionLocation);
			return stringBuilder.ToString();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00019174 File Offset: 0x00017374
		internal static ReturnType GetMandatoryXmlAttribute<ReturnType>(XmlNode definition, string attributeName, TracingContext traceContext)
		{
			ReturnType returnType = default(ReturnType);
			XmlAttribute xmlAttribute = DefinitionHelperBase.GetXmlAttribute(definition, attributeName, true, traceContext);
			return (ReturnType)((object)Convert.ChangeType(xmlAttribute.Value, typeof(ReturnType)));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000191B0 File Offset: 0x000173B0
		internal static ReturnType GetOptionalXmlAttribute<ReturnType>(XmlNode definition, string attributeName, ReturnType defaultValue, TracingContext traceContext)
		{
			ReturnType result = default(ReturnType);
			XmlAttribute xmlAttribute = DefinitionHelperBase.GetXmlAttribute(definition, attributeName, false, traceContext);
			if (xmlAttribute == null)
			{
				result = defaultValue;
			}
			else
			{
				result = (ReturnType)((object)Convert.ChangeType(xmlAttribute.Value, typeof(ReturnType)));
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000191F4 File Offset: 0x000173F4
		internal static ReturnType GetMandatoryXmlEnumAttribute<ReturnType>(XmlNode definition, string attributeName, TracingContext traceContext)
		{
			XmlAttribute xmlAttribute = DefinitionHelperBase.GetXmlAttribute(definition, attributeName, true, traceContext);
			return (ReturnType)((object)Enum.Parse(typeof(ReturnType), xmlAttribute.Value));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00019228 File Offset: 0x00017428
		internal static ReturnType GetOptionalXmlEnumAttribute<ReturnType>(XmlNode definition, string attributeName, ReturnType defaultValue, TracingContext traceContext)
		{
			XmlAttribute xmlAttribute = DefinitionHelperBase.GetXmlAttribute(definition, attributeName, false, traceContext);
			ReturnType result = defaultValue;
			if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Value) && Enum.IsDefined(typeof(ReturnType), xmlAttribute.Value))
			{
				result = (ReturnType)((object)Enum.Parse(typeof(ReturnType), xmlAttribute.Value));
			}
			return result;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00019284 File Offset: 0x00017484
		internal static XmlAttribute GetXmlAttribute(XmlNode definition, string attributeName, bool throwOnFailure, TracingContext traceContext)
		{
			XmlAttribute xmlAttribute = definition.Attributes[attributeName];
			if (xmlAttribute == null)
			{
				string text = string.Format("Attribute {0} was not found in the WorkDefinition xml.", attributeName);
				XmlAttribute xmlAttribute2 = definition.Attributes["Name"];
				if (xmlAttribute2 != null)
				{
					text = string.Format("{0} WorkDefinition name was {1}", text, xmlAttribute2.Value);
				}
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, traceContext, text, null, "GetXmlAttribute", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 345);
				if (throwOnFailure)
				{
					throw new XmlException(text);
				}
			}
			return xmlAttribute;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00019524 File Offset: 0x00017724
		internal static IEnumerable<XmlNode> GetDescendants(XmlNode node, string descendantName)
		{
			string filterString = string.Format("descendant::{0}", descendantName);
			using (XmlNodeList deploymentNodes = node.SelectNodes(filterString))
			{
				foreach (object obj in deploymentNodes)
				{
					XmlNode elementXml = (XmlNode)obj;
					yield return elementXml;
				}
			}
			yield break;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00019770 File Offset: 0x00017970
		internal static IEnumerable<XmlNode> GetDescendantsContainingFilter(XmlNode node, string filter)
		{
			string filterString = string.Format("descendant::*[contains(name(),'{0}')]", filter);
			using (XmlNodeList deploymentNodes = node.SelectNodes(filterString))
			{
				foreach (object obj in deploymentNodes)
				{
					XmlNode elementXml = (XmlNode)obj;
					yield return elementXml;
				}
			}
			yield break;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00019794 File Offset: 0x00017994
		internal static Dictionary<string, string> ConvertExtensionAttributesToDictionary(string extensionAttributes)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrWhiteSpace(extensionAttributes))
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
				xmlReaderSettings.CloseInput = true;
				xmlReaderSettings.IgnoreComments = true;
				xmlReaderSettings.IgnoreProcessingInstructions = true;
				xmlReaderSettings.IgnoreWhitespace = true;
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(extensionAttributes), xmlReaderSettings))
				{
					xmlReader.Read();
					dictionary = new Dictionary<string, string>(xmlReader.AttributeCount);
					for (int i = 0; i < xmlReader.AttributeCount; i++)
					{
						xmlReader.MoveToAttribute(i);
						dictionary.Add(xmlReader.Name, xmlReader.Value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00019844 File Offset: 0x00017A44
		internal virtual void ReadDiscoveryXml()
		{
			this.AssemblyPath = this.WorkItemType.Assembly.Location;
			this.TypeName = this.WorkItemType.FullName;
			this.Name = this.GetMandatoryXmlAttribute<string>("Name");
			this.ComponentName = this.GetMandatoryXmlAttribute<string>("ComponentName");
			this.ServiceName = this.ComponentName;
			this.SetComponentByName(this.ComponentName);
			this.MaxRetryAttempts = this.GetOptionalXmlAttribute<int>("MaxRetryAttempts", 0);
			this.RecurrenceIntervalSeconds = this.GetOptionalXmlAttribute<int>("RecurrenceIntervalSeconds", 0);
			this.TimeoutSeconds = this.GetOptionalXmlAttribute<int>("TimeoutSeconds", 30);
			this.Enabled = this.GetOptionalXmlAttribute<bool>("Enabled", true);
			XmlNode xmlNode = this.DefinitionNode.SelectSingleNode("ExtensionAttributes");
			this.attributes = null;
			this.ExtensionAttributes = null;
			if (xmlNode != null)
			{
				this.ExtensionAttributes = xmlNode.OuterXml;
				this.ParseExtensionAttributes();
			}
			this.TargetPartition = this.GetOptionalXmlAttribute<string>("TargetPartition", null);
			this.TargetGroup = this.GetOptionalXmlAttribute<string>("TargetGroup", null);
			this.TargetResource = this.GetOptionalXmlAttribute<string>("TargetResource", null);
			this.TargetVersion = this.GetOptionalXmlAttribute<string>("TargetVersion", null);
			this.TargetExtension = this.GetOptionalXmlAttribute<string>("TargetExtension", null);
			this.WorkItemVersion = this.GetOptionalXmlAttribute<string>("WorkItemVersion", "Binaries");
			this.DeploymentId = this.GetOptionalXmlAttribute<int>("DeploymentId", 0);
			this.ExecutionLocation = this.GetOptionalXmlAttribute<string>("ExecutionLocation", string.Empty);
			this.CreatedTime = this.GetOptionalXmlAttribute<DateTime>("CreatedTime", DateTime.UtcNow);
			this.StartTime = this.GetOptionalXmlAttribute<DateTime>("StartTime", DateTime.UtcNow);
			this.UpdateTime = this.GetOptionalXmlAttribute<DateTime>("UpdateTime", DateTime.UtcNow);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00019A0C File Offset: 0x00017C0C
		internal virtual string ToString(WorkDefinition workItem)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (workItem != null)
			{
				stringBuilder.AppendLine("AssemblyPath: " + workItem.AssemblyPath);
				stringBuilder.AppendLine("TypeName: " + workItem.TypeName);
				stringBuilder.AppendLine("Name: " + workItem.Name);
				stringBuilder.AppendLine("ServiceName: " + workItem.ServiceName);
				stringBuilder.AppendLine("MaxRetryAttempts: " + workItem.MaxRetryAttempts);
				stringBuilder.AppendLine("RecurrenceIntervalSeconds: " + workItem.RecurrenceIntervalSeconds);
				stringBuilder.AppendLine("TimeoutSeconds: " + workItem.TimeoutSeconds);
				stringBuilder.AppendLine("Enabled: " + workItem.Enabled);
				stringBuilder.AppendLine("ExtensionAttributes: " + workItem.ExtensionAttributes);
				stringBuilder.AppendLine("TargetPartition: " + workItem.TargetPartition);
				stringBuilder.AppendLine("TargetGroup: " + workItem.TargetGroup);
				stringBuilder.AppendLine("TargetResource: " + workItem.TargetResource);
				stringBuilder.AppendLine("TargetVersion: " + workItem.TargetVersion);
				stringBuilder.AppendLine("TargetExtension: " + workItem.TargetExtension);
				stringBuilder.AppendLine("WorkItemVersion: " + workItem.WorkItemVersion);
				stringBuilder.AppendLine("DeploymentId: " + workItem.DeploymentId);
				stringBuilder.AppendLine("ExecutionLocation: " + workItem.ExecutionLocation);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00019BCC File Offset: 0x00017DCC
		internal void ParseExtensionAttributes()
		{
			if (!string.IsNullOrWhiteSpace(this.ExtensionAttributes))
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
				xmlReaderSettings.CloseInput = true;
				xmlReaderSettings.IgnoreComments = true;
				xmlReaderSettings.IgnoreProcessingInstructions = true;
				xmlReaderSettings.IgnoreWhitespace = true;
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(this.ExtensionAttributes), xmlReaderSettings))
				{
					xmlReader.Read();
					this.attributes = new Dictionary<string, string>(xmlReader.AttributeCount);
					for (int i = 0; i < xmlReader.AttributeCount; i++)
					{
						xmlReader.MoveToAttribute(i);
						this.attributes.Add(xmlReader.Name, xmlReader.Value);
					}
				}
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00019C88 File Offset: 0x00017E88
		internal void LogDefinitions(WorkDefinition workItem)
		{
			DefinitionHelperBase definitionHelperBase;
			if (this is ProbeDefinitionHelper)
			{
				definitionHelperBase = (ProbeDefinitionHelper)this;
			}
			else if (this is MonitorDefinitionHelper)
			{
				definitionHelperBase = (MonitorDefinitionHelper)this;
			}
			else
			{
				definitionHelperBase = (ResponderDefinitionHelper)this;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("The following are the settings defined in the XML:");
			stringBuilder.AppendLine(definitionHelperBase.ToString());
			stringBuilder.AppendLine("The following are the actual settings of the work item created:");
			stringBuilder.AppendLine(definitionHelperBase.ToString(workItem));
			WTFDiagnostics.TraceInformation(ExTraceGlobals.GenericHelperTracer, this.TraceContext, stringBuilder.ToString(), null, "LogDefinitions", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 578);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00019D1D File Offset: 0x00017F1D
		protected ReturnType GetMandatoryXmlAttribute<ReturnType>(string attributeName)
		{
			return DefinitionHelperBase.GetMandatoryXmlAttribute<ReturnType>(this.DefinitionNode, attributeName, this.TraceContext);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00019D31 File Offset: 0x00017F31
		protected ReturnType GetOptionalXmlAttribute<ReturnType>(string attributeName, ReturnType defaultValue)
		{
			return DefinitionHelperBase.GetOptionalXmlAttribute<ReturnType>(this.DefinitionNode, attributeName, defaultValue, this.TraceContext);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00019D46 File Offset: 0x00017F46
		protected ReturnType GetOptionalXmlEnumAttribute<ReturnType>(string attributeName, ReturnType defaultValue)
		{
			return DefinitionHelperBase.GetOptionalXmlEnumAttribute<ReturnType>(this.DefinitionNode, attributeName, defaultValue, this.TraceContext);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00019D5C File Offset: 0x00017F5C
		protected ReturnType GetMandatoryValue<ReturnType>(string key)
		{
			string dictionaryValue = this.GetDictionaryValue(key, true);
			return (ReturnType)((object)Convert.ChangeType(dictionaryValue, typeof(ReturnType)));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00019D88 File Offset: 0x00017F88
		protected ReturnType GetOptionalValue<ReturnType>(string key, ReturnType defaultValue)
		{
			ReturnType result = default(ReturnType);
			string dictionaryValue = this.GetDictionaryValue(key, false);
			if (string.IsNullOrWhiteSpace(dictionaryValue))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, this.TraceContext, string.Format("Using default value of {0}", defaultValue), null, "GetOptionalValue", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 645);
				result = defaultValue;
			}
			else
			{
				result = (ReturnType)((object)Convert.ChangeType(dictionaryValue, typeof(ReturnType)));
			}
			return result;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00019DFC File Offset: 0x00017FFC
		protected ReturnType GetMandatoryEnumValue<ReturnType>(string key)
		{
			string dictionaryValue = this.GetDictionaryValue(key, true);
			return (ReturnType)((object)Enum.Parse(typeof(ReturnType), dictionaryValue));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00019E28 File Offset: 0x00018028
		protected ReturnType GetOptionalEnumValue<ReturnType>(string key, ReturnType defaultValue)
		{
			ReturnType result = default(ReturnType);
			string dictionaryValue = this.GetDictionaryValue(key, false);
			if (string.IsNullOrWhiteSpace(dictionaryValue))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.GenericHelperTracer, this.TraceContext, string.Format("Using default value of {0}", defaultValue), null, "GetOptionalEnumValue", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 687);
				result = defaultValue;
			}
			else
			{
				result = (ReturnType)((object)Enum.Parse(typeof(ReturnType), dictionaryValue));
			}
			return result;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00019E9C File Offset: 0x0001809C
		protected string GetDictionaryValue(string key, bool mandatory)
		{
			if (this.attributes == null)
			{
				if (mandatory)
				{
					string message = string.Format("The ExtensionAttributes node cannot be missing or empty.", new object[0]);
					WTFDiagnostics.TraceError(ExTraceGlobals.GenericHelperTracer, this.TraceContext, message, null, "GetDictionaryValue", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 711);
					throw new XmlException(message);
				}
				return null;
			}
			else if (!this.attributes.ContainsKey(key))
			{
				if (mandatory)
				{
					string message2 = string.Format("The ExtensionAttributes does not contain key '{0}'.", key);
					WTFDiagnostics.TraceError(ExTraceGlobals.GenericHelperTracer, this.TraceContext, message2, null, "GetDictionaryValue", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 725);
					throw new XmlException(message2);
				}
				return null;
			}
			else
			{
				string text = this.attributes[key];
				if (string.IsNullOrWhiteSpace(text) && mandatory)
				{
					string message3 = string.Format("The value of key '{0}' in the ExtensionAttributes cannot be null.", key);
					WTFDiagnostics.TraceError(ExTraceGlobals.GenericHelperTracer, this.TraceContext, message3, null, "GetDictionaryValue", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\GenericWorkItemHelper\\DefinitionHelperBase.cs", 739);
					throw new XmlException(message3);
				}
				return text;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00019F84 File Offset: 0x00018184
		protected void CreateBaseWorkDefinition(WorkDefinition workitem)
		{
			workitem.AssemblyPath = this.AssemblyPath;
			workitem.TypeName = this.TypeName;
			workitem.Name = this.Name;
			workitem.ServiceName = this.ServiceName;
			workitem.RecurrenceIntervalSeconds = this.RecurrenceIntervalSeconds;
			workitem.TimeoutSeconds = this.TimeoutSeconds;
			workitem.MaxRetryAttempts = this.MaxRetryAttempts;
			workitem.Enabled = this.Enabled;
			workitem.TargetResource = this.TargetResource;
			workitem.TargetGroup = this.TargetGroup;
			workitem.TargetPartition = this.TargetPartition;
			workitem.TargetExtension = this.TargetExtension;
			workitem.TargetVersion = this.TargetVersion;
			workitem.ExtensionAttributes = this.ExtensionAttributes;
			workitem.ParseExtensionAttributes(false);
		}

		// Token: 0x040002C7 RID: 711
		private Dictionary<string, string> attributes;
	}
}
