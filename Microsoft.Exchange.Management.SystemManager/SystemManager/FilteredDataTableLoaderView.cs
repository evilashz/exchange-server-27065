using System;
using System.ComponentModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200001C RID: 28
	public class FilteredDataTableLoaderView : DataTableLoaderView
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000785B File Offset: 0x00005A5B
		public FilteredDataTableLoaderView()
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007863 File Offset: 0x00005A63
		public FilteredDataTableLoaderView(DataTableLoader dataTableLoader, IObjectComparer defaultObjectComparer, ObjectToFilterableConverter defaultObjectToFilterableConverter) : base(dataTableLoader, defaultObjectComparer, defaultObjectToFilterableConverter)
		{
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000786E File Offset: 0x00005A6E
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00007876 File Offset: 0x00005A76
		[DefaultValue(null)]
		public QueryFilter PermanentFilter
		{
			get
			{
				return this.permanentFilter;
			}
			set
			{
				if (this.PermanentFilter != value)
				{
					this.permanentFilter = value;
					this.ApplyFilter(this.additionalFilter);
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007894 File Offset: 0x00005A94
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000789C File Offset: 0x00005A9C
		[DefaultValue(null)]
		public QueryFilter MasterFilter
		{
			get
			{
				return this.masterFilter;
			}
			set
			{
				if (this.MasterFilter != value)
				{
					this.masterFilter = value;
					this.ApplyFilter(this.additionalFilter);
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000078BC File Offset: 0x00005ABC
		public override void ApplyFilter(QueryFilter filter)
		{
			this.additionalFilter = filter;
			if (this.PermanentFilter != null)
			{
				if (filter == null)
				{
					filter = this.PermanentFilter;
				}
				else
				{
					filter = new AndFilter(new QueryFilter[]
					{
						filter,
						this.PermanentFilter
					});
				}
			}
			if (this.MasterFilter != null)
			{
				if (filter == null)
				{
					filter = this.MasterFilter;
				}
				else
				{
					filter = new AndFilter(new QueryFilter[]
					{
						filter,
						this.MasterFilter
					});
				}
			}
			base.ApplyFilter(filter);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007939 File Offset: 0x00005B39
		public new static FilteredDataTableLoaderView Create(DataTableLoader dataTableLoader)
		{
			return new FilteredDataTableLoaderView(dataTableLoader, ObjectComparer.DefaultObjectComparer, ObjectToFilterableConverter.DefaultObjectToFilterableConverter);
		}

		// Token: 0x0400006C RID: 108
		private QueryFilter permanentFilter;

		// Token: 0x0400006D RID: 109
		private QueryFilter masterFilter;

		// Token: 0x0400006E RID: 110
		private QueryFilter additionalFilter;
	}
}
