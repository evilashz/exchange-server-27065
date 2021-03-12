using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200001A RID: 26
	public class FilterSupportDescription
	{
		// Token: 0x06000192 RID: 402 RVA: 0x000073FC File Offset: 0x000055FC
		public FilterSupportDescription(string columnName)
		{
			if (string.IsNullOrEmpty(columnName))
			{
				throw new ArgumentException();
			}
			this.ColumnName = columnName;
			this.FilteredColumnReferenceNumberProperty = string.Format("ColumnName_{0}_FilteredColumn_ReferenceNumber", this.ColumnName);
			this.FilteredColumnName = string.Empty;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000743A File Offset: 0x0000563A
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00007442 File Offset: 0x00005642
		public string ColumnName { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000744B File Offset: 0x0000564B
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00007453 File Offset: 0x00005653
		public string FilteredColumnName { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000745C File Offset: 0x0000565C
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00007464 File Offset: 0x00005664
		public IFilterableConverter FilterableConverter { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000746D File Offset: 0x0000566D
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007475 File Offset: 0x00005675
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000074A3 File Offset: 0x000056A3
		public DataTableLoader DataTableLoader
		{
			get
			{
				return this.DataTableLoaderView.DataTableLoader;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000074B0 File Offset: 0x000056B0
		public ObjectToFilterableConverter DefaultObjectToFilterableConverter
		{
			get
			{
				return this.DataTableLoaderView.DefaultObjectToFilterableConverter;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000074BD File Offset: 0x000056BD
		private void AttachDataTableLoaderView()
		{
			this.AttachFilteredColumn();
			this.DataTableLoader.Table.Columns.CollectionChanged += this.DataColumns_CollectionChanged;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000074E8 File Offset: 0x000056E8
		private void AttachFilteredColumn()
		{
			if (this.DataTableLoader.Table.Columns.Contains(this.ColumnName))
			{
				Type dataType = this.DataTableLoader.Table.Columns[this.ColumnName].DataType;
				if (this.DefaultObjectToFilterableConverter.ShouldUseStandardFiltering(dataType))
				{
					this.FilteredColumnName = this.ColumnName;
					this.FilterableConverter = null;
					return;
				}
				string filterSupportDescriptionNameWithPostfix = this.GetFilterSupportDescriptionNameWithPostfix();
				IFilterableConverter filterableConverter;
				if (!this.DataTableLoader.Table.Columns.Contains(filterSupportDescriptionNameWithPostfix))
				{
					DataColumn dataColumn = new DataColumn(filterSupportDescriptionNameWithPostfix);
					dataColumn.DataType = typeof(IConvertible);
					dataColumn.ExtendedProperties[this.FilteredColumnReferenceNumberProperty] = 0;
					filterableConverter = this.DefaultObjectToFilterableConverter;
					dataColumn.ExtendedProperties["ColumnValueCalculator"] = new ColumnValueCalculator(this.ColumnName, filterSupportDescriptionNameWithPostfix, new FilterSupportDescription.FilterableObjectTypeConverter(filterableConverter));
					this.DataTableLoader.Table.Columns.Add(dataColumn);
				}
				else
				{
					filterableConverter = ((this.DataTableLoader.Table.Columns[filterSupportDescriptionNameWithPostfix].ExtendedProperties["ColumnValueCalculator"] as ColumnValueCalculator).TypeConverter as FilterSupportDescription.FilterableObjectTypeConverter).FilterableConverter;
				}
				this.DataTableLoader.Table.Columns[filterSupportDescriptionNameWithPostfix].ExtendedProperties[this.FilteredColumnReferenceNumberProperty] = (int)this.DataTableLoader.Table.Columns[filterSupportDescriptionNameWithPostfix].ExtendedProperties[this.FilteredColumnReferenceNumberProperty] + 1;
				this.FilterableConverter = filterableConverter;
				this.FilteredColumnName = filterSupportDescriptionNameWithPostfix;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007688 File Offset: 0x00005888
		private void DetachDataTableLoaderView()
		{
			this.DataTableLoader.Table.Columns.CollectionChanged -= this.DataColumns_CollectionChanged;
			this.DetachFilteredColumn();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000076B4 File Offset: 0x000058B4
		private void DetachFilteredColumn()
		{
			if (this.FilteredColumnName != this.ColumnName && this.DataTableLoader.Table.Columns.Contains(this.FilteredColumnName))
			{
				DataColumn dataColumn = this.DataTableLoader.Table.Columns[this.FilteredColumnName];
				dataColumn.ExtendedProperties[this.FilteredColumnReferenceNumberProperty] = (int)dataColumn.ExtendedProperties[this.FilteredColumnReferenceNumberProperty] - 1;
				if ((int)dataColumn.ExtendedProperties[this.FilteredColumnReferenceNumberProperty] == 0)
				{
					this.DataTableLoader.Table.Columns.Remove(this.FilteredColumnName);
				}
				this.FilteredColumnName = string.Empty;
				this.FilterableConverter = null;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007788 File Offset: 0x00005988
		private void DataColumns_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add)
			{
				DataColumn dataColumn = e.Element as DataColumn;
				if (string.Compare(dataColumn.ColumnName, this.ColumnName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AttachFilteredColumn();
					return;
				}
			}
			else if (e.Action == CollectionChangeAction.Remove)
			{
				DataColumn dataColumn2 = e.Element as DataColumn;
				if (string.Compare(dataColumn2.ColumnName, this.ColumnName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.DetachFilteredColumn();
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000077F4 File Offset: 0x000059F4
		private string GetFilterSupportDescriptionNameWithPostfix()
		{
			return string.Format("ColumnName_{0}_TableName_{1}_FilteredColumnName_Number_{2}", this.ColumnName, this.DataTableLoader.Table.TableName, 0);
		}

		// Token: 0x04000066 RID: 102
		private DataTableLoaderView dataTableLoaderView;

		// Token: 0x04000067 RID: 103
		private readonly string FilteredColumnReferenceNumberProperty;

		// Token: 0x0200001B RID: 27
		private class FilterableObjectTypeConverter : TypeConverter
		{
			// Token: 0x060001A3 RID: 419 RVA: 0x0000781C File Offset: 0x00005A1C
			public FilterableObjectTypeConverter(IFilterableConverter filterableConverter)
			{
				this.FilterableConverter = filterableConverter;
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000782B File Offset: 0x00005A2B
			// (set) Token: 0x060001A5 RID: 421 RVA: 0x00007833 File Offset: 0x00005A33
			public IFilterableConverter FilterableConverter { get; private set; }

			// Token: 0x060001A6 RID: 422 RVA: 0x0000783C File Offset: 0x00005A3C
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return true;
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x0000783F File Offset: 0x00005A3F
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				return this.FilterableConverter.ToFilterable(value);
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x0000784D File Offset: 0x00005A4D
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x00007854 File Offset: 0x00005A54
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				throw new NotSupportedException();
			}
		}
	}
}
