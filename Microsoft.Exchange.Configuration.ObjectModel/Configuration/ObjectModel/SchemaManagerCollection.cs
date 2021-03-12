using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000028 RID: 40
	internal class SchemaManagerCollection
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00006958 File Offset: 0x00004B58
		public SchemaManagerCollection(Type classType)
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManagerCollection::SchemaManagerCollection - creating a schema manager collection for class type {0}", (null == classType) ? "null" : classType.Name);
			this.classType = classType;
			MasterSchemaMappingEntry[] masterSchemaInfos = this.GetMasterSchemaInfos();
			this.schemaManagerArray = new SchemaManager[masterSchemaInfos.Length];
			for (int i = 0; i < masterSchemaInfos.Length; i++)
			{
				this.schemaManagerArray[i] = new SchemaManager(classType, masterSchemaInfos[i]);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000069D4 File Offset: 0x00004BD4
		public DataSourceManager[] GetDataSourceManagers(string propertyName)
		{
			DataSourceManager[] array = new DataSourceManager[this.schemaManagerArray.Length];
			int num = 0;
			foreach (SchemaManager schemaManager in this.schemaManagerArray)
			{
				if (schemaManager.GetPersistablePropertyMappings(this.classType.ToString(), propertyName) != null)
				{
					array[num++] = schemaManager.DataSourceManager;
					break;
				}
			}
			foreach (SchemaManager schemaManager2 in this.schemaManagerArray)
			{
				if (schemaManager2.DataSourceManager != array[0])
				{
					array[num++] = schemaManager2.DataSourceManager;
				}
			}
			return array;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006A70 File Offset: 0x00004C70
		private MasterSchemaMappingEntry[] GetMasterSchemaInfos()
		{
			ExTraceGlobals.SchemaManagerTracer.Information<string>((long)this.GetHashCode(), "SchemaManagerCollection::GetMasterSchemaInfo - retrieving the master schema mapping entries for class type {0}", (null == this.classType) ? "null" : this.classType.Name);
			string fullName = this.classType.FullName;
			List<MasterSchemaMappingEntry> list = new List<MasterSchemaMappingEntry>();
			foreach (MasterSchemaMappingEntry masterSchemaMappingEntry in SchemaManager.masterSchemaMappingEntryArray)
			{
				if (fullName == masterSchemaMappingEntry.ClassName)
				{
					list.Add(masterSchemaMappingEntry);
					break;
				}
			}
			if (list.Count == 0)
			{
				throw new SchemaMappingException(Strings.ExceptionMissingDetailSchemaFile(string.Empty, fullName));
			}
			return list.ToArray();
		}

		// Token: 0x04000079 RID: 121
		private SchemaManager[] schemaManagerArray;

		// Token: 0x0400007A RID: 122
		private Type classType;
	}
}
