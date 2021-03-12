using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000078 RID: 120
	public class DataObjectStore
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0000F33E File Offset: 0x0000D53E
		public DataObjectStore()
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000F368 File Offset: 0x0000D568
		public DataObjectStore(IList<DataObjectProfile> list)
		{
			if (list == null)
			{
				throw new ArgumentException("list cannot be null");
			}
			foreach (DataObjectProfile dataObjectProfile in list)
			{
				this.store[dataObjectProfile.Name] = dataObjectProfile;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		internal void RetrievePropertyInfo(string dataObjectName, string propertyName, out Type type)
		{
			type = null;
			if (!string.IsNullOrEmpty(dataObjectName))
			{
				this.store[dataObjectName].Retrieve(propertyName, out type);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000F410 File Offset: 0x0000D610
		public Type GetDataObjectType(string name)
		{
			return this.store[name].Type;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F423 File Offset: 0x0000D623
		public IDataObjectCreator GetDataObjectCreator(string name)
		{
			return this.store[name].DataObjectCreator;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000F436 File Offset: 0x0000D636
		public void UpdateDataObject(string name, object value)
		{
			this.store[name].DataObject = value;
			this.store[name].HasReportCorrupted = false;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F45C File Offset: 0x0000D65C
		public IList<string> GetKeys()
		{
			IList<string> list = new List<string>();
			foreach (string item in this.store.Keys)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		public object GetDataObject(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			return this.store[name].DataObject;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		public object GetValue(string name, string propertyName)
		{
			PropertyInfo property = this.store[name].Type.GetProperty(propertyName);
			return property.GetValue(this.store[name].DataObject, null);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F51C File Offset: 0x0000D71C
		public void SetValue(string name, string propertyName, object value, IPropertySetter setter)
		{
			if (DBNull.Value.Equals(value))
			{
				value = null;
			}
			if (setter != null)
			{
				setter.Set(this.store[name].DataObject, value);
				return;
			}
			PropertyInfo property = this.store[name].Type.GetProperty(propertyName);
			object value2 = LanguagePrimitives.ConvertTo(value, property.PropertyType);
			property.SetValue(this.store[name].DataObject, value2, null);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F598 File Offset: 0x0000D798
		public DataObjectStore Clone()
		{
			DataObjectStore dataObjectStore = new DataObjectStore();
			dataObjectStore.ModifiedColumns.AddRange(this.ModifiedColumns);
			dataObjectStore.ModifiedColumnsAfterCreation.AddRange(this.ModifiedColumnsAfterCreation);
			foreach (string key in this.store.Keys)
			{
				dataObjectStore.store[key] = (DataObjectProfile)this.store[key].Clone();
			}
			return dataObjectStore;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F634 File Offset: 0x0000D834
		public ValidationError[] Validate(DataTable table)
		{
			List<ValidationError> list = new List<ValidationError>();
			foreach (string key in this.store.Keys)
			{
				ValidationError[] array = this.store[key].Validate();
				foreach (ValidationError error in array)
				{
					list.Add(this.ConvertMappingProperty(error, this.store[key].DataObject, table));
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		private ValidationError ConvertMappingProperty(ValidationError error, object dataObject, DataTable table)
		{
			ValidationError result = error;
			PropertyValidationError propertyValidationError = error as PropertyValidationError;
			if (propertyValidationError != null)
			{
				string columnNameThruMappingProperty = this.GetColumnNameThruMappingProperty(propertyValidationError.PropertyDefinition.Name, table, dataObject);
				if (!string.IsNullOrEmpty(columnNameThruMappingProperty))
				{
					PropertyDefinition propertyDefinition = new AdminPropertyDefinition(columnNameThruMappingProperty, ExchangeObjectVersion.Exchange2003, typeof(bool), true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
					result = new PropertyValidationError(propertyValidationError.Description, propertyDefinition, propertyValidationError.InvalidData);
				}
			}
			return result;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F750 File Offset: 0x0000D950
		private string GetColumnNameThruMappingProperty(string mappingProperty, DataTable table, object dataObject)
		{
			string result = mappingProperty;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				ColumnProfile columnProfile = dataColumn.ExtendedProperties["ColumnProfile"] as ColumnProfile;
				if (mappingProperty.Equals(columnProfile.MappingProperty) && dataObject == this.GetDataObject(columnProfile.DataObjectName))
				{
					result = dataColumn.ColumnName;
				}
			}
			return result;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public bool IsCorrupted
		{
			get
			{
				bool flag = false;
				if (PSConnectionInfoSingleton.GetInstance().Type != OrganizationType.Cloud)
				{
					foreach (string key in this.store.Keys)
					{
						IConfigurable configurable = this.store[key].DataObject as IConfigurable;
						if (configurable != null && !this.store[key].HasReportCorrupted)
						{
							flag |= !configurable.IsValid;
							this.store[key].HasReportCorrupted = true;
						}
					}
				}
				return flag;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000F890 File Offset: 0x0000DA90
		public bool OverrideCorruptedValuesWithDefault()
		{
			bool flag = false;
			foreach (string key in this.store.Keys)
			{
				ADObject adobject = this.store[key].DataObject as ADObject;
				if (adobject != null)
				{
					flag |= WinformsHelper.OverrideCorruptedValuesWithDefault(adobject);
				}
			}
			return flag;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000F908 File Offset: 0x0000DB08
		public List<string> ModifiedColumns
		{
			get
			{
				return this.modifiedProperties;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000F910 File Offset: 0x0000DB10
		public List<string> ModifiedColumnsAfterCreation
		{
			get
			{
				return this.modifiedPropertiesAfterCreation;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F918 File Offset: 0x0000DB18
		public void SetModifiedColumns(IList<string> columns)
		{
			foreach (string item in columns)
			{
				if (!this.ModifiedColumns.Contains(item))
				{
					this.ModifiedColumns.Add(item);
				}
				if (!this.ModifiedColumnsAfterCreation.Contains(item))
				{
					this.ModifiedColumnsAfterCreation.Add(item);
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F990 File Offset: 0x0000DB90
		public void ClearModifiedColumns()
		{
			this.ModifiedColumns.Clear();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		public void ClearModifiedColumns(DataRow row, string dataObjectName)
		{
			List<string> properties = this.GetModifiedPropertiesBasedOnDataObject(row, dataObjectName);
			this.ModifiedColumns.RemoveAll((string c) => properties.Contains(c));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public List<string> GetModifiedPropertiesBasedOnDataObject(DataRow row, string dataObjectName)
		{
			List<string> list = new List<string>();
			foreach (string name in this.ModifiedColumns)
			{
				if (row.Table.Columns.Contains(name))
				{
					ColumnProfile columnProfile = row.Table.Columns[name].ExtendedProperties["ColumnProfile"] as ColumnProfile;
					if (columnProfile != null && string.Equals(columnProfile.DataObjectName, dataObjectName) && !columnProfile.IgnoreChangeTracking)
					{
						list.Add(columnProfile.MappingProperty);
					}
				}
			}
			return list;
		}

		// Token: 0x04000112 RID: 274
		private Dictionary<string, DataObjectProfile> store = new Dictionary<string, DataObjectProfile>();

		// Token: 0x04000113 RID: 275
		private List<string> modifiedProperties = new List<string>();

		// Token: 0x04000114 RID: 276
		private List<string> modifiedPropertiesAfterCreation = new List<string>();
	}
}
