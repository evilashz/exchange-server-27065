using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000047 RID: 71
	public class MultiValuedPropertyDataTableLoader : DataTableLoader
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A844 File Offset: 0x00008A44
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000A84C File Offset: 0x00008A4C
		[DefaultValue(null)]
		public MultiValuedPropertyBase Mvp
		{
			get
			{
				return this.mvp;
			}
			set
			{
				this.mvp = value;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A855 File Offset: 0x00008A55
		public MultiValuedPropertyDataTableLoader(string columnTitle, MultiValuedPropertyBase mvp)
		{
			base.Table = new DataTable();
			base.Table.Columns.Add(columnTitle);
			this.Mvp = mvp;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A881 File Offset: 0x00008A81
		public MultiValuedPropertyDataTableLoader()
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A88C File Offset: 0x00008A8C
		protected override void OnFillTable(RefreshRequestEventArgs e)
		{
			DataTable dataTable = (DataTable)e.Result;
			if (this.Mvp != null)
			{
				foreach (object obj in ((IEnumerable)this.Mvp))
				{
					dataTable.Rows.Add(new object[]
					{
						obj
					});
				}
			}
			base.OnFillTable(e);
		}

		// Token: 0x040000C3 RID: 195
		private MultiValuedPropertyBase mvp;
	}
}
