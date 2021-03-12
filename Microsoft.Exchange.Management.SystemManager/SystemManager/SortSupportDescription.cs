using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000019 RID: 25
	public class SortSupportDescription
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00006E70 File Offset: 0x00005070
		public SortSupportDescription(string columnName, SortMode sortMode, IComparer comparer, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText)
		{
			if (string.IsNullOrEmpty(columnName))
			{
				throw new ArgumentException();
			}
			this.ColumnName = columnName;
			this.SortedColumnReferenceNumberProperty = string.Format("ColumnName_{0}_SortedColumn_ReferenceNumber", this.ColumnName);
			this.SortMode = sortMode;
			this.Comparer = comparer;
			this.CustomFormatter = customFormatter;
			this.FormatProvider = formatProvider;
			this.FormatString = formatString;
			this.DefaultEmptyText = defaultEmptyText;
			this.Comparer = null;
			this.SortedColumnName = string.Empty;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006EEE File Offset: 0x000050EE
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00006EF6 File Offset: 0x000050F6
		public string ColumnName { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00006EFF File Offset: 0x000050FF
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00006F07 File Offset: 0x00005107
		public SortMode SortMode { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006F10 File Offset: 0x00005110
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006F18 File Offset: 0x00005118
		public IComparer Comparer { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006F21 File Offset: 0x00005121
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006F29 File Offset: 0x00005129
		public ICustomFormatter CustomFormatter { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006F32 File Offset: 0x00005132
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00006F3A File Offset: 0x0000513A
		public IFormatProvider FormatProvider { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006F43 File Offset: 0x00005143
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00006F4B File Offset: 0x0000514B
		public string FormatString { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006F54 File Offset: 0x00005154
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00006F5C File Offset: 0x0000515C
		public string DefaultEmptyText { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006F65 File Offset: 0x00005165
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00006F6D File Offset: 0x0000516D
		public string SortedColumnName { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006F76 File Offset: 0x00005176
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00006F7E File Offset: 0x0000517E
		public DataTableLoaderView DataTableLoaderView
		{
			get
			{
				return this.dataTableLoaderView;
			}
			internal set
			{
				if (this.DataTableLoaderView != value)
				{
					if (this.DataTableLoaderView != null)
					{
						this.DetachDataTableLoaderView();
					}
					this.dataTableLoaderView = value;
					if (this.DataTableLoaderView != null)
					{
						this.AttachDataTableLoaderView();
					}
				}
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006FAC File Offset: 0x000051AC
		public DataTableLoader DataTableLoader
		{
			get
			{
				return this.DataTableLoaderView.DataTableLoader;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006FB9 File Offset: 0x000051B9
		public IObjectComparer DefaultObjectComparer
		{
			get
			{
				return this.DataTableLoaderView.DefaultObjectComparer;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006FC6 File Offset: 0x000051C6
		public ITextComparer DefaultTextComparer
		{
			get
			{
				return this.DataTableLoaderView.DefaultTextComparer;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006FD3 File Offset: 0x000051D3
		private void AttachDataTableLoaderView()
		{
			if (this.SortMode == SortMode.NotSupported)
			{
				return;
			}
			this.AttachSortedColumn();
			this.DataTableLoader.Table.Columns.CollectionChanged += this.DataColumns_CollectionChanged;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007006 File Offset: 0x00005206
		private void DetachDataTableLoaderView()
		{
			if (this.SortMode == SortMode.NotSupported)
			{
				return;
			}
			this.DataTableLoader.Table.Columns.CollectionChanged -= this.DataColumns_CollectionChanged;
			this.DetachSortedColumn();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000703C File Offset: 0x0000523C
		private void AttachSortedColumn()
		{
			if (this.DataTableLoader.Table.Columns.Contains(this.ColumnName))
			{
				if (this.SortMode == SortMode.NotSpecified)
				{
					this.SortMode = this.DefaultObjectComparer.GetSortMode(this.DataTableLoader.Table.Columns[this.ColumnName].DataType);
				}
				if (this.SortMode == SortMode.Standard)
				{
					this.SortedColumnName = this.ColumnName;
					return;
				}
				if (this.SortMode == SortMode.DelegateColumn)
				{
					string text = this.DataTableLoader.Table.Columns[this.ColumnName].ExtendedProperties["DelegateColumnName"] as string;
					if (this.DefaultObjectComparer.GetSortMode(this.DataTableLoader.Table.Columns[text].DataType) == SortMode.Standard)
					{
						this.SortedColumnName = text;
						return;
					}
					this.SortedColumnName = this.ColumnName;
					return;
				}
				else
				{
					if (this.SortMode == SortMode.Text)
					{
						this.Comparer = new SupportTextComparerAdapter(this.DefaultTextComparer, this.CustomFormatter, this.FormatProvider, this.FormatString, this.DefaultEmptyText);
					}
					if (this.SortMode == SortMode.Custom)
					{
						this.Comparer = new SupportTextComparerAdapter(this.DefaultObjectComparer, this.CustomFormatter, this.FormatProvider, this.FormatString, this.DefaultEmptyText);
					}
					this.SortedColumnName = this.GetSortSupportDescriptionNameWithPostfix();
					if (!this.DataTableLoader.Table.Columns.Contains(this.SortedColumnName))
					{
						DataColumn dataColumn = new DataColumn(this.SortedColumnName);
						dataColumn.DataType = typeof(IComparable);
						dataColumn.ExtendedProperties[this.SortedColumnReferenceNumberProperty] = 0;
						dataColumn.ExtendedProperties["ColumnValueCalculator"] = new ColumnValueCalculator(this.ColumnName, this.SortedColumnName, new ComparableTypeConverter(this.Comparer));
						this.DataTableLoader.Table.Columns.Add(dataColumn);
					}
					this.DataTableLoader.Table.Columns[this.SortedColumnName].ExtendedProperties[this.SortedColumnReferenceNumberProperty] = (int)this.DataTableLoader.Table.Columns[this.SortedColumnName].ExtendedProperties[this.SortedColumnReferenceNumberProperty] + 1;
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007294 File Offset: 0x00005494
		private void DetachSortedColumn()
		{
			if (this.SortedColumnName != this.ColumnName && this.DataTableLoader.Table.Columns.Contains(this.SortedColumnName))
			{
				DataColumn dataColumn = this.DataTableLoader.Table.Columns[this.SortedColumnName];
				dataColumn.ExtendedProperties[this.SortedColumnReferenceNumberProperty] = (int)dataColumn.ExtendedProperties[this.SortedColumnReferenceNumberProperty] - 1;
				if ((int)dataColumn.ExtendedProperties[this.SortedColumnReferenceNumberProperty] == 0)
				{
					this.DataTableLoader.Table.Columns.Remove(this.SortedColumnName);
				}
				this.Comparer = null;
				this.SortedColumnName = string.Empty;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007368 File Offset: 0x00005568
		private void DataColumns_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add)
			{
				DataColumn dataColumn = e.Element as DataColumn;
				if (string.Compare(dataColumn.ColumnName, this.ColumnName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AttachSortedColumn();
					return;
				}
			}
			else if (e.Action == CollectionChangeAction.Remove)
			{
				DataColumn dataColumn2 = e.Element as DataColumn;
				if (string.Compare(dataColumn2.ColumnName, this.ColumnName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.DetachSortedColumn();
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000073D4 File Offset: 0x000055D4
		private string GetSortSupportDescriptionNameWithPostfix()
		{
			return string.Format("ColumnName_{0}_TableName_{1}_SortedColumnName_Number_{2}", this.ColumnName, this.DataTableLoader.Table.TableName, 0);
		}

		// Token: 0x0400005C RID: 92
		private DataTableLoaderView dataTableLoaderView;

		// Token: 0x0400005D RID: 93
		private readonly string SortedColumnReferenceNumberProperty;
	}
}
