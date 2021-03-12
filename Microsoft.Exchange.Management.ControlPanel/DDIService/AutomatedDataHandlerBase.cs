using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200019B RID: 411
	public abstract class AutomatedDataHandlerBase
	{
		// Token: 0x060022FE RID: 8958 RVA: 0x000697B8 File Offset: 0x000679B8
		public AutomatedDataHandlerBase(Service profileBuilder)
		{
			this.profileBuilder = profileBuilder;
			this.Table = new DataTable();
			this.DataObjectStore = new DataObjectStore(this.profileBuilder.DataObjects, profileBuilder.PredefinedTypes.ToArray());
			Dictionary<string, List<string>> rbacMetaData = null;
			if (typeof(DDICodeBehind).IsAssignableFrom(this.profileBuilder.Class))
			{
				object obj = Activator.CreateInstance(this.profileBuilder.Class);
				this.profileBuilder.Class.GetMethod("ApplyMetaData").Invoke(obj, new object[0]);
				rbacMetaData = (this.profileBuilder.Class.GetProperty("RbacMetaData").GetValue(obj, null) as Dictionary<string, List<string>>);
			}
			lock (AutomatedDataHandlerBase.syncObject)
			{
				this.CreateColumn(this.table, rbacMetaData);
			}
			this.InputTable = this.Table.Clone();
			this.InputTable.Rows.Add(this.InputTable.NewRow());
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000698D8 File Offset: 0x00067AD8
		public AutomatedDataHandlerBase(string schemaFilesInstallPath, string schema) : this(AutomatedDataHandlerBase.BuildProfile(schemaFilesInstallPath, schema))
		{
			DDIHelper.Trace("Schema: " + schema);
			this.Table.TableName = schema;
		}

		// Token: 0x17001AB9 RID: 6841
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x00069903 File Offset: 0x00067B03
		public Service ProfileBuilder
		{
			get
			{
				return this.profileBuilder;
			}
		}

		// Token: 0x17001ABA RID: 6842
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0006990B File Offset: 0x00067B0B
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x00069913 File Offset: 0x00067B13
		public DataTable InputTable { get; private set; }

		// Token: 0x17001ABB RID: 6843
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0006991C File Offset: 0x00067B1C
		public DataRow Input
		{
			get
			{
				return this.InputTable.Rows[0];
			}
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x0006992F File Offset: 0x00067B2F
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x00069937 File Offset: 0x00067B37
		public DataTable Table
		{
			get
			{
				return this.table;
			}
			private set
			{
				this.table = value;
				this.Table.ExtendedProperties["DataSourceStore"] = this.DataObjectStore;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0006995B File Offset: 0x00067B5B
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x00069963 File Offset: 0x00067B63
		public DataObjectStore DataObjectStore
		{
			get
			{
				return this.dataObjectStore;
			}
			set
			{
				if (value != this.DataObjectStore)
				{
					this.dataObjectStore = value;
					this.Table.ExtendedProperties["DataSourceStore"] = this.dataObjectStore;
				}
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00069990 File Offset: 0x00067B90
		protected DataRow Row
		{
			get
			{
				return this.Table.Rows[0];
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000699A3 File Offset: 0x00067BA3
		internal static Service BuildProfile(string schemaFilesInstallPath, string resourceName)
		{
			return ServiceManager.GetInstance().GetService(schemaFilesInstallPath, resourceName);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000699C4 File Offset: 0x00067BC4
		protected void CreateColumn(DataTable table, Dictionary<string, List<string>> rbacMetaData)
		{
			IList<Variable> variables = this.profileBuilder.Variables;
			foreach (Variable profile in variables)
			{
				table.Columns.Add(AutomatedDataHandlerBase.CreateColumn(profile, rbacMetaData, this.dataObjectStore));
			}
			if ((from c in variables
			where string.Equals("IsReadOnly", c.Name, StringComparison.OrdinalIgnoreCase)
			select c).Count<Variable>() == 0)
			{
				table.Columns.Add(AutomatedDataHandlerBase.CreateColumn(new Variable
				{
					Name = "IsReadOnly",
					Type = typeof(bool)
				}, rbacMetaData, this.dataObjectStore));
			}
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00069A8C File Offset: 0x00067C8C
		internal static DataColumn CreateColumn(Variable profile, Dictionary<string, List<string>> rbacMetaData, DataObjectStore store)
		{
			DataColumn dataColumn = new DataColumn(profile.Name);
			Type type = null;
			PropertyDefinition value = null;
			if (profile.PersistWholeObject)
			{
				type = store.GetDataObjectType(profile.DataObjectName);
			}
			else
			{
				store.RetrievePropertyInfo(profile.DataObjectName, profile.MappingProperty, out type, out value);
			}
			dataColumn.DataType = (profile.Type ?? typeof(object));
			Type type2;
			if ((type2 = profile.Type) == null)
			{
				type2 = (type ?? typeof(object));
			}
			profile.Type = type2;
			dataColumn.ExtendedProperties.Add("Variable", profile);
			dataColumn.ExtendedProperties.Add("RealDataType", profile.Type);
			dataColumn.ExtendedProperties.Add("PropertyDefinition", value);
			if (rbacMetaData != null && rbacMetaData.ContainsKey(profile.Name))
			{
				dataColumn.ExtendedProperties.Add("RbacMetaData", rbacMetaData[profile.Name]);
			}
			if (profile.Value != null)
			{
				string value2 = profile.Value as string;
				if (DDIHelper.IsLambdaExpression(value2))
				{
					dataColumn.ExtendedProperties["LambdaExpression"] = profile.Value;
				}
				else
				{
					dataColumn.DefaultValue = profile.Value;
				}
			}
			return dataColumn;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00069BB5 File Offset: 0x00067DB5
		protected void FillColumnsBasedOnLambdaExpression(DataRow row, Variable variable)
		{
			if (DDIHelper.IsLambdaExpression(variable.Value as string))
			{
				this.FillColumns(row, this.GetExpressionCalculator().CalculateSpecifiedColumn(variable.Name, row, this.Input));
			}
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00069BE8 File Offset: 0x00067DE8
		private void FillColumns(DataRow row, IList<KeyValuePair<string, object>> proposedValues)
		{
			foreach (KeyValuePair<string, object> keyValuePair in proposedValues)
			{
				if (keyValuePair.Value == null)
				{
					row[keyValuePair.Key] = DBNull.Value;
				}
				else
				{
					row[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00069C5C File Offset: 0x00067E5C
		private ExpressionCalculator GetExpressionCalculator()
		{
			if (this.expressionCalculator == null)
			{
				this.expressionCalculator = ExpressionCalculator.Parse(this.Table);
			}
			return this.expressionCalculator;
		}

		// Token: 0x04001DAA RID: 7594
		private DataTable table;

		// Token: 0x04001DAB RID: 7595
		private Service profileBuilder;

		// Token: 0x04001DAC RID: 7596
		private DataObjectStore dataObjectStore;

		// Token: 0x04001DAD RID: 7597
		private ExpressionCalculator expressionCalculator;

		// Token: 0x04001DAE RID: 7598
		private static readonly object syncObject = new object();
	}
}
