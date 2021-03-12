using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Markup;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000054 RID: 84
	internal abstract class AutomatedDataHandlerBase : DataHandler
	{
		// Token: 0x0600034E RID: 846 RVA: 0x0000BB93 File Offset: 0x00009D93
		public AutomatedDataHandlerBase(ITableCentricConfigurable profileBuilder)
		{
			this.profileBuilder = profileBuilder;
			this.Table = new DataTable();
			this.DataObjectStore = new DataObjectStore(this.profileBuilder.BuildDataObjectProfile());
			this.CreateColumn(this.table);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BBCF File Offset: 0x00009DCF
		public AutomatedDataHandlerBase(Assembly assembly, string schema) : this(AutomatedDataHandlerBase.BuildProfile(assembly, schema))
		{
			this.Assembly = assembly;
			this.SchemaName = schema;
			this.Table.TableName = schema;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000BC00 File Offset: 0x00009E00
		public Assembly Assembly { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000BC09 File Offset: 0x00009E09
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000BC11 File Offset: 0x00009E11
		public string SchemaName { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000BC1A File Offset: 0x00009E1A
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000BC24 File Offset: 0x00009E24
		public DataTable Table
		{
			get
			{
				return this.table;
			}
			protected set
			{
				if (!object.ReferenceEquals(this.Table, value))
				{
					if (this.Table != null)
					{
						this.Table.ExtendedProperties["DataSourceStore"] = null;
						this.Table.ColumnChanged -= this.table_ColumnChanged;
					}
					this.table = value;
					if (this.Table != null)
					{
						this.Table.ExtendedProperties["DataSourceStore"] = this.DataObjectStore;
						this.Table.ColumnChanged += this.table_ColumnChanged;
					}
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000BCB5 File Offset: 0x00009EB5
		// (set) Token: 0x06000357 RID: 855 RVA: 0x0000BCBD File Offset: 0x00009EBD
		public bool EnableBulkEdit
		{
			get
			{
				return this.enableBulkEdit;
			}
			internal set
			{
				this.enableBulkEdit = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000BCC6 File Offset: 0x00009EC6
		protected DataRow Row
		{
			get
			{
				return this.Table.Rows[0];
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000BCD9 File Offset: 0x00009ED9
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000BCE1 File Offset: 0x00009EE1
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000BD0E File Offset: 0x00009F0E
		internal ITableCentricConfigurable ProfileBuilder
		{
			get
			{
				return this.profileBuilder;
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000BD18 File Offset: 0x00009F18
		protected void CreateColumn(DataTable table)
		{
			IList<ColumnProfile> list = this.profileBuilder.BuildColumnProfile();
			foreach (ColumnProfile columnProfile in list)
			{
				DataColumn dataColumn = new DataColumn(columnProfile.Name);
				Type type = null;
				object defaultValue = null;
				if (columnProfile.PersistWholeObject)
				{
					type = this.dataObjectStore.GetDataObjectType(columnProfile.DataObjectName);
				}
				else
				{
					this.dataObjectStore.RetrievePropertyInfo(columnProfile.DataObjectName, columnProfile.MappingProperty, out type);
				}
				columnProfile.Retrieve(ref type, ref defaultValue);
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					type = type.GetGenericArguments()[0];
				}
				if (type.IsEnum)
				{
					dataColumn.DataType = typeof(object);
				}
				else
				{
					dataColumn.DataType = type;
				}
				dataColumn.DefaultValue = defaultValue;
				dataColumn.ExtendedProperties.Add("ColumnProfile", columnProfile);
				dataColumn.ExtendedProperties.Add("RealDataType", columnProfile.Type);
				if (!string.IsNullOrEmpty(columnProfile.LambdaExpression))
				{
					dataColumn.ExtendedProperties["LambdaExpression"] = columnProfile.LambdaExpression;
				}
				if (!string.IsNullOrEmpty(columnProfile.OnceLambdaExpression))
				{
					dataColumn.ExtendedProperties["OnceLambdaExpression"] = columnProfile.OnceLambdaExpression;
				}
				table.Columns.Add(dataColumn);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000BE98 File Offset: 0x0000A098
		public DataTable GetDataTableSchema()
		{
			return this.Table.Clone();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		private void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			if (this.suppressColumnChanged)
			{
				return;
			}
			this.Row.EndEdit();
			this.FillColumnsBasedOnLambdaExpression(e.Column.ColumnName);
			try
			{
				this.UpdateObject(e.Column);
			}
			catch (TargetInvocationException ex)
			{
				throw (ex.InnerException != null) ? ex.InnerException : ex;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000BF0C File Offset: 0x0000A10C
		private void FillColumnsBasedOnOnceLambdaExpression()
		{
			this.suppressColumnChanged = true;
			IList<KeyValuePair<string, object>> list = this.GetOnceExpressionCalculator().CalculateAll(this.Row, null);
			foreach (KeyValuePair<string, object> keyValuePair in list)
			{
				if (keyValuePair.Value == null)
				{
					this.Row[keyValuePair.Key] = DBNull.Value;
				}
				else
				{
					this.Row[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			this.suppressColumnChanged = false;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		private void FillColumnsBasedOnLambdaExpression(string changedColumn)
		{
			this.suppressColumnChanged = true;
			IList<KeyValuePair<string, object>> list = string.IsNullOrEmpty(changedColumn) ? this.GetExpressionCalculator().CalculateAll(this.Row, null) : this.GetExpressionCalculator().CalculateAffectedColumns(changedColumn, this.Row, null);
			foreach (KeyValuePair<string, object> keyValuePair in list)
			{
				if (keyValuePair.Value == null)
				{
					this.Row[keyValuePair.Key] = DBNull.Value;
				}
				else
				{
					this.Row[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			this.suppressColumnChanged = false;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000C068 File Offset: 0x0000A268
		private ExpressionCalculator GetExpressionCalculator()
		{
			if (this.expressionCalculator == null)
			{
				this.expressionCalculator = ExpressionCalculator.Parse(this.Table);
			}
			return this.expressionCalculator;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000C089 File Offset: 0x0000A289
		private ExpressionCalculator GetOnceExpressionCalculator()
		{
			if (this.onceExpressionCalculator == null)
			{
				this.onceExpressionCalculator = ExpressionCalculator.Parse(this.Table, "OnceLambdaExpression");
			}
			return this.onceExpressionCalculator;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public void RefreshDataObjectStore()
		{
			foreach (string targetConfigObject in this.DataObjectStore.GetKeys())
			{
				this.UpdateTable(this.Row, targetConfigObject);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C108 File Offset: 0x0000A308
		public void RefreshDataObjectStoreWithNewTable()
		{
			this.Table = this.Table.Copy();
			this.RefreshDataObjectStore();
			base.DataSource = this.Table;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C130 File Offset: 0x0000A330
		protected void UpdateObject(DataColumn column)
		{
			ColumnProfile columnProfile = column.ExtendedProperties["ColumnProfile"] as ColumnProfile;
			if (!string.IsNullOrEmpty(columnProfile.DataObjectName))
			{
				if (!columnProfile.PersistWholeObject)
				{
					this.dataObjectStore.SetValue(columnProfile.DataObjectName, columnProfile.MappingProperty, this.Row[column.ColumnName], columnProfile.PropertySetter);
				}
				else
				{
					this.dataObjectStore.UpdateDataObject(columnProfile.DataObjectName, this.Row[column.ColumnName]);
				}
				this.UpdateTable(this.Row, columnProfile.DataObjectName);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		internal void UpdateTable(DataRow row, string targetConfigObject)
		{
			this.UpdateTable(row, targetConfigObject, false);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		internal void UpdateTable(DataRow row, string targetConfigObject, bool isOnReading)
		{
			if (this.DataObjectStore.GetDataObject(targetConfigObject) == null)
			{
				return;
			}
			this.suppressColumnChanged = true;
			try
			{
				foreach (object obj in this.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					ColumnProfile columnProfile = dataColumn.ExtendedProperties["ColumnProfile"] as ColumnProfile;
					if (columnProfile != null && columnProfile.DataObjectName != null && targetConfigObject.Equals(columnProfile.DataObjectName, StringComparison.InvariantCultureIgnoreCase))
					{
						object obj2 = columnProfile.PersistWholeObject ? this.DataObjectStore.GetDataObject(columnProfile.DataObjectName) : this.DataObjectStore.GetValue(columnProfile.DataObjectName, columnProfile.MappingProperty);
						obj2 = (obj2 ?? DBNull.Value);
						this.Row[dataColumn] = obj2;
					}
				}
			}
			finally
			{
				this.suppressColumnChanged = false;
				if (isOnReading)
				{
					this.FillColumnsBasedOnOnceLambdaExpression();
				}
				this.FillColumnsBasedOnLambdaExpression(null);
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		protected override void CheckObjectReadOnly()
		{
			bool isObjectReadOnly = false;
			ExchangeObjectVersion exchangeObjectVersion = ExchangeObjectVersion.Exchange2003;
			foreach (string name in this.dataObjectStore.GetKeys())
			{
				IVersionable versionable = this.dataObjectStore.GetDataObject(name) as IVersionable;
				if (versionable != null && versionable.IsReadOnly)
				{
					isObjectReadOnly = true;
				}
				if (versionable != null && exchangeObjectVersion.IsOlderThan(versionable.ExchangeVersion))
				{
					exchangeObjectVersion = versionable.ExchangeVersion;
				}
			}
			base.IsObjectReadOnly = isObjectReadOnly;
			if (base.IsObjectReadOnly)
			{
				base.ObjectReadOnlyReason = Strings.VersionMismatchWarning(exchangeObjectVersion.ExchangeBuild);
				return;
			}
			base.ObjectReadOnlyReason = string.Empty;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		public override bool IsCorrupted
		{
			get
			{
				return !this.EnableBulkEdit && this.dataObjectStore.IsCorrupted;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		public override bool OverrideCorruptedValuesWithDefault()
		{
			return this.dataObjectStore.OverrideCorruptedValuesWithDefault();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public override ValidationError[] Validate()
		{
			if (this.EnableBulkEdit)
			{
				return new ValidationError[0];
			}
			return this.DataObjectStore.Validate(this.Table);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C3F6 File Offset: 0x0000A5F6
		public override ValidationError[] ValidateOnly(object objectToBeValidated)
		{
			return this.Validate();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		internal override void SpecifyParameterNames(Dictionary<object, List<string>> bindingMembers)
		{
			if (bindingMembers.Keys.Contains(this.Table))
			{
				this.DataObjectStore.SetModifiedColumns(bindingMembers[this.Table]);
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C464 File Offset: 0x0000A664
		internal bool IsBulkEditingSupportedParameterName(object dataSource, string propertyName)
		{
			return (from DataColumn c in this.Table.Columns
			where c.ColumnName == propertyName && (c.ExtendedProperties["ColumnProfile"] as ColumnProfile).SupportBulkEdit
			select c).Count<DataColumn>() > 0;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000C4A7 File Offset: 0x0000A6A7
		internal bool IsBulkEditingModifiedParameterName(object dataSource, string propertyName)
		{
			return this.DataObjectStore.ModifiedColumnsAfterCreation.Contains(propertyName);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		internal static ITableCentricConfigurable BuildProfile(Assembly resourceContainedAssembly, string resourceName)
		{
			string name = resourceName + ".xaml";
			ITableCentricConfigurable tableCentricConfigurable = null;
			Stopwatch stopwatch = new Stopwatch();
			ExTraceGlobals.DataFlowTracer.TracePerformance((long)Thread.CurrentThread.ManagedThreadId, "Start to parse the schema " + resourceName);
			stopwatch.Start();
			if (tableCentricConfigurable == null)
			{
				Stream manifestResourceStream = resourceContainedAssembly.GetManifestResourceStream(name);
				tableCentricConfigurable = (XamlReader.Load(manifestResourceStream) as ITableCentricConfigurable);
			}
			stopwatch.Stop();
			ExTraceGlobals.DataFlowTracer.TracePerformance<string, long>((long)Thread.CurrentThread.ManagedThreadId, "End to parse the schema {0} and it costs {1} ms.", resourceName, stopwatch.ElapsedMilliseconds);
			return tableCentricConfigurable;
		}

		// Token: 0x06000371 RID: 881
		internal abstract bool HasViewPermissionForPage(string pageName);

		// Token: 0x06000372 RID: 882
		internal abstract bool HasPermissionForProperty(string propertyName, bool canUpdate);

		// Token: 0x040000E0 RID: 224
		private DataTable table;

		// Token: 0x040000E1 RID: 225
		private ITableCentricConfigurable profileBuilder;

		// Token: 0x040000E2 RID: 226
		private DataObjectStore dataObjectStore;

		// Token: 0x040000E3 RID: 227
		private bool suppressColumnChanged;

		// Token: 0x040000E4 RID: 228
		private bool enableBulkEdit;

		// Token: 0x040000E5 RID: 229
		private ExpressionCalculator expressionCalculator;

		// Token: 0x040000E6 RID: 230
		private ExpressionCalculator onceExpressionCalculator;
	}
}
