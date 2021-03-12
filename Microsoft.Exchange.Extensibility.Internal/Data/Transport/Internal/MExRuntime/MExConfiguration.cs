using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000088 RID: 136
	internal sealed class MExConfiguration
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00013A24 File Offset: 0x00011C24
		static MExConfiguration()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			MExConfiguration.Schemas = new XmlSchemaSet();
			MExConfiguration.InternalSchemas = new XmlSchemaSet();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("MExRuntimeConfig.xsd"))
			{
				MExConfiguration.Schemas.Add(null, SafeXmlFactory.CreateSafeXmlTextReader(manifestResourceStream));
				manifestResourceStream.Position = 0L;
				MExConfiguration.InternalSchemas.Add(null, SafeXmlFactory.CreateSafeXmlTextReader(manifestResourceStream));
			}
			using (Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream("InternalMExRuntimeConfig.xsd"))
			{
				MExConfiguration.InternalSchemas.Add(null, SafeXmlFactory.CreateSafeXmlTextReader(manifestResourceStream2));
			}
			MExConfiguration.Schemas.Compile();
			MExConfiguration.InternalSchemas.Compile();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00013AEC File Offset: 0x00011CEC
		internal MExConfiguration(ProcessTransportRole transportProcessRole, string installPath) : this(MExConfiguration.GetExchangeSku(), transportProcessRole, installPath)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00013AFB File Offset: 0x00011CFB
		internal MExConfiguration(Datacenter.ExchangeSku exchangeSku, ProcessTransportRole transportProcessRole, string installPath)
		{
			this.monitoringOptions = new MonitoringOptions();
			this.agentList = new List<AgentInfo>();
			this.exchangeSku = exchangeSku;
			this.transportProcessRole = transportProcessRole;
			this.installPath = installPath;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00013B35 File Offset: 0x00011D35
		internal MonitoringOptions MonitoringOptions
		{
			get
			{
				return this.monitoringOptions;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00013B3D File Offset: 0x00011D3D
		internal IList<AgentInfo> AgentList
		{
			get
			{
				return this.agentList;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00013B45 File Offset: 0x00011D45
		internal bool DisposeAgents
		{
			get
			{
				return this.disposeAgents;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013B60 File Offset: 0x00011D60
		internal static bool ValidateFile(string filePath)
		{
			bool isValid = true;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = MExConfiguration.Schemas;
			xmlReaderSettings.ValidationEventHandler += delegate(object param0, ValidationEventArgs param1)
			{
				isValid = false;
			};
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(filePath, xmlReaderSettings))
				{
					while (xmlReader.Read())
					{
					}
				}
			}
			catch (IOException)
			{
				isValid = false;
			}
			catch (UnauthorizedAccessException)
			{
				isValid = false;
			}
			return isValid;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013C14 File Offset: 0x00011E14
		internal List<AgentInfo> GetPublicAgentList()
		{
			List<AgentInfo> list = new List<AgentInfo>();
			foreach (AgentInfo agentInfo in this.agentList)
			{
				if (!agentInfo.IsInternal)
				{
					list.Add(agentInfo);
				}
			}
			return list;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013C78 File Offset: 0x00011E78
		internal List<AgentInfo> GetPreExecutionInternalAgents()
		{
			List<AgentInfo> list = new List<AgentInfo>();
			foreach (AgentInfo agentInfo in this.agentList)
			{
				if (!agentInfo.IsInternal)
				{
					break;
				}
				list.Add(agentInfo);
			}
			return list;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013CF0 File Offset: 0x00011EF0
		internal bool Validate()
		{
			XmlDocument xmlDocument = this.CreateXmlDocument();
			xmlDocument.Schemas = MExConfiguration.Schemas;
			bool isValid = true;
			xmlDocument.Validate(delegate(object param0, ValidationEventArgs param1)
			{
				isValid = false;
			});
			return isValid;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013D34 File Offset: 0x00011F34
		internal void Load(string filePath)
		{
			if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.MissingConfigurationFile(filePath), new FileNotFoundException());
			}
			List<AgentInfo> preExecutionInternalAgents;
			List<AgentInfo> postExecutionInternalAgents;
			this.LoadInternalAgents(out preExecutionInternalAgents, out postExecutionInternalAgents);
			List<AgentInfo> publicAgents;
			this.LoadPublicAgents(filePath, out publicAgents);
			this.agentList = this.CreateFinalAgentsList(publicAgents, preExecutionInternalAgents, postExecutionInternalAgents);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013D84 File Offset: 0x00011F84
		internal AgentInfo[] GetEnabledAgentsByType(string type)
		{
			return this.GetEnabledAgentsByType(type, false);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013D8E File Offset: 0x00011F8E
		internal AgentInfo[] GetEnaledPublicAgentsByType(string type)
		{
			return this.GetEnabledAgentsByType(type, true);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013DAC File Offset: 0x00011FAC
		internal void Save(string filePath)
		{
			XmlDocument xmlDocument = this.CreateXmlDocument();
			xmlDocument.Schemas = MExConfiguration.Schemas;
			xmlDocument.Validate(delegate(object sender, ValidationEventArgs args)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfiguration, args.Exception);
			});
			int num = 20;
			try
			{
				IL_38:
				using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					xmlDocument.Save(fileStream);
				}
			}
			catch (XmlException innerException)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException);
			}
			catch (UnauthorizedAccessException innerException2)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException2);
			}
			catch (IOException innerException3)
			{
				if (num <= 0)
				{
					throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException3);
				}
				num--;
				Thread.Sleep(50);
				goto IL_38;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013E84 File Offset: 0x00012084
		private static Datacenter.ExchangeSku GetExchangeSku()
		{
			Datacenter.ExchangeSku result;
			try
			{
				if (Datacenter.IsForefrontForOfficeDatacenter())
				{
					result = Datacenter.ExchangeSku.ForefrontForOfficeDatacenter;
				}
				else
				{
					result = Datacenter.GetExchangeSku();
				}
			}
			catch (CannotDetermineExchangeModeException innerException)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.FailedToReadDataCenterMode, innerException);
			}
			return result;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013EC4 File Offset: 0x000120C4
		private List<AgentInfo> CreateFinalAgentsList(List<AgentInfo> publicAgents, List<AgentInfo> preExecutionInternalAgents, List<AgentInfo> postExecutionInternalAgents)
		{
			List<AgentInfo> list = new List<AgentInfo>();
			list.AddRange(preExecutionInternalAgents);
			list.AddRange(publicAgents);
			list.AddRange(postExecutionInternalAgents);
			return list;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013F10 File Offset: 0x00012110
		private void LoadPublicAgents(string filePath, out List<AgentInfo> publicAgents)
		{
			int num = 20;
			if (string.IsNullOrEmpty(filePath))
			{
				publicAgents = new List<AgentInfo>();
				return;
			}
			for (;;)
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				xmlDocument.Schemas = MExConfiguration.Schemas;
				try
				{
					using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						xmlDocument.Load(fileStream);
						xmlDocument.Validate(delegate(object sender, ValidationEventArgs args)
						{
							throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), args.Exception);
						});
						this.LoadSettings(xmlDocument.SelectSingleNode("/configuration/mexRuntime/settings"));
						this.LoadMonitoringOptions(xmlDocument.SelectSingleNode("/configuration/mexRuntime/monitoring"));
						publicAgents = this.LoadAgentList(xmlDocument.SelectSingleNode("/configuration/mexRuntime/agentList"), false);
					}
				}
				catch (XmlException innerException)
				{
					throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException);
				}
				catch (FormatException innerException2)
				{
					throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException2);
				}
				catch (UnauthorizedAccessException innerException3)
				{
					throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException3);
				}
				catch (IOException innerException4)
				{
					if (num <= 0)
					{
						throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile(filePath), innerException4);
					}
					num--;
					Thread.Sleep(50);
					continue;
				}
				break;
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00014078 File Offset: 0x00012278
		private void LoadSettings(XmlNode node)
		{
			if (node == null)
			{
				return;
			}
			foreach (object obj in node)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "disposeAgents")
				{
					this.disposeAgents = bool.Parse(xmlNode.InnerText);
				}
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00014104 File Offset: 0x00012304
		private void LoadInternalAgents(out List<AgentInfo> preExecutionInternalAgents, out List<AgentInfo> postExecutionInternalAgents)
		{
			preExecutionInternalAgents = new List<AgentInfo>();
			postExecutionInternalAgents = new List<AgentInfo>();
			try
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				xmlDocument.Schemas = MExConfiguration.InternalSchemas;
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("internalAgents.config"))
				{
					xmlDocument.Load(manifestResourceStream);
					xmlDocument.Validate(delegate(object sender, ValidationEventArgs args)
					{
						throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile("internalAgents.config"), args.Exception);
					});
					string xmlMarkups = this.GetXmlMarkups(this.exchangeSku, this.transportProcessRole);
					if (xmlMarkups != null)
					{
						preExecutionInternalAgents = this.LoadAgentList(xmlDocument.SelectSingleNode("/internalConfiguration/internalMexRuntime/" + xmlMarkups + "/preExecution"), true);
						postExecutionInternalAgents = this.LoadAgentList(xmlDocument.SelectSingleNode("/internalConfiguration/internalMexRuntime/" + xmlMarkups + "/postExecution"), true);
					}
				}
			}
			catch (XmlException innerException)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile("internalAgents.config"), innerException);
			}
			catch (UnauthorizedAccessException innerException2)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile("internalAgents.config"), innerException2);
			}
			catch (IOException innerException3)
			{
				throw new ExchangeConfigurationException(MExRuntimeStrings.InvalidConfigurationFile("internalAgents.config"), innerException3);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014240 File Offset: 0x00012440
		private string GetXmlMarkups(Datacenter.ExchangeSku exchangeSku, ProcessTransportRole transportProcessRole)
		{
			string result = null;
			switch (exchangeSku)
			{
			case Datacenter.ExchangeSku.Enterprise:
			case Datacenter.ExchangeSku.DatacenterDedicated:
				switch (transportProcessRole)
				{
				case ProcessTransportRole.Hub:
					result = "enterpriseBridgehead";
					break;
				case ProcessTransportRole.Edge:
					result = "enterpriseGateway";
					break;
				case ProcessTransportRole.FrontEnd:
					result = "enterpriseFrontend";
					break;
				case ProcessTransportRole.MailboxSubmission:
					result = "enterpriseMailboxSubmission";
					break;
				case ProcessTransportRole.MailboxDelivery:
					result = "enterpriseMailboxDelivery";
					break;
				}
				break;
			case Datacenter.ExchangeSku.ExchangeDatacenter:
				switch (transportProcessRole)
				{
				case ProcessTransportRole.Hub:
					result = "exchangeDatacenterBridgehead";
					break;
				case ProcessTransportRole.FrontEnd:
					result = "exchangeDatacenterFrontend";
					break;
				case ProcessTransportRole.MailboxSubmission:
					result = "exchangeDatacenterMailboxSubmission";
					break;
				case ProcessTransportRole.MailboxDelivery:
					result = "exchangeDatacenterMailboxDelivery";
					break;
				}
				break;
			case Datacenter.ExchangeSku.PartnerHosted:
				switch (transportProcessRole)
				{
				case ProcessTransportRole.Hub:
					result = "partnerHostedBridgehead";
					break;
				case ProcessTransportRole.Edge:
					result = "partnerHostedGateway";
					break;
				case ProcessTransportRole.MailboxSubmission:
					result = "partnerHostedMailboxSubmission";
					break;
				case ProcessTransportRole.MailboxDelivery:
					result = "partnerHostedMailboxDelivery";
					break;
				}
				break;
			case Datacenter.ExchangeSku.ForefrontForOfficeDatacenter:
				switch (transportProcessRole)
				{
				case ProcessTransportRole.Hub:
					result = "forefrontForOfficeBridgehead";
					break;
				case ProcessTransportRole.FrontEnd:
					result = "forefrontForOfficeFrontend";
					break;
				}
				break;
			}
			return result;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014370 File Offset: 0x00012570
		private AgentInfo[] GetEnabledAgentsByType(string type, bool onlyPublic)
		{
			List<AgentInfo> list = new List<AgentInfo>();
			foreach (AgentInfo agentInfo in this.agentList)
			{
				if (agentInfo.Enabled && agentInfo.BaseTypeName == type && (!onlyPublic || !agentInfo.IsInternal))
				{
					list.Add(agentInfo);
				}
			}
			AgentInfo[] array = new AgentInfo[list.Count];
			list.CopyTo(array);
			return array;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014400 File Offset: 0x00012600
		private void LoadMonitoringOptions(XmlNode node)
		{
			foreach (object obj in node)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "agentExecution")
				{
					this.monitoringOptions.AgentExecutionLimitInMilliseconds = int.Parse(xmlNode.Attributes.GetNamedItem("timeLimitInMilliseconds").Value, CultureInfo.InvariantCulture);
				}
				else if (xmlNode.Name == "messageSnapshot")
				{
					this.monitoringOptions.MessageSnapshotEnabled = bool.Parse(xmlNode.Attributes.GetNamedItem("enabled").Value);
				}
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000144C8 File Offset: 0x000126C8
		private List<AgentInfo> LoadAgentList(XmlNode node, bool isInternal)
		{
			List<AgentInfo> list = new List<AgentInfo>();
			foreach (object obj in node)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode.Name != "agent"))
				{
					XmlAttributeCollection attributes = xmlNode.Attributes;
					string value = attributes.GetNamedItem("assemblyPath").Value;
					string path = isInternal ? Path.Combine(this.installPath, value) : value;
					list.Add(new AgentInfo(attributes.GetNamedItem("name").Value, attributes.GetNamedItem("baseType").Value, attributes.GetNamedItem("classFactory").Value, path, bool.Parse(attributes.GetNamedItem("enabled").Value), isInternal));
				}
			}
			return list;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000145BC File Offset: 0x000127BC
		private XmlDocument CreateXmlDocument()
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.InsertBefore(xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null), xmlDocument.DocumentElement);
			xmlDocument.AppendChild(xmlDocument.CreateElement("configuration"));
			XmlElement xmlElement = xmlDocument.CreateElement("mexRuntime");
			xmlDocument.DocumentElement.PrependChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("monitoring");
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlDocument.CreateElement("agentExecution");
			xmlElement3.SetAttribute("timeLimitInMilliseconds", this.monitoringOptions.AgentExecutionLimitInMilliseconds.ToString(CultureInfo.InvariantCulture));
			xmlElement2.AppendChild(xmlElement3);
			if (!this.monitoringOptions.MessageSnapshotEnabled)
			{
				XmlElement xmlElement4 = xmlDocument.CreateElement("messageSnapshot");
				xmlElement4.SetAttribute("enabled", "false");
				xmlElement2.AppendChild(xmlElement4);
			}
			XmlElement xmlElement5 = xmlDocument.CreateElement("agentList");
			xmlElement.AppendChild(xmlElement5);
			foreach (AgentInfo agentInfo in this.agentList)
			{
				if (!agentInfo.IsInternal)
				{
					XmlElement xmlElement6 = xmlDocument.CreateElement("agent");
					xmlElement6.SetAttribute("name", agentInfo.AgentName);
					xmlElement6.SetAttribute("baseType", agentInfo.BaseTypeName);
					xmlElement6.SetAttribute("classFactory", agentInfo.FactoryTypeName);
					xmlElement6.SetAttribute("assemblyPath", agentInfo.FactoryAssemblyPath);
					xmlElement6.SetAttribute("enabled", agentInfo.Enabled.ToString().ToLower(CultureInfo.InvariantCulture));
					xmlElement5.AppendChild(xmlElement6);
				}
			}
			XmlElement xmlElement7 = xmlDocument.CreateElement("settings");
			if (!this.disposeAgents)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("disposeAgents");
				xmlElement8.InnerText = "false";
				xmlElement7.AppendChild(xmlElement8);
			}
			xmlElement.AppendChild(xmlElement7);
			return xmlDocument;
		}

		// Token: 0x0400049A RID: 1178
		private static readonly XmlSchemaSet Schemas;

		// Token: 0x0400049B RID: 1179
		private static readonly XmlSchemaSet InternalSchemas;

		// Token: 0x0400049C RID: 1180
		private MonitoringOptions monitoringOptions;

		// Token: 0x0400049D RID: 1181
		private List<AgentInfo> agentList;

		// Token: 0x0400049E RID: 1182
		private Datacenter.ExchangeSku exchangeSku;

		// Token: 0x0400049F RID: 1183
		private ProcessTransportRole transportProcessRole;

		// Token: 0x040004A0 RID: 1184
		private string installPath;

		// Token: 0x040004A1 RID: 1185
		private bool disposeAgents = true;

		// Token: 0x02000089 RID: 137
		private static class InternalAgentConstants
		{
			// Token: 0x040004A4 RID: 1188
			public const string ExchangeDatacenterHubTransport = "exchangeDatacenterBridgehead";

			// Token: 0x040004A5 RID: 1189
			public const string PartnerHostedHubTransport = "partnerHostedBridgehead";

			// Token: 0x040004A6 RID: 1190
			public const string PartnerHostedGateway = "partnerHostedGateway";

			// Token: 0x040004A7 RID: 1191
			public const string EnterpriseHubTransport = "enterpriseBridgehead";

			// Token: 0x040004A8 RID: 1192
			public const string EnterpriseGateway = "enterpriseGateway";

			// Token: 0x040004A9 RID: 1193
			public const string ForefrontForOfficeFrontend = "forefrontForOfficeFrontend";

			// Token: 0x040004AA RID: 1194
			public const string ForefrontForOfficeBridgehead = "forefrontForOfficeBridgehead";

			// Token: 0x040004AB RID: 1195
			public const string ExchangeDatacenterFrontend = "exchangeDatacenterFrontend";

			// Token: 0x040004AC RID: 1196
			public const string EnterpriseFrontend = "enterpriseFrontend";

			// Token: 0x040004AD RID: 1197
			public const string ExchangeDatacenterMailboxSubmission = "exchangeDatacenterMailboxSubmission";

			// Token: 0x040004AE RID: 1198
			public const string PartnerHostedMailboxSubmission = "partnerHostedMailboxSubmission";

			// Token: 0x040004AF RID: 1199
			public const string EnterpriseMailboxSubmission = "enterpriseMailboxSubmission";

			// Token: 0x040004B0 RID: 1200
			public const string ExchangeDatacenterMailboxDelivery = "exchangeDatacenterMailboxDelivery";

			// Token: 0x040004B1 RID: 1201
			public const string PartnerHostedMailboxDelivery = "partnerHostedMailboxDelivery";

			// Token: 0x040004B2 RID: 1202
			public const string EnterpriseMailboxDelivery = "enterpriseMailboxDelivery";

			// Token: 0x040004B3 RID: 1203
			public const string InternalAgentsConfigurationFileName = "internalAgents.config";
		}
	}
}
