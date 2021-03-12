using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000161 RID: 353
	public class StrongTypeEditorDataHandler<T> : IValidator
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x00037441 File Offset: 0x00035641
		public StrongTypeEditorDataHandler(StrongTypeEditor<T> strongTypeEditor, string schema)
		{
			this.strongTypeEditor = strongTypeEditor;
			this.schemaLoader = new StrongTypeSchemaLoader(schema);
			this.InitializeDataTable();
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003746D File Offset: 0x0003566D
		public StrongTypeEditorDataHandler(StrongTypeEditor<T> strongTypeEditor) : this(strongTypeEditor, null)
		{
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00037478 File Offset: 0x00035678
		private void InitializeDataTable()
		{
			this.table = new DataTable();
			this.CreateColumn(this.table);
			this.table.Rows.Add(this.table.NewRow());
			this.strongTypeEditor.BindingSource.DataSource = this.table;
			this.table.ColumnChanged += this.table_ColumnChanged;
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000374E4 File Offset: 0x000356E4
		protected StrongTypeEditor<T> StrongTypeEditor
		{
			get
			{
				return this.strongTypeEditor;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000374EC File Offset: 0x000356EC
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000374F4 File Offset: 0x000356F4
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000374FC File Offset: 0x000356FC
		public T StrongType
		{
			get
			{
				return this.strongType;
			}
			set
			{
				try
				{
					this.isLoadingData = true;
					if (!object.Equals(this.strongType, value))
					{
						this.strongType = value;
						if (this.AllowUpdateGUI)
						{
							if (value == null)
							{
								this.SetAsDefaultValue(this.Table);
							}
							else
							{
								this.UpdateTable();
							}
						}
						this.StrongTypeEditor.StrongType = value;
					}
				}
				finally
				{
					this.isLoadingData = false;
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0003757C File Offset: 0x0003577C
		public bool AllowUpdateGUI
		{
			get
			{
				return !this.suppressUpdateGUI;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00037587 File Offset: 0x00035787
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x000375A9 File Offset: 0x000357A9
		public bool IsOpenedAsEdit
		{
			get
			{
				return (bool)this.table.Rows[0]["IsEditMode"];
			}
			set
			{
				this.table.Rows[0]["IsEditMode"] = value;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000375CC File Offset: 0x000357CC
		private void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
		{
			this.table.Rows[0].EndEdit();
			if (this.isLoadingData)
			{
				return;
			}
			if (this.strongTypeEditor.IsHandleCreated && !e.Column.ColumnName.Equals("IsEditMode"))
			{
				this.CheckError(e.Column.ColumnName);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00037630 File Offset: 0x00035830
		private void CheckError(string propertyName)
		{
			ValidationError[] array = this.Validate();
			if (array.Length == 1)
			{
				StrongTypeValidationError strongTypeValidationError = array[0] as StrongTypeValidationError;
				bool isTargetProperty = strongTypeValidationError != null && strongTypeValidationError.PropertyName.Equals(propertyName);
				throw new StrongTypeException(strongTypeValidationError.Description, isTargetProperty);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00037678 File Offset: 0x00035878
		public ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			try
			{
				this.suppressUpdateGUI = true;
				this.UpdateStrongType();
			}
			catch (ArgumentException ex)
			{
				list.Add(new StrongTypeValidationError(new LocalizedString(ex.Message), ex.ParamName));
			}
			catch (StrongTypeFormatException ex2)
			{
				list.Add(new StrongTypeValidationError(new LocalizedString(ex2.Message), ex2.ParamName));
			}
			catch (Exception ex3)
			{
				list.Add(new StrongTypeValidationError(new LocalizedString(ex3.Message), string.Empty));
			}
			finally
			{
				this.suppressUpdateGUI = false;
			}
			return list.ToArray();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003773C File Offset: 0x0003593C
		private void SetAsDefaultValue(DataTable table)
		{
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				table.Rows[0][dataColumn] = dataColumn.DefaultValue;
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000377A8 File Offset: 0x000359A8
		protected virtual void CreateColumn(DataTable table)
		{
			DataColumn dataColumn = new DataColumn("IsEditMode", typeof(bool));
			dataColumn.DefaultValue = false;
			table.Columns.Add(dataColumn);
			table.Columns.AddRange(this.schemaLoader.LoadDataColumns(this.bindingMapping).ToArray());
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00037804 File Offset: 0x00035A04
		protected virtual void UpdateStrongType()
		{
			int count = this.schemaLoader.ArgumentList.Count;
			Type[] array = new Type[count];
			object[] array2 = new object[count];
			int num = 0;
			foreach (string text in this.schemaLoader.ArgumentList)
			{
				array[num] = this.table.Columns[text].DataType;
				array2[num++] = (DBNull.Value.Equals(this.table.Rows[0][text]) ? null : this.table.Rows[0][text]);
			}
			ConstructorInfo constructor = typeof(T).GetConstructor(array);
			try
			{
				this.StrongType = (T)((object)constructor.Invoke(array2));
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00037918 File Offset: 0x00035B18
		protected virtual void UpdateTable()
		{
			foreach (string text in this.bindingMapping)
			{
				PropertyInfo property = typeof(T).GetProperty(text);
				this.Table.Rows[0][text] = (property.GetValue(this.StrongType, null) ?? DBNull.Value);
			}
		}

		// Token: 0x040005D8 RID: 1496
		private const string IsEditMode = "IsEditMode";

		// Token: 0x040005D9 RID: 1497
		private StrongTypeEditor<T> strongTypeEditor;

		// Token: 0x040005DA RID: 1498
		private DataTable table;

		// Token: 0x040005DB RID: 1499
		private StrongTypeSchemaLoader schemaLoader;

		// Token: 0x040005DC RID: 1500
		private T strongType;

		// Token: 0x040005DD RID: 1501
		private bool suppressUpdateGUI;

		// Token: 0x040005DE RID: 1502
		private bool isLoadingData;

		// Token: 0x040005DF RID: 1503
		private List<string> bindingMapping = new List<string>();
	}
}
