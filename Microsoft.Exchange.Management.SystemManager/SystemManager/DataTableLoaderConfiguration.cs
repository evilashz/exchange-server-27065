using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000011 RID: 17
	public class DataTableLoaderConfiguration
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x0000545E File Offset: 0x0000365E
		public DataTableLoaderConfiguration(string name, IDataTableLoaderCreator dataTableLoaderCreator)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name can not be null or empty.", "name");
			}
			if (dataTableLoaderCreator == null)
			{
				throw new ArgumentNullException("dataTableLoaderCreator");
			}
			this.Name = name;
			this.DataTableLoaderCreator = dataTableLoaderCreator;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000549A File Offset: 0x0000369A
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000054A2 File Offset: 0x000036A2
		public string Name { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000054AB File Offset: 0x000036AB
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000054B3 File Offset: 0x000036B3
		public IDataTableLoaderCreator DataTableLoaderCreator { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000054C4 File Offset: 0x000036C4
		public DataTableLoader DataTableLoader { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000054CD File Offset: 0x000036CD
		public DataTable DataTable
		{
			get
			{
				if (this.DataTableLoader != null)
				{
					return this.DataTableLoader.Table;
				}
				return null;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000054E4 File Offset: 0x000036E4
		public void CreateDataTableLoader()
		{
			if (this.DataTableLoader != null)
			{
				throw new InvalidOperationException("The DataTableLoader has been created.");
			}
			this.DataTableLoader = this.DataTableLoaderCreator.CreateDataTableLoader(this.Name);
		}
	}
}
