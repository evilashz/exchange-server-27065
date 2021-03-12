using System;
using System.Collections;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002D7 RID: 727
	public class ViewDescriptor
	{
		// Token: 0x06001C0F RID: 7183 RVA: 0x000A1750 File Offset: 0x0009F950
		public ViewDescriptor(ColumnId defaultSortColumn, bool isFixedWidth, params ColumnId[] columns)
		{
			if (columns != null && columns.Length == 0)
			{
				throw new ArgumentException("columns can not be null or empty array");
			}
			this.defaultSortColumn = defaultSortColumn;
			this.columns = columns;
			this.isFixedWidth = isFixedWidth;
			this.columnWidth = new float[columns.Length];
			for (int i = 0; i < columns.Length; i++)
			{
				Column column = ListViewColumns.GetColumn(columns[i]);
				if (isFixedWidth || !column.IsFixedWidth)
				{
					this.width += column.Width;
				}
			}
			float num = 0f;
			Column column2 = ListViewColumns.GetColumn(columns[columns.Length - 1]);
			for (int j = 0; j < columns.Length; j++)
			{
				Column column3 = ListViewColumns.GetColumn(columns[j]);
				if (!isFixedWidth && column3.IsFixedWidth)
				{
					this.columnWidth[j] = (float)((column3.Width < 1) ? 1 : column3.Width);
				}
				else
				{
					this.columnWidth[j] = ((this.width != 0) ? ((float)column3.Width * 100f / (float)this.width) : 0f);
					float num2 = (float)Math.Round((double)this.columnWidth[j], 1);
					if (column3.Id == ColumnId.ContactIcon && num2 < this.columnWidth[j])
					{
						num2 += 0.1f;
					}
					this.columnWidth[j] = ((num2 < 1f) ? 1f : num2);
					if (j == columns.Length - 2)
					{
						if (!isFixedWidth && column2.IsFixedWidth)
						{
							this.columnWidth[j] = 100f - num;
						}
					}
					else if (j == columns.Length - 1)
					{
						this.columnWidth[j] = 100f - num;
					}
					num += this.columnWidth[j];
				}
			}
			Hashtable hashtable = new Hashtable();
			foreach (ColumnId columnId in columns)
			{
				Column column4 = ListViewColumns.GetColumn(columnId);
				for (int l = 0; l < column4.PropertyCount; l++)
				{
					if (!hashtable.ContainsKey(column4[l]))
					{
						hashtable.Add(column4[l], null);
					}
				}
			}
			this.properties = new PropertyDefinition[hashtable.Count];
			int num3 = 0;
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.properties[num3++] = (PropertyDefinition)enumerator.Key;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x000A19A0 File Offset: 0x0009FBA0
		public int ColumnCount
		{
			get
			{
				return this.columns.Length;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000A19AA File Offset: 0x0009FBAA
		public int PropertyCount
		{
			get
			{
				return this.properties.Length;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x000A19B4 File Offset: 0x0009FBB4
		public ColumnId DefaultSortColumn
		{
			get
			{
				return this.defaultSortColumn;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x000A19BC File Offset: 0x0009FBBC
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x000A19C4 File Offset: 0x0009FBC4
		public bool IsFixedWidth
		{
			get
			{
				return this.isFixedWidth;
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000A19CC File Offset: 0x0009FBCC
		public float GetColumnWidth(int columnIndex)
		{
			return this.columnWidth[columnIndex];
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000A19D6 File Offset: 0x0009FBD6
		public PropertyDefinition GetProperty(int propertyIndex)
		{
			return this.properties[propertyIndex];
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000A19E0 File Offset: 0x0009FBE0
		public ColumnId GetColumn(int columnIndex)
		{
			return this.columns[columnIndex];
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000A19EC File Offset: 0x0009FBEC
		public bool ContainsColumn(ColumnId columnId)
		{
			foreach (ColumnId columnId2 in this.columns)
			{
				if (columnId2 == columnId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040014BD RID: 5309
		private ColumnId[] columns;

		// Token: 0x040014BE RID: 5310
		private ColumnId defaultSortColumn;

		// Token: 0x040014BF RID: 5311
		private bool isFixedWidth;

		// Token: 0x040014C0 RID: 5312
		private int width;

		// Token: 0x040014C1 RID: 5313
		private float[] columnWidth;

		// Token: 0x040014C2 RID: 5314
		private PropertyDefinition[] properties;
	}
}
