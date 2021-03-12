using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Providers;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingWebService.PowerShell;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000033 RID: 51
	internal class ReportingSchema
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00005F76 File Offset: 0x00004176
		internal ReportingSchema(string schemaFilePath, string version)
		{
			this.Initialize(schemaFilePath);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005F9B File Offset: 0x0000419B
		private ReportingSchema(string version)
		{
			this.defaultConfigPath = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess\\ReportingWebService");
			this.Initialize(this.GetConfigPath(version));
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005FDB File Offset: 0x000041DB
		public Dictionary<string, IEntity> Entities
		{
			get
			{
				return this.entities;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005FE3 File Offset: 0x000041E3
		public Dictionary<string, ResourceType> ComplexTypeResourceTypes
		{
			get
			{
				return this.complexTypeResourceTypes;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005FEB File Offset: 0x000041EB
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00005FF3 File Offset: 0x000041F3
		public List<RoleEntry> CmdletFilter { get; private set; }

		// Token: 0x06000123 RID: 291 RVA: 0x0000602C File Offset: 0x0000422C
		public static ReportingSchema GetCurrentReportingSchema(HttpContext httpContext)
		{
			ReportingSchema schema = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.GetReportingSchemaLatency, delegate
			{
				string currentReportingVersion = ReportingVersion.GetCurrentReportingVersion(httpContext);
				schema = ReportingSchema.GetReportingSchema(currentReportingVersion);
			});
			return schema;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006100 File Offset: 0x00004300
		public static ReportingSchema GetReportingSchema(string version)
		{
			ReportingSchema schema = null;
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.GetReportingSchemaLatency, delegate
			{
				if (!ReportingSchema.SchemaDictionary.TryGetValue(version, out schema))
				{
					lock (ReportingSchema.SyncRoot)
					{
						if (!ReportingSchema.SchemaDictionary.TryGetValue(version, out schema))
						{
							schema = new ReportingSchema(version);
							ReportingSchema.SchemaDictionary[version] = schema;
						}
					}
				}
			});
			return schema;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000613C File Offset: 0x0000433C
		public static bool IsNullableType(Type type, out Type underlyingType)
		{
			underlyingType = null;
			bool flag = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
			if (flag)
			{
				NullableConverter nullableConverter = new NullableConverter(type);
				underlyingType = nullableConverter.UnderlyingType;
			}
			return flag;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006180 File Offset: 0x00004380
		public static void CheckCondition(bool condition, string error)
		{
			if (!condition)
			{
				throw new ReportingSchema.SchemaLoadException(error);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000618C File Offset: 0x0000438C
		public static XmlNode SelectSingleNode(XmlNode node, string xpath)
		{
			XmlNode result;
			using (XmlNodeList xmlNodeList = node.SelectNodes(xpath))
			{
				ReportingSchema.CheckCondition(xmlNodeList.Count == 1, string.Format("expect only one {0}.", xpath));
				result = xmlNodeList[0];
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000061E0 File Offset: 0x000043E0
		private void Initialize(string schemaFilePath)
		{
			this.LoadConfigFile(schemaFilePath);
			this.GenerateComplexTypesSchemaForEntities();
			this.CmdletFilter = this.GetCmdletFilter();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000061FC File Offset: 0x000043FC
		private void LoadConfigFile(string configPath)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			if (!File.Exists(configPath))
			{
				ReportingWebServiceEventLogConstants.Tuple_LoadReportingschemaFailed.LogEvent(new object[]
				{
					Strings.ErrorSchemaConfigurationFileMissing(configPath)
				});
				ServiceDiagnostics.ThrowError(ReportingErrorCode.ErrorSchemaInitializationFail, Strings.ErrorSchemaInitializationFail);
			}
			safeXmlDocument.Load(configPath);
			try
			{
				Dictionary<string, ReportingSchema.ReportPropertyCmdletParamMapping[]> reportPropertyCmdletParamMapping = this.LoadRbacMapping(safeXmlDocument);
				this.LoadEntityNodes(safeXmlDocument, reportPropertyCmdletParamMapping);
			}
			catch (ReportingSchema.SchemaLoadException ex)
			{
				ReportingWebServiceEventLogConstants.Tuple_LoadReportingschemaFailed.LogEvent(new object[]
				{
					ex.Message
				});
				ServiceDiagnostics.ThrowError(ReportingErrorCode.ErrorSchemaInitializationFail, Strings.ErrorSchemaInitializationFail, ex);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000062A4 File Offset: 0x000044A4
		private Dictionary<string, ReportingSchema.ReportPropertyCmdletParamMapping[]> LoadRbacMapping(SafeXmlDocument doc)
		{
			Dictionary<string, ReportingSchema.ReportPropertyCmdletParamMapping[]> result;
			using (XmlNodeList xmlNodeList = doc.SelectNodes("/Configuration/CmdletParameterMappings/CmdletParameterMapping"))
			{
				Dictionary<string, ReportingSchema.ReportPropertyCmdletParamMapping[]> dictionary = new Dictionary<string, ReportingSchema.ReportPropertyCmdletParamMapping[]>(xmlNodeList.Count);
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					using (XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("Cmdlet"))
					{
						using (XmlNodeList xmlNodeList3 = xmlNode.SelectNodes("Mappings/Mapping"))
						{
							ReportingSchema.CheckCondition(xmlNodeList2.Count == 1, "There must be one and only one Cmdlet node under Rbac node.");
							string key = xmlNodeList2[0].Attributes["Name"].Value.Trim();
							ReportingSchema.CheckCondition(!dictionary.ContainsKey(key), "There shouldn't be duplicate Cmdlet under Rbac node.");
							ReportingSchema.CheckCondition(xmlNodeList3.Count > 0, "The mapping shouldn't be empty.");
							ReportingSchema.ReportPropertyCmdletParamMapping[] array = new ReportingSchema.ReportPropertyCmdletParamMapping[xmlNodeList3.Count];
							int num = 0;
							foreach (object obj2 in xmlNodeList3)
							{
								XmlNode xmlNode2 = (XmlNode)obj2;
								array[num++] = new ReportingSchema.ReportPropertyCmdletParamMapping(xmlNode2.Attributes["CmdletParameter"].Value.Trim(), xmlNode2.Attributes["ReportObjectProperty"].Value.Trim());
							}
							dictionary.Add(key, array);
						}
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000064C0 File Offset: 0x000046C0
		private void LoadEntityNodes(SafeXmlDocument doc, Dictionary<string, ReportingSchema.ReportPropertyCmdletParamMapping[]> reportPropertyCmdletParamMapping)
		{
			using (XmlNodeList xmlNodeList = doc.SelectNodes("/Configuration/Reports/Report"))
			{
				HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					ReportingSchema.CheckCondition(!string.IsNullOrWhiteSpace(xmlNode.Attributes["Name"].Value) && !string.IsNullOrWhiteSpace(xmlNode.Attributes["Snapin"].Value) && !string.IsNullOrWhiteSpace(xmlNode.Attributes["Cmdlet"].Value), string.Format("Attributes {0}, {1}, {2} of entity should not be empty.", "Name", "Cmdlet", "Snapin"));
					hashSet.Add(xmlNode.Attributes["Snapin"].Value.Trim());
				}
				using (IPSCommandResolver ipscommandResolver = DependencyFactory.CreatePSCommandResolver(hashSet))
				{
					foreach (object obj2 in xmlNodeList)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						string text = xmlNode2.Attributes["Name"].Value.Trim();
						string text2 = xmlNode2.Attributes["Cmdlet"].Value.Trim();
						string snapinName = xmlNode2.Attributes["Snapin"].Value.Trim();
						ReportingSchema.CheckCondition(!this.entities.ContainsKey(text), "Duplicate entity in the config file");
						Dictionary<string, string> dictionary = null;
						using (XmlNodeList xmlNodeList2 = xmlNode2.SelectNodes("CmdletParameters/CmdletParameter"))
						{
							if (xmlNodeList2.Count > 0)
							{
								dictionary = new Dictionary<string, string>(xmlNodeList2.Count);
								foreach (object obj3 in xmlNodeList2)
								{
									XmlNode xmlNode3 = (XmlNode)obj3;
									ReportingSchema.CheckCondition(!string.IsNullOrWhiteSpace(xmlNode3.Attributes["Name"].Value) && !string.IsNullOrWhiteSpace(xmlNode3.Attributes["Value"].Value), "cmdlet parameter name and value should not be empty.");
									string key = xmlNode3.Attributes["Name"].Value.Trim();
									string value = xmlNode3.Attributes["Value"].Value.Trim();
									dictionary.Add(key, value);
								}
							}
						}
						Dictionary<string, List<string>> dictionary2 = null;
						if (reportPropertyCmdletParamMapping.ContainsKey(text2))
						{
							dictionary2 = new Dictionary<string, List<string>>(reportPropertyCmdletParamMapping[text2].Length);
							foreach (ReportingSchema.ReportPropertyCmdletParamMapping reportPropertyCmdletParamMapping2 in reportPropertyCmdletParamMapping[text2])
							{
								if (!dictionary2.ContainsKey(reportPropertyCmdletParamMapping2.ReportObjectProperty))
								{
									dictionary2.Add(reportPropertyCmdletParamMapping2.ReportObjectProperty, new List<string>());
								}
								dictionary2[reportPropertyCmdletParamMapping2.ReportObjectProperty].Add(reportPropertyCmdletParamMapping2.CmdletParameter);
							}
						}
						XmlNode annotationNode = ReportingSchema.SelectSingleNode(xmlNode2, "Annotation");
						IReportAnnotation annotation = DependencyFactory.CreateReportAnnotation(annotationNode);
						IEntity entity = DependencyFactory.CreateEntity(text, new TaskInvocationInfo(text2, snapinName, dictionary), dictionary2, annotation);
						entity.Initialize(ipscommandResolver);
						this.entities.Add(text, entity);
					}
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000068F0 File Offset: 0x00004AF0
		private void GenerateComplexTypesSchemaForEntities()
		{
			foreach (KeyValuePair<string, IEntity> keyValuePair in this.entities)
			{
				if (ResourceType.GetPrimitiveResourceType(keyValuePair.Value.ClrType) == null)
				{
					this.GenerateComplexTypeSchema(keyValuePair.Value.ClrType, 0);
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006964 File Offset: 0x00004B64
		private ResourceType GenerateComplexTypeSchema(Type clrType, ResourceTypeKind resourceTypeKind)
		{
			if (this.complexTypeResourceTypes.ContainsKey(clrType.FullName))
			{
				return this.complexTypeResourceTypes[clrType.FullName];
			}
			ResourceType resourceType = new ResourceType(clrType, resourceTypeKind, null, "TenantReporting", clrType.Name, false);
			foreach (PropertyInfo propertyInfo in clrType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy))
			{
				Type type = propertyInfo.PropertyType;
				Type type2;
				if (ReportingSchema.IsNullableType(type, out type2))
				{
					type = type2;
				}
				ResourcePropertyKind resourcePropertyKind = 1;
				ResourceType resourceType2 = ResourceType.GetPrimitiveResourceType(type);
				if (resourceType2 == null)
				{
					if (type.IsEnum || type.IsValueType)
					{
						throw new NotSupportedException("struct and enum are not supported. For struct, try to change it to class. For enum, try to change it to integer or string.");
					}
					if (type.Equals(clrType))
					{
						resourceType2 = resourceType;
					}
					else
					{
						resourceType2 = this.GenerateComplexTypeSchema(type, 1);
					}
					resourcePropertyKind = 4;
				}
				resourceType.AddProperty(new ResourceProperty(propertyInfo.Name, resourcePropertyKind, resourceType2));
			}
			if (resourceTypeKind == 1)
			{
				this.complexTypeResourceTypes.Add(clrType.FullName, resourceType);
			}
			return resourceType;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006A58 File Offset: 0x00004C58
		private string GetConfigPath(string version)
		{
			string path = this.defaultConfigPath;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange Control Panel", false))
			{
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("TenantReportingSchemaPath");
					if (!string.IsNullOrWhiteSpace(text))
					{
						path = text;
					}
				}
			}
			string path2 = string.Format("ReportingSchema_{0}.xml", version);
			return Path.Combine(path, path2);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006AD0 File Offset: 0x00004CD0
		private List<RoleEntry> GetCmdletFilter()
		{
			Dictionary<string, RoleEntry> dictionary = new Dictionary<string, RoleEntry>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, IEntity> keyValuePair in this.Entities)
			{
				string cmdletName = keyValuePair.Value.TaskInvocationInfo.CmdletName;
				if (!dictionary.ContainsKey(cmdletName))
				{
					CmdletRoleEntry value = new CmdletRoleEntry(cmdletName, keyValuePair.Value.TaskInvocationInfo.SnapinName, null);
					dictionary.Add(cmdletName, value);
				}
			}
			List<RoleEntry> list = dictionary.Values.ToList<RoleEntry>();
			list.Sort(RoleEntry.NameComparer);
			return list;
		}

		// Token: 0x04000086 RID: 134
		public const string ECPRegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange Control Panel";

		// Token: 0x04000087 RID: 135
		public const string SchemaPathRegistryValueName = "TenantReportingSchemaPath";

		// Token: 0x04000088 RID: 136
		public const string ContainerNamespace = "TenantReporting";

		// Token: 0x04000089 RID: 137
		private const string EntityNodePath = "/Configuration/Reports/Report";

		// Token: 0x0400008A RID: 138
		private const string CmdletParameterMappingNodePath = "/Configuration/CmdletParameterMappings/CmdletParameterMapping";

		// Token: 0x0400008B RID: 139
		private const string EntityNameAttributeName = "Name";

		// Token: 0x0400008C RID: 140
		private const string EntityCmdletAttributeName = "Cmdlet";

		// Token: 0x0400008D RID: 141
		private const string EntitySnapinAttributeName = "Snapin";

		// Token: 0x0400008E RID: 142
		private const string CmdletParameterNodeRelativePath = "CmdletParameters/CmdletParameter";

		// Token: 0x0400008F RID: 143
		private const string CmdletParameterNameAttributeName = "Name";

		// Token: 0x04000090 RID: 144
		private const string CmdletParameterValueAttributeName = "Value";

		// Token: 0x04000091 RID: 145
		private const string CmdletNode = "Cmdlet";

		// Token: 0x04000092 RID: 146
		private const string CmdletNameAttributeName = "Name";

		// Token: 0x04000093 RID: 147
		private const string CmdletParameterMappingRelativePath = "Mappings/Mapping";

		// Token: 0x04000094 RID: 148
		private const string CmdletParameterAttributeName = "CmdletParameter";

		// Token: 0x04000095 RID: 149
		private const string ReportObjectPropertyAttributeName = "ReportObjectProperty";

		// Token: 0x04000096 RID: 150
		private const string AnnotationNode = "Annotation";

		// Token: 0x04000097 RID: 151
		private static readonly Dictionary<string, ReportingSchema> SchemaDictionary = new Dictionary<string, ReportingSchema>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000098 RID: 152
		private static readonly object SyncRoot = new object();

		// Token: 0x04000099 RID: 153
		private readonly string defaultConfigPath;

		// Token: 0x0400009A RID: 154
		private readonly Dictionary<string, IEntity> entities = new Dictionary<string, IEntity>();

		// Token: 0x0400009B RID: 155
		private readonly Dictionary<string, ResourceType> complexTypeResourceTypes = new Dictionary<string, ResourceType>();

		// Token: 0x02000034 RID: 52
		private class ReportPropertyCmdletParamMapping
		{
			// Token: 0x06000131 RID: 305 RVA: 0x00006B9B File Offset: 0x00004D9B
			public ReportPropertyCmdletParamMapping(string cmdletParameter, string reportObjectProperty)
			{
				this.CmdletParameter = cmdletParameter;
				this.ReportObjectProperty = reportObjectProperty;
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000132 RID: 306 RVA: 0x00006BB1 File Offset: 0x00004DB1
			// (set) Token: 0x06000133 RID: 307 RVA: 0x00006BB9 File Offset: 0x00004DB9
			public string CmdletParameter { get; private set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x06000134 RID: 308 RVA: 0x00006BC2 File Offset: 0x00004DC2
			// (set) Token: 0x06000135 RID: 309 RVA: 0x00006BCA File Offset: 0x00004DCA
			public string ReportObjectProperty { get; private set; }
		}

		// Token: 0x02000035 RID: 53
		private class SchemaLoadException : Exception
		{
			// Token: 0x06000136 RID: 310 RVA: 0x00006BD3 File Offset: 0x00004DD3
			public SchemaLoadException(string message) : base(message)
			{
			}
		}
	}
}
