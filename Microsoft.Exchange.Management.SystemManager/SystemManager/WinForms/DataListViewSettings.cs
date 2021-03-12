using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000128 RID: 296
	[SettingsProvider(typeof(ExchangeSettingsProvider))]
	public class DataListViewSettings : ExchangeSettings
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0002A7E5 File Offset: 0x000289E5
		public DataListViewSettings(IComponent owner) : base(owner)
		{
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002A7EE File Offset: 0x000289EE
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0002A800 File Offset: 0x00028A00
		[UserScopedSetting]
		[DefaultSettingValue("")]
		public Hashtable DataListViewInfo
		{
			get
			{
				return (Hashtable)this["DataListViewInfo"];
			}
			set
			{
				this["DataListViewInfo"] = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0002A80E File Offset: 0x00028A0E
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0002A820 File Offset: 0x00028A20
		[DefaultSettingValue(null)]
		[UserScopedSetting]
		public byte[] FilterExpression
		{
			get
			{
				return (byte[])this["FilterExpression"];
			}
			set
			{
				this["FilterExpression"] = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002A82E File Offset: 0x00028A2E
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0002A840 File Offset: 0x00028A40
		[UserScopedSetting]
		[DefaultSettingValue("25")]
		public int ResultsPerPage
		{
			get
			{
				return (int)this["ResultsPerPage"];
			}
			set
			{
				this["ResultsPerPage"] = value;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002A854 File Offset: 0x00028A54
		public void SaveDataListViewSettings(DataListView listView)
		{
			if (listView == null)
			{
				throw new ArgumentNullException();
			}
			int count = listView.Columns.Count;
			DataListViewSettings.SerializableColumnInfo[] array = new DataListViewSettings.SerializableColumnInfo[count];
			for (int i = 0; i < count; i++)
			{
				array[listView.Columns[i].DisplayIndex] = new DataListViewSettings.SerializableColumnInfo(listView.Columns[i].Name, listView.Columns[i].Width, listView.Columns[i].DisplayIndex);
			}
			if (this.DataListViewInfo == null)
			{
				this.DataListViewInfo = new Hashtable();
			}
			this.DataListViewInfo[listView.Name] = new DataListViewSettings.SerializableDataListViewInfo(array, listView.SortDirection, listView.SortProperty, listView.IsColumnsWidthDirty);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002A91C File Offset: 0x00028B1C
		public void LoadDataListViewSettings(DataListView listView)
		{
			if (listView == null)
			{
				throw new ArgumentNullException();
			}
			listView.BeginUpdate();
			DataListViewSettings.SerializableDataListViewInfo serializableDataListViewInfo = this.FindSuitableSetting(listView);
			if (serializableDataListViewInfo == null)
			{
				listView.SortDirection = ListSortDirection.Ascending;
			}
			else
			{
				listView.SortDirection = serializableDataListViewInfo.SortDirection;
				listView.SortProperty = serializableDataListViewInfo.SortProperty;
				listView.IsColumnsWidthDirty = serializableDataListViewInfo.IsColumnsWidthDirty;
				int length = serializableDataListViewInfo.Columns.GetLength(0);
				ArrayList arrayList = new ArrayList(length);
				int num = 0;
				for (int i = 0; i < length; i++)
				{
					if (!string.IsNullOrEmpty(serializableDataListViewInfo.Columns[i].ColumnName))
					{
						ExchangeColumnHeader exchangeColumnHeader = listView.AvailableColumns[serializableDataListViewInfo.Columns[i].ColumnName];
						exchangeColumnHeader.Visible = true;
						exchangeColumnHeader.Width = serializableDataListViewInfo.Columns[i].ColumnWidth;
						exchangeColumnHeader.DisplayIndex = num++;
						arrayList.Add(exchangeColumnHeader);
					}
				}
				if (arrayList.Count > 0)
				{
					foreach (ExchangeColumnHeader exchangeColumnHeader2 in listView.AvailableColumns)
					{
						exchangeColumnHeader2.Visible = arrayList.Contains(exchangeColumnHeader2);
					}
				}
			}
			listView.EndUpdate();
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002AA64 File Offset: 0x00028C64
		private DataListViewSettings.SerializableDataListViewInfo FindSuitableSetting(DataListView listView)
		{
			if (this.DataListViewInfo == null || this.DataListViewInfo[listView.Name] == null)
			{
				return null;
			}
			DataListViewSettings.SerializableDataListViewInfo serializableDataListViewInfo = (DataListViewSettings.SerializableDataListViewInfo)this.DataListViewInfo[listView.Name];
			if (listView.AvailableColumns[serializableDataListViewInfo.SortProperty] == null)
			{
				return null;
			}
			int length = serializableDataListViewInfo.Columns.GetLength(0);
			if (length > listView.AvailableColumns.Count)
			{
				return null;
			}
			for (int i = 0; i < length; i++)
			{
				if (!string.IsNullOrEmpty(serializableDataListViewInfo.Columns[i].ColumnName) && listView.AvailableColumns[serializableDataListViewInfo.Columns[i].ColumnName] == null)
				{
					return null;
				}
			}
			return serializableDataListViewInfo;
		}

		// Token: 0x02000129 RID: 297
		[Serializable]
		public class SerializableDataListViewInfo
		{
			// Token: 0x06000BD7 RID: 3031 RVA: 0x0002AB1C File Offset: 0x00028D1C
			public SerializableDataListViewInfo(DataListViewSettings.SerializableColumnInfo[] cols, ListSortDirection sortDir, string sortProp, bool columnDirty)
			{
				this.columns = cols;
				this.sortDirection = sortDir;
				this.sortProperty = sortProp;
				this.isColumnsWidthDirty = columnDirty;
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0002AB41 File Offset: 0x00028D41
			// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0002AB49 File Offset: 0x00028D49
			public DataListViewSettings.SerializableColumnInfo[] Columns
			{
				get
				{
					return this.columns;
				}
				set
				{
					this.columns = value;
				}
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002AB52 File Offset: 0x00028D52
			// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0002AB5A File Offset: 0x00028D5A
			public string SortProperty
			{
				get
				{
					return this.sortProperty;
				}
				set
				{
					this.sortProperty = value;
				}
			}

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0002AB63 File Offset: 0x00028D63
			// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0002AB6B File Offset: 0x00028D6B
			public ListSortDirection SortDirection
			{
				get
				{
					return this.sortDirection;
				}
				set
				{
					this.sortDirection = value;
				}
			}

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002AB74 File Offset: 0x00028D74
			// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0002AB7C File Offset: 0x00028D7C
			public bool IsColumnsWidthDirty
			{
				get
				{
					return this.isColumnsWidthDirty;
				}
				set
				{
					this.isColumnsWidthDirty = value;
				}
			}

			// Token: 0x040004D4 RID: 1236
			private DataListViewSettings.SerializableColumnInfo[] columns;

			// Token: 0x040004D5 RID: 1237
			private ListSortDirection sortDirection;

			// Token: 0x040004D6 RID: 1238
			private string sortProperty;

			// Token: 0x040004D7 RID: 1239
			private bool isColumnsWidthDirty;
		}

		// Token: 0x0200012A RID: 298
		[Serializable]
		public struct SerializableColumnInfo
		{
			// Token: 0x06000BE0 RID: 3040 RVA: 0x0002AB85 File Offset: 0x00028D85
			public SerializableColumnInfo(string name, int width, int index)
			{
				this.columnName = name;
				this.columnWidth = width;
				this.displayIndex = index;
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002AB9C File Offset: 0x00028D9C
			// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0002ABA4 File Offset: 0x00028DA4
			public int ColumnWidth
			{
				get
				{
					return this.columnWidth;
				}
				set
				{
					this.columnWidth = value;
				}
			}

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002ABAD File Offset: 0x00028DAD
			// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0002ABB5 File Offset: 0x00028DB5
			public string ColumnName
			{
				get
				{
					return this.columnName;
				}
				set
				{
					this.columnName = value;
				}
			}

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002ABBE File Offset: 0x00028DBE
			// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0002ABC6 File Offset: 0x00028DC6
			public int DisplayIndex
			{
				get
				{
					return this.displayIndex;
				}
				set
				{
					this.displayIndex = value;
				}
			}

			// Token: 0x040004D8 RID: 1240
			private int columnWidth;

			// Token: 0x040004D9 RID: 1241
			private string columnName;

			// Token: 0x040004DA RID: 1242
			private int displayIndex;
		}
	}
}
