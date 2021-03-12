using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000205 RID: 517
	internal struct CsvColumnMap
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x000361E8 File Offset: 0x000343E8
		public CsvColumnMap(IList<string> columnNames)
		{
			this = new CsvColumnMap(columnNames, true);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000361F4 File Offset: 0x000343F4
		public CsvColumnMap(IList<string> columnNames, bool throwOnDuplicateColumns)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(columnNames.Count, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < columnNames.Count; i++)
			{
				string text = columnNames[i];
				if (!string.IsNullOrEmpty(text))
				{
					if (dictionary.ContainsKey(text))
					{
						if (throwOnDuplicateColumns)
						{
							throw new CsvDuplicatedColumnException(text);
						}
					}
					else
					{
						dictionary[text] = i;
					}
				}
			}
			this.columnNames = columnNames;
			this.columnIndex = dictionary;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0003625C File Offset: 0x0003445C
		public int Count
		{
			get
			{
				return this.columnNames.Count;
			}
		}

		// Token: 0x17000590 RID: 1424
		public string this[int columnIndex]
		{
			get
			{
				return this.columnNames[columnIndex];
			}
		}

		// Token: 0x17000591 RID: 1425
		public int this[string columnName]
		{
			get
			{
				return this.columnIndex[columnName];
			}
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00036285 File Offset: 0x00034485
		public bool Contains(string columnName)
		{
			return this.columnIndex.ContainsKey(columnName);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00036293 File Offset: 0x00034493
		public bool TryGetIndex(string columnName, out int columnIndex)
		{
			return this.columnIndex.TryGetValue(columnName, out columnIndex);
		}

		// Token: 0x04000ADC RID: 2780
		private IList<string> columnNames;

		// Token: 0x04000ADD RID: 2781
		private IDictionary<string, int> columnIndex;
	}
}
