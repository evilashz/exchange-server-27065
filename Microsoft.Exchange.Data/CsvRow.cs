using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000207 RID: 519
	internal struct CsvRow
	{
		// Token: 0x06001211 RID: 4625 RVA: 0x00036778 File Offset: 0x00034978
		public CsvRow(int index, IList<string> data, CsvColumnMap columnMap)
		{
			if (data.Count != columnMap.Count)
			{
				throw new CsvWrongNumberOfColumnsException(index, columnMap.Count, data.Count);
			}
			this.index = index;
			this.data = data;
			this.columnMap = columnMap;
			this.errors = new Dictionary<string, PropertyValidationError>();
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x000367C8 File Offset: 0x000349C8
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000367D0 File Offset: 0x000349D0
		public IList<string> Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000367D8 File Offset: 0x000349D8
		public bool IsValid
		{
			get
			{
				return this.errors.Count == 0;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000367E8 File Offset: 0x000349E8
		public IDictionary<string, PropertyValidationError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x000367F0 File Offset: 0x000349F0
		public CsvColumnMap ColumnMap
		{
			get
			{
				return this.columnMap;
			}
		}

		// Token: 0x17000599 RID: 1433
		public string this[string columnName]
		{
			get
			{
				int num = this.columnMap[columnName];
				return this.data[num];
			}
			set
			{
				int num = this.columnMap[columnName];
				this.data[num] = value;
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00036848 File Offset: 0x00034A48
		public bool TryGetColumnValue(string columnName, out string columnValue)
		{
			int num;
			if (this.columnMap.TryGetIndex(columnName, out num))
			{
				columnValue = this.data[num];
				return true;
			}
			columnValue = null;
			return false;
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000369DC File Offset: 0x00034BDC
		public IEnumerable<KeyValuePair<string, string>> GetExistingValues()
		{
			for (int columnIndex = 0; columnIndex < this.columnMap.Count; columnIndex++)
			{
				string columnData = this.data[columnIndex];
				if (!string.IsNullOrEmpty(columnData))
				{
					string columnName = this.columnMap[columnIndex];
					yield return new KeyValuePair<string, string>(columnName, columnData);
				}
			}
			yield break;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000369FE File Offset: 0x00034BFE
		internal void SetError(string columnName, PropertyValidationError error)
		{
			this.errors[columnName] = error;
		}

		// Token: 0x04000AE6 RID: 2790
		private int index;

		// Token: 0x04000AE7 RID: 2791
		private IList<string> data;

		// Token: 0x04000AE8 RID: 2792
		private CsvColumnMap columnMap;

		// Token: 0x04000AE9 RID: 2793
		private IDictionary<string, PropertyValidationError> errors;
	}
}
