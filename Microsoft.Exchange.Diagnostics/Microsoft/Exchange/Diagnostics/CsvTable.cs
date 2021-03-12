using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001AE RID: 430
	public class CsvTable
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x0002BEC0 File Offset: 0x0002A0C0
		public CsvTable(CsvField[] fields)
		{
			List<CsvField> list = new List<CsvField>();
			this.fields = (CsvField[])fields.Clone();
			for (int i = 0; i < fields.Length; i++)
			{
				this.nameMap.Add(fields[i].Name, i);
				this.nameToTypeMap.Add(fields[i].Name, fields[i].Type);
				if (this.fields[i].IsIndexed)
				{
					list.Add(this.fields[i]);
				}
			}
			this.indexedFields = list.ToArray();
			this.index = new CsvClusteredIndex(0);
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0002BF72 File Offset: 0x0002A172
		public CsvField[] Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002BF7A File Offset: 0x0002A17A
		public CsvField[] IndexedFields
		{
			get
			{
				return this.indexedFields;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002BF82 File Offset: 0x0002A182
		public CsvClusteredIndex ClusteredIndex
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002BF8C File Offset: 0x0002A18C
		public int NameToIndex(string name)
		{
			int result;
			if (!this.nameMap.TryGetValue(name, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002BFAC File Offset: 0x0002A1AC
		public bool TryGetTypeByName(string name, out Type type)
		{
			return this.nameToTypeMap.TryGetValue(name, out type);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002BFBC File Offset: 0x0002A1BC
		public int[] GetFieldsAddedAfterVersion(Version lowerBoundVersion)
		{
			List<int> list = new List<int>(this.fields.Length);
			for (int i = 0; i < this.Fields.Length; i++)
			{
				Version buildAdded = this.fields[i].BuildAdded;
				if (!(buildAdded == null) && !(buildAdded <= lowerBoundVersion))
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002C018 File Offset: 0x0002A218
		public byte[] SerializeFieldNameList(Version highestBuildToInclude)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			for (int i = 0; i < this.fields.Length; i++)
			{
				if (this.Fields[i].BuildAdded <= highestBuildToInclude)
				{
					if (flag)
					{
						stringBuilder.Append(this.fields[i].Name);
						flag = false;
					}
					else
					{
						stringBuilder.AppendFormat(",{0}", this.fields[i].Name);
					}
				}
			}
			stringBuilder.AppendFormat("\r\n", new object[0]);
			return Encoding.ASCII.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x040008B4 RID: 2228
		private CsvField[] fields;

		// Token: 0x040008B5 RID: 2229
		private CsvField[] indexedFields;

		// Token: 0x040008B6 RID: 2230
		private Dictionary<string, int> nameMap = new Dictionary<string, int>();

		// Token: 0x040008B7 RID: 2231
		private Dictionary<string, Type> nameToTypeMap = new Dictionary<string, Type>();

		// Token: 0x040008B8 RID: 2232
		private CsvClusteredIndex index;
	}
}
