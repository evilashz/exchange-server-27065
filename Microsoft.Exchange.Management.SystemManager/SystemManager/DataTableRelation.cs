using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000014 RID: 20
	public class DataTableRelation
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000055FC File Offset: 0x000037FC
		public DataTableRelation(string relationName, DataTableLoaderConfiguration parentDataTableLoaderConfiguration, string parentColumn, DataTableLoaderConfiguration childDataTableLoaderConfiguration, string childColumn)
		{
			if (string.IsNullOrEmpty(relationName))
			{
				throw new ArgumentException("relationName can not be null or empty.", "relationName");
			}
			if (parentDataTableLoaderConfiguration == null)
			{
				throw new ArgumentNullException("parentDataTableLoaderConfiguration");
			}
			if (string.IsNullOrEmpty(parentColumn))
			{
				throw new ArgumentException("parentColumn can not be null or empty.", "parentColumn");
			}
			if (childDataTableLoaderConfiguration == null)
			{
				throw new ArgumentNullException("childDataTableLoaderConfiguration");
			}
			if (string.IsNullOrEmpty(childColumn))
			{
				throw new ArgumentException("childColumn can not be null or empty.", "childColumn");
			}
			this.RelationName = relationName;
			this.ParentDataTableLoaderConfiguration = parentDataTableLoaderConfiguration;
			this.ParentColumn = parentColumn;
			this.ChildDataTableLoaderConfiguration = childDataTableLoaderConfiguration;
			this.ChildColumn = childColumn;
			this.Nested = true;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000056A1 File Offset: 0x000038A1
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000056A9 File Offset: 0x000038A9
		public string RelationName { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000056B2 File Offset: 0x000038B2
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000056BA File Offset: 0x000038BA
		public DataTableLoaderConfiguration ParentDataTableLoaderConfiguration { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000056C3 File Offset: 0x000038C3
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000056CB File Offset: 0x000038CB
		public string ParentColumn { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000056D4 File Offset: 0x000038D4
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000056DC File Offset: 0x000038DC
		public DataTableLoaderConfiguration ChildDataTableLoaderConfiguration { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000056E5 File Offset: 0x000038E5
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000056ED File Offset: 0x000038ED
		public string ChildColumn { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000056F6 File Offset: 0x000038F6
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000056FE File Offset: 0x000038FE
		public bool Nested { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005707 File Offset: 0x00003907
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000570F File Offset: 0x0000390F
		public DataTableLoaderSet DataTableLoaderSet
		{
			get
			{
				return this.dataTableLoaderSet;
			}
			set
			{
				if (this.DataTableLoaderSet != value)
				{
					if (this.DataTableLoaderSet != null)
					{
						throw new InvalidOperationException("Can not change the DataTableLoaderSet once it is setted.");
					}
					this.dataTableLoaderSet = value;
					this.dataTableLoaderSet.AddDataTableRelation(this);
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005740 File Offset: 0x00003940
		public bool Appliable
		{
			get
			{
				return this.DataTableLoaderSet != null && this.ParentDataTableLoaderConfiguration.DataTableLoader != null && this.ChildDataTableLoaderConfiguration.DataTableLoader != null;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000576C File Offset: 0x0000396C
		public void Apply()
		{
			if (!this.DataTableLoaderSet.DataSet.Relations.Contains(this.RelationName))
			{
				DataRelation dataRelation = new DataRelation(this.RelationName, this.ParentDataTableLoaderConfiguration.DataTable.Columns[this.ParentColumn], this.ChildDataTableLoaderConfiguration.DataTableLoader.Columns[this.ChildColumn], false);
				dataRelation.Nested = this.Nested;
				this.DataTableLoaderSet.DataSet.Relations.Add(dataRelation);
			}
		}

		// Token: 0x04000042 RID: 66
		private DataTableLoaderSet dataTableLoaderSet;
	}
}
