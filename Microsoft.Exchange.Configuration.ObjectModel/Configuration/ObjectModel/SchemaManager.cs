using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	internal class SchemaManager
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00005C73 File Offset: 0x00003E73
		internal SchemaManager(Type persistableType, MasterSchemaMappingEntry masterSchemaMappingEntry)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManager::SchemaManager - creating a schema manager for class type {0}", masterSchemaMappingEntry.ClassName);
			this.persistableType = persistableType;
			this.LoadDetailSchemaInfo(masterSchemaMappingEntry);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005CA5 File Offset: 0x00003EA5
		public DataSourceManager DataSourceManager
		{
			get
			{
				return this.dataSourceManager;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005CAD File Offset: 0x00003EAD
		internal Type PersistableType
		{
			get
			{
				return this.persistableType;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005CB5 File Offset: 0x00003EB5
		public SchemaMappingEntry[] GetAllMappings()
		{
			ExTraceGlobals.SchemaManagerTracer.Information((long)this.GetHashCode(), "SchemaManager::GetAllMappings - retrieving all schema mappings.");
			return this.schemaMappingEntryArray;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005D00 File Offset: 0x00003F00
		public PersistableClass[] GetPersistableClassMappings(string className)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManager::GetPersistableClassMappings - retrieving all persistable class mappings for class {0}.", className);
			return this.FilterSchemaMappingEntry<PersistableClass>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is PersistableClass && ((PersistableClass)schemaMappingEntry).SourceClassName == className);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005D80 File Offset: 0x00003F80
		public PersistableClass[] GetPersistableClassMappings(string className, Type mappingType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, Type>((long)this.GetHashCode(), "SchemaManager::GetPersistableClassMappings - retrieving all persistable class mappings for class {0} and mapping type {1}.", className, mappingType);
			return this.FilterSchemaMappingEntry<PersistableClass>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is PersistableClass && ((PersistableClass)schemaMappingEntry).SourceClassName == className && mappingType.IsInstanceOfType(schemaMappingEntry));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005E00 File Offset: 0x00004000
		public PersistableProperty[] GetPersistablePropertyMappings(string className)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManager::GetPersistablePropertyMappings - retrieving all persistable property mappings for class {0}.", className);
			return this.FilterSchemaMappingEntry<PersistableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is PersistableProperty && ((PersistableProperty)schemaMappingEntry).SourceClassName == className);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005E80 File Offset: 0x00004080
		public PersistableProperty[] GetPersistablePropertyMappings(string className, Type mappingType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, Type>((long)this.GetHashCode(), "SchemaManager::GetPersistablePropertyMappings - retrieving all persistable property mappings for class {0} and mapping type {1}.", className, mappingType);
			return this.FilterSchemaMappingEntry<PersistableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is PersistableProperty && ((PersistableProperty)schemaMappingEntry).SourceClassName == className && mappingType.IsInstanceOfType(schemaMappingEntry));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005F18 File Offset: 0x00004118
		public PersistableProperty[] GetPersistablePropertyMappings(string className, string propertyName)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, string>((long)this.GetHashCode(), "SchemaManager::GetPersistablePropertyMappings - retrieving all persistable property mappings for class {0} and property {1}.", className, propertyName);
			return this.FilterSchemaMappingEntry<PersistableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is PersistableProperty && ((PersistableProperty)schemaMappingEntry).SourceClassName == className && ((PersistableProperty)schemaMappingEntry).SourcePropertyName == propertyName);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005FCC File Offset: 0x000041CC
		public PersistableProperty[] GetPersistablePropertyMappings(string className, string propertyName, Type mappingType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, string, Type>((long)this.GetHashCode(), "SchemaManager::GetPersistablePropertyMappings - retrieving all persistable property mappings for class {0} and property name {1} and mapping type {2}.", className, propertyName, mappingType);
			return this.FilterSchemaMappingEntry<PersistableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is PersistableProperty && ((PersistableProperty)schemaMappingEntry).SourceClassName == className && ((PersistableProperty)schemaMappingEntry).SourcePropertyName == propertyName && mappingType.IsInstanceOfType(schemaMappingEntry));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006058 File Offset: 0x00004258
		public SearchableProperty[] GetSearchablePropertyMappings(string className)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManager::GetSearchablePropertyMappings - retrieving all searchable property mappings for class {0}.", className);
			return this.FilterSchemaMappingEntry<SearchableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is SearchableProperty && ((SearchableProperty)schemaMappingEntry).SourceClassName == className);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000060D8 File Offset: 0x000042D8
		public SearchableProperty[] GetSearchablePropertyMappings(string className, Type mappingType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, Type>((long)this.GetHashCode(), "SchemaManager::GetSearchablePropertyMappings - retrieving all searchable property mappings for class {0} and mapping type {1}.", className, mappingType);
			return this.FilterSchemaMappingEntry<SearchableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is SearchableProperty && ((SearchableProperty)schemaMappingEntry).SourceClassName == className && mappingType.IsInstanceOfType(schemaMappingEntry));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006170 File Offset: 0x00004370
		public SearchableProperty[] GetSearchablePropertyMappings(string className, string propertyName)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, string>((long)this.GetHashCode(), "SchemaManager::GetSearchablePropertyMappings - retrieving all searchable property mappings for class {0} and property name {1}.", className, propertyName);
			return this.FilterSchemaMappingEntry<SearchableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is SearchableProperty && ((SearchableProperty)schemaMappingEntry).SourceClassName == className && ((SearchableProperty)schemaMappingEntry).SourcePropertyName == propertyName);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006224 File Offset: 0x00004424
		public SearchableProperty[] GetSearchablePropertyMappings(string className, string propertyName, Type mappingType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, string, Type>((long)this.GetHashCode(), "SchemaManager::GetSearchablePropertyMappings - retrieving all searchable property mappings for class {0} and property name {1} and mapping type {2}.", className, propertyName, mappingType);
			return this.FilterSchemaMappingEntry<SearchableProperty>((SchemaMappingEntry schemaMappingEntry) => schemaMappingEntry is SearchableProperty && ((SearchableProperty)schemaMappingEntry).SourceClassName == className && ((SearchableProperty)schemaMappingEntry).SourcePropertyName == propertyName && mappingType.IsInstanceOfType(schemaMappingEntry));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000629C File Offset: 0x0000449C
		public SchemaMappingEntry[] GetMappingsOfType(Type mappingType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<Type>((long)this.GetHashCode(), "SchemaManager::GetMappingsOfType - retrieving all mappings of type {0}.", mappingType);
			return this.FilterSchemaMappingEntry<SchemaMappingEntry>((SchemaMappingEntry schemaMappingEntry) => mappingType.IsInstanceOfType(schemaMappingEntry));
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000062E4 File Offset: 0x000044E4
		private T[] FilterSchemaMappingEntry<T>(SchemaManager.SchemaMappingEntryCondition condition) where T : SchemaMappingEntry
		{
			List<T> list = new List<T>();
			foreach (SchemaMappingEntry schemaMappingEntry in this.schemaMappingEntryArray)
			{
				if (condition(schemaMappingEntry))
				{
					list.Add((T)((object)schemaMappingEntry));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000632B File Offset: 0x0000452B
		protected static MasterSchemaMappingEntry[] GetMasterSchemaMappings()
		{
			return new MasterSchemaMappingEntry[0];
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006334 File Offset: 0x00004534
		protected static Stream GetSchemaXmlDocument(string documentName)
		{
			Stream stream = null;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (null != entryAssembly)
			{
				try
				{
					stream = entryAssembly.GetManifestResourceStream(documentName);
				}
				catch (NotSupportedException)
				{
				}
			}
			if (stream != null)
			{
				return stream;
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				try
				{
					stream = assembly.GetManifestResourceStream(documentName);
				}
				catch (NotSupportedException)
				{
				}
				catch (SecurityException)
				{
				}
				if (stream != null)
				{
					return stream;
				}
			}
			string path = Path.Combine(SchemaManager.currentDirectory, documentName);
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (FileNotFoundException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			if (fileStream != null)
			{
				return fileStream;
			}
			path = Path.Combine(ConfigurationContext.Setup.DataPath, documentName);
			try
			{
				fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (FileNotFoundException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			if (fileStream != null)
			{
				return fileStream;
			}
			path = Path.Combine(ConfigurationContext.Setup.BinPath, documentName);
			try
			{
				fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			catch (FileNotFoundException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			if (fileStream != null)
			{
				return fileStream;
			}
			throw new FileNotFoundException(documentName);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000649C File Offset: 0x0000469C
		protected static string GetCurrentDirectory()
		{
			Uri uri = new Uri(Assembly.GetExecutingAssembly().CodeBase);
			return Path.GetDirectoryName(uri.LocalPath);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000064C4 File Offset: 0x000046C4
		protected void LoadDetailSchemaInfo(MasterSchemaMappingEntry masterSchemaMappingEntry)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string, string>((long)this.GetHashCode(), "SchemaManager::LoadDetailSchemaInfo - retrieving info from detail schema file {0} and DataSourceManager assembly {1}.", (masterSchemaMappingEntry == null) ? "null" : masterSchemaMappingEntry.SchemaFileName, (masterSchemaMappingEntry == null) ? "null" : masterSchemaMappingEntry.DataSourceManagerAssemblyName);
			this.LoadDetailSchemaMappings(masterSchemaMappingEntry);
			this.LoadDataSourceManager(masterSchemaMappingEntry);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006518 File Offset: 0x00004718
		protected void LoadDetailSchemaMappings(MasterSchemaMappingEntry masterSchemaMappingEntry)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManager::LoadDetailSchemaMappings - retrieving info from detail schema mapping XML file {0}.", (masterSchemaMappingEntry == null) ? "null" : masterSchemaMappingEntry.SchemaFileName);
			string schemaFileName = masterSchemaMappingEntry.SchemaFileName;
			this.schemaMappingEntryArray = (SchemaMappingEntry[])SchemaManager.schemaMappingEntryArrayHashTable[schemaFileName];
			if (this.schemaMappingEntryArray == null)
			{
				lock (typeof(SchemaManager))
				{
					string dataSourceManagerAssemblyName = masterSchemaMappingEntry.DataSourceManagerAssemblyName;
					Assembly assembly = (Assembly)SchemaManager.dsmAssemblyHashTable[dataSourceManagerAssemblyName];
					if (null == assembly)
					{
						string assemblyString = dataSourceManagerAssemblyName;
						if (Path.GetExtension(dataSourceManagerAssemblyName).ToLower() == ".dll")
						{
							assemblyString = Path.GetFileNameWithoutExtension(dataSourceManagerAssemblyName);
						}
						try
						{
							assembly = Assembly.Load(assemblyString);
						}
						catch (FileNotFoundException)
						{
						}
						if (assembly == null)
						{
							string assemblyFile = Path.Combine(ConfigurationContext.Setup.BinPath, dataSourceManagerAssemblyName);
							assembly = Assembly.LoadFrom(assemblyFile);
						}
						SchemaManager.dsmAssemblyHashTable[dataSourceManagerAssemblyName] = assembly;
					}
					List<Type> list = new List<Type>();
					foreach (Type type in assembly.GetTypes())
					{
						if (type.IsSubclassOf(typeof(SchemaMappingEntry)))
						{
							list.Add(type);
						}
					}
					Assembly executingAssembly = Assembly.GetExecutingAssembly();
					foreach (Type type2 in executingAssembly.GetTypes())
					{
						if (type2.IsSubclassOf(typeof(SchemaMappingEntry)))
						{
							list.Add(type2);
						}
					}
					Type[] extraTypes = list.ToArray();
					SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(SchemaMappingEntry[]), extraTypes);
					Stream schemaXmlDocument = SchemaManager.GetSchemaXmlDocument(masterSchemaMappingEntry.SchemaFileName);
					using (schemaXmlDocument)
					{
						this.schemaMappingEntryArray = (SchemaMappingEntry[])safeXmlSerializer.Deserialize(schemaXmlDocument);
						SchemaManager.schemaMappingEntryArrayHashTable[masterSchemaMappingEntry.SchemaFileName] = this.schemaMappingEntryArray;
					}
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000674C File Offset: 0x0000494C
		protected void LoadDataSourceManager(MasterSchemaMappingEntry masterSchemaMappingEntry)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManager::LoadDataSourceManager - retrieving info from DataSourceManager assembly {0}.", (masterSchemaMappingEntry == null) ? "null" : masterSchemaMappingEntry.DataSourceManagerAssemblyName);
			DataSourceManagerDelegate dataSourceManagerDelegate = (DataSourceManagerDelegate)SchemaManager.dsmDelegateHashTable[masterSchemaMappingEntry.DataSourceManagerAssemblyName];
			if (dataSourceManagerDelegate == null)
			{
				Assembly assembly = (Assembly)SchemaManager.dsmAssemblyHashTable[masterSchemaMappingEntry.DataSourceManagerAssemblyName];
				lock (typeof(SchemaManager))
				{
					Type[] types = assembly.GetTypes();
					int i = 0;
					while (i < types.Length)
					{
						Type type = types[i];
						if (type.IsSubclassOf(typeof(DataSourceManager)))
						{
							MethodInfo method = type.GetMethod("CreateInstance", BindingFlags.Static | BindingFlags.Public);
							if (null == method)
							{
								throw new SchemaMappingException(Strings.ExceptionMissingCreateInstance(type, assembly.CodeBase));
							}
							dataSourceManagerDelegate = (DataSourceManagerDelegate)Delegate.CreateDelegate(typeof(DataSourceManagerDelegate), method);
							string dataSourceManagerAssemblyName = masterSchemaMappingEntry.DataSourceManagerAssemblyName;
							SchemaManager.dsmDelegateHashTable[dataSourceManagerAssemblyName] = dataSourceManagerDelegate;
							break;
						}
						else
						{
							i++;
						}
					}
				}
				if (dataSourceManagerDelegate == null)
				{
					throw new SchemaMappingException(Strings.ExceptionMissingDataSourceManager(assembly.CodeBase));
				}
			}
			this.dataSourceManager = dataSourceManagerDelegate(this, masterSchemaMappingEntry.DataSourceInfoPath);
		}

		// Token: 0x0400006C RID: 108
		internal static readonly MasterSchemaMappingEntry[] masterSchemaMappingEntryArray = SchemaManager.GetMasterSchemaMappings();

		// Token: 0x0400006D RID: 109
		private static readonly string currentDirectory = SchemaManager.GetCurrentDirectory();

		// Token: 0x0400006E RID: 110
		private static Hashtable schemaMappingEntryArrayHashTable = new Hashtable();

		// Token: 0x0400006F RID: 111
		private static Hashtable dsmAssemblyHashTable = new Hashtable();

		// Token: 0x04000070 RID: 112
		private static Hashtable dsmDelegateHashTable = new Hashtable();

		// Token: 0x04000071 RID: 113
		private SchemaMappingEntry[] schemaMappingEntryArray;

		// Token: 0x04000072 RID: 114
		private DataSourceManager dataSourceManager;

		// Token: 0x04000073 RID: 115
		private Type persistableType;

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x0600016A RID: 362
		private delegate bool SchemaMappingEntryCondition(SchemaMappingEntry schemaMappingEntry);
	}
}
