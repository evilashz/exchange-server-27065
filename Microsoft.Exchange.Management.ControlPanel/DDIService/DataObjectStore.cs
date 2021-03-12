using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200011B RID: 283
	public class DataObjectStore
	{
		// Token: 0x06001FF6 RID: 8182 RVA: 0x000600E8 File Offset: 0x0005E2E8
		public DataObjectStore()
		{
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00060111 File Offset: 0x0005E311
		public DataObjectStore(IList<DataObject> list) : this(list, null)
		{
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0006011C File Offset: 0x0005E31C
		public DataObjectStore(IList<DataObject> list, Type[] servicePredefinedTypes)
		{
			if (list == null)
			{
				throw new ArgumentException("list cannot be null");
			}
			this.servicePredefinedTypes = servicePredefinedTypes;
			foreach (DataObject dataObject in list)
			{
				this.store[dataObject.Name] = dataObject;
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x000601AC File Offset: 0x0005E3AC
		public List<string> ModifiedColumns
		{
			get
			{
				return this.modifiedProperties;
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x000601B4 File Offset: 0x0005E3B4
		// (set) Token: 0x06001FFB RID: 8187 RVA: 0x000601BC File Offset: 0x0005E3BC
		public ListAsyncType AsyncType { get; set; }

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x000601C5 File Offset: 0x0005E3C5
		// (set) Token: 0x06001FFD RID: 8189 RVA: 0x000601CD File Offset: 0x0005E3CD
		public bool IsGetListWorkflow { get; set; }

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x000601D6 File Offset: 0x0005E3D6
		internal Type[] ServicePredefinedTypes
		{
			get
			{
				return this.servicePredefinedTypes;
			}
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x000601DE File Offset: 0x0005E3DE
		public void RetrievePropertyInfo(string dataObjectName, string propertyName, out Type type, out PropertyDefinition propertyDefinition)
		{
			type = null;
			propertyDefinition = null;
			if (!string.IsNullOrEmpty(dataObjectName))
			{
				this.store[dataObjectName].Retrieve(propertyName, out type, out propertyDefinition);
			}
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00060204 File Offset: 0x0005E404
		public Type GetDataObjectType(string name)
		{
			return this.store[name].Type;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00060217 File Offset: 0x0005E417
		public IDataObjectCreator GetDataObjectCreator(string name)
		{
			return this.store[name].DataObjectCreator;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0006022A File Offset: 0x0005E42A
		public void UpdateDataObject(string name, object value)
		{
			this.UpdateDataObject(name, value, false);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00060235 File Offset: 0x0005E435
		public void UpdateDataObject(string name, object value, bool isDummy)
		{
			this.store[name].Value = value;
			if (isDummy)
			{
				this.AddDummyDataObject(name);
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00060254 File Offset: 0x0005E454
		public IList<string> GetKeys()
		{
			IList<string> list = new List<string>();
			foreach (string item in this.store.Keys)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000602B4 File Offset: 0x0005E4B4
		public object GetDataObject(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			return this.store[name].Value;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000602D1 File Offset: 0x0005E4D1
		public DataObject GetDataObjectDeclaration(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			return this.store[name];
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000602E9 File Offset: 0x0005E4E9
		public bool IsDataObjectDummy(string name)
		{
			return this.dummyDataObjects.Contains(name);
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000602F7 File Offset: 0x0005E4F7
		private void AddDummyDataObject(string name)
		{
			if (!this.IsDataObjectDummy(name))
			{
				this.dummyDataObjects.Add(name);
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00060310 File Offset: 0x0005E510
		public object GetValue(string name, string propertyName)
		{
			PropertyInfo propertyEx = this.store[name].Type.GetPropertyEx(propertyName);
			if (!(propertyEx != null) || this.store[name].Value == null)
			{
				return null;
			}
			return propertyEx.GetValue(this.store[name].Value, null);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0006036C File Offset: 0x0005E56C
		public void SetValue(string name, string propertyName, object value, IPropertySetter setter)
		{
			if (DBNull.Value.Equals(value))
			{
				value = null;
			}
			if (setter != null)
			{
				setter.Set(this.store[name].Value, value);
				return;
			}
			if (this.store[name].Value != null)
			{
				PropertyInfo propertyEx = this.store[name].Type.GetPropertyEx(propertyName);
				object value2 = LanguagePrimitives.ConvertTo(value, propertyEx.PropertyType);
				propertyEx.SetValue(this.store[name].Value, value2, null);
			}
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000603F8 File Offset: 0x0005E5F8
		public void SetModifiedColumns(List<string> columns)
		{
			foreach (string item in columns)
			{
				if (!this.ModifiedColumns.Contains(item))
				{
					this.ModifiedColumns.Add(item);
				}
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0006045C File Offset: 0x0005E65C
		public void ClearModifiedColumns()
		{
			this.ModifiedColumns.Clear();
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0006046C File Offset: 0x0005E66C
		public List<string> GetModifiedPropertiesBasedOnDataObject(DataRow row, string dataObjectName)
		{
			List<string> list = new List<string>();
			foreach (string name in this.ModifiedColumns)
			{
				if (row.Table.Columns.Contains(name))
				{
					Variable variable = row.Table.Columns[name].ExtendedProperties["Variable"] as Variable;
					if (variable != null && !string.IsNullOrEmpty(variable.DataObjectName) && string.Equals(variable.DataObjectName, dataObjectName) && !variable.IgnoreChangeTracking)
					{
						list.Add(variable.MappingProperty);
					}
				}
			}
			return list;
		}

		// Token: 0x04001CB1 RID: 7345
		private Dictionary<string, DataObject> store = new Dictionary<string, DataObject>();

		// Token: 0x04001CB2 RID: 7346
		private List<string> dummyDataObjects = new List<string>();

		// Token: 0x04001CB3 RID: 7347
		private List<string> modifiedProperties = new List<string>();

		// Token: 0x04001CB4 RID: 7348
		private Type[] servicePredefinedTypes;
	}
}
